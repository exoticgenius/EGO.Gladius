using EGO.Gladius.Contracts;

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Transactions;

namespace EGO.Gladius.DataTypes;

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

[DebuggerDisplay("{DebuggerPreview}")]
public struct DSPR<T> : IDSP<T, DSPR<T>, SPR<T>>, ISPRDescendable<SPR<T>>, ISPRVoidable<DVSP>
{
    #region props
    List<KeyValuePair<short, IDisposable>>? _disposables;
    List<KeyValuePair<short, IAsyncDisposable>>? _asyncDisposables;
    private SPV<T> _value;

    List<KeyValuePair<short, IDisposable>>? IDSP.Disposables => _disposables;
    List<KeyValuePair<short, IAsyncDisposable>>? IDSP.AsyncDisposables => _asyncDisposables;

    public SPF Fault { get; set; }
    SPV<T> ISP<T>.Value => _value;
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
        _value = value;
        Fault = fault;
        _disposables = disposables;
        _asyncDisposables = asyncDisposables;
    }
    #endregion ctors

    #region core funcs
    public SPR<T> Descend() =>
        DisposeAll();
    public DVSP Void() => 
        new DVSP(
            Succeed(),
            Fault,
            ((IDSP)this).Disposables,
            ((IDSP)this).AsyncDisposables);
    public bool HasValue() => _value.HasValue();

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
    public bool Faulted() => !((ISP<T>)this).Value.Completed;
    #endregion core funcs

    #region disposal
    public DSPR<T> MarkDispose(short index = 0)
    {
        _disposables ??= [];
        _asyncDisposables ??= [];
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

[DebuggerDisplay("{DebuggerPreview}")]
public struct TSPR<T> : ITSP<T, TSPR<T>, SPR<T>>, ISPRDescendable<SPR<T>>, ISPRVoidable<TVSP>
{
    #region props
    List<KeyValuePair<short, TransactionScope>>? _transactions;
    private SPV<T> _value;

    List<KeyValuePair<short, TransactionScope>>? ITSP.Transactions => _transactions;

    public SPF Fault { get; set; }
    SPV<T> ISP<T>.Value => _value;
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
        _value = val;
        Fault = fault;
        _transactions = transactions;
    }
    #endregion ctors

    #region core funcs
    public SPR<T> Descend() =>
        CompleteAllScopes();
    public TVSP Void() =>
        new TVSP(
            Succeed(),
            Fault,
            ((ITSP)this).Transactions);
    public bool HasValue() => _value.HasValue();

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
    public bool Faulted() => !((ISP<T>)this).Value.Completed;
    #endregion core funcs

    #region transactional
    public TSPR<T> MarkScope(short index = 0)
    {
        _transactions ??= [];
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

[DebuggerDisplay("{DebuggerPreview}")]
public struct TDSPR<T> : ITSP<T, TDSPR<T>, DSPR<T>>, IDSP<T, TDSPR<T>, TSPR<T>>, ISPRDescendable<SPR<T>>, ISPRVoidable<TDVSP>
{
    #region props
    List<KeyValuePair<short, TransactionScope>>? _transactions;
    List<KeyValuePair<short, IDisposable>>? _disposables;
    List<KeyValuePair<short, IAsyncDisposable>>? _asyncDisposables;
    private SPV<T> _value;

    List<KeyValuePair<short, TransactionScope>>? ITSP.Transactions => _transactions;
    List<KeyValuePair<short, IDisposable>>? IDSP.Disposables => _disposables;
    List<KeyValuePair<short, IAsyncDisposable>>? IDSP.AsyncDisposables => _asyncDisposables;

    public SPF Fault { get; set; }
    SPV<T> ISP<T>.Value => _value;
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
        _value = value;
        Fault = fault;
        _transactions = transactions;
        _disposables = disposables;
        _asyncDisposables = asyncDisposables;
    }
    #endregion ctors

    #region core funcs
    public SPR<T> Descend() =>
        CompleteAllScopes()
        .DisposeAll();
    public TDVSP Void() =>
        new TDVSP(
            Succeed(),
            Fault,
            ((ITSP)this).Transactions,
            ((IDSP)this).Disposables,
            ((IDSP)this).AsyncDisposables);
    public bool HasValue() => _value.HasValue();

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

    public bool Succeed() => ((ISP<T>)this).Value.Completed;
    public bool Faulted() => !((ISP<T>)this).Value.Completed;
    #endregion core funcs

    #region disposal
    public TDSPR<T> MarkDispose(short index = 0)
    {
        _disposables ??= [];
        _asyncDisposables ??= [];
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
            ((ISP<T>)this).Value,
            Fault,
            ((ITSP<T, TDSPR<T>, DSPR<T>>)this).Transactions);
    }
    #endregion disposal

    #region transactional
    public TDSPR<T> MarkScope(short index = 0)
    {
        _transactions ??= [];
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
            ((ISP<T>)this).Value,
            Fault,
            ((IDSP<T, TDSPR<T>, TSPR<T>>)this).Disposables,
            ((IDSP<T, TDSPR<T>, TSPR<T>>)this).AsyncDisposables);
    }
    public DSPR<T> DisposeAllScopes()
    {
        ((ITSP<T, TDSPR<T>, DSPR<T>>)this).InternalDisposeAllScopes();

        return new DSPR<T>(
            ((ISP<T>)this).Value,
            Fault,
            ((IDSP<T, TDSPR<T>, TSPR<T>>)this).Disposables,
            ((IDSP<T, TDSPR<T>, TSPR<T>>)this).AsyncDisposables);
    }
    #endregion transactional

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


