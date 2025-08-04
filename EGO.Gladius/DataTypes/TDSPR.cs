using EGO.Gladius.Contracts;

using System.ComponentModel;
using System.Diagnostics;
using System.Transactions;

namespace EGO.Gladius.DataTypes;

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
