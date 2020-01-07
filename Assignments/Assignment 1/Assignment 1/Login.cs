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

    public partial class frmLogin : Form
    {
        public List<User> users = new List<User>();

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
            if (Program.ValidateUserLogin(newUser, users))
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
                if (EncryptString(txtPassword.Text) == userLookup.password)
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
                while (sr.Peek() > -1)
                    if (sr.ReadLine() == "~")
                        users.Add(new User(sr.ReadLine(), sr.ReadLine(), Convert.ToBoolean(sr.ReadLine())));
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
    }
}
