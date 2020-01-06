using System;

namespace CrispMachine
{
    class Program
    {
        static void Main()
        {
            // Variable Declarations
            int numOfCrisps = 0;
            bool exit = false;

            do
            {
                // Show menu and call a function depending on what the user has chosen
                Console.Clear();
                switch (GetMenuOption())
                {
                    case '1':
                        FillCrisps(ref numOfCrisps);
                        break;
                    case '2':
                        BuyCrisps(ref numOfCrisps);
                        break;
                    case '3':
                        InspectMachine(ref numOfCrisps);
                        break;
                    case '4':
                        Exit(ref exit);
                        break;
                }
                // Repeatedly show the menu until the user choses to exit
            } while (!exit);
        }

        static char GetMenuOption()
        {
            // Show the user a menu of all the avaliable options
            Console.WriteLine("1 - Fill machine with crisps");
            Console.WriteLine("2 - Buy crisps");
            Console.WriteLine("3 - Inspect Machine");
            Console.WriteLine("4 - Exit");
            Console.Write("Please select an option: ");
            char option = Console.ReadKey().KeyChar;
            Console.Clear();
            return option;
        }

        static void FillCrisps(ref int numOfCrisps)
        {
            // Fill the machine to its maximum of 10 crisps
            if (numOfCrisps >= 10)
                Console.WriteLine("The machine is already full");
            else
                Console.WriteLine("Crisp machine filled with 10 crisps.");
            numOfCrisps = 10;
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        static void BuyCrisps(ref int numOfCrisps)
        {
            // Dispences a packet of crisps if there is more than 0 in the machine
            if (numOfCrisps > 0)
            {
                Console.WriteLine("The machine dispences a packet of crisps");
                numOfCrisps--;
            } else
                Console.WriteLine("There aren't enough crisps in the machine");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        static void InspectMachine(ref int numOfCrisps)
        {
            // Shows the user how many crisps are in the machine
            Console.WriteLine("The machine has " + numOfCrisps + " packets of crisps");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        static void Exit(ref bool exit)
        {
            // Sets the exit bool to true if the user confirms they want to exit
            Console.WriteLine("Press 4 again if you are sure you want to exit");
            Console.WriteLine("Press any key other key to return to the menu");
            if (Console.ReadKey().KeyChar == '4')
                exit = true;
        }
    }
}
