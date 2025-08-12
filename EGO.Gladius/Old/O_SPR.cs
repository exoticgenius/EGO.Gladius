using EGO.Gladius.DataTypes;

using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace EGO.Gladius.Old;

public struct O_SPR
{
    public static readonly O_SPR Completed = new();

    public static O_SPR<T> FromResult<T>(T item) => new(item);

    public bool Succeed() => true;

    public bool Faulted() => false;

    public static O_SPR<T> Gen<T>(T val) => val;

    public static async ValueTask<O_VSP> Gen(Task<O_VSP> val)
    {
        try
        {
            return await val;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(e);
        }
    }

    public static async ValueTask<O_VSP> Gen(Task val)
    {
        try
        {
            await val;

            return O_VSP.Completed;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(e);
        }
    }

    public static async ValueTask<O_VSP> Gen(ValueTask val)
    {
        try
        {
            await val;

            return O_VSP.Completed;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(e);
        }
    }

    public static async ValueTask<O_VSP> Gen(ValueTask<O_VSP> val)
    {
        try
        {
            return await val;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(e);
        }
    }

    public static async Task<O_SPR<T>> Run<T>([NotNull] Func<Task<T>> del)
    {
        try
        {
            return await del();
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, e);
        }
    }

    public static async Task<O_SPR<T>> InsistAsync<T>([NotNull] Func<Task<T>> source, int tryFor)
    {
        Exception caught = null;
        while (tryFor != 0)
        {
            try
            {
                --tryFor;
                return await source();
            }
            catch (Exception e)
            {
                caught = e;
            }
        }

        return O_SPF.Gen(caught);
    }

    public static async Task<O_SPR<T>> InsistAsync<T>([NotNull] Func<Task<O_SPR<T>>> source, int tryFor)
    {
        O_SPF? lastSPF = null;

        while (tryFor != 0)
        {
            --tryFor;
            O_SPR<T> res = await source();

            if (res.Succeed())
                return res;

            lastSPF = res.Fault;
        }

        return lastSPF ?? O_SPF.Gen("fault running source");
    }

    public static async Task<O_SPR<T>> InsistAsync<T>([NotNull] Func<Task<O_SPR<T>>> source, CancellationToken ct)
    {
        O_SPF? lastSPF = null;

        while (!ct.IsCancellationRequested)
        {
            O_SPR<T> res = await source();

            if (res.Succeed())
                return res;

            lastSPF = res.Fault;
        }

        return lastSPF ?? O_SPF.Gen("fault running source");
    }

    public static async Task<O_SPR<T>> InsistAsync<T>([NotNull] Func<Task<O_SPR<T>>> source, TimeSpan timeout)
    {
        O_SPF? lastSPF = null;
        Stopwatch sw = Stopwatch.StartNew();

        while (sw.Elapsed < timeout)
        {
            O_SPR<T> res = await source();

            if (res.Succeed())
                return res;

            lastSPF = res.Fault;
        }

        sw.Stop();

        return lastSPF ?? O_SPF.Gen("fault running source");
    }
}

/// <summary>
/// super position result,
/// equivalents to a maybe result that can contain result data or exception at the same time,
/// and is not determinable until result qualification happens
/// </summary>
[DebuggerDisplay("{DebuggerPreview}")]
public struct O_SPR<T>
{
    public static readonly O_SPR<T> Completed = new(default(T)!);
    private O_SPV<T> Value { get; }
    public O_SPF Fault { get; }

    public O_SPR(T val)
    {
        Value = new(val);
        Fault = default;
    }

    public O_SPR(O_SPF fault)
    {
        Value = default;
        Fault = fault;
    }

    internal O_SPR(O_SPV<T> val, O_SPF fault)
    {
        Value = val;
        Fault = fault;
    }

    #region suppress fault

    public O_SPR<T> SuppressFault([NotNull] Func<O_SPR<T>, O_SPR<T>> del)
    {
        if (Succeed())
            return this;

        return Transform(del);
    }

    public async ValueTask<O_SPR<T>> SuppressFault([NotNull] Func<O_SPR<T>, ValueTask<O_SPR<T>>> del)
    {
        if (Succeed())
            return this;

        return await Transform(del);
    }

    #endregion

    #region suppress null

