using EGO.Gladius.Contracts;
using EGO.Gladius.DataTypes;

namespace EGO.Gladius.Extensions;

public static class TSPR_Sync_To
{
    public static TSPR<R> To<T, R>(this TSPR<T> spr, Func<T, R> del)
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

    public static TSPR<R> To<T, R>(this TSPR<T> spr, Func<T, SPR<R>> del)
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
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
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
                del(spr.Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
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
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
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
                return spr.Pass(await del(spr.Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<R>> To<T, R>(this TSPR<T> spr, Func<T, Task<R>> del)
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


    public static async ValueTask<TSPR<R>> To<T, R>(this TSPR<T> spr, Func<T, ValueTask<SPR<R>>> del)
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

    public static async ValueTask<TSPR<R>> To<T, R>(this TSPR<T> spr, Func<T, Task<SPR<R>>> del)
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
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
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
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
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
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
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
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
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
                await del(spr.Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<T>> See<T>(this TSPR<T> spr, Func<T, Task> del)
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
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
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
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
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
                return spr.Pass(del(spr.Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<R>> To<T, R>(this ValueTask<TSPR<T>> taskSpr, Func<T, SPR<R>> del)
    {
        var spr = await taskSpr;
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
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }


    public static async ValueTask<TSPR<R>> To<T, R>(this ValueTask<TSPR<T>> taskSpr, Func<T, Task<R>> del)
    {
        var spr = await taskSpr;
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

    public static async ValueTask<TSPR<R>> To<T, R>(this ValueTask<TSPR<T>> taskSpr, Func<T, Task<SPR<R>>> del)
    {
        var spr = await taskSpr;
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
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }


    public static async ValueTask<TSPR<R>> To<T, R>(this ValueTask<TSPR<T>> taskSpr, Func<T, ValueTask<R>> del)
    {
        var spr = await taskSpr;
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

    public static async ValueTask<TSPR<R>> To<T, R>(this ValueTask<TSPR<T>> taskSpr, Func<T, ValueTask<SPR<R>>> del)
    {
        var spr = await taskSpr;
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
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
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
                del(spr.Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
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
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }


    public static async ValueTask<TSPR<T>> See<T>(this ValueTask<TSPR<T>> taskSpr, Func<T, Task> del)
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
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
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
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }


    public static async ValueTask<TSPR<T>> See<T>(this ValueTask<TSPR<T>> taskSpr, Func<T, ValueTask> del)
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
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
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
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
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
                return spr.Pass(del(spr.Value.Payload));

            return spr.Pass<R>(spr.Fault);
        }
        catch (Exception e)
        {
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }

    public static async ValueTask<TSPR<R>> To<T, R>(this Task<TSPR<T>> taskSpr, Func<T, SPR<R>> del)
    {
        var spr = await taskSpr;
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
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }


    public static async ValueTask<TSPR<R>> To<T, R>(this Task<TSPR<T>> taskSpr, Func<T, Task<R>> del)
    {
        var spr = await taskSpr;
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

    public static async ValueTask<TSPR<R>> To<T, R>(this Task<TSPR<T>> taskSpr, Func<T, Task<SPR<R>>> del)
    {
        var spr = await taskSpr;
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
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }


    public static async ValueTask<TSPR<R>> To<T, R>(this Task<TSPR<T>> taskSpr, Func<T, ValueTask<R>> del)
    {
        var spr = await taskSpr;
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

    public static async ValueTask<TSPR<R>> To<T, R>(this Task<TSPR<T>> taskSpr, Func<T, ValueTask<SPR<R>>> del)
    {
        var spr = await taskSpr;
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
            return spr.Pass<R>(SPF.Gen(del.Method, [spr.Value.Payload], e));
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
                del(spr.Value.Payload);

            return spr;
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
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
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }


    public static async ValueTask<TSPR<T>> See<T>(this Task<TSPR<T>> taskSpr, Func<T, Task> del)
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
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
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
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }


    public static async ValueTask<TSPR<T>> See<T>(this Task<TSPR<T>> taskSpr, Func<T, ValueTask> del)
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
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
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
            return spr.Pass<T>(SPF.Gen(del.Method, [spr.Value.Payload], e));
        }
    }
}

public static class TSPR_Fault_Handlers
{
    public static TSPR<T> HandleFault<T>(this TSPR<T> spr, Func<TSPR<T>, SPR<T>> del)
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

    public static async ValueTask<TSPR<T>> HandleFault<T>(this TSPR<T> spr, Func<TSPR<T>, ValueTask<SPR<T>>> del)
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

public static class TSPR_Null_Handlers
{
    public static TSPR<T> HandleNull<T>(this TSPR<T> spr, Func<TSPR<T>, SPR<T>> del)
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

    public static async ValueTask<TSPR<T>> HandleNull<T>(this TSPR<T> spr, Func<TSPR<T>, ValueTask<SPR<T>>> del)
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

public static class TSPR_To_Void
{
    public static async ValueTask<TVSP> Void<T>(this ValueTask<TSPR<T>> taskSpr)
    {
        var spr = await taskSpr;
        try
        {
            return spr.Void();
        }
        catch (Exception e)
        {
            return spr.Pass<T>(SPF.Gen(e)).Void();
        }
    }

    public static async ValueTask<TVSP> Void<T>(this Task<TSPR<T>> taskSpr)
    {
        var spr = await taskSpr;
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