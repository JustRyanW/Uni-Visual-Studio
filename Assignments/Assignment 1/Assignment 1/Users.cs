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
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();

            // Adds the users to the list and defines the property that should be displayed
            lsbUsers.DataSource = UserManager.users;
            lsbUsers.DisplayMember = "Username";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            frmHome home = new frmHome();
            home.ShowDialog();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            User selectedUser = (User)lsbUsers.SelectedItem;
            frmProfile profile = new frmProfile(selectedUser, true);
            profile.ShowDialog();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            UserManager.SortUsers(User.fieldInfo[0], true);
            lsbUsers.DataSource = UserManager.users;
        }
    }
}
