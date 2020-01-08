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
    public partial class frmMenu : Form
    {
        User user = UserManager.user;

        public frmMenu()
        {
            InitializeComponent();

            txtUsername.Text = user.username;
            txtBio.Text = user.bio;
            cbxGender.SelectedIndex = user.gender;
            txtAge.Text = user.age.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (user.Validate())
            {
                user.username = txtUsername.Text;
                user.age = Convert.ToInt32(txtAge.Text);
                user.gender = cbxGender.SelectedIndex;
                user.bio = txtBio.Text;

                UserManager.ListUserData();
                UserManager.WriteUsers();
            }
        }
    }
}
