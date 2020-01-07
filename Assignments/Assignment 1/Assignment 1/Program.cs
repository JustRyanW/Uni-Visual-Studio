using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        public static bool ValidateUserLogin(User userLogin, List<User> users)
        {
            if (FindUser(users, userLogin.username))
                MessageBox.Show("This username is already taken");
            else if (userLogin.username.Length < 4)
                MessageBox.Show("Username must be more than 4 characters");
            else if (userLogin.password.Length < 4)
                MessageBox.Show("Password must be more than 4 characters");
            else if (userLogin.password == userLogin.username)
                MessageBox.Show("Your password must be different from your username");
            else
                return true;
            return false;
        }

        public static bool FindUser(List<User> users, string username, out User foundUser)
        {
            // Returns true if it finds a user with the same name as the userLogin reference parameter then updates it with the users data
            bool userExists = false;
            foundUser = new User(username);
            foreach (User user in users)
            {
                if (username == user.username)
                {
                    foundUser = user;
                    userExists = true;
                    break;
                }
            }
            return userExists;
        }

        public static bool FindUser(List<User> users, string username)
        {
           return FindUser(users, username, out _);
        }
             
        public static string EncryptString(string str)
        {
            // Offsets all characters in the string by +100 in the unicode space
            string encryptedString = "";
            foreach (char c in str)
            {
                encryptedString += (char)((c + 100) % 65535);
            }
            return encryptedString;
        }
    }
}
