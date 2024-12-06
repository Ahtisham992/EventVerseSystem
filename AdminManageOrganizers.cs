using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EventVerse
{
    public partial class AdminManageOrganizers : Form
    {
        private Form prev;

        // Constructor for initializing the form with a reference to the previous form
        public AdminManageOrganizers(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        // Form Load Event - Load organizer data into ListView
        private void AdminManageOrganizers_Load(object sender, EventArgs e)
        {
            // Set up ListView columns
            listView1.Columns.Add("Organizer ID", 20);
            listView1.Columns.Add("Name", 50);
            listView1.Columns.Add("Contact Info", 100);
            listView1.View = View.Details;

            LoadOrganizersData();
        }

        // Method to load organizer data from the database into ListView
        private void LoadOrganizersData()
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            string query = "SELECT OrganizerID, OrganizationName, ContactInfo FROM Organizers";

            // Clear any existing data in the ListView
            listView1.Items.Clear();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Add data to the ListView
                        ListViewItem item = new ListViewItem(reader["OrganizerID"].ToString());
                        item.SubItems.Add(reader["OrganizationName"].ToString());
                        item.SubItems.Add(reader["ContactInfo"].ToString());

                        listView1.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading organizers data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to delete organizer data from the database
        private void DeleteOrganizer(int organizerId)
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            string query = "DELETE FROM Organizers WHERE OrganizerID = @OrganizerID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrganizerID", organizerId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Organizer deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error deleting organizer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting organizer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        // Cancel button to close the current form and go back
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            prev.Show();
        }

        // Placeholder for another button if needed
        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an organizer to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the selected organizer's ID
            int organizerId = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);

            // Confirm the delete action
            DialogResult result = MessageBox.Show("Are you sure you want to delete this organizer?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                // Delete the organizer
                DeleteOrganizer(organizerId);
                // Reload the data after deletion
                LoadOrganizersData();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an organizer to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the selected organizer's ID
            int organizerId = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
            string orgName = listView1.SelectedItems[0].SubItems[1].Text;
            string contactInfo = listView1.SelectedItems[0].SubItems[2].Text;

            // Pre-fill textboxes for editing
            textBox1.Text = orgName;
            textBox4.Text = contactInfo;

            // Update the organizer details when the user clicks the "Update" button
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            string query = "UPDATE Organizers SET OrganizationName = @OrganizationName, ContactInfo = @ContactInfo WHERE OrganizerID = @OrganizerID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrganizerID", organizerId);
                    cmd.Parameters.AddWithValue("@OrganizationName", textBox1.Text);
                    cmd.Parameters.AddWithValue("@ContactInfo", textBox4.Text);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Organizer updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Close the form after the update
                    }
                    else
                    {
                        MessageBox.Show("Error updating organizer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating organizer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Reload the data after updating
            LoadOrganizersData();
        }
    }
}
