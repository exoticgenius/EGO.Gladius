using EGO.Gladius.Contracts;

using System.Transactions;

namespace EGO.Gladius.DataTypes;

public struct SPR<T> : ISP<T>, ISPRConvertible<T>
{
    #region props
    SPV<T> ISP<T>.Value { get; set; }
    public SPF Fault { get; }
    #endregion props

    #region ctors
    public SPR()
    {
    }
    internal SPR(SPF fault)
    {
        ((ISP<T>)this).Value = default;
        Fault = fault;
    }
    internal SPR(T payload)
    {
        ((ISP<T>)this).Value = new SPV<T>(payload);
        Fault = default;
    }
    internal SPR(SPV<T> val, SPF fault)
    {
        ((ISP<T>)this).Value = val;
        Fault = fault;
    }
    internal SPR(T payload, SPF fault)
    {
        ((ISP<T>)this).Value = new SPV<T>(payload);
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
    public bool Faulted(out SPF fault)
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

    public static implicit operator SPR<T>(in SPF fault) =>
        new(fault);


#if Release
    public override string ToString()
    {
        throw new Exception("calling ToString on SPR<> object is impossible");
    }
#endif
    #endregion Operators
}

public struct DSPR<T> : IDSP<T, DSPR<T>, SPR<T>>, ISPRConvertible<SPR<T>>
{
    #region props
    List<KeyValuePair<short, IDisposable>>? IDSP.Disposables { get; set; }
    List<KeyValuePair<short, IAsyncDisposable>>? IDSP.AsyncDisposables { get; set; }

    public SPF Fault { get; set; }
    SPV<T> ISP<T>.Value { get; set; }
    #endregion props

    #region ctors
    public DSPR()
    {
    }

    public DSPR(
        SPV<T> value,
        SPF fault,
        List<KeyValuePair<short, IDisposable>>? disposables,
        List<KeyValuePair<short, IAsyncDisposable>>? asyncDisposables)
    {
        ((ISP<T>)this).Value = value;
        Fault = fault;
        ((IDSP)this).Disposables = disposables;
        ((IDSP)this).AsyncDisposables = asyncDisposables;
    }
    #endregion ctors

    #region core funcs
    public SPR<T> Descend() =>
        DisposeAll();
    public DSPR<X> Pass<X>(X val) =>
        new DSPR<X>(
            new SPV<X>(val),
            Fault,
            ((IDSP)this).Disposables,
            ((IDSP)this).AsyncDisposables);

    public DSPR<X> Pass<X>(SPR<X> spr) =>
        new DSPR<X>(
            ((ISP<X>)spr).Value,
            spr.Fault,
            ((IDSP)this).Disposables,
            ((IDSP)this).AsyncDisposables);

    public DSPR<X> Pass<X>(SPF fault) =>
        new DSPR<X>(
            default,
            fault,
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
    public bool Faulted(out SPF fault)
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
    public DSPR<T> MarkDispose(short index = 0)
    {
        ((IDSP<T, DSPR<T>, SPR<T>>)this).InternalMarkDispose(index);

        return this;
    }
    public DSPR<T> Dispose(short index = -1)
    {
        ((IDSP<T, DSPR<T>, SPR<T>>)this).InternalDispose(index);

        return this;
    }
    public SPR<T> DisposeAll()
    {
        ((IDSP<T, DSPR<T>, SPR<T>>)this).InternalDisposeAll();

        return new SPR<T>(((ISP<T>)this).Value, Fault);
    }
    #endregion disposal

    #region Operators
    //public static implicit operator N_SPR<T>(in T val) =>
    //    new(val, default);

    public static implicit operator DSPR<T>(in SPF fault) =>
        new(new SPV<T>(), fault, default, default);


#if Release
    public override string ToString()
    {
        throw new Exception("calling ToString on SPR<> object is impossible");
    }
#endif
    #endregion Operators
}

public struct TSPR<T> : ITSP<T, TSPR<T>, SPR<T>>, ISPRConvertible<SPR<T>>
{
    #region props
    List<KeyValuePair<short, TransactionScope>>? ITSP.Transactions { get; set; }

    SPV<T> ISP<T>.Value { get; set; }
    public SPF Fault { get; set; }
    #endregion props

