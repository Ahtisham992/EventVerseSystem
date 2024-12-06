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
    public partial class Attendeestartup : Form
    {
        public Attendeestartup()
        {
            InitializeComponent();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var registrationForm = new AttendeeRegistrationForm(this);
            registrationForm.Show();

            // Optional: Hide or close the current Login Form
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            var loginForm = new AttendeeLoginForm(this);
            loginForm.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
