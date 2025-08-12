using System.Diagnostics.CodeAnalysis;
using System.Transactions;

namespace EGO.Gladius.Old;

public struct O_TSPR<T>
{
    internal List<KeyValuePair<int, TransactionScope>>? Transactions;

    public O_SPR<T> SPR { get; set; }

    public O_TSPR(O_SPR<T> spr)
    {
        SPR = spr;
    }

    public O_TSPR<T> MarkScope(int index = 0)
    {
        if (!SPR.Succeed(out T? res))
            return this;

        if (res is TransactionScope tr)
            (Transactions ??= [])
                .Add(new(index, tr));

        return this;
    }
    public O_TSPR<T> MarkScope<E>(E index) where E : Enum =>
        MarkScope(Convert.ToInt32(index));

    public O_TSPR<T> CompleteScope<E>(E index) where E : Enum =>
        CompleteScope(Convert.ToInt32(index));

    public O_TSPR<T> CompleteScope(int index = -1)
    {
        foreach (KeyValuePair<int, TransactionScope> item in Transactions ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
            {
                //if (Succeed())
                //    c.Complete();
                c.Dispose();
            }
        return this;
    }

    public O_TSPR<T> DisposeScope<E>(E index) where E : Enum =>
        DisposeScope(Convert.ToInt32(index));

    public O_TSPR<T> DisposeScope(int index = -1)
    {
        foreach (KeyValuePair<int, TransactionScope> item in Transactions ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                c.Dispose();

        return this;
    }
}

public struct O_DSPR<T>
{
    internal List<KeyValuePair<int, IDisposable>>? Disposables;
    internal List<KeyValuePair<int, IAsyncDisposable>>? AsyncDisposables;

    public O_SPR<T> SPR { get; set; }

    public O_DSPR(O_SPR<T> spr)
    {
        SPR = spr;
    }

    public O_DSPR(O_SPR<T> spr, List<KeyValuePair<int, IDisposable>>? disposables, List<KeyValuePair<int, IAsyncDisposable>>? asyncDisposables)
    {
        SPR = spr;
        Disposables = disposables;
        AsyncDisposables = asyncDisposables;
    }

    #region suppress fault

    public O_DSPR<T> SuppressFault([NotNull] Func<O_SPR<T>, O_SPR<T>> del)
    {
        if (Succeed())
            return this;

        return Transform(del);
    }

    public async ValueTask<O_DSPR<T>> SuppressFault([NotNull] Func<O_SPR<T>, ValueTask<O_SPR<T>>> del)
    {
        if (Succeed())
            return this;

        return await Transform(del);
    }

    #endregion

    #region suppress null

    public O_DSPR<T> SuppressNull([NotNull] Func<T> del)
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
            return Fault(O_SPF.Gen(del.Method, e));
        }
    }

