using System.Reflection;

namespace EGO.Gladius.DataTypes;

public struct N_SPF
{

    #region ' props '
    public LinkedList<MethodInfo>? CapturedContext { get; }

    public object[]? Parameters { get; }

    public Exception? Exception { get; }

    public string? Message { get; }
    #endregion ' props '

    public void Throw() => throw new N_SPFE(this);
    public N_SPFE GenSPFE() => new(this);
}
