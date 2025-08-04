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
