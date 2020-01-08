using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace Assignment_1
{
    public class User
    {
        public static FieldInfo[] fieldInfo = typeof(User).GetFields(BindingFlags.Public | BindingFlags.Instance);

        public string username, password, bio, gender, firstName, LastName, email;
        public bool isAdmin;
        public int id, age;

        public User(int id)
        {
            this.id = id;
        }

        public static bool ValidateUserLogin(List<User> users, string username, string password)
        {
            // Checks if the username and password fit the criteria and returns true if it does
            if (ValidateUsername(users, username) && ValidatePassword(password))
                if (password == username)
                    MessageBox.Show("Your password must be different from your username");
                else
                    return true;
            return false;
        }

        public static bool ValidateUsername(List<User> users, string username)
        {
            if (Program.FindUser(users, username))
                MessageBox.Show("This username is already taken");
            else if (username.Length < 4)
                MessageBox.Show("Username must be more than 4 characters");
            else
                return true;
            return false;
        }

        public static bool ValidatePassword(string password)
        {
            if (password.Length < 4)
                MessageBox.Show("Password must be more than 4 characters");
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

        private const int encryptionOffsetAmount = 100;

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
            foundUser = new User(0);
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
                encryptedString += (char)((c + encryptionOffsetAmount) % 65535);
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

        public static void WriteUsers(List<User> users)
        {
            // Writes all the data from each user into the Users file. Uses "~" to indicate the start user data
            using (StreamWriter sw = new StreamWriter("Users.txt"))
            {
                foreach (User user in users)
                {
                    sw.WriteLine("~");
                    // Loops over each data field in the user class
                    foreach (FieldInfo info in User.fieldInfo)
                    {
                        // Writes the field name followed by its value
                        sw.WriteLine(info.Name);
                        sw.WriteLine(info.GetValue(user));
                    }
                }
            }
        }

        public static void ReadUsers(out List<User> users)
        {
            // Creates a file if one doesnt exist
            if (!File.Exists("Users.txt"))
                File.Create("Users.txt");


            using (StreamReader sr = new StreamReader("Users.txt"))
            {
                users = new List<User>();

                string flag = sr.ReadLine();
                while (sr.Peek() > -1)
                {
                    // Creates user, adds them to the users list and starts reading for user data
                    User user = new User(0);
                    users.Add(user);

                    while (true)
                    {
                        // Reads for data which either defines a data field or a new user
                        flag = sr.ReadLine();
                        // If it finds a "~" then break the loop and create a new user
                        if (flag == "~" || sr.Peek() == -1)
                            break;

                        // Loops over each field in the User class to find one matching the flag
                        string value = sr.ReadLine().ToString();
                        foreach (FieldInfo info in User.fieldInfo)
                        {
                            if (flag != info.Name)
                                continue;

                            // Converts read data to the right data type
                            switch (Type.GetTypeCode(info.FieldType))
                            {
                                case TypeCode.Int32: info.SetValue(user, Convert.ToInt32(value)); break;
                                case TypeCode.Boolean: info.SetValue(user, Convert.ToBoolean(value)); break;
                                default: info.SetValue(user, value); break;
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}
