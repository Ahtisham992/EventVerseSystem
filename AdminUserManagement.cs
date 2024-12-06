using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventVerse
{
    public partial class AdminUserManagement : Form
    {
        private Form prev;
        public AdminUserManagement(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        private void UserManagementAdmin_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            prev.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var form = new AdminManageUsers(this);
            this.Hide();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new AdminManageOrganizers(this);
            this.Hide();
            form.Show();
        }
    }
}
