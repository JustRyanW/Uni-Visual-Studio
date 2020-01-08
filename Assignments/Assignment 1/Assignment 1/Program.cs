using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace Assignment_1
{
    public class User
    {
        public static FieldInfo[] fieldInfo = typeof(User).GetFields(BindingFlags.Public | BindingFlags.Instance);

        public string username, password, bio, gender, firstName, LastName, email;
        public bool isAdmin;
        public int age;

        public User(string username)
        {
            this.username = username;
        }

        public bool ValidateUserLogin(List<User> users)
        {
            // Checks if the username and password fit the criteria and returns true if it does
            if (Program.FindUser(users, username))
                MessageBox.Show("This username is already taken");
            else if (username.Length < 4)
                MessageBox.Show("Username must be more than 4 characters");
            else if (password.Length < 4)
                MessageBox.Show("Password must be more than 4 characters");
            else if (password == username)
                MessageBox.Show("Your password must be different from your username");
            else
                return true;
            return false;
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
        static void Main()
        {
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }

        public static bool FindUser(List<User> users, string username, out User foundUser)
        {
            // Returns true if it finds a user by a certian username and outputs the user to foundUser
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
            // Overrides the FindUser fucntion and disreards the user output
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

        public static void SortUsers(ref List<User> users, FieldInfo field, bool reversed = false)
        {
            // Bubble Sort

            User[] userList = users.ToArray();
            bool sorting;

            // Loops over the array repeatedly unity it has finished sorting
            do
            {
                sorting = false;
                for (int i = 1; i < userList.Length; i++)
                {
                    // Sorts differently depending on the variable type of the field it is sorting by
                    switch (Type.GetTypeCode(field.FieldType))
                    {
                        case TypeCode.Int32:
                            if ((int)field.GetValue(userList[i - 1]) < (int)field.GetValue(userList[i]))
                                sorting = SwapUsers(ref userList[i - 1], ref userList[i]);
                            break;
                        case TypeCode.Boolean:
                            if (!(bool)field.GetValue(userList[i - 1]) && (bool)field.GetValue(userList[i]))
                                sorting = SwapUsers(ref userList[i - 1], ref userList[i]);
                            break;
                        case TypeCode.String:
                            if (String.Compare((string)field.GetValue(userList[i - 1]), (string)field.GetValue(userList[i])) < 0)
                                sorting = SwapUsers(ref userList[i - 1], ref userList[i]);
                            break;
                    }
                }
            } while (sorting);

            users = userList.ToList();
            if (reversed)
                users.Reverse();
        }

        public static bool SwapUsers(ref User userA, ref User userB)
        {
            // Swaps 2 users positions
            User temp = userA;
            userA = userB;
            userB = temp;
            return true;
        }
    }
}
