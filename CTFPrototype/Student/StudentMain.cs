﻿using Microsoft.Data.SqlClient;
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
    public partial class StudentMain : Form
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";
        private int points = 0;
        private readonly Login loginForm;

        Timer timer = new Timer();
        private TeamInfo teamInfo;
        private int countdownSeconds = 1800;
        private Timer countdownTimer = new Timer();

        private int userID;
        public StudentMain(Login login, int userID)
        {
            InitializeComponent();

            this.loginForm = login;
            this.userID = userID;
            this.Load += new EventHandler(StudentMain_Load);

            this.FormClosed += (sender, args) => Application.Exit();

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

            // Timer initialize
            timer.Interval = 1800000; // 1,800,000 milliseconds (30 minutes)
            timer.Enabled = true;
            timer.Tick += timer1_Tick;

            countdownTimer.Interval = 1000;
            countdownTimer.Tick += CountdownTimer_Tick;

        }
        private void StudentMain_Load(object sender, EventArgs e)
        {
            // Retrieve the team information when the form loads
            TeamInfo teamInfo = GetTeamInfo();

            int teamID = teamInfo.TeamID;
            string teamName = teamInfo.TeamName;
            int points = teamInfo.Points;

            this.teamInfo = GetTeamInfo();
            UpdatePointsLabel(teamID);

            TimeKeeper.Visible = false;
        }

        public struct TeamInfo
        {
            public int TeamID;
            public string TeamName;
            public int Points;
        }

        private TeamInfo GetTeamInfo()
        {
            TeamInfo teamInfo = new TeamInfo { TeamID = -1, TeamName = string.Empty, Points = 0 };

            // SQL query to fetch the TeamID, TeamName, and Points for the current user's team.
            string query = @"
            SELECT t.TeamID, t.TeamName, t.Points 
            FROM Users u 
            INNER JOIN Teams t ON u.TeamID = t.TeamID 
            WHERE u.UserID = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // If we have a result, populate the TeamInfo struct
                        {
                            teamInfo.TeamID = reader.IsDBNull(reader.GetOrdinal("TeamID")) ? -1 : reader.GetInt32(reader.GetOrdinal("TeamID"));
                            teamInfo.TeamName = reader.IsDBNull(reader.GetOrdinal("TeamName")) ? string.Empty : reader.GetString(reader.GetOrdinal("TeamName"));
                            teamInfo.Points = reader.IsDBNull(reader.GetOrdinal("Points")) ? 0 : reader.GetInt32(reader.GetOrdinal("Points"));
                        }
                    }
                }
            }

            return teamInfo;
        }

        public void AddPoints(int questionId, int teamID)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Begin a transaction
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Fetch points worth from the Questions table
                        string fetchPointsQuery = "SELECT PointsWorth FROM Questions WHERE QuestionID = @QuestionId";
                        int pointsToAdd = 0;
                        using (SqlCommand cmd = new SqlCommand(fetchPointsQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@QuestionId", questionId);
                            object result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                pointsToAdd = Convert.ToInt32(result);
                            }
                            else
                            {
                                throw new InvalidOperationException("Question ID not found.");
                            }
                        }

                        // Update the Teams table
                        string updatePointsQuery = "UPDATE Teams SET Points = Points + @pointsToAdd WHERE TeamID = @teamId";
                        using (SqlCommand cmd = new SqlCommand(updatePointsQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@pointsToAdd", pointsToAdd);
                            cmd.Parameters.AddWithValue("@teamId", teamID);
                            cmd.ExecuteNonQuery();
                        }

                        // Insert a record into the ScoreTransactions table
                        string insertTransactionQuery = "INSERT INTO ScoreTransactions (TeamID, PointsChanged, Reason) VALUES (@teamId, @pointsToAdd, 'Answered Question')";
                        using (SqlCommand cmd = new SqlCommand(insertTransactionQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@teamId", teamID);
                            cmd.Parameters.AddWithValue("@pointsToAdd", pointsToAdd);
                            cmd.ExecuteNonQuery();
                        }

                        // Commit the transaction
                        transaction.Commit();

                        // Reflect the change on the pointsLabel
                        UpdatePointsLabel(teamID);
                    }
                    catch (Exception ex)
                    {
                        // If an error occurs, roll back the transaction
                        transaction.Rollback();
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
            }
        }


        // Point Label Handler
        private void UpdatePointsLabel(int teamId)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";
            string query = "SELECT Points FROM Teams WHERE TeamID = @TeamId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Use parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@TeamId", teamId);

                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            // Update the points label with the points from the database
                            pointsLabel.Text = $"Points: {result}";
                        }
                        else
                        {
                            // Handle the case where there is no result (e.g., invalid teamId)
                            pointsLabel.Text = "Points: N/A";
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions, possibly log them and inform the user
                    MessageBox.Show($"An error occurred while retrieving points: {ex.Message}");
                }
            }
        }

        private void StartTimer()
        {
            countdownSeconds = 1800; // Reset countdown time to 30 minutes
            countdownTimer.Start();
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

                // Show and start timer
                TimeKeeper.Visible = true;
                StartTimer();
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

            // Retrieve the current user's team information
            TeamInfo userTeamInfo = GetTeamInfo(); // Fetches team info based on the class-level userID

            // Check if the answer is correct
            if (!string.IsNullOrEmpty(userAnswer) && userAnswer.Equals(currentQuestion.AnswerText, StringComparison.OrdinalIgnoreCase))
            {
                // Add points to the user's team score for the current question
                AddPoints(currentQuestion.QuestionID, userTeamInfo.TeamID);
                MessageBox.Show("Congratulations! You've earned " + currentQuestion.PointsWorth + " points.", "Correct Answer", MessageBoxButtons.OK);

                // After closing the MessageBox, refresh the question
                CategoryButton_Click(sender, e);
            }
            else
            {
                // If the answer is incorrect, inform the user
                MessageBox.Show("Incorrect answer. Try again!", "Wrong Answer", MessageBoxButtons.OK);
            }

            // Update the points label for the user's team
            UpdatePointsLabel(userTeamInfo.TeamID);
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
            // Stop the timer
            countdownTimer.Stop();

            // Disable text fields and the submit button
            questionBox.Enabled = false;
            answerBox.Enabled = false;
            submitButton.Enabled = false;

            // Show a message to the user
            MessageBox.Show("Time is up!");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
        /*
        private void StartCountdown()
        {
            countdownTimer.Start();
        }
        */

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

        private void logoutButton_Click(object sender, EventArgs e)
        {
            loginForm.ClearPassword();
            loginForm.Show();

            // Close the current (StudentMain) form
            this.Hide();
        }

        private void rankButton_Click_1(object sender, EventArgs e)
        {
            TeamRankingForm rankingForm = new TeamRankingForm();
            rankingForm.ShowDialog();
        }
    }
}
