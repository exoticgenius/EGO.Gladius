using EGO.Gladius.Core;
using EGO.Gladius.DataTypes;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Transactions;

namespace EGO.Gladius.Contracts;

public static class SPR_Sync_Transformer
{
    public static SPR<R> Transform<T, R>(this SPR<T> spr, Func<T, R> del)
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

    public static SPR<R> Transform<T, R>(this SPR<T> spr, Func<T, SPR<R>> del)
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

    public static SPR<R> Transform<T, R>(this SPR<T> spr, Func<SPR<T>, SPR<R>> del)
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

public static class SPR_To_Async_Transformer
{
    public static async ValueTask<SPR<R>> Transform<T, R>(this SPR<T> spr, Func<T, ValueTask<R>> del)
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

    public static async ValueTask<SPR<R>> Transform<T, R>(this SPR<T> spr, Func<T, Task<R>> del)
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


    public static async ValueTask<SPR<R>> Transform<T, R>(this SPR<T> spr, Func<T, ValueTask<SPR<R>>> del)
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

    public static async ValueTask<SPR<R>> Transform<T, R>(this SPR<T> spr, Func<T, Task<SPR<R>>> del)
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


    public static async ValueTask<SPR<R>> Transform<T, R>(this SPR<T> spr, Func<SPR<T>, ValueTask<R>> del)
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

    public static async ValueTask<SPR<R>> Transform<T, R>(this SPR<T> spr, Func<SPR<T>, Task<R>> del)
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


    public static async ValueTask<SPR<R>> Transform<T, R>(this SPR<T> spr, Func<SPR<T>, ValueTask<SPR<R>>> del)
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

    public static async ValueTask<SPR<R>> Transform<T, R>(this SPR<T> spr, Func<SPR<T>, Task<SPR<R>>> del)
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

public static class SPR_From_ValueTask_Transformer
{
    public static async ValueTask<SPR<R>> Transform<T, R>(this ValueTask<SPR<T>> taskSpr, Func<T, R> del)
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

    public static async ValueTask<SPR<R>> Transform<T, R>(this ValueTask<SPR<T>> taskSpr, Func<T, SPR<R>> del)
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

    public static async ValueTask<SPR<R>> Transform<T, R>(this ValueTask<SPR<T>> taskSpr, Func<SPR<T>, SPR<R>> del)
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


    public static async ValueTask<SPR<R>> Transform<T, R>(this ValueTask<SPR<T>> taskSpr, Func<T, Task<R>> del)
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

    public static async ValueTask<SPR<R>> Transform<T, R>(this ValueTask<SPR<T>> taskSpr, Func<T, Task<SPR<R>>> del)
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

    public static async ValueTask<SPR<R>> Transform<T, R>(this ValueTask<SPR<T>> taskSpr, Func<SPR<T>, Task<SPR<R>>> del)
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


    public static async ValueTask<SPR<R>> Transform<T, R>(this ValueTask<SPR<T>> taskSpr, Func<T, ValueTask<R>> del)
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

    public static async ValueTask<SPR<R>> Transform<T, R>(this ValueTask<SPR<T>> taskSpr, Func<T, ValueTask<SPR<R>>> del)
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

    public static async ValueTask<SPR<R>> Transform<T, R>(this ValueTask<SPR<T>> taskSpr, Func<SPR<T>, ValueTask<SPR<R>>> del)
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

public static class SPR_From_Task_Transformer
{
    public static async ValueTask<SPR<R>> Transform<T, R>(this Task<SPR<T>> taskSpr, Func<T, R> del)
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

    public static async ValueTask<SPR<R>> Transform<T, R>(this Task<SPR<T>> taskSpr, Func<T, SPR<R>> del)
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

    public static async ValueTask<SPR<R>> Transform<T, R>(this Task<SPR<T>> taskSpr, Func<SPR<T>, SPR<R>> del)
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


    public static async ValueTask<SPR<R>> Transform<T, R>(this Task<SPR<T>> taskSpr, Func<T, Task<R>> del)
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

    public static async ValueTask<SPR<R>> Transform<T, R>(this Task<SPR<T>> taskSpr, Func<T, Task<SPR<R>>> del)
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

    public static async ValueTask<SPR<R>> Transform<T, R>(this Task<SPR<T>> taskSpr, Func<SPR<T>, Task<SPR<R>>> del)
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


    public static async ValueTask<SPR<R>> Transform<T, R>(this Task<SPR<T>> taskSpr, Func<T, ValueTask<R>> del)
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

