using EGO.Gladius.Contracts;

using System.ComponentModel;
using System.Diagnostics;
using System.Transactions;

namespace EGO.Gladius.DataTypes;

[DebuggerDisplay("{DebuggerPreview}")]
public struct TDSPR<T> : ITSP<TDSPR<T>, DSPR<T>, T>, IDSP<TDSPR<T>, TSPR<T>, T>, ISPRDescendable<SPR<T>>, ISPRVoidable<TDVSP>
{
    #region props
    private List<KeyValuePair<short, TransactionScope>>? _transactions;
    private List<KeyValuePair<short, IDisposable>>? _disposables;
    private List<KeyValuePair<short, IAsyncDisposable>>? _asyncDisposables;

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
       List<KeyValuePair<short, TransactionScope>>? transactions)
    {
        Value = value;
        Fault = fault;
        _transactions = transactions;
    }
    public TDSPR(
       SPV<T> value,
       SPF fault,
       List<KeyValuePair<short, IDisposable>>? disposables,
       List<KeyValuePair<short, IAsyncDisposable>>? asyncDisposables)
    {
        Value = value;
        Fault = fault;
        _disposables = disposables;
        _asyncDisposables = asyncDisposables;
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
        new(
            Succeed(),
            Fault,
            _transactions,
            _disposables,
            _asyncDisposables);
    public bool HasValue() => Value.HasValue();

    public TDSPR<X> Pass<X>(X val) =>
        new(
            new SPV<X>(val),
            Fault,
            _transactions,
            _disposables,
            _asyncDisposables);

    public TDSPR<X> Pass<X>(SPR<X> spr) =>
        new(
            spr.Value,
            spr.Fault,
            _transactions,
            _disposables,
            _asyncDisposables);

    public TDSPR<X> Pass<X>(SPF fault) =>
        new(
            default,
            fault,
            _transactions,
            _disposables,
            _asyncDisposables);

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
    public TDSPR<T> MarkDispose<E>(E index) where E : Enum =>
        MarkDispose(Convert.ToInt16(index));

    public TDSPR<T> Dispose(short index = -1)
    {
        foreach (KeyValuePair<short, IDisposable> item in _disposables ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                c.Dispose();

        return this;
    }
    public TDSPR<T> Dispose<E>(E index) where E : Enum =>
        Dispose(Convert.ToInt16(index));
    public TSPR<T> DisposeAll()
    {
        foreach (KeyValuePair<short, IDisposable> item in _disposables ?? [])
            item.Value?.Dispose();

        return new TSPR<T>(
            Value,
            Fault,
            (_transactions);
    }

    public async ValueTask<TDSPR<T>> DisposeAsync(short index = -1)
    {
        foreach (KeyValuePair<short, IAsyncDisposable> item in _asyncDisposables ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                await c.DisposeAsync();

        return this;
    }
    public ValueTask<TDSPR<T>> DisposeAsync<E>(E index) where E : Enum =>
        DisposeAsync(Convert.ToInt16(index));
    public async ValueTask<TSPR<T>> DisposeAllAsync()
    {
        DisposeAll();

        foreach (KeyValuePair<short, IAsyncDisposable> item in _asyncDisposables ?? [])
            if (item is { })
                await item.Value.DisposeAsync();

        return new TSPR<T>(Value, Fault, _transactions);
    }
    #endregion disposal

    #region transactional
    public TDSPR<T> MarkScope(short index = 0)
    {
        if (Value.Completed && Value.Payload is TransactionScope tr)
            (_transactions ??= []).Add(new(index, tr));

        return this;
    }
    public TDSPR<T> MarkScope<E>(E index) where E : Enum =>
        MarkScope(Convert.ToInt16(index));

    public TDSPR<T> CompleteScope(short index = -1)
    {
        foreach (KeyValuePair<short, TransactionScope> item in _transactions ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
            {
                if (Succeed())
                    c.Complete();
                c.Dispose();
            }

        return this;
    }
    public TDSPR<T> CompleteScope<E>(E index) where E : Enum =>
        CompleteScope(Convert.ToInt16(index));

    public TDSPR<T> DisposeScope(short index = -1)
    {
        foreach (KeyValuePair<short, TransactionScope> item in _transactions ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                c.Dispose();

        return this;
    }
    public TDSPR<T> DisposeScope<E>(E index) where E : Enum =>
        DisposeScope(Convert.ToInt16(index));

    public DSPR<T> CompleteAllScopes()
    {
        foreach (KeyValuePair<short, TransactionScope> item in _transactions ?? [])
            CompleteScope(item.Key);

        return new DSPR<T>(
            Value,
            Fault,
            _disposables,
            _asyncDisposables);
    }
    public DSPR<T> DisposeAllScopes()
    {
        foreach (KeyValuePair<short, TransactionScope> item in _transactions ?? [])
            DisposeScope(item.Key);

        return new DSPR<T>(
            Value,
            Fault,
            _disposables,
            _asyncDisposables);
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
