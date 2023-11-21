using CTFPrototype.Instructor;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;

namespace CTFPrototype
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            passwordBox.KeyDown += new KeyEventHandler(passwordBox_KeyDown);
        }

        private void passwordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AuthenticateUser();
            }
        }

        private void AuthenticateUser()
        {
            string enteredUsername = usernameBox.Text;
            string enteredPassword = passwordBox.Text;

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT UserID, PasswordHash, RoleID FROM Users WHERE Username = @Username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", enteredUsername);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedPasswordHash = reader["PasswordHash"].ToString();
                            int roleID = (int)reader["RoleID"];
                            int userID = (int)reader["UserID"];

                            if (BCrypt.Net.BCrypt.Verify(enteredPassword, storedPasswordHash))
                            {
                                this.Hide(); // Hide the login form.
                                Form mainForm = null;

                                if (roleID == 1)
                                {
                                    mainForm = new InstructorMain(this, userID);
                                }
                                else if (roleID == 2)
                                {
                                    mainForm = new StudentMain(this, userID);
                                }

                                if (mainForm != null)
                                {
                                    mainForm.Show();
                                }
                                else
                                {
                                    // Handle invalid roleID or other issues
                                    MessageBox.Show("Invalid role or user configuration. Please contact support.");
                                    this.Show(); // Show the login form again if there's an issue
                                }
                            }
                            else
                            {
                                MessageBox.Show("Incorrect Username or Password. Please try again.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Incorrect Username or Password. Please try again.");
                        }
                    }
                }
            }
        }

        public void ClearPassword()
        {
            passwordBox.Text = string.Empty;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AuthenticateUser();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void passwordBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
