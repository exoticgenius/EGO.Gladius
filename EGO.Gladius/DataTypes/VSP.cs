using EGO.Gladius.Contracts;

using System.ComponentModel;
using System.Diagnostics;

namespace EGO.Gladius.DataTypes;

[DebuggerDisplay("{DebuggerPreview}")]
public struct VSP : ISP
{
    #region props
    public SPF Fault { get; }
    internal bool Success { get; set; }
    #endregion props

    #region ctors
    public VSP()
    {
        Success = true;
        Fault = default;
    }

    public VSP(bool success, SPF fault)
    {
        Success = success;
        Fault = Fault;
    }

    public VSP(SPF fault)
    {
        Success = false;
        Fault = fault;
    }
    #endregion ctors

    #region core funcs
    public bool Succeed() => Success;
    public bool Faulted() => !Success;
    #endregion core funcs

    #region operators
    public static implicit operator VSP(in SPF fault) =>
        new(fault);
    public static implicit operator VSP(in SPR tag) =>
        new(true, default);
    
    public static bool operator true(VSP vsp) => vsp.Succeed();
    public static bool operator false(VSP vsp) => vsp.Succeed();
    #endregion operators

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
