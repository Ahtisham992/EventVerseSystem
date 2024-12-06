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
    public partial class AttendeeTicketCheckIncs : Form
    {
        private Form prev;
        public AttendeeTicketCheckIncs(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        private void AttendeeTicketCheckIncs_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("Ticket ID", 100);
            listView1.Columns.Add("Event ID", 200);
            listView1.Columns.Add("Ticket Type", 150);
            listView1.Columns.Add("Price", 150);
            listView1.Columns.Add("Availability", 150);

            LoadDataIntoListView();
        }

        private void LoadDataIntoListView()
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            string query = "SELECT TicketID, EventID, TicketType, Price FROM TICKET";

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
                        ListViewItem item = new ListViewItem(reader["TicketID"].ToString());
                        item.SubItems.Add(reader["EventID"].ToString());
                        item.SubItems.Add(reader["TicketType"].ToString());
                        item.SubItems.Add(reader["Price"].ToString());

                        listView1.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading event data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            prev.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a ticket to check in.", "No Ticket Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the selected ticket
            ListViewItem selectedItem = listView1.SelectedItems[0];
            string ticketId = selectedItem.SubItems[0].Text;

            string connectionString = "Data Source=DESKTOP-425ALTB\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            string deleteQuery = "DELETE FROM TICKET WHERE TicketID = @TicketID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@TicketID", ticketId);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Remove the ticket from the ListView
                        listView1.Items.Remove(selectedItem);

                        // Show success message
                        MessageBox.Show("CHECKED IN successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to check in the ticket. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during ticket check-in: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
