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
        private const string CorrectUsername = "admin";
        private const string CorrectPassword = "password";

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
                                if (roleID == 1)
                                {
                                    InstructorMain instructorForm = new InstructorMain(userID);
                                    instructorForm.ShowDialog();
                                }
                                else if (roleID == 2)
                                {
                                    StudentMain studentForm = new StudentMain(userID);
                                    studentForm.ShowDialog();
                                }
                                this.Close(); // Close the login form after the main form is closed.
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
        /*
        private bool VerifyPasswordHash(string enteredPassword, String storedPasswordHash)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedPasswordHash);
        }
        */

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AuthenticateUser();
            /*
            string enteredUsername = usernameBox.Text;
            string enteredPassword = passwordBox.Text;

            if (enteredUsername == CorrectUsername && enteredPassword == CorrectPassword)
            {
                StudentMain form2 = new StudentMain();
                form2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Incorrect Username or Password. Please try Again.");
            }
            */
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
