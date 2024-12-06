using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EventVerse
{
    public partial class OrganizerAttendeeFeedback : Form
    {
        private Form prev;

        // Constructor
        public OrganizerAttendeeFeedback(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }


        private void OrganizerAttendeeFeedback_Load(object sender, EventArgs e)
        {
            // Configure the ListView columns
            ConfigureListView();

            // Load all feedback when the form loads
            LoadAllFeedback();
        }

        // Method to configure the ListView with proper columns
        private void ConfigureListView()
        {
            listView1.View = View.Details; // Set to Details view for table-like appearance
            listView1.FullRowSelect = true; // Enable full row selection
            listView1.GridLines = true; // Show grid lines for better readability
            listView1.Columns.Clear(); // Clear any existing columns

            // Add appropriate columns
            listView1.Columns.Add("Event Title", 150, HorizontalAlignment.Left); // Column for Event Title
            listView1.Columns.Add("Attendee Name", 150, HorizontalAlignment.Left); // Column for Attendee Name
            listView1.Columns.Add("Rating", 100, HorizontalAlignment.Left); // Column for Rating
        }

        // Method to load feedback data into the ListView
        private void LoadAllFeedback()
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            string query = "SELECT e.Title AS EventTitle, u.Name AS AttendeeName, f.Rating " +
                           "FROM FEEDBACK f " +
                           "JOIN [USER] u ON f.AttendeeID = u.UserID " +
                           "JOIN EVENT e ON f.EventID = e.EventID";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Clear the existing items in the ListView before adding new data
                    listView1.Items.Clear();

                    // Loop through the data and add it to the ListView
                    while (reader.Read())
                    {
                        // Create a new ListViewItem with EventTitle as the first column
                        ListViewItem item = new ListViewItem(reader["EventTitle"].ToString());

                        // Add sub-items for AttendeeName and Rating
                        item.SubItems.Add(reader["AttendeeName"].ToString());
                        item.SubItems.Add(reader["Rating"].ToString());

                        // Add the item to the ListView
                        listView1.Items.Add(item);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while loading feedback: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        // Button Click to go back to the previous form
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            prev.Show();
        }
    }
}
