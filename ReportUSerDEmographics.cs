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
    public partial class ReportUSerDEmographics : Form
    {
        private Form prev;
        public ReportUSerDEmographics(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

     

        private void ReportUSerDEmographics_Load_1(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.DataTable2' table. You can move, or remove it, as needed.
            this.dataTable2TableAdapter1.Fill(this.dataSet1.DataTable2);

            this.reportViewer2.RefreshReport();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            prev.Show();
            this.Hide();
        }
    }
}
