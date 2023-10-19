namespace CaptureTheFlagPrototype
{
    partial class Form1
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
            this.Forensics = new System.Windows.Forms.Button();
            this.Cryptography = new System.Windows.Forms.Button();
            this.WebExploitation = new System.Windows.Forms.Button();
            this.ReverseEngineering = new System.Windows.Forms.Button();
            this.BinaryExploitation = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Forensics
            // 
            this.Forensics.AccessibleName = "";
            this.Forensics.Location = new System.Drawing.Point(15, 15);
            this.Forensics.Name = "Forensics";
            this.Forensics.Size = new System.Drawing.Size(80, 45);
            this.Forensics.TabIndex = 0;
            this.Forensics.Text = "Forensics";
            this.Forensics.UseVisualStyleBackColor = true;
            this.Forensics.Click += new System.EventHandler(this.button1_Click);
            // 
            // Cryptography
            // 
            this.Cryptography.Location = new System.Drawing.Point(115, 15);
            this.Cryptography.Name = "Cryptography";
            this.Cryptography.Size = new System.Drawing.Size(80, 45);
            this.Cryptography.TabIndex = 1;
            this.Cryptography.Text = "Cryptography";
            this.Cryptography.UseVisualStyleBackColor = true;
            this.Cryptography.Click += new System.EventHandler(this.button2_Click);
            // 
            // WebExploitation
            // 
            this.WebExploitation.Location = new System.Drawing.Point(215, 15);
            this.WebExploitation.Name = "WebExploitation";
            this.WebExploitation.Size = new System.Drawing.Size(80, 45);
            this.WebExploitation.TabIndex = 2;
            this.WebExploitation.Text = "Web Exploitation";
            this.WebExploitation.UseVisualStyleBackColor = true;
            this.WebExploitation.Click += new System.EventHandler(this.button3_Click);
            // 
            // ReverseEngineering
            // 
            this.ReverseEngineering.Location = new System.Drawing.Point(315, 15);
            this.ReverseEngineering.Name = "ReverseEngineering";
            this.ReverseEngineering.Size = new System.Drawing.Size(80, 45);
            this.ReverseEngineering.TabIndex = 3;
            this.ReverseEngineering.Text = "Reverse Engineering";
            this.ReverseEngineering.UseVisualStyleBackColor = true;
            this.ReverseEngineering.Click += new System.EventHandler(this.button4_Click);
            // 
            // BinaryExploitation
            // 
            this.BinaryExploitation.Location = new System.Drawing.Point(415, 15);
            this.BinaryExploitation.Name = "BinaryExploitation";
            this.BinaryExploitation.Size = new System.Drawing.Size(80, 45);
            this.BinaryExploitation.TabIndex = 4;
            this.BinaryExploitation.Text = "Binary Exploitation";
            this.BinaryExploitation.UseVisualStyleBackColor = true;
            this.BinaryExploitation.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 79);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(850, 550);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "Example Text Box\r\n\r\n\r\n\r\n\r\nTest Answer Space\r\n\r\n\r\n\r\n\r\nExample Text Box\r\n\r\n\r\n\r\n\r\nTe" +
    "st Answer Box\r\n\r\n";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 661);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.BinaryExploitation);
            this.Controls.Add(this.ReverseEngineering);
            this.Controls.Add(this.WebExploitation);
            this.Controls.Add(this.Cryptography);
            this.Controls.Add(this.Forensics);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Forensics;
        private System.Windows.Forms.Button Cryptography;
        private System.Windows.Forms.Button WebExploitation;
        private System.Windows.Forms.Button ReverseEngineering;
        private System.Windows.Forms.Button BinaryExploitation;
        private System.Windows.Forms.TextBox textBox1;
    }
}

