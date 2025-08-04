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
    public bool Succeed(out T result);

    public bool Faulted(out SPF fault);

    public bool HasValue();
}