    public O_SPR<T> SuppressNull([NotNull] Func<T> del)
    {
        if (Faulted())
            return this;

        if (HasValue())
            return this;

        try
        {
            return del();
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, e);
        }
    }

    public O_SPR<T> SuppressNull([NotNull] Func<O_SPR<T>> del)
    {
        if (Faulted())
            return this;

        if (HasValue())
            return this;

        try
        {
            return del();
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, e);
        }
    }

    public async ValueTask<O_SPR<T>> SuppressNull([NotNull] Func<ValueTask<O_SPR<T>>> del)
    {
        try
        {
            if (Faulted())
                return this;

            if (HasValue())
                return this;

            return await del();
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, e);
        }
    }

    #endregion

    #region transform

    public O_SPR<R> Transform<R>([NotNull] Func<T, R> del)
    {
        try
        {
            if (Succeed())
                return del(Value.Payload);

            return Fault;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    public O_SPR<R> Transform<R>([NotNull] Func<T, O_SPR<R>> del)
    {
        try
        {
            if (Succeed())
                return del(Value.Payload);

            return Fault;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    private O_SPR<R> Transform<R>([NotNull] Func<O_SPR<T>, O_SPR<R>> del)
    {
        try
        {
            if (Succeed())
                return del(this);

            return Fault;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    public async ValueTask<O_SPR<R>> Transform<R>([NotNull] Func<T, ValueTask<O_SPR<R>>> del)
    {
        try
        {
            if (Succeed())
                return await del(Value.Payload);

            return Fault;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    public async ValueTask<O_SPR<R>> Transform<R>([NotNull] Func<O_SPR<T>, ValueTask<O_SPR<R>>> del)
    {
        try
        {
            if (Succeed())
                return await del(this);

            return Fault;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    public async ValueTask<O_SPR<R>> Transform<R>([NotNull] Func<T, ValueTask<R>> del)
    {
        try
        {
            if (Succeed())
                return await del(Value.Payload);

            return Fault;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    public async ValueTask<O_SPR<R>> Transform<R>([NotNull] Func<T, Task<O_SPR<R>>> del)
    {
        try
        {
            if (Succeed())
                return await del(Value.Payload);

            return Fault;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    public async ValueTask<O_SPR<R>> Transform<R>([NotNull] Func<T, Task<R>> del)
    {
        try
        {
            if (Succeed())
                return await del(Value.Payload);

            return Fault;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    #endregion

    #region transparent

    public O_SPR<T> Transparent([NotNull] Action<T?> del)
    {
        try
        {
            if (Succeed(out T? res))
                del(res);
            else
                del(default);

            return this;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    public async ValueTask<O_SPR<T>> Transparent([NotNull] Func<T?, Task> del)
    {
        try
        {
            if (Succeed(out T? res))
                await del(res);
            else
                await del(default);

            return this;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    public async ValueTask<O_SPR<T>> Transparent([NotNull] Func<T?, ValueTask> del)
    {
        try
        {
            if (Succeed(out T? res))
                await del(res);
            else
                await del(default);

            return this;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    public O_SPR<T> Transparent([NotNull] Action del)
    {
        try
        {
            del();

            return this;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    public async ValueTask<O_SPR<T>> Transparent([NotNull] Func<Task> del)
    {
        try
        {
            await del();

            return this;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    public async ValueTask<O_SPR<T>> Transparent([NotNull] Func<ValueTask> del)
    {
        try
        {
            await del();

            return this;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    #endregion

    #region to vsp

    private O_VSP ToVSP([NotNull] Func<O_SPR<T>, O_VSP> del)
    {
        try
        {
            if (Succeed())
                return del(Value.Payload);

            return Fault;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    public O_VSP ToVSP([NotNull] Func<T, O_VSP> del)
    {
        try
        {
            if (Succeed())
                return del(Value.Payload);

            return Fault;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    #endregion to vsp

    public O_DSPR<T> MarkDispose(int index) =>
        new O_DSPR<T>(this).MarkDispose(index);

    public O_DSPR<T> MarkDispose<E>(E index) where E : Enum =>
        new O_DSPR<T>(this).MarkDispose(index);

    public bool Succeed() => Value.Completed;

    public bool Succeed(out T result)
    {
        if (Value.Completed)
        {
            result = Value.Payload;
            return true;
        }

        result = default!;
        return false;
    }

    public bool Succeed(out T result, out O_SPF fault)
    {
        fault = Fault;

        if (Value.Completed)
        {
            result = Value.Payload;
            return true;
        }

        result = default!;
        return false;
    }
    public bool Faulted() => !Value.Completed;

    public bool Faulted(out O_SPF fault)
    {
        if (!Value.Completed)
        {
            fault = Fault;
            return true;
        }

        fault = default;
        return false;
    }

    public bool HasValue() =>
        Value.HasValue();

    public object? ExtractPayload()
    {
        if (Succeed(out T? result))
            return result;

        return Fault;
    }

    public static implicit operator O_SPR<T>(in T val) =>
        new(val);

    public static implicit operator O_SPR<T>(in O_SPF fault) =>
        new(fault);

    public static implicit operator O_SPR<T>(in O_SPR tag) =>
        new(default(T)!);

#if Release
    public override string ToString()
    {
        throw new Exception("calling ToString on SPR<> object is impossible");
    }
#endif

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    private object? DebuggerPreview
    {
        get
        {
            if (!Value.Completed)
                return Fault.Message ??
                    Fault.Exception?.Message ??
                    "result faulted";

            return Value.Payload;
        }
    }
}
