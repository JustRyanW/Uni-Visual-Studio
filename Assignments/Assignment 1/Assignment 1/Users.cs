using System;
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

            // 
            cbxSort.DataSource = UserManager.sortKeys;
            cbxSort.DisplayMember = "Name";

            dgvList.DataSource = UserManager.users;
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

        private void cbxSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserManager.SortUsers((SortKey)cbxSort.SelectedItem);
            lsbUsers.DataSource = UserManager.users;
        }
    }
}
