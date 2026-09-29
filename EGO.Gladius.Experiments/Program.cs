using EGO.Gladius.Contracts;
using EGO.Gladius.DataTypes;
using EGO.Gladius.Metadata;

namespace DEF;

public class Program
{
    public enum TransactionSteps
    {
        First
    }
    public static async Task Main(string[] args)
    {
        await Task.Yield();
        var r = GetSomeResult("123");
        if (r.Succeed())
        {
            Console.WriteLine("caught");
        }
        else
        {
            Console.WriteLine(r.Fault.Message ?? r.Fault.Exception.Message);
        }

        Console.ReadLine();
    }

    public static VSP GetSomeResult<T>(T input)
    {
        throw  new  NotImplementedException();
    }
}