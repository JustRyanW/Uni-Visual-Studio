using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Declare constants and variables
            const int kitkatCalories = 250;
            int kitkatsAWeek, kitkatsAYear, caloriesAYear;

            // Get number of kitkats the user eats a week
            Console.Write("How many kitkats have you eaten this week: ");
            kitkatsAWeek = Convert.ToInt16(Console.ReadLine());

            // Calculate number of kitkats a year and calories per year
            kitkatsAYear = kitkatsAWeek * 52;
            caloriesAYear = kitkatsAYear * kitkatCalories;

            // Write how many kitkats the user eats a year and how many calories they intake a year.
            Console.WriteLine("You will eat " + kitkatsAYear + " kitkats per year and intake " + caloriesAYear + " calories a year.");

            // End program
            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }
    }
}
