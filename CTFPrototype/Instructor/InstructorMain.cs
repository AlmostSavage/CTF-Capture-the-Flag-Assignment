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
        }

        private void InstructorMain_Load(object sender, EventArgs e)
        {

        }
    }
}
