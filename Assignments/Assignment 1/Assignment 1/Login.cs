using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_1
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();

            UserManager.ReadUsers();

            //UserManager.SortUsers(User.fieldInfo[8]);
            //UserManager.ListUserData();
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            if(UserManager.CreateUser(txtUsername.Text, txtPassword.Text))
                Login();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (UserManager.Login(txtUsername.Text, txtPassword.Text))
                Login();
        }

        void Login()
        {
            Hide();
            frmMenu menu = new frmMenu();
            menu.ShowDialog();
        }
    }
}
