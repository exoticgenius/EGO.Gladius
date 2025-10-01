using EGO.Gladius.Contracts;
using EGO.Gladius.DataTypes;

using System.Runtime.CompilerServices;

namespace EGO.Gladius.Extensions;

public static class MigratorExtensions_SPR_TO_DSPR
{
    public static DSPR<T> MarkDispose<T>(this SPR<T> spr, short index = -1) =>
        new DSPR<T>(spr.Value, spr.Fault).MarkDispose(index);
    public static DSPR<T> MarkDispose<T, E>(this SPR<T> spr, E index) where E : Enum =>
        MarkDispose(spr, Convert.ToInt16(index));

    public static async ValueTask<DSPR<T>> MarkDispose<T>(this Task<SPR<T>> taskSpr, short index = -1)
    {
        var spr = await taskSpr;
        return new DSPR<T>(spr.Value, spr.Fault).MarkDispose(index);
    }
    public static ValueTask<DSPR<T>> MarkDispose<T, E>(this Task<SPR<T>> taskSpr, E index) where E : Enum =>
        MarkDispose(taskSpr, Convert.ToInt16(index));

    [OverloadResolutionPriority(1)]
    public static async ValueTask<DSPR<T>> MarkDispose<T>(this ValueTask<SPR<T>> taskSpr, short index = -1)
    {
        var spr = await taskSpr;
        return new DSPR<T>(spr.Value, spr.Fault).MarkDispose(index);
    }
    [OverloadResolutionPriority(1)]
    public static ValueTask<DSPR<T>> MarkDispose<T, E>(this ValueTask<SPR<T>> taskSpr, E index) where E : Enum =>
        MarkDispose(taskSpr, Convert.ToInt16(index));
}

public static class MigratorExtensions_TSPR_TO_TDSPR
{
    public static TDSPR<T> MarkDispose<T>(this TSPR<T> spr, short index = -1) =>
        new TDSPR<T>(spr.Value, spr.Fault, ((ITSP)spr).Transactions).MarkDispose(index);
    public static TDSPR<T> MarkDispose<T, E>(this TSPR<T> spr, E index) where E : Enum =>
        MarkDispose(spr, Convert.ToInt16(index));

    public static async ValueTask<TDSPR<T>> MarkDispose<T>(this Task<TSPR<T>> taskSpr, short index = -1)
    {
        var spr = await taskSpr;
        return new TDSPR<T>(spr.Value, spr.Fault, ((ITSP)spr).Transactions).MarkDispose(index);
    }
    public static ValueTask<TDSPR<T>> MarkDispose<T, E>(this Task<TSPR<T>> taskSpr, E index) where E : Enum =>
        MarkDispose(taskSpr, Convert.ToInt16(index));

    [OverloadResolutionPriority(1)]
    public static async ValueTask<TDSPR<T>> MarkDispose<T>(this ValueTask<TSPR<T>> taskSpr, short index = -1)
    {
        var spr = await taskSpr;
        return new TDSPR<T>(spr.Value, spr.Fault, ((ITSP)spr).Transactions).MarkDispose(index);
    }
    [OverloadResolutionPriority(1)]
    public static ValueTask<TDSPR<T>> MarkDispose<T, E>(this ValueTask<TSPR<T>> taskSpr, E index) where E : Enum =>
        MarkDispose(taskSpr, Convert.ToInt16(index));
}

public static class MigratorExtensions_SPR_TO_TSPR
{
    public static TSPR<T> MarkScope<T>(this SPR<T> spr, short index = -1) =>
        new TSPR<T>(spr.Value, spr.Fault).MarkScope(index);
    public static TSPR<T> MarkScope<T, E>(this SPR<T> spr, E index) where E : Enum =>
        MarkScope(spr, Convert.ToInt16(index));

    public static async ValueTask<TSPR<T>> MarkScope<T>(this Task<SPR<T>> taskSpr, short index = -1)
    {
        var spr = await taskSpr;
        return new TSPR<T>(spr.Value, spr.Fault).MarkScope(index);
    }
    public static ValueTask<TSPR<T>> MarkScope<T, E>(this Task<SPR<T>> taskSpr, E index) where E : Enum =>
        MarkScope(taskSpr, Convert.ToInt16(index));

    [OverloadResolutionPriority(1)]
    public static async ValueTask<TSPR<T>> MarkScope<T>(this ValueTask<SPR<T>> taskSpr, short index = -1)
    {
        var spr = await taskSpr;
        return new TSPR<T>(spr.Value, spr.Fault).MarkScope(index);
    }
    [OverloadResolutionPriority(1)]
    public static ValueTask<TSPR<T>> MarkScope<T, E>(this ValueTask<SPR<T>> taskSpr, E index) where E : Enum =>
        MarkScope(taskSpr, Convert.ToInt16(index));
}

public static class MigratorExtensions_DSPR_TO_TDSPR
{
    public static TDSPR<T> MarkScope<T>(this DSPR<T> spr, short index = -1) =>
        new TDSPR<T>(spr.Value, spr.Fault, ((IDSP)spr).Disposables, ((IDSP)spr).AsyncDisposables).MarkScope(index);
    public static TDSPR<T> MarkScope<T, E>(this DSPR<T> spr, E index) where E : Enum =>
        MarkScope(spr, Convert.ToInt16(index));

    public static async ValueTask<TDSPR<T>> MarkScope<T>(this Task<DSPR<T>> taskSpr, short index = -1)
    {
        var spr = await taskSpr;
        return new TDSPR<T>(spr.Value, spr.Fault, ((IDSP)spr).Disposables, ((IDSP)spr).AsyncDisposables).MarkScope(index);
    }
    public static ValueTask<TDSPR<T>> MarkScope<T, E>(this Task<DSPR<T>> taskSpr, E index) where E : Enum =>
        MarkScope(taskSpr, Convert.ToInt16(index));

