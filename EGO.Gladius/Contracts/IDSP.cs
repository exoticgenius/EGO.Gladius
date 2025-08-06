using EGO.Gladius.DataTypes;

using System.Transactions;

namespace EGO.Gladius.Contracts;


public interface IDSP : ISP
{
    internal List<KeyValuePair<short, IDisposable>>? Disposables { get; }
    internal List<KeyValuePair<short, IAsyncDisposable>>? AsyncDisposables { get; }
}

public interface IDSP<T> : IDSP, ISP<T>;
public interface IDSP<T, Ret> : IDSP<T>
{
    public Ret MarkDispose(short index = 0);
}

public interface IDSP<T, Ret, AllRet> : IDSP<T, Ret>
{
    public Ret Dispose(short index = -1);
    public AllRet DisposeAll();
}