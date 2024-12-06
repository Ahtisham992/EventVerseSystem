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
    public partial class VendorAndsponsorInterface : Form
    {
        public VendorAndsponsorInterface()
        {
            InitializeComponent();
        }

        private void VendorAndsponsorInterface_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var sponsor = new VendorServicesAndSponsorshipbidding(this);
            sponsor.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var sponsor = new VendorRegistrationAndProfilemgmt(this);
            sponsor.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var sponsor = new VendorContractAndpayment(this);
            sponsor.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var n = new VendorloginBoth(this);
            this.Hide();
            n.Show();
        }
    }
}
