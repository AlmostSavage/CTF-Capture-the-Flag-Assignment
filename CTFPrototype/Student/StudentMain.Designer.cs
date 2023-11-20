namespace CTFPrototype
{
    partial class StudentMain
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
            this.components = new System.ComponentModel.Container();
            this.forensicsButton = new System.Windows.Forms.Button();
            this.cryptographyButton = new System.Windows.Forms.Button();
            this.webButton = new System.Windows.Forms.Button();
            this.reverseButton = new System.Windows.Forms.Button();
            this.binaryButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.TimeKeeper = new System.Windows.Forms.Label();
            this.pointsLabel = new System.Windows.Forms.Label();
            this.questionBox = new System.Windows.Forms.TextBox();
            this.answerBox = new System.Windows.Forms.TextBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.logoutButton = new System.Windows.Forms.Button();
            this.rankButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // forensicsButton
            // 
            this.forensicsButton.Location = new System.Drawing.Point(15, 67);
            this.forensicsButton.Name = "forensicsButton";
            this.forensicsButton.Size = new System.Drawing.Size(125, 46);
            this.forensicsButton.TabIndex = 0;
            this.forensicsButton.Text = "Forensics";
            this.forensicsButton.UseVisualStyleBackColor = true;
            this.forensicsButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // cryptographyButton
            // 
            this.cryptographyButton.Location = new System.Drawing.Point(165, 67);
            this.cryptographyButton.Name = "cryptographyButton";
            this.cryptographyButton.Size = new System.Drawing.Size(125, 46);
            this.cryptographyButton.TabIndex = 1;
            this.cryptographyButton.Text = "Cryptography";
            this.cryptographyButton.UseVisualStyleBackColor = true;
            this.cryptographyButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // webButton
            // 
            this.webButton.Location = new System.Drawing.Point(313, 67);
            this.webButton.Name = "webButton";
            this.webButton.Size = new System.Drawing.Size(125, 46);
            this.webButton.TabIndex = 2;
            this.webButton.Text = "Web Exploitation";
            this.webButton.UseVisualStyleBackColor = true;
            this.webButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // reverseButton
            // 
            this.reverseButton.Location = new System.Drawing.Point(618, 67);
            this.reverseButton.Name = "reverseButton";
            this.reverseButton.Size = new System.Drawing.Size(125, 46);
            this.reverseButton.TabIndex = 3;
            this.reverseButton.Text = "Reverse Engineering";
            this.reverseButton.UseVisualStyleBackColor = true;
            this.reverseButton.Click += new System.EventHandler(this.button4_Click);
            // 
            // binaryButton
            // 
            this.binaryButton.Location = new System.Drawing.Point(468, 67);
            this.binaryButton.Name = "binaryButton";
            this.binaryButton.Size = new System.Drawing.Size(125, 46);
            this.binaryButton.TabIndex = 4;
            this.binaryButton.Text = "Binary Exploitation";
            this.binaryButton.UseVisualStyleBackColor = true;
            this.binaryButton.Click += new System.EventHandler(this.button5_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // TimeKeeper
            // 
            this.TimeKeeper.AutoSize = true;
            this.TimeKeeper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TimeKeeper.Location = new System.Drawing.Point(313, 444);
            this.TimeKeeper.Name = "TimeKeeper";
            this.TimeKeeper.Size = new System.Drawing.Size(49, 14);
            this.TimeKeeper.TabIndex = 5;
            this.TimeKeeper.Text = "Time: 0";
            this.TimeKeeper.Click += new System.EventHandler(this.TimeKeeper_Click);
            // 
            // pointsLabel
            // 
            this.pointsLabel.AutoSize = true;
            this.pointsLabel.Location = new System.Drawing.Point(87, 444);
            this.pointsLabel.Name = "pointsLabel";
            this.pointsLabel.Size = new System.Drawing.Size(59, 12);
            this.pointsLabel.TabIndex = 6;
            this.pointsLabel.Text = "Points: 0";
            this.pointsLabel.Click += new System.EventHandler(this.PointTracker_Click);
            // 
            // questionBox
            // 
            this.questionBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.questionBox.Location = new System.Drawing.Point(65, 161);
            this.questionBox.Multiline = true;
            this.questionBox.Name = "questionBox";
            this.questionBox.ReadOnly = true;
            this.questionBox.Size = new System.Drawing.Size(628, 116);
            this.questionBox.TabIndex = 7;
            this.questionBox.Visible = false;
            this.questionBox.TextChanged += new System.EventHandler(this.questionBox_TextChanged);
            // 
            // answerBox
            // 
            this.answerBox.Location = new System.Drawing.Point(65, 346);
            this.answerBox.Name = "answerBox";
            this.answerBox.Size = new System.Drawing.Size(528, 21);
            this.answerBox.TabIndex = 8;
            this.answerBox.Visible = false;
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(618, 344);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(75, 23);
            this.submitButton.TabIndex = 9;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Visible = false;
            // 
            // logoutButton
            // 
            this.logoutButton.Location = new System.Drawing.Point(649, 433);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(94, 35);
            this.logoutButton.TabIndex = 10;
            this.logoutButton.Text = "Log out";
            this.logoutButton.UseVisualStyleBackColor = true;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // rankButton
            // 
            this.rankButton.Enabled = false;
            this.rankButton.Location = new System.Drawing.Point(468, 433);
            this.rankButton.Name = "rankButton";
            this.rankButton.Size = new System.Drawing.Size(104, 35);
            this.rankButton.TabIndex = 11;
            this.rankButton.Text = "Ranking";
            this.rankButton.UseVisualStyleBackColor = true;
            this.rankButton.Click += new System.EventHandler(this.rankButton_Click_1);
            // 
            // StudentMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 518);
            this.Controls.Add(this.rankButton);
            this.Controls.Add(this.logoutButton);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.answerBox);
            this.Controls.Add(this.questionBox);
            this.Controls.Add(this.pointsLabel);
            this.Controls.Add(this.TimeKeeper);
            this.Controls.Add(this.binaryButton);
            this.Controls.Add(this.reverseButton);
            this.Controls.Add(this.webButton);
            this.Controls.Add(this.cryptographyButton);
            this.Controls.Add(this.forensicsButton);
            this.Name = "StudentMain";
            this.Text = "Capture The Flag (CTF) Practice";
            this.Load += new System.EventHandler(this.Tabs_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button forensicsButton;
        private System.Windows.Forms.Button cryptographyButton;
        private System.Windows.Forms.Button webButton;
        private System.Windows.Forms.Button reverseButton;
        private System.Windows.Forms.Button binaryButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label TimeKeeper;
        private System.Windows.Forms.Label pointsLabel;
        private System.Windows.Forms.TextBox questionBox;
        private System.Windows.Forms.TextBox answerBox;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Button logoutButton;
        private System.Windows.Forms.Button rankButton;
    }
}