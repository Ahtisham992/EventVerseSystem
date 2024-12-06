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
    public partial class VendorViewContracts : Form
    {
        private Form prev;
        public VendorViewContracts(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        private void VendorViewContracts_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("Event ID", 100);
            listView1.Columns.Add("Vendor ID", 200);
            listView1.Columns.Add("ServiceProvided", 150);
            listView1.Columns.Add("Payment", 150);

            LoadEventDataIntoListView();
        }

        private void LoadEventDataIntoListView()
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            string query = "SELECT EventID, VendorID, ServiceProvided, Payment FROM CONTRACTNEW";

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
                        ListViewItem item = new ListViewItem(reader["EventID"].ToString());
                        item.SubItems.Add(reader["VendorID"].ToString());
                        item.SubItems.Add(reader["ServiceProvided"].ToString());
                        item.SubItems.Add(reader["Payment"].ToString());

                        listView1.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading event data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            prev.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if an item is selected
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a contract to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the selected EventID and VendorID from the ListView
            int eventID = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text); // First column is EventID
            int vendorID = Convert.ToInt32(listView1.SelectedItems[0].SubItems[1].Text); // Second column is VendorID

            // Confirm deletion
            DialogResult result = MessageBox.Show($"Are you sure you want to delete the contract for Event ID {eventID} and Vendor ID {vendorID}?",
                                                  "Confirm Deletion",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;

            // Database connection string
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Query to delete the contract based on EventID and VendorID
                    string query = @"DELETE FROM CONTRACTNEW WHERE EventID = @EventID AND VendorID = @VendorID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to prevent SQL injection
                        cmd.Parameters.AddWithValue("@EventID", eventID);
                        cmd.Parameters.AddWithValue("@VendorID", vendorID);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Contract deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Remove the deleted item from the ListView
                            listView1.SelectedItems[0].Remove();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete the contract. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
