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
    public partial class AdminReportsAnalysis : Form
    {
        private Form prev;
        public AdminReportsAnalysis(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            prev.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var rep = new ReportEventAttendence(this);
            rep.Show();

            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ticrep = new ticketSalesReport(this);
            ticrep.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var of = new reportOrganizerPerformance(this);
            of.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var pg = new REportPlatformGRowth(this);
            pg.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var ud = new ReportUSerDEmographics(this);
            ud.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var ev=new ReportEventRevenue(this);
            ev.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var tl = new ReportLoss(this);
            tl.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var fa = new ReportEventFeedback(this);
            fa.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var vp = new ReportVendorPerformance(this);
            vp.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var sp = new ReportSponserPerformance(this);
            sp.Show();
            this.Hide();
        }
    }
}
