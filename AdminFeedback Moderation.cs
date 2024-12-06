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
    public partial class Feedback_Moderation : Form
    {
        private Form prev;

        public Feedback_Moderation(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        private void Feedback_Moderation_Load(object sender, EventArgs e)
        {
            LoadFeedbackData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            prev.Show();
        }

        private void LoadFeedbackData()
        {
            // Define your connection string
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

            string query = @"
                SELECT 
                    F.FeedbackID AS FeedbackID,
                    U.Name AS AttendeeName,
                    E.Title AS EventName,
                    F.Rating AS Rate
                FROM 
                    FEEDBACK F
                JOIN 
                    [USER] U ON F.AttendeeID = U.UserID
                JOIN 
                    EVENT E ON F.EventID = E.EventID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Load data into a DataTable
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable feedbackTable = new DataTable();
                    adapter.Fill(feedbackTable);

                    // Bind the data to the DataGridView
                    dataGridView1.DataSource = feedbackTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while loading feedback data: {ex.Message}");
                }
            }
        }

        private void DeleteFeedback(int feedbackId)
        {
            // Define your connection string
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

            string deleteQuery = "DELETE FROM FEEDBACK WHERE FeedbackID = @FeedbackID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        // Add the parameter to the command
                        command.Parameters.AddWithValue("@FeedbackID", feedbackId);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Feedback deleted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("No feedback found with the selected ID.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while deleting feedback: {ex.Message}");
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Ensure a row is selected
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the FeedbackID of the selected row
                int feedbackId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["FeedbackID"].Value);

                // Delete the feedback from the database
                DeleteFeedback(feedbackId);

                // Reload the DataGridView
                LoadFeedbackData();
            }
            else
            {
                MessageBox.Show("Please select a feedback entry to delete.");
            }
        }
    }
}