    public static async ValueTask<SPR<R>> Transform<T, R>(this Task<SPR<T>> taskSpr, Func<T, ValueTask<SPR<R>>> del)
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

    public static async ValueTask<SPR<R>> Transform<T, R>(this Task<SPR<T>> taskSpr, Func<SPR<T>, ValueTask<SPR<R>>> del)
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

///////////////////////////////////////////////

public static class DSPR_Sync_Transformer
{
    public static DSPR<R> Transform<T, R>(this DSPR<T> spr, Func<T, R> del)
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

    public static DSPR<R> Transform<T, R>(this DSPR<T> spr, Func<T, SPR<R>> del)
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

    public static DSPR<R> Transform<T, R>(this DSPR<T> spr, Func<DSPR<T>, SPR<R>> del)
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

public static class DSPR_To_Async_Transformer
{
    public static async ValueTask<DSPR<R>> Transform<T, R>(this DSPR<T> spr, Func<T, ValueTask<R>> del)
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

    public static async ValueTask<DSPR<R>> Transform<T, R>(this DSPR<T> spr, Func<T, Task<R>> del)
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


    public static async ValueTask<DSPR<R>> Transform<T, R>(this DSPR<T> spr, Func<T, ValueTask<SPR<R>>> del)
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

    public static async ValueTask<DSPR<R>> Transform<T, R>(this DSPR<T> spr, Func<T, Task<SPR<R>>> del)
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


    public static async ValueTask<DSPR<R>> Transform<T, R>(this DSPR<T> spr, Func<DSPR<T>, ValueTask<R>> del)
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

    public static async ValueTask<DSPR<R>> Transform<T, R>(this DSPR<T> spr, Func<DSPR<T>, Task<R>> del)
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


    public static async ValueTask<DSPR<R>> Transform<T, R>(this DSPR<T> spr, Func<DSPR<T>, ValueTask<SPR<R>>> del)
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

    public static async ValueTask<DSPR<R>> Transform<T, R>(this DSPR<T> spr, Func<DSPR<T>, Task<SPR<R>>> del)
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

public static class DSPR_From_ValueTask_Transformer
{
    public static async ValueTask<DSPR<R>> Transform<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<T, R> del)
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

    public static async ValueTask<DSPR<R>> Transform<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<T, SPR<R>> del)
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

    public static async ValueTask<DSPR<R>> Transform<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<DSPR<T>, SPR<R>> del)
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


    public static async ValueTask<DSPR<R>> Transform<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<T, Task<R>> del)
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

    public static async ValueTask<DSPR<R>> Transform<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<T, Task<SPR<R>>> del)
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

    public static async ValueTask<DSPR<R>> Transform<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<DSPR<T>, Task<SPR<R>>> del)
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


    public static async ValueTask<DSPR<R>> Transform<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<T, ValueTask<R>> del)
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

    public static async ValueTask<DSPR<R>> Transform<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<T, ValueTask<SPR<R>>> del)
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

    public static async ValueTask<DSPR<R>> Transform<T, R>(this ValueTask<DSPR<T>> taskSpr, Func<DSPR<T>, ValueTask<SPR<R>>> del)
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

public static class DSPR_From_Task_Transformer
{
    public static async ValueTask<DSPR<R>> Transform<T, R>(this Task<DSPR<T>> taskSpr, Func<T, R> del)
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

    public static async ValueTask<DSPR<R>> Transform<T, R>(this Task<DSPR<T>> taskSpr, Func<T, SPR<R>> del)
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

    public static async ValueTask<DSPR<R>> Transform<T, R>(this Task<DSPR<T>> taskSpr, Func<DSPR<T>, SPR<R>> del)
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


    public static async ValueTask<DSPR<R>> Transform<T, R>(this Task<DSPR<T>> taskSpr, Func<T, Task<R>> del)
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

    public static async ValueTask<DSPR<R>> Transform<T, R>(this Task<DSPR<T>> taskSpr, Func<T, Task<SPR<R>>> del)
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

    public static async ValueTask<DSPR<R>> Transform<T, R>(this Task<DSPR<T>> taskSpr, Func<DSPR<T>, Task<SPR<R>>> del)
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


    public static async ValueTask<DSPR<R>> Transform<T, R>(this Task<DSPR<T>> taskSpr, Func<T, ValueTask<R>> del)
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

    public static async ValueTask<DSPR<R>> Transform<T, R>(this Task<DSPR<T>> taskSpr, Func<T, ValueTask<SPR<R>>> del)
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

