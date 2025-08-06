using System.Transactions;

namespace EGO.Gladius.Contracts;

public interface ITSP : ISP
{
    internal List<KeyValuePair<short, TransactionScope>>? Transactions { get; }



}

public interface ITSP<T> : ITSP, ISP<T>;
public interface ITSP<T, Ret> : ITSP<T>
{
    public Ret MarkScope(short index);

}

public interface ITSP<T, Ret, AllRet> : ITSP<T, Ret>
{
    public Ret CompleteScope(short index);
    public Ret DisposeScope(short index);
    public AllRet CompleteAllScopes();
    public AllRet DisposeAllScopes();
}