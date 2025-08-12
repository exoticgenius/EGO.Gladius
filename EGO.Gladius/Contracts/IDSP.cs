namespace EGO.Gladius.Contracts;

public interface IDSP
{
    internal List<KeyValuePair<short, IDisposable>>? Disposables { get; }
    internal List<KeyValuePair<short, IAsyncDisposable>>? AsyncDisposables { get; }
}

public interface IDSP<T> : IDSP, ISP<T>;

public interface IDSP<Ret, AllRet> : ISP, IDSP
{
    Ret Dispose(short index = -1);
    Ret Dispose<E>(E index) where E : Enum;
    AllRet DisposeAll();

    ValueTask<Ret> DisposeAsync(short index = -1);
    ValueTask<Ret> DisposeAsync<E>(E index) where E : Enum;
    ValueTask<AllRet> DisposeAllAsync();
}

public interface IDSP<Ret, AllRet, T> : IDSP<T>, IDSP<Ret, AllRet>
{
    Ret MarkDispose(short index = 0);
    Ret MarkDispose<E>(E index) where E : Enum;
}