    public static async ValueTask<DSPR<R>> Transform<T, R>(this Task<DSPR<T>> taskSpr, Func<DSPR<T>, ValueTask<SPR<R>>> del)
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

///////////////////////////////////////////////

public static class TSPR_Sync_Transformer
{
    public static TSPR<R> Transform<T, R>(this TSPR<T> spr, Func<T, R> del)
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

    public static TSPR<R> Transform<T, R>(this TSPR<T> spr, Func<T, SPR<R>> del)
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

    public static TSPR<R> Transform<T, R>(this TSPR<T> spr, Func<TSPR<T>, SPR<R>> del)
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

public static class TSPR_To_Async_Transformer
{
    public static async ValueTask<TSPR<R>> Transform<T, R>(this TSPR<T> spr, Func<T, ValueTask<R>> del)
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
    
    public static async ValueTask<TSPR<R>> Transform<T, R>(this TSPR<T> spr, Func<T, Task<R>> del)
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


    public static async ValueTask<TSPR<R>> Transform<T, R>(this TSPR<T> spr, Func<T, ValueTask<SPR<R>>> del)
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

    public static async ValueTask<TSPR<R>> Transform<T, R>(this TSPR<T> spr, Func<T, Task<SPR<R>>> del)
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


    public static async ValueTask<TSPR<R>> Transform<T, R>(this TSPR<T> spr, Func<TSPR<T>, ValueTask<R>> del)
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

    public static async ValueTask<TSPR<R>> Transform<T, R>(this TSPR<T> spr, Func<TSPR<T>, Task<R>> del)
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


    public static async ValueTask<TSPR<R>> Transform<T, R>(this TSPR<T> spr, Func<TSPR<T>, ValueTask<SPR<R>>> del)
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

    public static async ValueTask<TSPR<R>> Transform<T, R>(this TSPR<T> spr, Func<TSPR<T>, Task<SPR<R>>> del)
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

public static class TSPR_From_ValueTask_Transformer
{
    public static async ValueTask<TSPR<R>> Transform<T, R>(this ValueTask<TSPR<T>> taskSpr, Func<T, R> del)
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

    public static async ValueTask<TSPR<R>> Transform<T, R>(this ValueTask<TSPR<T>> taskSpr, Func<T, SPR<R>> del)
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

    public static async ValueTask<TSPR<R>> Transform<T, R>(this ValueTask<TSPR<T>> taskSpr, Func<TSPR<T>, SPR<R>> del)
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


    public static async ValueTask<TSPR<R>> Transform<T, R>(this ValueTask<TSPR<T>> taskSpr, Func<T, Task<R>> del)
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

    public static async ValueTask<TSPR<R>> Transform<T, R>(this ValueTask<TSPR<T>> taskSpr, Func<T, Task<SPR<R>>> del)
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

    public static async ValueTask<TSPR<R>> Transform<T, R>(this ValueTask<TSPR<T>> taskSpr, Func<TSPR<T>, Task<SPR<R>>> del)
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


    public static async ValueTask<TSPR<R>> Transform<T, R>(this ValueTask<TSPR<T>> taskSpr, Func<T, ValueTask<R>> del)
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

    public static async ValueTask<TSPR<R>> Transform<T, R>(this ValueTask<TSPR<T>> taskSpr, Func<T, ValueTask<SPR<R>>> del)
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

    public static async ValueTask<TSPR<R>> Transform<T, R>(this ValueTask<TSPR<T>> taskSpr, Func<TSPR<T>, ValueTask<SPR<R>>> del)
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

public static class TSPR_From_Task_Transformer
{
    public static async ValueTask<TSPR<R>> Transform<T, R>(this Task<TSPR<T>> taskSpr, Func<T, R> del)
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

    public static async ValueTask<TSPR<R>> Transform<T, R>(this Task<TSPR<T>> taskSpr, Func<T, SPR<R>> del)
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

    public static async ValueTask<TSPR<R>> Transform<T, R>(this Task<TSPR<T>> taskSpr, Func<TSPR<T>, SPR<R>> del)
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


    public static async ValueTask<TSPR<R>> Transform<T, R>(this Task<TSPR<T>> taskSpr, Func<T, Task<R>> del)
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

    public static async ValueTask<TSPR<R>> Transform<T, R>(this Task<TSPR<T>> taskSpr, Func<T, Task<SPR<R>>> del)
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

