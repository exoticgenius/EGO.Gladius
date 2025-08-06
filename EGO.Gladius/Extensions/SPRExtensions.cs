using EGO.Gladius.Contracts;
using EGO.Gladius.DataTypes;

namespace EGO.Gladius.Extensions;

public static class SPR_Sync_To
{
    public static SPR<R> To<T, R>(this SPR<T> spr, Func<T, R> del)
    {
        try
        {
            if (spr.Succeed())
                return new SPR<R>(del(spr.Value.Payload));

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static SPR<R> To<T, R>(this SPR<T> spr, Func<T, SPR<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return del(spr.Value.Payload);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static SPR<R> To<T, R>(this SPR<T> spr, Func<SPR<T>, SPR<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return del(spr);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }
}
public static class SPR_Sync_See
{
    public static SPR<T> See<T>(this SPR<T> spr, Action<T> del)
    {
        try
        {
            if (spr.Succeed())
                del(spr.Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static SPR<T> See<T>(this SPR<T> spr, Action<SPR<T>> del)
    {
        try
        {
            if (spr.Succeed())
                del(spr);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }
}


public static class SPR_To_Async_To
{
    public static async ValueTask<SPR<R>> To<T, R>(this SPR<T> spr, Func<T, ValueTask<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return new SPR<R>(await del(spr.Value.Payload));

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<R>> To<T, R>(this SPR<T> spr, Func<T, Task<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return new SPR<R>(await del(spr.Value.Payload));

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }


    public static async ValueTask<SPR<R>> To<T, R>(this SPR<T> spr, Func<T, ValueTask<SPR<R>>> del)
    {
        try
        {
            if (spr.Succeed())
                return await del(spr.Value.Payload);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<R>> To<T, R>(this SPR<T> spr, Func<T, Task<SPR<R>>> del)
    {
        try
        {
            if (spr.Succeed())
                return await del(spr.Value.Payload);
            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }


    public static async ValueTask<SPR<R>> To<T, R>(this SPR<T> spr, Func<SPR<T>, ValueTask<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return new SPR<R>(await del(spr));

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<R>> To<T, R>(this SPR<T> spr, Func<SPR<T>, Task<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return new SPR<R>(await del(spr));

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }


    public static async ValueTask<SPR<R>> To<T, R>(this SPR<T> spr, Func<SPR<T>, ValueTask<SPR<R>>> del)
    {
        try
        {
            if (spr.Succeed())
                return await del(spr);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<R>> To<T, R>(this SPR<T> spr, Func<SPR<T>, Task<SPR<R>>> del)
    {
        try
        {
            if (spr.Succeed())
                return await del(spr);
            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }
}
public static class SPR_To_Async_See
{
    public static async ValueTask<SPR<T>> See<T>(this SPR<T> spr, Func<T, ValueTask> del)
    {
        try
        {
            if (spr.Succeed())
                await del(spr.Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<T>> See<T>(this SPR<T> spr, Func<T, Task> del)
    {
        try
        {
            if (spr.Succeed())
                await del(spr.Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<T>> See<T>(this SPR<T> spr, Func<SPR<T>, ValueTask> del)
    {
        try
        {
            if (spr.Succeed())
                await del(spr);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<T>> See<T>(this SPR<T> spr, Func<SPR<T>, Task> del)
    {
        try
        {
            if (spr.Succeed())
                await del(spr);
            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }
}


public static class SPR_From_ValueTask_To
{
    public static async ValueTask<SPR<R>> To<T, R>(this ValueTask<SPR<T>> taskSpr, Func<T, R> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return new SPR<R>(del(spr.Value.Payload));

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<R>> To<T, R>(this ValueTask<SPR<T>> taskSpr, Func<T, SPR<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return del(spr.Value.Payload);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<R>> To<T, R>(this ValueTask<SPR<T>> taskSpr, Func<SPR<T>, SPR<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return del(spr);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }


    public static async ValueTask<SPR<R>> To<T, R>(this ValueTask<SPR<T>> taskSpr, Func<T, Task<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return new SPR<R>(await del(spr.Value.Payload));

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<R>> To<T, R>(this ValueTask<SPR<T>> taskSpr, Func<T, Task<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return await del(spr.Value.Payload);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<R>> To<T, R>(this ValueTask<SPR<T>> taskSpr, Func<SPR<T>, Task<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return await del(spr);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }


    public static async ValueTask<SPR<R>> To<T, R>(this ValueTask<SPR<T>> taskSpr, Func<T, ValueTask<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return new SPR<R>(await del(spr.Value.Payload));

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<R>> To<T, R>(this ValueTask<SPR<T>> taskSpr, Func<T, ValueTask<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return await del(spr.Value.Payload);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<R>> To<T, R>(this ValueTask<SPR<T>> taskSpr, Func<SPR<T>, ValueTask<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return await del(spr);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }
}
public static class SPR_From_ValueTask_See
{
    public static async ValueTask<SPR<T>> See<T>(this ValueTask<SPR<T>> taskSpr, Action<T> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                del(spr.Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<T>> See<T>(this ValueTask<SPR<T>> taskSpr, Action<SPR<T>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                del(spr);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }


    public static async ValueTask<SPR<T>> See<T>(this ValueTask<SPR<T>> taskSpr, Func<T, Task> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(spr.Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<T>> See<T>(this ValueTask<SPR<T>> taskSpr, Func<SPR<T>, Task> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(spr);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }


    public static async ValueTask<SPR<T>> See<T>(this ValueTask<SPR<T>> taskSpr, Func<T, ValueTask> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(spr.Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<T>> See<T>(this ValueTask<SPR<T>> taskSpr, Func<SPR<T>, ValueTask> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(spr);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }
}


public static class SPR_From_Task_To
{
    public static async ValueTask<SPR<R>> To<T, R>(this Task<SPR<T>> taskSpr, Func<T, R> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return new SPR<R>(del(spr.Value.Payload));

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<R>> To<T, R>(this Task<SPR<T>> taskSpr, Func<T, SPR<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return del(spr.Value.Payload);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<R>> To<T, R>(this Task<SPR<T>> taskSpr, Func<SPR<T>, SPR<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return del(spr);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }


    public static async ValueTask<SPR<R>> To<T, R>(this Task<SPR<T>> taskSpr, Func<T, Task<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return new SPR<R>(await del(spr.Value.Payload));

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<R>> To<T, R>(this Task<SPR<T>> taskSpr, Func<T, Task<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return await del(spr.Value.Payload);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<R>> To<T, R>(this Task<SPR<T>> taskSpr, Func<SPR<T>, Task<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return await del(spr);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }


    public static async ValueTask<SPR<R>> To<T, R>(this Task<SPR<T>> taskSpr, Func<T, ValueTask<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return new SPR<R>(await del(spr.Value.Payload));

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<R>> To<T, R>(this Task<SPR<T>> taskSpr, Func<T, ValueTask<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return await del(spr.Value.Payload);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<R>> To<T, R>(this Task<SPR<T>> taskSpr, Func<SPR<T>, ValueTask<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return await del(spr);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }
}
public static class SPR_From_Task_See
{
    public static async ValueTask<SPR<T>> See<T>(this Task<SPR<T>> taskSpr, Action<T> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                del(spr.Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<T>> See<T>(this Task<SPR<T>> taskSpr, Action<SPR<T>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                del(spr);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }


    public static async ValueTask<SPR<T>> See<T>(this Task<SPR<T>> taskSpr, Func<T, Task> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(spr.Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<T>> See<T>(this Task<SPR<T>> taskSpr, Func<SPR<T>, Task> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(spr);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }


    public static async ValueTask<SPR<T>> See<T>(this Task<SPR<T>> taskSpr, Func<T, ValueTask> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(spr.Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<T>> See<T>(this Task<SPR<T>> taskSpr, Func<SPR<T>, ValueTask> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(spr);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }
}

public static class SPR_Fault_Handlers
{
    public static SPR<T> HandleFault<T>(this SPR<T> spr, Func<SPR<T>, SPR<T>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr;

            return del(spr);
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<T>> HandleFault<T>(this SPR<T> spr, Func<SPR<T>, ValueTask<SPR<T>>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr;

            return await del(spr);
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }
}
public static class SPR_Null_Handlers
{
    public static SPR<T> HandleNull<T>(this SPR<T> spr, Func<SPR<T>, SPR<T>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr;

            if (spr.HasValue())
                return spr;

            return del(spr);
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<T>> HandleNull<T>(this SPR<T> spr, Func<SPR<T>, ValueTask<SPR<T>>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr;

            if (spr.HasValue())
                return spr;

            return await del(spr);
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [spr.Value.Payload], e);
        }
    }
}

public static class SPR_To_Void
{
    public static async ValueTask<VSP> Void<T>(this ValueTask<SPR<T>> taskSpr)
    {
        try
        {
            var spr = await taskSpr;
            return spr.Void();
        }
        catch (Exception e)
        {
            return SPF.Gen(e);
        }
    }

    public static async ValueTask<VSP> Void<T>(this Task<SPR<T>> taskSpr)
    {
        try
        {
            var spr = await taskSpr;
            return spr.Void();
        }
        catch (Exception e)
        {
            return SPF.Gen(e);
        }
    }
}