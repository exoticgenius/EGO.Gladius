using EGO.Gladius.DataTypes;

using System;
using System.Transactions;

namespace EGO.Gladius.Contracts;


public interface ISP
{
    bool Faulted();
    bool Succeed();
}

public interface ISP<T>
{
    public N_SPV<T> Value { get; }
    public N_SPF Fault { get; }

    public bool Succeed() => Value.Completed;
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

    public bool Faulted() => !Value.Completed;
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

public interface ISPRConvertible<To>
{
    public To Descend();
}


public interface IDSP<T, Ret,AllRet> : ISP<T>
{
    internal List<KeyValuePair<short, IDisposable>>? Disposables { get; set; }
    internal List<KeyValuePair<short, IAsyncDisposable>>? AsyncDisposables { get; set; }

    Ret Dispose(short index = -1);
    AllRet DisposeAll();
    Ret MarkDispose(short index = 0);


    public void InternalMarkDispose(short index = 0)
    {
        if (!Value.Completed)
            return;

        if (Value.Payload is IAsyncDisposable adis)
            (AsyncDisposables ??= [])
                .Add(new(index, adis));

        else if (Value.Payload is IDisposable dis)
            (Disposables ??= [])
                .Add(new(index, dis));

    }

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

public interface ITSP<T, Ret> : ISP<T>
{
    internal List<KeyValuePair<short, TransactionScope>>? Transactions { get; set; }

    Ret MarkScope(short index);
    Ret CompleteScope(short index);
    Ret DisposeScope(short index);


    internal void InternalMarkScope(short index = 0)
    {
        if (Value.Completed && Value.Payload is TransactionScope tr)
            (Transactions ??= [])
                .Add(new(index, tr));
    }

    internal void InternalCompleteScope(short index = -1)
    {
        
        foreach (var item in Transactions ?? [])
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
}