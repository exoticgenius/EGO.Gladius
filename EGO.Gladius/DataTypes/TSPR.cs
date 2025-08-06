using EGO.Gladius.Contracts;

using System.ComponentModel;
using System.Diagnostics;
using System.Transactions;

namespace EGO.Gladius.DataTypes;

[DebuggerDisplay("{DebuggerPreview}")]
public struct TSPR<T> : ITSP<T, TSPR<T>, SPR<T>>, ISPRDescendable<SPR<T>>, ISPRVoidable<TVSP>
{
    #region props
    List<KeyValuePair<short, TransactionScope>>? _transactions;

    List<KeyValuePair<short, TransactionScope>>? ITSP.Transactions => _transactions;

    public SPF Fault { get; set; }
    internal SPV<T> Value { get; }
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
        Value = val;
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
    public bool HasValue() => Value.HasValue();

    public TSPR<X> Pass<X>(X val) =>
        new TSPR<X>(
            new SPV<X>(val),
            Fault,
            ((ITSP)this).Transactions);

    public TSPR<X> Pass<X>(SPR<X> spr) =>
        new TSPR<X>(
            spr.Value,
            spr.Fault,
            ((ITSP)this).Transactions);

    public TSPR<X> Pass<X>(SPF fault) =>
        new TSPR<X>(
            default,
            fault,
            ((ITSP)this).Transactions);

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

    #region transactional
    public TSPR<T> MarkScope(short index = 0)
    {
        if (Value.Completed && Value.Payload is TransactionScope tr)
            (_transactions??= []).Add(new(index, tr));

        return this;
    }
    public TSPR<T> CompleteScope(short index = -1)
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
    public TSPR<T> DisposeScope(short index = -1)
    {
        foreach (var item in _transactions ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                c.Dispose();

        return this;
    }
    public SPR<T> CompleteAllScopes()
    {
        foreach (var item in _transactions ?? [])
            CompleteScope(item.Key);

        return new SPR<T>(Value, Fault);
    }
    public SPR<T> DisposeAllScopes()
    {
        foreach (var item in _transactions ?? [])
            DisposeScope(item.Key);

        return new SPR<T>(Value, Fault);
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
