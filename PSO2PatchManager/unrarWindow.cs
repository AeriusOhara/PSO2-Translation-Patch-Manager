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
using SevenZipLib;
using System.Threading;

namespace PSO2PatchManager
{
    public partial class unrarWindow : Form
    {
        public unrarWindow()
        {
            InitializeComponent();
        }

        private void unrarWindow_Load(object sender, EventArgs e)
        {
            // Set Form location
            this.Location = new Point((MyGlobals.formX + 10), (MyGlobals.formY + 30));

            if (MyGlobals.rarType == "LARGE")
            {
                actionLabel.Text = "Extracting Large Patch";
            }
            else if (MyGlobals.rarType == "SMALL")
            {
                actionLabel.Text = "Extracting Small Patch";
            }
            else if (MyGlobals.rarType == "STORY")
            {
                actionLabel.Text = "Extracting Story Patch";
            }
            else
            {
                MessageBox.Show("Derp?\n\nArchive Type came back as: " + MyGlobals.rarType + " (was especting .zip or .rar)");
            }

            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;

            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            if (bw.IsBusy != true)
            {
                //MessageBox.Show("Background worker is not busy!");
                bw.RunWorkerAsync();
            }
            else
            {
                //MessageBox.Show("Guess the background worker must be busy? ._.");
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            //MessageBox.Show("We're in!");
            BackgroundWorker worker = sender as BackgroundWorker;
            using (SevenZipArchive archive = new SevenZipArchive(MyGlobals.rarFile))
            {
                int i = 0;
                int max = archive.Count();
                //MessageBox.Show("Max: " + max);
                int curPercent = 0;
                foreach (ArchiveEntry entry in archive)
                {
                    //entry.Extract(MyGlobals.rarExtractToLocation);
                    try
                    {
                        entry.Extract(MyGlobals.workDirectory + "data\\patch\\temp\\");
                    }
                    catch(SevenZipException ex)
                    {
                        MessageBox.Show("Error:\n" + ex);
                    }
                        
                    // Increment & update stats as well as progress bar
                    i++;
                    // This operating will only get to 50%, and the file transfer will be the other 50%
                    curPercent = (((i *100)/max)/ 2);


                    // Update the UI
                    if ((worker.CancellationPending == true))
                    {
                        e.Cancel = true;
                        break;
                    }
                    else
                    {
                        // Perform a time consuming operation and report progress.
                        System.Threading.Thread.Sleep(50);
                        worker.ReportProgress(curPercent);
                    }
                }

                int lastPercent = curPercent;

                //DirectoryInfo dir = new DirectoryInfo(MyGlobals.workDirectory + "data\\patch\\temp\\");
                String[] allfiles = System.IO.Directory.GetFiles(MyGlobals.workDirectory + "data\\patch\\temp\\", "*.*", System.IO.SearchOption.AllDirectories);
                max = allfiles.Length;
                i = 0;
                foreach(string file in allfiles)
                {
                    //MessageBox.Show("Moving: " + file + "\nTo: " + MyGlobals.rarExtractToLocation + Path.GetFileName(file));
                    Directory.Move(file, MyGlobals.rarExtractToLocation + Path.GetFileName(file));

                    i++;
                    curPercent = (((i *100)/max)/ 2);
                    curPercent += lastPercent;

                    // Update the UI
                    if ((worker.CancellationPending == true))
                    {
                        e.Cancel = true;
                        break;
                    }
                    else
                    {
                        // Perform a time consuming operation and report progress.
                        System.Threading.Thread.Sleep(50);
                        worker.ReportProgress(curPercent);
                    }
                }
            }

            //MessageBox.Show("Unrar Complete");
            //this.Close();
        }
        
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar.Value = e.ProgressPercentage;
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
                // Done unrarring

                // Delete old rar
                System.IO.DirectoryInfo folderInformation = new DirectoryInfo(MyGlobals.workDirectory + "data\\patch\\temp");
                foreach (FileInfo file in folderInformation.GetFiles())
                {
                    // If any files were found, delete them all
                    file.Delete();
                }

                // Close the window
                this.Close();
            }
        }
    }
}
