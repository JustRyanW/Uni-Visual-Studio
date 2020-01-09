﻿using System;
using System.Windows.Forms;

namespace Assignment_1
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        private void brnProfile_Click(object sender, EventArgs e)
        {
            Hide();
            frmProfile profile = new frmProfile(UserManager.user);
            profile.ShowDialog();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            Hide();
            frmUsers usersForm = new frmUsers();
            usersForm.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            UserManager.user = null;
            Hide();
            frmLogin login = new frmLogin();
            login.ShowDialog();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