[DebuggerDisplay("{DebuggerPreview}")]
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
    #endregion core funcs

    #region operators
    public static implicit operator VSP(in SPF fault) =>
        new(fault);
    #endregion operators

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

#if Release
    public override string ToString()
    {
        throw new Exception("Calling ToStringon ISP object is impossible");
    }
#endif
    #endregion utils
}

[DebuggerDisplay("{DebuggerPreview}")]
public struct DVSP : IDSP, ISPRDescendable<VSP>
{
    #region props
    List<KeyValuePair<short, IDisposable>>? _disposables;
    List<KeyValuePair<short, IAsyncDisposable>>? _asyncDisposables;

    List<KeyValuePair<short, IDisposable>>? IDSP.Disposables => _disposables;
    List<KeyValuePair<short, IAsyncDisposable>>? IDSP.AsyncDisposables => _asyncDisposables;

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
        _disposables = disposables;
        _asyncDisposables = asyncDisposables;
    }
    #endregion ctors

    #region core funcs
    public VSP Descend() =>
        DisposeAll();

    public bool Succeed() => Success;
    public bool Faulted() => !Success;
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

#if Release
    public override string ToString()
    {
        throw new Exception("Calling ToStringon ISP object is impossible");
    }
#endif
    #endregion utils
}

[DebuggerDisplay("{DebuggerPreview}")]
public struct TVSP : ITSP, ISPRDescendable<VSP>
{
    #region props
    List<KeyValuePair<short, TransactionScope>>? _transactions;

    List<KeyValuePair<short, TransactionScope>>? ITSP.Transactions => _transactions;

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
        _transactions = transactions;
    }
    #endregion ctors

    #region core funcs
    public VSP Descend() =>
        CompleteAllScopes();

    public bool Succeed() => Success;
    public bool Faulted() => !Success;
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

#if Release
    public override string ToString()
    {
        throw new Exception("Calling ToStringon ISP object is impossible");
    }
#endif
    #endregion utils
}

[DebuggerDisplay("{DebuggerPreview}")]
public struct TDVSP : ITSP, IDSP, ISPRDescendable<VSP>
{
    #region props
    List<KeyValuePair<short, TransactionScope>>? _transactions;
    List<KeyValuePair<short, IDisposable>>? _disposables;
    List<KeyValuePair<short, IAsyncDisposable>>? _asyncDisposables;

    List<KeyValuePair<short, TransactionScope>>? ITSP.Transactions => _transactions;
    List<KeyValuePair<short, IDisposable>>? IDSP.Disposables => _disposables;
    List<KeyValuePair<short, IAsyncDisposable>>? IDSP.AsyncDisposables => _asyncDisposables;

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
        _transactions = transactions;
        _disposables = disposables;
        _asyncDisposables = asyncDisposables;
    }
    #endregion ctors

    #region core funcs
    public VSP Descend() =>
        CompleteAllScopes()
        .DisposeAll();

    public bool Succeed() => Success;
    public bool Faulted() => !Success;
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

#if Release
    public override string ToString()
    {
        throw new Exception("Calling ToStringon ISP object is impossible");
    }
#endif
    #endregion utils
}