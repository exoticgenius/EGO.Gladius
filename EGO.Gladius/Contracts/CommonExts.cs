using EGO.Gladius.DataTypes;

namespace EGO.Gladius.Contracts;

public static class CommonExts
{
    public static SPR<T> AsSPR<T>(this T obj) =>
        new SPR<T>(obj);
}

