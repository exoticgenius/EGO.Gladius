using EGO.Gladius.DataTypes;

using System.Runtime.CompilerServices;

namespace EGO.Gladius.Extensions;
#nullable disable
public static class DSPR_Sync_To
{
    public static DSPR<R> To<T, R>(this DSPR<T> spr, Func<T, R> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(spr.Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    public static DSPR<R> To<T, R>(this DSPR<T> spr, Func<T, SPR<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(spr.Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
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
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
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
                del(spr.Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
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
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }
}


public static class DSPR_To_Async_To
{
    [OverloadResolutionPriority(1)]
    public static async ValueTask<DSPR<R>> To<T, R>(this DSPR<T> spr, Func<T, ValueTask<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr.Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this DSPR<T> spr, Func<T, Task<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr.Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }


    [OverloadResolutionPriority(1)]
    public static async ValueTask<DSPR<R>> To<T, R>(this DSPR<T> spr, Func<T, ValueTask<SPR<R>>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr.Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this DSPR<T> spr, Func<T, Task<SPR<R>>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr.Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }


    [OverloadResolutionPriority(1)]
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
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
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
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }


    [OverloadResolutionPriority(1)]
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
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
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
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }
}
public static class DSPR_To_Async_See
{
    [OverloadResolutionPriority(1)]
    public static async ValueTask<DSPR<T>> See<T>(this DSPR<T> spr, Func<T, ValueTask> del)
    {
        try
        {
            if (spr.Succeed())
                await del(spr.Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<T>> See<T>(this DSPR<T> spr, Func<T, Task> del)
    {
        try
        {
            if (spr.Succeed())
                await del(spr.Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }


    [OverloadResolutionPriority(1)]
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
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
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
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }
}


public static class DSPR_From_ValueTask_To
{
    public static async ValueTask<DSPR<R>> To<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<T, R> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(spr.Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<T, SPR<R>> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(spr.Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<DSPR<T>, SPR<R>> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }


    public static async ValueTask<DSPR<R>> To<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<T, Task<R>> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr.Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<T, Task<SPR<R>>> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr.Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<DSPR<T>, Task<SPR<R>>> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }


    [OverloadResolutionPriority(1)]
    public static async ValueTask<DSPR<R>> To<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<T, ValueTask<R>> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr.Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    [OverloadResolutionPriority(1)]
    public static async ValueTask<DSPR<R>> To<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<T, ValueTask<SPR<R>>> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr.Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    [OverloadResolutionPriority(1)]
    public static async ValueTask<DSPR<R>> To<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<DSPR<T>, ValueTask<SPR<R>>> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }
}
public static class DSPR_From_ValueTask_See
{
    public static async ValueTask<DSPR<T>> See<T>(this ValueTask<DSPR<T>> taskSpr, Action<T> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                del(spr.Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<T>> See<T>(this ValueTask<DSPR<T>> taskSpr, Action<DSPR<T>> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                del(spr);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }


    public static async ValueTask<DSPR<T>> See<T>(this ValueTask<DSPR<T>> taskSpr, Func<T, Task> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(spr.Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<T>> See<T>(this ValueTask<DSPR<T>> taskSpr, Func<DSPR<T>, Task> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(spr);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }


    [OverloadResolutionPriority(1)]
    public static async ValueTask<DSPR<T>> See<T>(this ValueTask<DSPR<T>> taskSpr, Func<T, ValueTask> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(spr.Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    [OverloadResolutionPriority(1)]
    public static async ValueTask<DSPR<T>> See<T>(this ValueTask<DSPR<T>> taskSpr, Func<DSPR<T>, ValueTask> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(spr);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }
}


public static class DSPR_From_Task_To
{
    public static async ValueTask<DSPR<R>> To<T, R>(this Task<DSPR<T>> taskSpr, Func<T, R> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(spr.Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this Task<DSPR<T>> taskSpr, Func<T, SPR<R>> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(spr.Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this Task<DSPR<T>> taskSpr, Func<DSPR<T>, SPR<R>> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }


    public static async ValueTask<DSPR<R>> To<T, R>(this Task<DSPR<T>> taskSpr, Func<T, Task<R>> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr.Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this Task<DSPR<T>> taskSpr, Func<T, Task<SPR<R>>> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr.Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<R>> To<T, R>(this Task<DSPR<T>> taskSpr, Func<DSPR<T>, Task<SPR<R>>> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }


    [OverloadResolutionPriority(1)]
    public static async ValueTask<DSPR<R>> To<T, R>(this Task<DSPR<T>> taskSpr, Func<T, ValueTask<R>> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr.Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    [OverloadResolutionPriority(1)]
    public static async ValueTask<DSPR<R>> To<T, R>(this Task<DSPR<T>> taskSpr, Func<T, ValueTask<SPR<R>>> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr.Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    [OverloadResolutionPriority(1)]
    public static async ValueTask<DSPR<R>> To<T, R>(this Task<DSPR<T>> taskSpr, Func<DSPR<T>, ValueTask<SPR<R>>> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                return spr.Pass(await del(spr));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }
}
public static class DSPR_From_Task_See
{
    public static async ValueTask<DSPR<T>> See<T>(this Task<DSPR<T>> taskSpr, Action<T> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                del(spr.Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<T>> See<T>(this Task<DSPR<T>> taskSpr, Action<DSPR<T>> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                del(spr);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }


    public static async ValueTask<DSPR<T>> See<T>(this Task<DSPR<T>> taskSpr, Func<T, Task> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(spr.Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<T>> See<T>(this Task<DSPR<T>> taskSpr, Func<DSPR<T>, Task> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(spr);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }


    [OverloadResolutionPriority(1)]
    public static async ValueTask<DSPR<T>> See<T>(this Task<DSPR<T>> taskSpr, Func<T, ValueTask> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(spr.Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    [OverloadResolutionPriority(1)]
    public static async ValueTask<DSPR<T>> See<T>(this Task<DSPR<T>> taskSpr, Func<DSPR<T>, ValueTask> del)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            if (spr.Succeed())
                await del(spr);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }
}

public static class DSPR_Fault_Handlers
{
    public static DSPR<T> HandleFault<T>(this DSPR<T> spr, Func<DSPR<T>, SPR<T>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr;

            return spr.Pass<T>(del(spr));
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<T>> HandleFault<T>(this DSPR<T> spr, Func<DSPR<T>, ValueTask<SPR<T>>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr;

            return spr.Pass<T>(await del(spr));
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }
}

public static class DSPR_Null_Handlers
{
    public static DSPR<T> HandleNull<T>(this DSPR<T> spr, Func<DSPR<T>, SPR<T>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr;

            if (spr.HasValue())
                return spr;

            return spr.Pass<T>(del(spr));
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    public static async ValueTask<DSPR<T>> HandleNull<T>(this DSPR<T> spr, Func<DSPR<T>, ValueTask<SPR<T>>> del)
    {
        try
        {
            if (spr.Succeed())
                return spr;

            if (spr.HasValue())
                return spr;

            return spr.Pass<T>(await del(spr));
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }
}

public static class DSPR_To_Void
{
    public static async ValueTask<DVSP> Void<T>(this ValueTask<DSPR<T>> taskSpr)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            return spr.Void();
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(e)).Void();
        }
    }

    public static async ValueTask<DVSP> Void<T>(this Task<DSPR<T>> taskSpr)
    {
        DSPR<T> spr = await taskSpr;
        try
        {
            return spr.Void();
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(e)).Void();
        }
    }
}