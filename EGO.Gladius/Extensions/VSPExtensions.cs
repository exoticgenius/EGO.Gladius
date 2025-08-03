using EGO.Gladius.DataTypes;

using System.Diagnostics.CodeAnalysis;

namespace EGO.Gladius.Extensions;

public static class VSPExtensions
{
    public static async Task<O_SPR<T>> Transform<T>(this Task<O_VSP> task, [NotNull] Func<T> del)
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
            return O_SPF.Gen(del.Method, e);
        }
    }

    public static async Task<O_SPR<T>> Transform<T>(this Task<O_VSP> task, [NotNull] Func<O_SPR<T>> del)
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
            return O_SPF.Gen(del.Method, e);
        }
    }

    public static async Task<O_SPR<T>> Transform<T>(this Task<O_VSP> task, [NotNull] Func<Task<T>> del)
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
            return O_SPF.Gen(del.Method, e);
        }
    }

    public static async Task<O_SPR<T>> Transform<T>(this Task<O_VSP> task, [NotNull] Func<Task<O_SPR<T>>> del)
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
            return O_SPF.Gen(del.Method, e);
        }
    }

    public static async ValueTask<O_SPR<T>> Transform<T>(this ValueTask<O_VSP> task, [NotNull] Func<T> del)
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
            return O_SPF.Gen(del.Method, e);
        }
    }

    public static async ValueTask<O_SPR<T>> Transform<T>(this ValueTask<O_VSP> task, [NotNull] Func<O_SPR<T>> del)
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
            return O_SPF.Gen(del.Method, e);
        }
    }

    public static async ValueTask<O_SPR<T>> Transform<T>(this ValueTask<O_VSP> task, [NotNull] Func<Task<T>> del)
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
            return O_SPF.Gen(del.Method, e);
        }
    }

    public static async ValueTask<O_SPR<T>> Transform<T>(this ValueTask<O_VSP> task, [NotNull] Func<Task<O_SPR<T>>> del)
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
            return O_SPF.Gen(del.Method, e);
        }
    }

    public static async ValueTask<O_SPR<T>> Transform<T>(this ValueTask<O_VSP> task, [NotNull] Func<ValueTask<T>> del)
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
            return O_SPF.Gen(del.Method, e);
        }
    }

    public static async ValueTask<O_SPR<T>> Transform<T>(this ValueTask<O_VSP> task, [NotNull] Func<ValueTask<O_SPR<T>>> del)
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
            return O_SPF.Gen(del.Method, e);
        }
    }

    public static async ValueTask<O_SPR<T>> Transform<T>(this Task<O_VSP> task, [NotNull] Func<ValueTask<T>> del)
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
            return O_SPF.Gen(del.Method, e);
        }
    }

    public static async ValueTask<O_SPR<T>> Transform<T>(this Task<O_VSP> task, [NotNull] Func<ValueTask<O_SPR<T>>> del)
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
            return O_SPF.Gen(del.Method, e);
        }
    }

}
