using System;
using System.Windows.Forms;

namespace Assignment_1
{
    public partial class frmLogin : Form
    {

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            // Validates the new user info
            if(UserManager.CreateUser(txtUsername.Text, txtPassword.Text))
            {
                // Go to the profile edit page to setup a new user
                Hide();
                frmProfileEdit profileEdit = new frmProfileEdit(UserManager.user);
                profileEdit.ShowDialog();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Validates the loging info
            if (UserManager.Login(txtUsername.Text, txtPassword.Text))
            {
                // Go to the home page
                Hide();
                frmHome home = new frmHome();
                home.ShowDialog();
            }
        }
    }
}
