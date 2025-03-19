using EGO.Gladius.Contracts;

using System.Transactions;

namespace EGO.Gladius.DataTypes;

public struct DVSP : ISP
{
    public static readonly DVSP Completed = new();

    internal List<KeyValuePair<int, IDisposable>>? Disposables;
    internal List<KeyValuePair<int, IAsyncDisposable>>? AsyncDisposables;
    internal List<KeyValuePair<int, TransactionScope>>? Transactions;

    private bool Success { get; }
    public SPF Fault { get; }

    public DVSP()
    {
        Success = true;
        Fault = default;
    }

    public DVSP(SPF fault)
    {
        Success = false;
        Fault = fault;
    }

    public DVSP(SPF fault, List<KeyValuePair<int, IDisposable>>? disposables, List<KeyValuePair<int, IAsyncDisposable>>? asyncDisposables, List<KeyValuePair<int, TransactionScope>>? transactions)
    {
        Fault = fault;
        Disposables = disposables;
        AsyncDisposables = asyncDisposables;
        Transactions = transactions;
    }

    public DVSP(List<KeyValuePair<int, IDisposable>>? disposables, List<KeyValuePair<int, IAsyncDisposable>>? asyncDisposables, List<KeyValuePair<int, TransactionScope>>? transactions)
    {
        Success = true;
        Disposables = disposables;
        AsyncDisposables = asyncDisposables;
        Transactions = transactions;
    }

    public DVSP Dispose<E>(E index) where E : Enum =>
        Dispose(Convert.ToInt32(index));

    public DVSP Dispose(int index = -1)
    {
        foreach (var item in Disposables ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                c.Dispose();

        return this;
    }

    public ValueTask<DVSP> DisposeAsync<E>(E index) where E : Enum =>
        DisposeAsync(Convert.ToInt32(index));

    public async ValueTask<DVSP> DisposeAsync(int index = -1)
    {
        foreach (var item in AsyncDisposables ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                await c.DisposeAsync();

        return this;
    }

    public DVSP DisposeAll()
    {
        foreach (var item in Disposables ?? [])
            item.Value?.Dispose();

        return Completed;
    }

    public async ValueTask<DVSP> DisposeAllAsync()
    {
        foreach (var item in AsyncDisposables ?? [])
            if (item.Value is { } c)
                await c.DisposeAsync();

        return Completed;
    }

    public bool Succeed() => Success;

    public bool Faulted() => !Success;

    public bool Faulted(out SPF fault)
    {
        if (!Success)
        {
            fault = Fault;
            return true;
        }

        fault = default;
        return false;
    }

    public DVSP Pass(VSP spr) =>
        new(Fault, Disposables, AsyncDisposables, Transactions);

    public DVSP Pass<R>(SPR<R> spr) =>
        new(spr.Fault, Disposables, AsyncDisposables, Transactions);

    public DVSP CompleteScope<E>(E index) where E : Enum =>
        CompleteScope(Convert.ToInt32(index));

    public DVSP CompleteScope(int index = -1)
    {
        foreach (var item in Transactions ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
            {
                c.Complete();
                c.Dispose();
            }
        return this;
    }

    public DVSP DisposeScope<E>(E index) where E : Enum =>
        DisposeScope(Convert.ToInt32(index));

    public DVSP DisposeScope(int index = -1)
    {
        foreach (var item in Transactions ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                c.Dispose();

        return this;
    }

#if Release
    public override string ToString()
    {
        throw new Exception("calling ToString on VSP object is impossible");
    }
#endif
}
