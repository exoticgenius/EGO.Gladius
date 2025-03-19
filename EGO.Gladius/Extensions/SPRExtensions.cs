using EGO.Gladius.DataTypes;

using System.Diagnostics.CodeAnalysis;

namespace EGO.Gladius.Extensions;

public static class SPRExtensions
{
    public static async Task<SPR<R>> Transform<T, R>(this Task<SPR<T>> task, Func<T, R> del)
    {
        var taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out var res))
                return taskSPR.Fault;

            return del(res);
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static async Task<SPR<R>> Transform<T, R>(this Task<SPR<T>> task, Func<T, SPR<R>> del)
    {
        var taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out var res))
                return taskSPR.Fault;

            return del(res);
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static async Task<SPR<R>> Transform<T, R>(this Task<SPR<T>> task, Func<T, ValueTask<R>> del)
    {
        var taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out var res))
                return taskSPR.Fault;

            return await del(res);
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static async Task<SPR<R>> Transform<T, R>(this Task<SPR<T>> task, Func<T, Task<SPR<R>>> del)
    {
        var taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out var res))
                return taskSPR.Fault;

            return await del(res);
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static async ValueTask<SPR<R>> Transform<T, R>(this ValueTask<SPR<T>> task, Func<T, R> del)
    {
        var taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out var res))
                return taskSPR.Fault;

            return del(res);
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static async ValueTask<SPR<R>> Transform<T, R>(this ValueTask<SPR<T>> task, Func<T, SPR<R>> del)
    {
        var taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out var res))
                return taskSPR.Fault;

            return del(res);
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    //public static async ValueTask<SPR<R>> Transform<T, R>(this ValueTask<SPR<T>> task, Func<T, Task<R>> del)
    //{
    //	var taskSPR = await task;
    //	try
    //	{
    //		if (!taskSPR.Succeed(out var res))
    //			return taskSPR.Fault;

    //		return await del(res);
    //	}
    //	catch (Exception e)
    //	{
    //		return SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
    //	}
    //}

    public static async ValueTask<SPR<R>> Transform<T, R>(this ValueTask<SPR<T>> task, Func<T, Task<SPR<R>>> del)
    {
        var taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out var res))
                return taskSPR.Fault;

            return await del(res);
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static async ValueTask<SPR<R>> Transform<T, R>(this ValueTask<SPR<T>> task, Func<T, ValueTask<R>> del)
    {
        var taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out var res))
                return taskSPR.Fault;

            return await del(res);
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static async ValueTask<SPR<R>> Transform<T, R>(this ValueTask<SPR<T>> task, Func<T, ValueTask<SPR<R>>> del)
    {
        var taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out var res))
                return taskSPR.Fault;

            return await del(res);
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static async ValueTask<SPR<R>> Transform<T, R>(this Task<SPR<T>> task, Func<T, ValueTask<SPR<R>>> del)
    {
        var taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out var res))
                return taskSPR.Fault;

            return await del(res);
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static async ValueTask<VSP> Transform<T>(this Task<SPR<T>> task, Func<T, ValueTask<VSP>> del)
    {
        var taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out var res))
                return taskSPR.Fault;

            return await del(res);
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static async ValueTask<VSP> ToVSP<T>(this ValueTask<SPR<T>> task)
    {
        var taskSPR = await task;

        if (!taskSPR.Succeed())
            return taskSPR.Fault;

        return VSP.Completed;
    }

    public static async ValueTask<VSP> ToVSP<T>(this Task<SPR<T>> task)
    {
        var taskSPR = await task;

        if (!taskSPR.Succeed())
            return taskSPR.Fault;

        return VSP.Completed;
    }

    public static async ValueTask<VSP> ToVSP<T>(this ValueTask<SPR<T>> task, [NotNull] Func<T, VSP> del)
    {
        var taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out var res))
                return taskSPR.Fault;

            return del(res);
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static async ValueTask<VSP> ToVSP<T>(this Task<SPR<T>> task, [NotNull] Func<T, VSP> del)
    {
        var taskSPR = await task;
        try
        {
            if (!taskSPR.Succeed(out var res))
                return taskSPR.Fault;

            return del(res);
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [taskSPR.ExtractPayload()], e);
        }
    }

    public static VSP ToVSP<T>(this SPR<T> task)
    {
        if (!task.Succeed())
            return task.Fault;

        return VSP.Completed;
    }
}
