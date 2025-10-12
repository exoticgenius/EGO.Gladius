using EGO.Gladius.Contracts;

using System.ComponentModel;
using System.Diagnostics;

namespace EGO.Gladius.DataTypes;

[DebuggerDisplay("{DebuggerPreview}")]
public struct DVSP : IDSP<DVSP, VSP>, ISPRDescendable<VSP>
{
    #region props
    private List<KeyValuePair<short, IDisposable>>? _disposables;
    private List<KeyValuePair<short, IAsyncDisposable>>? _asyncDisposables;

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

    public void ThrowIfFaulted()
    {
        if (Faulted())
            Fault.Throw();
    }
    #endregion core funcs

    #region disposal
    public DVSP Dispose(short index = -1)
    {
        foreach (KeyValuePair<short, IDisposable> item in _disposables ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                c.Dispose();

        return this;
    }
    public DVSP Dispose<E>(E index) where E : Enum =>
        Dispose(Convert.ToInt16(index));
    public VSP DisposeAll()
    {
        foreach (KeyValuePair<short, IDisposable> item in _disposables ?? [])
            item.Value?.Dispose();

        return new VSP(Success, Fault);
    }

    public async ValueTask<DVSP> DisposeAsync(short index = -1)
    {
        foreach (KeyValuePair<short, IAsyncDisposable> item in _asyncDisposables ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                await c.DisposeAsync();

        return this;
    }
    public ValueTask<DVSP> DisposeAsync<E>(E index) where E : Enum =>
        DisposeAsync(Convert.ToInt16(index));
    public async ValueTask<VSP> DisposeAllAsync()
    {
        DisposeAll();

        foreach (KeyValuePair<short, IAsyncDisposable> item in _asyncDisposables ?? [])
            if (item is { })
                await item.Value.DisposeAsync();

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
