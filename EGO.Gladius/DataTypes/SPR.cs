using EGO.Gladius.Contracts;

using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace EGO.Gladius.DataTypes;

[DebuggerDisplay("{DebuggerPreview}")]
public struct SPR : ISP
{
    public readonly static SPR Completed = new();
    private readonly static SPF _fault = new();

    public SPF Fault => _fault;

    public static SPR<T> FromResult<T>(T item) => new(item);

    public bool Succeed() => true;

    public bool Faulted() => false;

    public static SPR<T> Gen<T>(T val) => new(val);

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

            return Completed;
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

            return Completed;
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

    public static async ValueTask<SPR<T>> Run<T>([NotNull] Func<Task<T>> del)
    {
        try
        {
            return new SPR<T>(await del());
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, e);
        }
    }

    public static async ValueTask<SPR<T>> Run<T>([NotNull] Func<ValueTask<T>> del)
    {
        try
        {
            return new SPR<T>(await del());
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, e);
        }
    }

    public static async Task<SPR<T>> TryFor<T>([NotNull] Func<Task<T>> source, int tryFor)
    {
        Exception? caught = null;
        while (tryFor != 0)
        {
            try
            {
                --tryFor;
                return new SPR<T>(await source());
            }
            catch (Exception e)
            {
                caught = e;
            }
        }

        return SPF.Gen(caught!);
    }

    public static async Task<SPR<T>> TryFor<T>([NotNull] Func<Task<SPR<T>>> source, int tryFor)
    {
        SPF? lastSPF = null;

        while (tryFor != 0)
        {
            --tryFor;
            SPR<T> res = await source();

            if (res.Succeed())
                return res;

            lastSPF = res.Fault;
        }

        return lastSPF ?? SPF.Gen("fault running source");
    }

    public static async Task<SPR<T>> TryFor<T>([NotNull] Func<Task<SPR<T>>> source, CancellationToken ct)
    {
        SPF? lastSPF = null;

        while (!ct.IsCancellationRequested)
        {
            SPR<T> res = await source();

            if (res.Succeed())
                return res;

            lastSPF = res.Fault;
        }

        return lastSPF ?? SPF.Gen("fault running source");
    }

    public static async Task<SPR<T>> TryFor<T>([NotNull] Func<Task<SPR<T>>> source, TimeSpan timeout)
    {
        SPF? lastSPF = null;
        Stopwatch sw = Stopwatch.StartNew();

        while (sw.Elapsed < timeout)
        {
            SPR<T> res = await source();

            if (res.Succeed())
                return res;

            lastSPF = res.Fault;
        }

        sw.Stop();

        return lastSPF ?? SPF.Gen("fault running source");
    }

    public static SPR<T[]> WhenAll<T>(IEnumerable<SPR<T>> values)
    {
        List<Exception> faults = [];
        List<T> results = [];

        foreach (var item in values)
        {
            try
            {
                item.ThrowIfFaulted(out var val);

                results.Add(val);
            }
            catch(Exception e)
            {
                faults.Add(e);
            }
        }

        if(faults.Count > 0) return SPF.Gen(new AggregateException(faults));

        return results.ToArray();
    }

    #region utils
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    private object? DebuggerPreview
    {
        get
        {
            if (!Succeed())
                return Fault.Message ??
                    Fault.Exception?.Message ??
                    "Operation Faulted";

            return "Successfuly Executed";
        }
    }

    public override string ToString()
    {
        throw new Exception("Calling ToStringon ISP object is impossible");
    }

    public void ThrowIfFaulted()
    {
        
    }

    #endregion utils
}

[DebuggerDisplay("{DebuggerPreview}")]
public struct SPR<T> : ISP<T>, ISPRDescendable<T>, ISPRVoidable<VSP>
{
    #region props
    public SPF Fault { get; }
    internal SPV<T> Value { get; }
    #endregion props

    #region ctors
    public SPR()
    {
    }
    public SPR(SPF fault)
    {
        Value = default;
        Fault = fault;
    }
    internal SPR(T payload)
    {
        Value = new SPV<T>(payload);
        Fault = default;
    }
    internal SPR(SPV<T> val, SPF fault)
    {
        Value = val;
        Fault = fault;
    }
    internal SPR(T payload, SPF fault)
    {
        Value = new SPV<T>(payload);
        Fault = fault;
    }
    #endregion ctors

    #region core funcs
    public T Descend() =>
        Succeed() ?
        Value.Payload :
        throw Fault.GenSPFE();
    public VSP Void() => new(Succeed(), Fault);
    public bool HasValue() => Value.HasValue();

    public bool Succeed() => Value.Completed;
    public bool Faulted() => !Value.Completed;

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

    public void ThrowIfFaulted(out T result)
    {
        if (Faulted())
            Fault.Throw();

        result = Value.Payload;
    }

    public void ThrowIfFaulted()
    {
        if (Faulted())
            Fault.Throw();
    }

    #endregion core funcs

    #region Operators
    public static implicit operator SPR<T>(in SPR tag) =>
        new(new SPV<T>(true), default);
    public static implicit operator SPR<T>(in T val) =>
        new(val, default);

    public static implicit operator SPR<T>(in SPF fault) =>
        new(fault);

    public static implicit operator SPR<T>(in Exception e) =>
        new(SPF.Gen(e));

    #endregion Operators

    #region utils
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    private object? DebuggerPreview
    {
        get
        {
            if (!Succeed(out T? val))
                return Fault.Message ??
                    Fault.Exception?.Message ??
                    "Result Faulted";

            return val;
        }
    }

#if Release
    public override string ToString()
    {
        throw new Exception("Calling ToStringon ISP object is impossible");
    }
#endif
    #endregion utils
}