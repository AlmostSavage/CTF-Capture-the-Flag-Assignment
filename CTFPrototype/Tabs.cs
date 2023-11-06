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
    public partial class Tabs : Form
    {

        private int points = 0;
        
        Timer timer = new Timer();
        private int countdownSeconds = 1800;
        private Timer countdownTimer = new Timer();
        public Tabs()
        {
            InitializeComponent();

            timer.Interval = 1800000; // 1,800,000 milliseconds (30 minutes)
            timer.Enabled = true;
            timer.Tick += timer1_Tick;

            countdownTimer.Interval = 1000;
            countdownTimer.Tick += CountdownTimer_Tick;

            StartCountdown();
        }

        public void AddPoints(int pointsToAdd)
        {
            points += pointsToAdd;
            pointsLabel.Text = "Points: " + points; // Update the label to display the updated points
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Forensics form2 = new Forensics(this);
            form2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cryptography form2 = new Cryptography(this);
            form2.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Web_Exploitation form2 = new Web_Exploitation(this);
            form2.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Reverse_Engineering form2 = new Reverse_Engineering(this);
            form2.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Binary_Exploitation form2 = new Binary_Exploitation(this);
            form2.ShowDialog();
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
    }
}
