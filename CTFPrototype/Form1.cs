﻿using System;
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
    public partial class Form1 : Form

    {
        private const string CorrectUsername = "admin";
        private const string CorrectPassword = "password";

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string enteredUsername = textBoxUsername.Text;
            string enteredPassword = textBoxPassword.Text;

            if (enteredUsername == CorrectUsername && enteredPassword == CorrectPassword)
            {
                Tabs form2 = new Tabs();
                form2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Incorrect Username or Password. Please try Again.");
            }
        }
    }
}
