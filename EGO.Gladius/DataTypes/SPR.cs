using EGO.Gladius.Contracts;

using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace EGO.Gladius.DataTypes;

public struct SPR : ISP
{
    public static readonly SPR Completed = new();

    public static SPR<T> FromResult<T>(T item) => new(item);

    public bool Succeed() => true;

    public bool Faulted() => false;

    public static SPR<T> Gen<T>(T val) => val;

    public static VSP Gen(VSP val) => val;

    public static async ValueTask<VSP> Gen(Task<VSP> val)
    {
        try
        {
            return await val;
        }
        catch (Exception e)
        {
            return SPF.Gen(e);
        }
    }

    public static async ValueTask<VSP> Gen(Task val)
    {
        try
        {
            await val;

            return VSP.Completed;
        }
        catch (Exception e)
        {
            return SPF.Gen(e);
        }
    }

    public static async ValueTask<VSP> Gen(ValueTask val)
    {
        try
        {
            await val;

            return VSP.Completed;
        }
        catch (Exception e)
        {
            return SPF.Gen(e);
        }
    }

    public static async ValueTask<VSP> Gen(ValueTask<VSP> val)
    {
        try
        {
            return await val;
        }
        catch (Exception e)
        {
            return SPF.Gen(e);
        }
    }

    public static async Task<SPR<T>> Run<T>([NotNull] Func<Task<T>> del)
    {
        try
        {
            return await del();
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, e);
        }
    }

    public static async Task<SPR<T>> InsistAsync<T>([NotNull] Func<Task<T>> source, int tryFor)
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

        return SPF.Gen(caught);
    }

    public static async Task<SPR<T>> InsistAsync<T>([NotNull] Func<Task<SPR<T>>> source, int tryFor)
    {
        SPF? lastSPF = null;

        while (tryFor != 0)
        {
            --tryFor;
            var res = await source();

            if (res.Succeed())
                return res;

            lastSPF = res.Fault;
        }

        return lastSPF ?? SPF.Gen("fault running source");
    }

    public static async Task<SPR<T>> InsistAsync<T>([NotNull] Func<Task<SPR<T>>> source, CancellationToken ct)
    {
        SPF? lastSPF = null;

        while (!ct.IsCancellationRequested)
        {
            var res = await source();

            if (res.Succeed())
                return res;

            lastSPF = res.Fault;
        }

        return lastSPF ?? SPF.Gen("fault running source");
    }

    public static async Task<SPR<T>> InsistAsync<T>([NotNull] Func<Task<SPR<T>>> source, TimeSpan timeout)
    {
        SPF? lastSPF = null;
        var sw = Stopwatch.StartNew();

        while (sw.Elapsed < timeout)
        {
            var res = await source();

            if (res.Succeed())
                return res;

            lastSPF = res.Fault;
        }

        sw.Stop();

        return lastSPF ?? SPF.Gen("fault running source");
    }
}

/// <summary>
/// super position result,
/// equivalents to a maybe result that can contain result data or exception at the same time,
/// and is not determinable until result qualification happens
/// </summary>
[DebuggerDisplay("{DebuggerPreview}")]
public struct SPR<T> : ISP, INeutralSPR
{
    public static readonly SPR<T> Completed = new(default(T)!);
    private SPV<T> Value { get; }
    public SPF Fault { get; }

    public SPR(T val)
    {
        Value = new(val);
        Fault = default;
    }

    public SPR(SPF fault)
    {
        Value = default;
        Fault = fault;
    }

    #region suppress fault

    public SPR<T> SuppressFault([NotNull] Func<SPR<T>, SPR<T>> del)
    {
        if (Succeed())
            return this;

        return Transform(del);
    }

    public async ValueTask<SPR<T>> SuppressFault([NotNull] Func<SPR<T>, ValueTask<SPR<T>>> del)
    {
        if (Succeed())
            return this;

        return await Transform(del);
    }

    #endregion

    #region suppress null

