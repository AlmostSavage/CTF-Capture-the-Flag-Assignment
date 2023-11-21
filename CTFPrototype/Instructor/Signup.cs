using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;
using Microsoft.Data.SqlClient;

namespace CTFPrototype
{
    public partial class Signup : Form
    {
        // Connect database
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";

        public Signup()
        {
            InitializeComponent();

            // Populate the team list
            PopulateTeamList();
        }

        private void Signup_Load(object sender, EventArgs e)
        {

        }

        private bool CreateUser(string username, string passwordHash, int roleId, int teamId)
        {
            string checkUserQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
            string createUserQuery = "INSERT INTO Users (Username, PasswordHash, RoleID, TeamID) VALUES (@Username, @PasswordHash, @RoleId, @TeamId)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    // Check if the username exists
                    using (SqlCommand cmd = new SqlCommand(checkUserQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        int userCount = Convert.ToInt32(cmd.ExecuteScalar());

                        if (userCount > 0)
                        {
                            // Username exists, rollback the transaction and return false
                            transaction.Rollback();
                            return false;
                        }
                    }

                    // Create the new user
                    using (SqlCommand cmd = new SqlCommand(createUserQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        cmd.Parameters.AddWithValue("@RoleId", roleId);
                        cmd.Parameters.AddWithValue("@TeamId", teamId);
                        cmd.ExecuteNonQuery();
                    }

                    // Commit the transaction
                    transaction.Commit();
                }
            }
            return true;
        }

        private void signupButton_Click(object sender, EventArgs e)
        {
            string username = signupUsername.Text;
            string password = signupPassword.Text;

            // You may want to add validation here to ensure username and password are not empty

            // Hash the password using BCrypt
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            // Assuming RoleID for new users is 2
            int roleId = 2;

            // Get the selected Team ID from teamList
            int teamId = (int)teamList.SelectedValue;

            // Try to create the new user
            bool userCreated = CreateUser(username, passwordHash, roleId, teamId);

            if (userCreated)
            {
                MessageBox.Show("User account created successfully.");

                // Reset text fields
                signupUsername.Text = "";
                signupPassword.Text = "";
            }
            else
            {
                MessageBox.Show("Username already exists. Please choose a different username.");
            }
        }

        private void PopulateTeamList()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";
            string query = "SELECT TeamID, TeamName FROM Teams";

            DataTable teams = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(teams);
                }
            }

            // Set the teamList ListBox properties
            teamList.DataSource = teams;
            teamList.DisplayMember = "TeamName";
            teamList.ValueMember = "TeamID";
        }
    }
}
