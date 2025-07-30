using EGO.Gladius.Contracts;

using System.Transactions;

namespace EGO.Gladius.DataTypes;

public struct N_SPR<T> : ISP, ISPRConvertible<T>
{
    public N_SPF Fault { get; }
    private N_SPV<T> Value { get; }
    public N_SPR()
    {
    }
    internal N_SPR(N_SPV<T> val, N_SPF fault)
    {
        Value = val;
        Fault = fault;
    }


    public T Descend() =>
        Succeed() ?
        Value.Payload :
        throw Fault.GenSPFE();

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

    public bool Faulted() => !Value.Completed;
    public bool Faulted(out N_SPF fault)
    {
        if (!Value.Completed)
        {
            fault = Fault;
            return true;
        }

        fault = default;
        return false;
    }
}


public struct N_DSPR<T> : IDSP<T, N_DSPR<T>, N_SPR<T>>, ISPRConvertible<N_SPR<T>>
{
    List<KeyValuePair<short, IDisposable>>? IDSP<T, N_DSPR<T>, N_SPR<T>>.Disposables { get; set; }
    List<KeyValuePair<short, IAsyncDisposable>>? IDSP<T, N_DSPR<T>, N_SPR<T>>.AsyncDisposables { get; set; }

    public N_SPF Fault { get; set; }
    public N_SPV<T> Value { get; set; }

    public N_DSPR()
    {
    }

    public N_SPR<T> Descend() =>
        DisposeAll();

    #region disposal
    public N_DSPR<T> MarkDispose(short index = 0)
    {
        ((IDSP<T, N_DSPR<T>, N_SPR<T>>)this).MarkDispose(index);

        return this;
    }

    public N_DSPR<T> Dispose(short index = -1)
    {
        ((IDSP<T, N_DSPR<T>, N_SPR<T>>)this).Dispose(index);

        return this;
    }

    public N_SPR<T> DisposeAll()
    {
        ((IDSP<T, N_DSPR<T>, N_SPR<T>>)this).DisposeAll();

        return new N_SPR<T>(Value, Fault);
    }

    #endregion disposal
}


public struct N_TSPR<T> : ITSP<T, N_TSPR<T>>, ISPRConvertible<T>
{
    List<KeyValuePair<short, TransactionScope>>? ITSP<T, N_TSPR<T>>.Transactions { get; set; }

    public N_SPV<T> Value { get; set; }
    public N_SPF Fault { get; set; }

    public N_TSPR()
    {
    }
    internal N_TSPR(N_SPV<T> val, N_SPF fault)
    {
        Value = val;
        Fault = fault;
    }

    public T Descend() =>
        Succeed() ?
        Value.Payload :
        throw Fault.GenSPFE();

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

    #region transactional
    public N_TSPR<T> MarkScope(short index = 0)
    {
        ((ITSP<T, N_TSPR<T>>)this).InternalMarkScope(index);

        return this;
    }

    public N_TSPR<T> CompleteScope(short index = -1)
    {
        ((ITSP<T, N_TSPR<T>>)this).InternalCompleteScope(index);

        return this;
    }

    public N_TSPR<T> DisposeScope(short index = -1)
    {
        ((ITSP<T, N_TSPR<T>>)this).InternalDisposeScope(index);

        return this;
    }
    #endregion transactional

}


public struct N_TDSPR<T> : ITSP<T, N_TDSPR<T>>, IDSP<T, N_TDSPR<T>, N_TSPR<T>>, ISPRConvertible<T>
{
    List<KeyValuePair<short, IDisposable>>? IDSP<T, N_TDSPR<T>, N_TSPR<T>>.Disposables { get; set; }
    List<KeyValuePair<short, IAsyncDisposable>>? IDSP<T, N_TDSPR<T>, N_TSPR<T>>.AsyncDisposables { get; set; }
    List<KeyValuePair<short, TransactionScope>>? ITSP<T, N_TDSPR<T>>.Transactions { get; set; }

    public N_SPV<T> Value { get; set; }
    public N_SPF Fault { get; set; }

    public N_TDSPR()
    {
    }
    internal N_TDSPR(N_SPV<T> val, N_SPF fault)
    {
        Value = val;
        Fault = fault;
    }

    public T Descend() =>
        Succeed() ?
        Value.Payload :
        throw Fault.GenSPFE();

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

    #region disposal
    public N_TDSPR<T> MarkDispose(short index = 0)
    {
        ((IDSP<T, N_TDSPR<T>, N_TSPR<T>>)this).MarkDispose(index);

        return this;
    }

    public N_TDSPR<T> Dispose(short index = -1)
    {
        ((IDSP<T, N_TDSPR<T>, N_TSPR<T>>)this).Dispose(index);

        return this;
    }

    public N_TSPR<T> DisposeAll()
    {
        ((IDSP<T, N_TDSPR<T>, N_TSPR<T>>)this).DisposeAll();

        return new N_TSPR<T>(Value, Fault);
    }
    #endregion disposal

    #region transactional
    public N_TDSPR<T> MarkScope(short index = 0)
    {
        ((ITSP<T, N_TDSPR<T>>)this).InternalMarkScope(index);

        return this;
    }

    public N_TDSPR<T> CompleteScope(short index = -1)
    {
        ((ITSP<T, N_TDSPR<T>>)this).InternalCompleteScope(index);

        return this;
    }

    public N_TDSPR<T> DisposeScope(short index = -1)
    {
        ((ITSP<T, N_TDSPR<T>>)this).InternalDisposeScope(index);

        return this;
    }
    #endregion transactional
}