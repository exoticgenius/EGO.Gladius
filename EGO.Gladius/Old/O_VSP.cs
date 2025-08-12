using EGO.Gladius.Old;

using System.Diagnostics.CodeAnalysis;

namespace EGO.Gladius.DataTypes;

public struct O_VSP
{
    public static readonly O_VSP Completed = new();

    private bool Success { get; }
    public O_SPF Fault { get; }

    public O_VSP()
    {
        Success = true;
        Fault = default;
    }

    public O_VSP(O_SPF fault)
    {
        Success = false;
        Fault = fault;
    }

    public bool Succeed() => Success;

    public bool Faulted() => !Success;

    public bool Faulted(out O_SPF fault)
    {
        if (!Success)
        {
            fault = Fault;
            return true;
        }

        fault = default;
        return false;
    }

    //public void ThrowIfFaulted()
    //{
    //    if (!Success)
    //        Fault.Throw();
    //}

    #region transform

    public O_SPR<T> Transform<T>([NotNull] Func<T> del)
    {
        try
        {
            if (Succeed())
                return del();

            return Fault;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, e);
        }
    }

    public O_SPR<T> Transform<T>([NotNull] Func<O_SPR<T>> del)
    {
        try
        {
            if (Succeed())
                return del();

            return Fault;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, e);
        }
    }

    public async ValueTask<O_SPR<T>> Transform<T>([NotNull] Func<ValueTask<O_SPR<T>>> del)
    {
        try
        {
            if (Succeed())
                return await del();

            return Fault;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, e);
        }
    }

    public async ValueTask<O_SPR<T>> Transform<T>([NotNull] Func<ValueTask<T>> del)
    {
        try
        {
            if (Succeed())
                return await del();

            return Fault;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, e);
        }
    }

    public async ValueTask<O_SPR<T>> Transform<T>([NotNull] Func<Task<O_SPR<T>>> del)
    {
        try
        {
            if (Succeed())
                return await del();

            return Fault;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, e);
        }
    }

    public async ValueTask<O_SPR<T>> Transform<T>([NotNull] Func<Task<T>> del)
    {
        try
        {
            if (Succeed())
                return await del();

            return Fault;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, e);
        }
    }

    #endregion

    #region transparent

    public O_VSP Transparent([NotNull] Func<O_VSP> del)
    {
        try
        {
            if (Succeed())
                del();

            return this;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, e);
        }
    }

    public async ValueTask<O_VSP> Transparent([NotNull] Func<Task<O_VSP>> del)
    {
        try
        {
            await del();

            return this;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, e);
        }
    }

    public async ValueTask<O_VSP> Transparent([NotNull] Func<ValueTask<O_VSP>> del)
    {
        try
        {
            await del();

            return this;
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, e);
        }
    }

    #endregion

    public static implicit operator O_VSP(in O_SPF fault) =>
        new(fault);

#if Release
    public override string ToString()
    {
        throw new Exception("calling ToString on VSP object is impossible");
    }
#endif
}
