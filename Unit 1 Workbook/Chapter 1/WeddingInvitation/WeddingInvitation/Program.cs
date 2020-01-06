using System;

namespace WeddingInvitation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Declares name variables
            string guestName;
            string brideName;
            string groomName;

            // Gets the names from the user
            Console.Write("Please type the name of the guest: ");
            guestName = Console.ReadLine();
            Console.Clear();
            Console.Write("Please type the name of the bride: ");
            brideName = Console.ReadLine();
            Console.Clear();
            Console.Write("Please type the name of the groom: ");
            groomName = Console.ReadLine();
            Console.Clear();

            // Makes the invitation background
            Console.WriteLine("****************************************************");
            for (int i = 0; i < 9; i++)
                Console.WriteLine();
            Console.WriteLine("****************************************************");

            // Centers and writes the guest name
            Console.SetCursorPosition((guestName.Length <= 52) ? 26 - guestName.Length / 2 : 0, 2);
            Console.Write(guestName);

            // Centers and writes the invite text
            const string inviteText = "is invited to the wedding of:";
            Console.SetCursorPosition((inviteText.Length <= 52) ? 26 - inviteText.Length / 2 : 0, 4);
            Console.Write(inviteText);

            // Centers and writes the bride and grooms names
            string brideGroomText = brideName + " and " + groomName;
            Console.SetCursorPosition((brideGroomText.Length <= 52) ? 26 - brideGroomText.Length / 2 : 0, 6);
            Console.Write(brideGroomText);

            // Centers and writes the date
            const string dateText = "on Saturday 17th July at 2:00pm";
            Console.SetCursorPosition((dateText.Length <= 52) ? 26 - dateText.Length / 2 : 0, 8);
            Console.Write(dateText);

            // Exits the program once the user presses a key
            Console.SetCursorPosition(0, 11);
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
