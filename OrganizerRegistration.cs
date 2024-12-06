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
    public partial class OrganizerRegistration : Form
    {
        private Form prev;
        private int organizerID; // Store the logged-in or selected organizer's ID

        public OrganizerRegistration(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        // Handle the "Register" button click
       

        // Load events for the organizer and display them in the ListView
        private void LoadEvents()
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT EventID, Title FROM EVENT WHERE OrganizerID = @OrganizerID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OrganizerID", organizerID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                listView1.Items.Clear(); // Clear existing items in the ListView
                while (reader.Read())
                {
                    var item = new ListViewItem(reader["Title"].ToString());
                    item.Tag = reader["EventID"]; // Store the EventID as the item Tag
                    listView1.Items.Add(item);
                }

                connection.Close();
            }
        }


        // Handle the "Manage Event" button click
        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an event to manage.");
                return;
            }

            // Get the EventID from the selected item
            int selectedEventID = (int)listView1.SelectedItems[0].Tag;

            // Pass the EventID to the Event Management form
            var manageEventForm = new OrganizerManageEvents(this, selectedEventID);  // Pass the selected EventID
            manageEventForm.Show();
            this.Hide();  // Hide the current form if needed
        }

        // Handle the "Back" button click
        private void button3_Click(object sender, EventArgs e)
        {
            var s = new Startup();  // Pass the selected EventID
            s.Show();
            this.Hide();
        }


        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void OrganizerRegistration_Load(object sender, EventArgs e)
        {

        }

       
        private void label1_Click(object sender, EventArgs e)
        {

        }

  

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string organizationName = textBox1.Text; // Organization Name
            string contactInfo = textBox2.Text;      // Contact Info

            if (string.IsNullOrWhiteSpace(organizationName) || string.IsNullOrWhiteSpace(contactInfo))
            {
                MessageBox.Show("Please fill all the fields.");
                return;
            }

            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // First, check if the organizer already exists
                string checkQuery = "SELECT OrganizerID FROM Organizers WHERE OrganizationName = @OrganizationName";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@OrganizationName", organizationName);

                connection.Open();
                var result = checkCommand.ExecuteScalar();

                // If the organizer exists, login (set organizerID)
                if (result != null)
                {
                    organizerID = (int)result;  // Get existing OrganizerID
                    MessageBox.Show("Welcome back! You are now logged in.");
                }
                else
                {
                    // If the organizer does not exist, register the new organizer
                    string query = "INSERT INTO Organizers (OrganizationName, ContactInfo) VALUES (@OrganizationName, @ContactInfo)";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@OrganizationName", organizationName);
                    command.Parameters.AddWithValue("@ContactInfo", contactInfo);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Organizer Registered Successfully!");

                    // Get the new organizer's ID after registration
                    string getIDQuery = "SELECT TOP 1 OrganizerID FROM Organizers ORDER BY OrganizerID DESC";
                    SqlCommand getIDCommand = new SqlCommand(getIDQuery, connection);
                    organizerID = (int)getIDCommand.ExecuteScalar();
                }

                connection.Close();

                LoadEvents();  // Now that the organizer is logged in/registered, load events
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string eventTitle = textBox3.Text;       // Event Title
            DateTime startDate = dateTimePicker1.Value; // Start Date
            DateTime endDate = dateTimePicker2.Value;   // End Date
            string description = textBox4.Text;     // Description
            string location = textBox6.Text;        // Location

            if (string.IsNullOrWhiteSpace(eventTitle) || string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(location))
            {
                MessageBox.Show("Please fill all the fields.");
                return;
            }
            if (organizerID <= 0)
            {
                MessageBox.Show("Login/ Register before Event creation.");
                return;
            }
            if (startDate > endDate)
            {
                MessageBox.Show("Start Date cannot be later than End Date.");
                return;
            }

            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO EVENT (OrganizerID, Title, StartDate, EndDate, Description, Location) " +
                               "VALUES (@OrganizerID, @EventTitle, @StartDate, @EndDate, @Description, @Location)";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@OrganizerID", organizerID); // Use the selected or logged-in OrganizerID
                command.Parameters.AddWithValue("@EventTitle", eventTitle);
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@Location", location);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Event Created Successfully!");
                LoadEvents(); // Refresh the event list
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var loginForm = new OrganizerTicketandSales(this);
            loginForm.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var loginForm = new OrganizerAttendeeMgmt(this);
            loginForm.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var loginForm = new VendorManagementOrganizer(this);
            loginForm.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var loginForm = new OrganizerEventAnalytics(this);
            loginForm.Show();
            this.Hide();
        }
    }
}
  