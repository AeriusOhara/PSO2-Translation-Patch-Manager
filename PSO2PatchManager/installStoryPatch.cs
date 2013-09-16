using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace PSO2PatchManager
{
    public partial class installStoryPatch : Form
    {
        string storyTransFilePath = "";
        string storyTransFileName = "";
        bool encounteredError = false;
        public installStoryPatch()
        {
            InitializeComponent();
        }

        private void browseStoryPatch_Click(object sender, EventArgs e)
        {
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "Browse for the Translation Patch File";
            fDialog.Filter = "Story Patch File (*.zip,*.rar)|*.zip;*.rar";
            fDialog.InitialDirectory = @"C:\";

            if(fDialog.ShowDialog() == DialogResult.OK)
            {
                storyTransFilePath = fDialog.FileName.ToString();
                storyTransFileName = Path.GetFileName(storyTransFilePath);
                storyTransPatchDirectoryTextBox.Text = storyTransFilePath;

                // Enable the install button
                closeAndInstallStoryPatch.Enabled = true;
            }
            else
            {
                // If the install button is enabled
                if(closeAndInstallStoryPatch.Enabled == true)
                {
                    // Disable the Install button
                    closeAndInstallStoryPatch.Enabled = false;
                }
            }
        }

        private void closeAndInstallStoryPatch_Click(object sender, EventArgs e)
        {
            // Disable the buttons
            closeAndInstallStoryPatch.Enabled = false;
            closeAndInstallStoryPatch.Text = "Working...";
            browseStoryPatch.Enabled = false;
            browseStoryPatch.Text = "Working...";

            // Delete the files of the previous version of the
            // story patch, if found
            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = false;
            bw.WorkerReportsProgress = false;

            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            // Disable the controlbox
            this.ControlBox = false;

            if(bw.IsBusy != true)
            {
                bw.RunWorkerAsync();
            }
        }

        private void installStoryPatch_Load(object sender, EventArgs e)
        {
            // Set Form location
            this.Location = new Point((MyGlobals.formX + 10), (MyGlobals.formY + 30));

            // Disable the Install button by default
            closeAndInstallStoryPatch.Enabled = false;
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            
            // Delete all files in the translation directory, if found
            foreach (string file in Directory.GetFiles(MyGlobals.workDirectory + "data\\patch\\story\\files\\"))
            {
                // Save the filename
                string filename = Path.GetFileName(file);
                string fileLocation = MyGlobals.workDirectory + "data\\patch\\story\\files\\" + filename;

                try
                {
                    File.Delete(fileLocation);
                }
                catch(UnauthorizedAccessException)
                {
                    MessageBox.Show("Couldn't delete the file [" + fileLocation + "]\nMake sure the file is not locked.\n\nAlternatively you can browse to the below location and delete the file manually.");
                    encounteredError = true;
                    this.Close();
                }

                // Small break
                //System.Threading.Thread.Sleep(50);
            }

            // Delete the translation patch archive, if found
            foreach (string file in Directory.GetFiles(MyGlobals.workDirectory + "data\\patch\\story\\"))
            {
                // Save the filename
                string filename = Path.GetFileName(file);
                string fileLocation = MyGlobals.workDirectory + "data\\patch\\story\\" + filename;

                try
                {
                    File.Delete(fileLocation);
                }
                catch(UnauthorizedAccessException)
                {
                    MessageBox.Show("Couldn't delete the file [" + fileLocation + "]\nMake sure the file is not locked.\n\nAlternatively you can browse to the below location and delete the file manually.");
                    encounteredError = true;
                    this.Close();
                }

                // Small break
                //System.Threading.Thread.Sleep(50);
            }
        }
        
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //this.progressBar.Value = e.ProgressPercentage;
            //filesOfLabel.Text = i + "/" + transFilesNum.ToString();
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                // Cancelled
            }
            else if (!(e.Error == null))
            {
                // An error occured
            }
            else
            {
                // Done checking for a previous version and or deleting
                // Move the file if no errors were detected
                if(encounteredError == false)
                {
                    File.Move(storyTransFilePath, MyGlobals.workDirectory + "data\\patch\\story\\" + storyTransFileName);
                }

                // Set up a flag that'll trigger the story patch being
                // installed automatically
                MyGlobals.installStoryPatch = true;
                this.Close();
            }
        }
    }
}
