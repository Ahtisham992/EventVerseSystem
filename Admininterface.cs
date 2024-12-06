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
    public partial class Admin_interface : Form
    {
        public Admin_interface()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var registrationForm = new AdminUserManagement(this);
            registrationForm.Show();

            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var registrationForm = new AdminEventManagement(this);
            registrationForm.Show();

            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var registrationForm = new AdminReportsAnalysis(this);
            registrationForm.Show();

            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var registrationForm = new Feedback_Moderation(this);
            registrationForm.Show();

            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var s = new Startup();
            s.Show();

            this.Hide();
        }
    }
}
