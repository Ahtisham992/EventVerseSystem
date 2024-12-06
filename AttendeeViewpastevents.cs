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
    public partial class AttendeeViewpastevents : Form
    {
        private int userid;
       private Form prev;
        public AttendeeViewpastevents(int userid, Form prev)
        {
            InitializeComponent();
            this.userid = userid;
            this.prev = prev;
        }

        private void AttendeeViewpastevents_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("Event ID", 100);
            listView1.Columns.Add("Your ID", 200);
            listView1.Columns.Add("Registration Date", 150);
            listView1.Columns.Add("Status", 150);

            LoadEventDataIntoListView();
        }

        private void LoadEventDataIntoListView()
        {
            // Connection string
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

            // SQL query to fetch data from REGISTRATION table based on the user ID
            string query = "SELECT EventID, AttendeeID, RegistrationDate, Status FROM REGISTRATION WHERE AttendeeID = @UserID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userid);

                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    listView1.Items.Clear();

                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(reader["EventID"].ToString()); // Event ID
                        item.SubItems.Add(reader["AttendeeID"].ToString());               // User ID
                        item.SubItems.Add(reader["RegistrationDate"].ToString());         // Registration Date
                        item.SubItems.Add(reader["Status"].ToString());                   

                        listView1.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading event data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            prev.Show();    
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