    #region ctors
    public TSPR()
    {
    }
    internal TSPR(
        SPV<T> val,
        SPF fault,
        List<KeyValuePair<short, TransactionScope>>? transactions)
    {
        ((ISP<T>)this).Value = val;
        Fault = fault;
        ((ITSP<T, TSPR<T>, SPR<T>>)this).Transactions = transactions;
    }
    #endregion ctors

    #region core funcs
    public SPR<T> Descend() =>
        CompleteAllScopes();

    public TSPR<X> Pass<X>(X val) =>
        new TSPR<X>(
            new SPV<X>(val),
            Fault,
            ((ITSP)this).Transactions);

    public TSPR<X> Pass<X>(SPR<X> spr) =>
        new TSPR<X>(
            ((ISP<X>)spr).Value,
            spr.Fault,
            ((ITSP)this).Transactions);

    public TSPR<X> Pass<X>(SPF fault) =>
        new TSPR<X>(
            default,
            fault,
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
    public bool Faulted(out SPF fault)
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
    public TSPR<T> MarkScope(short index = 0)
    {
        ((ITSP<T, TSPR<T>, SPR<T>>)this).InternalMarkScope(index);

        return this;
    }
    public TSPR<T> CompleteScope(short index = -1)
    {
        ((ITSP<T, TSPR<T>, SPR<T>>)this).InternalCompleteScope(index);

        return this;
    }
    public TSPR<T> DisposeScope(short index = -1)
    {
        ((ITSP<T, TSPR<T>, SPR<T>>)this).InternalDisposeScope(index);

        return this;
    }
    public SPR<T> CompleteAllScopes()
    {
        ((ITSP<T, TSPR<T>, SPR<T>>)this).InternalCompleteAllScopes();

        return new SPR<T>(((ISP<T>)this).Value, Fault);
    }
    public SPR<T> DisposeAllScopes()
    {
        ((ITSP<T, TSPR<T>, SPR<T>>)this).InternalDisposeAllScopes();

        return new SPR<T>(((ISP<T>)this).Value, Fault);
    }
    #endregion transactional
}

public struct TDSPR<T> : ITSP<T, TDSPR<T>, DSPR<T>>, IDSP<T, TDSPR<T>, TSPR<T>>, ISPRConvertible<SPR<T>>
{
    #region props
    List<KeyValuePair<short, IDisposable>>? IDSP.Disposables { get; set; }
    List<KeyValuePair<short, IAsyncDisposable>>? IDSP.AsyncDisposables { get; set; }
    List<KeyValuePair<short, TransactionScope>>? ITSP.Transactions { get; set; }

    public SPV<T> Value { get; set; }
    public SPF Fault { get; set; }
    #endregion props

