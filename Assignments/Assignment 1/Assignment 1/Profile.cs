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

        User user = UserManager.user;

        public frmProfile()
        {
            InitializeComponent();

            lblUsername.Text = user.username;
            txtBio.Text = user.bio;
            lblFirstname.Text = user.firstName;
            lblLastname.Text = user.lastName;
            lblEmail.Text = user.email;
            lblAge.Text = user.age.ToString();
            lblGender.Text = genders[user.gender];
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Hide();
            frmProfileEdit profileEdit = new frmProfileEdit();
            profileEdit.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            UserManager.user = null;
            Hide();
            frmLogin login = new frmLogin();
            login.ShowDialog();
        }
    }
}
