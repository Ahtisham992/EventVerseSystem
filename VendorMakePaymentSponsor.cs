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
    public partial class VendorMakePaymentSponsor : Form
    {
        private Form prev;
        public VendorMakePaymentSponsor(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        private void VendorMakePaymentSponsor_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("Event ID", 100);
            listView1.Columns.Add("Event Name", 200);
            listView1.Columns.Add("Event Location", 150);
            listView1.Columns.Add("Event Start Date", 150);
            listView1.Columns.Add("Event End Date", 150);

            LoadEventDataIntoListView();
            LoadSponsorIdsIntoComboBox();  // Load Sponsor IDs into ComboBox
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

        private void LoadSponsorIdsIntoComboBox()
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            string query = "SELECT SponsorID FROM SPONSORNEW";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    comboBox1.Items.Clear();

                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader["SponsorID"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sponsor data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate Amount Input
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter the payment amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBox1.SelectedItem == null)  // Validate Sponsor ID selection
            {
                MessageBox.Show("Please select a Sponsor ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal amount;
            if (!decimal.TryParse(textBox1.Text, out amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid payment amount greater than 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int sponsorId = Convert.ToInt32(comboBox1.SelectedItem.ToString());  // Get SponsorID from ComboBox

            // Validate Event Selection
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an event from the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int eventId = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text); // EventID from selected row

            // Here, assume that RegistrationID is somehow passed, either from a previous form or selected in the UI
            int registrationId = 1; // Implement this method to get the Registration ID

            // Validate RegistrationID
            if (registrationId <= 0)
            {
                MessageBox.Show("Please select a valid registration.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Database connection string
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Verify SponsorID exists in SPONSORNEW table
                    string checkSponsorQuery = "SELECT COUNT(*) FROM SPONSORNEW WHERE SponsorID = @SponsorID";
                    using (SqlCommand checkSponsorCmd = new SqlCommand(checkSponsorQuery, conn))
                    {
                        checkSponsorCmd.Parameters.AddWithValue("@SponsorID", sponsorId);
                        int sponsorExists = (int)checkSponsorCmd.ExecuteScalar();

                        if (sponsorExists == 0)
                        {
                            MessageBox.Show("Sponsor ID not found. Please check the Sponsor ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Verify RegistrationID exists in REGISTRATIONNEW table
                    string checkRegistrationQuery = "SELECT COUNT(*) FROM REGISTRATION WHERE RegistrationID = @RegistrationID";
                    using (SqlCommand checkRegistrationCmd = new SqlCommand(checkRegistrationQuery, conn))
                    {
                        checkRegistrationCmd.Parameters.AddWithValue("@RegistrationID", registrationId);
                        int registrationExists = (int)checkRegistrationCmd.ExecuteScalar();

                        if (registrationExists == 0)
                        {
                            MessageBox.Show("Registration ID not found. Please check the Registration ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Insert payment into PAYMENTNEW table
                    string query = @"
            INSERT INTO PAYMENTNEW (RegistrationID, EventID, SponsorID, Amount, PaymentDate, PaymentStatus) 
            VALUES (@RegistrationID, @EventID, @SponsorID, @Amount, GETDATE(), 'Completed')";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters
                        cmd.Parameters.AddWithValue("@RegistrationID", registrationId);
                        cmd.Parameters.AddWithValue("@EventID", eventId);
                        cmd.Parameters.AddWithValue("@SponsorID", sponsorId);
                        cmd.Parameters.AddWithValue("@Amount", amount);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Payment successful! Thank you.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Optionally, clear the textboxes for the next input
                            textBox1.Clear();
                            comboBox1.SelectedIndex = -1; // Clear ComboBox selection
                        }
                        else
                        {
                            MessageBox.Show("Payment failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            prev.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //amount to pay
        }



        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //sponsorid
        }
    }
}
