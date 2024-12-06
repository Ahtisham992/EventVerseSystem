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
    public partial class VendorRegistersponsor : Form
    {
        private Form prev;

        public VendorRegistersponsor(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        private void VendorRegistersponsor_Load(object sender, EventArgs e)
        {
            // Populate ComboBox with valid users
            PopulateUserComboBox();
        }

        private void PopulateUserComboBox()
        {
            // Database connection string
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Query to fetch users whose role is not 'Admin' or 'Vendor'
                    string query = "SELECT UserID, Name FROM [USER] WHERE Role NOT IN ('Admin', 'Vendor')";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Add user to ComboBox
                                int userId = reader.GetInt32(0);
                                string username = reader.GetString(1);
                                comboBox1.Items.Add(new ComboBoxItem { UserId = userId, Username = username });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            prev.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (comboBox1.SelectedItem == null || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Please select a user and fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Retrieve selected user
            ComboBoxItem selectedUser = (ComboBoxItem)comboBox1.SelectedItem;
            int userId = selectedUser.UserId;
            string companyName = textBox2.Text;
            string sponsorshipType = textBox3.Text;

            // Database connection string
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Insert into SPONSOR table
                    string insertSponsorQuery = @"
                INSERT INTO SPONSORNEW (UserID, CompanyName, SponsorshipType)
                VALUES (@UserID, @CompanyName, @SponsorshipType)";

                    using (SqlCommand cmd = new SqlCommand(insertSponsorQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        cmd.Parameters.AddWithValue("@CompanyName", companyName);
                        cmd.Parameters.AddWithValue("@SponsorshipType", sponsorshipType);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Sponsor registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            comboBox1.SelectedIndex = -1;
                            textBox2.Clear();
                            textBox3.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Failed to register sponsor. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper class for ComboBox items
        private class ComboBoxItem
        {
            public int UserId { get; set; }
            public string Username { get; set; }

            public override string ToString()
            {
                return Username; // Display username in ComboBox
            }
        }
       
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //userid
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //company name
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //sponsorshiptype
        }


    }
}
