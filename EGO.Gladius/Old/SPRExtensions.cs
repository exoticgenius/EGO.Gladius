using EGO.Gladius.DataTypes;

using System.Diagnostics.CodeAnalysis;

namespace EGO.Gladius.Old;

public static class SPRExtensions
{
    public static async Task<O_SPR<R>> Transform<T, R>(this Task<O_SPR<T>> task, Func<T, R> del)
    {
        O_SPR<T> taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out T? res))
                return taskSPR.Fault;

            return del(res);
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static async Task<O_SPR<R>> Transform<T, R>(this Task<O_SPR<T>> task, Func<T, O_SPR<R>> del)
    {
        O_SPR<T> taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out T? res))
                return taskSPR.Fault;

            return del(res);
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static async Task<O_SPR<R>> Transform<T, R>(this Task<O_SPR<T>> task, Func<T, ValueTask<R>> del)
    {
        O_SPR<T> taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out T? res))
                return taskSPR.Fault;

            return await del(res);
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static async Task<O_SPR<R>> Transform<T, R>(this Task<O_SPR<T>> task, Func<T, Task<O_SPR<R>>> del)
    {
        O_SPR<T> taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out T? res))
                return taskSPR.Fault;

            return await del(res);
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static async ValueTask<O_SPR<R>> Transform<T, R>(this ValueTask<O_SPR<T>> task, Func<T, R> del)
    {
        O_SPR<T> taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out T? res))
                return taskSPR.Fault;

            return del(res);
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static async ValueTask<O_SPR<R>> Transform<T, R>(this ValueTask<O_SPR<T>> task, Func<T, O_SPR<R>> del)
    {
        O_SPR<T> taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out T? res))
                return taskSPR.Fault;

            return del(res);
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static async ValueTask<O_SPR<R>> Transform<T, R>(this ValueTask<O_SPR<T>> task, Func<T, Task<O_SPR<R>>> del)
    {
        O_SPR<T> taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out T? res))
                return taskSPR.Fault;

            return await del(res);
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static async ValueTask<O_SPR<R>> Transform<T, R>(this ValueTask<O_SPR<T>> task, Func<T, ValueTask<R>> del)
    {
        O_SPR<T> taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out T? res))
                return taskSPR.Fault;

            return await del(res);
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static async ValueTask<O_SPR<R>> Transform<T, R>(this ValueTask<O_SPR<T>> task, Func<T, ValueTask<O_SPR<R>>> del)
    {
        O_SPR<T> taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out T? res))
                return taskSPR.Fault;

            return await del(res);
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static async ValueTask<O_SPR<R>> Transform<T, R>(this Task<O_SPR<T>> task, Func<T, ValueTask<O_SPR<R>>> del)
    {
        O_SPR<T> taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out T? res))
                return taskSPR.Fault;

            return await del(res);
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static async ValueTask<O_VSP> Transform<T>(this Task<O_SPR<T>> task, Func<T, ValueTask<O_VSP>> del)
    {
        O_SPR<T> taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out T? res))
                return taskSPR.Fault;

            return await del(res);
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static async ValueTask<O_VSP> ToVSP<T>(this ValueTask<O_SPR<T>> task)
    {
        O_SPR<T> taskSPR = await task;

        if (!taskSPR.Succeed())
            return taskSPR.Fault;

        return O_VSP.Completed;
    }

    public static async ValueTask<O_VSP> ToVSP<T>(this Task<O_SPR<T>> task)
    {
        O_SPR<T> taskSPR = await task;

        if (!taskSPR.Succeed())
            return taskSPR.Fault;

        return O_VSP.Completed;
    }

    public static async ValueTask<O_VSP> ToVSP<T>(this ValueTask<O_SPR<T>> task, [NotNull] Func<T, O_VSP> del)
    {
        O_SPR<T> taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out T? res))
                return taskSPR.Fault;

            return del(res);
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static async ValueTask<O_VSP> ToVSP<T>(this Task<O_SPR<T>> task, [NotNull] Func<T, O_VSP> del)
    {
        O_SPR<T> taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out T? res))
                return taskSPR.Fault;

            return del(res);
        }
        catch (Exception e)
        {
            return O_SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static O_VSP ToVSP<T>(this O_SPR<T> task)
    {
        if (!task.Succeed())
            return task.Fault;

        return O_VSP.Completed;
    }
}
