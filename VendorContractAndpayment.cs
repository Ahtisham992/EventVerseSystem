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
    public partial class VendorContractAndpayment : Form
    {
        private Form prev;
        public VendorContractAndpayment(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        private void ContractAndpaymentVENDOR_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            prev.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var track = new VendorTrackallpayments(this);
            this.Hide();
            track.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var track = new VendorViewContracts(this);
            this.Hide();
            track.Show();
        }
    }
}
