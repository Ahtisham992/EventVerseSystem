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
    public partial class VendorloginBoth : Form
    {
        private Form prev;
        public VendorloginBoth(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        private void VendorloginBoth_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ven = new VendorLoginvendor(this);
            this.Hide();
            ven.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var ven = new Vendorloginsponsor(this);
            this.Hide();
            ven.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            prev.Show
                ();
        }
    }
}
