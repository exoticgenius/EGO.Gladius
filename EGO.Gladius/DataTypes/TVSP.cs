using EGO.Gladius.Contracts;

using System.ComponentModel;
using System.Diagnostics;
using System.Transactions;

namespace EGO.Gladius.DataTypes;

[DebuggerDisplay("{DebuggerPreview}")]
public struct TVSP : ITSP<TVSP, VSP>, ISPRDescendable<VSP>
{
    #region props
    private List<KeyValuePair<short, TransactionScope>>? _transactions;

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
        foreach (KeyValuePair<short, TransactionScope> item in _transactions ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
            {
                if (Succeed())
                    c.Complete();
                c.Dispose();
            }

        return this;
    }
    public TVSP CompleteScope<E>(E index) where E : Enum =>
        CompleteScope(Convert.ToInt16(index));

    public TVSP DisposeScope(short index = -1)
    {
        foreach (KeyValuePair<short, TransactionScope> item in _transactions ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                c.Dispose();

        return this;
    }
    public TVSP DisposeScope<E>(E index) where E : Enum =>
        DisposeScope(Convert.ToInt16(index));

    public VSP CompleteAllScopes()
    {
        foreach (KeyValuePair<short, TransactionScope> item in _transactions ?? [])
            CompleteScope(item.Key);

        return new VSP(Success, Fault);
    }
    public VSP DisposeAllScopes()
    {
        foreach (KeyValuePair<short, TransactionScope> item in _transactions ?? [])
            DisposeScope(item.Key);

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
