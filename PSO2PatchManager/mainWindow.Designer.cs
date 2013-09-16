namespace PSO2PatchManager
{
    partial class mainForm
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
            this.smallPatchInstallButton = new System.Windows.Forms.Button();
            this.smallPatchRevertButton = new System.Windows.Forms.Button();
            this.textLog = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.smallPatchStatus = new System.Windows.Forms.Label();
            this.largePatchStatus = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.largePatchRevertButton = new System.Windows.Forms.Button();
            this.largePatchInstallButton = new System.Windows.Forms.Button();
            this.storyPatchRevertButton = new System.Windows.Forms.Button();
            this.storyPatchInstallButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.clientVersionLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.runPSO2Button = new System.Windows.Forms.Button();
            this.settingsButton = new System.Windows.Forms.Button();
            this.versionFromServerLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.manualInstallStoryPatchButton = new System.Windows.Forms.Button();
            this.devChannelNotification = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.devChannelNotification)).BeginInit();
            this.SuspendLayout();
            // 
            // smallPatchInstallButton
            // 
            this.smallPatchInstallButton.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.smallPatchInstallButton.Location = new System.Drawing.Point(15, 72);
            this.smallPatchInstallButton.Name = "smallPatchInstallButton";
            this.smallPatchInstallButton.Size = new System.Drawing.Size(155, 27);
            this.smallPatchInstallButton.TabIndex = 0;
            this.smallPatchInstallButton.Text = "Install Patch";
            this.smallPatchInstallButton.UseVisualStyleBackColor = true;
            this.smallPatchInstallButton.Click += new System.EventHandler(this.installButton_Click);
            // 
            // smallPatchRevertButton
            // 
            this.smallPatchRevertButton.Font = new System.Drawing.Font("Courier New", 12F);
            this.smallPatchRevertButton.Location = new System.Drawing.Point(182, 72);
            this.smallPatchRevertButton.Name = "smallPatchRevertButton";
            this.smallPatchRevertButton.Size = new System.Drawing.Size(155, 27);
            this.smallPatchRevertButton.TabIndex = 1;
            this.smallPatchRevertButton.Text = "Revert Patch";
            this.smallPatchRevertButton.UseVisualStyleBackColor = true;
            this.smallPatchRevertButton.Click += new System.EventHandler(this.revertButton_Click);
            // 
            // textLog
            // 
            this.textLog.Font = new System.Drawing.Font("Courier New", 8F);
            this.textLog.Location = new System.Drawing.Point(15, 409);
            this.textLog.Multiline = true;
            this.textLog.Name = "textLog";
            this.textLog.ReadOnly = true;
            this.textLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textLog.Size = new System.Drawing.Size(322, 36);
            this.textLog.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Small Patch:";
            // 
            // smallPatchStatus
            // 
            this.smallPatchStatus.AutoSize = true;
            this.smallPatchStatus.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.smallPatchStatus.Location = new System.Drawing.Point(111, 56);
            this.smallPatchStatus.Name = "smallPatchStatus";
            this.smallPatchStatus.Size = new System.Drawing.Size(95, 13);
            this.smallPatchStatus.TabIndex = 4;
            this.smallPatchStatus.Text = "Checking...";
            // 
            // largePatchStatus
            // 
            this.largePatchStatus.AutoSize = true;
            this.largePatchStatus.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.largePatchStatus.Location = new System.Drawing.Point(111, 122);
            this.largePatchStatus.Name = "largePatchStatus";
            this.largePatchStatus.Size = new System.Drawing.Size(95, 13);
            this.largePatchStatus.TabIndex = 6;
            this.largePatchStatus.Text = "Checking...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Large Patch:";
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Location = new System.Drawing.Point(5, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(330, 2);
            this.label7.TabIndex = 11;
            this.label7.Text = "label7";
            // 
            // largePatchRevertButton
            // 
            this.largePatchRevertButton.Font = new System.Drawing.Font("Courier New", 12F);
            this.largePatchRevertButton.Location = new System.Drawing.Point(182, 138);
            this.largePatchRevertButton.Name = "largePatchRevertButton";
            this.largePatchRevertButton.Size = new System.Drawing.Size(155, 27);
            this.largePatchRevertButton.TabIndex = 13;
            this.largePatchRevertButton.Text = "Revert Patch";
            this.largePatchRevertButton.UseVisualStyleBackColor = true;
            this.largePatchRevertButton.Click += new System.EventHandler(this.largePatchRevertButton_Click);
            // 
            // largePatchInstallButton
            // 
            this.largePatchInstallButton.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.largePatchInstallButton.Location = new System.Drawing.Point(15, 138);
            this.largePatchInstallButton.Name = "largePatchInstallButton";
            this.largePatchInstallButton.Size = new System.Drawing.Size(155, 27);
            this.largePatchInstallButton.TabIndex = 12;
            this.largePatchInstallButton.Text = "Install Patch";
            this.largePatchInstallButton.UseVisualStyleBackColor = true;
            this.largePatchInstallButton.Click += new System.EventHandler(this.largePatchInstallButton_Click);
            // 
            // storyPatchRevertButton
            // 
            this.storyPatchRevertButton.Font = new System.Drawing.Font("Courier New", 12F);
            this.storyPatchRevertButton.Location = new System.Drawing.Point(182, 205);
            this.storyPatchRevertButton.Name = "storyPatchRevertButton";
            this.storyPatchRevertButton.Size = new System.Drawing.Size(155, 27);
            this.storyPatchRevertButton.TabIndex = 18;
            this.storyPatchRevertButton.Text = "Revert Patch";
            this.storyPatchRevertButton.UseVisualStyleBackColor = true;
            this.storyPatchRevertButton.Click += new System.EventHandler(this.storyRevertPatchButton_Click);
            // 
            // storyPatchInstallButton
            // 
            this.storyPatchInstallButton.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storyPatchInstallButton.Location = new System.Drawing.Point(15, 205);
            this.storyPatchInstallButton.Name = "storyPatchInstallButton";
            this.storyPatchInstallButton.Size = new System.Drawing.Size(155, 27);
            this.storyPatchInstallButton.TabIndex = 17;
            this.storyPatchInstallButton.Text = "Install Patch";
            this.storyPatchInstallButton.UseVisualStyleBackColor = true;
            this.storyPatchInstallButton.Click += new System.EventHandler(this.storyPatchInstallButton_Click);
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(5, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(330, 2);
            this.label5.TabIndex = 16;
            this.label5.Text = "label5";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 189);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Story Patch";
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label9.Location = new System.Drawing.Point(7, 280);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(330, 2);
            this.label9.TabIndex = 19;
            this.label9.Text = "label9";
            // 
            // clientVersionLabel
            // 
            this.clientVersionLabel.AutoSize = true;
            this.clientVersionLabel.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clientVersionLabel.Location = new System.Drawing.Point(175, 9);
            this.clientVersionLabel.Name = "clientVersionLabel";
            this.clientVersionLabel.Size = new System.Drawing.Size(95, 13);
            this.clientVersionLabel.TabIndex = 21;
            this.clientVersionLabel.Text = "Checking...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "PSO2 Client Version:";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(7, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(330, 2);
            this.label1.TabIndex = 22;
            this.label1.Text = "label1";
            // 
            // runPSO2Button
            // 
            this.runPSO2Button.Font = new System.Drawing.Font("Courier New", 12F);
            this.runPSO2Button.Location = new System.Drawing.Point(15, 293);
            this.runPSO2Button.Name = "runPSO2Button";
            this.runPSO2Button.Size = new System.Drawing.Size(322, 68);
            this.runPSO2Button.TabIndex = 23;
            this.runPSO2Button.Text = "Start PSO2 Launcher";
            this.runPSO2Button.UseVisualStyleBackColor = true;
            this.runPSO2Button.Click += new System.EventHandler(this.button1_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.Font = new System.Drawing.Font("Courier New", 12F);
            this.settingsButton.Location = new System.Drawing.Point(126, 373);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(104, 27);
            this.settingsButton.TabIndex = 24;
            this.settingsButton.Text = "Settings";
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // versionFromServerLabel
            // 
            this.versionFromServerLabel.AutoSize = true;
            this.versionFromServerLabel.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionFromServerLabel.Location = new System.Drawing.Point(175, 25);
            this.versionFromServerLabel.Name = "versionFromServerLabel";
            this.versionFromServerLabel.Size = new System.Drawing.Size(95, 13);
            this.versionFromServerLabel.TabIndex = 26;
            this.versionFromServerLabel.Text = "Checking...";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(12, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(167, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Version from Server:";
            // 
            // manualInstallStoryPatchButton
            // 
            this.manualInstallStoryPatchButton.Font = new System.Drawing.Font("Courier New", 12F);
            this.manualInstallStoryPatchButton.Location = new System.Drawing.Point(90, 243);
            this.manualInstallStoryPatchButton.Name = "manualInstallStoryPatchButton";
            this.manualInstallStoryPatchButton.Size = new System.Drawing.Size(175, 27);
            this.manualInstallStoryPatchButton.TabIndex = 27;
            this.manualInstallStoryPatchButton.Text = "Manual Install";
            this.manualInstallStoryPatchButton.UseVisualStyleBackColor = true;
            this.manualInstallStoryPatchButton.Click += new System.EventHandler(this.manualInstallStoryPatch_Click);
            // 
            // devChannelNotification
            // 
            this.devChannelNotification.Image = global::PSO2PatchManager.Properties.Resources.developer_channel_build;
            this.devChannelNotification.Location = new System.Drawing.Point(115, 451);
            this.devChannelNotification.Name = "devChannelNotification";
            this.devChannelNotification.Size = new System.Drawing.Size(135, 12);
            this.devChannelNotification.TabIndex = 28;
            this.devChannelNotification.TabStop = false;
            this.devChannelNotification.Visible = false;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 464);
            this.Controls.Add(this.devChannelNotification);
            this.Controls.Add(this.manualInstallStoryPatchButton);
            this.Controls.Add(this.versionFromServerLabel);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.runPSO2Button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.clientVersionLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.storyPatchRevertButton);
            this.Controls.Add(this.storyPatchInstallButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.largePatchRevertButton);
            this.Controls.Add(this.largePatchInstallButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.largePatchStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.smallPatchStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textLog);
            this.Controls.Add(this.smallPatchRevertButton);
            this.Controls.Add(this.smallPatchInstallButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PSO2 Translation Patch Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.resizedForm);
            ((System.ComponentModel.ISupportInitialize)(this.devChannelNotification)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button smallPatchInstallButton;
        private System.Windows.Forms.Button smallPatchRevertButton;
        private System.Windows.Forms.TextBox textLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label smallPatchStatus;
        private System.Windows.Forms.Label largePatchStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button largePatchRevertButton;
        private System.Windows.Forms.Button largePatchInstallButton;
        private System.Windows.Forms.Button storyPatchRevertButton;
        private System.Windows.Forms.Button storyPatchInstallButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label clientVersionLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button runPSO2Button;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.Label versionFromServerLabel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button manualInstallStoryPatchButton;
        private System.Windows.Forms.PictureBox devChannelNotification;
    }
}

