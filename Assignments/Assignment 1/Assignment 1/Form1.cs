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
        string username, password;
        bool admin;

        public User (string username, string password = "", bool admin = false)
        {
            this.username = username;
            this.password = password;
            this.admin = admin;
        }
    }

    public partial class Form1 : Form
    {
        private string username, password;

        private List<User> users;

        public Form1()
        {
            InitializeComponent();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            username = txtUsername.Text;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            password = txtPassword.Text;
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

        }

        string encryptString(string str)
        {
            return str;
        }

        void readUsers()
        {

        }

        void writeUsers()
        {

        }
    }
}
