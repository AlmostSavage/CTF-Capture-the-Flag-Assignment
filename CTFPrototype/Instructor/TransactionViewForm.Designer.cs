namespace CTFPrototype.Instructor
{
    partial class TransactionViewForm
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
            this.transactionGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.transactionGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // transactionGrid
            // 
            this.transactionGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.transactionGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transactionGrid.Location = new System.Drawing.Point(0, 0);
            this.transactionGrid.Name = "transactionGrid";
            this.transactionGrid.RowTemplate.Height = 23;
            this.transactionGrid.Size = new System.Drawing.Size(800, 450);
            this.transactionGrid.TabIndex = 0;
            this.transactionGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // TransactionViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.transactionGrid);
            this.Name = "TransactionViewForm";
            this.Text = "TransactionViewForm";
            ((System.ComponentModel.ISupportInitialize)(this.transactionGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView transactionGrid;
    }
}