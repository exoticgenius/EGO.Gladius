using EGO.Gladius.DataTypes;

using System.Diagnostics.CodeAnalysis;

namespace EGO.Gladius.Extensions;

public static class VSPExtensions
{
    public static async Task<SPR<T>> Transform<T>(this Task<VSP> task, [NotNull] Func<T> del)
    {
        try
        {
            var taskSPR = await task;
            if (!taskSPR.Succeed())
                return taskSPR.Fault;

            return del();
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, e);
        }
    }

    public static async Task<SPR<T>> Transform<T>(this Task<VSP> task, [NotNull] Func<SPR<T>> del)
    {
        try
        {
            var taskSPR = await task;
            if (!taskSPR.Succeed())
                return taskSPR.Fault;

            return del();
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, e);
        }
    }

    public static async Task<SPR<T>> Transform<T>(this Task<VSP> task, [NotNull] Func<Task<T>> del)
    {
        try
        {
            var taskSPR = await task;
            if (!taskSPR.Succeed())
                return taskSPR.Fault;

            return await del();
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, e);
        }
    }

    public static async Task<SPR<T>> Transform<T>(this Task<VSP> task, [NotNull] Func<Task<SPR<T>>> del)
    {
        try
        {
            var taskSPR = await task;
            if (!taskSPR.Succeed())
                return taskSPR.Fault;

            return await del();
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, e);
        }
    }

    public static async ValueTask<SPR<T>> Transform<T>(this ValueTask<VSP> task, [NotNull] Func<T> del)
    {
        try
        {
            var taskSPR = await task;
            if (!taskSPR.Succeed())
                return taskSPR.Fault;

            return del();
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, e);
        }
    }

    public static async ValueTask<SPR<T>> Transform<T>(this ValueTask<VSP> task, [NotNull] Func<SPR<T>> del)
    {
        try
        {
            var taskSPR = await task;
            if (!taskSPR.Succeed())
                return taskSPR.Fault;

            return del();
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, e);
        }
    }

    public static async ValueTask<SPR<T>> Transform<T>(this ValueTask<VSP> task, [NotNull] Func<Task<T>> del)
    {
        try
        {
            var taskSPR = await task;
            if (!taskSPR.Succeed())
                return taskSPR.Fault;

            return await del();
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, e);
        }
    }

    public static async ValueTask<SPR<T>> Transform<T>(this ValueTask<VSP> task, [NotNull] Func<Task<SPR<T>>> del)
    {
        try
        {
            var taskSPR = await task;
            if (!taskSPR.Succeed())
                return taskSPR.Fault;

            return await del();
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, e);
        }
    }

    public static async ValueTask<SPR<T>> Transform<T>(this ValueTask<VSP> task, [NotNull] Func<ValueTask<T>> del)
    {
        try
        {
            var taskSPR = await task;
            if (!taskSPR.Succeed())
                return taskSPR.Fault;

            return await del();
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, e);
        }
    }

    public static async ValueTask<SPR<T>> Transform<T>(this ValueTask<VSP> task, [NotNull] Func<ValueTask<SPR<T>>> del)
    {
        try
        {
            var taskSPR = await task;
            if (!taskSPR.Succeed())
                return taskSPR.Fault;

            return await del();
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, e);
        }
    }

    public static async ValueTask<SPR<T>> Transform<T>(this Task<VSP> task, [NotNull] Func<ValueTask<T>> del)
    {
        try
        {
            var taskSPR = await task;
            if (!taskSPR.Succeed())
                return taskSPR.Fault;

            return await del();
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, e);
        }
    }

    public static async ValueTask<SPR<T>> Transform<T>(this Task<VSP> task, [NotNull] Func<ValueTask<SPR<T>>> del)
    {
        try
        {
            var taskSPR = await task;
            if (!taskSPR.Succeed())
                return taskSPR.Fault;

            return await del();
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, e);
        }
    }

}
