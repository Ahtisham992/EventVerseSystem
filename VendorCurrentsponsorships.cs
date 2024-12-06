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
    public partial class VendorCurrentsponsorships : Form
    {
        private Form prev;
        public VendorCurrentsponsorships(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            prev.Show();

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void VendorCurrentsponsorships_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("Sponsorship ID", 100);
            listView1.Columns.Add("Event ID", 200);
            listView1.Columns.Add("Sponsor ID", 150);
            listView1.Columns.Add("Contribution Amount", 150);

            LoadDataIntoListView();
        }

        private void LoadDataIntoListView()
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            string query = "SELECT SponsorshipID, EventID, SponsorID, ContributionAmount FROM SPONSORSHIPNEW";

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
                        ListViewItem item = new ListViewItem(reader["SponsorshipID"].ToString());
                        item.SubItems.Add(reader["EventID"].ToString());
                        item.SubItems.Add(reader["SponsorID"].ToString());
                        item.SubItems.Add(reader["ContributionAmount"].ToString());

                        listView1.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading event data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
