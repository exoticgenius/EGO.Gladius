using EGO.Gladius.DataTypes;

using System;
using System.Transactions;

namespace EGO.Gladius.Contracts;

public static class ISPTransformer
{
    public static N_SPR<R> Transform<T, R>(this N_SPR<T> spr, Func<T, R> del)
    {
        try
        {
            if (spr.Succeed())
                return spr.Pass(del(((ISP<T>)spr).Value.Payload));

            return spr.Fault;
        }
        catch (Exception e)
        {
            return N_SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
        }
    }
    
    public static N_SPR<R> Transform<T, R>(this N_SPR<T> spr, Func<T, N_SPR<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return del(((ISP<T>)spr).Value.Payload);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return N_SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
        }
    }

    public static N_SPR<R> Transform<T, R>(this N_SPR<T> spr, Func<N_SPR<T>, N_SPR<R>> del)
    {
        try
        {
            if (spr.Succeed())
                return del(spr);

            return spr.Fault;
        }
        catch (Exception e)
        {
            return N_SPF.Gen(del.Method, [((ISP<T>)spr).Value.Payload], e);
        }
    }

}



public interface ISP//: ISPPass
{
    public N_SPF Fault { get; }

    public bool Faulted();
    public  bool Succeed();
}

public interface ISP<T> : ISP
{
    internal N_SPV<T> Value { get; set; }

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
    public bool Faulted(out N_SPF fault)
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

public interface ISPPass
{
    internal X Pass<X, T>(T input) where X : ISP<T>;
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