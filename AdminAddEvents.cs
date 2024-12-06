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
    public partial class AdminAddEvents : Form
    {
        private Form prev;

        public AdminAddEvents(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        private void AdminAddEvents_Load(object sender, EventArgs e)
        {
            // Call the function to populate ComboBoxes with data when the form loads
            PopulateComboBoxes();
        }

        private void PopulateComboBoxes()
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Populate Organizer ComboBox
                    string organizerQuery = "SELECT OrganizerID, OrganizationName FROM Organizers";
                    using (SqlCommand cmd = new SqlCommand(organizerQuery, con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            comboBox1.Items.Add(new { Text = reader.GetString(1), Value = reader.GetInt32(0) });
                        }
                        reader.Close();  // Close the first reader before executing the second one
                    }

                    // Populate Category ComboBox
                    string categoryQuery = "SELECT CategoryID, Name FROM CATEGORY";
                    using (SqlCommand cmd = new SqlCommand(categoryQuery, con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            comboBox2.Items.Add(new { Text = reader.GetString(1), Value = reader.GetInt32(0) });
                        }
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
            this.Hide   ();
            prev.Show   ();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Validation for empty fields
            if (string.IsNullOrEmpty(textBox1.Text) ||
                string.IsNullOrEmpty(textBox4.Text) ||
                comboBox1.SelectedItem == null ||
                comboBox2.SelectedItem == null ||
                dateTimePicker1.Value >= dateTimePicker2.Value ||
                string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please fill in all the fields correctly.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Extract the OrganizerID and CategoryID from the combo box selections
            int organizerId = ((dynamic)comboBox1.SelectedItem).Value;
            int categoryId = ((dynamic)comboBox2.SelectedItem).Value;

            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO EVENT (Title, StartDate, EndDate, Location, OrganizerID, CategoryID, Description) " +
                                   "VALUES (@Title, @StartDate, @EndDate, @Location, @OrganizerID, @CategoryID, @Description)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Title", textBox1.Text);
                        cmd.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@EndDate", dateTimePicker2.Value);
                        cmd.Parameters.AddWithValue("@Location", textBox4.Text);
                        cmd.Parameters.AddWithValue("@OrganizerID", organizerId);
                        cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                        cmd.Parameters.AddWithValue("@Description", textBox2.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Event added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to add the event.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
