using System;

namespace TipTheWaiter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Declaes variables
            const double tipPercent = 0.15, minnimumTip = 1;
            double bill, tip;

            // Gets the bill from the user
            Console.Write("How much was the bill: ");
            bill = Convert.ToDouble(Console.ReadLine());

            // Calculates the tip and enforces a minnimum tip
            tip = bill * tipPercent;
            tip = (tip > minnimumTip) ? tip : minnimumTip;

            // Displays the info to the user
            Console.WriteLine("The bill is " + bill.ToString("C"));
            Console.WriteLine("The tip should be " + tip.ToString("C"));

            // Ends the program
            Console.Write("\nPress any key to continue.");
            Console.ReadKey();
        }
    }
}
