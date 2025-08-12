using EGO.Gladius.Contracts;
using EGO.Gladius.DataTypes;

using System.Runtime.CompilerServices;
using System.Transactions;

namespace EGO.Gladius.Extensions;

public static class MigratorExtensions_SPR_TO_DSPR
{
    public static DSPR<T> MarkDispose<T>(this SPR<T> spr, short index = -1) where T : IDisposable, IAsyncDisposable =>
        new DSPR<T>(spr.Value, spr.Fault).MarkDispose(index);

    public static async Task<DSPR<T>> MarkDispose<T>(this Task<SPR<T>> taskSpr, short index = -1) where T : IDisposable, IAsyncDisposable
    {
        var spr = await taskSpr;
        return new DSPR<T>(spr.Value, spr.Fault).MarkDispose(index);
    }

    [OverloadResolutionPriority(1)]
    public static async Task<DSPR<T>> MarkDispose<T>(this ValueTask<SPR<T>> taskSpr, short index = -1) where T : IDisposable, IAsyncDisposable
    {
        var spr = await taskSpr;
        return new DSPR<T>(spr.Value, spr.Fault).MarkDispose(index);
    }
}

public static class MigratorExtensions_TSPR_TO_TDSPR
{
    public static TDSPR<T> MarkDispose<T>(this TSPR<T> spr, short index = -1) where T : IDisposable, IAsyncDisposable =>
        new TDSPR<T>(spr.Value, spr.Fault, ((ITSP)spr).Transactions).MarkDispose(index);

    public static async Task<TDSPR<T>> MarkDispose<T>(this Task<TSPR<T>> taskSpr, short index = -1) where T : IDisposable, IAsyncDisposable
    {
        var spr = await taskSpr;
        return new TDSPR<T>(spr.Value, spr.Fault, ((ITSP)spr).Transactions).MarkDispose(index);
    }

    [OverloadResolutionPriority(1)]
    public static async Task<TDSPR<T>> MarkDispose<T>(this ValueTask<TSPR<T>> taskSpr, short index = -1) where T : IDisposable, IAsyncDisposable
    {
        var spr = await taskSpr;
        return new TDSPR<T>(spr.Value, spr.Fault, ((ITSP)spr).Transactions).MarkDispose(index);
    }
}

public static class MigratorExtensions_SPR_TO_TSPR
{
    public static TSPR<T> MarkScope<T>(this SPR<T> spr, short index = -1) =>
        new TSPR<T>(spr.Value, spr.Fault).MarkScope(index);

    public static async Task<TSPR<T>> MarkScope<T>(this Task<SPR<T>> taskSpr, short index = -1)
    {
        var spr = await taskSpr;
        return new TSPR<T>(spr.Value, spr.Fault).MarkScope(index);
    }

    [OverloadResolutionPriority(1)]
    public static async Task<TSPR<T>> MarkScope<T>(this ValueTask<SPR<T>> taskSpr, short index = -1)
    {
        var spr = await taskSpr;
        return new TSPR<T>(spr.Value, spr.Fault).MarkScope(index);
    }
}

public static class MigratorExtensions_DSPR_TO_TDSPR
{
    public static TDSPR<T> MarkScope<T>(this DSPR<T> spr, short index = -1) =>
        new TDSPR<T>(spr.Value, spr.Fault, ((IDSP)spr).Disposables, ((IDSP)spr).AsyncDisposables).MarkScope(index);

    public static async Task<TDSPR<T>> MarkScope<T>(this Task<DSPR<T>> taskSpr, short index = -1)
    {
        var spr = await taskSpr;
        return new TDSPR<T>(spr.Value, spr.Fault, ((IDSP)spr).Disposables, ((IDSP)spr).AsyncDisposables).MarkScope(index);
    }

    [OverloadResolutionPriority(1)]
    public static async Task<TDSPR<T>> MarkScope<T>(this ValueTask<DSPR<T>> taskSpr, short index = -1)
    {
        var spr = await taskSpr;
        return new TDSPR<T>(spr.Value, spr.Fault, ((IDSP)spr).Disposables, ((IDSP)spr).AsyncDisposables).MarkScope(index);
    }
}