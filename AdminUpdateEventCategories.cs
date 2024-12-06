using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EventVerse
{
    public partial class AdminUpdateEventCategories : Form
    {
        private Form prev;
        public AdminUpdateEventCategories(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        private void AdminUpdateEventCategories_Load(object sender, EventArgs e)
        {
            LoadCategoryData(); // Load the data when the form loads
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            prev.Show();
        }

        private void LoadCategoryData()
        {
            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT CategoryID, Name, description FROM CATEGORY";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        // Bind the data to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                int categoryId = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["CategoryID"].Value);

                string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string query = "DELETE FROM CATEGORY WHERE CategoryID = @CategoryID";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Category deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadCategoryData(); // Refresh the DataGridView
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string categoryName = textBox2.Text;
            string categoryDescription = textBox4.Text;

            if (string.IsNullOrEmpty(categoryName) || string.IsNullOrEmpty(categoryDescription))
            {
                MessageBox.Show("Please fill out all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO CATEGORY (Name, description) VALUES (@Name, @Description)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", categoryName);
                        cmd.Parameters.AddWithValue("@Description", categoryDescription);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Category added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadCategoryData(); // Refresh the DataGridView
                        }
                        else
                        {
                            MessageBox.Show("Failed to add category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure the clicked cell is within valid range
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Populate the textboxes with the selected row's values
                textBox1.Text = row.Cells["CategoryID"].Value.ToString();
                textBox2.Text = row.Cells["Name"].Value.ToString();
                textBox4.Text = row.Cells["Description"].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int categoryId = Convert.ToInt32(textBox1.Text);
            string categoryName = textBox2.Text;
            string categoryDescription = textBox4.Text;

            if (string.IsNullOrEmpty(categoryName) || string.IsNullOrEmpty(categoryDescription))
            {
                MessageBox.Show("Please fill out all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Data Source=DESKTOP-J9BIC6A\\SQLEXPRESS;Initial Catalog=EventVerse;Integrated Security=True";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE CATEGORY SET Name = @Name, description = @Description WHERE CategoryID = @CategoryID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                        cmd.Parameters.AddWithValue("@Name", categoryName);
                        cmd.Parameters.AddWithValue("@Description", categoryDescription);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Category updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadCategoryData(); // Refresh the DataGridView
                        }
                        else
                        {
                            MessageBox.Show("Failed to update category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
