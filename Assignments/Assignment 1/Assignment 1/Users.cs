using System;
using System.Windows.Forms;

namespace Assignment_1
{
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();

            dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            // Adds the users to the list and defines the property that should be displayed
            dgvList.DataSource = UserManager.users;

            // Adds the sortKeys to the drop down so a user can select them
            cbxSort.DataSource = UserManager.sortKeys;
            cbxSort.DisplayMember = "Name";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Go the the home page
            Hide();
            frmHome home = new frmHome();
            home.ShowDialog();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            // Open a profile window of the selected user
            User selectedUser = (User)dgvList.CurrentRow.DataBoundItem;
            frmProfile profile = new frmProfile(selectedUser, true);
            profile.ShowDialog();
        }

        private void cbxSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Sorts user by the selected sortKey
            UserManager.SortUsers((SortKey)cbxSort.SelectedItem);
            dgvList.DataSource = UserManager.users;
        }
    }
}
