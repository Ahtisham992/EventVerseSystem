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
    public partial class AttendeeTickets : Form
    {
        private int userid;
        private int selectedEventID = -1; // Initialize to -1 to indicate no selection
        private Form prev;
        private string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";

        public AttendeeTickets(int userid, Form prev)
        {
            InitializeComponent();
            this.userid = userid;
            this.prev = prev;
        }

        private void AttendeeTickets_Load(object sender, EventArgs e)
        {
            LoadBookedEvents();

            // Configure DataGridView to allow full row selection
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        private void LoadBookedEvents()
        {
            string query = @"
                SELECT 
                    e.EventID, 
                    e.Title AS EventTitle, 
                    e.StartDate AS EventDate, 
                    e.Location AS EventLocation, 
                    r.Status 
                FROM REGISTRATION r
                JOIN EVENT e ON r.EventID = e.EventID
                WHERE r.AttendeeID = @UserID 
                AND r.Status = 'Confirmed'";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@UserID", userid);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading booked events: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore header row
            if (e.RowIndex < 0) return;

            try
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Ensure the EventID column exists and has a value
                if (dataGridView1.Columns.Contains("EventID") &&
                    selectedRow.Cells["EventID"].Value != null)
                {
                    // Parse and store the selected EventID
                    selectedEventID = Convert.ToInt32(selectedRow.Cells["EventID"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error selecting event: {ex.Message}");
                selectedEventID = -1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (selectedEventID <= 0)
            {
                // If no row is selected, try to get the first selected row
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    if (selectedRow.Cells["EventID"].Value != null)
                    {
                        selectedEventID = Convert.ToInt32(selectedRow.Cells["EventID"].Value);
                    }
                }

                // If still no valid event selected, show error
                if (selectedEventID <= 0)
                {
                    MessageBox.Show("Please select a valid event from the list first.");
                    return;
                }
            }

            try
            {
                // Pass the selected EventID to the AttendeeDownloadEticket form
                var ticket = new AttendeeDownloadEticket(this, selectedEventID);
                this.Hide();
                ticket.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening ticket form: {ex.Message}");
            }
        }

    

private void button2_Click(object sender, EventArgs e)
        {
            if (selectedEventID <= 0)
            {
                MessageBox.Show("Please select a valid event from the list first.");
                return;
            }

            try
            {
                // Pass the selected EventID to the AttendeeTicketCheckIncs form
                //var ticket = new AttendeeTicketCheckIncs(this, selectedEventID);
                //this.Hide();
                //ticket.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening check-in form: {ex.Message}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            prev.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Optional: Handle label click event
        }
    }
}
