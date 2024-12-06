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
    public partial class Attendeemanagepersonal : Form
    {
        private Form prev;
        private int userid;
        public Attendeemanagepersonal(int userid,Form prev)
        {
            InitializeComponent();
            this.userid = userid;
            this.prev = prev;
        }

        private void PersonalManagementAttendee_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            prev.Show();    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var manage = new AttendeeManagePersonalInfo(userid,this);
            this.Hide();
            manage.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var manage = new AttendeeViewpastevents(userid, this);
            this.Hide();
            manage.Show();
        }
    }
}
