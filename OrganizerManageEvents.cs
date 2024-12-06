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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EventVerse
{
    public partial class OrganizerManageEvents : Form
    {
     
            private Form prev;
            private int eventID;  // Store the EventID

            // Constructor accepting EventID and previous form
            public OrganizerManageEvents(Form prev, int eventID)
            {
                InitializeComponent();
                this.prev = prev;
                this.eventID = eventID; // Store the event ID
            }

            // Load event data from the database when the form is loaded
            private void OrganizerManageEvents_Load(object sender, EventArgs e)
            {
                LoadEventData(eventID);
            }

            // Method to load event data from the database
            private void LoadEventData(int eventID)
            {
                string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
                string query = "SELECT * FROM EVENT WHERE EventID = @EventID";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@EventID", eventID);

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            textBox1.Text = reader["Title"].ToString();
                            textBox2.Text = reader["Description"].ToString();
                            textBox4.Text = reader["Location"].ToString();
                            dateTimePicker1.Value = Convert.ToDateTime(reader["StartDate"]);
                            dateTimePicker2.Value = Convert.ToDateTime(reader["EndDate"]);
                            textBox5.Text = reader["EventID"].ToString();  // EventID field for deletion
                        }
                        else
                        {
                            MessageBox.Show("Event not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while loading event data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            // Button click to go back to the previous form
            private void button3_Click(object sender, EventArgs e)
            {
                this.Hide();
                prev.Show();
            }

            // Button click to update the event in the database
           
                          

            // Method to update the event in the database
            private void UpdateEventInDatabase(string eventID, string eventTitle, string eventDescription, string eventLocation, DateTime eventStartDate, DateTime eventEndDate)
            {
                string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
                string query = "UPDATE EVENT SET Title = @EventTitle, StartDate = @StartDate, EndDate = @EndDate, Location = @Location, Description = @Description WHERE EventID = @EventID";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@EventID", eventID);
                        cmd.Parameters.AddWithValue("@EventTitle", eventTitle);
                        cmd.Parameters.AddWithValue("@StartDate", eventStartDate);
                        cmd.Parameters.AddWithValue("@EndDate", eventEndDate);
                        cmd.Parameters.AddWithValue("@Location", eventLocation);
                        cmd.Parameters.AddWithValue("@Description", eventDescription);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Event updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Event not found or no changes made.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while updating the event: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            // Method to delete the event from the database
            private void DeleteEventFromDatabase(string eventID)
            {
                string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
                string query = "DELETE FROM EVENT WHERE EventID = @EventID";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@EventID", eventID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Event deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Event not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while deleting the event: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
      

        private void label3_Click(object sender, EventArgs e)
        {

        }

       
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            string eventID = textBox5.Text;
            string eventTitle = textBox1.Text;
            string eventDescription = textBox2.Text;
            string eventLocation = textBox4.Text;
            DateTime eventStartDate = dateTimePicker1.Value;
            DateTime eventEndDate = dateTimePicker2.Value;

            // Validate input
            if (string.IsNullOrEmpty(eventID) || string.IsNullOrEmpty(eventTitle) || string.IsNullOrEmpty(eventDescription) || string.IsNullOrEmpty(eventLocation))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Update the event in the database
            UpdateEventInDatabase(eventID, eventTitle, eventDescription, eventLocation, eventStartDate, eventEndDate);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string eventID = textBox5.Text;

            if (string.IsNullOrEmpty(eventID))
            {
                MessageBox.Show("Please enter the EventID to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirm deletion
            var result = MessageBox.Show("Are you sure you want to delete this event?", "Delete Event", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Delete the event from the database
                DeleteEventFromDatabase(eventID);
            }
        }
    }
}
    
