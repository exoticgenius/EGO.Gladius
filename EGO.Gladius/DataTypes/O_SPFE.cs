namespace EGO.Gladius.DataTypes;

public class O_SPFE : Exception
{
    public O_SPF Fault { get; }

    public O_SPFE(O_SPF fault) : base(fault.Message)
    {
        Fault = fault;
    }
}

public class SPFE : Exception
{
    public SPF Fault { get; }

    public SPFE(SPF fault) : base(fault.Message)
    {
        Fault = fault;
    }
}
