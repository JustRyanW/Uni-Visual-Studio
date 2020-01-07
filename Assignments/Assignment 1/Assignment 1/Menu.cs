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

        public frmMenu(User user, List<User> users)
        {
            InitializeComponent();
            this.user = user;
            this.users = users;

            lblUsername.Text = user.username;
        }
    }
}
