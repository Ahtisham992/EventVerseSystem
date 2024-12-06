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
    public partial class Vendorloginsponsor : Form
    {
        private Form prev;
        public Vendorloginsponsor(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            prev.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //userid
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //companyname
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //sponsorshiptype
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int userId;
            if (!int.TryParse(textBox1.Text, out userId)) // Check if UserID is numeric
            {
                MessageBox.Show("Please enter a valid User ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string companyName = textBox2.Text.Trim();
            string sponsorshipType = textBox3.Text.Trim();

            // Database connection string
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if UserID exists in USERNEW table
                    string checkUserQuery = "SELECT COUNT(*) FROM [USER] WHERE UserID = @UserID";
                    using (SqlCommand checkUserCmd = new SqlCommand(checkUserQuery, conn))
                    {
                        checkUserCmd.Parameters.AddWithValue("@UserID", userId);
                        int userExists = (int)checkUserCmd.ExecuteScalar();

                        if (userExists == 0)
                        {
                            MessageBox.Show("User ID not found. Please check your User ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Check if UserID exists in SPONSOR table with matching CompanyName and SponsorshipType
                    string checkSponsorQuery = @"SELECT COUNT(*) FROM SPONSORNEW
                                         WHERE UserID = @UserID AND CompanyName = @CompanyName AND SponsorshipType = @SponsorshipType";
                    using (SqlCommand checkSponsorCmd = new SqlCommand(checkSponsorQuery, conn))
                    {
                        checkSponsorCmd.Parameters.AddWithValue("@UserID", userId);
                        checkSponsorCmd.Parameters.AddWithValue("@CompanyName", companyName);
                        checkSponsorCmd.Parameters.AddWithValue("@SponsorshipType", sponsorshipType);

                        int sponsorExists = (int)checkSponsorCmd.ExecuteScalar();

                        if (sponsorExists == 0)
                        {
                            MessageBox.Show("Sponsor details not found. Please check your information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // All checks passed, redirect to vendor dashboard
                    MessageBox.Show("Login successful! Redirecting...", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Redirect to Vendor Dashboard or the next form
                    VendorDashboard vendorDashboard = new VendorDashboard(); // Replace with your next form
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
