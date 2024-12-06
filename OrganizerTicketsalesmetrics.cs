using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EventVerse
{
    public partial class OrganizerTicketsalesmetrics : Form
    {
        private Form prev;
        public OrganizerTicketsalesmetrics(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        private void OrganizerTicketsalesmetrics_Load(object sender, EventArgs e)
        {
            // Initially load all ticket sales metrics
            LoadTicketSalesMetrics();
        }

        private void LoadTicketSalesMetrics(int? ticketId = null)
        {
            // Connection string to connect to the database
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

            // SQL query with an optional filter for ticket ID
            string query = @"
                SELECT 
                    E.Title AS EventTitle, 
                    T.TicketType, 
                    SUM(T.Availability) AS TicketsSold, 
                    SUM(T.Price * T.Availability) AS Revenue 
                FROM TICKET T
                JOIN EVENT E ON T.EventID = E.EventID";

            // Add filter condition if ticketId is provided
            if (ticketId.HasValue)
            {
                query += " WHERE T.TicketID = @TicketID";
            }

            query += " GROUP BY E.Title, T.TicketType";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

                    // Add parameter to prevent SQL injection
                    if (ticketId.HasValue)
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@TicketID", ticketId.Value);
                    }

                    // Fill DataTable with results
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Bind data to DataGridView
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Hide current form and show the previous form
            this.Hide();
            prev.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // Attempt to get the TicketID from textBox1
                int ticketId;
                if (int.TryParse(textBox1.Text, out ticketId))
                {
                    // If valid, load ticket sales metrics for the given ticket ID
                    LoadTicketSalesMetrics(ticketId);
                }
                else
                {
                    // If invalid, show an error message
                    MessageBox.Show("Please enter a valid Ticket ID.");
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that may occur
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // You can add logic here to handle any specific behavior when the text in textBox1 changes
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadTicketSalesMetrics();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
