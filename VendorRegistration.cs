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
    public partial class VendorRegistration : Form
    {
        private Form prev;
        public VendorRegistration(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        private void VendorRegistration_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            prev.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get values from TextBoxes
            string name = textBox1.Text;
            string email = textBox2.Text;
            string contactInfo = textBox4.Text;
            string businessType = textBox5.Text;
            string serviceDetails = textBox6.Text;

            // Generate a random password for the new user
            string password = Guid.NewGuid().ToString().Substring(0, 8); // Random 8-character password
            string role = "Vendor";

            // Database connection string
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Insert user into USERNEW table
                    string insertUserQuery = @"INSERT INTO [USER] (Name, Email, Password, ContactInfo, Role) 
                                       OUTPUT INSERTED.UserID 
                                       VALUES (@Name, @Email, @Password, @ContactInfo, @Role)";
                    int userId;
                    using (SqlCommand cmd = new SqlCommand(insertUserQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@ContactInfo", contactInfo);
                        cmd.Parameters.AddWithValue("@Role", role);

                        // Retrieve the newly generated UserID
                        userId = (int)cmd.ExecuteScalar();
                    }

                    // Insert vendor into VENDORNEW table
                    string insertVendorQuery = @"INSERT INTO VENDORNEW (UserID, BusinessType, ServiceDetails) 
                                         VALUES (@UserID, @BusinessType, @ServiceDetails)";
                    using (SqlCommand cmd = new SqlCommand(insertVendorQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        cmd.Parameters.AddWithValue("@BusinessType", businessType);
                        cmd.Parameters.AddWithValue("@ServiceDetails", serviceDetails);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Vendor added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear text boxes after successful operation
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    textBox6.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

    }
}
