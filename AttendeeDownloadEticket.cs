using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace EventVerse
{
    public partial class AttendeeDownloadEticket : Form
    {
        private Form prev;
        private int eventID;
        private string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

        public AttendeeDownloadEticket(Form prev, int eventID)
        {
            InitializeComponent();
            this.prev = prev;
            this.eventID = eventID;
        }

        private void DownloadEticketattendee_Load(object sender, EventArgs e)
        {
            // Add columns to the ListView
            listView1.Columns.Add("Ticket ID", 100);
            listView1.Columns.Add("Event ID", 100);
            listView1.Columns.Add("Ticket Type", 150);
            listView1.Columns.Add("Price", 100);
            listView1.Columns.Add("Availability", 100);

            // Load data into the ListView
            LoadDataIntoListView();
        }

        private void LoadDataIntoListView()
        {
            string query = "SELECT TicketID, EventID, TicketType, Price, Availability FROM TICKET WHERE EventID = @EventID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EventID", eventID);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Create a ListViewItem for each row
                        ListViewItem item = new ListViewItem(reader["TicketID"].ToString());
                        item.SubItems.Add(reader["EventID"].ToString());
                        item.SubItems.Add(reader["TicketType"].ToString());
                        item.SubItems.Add(reader["Price"].ToString());
                        item.SubItems.Add(reader["Availability"].ToString());

                        // Add the item to the ListView
                        listView1.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0) // Ensure an item is selected
            {
                // Get data from the selected row
                ListViewItem selectedItem = listView1.SelectedItems[0];
                string ticketID = selectedItem.SubItems[0].Text;
                string ticketType = selectedItem.SubItems[2].Text;
                string price = selectedItem.SubItems[3].Text;
                int availability = int.Parse(selectedItem.SubItems[4].Text);

                if (availability > 0)
                {
                    // Create ticket content
                    string ticketContent = $"Ticket ID: {ticketID}\nEvent ID: {eventID}\nTicket Type: {ticketType}\nPrice: {price}\n";

                    // Save file locally
                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "Text Files (*.txt)|*.txt",
                        FileName = $"Ticket_{ticketID}.txt"
                    };

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(saveFileDialog.FileName, ticketContent);

                        // Update ticket availability in the database
                        UpdateTicketAvailability(ticketID);

                        MessageBox.Show("Ticket downloaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Refresh ListView
                        listView1.Items.Clear();
                        LoadDataIntoListView();
                    }
                }
                else
                {
                    MessageBox.Show("No tickets available for this type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a ticket to download.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateTicketAvailability(string ticketID)
        {
            string updateQuery = "UPDATE TICKET SET Availability = Availability - 1 WHERE TicketID = @TicketID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@TicketID", ticketID);
                    conn.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Ticket availability updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to update ticket availability.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating ticket availability: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            prev.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Optional: Handle selection change event
        }
    }
}
