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
    public partial class Forensics : Form
    {
        private Random random = new Random();
        public Forensics()
        {
            InitializeComponent();
            this.Load += new EventHandler(MainForm_Load);
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
                    break;
                case 1:
                    label2.Visible = true;
                    break;
                case 2:
                    label3.Visible = true;
                    break;
                case 3:
                    label4.Visible = true;
                    break;
                case 4:
                    label5.Visible = true;
                    break;
                case 5:
                    label6.Visible = true;
                    break;
                case 6:
                    label7.Visible = true;
                    break;
                case 7:
                    label8.Visible = true;
                    break;
                case 8:
                    label9.Visible = true;
                    break;
                case 9:
                    label10.Visible = true;
                    break;
            }
        }
    }
}
