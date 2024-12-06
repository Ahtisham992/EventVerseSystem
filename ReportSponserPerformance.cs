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
    public partial class ReportSponserPerformance : Form
    {
        private Form prev;
        public ReportSponserPerformance(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        private void ReportSponserPerformance_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.DataTable5' table. You can move, or remove it, as needed.
            this.dataTable5TableAdapter.Fill(this.dataSet1.DataTable5);

            this.reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            prev.Show();
            this.Hide();
        }
    }
}
