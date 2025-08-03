using EGO.Gladius.Contracts;

using System;
using System.Transactions;

namespace EGO.Gladius.DataTypes;

public struct N_SPR<T> : ISP<T>, ISPRConvertible<T>
{
    #region props
    N_SPV<T> ISP<T>.Value { get; set; }
    public N_SPF Fault { get; }
    #endregion props

    #region ctors
    public N_SPR()
    {
    }
    internal N_SPR(N_SPF fault)
    {
        ((ISP<T>)this).Value = default;
        Fault = fault;
    }
    internal N_SPR(T payload)
    {
        ((ISP<T>)this).Value = new N_SPV<T>(payload);
        Fault = default;
    }
    internal N_SPR(N_SPV<T> val, N_SPF fault)
    {
        ((ISP<T>)this).Value = val;
        Fault = fault;
    }
    internal N_SPR(T payload, N_SPF fault)
    {
        ((ISP<T>)this).Value = new N_SPV<T>(payload);
        Fault = fault;
    }
    #endregion ctors

    #region core funcs
    public T Descend() =>
        Succeed() ?
        ((ISP<T>)this).Value.Payload :
        throw Fault.GenSPFE();

    //public N_SPR<X> Pass<X>(X val) =>
    //    new N_SPR<X>(
    //        new N_SPV<X>(val),
    //        Fault);

    public bool Succeed() => ((ISP<T>)this).Value.Completed;
    public bool Succeed(out T result)
    {
        if (((ISP<T>)this).Value.Completed)
        {
            result = ((ISP<T>)this).Value.Payload;
            return true;
        }

        result = default!;
        return false;
    }

    public bool Faulted() => !((ISP<T>)this).Value.Completed;
    public bool Faulted(out N_SPF fault)
    {
        if (!((ISP<T>)this).Value.Completed)
        {
            fault = Fault;
            return true;
        }

        fault = default;
        return false;
    }
    #endregion core funcs

    #region Operators
    //public static implicit operator N_SPR<T>(in T val) =>
    //    new(val, default);

    public static implicit operator N_SPR<T>(in N_SPF fault) =>
        new(fault);


#if Release
    public override string ToString()
    {
        throw new Exception("calling ToString on SPR<> object is impossible");
    }
#endif
    #endregion Operators
}

public struct N_DSPR<T> : IDSP<T, N_DSPR<T>, N_SPR<T>>, ISPRConvertible<N_SPR<T>>
{
    #region props
    List<KeyValuePair<short, IDisposable>>? IDSP.Disposables { get; set; }
    List<KeyValuePair<short, IAsyncDisposable>>? IDSP.AsyncDisposables { get; set; }

    public N_SPF Fault { get; set; }
    N_SPV<T> ISP<T>.Value { get; set; }
    #endregion props

    #region ctors
    public N_DSPR()
    {
    }

    public N_DSPR(
        N_SPV<T> value,
        N_SPF fault,
        List<KeyValuePair<short, IDisposable>>? disposables,
        List<KeyValuePair<short, IAsyncDisposable>>? asyncDisposables)
    {
        ((ISP<T>)this).Value = value;
        Fault = fault;
        ((IDSP<T, N_DSPR<T>, N_SPR<T>>)this).Disposables = disposables;
        ((IDSP<T, N_DSPR<T>, N_SPR<T>>)this).AsyncDisposables = asyncDisposables;
    }
    #endregion ctors

    #region core funcs
    public N_SPR<T> Descend() =>
        DisposeAll();
    public N_DSPR<X> Pass<X>(X val) =>
        new N_DSPR<X>(
            new N_SPV<X>(val),
            Fault,
            ((IDSP)this).Disposables,
            ((IDSP)this).AsyncDisposables);

    public bool Succeed() => ((ISP<T>)this).Value.Completed;
    public bool Succeed(out T result)
    {
        if (((ISP<T>)this).Value.Completed)
        {
            result = ((ISP<T>)this).Value.Payload;
            return true;
        }

        result = default!;
        return false;
    }

    public bool Faulted() => !((ISP<T>)this).Value.Completed;
    public bool Faulted(out N_SPF fault)
    {
        if (!((ISP<T>)this).Value.Completed)
        {
            fault = Fault;
            return true;
        }

        fault = default;
        return false;
    }
    #endregion core funcs

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

        return new N_SPR<T>(((ISP<T>)this).Value, Fault);
    }
    #endregion disposal
}

public struct N_TSPR<T> : ITSP<T, N_TSPR<T>, N_SPR<T>>, ISPRConvertible<N_SPR<T>>
{
    #region props
    List<KeyValuePair<short, TransactionScope>>? ITSP.Transactions { get; set; }

    N_SPV<T> ISP<T>.Value { get; set; }
    public N_SPF Fault { get; set; }
    #endregion props

    #region ctors
    public N_TSPR()
    {
    }
    internal N_TSPR(
        N_SPV<T> val,
        N_SPF fault,
        List<KeyValuePair<short, TransactionScope>>? transactions)
    {
        ((ISP<T>)this).Value = val;
        Fault = fault;
        ((ITSP<T, N_TSPR<T>, N_SPR<T>>)this).Transactions = transactions;
    }
    #endregion ctors

    #region core funcs
    public N_SPR<T> Descend() =>
        CompleteAllScopes();
    public N_TSPR<X> Pass<X>(X val) =>
        new N_TSPR<X>(
            new N_SPV<X>(val),
            Fault,
            ((ITSP)this).Transactions);

