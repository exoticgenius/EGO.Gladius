using EGO.Gladius.DataTypes;
using EGO.Gladius.Extensions;

namespace DEF
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("before");


            SPR<int> res = SPR.FromResult(1).To(Transform).To(Transform);


            Console.WriteLine("after");





            if (res.Succeed(out var re))
                Console.WriteLine(re);
            else
                Console.WriteLine(res.Fault.Exception.Message ?? res.Fault.Message);


            SPR<int> res2 = SPR.FromResult(1).To(Transform).To(Transform);

            if (res2.Succeed(out var re2))
                Console.WriteLine(re2);
            else
                Console.WriteLine(res2.Fault.Exception.Message ?? res2.Fault.Message);

            Console.ReadLine();
        }

        public static int Transform(int x)
        {
                Console.WriteLine("inner");
                if (x == 3)
                    throw new Exception("catch 3");
                return x + 2;
        }
    }
}