    public static async ValueTask<TSPR<R>> Transform<T, R>(this Task<TSPR<T>> taskSpr, Func<TSPR<T>, Task<SPR<R>>> del)
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


    public static async ValueTask<TSPR<R>> Transform<T, R>(this Task<TSPR<T>> taskSpr, Func<T, ValueTask<R>> del)
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

    public static async ValueTask<TSPR<R>> Transform<T, R>(this Task<TSPR<T>> taskSpr, Func<T, ValueTask<SPR<R>>> del)
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

    public static async ValueTask<TSPR<R>> Transform<T, R>(this Task<TSPR<T>> taskSpr, Func<TSPR<T>, ValueTask<SPR<R>>> del)
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

///////////////////////////////////////////////

public static class TDSPR_Sync_Transformer
{
    public static TDSPR<R> Transform<T, R>(this TDSPR<T> spr, Func<T, R> del)
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

    public static TDSPR<R> Transform<T, R>(this TDSPR<T> spr, Func<T, SPR<R>> del)
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

    public static TDSPR<R> Transform<T, R>(this TDSPR<T> spr, Func<TDSPR<T>, SPR<R>> del)
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

public static class TDSPR_To_Async_Transformer
{
    public static async ValueTask<TDSPR<R>> Transform<T, R>(this TDSPR<T> spr, Func<T, ValueTask<R>> del)
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

    public static async ValueTask<TDSPR<R>> Transform<T, R>(this TDSPR<T> spr, Func<T, Task<R>> del)
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


    public static async ValueTask<TDSPR<R>> Transform<T, R>(this TDSPR<T> spr, Func<T, ValueTask<SPR<R>>> del)
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

    public static async ValueTask<TDSPR<R>> Transform<T, R>(this TDSPR<T> spr, Func<T, Task<SPR<R>>> del)
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


    public static async ValueTask<TDSPR<R>> Transform<T, R>(this TDSPR<T> spr, Func<TDSPR<T>, ValueTask<R>> del)
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

    public static async ValueTask<TDSPR<R>> Transform<T, R>(this TDSPR<T> spr, Func<TDSPR<T>, Task<R>> del)
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


    public static async ValueTask<TDSPR<R>> Transform<T, R>(this TDSPR<T> spr, Func<TDSPR<T>, ValueTask<SPR<R>>> del)
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

    public static async ValueTask<TDSPR<R>> Transform<T, R>(this TDSPR<T> spr, Func<TDSPR<T>, Task<SPR<R>>> del)
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

public static class TDSPR_From_ValueTask_Transformer
{
    public static async ValueTask<TDSPR<R>> Transform<T, R>(this ValueTask<TDSPR<T>> taskSpr, Func<T, R> del)
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

    public static async ValueTask<TDSPR<R>> Transform<T, R>(this ValueTask<TDSPR<T>> taskSpr, Func<T, SPR<R>> del)
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

    public static async ValueTask<TDSPR<R>> Transform<T, R>(this ValueTask<TDSPR<T>> taskSpr, Func<TDSPR<T>, SPR<R>> del)
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


    public static async ValueTask<TDSPR<R>> Transform<T, R>(this ValueTask<TDSPR<T>> taskSpr, Func<T, Task<R>> del)
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

    public static async ValueTask<TDSPR<R>> Transform<T, R>(this ValueTask<TDSPR<T>> taskSpr, Func<T, Task<SPR<R>>> del)
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

    public static async ValueTask<TDSPR<R>> Transform<T, R>(this ValueTask<TDSPR<T>> taskSpr, Func<TDSPR<T>, Task<SPR<R>>> del)
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


    public static async ValueTask<TDSPR<R>> Transform<T, R>(this ValueTask<TDSPR<T>> taskSpr, Func<T, ValueTask<R>> del)
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

    public static async ValueTask<TDSPR<R>> Transform<T, R>(this ValueTask<TDSPR<T>> taskSpr, Func<T, ValueTask<SPR<R>>> del)
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

    public static async ValueTask<TDSPR<R>> Transform<T, R>(this ValueTask<TDSPR<T>> taskSpr, Func<TDSPR<T>, ValueTask<SPR<R>>> del)
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

public static class TDSPR_From_Task_Transformer
{
    public static async ValueTask<TDSPR<R>> Transform<T, R>(this Task<TDSPR<T>> taskSpr, Func<T, R> del)
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

    public static async ValueTask<TDSPR<R>> Transform<T, R>(this Task<TDSPR<T>> taskSpr, Func<T, SPR<R>> del)
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

