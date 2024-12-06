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
    public partial class AttendeeManagePersonalInfo : Form
    {
        private Form prev;
        private int userid;

        public AttendeeManagePersonalInfo(int userid, Form prev)
        {
            InitializeComponent();
            this.prev = prev;
            this.userid = userid;
        }

        private void AttendeeManagePersonalInfo_Load(object sender, EventArgs e)
        {
            // Connection string
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

            // Query to fetch user data
            string query = "SELECT Name, Email, Password, ContactInfo FROM [USER] WHERE UserID = @UserID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userid);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Populate text boxes with user data
                            textBox1.Text = reader["Name"]?.ToString() ?? string.Empty;
                            textBox2.Text = reader["Email"]?.ToString() ?? string.Empty;
                            textBox3.Text = reader["Password"]?.ToString() ?? string.Empty;
                            textBox4.Text = reader["ContactInfo"]?.ToString() ?? string.Empty;
                        }
                        else
                        {
                            MessageBox.Show("No user data found. Please check the user ID.", "Data Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching user data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Navigate back to the previous form
            this.Close();
            prev.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Connection string
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

            // Collect updates from textboxes
            Dictionary<string, string> updates = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(textBox1.Text))
                updates.Add("Name", textBox1.Text);

            if (!string.IsNullOrWhiteSpace(textBox2.Text))
                updates.Add("Email", textBox2.Text);

            if (!string.IsNullOrWhiteSpace(textBox3.Text))
                updates.Add("Password", textBox3.Text);

            if (!string.IsNullOrWhiteSpace(textBox4.Text))
                updates.Add("ContactInfo", textBox4.Text);

            // Check if there are any updates
            if (updates.Count == 0)
            {
                MessageBox.Show("No fields to update. Please fill in at least one field.", "No Changes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Build update query dynamically
            StringBuilder queryBuilder = new StringBuilder("UPDATE [USER] SET ");
            foreach (var update in updates)
            {
                queryBuilder.Append($"{update.Key} = @{update.Key}, ");
            }

            // Remove the trailing comma and space
            queryBuilder.Length -= 2;

            queryBuilder.Append(" WHERE UserID = @UserID");

            // Execute the query
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn))
                {
                    // Add parameters
                    foreach (var update in updates)
                    {
                        cmd.Parameters.AddWithValue($"@{update.Key}", update.Value);
                    }
                    cmd.Parameters.AddWithValue("@UserID", userid);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Your personal information has been updated successfully.", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No changes were made to your information.", "No Changes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating your information: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}