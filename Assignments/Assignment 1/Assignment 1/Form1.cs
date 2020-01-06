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

namespace Assignment_1
{
    public struct User
    {
        public string username, password;
        public bool admin;

        public User (string username, string password = "", bool admin = false)
        {
            this.username = username;
            this.password = password;
            this.admin = admin;
        }
    }

    public partial class Form1 : Form
    {
        public List<User> users = new List<User>();

        public Form1()
        {
            InitializeComponent();

            ReadUsers();
            MessageBox.Show(ListUsers());
            WriteUsers();
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

        }

        string EncryptString(string str)
        {
            return str;
        }

        void ReadUsers()
        {
            if (!File.Exists("Users.txt"))
                File.Create("Users.txt");

            using (StreamReader sr = new StreamReader("Users.txt"))
                while (sr.Peek() > -1)
                    if (sr.ReadLine() == "~")
                        users.Add(new User(sr.ReadLine(), sr.ReadLine(), Convert.ToBoolean(sr.ReadLine())));
        }

        void WriteUsers()
        {
            using (StreamWriter sw = new StreamWriter("Users.txt"))
            {
                foreach (User user in users)
                {
                    sw.WriteLine("~");
                    sw.WriteLine(user.username);
                    sw.WriteLine(user.password);
                    sw.WriteLine(user.admin);
                }
            }
        }
         
        string ListUsers()
        {
            string userList = "";
            foreach (User user in users)
            {
                userList += "Username: " + user.username + "\n";
                userList += "Password: " + user.password + "\n";
                userList += "Admin: " + user.admin.ToString() + "\n\n";
            }
            return userList;
        }
    }
}
