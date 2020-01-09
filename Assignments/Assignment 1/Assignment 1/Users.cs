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

            // 
            cbxSort.DataSource = UserManager.sortKeys;
            cbxSort.DisplayMember = "Name";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            frmHome home = new frmHome();
            home.ShowDialog();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            User selectedUser = (User)dgvList.CurrentRow.DataBoundItem;
            frmProfile profile = new frmProfile(selectedUser, true);
            profile.ShowDialog();
        }

        private void cbxSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserManager.SortUsers((SortKey)cbxSort.SelectedItem);
            dgvList.DataSource = UserManager.users;
        }

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
