namespace PSO2PatchManager
{
    partial class installRevertFiles
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
            this.filesOfLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.actionLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // filesOfLabel
            // 
            this.filesOfLabel.AutoSize = true;
            this.filesOfLabel.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filesOfLabel.Location = new System.Drawing.Point(52, 53);
            this.filesOfLabel.Name = "filesOfLabel";
            this.filesOfLabel.Size = new System.Drawing.Size(55, 13);
            this.filesOfLabel.TabIndex = 13;
            this.filesOfLabel.Text = "1/9999";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "File:";
            // 
            // actionLabel
            // 
            this.actionLabel.AutoSize = true;
            this.actionLabel.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actionLabel.Location = new System.Drawing.Point(9, 9);
            this.actionLabel.Name = "actionLabel";
            this.actionLabel.Size = new System.Drawing.Size(55, 13);
            this.actionLabel.TabIndex = 11;
            this.actionLabel.Text = "Action";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 25);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(183, 23);
            this.progressBar.TabIndex = 9;
            // 
            // installRevertFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 76);
            this.ControlBox = false;
            this.Controls.Add(this.filesOfLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.actionLabel);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "installRevertFiles";
            this.Text = "Processing...";
            this.Load += new System.EventHandler(this.installRevertFiles_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label filesOfLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label actionLabel;
        private System.Windows.Forms.ProgressBar progressBar;

    }
}