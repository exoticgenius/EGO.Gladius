using EGO.Gladius.DataTypes;
using EGO.Gladius.Extensions;
namespace DEF
{
    public class Program
    {
        public enum TransactionSteps
        {
            First
        }
        public static async Task Main(string[] args)
        {
            Console.WriteLine("before");


            //SPR<int> res = SPR.FromResult(1).To(Transform).To(Transform);

            //Console.WriteLine("after");

            //if (res.Succeed(out var re))
            //    Console.WriteLine(re);
            //else
            //    Console.WriteLine(res.Fault.Exception.Message ?? res.Fault.Message);

            var res = Transform(3).MarkScope(TransactionSteps.First).CompleteScope(TransactionSteps.First);

            Console.WriteLine(res.Fault.Exception?.Message);

            Console.WriteLine("caught");

            Console.ReadLine();
        }

        public static SPR<int> Transform(int x)
        {
            if (x == 44)
                conv(x);

            else if (x == 109)
                throw new Exception("plain ex");

            if (x == 45)
                return SPF.Gen("fault");

            if (x == 46)
                return SPR.Completed;

            switch (x)
            {
                case 1: return 2;

                case 2: return 2;
                default:
                    throw new Exception("sw def");
            }

            int conv(int z)
            {
                return z + C3(z) * 2;

                int C3(int xx)
                {
                    return x + 1;
                }
            }
        }
    }
}