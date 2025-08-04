using EGO.Gladius.Contracts;
using EGO.Gladius.DataTypes;

using System.Transactions;

namespace EGO.Gladius.Old;

public struct O_DVSP
{
    public static readonly O_DVSP Completed = new();

    internal List<KeyValuePair<int, IDisposable>>? Disposables;
    internal List<KeyValuePair<int, IAsyncDisposable>>? AsyncDisposables;
    internal List<KeyValuePair<int, TransactionScope>>? Transactions;

    private bool Success { get; }
    public O_SPF Fault { get; }

    public O_DVSP()
    {
        Success = true;
        Fault = default;
    }

    public O_DVSP(O_SPF fault)
    {
        Success = false;
        Fault = fault;
    }

    public O_DVSP(O_SPF fault, List<KeyValuePair<int, IDisposable>>? disposables, List<KeyValuePair<int, IAsyncDisposable>>? asyncDisposables, List<KeyValuePair<int, TransactionScope>>? transactions)
    {
        Fault = fault;
        Disposables = disposables;
        AsyncDisposables = asyncDisposables;
        Transactions = transactions;
    }

    public O_DVSP(List<KeyValuePair<int, IDisposable>>? disposables, List<KeyValuePair<int, IAsyncDisposable>>? asyncDisposables, List<KeyValuePair<int, TransactionScope>>? transactions)
    {
        Success = true;
        Disposables = disposables;
        AsyncDisposables = asyncDisposables;
        Transactions = transactions;
    }

    public O_DVSP Dispose<E>(E index) where E : Enum =>
        Dispose(Convert.ToInt32(index));

    public O_DVSP Dispose(int index = -1)
    {
        foreach (var item in Disposables ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                c.Dispose();

        return this;
    }

    public ValueTask<O_DVSP> DisposeAsync<E>(E index) where E : Enum =>
        DisposeAsync(Convert.ToInt32(index));

    public async ValueTask<O_DVSP> DisposeAsync(int index = -1)
    {
        foreach (var item in AsyncDisposables ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
                await c.DisposeAsync();

        return this;
    }

    public O_DVSP DisposeAll()
    {
        foreach (var item in Disposables ?? [])
            item.Value?.Dispose();

        return Completed;
    }

    public async ValueTask<O_DVSP> DisposeAllAsync()
    {
        foreach (var item in AsyncDisposables ?? [])
            if (item.Value is { } c)
                await c.DisposeAsync();

        return Completed;
    }

    public bool Succeed() => Success;

    public bool Faulted() => !Success;

    public bool Faulted(out O_SPF fault)
    {
        if (!Success)
        {
            fault = Fault;
            return true;
        }

        fault = default;
        return false;
    }

    public O_DVSP Pass(O_VSP spr) =>
        new(Fault, Disposables, AsyncDisposables, Transactions);

    public O_DVSP Pass<R>(O_SPR<R> spr) =>
        new(spr.Fault, Disposables, AsyncDisposables, Transactions);

    public O_DVSP CompleteScope<E>(E index) where E : Enum =>
        CompleteScope(Convert.ToInt32(index));

    public O_DVSP CompleteScope(int index = -1)
    {
        foreach (var item in Transactions ?? [])
            if ((index == -1 || item.Key == index) && item.Value is { } c)
            {
                c.Complete();
                c.Dispose();
            }
        return this;
    }

    public O_DVSP DisposeScope<E>(E index) where E : Enum =>
        DisposeScope(Convert.ToInt32(index));

    public O_DVSP DisposeScope(int index = -1)
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
