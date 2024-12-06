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
    public partial class REportPlatformGRowth : Form
    {
        private Form prev;
        public REportPlatformGRowth(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

      
        private void REportPlatformGRowth_Load_1(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.CATEGORY1' table. You can move, or remove it, as needed.
            this.cATEGORY1TableAdapter.Fill(this.dataSet1.CATEGORY1);

            this.reportViewer2.RefreshReport();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            prev.Show();
            this.Hide();
        }
    }
}
