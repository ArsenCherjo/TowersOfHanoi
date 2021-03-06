namespace TowersOfHanoi
{
    partial class TowerOfHanoi
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
            this.DiskCount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.bSolve = new System.Windows.Forms.Button();
            this.listMoves = new System.Windows.Forms.ListBox();
            this.lblMoves = new System.Windows.Forms.Label();
            this.lblCounter = new System.Windows.Forms.Label();
            this.Panel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.DiskCount)).BeginInit();
            this.SuspendLayout();
            // 
            // DiskCount
            // 
            this.DiskCount.Location = new System.Drawing.Point(123, 12);
            this.DiskCount.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.DiskCount.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.DiskCount.Name = "DiskCount";
            this.DiskCount.Size = new System.Drawing.Size(52, 20);
            this.DiskCount.TabIndex = 0;
            this.DiskCount.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.DiskCount.ValueChanged += new System.EventHandler(this.DiskCount_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Количество дисков";
            // 
            // bSolve
            // 
            this.bSolve.Location = new System.Drawing.Point(185, 9);
            this.bSolve.Name = "bSolve";
            this.bSolve.Size = new System.Drawing.Size(100, 23);
            this.bSolve.TabIndex = 2;
            this.bSolve.Text = "Решить";
            this.bSolve.UseVisualStyleBackColor = true;
            this.bSolve.Click += new System.EventHandler(this.BSolve_Click);
            // 
            // listMoves
            // 
            this.listMoves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listMoves.FormattingEnabled = true;
            this.listMoves.Location = new System.Drawing.Point(821, 38);
            this.listMoves.Name = "listMoves";
            this.listMoves.Size = new System.Drawing.Size(231, 394);
            this.listMoves.TabIndex = 4;
            // 
            // lblMoves
            // 
            this.lblMoves.AutoSize = true;
            this.lblMoves.Location = new System.Drawing.Point(818, 12);
            this.lblMoves.Name = "lblMoves";
            this.lblMoves.Size = new System.Drawing.Size(34, 13);
            this.lblMoves.TabIndex = 5;
            this.lblMoves.Text = "Ходы";
            // 
            // lblCounter
            // 
            this.lblCounter.AutoSize = true;
            this.lblCounter.Location = new System.Drawing.Point(291, 14);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(183, 13);
            this.lblCounter.TabIndex = 6;
            this.lblCounter.Text = "Наименьшее количество ходов {7}";
            // 
            // panel1
            // 
            this.Panel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel.Location = new System.Drawing.Point(15, 38);
            this.Panel.Name = "panel1";
            this.Panel.Size = new System.Drawing.Size(800, 400);
            this.Panel.TabIndex = 7;
            // 
            // TowerOfHanoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 451);
            this.Controls.Add(this.Panel);
            this.Controls.Add(this.lblCounter);
            this.Controls.Add(this.lblMoves);
            this.Controls.Add(this.listMoves);
            this.Controls.Add(this.bSolve);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DiskCount);
            this.Name = "TowerOfHanoi";
            this.Text = "TowerOfHanoi";
            ((System.ComponentModel.ISupportInitialize)(this.DiskCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown DiskCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bSolve;
        private System.Windows.Forms.ListBox listMoves;
        private System.Windows.Forms.Label lblMoves;
        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.Panel Panel;
    }
}

