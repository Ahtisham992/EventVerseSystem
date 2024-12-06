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
    public partial class VendorServicesAndSponsorshipbidding : Form
    {
        private Form prev;
        public VendorServicesAndSponsorshipbidding(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        private void ServicesAndSponsorshipbiddingVENDOR_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("Event ID", 100);
            listView1.Columns.Add("Event Name", 200);
            listView1.Columns.Add("Event Location", 150);
            listView1.Columns.Add("Event Start Date", 150);
            listView1.Columns.Add("Event End Date", 150);

            LoadEventDataIntoListView();

        }
        private void LoadEventDataIntoListView()
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            string query = "SELECT EventID, Title, Location, StartDate, EndDate FROM EVENT";

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
                        item.SubItems.Add(reader["Title"].ToString());
                        item.SubItems.Add(reader["Location"].ToString());
                        item.SubItems.Add(reader["StartDate"].ToString());
                        item.SubItems.Add(reader["EndDate"].ToString());

                        listView1.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading event data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            prev.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //contribution amount
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please fill in all fields (Sponsor ID and Contribution Amount).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int sponsorID;
            if (!int.TryParse(textBox2.Text, out sponsorID))  // Check if SponsorID is numeric
            {
                MessageBox.Show("Please enter a valid Sponsor ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal contributionAmount;
            if (!decimal.TryParse(textBox1.Text, out contributionAmount))  // Check if ContributionAmount is valid
            {
                MessageBox.Show("Please enter a valid Contribution Amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the selected EventID from the ListView
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an event from the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int eventID = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text); // Getting EventID from selected row

            // Database connection string
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Query to insert the sponsorship data
                    string query = @"
                INSERT INTO SPONSORSHIPNEW (EventID, SponsorID, ContributionAmount)
                VALUES (@EventID, @SponsorID, @ContributionAmount)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to prevent SQL injection
                        cmd.Parameters.AddWithValue("@EventID", eventID);
                        cmd.Parameters.AddWithValue("@SponsorID", sponsorID);
                        cmd.Parameters.AddWithValue("@ContributionAmount", contributionAmount);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Sponsor bid added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Optionally, clear the fields for new input
                            textBox1.Clear();
                            textBox2.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add the bid. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //sponsor ID
        }
    }
}
