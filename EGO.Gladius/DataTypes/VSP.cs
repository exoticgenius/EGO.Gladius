using EGO.Gladius.Contracts;

using System.Diagnostics.CodeAnalysis;

namespace EGO.Gladius.DataTypes;

public struct VSP : ISP
{
    public static readonly VSP Completed = new();

    private bool Success { get; }
    public SPF Fault { get; }

    public VSP()
    {
        Success = true;
        Fault = default;
    }

    public VSP(SPF fault)
    {
        Success = false;
        Fault = fault;
    }

    public bool Succeed() => Success;

    public bool Faulted() => !Success;

    public bool Faulted(out SPF fault)
    {
        if (!Success)
        {
            fault = Fault;
            return true;
        }

        fault = default;
        return false;
    }

    public void ThrowIfFaulted()
    {
        if (!Success)
            Fault.Throw();
    }

    #region transform

    public SPR<T> Transform<T>([NotNull] Func<T> del)
    {
        try
        {
            if (Succeed())
                return del();

            return Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, e);
        }
    }

    public SPR<T> Transform<T>([NotNull] Func<SPR<T>> del)
    {
        try
        {
            if (Succeed())
                return del();

            return Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, e);
        }
    }

    public async ValueTask<SPR<T>> Transform<T>([NotNull] Func<ValueTask<SPR<T>>> del)
    {
        try
        {
            if (Succeed())
                return await del();

            return Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, e);
        }
    }

    public async ValueTask<SPR<T>> Transform<T>([NotNull] Func<ValueTask<T>> del)
    {
        try
        {
            if (Succeed())
                return await del();

            return Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, e);
        }
    }

    public async ValueTask<SPR<T>> Transform<T>([NotNull] Func<Task<SPR<T>>> del)
    {
        try
        {
            if (Succeed())
                return await del();

            return Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, e);
        }
    }

    public async ValueTask<SPR<T>> Transform<T>([NotNull] Func<Task<T>> del)
    {
        try
        {
            if (Succeed())
                return await del();

            return Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, e);
        }
    }

    #endregion

    #region transparent

    public VSP Transparent([NotNull] Func<VSP> del)
    {
        try
        {
            if (Succeed())
                del();

            return this;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, e);
        }
    }

    public async ValueTask<VSP> Transparent([NotNull] Func<Task<VSP>> del)
    {
        try
        {
            await del();

            return this;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, e);
        }
    }

    public async ValueTask<VSP> Transparent([NotNull] Func<ValueTask<VSP>> del)
    {
        try
        {
            await del();

            return this;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, e);
        }
    }

    #endregion

    public static implicit operator VSP(in SPF fault) =>
        new(fault);

#if Release
    public override string ToString()
    {
        throw new Exception("calling ToString on VSP object is impossible");
    }
#endif
}
