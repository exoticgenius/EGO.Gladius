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

            Console.ReadLine();
        }

        public  static int Transform(int x)
        {
            Console.WriteLine("inner");
            return x + 2;
        }
    }
}