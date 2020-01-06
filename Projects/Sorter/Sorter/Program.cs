using System;

namespace Sorter
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = {50, 48, 32, 61, 79, 37, 1, 22, 19, 93};

            foreach (int number in numbers)
                Console.Write(number + " ");

            Console.WriteLine("\n");

            numbers = Sort(numbers);

            foreach (int number in numbers)
                Console.Write(number + " ");

            Console.WriteLine("\n\nPress any key to exit...");
            Console.ReadKey();
        }

        static int[] Sort(int[] numbers)
        {
            bool sorting;
            do
            {
                sorting = false;
                for (int i = 0; i < numbers.Length - 1; i++)
                {
                    int curr = numbers[i];
                    if (curr > numbers[i + 1])
                    {
                        numbers[i] = numbers[i + 1];
                        numbers[i + 1] = curr;
                        sorting = true;
                    }
                }
            } while (sorting);


            return numbers;
        }
    }
}
