using EGO.Gladius.DataTypes;

using System.Diagnostics.CodeAnalysis;

namespace EGO.Gladius.Old;

public static class DSPRExtensions
{

    public static async Task<O_DSPR<R>> Transform<T, R>(this Task<O_DSPR<T>> task, [NotNull] Func<T, R> del) where T : IDisposable, IAsyncDisposable
    {
        O_DSPR<T> taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out T? res))
                return taskDSPR.Fault<R>();

            return taskDSPR.Pass(del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.Fault<R>(O_SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async Task<O_DSPR<R>> Transform<T, R>(this Task<O_DSPR<T>> task, [NotNull] Func<T, O_SPR<R>> del) where T : IDisposable, IAsyncDisposable
    {
        O_DSPR<T> taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out T? res))
                return taskDSPR.Fault<R>();

            return taskDSPR.Pass(del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.Fault<R>(O_SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async Task<O_DSPR<R>> Transform<T, R>(this Task<O_DSPR<T>> task, [NotNull] Func<T, Task<R>> del)
    {
        O_DSPR<T> taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out T? res))
                return taskDSPR.Fault<R>();

            return taskDSPR.Pass(await del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.Fault<R>(O_SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async Task<O_DSPR<R>> Transform<T, R>(this Task<O_DSPR<T>> task, [NotNull] Func<T, Task<O_SPR<R>>> del)
    {
        O_DSPR<T> taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out T? res))
                return taskDSPR.Fault<R>();

            return taskDSPR.Pass(await del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.Fault<R>(O_SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async ValueTask<O_DSPR<R>> Transform<T, R>(this ValueTask<O_DSPR<T>> task, [NotNull] Func<T, R> del)
    {
        O_DSPR<T> taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out T? res))
                return taskDSPR.Fault<R>();

            return taskDSPR.Pass(del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.Fault<R>(O_SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async ValueTask<O_DSPR<R>> Transform<T, R>(this ValueTask<O_DSPR<T>> task, [NotNull] Func<T, O_SPR<R>> del)
    {
        O_DSPR<T> taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out T? res))
                return taskDSPR.Fault<R>();

            return taskDSPR.Pass(del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.Fault<R>(O_SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async ValueTask<O_DSPR<R>> Transform<T, R>(this ValueTask<O_DSPR<T>> task, [NotNull] Func<T, Task<R>> del)
    {
        O_DSPR<T> taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out T? res))
                return taskDSPR.Fault<R>();

            return taskDSPR.Pass(await del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.Fault<R>(O_SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async ValueTask<O_DSPR<R>> Transform<T, R>(this ValueTask<O_DSPR<T>> task, [NotNull] Func<T, Task<O_SPR<R>>> del)
    {
        O_DSPR<T> taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out T? res))
                return taskDSPR.Fault<R>();

            return taskDSPR.Pass(await del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.Fault<R>(O_SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async ValueTask<O_DSPR<R>> Transform<T, R>(this ValueTask<O_DSPR<T>> task, [NotNull] Func<T, ValueTask<R>> del)
    {
        O_DSPR<T> taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out T? res))
                return taskDSPR.Fault<R>();

            return taskDSPR.Pass(await del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.Fault<R>(O_SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async ValueTask<O_DVSP> Transform<T>(this ValueTask<O_DSPR<T>> task, [NotNull] Func<T, ValueTask<O_VSP>> del)
    {
        O_DSPR<T> taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out T? res))
                return taskDSPR.ToDVSP();

            return taskDSPR.ToDVSP().Pass(await del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.ToDVSP(O_SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async ValueTask<O_DSPR<R>> Transform<T, R>(this ValueTask<O_DSPR<T>> task, [NotNull] Func<T, ValueTask<O_SPR<R>>> del)
    {
        O_DSPR<T> taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out T? res))
                return taskDSPR.Fault<R>();

            return taskDSPR.Pass(await del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.Fault<R>(O_SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async ValueTask<O_DSPR<R>> Transform<T, R>(this Task<O_DSPR<T>> task, [NotNull] Func<T, ValueTask<R>> del)
    {
        O_DSPR<T> taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out T? res))
                return taskDSPR.Fault<R>();

            return taskDSPR.Pass(await del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.Fault<R>(O_SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async ValueTask<O_DSPR<R>> Transform<T, R>(this Task<O_DSPR<T>> task, [NotNull] Func<T, ValueTask<O_SPR<R>>> del)
    {
        O_DSPR<T> taskDSPR = await task;
        try
        {
            if (!taskDSPR.Succeed(out T? res))
                return taskDSPR.Fault<R>();

            return taskDSPR.Pass(await del(res));
        }
        catch (Exception e)
        {
            return taskDSPR.Fault<R>(O_SPF.Gen(del.Method, [taskDSPR.SPR.ExtractPayload()], e));
        }
    }

    public static async ValueTask<O_DSPR<T>> MarkDispose<T>(this ValueTask<O_DSPR<T>> task, int index = 0) =>
        (await task).MarkDispose(index);

    public static async ValueTask<O_DSPR<T>> MarkDispose<T, E>(this ValueTask<O_DSPR<T>> task, E index) where E : Enum =>
        (await task).MarkDispose(index);

    public static async ValueTask<O_DSPR<T>> MarkScope<T>(this ValueTask<O_DSPR<T>> task, int index = 0) =>
        throw new Exception(); //(await task).MarkScope(index);

    public static async ValueTask<O_DSPR<T>> MarkScope<T, E>(this ValueTask<O_DSPR<T>> task, E index) where E : Enum =>
        throw new Exception(); //(await task).MarkScope(index);

#pragma warning disable CA1849 // Call async methods when in an async method
    public static async ValueTask<O_DSPR<T>> Dispose<T>(this ValueTask<O_DSPR<T>> task, int index = -1) =>
        (await task).Dispose(index);

    public static async ValueTask<O_DSPR<T>> Dispose<T, E>(this ValueTask<O_DSPR<T>> task, E index) where E : Enum =>
        (await task).Dispose(index);

    public static async ValueTask<O_DSPR<T>> CompleteScope<T>(this ValueTask<O_DSPR<T>> task, int index = -1) =>
        throw new Exception(); //(await task).CompleteScope(index);

    public static async ValueTask<O_DVSP> CompleteScope(this ValueTask<O_DVSP> task, int index = -1) =>
        (await task).CompleteScope(index);

    public static async ValueTask<O_DSPR<T>> CompleteScope<T, E>(this ValueTask<O_DSPR<T>> task, E index) where E : Enum =>
        throw new Exception(); //(await task).CompleteScope(index);

    public static async ValueTask<O_DVSP> CompleteScope<E>(this ValueTask<O_DVSP> task, E index) where E : Enum =>
        (await task).CompleteScope(index);

    public static async ValueTask<O_DSPR<T>> DisposeScope<T>(this ValueTask<O_DSPR<T>> task, int index = -1) =>
        throw new Exception(); //(await task).DisposeScope(index);

    public static async ValueTask<O_DSPR<T>> DisposeScope<T, E>(this ValueTask<O_DSPR<T>> task, E index) where E : Enum =>
       throw new Exception();// (await task).DisposeScope(index);

    public static async ValueTask<O_SPR<T>> DisposeAll<T>(this ValueTask<O_DSPR<T>> task) =>
        (await task).DisposeAll();

#pragma warning restore CA1849 // Call async methods when in an async method
    public static async ValueTask<O_DVSP> ToDVSP<T>(this ValueTask<O_DSPR<T>> task)
    {
        O_DSPR<T> taskDSPR = await task;
        return taskDSPR.ToDVSP();
    }
    public static async ValueTask<O_SPR<T>> ToSPR<T>(this ValueTask<O_DSPR<T>> task)
    {
        O_DSPR<T> taskDSPR = await task;
        return taskDSPR.SPR;
    }
}
