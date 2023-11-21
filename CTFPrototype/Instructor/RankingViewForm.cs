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

            DataTable rankings = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(rankings);
                }
            }

            rankingGrid.DataSource = rankings;
        }

        private void RankingViewForm_Load(object sender, EventArgs e)
        {

        }

        private void rankingGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
