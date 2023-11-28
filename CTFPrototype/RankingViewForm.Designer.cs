namespace CTFPrototype.Instructor
{
    partial class RankingViewForm
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
            this.rankingGrid = new System.Windows.Forms.DataGridView();
            this.Ranking = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TeamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Points = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.rankingGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // rankingGrid
            // 
            this.rankingGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.rankingGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.rankingGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rankingGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Ranking,
            this.TeamName,
            this.Points});
            this.rankingGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rankingGrid.Location = new System.Drawing.Point(0, 0);
            this.rankingGrid.Name = "rankingGrid";
            this.rankingGrid.RowTemplate.Height = 23;
            this.rankingGrid.Size = new System.Drawing.Size(459, 450);
            this.rankingGrid.TabIndex = 0;
            this.rankingGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.rankingGrid_CellContentClick);
            // 
            // Ranking
            // 
            this.Ranking.FillWeight = 30F;
            this.Ranking.HeaderText = "Ranking";
            this.Ranking.Name = "Ranking";
            this.Ranking.ReadOnly = true;
            // 
            // TeamName
            // 
            this.TeamName.FillWeight = 60F;
            this.TeamName.HeaderText = "Team Name";
            this.TeamName.Name = "TeamName";
            // 
            // Points
            // 
            this.Points.FillWeight = 30F;
            this.Points.HeaderText = "Score";
            this.Points.Name = "Points";
            this.Points.ReadOnly = true;
            // 
            // RankingViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 450);
            this.Controls.Add(this.rankingGrid);
            this.Name = "RankingViewForm";
            this.Text = "RankingViewForm";
            this.Load += new System.EventHandler(this.RankingViewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rankingGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView rankingGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ranking;
        private System.Windows.Forms.DataGridViewTextBoxColumn TeamName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Points;
    }
}