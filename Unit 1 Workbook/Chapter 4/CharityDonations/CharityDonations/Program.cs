using System;
using System.IO;

namespace CharityDonations
{
    class Program
    {
        static void Main(string[] args)
        {
            // Variable declarations
            double amount;
            string name;

            // Checks if the user wants to see the dontaions
            Console.Write("\nPress Enter to see the current donations");
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                Console.Clear();
                // Opens a stream reader on the text file
                using (StreamReader sr = new StreamReader("Donations.TXT"))
                {
                    // Writes the file to the console as long as theres data
                    while (sr.Peek() != -1)
                    {
                        Console.WriteLine(sr.ReadLine());
                    }
                }
            }

            // Checks if the user wants to add a donation
            Console.Write("\nPress Enter to add a donation");
            if (Console.ReadKey().Key == ConsoleKey.Enter) {
                // Opens a stream writer for the text file
                using (StreamWriter sw = new StreamWriter("Donations.TXT", true))
                {
                    do {
                        // Gets the name of the donor from the user
                        Console.Clear();
                        Console.Write("Enter the name of the donor: ");
                        name = Console.ReadLine();
                        Console.Clear();

                        // Gets the amount of the donation from the user
                        Console.Write("How much did " + name + " donate?: ");
                        amount = Convert.ToDouble(Console.ReadLine());
                        Console.Clear();
                        
                        // Lets the user if the information is correct before writing it
                        Console.WriteLine("Name: " + name);
                        Console.WriteLine("Amount: " + amount);
                        Console.Write("\nPress Enter to confirm");
                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                        {
                            sw.WriteLine(name);
                            sw.WriteLine(amount);
                        }
                        Console.Clear();

                        // If the user wants to add another donation then repeat the loop
                        Console.Write("\nPress Enter to add another donation");
                    } while (Console.ReadKey().Key == ConsoleKey.Enter);
                }
            }

            // Ends the program
            Console.Write("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
