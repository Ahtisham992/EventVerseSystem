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
    public partial class VendorDashboard : Form
    {
        public VendorDashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var n = new VendorContractAndpayment(this);
            this.Hide();
            n.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var n = new VendorServicesAndSponsorshipbidding(this);
            this.Hide();
            n.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var n = new VendorCurrentsponsorships(this);
            this.Hide();
            n.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var n = new Startup();
            this.Hide();
            n.Show();
        }
    }
}
