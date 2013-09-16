namespace PSO2PatchManager
{
    partial class PMSettings
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.mainSettings = new System.Windows.Forms.TabPage();
            this.pso2Notify3 = new System.Windows.Forms.CheckBox();
            this.transNotify3 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.saveSettings = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.transNotify2 = new System.Windows.Forms.CheckBox();
            this.pso2Notify2 = new System.Windows.Forms.CheckBox();
            this.transNotify1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pso2Notify1 = new System.Windows.Forms.CheckBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.pso2DirectoryTextBox = new System.Windows.Forms.TextBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.updaterSettings = new System.Windows.Forms.TabPage();
            this.checkForUpdates = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.doNotAskCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.titleLabel2 = new System.Windows.Forms.Label();
            this.miscSettings = new System.Windows.Forms.TabPage();
            this.usingVersion = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.titleLabel3 = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.mainSettings.SuspendLayout();
            this.updaterSettings.SuspendLayout();
            this.miscSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.mainSettings);
            this.tabControl.Controls.Add(this.updaterSettings);
            this.tabControl.Controls.Add(this.miscSettings);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(406, 353);
            this.tabControl.TabIndex = 0;
            // 
            // mainSettings
            // 
            this.mainSettings.BackColor = System.Drawing.Color.Transparent;
            this.mainSettings.Controls.Add(this.pso2Notify3);
            this.mainSettings.Controls.Add(this.transNotify3);
            this.mainSettings.Controls.Add(this.label6);
            this.mainSettings.Controls.Add(this.saveSettings);
            this.mainSettings.Controls.Add(this.label5);
            this.mainSettings.Controls.Add(this.label4);
            this.mainSettings.Controls.Add(this.label2);
            this.mainSettings.Controls.Add(this.transNotify2);
            this.mainSettings.Controls.Add(this.pso2Notify2);
            this.mainSettings.Controls.Add(this.transNotify1);
            this.mainSettings.Controls.Add(this.label1);
            this.mainSettings.Controls.Add(this.pso2Notify1);
            this.mainSettings.Controls.Add(this.browseButton);
            this.mainSettings.Controls.Add(this.pso2DirectoryTextBox);
            this.mainSettings.Controls.Add(this.titleLabel);
            this.mainSettings.Location = new System.Drawing.Point(4, 22);
            this.mainSettings.Name = "mainSettings";
            this.mainSettings.Padding = new System.Windows.Forms.Padding(3);
            this.mainSettings.Size = new System.Drawing.Size(398, 327);
            this.mainSettings.TabIndex = 0;
            this.mainSettings.Text = "Main Settings";
            // 
            // pso2Notify3
            // 
            this.pso2Notify3.AutoSize = true;
            this.pso2Notify3.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.pso2Notify3.Location = new System.Drawing.Point(27, 248);
            this.pso2Notify3.Name = "pso2Notify3";
            this.pso2Notify3.Size = new System.Drawing.Size(204, 17);
            this.pso2Notify3.TabIndex = 66;
            this.pso2Notify3.Text = "Don\'t notify me and don\'t do anything.";
            this.pso2Notify3.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.pso2Notify3.UseVisualStyleBackColor = true;
            this.pso2Notify3.CheckedChanged += new System.EventHandler(this.pso2Notify3_CheckedChanged);
            // 
            // transNotify3
            // 
            this.transNotify3.AutoSize = true;
            this.transNotify3.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.transNotify3.Location = new System.Drawing.Point(27, 154);
            this.transNotify3.Name = "transNotify3";
            this.transNotify3.Size = new System.Drawing.Size(204, 17);
            this.transNotify3.TabIndex = 65;
            this.transNotify3.Text = "Don\'t notify me and don\'t do anything.";
            this.transNotify3.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.transNotify3.UseVisualStyleBackColor = true;
            this.transNotify3.CheckedChanged += new System.EventHandler(this.transNotify3_CheckedChanged);
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Location = new System.Drawing.Point(3, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(390, 2);
            this.label6.TabIndex = 64;
            this.label6.Text = "label6";
            // 
            // saveSettings
            // 
            this.saveSettings.Location = new System.Drawing.Point(126, 285);
            this.saveSettings.Name = "saveSettings";
            this.saveSettings.Size = new System.Drawing.Size(117, 33);
            this.saveSettings.TabIndex = 63;
            this.saveSettings.Text = "Save Settings";
            this.saveSettings.UseVisualStyleBackColor = true;
            this.saveSettings.Click += new System.EventHandler(this.saveSettings_Click);
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(3, 273);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(390, 2);
            this.label5.TabIndex = 62;
            this.label5.Text = "label5";
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(3, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(390, 2);
            this.label4.TabIndex = 61;
            this.label4.Text = "label4";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(1, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(390, 2);
            this.label2.TabIndex = 60;
            this.label2.Text = "label2";
            // 
            // transNotify2
            // 
            this.transNotify2.AutoSize = true;
            this.transNotify2.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.transNotify2.Location = new System.Drawing.Point(27, 132);
            this.transNotify2.Name = "transNotify2";
            this.transNotify2.Size = new System.Drawing.Size(335, 17);
            this.transNotify2.TabIndex = 59;
            this.transNotify2.Text = "Don\'t notify me and automatically update the Translation Patches.";
            this.transNotify2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.transNotify2.UseVisualStyleBackColor = true;
            this.transNotify2.CheckedChanged += new System.EventHandler(this.transNotify2_CheckedChanged);
            // 
            // pso2Notify2
            // 
            this.pso2Notify2.AutoSize = true;
            this.pso2Notify2.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.pso2Notify2.Location = new System.Drawing.Point(27, 225);
            this.pso2Notify2.Name = "pso2Notify2";
            this.pso2Notify2.Size = new System.Drawing.Size(329, 17);
            this.pso2Notify2.TabIndex = 58;
            this.pso2Notify2.Text = "Don\'t notify me, but automatically revert the Translation Patches.";
            this.pso2Notify2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.pso2Notify2.UseVisualStyleBackColor = true;
            this.pso2Notify2.CheckedChanged += new System.EventHandler(this.pso2Notify2_CheckedChanged);
            // 
            // transNotify1
            // 
            this.transNotify1.AutoSize = true;
            this.transNotify1.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.transNotify1.Location = new System.Drawing.Point(9, 96);
            this.transNotify1.Name = "transNotify1";
            this.transNotify1.Size = new System.Drawing.Size(325, 30);
            this.transNotify1.TabIndex = 57;
            this.transNotify1.Text = "Notify me if an update for the Translation Patches are detected.\r\n(The  notificat" +
    "ion will also allow you to Update or Postpone.)";
            this.transNotify1.UseVisualStyleBackColor = true;
            this.transNotify1.CheckedChanged += new System.EventHandler(this.transNotify1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "PSO2 Directory:";
            // 
            // pso2Notify1
            // 
            this.pso2Notify1.AutoSize = true;
            this.pso2Notify1.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.pso2Notify1.Location = new System.Drawing.Point(9, 189);
            this.pso2Notify1.Name = "pso2Notify1";
            this.pso2Notify1.Size = new System.Drawing.Size(307, 30);
            this.pso2Notify1.TabIndex = 55;
            this.pso2Notify1.Text = "Notify me if an update for PSO2 is detected.\r\n(The  notification will also allow " +
    "you to Update or Postpone.)";
            this.pso2Notify1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.pso2Notify1.UseVisualStyleBackColor = true;
            this.pso2Notify1.CheckedChanged += new System.EventHandler(this.pso2Notify1_CheckedChanged);
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(309, 47);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 54;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // pso2DirectoryTextBox
            // 
            this.pso2DirectoryTextBox.Location = new System.Drawing.Point(9, 54);
            this.pso2DirectoryTextBox.Name = "pso2DirectoryTextBox";
            this.pso2DirectoryTextBox.ReadOnly = true;
            this.pso2DirectoryTextBox.Size = new System.Drawing.Size(294, 20);
            this.pso2DirectoryTextBox.TabIndex = 53;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(6, 8);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(47, 13);
            this.titleLabel.TabIndex = 52;
            this.titleLabel.Text = "Title";
            // 
            // updaterSettings
            // 
            this.updaterSettings.BackColor = System.Drawing.Color.Transparent;
            this.updaterSettings.Controls.Add(this.checkForUpdates);
            this.updaterSettings.Controls.Add(this.label7);
            this.updaterSettings.Controls.Add(this.doNotAskCheckBox);
            this.updaterSettings.Controls.Add(this.label3);
            this.updaterSettings.Controls.Add(this.titleLabel2);
            this.updaterSettings.Location = new System.Drawing.Point(4, 22);
            this.updaterSettings.Name = "updaterSettings";
            this.updaterSettings.Padding = new System.Windows.Forms.Padding(3);
            this.updaterSettings.Size = new System.Drawing.Size(398, 327);
            this.updaterSettings.TabIndex = 1;
            this.updaterSettings.Text = "Updater Settings";
            // 
            // checkForUpdates
            // 
            this.checkForUpdates.Location = new System.Drawing.Point(118, 285);
            this.checkForUpdates.Name = "checkForUpdates";
            this.checkForUpdates.Size = new System.Drawing.Size(134, 33);
            this.checkForUpdates.TabIndex = 69;
            this.checkForUpdates.Text = "Check For Updates Now";
            this.checkForUpdates.UseVisualStyleBackColor = true;
            this.checkForUpdates.Click += new System.EventHandler(this.checkForUpdates_Click);
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Location = new System.Drawing.Point(3, 273);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(390, 2);
            this.label7.TabIndex = 68;
            this.label7.Text = "label7";
            // 
            // doNotAskCheckBox
            // 
            this.doNotAskCheckBox.AutoSize = true;
            this.doNotAskCheckBox.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.doNotAskCheckBox.Location = new System.Drawing.Point(9, 37);
            this.doNotAskCheckBox.Name = "doNotAskCheckBox";
            this.doNotAskCheckBox.Size = new System.Drawing.Size(323, 17);
            this.doNotAskCheckBox.TabIndex = 67;
            this.doNotAskCheckBox.Text = "Do not ask me for verification when I choose to Skip a Version.";
            this.doNotAskCheckBox.UseVisualStyleBackColor = true;
            this.doNotAskCheckBox.CheckedChanged += new System.EventHandler(this.doNotAskCheckBox_CheckedChanged);
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(3, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(390, 2);
            this.label3.TabIndex = 66;
            this.label3.Text = "label3";
            // 
            // titleLabel2
            // 
            this.titleLabel2.AutoSize = true;
            this.titleLabel2.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel2.Location = new System.Drawing.Point(6, 8);
            this.titleLabel2.Name = "titleLabel2";
            this.titleLabel2.Size = new System.Drawing.Size(47, 13);
            this.titleLabel2.TabIndex = 65;
            this.titleLabel2.Text = "Title";
            // 
            // miscSettings
            // 
            this.miscSettings.BackColor = System.Drawing.Color.Transparent;
            this.miscSettings.Controls.Add(this.usingVersion);
            this.miscSettings.Controls.Add(this.label10);
            this.miscSettings.Controls.Add(this.label8);
            this.miscSettings.Controls.Add(this.titleLabel3);
            this.miscSettings.Location = new System.Drawing.Point(4, 22);
            this.miscSettings.Name = "miscSettings";
            this.miscSettings.Padding = new System.Windows.Forms.Padding(3);
            this.miscSettings.Size = new System.Drawing.Size(398, 327);
            this.miscSettings.TabIndex = 2;
            this.miscSettings.Text = "Misc. Settings";
            // 
            // usingVersion
            // 
            this.usingVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.usingVersion.FormattingEnabled = true;
            this.usingVersion.Items.AddRange(new object[] {
            "Stable Version",
            "Development Version"});
            this.usingVersion.Location = new System.Drawing.Point(91, 35);
            this.usingVersion.Name = "usingVersion";
            this.usingVersion.Size = new System.Drawing.Size(121, 21);
            this.usingVersion.TabIndex = 72;
            this.usingVersion.SelectedIndexChanged += new System.EventHandler(this.usingVersion_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 13);
            this.label10.TabIndex = 71;
            this.label10.Text = "Version Usage:";
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Location = new System.Drawing.Point(3, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(390, 2);
            this.label8.TabIndex = 69;
            this.label8.Text = "label8";
            // 
            // titleLabel3
            // 
            this.titleLabel3.AutoSize = true;
            this.titleLabel3.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel3.Location = new System.Drawing.Point(6, 8);
            this.titleLabel3.Name = "titleLabel3";
            this.titleLabel3.Size = new System.Drawing.Size(47, 13);
            this.titleLabel3.TabIndex = 68;
            this.titleLabel3.Text = "Title";
            // 
            // PMSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 352);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PMSettings";
            this.ShowInTaskbar = false;
            this.Text = "PSO2 Patch Manager Settings";
            this.Load += new System.EventHandler(this.PMSettings_Load);
            this.tabControl.ResumeLayout(false);
            this.mainSettings.ResumeLayout(false);
            this.mainSettings.PerformLayout();
            this.updaterSettings.ResumeLayout(false);
            this.updaterSettings.PerformLayout();
            this.miscSettings.ResumeLayout(false);
            this.miscSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage mainSettings;
        private System.Windows.Forms.CheckBox pso2Notify3;
        private System.Windows.Forms.CheckBox transNotify3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button saveSettings;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox transNotify2;
        private System.Windows.Forms.CheckBox pso2Notify2;
        private System.Windows.Forms.CheckBox transNotify1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox pso2Notify1;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox pso2DirectoryTextBox;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.TabPage updaterSettings;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label titleLabel2;
        private System.Windows.Forms.Button checkForUpdates;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox doNotAskCheckBox;
        private System.Windows.Forms.TabPage miscSettings;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label titleLabel3;
        private System.Windows.Forms.ComboBox usingVersion;
        private System.Windows.Forms.Label label10;

    }
}