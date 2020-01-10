using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace Assignment_1
{
    public class SortKey
    {
        public string name;
        public FieldInfo field;
        public bool reversed;

        public SortKey(string name, FieldInfo field, bool reversed)
        {
            this.name = name;
            this.field = field;
            this.reversed = reversed;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }
    }

    public class User
    {
        public static readonly FieldInfo[] fieldInfo = typeof(User).GetFields(BindingFlags.Public | BindingFlags.Instance);
        public static readonly string[] genders = { "Other", "Male", "Female" };

        public string username, password, bio, firstName, lastName, email;
        public bool isAdmin;
        public int id, age, gender;

        public User(int id, string username, string password)
        {
            this.id = id;
            this.username = username;
            this.password = password;
        }

        public User ()
        {
            id = 0;
        }

        public bool Validate()
        {
            // Checks the username/password agains different criteria and returns true if it passes
            if (UserManager.FindUser(username, this))
                MessageBox.Show("This username is already taken");
            else if (username.Length < 4)
                MessageBox.Show("Username must be more than 4 characters");
            else if (username.Length > 16)
                MessageBox.Show("Username cannot be more than 16 characters");
            else if (password.Length < 4)
                MessageBox.Show("Password must be more than 4 characters");
            else if (password.Length > 16)
                MessageBox.Show("Password cannot be more than 16 characters");
            else if (password == username || password == UserManager.Encrypt(username))
                MessageBox.Show("Your username and password must be different");
            else
                return true;
            return false;
        }
    
        public bool ValidateAndAssign(string username, string bio, string firstName, string lastName, string email, int age, int gender)
        {
            // Checks user data against certain criteria
            if (age < 16)
                MessageBox.Show("You must be 16 or over to use this system");
            else if (age > 130)
                MessageBox.Show("Please enter a valid age");
            else if (UserManager.FindUser(username, this))
                MessageBox.Show("This username is already taken");
            else if (username.Length < 4)
                MessageBox.Show("Username must be more than 4 characters");
            else if (password == username || password == UserManager.Encrypt(username))
                MessageBox.Show("Your password must be different from your username");
            else if (firstName.Length > 16)
                MessageBox.Show("First name must be more than 16 characters");
            else if (lastName.Length > 16)
                MessageBox.Show("Last name must be more than 16 characters");
            else
            {
                // Assigns data to the user and returns true
                this.username = username;
                this.bio = bio;
                this.firstName = firstName;
                this.lastName = lastName;
                this.email = email;
                this.age = age;
                this.gender = gender;
                return true;
            }
            // Returns false if it doesnt pass the validations
            return false;
        }
    
        // Data below here is data that is dispayed on the users list

        public string Username
        {
            get { return username; }
        }

        public int ID
        {
            get { return id; }
        }

        public string Firstname
        {
            get { return firstName; }
        }

        public string Lastname
        {
            get { return lastName; }
        }

        public string Email
        {
            get { return email; }
        }

        public int Age
        {
            get { return age; }
        }

        public string Gender
        {
            get { return genders[gender]; }
        }

        public bool Admin
        {
            get { return isAdmin; }
        }
    }

    public static class UserManager
    {
        public static List<User> users;
        public static User user;

        public static SortKey[] sortKeys = new SortKey[] {
            new SortKey("Username A-Z", typeof(User).GetField("username"), false),
            new SortKey("Username Z-A", typeof(User).GetField("username"), true),
            new SortKey("Id Ascending", typeof(User).GetField("id"), false),
            new SortKey("Id Descending", typeof(User).GetField("id"), true),
            new SortKey("Firstname A-Z", typeof(User).GetField("firstName"), false),
            new SortKey("Firstname Z-A", typeof(User).GetField("firstName"), true),
            new SortKey("Lastname A-Z", typeof(User).GetField("lastName"), false),
            new SortKey("Lastname Z-A", typeof(User).GetField("lastName"), true),
            new SortKey("Age Ascending", typeof(User).GetField("age"), false),
            new SortKey("Age Descending", typeof(User).GetField("age"), true),
            new SortKey("Admin", typeof(User).GetField("isAdmin"), false),
            new SortKey("Not Admin", typeof(User).GetField("isAdmin"), true)
        };

        private const int encryptionOffsetAmount = 100;

        public static bool FindUser(string username, out User foundUser, User user)
        {
            // Returns true if it finds a user by a certian username and outputs the user to foundUser
            bool userExists = false;
            foundUser = new User();
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

        public static bool FindUser(string username, User user)
        {
            // Overloads the FindUser fucntion and disregards the user output
            return FindUser(username, out _, user);
        }

        public static void SortUsers(SortKey sortKey)
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
                    switch (Type.GetTypeCode(sortKey.field.FieldType))
                    {
                        case TypeCode.Int32:
                            if ((int)sortKey.field.GetValue(userList[i - 1]) > (int)sortKey.field.GetValue(userList[i]))
                                sorting = SwapUsers(ref userList[i - 1], ref userList[i]);
                            break;
                        case TypeCode.Boolean:
                            if (!(bool)sortKey.field.GetValue(userList[i - 1]) && (bool)sortKey.field.GetValue(userList[i]))
                                sorting = SwapUsers(ref userList[i - 1], ref userList[i]);
                            break;
                        case TypeCode.String:
                            if (String.Compare((string)sortKey.field.GetValue(userList[i - 1]), (string)sortKey.field.GetValue(userList[i])) > 0)
                                sorting = SwapUsers(ref userList[i - 1], ref userList[i]);
                            break;
                    }
                }
            } while (sorting);

            users = userList.ToList();
            if (sortKey.reversed)
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
            // Writes all the data from each user into the Users file
            using (StreamWriter sw = new StreamWriter("Users.txt"))
            {
                foreach (User user in users)
                {
                    //  Uses "~" to indicate the start of new user data
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
                    User user = new User();
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
            User newUser = new User(users.Count, username, Encrypt(password));
            if (newUser.Validate())
            {
                users.Add(newUser);
                user = newUser;
                WriteUsers();
                return true;
            }
            return false;
        }

        public static bool Login(string username, string password)
        {
            // Logs in if the username/password is valid
            if (FindUser(username, out User userLookup, user))
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
            UserManager.ReadUsers();

            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }
    }
}
