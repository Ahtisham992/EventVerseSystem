using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventVerse
{
    public partial class AttendeeeventDashboard : Form
    {
        private int Userid;
        private Form prev;
        public AttendeeeventDashboard(int Userid,Form prev)
        {
            InitializeComponent();
            this.Userid = Userid;
            this.prev = prev;

        }
        private void LoadEvents()
        {
            string query = @"
    SELECT e.Title, e.StartDate, e.EndDate, e.Location, e.EventID, e.OrganizerID, e.CategoryID, e.Description
    FROM EVENT e"; // Fetching all events

            try
            {
                // Create a new SqlConnection
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True"))
                {
                    // Create a DataAdapter to fetch data
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);

                    // Create a DataTable to hold the fetched data
                    DataTable dataTable = new DataTable();

                    // Fill the DataTable with data from the database
                    dataAdapter.Fill(dataTable);

                    // Bind the DataTable to DataGridView2
                    dataGridView2.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during data fetching
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void eventDashboard_Load(object sender, EventArgs e)
        {
            LoadEvents();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var startup = new Startup();
            startup.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var eventsearh = new AttendeeEventSearch(Userid, this);
            eventsearh.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var personal = new Attendeemanagepersonal(Userid, this);
            personal.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var eventsearh = new Rate_events_Attendee(Userid,this);
            eventsearh.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var eventsearh = new AttendeeTickets(Userid,this);
            eventsearh.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
            prev.Show();
        }
    }
    }
