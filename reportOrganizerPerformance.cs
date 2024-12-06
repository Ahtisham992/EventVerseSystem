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
    public partial class reportOrganizerPerformance : Form
    {
        private Form prev;
        public reportOrganizerPerformance(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }


        private void reportOrganizerPerformance_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.Organizers' table. You can move, or remove it, as needed.
            this.organizersTableAdapter2.Fill(this.dataSet1.Organizers);

            this.reportViewer3.RefreshReport();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            prev.Show();
            this.Hide();
        }
    }
}
