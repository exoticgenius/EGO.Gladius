using EGO.Gladius.Contracts;

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace EGO.Gladius.DataTypes;

[DebuggerDisplay("{DebuggerPreview}")]
public struct SPR : ISP
{
    public readonly static SPR Completed = new();
    private readonly static SPF _fault = new SPF();

    public SPF Fault => _fault;

    public static SPR<T> FromResult<T>(T item) => new(item);

    public bool Succeed() => true;

    public bool Faulted() => false;

    public static SPR<T> Gen<T>(T val) => new SPR<T>(val);

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
        Exception caught = null;
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

        return SPF.Gen(caught);
    }

    public static async Task<SPR<T>> TryFor<T>([NotNull] Func<Task<SPR<T>>> source, int tryFor)
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

    public static async Task<SPR<T>> TryFor<T>([NotNull] Func<Task<SPR<T>>> source, CancellationToken ct)
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

    public static async Task<SPR<T>> TryFor<T>([NotNull] Func<Task<SPR<T>>> source, TimeSpan timeout)
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

    #endregion utils
}

[DebuggerDisplay("{DebuggerPreview}")]
public struct SPR<T> : ISP<T>, ISPRDescendable<T>, ISPRVoidable<VSP>
{
    #region props
    private SPV<T> _value;
    public SPF Fault { get; }
    SPV<T> ISP<T>.Value => _value;
    #endregion props

    #region ctors
    public SPR()
    {
    }
    internal SPR(SPF fault)
    {
        _value = default;
        Fault = fault;
    }
    internal SPR(T payload)
    {
        _value = new SPV<T>(payload);
        Fault = default;
    }
    internal SPR(SPV<T> val, SPF fault)
    {
        _value = val;
        Fault = fault;
    }
    internal SPR(T payload, SPF fault)
    {
        _value = new SPV<T>(payload);
        Fault = fault;
    }
    #endregion ctors

    #region core funcs
    public T Descend() =>
        Succeed() ?
        ((ISP<T>)this).Value.Payload :
        throw Fault.GenSPFE();
    public VSP Void() => new VSP(Succeed(), Fault);
    public bool HasValue() => _value.HasValue();

    public bool Succeed() => ((ISP<T>)this).Value.Completed;
    public bool Faulted() => !((ISP<T>)this).Value.Completed;
    #endregion core funcs

    #region Operators
    //public static implicit operator N_SPR<T>(in T val) =>
    //    new(val, default);

    public static implicit operator SPR<T>(in SPF fault) =>
        new(fault);
    #endregion Operators

    #region utils
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    private object? DebuggerPreview
    {
        get
        {
            if (!((ISP<T>)this).Succeed(out var val))
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