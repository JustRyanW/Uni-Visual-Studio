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
            if(UserManager.CreateUser(txtUsername.Text, txtPassword.Text))
            {
                Hide();
                frmProfileEdit profileEdit = new frmProfileEdit(UserManager.user);
                profileEdit.ShowDialog();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (UserManager.Login(txtUsername.Text, txtPassword.Text))
            {
                Hide();
                frmHome home = new frmHome();
                home.ShowDialog();
            }
        }
    }
}
