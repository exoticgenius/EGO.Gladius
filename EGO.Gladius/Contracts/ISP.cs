using EGO.Gladius.DataTypes;

namespace EGO.Gladius.Contracts;

public interface ISP
{
    public SPF Fault { get; }

    public bool Faulted();
    public bool Succeed();
}

public interface ISP<T> : ISP
{
    internal SPV<T> Value { get; }

    public bool Succeed(out T result)
    {
        if (Value.Completed)
        {
            result = Value.Payload;
            return true;
        }

        result = default!;
        return false;
    }

    public bool Faulted(out SPF fault)
    {
        if (!Value.Completed)
        {
            fault = Fault;
            return true;
        }

        fault = default;
        return false;
    }

    public bool HasValue();
}