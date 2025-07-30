using EGO.Gladius.Contracts;

using System.Diagnostics.CodeAnalysis;
using System.Transactions;

namespace EGO.Gladius.DataTypes;

public struct TSPR<T>
{
    internal List<KeyValuePair<int, TransactionScope>>? Transactions;

    public SPR<T> SPR { get; set; }

    public TSPR(SPR<T> spr)
    {
        SPR = spr;
    }

    public TSPR<T> MarkScope(int index = 0)
    {
        if (!SPR.Succeed(out var res))
            return this;

        if (res is TransactionScope tr)
            (Transactions ??= [])
                .Add(new(index, tr));

        return this;
    }
    public TSPR<T> MarkScope<E>(E index) where E : Enum =>
        MarkScope(Convert.ToInt32(index));

    public TSPR<T> CompleteScope<E>(E index) where E : Enum =>
        CompleteScope(Convert.ToInt32(index));

    public TSPR<T> CompleteScope(int index = -1)
    {
        foreach (var item in Transactions ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
            {
                if (Succeed())
                    c.Complete();
                c.Dispose();
            }
        return this;
    }

    public DSPR<T> DisposeScope<E>(E index) where E : Enum =>
        DisposeScope(Convert.ToInt32(index));

    public DSPR<T> DisposeScope(int index = -1)
    {
        foreach (var item in Transactions ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                c.Dispose();

        return this;
    }
}

public struct DSPR<T>
{
    internal List<KeyValuePair<int, IDisposable>>? Disposables;
    internal List<KeyValuePair<int, IAsyncDisposable>>? AsyncDisposables;

    public SPR<T> SPR { get; set; }

    public DSPR(SPR<T> spr)
    {
        SPR = spr;
    }

    public DSPR(SPR<T> spr, List<KeyValuePair<int, IDisposable>>? disposables, List<KeyValuePair<int, IAsyncDisposable>>? asyncDisposables)
    {
        SPR = spr;
        Disposables = disposables;
        AsyncDisposables = asyncDisposables;
    }

    #region suppress fault

    public DSPR<T> SuppressFault([NotNull] Func<SPR<T>, SPR<T>> del)
    {
        if (Succeed())
            return this;

        return Transform(del);
    }

    public async ValueTask<DSPR<T>> SuppressFault([NotNull] Func<SPR<T>, ValueTask<SPR<T>>> del)
    {
        if (Succeed())
            return this;

        return await Transform(del);
    }

    #endregion

    #region suppress null

    public DSPR<T> SuppressNull([NotNull] Func<T> del)
    {
        if (Faulted())
            return this;

        if (HasValue())
            return this;
        del += () => default;
        try
        {
            return Pass(del());
        }
        catch (Exception e)
        {
            return Fault(SPF.Gen(del.Method, e));
        }
    }

    public async ValueTask<DSPR<T>> SuppressNull([NotNull] Func<ValueTask<SPR<T>>> del)
    {
        if (Faulted())
            return this;

        if (HasValue())
            return this;

        try
        {
            return Pass(await del());
        }
        catch (Exception e)
        {
            return Fault(SPF.Gen(del.Method, e));
        }
    }

    #endregion

    #region transparent

    public DSPR<T> Transparent([NotNull] Action<T?> del)
    {
        try
        {
            if (Succeed(out var res))
                del(res);
            else
                del(default);

            return this;
        }
        catch (Exception e)
        {
            return Fault(SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    public async ValueTask<DSPR<T>> Transparent([NotNull] Func<T?, Task> del)
    {
        try
        {
            if (Succeed(out var res))
                await del(res);
            else
                await del(default);

            return this;
        }
        catch (Exception e)
        {
            return Fault(SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    public async ValueTask<DSPR<T>> Transparent([NotNull] Func<T?, ValueTask> del)
    {
        try
        {
            if (Succeed(out var res))
                await del(res);
            else
                await del(default);

            return this;
        }
        catch (Exception e)
        {
            return Fault(SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    public DSPR<T> Transparent([NotNull] Action del)
    {
        try
        {
            del();

            return this;
        }
        catch (Exception e)
        {
            return Fault(SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    public async ValueTask<DSPR<T>> Transparent([NotNull] Func<Task> del)
    {
        try
        {
            await del();

            return this;
        }
        catch (Exception e)
        {
            return Fault(SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    public async ValueTask<DSPR<T>> Transparent([NotNull] Func<ValueTask> del)
    {
        try
        {
            await del();

            return this;
        }
        catch (Exception e)
        {
            return Fault(SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    #endregion

    #region transform

    public DSPR<R> Transform<R>([NotNull] Func<T, R> del)
    {
        try
        {
            if (Succeed(out var res))
                return Pass(del(res));

            return Fault<R>();
        }
        catch (Exception e)
        {
            return Fault<R>(SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    public DSPR<R> Transform<R>([NotNull] Func<T, SPR<R>> del)
    {
        try
        {
            if (Succeed(out var res))
                return Pass(del(res));

            return Fault<R>();
        }
        catch (Exception e)
        {
            return Fault<R>(SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    private DSPR<R> Transform<R>([NotNull] Func<SPR<T>, SPR<R>> del)
    {
        try
        {
            if (Succeed(out var res))
                return Pass(del(res));

            return Fault<R>();
        }
        catch (Exception e)
        {
            return Fault<R>(SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    public async ValueTask<DSPR<R>> Transform<R>([NotNull] Func<T, ValueTask<SPR<R>>> del)
    {
        try
        {
            if (Succeed(out var res))
                return Pass(await del(res));

            return Fault<R>();
        }
        catch (Exception e)
        {
            return Fault<R>(SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    public async ValueTask<DSPR<R>> Transform<R>([NotNull] Func<SPR<T>, ValueTask<SPR<R>>> del)
    {
        try
        {
            if (Succeed(out var res))
                return Pass(await del(res));

            return Fault<R>();
        }
        catch (Exception e)
        {
            return Fault<R>(SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    public async ValueTask<DSPR<R>> Transform<R>([NotNull] Func<T, ValueTask<R>> del)
    {
        try
        {
            if (Succeed(out var res))
                return Pass(await del(res));

            return Fault<R>();
        }
        catch (Exception e)
        {
            return Fault<R>(SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    public async ValueTask<DSPR<R>> Transform<R>([NotNull] Func<T, Task<SPR<R>>> del)
    {
        try
        {
            if (Succeed(out var res))
                return Pass(await del(res));

            return Fault<R>();
        }
        catch (Exception e)
        {
            return Fault<R>(SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    public async ValueTask<DSPR<R>> Transform<R>([NotNull] Func<T, Task<R>> del)
    {
        try
        {
            if (Succeed(out var res))
                return Pass(await del(res));

            return Fault<R>();
        }
        catch (Exception e)
        {
            return Fault<R>(SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    #endregion

    public DSPR<T> MarkDispose<E>(E index) where E : Enum =>
        MarkDispose(Convert.ToInt32(index));

    public DSPR<T> MarkDispose(int index = 0)
    {
        if (!SPR.Succeed(out var res))
            return this;

        if (res is IAsyncDisposable adis)
            (AsyncDisposables ??= [])
                .Add(new(index, adis));

        else if (res is IDisposable dis)
            (Disposables ??= [])
                .Add(new(index, dis));

        return this;
    }


    public DSPR<T> Dispose<E>(E index) where E : Enum =>
        Dispose(Convert.ToInt32(index));

    public DSPR<T> Dispose(int index = -1)
    {
        foreach (var item in Disposables ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                c.Dispose();

        return this;
    }


    public ValueTask<DSPR<T>> DisposeAsync<E>(E index) where E : Enum =>
        DisposeAsync(Convert.ToInt32(index));

    public async ValueTask<DSPR<T>> DisposeAsync(int index = -1)
    {
        foreach (var item in AsyncDisposables ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                await c.DisposeAsync();

        return this;
    }

    public SPR<T> DisposeAll()
    {
        foreach (var item in Disposables ?? [])
            item.Value?.Dispose();

        return SPR;
    }

    public async ValueTask<SPR<T>> DisposeAllAsync()
    {
        foreach (var item in AsyncDisposables ?? [])
            if (item.Value is { } c)
                await c.DisposeAsync();

        return SPR;
    }

    public DSPR<T> Pass(SPR<T> spr) =>
        new(spr, Disposables, AsyncDisposables, Transactions);

    public DSPR<R> Pass<R>(SPR<R> spr) =>
        new(spr, Disposables, AsyncDisposables, Transactions);

    public DSPR<T> Pass(T val) =>
        new(val, Disposables, AsyncDisposables, Transactions);

    public DSPR<R> Pass<R>(R val) =>
        new(val, Disposables, AsyncDisposables, Transactions);

    public bool Faulted() =>
        SPR.Faulted();

    public bool Succeed() =>
        SPR.Succeed();

    public bool Succeed(out T result) =>
        SPR.Succeed(out result);

    public bool Succeed(out T result, out SPF fault) =>
        SPR.Succeed(out result, out fault);

    public bool Faulted(out SPF fault) =>
        SPR.Faulted(out fault);

    public bool HasValue() =>
        SPR.HasValue();

    public DSPR<T> Fault() =>
        new(new(SPR.Fault), Disposables, AsyncDisposables, Transactions);

    public DSPR<T> Fault(SPF fault) =>
        new(new(fault), Disposables, AsyncDisposables, Transactions);

    public DSPR<R> Fault<R>() =>
        new(new(SPR.Fault), Disposables, AsyncDisposables, Transactions);

    public DSPR<R> Fault<R>(SPF fault) =>
        new(new(fault), Disposables, AsyncDisposables, Transactions);

    public DVSP ToDVSP() =>
        new(SPR.Fault, Disposables, AsyncDisposables, Transactions);

    public DVSP ToDVSP(SPF fault) =>
        new(fault, Disposables, AsyncDisposables, Transactions);

}
