using EGO.Gladius.DataTypes;
using EGO.Gladius.Extensions;

namespace DEF
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("before");


            //SPR<int> res = SPR.FromResult(1).To(Transform).To(Transform);

            //Console.WriteLine("after");

            //if (res.Succeed(out var re))
            //    Console.WriteLine(re);
            //else
            //    Console.WriteLine(res.Fault.Exception.Message ?? res.Fault.Message);

            var res =  await Transform(3);

            Console.WriteLine(res.Fault.Exception?.Message);

            Console.WriteLine("caught");

            Console.ReadLine();
        }

        public static async Task<SPR<int>> Transform(int x)
        {
                try
                {
                    await Task.Delay(33);
                    Console.WriteLine("inner");
                    if (x == 3)
                        throw new Exception("catch 3");
                    return x + 2;
                }
                catch (Exception e)
                {
                    throw new Exception("catch 4");
                }
                finally
                {
                    Console.WriteLine("zsd,fjghdruighr");
                }
        }
    }
}