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
    public partial class Startup : Form
    {
        public Startup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var startup = new Attendeestartup();
            startup.Show();

            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var startup = new OrganizerInterface();
            startup.Show();

            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var loginForm = new AttendeeLoginForm(this);
            loginForm.Show();
            this.Hide();
            //var startup = new Admin_interface();
            //startup.Show();

            //this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var startup = new VendorAndsponsorInterface();
            startup.Show();

            this.Hide();
        }

        private void Startup_Load(object sender, EventArgs e)
        {

        }
    }
}
