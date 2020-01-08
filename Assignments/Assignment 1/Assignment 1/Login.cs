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

        public frmLogin()
        {
            InitializeComponent();


            Program.ReadUsers(out users);
            //Program.SortUsers(ref users, User.fieldInfo[8]);
            //ListUserData(users);
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            // Creates an account and logs into that account if the username and password are valid
            if (User.ValidateUserLogin(users, txtUsername.Text, txtPassword.Text))
            {
                User newUser = new User(users.Count);
                newUser.username = txtUsername.Text;
                newUser.password = Program.EncryptString(txtPassword.Text);
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
            Program.WriteUsers(users);

            users.Remove(user);
            Hide();
            frmMenu menu = new frmMenu(user, users);
            menu.ShowDialog();
        }

        void ListUserData(List<User> users, bool showEmpty = false)
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
    }
}