    public SPR<T> SuppressNull([NotNull] Func<T> del)
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
            return SPF.Gen(del.Method, e);
        }
    }

    public SPR<T> SuppressNull([NotNull] Func<SPR<T>> del)
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
            return SPF.Gen(del.Method, e);
        }
    }

    public async ValueTask<SPR<T>> SuppressNull([NotNull] Func<ValueTask<SPR<T>>> del)
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
            return SPF.Gen(del.Method, e);
        }
    }

    #endregion

    #region transform

    public SPR<R> Transform<R>([NotNull] Func<T, R> del)
    {
        try
        {
            if (Succeed())
                return del(Value.Payload);

            return Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    public SPR<R> Transform<R>([NotNull] Func<T, SPR<R>> del)
    {
        try
        {
            if (Succeed())
                return del(Value.Payload);

            return Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    private SPR<R> Transform<R>([NotNull] Func<SPR<T>, SPR<R>> del)
    {
        try
        {
            if (Succeed())
                return del(Value.Payload);

            return Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    public async ValueTask<SPR<R>> Transform<R>([NotNull] Func<T, ValueTask<SPR<R>>> del)
    {
        try
        {
            if (Succeed())
                return await del(Value.Payload);

            return Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    public async ValueTask<SPR<R>> Transform<R>([NotNull] Func<SPR<T>, ValueTask<SPR<R>>> del)
    {
        try
        {
            if (Succeed())
                return await del(this);

            return Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    public async ValueTask<SPR<R>> Transform<R>([NotNull] Func<T, ValueTask<R>> del)
    {
        try
        {
            if (Succeed())
                return await del(Value.Payload);

            return Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    public async ValueTask<SPR<R>> Transform<R>([NotNull] Func<T, Task<SPR<R>>> del)
    {
        try
        {
            if (Succeed())
                return await del(Value.Payload);

            return Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    public async ValueTask<SPR<R>> Transform<R>([NotNull] Func<T, Task<R>> del)
    {
        try
        {
            if (Succeed())
                return await del(Value.Payload);

            return Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    #endregion

    #region transparent

    public SPR<T> Transparent([NotNull] Action<T?> del)
    {
        try
        {
            if (Succeed(out var res))
                del(res);
            else
                del(default);

            return this;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    public async ValueTask<SPR<T>> Transparent([NotNull] Func<T?, Task> del)
    {
        try
        {
            if (Succeed(out var res))
                await del(res);
            else
                await del(default);

            return this;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    public async ValueTask<SPR<T>> Transparent([NotNull] Func<T?, ValueTask> del)
    {
        try
        {
            if (Succeed(out var res))
                await del(res);
            else
                await del(default);

            return this;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    public SPR<T> Transparent([NotNull] Action del)
    {
        try
        {
            del();

            return this;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    public async ValueTask<SPR<T>> Transparent([NotNull] Func<Task> del)
    {
        try
        {
            await del();

            return this;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    public async ValueTask<SPR<T>> Transparent([NotNull] Func<ValueTask> del)
    {
        try
        {
            await del();

            return this;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    #endregion

    #region to vsp

    private VSP ToVSP([NotNull] Func<SPR<T>, VSP> del)
    {
        try
        {
            if (Succeed())
                return del(Value.Payload);

            return Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    public VSP ToVSP([NotNull] Func<T, VSP> del)
    {
        try
        {
            if (Succeed())
                return del(Value.Payload);

            return Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [Value.Payload], e);
        }
    }

    #endregion to vsp

    public DSPR<T> MarkDispose(int index) =>
        new DSPR<T>(this).MarkDispose(index);

    public DSPR<T> MarkDispose<E>(E index) where E : Enum =>
        new DSPR<T>(this).MarkDispose(index);

    public DSPR<T> MarkScope(int index) =>
        new DSPR<T>(this).MarkScope(index);

    public DSPR<T> MarkScope<E>(E index) where E : Enum =>
        new DSPR<T>(this).MarkScope(index);

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

    public bool Succeed(out T result, out SPF fault)
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

    public bool Faulted(out SPF fault)
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
        if (Succeed(out var result))
            return result;

        return Fault;
    }

    public static implicit operator SPR<T>(in T val) =>
        new(val);

    public static implicit operator SPR<T>(in SPF fault) =>
        new(fault);

    public static implicit operator SPR<T>(in SPR tag) =>
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
