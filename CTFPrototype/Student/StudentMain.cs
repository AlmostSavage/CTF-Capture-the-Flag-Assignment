using CTFPrototype.Instructor;
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
    public partial class StudentMain : Form
    {
        // Connect database
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";

        private List<Question> questionsInCategory;
        private int currentQuestionIndex = -1;

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

            hintButton.Visible = false;

            this.loginForm = login;
            this.userID = userID;
            this.Load += new EventHandler(StudentMain_Load);

            // Terminate the program when form closed
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
            public string Username;
        }

        private TeamInfo GetTeamInfo()
        {
            TeamInfo teamInfo = new TeamInfo { TeamID = -1, TeamName = string.Empty, Points = 0, Username = string.Empty };

            // Modified query to fetch the Username as well
            string query = @"
    SELECT t.TeamID, t.TeamName, t.Points, u.Username 
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
                        if (reader.Read())
                        {
                            teamInfo.TeamID = reader.IsDBNull(reader.GetOrdinal("TeamID")) ? -1 : reader.GetInt32(reader.GetOrdinal("TeamID"));
                            teamInfo.TeamName = reader.IsDBNull(reader.GetOrdinal("TeamName")) ? string.Empty : reader.GetString(reader.GetOrdinal("TeamName"));
                            teamInfo.Points = reader.IsDBNull(reader.GetOrdinal("Points")) ? 0 : reader.GetInt32(reader.GetOrdinal("Points"));
                            teamInfo.Username = reader.IsDBNull(reader.GetOrdinal("Username")) ? string.Empty : reader.GetString(reader.GetOrdinal("Username"));
                        }
                    }
                }
            }

            return teamInfo;
        }

        public void AddPoints(int questionId, int teamID, bool hintUsed)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Start a database transaction
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Fetch question details (including category name)
                        string fetchQuestionDetailsQuery = @"
                SELECT q.PointsWorth, c.CategoryName 
                FROM Questions q 
                INNER JOIN Categories c ON q.CategoryID = c.CategoryID 
                WHERE q.QuestionID = @QuestionId";

                        int pointsToAdd = 0;
                        string categoryName = "";
                        using (SqlCommand cmd = new SqlCommand(fetchQuestionDetailsQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@QuestionId", questionId);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    pointsToAdd = reader.GetInt32(reader.GetOrdinal("PointsWorth"));
                                    categoryName = reader.GetString(reader.GetOrdinal("CategoryName"));
                                }
                                else
                                {
                                    throw new InvalidOperationException("Question ID not found.");
                                }
                            }
                        }

                        // Deduct points if hint was used
                        int deduction = hintUsed ? Math.Min(5, pointsToAdd) : 0;
                        pointsToAdd -= deduction;

                        // Update the Teams table
                        string updatePointsQuery = "UPDATE Teams SET Points = Points + @pointsToAdd WHERE TeamID = @teamId";
                        using (SqlCommand cmd = new SqlCommand(updatePointsQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@pointsToAdd", pointsToAdd);
                            cmd.Parameters.AddWithValue("@teamId", teamID);
                            cmd.ExecuteNonQuery();
                        }

                        // Construct the reason string
                        string reason = $"User {userID} answered question {questionId} in category '{categoryName}'";
                        if (hintUsed)
                        {
                            reason += " using a hint";
                        }

                        // Insert a record into the ScoreTransactions table
                        string insertTransactionQuery = "INSERT INTO ScoreTransactions (TeamID, PointsChanged, Reason) VALUES (@teamId, @pointsToAdd, @reason)";
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
                            // Handle the case where there if no result
                            pointsLabel.Text = "Points: N/A";
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions, log and notify the user
                    MessageBox.Show($"An error occurred while retrieving points: {ex.Message}");
                }
            }
        }

        // Initialize timer
        private void StartTimer()
        {
            countdownSeconds = 1800;
            countdownTimer.Start();
        }

        // Event handler for all category buttons
        private void CategoryButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            int categoryID = Convert.ToInt32(clickedButton.Tag);

            LoadQuestionsForCategory(categoryID);
            UpdateAnsweredQuestions(userID, categoryID);
            DisplayCurrentQuestion();
        }

        // Get dict to store asked question
        private Dictionary<int, HashSet<int>> askedQuestions = new Dictionary<int, HashSet<int>>();

        // Get random question from database
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
                                AnswerText = reader.GetString(reader.GetOrdinal("AnswerText")),
                                HintText = reader.IsDBNull(reader.GetOrdinal("HintText")) ? string.Empty : reader.GetString(reader.GetOrdinal("HintText")),
                                HintWorth = reader.GetInt32(reader.GetOrdinal("HintWorth")),
                                HintUsed = false
                            };
                        }
                    }
                }
            }
            return null;
        }

        // Define the Question class according to the table2
        class Question
        {
            public int QuestionID { get; set; }
            public int CategoryID { get; set; }
            public int PointsWorth { get; set; }
            public int DifficultyLevel { get; set; }
            public string QuestionText { get; set; }
            public string AnswerText { get; set; }
            public string HintText { get; set; }
            public bool HintUsed { get; set; }
            public int HintWorth { get; set; }
            public bool AnsweredCorrectly { get; set; }
        }


        private void LoadQuestionsForCategory(int categoryID)
        {
            questionsInCategory = new List<Question>();
            currentQuestionIndex = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Questions WHERE CategoryID = @CategoryID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CategoryID", categoryID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            questionsInCategory.Add(new Question
                            {
                                QuestionID = reader.GetInt32(reader.GetOrdinal("QuestionID")),
                                CategoryID = categoryID,
                                PointsWorth = reader.GetInt32(reader.GetOrdinal("PointsWorth")),
                                DifficultyLevel = reader.GetInt32(reader.GetOrdinal("DifficultyLevel")),
                                QuestionText = reader.GetString(reader.GetOrdinal("QuestionText")),
                                AnswerText = reader.GetString(reader.GetOrdinal("AnswerText")),
                                HintText = reader.IsDBNull(reader.GetOrdinal("HintText")) ? string.Empty : reader.GetString(reader.GetOrdinal("HintText")),
                                HintWorth = reader.GetInt32(reader.GetOrdinal("HintWorth")),
                                HintUsed = false,
                                AnsweredCorrectly = false
                            });
                        }
                    }
                }
            }

            if (questionsInCategory.Count > 0)
            {
                currentQuestionIndex = 0; // Set to the first question
                DisplayCurrentQuestion();
            }
        }

        private void LoadNextQuestion()
        {
            currentQuestionIndex++;
            if (currentQuestionIndex < questionsInCategory.Count)
            {
                currentQuestion = questionsInCategory[currentQuestionIndex];
                DisplayCurrentQuestion();
            }
            else
            {
                MessageBox.Show("No more questions available in this category.");
                // Handle end of questions scenario
            }
        }

        private void UpdateAnsweredQuestions(int userID, int categoryID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
            SELECT QuestionID 
            FROM UserQuestionAnswers 
            WHERE UserID = @UserID AND AnsweredCorrectly = 1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int questionID = reader.GetInt32(0);
                            var question = questionsInCategory.FirstOrDefault(q => q.QuestionID == questionID);
                            if (question != null)
                            {
                                question.AnsweredCorrectly = true;
                                question.HintUsed = true; // Assume hint used if answered correctly
                            }
                        }
                    }
                }
            }
        }

        private void DisplayCurrentQuestion()
        {
            if (currentQuestionIndex >= 0 && currentQuestionIndex < questionsInCategory.Count)
            {
                currentQuestion = questionsInCategory[currentQuestionIndex];

                questionBox.Visible = true;
                answerBox.Visible = true;
                previousButton.Visible = true;
                nextButton.Visible = true;
                submitButton.Visible = true;
                hintButton.Visible = true;

                previousButton.Enabled = currentQuestionIndex > 0;
                nextButton.Enabled = currentQuestionIndex < questionsInCategory.Count - 1;

                questionBox.Text = currentQuestion.QuestionText;
                answerBox.Text = "";
                answerBox.Enabled = !currentQuestion.AnsweredCorrectly;
                submitButton.Enabled = !currentQuestion.AnsweredCorrectly;
                hintButton.Enabled = !(currentQuestion.AnsweredCorrectly || currentQuestion.HintUsed);

                hintButton.Enabled = !currentQuestion.HintUsed;

                TimeKeeper.Visible = true;
                StartTimer();
            }
            else
            {
                MessageBox.Show("No more questions available in this category.");

                // Reset and disable question and answer fields
                questionBox.Text = "";
                answerBox.Text = "";
                questionBox.Enabled = false;
                answerBox.Enabled = false;
                submitButton.Enabled = false;
                hintButton.Enabled = false;

                previousButton.Enabled = false;
                nextButton.Enabled = false;



                countdownTimer.Stop();
            }
        }

        private void LogCorrectAnswer(int userID, int questionID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Check if the record already exists
                string checkQuery = @"
            SELECT COUNT(1) 
            FROM UserQuestionAnswers 
            WHERE UserID = @UserID AND QuestionID = @QuestionID";

                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@UserID", userID);
                    checkCmd.Parameters.AddWithValue("@QuestionID", questionID);
                    int count = (int)checkCmd.ExecuteScalar();

                    // If record exists, update, otherwise insert
                    string sqlQuery;
                    if (count > 0)
                    {
                        sqlQuery = @"
                    UPDATE UserQuestionAnswers 
                    SET AnsweredCorrectly = 1 
                    WHERE UserID = @UserID AND QuestionID = @QuestionID";
                    }
                    else
                    {
                        sqlQuery = @"
                    INSERT INTO UserQuestionAnswers (UserID, QuestionID, AnsweredCorrectly) 
                    VALUES (@UserID, @QuestionID, 1)";
                    }

                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        cmd.Parameters.AddWithValue("@QuestionID", questionID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private Question currentQuestion;
        private void submitButton_Click(object sender, EventArgs e)
        {
            string userAnswer = answerBox.Text.Trim();

            // Retrieve the current user's team information
            TeamInfo userTeamInfo = GetTeamInfo();

            // Check if the answer is correct
            if (!string.IsNullOrEmpty(userAnswer) && userAnswer.Equals(currentQuestion.AnswerText, StringComparison.OrdinalIgnoreCase))
            {
                currentQuestion.AnsweredCorrectly = true;

                // Log correct answered question into database
                LogCorrectAnswer(userID, currentQuestion.QuestionID);

                // Calculate the adjusted points
                int adjustedPoints = currentQuestion.PointsWorth;
                if (currentQuestion.HintUsed)
                {
                    // Subtract the hint worth from the points
                    adjustedPoints = Math.Max(0, adjustedPoints - currentQuestion.HintWorth);
                }

                // Add points to the user's team score for the current question
                AddPoints(currentQuestion.QuestionID, userTeamInfo.TeamID, currentQuestion.HintUsed);

                // Show message with adjusted points
                MessageBox.Show("Congratulations! You've earned " + adjustedPoints + " points.", "Correct Answer", MessageBoxButtons.OK);

                answerBox.Enabled = false;
                submitButton.Enabled = false;
                hintButton.Enabled = false;

                answerBox.Text = "";
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
        

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (countdownSeconds > 0)
            {
                countdownSeconds--;
                UpdateCountdownLabel();
            }
            else
            {
                countdownTimer.Stop(); 
                MessageBox.Show("Countdown is over!");
            }
        }

        private void UpdateCountdownLabel()
        {
            // Update the Label to display the remaining time
            TimeKeeper.Text = $"Time: {countdownSeconds} Seconds";
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
            RankingViewForm rankingViewForm = new RankingViewForm();
            rankingViewForm.ShowDialog();
        }

        private void TimeKeeper_Click(object sender, EventArgs e)
        {

        }

        private void hintButton_Click(object sender, EventArgs e)
        {
            if (currentQuestion != null)
            {
                MessageBox.Show(currentQuestion.HintText);
                currentQuestion.HintUsed = true;
                hintButton.Enabled = false;
            }
        }

        private void welcomeLabel_Click(object sender, EventArgs e)
        {

        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            if (currentQuestionIndex > 0)
            {
                currentQuestionIndex--;
                UpdateAnsweredQuestions(userID, currentQuestion.CategoryID);
                DisplayCurrentQuestion();
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (currentQuestionIndex < questionsInCategory.Count - 1)
            {
                currentQuestionIndex++;
                UpdateAnsweredQuestions(userID, currentQuestion.CategoryID);
                DisplayCurrentQuestion();
            }
        }
    }
}
