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
    public partial class AdminEventManagement : Form
    {

        private Form prev;

        public AdminEventManagement(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        // Event handler for form load
        private void EventManagementAdmin_Load(object sender, EventArgs e)
        {
            LoadEventData();
        }

        // Method to load event data into the DataGridView
        private void LoadEventData()
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT EventID, Title, StartDate, EndDate, Location, OrganizerID, CategoryID FROM EVENT";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler to navigate back to the previous form
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            prev.Show();
        }

        // Event handler for adding a new event
        private void button3_Click(object sender, EventArgs e)
        {
            var add = new AdminAddEvents(this);
            this.Hide();
            add.Show();
        }

        // Event handler for updating an event
        private void button1_Click(object sender, EventArgs e)
        {
            var add = new AdminUpdateEvents(this);
            this.Hide();
            add.Show();
        }

        // Event handler for updating event categories
        private void button2_Click(object sender, EventArgs e)
        {
            var add = new AdminUpdateEventCategories(this);
            this.Hide();
            add.Show();
        }


        // Method to delete an event from the database
        private void DeleteEvent(int eventID)
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // First, delete the related feedback records
                    string deleteFeedbackQuery = "DELETE FROM FEEDBACK WHERE EventID = @EventID";

                    using (SqlCommand cmdFeedback = new SqlCommand(deleteFeedbackQuery, con))
                    {
                        cmdFeedback.Parameters.AddWithValue("@EventID", eventID);
                        cmdFeedback.ExecuteNonQuery();
                    }

                    // Now, delete the event itself
                    string deleteEventQuery = "DELETE FROM EVENT WHERE EventID = @EventID";

                    using (SqlCommand cmdEvent = new SqlCommand(deleteEventQuery, con))
                    {
                        cmdEvent.Parameters.AddWithValue("@EventID", eventID);
                        int rowsAffected = cmdEvent.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Event deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadEventData(); // Refresh the DataGridView
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete the event.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler for DataGridView cell content click (optional, if needed for more actions)
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // You can add more logic here if needed
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Check if a row is selected in the DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the EventID of the selected row
                int eventID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["EventID"].Value);

                // Ask for confirmation before deleting
                var confirmation = MessageBox.Show("Are you sure you want to delete this event?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmation == DialogResult.Yes)
                {
                    DeleteEvent(eventID);
                }
            }
            else
            {
                MessageBox.Show("Please select an event to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
