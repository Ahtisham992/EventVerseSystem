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
    public partial class ReportEventFeedback : Form
    {
        private Form prev;
        public ReportEventFeedback(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }
        

        private void ReportEventFeedback_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.DataTable1' table. You can move, or remove it, as needed.
            this.dataTable1TableAdapter.Fill(this.dataSet1.DataTable1);

            this.reportViewer3.RefreshReport();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            prev.Show();
            this.Hide();
        }
    }
}
