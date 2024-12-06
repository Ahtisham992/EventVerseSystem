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
    public partial class OrganizerInterface : Form
    {
        public OrganizerInterface()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var loginForm = new OrganizerRegistration(this);
            loginForm.Show();
            this.Hide();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void OrganizerInterface_Load(object sender, EventArgs e)
        {

        }
    }
}
