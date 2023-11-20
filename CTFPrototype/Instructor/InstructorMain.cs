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
    }
}
