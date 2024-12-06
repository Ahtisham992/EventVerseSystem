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
    public partial class OrganizerEventAnalytics : Form
    {
        private Form prev;
        public OrganizerEventAnalytics(Form prev)
        {
            InitializeComponent();
            this.prev = prev;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            prev.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var grud=new OrganizerAttendeeFeedback(this);
            this.Hide();
            grud.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var gru = new OrganizerTicketsalesmetrics(this);
            this.Hide();
            gru.Show();
        }
    }
}
