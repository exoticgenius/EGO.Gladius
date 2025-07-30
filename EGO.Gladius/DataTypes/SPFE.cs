namespace EGO.Gladius.DataTypes;

public class SPFE : Exception
{
    public SPF Fault { get; }

    public SPFE(SPF fault) : base(fault.Message)
    {
        Fault = fault;
    }
}

public class N_SPFE : Exception
{
    public N_SPF Fault { get; }

    public N_SPFE(N_SPF fault) : base(fault.Message)
    {
        Fault = fault;
    }
}
