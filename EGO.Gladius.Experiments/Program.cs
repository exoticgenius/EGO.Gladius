using EGO.Gladius.DataTypes;
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
            await Task.Yield();

            //Console.WriteLine("before");


            //SPR<string> result = "Your Result".AsSPR();

            //SPR<string> result = GetSomeResult();

            ////if (result.Faulted())
            ////{
            ////    // handle error here using
            ////    var fault = result.Fault;
            ////    fault.Message ...
            ////    fault.Exception ...
            ////    fault.CapturedContext ...
            ////    fault.Parameters ...
            ////}

            //if(result.Succeed(out var val))
            //{
            //    // if result succeeded you can have the returned value
            //}
            //else
            //{
            //    // also you have to implement error handling right here
            //    // to have a more robust method with least edge cases and unhandled errors/branches
            //}











            //    //SPR<int> res = SPR.FromResult(1).To(Transform).To(Transform);

            //    //Console.WriteLine("after");

            //    //if (res.Succeed(out var re))
            //    //    Console.WriteLine(re);
            //    //else
            //    //    Console.WriteLine(res.Fault.Exception.Message ?? res.Fault.Message);

            //    var res = Transform(3).MarkScope(TransactionSteps.First).CompleteScope(TransactionSteps.First);

            //Console.WriteLine(res.Fault.Exception?.Message);

            //Console.WriteLine("caught");

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

        static bool hasError = true;

        SPR<string> GetSomeResult()
        {

            if (hasError)
                throw new Exception("Faulted Here");

            if (hasError)
                SPF.Gen("Faulted Here");

            return "Processed Result";
        }
    }
}