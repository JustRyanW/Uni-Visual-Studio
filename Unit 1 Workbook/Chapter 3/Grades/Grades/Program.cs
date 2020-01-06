using System;

namespace Grades
{
    class Program
    {
        static void Main(string[] args)
        {
            // Declares variables
            int marks = 0;
            string grade;

            // Loops 3 times to ask the user to enter their marks for each of the 3 assignments
            for (int i = 0; i < 3; i++)
            {
                Console.Write("Enter the marks for assignment " + (i + 1) + ": "); 
                marks += Convert.ToInt32(Console.ReadLine());
                Console.Clear();
            }

            // Checks if the number of marks is valid
            if (marks >= 0 && marks <= 150)
            {
                // Checks marks for what value it should equal and displays it
                grade = (marks >= 30) ? (marks >= 50) ? (marks >= 90) ? (marks >= 120) ? "A" : "B" : "C" : "D" : "E";
                Console.WriteLine("Grade: " + grade);
            }  
            else
                Console.WriteLine("Error, number of marks is invalid: " + marks);

            // Ends the program
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
