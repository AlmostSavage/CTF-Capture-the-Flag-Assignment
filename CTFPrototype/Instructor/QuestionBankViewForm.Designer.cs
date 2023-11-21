namespace CTFPrototype.Instructor
{
    partial class QuestionBankViewForm
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
            this.questionsGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.questionsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // questionsGrid
            // 
            this.questionsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.questionsGrid.Location = new System.Drawing.Point(12, 12);
            this.questionsGrid.Name = "questionsGrid";
            this.questionsGrid.RowTemplate.Height = 23;
            this.questionsGrid.Size = new System.Drawing.Size(776, 426);
            this.questionsGrid.TabIndex = 0;
            this.questionsGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // QuestionBankViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.questionsGrid);
            this.Name = "QuestionBankViewForm";
            this.Text = "QuestionBankViewForm";
            ((System.ComponentModel.ISupportInitialize)(this.questionsGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView questionsGrid;
    }
}