using System;

namespace FenceForMelody
{
    class Program
    {
        static void Main(string[] args)
        {
            // Declares variables
            const double poundToMetre = 2.35;
            const int costPerGate = 10;
            double length, width, perimiter, cost, gateCost;
            int gates;

            // Gets the length from the user
            Console.Write("What is the length of the field in metres?: ");
            length = Convert.ToDouble(Console.ReadLine());
            Console.Clear();

            // Gets the width from the user
            Console.Write("What is the width of the field in metres?: ");
            width = Convert.ToDouble(Console.ReadLine());
            Console.Clear();

            // Gets the number of gates needed from the user
            Console.Write("How many gates does the fence need?: ");
            gates = Convert.ToInt16(Console.ReadLine());
            Console.Clear();

            // Calculates the perimiter(length) of the fence needed, its cost and the cost of the gates
            perimiter = length * 2 + width * 2;
            gateCost = gates * costPerGate;
            cost = perimiter * poundToMetre + gateCost;

            // Shows the user how long the fence will need to be and its cost
            Console.WriteLine("The fence will need to be " + perimiter + " metres");
            Console.WriteLine("It will have " + gates + " gates costing " + gateCost.ToString("C"));
            Console.WriteLine("The fence and the gates will cost " + cost.ToString("C"));

            // Ends Program
            Console.Write("\nPress any key to continue");
            Console.ReadKey();
        }
    }
}
