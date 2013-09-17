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
    public partial class installRevertFiles : Form
    {
        string[] transFiles;
        int transFilesNum = 0;
        string transdir = "";
        string transorigdir = "";
        string pso2dir = "";
        int i;

        bool install = Globals.install;

        public installRevertFiles()
        {
            InitializeComponent();
        }

        private void installRevertFiles_Load(object sender, EventArgs e)
        {
            // Set Form location
            this.Location = new Point((Globals.formX + 10), (Globals.formY + 30));

            string currentDir = Environment.CurrentDirectory + "\\";
            if(install)
            {
                actionLabel.Text = "Installing " + Globals.action.ToLower() + " Patch";
                transFiles = Directory.GetFiles(Globals.workDirectory + "data\\patch\\" + Globals.action.ToLower()  + "\\files");
            }
            else
            {
                actionLabel.Text = "Reverting " + Globals.action.ToLower()  + " Patch";
                transFiles = Directory.GetFiles(Globals.workDirectory + "data\\patch\\" + Globals.action.ToLower()  + "\\files\\original");
            }
            transFilesNum = transFiles.Length;
            transdir = Globals.workDirectory + "data\\patch\\" + Globals.action.ToLower()  + "\\files\\";
            transorigdir = Globals.workDirectory + "data\\patch\\" + Globals.action.ToLower()  + "\\files\\original\\";
            pso2dir = Globals.pso2Directory + "data\\win32\\";

            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;

            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            if(bw.IsBusy != true)
            {
                bw.RunWorkerAsync();
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            
            int curPercent = 0;
            
            // Increment & update stats as well as progress bar
            if(install == true)
            {
                i = 0;
                foreach(string file in Directory.GetFiles(transdir))
                {
                    // Save the filename
                    string filename = Path.GetFileName(file);

                    // Move original to 'original' folder
                    File.Move(pso2dir + "\\" + filename, transorigdir + filename);

                    // Move translated to 'pso2 data directory'
                    File.Move(transdir + "\\" + filename, pso2dir + filename);

                    // Update the stats & progress bar
                    i++;
                    curPercent = ((i * 100) / transFilesNum);

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
            else
            {
                i = 0;
                foreach(string file in Directory.GetFiles(transorigdir))
                {
                    // Save the filename
                    string filename = Path.GetFileName(file);

                    // Move translated back to it's own root folder
                    File.Move(pso2dir + "\\" + filename, transdir + filename);

                    // Move original back to 'pso2 data directory'
                    File.Move(transorigdir + "\\" + filename, pso2dir + filename);

                    // Update the stats & progress bar
                    i++;
                    curPercent = ((i * 100) / transFilesNum);

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
        }
        
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar.Value = e.ProgressPercentage;
            filesOfLabel.Text = i + "/" + transFilesNum.ToString();
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
                this.Close();
            }
        }
    }
}
