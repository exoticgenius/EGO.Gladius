using EGO.Gladius.DataTypes;

namespace EGO.Gladius.Contracts;

public static class SPR_Sync_To
{
    public static SPR<R> To<T, R>(this SPR<T> spr, Func<T, R> del)
    {
        try
        {
            if (spr.Succeed())
                return new SPR<R>(del(((ISP<T>)spr).Value.Payload));

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
        }
    }

    public static SPR<R> To<T, R>(this SPR<T> spr, Func<T, SPR<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return del(((ISP<T>)spr).Value.Payload);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
                del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
                return new SPR<R>(await del(((ISP<T>)spr).Value.Payload));

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<R>> To<T, R>(this SPR<T> spr, Func<T, Task<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return new SPR<R>(await del(((ISP<T>)spr).Value.Payload));

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
        }
    }


    public static async ValueTask<SPR<R>> To<T, R>(this SPR<T> spr, Func<T, ValueTask<SPR<R>>> del)
    {
        try
        {
            if (spr.Succeed())
                return await del(((ISP<T>)spr).Value.Payload);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<R>> To<T, R>(this SPR<T> spr, Func<T, Task<SPR<R>>> del)
    {
        try
        {
            if (spr.Succeed())
                return await del(((ISP<T>)spr).Value.Payload);
            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
                await del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<T>> See<T>(this SPR<T> spr, Func<T, Task> del)
    {
        try
        {
            if (spr.Succeed())
                await del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
                return new SPR<R>(del(((ISP<T>)spr).Value.Payload));

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<R>> To<T, R>(this ValueTask<SPR<T>> taskSpr, Func<T, SPR<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return del(((ISP<T>)spr).Value.Payload);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
        }
    }


    public static async ValueTask<SPR<R>> To<T, R>(this ValueTask<SPR<T>> taskSpr, Func<T, Task<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return new SPR<R>(await del(((ISP<T>)spr).Value.Payload));

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<R>> To<T, R>(this ValueTask<SPR<T>> taskSpr, Func<T, Task<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return await del(((ISP<T>)spr).Value.Payload);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
        }
    }


    public static async ValueTask<SPR<R>> To<T, R>(this ValueTask<SPR<T>> taskSpr, Func<T, ValueTask<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return new SPR<R>(await del(((ISP<T>)spr).Value.Payload));

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<R>> To<T, R>(this ValueTask<SPR<T>> taskSpr, Func<T, ValueTask<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return await del(((ISP<T>)spr).Value.Payload);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
                del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
        }
    }


    public static async ValueTask<SPR<T>> See<T>(this ValueTask<SPR<T>> taskSpr, Func<T, Task> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
        }
    }


    public static async ValueTask<SPR<T>> See<T>(this ValueTask<SPR<T>> taskSpr, Func<T, ValueTask> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
                return new SPR<R>(del(((ISP<T>)spr).Value.Payload));

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<R>> To<T, R>(this Task<SPR<T>> taskSpr, Func<T, SPR<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return del(((ISP<T>)spr).Value.Payload);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
        }
    }


    public static async ValueTask<SPR<R>> To<T, R>(this Task<SPR<T>> taskSpr, Func<T, Task<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return new SPR<R>(await del(((ISP<T>)spr).Value.Payload));

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<R>> To<T, R>(this Task<SPR<T>> taskSpr, Func<T, Task<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return await del(((ISP<T>)spr).Value.Payload);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
        }
    }


    public static async ValueTask<SPR<R>> To<T, R>(this Task<SPR<T>> taskSpr, Func<T, ValueTask<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return new SPR<R>(await del(((ISP<T>)spr).Value.Payload));

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
        }
    }

    public static async ValueTask<SPR<R>> To<T, R>(this Task<SPR<T>> taskSpr, Func<T, ValueTask<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return await del(((ISP<T>)spr).Value.Payload);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
                del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
        }
    }


    public static async ValueTask<SPR<T>> See<T>(this Task<SPR<T>> taskSpr, Func<T, Task> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
        }
    }


    public static async ValueTask<SPR<T>> See<T>(this Task<SPR<T>> taskSpr, Func<T, ValueTask> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
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
            return SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
        }
    }
}

///////////////////////////////////////////////

