using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace CTFPrototype.Instructor
{
    public partial class RankingViewForm : Form
    {
        public RankingViewForm()
        {
            InitializeComponent();
            LoadRankings();
        }

        private void LoadRankings()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";
            string query = "SELECT TeamName, Points FROM Teams ORDER BY Points DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Clear existing rows
                        rankingGrid.Rows.Clear();

                        int rank = 1;
                        while (reader.Read())
                        {
                            // Create a new row
                            int rowIndex = rankingGrid.Rows.Add();
                            DataGridViewRow newRow = rankingGrid.Rows[rowIndex];

                            // Set the values for each cell in the row
                            newRow.Cells["Ranking"].Value = rank++;
                            newRow.Cells["TeamName"].Value = reader["TeamName"].ToString();
                            newRow.Cells["Points"].Value = reader["Points"].ToString();
                        }
                    }
                }
            }
        }

        private void RankingViewForm_Load(object sender, EventArgs e)
        {

        }

        private void rankingGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