    public bool Succeed() => ((ISP<T>)this).Value.Completed;
    public bool Succeed(out T result)
    {
        if (((ISP<T>)this).Value.Completed)
        {
            result = ((ISP<T>)this).Value.Payload;
            return true;
        }

        result = default!;
        return false;
    }

    public bool Faulted() => !((ISP<T>)this).Value.Completed;
    public bool Faulted(out N_SPF fault)
    {
        if (!((ISP<T>)this).Value.Completed)
        {
            fault = Fault;
            return true;
        }

        fault = default;
        return false;
    }
    #endregion core funcs

    #region transactional
    public N_TSPR<T> MarkScope(short index = 0)
    {
        ((ITSP<T, N_TSPR<T>, N_SPR<T>>)this).InternalMarkScope(index);

        return this;
    }
    public N_TSPR<T> CompleteScope(short index = -1)
    {
        ((ITSP<T, N_TSPR<T>, N_SPR<T>>)this).InternalCompleteScope(index);

        return this;
    }
    public N_TSPR<T> DisposeScope(short index = -1)
    {
        ((ITSP<T, N_TSPR<T>, N_SPR<T>>)this).InternalDisposeScope(index);

        return this;
    }
    public N_SPR<T> CompleteAllScopes()
    {
        ((ITSP<T, N_TSPR<T>, N_SPR<T>>)this).InternalCompleteAllScopes();

        return new N_SPR<T>(((ISP<T>)this).Value, Fault);
    }
    public N_SPR<T> DisposeAllScopes()
    {
        ((ITSP<T, N_TSPR<T>, N_SPR<T>>)this).InternalDisposeAllScopes();

        return new N_SPR<T>(((ISP<T>)this).Value, Fault);
    }
    #endregion transactional
}

public struct N_TDSPR<T> : ITSP<T, N_TDSPR<T>, N_DSPR<T>>, IDSP<T, N_TDSPR<T>, N_TSPR<T>>, ISPRConvertible<N_SPR<T>>
{
    #region props
    List<KeyValuePair<short, IDisposable>>? IDSP.Disposables { get; set; }
    List<KeyValuePair<short, IAsyncDisposable>>? IDSP.AsyncDisposables { get; set; }
    List<KeyValuePair<short, TransactionScope>>? ITSP.Transactions { get; set; }

    public N_SPV<T> Value { get; set; }
    public N_SPF Fault { get; set; }
    #endregion props

    #region ctors
    public N_TDSPR()
    {
    }
    public N_TDSPR(
       N_SPV<T> value,
       N_SPF fault,
       List<KeyValuePair<short, TransactionScope>>? transactions,
       List<KeyValuePair<short, IDisposable>>? disposables,
       List<KeyValuePair<short, IAsyncDisposable>>? asyncDisposables)
    {
        ((ISP<T>)this).Value = value;
        Fault = fault;
        ((ITSP)this).Transactions = transactions;
        ((IDSP)this).Disposables = disposables;
        ((IDSP)this).AsyncDisposables = asyncDisposables;
    }
    #endregion ctors

    #region core funcs
    public N_SPR<T> Descend() =>
        CompleteAllScopes()
        .DisposeAll();
    public N_TDSPR<X> Pass<X>(X val) =>
        new N_TDSPR<X>(
            new N_SPV<X>(val),
            Fault,
            ((ITSP)this).Transactions,
            ((IDSP)this).Disposables,
            ((IDSP)this).AsyncDisposables);

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
    #endregion core funcs

    #region disposal
    public N_TDSPR<T> MarkDispose(short index = 0)
    {
        ((IDSP<T, N_TDSPR<T>>)this).MarkDispose(index);

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

        return new N_TSPR<T>(
            Value,
            Fault,
            ((ITSP<T, N_TDSPR<T>, N_DSPR<T>>)this).Transactions);
    }
    #endregion disposal

    #region transactional
    public N_TDSPR<T> MarkScope(short index = 0)
    {
        ((ITSP<T, N_TDSPR<T>, N_DSPR<T>>)this).InternalMarkScope(index);

        return this;
    }
    public N_TDSPR<T> CompleteScope(short index = -1)
    {
        ((ITSP<T, N_TDSPR<T>, N_DSPR<T>>)this).InternalCompleteScope(index);

        return this;
    }
    public N_TDSPR<T> DisposeScope(short index = -1)
    {
        ((ITSP<T, N_TDSPR<T>, N_DSPR<T>>)this).InternalDisposeScope(index);

        return this;
    }
    public N_DSPR<T> CompleteAllScopes()
    {
        ((ITSP<T, N_TDSPR<T>, N_DSPR<T>>)this).InternalCompleteAllScopes();

        return new N_DSPR<T>(
            Value,
            Fault,
            ((IDSP<T, N_TDSPR<T>, N_TSPR<T>>)this).Disposables,
            ((IDSP<T, N_TDSPR<T>, N_TSPR<T>>)this).AsyncDisposables);
    }
    public N_DSPR<T> DisposeAllScopes()
    {
        ((ITSP<T, N_TDSPR<T>, N_DSPR<T>>)this).InternalDisposeAllScopes();

        return new N_DSPR<T>(
            Value,
            Fault,
            ((IDSP<T, N_TDSPR<T>, N_TSPR<T>>)this).Disposables,
            ((IDSP<T, N_TDSPR<T>, N_TSPR<T>>)this).AsyncDisposables);
    }
    #endregion transactional
}