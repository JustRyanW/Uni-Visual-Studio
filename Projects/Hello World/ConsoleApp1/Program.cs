using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            String birthDay;

            Console.WriteLine("What is your birthday: ");

            birthDay = Console.ReadLine();

            Console.WriteLine("Happy Birthday " + birthDay);

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
