namespace PSO2PatchManager
{
    partial class downloadWindow
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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.smallPatchStatus = new System.Windows.Forms.Label();
            this.largePatchStatus = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.downloadingMessage = new System.Windows.Forms.Label();
            this.fileStatusLabel = new System.Windows.Forms.Label();
            this.downloadingTypeLabel = new System.Windows.Forms.Label();
            this.downloadingFileNameLabel = new System.Windows.Forms.Label();
            this.fileSizeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(15, 123);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(197, 23);
            this.progressBar.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Large Patch:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Small Patch:";
            // 
            // smallPatchStatus
            // 
            this.smallPatchStatus.AutoSize = true;
            this.smallPatchStatus.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.smallPatchStatus.Location = new System.Drawing.Point(111, 11);
            this.smallPatchStatus.Name = "smallPatchStatus";
            this.smallPatchStatus.Size = new System.Drawing.Size(55, 13);
            this.smallPatchStatus.TabIndex = 4;
            this.smallPatchStatus.Text = "STATUS";
            this.smallPatchStatus.Click += new System.EventHandler(this.smallPatchStatus_Click);
            // 
            // largePatchStatus
            // 
            this.largePatchStatus.AutoSize = true;
            this.largePatchStatus.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.largePatchStatus.Location = new System.Drawing.Point(111, 29);
            this.largePatchStatus.Name = "largePatchStatus";
            this.largePatchStatus.Size = new System.Drawing.Size(55, 13);
            this.largePatchStatus.TabIndex = 3;
            this.largePatchStatus.Text = "STATUS";
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(2, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(230, 2);
            this.label5.TabIndex = 10;
            this.label5.Text = "label5";
            // 
            // downloadingMessage
            // 
            this.downloadingMessage.AutoSize = true;
            this.downloadingMessage.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadingMessage.Location = new System.Drawing.Point(12, 63);
            this.downloadingMessage.Name = "downloadingMessage";
            this.downloadingMessage.Size = new System.Drawing.Size(103, 13);
            this.downloadingMessage.TabIndex = 11;
            this.downloadingMessage.Text = "Downloading:";
            // 
            // fileStatusLabel
            // 
            this.fileStatusLabel.AutoSize = true;
            this.fileStatusLabel.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileStatusLabel.Location = new System.Drawing.Point(65, 151);
            this.fileStatusLabel.Name = "fileStatusLabel";
            this.fileStatusLabel.Size = new System.Drawing.Size(71, 13);
            this.fileStatusLabel.TabIndex = 12;
            this.fileStatusLabel.Text = "File 0/0";
            // 
            // downloadingTypeLabel
            // 
            this.downloadingTypeLabel.AutoSize = true;
            this.downloadingTypeLabel.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadingTypeLabel.Location = new System.Drawing.Point(111, 63);
            this.downloadingTypeLabel.Name = "downloadingTypeLabel";
            this.downloadingTypeLabel.Size = new System.Drawing.Size(79, 13);
            this.downloadingTypeLabel.TabIndex = 14;
            this.downloadingTypeLabel.Text = "L/S Patch";
            // 
            // downloadingFileNameLabel
            // 
            this.downloadingFileNameLabel.AutoSize = true;
            this.downloadingFileNameLabel.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadingFileNameLabel.Location = new System.Drawing.Point(12, 81);
            this.downloadingFileNameLabel.Name = "downloadingFileNameLabel";
            this.downloadingFileNameLabel.Size = new System.Drawing.Size(71, 13);
            this.downloadingFileNameLabel.TabIndex = 16;
            this.downloadingFileNameLabel.Text = "FileName";
            // 
            // fileSizeLabel
            // 
            this.fileSizeLabel.AutoSize = true;
            this.fileSizeLabel.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileSizeLabel.Location = new System.Drawing.Point(12, 100);
            this.fileSizeLabel.Name = "fileSizeLabel";
            this.fileSizeLabel.Size = new System.Drawing.Size(95, 13);
            this.fileSizeLabel.TabIndex = 18;
            this.fileSizeLabel.Text = "50mb / 50mb";
            // 
            // downloadWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 171);
            this.ControlBox = false;
            this.Controls.Add(this.fileSizeLabel);
            this.Controls.Add(this.downloadingFileNameLabel);
            this.Controls.Add(this.downloadingTypeLabel);
            this.Controls.Add(this.fileStatusLabel);
            this.Controls.Add(this.downloadingMessage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.smallPatchStatus);
            this.Controls.Add(this.largePatchStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "downloadWindow";
            this.Text = "Downloading...";
            this.Load += new System.EventHandler(this.downloadWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label smallPatchStatus;
        private System.Windows.Forms.Label largePatchStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label downloadingMessage;
        private System.Windows.Forms.Label fileStatusLabel;
        private System.Windows.Forms.Label downloadingTypeLabel;
        private System.Windows.Forms.Label downloadingFileNameLabel;
        private System.Windows.Forms.Label fileSizeLabel;
    }
}