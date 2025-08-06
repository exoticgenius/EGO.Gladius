using EGO.Gladius.Contracts;

using System.ComponentModel;
using System.Diagnostics;

namespace EGO.Gladius.DataTypes;

[DebuggerDisplay("{DebuggerPreview}")]
public struct DVSP : IDSP, ISPRDescendable<VSP>
{
    #region props
    List<KeyValuePair<short, IDisposable>>? _disposables;
    List<KeyValuePair<short, IAsyncDisposable>>? _asyncDisposables;

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
    #endregion core funcs

    #region disposal
    public DVSP Dispose(short index = -1)
    {
        foreach (var item in _disposables ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                c.Dispose();

        return this;
    }
    public VSP DisposeAll()
    {
        foreach (var item in _disposables ?? [])
            item.Value?.Dispose();

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
