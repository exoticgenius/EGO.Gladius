
namespace EGO.Gladius.DataTypes;

public struct N_SPV<T>
{
    public bool Completed { get; }
    public T Payload { get; }

    public N_SPV() : this(false, default!) { }

    public N_SPV(bool completed) : this(completed, default!) { }

    public N_SPV(T payload) : this(true, payload) { }

    public N_SPV(bool completed, T payload)
    {
        Completed = completed;
        Payload = payload;
    }

    public bool HasValue() =>
        Payload is not null;

    public static N_SPV<T> DoneSPV() =>
        new(true);

    public static N_SPV<T> UndoneSPV() =>
        new(false);
}
