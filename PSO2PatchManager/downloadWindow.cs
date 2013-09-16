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
            this.Location = new Point((MyGlobals.formX + 10), (MyGlobals.formY + 30));

            if (MyGlobals.downloadLargePatch)
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

            if (MyGlobals.downloadSmallPatch)
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
            if (MyGlobals.downloadLargePatch)
            {
                // Delete old rar
                System.IO.DirectoryInfo folderInformation = new DirectoryInfo(MyGlobals.workDirectory + "data\\patch\\large");
                foreach (FileInfo file in folderInformation.GetFiles())
                {
                    // If any files were found, delete them all
                    file.Delete();
                }

                // Delete old translated files
                folderInformation = new DirectoryInfo(MyGlobals.workDirectory + "data\\patch\\large\\files");
                foreach (FileInfo file in folderInformation.GetFiles())
                {
                    // If any files were found, delete them all
                    file.Delete();
                }

                // The temporary download file path
                temporaryDownloadingFilePath = MyGlobals.workDirectory + "data/patch/large/" + MyGlobals.latestLargePatchFileName;

                // Add the download to the queue
                downloadQueue.Add(new DownloadQueue
                {
                    url = "http://hiigara.arghargh200.net/pso2/" + MyGlobals.latestLargePatchFileName,
                    downloadToPath = temporaryDownloadingFilePath,
                    fileName = MyGlobals.latestLargePatchFileName,
                    fileType = "Large Patch"
                });
            }

            // If we have to download the small patch
            if (MyGlobals.downloadSmallPatch)
            {
                // Delete old rar
                System.IO.DirectoryInfo folderInformation = new DirectoryInfo(MyGlobals.workDirectory + "data\\patch\\small");
                foreach (FileInfo file in folderInformation.GetFiles())
                {
                    // If any files were found, delete them all
                    file.Delete();
                }

                // Delete old translated files
                folderInformation = new DirectoryInfo(MyGlobals.workDirectory + "data\\patch\\small\\files");
                foreach (FileInfo file in folderInformation.GetFiles())
                {
                    // If any files were found, delete them all
                    file.Delete();
                }

                // The temporary download file path
                temporaryDownloadingFilePath = MyGlobals.workDirectory + "data/patch/small/" + MyGlobals.latestSmallPatchFileName;

                // Add the download to the queue
                downloadQueue.Add(new DownloadQueue
                {
                    url = "http://hiigara.arghargh200.net/pso2/" + MyGlobals.latestSmallPatchFileName,
                    downloadToPath = temporaryDownloadingFilePath,
                    fileName = MyGlobals.latestSmallPatchFileName,
                    fileType = "Small Patch"
                });
            }

            if (downloadQueue.Count() > 0)
            {
                downloadFile();
            }
        }

        //public void downloadFile(string urlAddress, string location)
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
                        // Increment the at file download count and update the labels
                        downloadingFileOf++;
                        fileStatusLabel.Text = "Files: " + downloadingFileOf + "/" + MyGlobals.filesToDownload.ToString();
                        downloadingTypeLabel.Text = downloadQueue[0].fileType;
                        downloadingFileNameLabel.Text = downloadQueue[0].fileName;

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
                this.Close();
            }
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                // Update the progressbar percentage only when the value is not the same (to avoid updating the control constantly)
                if (progressBar.Value != e.ProgressPercentage)
                    progressBar.Value = e.ProgressPercentage;

                // Update the label with how much data have been downloaded so far and the total size of the file we are currently downloading
                fileSizeLabel.Text = (Convert.ToDouble(e.BytesReceived) / 1024 / 1024).ToString("0.00") + " Mb" + " of " + (Convert.ToDouble(e.TotalBytesToReceive) / 1024 / 1024).ToString("0.00") + " Mb";
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
                // Delete the file we were downloading
                File.Delete(temporaryDownloadingFilePath);
                MessageBox.Show("You have cancelled the Download.");
            }
            else
            {
                //MessageBox.Show("The Download has Completed!");
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

public class DownloadQueue
{
    public string url { set; get; }
    public string downloadToPath { set; get; }
    public string fileName { set; get; }
    public string fileType { set; get; }

    public void clear()
    {
        this.url = "";
        this.downloadToPath = "";
        this.fileName = "";
        this.fileType = "";
    }
}