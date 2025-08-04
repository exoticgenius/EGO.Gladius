using EGO.Gladius.DataTypes;

using System.Transactions;

namespace EGO.Gladius.Contracts;


public interface IDSP : ISP
{
    internal List<KeyValuePair<short, IDisposable>>? Disposables { get; }
    internal List<KeyValuePair<short, IAsyncDisposable>>? AsyncDisposables { get; }

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
            Disposables?.Add(new(index, dis));

        else if (Value.Payload is IAsyncDisposable adis)
            AsyncDisposables?.Add(new(index, adis));
    }
}

public interface IDSP<T, Ret, AllRet> : IDSP<T, Ret>
{
    public Ret Dispose(short index = -1);
    public AllRet DisposeAll();
}