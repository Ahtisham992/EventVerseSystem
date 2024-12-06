using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EventVerse
{
    public partial class AttendeeLoginForm : Form
    {
        private Form prev;
        public AttendeeLoginForm(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    // Modify the query to select the Role of the user, including handling NULL Role values
                    string query = "SELECT UserID, Name, Role FROM [USER] WHERE Email=@Email AND Password=@Password";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Email", textBox1.Text);
                        cmd.Parameters.AddWithValue("@Password", textBox2.Text);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Fetch UserID and Name
                                int userId = reader.GetInt32(0);
                                string userName = reader.GetString(1);

                                // Handle NULL Role values
                                string userRole = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);  // If Role is NULL, set it to an empty string

                                MessageBox.Show($"Welcome, {userName}!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Check if the Role is 'admin' or empty (for users with no assigned role)
                                if (userRole.Equals("admin", StringComparison.OrdinalIgnoreCase))
                                {
                                    // Navigate to the Admin Interface
                                    var adminInterface = new Admin_interface();  // Assuming Admin_interface is the admin form
                                    adminInterface.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    // Navigate to the Attendee Dashboard (Normal user)
                                    var dashboard = new AttendeeeventDashboard(userId, this);
                                    dashboard.Show();
                                    this.Hide();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Invalid email or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var registrationForm = new AttendeeRegistrationForm(this);
            registrationForm.Show();

            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            prev.Show();
        }

        private void AttendeeLoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
