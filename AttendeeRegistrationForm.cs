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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EventVerse
{
    public partial class AttendeeRegistrationForm : Form
    {

        private Form previous;

        public AttendeeRegistrationForm(Form prev)
        {
            InitializeComponent();
            previous = prev;
        }

        private void AttendeeRegistrationForm_Load(object sender, EventArgs e)
        {
            // Populate the Gender ComboBox
            comboBox1.Items.Add("Male");
            comboBox1.Items.Add("Female");
            comboBox1.SelectedIndex = 0; // Default selection
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Database connection string
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

            // Ensure all fields are filled
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                comboBox1.SelectedIndex == -1 ||
                dateTimePicker1.Value == null)
            {
                MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Insert user with "Attendee" role
                    string query = @"INSERT INTO [USER] (Name, Email, Password, ContactInfo, Role, Gender, DateOfBirth) 
                                     VALUES (@Name, @Email, @Password, @ContactInfo, 'Attendee', @Gender, @DateOfBirth)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add values to parameters
                        cmd.Parameters.AddWithValue("@Name", textBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", textBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", textBox3.Text.Trim());
                        cmd.Parameters.AddWithValue("@ContactInfo", textBox4.Text.Trim());
                        cmd.Parameters.AddWithValue("@Gender", comboBox1.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@DateOfBirth", dateTimePicker1.Value.Date);

                        // Execute query
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearFields();
                        }
                        else
                        {
                            MessageBox.Show("Registration failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            comboBox1.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Today;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to go back to the login page?",
                                         "Confirm Navigation",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var loginForm = new AttendeeLoginForm(this);
                loginForm.Show();
                this.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            previous.Show();
        }




        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

