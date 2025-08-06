using EGO.Gladius.Contracts;

using System;
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

    List<KeyValuePair<short, TransactionScope>>? ITSP.Transactions => _transactions;
    List<KeyValuePair<short, IDisposable>>? IDSP.Disposables => _disposables;
    List<KeyValuePair<short, IAsyncDisposable>>? IDSP.AsyncDisposables => _asyncDisposables;

    public SPF Fault { get; set; }
    internal SPV<T> Value { get; }
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
        Value = value;
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
    public bool HasValue() => Value.HasValue();

    public TDSPR<X> Pass<X>(X val) =>
        new TDSPR<X>(
            new SPV<X>(val),
            Fault,
            ((ITSP)this).Transactions,
            ((IDSP)this).Disposables,
            ((IDSP)this).AsyncDisposables);

    public TDSPR<X> Pass<X>(SPR<X> spr) =>
        new TDSPR<X>(
            spr.Value,
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

    #endregion core funcs

    #region disposal
    public TDSPR<T> MarkDispose(short index = 0)
    {
        if (!Value.Completed)
            return this;

        if (Value.Payload is IDisposable dis)
            (_disposables ??= []).Add(new(index, dis));

        else if (Value.Payload is IAsyncDisposable adis)
            (_asyncDisposables ??= []).Add(new(index, adis));

        return this;
    }
    public TDSPR<T> Dispose(short index = -1)
    {
        foreach (var item in _disposables ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                c.Dispose();

        return this;
    }
    public TSPR<T> DisposeAll()
    {
        foreach (var item in _disposables ?? [])
            item.Value?.Dispose();

        return new TSPR<T>(
            Value,
            Fault,
            ((ITSP<T, TDSPR<T>, DSPR<T>>)this).Transactions);
    }
    #endregion disposal

    #region transactional
    public TDSPR<T> MarkScope(short index = 0)
    {
        if (Value.Completed && Value.Payload is TransactionScope tr)
            (_transactions ??= []).Add(new(index, tr));

        return this;
    }
    public TDSPR<T> CompleteScope(short index = -1)
    {
        foreach (var item in ((ITSP)this).Transactions ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
            {
                if (Succeed())
                    c.Complete();
                c.Dispose();
            }

        return this;
    }
    public TDSPR<T> DisposeScope(short index = -1)
    {
        foreach (var item in _transactions ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                c.Dispose();

        return this;
    }
    public DSPR<T> CompleteAllScopes()
    {
        foreach (var item in _transactions ?? [])
            CompleteScope(item.Key);

        return new DSPR<T>(
            Value,
            Fault,
            ((IDSP)this).Disposables,
            ((IDSP)this).AsyncDisposables);
    }
    public DSPR<T> DisposeAllScopes()
    {
        foreach (var item in _transactions ?? [])
            DisposeScope(item.Key);

        return new DSPR<T>(
            Value,
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