    [OverloadResolutionPriority(1)]
    public static async ValueTask<TDSPR<T>> MarkScope<T>(this ValueTask<DSPR<T>> taskSpr, short index = -1)
    {
        var spr = await taskSpr;
        return new TDSPR<T>(spr.Value, spr.Fault, ((IDSP)spr).Disposables, ((IDSP)spr).AsyncDisposables).MarkScope(index);
    }
    [OverloadResolutionPriority(1)]
    public static ValueTask<TDSPR<T>> MarkScope<T, E>(this ValueTask<DSPR<T>> taskSpr, E index) where E : Enum =>
        MarkScope(taskSpr, Convert.ToInt16(index));
}

public static class MigratorExtensions_DSPR_TO_DSPR
{
    public static async ValueTask<DSPR<T>> MarkDispose<T>(this Task<DSPR<T>> taskSpr, short index = -1)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.MarkDispose(index);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen([spr.Value.Payload], e));
        }
    }
    public static ValueTask<DSPR<T>> MarkDispose<T, E>(this Task<DSPR<T>> taskSpr, E index) where E : Enum =>
        MarkDispose(taskSpr, Convert.ToInt16(index));

    [OverloadResolutionPriority(1)]
    public static async ValueTask<DSPR<T>> MarkDispose<T>(this ValueTask<DSPR<T>> taskSpr, short index = -1)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.MarkDispose(index);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen([spr.Value.Payload], e));
        }
    }
    [OverloadResolutionPriority(1)]
    public static ValueTask<DSPR<T>> MarkDispose<T, E>(this ValueTask<DSPR<T>> taskSpr, E index) where E : Enum =>
        MarkDispose(taskSpr, Convert.ToInt16(index));


    public static async ValueTask<DSPR<T>> Dispose<T>(this Task<DSPR<T>> taskSpr, short index = -1)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Dispose(index);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen([spr.Value.Payload], e));
        }
    }
    public static ValueTask<DSPR<T>> Dispose<T, E>(this Task<DSPR<T>> taskSpr, E index) where E : Enum =>
        Dispose(taskSpr, Convert.ToInt16(index));

    [OverloadResolutionPriority(1)]
    public static async ValueTask<DSPR<T>> Dispose<T>(this ValueTask<DSPR<T>> taskSpr, short index = -1)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Dispose(index);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen([spr.Value.Payload], e));
        }
    }
    [OverloadResolutionPriority(1)]
    public static ValueTask<DSPR<T>> Dispose<T, E>(this ValueTask<DSPR<T>> taskSpr, E index) where E : Enum =>
        Dispose(taskSpr, Convert.ToInt16(index));


    public static async ValueTask<DSPR<T>> DisposeAsync<T>(this Task<DSPR<T>> taskSpr, short index = -1)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return await spr.DisposeAsync(index);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen([spr.Value.Payload], e));
        }
    }
    public static ValueTask<DSPR<T>> DisposeAsync<T, E>(this Task<DSPR<T>> taskSpr, E index) where E : Enum =>
        DisposeAsync(taskSpr, Convert.ToInt16(index));

    [OverloadResolutionPriority(1)]
    public static async ValueTask<DSPR<T>> DisposeAsync<T>(this ValueTask<DSPR<T>> taskSpr, short index = -1)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return await spr.DisposeAsync(index);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen([spr.Value.Payload], e));
        }
    }
    [OverloadResolutionPriority(1)]
    public static ValueTask<DSPR<T>> DisposeAsync<T, E>(this ValueTask<DSPR<T>> taskSpr, E index) where E : Enum =>
        DisposeAsync(taskSpr, Convert.ToInt16(index));


    public static async ValueTask<SPR<T>> DisposeAll<T>(this Task<DSPR<T>> taskSpr)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.DisposeAll();

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen([spr.Value.Payload], e);
        }
    }
    public static ValueTask<SPR<T>> DisposeAll<T, E>(this Task<DSPR<T>> taskSpr) =>
        DisposeAll(taskSpr);

    [OverloadResolutionPriority(1)]
    public static async ValueTask<SPR<T>> DisposeAll<T>(this ValueTask<DSPR<T>> taskSpr)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.DisposeAll();

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen([spr.Value.Payload], e);
        }
    }
    [OverloadResolutionPriority(1)]
    public static ValueTask<SPR<T>> DisposeAll<T, E>(this ValueTask<DSPR<T>> taskSpr) =>
        DisposeAll(taskSpr);


    public static async ValueTask<SPR<T>> DisposeAllAsync<T>(this Task<DSPR<T>> taskSpr)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return await spr.DisposeAllAsync();

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen([spr.Value.Payload], e);
        }
    }
    public static ValueTask<SPR<T>> DisposeAllAsync<T, E>(this Task<DSPR<T>> taskSpr) =>
        DisposeAllAsync(taskSpr);

    [OverloadResolutionPriority(1)]
    public static async ValueTask<SPR<T>> DisposeAllAsync<T>(this ValueTask<DSPR<T>> taskSpr)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return await spr.DisposeAllAsync();

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen([spr.Value.Payload], e);
        }
    }
    [OverloadResolutionPriority(1)]
    public static ValueTask<SPR<T>> DisposeAllAsync<T, E>(this ValueTask<DSPR<T>> taskSpr) =>
        DisposeAllAsync(taskSpr);
}