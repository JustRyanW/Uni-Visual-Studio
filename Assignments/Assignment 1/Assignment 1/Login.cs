﻿using System;
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

        public frmLogin()
        {
            InitializeComponent();


            //test();
            ReadUsers();
            ListUserData(users);
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

            // Searches each line until it finds ~ marking a users data then writes each consecutive line to each user property then adds them to the list
            using (StreamReader sr = new StreamReader("Users.txt"))
            {
                while (sr.Peek() > -1)
                {
                    if (sr.ReadLine() == "~")
                    {
                        User user = new User("");

                        FieldInfo[] fi = typeof(User).GetFields(BindingFlags.Public | BindingFlags.Instance);
                        foreach (FieldInfo info in fi)
                        {
                            string flag = sr.ReadLine();
                            if (flag == "~")
                                break;
                            if (flag != info.Name)
                                continue;

                            string value = sr.ReadLine().ToString();
                            switch (Type.GetTypeCode(info.FieldType))
                            {
                                case TypeCode.Boolean: info.SetValue(user, Convert.ToBoolean(value)); break;
                                default: info.SetValue(user, value); break;
                            }
                        }

                        users.Add(user);
                    }
                }
            }
        }

        void WriteUsers()
        {
            // Writes all the data from each user into the Users file. Uses ~ to indicate the start of the next user data
            using (StreamWriter sw = new StreamWriter("Users.txt"))
            {
                foreach (User user in users)
                {
                    sw.WriteLine("~");
                    sw.WriteLine(user.username);
                    sw.WriteLine(user.password);
                    sw.WriteLine(user.bio);
                    sw.WriteLine(user.admin);
                }
            }
        }
         
        void ListUserData(List<User> users)
        {
            // Creates a list showing all the user data (for debug porpuses)
            string userList = "";
            foreach (User user in users)
            {
                userList += "Username: " + user.username + "\n";
                userList += "Password: " + user.password + "\n";
                userList += "Admin: " + user.admin.ToString() + "\n\n";
            }
            MessageBox.Show(userList);
        }

        void test()
        {
            User ryan = new User("ryan", "password123")
            {
                bio = "bio text",
                admin = true
            };












            FieldInfo[] fi = typeof(User).GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach(FieldInfo info in fi)
            {
                if (info.Name == "password")
                    continue;
                MessageBox.Show(info.Name + ": " + info.GetValue(ryan));

            }
        }
    }
}