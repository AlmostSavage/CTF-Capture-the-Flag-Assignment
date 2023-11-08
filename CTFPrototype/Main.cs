using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CTFPrototype
{
    public partial class Main : Form
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";
        private int points = 0;
        
        Timer timer = new Timer();
        private int countdownSeconds = 1800;
        private Timer countdownTimer = new Timer();
        public Main()
        {
            InitializeComponent();

            // Event handler for 5 buttons
            this.forensicsButton.Click += new System.EventHandler(this.CategoryButton_Click);
            this.cryptographyButton.Click += new System.EventHandler(this.CategoryButton_Click);
            this.webButton.Click += new System.EventHandler(this.CategoryButton_Click);
            this.binaryButton.Click += new System.EventHandler(this.CategoryButton_Click);
            this.reverseButton.Click += new System.EventHandler(this.CategoryButton_Click);

            forensicsButton.Tag = 1;
            cryptographyButton.Tag = 2;
            webButton.Tag = 3;
            binaryButton.Tag = 4;
            reverseButton.Tag = 5;

            // Submit button handler
            this.submitButton.Click += new EventHandler(this.submitButton_Click);

            // Startup Handler
            this.Load += new EventHandler(MainForm_Load);

            // Timer initialize
            timer.Interval = 1800000; // 1,800,000 milliseconds (30 minutes)
            timer.Enabled = true;
            timer.Tick += timer1_Tick;

            countdownTimer.Interval = 1000;
            countdownTimer.Tick += CountdownTimer_Tick;

            StartCountdown();
        }

        // Get Team ID for Points & Ranking purpose
        private void MainForm_Load(object sender, EventArgs e)
        {
            int teamId = GetTeamId();
            UpdatePointsLabel(teamId);
        }

        private int GetTeamId()
        {
            return 1; // Dummy value for demonstration
        }

        public void AddPoints(int pointsToAdd, int teamID, string reason)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Begin a transaction
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Update the Teams table
                        string updatePointsQuery = @"
                    UPDATE Teams 
                    SET Points = Points + @pointsToAdd 
                    WHERE TeamID = @teamId;
                ";
                        using (SqlCommand cmd = new SqlCommand(updatePointsQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@pointsToAdd", pointsToAdd);
                            cmd.Parameters.AddWithValue("@teamId", teamID);
                            cmd.ExecuteNonQuery();
                        }

                        // Insert a record into the ScoreTransactions table
                        string insertTransactionQuery = @"
                    INSERT INTO ScoreTransactions (TeamID, PointsChanged, Reason) 
                    VALUES (@teamId, @pointsToAdd, @reason);
                ";
                        using (SqlCommand cmd = new SqlCommand(insertTransactionQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@teamId", teamID);
                            cmd.Parameters.AddWithValue("@pointsToAdd", pointsToAdd);
                            cmd.Parameters.AddWithValue("@reason", reason);
                            cmd.ExecuteNonQuery();
                        }

                        // Commit the transaction
                        transaction.Commit();

                        // Reflect the change on the pointsLabel
                        UpdatePointsLabel(teamID);
                    }
                    catch
                    {
                        // If an error occurs, roll back the transaction
                        transaction.Rollback();
                        throw;
                    }
                }
            }


            /*
            points += pointsToAdd;
            pointsLabel.Text = "Points: " + points; // Update the label to display the updated points
            */
        }

        
        // Point Label Handler
        private void UpdatePointsLabel(int teamId)
        {
            // Assume this method is part of a form and pointsLabel is a control on that form.
            // This method should run on the UI thread because it interacts with the UI.

            if (InvokeRequired)
            {
                // If the current thread is not the UI thread, invoke the update on the UI thread
                Invoke(new Action(() => UpdatePointsLabel(teamId)));
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string selectPointsQuery = "SELECT Points FROM Teams WHERE TeamID = @teamId";

                using (SqlCommand cmd = new SqlCommand(selectPointsQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@teamId", teamId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pointsLabel.Text = $"Points: {reader["Points"].ToString()}";
                        }
                    }
                }
            }
        }

        // Event handler for all category buttons
        private void CategoryButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            int categoryID = Convert.ToInt32(clickedButton.Tag);

            currentQuestion = GetRandomQuestion(categoryID);
            Question question = GetRandomQuestion(categoryID);
            if (question != null)
            {
                questionBox.Text = question.QuestionText;
                questionBox.Visible = true;
                answerBox.Visible = true;
                submitButton.Visible = true;
                questionBox.Enabled = true;
                answerBox.Enabled = true;
                submitButton.Enabled = true;
                // Remember to clear the answer text box each time
                answerBox.Text = "";
            }
            else
            {
                questionBox.Text = "";
                answerBox.Text = "";
                questionBox.Enabled = false;
                answerBox.Enabled = false;
                submitButton.Enabled = false;
                MessageBox.Show("No more questions available for this category.");
            }
        }

        private Question GetRandomQuestion(int categoryID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT TOP 1 * FROM Questions WHERE CategoryID = @CategoryID ORDER BY NEWID()";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CategoryID", categoryID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Question
                            {
                                QuestionID = reader.GetInt32(reader.GetOrdinal("QuestionID")),
                                CategoryID = categoryID,
                                PointsWorth = reader.GetInt32(reader.GetOrdinal("PointsWorth")),
                                DifficultyLevel = reader.GetInt32(reader.GetOrdinal("DifficultyLevel")),
                                QuestionText = reader.GetString(reader.GetOrdinal("QuestionText")),
                                AnswerText = reader.GetString(reader.GetOrdinal("AnswerText"))
                            };
                        }
                    }
                }
            }
            return null;
        }

        // Define the Question class according to the table structure
        class Question
        {
            public int QuestionID { get; set; }
            public int CategoryID { get; set; }
            public int PointsWorth { get; set; }
            public int DifficultyLevel { get; set; }
            public string QuestionText { get; set; }
            public string AnswerText { get; set; }
        }

        private Question currentQuestion;
        private void submitButton_Click(object sender, EventArgs e)
        {
            string userAnswer = answerBox.Text.Trim(); // Get user's answer and trim it

            // Check if the answer is correct
            if (!string.IsNullOrEmpty(userAnswer) && userAnswer.Equals(currentQuestion.AnswerText, StringComparison.OrdinalIgnoreCase))
            {
                // Add points to the team's score
                AddPoints(currentQuestion.PointsWorth, 1, "Answer Question");

                // Show congratulation message
                MessageBox.Show("Congratulations! You've earned " + currentQuestion.PointsWorth + " points.", "Correct Answer", MessageBoxButtons.OK);

                // After closing the MessageBox, refresh the question
                CategoryButton_Click(sender, e);
            }
            else
            {
                // If the answer is incorrect, inform the user
                MessageBox.Show("Incorrect answer. Try again!", "Wrong Answer", MessageBoxButtons.OK);
                // Optionally clear the answer box
                answerBox.Text = "";
            }
            UpdatePointsLabel(1);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void StartCountdown()
        {
            countdownTimer.Start();
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (countdownSeconds > 0)
            {
                countdownSeconds--;
                UpdateCountdownLabel();
            }
            else
            {
                countdownTimer.Stop(); // Stop the timer when the countdown reaches zero
                MessageBox.Show("Countdown is over!");
            }
        }

        private void UpdateCountdownLabel()
        {
            // Update the Label to display the remaining time
            TimeKeeper.Text = $"Time Left: {countdownSeconds} seconds";
        }

        private void PointTracker_Click(object sender, EventArgs e)
        {

        }

        private void Tabs_Load(object sender, EventArgs e)
        {

        }

        private void questionBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
