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
    public partial class VendorLoginvendor : Form
    {
        private Form prev;
        public VendorLoginvendor(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            prev.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //email user check
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //business type vendor check
        }

        private void VendorLoginvendor_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate email input
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter your email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Please enter your business type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string email = textBox2.Text.Trim();
            string businessType = textBox5.Text.Trim();
            int userId = -1;  // Placeholder for UserID

            // Database connection string
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if email exists in USERNEW table
                    string checkEmailQuery = "SELECT UserID FROM [USER] WHERE Email = @Email";
                    using (SqlCommand checkEmailCmd = new SqlCommand(checkEmailQuery, conn))
                    {
                        checkEmailCmd.Parameters.AddWithValue("@Email", email);
                        object result = checkEmailCmd.ExecuteScalar();

                        if (result == null)
                        {
                            MessageBox.Show("Email not found. Please check your email or register first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Fetch UserID
                        userId = Convert.ToInt32(result);
                    }

                    // Check if UserID exists in VENDORNEW table with the correct BusinessType
                    string checkVendorQuery = "SELECT COUNT(*) FROM VENDORNEW WHERE UserID = @UserID AND BusinessType = @BusinessType";
                    using (SqlCommand checkVendorCmd = new SqlCommand(checkVendorQuery, conn))
                    {
                        checkVendorCmd.Parameters.AddWithValue("@UserID", userId);
                        checkVendorCmd.Parameters.AddWithValue("@BusinessType", businessType);

                        int vendorExists = (int)checkVendorCmd.ExecuteScalar();

                        if (vendorExists == 0)
                        {
                            MessageBox.Show("Vendor not found or Business Type does not match. Please verify your details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // All checks passed, redirect to the next form

                    MessageBox.Show("Login successful! Redirecting...", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    VendorDashboard vendorDashboard = new VendorDashboard();  // Replace with your next form
                    this.Hide();
                    vendorDashboard.Show();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
