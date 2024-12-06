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
    public partial class VendorTrackallpayments : Form
    {
        private Form prev;
        public VendorTrackallpayments(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        private void VendorTrackallpayments_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("Event ID", 100);
            listView1.Columns.Add("Amount", 200);
            listView1.Columns.Add("Payment Date", 150);
            listView1.Columns.Add("Payment Status", 150);

            LoadEventDataIntoListView();
        }

        private void LoadEventDataIntoListView()
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            string query = "SELECT E.EventID,P.Amount,P.PaymentDate, P.PaymentStatus FROM PAYMENTNEW P INNER JOIN REGISTRATION R ON P.RegistrationID = R.RegistrationID INNER JOIN EVENT E ON R.EventID = E.EventID";

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
                        item.SubItems.Add(reader["Amount"].ToString());
                        item.SubItems.Add(reader["PaymentDate"].ToString());
                        item.SubItems.Add(reader["PaymentStatus"].ToString());

                        listView1.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading event data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            prev.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var n = new VendorMakePaymentSponsor(this);
            this.Hide();
            n.Show();
        }
    }
}
