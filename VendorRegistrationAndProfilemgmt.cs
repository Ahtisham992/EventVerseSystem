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
    public partial class VendorRegistrationAndProfilemgmt : Form
    {
        private Form prev;
        public VendorRegistrationAndProfilemgmt(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        private void RegistrationAndProfilemgmtVENDOR_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            prev.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var reg = new VendorRegistration(this);
            this.Hide();
            reg.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var reg = new VendorRegistersponsor(this);
            this.Hide();
            reg.Show();
        }
    }
}
