namespace PSO2PatchManager
{
    partial class installStoryPatch
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
            this.label2 = new System.Windows.Forms.Label();
            this.storyTransPatchDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.browseStoryPatch = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.closeAndInstallStoryPatch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(287, 26);
            this.label2.TabIndex = 4;
            this.label2.Text = "Please select the Translation Patch\r\nRAR file below.";
            // 
            // storyTransPatchDirectoryTextBox
            // 
            this.storyTransPatchDirectoryTextBox.Location = new System.Drawing.Point(15, 41);
            this.storyTransPatchDirectoryTextBox.Name = "storyTransPatchDirectoryTextBox";
            this.storyTransPatchDirectoryTextBox.ReadOnly = true;
            this.storyTransPatchDirectoryTextBox.Size = new System.Drawing.Size(284, 20);
            this.storyTransPatchDirectoryTextBox.TabIndex = 24;
            // 
            // browseStoryPatch
            // 
            this.browseStoryPatch.Font = new System.Drawing.Font("Courier New", 12F);
            this.browseStoryPatch.Location = new System.Drawing.Point(79, 67);
            this.browseStoryPatch.Name = "browseStoryPatch";
            this.browseStoryPatch.Size = new System.Drawing.Size(155, 27);
            this.browseStoryPatch.TabIndex = 25;
            this.browseStoryPatch.Text = "Browse";
            this.browseStoryPatch.UseVisualStyleBackColor = true;
            this.browseStoryPatch.Click += new System.EventHandler(this.browseStoryPatch_Click);
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label9.Location = new System.Drawing.Point(5, 112);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(310, 2);
            this.label9.TabIndex = 27;
            this.label9.Text = "label9";
            // 
            // closeAndInstallStoryPatch
            // 
            this.closeAndInstallStoryPatch.Font = new System.Drawing.Font("Courier New", 12F);
            this.closeAndInstallStoryPatch.Location = new System.Drawing.Point(79, 130);
            this.closeAndInstallStoryPatch.Name = "closeAndInstallStoryPatch";
            this.closeAndInstallStoryPatch.Size = new System.Drawing.Size(155, 27);
            this.closeAndInstallStoryPatch.TabIndex = 26;
            this.closeAndInstallStoryPatch.Text = "Install";
            this.closeAndInstallStoryPatch.UseVisualStyleBackColor = true;
            this.closeAndInstallStoryPatch.Click += new System.EventHandler(this.closeAndInstallStoryPatch_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(5, 174);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 2);
            this.label1.TabIndex = 28;
            this.label1.Text = "label1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(271, 26);
            this.label3.TabIndex = 29;
            this.label3.Text = "You can get the Story Translation\r\npatch from:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 222);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(222, 20);
            this.textBox1.TabIndex = 30;
            this.textBox1.Text = "http://arks-layer.com/";
            // 
            // installStoryPatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 257);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.closeAndInstallStoryPatch);
            this.Controls.Add(this.browseStoryPatch);
            this.Controls.Add(this.storyTransPatchDirectoryTextBox);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "installStoryPatch";
            this.Text = "Install Story Patch";
            this.Load += new System.EventHandler(this.installStoryPatch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox storyTransPatchDirectoryTextBox;
        private System.Windows.Forms.Button browseStoryPatch;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button closeAndInstallStoryPatch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
    }
}