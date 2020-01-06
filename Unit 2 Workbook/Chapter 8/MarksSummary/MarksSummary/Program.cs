using System;

namespace MarksSummary
{
    class Program
    {
        static void Main(string[] args)
        {
            // 2D array of student marks
            int[,] iMarks = new int[4, 3]
            {
                {3, 4, 2},
                {6, 4, 8},
                {5, 7, 3},
                {4, 8, 6}
            };

            AverageStudentMark(iMarks);
            AverageTestMark(iMarks);

            Console.WriteLine("\nPress any key to exit");
            Console.ReadKey();
        }

        static void AverageStudentMark(int[,] iMarks)
        {
            // Loops through each student(row)
            for (int iRow = 0; iRow < iMarks.GetLength(0); iRow++)
            {
                // Loops through each test(column) and adds this students mark to the total marks
                int total = 0;
                for (int iCol = 0; iCol < iMarks.GetLength(1); iCol++)
                    total += iMarks[iRow, iCol];

                // Calculates the average mark of this student by dividing the total marks by the amount of tests
                double average = (double)total / iMarks.GetLength(1);
                Console.WriteLine("Student " + iRow + " has an average mark of " + average);
            }
        }

        static void AverageTestMark(int[,] iMarks)
        {
            // Loops through each test(column)
            for (int iCol = 0; iCol < iMarks.GetLength(1); iCol++)
            {
                // Loops through each student(row) and adds that students mark to the total marks
                int total = 0;
                for (int iRow = 0; iRow < iMarks.GetLength(0); iRow++)
                    total += iMarks[iRow, iCol];

                // Calculates the average mark of this test by dividing the total marks by the amount of students
                double average = (double)total / iMarks.GetLength(0);
                Console.WriteLine("Test " + iCol + " has an average mark of " + average);
            }
        }
    }
}
