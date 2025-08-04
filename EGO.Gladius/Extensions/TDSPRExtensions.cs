using EGO.Gladius.Contracts;
using EGO.Gladius.DataTypes;

namespace EGO.Gladius.Extensions;

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