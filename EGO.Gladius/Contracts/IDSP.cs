using EGO.Gladius.DataTypes;

namespace EGO.Gladius.Contracts;

public interface IDSP<T> : ISP
{
    DSPR<T> Dispose(int index = -1);
    SPR<T> DisposeAll();
    DSPR<T> MarkDispose(int index = 0);
}