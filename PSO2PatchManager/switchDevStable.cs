using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.IO;
using System.Net;

namespace PSO2PatchManager
{
    public partial class switchDevStable : Form
    {
        // Will be used for the file download & progress bar
        WebClient webClient;
        Stopwatch stopWatch = new Stopwatch();

        // Make the Download Queue
        List<DownloadQueue> downloadQueue = new List<DownloadQueue>();

        public switchDevStable()
        {
            InitializeComponent();
        }

        private void switchDevStable_Load(object sender, EventArgs e)
        {
            // Set Form location
            this.Location = new Point((MyGlobals.formX + 10), (MyGlobals.formY + 30));

            restartThisApp.Enabled = false;

            // Replace the text
            string channelShortName = "";
            string explanationTextStr = explanationText.Text;
            if (MyGlobals.usingVersion == 0)
            {
                explanationTextStr = explanationTextStr.Replace("%CURVER%", "Stable");
                explanationTextStr = explanationTextStr.Replace("%NEXTVER%", "Developer");
                explanationText.Text = explanationTextStr;
                File.WriteAllText(@MyGlobals.workDirectory + "using.dat", "1");
                channelShortName = "dev";
            }
            else
            {
                explanationTextStr = explanationTextStr.Replace("%CURVER%", "Developer");
                explanationTextStr = explanationTextStr.Replace("%NEXTVER%", "Stable");
                explanationText.Text = explanationTextStr;
                File.WriteAllText(@MyGlobals.workDirectory + "using.dat", "0");
                channelShortName = "stable";
            }

            // Temporary download path
            string temporaryDownloadingFilePath = "updates/updater/" + "update.zip";

            // Add the download to the queue
            downloadQueue.Add(new DownloadQueue
            {
                url = "http://aerius.no-ip.biz/pso2tpm/updater/" + channelShortName + "/update.zip",
                downloadToPath = temporaryDownloadingFilePath,
                fileName = "update.zip",
                fileType = "zip"
            });

            downloadFile();
        }

        public void downloadFile()
        {
            if (downloadQueue.Count() > 0)
            {
                using (webClient = new WebClient())
                {
                    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                    webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                    try
                    {
                        // The variable that will be holding the url address
                        Uri URL;

                        // Make sure the url starts with "http://"
                        if (!downloadQueue[0].url.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                        {
                            downloadQueue[0].url = "http://" + downloadQueue[0].url;
                        }

                        //MessageBox.Show("Value: " + urlAddress);
                        if (!Uri.TryCreate(downloadQueue[0].url, UriKind.Absolute, out URL))
                        {
                            MessageBox.Show("Bad URL! -> " + downloadQueue[0].url);
                        }

                        // Start the stopwatch which we will be using to calculate the download speed
                        stopWatch.Start();

                        // Start downloading the file
                        webClient.DownloadFileAsync(URL, downloadQueue[0].downloadToPath);

                        downloadQueue.RemoveAt(0);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                //MessageBox.Show("You are now up to date");
                //this.Close();
                restartThisApp.Enabled = true;
            }
        }


        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                // Update the progressbar percentage only when the value is not the same (to avoid updating the control constantly)
                if (progressBar.Value != e.ProgressPercentage)
                    progressBar.Value = e.ProgressPercentage;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            stopWatch.Reset();
            if (e.Cancelled == true)
            {
                // No cancelling allowed
            }
            else
            {
                downloadFile();
            }
        }

        private void restartThisApp_Click(object sender, EventArgs e)
        {
            Process pso2tpmboot = new Process();
            pso2tpmboot.StartInfo.FileName = "StarBoot.exe";
            pso2tpmboot.Start();
            Process.GetCurrentProcess().Kill();
        }
    }
}
