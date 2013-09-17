using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Net;
using HtmlAgilityPack;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace PSO2PatchManager
{
    public partial class mainForm : Form
    {
        string currentDir;

        public mainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        // OnLoaded should be executed when the app is loaded and
        // goes into Idle
        protected override void OnLoad(EventArgs args)
        {
            Application.Idle += new EventHandler(OnLoaded);
        }

        // Execute our everything here on bootup.
        // This way our form loads and displays
        // immediately while things keep loading
        public void OnLoaded(object sender, EventArgs args)
        {
            Application.Idle -= new EventHandler(OnLoaded);

            bool errorsEncountered = false;

            // Check if PSO2 or another instance of the
            // Patch Manager is already running or not
            if(processIsRunning("pso2") || processIsRunning("pso2launcher"))
            {
                MessageBox.Show("Either PSO2 or the PSO2 Launcher is currently running.\nPlease boot the PSO2 Patch Manager again when PSO2 or the Launcher is not running.");
                errorsEncountered = true;
            }
            if(processIsRunningNumber("PSO2 Patch Manager") > 1)
            {
                MessageBox.Show("Another instance of PSO2 Patch Manager is already running. This instance will now quit.");
                errorsEncountered = true;
            }
            
            // If there have been no errors, load the rest
            if(errorsEncountered == true)
            {
                Application.Exit();
            }
            else
            {
                // Basically this will check if the user is running a PSO2 Translation Patch Manager
                // below version 1.5 or not, since 1.5 made the move to store data in the AppData folder
                // whereas the previous versions used the Documents folder. So this function, if it 
                // detects the data file(s) in Documents, will move them over into the AppData folder
                // and then resume regular operations. The user shouldn't notice...much except for a
                // slightly longer boot time the first time.
                checkOlderVersion();

                // Check whether we're running Stable or Dev build
                checkUsingVersion();

                // Disable buttons by default
                disableButtons();

                // Check for any Readme's that may be haunting
                // current users who already have the application
                // since before this version
                removeReadmeIfExists();

                // Store the form's location
                Globals.formX = this.Location.X;
                Globals.formY = this.Location.Y;

                // Initialize SQLite
                sqliteHandler sqlh = new sqliteHandler();
                sqlh.initSqlite();

                // Check if this is our first run or not
                checkFirstRun();

                // Check Client & Server version
                checkVersion();

                // Make sure the buttons are disabled by default until we've done our checks
                disableButtons();

                // Get our current working directory
                currentDir = Environment.CurrentDirectory + "\\";

                // Clear out the temp folder, if any files were there
                System.IO.DirectoryInfo folderInformation = new DirectoryInfo(Globals.workDirectory + "data\\patch\\temp");
                foreach (FileInfo file in folderInformation.GetFiles())
                {
                    // If any files were found, delete them all
                    file.Delete();
                }

                // Clear the log of any debug messages
                textLog.Clear();

                // Check all directories and according to that, 
                // enable or disable the buttons
                checkFolders();

                // Check for updates (and all that jazz)
                checkForUpdates();

                // Check if we should initiate the downloader or not
                checkDownloader();
            }

            // Ping the server
            /*
             * NOTE: This is used only for statistics, only the MD5'd hash is sent to the
             * server to keep people apart. This way I can track how many people are
             * approximately using the application
             * Will include an opt-out in the future (which will also mean the entry
             * will be deleted off the server)
             * Only stats recorded: md5'd hash as "user id", and the times the application
             * has booted and pinged the server, that's all
             * 
             * When implementing the opt-out, I will also clear out the entire database
             * of any entries, otherwise the opt-out is kind of silly. For now this is
             * just a test thing. I will likely rework it to use something entirely
             * different
             */
            Globals.pingSiteWithData("http://aerius.no-ip.biz/pso2tpm/stats.php?a=p&u=" + Globals.GetMd5Hash(Environment.ExpandEnvironmentVariables("%userprofile%")));
        }

        public bool processIsRunning(string process)
        {
            // Get how many process of the given parameter
            return (System.Diagnostics.Process.GetProcessesByName(process).Length != 0);
        }

        public int processIsRunningNumber(string process)
        {
            // Get how many processes are running of the given parameter
            return Process.GetProcessesByName(process).Length;
        }

        public void Log(string text)
        {
            // Add the text to the log box
            textLog.Text += text;

            // Scroll down if any text goes off-screen
            textLog.SelectionStart = textLog.Text.Length;
            textLog.ScrollToCaret();
            textLog.Refresh();
        }

        public void checkOlderVersion()
        {
            if(File.Exists(Globals.oldWorkDirectory+"data\\data.s3db"))
            {
                // Prompt the user of what's about to happen, so they know what's going on
                string message = "Hi!\n";
                message += "This update (1.5) uses your AppData folder to store it's data rather than the\n";
                message += "previous versions who have used your Documents folder to operate. That folder\n";
                message += "will now be moved to the new working folder in the AppData directory, so the\n";
                message += "bootup might be a tad slower than usual just this once.\n\n";
                message += "Thank you so much for using this tool!";
                MessageBox.Show(message);

                // Older version files detected. Move them to the new folder
                Directory.Move(Globals.oldWorkDirectory+"data", Globals.workDirectory+"data");
            }
        }

        public void checkUsingVersion()
        {
            // Grab the pso2tpm's current version
            string[] tmp = new string[]{"0"};
            try
            {
                // If the file exists
                tmp = File.ReadAllLines(Globals.workDirectory+"using.dat");
            }
            catch(FileNotFoundException ex)
            {
                // File was not found, generate it and then attempt to read it again
                // once more
                string versionFileContents = "0";
                System.IO.File.WriteAllText(@Globals.workDirectory+"using.dat", versionFileContents);

                try
                {
                    // Attempt to read the file again
                    tmp = File.ReadAllLines(Globals.workDirectory+"using.dat");
                }
                catch(FileNotFoundException ex2)
                {
                    // Couldn't read the file, which means we failed to create it
                    MessageBox.Show("Could not generate the version.ver file. Please make sure this application is running with Administrator Privileges if this problem persists.");
                    Application.Exit();
                }
            }
            Int32.TryParse(tmp[0], out Globals.usingVersion);

            // Show the "using developer build" image at the bottom
            // Will change this to use a regular text label later,
            // just wanted to see if this would have worked with an
            // image or not
            if(Globals.usingVersion == 1)
            {
                devChannelNotification.Visible = true;
            }
        }

        private void checkFirstRun()
        {
            // Check if this is our first run or not, which will
            // call up the modified settings window
            if(Globals.getSetting("firstRun") == "")
            {
                Form PMSettings = new PMSettings();
                PMSettings.ShowDialog(this);
            }
        }

        private void checkVersion()
        {
            WebClient client = new WebClient();
            // User-agent needed to get access to version.ver on SEGA's servers
            client.Headers.Add("user-agent", "AQUA_HTTP");

            // Read the file
            String versionOnServer = "Unavailable";
            try
            {
                Stream stream = client.OpenRead("http://download.pso2.jp/patch_prod/patches/version.ver");
                StreamReader reader = new StreamReader(stream);
                versionOnServer = reader.ReadToEnd();
            }
            catch(TimeoutException ex)
            {
                // Server couldn't be reached
            }

            // Get the client's current version
            string clientVersion = "Unavailable";
            try
            {
                clientVersion = File.ReadAllText(Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents") + "\\SEGA\\PHANTASYSTARONLINE2\\version.ver");
            }
            catch(FileNotFoundException ex)
            {
                // File's not there, probably because of an HTTP error that stopped the download, so the version.ver is now
                // called version_precede.ver, do nothing
                // Alternatively the PSO2 client may not have been installed or ran first
            }
            
            // Update the labels
            clientVersionLabel.Text = clientVersion;
            versionFromServerLabel.Text = versionOnServer;

            if(clientVersion != versionOnServer)
            {
                if(clientVersionLabel.Text == "" || clientVersionLabel.Text == " ")
                {
                    clientVersionLabel.Text = "OUTDATED";
                }

                clientVersionLabel.ForeColor = ColorTranslator.FromHtml("#FF3333");
                clientVersionLabel.Text += " (!)";
                Globals.clientOutdated = true;
            }
        }

        private bool directoryHasFiles(string location)
        {
            string[] filesInDirectory = Directory.GetFiles(location);
            if(filesInDirectory.Length > 0)
            {
                return true;
            }

            return false;
        }

        private void disableButtons()
        {
            largePatchInstallButton.Enabled = false;
            largePatchRevertButton.Enabled = false;
            smallPatchInstallButton.Enabled = false;
            smallPatchRevertButton.Enabled = false;
            storyPatchInstallButton.Enabled = false;
            storyPatchRevertButton.Enabled = false;
            manualInstallStoryPatchButton.Enabled = false;
            runPSO2Button.Enabled = false;
            settingsButton.Enabled = false;
        }

        private void checkFolders()
        {
            // Check if the folders exist or not, if not, create them
            if(!Directory.Exists(Globals.workDirectory + "data\\patch\\large"))                     { Globals.createDirectory(Globals.workDirectory + "data\\patch\\large"); }
            if(!Directory.Exists(Globals.workDirectory + "data\\patch\\small"))                     { Globals.createDirectory(Globals.workDirectory + "data\\patch\\small"); }
            if(!Directory.Exists(Globals.workDirectory + "data\\patch\\large\\files"))              { Globals.createDirectory(Globals.workDirectory + "data\\patch\\large\\files"); }
            if(!Directory.Exists(Globals.workDirectory + "data\\patch\\small\\files"))              { Globals.createDirectory(Globals.workDirectory + "data\\patch\\small\\files"); }
            if(!Directory.Exists(Globals.workDirectory + "data\\patch\\story\\files"))              { Globals.createDirectory(Globals.workDirectory + "data\\patch\\story\\files"); }
            if(!Directory.Exists(Globals.workDirectory + "data\\patch\\large\\files\\original"))    { Globals.createDirectory(Globals.workDirectory + "data\\patch\\large\\files\\original"); }
            if(!Directory.Exists(Globals.workDirectory + "data\\patch\\small\\files\\original"))    { Globals.createDirectory(Globals.workDirectory + "data\\patch\\small\\files\\original"); }
            if(!Directory.Exists(Globals.workDirectory + "data\\patch\\story\\files\\original"))    { Globals.createDirectory(Globals.workDirectory + "data\\patch\\story\\files\\original"); }
            if(!Directory.Exists(Globals.workDirectory + "data\\patch\\temp"))                      { Globals.createDirectory(Globals.workDirectory + "data\\patch\\temp"); }

            // Check the Large Patch
            string[] largePatchDirectory = Directory.GetFiles(Globals.workDirectory + "data\\patch\\large\\files\\");
            string[] largePatchOrigDirectory = Directory.GetFiles(Globals.workDirectory + "data\\patch\\large\\files\\original\\");

            largePatchInstallButton.Enabled = false;
            largePatchRevertButton.Enabled = false;

            // If the large-translation patch has not been installed
            if(largePatchDirectory.Length > 0)
            {
                // Enable the Install button
                largePatchInstallButton.Enabled = true;
            }

            // If the large-translation patch has been installed
            if(largePatchOrigDirectory.Length > 0)
            {
                // Enable the Revert button
                largePatchRevertButton.Enabled = true;
            }

            // Check the Small Patch
            string[] smallPatchDirectory = Directory.GetFiles(Globals.workDirectory + "data\\patch\\small\\files\\");
            string[] smallPatchOrigDirectory = Directory.GetFiles(Globals.workDirectory + "data\\patch\\small\\files\\original\\");

            smallPatchInstallButton.Enabled = false;
            smallPatchRevertButton.Enabled = false;

            // If the small-translation patch has not been installed
            if(smallPatchDirectory.Length > 0)
            {
                // Enable the Install button
                smallPatchInstallButton.Enabled = true;
            }

            // If the small-translation patch has been installed
            if(smallPatchOrigDirectory.Length > 0)
            {
                // Enable the Revert button
                smallPatchRevertButton.Enabled = true;
            }

            // Check the Story Patch
            string[] storyPatchDirectory = Directory.GetFiles(Globals.workDirectory + "data\\patch\\story\\files\\");
            string[] storyPatchOrigDirectory = Directory.GetFiles(Globals.workDirectory + "data\\patch\\story\\files\\original\\");

            storyPatchInstallButton.Enabled = false;
            storyPatchRevertButton.Enabled = false;
            manualInstallStoryPatchButton.Enabled = true;

            // If the story-translation patch has not been installed
            if(storyPatchDirectory.Length > 0)
            {
                // Enable the Install button
                storyPatchInstallButton.Enabled = true;
            }

            // If the story-translation patch has been installed
            if(storyPatchOrigDirectory.Length > 0)
            {
                // Enable the Revert button
                storyPatchRevertButton.Enabled = true;

                // Disable the Manual Installation button
                manualInstallStoryPatchButton.Enabled = false;
            }

            // Enable bottom buttons
            runPSO2Button.Enabled = true;
            settingsButton.Enabled = true;
        }

        private void checkForUpdates()
        {
            if(Globals.clientOutdated == true)
            {
                int pso2NotifySetting = Globals.getIntFromString(Globals.getSetting("pso2NotifySettings"));
                bool doRevert = false;
                bool patchesInstalled = false;

                if(largePatchRevertButton.Enabled == true || smallPatchRevertButton.Enabled == true || storyPatchRevertButton.Enabled == true)
                {
                    patchesInstalled = true;
                }

                if(pso2NotifySetting == 1 && patchesInstalled)
                {
                    // If we chose to get a notification, and have translated files installed, ask
                    DialogResult dialogResult = MessageBox.Show("Your PSO2 client seems to be outdated. An update was found for PSO2. Would you like to revert the Translation Files?\nThis is recommended, updating PSO2 while there are translation files installed can cause unexpected behaviours.", "PSO2 Update Found", MessageBoxButtons.YesNo);
                    if(dialogResult == DialogResult.Yes)
                    {
                        doRevert = true;
                    }
                }
                else if(pso2NotifySetting == 2 && patchesInstalled)
                {
                    // If we chose to have stuff automated, and have translated files installed, auto-revert
                    doRevert = true;
                }

                if(doRevert)
                {
                    if(largePatchRevertButton.Enabled == true)
                    {
                        // Revert the Large-translation patch
                        revertPatch("LARGE");
                    }

                    if(smallPatchRevertButton.Enabled == true)
                    {
                        // Revert the Small-translation patch
                        revertPatch("SMALL");
                    }

                    if(storyPatchRevertButton.Enabled == true)
                    {
                        // Revert the Story-translation patch
                        revertPatch("STORY");
                    }
                }
            }

            // Parse the page where the releases are and grab the newest files.
            WebClient webClient = new WebClient();
            string page = webClient.DownloadString("http://hiigara.arghargh200.net/pso2/");
            
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);
            foreach(HtmlNode table in doc.DocumentNode.SelectNodes("//table"))
            {
                foreach(HtmlNode tbody in table.SelectNodes("tbody"))
                {
                    foreach(HtmlNode row in tbody.SelectNodes("tr"))
                    {
                        int i = 0;
                        bool skip = false;
                        Globals.filename = "";
                        Globals.type = "";
                        Globals.processedDate = "";
                        foreach(HtmlNode cell in row.SelectNodes("td"))
                        {
                            if(skip == false)
                            {
                                if(i == 0) 
                                {
                                    // Grab the Filename
                                    Globals.filename = cell.InnerText;
                                    if(!Globals.filename.Contains("rar"))
                                    {
                                        // It's not a rar file, skip
                                        skip = true;
                                    }
                                    else
                                    {
                                        if(Globals.filename.Contains("large"))
                                        {
                                            Globals.type = "LARGE";
                                        }
                                        else
                                        {
                                            Globals.type = "SMALL";
                                        }
                                    }
                                }
                                else if(i == 1)
                                {
                                    // Grab and process/parse the Date Last Modified
                                    string[] fulldate = cell.InnerText.Split(new string[] { " " }, StringSplitOptions.None);
                                    string[] dates = fulldate[0].Split(new string[] { "-" }, StringSplitOptions.None);
                                    string[] times = fulldate[1].Split(new string[] { ":" }, StringSplitOptions.None);
                                    string year = dates[0];
                                    string monthText = dates[1];
                                    string month = "";
                                    if (monthText == "Jan") { month = "01"; }
                                    else if (monthText == "Feb") { month = "02"; }
                                    else if (monthText == "Mar") { month = "03"; }
                                    else if (monthText == "Apr") { month = "04"; }
                                    else if (monthText == "May") { month = "05"; }
                                    else if (monthText == "Jun") { month = "06"; }
                                    else if (monthText == "Jul") { month = "07"; }
                                    else if (monthText == "Aug") { month = "08"; }
                                    else if (monthText == "Sep") { month = "09"; }
                                    else if (monthText == "Oct") { month = "10"; }
                                    else if (monthText == "Nov") { month = "11"; }
                                    else if (monthText == "Dec") { month = "12"; }
                                    string day = dates[2];
                                    string hour = times[0];
                                    string minute = times[1];

                                    Globals.processedDate = year + "-" + month + "-" + day + " " + hour + ":" + minute;
                                }
                                else if(i == 2)
                                {
                                    // The File Size, which we don't use
                                }
                                else if(i == 3)
                                {
                                    // The Filetype
                                    string query = "INSERT INTO `FileList` (`FileName`, `Type`, `DateModified`) VALUES (\"" + Globals.filename + "\", \"" + Globals.type + "\",  \"" + Globals.processedDate + "\");";
                                    Globals.executeQuery(query);
                                }
                                else
                                {
                                    // What's this?
                                    //Log("DAFUQ");
                                }
                            }

                            i++;
                        }
                    }
                }
            }
            
            // Grab the latest Large Patch file entry, and then delete all other Large type file entries
            string query2 = "SELECT * FROM `FileList` WHERE `Type`=\"LARGE\" ORDER BY `DateModified` DESC LIMIT 1;";
            SQLiteCommand cmd = new SQLiteCommand(query2, Globals.dblink);
            SQLiteDataReader reader = cmd.ExecuteReader();
            
            // For every entry we found
            while(reader.Read())
            {
                // Save the entry with the latest date into the global
                Globals.latestLargePatchFileName = reader["FileName"].ToString();

                // Delete all other entries that are now irrelevant
                string query3 = "DELETE FROM `FileList` WHERE `FileName`!=\"" + reader["FileName"] + "\" and `Type`=\"LARGE\";";
                Globals.executeQuery(query3);
            }

            // Grab the latest Small Patch file entry, and then delete all other Small type file entries
            query2 = "SELECT * FROM `FileList` WHERE `Type`=\"SMALL\" ORDER BY `DateModified` DESC LIMIT 1;";
            cmd = new SQLiteCommand(query2, Globals.dblink);
            reader = cmd.ExecuteReader();
            // For every entry we found
            while(reader.Read())
            {
                // Get the entry with the latest date
                //MessageBox.Show(reader["FileName"] + " -> " + reader["DateModified"]);

                // Save the entry with the latest date into the global
                Globals.latestSmallPatchFileName = reader["fileName"].ToString();

                // Delete all other entries that are now irrelevant
                string query3 = "DELETE FROM `FileList` WHERE `FileName`!=\"" + reader["FileName"] + "\" and `Type`=\"SMALL\";";
                Globals.executeQuery(query3);
            }

            compareLocalFiles();
        }

        private void compareLocalFiles()
        {
            // Grab all files in the small and large patch folders
            string[] largePatchFiles = Directory.GetFiles(Globals.workDirectory + "data\\patch\\large\\");
            string[] smallPatchFiles = Directory.GetFiles(Globals.workDirectory + "data\\patch\\small\\");

            // Here we'll check to make sure only 1 file is in both the
            // small and large patch folder
            int largeFilesOnFile = largePatchFiles.Length;
            int smallFilesOnFile = smallPatchFiles.Length;

            // Checks on the Large patch file
            if(largeFilesOnFile == 0 || largeFilesOnFile > 1)
            {
                // We have no large patch file yet or have more than 1 (somehow)
                Globals.downloadLargePatch = true;
                Globals.filesToDownload++;

                if(largeFilesOnFile == 0)
                {
                    // Update the Label
                    largePatchStatus.Text = "Outdated";
                }
                else
                {
                    // Delete the files
                    Globals.deleteLargeFilesOnFile = true;

                    // Update the Label
                    largePatchStatus.Text = "Error, redownloading";
                }
            }
            else if(largeFilesOnFile == 1)
            {
                // If we have a Large patch file, get the file name, and compare it to the
                // newest in the database we just pulled
                if(Globals.latestLargePatchFileName == Path.GetFileName(largePatchFiles[0].ToString()))
                {
                    // Don't download anything, we're up to date
                    // Update the Label
                    largePatchStatus.Text = "Up to date";
                }
                else
                {
                    // Delete the local file and grab the newest file
                    Globals.downloadLargePatch = true;
                    Globals.filesToDownload++;
                    Globals.deleteLargeFilesOnFile = true;

                    // Update the Label
                    largePatchStatus.Text = "Outdated";
                }
            }

            // Checks on the Small patch file
            if(smallFilesOnFile == 0 || smallFilesOnFile > 1)
            {
                // We have no small patch file yet or have more than 1 (somehow)
                Globals.downloadSmallPatch = true;
                Globals.filesToDownload++;

                if(smallFilesOnFile == 0)
                {
                    // Update the Label
                    smallPatchStatus.Text = "Outdated";
                }
                else
                {
                    // Delete the files
                    Globals.deleteSmallFilesOnFile = true;

                    // Update the Label
                    smallPatchStatus.Text = "Error, redownloading";
                }
            }
            else if(smallFilesOnFile == 1)
            {
                // If we have a Large patch file, get the file name, and compare it to the
                // newest in the database we just pulled
                if(Globals.latestSmallPatchFileName == Path.GetFileName(smallPatchFiles[0].ToString()))
                {
                    // Don't download anything, we're up to date
                    // Update the Label
                    smallPatchStatus.Text = "Up to date";
                }
                else
                {
                    // Delete the local file and grab the newest file
                    Globals.downloadSmallPatch = true;
                    Globals.filesToDownload++;
                    Globals.deleteSmallFilesOnFile = true;

                    // Update the Label
                    smallPatchStatus.Text = "Outdated";
                }
            }
        }

        private void checkDownloader()
        {
            if(Globals.downloadLargePatch || Globals.downloadSmallPatch)
            {
                int transNotifySetting = Globals.getIntFromString(Globals.getSetting("transNotifySettings"));
                bool doUpdate = false;
                bool patchesInstalled = false;

                if(largePatchInstallButton.Enabled == true || smallPatchInstallButton.Enabled == true || largePatchRevertButton.Enabled == true || smallPatchRevertButton.Enabled == true)
                {
                    patchesInstalled = true;
                }

                if(transNotifySetting == 1)
                {
                    string dialogueName = "";
                    string dialogue = "";

                    if(patchesInstalled)
                    {
                        dialogueName = "Translation Files Update Found";
                        dialogue = "A Translation File(s) update was found, would you like to Update?\n\n";
                        dialogue += "Note: If you have the Translation Files installed, I will automatically revert their installation for you before updating.";
                    }
                    else
                    {
                        dialogueName = "Translation Files not found";
                        dialogue = "You don't seem to have the Translated Files yet, would you like to download them?";
                    }

                    // If we chose to get a notification, and have translated files installed, ask
                    DialogResult dialogResult = MessageBox.Show(dialogue, dialogueName, MessageBoxButtons.YesNo);
                    if(dialogResult == DialogResult.Yes)
                    {
                        doUpdate = true;
                    }
                }
                else if(transNotifySetting == 2)
                {
                    // If we chose to have stuff automated, and have translated files installed, auto-revert and download new ones
                    doUpdate = true;
                }

                if(doUpdate)
                {
                    disableButtons();

                    // An update was found, if files were installed, we have to revert the files first
                    if(Globals.deleteLargeFilesOnFile == true)
                    {
                        Globals.action = "LARGE";
                        Globals.install = false;
                        Form installRevertWindow = new installRevertFiles();
                        installRevertWindow.ShowDialog(this);
                    }

                    if(Globals.deleteSmallFilesOnFile == true)
                    {
                        Globals.action = "SMALL";
                        Globals.install = false;
                        Form installRevertWindow = new installRevertFiles();
                        installRevertWindow.ShowDialog(this);
                    }

                    // Launch the downloader
                    Form downloadWindow = new downloadWindow();
                    downloadWindow.ShowDialog(this);

                    // Unrar the files
                    // Handle the large patch
                    if (Globals.downloadLargePatch)
                    {
                        Globals.rarType = "LARGE";
                        unrar(Globals.workDirectory + "data\\patch\\large\\" + Globals.latestLargePatchFileName, Globals.workDirectory + "data\\patch\\large\\files\\");
                    }

                    // Handle the small patch
                    if (Globals.downloadSmallPatch)
                    {
                        Globals.rarType = "SMALL";
                        unrar(Globals.workDirectory + "data\\patch\\small\\" + Globals.latestSmallPatchFileName, Globals.workDirectory + "data\\patch\\small\\files\\");
                    }

                    // Do some verify-checking
                    checkFolders();
                }
            }
        }

        private void unrar(string file, string location)
        {
            Globals.rarExtractToLocation = location;
            Globals.rarFile = file;
            unrarWindow unrarWindow = new unrarWindow();
            unrarWindow.ShowDialog(this);

            // Remove any readme's that may have been in the archive
            removeReadmeIfExists();
        }
        
        private void removeReadmeIfExists()
        {
            // Small Patch
            if(File.Exists(Globals.workDirectory + "data\\patch\\small\\files\\readme.txt")){ File.Delete(Globals.workDirectory + "data\\patch\\small\\files\\readme.txt"); }
            if(File.Exists(Globals.workDirectory + "data\\patch\\small\\files\\original\\readme.txt")){ File.Delete(Globals.workDirectory + "data\\patch\\small\\files\\original\\readme.txt"); }

            // Story Patch
            if(File.Exists(Globals.workDirectory + "data\\patch\\large\\files\\readme.txt")){ File.Delete(Globals.workDirectory + "data\\patch\\large\\files\\readme.txt"); }
            if(File.Exists(Globals.workDirectory + "data\\patch\\large\\files\\original\\readme.txt")){ File.Delete(Globals.workDirectory + "data\\patch\\large\\files\\original\\readme.txt"); }

            // Small Patch
            if(File.Exists(Globals.workDirectory + "data\\patch\\story\\files\\readme.txt")){ File.Delete(Globals.workDirectory + "data\\patch\\story\\files\\readme.txt"); }
            if(File.Exists(Globals.workDirectory + "data\\patch\\story\\files\\original\\readme.txt")){ File.Delete(Globals.workDirectory + "data\\patch\\story\\files\\original\\readme.txt"); }
        }

        private void installButton_Click(object sender, EventArgs e)
        {
            installPatch("SMALL");
        }

        private void revertButton_Click(object sender, EventArgs e)
        {
            revertPatch("SMALL");
        }

        private void largePatchInstallButton_Click(object sender, EventArgs e)
        {
            installPatch("LARGE");
        }

        private void largePatchRevertButton_Click(object sender, EventArgs e)
        {
            revertPatch("LARGE");
        }

        private void storyPatchInstallButton_Click(object sender, EventArgs e)
        {
            installPatch("STORY");
        }

        private void storyRevertPatchButton_Click(object sender, EventArgs e)
        {
            revertPatch("STORY");
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            Form PMSettings = new PMSettings();
            PMSettings.ShowDialog(this);
        }

        private void manualInstallStoryPatch_Click(object sender, EventArgs e)
        {
            Form installStoryPatch = new installStoryPatch();
            installStoryPatch.ShowDialog(this);

            // Install the story patch, if the variable was set
            string rarPath = "";
            if(Globals.installStoryPatch)
            {
                Globals.installStoryPatch = false;
                DirectoryInfo folderInformation = new DirectoryInfo(Globals.workDirectory + "data\\patch\\story\\");
                foreach (FileInfo file in folderInformation.GetFiles())
                {
                    rarPath = Path.GetFileName(file.FullName);
                }
                
                // Set the shortname of what we're unrarring
                Globals.rarType = "STORY";
                
                // Unrar the file to the proper location
                unrar(Globals.workDirectory + "data\\patch\\story\\" + rarPath, Globals.workDirectory + "data\\patch\\story\\files\\");

                // Do some verify-checking
                checkFolders();
            }
        }
        
        private void installPatch(string name)
        {
            bool proceedInstall = true;

            // If the client is outdated, and we're trying to install a patch
            if(Globals.clientOutdated)
            {
                // If we chose to get a notification, and have translated files installed, ask
                string dialogue = "Your client seems to be outdated. Please perform the PSO2 Update first by starting the PSO2 Launcher with the button below. After PSO2 is updated, please relaunch this application and you'll be able to install the Translation Files.";
                DialogResult dialogResult = MessageBox.Show(dialogue, "PSO2 Update Found");
                proceedInstall = false;
            }

            // Proceed installing, if we're allowed
            if(proceedInstall)
            {
                if(name != "LARGE" && name != "SMALL" && name != "STORY")
                {
                    MessageBox.Show("Wrong parameter given for installPatch (param: "+name+"), aborting!");
                }
                else
                {
                    disableButtons();
                    Globals.action = name;
                    Globals.install = true;
                    Form installRevertWindow = new installRevertFiles();
                    installRevertWindow.ShowDialog(this);
                    checkFolders();

                    if(name == "STORY")
                    {
                        // Disable the Manual Installation button
                        manualInstallStoryPatchButton.Enabled = false;
                    }
                }
            }
        }

        private void revertPatch(string name)
        {
            if(name != "LARGE" && name != "SMALL" && name != "STORY")
            {
                MessageBox.Show("Wrong parameter given for installPatch (param: "+name+"), aborting!");
            }
            else
            {
                disableButtons();
                Globals.action = name;
                Globals.install = false;
                Form installRevertWindow = new installRevertFiles();
                installRevertWindow.ShowDialog(this);
                checkFolders();

                if(name == "STORY")
                {
                    // Enable the Manual Installation button
                    manualInstallStoryPatchButton.Enabled = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(Globals.pso2Directory + "PSO2Launcher.exe");
            this.Close();
        }

        private void resizedForm(object sender, EventArgs e)
        {
            // Store the form's location
            Globals.formX = this.Location.X;
            Globals.formY = this.Location.Y;
        }
    }
}