namespace CTFPrototype
{
    partial class Signup
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
            this.signupUsername = new System.Windows.Forms.TextBox();
            this.signupPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.signupButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // signupUsername
            // 
            this.signupUsername.Location = new System.Drawing.Point(280, 106);
            this.signupUsername.Name = "signupUsername";
            this.signupUsername.Size = new System.Drawing.Size(316, 21);
            this.signupUsername.TabIndex = 0;
            // 
            // signupPassword
            // 
            this.signupPassword.Location = new System.Drawing.Point(280, 221);
            this.signupPassword.Name = "signupPassword";
            this.signupPassword.PasswordChar = '*';
            this.signupPassword.Size = new System.Drawing.Size(316, 21);
            this.signupPassword.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(213, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(213, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password: ";
            // 
            // signupButton
            // 
            this.signupButton.Location = new System.Drawing.Point(681, 218);
            this.signupButton.Name = "signupButton";
            this.signupButton.Size = new System.Drawing.Size(75, 23);
            this.signupButton.TabIndex = 4;
            this.signupButton.Text = "Sign Up";
            this.signupButton.UseVisualStyleBackColor = true;
            // 
            // Signup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.signupButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.signupPassword);
            this.Controls.Add(this.signupUsername);
            this.Name = "Signup";
            this.Text = "Signup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox signupUsername;
        private System.Windows.Forms.TextBox signupPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button signupButton;
    }
}