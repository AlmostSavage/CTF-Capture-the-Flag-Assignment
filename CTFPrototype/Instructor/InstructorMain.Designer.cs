namespace CTFPrototype.Instructor
{
    partial class InstructorMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.logoutButton = new System.Windows.Forms.Button();
            this.viewTransactionButton = new System.Windows.Forms.Button();
            this.signupButton = new System.Windows.Forms.Button();
            this.viewRankButton = new System.Windows.Forms.Button();
            this.viewQuestionsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // logoutButton
            // 
            this.logoutButton.Location = new System.Drawing.Point(604, 361);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(75, 23);
            this.logoutButton.TabIndex = 0;
            this.logoutButton.Text = "Log Out";
            this.logoutButton.UseVisualStyleBackColor = true;
            this.logoutButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // viewTransactionButton
            // 
            this.viewTransactionButton.Location = new System.Drawing.Point(105, 106);
            this.viewTransactionButton.Name = "viewTransactionButton";
            this.viewTransactionButton.Size = new System.Drawing.Size(172, 44);
            this.viewTransactionButton.TabIndex = 1;
            this.viewTransactionButton.Text = "View Points Log";
            this.viewTransactionButton.UseVisualStyleBackColor = true;
            this.viewTransactionButton.Click += new System.EventHandler(this.viewTransactionButton_Click);
            // 
            // signupButton
            // 
            this.signupButton.Location = new System.Drawing.Point(407, 106);
            this.signupButton.Name = "signupButton";
            this.signupButton.Size = new System.Drawing.Size(172, 44);
            this.signupButton.TabIndex = 2;
            this.signupButton.Text = "Add Student";
            this.signupButton.UseVisualStyleBackColor = true;
            this.signupButton.Click += new System.EventHandler(this.signupButton_Click);
            // 
            // viewRankButton
            // 
            this.viewRankButton.Location = new System.Drawing.Point(105, 233);
            this.viewRankButton.Name = "viewRankButton";
            this.viewRankButton.Size = new System.Drawing.Size(172, 44);
            this.viewRankButton.TabIndex = 3;
            this.viewRankButton.Text = "View Ranking";
            this.viewRankButton.UseVisualStyleBackColor = true;
            this.viewRankButton.Click += new System.EventHandler(this.viewRankButton_Click);
            // 
            // viewQuestionsButton
            // 
            this.viewQuestionsButton.Location = new System.Drawing.Point(407, 233);
            this.viewQuestionsButton.Name = "viewQuestionsButton";
            this.viewQuestionsButton.Size = new System.Drawing.Size(172, 44);
            this.viewQuestionsButton.TabIndex = 4;
            this.viewQuestionsButton.Text = "View Questions";
            this.viewQuestionsButton.UseVisualStyleBackColor = true;
            this.viewQuestionsButton.Click += new System.EventHandler(this.viewQuestionsButton_Click);
            // 
            // InstructorMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.viewQuestionsButton);
            this.Controls.Add(this.viewRankButton);
            this.Controls.Add(this.signupButton);
            this.Controls.Add(this.viewTransactionButton);
            this.Controls.Add(this.logoutButton);
            this.Name = "InstructorMain";
            this.Text = "InstructorMain";
            this.Load += new System.EventHandler(this.InstructorMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button logoutButton;
        private System.Windows.Forms.Button viewTransactionButton;
        private System.Windows.Forms.Button signupButton;
        private System.Windows.Forms.Button viewRankButton;
        private System.Windows.Forms.Button viewQuestionsButton;
    }
}