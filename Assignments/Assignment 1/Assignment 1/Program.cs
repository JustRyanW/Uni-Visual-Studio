﻿using System;
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

        public string username, password, bio, firstName, LastName, email;
        public bool isAdmin;
        public int id, age, gender;

        public User(int id)
        {
            this.id = id;
        }

        public bool Validate()
        {
            if (UserManager.FindUser(username))
                MessageBox.Show("This username is already taken");
            else if (username.Length < 4)
                MessageBox.Show("Username must be more than 4 characters");
            else if (password.Length < 4)
                MessageBox.Show("Password must be more than 4 characters");
            else if (password == username || password == UserManager.Encrypt(username))
                MessageBox.Show("Your password must be different from your username");
            else
                return true;
            return false;
        }
    }

    public static class UserManager
    {
        public static List<User> users;
        public static User user;

        private const int encryptionOffsetAmount = 100;

        public static bool FindUser(string username, out User foundUser)
        {
            // Returns true if it finds a user by a certian username and outputs the user to foundUser
            bool userExists = false;
            foundUser = new User(0);
            foreach (User viewedUser in users)
            {
                if (viewedUser != user && viewedUser.username == username)
                {
                    foundUser = viewedUser;
                    userExists = true;
                    break;
                }
            }
            return userExists;
        }

        public static bool FindUser(string username)
        {
            // Overrides the FindUser fucntion and disreards the user output
            return FindUser(username, out _);
        }

        public static void SortUsers(FieldInfo field, bool reversed = false)
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

        private static bool SwapUsers(ref User userA, ref User userB)
        {
            // Swaps 2 users positions
            User temp = userA;
            userA = userB;
            userB = temp;
            return true;
        }

        public static void WriteUsers()
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

        public static void ReadUsers()
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

        public static void ListUserData(bool showEmpty = false)
        {
            // Creates a list showing all the user data (for debug porpuses)
            string userList = "";
            foreach (User user in users)
            {
                userList += "User\n";
                FieldInfo[] fi = typeof(User).GetFields(BindingFlags.Public | BindingFlags.Instance);
                foreach (FieldInfo info in fi)
                {
                    if (showEmpty || info.GetValue(user) != String.Empty)
                        userList += info.Name + ": " + info.GetValue(user) + "\n";
                }
                userList += "\n";
            }
            MessageBox.Show(userList);
        }

        public static bool CreateUser(string username, string password)
        {
            // Creates an account if the username and password are valid
            User newUser = new User(users.Count);
            newUser.username = username;
            newUser.password = Encrypt(password);
            if (newUser.Validate())
            {
                users.Add(newUser);
                user = newUser;
                UserManager.WriteUsers();
                return true;
            }
            return false;
        }

        public static bool Login(string username, string password)
        {
            // Logs in if the username/password is valid
            if (FindUser(username, out User userLookup))
            {
                if (Encrypt(password) == userLookup.password)
                {
                    user = userLookup;
                    return true;
                }
                else
                    MessageBox.Show("Incorrect password");
            }
            else
                MessageBox.Show("No user by this name found");
            return false;
        }

        public static string Encrypt(string str)
        {
            // Offsets all characters in the string by +100 in the unicode space
            string encryptedString = "";
            foreach (char c in str)
            {
                encryptedString += (char)((c + encryptionOffsetAmount) % 65535);
            }
            return encryptedString;
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

    }
}
