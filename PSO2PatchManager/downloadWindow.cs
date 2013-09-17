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
    public partial class downloadWindow : Form
    {
        // Which file of how many we're downloading
        int downloadingFileOf;

        // Will be used for the file download & progress bar
        WebClient webClient;
        Stopwatch stopWatch = new Stopwatch();

        // Make the Download Queue
        List<DownloadQueue> downloadQueue = new List<DownloadQueue>();

        // Temporary download file storage, so when we cancel the update, it
        // will automatically delete the file
        string temporaryDownloadingFilePath;

        public downloadWindow()
        {
            InitializeComponent();
        }

        private void downloadWindow_Load(object sender, EventArgs e)
        {
            // Set Form location
            this.Location = new Point((Globals.formX + 10), (Globals.formY + 30));

            if(Globals.downloadLargePatch)
            {
                largePatchStatus.ForeColor = ColorTranslator.FromHtml("#FF3333");
                largePatchStatus.Text = "OUTDATED";
            }
            else
            {
                // File is up to date
                largePatchStatus.ForeColor = ColorTranslator.FromHtml("#0066CC");
                largePatchStatus.Text = "UP TO DATE";
            }

            if(Globals.downloadSmallPatch)
            {
                smallPatchStatus.ForeColor = ColorTranslator.FromHtml("#FF3333");
                smallPatchStatus.Text = "OUTDATED";
            }
            else
            {
                // File is up to date
                smallPatchStatus.ForeColor = ColorTranslator.FromHtml("#0066CC");
                smallPatchStatus.Text = "UP TO DATE";
            }

            // If we have to download the large patch
            if(Globals.downloadLargePatch)
            {
                // Delete old rar
                System.IO.DirectoryInfo folderInformation = new DirectoryInfo(Globals.workDirectory + "data\\patch\\large");
                foreach (FileInfo file in folderInformation.GetFiles())
                {
                    // If any files were found, delete them all
                    file.Delete();
                }

                // Delete old translated files
                folderInformation = new DirectoryInfo(Globals.workDirectory + "data\\patch\\large\\files");
                foreach(FileInfo file in folderInformation.GetFiles())
                {
                    // If any files were found, delete them all
                    file.Delete();
                }

                // The temporary download file path
                temporaryDownloadingFilePath = Globals.workDirectory + "data/patch/large/" + Globals.latestLargePatchFileName;

                // Add the download to the queue
                downloadQueue.Add(new DownloadQueue
                {
                    url = "http://hiigara.arghargh200.net/pso2/" + Globals.latestLargePatchFileName,
                    downloadToPath = temporaryDownloadingFilePath,
                    fileName = Globals.latestLargePatchFileName,
                    fileType = "Large Patch"
                });
            }

            // If we have to download the small patch
            if(Globals.downloadSmallPatch)
            {
                // Delete old rar
                System.IO.DirectoryInfo folderInformation = new DirectoryInfo(Globals.workDirectory + "data\\patch\\small");
                foreach (FileInfo file in folderInformation.GetFiles())
                {
                    // If any files were found, delete them all
                    file.Delete();
                }

                // Delete old translated files
                folderInformation = new DirectoryInfo(Globals.workDirectory + "data\\patch\\small\\files");
                foreach (FileInfo file in folderInformation.GetFiles())
                {
                    // If any files were found, delete them all
                    file.Delete();
                }

                // The temporary download file path
                temporaryDownloadingFilePath = Globals.workDirectory + "data/patch/small/" + Globals.latestSmallPatchFileName;

                // Add the download to the queue
                downloadQueue.Add(new DownloadQueue
                {
                    url = "http://hiigara.arghargh200.net/pso2/" + Globals.latestSmallPatchFileName,
                    downloadToPath = temporaryDownloadingFilePath,
                    fileName = Globals.latestSmallPatchFileName,
                    fileType = "Small Patch"
                });
            }

            if(downloadQueue.Count() > 0)
            {
                downloadFile();
            }
        }

        public void downloadFile()
        {
            if(downloadQueue.Count() > 0)
            {
                using(webClient = new WebClient())
                {
                    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                    webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                    try
                    {
                        // Increment the at file download count and update the labels
                        downloadingFileOf++;
                        fileStatusLabel.Text = "Files: " + downloadingFileOf + "/" + Globals.filesToDownload.ToString();
                        downloadingTypeLabel.Text = downloadQueue[0].fileType;
                        downloadingFileNameLabel.Text = downloadQueue[0].fileName;

                        // The variable that will be holding the url address
                        Uri URL;

                        // Make sure the url starts with "http://"
                        if(!downloadQueue[0].url.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                        {
                            downloadQueue[0].url = "http://" + downloadQueue[0].url;
                        }

                        if(!Uri.TryCreate(downloadQueue[0].url, UriKind.Absolute, out URL))
                        {
                            MessageBox.Show("Bad URL! -> " + downloadQueue[0].url);
                        }

                        // Start the stopwatch which we will be using to calculate the download speed
                        stopWatch.Start();

                        // Start downloading the file
                        webClient.DownloadFileAsync(URL, downloadQueue[0].downloadToPath);

                        downloadQueue.RemoveAt(0);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                // Download(s) is/are completed
                this.Close();
            }
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                // Update the progressbar percentage only when the value is not the same (to avoid updating the control constantly)
                if(progressBar.Value != e.ProgressPercentage)
                {
                    progressBar.Value = e.ProgressPercentage;
                }

                // Update the label with how much data have been downloaded so far and the total size of the file we are currently downloading
                fileSizeLabel.Text = (Convert.ToDouble(e.BytesReceived) / 1024 / 1024).ToString("0.00") + " Mb" + " of " + (Convert.ToDouble(e.TotalBytesToReceive) / 1024 / 1024).ToString("0.00") + " Mb";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            stopWatch.Reset();
            if(e.Cancelled == true)
            {
                // Delete the file we were downloading
                File.Delete(temporaryDownloadingFilePath);
                MessageBox.Show("You have cancelled the Download.");
            }
            else
            {
                // Download completed
                // Download the next file, if necessary
                downloadFile();
            }
        }

        private void smallPatchStatus_Click(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            // Delete the file we were downloading
            File.Delete(temporaryDownloadingFilePath);
            MessageBox.Show("You have cancelled the Download.");
        }
    }
}