namespace Tic_tac_toe
{
    partial class StatsForm
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
            this.ganeInfoDataGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.ganeInfoDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ganeInfoDataGrid
            // 
            this.ganeInfoDataGrid.AllowUserToAddRows = false;
            this.ganeInfoDataGrid.AllowUserToDeleteRows = false;
            this.ganeInfoDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ganeInfoDataGrid.Location = new System.Drawing.Point(0, 0);
            this.ganeInfoDataGrid.Name = "ganeInfoDataGrid";
            this.ganeInfoDataGrid.ReadOnly = true;
            this.ganeInfoDataGrid.Size = new System.Drawing.Size(539, 460);
            this.ganeInfoDataGrid.TabIndex = 0;
            // 
            // StatsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 461);
            this.Controls.Add(this.ganeInfoDataGrid);
            this.Name = "StatsForm";
            this.Text = "StatsForm";
            ((System.ComponentModel.ISupportInitialize)(this.ganeInfoDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ganeInfoDataGrid;
    }
}