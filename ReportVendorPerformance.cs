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
    public partial class ReportVendorPerformance : Form
    {
        private Form prev;
        public ReportVendorPerformance(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        private void ReportVendorPerformance_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.DataTable4' table. You can move, or remove it, as needed.
            this.dataTable4TableAdapter.Fill(this.dataSet1.DataTable4);

            this.reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            prev.Show();
            this.Hide();
        }
    }
}
