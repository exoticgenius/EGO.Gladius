namespace EGO.Gladius.DataTypes;

public class SPFE : Exception
{
    public SPF Fault { get; }

    public SPFE(SPF fault) : base(fault.Message)
    {
        Fault = fault;
    }
}