    #region ctors
    public TDSPR()
    {
    }
    public TDSPR(
       SPV<T> value,
       SPF fault,
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
    public SPR<T> Descend() =>
        CompleteAllScopes()
        .DisposeAll();

    public TDSPR<X> Pass<X>(X val) =>
        new TDSPR<X>(
            new SPV<X>(val),
            Fault,
            ((ITSP)this).Transactions,
            ((IDSP)this).Disposables,
            ((IDSP)this).AsyncDisposables);

    public TDSPR<X> Pass<X>(SPR<X> spr) =>
        new TDSPR<X>(
            ((ISP<X>)spr).Value,
            spr.Fault,
            ((ITSP)this).Transactions,
            ((IDSP)this).Disposables,
            ((IDSP)this).AsyncDisposables);

    public TDSPR<X> Pass<X>(SPF fault) =>
        new TDSPR<X>(
            default,
            fault,
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
    #endregion core funcs

    #region disposal
    public TDSPR<T> MarkDispose(short index = 0)
    {
        ((IDSP<T, TDSPR<T>>)this).InternalMarkDispose(index);

        return this;
    }
    public TDSPR<T> Dispose(short index = -1)
    {
        ((IDSP<T, TDSPR<T>, TSPR<T>>)this).InternalDispose(index);

        return this;
    }
    public TSPR<T> DisposeAll()
    {
        ((IDSP<T, TDSPR<T>, TSPR<T>>)this).InternalDisposeAll();

        return new TSPR<T>(
            Value,
            Fault,
            ((ITSP<T, TDSPR<T>, DSPR<T>>)this).Transactions);
    }
    #endregion disposal

    #region transactional
    public TDSPR<T> MarkScope(short index = 0)
    {
        ((ITSP<T, TDSPR<T>, DSPR<T>>)this).InternalMarkScope(index);

        return this;
    }
    public TDSPR<T> CompleteScope(short index = -1)
    {
        ((ITSP<T, TDSPR<T>, DSPR<T>>)this).InternalCompleteScope(index);

        return this;
    }
    public TDSPR<T> DisposeScope(short index = -1)
    {
        ((ITSP<T, TDSPR<T>, DSPR<T>>)this).InternalDisposeScope(index);

        return this;
    }
    public DSPR<T> CompleteAllScopes()
    {
        ((ITSP<T, TDSPR<T>, DSPR<T>>)this).InternalCompleteAllScopes();

        return new DSPR<T>(
            Value,
            Fault,
            ((IDSP<T, TDSPR<T>, TSPR<T>>)this).Disposables,
            ((IDSP<T, TDSPR<T>, TSPR<T>>)this).AsyncDisposables);
    }
    public DSPR<T> DisposeAllScopes()
    {
        ((ITSP<T, TDSPR<T>, DSPR<T>>)this).InternalDisposeAllScopes();

        return new DSPR<T>(
            Value,
            Fault,
            ((IDSP<T, TDSPR<T>, TSPR<T>>)this).Disposables,
            ((IDSP<T, TDSPR<T>, TSPR<T>>)this).AsyncDisposables);
    }
    #endregion transactional
}


public struct VSP : ISP
{
    #region props
    public SPF Fault { get; }
    internal bool Success { get; set; }
    #endregion props

    #region ctors
    public VSP()
    {
        Success = true;
        Fault = default;
    }

    public VSP(bool success, SPF fault)
    {
        Success = success;
        Fault = Fault;
    }

    public VSP(SPF fault)
    {
        Success = false;
        Fault = fault;
    }
    #endregion ctors

    #region core funcs
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
    #endregion core funcs

    #region operators
    public static implicit operator VSP(in SPF fault) =>
        new(fault);
    #endregion operators
}

public struct DVSP : IDSP, ISPRConvertible<VSP>
{
    #region props
    List<KeyValuePair<short, IDisposable>>? IDSP.Disposables { get; set; }
    List<KeyValuePair<short, IAsyncDisposable>>? IDSP.AsyncDisposables { get; set; }

    public SPF Fault { get; set; }
    internal bool Success { get; set; }
    #endregion props

    #region ctors
    public DVSP()
    {
        Success = true;
        Fault = default;
    }

    public DVSP(bool success, SPF fault)
    {
        Success = success;
        Fault = Fault;
    }

    public DVSP(
        bool success,
        SPF fault,
        List<KeyValuePair<short, IDisposable>>? disposables,
        List<KeyValuePair<short, IAsyncDisposable>>? asyncDisposables)
    {
        Success = success;
        Fault = fault;
        ((IDSP)this).Disposables = disposables;
        ((IDSP)this).AsyncDisposables = asyncDisposables;
    }
    #endregion ctors

    #region core funcs
    public VSP Descend() =>
        DisposeAll();

    //public DSPR<X> Pass<X>(X val) =>
    //    new DSPR<X>(
    //        new SPV<X>(val),
    //        Fault,
    //        ((IDSP)this).Disposables,
    //        ((IDSP)this).AsyncDisposables);

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
    #endregion core funcs

    #region disposal
    public DVSP Dispose(short index = -1)
    {
        ((IDSP)this).InternalDispose(index);

        return this;
    }
    public VSP DisposeAll()
    {
        ((IDSP)this).InternalDispose();

        return new VSP(Success, Fault);
    }
    #endregion disposal
}

public struct TVSP : ITSP, ISPRConvertible<VSP>
{
    #region props
    List<KeyValuePair<short, TransactionScope>>? ITSP.Transactions { get; set; }

    internal bool Success { get; set; }
    public SPF Fault { get; set; }
    #endregion props

    #region ctors
    public TVSP()
    {
    }

    public TVSP(bool success, SPF fault)
    {
        Success = success;
        Fault = Fault;
    }

    internal TVSP(
        bool success,
        SPF fault,
        List<KeyValuePair<short, TransactionScope>>? transactions)
    {
        Success = success;
        Fault = fault;
        ((ITSP)this).Transactions = transactions;
    }
    #endregion ctors

    #region core funcs
    public VSP Descend() =>
        CompleteAllScopes();

    //public TSPR<X> Pass<X>(X val) =>
    //    new TSPR<X>(
    //        new SPV<X>(val),
    //        Fault,
    //        ((ITSP)this).Transactions);

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
    #endregion core funcs

    #region transactional
    public TVSP CompleteScope(short index = -1)
    {
        ((ITSP)this).InternalCompleteScope(index);

        return this;
    }
    public TVSP DisposeScope(short index = -1)
    {
        ((ITSP)this).InternalDisposeScope(index);

        return this;
    }
    public VSP CompleteAllScopes()
    {
        ((ITSP)this).InternalCompleteAllScopes();

        return new VSP(Success, Fault);
    }
    public VSP DisposeAllScopes()
    {
        ((ITSP)this).InternalDisposeAllScopes();

        return new VSP(Success, Fault);
    }
    #endregion transactional
}

public struct TDVSP : ITSP, IDSP, ISPRConvertible<VSP>
{
    #region props
    List<KeyValuePair<short, IDisposable>>? IDSP.Disposables { get; set; }
    List<KeyValuePair<short, IAsyncDisposable>>? IDSP.AsyncDisposables { get; set; }
    List<KeyValuePair<short, TransactionScope>>? ITSP.Transactions { get; set; }

    internal bool Success { get; set; }
    public SPF Fault { get; set; }
    #endregion props

    #region ctors
    public TDVSP()
    {
    }
    public TDVSP(bool success, SPF fault)
    {
        Success = success;
        Fault = Fault;
    }
    public TDVSP(
       bool success,
       SPF fault,
       List<KeyValuePair<short, TransactionScope>>? transactions,
       List<KeyValuePair<short, IDisposable>>? disposables,
       List<KeyValuePair<short, IAsyncDisposable>>? asyncDisposables)
    {
        Success = success;
        Fault = fault;
        ((ITSP)this).Transactions = transactions;
        ((IDSP)this).Disposables = disposables;
        ((IDSP)this).AsyncDisposables = asyncDisposables;
    }
    #endregion ctors

    #region core funcs
    public VSP Descend() =>
        CompleteAllScopes()
        .DisposeAll();
    //public TDSPR<X> Pass<X>(X val) =>
    //    new TDSPR<X>(
    //        new SPV<X>(val),
    //        Fault,
    //        ((ITSP)this).Transactions,
    //        ((IDSP)this).Disposables,
    //        ((IDSP)this).AsyncDisposables);

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
    #endregion core funcs

    #region disposal
    public TDVSP Dispose(short index = -1)
    {
        ((IDSP)this).InternalDispose(index);

        return this;
    }
    public TVSP DisposeAll()
    {
        ((IDSP)this).InternalDisposeAll();

        return new TVSP(
            Success,
            Fault,
            ((ITSP)this).Transactions);
    }
    #endregion disposal

    #region transactional
    public TDVSP CompleteScope(short index = -1)
    {
        ((ITSP)this).InternalCompleteScope(index);

        return this;
    }
    public TDVSP DisposeScope(short index = -1)
    {
        ((ITSP)this).InternalDisposeScope(index);

        return this;
    }
    public DVSP CompleteAllScopes()
    {
        ((ITSP)this).InternalCompleteAllScopes();

        return new DVSP(
            Success,
            Fault,
            ((IDSP)this).Disposables,
            ((IDSP)this).AsyncDisposables);
    }
    public DVSP DisposeAllScopes()
    {
        ((ITSP)this).InternalDisposeAllScopes();

        return new DVSP(
            Success,
            Fault,
            ((IDSP)this).Disposables,
            ((IDSP)this).AsyncDisposables);
    }
    #endregion transactional
}