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
    public partial class ReportEventAttendence : Form
    {
        private Form prev;
        public ReportEventAttendence(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }
        private void ReportEventAttendence_Load(object sender, EventArgs e)
        {
            this.eVENTTableAdapter.Fill(this.dataSet1.EVENT);

            this.reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            prev.Show();
            this.Hide();
        }
    }
}
