namespace EGO.Gladius.DataTypes;

public class SPFE : Exception
{
    public SPF Fault { get; }

    public SPFE(SPF fault) : base(fault.Message)
    {
        Fault = fault;
    }
}
public class SPFST : Exception
{
    private string _stackTrace = string.Empty;
    public override string? StackTrace { get => _stackTrace; }
    public SPFST(string stackTrace)
    {
        _stackTrace = stackTrace;
    }
}