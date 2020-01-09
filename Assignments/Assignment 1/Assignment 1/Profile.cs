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
        static readonly string[] genders = { "Other", "Male", "Female" };

        public frmProfile(User user)
        {
            InitializeComponent();

            lblUsername.Text = user.username;
            txtBio.Text = user.bio;
            lblFirstname.Text = user.firstName;
            lblLastname.Text = user.lastName;
            lblEmail.Text = user.email;
            lblAge.Text = user.age.ToString();
            lblGender.Text = genders[user.gender];

            if (user == UserManager.user)
                btnEdit.Visible = true;
            else
                btnEdit.Visible = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Hide();
            frmProfileEdit profileEdit = new frmProfileEdit();
            profileEdit.ShowDialog();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            frmHome home = new frmHome();
            home.ShowDialog();
        }
    }
}
