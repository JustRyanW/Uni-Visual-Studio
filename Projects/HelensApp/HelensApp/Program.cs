using System;

namespace HelensApp
{
    class Program
    {
        static void Main()
        {
            double width;
            double length;


            Console.WriteLine("What is the width of your field?");
            width = Convert.ToDouble(Console.ReadLine());
            
            Console.WriteLine("What is the length if your feild");
            length = Convert.ToDouble(Console.ReadLine());







            Console.Write("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
