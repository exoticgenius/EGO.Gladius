namespace EGO.Gladius.DataTypes;

/// <summary>
/// super position value
/// </summary>
public struct SPV<T>
{
    public bool Completed { get; }
    public T Payload { get; }

    public SPV() : this(false, default!) { }

    public SPV(bool completed) : this(completed, default!) { }

    public SPV(T payload) : this(true, payload) { }

    public SPV(bool completed, T payload)
    {
        Completed = completed;
        Payload = payload;
    }

    public bool HasValue() =>
        Payload is not null;

    public static SPV<T> DoneSPV() =>
        new(true);

    public static SPV<T> UndoneSPV() =>
        new(false);
}
