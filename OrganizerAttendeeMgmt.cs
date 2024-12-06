using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EventVerse
{
    public partial class OrganizerAttendeeMgmt : Form
    {
        private Form prev;
        private string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

        public OrganizerAttendeeMgmt(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
            LoadRegistrationData();
            LoadAttendeeQueries();
        }

        // Method to load data from REGISTRATION table into DataGridView
        private void LoadRegistrationData()
        {
            string query = @"
                SELECT r.RegistrationID, u.Name AS AttendeeName, e.Title AS EventTitle, r.Status 
                FROM REGISTRATION r
                JOIN [USER] u ON r.AttendeeID = u.UserID
                JOIN EVENT e ON r.EventID = e.EventID
where r.Status='Pending'";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Set the DataGridView's DataSource to the DataTable
                    dataGridView2.DataSource = dataTable;

                    // Optionally, auto-resize columns to fit the content
                    dataGridView2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading registration data: " + ex.Message);
            }
        }

        // Method to load attendee queries into DataGridView
        private void LoadAttendeeQueries()
        {
            string query = @"
                SELECT 
                    Q.QueryID, 
                    U.Name AS AttendeeName, 
                    E.Title AS EventTitle, 
                    Q.QueryDesc, 
                    Q.SubmittedDate AS QueryDate, 
                    Q.Status 
                FROM QUERY Q
                JOIN [USER] U ON Q.AttendeeID = U.UserID
                JOIN EVENT E ON Q.EventID = E.EventID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Set the DataGridView's DataSource to the DataTable
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading queries: " + ex.Message);
            }
        }

        // Event handler for item selection in DataGridView
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure a valid row is selected
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Populate the selected query ID, query description, and status
                textBox1.Text = row.Cells["QueryID"].Value.ToString();  // QueryID
                richTextBox1.Focus();  // Focus on the reply textbox to allow the user to enter a reply
            }
        }

        // Button to close the current form and go back to the previous one
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            prev.Show();
        }

        // Button to resolve or update the status of an attendee's query
        private void buttonResolve_Click_1(object sender, EventArgs e)
        {
            // Check if the query and the reply text box are filled
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(richTextBox1.Text))
            {
                MessageBox.Show("Please select a query and provide a reply.");
                return;
            }

            // Extract QueryID and reply from the text boxes
            int queryID = Convert.ToInt32(textBox1.Text);  // Get Query ID from TextBox
            string status = "Resolved";  // Set status as "Resolved"
            string reply = richTextBox1.Text;  // Get the reply from TextBox3

            // SQL query to update the query status and reply in the database
            string updateQuery = @"
                UPDATE QUERY 
                SET Status = @Status, Reply = @Reply, ResponseDate = GETDATE() 
                WHERE QueryID = @QueryID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(updateQuery, conn);
                    cmd.Parameters.AddWithValue("@QueryID", queryID);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Reply", reply);

                    conn.Open();
                    cmd.ExecuteNonQuery();  // Execute the query
                    MessageBox.Show("Query resolved and reply sent!");  // Confirmation message

                    // Reload the attendee queries to reflect changes
                    LoadAttendeeQueries();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error resolving query: " + ex.Message);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        // Button to confirm registration
        private void button1_Click(object sender, EventArgs e)
        {
            // Check if a row is selected in the DataGridView
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a registration to confirm.");
                return;
            }

            // Get the RegistrationID from the selected row in the DataGridView
            int registrationID = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["RegistrationID"].Value);  // RegistrationID from the "RegistrationID" column

            // SQL query to update the status to "Confirmed"
            string updateQuery = @"
                UPDATE REGISTRATION
                SET Status = 'Confirmed'
                WHERE RegistrationID = @RegistrationID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(updateQuery, conn);
                    cmd.Parameters.AddWithValue("@RegistrationID", registrationID);

                    conn.Open();
                    cmd.ExecuteNonQuery();  // Execute the update query
                    MessageBox.Show("Registration confirmed!");  // Confirmation message

                    // Reload the registration data to reflect the updated status
                    LoadRegistrationData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error confirming registration: " + ex.Message);
            }
        }
    }
}
