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

namespace CTFPrototype
{
    public partial class TeamRankingForm : Form
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";
        public TeamRankingForm()
        {
            InitializeComponent();
            LoadTeamData();
        }
        private void LoadTeamData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT TeamName, Points FROM Teams ORDER BY Points DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt; // Assuming your DataGridView is named dataGridView1
                }
            }
        }

        private void TeamRankingForm_Load(object sender, EventArgs e)
        {

        }
    }
}
