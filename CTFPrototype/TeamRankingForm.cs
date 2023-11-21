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
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;
using LiveChartsCore.SkiaSharpView.WinForms;

namespace CTFPrototype
{
    public partial class TeamRankingForm : Form
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";

        public TeamRankingForm()
        {
            InitializeComponent();

            // Fetch data from the database
            var fetchedTeams = GetTeamData();

            // Debug
            Console.WriteLine(fetchedTeams.Count);

            // Prepare the series for the chart
            List<ISeries> series = new List<ISeries>();
            foreach (var team in fetchedTeams)
            {
                series.Add(new RowSeries<double>
                {
                    Values = new double[] { team.Points },
                    Name = team.TeamName
                });
            }

            // Configure the chart
            var barChart = new CartesianChart
            {
                Series = series,
                YAxes = new List<Axis>
                {
                    new Axis
                    {
                        LabelsRotation = 15,
                        Labeler = value =>
                        {
                            // Ensure the index is within the bounds of fetchedTeams
                            int index = (int)value;
                            if (index >= 0 && index < fetchedTeams.Count)
                                return fetchedTeams[index].TeamName;
                            return ""; // Return an empty string or some default value if out of bounds
                        }
                    }
                },
                XAxes = new List<Axis>
                {
                    new Axis
                    {
                        // Configure axis as needed, e.g., min/max range
                    }
                }
            };

            // Add the chart to the form
            this.Controls.Add(barChart);
            barChart.Dock = DockStyle.Fill; // To auto-adjust the size based on the form
        }

        private List<Team> GetTeamData()
        {
            var teams = new List<Team>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT TeamName, Points FROM Teams ORDER BY Points DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            teams.Add(new Team
                            {
                                TeamName = reader["TeamName"].ToString(),
                                Points = (int)reader["Points"]
                            });
                        }
                    }
                }
            }
            return teams;
        }
    }

    public class Team
    {
        public string TeamName { get; set; }
        public int Points { get; set; }
    }
}
