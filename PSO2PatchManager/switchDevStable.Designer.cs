namespace PSO2PatchManager
{
    partial class switchDevStable
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
            this.label5 = new System.Windows.Forms.Label();
            this.explanationText = new System.Windows.Forms.Label();
            this.restartThisApp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(5, 79);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(365, 23);
            this.progressBar.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(5, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(370, 2);
            this.label5.TabIndex = 13;
            this.label5.Text = "label5";
            // 
            // explanationText
            // 
            this.explanationText.AutoSize = true;
            this.explanationText.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.explanationText.Location = new System.Drawing.Point(11, 9);
            this.explanationText.Name = "explanationText";
            this.explanationText.Size = new System.Drawing.Size(359, 52);
            this.explanationText.TabIndex = 11;
            this.explanationText.Text = "Switching your Version from %CURVER% to\r\n%NEXTVER%. The Patcher is now downloadin" +
    "g\r\nall the necessary files. When it\'s done it\r\nwill prompt you to restart this a" +
    "pplication.";
            // 
            // restartThisApp
            // 
            this.restartThisApp.Font = new System.Drawing.Font("Courier New", 12F);
            this.restartThisApp.Location = new System.Drawing.Point(52, 111);
            this.restartThisApp.Name = "restartThisApp";
            this.restartThisApp.Size = new System.Drawing.Size(281, 27);
            this.restartThisApp.TabIndex = 19;
            this.restartThisApp.Text = "Restart this application";
            this.restartThisApp.UseVisualStyleBackColor = true;
            this.restartThisApp.Click += new System.EventHandler(this.restartThisApp_Click);
            // 
            // switchDevStable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 155);
            this.ControlBox = false;
            this.Controls.Add(this.restartThisApp);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.explanationText);
            this.Controls.Add(this.progressBar);
            this.Name = "switchDevStable";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Switching...";
            this.Load += new System.EventHandler(this.switchDevStable_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label explanationText;
        private System.Windows.Forms.Button restartThisApp;
    }
}