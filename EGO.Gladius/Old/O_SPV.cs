namespace EGO.Gladius.DataTypes;

/// <summary>
/// super position value
/// </summary>
public struct O_SPV<T>
{
    public bool Completed { get; }
    public T Payload { get; }

    public O_SPV() : this(false, default!) { }

    public O_SPV(bool completed) : this(completed, default!) { }

    public O_SPV(T payload) : this(true, payload) { }

    public O_SPV(bool completed, T payload)
    {
        Completed = completed;
        Payload = payload;
    }

    public bool HasValue() =>
        Payload is not null;

    public static O_SPV<T> DoneSPV() =>
        new(true);

    public static O_SPV<T> UndoneSPV() =>
        new(false);
}
