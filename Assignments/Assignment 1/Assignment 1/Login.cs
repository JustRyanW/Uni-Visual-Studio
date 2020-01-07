using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Assignment_1
{
    public partial class frmLogin : Form
    {
        public List<User> users = new List<User>();

        FieldInfo[] userFieldInfo = typeof(User).GetFields(BindingFlags.Public | BindingFlags.Instance);

        public frmLogin()
        {
            InitializeComponent();

            ReadUsers();
            //ListUserData(users);
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            // Creates an account and logs into that account if the username and password are valid
            User newUser = new User(txtUsername.Text, txtPassword.Text);
            if (newUser.ValidateUserLogin(users))
            {
                newUser.password = Program.EncryptString(newUser.password);
                users.Add(newUser);
                Login(newUser);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Logs in if the username/password is valid
            if (Program.FindUser(users, txtUsername.Text, out User userLookup))
            {
                if (Program.EncryptString(txtPassword.Text) == userLookup.password)
                    Login(userLookup);
                else
                    MessageBox.Show("Incorrect password");
            }
            else
                MessageBox.Show("No user by this name found");
        }

        void Login(User user)
        {
            // move to quit
            WriteUsers();

            users.Remove(user);
            Hide();
            frmMenu menu = new frmMenu(user, users);
            menu.ShowDialog();
        }

        void ReadUsers()
        {
            if (!File.Exists("Users.txt"))
                File.Create("Users.txt");

            // Uses ~ to find a new user and finds properites from the data afterwards
            using (StreamReader sr = new StreamReader("Users.txt"))
            {
                string flag = sr.ReadLine();
                while (sr.Peek() > -1)
                {
                    User user = new User("");
                    users.Add(user);

                    while (true)
                    {
                        flag = sr.ReadLine();
                        if (flag == "~" || sr.Peek() == -1)
                            break;
                        string value = sr.ReadLine().ToString();
                        foreach (FieldInfo info in userFieldInfo)
                        {
                            if (flag != info.Name)
                                continue;

                            switch (Type.GetTypeCode(info.FieldType))
                            {
                                case TypeCode.Boolean: info.SetValue(user, Convert.ToBoolean(value)); break;
                                default: info.SetValue(user, value); break;
                            }
                            break;
                        }
                    }
                }
            }
        }

        void WriteUsers()
        {
            // Writes all the data from each user into the Users file. Uses ~ to indicate the start and end of the user data
            using (StreamWriter sw = new StreamWriter("Users.txt"))
            {
                foreach (User user in users)
                {
                    sw.WriteLine("~");
                    foreach (FieldInfo info in userFieldInfo)
                    {
                        sw.WriteLine(info.Name);
                        sw.WriteLine(info.GetValue(user));
                    }
                }
            }
        }

        void ListUserData(List<User> users)
        {
            // Creates a list showing all the user data (for debug porpuses)
            string userList = "";
            foreach (User user in users)
            {
                userList += "User:\n";
                FieldInfo[] fi = typeof(User).GetFields(BindingFlags.Public | BindingFlags.Instance);
                foreach (FieldInfo info in fi)
                {
                    userList += info.Name + ": " + info.GetValue(user) + "\n";
                }
                userList += "\n";
            }
            MessageBox.Show(userList);
        }
    }
}
