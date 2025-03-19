using EGO.Gladius.DataTypes;

using System.Diagnostics.CodeAnalysis;

namespace EGO.Gladius.Extensions;

public static class DSPRExtensions
{

    public static async Task<DSPR<R>> Transform<T, R>(this Task<DSPR<T>> task, [NotNull] Func<T, R> del) where T : IDisposable, IAsyncDisposable
    {
        var taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out var res))
                return taskDSPR.Fault<R>();

            return taskDSPR.Pass(del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.Fault<R>(SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async Task<DSPR<R>> Transform<T, R>(this Task<DSPR<T>> task, [NotNull] Func<T, SPR<R>> del) where T : IDisposable, IAsyncDisposable
    {
        var taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out var res))
                return taskDSPR.Fault<R>();

            return taskDSPR.Pass(del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.Fault<R>(SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async Task<DSPR<R>> Transform<T, R>(this Task<DSPR<T>> task, [NotNull] Func<T, Task<R>> del)
    {
        var taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out var res))
                return taskDSPR.Fault<R>();

            return taskDSPR.Pass(await del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.Fault<R>(SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async Task<DSPR<R>> Transform<T, R>(this Task<DSPR<T>> task, [NotNull] Func<T, Task<SPR<R>>> del)
    {
        var taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out var res))
                return taskDSPR.Fault<R>();

            return taskDSPR.Pass(await del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.Fault<R>(SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async ValueTask<DSPR<R>> Transform<T, R>(this ValueTask<DSPR<T>> task, [NotNull] Func<T, R> del)
    {
        var taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out var res))
                return taskDSPR.Fault<R>();

            return taskDSPR.Pass(del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.Fault<R>(SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async ValueTask<DSPR<R>> Transform<T, R>(this ValueTask<DSPR<T>> task, [NotNull] Func<T, SPR<R>> del)
    {
        var taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out var res))
                return taskDSPR.Fault<R>();

            return taskDSPR.Pass(del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.Fault<R>(SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async ValueTask<DSPR<R>> Transform<T, R>(this ValueTask<DSPR<T>> task, [NotNull] Func<T, Task<R>> del)
    {
        var taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out var res))
                return taskDSPR.Fault<R>();

            return taskDSPR.Pass(await del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.Fault<R>(SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async ValueTask<DSPR<R>> Transform<T, R>(this ValueTask<DSPR<T>> task, [NotNull] Func<T, Task<SPR<R>>> del)
    {
        var taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out var res))
                return taskDSPR.Fault<R>();

            return taskDSPR.Pass(await del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.Fault<R>(SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async ValueTask<DSPR<R>> Transform<T, R>(this ValueTask<DSPR<T>> task, [NotNull] Func<T, ValueTask<R>> del)
    {
        var taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out var res))
                return taskDSPR.Fault<R>();

            return taskDSPR.Pass(await del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.Fault<R>(SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async ValueTask<DVSP> Transform<T>(this ValueTask<DSPR<T>> task, [NotNull] Func<T, ValueTask<VSP>> del)
    {
        var taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out var res))
                return taskDSPR.ToDVSP();

            return taskDSPR.ToDVSP().Pass(await del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.ToDVSP(SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async ValueTask<DSPR<R>> Transform<T, R>(this ValueTask<DSPR<T>> task, [NotNull] Func<T, ValueTask<SPR<R>>> del)
    {
        var taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out var res))
                return taskDSPR.Fault<R>();

            return taskDSPR.Pass(await del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.Fault<R>(SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async ValueTask<DSPR<R>> Transform<T, R>(this Task<DSPR<T>> task, [NotNull] Func<T, ValueTask<R>> del)
    {
        var taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out var res))
                return taskDSPR.Fault<R>();

            return taskDSPR.Pass(await del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.Fault<R>(SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async ValueTask<DSPR<R>> Transform<T, R>(this Task<DSPR<T>> task, [NotNull] Func<T, ValueTask<SPR<R>>> del)
    {
        var taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out var res))
                return taskDSPR.Fault<R>();

            return taskDSPR.Pass(await del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.Fault<R>(SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async ValueTask<DSPR<T>> MarkDispose<T>(this ValueTask<DSPR<T>> task, int index = 0) =>
        (await task).MarkDispose(index);

    public static async ValueTask<DSPR<T>> MarkDispose<T, E>(this ValueTask<DSPR<T>> task, E index) where E : Enum =>
        (await task).MarkDispose(index);

    public static async ValueTask<DSPR<T>> MarkScope<T>(this ValueTask<DSPR<T>> task, int index = 0) =>
        (await task).MarkScope(index);

    public static async ValueTask<DSPR<T>> MarkScope<T, E>(this ValueTask<DSPR<T>> task, E index) where E : Enum =>
        (await task).MarkScope(index);

#pragma warning disable CA1849 // Call async methods when in an async method
    public static async ValueTask<DSPR<T>> Dispose<T>(this ValueTask<DSPR<T>> task, int index = -1) =>
        (await task).Dispose(index);

    public static async ValueTask<DSPR<T>> Dispose<T, E>(this ValueTask<DSPR<T>> task, E index) where E : Enum =>
        (await task).Dispose(index);

    public static async ValueTask<DSPR<T>> CompleteScope<T>(this ValueTask<DSPR<T>> task, int index = -1) =>
        (await task).CompleteScope(index);

    public static async ValueTask<DVSP> CompleteScope(this ValueTask<DVSP> task, int index = -1) =>
        (await task).CompleteScope(index);

    public static async ValueTask<DSPR<T>> CompleteScope<T, E>(this ValueTask<DSPR<T>> task, E index) where E : Enum =>
        (await task).CompleteScope(index);

    public static async ValueTask<DVSP> CompleteScope<E>(this ValueTask<DVSP> task, E index) where E : Enum =>
        (await task).CompleteScope(index);

    public static async ValueTask<DSPR<T>> DisposeScope<T>(this ValueTask<DSPR<T>> task, int index = -1) =>
        (await task).DisposeScope(index);

    public static async ValueTask<DSPR<T>> DisposeScope<T, E>(this ValueTask<DSPR<T>> task, E index) where E : Enum =>
        (await task).DisposeScope(index);

    public static async ValueTask<SPR<T>> DisposeAll<T>(this ValueTask<DSPR<T>> task) =>
        (await task).DisposeAll();

#pragma warning restore CA1849 // Call async methods when in an async method
    public static async ValueTask<DVSP> ToDVSP<T>(this ValueTask<DSPR<T>> task)
    {
        var taskDSPR = await task;
        return taskDSPR.ToDVSP();
    }
    public static async ValueTask<SPR<T>> ToSPR<T>(this ValueTask<DSPR<T>> task)
    {
        var taskDSPR = await task;
        return taskDSPR.SPR;
    }
}
