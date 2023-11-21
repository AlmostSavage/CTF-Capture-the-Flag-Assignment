using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CTFPrototype.Instructor
{
    public partial class InstructorMain : Form
    {
        // Connect database
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";

        private readonly Login loginForm;
        private int userID;
        public InstructorMain(Login login, int userID)
        {
            InitializeComponent();
            this.loginForm = login;
            this.userID = userID;

            // Terminate program when form closed
            this.FormClosed += (sender, args) => Application.Exit();
        }

        private void InstructorMain_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            loginForm.ClearPassword();
            loginForm.Show();

            // Close the current (StudentMain) form
            this.Hide();
        }

        private void signupButton_Click(object sender, EventArgs e)
        {
            Signup signupForm = new Signup();
            signupForm.ShowDialog();
        }

        private void viewTransactionButton_Click(object sender, EventArgs e)
        {
            TransactionViewForm transactionViewForm = new TransactionViewForm();
            transactionViewForm.ShowDialog();
        }

        private void viewRankButton_Click(object sender, EventArgs e)
        {
            RankingViewForm rankingViewForm = new RankingViewForm();
            rankingViewForm.ShowDialog();
        }

        private void viewQuestionsButton_Click(object sender, EventArgs e)
        {
            QuestionBankViewForm questionBankViewForm = new QuestionBankViewForm();
            questionBankViewForm.ShowDialog();
        }
    }
}
