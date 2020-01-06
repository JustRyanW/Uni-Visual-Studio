using System;
using System.IO;

namespace Mountains
{
    class Program
    {
        static void Main(string[] args)
        {
            const int iNUMBER = 10;
            int[] iMountains = new int[iNUMBER];

            ReadHeights(ref iMountains);

            DisplayHeights(iMountains);

            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static void ReadHeights(ref int[] iMountains)
        {
            // Reads the mountain heights from the file and puts them into the list
            using (StreamReader sr = new StreamReader("heights.txt"))
            {
                for (int i = 0; i < iMountains.Length; i++)
                {
                    if (sr.Peek() == -1)
                        break;
                    iMountains[i] = Convert.ToInt32(sr.ReadLine());
                }
            }
        }

        static void DisplayHeights(int[] iMountains)
        {
            // Writes each mountain from the list to the console
            Console.WriteLine("Heights of the 10 highest mountains:\n");
            for (int i = 0; i < iMountains.Length; i++)
            {
                Console.WriteLine("Mountains: " + (i + 1) + "   " + iMountains[i].ToString("#,###") + " Kms");
            }
        }
    }
}
