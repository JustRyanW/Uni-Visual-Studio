using System;
using System.Windows.Forms;

namespace Assignment_1
{
    public partial class frmProfileEdit : Form
    {
        User user;

        public frmProfileEdit(User userToEdit)
        {
            InitializeComponent();

            user = userToEdit;
            // Setup page with user data
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
            // Checks if all the user data is valid
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
                // Save the users and go back to the profile
                UserManager.WriteUsers();

                Hide();
                frmProfile profile = new frmProfile(user);
                profile.ShowDialog();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Go to the profile page
            Hide();
            frmProfile profile = new frmProfile(user);
            profile.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool logout = UserManager.user == user;
            // Remove user and save users to the file
            UserManager.users.Remove(user);
            UserManager.WriteUsers();
            // If a logged in account deletes their own account then go to the login page
            if (logout)
            {
                UserManager.user = null;
                Hide();
                frmLogin login = new frmLogin();
                login.ShowDialog();
            } else
            {
                // Else go back to the users page
                Hide();
                frmUsers users = new frmUsers();
                users.ShowDialog();
            }
        }
    }
}
