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
    public partial class AdminUpdateEvents : Form
    {
        private Form prev;

        public AdminUpdateEvents(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        // Load EventID combo box and populate it with data from the database
        private void AdminUpdateEvents_Load(object sender, EventArgs e)
        {
            PopulateEventIDComboBox();
        }

        // Populate EventID ComboBox with event data
        private void PopulateEventIDComboBox()
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT EventID, Title FROM EVENT";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            comboBox1.Items.Add(new { Text = reader.GetString(1), Value = reader.GetInt32(0) });
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // When an event is selected from the EventID ComboBox
        private void comboBoxEventID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                int selectedEventID = ((dynamic)comboBox1.SelectedItem).Value;
                PopulateEventDetails(selectedEventID);
            }
        }

        // Populate the event details based on the selected EventID
        private void PopulateEventDetails(int eventID)
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Query to fetch event details
                    string query = "SELECT Title, StartDate, EndDate, Location, Description FROM EVENT WHERE EventID = @EventID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EventID", eventID);

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            textBox1.Text = reader.GetString(0);
                            dateTimePicker1.Value = reader.GetDateTime(1);
                            dateTimePicker2.Value = reader.GetDateTime(2);
                            textBox4.Text = reader.GetString(3);
                            textBox2.Text = reader.GetString(6);
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            prev.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                if (string.IsNullOrEmpty(textBox1.Text) ||
                    string.IsNullOrEmpty(textBox4.Text) ||
                    dateTimePicker1.Value >= dateTimePicker2.Value ||
                    string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Please fill in all the fields correctly.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int eventID = ((dynamic)comboBox1.SelectedItem).Value;

                string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        string query = "UPDATE EVENT SET Title = @Title, StartDate = @StartDate, EndDate = @EndDate, Location = @Location, " +
                                       "Description = @Description WHERE EventID = @EventID";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@Title", textBox1.Text);
                            cmd.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value);
                            cmd.Parameters.AddWithValue("@EndDate", dateTimePicker2.Value);
                            cmd.Parameters.AddWithValue("@Location", textBox4.Text);
                            cmd.Parameters.AddWithValue("@Description", textBox2.Text);
                            cmd.Parameters.AddWithValue("@EventID", eventID);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Event updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Failed to update the event.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
