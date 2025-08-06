using EGO.Gladius.Contracts;

using System.ComponentModel;
using System.Diagnostics;
using System.Transactions;

namespace EGO.Gladius.DataTypes;

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
        foreach (var item in _disposables ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                c.Dispose();

        return this;
    }
    public TVSP DisposeAll()
    {
        foreach (var item in _disposables ?? [])
            item.Value?.Dispose();

        return new TVSP(
            Success,
            Fault,
            ((ITSP)this).Transactions);
    }
    #endregion disposal

    #region transactional
    public TDVSP CompleteScope(short index = -1)
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
    public TDVSP DisposeScope(short index = -1)
    {
        foreach (var item in _transactions ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                c.Dispose();

        return this;
    }
    public DVSP CompleteAllScopes()
    {
        foreach (var item in _transactions ?? [])
            CompleteScope(item.Key);

        return new DVSP(
            Success,
            Fault,
            ((IDSP)this).Disposables,
            ((IDSP)this).AsyncDisposables);
    }
    public DVSP DisposeAllScopes()
    {
        foreach (var item in _transactions ?? [])
            DisposeScope(item.Key);

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
