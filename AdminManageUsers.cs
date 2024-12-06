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
    public partial class AdminManageUsers : Form
    {
        private Form prev;

        public AdminManageUsers(Form prev)
        {
            InitializeComponent();
            this.prev = prev;

            // Initialize the Gender ComboBox
            comboBox1.Items.AddRange(new string[] { "Male", "Female", "Other" });
        }
        private void AdminManageUsers_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("User ID", 100);
            listView1.Columns.Add("Name", 150);
            listView1.Columns.Add("Email", 200);
            listView1.Columns.Add("Role", 100);
            listView1.Columns.Add("DateOfBirth", 150); // New column for Date of Birth
            listView1.Columns.Add("Gender", 100); // New column for Gender
            listView1.View = View.Details;

            // Attach the SelectedIndexChanged event handler
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;

            LoadUsersData();
        }


        private void LoadUsersData()
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            string query = "SELECT UserID, Name, Email, Role, DateOfBirth, Gender FROM [USER]";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    listView1.Items.Clear();

                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(reader["UserID"].ToString());
                        item.SubItems.Add(reader["Name"].ToString());
                        item.SubItems.Add(reader["Email"].ToString());
                        item.SubItems.Add(reader["Role"].ToString());
                        item.SubItems.Add(Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd")); // Format DOB
                        item.SubItems.Add(reader["Gender"].ToString());

                        listView1.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateTextBoxes(int userId)
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            string query = "SELECT Name, Email, Password, ContactInfo, Role, DateOfBirth, Gender FROM [USER] WHERE UserID = @UserID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        textBox1.Text = reader["Name"].ToString();
                        textBox2.Text = reader["Email"].ToString();
                        textBox3.Text = reader["Password"].ToString();
                        textBox4.Text = reader["ContactInfo"].ToString();
                        textBox5.Text = reader["Role"].ToString();
                        dateTimePicker1.Value = Convert.ToDateTime(reader["DateOfBirth"]); // Set DOB
                        comboBox1.SelectedItem = reader["Gender"].ToString(); // Set Gender
                    }
                    else
                    {
                        MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void UpdateUser(int userId, string name, string email, string password, string contactInfo, string role, DateTime dob, string gender)
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            string query = "UPDATE [USER] SET Name = @Name, Email = @Email, Password = @Password, ContactInfo = @ContactInfo, Role = @Role, DateOfBirth = @DOB, Gender = @Gender WHERE UserID = @UserID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@ContactInfo", contactInfo);
                    cmd.Parameters.AddWithValue("@Role", role);
                    cmd.Parameters.AddWithValue("@DOB", dob);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error updating user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a user to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int userId = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
            string name = textBox1.Text;
            string email = textBox2.Text;
            string password = textBox3.Text;
            string contactInfo = textBox4.Text;
            string role = textBox5.Text;
            DateTime dob = dateTimePicker1.Value;
            string gender = comboBox1.SelectedItem?.ToString();

            UpdateUser(userId, name, email, password, contactInfo, role, dob, gender);
            LoadUsersData();
        }


        private void DeleteUser(int userId)
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

            // Start by deleting related records in the Registration table (or any other table with a foreign key constraint)
            string deleteRelatedDataQuery = "DELETE FROM Registration WHERE AttendeeID = @UserID"; // Change Registration to your related table name
            string deleteRelatedDataQuery1 = "DELETE FROM FEEDBACK WHERE AttendeeID = @UserID"; // Change Registration to your related table name
            string deleteRelatedDataQuery2 = "DELETE FROM QUERY WHERE AttendeeID = @UserID"; // Change Registration to your related table name
            string deleteRelatedDataQuery3 = "DELETE FROM VENDORNEW WHERE UserID = @UserID"; // Change Registration to your related table name
            string deleteRelatedDataQuery4 = "DELETE FROM SPONSORNEW WHERE UserID = @UserID"; // Change Registration to your related table name

            // Then, delete the user from the Users table
            string deleteUserQuery = "DELETE FROM [USER] WHERE UserID = @UserID"; // Change to your actual Users table name

            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();

                // Start a transaction to ensure both deletions are done together
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Delete related data from the Registration table
                        using (SqlCommand cmd = new SqlCommand(deleteRelatedDataQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@UserID", userId);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = new SqlCommand(deleteRelatedDataQuery1, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@UserID", userId);
                            cmd.ExecuteNonQuery();
                        } using (SqlCommand cmd = new SqlCommand(deleteRelatedDataQuery2, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@UserID", userId);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = new SqlCommand(deleteRelatedDataQuery3, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@UserID", userId);
                            cmd.ExecuteNonQuery();
                        }
                         using (SqlCommand cmd = new SqlCommand(deleteRelatedDataQuery4, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@UserID", userId);
                            cmd.ExecuteNonQuery();
                        }

                        // Now, delete the user from the Users table
                        using (SqlCommand cmd = new SqlCommand(deleteUserQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@UserID", userId);
                            cmd.ExecuteNonQuery();
                        }

                        // Commit the transaction if both deletes are successful
                        transaction.Commit();

                        MessageBox.Show("User and related data deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        // Rollback the transaction if an error occurs
                        transaction.Rollback();
                        MessageBox.Show($"Error deleting user and related data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a user to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get selected user's UserID
            int userId = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);

            // Confirm the delete action
            DialogResult result = MessageBox.Show("Are you sure you want to delete this user?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                // Delete the user and related data from the database
                DeleteUser(userId);
                // Reload the data after deletion
                LoadUsersData();
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            prev.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count > 0)
            {
                // Get the UserID of the selected user
                int userId = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);

                // Populate the textboxes and other fields with the selected user's data
                PopulateTextBoxes(userId);
            }
            

        }
    }
}
