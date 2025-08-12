using EGO.Gladius.Contracts;

using System.ComponentModel;
using System.Diagnostics;
using System.Transactions;

namespace EGO.Gladius.DataTypes;

[DebuggerDisplay("{DebuggerPreview}")]
public struct TDVSP : ITSP<TDVSP, DVSP>, IDSP<TDVSP, TVSP>, ISPRDescendable<VSP>
{
    #region props
    private List<KeyValuePair<short, TransactionScope>>? _transactions;
    private List<KeyValuePair<short, IDisposable>>? _disposables;
    private List<KeyValuePair<short, IAsyncDisposable>>? _asyncDisposables;

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
        foreach (KeyValuePair<short, IDisposable> item in _disposables ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                c.Dispose();

        return this;
    }
    public TDVSP Dispose<E>(E index) where E : Enum =>
        Dispose(Convert.ToInt16(index));
    public TVSP DisposeAll()
    {
        foreach (KeyValuePair<short, IDisposable> item in _disposables ?? [])
            item.Value?.Dispose();

        return new TVSP(Success, Fault, _transactions);
    }


    public async ValueTask<TDVSP> DisposeAsync(short index = -1)
    {
        foreach (KeyValuePair<short, IAsyncDisposable> item in _asyncDisposables ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                await c.DisposeAsync();

        return this;
    }
    public ValueTask<TDVSP> DisposeAsync<E>(E index) where E : Enum =>
        DisposeAsync(Convert.ToInt16(index));
    public async ValueTask<TVSP> DisposeAllAsync()
    {
        DisposeAll();

        foreach (KeyValuePair<short, IAsyncDisposable> item in _asyncDisposables ?? [])
            if (item is { })
                await item.Value.DisposeAsync();

        return new TVSP(Success, Fault, _transactions);
    }
    #endregion disposal

    #region transactional
    public TDVSP CompleteScope(short index = -1)
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
    public TDVSP CompleteScope<E>(E index) where E : Enum =>
        CompleteScope(Convert.ToInt16(index));

    public TDVSP DisposeScope(short index = -1)
    {
        foreach (KeyValuePair<short, TransactionScope> item in _transactions ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                c.Dispose();

        return this;
    }
    public TDVSP DisposeScope<E>(E index) where E : Enum =>
        DisposeScope(Convert.ToInt16(index));

    public DVSP CompleteAllScopes()
    {
        foreach (KeyValuePair<short, TransactionScope> item in _transactions ?? [])
            CompleteScope(item.Key);

        return new DVSP(
            Success,
            Fault,
            _disposables,
            _asyncDisposables);
    }
    public DVSP DisposeAllScopes()
    {
        foreach (KeyValuePair<short, TransactionScope> item in _transactions ?? [])
            DisposeScope(item.Key);

        return new DVSP(
            Success,
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