    public async ValueTask<O_DSPR<T>> SuppressNull([NotNull] Func<ValueTask<O_SPR<T>>> del)
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
            return Fault(O_SPF.Gen(del.Method, e));
        }
    }

    #endregion

    #region transparent

    public O_DSPR<T> Transparent([NotNull] Action<T?> del)
    {
        try
        {
            if (Succeed(out T? res))
                del(res);
            else
                del(default);

            return this;
        }
        catch (Exception e)
        {
            return Fault(O_SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    public async ValueTask<O_DSPR<T>> Transparent([NotNull] Func<T?, Task> del)
    {
        try
        {
            if (Succeed(out T? res))
                await del(res);
            else
                await del(default);

            return this;
        }
        catch (Exception e)
        {
            return Fault(O_SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    public async ValueTask<O_DSPR<T>> Transparent([NotNull] Func<T?, ValueTask> del)
    {
        try
        {
            if (Succeed(out T? res))
                await del(res);
            else
                await del(default);

            return this;
        }
        catch (Exception e)
        {
            return Fault(O_SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    public O_DSPR<T> Transparent([NotNull] Action del)
    {
        try
        {
            del();

            return this;
        }
        catch (Exception e)
        {
            return Fault(O_SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    public async ValueTask<O_DSPR<T>> Transparent([NotNull] Func<Task> del)
    {
        try
        {
            await del();

            return this;
        }
        catch (Exception e)
        {
            return Fault(O_SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    public async ValueTask<O_DSPR<T>> Transparent([NotNull] Func<ValueTask> del)
    {
        try
        {
            await del();

            return this;
        }
        catch (Exception e)
        {
            return Fault(O_SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    #endregion

    #region transform

    public O_DSPR<R> Transform<R>([NotNull] Func<T, R> del)
    {
        try
        {
            if (Succeed(out T? res))
                return Pass(del(res));

            return Fault<R>();
        }
        catch (Exception e)
        {
            return Fault<R>(O_SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    public O_DSPR<R> Transform<R>([NotNull] Func<T, O_SPR<R>> del)
    {
        try
        {
            if (Succeed(out T? res))
                return Pass(del(res));

            return Fault<R>();
        }
        catch (Exception e)
        {
            return Fault<R>(O_SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    private O_DSPR<R> Transform<R>([NotNull] Func<O_SPR<T>, O_SPR<R>> del)
    {
        try
        {
            if (Succeed(out T? res))
                return Pass(del(res));

            return Fault<R>();
        }
        catch (Exception e)
        {
            return Fault<R>(O_SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    public async ValueTask<O_DSPR<R>> Transform<R>([NotNull] Func<T, ValueTask<O_SPR<R>>> del)
    {
        try
        {
            if (Succeed(out T? res))
                return Pass(await del(res));

            return Fault<R>();
        }
        catch (Exception e)
        {
            return Fault<R>(O_SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    public async ValueTask<O_DSPR<R>> Transform<R>([NotNull] Func<O_SPR<T>, ValueTask<O_SPR<R>>> del)
    {
        try
        {
            if (Succeed(out T? res))
                return Pass(await del(res));

            return Fault<R>();
        }
        catch (Exception e)
        {
            return Fault<R>(O_SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    public async ValueTask<O_DSPR<R>> Transform<R>([NotNull] Func<T, ValueTask<R>> del)
    {
        try
        {
            if (Succeed(out T? res))
                return Pass(await del(res));

            return Fault<R>();
        }
        catch (Exception e)
        {
            return Fault<R>(O_SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    public async ValueTask<O_DSPR<R>> Transform<R>([NotNull] Func<T, Task<O_SPR<R>>> del)
    {
        try
        {
            if (Succeed(out T? res))
                return Pass(await del(res));

            return Fault<R>();
        }
        catch (Exception e)
        {
            return Fault<R>(O_SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    public async ValueTask<O_DSPR<R>> Transform<R>([NotNull] Func<T, Task<R>> del)
    {
        try
        {
            if (Succeed(out T? res))
                return Pass(await del(res));

            return Fault<R>();
        }
        catch (Exception e)
        {
            return Fault<R>(O_SPF.Gen(del.Method, [SPR.ExtractPayload()], e));
        }
    }

    #endregion

    public O_DSPR<T> MarkDispose<E>(E index) where E : Enum =>
        MarkDispose(Convert.ToInt32(index));

    public O_DSPR<T> MarkDispose(int index = 0)
    {
        if (!SPR.Succeed(out T? res))
            return this;

        if (res is IAsyncDisposable adis)
            (AsyncDisposables ??= [])
                .Add(new(index, adis));

        else if (res is IDisposable dis)
            (Disposables ??= [])
                .Add(new(index, dis));

        return this;
    }


    public O_DSPR<T> Dispose<E>(E index) where E : Enum =>
        Dispose(Convert.ToInt32(index));

    public O_DSPR<T> Dispose(int index = -1)
    {
        foreach (KeyValuePair<int, IDisposable> item in Disposables ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                c.Dispose();

        return this;
    }


    public ValueTask<O_DSPR<T>> DisposeAsync<E>(E index) where E : Enum =>
        DisposeAsync(Convert.ToInt32(index));

    public async ValueTask<O_DSPR<T>> DisposeAsync(int index = -1)
    {
        foreach (KeyValuePair<int, IAsyncDisposable> item in AsyncDisposables ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                await c.DisposeAsync();

        return this;
    }

    public O_SPR<T> DisposeAll()
    {
        foreach (KeyValuePair<int, IDisposable> item in Disposables ?? [])
            item.Value?.Dispose();

        return SPR;
    }

    public async ValueTask<O_SPR<T>> DisposeAllAsync()
    {
        foreach (KeyValuePair<int, IAsyncDisposable> item in AsyncDisposables ?? [])
            if (item.Value is { } c)
                await c.DisposeAsync();

        return SPR;
    }

    public O_DSPR<T> Pass(O_SPR<T> spr) => throw new Exception();
    //new (spr, Disposables, AsyncDisposables, Transactions);

    public O_DSPR<R> Pass<R>(O_SPR<R> spr) => throw new Exception();
    //new (spr, Disposables, AsyncDisposables, Transactions);

    public O_DSPR<T> Pass(T val) => throw new Exception();
    //new (val, Disposables, AsyncDisposables, Transactions);

    public O_DSPR<R> Pass<R>(R val) => throw new Exception();
    //        new(val, Disposables, AsyncDisposables, Transactions);

    public bool Faulted() =>
        SPR.Faulted();

    public bool Succeed() =>
        SPR.Succeed();

    public bool Succeed(out T result) =>
        SPR.Succeed(out result);

    public bool Succeed(out T result, out O_SPF fault) =>
        SPR.Succeed(out result, out fault);

    public bool Faulted(out O_SPF fault) =>
        SPR.Faulted(out fault);

    public bool HasValue() =>
        SPR.HasValue();

    public O_DSPR<T> Fault() => throw new Exception();
    // new (new(SPR.Fault), Disposables, AsyncDisposables, Transactions);

    public O_DSPR<T> Fault(O_SPF fault) => throw new Exception();
    //new (new(fault), Disposables, AsyncDisposables, Transactions);

    public O_DSPR<R> Fault<R>() => throw new Exception();
    //new (new(SPR.Fault), Disposables, AsyncDisposables, Transactions);

    public O_DSPR<R> Fault<R>(O_SPF fault) => throw new Exception();
    //new (new(fault), Disposables, AsyncDisposables, Transactions);

    public O_DVSP ToDVSP() => throw new Exception();
    //new (SPR.Fault, Disposables, AsyncDisposables, Transactions);

    public O_DVSP ToDVSP(O_SPF fault) => throw new Exception();
    // new (fault, Disposables, AsyncDisposables, Transactions);throw new Exception();

}
