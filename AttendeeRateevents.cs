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
    public partial class Rate_events_Attendee : Form
    {
        private int userid;
        private Form prev;
        public Rate_events_Attendee(int userid,Form prev)
        {
            InitializeComponent();
            this.userid = userid;
            this.prev = prev;
        }

        private void Rate_events_Attendee_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("Event ID", 100);
            listView1.Columns.Add("Event Name", 200);
            listView1.Columns.Add("Event Location", 150);
            listView1.Columns.Add("Event Start Date", 150);
            listView1.Columns.Add("Event End Date", 150);

            LoadEventDataIntoListView();
            LoadQueryDataIntoListView();  // Call this to load query data when the form loads

        }

        private void LoadQueryDataIntoListView()
        {
            // Set the column headers for listView2 (Query table)
            listView2.Columns.Clear();
            listView2.Columns.Add("Query ID", 100);
            listView2.Columns.Add("Attendee ID", 100);
            listView2.Columns.Add("Event ID", 100);
            listView2.Columns.Add("Query Description", 300);
            listView2.Columns.Add("Status", 100);
            listView2.Columns.Add("Submitted Date", 150);
            listView2.Columns.Add("Response Date", 150);
            listView2.Columns.Add("Reply", 300); // Add column for Reply

            // Define the connection string
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

            // Define the SQL query to fetch all columns from the QUERY table
            string query = "SELECT TOP (1000) [QueryID], [AttendeeID], [EventID], [QueryDesc], [Status], [Reply], [SubmittedDate], [ResponseDate] FROM [EventVerse].[dbo].[QUERY]";

            try
            {
                // Open the connection and fetch the data
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Clear any previous items in listView2
                    listView2.Items.Clear();

                    // Read the data and populate the listView
                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(reader["QueryID"].ToString());
                        item.SubItems.Add(reader["AttendeeID"].ToString());
                        item.SubItems.Add(reader["EventID"].ToString());
                        item.SubItems.Add(reader["QueryDesc"].ToString());
                        item.SubItems.Add(reader["Status"].ToString());
                        item.SubItems.Add(Convert.ToDateTime(reader["SubmittedDate"]).ToString("yyyy-MM-dd HH:mm:ss"));
                        item.SubItems.Add(reader["ResponseDate"] != DBNull.Value ? Convert.ToDateTime(reader["ResponseDate"]).ToString("yyyy-MM-dd HH:mm:ss") : "Not Available"); // Handle possible NULL values for ResponseDate
                        item.SubItems.Add(reader["Reply"].ToString()); // Add Reply column data

                        listView2.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading query data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadEventDataIntoListView()
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            string query = "SELECT EventID, Title, Location, StartDate, EndDate FROM EVENT";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    listView1.Items.Clear();

                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(reader["EventID"].ToString());
                        item.SubItems.Add(reader["Title"].ToString());
                        item.SubItems.Add(reader["Location"].ToString());
                        item.SubItems.Add(reader["StartDate"].ToString());
                        item.SubItems.Add(reader["EndDate"].ToString());

                        listView1.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading event data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            prev.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                string eventID = selectedItem.SubItems[0].Text;

                string rating = textBox1.Text;

                if (!string.IsNullOrEmpty(rating))
                {
                    string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

                    string query = "INSERT INTO FEEDBACK (AttendeeID, EventID, Rating) VALUES (@AttendeeID, @EventID, @Rating)";

                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@AttendeeID", userid);  
                            cmd.Parameters.AddWithValue("@EventID", eventID);
                            cmd.Parameters.AddWithValue("@Rating", rating);

                            conn.Open();

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Your feedback has been submitted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error submitting feedback: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please provide a rating before submitting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an event to submit feedback.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                string eventID = selectedItem.SubItems[0].Text;
                string queryDescription = textBox2.Text;

                if (!string.IsNullOrEmpty(queryDescription))
                {
                    string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

                    string insertQuery = @"
                        INSERT INTO QUERY 
                        (AttendeeID, EventID, QueryDesc, Status, SubmittedDate) 
                        VALUES 
                        (@AttendeeID, @EventID, @QueryDesc, @Status, @SubmittedDate)";

                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@AttendeeID", userid); // Attendee/User ID
                            cmd.Parameters.AddWithValue("@EventID", eventID); // Event ID
                            cmd.Parameters.AddWithValue("@QueryDesc", queryDescription); // Query Description
                            cmd.Parameters.AddWithValue("@Status", "Pending"); // Default status
                            cmd.Parameters.AddWithValue("@SubmittedDate", DateTime.Now); // Current date/time

                            conn.Open();
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Your query has been submitted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error submitting your query: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please provide a query description before submitting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select an event to submit a query.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
