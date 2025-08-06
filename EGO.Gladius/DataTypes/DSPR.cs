using EGO.Gladius.Contracts;

using System;
using System.ComponentModel;
using System.Diagnostics;

namespace EGO.Gladius.DataTypes;

[DebuggerDisplay("{DebuggerPreview}")]
public struct DSPR<T> : IDSP<T, DSPR<T>, SPR<T>>, ISPRDescendable<SPR<T>>, ISPRVoidable<DVSP>
{
    #region props
    List<KeyValuePair<short, IDisposable>>? _disposables;
    List<KeyValuePair<short, IAsyncDisposable>>? _asyncDisposables;

    List<KeyValuePair<short, IDisposable>>? IDSP.Disposables => _disposables;
    List<KeyValuePair<short, IAsyncDisposable>>? IDSP.AsyncDisposables => _asyncDisposables;

    public SPF Fault { get; set; }
    internal SPV<T> Value { get; }
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
        Value = value;
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
    public bool HasValue() => Value.HasValue();

    public DSPR<X> Pass<X>(X val) =>
        new DSPR<X>(
            new SPV<X>(val),
            Fault,
            ((IDSP)this).Disposables,
            ((IDSP)this).AsyncDisposables);

    public DSPR<X> Pass<X>(SPR<X> spr) =>
        new DSPR<X>(
            spr.Value,
            spr.Fault,
            ((IDSP)this).Disposables,
            ((IDSP)this).AsyncDisposables);

    public DSPR<X> Pass<X>(SPF fault) =>
        new DSPR<X>(
            default,
            fault,
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
    public DSPR<T> MarkDispose(short index = 0)
    {
        if (!Value.Completed)
            return this;

        if (Value.Payload is IDisposable dis)
            (_disposables ??= []).Add(new(index, dis));

        else if (Value.Payload is IAsyncDisposable adis)
            (_asyncDisposables ??= []).Add(new(index, adis));

        return this;
    }
    public DSPR<T> Dispose(short index = -1)
    {
        foreach (var item in _disposables ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                c.Dispose();

        return this;
    }
    public SPR<T> DisposeAll()
    {
        foreach (var item in _disposables ?? [])
            item.Value?.Dispose();
     
        return new SPR<T>(Value, Fault);
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