    public static async ValueTask<TDSPR<R>> Transform<T, R>(this Task<TDSPR<T>> taskSpr, Func<TDSPR<T>, SPR<R>> del)
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


    public static async ValueTask<TDSPR<R>> Transform<T, R>(this Task<TDSPR<T>> taskSpr, Func<T, Task<R>> del)
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

    public static async ValueTask<TDSPR<R>> Transform<T, R>(this Task<TDSPR<T>> taskSpr, Func<T, Task<SPR<R>>> del)
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

    public static async ValueTask<TDSPR<R>> Transform<T, R>(this Task<TDSPR<T>> taskSpr, Func<TDSPR<T>, Task<SPR<R>>> del)
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


    public static async ValueTask<TDSPR<R>> Transform<T, R>(this Task<TDSPR<T>> taskSpr, Func<T, ValueTask<R>> del)
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

    public static async ValueTask<TDSPR<R>> Transform<T, R>(this Task<TDSPR<T>> taskSpr, Func<T, ValueTask<SPR<R>>> del)
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

    public static async ValueTask<TDSPR<R>> Transform<T, R>(this Task<TDSPR<T>> taskSpr, Func<TDSPR<T>, ValueTask<SPR<R>>> del)
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

































public interface ISP
{
    public SPF Fault { get; }

    public bool Faulted();
    public bool Succeed();
}

public interface ISP<T> : ISP
{
    internal SPV<T> Value { get; set; }

    public new bool Succeed() => Value.Completed;
    public bool Succeed(out T result)
    {
        if (Value.Completed)
        {
            result = Value.Payload;
            return true;
        }

        result = default!;
        return false;
    }

    public new bool Faulted() => !Value.Completed;
    public bool Faulted(out SPF fault)
    {
        if (!Value.Completed)
        {
            fault = Fault;
            return true;
        }

        fault = default;
        return false;
    }
}


public interface ISPRConvertible<To>
{
    public To Descend();
}

public interface IDSP : ISP
{
    internal List<KeyValuePair<short, IDisposable>>? Disposables { get; set; }
    internal List<KeyValuePair<short, IAsyncDisposable>>? AsyncDisposables { get; set; }

    public void InternalDispose(short index = -1)
    {
        foreach (var item in Disposables ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                c.Dispose();
    }

    public void InternalDisposeAll()
    {
        foreach (var item in Disposables ?? [])
            item.Value?.Dispose();
    }
}

public interface IDSP<T> : IDSP, ISP<T>;
public interface IDSP<T, Ret> : IDSP<T>
{
    public Ret MarkDispose(short index = 0);

    public void InternalMarkDispose(short index = 0)
    {
        if (!Value.Completed)
            return;

        if (Value.Payload is IDisposable dis)
            (Disposables ??= [])
                .Add(new(index, dis));

        else if (Value.Payload is IAsyncDisposable adis)
            (AsyncDisposables ??= [])
                .Add(new(index, adis));
    }
}

public interface IDSP<T, Ret, AllRet> : IDSP<T, Ret>
{
    public Ret Dispose(short index = -1);
    public AllRet DisposeAll();
}



public interface ITSP : ISP
{
    internal List<KeyValuePair<short, TransactionScope>>? Transactions { get; set; }

    internal void InternalCompleteScope(short index = -1)
    {
        foreach (var item in ((ITSP)this).Transactions ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
            {
                if (Succeed())
                    c.Complete();
                c.Dispose();
            }
    }

    internal void InternalDisposeScope(short index = -1)
    {
        foreach (var item in Transactions ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                c.Dispose();
    }

    internal void InternalCompleteAllScopes()
    {
        foreach (var item in Transactions ?? [])
            InternalCompleteScope(item.Key);
    }

    internal void InternalDisposeAllScopes()
    {
        foreach (var item in Transactions ?? [])
            InternalDisposeScope(item.Key);
    }

}
public interface ITSP<T> : ITSP, ISP<T>;
public interface ITSP<T, Ret> : ITSP<T>
{
    public Ret MarkScope(short index);

    internal void InternalMarkScope(short index = 0)
    {
        if (Value.Completed && Value.Payload is TransactionScope tr)
            (Transactions ??= [])
                .Add(new(index, tr));
    }
}

public interface ITSP<T, Ret, AllRet> : ITSP<T, Ret>
{
    public Ret CompleteScope(short index);
    public Ret DisposeScope(short index);
    public AllRet CompleteAllScopes();
    public AllRet DisposeAllScopes();
}