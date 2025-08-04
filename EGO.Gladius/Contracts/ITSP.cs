using System.Transactions;

namespace EGO.Gladius.Contracts;

public interface ITSP : ISP
{
    internal List<KeyValuePair<short, TransactionScope>>? Transactions { get; }

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
            Transactions?.Add(new(index, tr));
    }
}

public interface ITSP<T, Ret, AllRet> : ITSP<T, Ret>
{
    public Ret CompleteScope(short index);
    public Ret DisposeScope(short index);
    public AllRet CompleteAllScopes();
    public AllRet DisposeAllScopes();
}