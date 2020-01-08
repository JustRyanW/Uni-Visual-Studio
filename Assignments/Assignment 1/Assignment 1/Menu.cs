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
        User user;
        public List<User> users = new List<User>();

        bool isEditMode = false;

        public frmMenu(User user, List<User> users)
        {
            InitializeComponent();
            this.user = user;
            this.users = users;

            txtUsername.Text = user.username;
            txtBio.Text = user.bio;
            txtGender.Text = user.gender;
            txtAge.Text = user.age.ToString();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBio_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Hide();
            frmLogin login = new frmLogin();
            login.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (isEditMode)
            {
                if (User.ValidateUsername(users, txtUsername.Text))
                {

                    btnEdit.Text = "Edit";
                    isEditMode = false;
                }
            }
            else
            {
                users.Add(user);
                Program.WriteUsers(users);
                users.Remove(user);

                btnEdit.Text = "Save";
                isEditMode = true;
            }
            txtUsername.ReadOnly = !isEditMode;
            txtAge.ReadOnly = !isEditMode;
            txtGender.ReadOnly = !isEditMode;
            txtBio.ReadOnly = !isEditMode;
        }
    }
}
