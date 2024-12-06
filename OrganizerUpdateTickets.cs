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
    public partial class OrganizerUpdateTickets : Form
    {
        private Form prev;
        private int ticketId;  // Variable to store the TicketID

        // Constructor to pass the previous form and TicketID
        public OrganizerUpdateTickets(Form prev, int ticketId)
        {
            InitializeComponent();
            this.prev = prev;
            this.ticketId = ticketId; // Store the TicketID
        }

        // Method to populate the controls with the ticket data
        private void LoadTicketData()
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            string query = "SELECT TicketID, EventID, TicketType, Price, Availability FROM TICKET WHERE TicketID = @TicketID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TicketID", ticketId); // Use the passed TicketID

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Populate the controls with the ticket data
                        textBox5.Text = reader["TicketID"].ToString(); // Display TicketID (read-only)
                        comboBox1.Text = reader["EventID"].ToString(); // Populate EventID (can be ComboBox or TextBox)
                        textBox1.Text = reader["TicketType"].ToString();
                        textBox2.Text = reader["Price"].ToString();
                        textBox3.Text = reader["Availability"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Ticket not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading ticket data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Handle form load event to load ticket data based on TicketID
        private void OrganizerUpdateTickets_Load(object sender, EventArgs e)
        {
            LoadTicketData(); // Load the ticket data when the form loads
            LoadEventIDs(); // Load the available EventIDs in the ComboBox
        }

        // Method to load available EventIDs into the ComboBox
        private void LoadEventIDs()
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            string query = "SELECT EventID, Title FROM EVENT"; // Assuming you have an EVENTS table

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    comboBox1.Items.Clear(); // Clear any previous items in the ComboBox

                    while (reader.Read())
                    {
                        // Add EventID and EventName as items to the ComboBox
                        comboBox1.Items.Add(reader["EventID"].ToString() + " - " + reader["Title"].ToString());
                    }

                    if (comboBox1.Items.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0; // Select the first item by default
                    }
                }   
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading EventIDs: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Handle the "Back" button click to return to the previous form
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            prev.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string ticketType = textBox1.Text;  // TicketType from TextBox
            decimal price = Convert.ToDecimal(textBox2.Text);  // Price from TextBox
            int availability = Convert.ToInt32(textBox3.Text);  // Availability from TextBox
            string selectedEventID = comboBox1.SelectedItem.ToString().Split(' ')[0]; // Get the EventID from the ComboBox (before space)

            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            string query = "UPDATE TICKET SET TicketType = @TicketType, Price = @Price, Availability = @Availability, EventID = @EventID WHERE TicketID = @TicketID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TicketType", ticketType);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@Availability", availability);
                    command.Parameters.AddWithValue("@EventID", selectedEventID); // Update EventID
                    command.Parameters.AddWithValue("@TicketID", ticketId);  // Use the stored TicketID

                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Ticket updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating ticket: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            string query = "DELETE FROM TICKET WHERE TicketID = @TicketID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TicketID", ticketId); // Use the stored TicketID

                    DialogResult confirmResult = MessageBox.Show(
                        "Are you sure you want to delete this ticket?",
                        "Confirm Delete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (confirmResult == DialogResult.Yes)
                    {
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Ticket deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide(); // Close the form after deletion
                            prev.Show(); // Return to the previous form
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete ticket. It might not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting ticket: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