public static class DSPR_Sync_To
{
    public static DSPR<R> To<T, R>(this DSPR<T> spr, Func<T, R> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static DSPR<R> To<T, R>(this DSPR<T> spr, Func<T, SPR<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static DSPR<R> To<T, R>(this DSPR<T> spr, Func<DSPR<T>, SPR<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }
}
public static class DSPR_Sync_See
{
    public static DSPR<T> See<T>(this DSPR<T> spr, Action<T> del)
    {
        try
        {
            if (spr.Succeed())
                del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static DSPR<T> See<T>(this DSPR<T> spr, Action<DSPR<T>> del)
    {
        try
        {
            if (spr.Succeed())
                del(spr);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }
}


public static class DSPR_To_Async_To
{
    public static async ValueTask<DSPR<R>> To<T, R>(this DSPR<T> spr, Func<T, ValueTask<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this DSPR<T> spr, Func<T, Task<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<DSPR<R>> To<T, R>(this DSPR<T> spr, Func<T, ValueTask<SPR<R>>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this DSPR<T> spr, Func<T, Task<SPR<R>>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<DSPR<R>> To<T, R>(this DSPR<T> spr, Func<DSPR<T>, ValueTask<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this DSPR<T> spr, Func<DSPR<T>, Task<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<DSPR<R>> To<T, R>(this DSPR<T> spr, Func<DSPR<T>, ValueTask<SPR<R>>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this DSPR<T> spr, Func<DSPR<T>, Task<SPR<R>>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }
}
public static class DSPR_To_Async_See
{
    public static async ValueTask<DSPR<T>> See<T>(this DSPR<T> spr, Func<T, ValueTask> del)
    {
        try
        {
            if (spr.Succeed())
                await del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<T>> See<T>(this DSPR<T> spr, Func<T, Task> del)
    {
        try
        {
            if (spr.Succeed())
                await del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<DSPR<T>> See<T>(this DSPR<T> spr, Func<DSPR<T>, ValueTask> del)
    {
        try
        {
            if (spr.Succeed())
                await del(spr);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<T>> See<T>(this DSPR<T> spr, Func<DSPR<T>, Task> del)
    {
        try
        {
            if (spr.Succeed())
                await del(spr);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }
}


public static class DSPR_From_ValueTask_To
{
    public static async ValueTask<DSPR<R>> To<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<T, R> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<T, SPR<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<DSPR<T>, SPR<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<DSPR<R>> To<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<T, Task<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<T, Task<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<DSPR<T>, Task<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<DSPR<R>> To<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<T, ValueTask<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<T, ValueTask<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<DSPR<T>, ValueTask<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }
}
public static class DSPR_From_ValueTask_See
{
    public static async ValueTask<DSPR<T>> See<T>(this ValueTask<DSPR<T>> taskSpr, Action<T> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<T>> See<T>(this ValueTask<DSPR<T>> taskSpr, Action<DSPR<T>> del)
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
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<DSPR<T>> See<T>(this ValueTask<DSPR<T>> taskSpr, Func<T, Task> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<T>> See<T>(this ValueTask<DSPR<T>> taskSpr, Func<DSPR<T>, Task> del)
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
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<DSPR<T>> See<T>(this ValueTask<DSPR<T>> taskSpr, Func<T, ValueTask> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<T>> See<T>(this ValueTask<DSPR<T>> taskSpr, Func<DSPR<T>, ValueTask> del)
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
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }
}


public static class DSPR_From_Task_To
{
    public static async ValueTask<DSPR<R>> To<T, R>(this Task<DSPR<T>> taskSpr, Func<T, R> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this Task<DSPR<T>> taskSpr, Func<T, SPR<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this Task<DSPR<T>> taskSpr, Func<DSPR<T>, SPR<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<DSPR<R>> To<T, R>(this Task<DSPR<T>> taskSpr, Func<T, Task<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this Task<DSPR<T>> taskSpr, Func<T, Task<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this Task<DSPR<T>> taskSpr, Func<DSPR<T>, Task<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<DSPR<R>> To<T, R>(this Task<DSPR<T>> taskSpr, Func<T, ValueTask<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this Task<DSPR<T>> taskSpr, Func<T, ValueTask<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this Task<DSPR<T>> taskSpr, Func<DSPR<T>, ValueTask<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }
}
public static class DSPR_From_Task_See
{
    public static async ValueTask<DSPR<T>> See<T>(this Task<DSPR<T>> taskSpr, Action<T> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<T>> See<T>(this Task<DSPR<T>> taskSpr, Action<DSPR<T>> del)
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
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<DSPR<T>> See<T>(this Task<DSPR<T>> taskSpr, Func<T, Task> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<T>> See<T>(this Task<DSPR<T>> taskSpr, Func<DSPR<T>, Task> del)
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
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<DSPR<T>> See<T>(this Task<DSPR<T>> taskSpr, Func<T, ValueTask> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<T>> See<T>(this Task<DSPR<T>> taskSpr, Func<DSPR<T>, ValueTask> del)
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
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }
}

///////////////////////////////////////////////

public static class TSPR_Sync_To
{
    public static TSPR<R> To<T, R>(this TSPR<T> spr, Func<T, R> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static TSPR<R> To<T, R>(this TSPR<T> spr, Func<T, SPR<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static TSPR<R> To<T, R>(this TSPR<T> spr, Func<TSPR<T>, SPR<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }
}
public static class TSPR_Sync_See
{
    public static TSPR<T> See<T>(this TSPR<T> spr, Action<T> del)
    {
        try
        {
            if (spr.Succeed())
                del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static TSPR<T> See<T>(this TSPR<T> spr, Action<TSPR<T>> del)
    {
        try
        {
            if (spr.Succeed())
                del(spr);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }
}


public static class TSPR_To_Async_To
{
    public static async ValueTask<TSPR<R>> To<T, R>(this TSPR<T> spr, Func<T, ValueTask<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<R>> To<T, R>(this TSPR<T> spr, Func<T, Task<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<TSPR<R>> To<T, R>(this TSPR<T> spr, Func<T, ValueTask<SPR<R>>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<R>> To<T, R>(this TSPR<T> spr, Func<T, Task<SPR<R>>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<TSPR<R>> To<T, R>(this TSPR<T> spr, Func<TSPR<T>, ValueTask<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<R>> To<T, R>(this TSPR<T> spr, Func<TSPR<T>, Task<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<TSPR<R>> To<T, R>(this TSPR<T> spr, Func<TSPR<T>, ValueTask<SPR<R>>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<R>> To<T, R>(this TSPR<T> spr, Func<TSPR<T>, Task<SPR<R>>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }
}
public static class TSPR_To_Async_See
{
    public static async ValueTask<TSPR<T>> See<T>(this TSPR<T> spr, Func<T, ValueTask> del)
    {
        try
        {
            if (spr.Succeed())
                await del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<T>> See<T>(this TSPR<T> spr, Func<T, Task> del)
    {
        try
        {
            if (spr.Succeed())
                await del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<TSPR<T>> See<T>(this TSPR<T> spr, Func<TSPR<T>, ValueTask> del)
    {
        try
        {
            if (spr.Succeed())
                await del(spr);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<T>> See<T>(this TSPR<T> spr, Func<TSPR<T>, Task> del)
    {
        try
        {
            if (spr.Succeed())
                await del(spr);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }
}


public static class TSPR_From_ValueTask_To
{
    public static async ValueTask<TSPR<R>> To<T, R>(this ValueTask<TSPR<T>> taskSpr, Func<T, R> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<R>> To<T, R>(this ValueTask<TSPR<T>> taskSpr, Func<T, SPR<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<R>> To<T, R>(this ValueTask<TSPR<T>> taskSpr, Func<TSPR<T>, SPR<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<TSPR<R>> To<T, R>(this ValueTask<TSPR<T>> taskSpr, Func<T, Task<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<R>> To<T, R>(this ValueTask<TSPR<T>> taskSpr, Func<T, Task<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<R>> To<T, R>(this ValueTask<TSPR<T>> taskSpr, Func<TSPR<T>, Task<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<TSPR<R>> To<T, R>(this ValueTask<TSPR<T>> taskSpr, Func<T, ValueTask<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<R>> To<T, R>(this ValueTask<TSPR<T>> taskSpr, Func<T, ValueTask<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<R>> To<T, R>(this ValueTask<TSPR<T>> taskSpr, Func<TSPR<T>, ValueTask<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }
}
public static class TSPR_From_ValueTask_See
{
    public static async ValueTask<TSPR<T>> See<T>(this ValueTask<TSPR<T>> taskSpr, Action<T> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<T>> See<T>(this ValueTask<TSPR<T>> taskSpr, Action<TSPR<T>> del)
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
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<TSPR<T>> See<T>(this ValueTask<TSPR<T>> taskSpr, Func<T, Task> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<T>> See<T>(this ValueTask<TSPR<T>> taskSpr, Func<TSPR<T>, Task> del)
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
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<TSPR<T>> See<T>(this ValueTask<TSPR<T>> taskSpr, Func<T, ValueTask> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<T>> See<T>(this ValueTask<TSPR<T>> taskSpr, Func<TSPR<T>, ValueTask> del)
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
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }
}


public static class TSPR_From_Task_To
{
    public static async ValueTask<TSPR<R>> To<T, R>(this Task<TSPR<T>> taskSpr, Func<T, R> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<R>> To<T, R>(this Task<TSPR<T>> taskSpr, Func<T, SPR<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<R>> To<T, R>(this Task<TSPR<T>> taskSpr, Func<TSPR<T>, SPR<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<TSPR<R>> To<T, R>(this Task<TSPR<T>> taskSpr, Func<T, Task<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<R>> To<T, R>(this Task<TSPR<T>> taskSpr, Func<T, Task<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<R>> To<T, R>(this Task<TSPR<T>> taskSpr, Func<TSPR<T>, Task<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<TSPR<R>> To<T, R>(this Task<TSPR<T>> taskSpr, Func<T, ValueTask<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<R>> To<T, R>(this Task<TSPR<T>> taskSpr, Func<T, ValueTask<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<R>> To<T, R>(this Task<TSPR<T>> taskSpr, Func<TSPR<T>, ValueTask<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }
}
public static class TSPR_From_Task_See
{
    public static async ValueTask<TSPR<T>> See<T>(this Task<TSPR<T>> taskSpr, Action<T> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<T>> See<T>(this Task<TSPR<T>> taskSpr, Action<TSPR<T>> del)
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
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<TSPR<T>> See<T>(this Task<TSPR<T>> taskSpr, Func<T, Task> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<T>> See<T>(this Task<TSPR<T>> taskSpr, Func<TSPR<T>, Task> del)
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
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<TSPR<T>> See<T>(this Task<TSPR<T>> taskSpr, Func<T, ValueTask> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<T>> See<T>(this Task<TSPR<T>> taskSpr, Func<TSPR<T>, ValueTask> del)
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
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }
}

///////////////////////////////////////////////

public static class TDSPR_Sync_To
{
    public static TDSPR<R> To<T, R>(this TDSPR<T> spr, Func<T, R> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static TDSPR<R> To<T, R>(this TDSPR<T> spr, Func<T, SPR<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static TDSPR<R> To<T, R>(this TDSPR<T> spr, Func<TDSPR<T>, SPR<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }
}
public static class TDSPR_Sync_See
{
    public static TDSPR<T> See<T>(this TDSPR<T> spr, Action<T> del)
    {
        try
        {
            if (spr.Succeed())
                del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static TDSPR<T> See<T>(this TDSPR<T> spr, Action<TDSPR<T>> del)
    {
        try
        {
            if (spr.Succeed())
                del(spr);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }
}


public static class TDSPR_To_Async_To
{
    public static async ValueTask<TDSPR<R>> To<T, R>(this TDSPR<T> spr, Func<T, ValueTask<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TDSPR<R>> To<T, R>(this TDSPR<T> spr, Func<T, Task<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<TDSPR<R>> To<T, R>(this TDSPR<T> spr, Func<T, ValueTask<SPR<R>>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TDSPR<R>> To<T, R>(this TDSPR<T> spr, Func<T, Task<SPR<R>>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<TDSPR<R>> To<T, R>(this TDSPR<T> spr, Func<TDSPR<T>, ValueTask<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TDSPR<R>> To<T, R>(this TDSPR<T> spr, Func<TDSPR<T>, Task<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<TDSPR<R>> To<T, R>(this TDSPR<T> spr, Func<TDSPR<T>, ValueTask<SPR<R>>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TDSPR<R>> To<T, R>(this TDSPR<T> spr, Func<TDSPR<T>, Task<SPR<R>>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }
}
public static class TDSPR_To_Async_See
{
    public static async ValueTask<TDSPR<T>> See<T>(this TDSPR<T> spr, Func<T, ValueTask> del)
    {
        try
        {
            if (spr.Succeed())
                await del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TDSPR<T>> See<T>(this TDSPR<T> spr, Func<T, Task> del)
    {
        try
        {
            if (spr.Succeed())
                await del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<TDSPR<T>> See<T>(this TDSPR<T> spr, Func<TDSPR<T>, ValueTask> del)
    {
        try
        {
            if (spr.Succeed())
                await del(spr);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TDSPR<T>> See<T>(this TDSPR<T> spr, Func<TDSPR<T>, Task> del)
    {
        try
        {
            if (spr.Succeed())
                await del(spr);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }
}


public static class TDSPR_From_ValueTask_To
{
    public static async ValueTask<TDSPR<R>> To<T, R>(this ValueTask<TDSPR<T>> taskSpr, Func<T, R> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TDSPR<R>> To<T, R>(this ValueTask<TDSPR<T>> taskSpr, Func<T, SPR<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TDSPR<R>> To<T, R>(this ValueTask<TDSPR<T>> taskSpr, Func<TDSPR<T>, SPR<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<TDSPR<R>> To<T, R>(this ValueTask<TDSPR<T>> taskSpr, Func<T, Task<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TDSPR<R>> To<T, R>(this ValueTask<TDSPR<T>> taskSpr, Func<T, Task<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TDSPR<R>> To<T, R>(this ValueTask<TDSPR<T>> taskSpr, Func<TDSPR<T>, Task<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<TDSPR<R>> To<T, R>(this ValueTask<TDSPR<T>> taskSpr, Func<T, ValueTask<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TDSPR<R>> To<T, R>(this ValueTask<TDSPR<T>> taskSpr, Func<T, ValueTask<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TDSPR<R>> To<T, R>(this ValueTask<TDSPR<T>> taskSpr, Func<TDSPR<T>, ValueTask<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }
}
public static class TDSPR_From_ValueTask_See
{
    public static async ValueTask<TDSPR<T>> See<T>(this ValueTask<TDSPR<T>> taskSpr, Action<T> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TDSPR<T>> See<T>(this ValueTask<TDSPR<T>> taskSpr, Action<TDSPR<T>> del)
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
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<TDSPR<T>> See<T>(this ValueTask<TDSPR<T>> taskSpr, Func<T, Task> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TDSPR<T>> See<T>(this ValueTask<TDSPR<T>> taskSpr, Func<TDSPR<T>, Task> del)
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
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<TDSPR<T>> See<T>(this ValueTask<TDSPR<T>> taskSpr, Func<T, ValueTask> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TDSPR<T>> See<T>(this ValueTask<TDSPR<T>> taskSpr, Func<TDSPR<T>, ValueTask> del)
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
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }
}


public static class TDSPR_From_Task_To
{
    public static async ValueTask<TDSPR<R>> To<T, R>(this Task<TDSPR<T>> taskSpr, Func<T, R> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TDSPR<R>> To<T, R>(this Task<TDSPR<T>> taskSpr, Func<T, SPR<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TDSPR<R>> To<T, R>(this Task<TDSPR<T>> taskSpr, Func<TDSPR<T>, SPR<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<TDSPR<R>> To<T, R>(this Task<TDSPR<T>> taskSpr, Func<T, Task<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TDSPR<R>> To<T, R>(this Task<TDSPR<T>> taskSpr, Func<T, Task<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TDSPR<R>> To<T, R>(this Task<TDSPR<T>> taskSpr, Func<TDSPR<T>, Task<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<TDSPR<R>> To<T, R>(this Task<TDSPR<T>> taskSpr, Func<T, ValueTask<R>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TDSPR<R>> To<T, R>(this Task<TDSPR<T>> taskSpr, Func<T, ValueTask<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(((ISP<T>)spr).Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TDSPR<R>> To<T, R>(this Task<TDSPR<T>> taskSpr, Func<TDSPR<T>, ValueTask<SPR<R>>> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }
}
public static class TDSPR_From_Task_See
{
    public static async ValueTask<TDSPR<T>> See<T>(this Task<TDSPR<T>> taskSpr, Action<T> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TDSPR<T>> See<T>(this Task<TDSPR<T>> taskSpr, Action<TDSPR<T>> del)
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
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<TDSPR<T>> See<T>(this Task<TDSPR<T>> taskSpr, Func<T, Task> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TDSPR<T>> See<T>(this Task<TDSPR<T>> taskSpr, Func<TDSPR<T>, Task> del)
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
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }


    public static async ValueTask<TDSPR<T>> See<T>(this Task<TDSPR<T>> taskSpr, Func<T, ValueTask> del)
    {
        var spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(((ISP<T>)spr).Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }

    public static async ValueTask<TDSPR<T>> See<T>(this Task<TDSPR<T>> taskSpr, Func<TDSPR<T>, ValueTask> del)
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
            return spr.Pass<T>(SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e));
        }
    }
}

public static class ALL_To_VSP
{
    public static VSP Void<T>(this SPR<T> spr)
    {
        return new VSP(spr.Succeed(), spr.Fault);
    }

    public static DVSP Void<T>(this DSPR<T> spr)
    {
        return new DVSP(spr.Succeed(), spr.Fault);
    }

    public static TVSP Void<T>(this TSPR<T> spr)
    {
        return new TVSP(spr.Succeed(), spr.Fault);
    }

    public static TDVSP Void<T>(this TDSPR<T> spr)
    {
        return new TDVSP(spr.Succeed(), spr.Fault);
    }


    public static VSP Void<T>(this SPR<T> spr)
    {
        return new VSP(spr.Succeed(), spr.Fault);
    }
}