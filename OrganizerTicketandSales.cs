using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EventVerse
{
    public partial class OrganizerTicketandSales : Form
    {
        private Form prev;

        public OrganizerTicketandSales(Form prev)
        {
            InitializeComponent();
            PopulateTickets();
            this.prev = prev;
        }

        // Custom class to hold EventID and EventName
        public class EventItem
        {
            public int EventID { get; set; }
            public string Title { get; set; }

            public override string ToString()
            {
                return Title; // This will display the Title in the ComboBox
            }
        }

        // Method to populate the ComboBox with events from the database
        private void PopulateEvents()
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            string query = "SELECT EventID, Title FROM EVENT"; // Assuming EventID and Title are columns in the EVENT table

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    // Clear existing items in ComboBox
                    comboBox1.Items.Clear();

                    while (reader.Read())
                    {
                        // Add EventName to ComboBox as EventItem
                        comboBox1.Items.Add(reader["EventID"].ToString() + " - " + reader["Title"].ToString());

                    }

                    // Set the default selected item to the first event (optional)
                    if (comboBox1.Items.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching events: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to populate ListView with tickets for the selected event
        // Method to populate ListView with tickets for the selected event
        // Method to populate ListView with tickets for the selected event
        private void PopulateTickets()
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            string query = "SELECT TicketID,EventID, TicketType, Price, Availability FROM TICKET"; // Use parameterized query for the selected event

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    // Clear existing items in ListView
                    listView1.Items.Clear();

                    // Ensure the ListView is in Details view (show columns properly)
                    listView1.View = View.Details;

                    // Define columns for the ListView if not already set in the designer
                    if (listView1.Columns.Count == 0)
                    {
                        listView1.Columns.Add("TicketID", 100); // Set column width as needed
                        listView1.Columns.Add("EventID", 100); 
                        listView1.Columns.Add("TicketType", 150);
                        listView1.Columns.Add("Price", 100);
                        listView1.Columns.Add("Availability", 100);
                    }

                    while (reader.Read())
                    {
                        // Create a ListViewItem for each row in the database
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
                MessageBox.Show($"Error fetching tickets: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        // Handle form load event to populate ComboBox and ListView
        private void TicketandSalesOrganizer_Load(object sender, EventArgs e)
        {
            PopulateEvents();
        }

        // Handle closing the form and showing the previous form
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            prev.Show();
        }

        // Handle navigating to the "Update Tickets" form
        private void button5_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a ticket to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Retrieve the selected ticket's TicketID
            int ticketId = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);

            // Pass TicketID to the Update form
            var updateTicketForm = new OrganizerUpdateTickets(this, ticketId);
            this.Hide();
            updateTicketForm.Show();
        }

        // Handle ticket insertion when button1 is clicked
        private void button1_Click(object sender, EventArgs e)
        {
            // Retrieve the selected event from the ComboBox
            EventItem selectedEvent = comboBox1.SelectedItem as EventItem;
            int eventId = selectedEvent != null ? selectedEvent.EventID : -1; // Get the EventID of the selected event

            string ticketType = textBox1.Text; // Assuming you have a TextBox for TicketType
            decimal price = Convert.ToDecimal(textBox2.Text); // Assuming you have a TextBox for Price
            int availability = Convert.ToInt32(textBox3.Text); // Assuming you have a TextBox for Availability

            // Check if an event is selected
            if (eventId == -1)
            {
                MessageBox.Show("Please select an event", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // SQL Connection string
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True"; // Replace with your actual connection string

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL query to insert a new ticket
                    string query = "INSERT INTO TICKET (EventID, TicketType, Price, Availability) " +
                                   "VALUES (@EventID, @TicketType, @Price, @Availability)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@EventID", eventId);
                        command.Parameters.AddWithValue("@TicketType", ticketType);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@Availability", availability);

                        // Execute the query
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Ticket added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Refresh the ListView to show the new ticket
                            PopulateTickets();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            // Any label click actions (if needed) can be placed here
        }

        // When the ComboBox selection changes, load the tickets for the selected event
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            EventItem selectedEvent = comboBox1.SelectedItem as EventItem;
            if (selectedEvent != null)
            {
                PopulateTickets();
            }
        }
    }
}
