using System.Transactions;

namespace EGO.Gladius.Contracts;

public interface ITSP
{
    internal List<KeyValuePair<short, TransactionScope>>? Transactions { get; }
}

public interface ITSP<T> : ITSP, ISP<T>;

public interface ITSP<Ret, AllRet> : ISP, ITSP
{
    Ret CompleteScope(short index);
    Ret CompleteScope<E>(E index) where E : Enum;

    Ret DisposeScope(short index);
    Ret DisposeScope<E>(E index) where E : Enum;

    AllRet CompleteAllScopes();
    AllRet DisposeAllScopes();
}

public interface ITSP<Ret, AllRet, T> : ITSP<T>, ITSP<Ret, AllRet>
{
    Ret MarkScope(short index);
    Ret MarkScope<E>(E index) where E : Enum;
}
