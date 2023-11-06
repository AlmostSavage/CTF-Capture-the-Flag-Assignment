using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CTFPrototype
{
    public partial class Reverse_Engineering : Form
    {
        private bool hintUsed = false;

        private Tabs tabs;

        private string CorrectAnswer = "";

        private string CorrectAnswer1 = "Question 1";
        private string CorrectAnswer2 = "Question 2";
        private string CorrectAnswer3 = "Question 3";
        private string CorrectAnswer4 = "Question 4";
        private string CorrectAnswer5 = "Question 5";
        private string CorrectAnswer6 = "Question 6";
        private string CorrectAnswer7 = "Question 7";
        private string CorrectAnswer8 = "Question 8";
        private string CorrectAnswer9 = "Question 9";
        private string CorrectAnswer10 = "Question 10";

        private Random random = new Random();
        public Reverse_Engineering(Tabs tabs)
        {
            InitializeComponent();
            this.Load += new EventHandler(MainForm_Load);
            this.tabs = tabs;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String Input = textBox1.Text;

            if (Input == CorrectAnswer)
            {
                MessageBox.Show("Correct Answer");
                int pointsToAdd = 10;
                if (hintUsed == true)
                {
                    pointsToAdd -= 5;
                }
                tabs.AddPoints(pointsToAdd);

            }
            else
            {
                MessageBox.Show("Incorrect Answer");
            }

            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Generate a random number to select which label to show (from 0 to 9)
            int randomLabelIndex = random.Next(10);
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;

            // Make the randomly selected label visible
            switch (randomLabelIndex)
            {
                case 0:
                    label1.Visible = true;
                    CorrectAnswer = CorrectAnswer1;
                    break;
                case 1:
                    label2.Visible = true;
                    CorrectAnswer = CorrectAnswer2;
                    break;
                case 2:
                    label3.Visible = true;
                    CorrectAnswer = CorrectAnswer3;
                    break;
                case 3:
                    label4.Visible = true;
                    CorrectAnswer = CorrectAnswer4;
                    break;
                case 4:
                    label5.Visible = true;
                    CorrectAnswer = CorrectAnswer5;
                    break;
                case 5:
                    label6.Visible = true;
                    CorrectAnswer = CorrectAnswer6;
                    break;
                case 6:
                    label7.Visible = true;
                    CorrectAnswer = CorrectAnswer7;
                    break;
                case 7:
                    label8.Visible = true;
                    CorrectAnswer = CorrectAnswer8;
                    break;
                case 8:
                    label9.Visible = true;
                    CorrectAnswer = CorrectAnswer9;
                    break;
                case 9:
                    label10.Visible = true;
                    CorrectAnswer = CorrectAnswer10;
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hintUsed = true;
            if (label1.Visible == true)
            {
                MessageBox.Show("Question and Number");
            }
            if (label2.Visible == true)
            {
                MessageBox.Show("Question and Number");
            }
            if (label3.Visible == true)
            {
                MessageBox.Show("Question and Number");
            }
            if (label4.Visible == true)
            {
                MessageBox.Show("Question and Number");
            }
            if (label5.Visible == true)
            {
                MessageBox.Show("Question and Number");
            }
            if (label6.Visible == true)
            {
                MessageBox.Show("Question and Number");
            }
            if (label7.Visible == true)
            {
                MessageBox.Show("Question and Number");
            }
            if (label8.Visible == true)
            {
                MessageBox.Show("Question and Number");
            }
            if (label9.Visible == true)
            {
                MessageBox.Show("Question and Number");
            }
            if (label10.Visible == true)
            {
                MessageBox.Show("Question and Number");
            }
        }
    }
}
