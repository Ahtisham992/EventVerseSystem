using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public partial class AttendeeEventSearch : Form
    {
        private int userid;
        private Form prev;

        public AttendeeEventSearch(int Userid, Form prev)
        {
            InitializeComponent();
            userid = Userid;
            this.prev = prev;

        }

        private void EventSearch_Load(object sender, EventArgs e)
        {
            string query = "SELECT Name FROM CATEGORY"; // Query to get category names

            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True"))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        // Clear the ComboBox first
                        comboBox1.Items.Clear();

                        // Add the categories to the ComboBox
                        while (reader.Read())
                        {
                            comboBox1.Items.Add(reader["Name"].ToString());
                        }

                        // Optionally, you can set a default value in ComboBox
                        if (comboBox1.Items.Count > 0)
                        {
                            comboBox1.SelectedIndex = 0; // Select the first category by default
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string category = comboBox1.SelectedItem.ToString(); // Category selected in ComboBox
            DateTime eventDate = dateTimePicker1.Value; // Date input from DateTimePicker

            string query = @"
            SELECT e.EventID, e.Title, e.StartDate, e.EndDate, e.Location, c.Name AS Category
            FROM EVENT e
            JOIN CATEGORY c ON e.CategoryID = c.CategoryID
            WHERE c.Name LIKE @Category AND e.StartDate >= @EventDate";

            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True"))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Category", "%" + category + "%");
                        cmd.Parameters.AddWithValue("@EventDate", eventDate);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an event to book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int selectedEventID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

            string query = @"
            INSERT INTO REGISTRATION (EventID, AttendeeID, RegistrationDate, Status)
            VALUES (@EventID, @UserID, @RegistrationDate, @Status)";

            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True"))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters for the query
                        cmd.Parameters.AddWithValue("@EventID", selectedEventID);
                        cmd.Parameters.AddWithValue("@UserID", userid); // Use the logged-in user's ID
                        cmd.Parameters.AddWithValue("@RegistrationDate", DateTime.Now); // Current date/time
                        cmd.Parameters.AddWithValue("@Status", "Pending"); // Example status

                        // Execute the insert query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Event booking successfully. Wait for confirmation", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to book the event. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        

       
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            prev.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            // SQL query to fetch all events (no filter)
            string query = @"
    SELECT e.EventID, e.Title, e.Description, e.StartDate, e.EndDate, e.Location, c.Name AS Category
    FROM EVENT e
    JOIN CATEGORY c ON e.CategoryID = c.CategoryID";

            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True"))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Execute the query and fill the data grid with the result
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Set the DataSource of the dataGridView to the result
                        dataGridView1.DataSource = dt;
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

