using System;
using System.Windows.Forms;

namespace Assignment_1
{
    public partial class frmProfileEdit : Form
    {
        User user = UserManager.user;
        public frmProfileEdit()
        {
            InitializeComponent();

            txtUsername.Text = user.username;
            txtBio.Text = user.bio;
            txtFirstname.Text = user.firstName;
            txtLastname.Text = user.lastName;
            txtEmail.Text = user.email;
            nudAge.Value = user.age;
            cbxGender.SelectedIndex = user.gender;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (user.ValidateAndAssign(
                txtUsername.Text,
                txtBio.Text,
                txtFirstname.Text,
                txtLastname.Text,
                txtEmail.Text,
                (int)nudAge.Value,
                cbxGender.SelectedIndex
                ))
            {
                UserManager.WriteUsers();

                Hide();
                frmProfile profile = new frmProfile(UserManager.user);
                profile.ShowDialog();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Hide();
            frmProfile profile = new frmProfile(UserManager.user);
            profile.ShowDialog();
        }
    }
}
