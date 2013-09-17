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
            this.Location = new Point((Globals.formX + 10), (Globals.formY + 30));

            if(Globals.rarType == "LARGE")
            {
                actionLabel.Text = "Extracting Large Patch";
            }
            else if(Globals.rarType == "SMALL")
            {
                actionLabel.Text = "Extracting Small Patch";
            }
            else if(Globals.rarType == "STORY")
            {
                actionLabel.Text = "Extracting Story Patch";
            }
            else
            {
                MessageBox.Show("Wrong archive format received!\n\nArchive Type came back as: " + Globals.rarType + " (was especting .zip or .rar)");
            }

            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;

            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            if(bw.IsBusy != true)
            {
                // If the background worker is not busy, lets get this thing started
                bw.RunWorkerAsync();
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            using (SevenZipArchive archive = new SevenZipArchive(Globals.rarFile))
            {
                int i = 0;
                int max = archive.Count();
                int curPercent = 0;
                foreach (ArchiveEntry entry in archive)
                {
                    try
                    {
                        entry.Extract(Globals.workDirectory + "data\\patch\\temp\\");
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
                    if((worker.CancellationPending == true))
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

                //DirectoryInfo dir = new DirectoryInfo(Globals.workDirectory + "data\\patch\\temp\\");
                String[] allfiles = System.IO.Directory.GetFiles(Globals.workDirectory + "data\\patch\\temp\\", "*.*", System.IO.SearchOption.AllDirectories);
                max = allfiles.Length;
                i = 0;
                foreach(string file in allfiles)
                {
                    //MessageBox.Show("Moving: " + file + "\nTo: " + Globals.rarExtractToLocation + Path.GetFileName(file));
                    Directory.Move(file, Globals.rarExtractToLocation + Path.GetFileName(file));

                    i++;
                    curPercent = (((i *100)/max)/ 2);
                    curPercent += lastPercent;

                    // Update the UI
                    if((worker.CancellationPending == true))
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
            if((e.Cancelled == true))
            {
                // Cancelled
            }

            else if(!(e.Error == null))
            {
                // An error occured
            }

            else
            {
                // Done unrarring

                // Delete old rar
                System.IO.DirectoryInfo folderInformation = new DirectoryInfo(Globals.workDirectory + "data\\patch\\temp");
                foreach(FileInfo file in folderInformation.GetFiles())
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
