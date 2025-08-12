using EGO.Gladius.DataTypes;

namespace EGO.Gladius.Contracts;

public interface ISP
{
    SPF Fault { get; }

    bool Faulted();
    bool Succeed();
}

public interface ISP<T> : ISP
{
    bool Succeed(out T result);

    bool Faulted(out SPF fault);

    bool HasValue();
}