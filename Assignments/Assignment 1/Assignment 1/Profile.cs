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
    public partial class frmProfile : Form
    {
        User user;
        bool popout;

        public frmProfile(User userProfile, bool popout = false)
        {
            InitializeComponent();

            user = userProfile;
            this.popout = popout;
            // Setups the form with the user data
            lblUsername.Text = user.username;
            txtBio.Text = user.bio;
            lblFirstname.Text = user.firstName;
            lblLastname.Text = user.lastName;
            lblEmail.Text = user.email;
            lblAge.Text = user.age.ToString();
            lblGender.Text = User.genders[user.gender];

            // Checks if the user should be able to edit the profile
            if (user == UserManager.user || UserManager.user.isAdmin)
                btnEdit.Visible = true;
            else
                btnEdit.Visible = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Goes to the profile edit page
            Hide();
            frmProfileEdit profileEdit = new frmProfileEdit(user, popout);
            profileEdit.ShowDialog();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // If it is a popout window then go back to users
            if (popout)
            {
                Hide();
                frmUsers usersForm = new frmUsers();
                usersForm.ShowDialog();
            }
            else
            {
                // Otherwise go to home
                Hide();
                frmHome home = new frmHome();
                home.ShowDialog();
            } 
        }
    }
}
