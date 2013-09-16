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
using System.Net;
using HtmlAgilityPack;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Security.Cryptography;

namespace PSO2PatchManager
{
    public partial class mainForm : Form
    {
        string[] transFiles;
        string[] origFiles;
        string currentDir;

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
                MyGlobals.formX = this.Location.X;
                MyGlobals.formY = this.Location.Y;

                // Initialize SQLite
                initSqlite();

                // Check if this is our first run or not
                checkFirstRun();

                // Check Client & Server version
                checkVersion();

                // Make sure the buttons are disabled by default until we've done our checks
                largePatchInstallButton.Enabled = false;
                largePatchRevertButton.Enabled = false;
                smallPatchInstallButton.Enabled = false;
                smallPatchRevertButton.Enabled = false;
                storyPatchInstallButton.Enabled = false;
                storyPatchRevertButton.Enabled = false;

                // Get our current working directory
                currentDir = Environment.CurrentDirectory + "\\";

                // Check if we're in the PSO2 directory or not
                if(Directory.Exists(currentDir + "data\\win32"))
                {
                    Log("[o] OK! translated_files and original_files folders exist.\r\n");
                }
                else
                {
                    Log("[x] The application is not running from the PSO2 Directory.");
                    Log("\r\n\r\n");
                    Log("Please make sure the application is running in the same directory that pso2.exe is in.");
                    Log("\r\n\r\n");
                    Log("This is often the path:\r\n");
                    Log("C:\\Program Files (x86)\\SEGA\\PHANTASYSTARONLINE2\\pso2_bin");
                }

                // Check if the folders exist or not, if not, create them
                //string[] curDirectories = Directory.GetDirectories("/");
                /*bool translatedFilesExists  = Directory.Exists("translated_files");
                bool originalFilesExists    = Directory.Exists("original_files");*/
                bool largePatchFolderExists = Directory.Exists(MyGlobals.workDirectory + "data\\patch\\large");
                bool smallPatchFolderExists = Directory.Exists(MyGlobals.workDirectory + "data\\patch\\small");
                bool largeFilesFolderExists = Directory.Exists(MyGlobals.workDirectory + "data\\patch\\large\\files");
                bool smallFilesFolderExists = Directory.Exists(MyGlobals.workDirectory + "data\\patch\\small\\files");
                bool storyFilesFolderExists = Directory.Exists(MyGlobals.workDirectory + "data\\patch\\story\\files");
                bool largeFilesOrigFolderExists = Directory.Exists(MyGlobals.workDirectory + "data\\patch\\large\\files\\original");
                bool smallFilesOrigFolderExists = Directory.Exists(MyGlobals.workDirectory + "data\\patch\\small\\files\\original");
                bool storyFilesOrigFolderExists = Directory.Exists(MyGlobals.workDirectory + "data\\patch\\story\\files\\original");
                bool tmpFolderExists = Directory.Exists(MyGlobals.workDirectory + "data\\patch\\temp");

                // If the Large patch directory doesn't exist, create it
                if (!largePatchFolderExists)
                {
                    if (createDirectory(MyGlobals.workDirectory + "data\\patch\\large"))
                    {
                        largeFilesFolderExists = true;
                    }
                }

                // If the Small patch directory doesn't exist, create it
                if (!smallPatchFolderExists)
                {
                    if (createDirectory(MyGlobals.workDirectory + "data\\patch\\small"))
                    {
                        smallFilesFolderExists = true;
                    }
                }

                // If the Large patch files directory doesn't exist, create it
                if (!largeFilesFolderExists)
                {
                    if (createDirectory(MyGlobals.workDirectory + "data\\patch\\large\\files"))
                    {
                        largeFilesFolderExists = true;
                    }
                }

                // If the Small patch files directory doesn't exist, create it
                if (!smallFilesFolderExists)
                {
                    if (createDirectory(MyGlobals.workDirectory + "data\\patch\\small\\files"))
                    {
                        smallFilesFolderExists = true;
                    }
                }

                // If the story patch files directory doesn't exist, create it
                if (!storyFilesFolderExists)
                {
                    if (createDirectory(MyGlobals.workDirectory + "data\\patch\\story\\files"))
                    {
                        storyFilesFolderExists = true;
                    }
                }

                // If the original files directory for the large patch doesn't exist, create it
                if (!largeFilesOrigFolderExists)
                {
                    if (createDirectory(MyGlobals.workDirectory + "data\\patch\\large\\files\\original"))
                    {
                        largeFilesOrigFolderExists = true;
                    }
                }

                // If the original files directory for the small patch doesn't exist, create it
                if (!smallFilesOrigFolderExists)
                {
                    if (createDirectory(MyGlobals.workDirectory + "data\\patch\\small\\files\\original"))
                    {
                        smallFilesOrigFolderExists = true;
                    }
                }

                // If the original files directory for the story patch doesn't exist, create it
                if (!storyFilesOrigFolderExists)
                {
                    if (createDirectory(MyGlobals.workDirectory + "data\\patch\\story\\files\\original"))
                    {
                        storyFilesOrigFolderExists = true;
                    }
                }

                // If the original files directory for the story patch doesn't exist, create it
                if (!tmpFolderExists)
                {
                    if (createDirectory(MyGlobals.workDirectory + "data\\patch\\temp"))
                    {
                        tmpFolderExists = true;
                    }
                }

                // Clear out the temp folder, if any folders were there
                System.IO.DirectoryInfo folderInformation = new DirectoryInfo(MyGlobals.workDirectory + "data\\patch\\temp");
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

                // Show form before updating
                this.Show();

                // Check if we should initiate the downloader or not
                checkDownloader();
            }

            MyGlobals.pingSiteWithData("http://aerius.no-ip.biz/pso2tpm/stats.php?a=p&u=" + MyGlobals.GetMd5Hash(Environment.ExpandEnvironmentVariables("%userprofile%")));
        }

        public mainForm()
        {
            InitializeComponent();
        }

        public bool processIsRunning(string process)
        {
            // Got this from stackoverflow, investigate later
            return (System.Diagnostics.Process.GetProcessesByName(process).Length != 0);
        }

        public int processIsRunningNumber(string process)
        {
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
            if(File.Exists(MyGlobals.oldWorkDirectory+"data\\data.s3db"))
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
                Directory.Move(MyGlobals.oldWorkDirectory+"data", MyGlobals.workDirectory+"data");
            }
        }

        public void checkUsingVersion()
        {
            string[] tmp = new string[]{"0"};
            try{
                tmp = File.ReadAllLines(MyGlobals.workDirectory+"using.dat");
            }
            catch(FileNotFoundException ex){
                string versionFileContents = "0";
                System.IO.File.WriteAllText(@MyGlobals.workDirectory+"using.dat", versionFileContents);

                tmp = File.ReadAllLines(MyGlobals.workDirectory+"using.dat");
            }
            Int32.TryParse(tmp[0], out MyGlobals.usingVersion);

            if(MyGlobals.usingVersion == 1)
            {
                devChannelNotification.Visible = true;
            }
        }

        public void initSqlite()
        {
            // If the Large patch directory doesn't exist, create it
            bool workDirectoryExists = Directory.Exists(MyGlobals.workDirectory);
            if (!workDirectoryExists)
            {
                if (createDirectory(MyGlobals.workDirectory))
                {
                    workDirectoryExists = true;
                }
            }

            // Check if our database exixts, if not, create it
            MyGlobals.dblink = null;
            try
            {
                MyGlobals.dblink = new SQLiteConnection("Data Source=" + MyGlobals.workDirectory + "data\\data.s3db;Version=3;FailIfMissing=True;");
            }
            catch (SQLiteException ex)
            {
                 //MessageBox.Show(ex.ToString());
            }

            try
            {
                //MessageBox.Show("Opening Database File");
                MyGlobals.dblink.Open();

                initTables();
            }
            catch (SQLiteException ex)
            {
                //MessageBox.Show(ex.ToString());
                
                // Create the folder 'data'
                createDirectory(MyGlobals.workDirectory + "data");

                //MessageBox.Show("Creating Database File");
                SQLiteConnection.CreateFile(MyGlobals.workDirectory + "data\\data.s3db");

                // Try to open the file once more, now we've created it
                try
                {
                    // Try opening the file
                    MyGlobals.dblink.Open();

                    initTables();
                }
                catch (SQLiteException ex3)
                {
                    //MessageBox.Show(ex3.ToString());
                    // Couldn't open the database file, which means it wasn't
                    // created, no administrative priviledges perhaps? Throw
                    // an error
                    MessageBox.Show("Could not create the database file directory!\r\nTry running this application with Administrative Privileges.");
                }
            }
        }

        public void initTables()
        {
            // Create the tables
            string query = null;

            // The table that holds the Folders we'll monitor
            query = "CREATE TABLE IF NOT EXISTS [FileList](";
            query += "[FileName] TEXT NULL,";
            query += "[Type] TEXT NULL,";
            query += "[DateModified] DATE NULL";
            query += ");";
            MyGlobals.executeQuery(query);

            // Clear the file list
            query = "DELETE FROM `FileList`;";
            MyGlobals.executeQuery(query);

            // Settings table
            query = "CREATE TABLE IF NOT EXISTS [Settings](";
            query += "[Name] TEXT NULL,";
            query += "[Value] TEXT NULL";
            query += ");";
            MyGlobals.executeQuery(query);

            // Set up the values
            if(!MyGlobals.settingExists("firstRun")){MyGlobals.addSetting("firstRun", "");}
            if(!MyGlobals.settingExists("workDirectory")){MyGlobals.addSetting("workDirectory", "");}
            if(!MyGlobals.settingExists("pso2Directory")){MyGlobals.addSetting("pso2Directory", "");}
            if(!MyGlobals.settingExists("largePatchOnFile")){MyGlobals.addSetting("largePatchOnFile", "");}
            if(!MyGlobals.settingExists("smallPatchOnFile")) { MyGlobals.addSetting("smallPatchOnFile", "");}
            if(!MyGlobals.settingExists("transNotifySettings")) { MyGlobals.addSetting("transNotifySettings", "");}
            if(!MyGlobals.settingExists("pso2NotifySettings")) { MyGlobals.addSetting("pso2NotifySettings", "");}
        }

        public bool createDirectory(string directory)
        {
            try
            {
                Directory.CreateDirectory(directory);
                Log("Created the " + directory);
                Log("\r\n");

                return true;
            }
            catch (System.Exception excpt)
            {
                Log("Could not create the " + directory + ".");
                Log("\r\n\r\n");
                Log("Try running the application with Administrative Privileges.");
                Log("\r\n");

                Console.WriteLine(excpt.Message);

                return false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void checkFirstRun()
        {
            if (MyGlobals.getSetting("firstRun") == "")
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
                MyGlobals.clientOutdated = true;
            }
        }

        private bool directoryHasFiles(string location)
        {
            string[] filesInDirectory = Directory.GetFiles(location);
            if(transFiles.Length > 0)
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
            // Check the Large Patch
            string[] largePatchDirectory = Directory.GetFiles(MyGlobals.workDirectory + "data\\patch\\large\\files\\");
            string[] largePatchOrigDirectory = Directory.GetFiles(MyGlobals.workDirectory + "data\\patch\\large\\files\\original\\");

            if (largePatchDirectory.Length > 0)
            {
                largePatchInstallButton.Enabled = true;
            }
            else
            {
                largePatchInstallButton.Enabled = false;
            }
            if (largePatchOrigDirectory.Length > 0)
            {
                largePatchRevertButton.Enabled = true;
            }
            else
            {
                largePatchRevertButton.Enabled = false;
            }

            // Check the Small Patch
            string[] smallPatchDirectory = Directory.GetFiles(MyGlobals.workDirectory + "data\\patch\\small\\files\\");
            string[] smallPatchOrigDirectory = Directory.GetFiles(MyGlobals.workDirectory + "data\\patch\\small\\files\\original\\");

            if(smallPatchDirectory.Length > 0)
            {
                smallPatchInstallButton.Enabled = true;
            }
            else
            {
                smallPatchInstallButton.Enabled = false;
            }
            if(smallPatchOrigDirectory.Length > 0)
            {
                smallPatchRevertButton.Enabled = true;
            }
            else
            {
                smallPatchRevertButton.Enabled = false;
            }

            // Check the Story Patch
            string[] storyPatchDirectory = Directory.GetFiles(MyGlobals.workDirectory + "data\\patch\\story\\files\\");
            string[] storyPatchOrigDirectory = Directory.GetFiles(MyGlobals.workDirectory + "data\\patch\\story\\files\\original\\");

            if (storyPatchDirectory.Length > 0)
            {
                storyPatchInstallButton.Enabled = true;
            }
            else
            {
                storyPatchInstallButton.Enabled = false;
            }
            if (storyPatchOrigDirectory.Length > 0)
            {
                storyPatchRevertButton.Enabled = true;

                // Disable the Manual Installation button
                manualInstallStoryPatchButton.Enabled = false;
            }
            else
            {
                // Disable revert button
                storyPatchRevertButton.Enabled = false;

                // Enable the Manual Installation button
                manualInstallStoryPatchButton.Enabled = true;
            }

            // Enable bottom buttons
            runPSO2Button.Enabled = true;
            settingsButton.Enabled = true;
        }

        private void checkForUpdates()
        {
            if(MyGlobals.clientOutdated == true)
            {
                int pso2NotifySetting = MyGlobals.getIntFromString(MyGlobals.getSetting("pso2NotifySettings"));
                bool doRevert = false;
                bool patchesInstalled = false;

                if(largePatchRevertButton.Enabled == true || smallPatchRevertButton.Enabled == true || storyPatchRevertButton.Enabled == true)
                {
                    patchesInstalled = true;
                }

                if(pso2NotifySetting == 1 && patchesInstalled)
                {
                    // If we chose to get a notification, and have translated files installed, ask
                    DialogResult dialogResult = MessageBox.Show("A PSO2 Update was found as your client seems to be outdated. Would you like to revert the Translation Files?", "PSO2 Update Found", MessageBoxButtons.YesNo);
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
                        revertPatch("LARGE");
                    }

                    if(smallPatchRevertButton.Enabled == true)
                    {
                        revertPatch("SMALL");
                    }

                    if(storyPatchRevertButton.Enabled == true)
                    {
                        revertPatch("STORY");
                    }
                }
            }

            // Fetch the regular patch
            WebClient webClient = new WebClient();
            string page = webClient.DownloadString("http://hiigara.arghargh200.net/pso2/");
            
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);
            foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table"))
            {
                foreach (HtmlNode tbody in table.SelectNodes("tbody"))
                {
                    foreach (HtmlNode row in tbody.SelectNodes("tr"))
                    {
                        int i = 0;
                        bool skip = false;
                        MyGlobals.filename = "";
                        MyGlobals.type = "";
                        MyGlobals.processedDate = "";
                        foreach (HtmlNode cell in row.SelectNodes("td"))
                        {
                            if (skip == false)
                            {
                                if (i == 0) // File Name
                                {
                                    MyGlobals.filename = cell.InnerText;
                                    if (!MyGlobals.filename.Contains("rar"))
                                    {
                                        skip = true;
                                    }
                                    else
                                    {
                                        if (MyGlobals.filename.Contains("large"))
                                        {
                                            MyGlobals.type = "LARGE";
                                        }
                                        else
                                        {
                                            MyGlobals.type = "SMALL";
                                        }
                                    }
                                }
                                else if (i == 1) // Date Last Modified
                                {
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

                                    MyGlobals.processedDate = year + "-" + month + "-" + day + " " + hour + ":" + minute;
                                }
                                else if (i == 2) // Size
                                {
                                    
                                }
                                else if (i == 3) // File Type
                                {
                                    string query = "INSERT INTO `FileList` (`FileName`, `Type`, `DateModified`) VALUES (\"" + MyGlobals.filename + "\", \"" + MyGlobals.type + "\",  \"" + MyGlobals.processedDate + "\");";
                                    MyGlobals.executeQuery(query);
                                }
                                else
                                {
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
            SQLiteCommand cmd = new SQLiteCommand(query2, MyGlobals.dblink);
            SQLiteDataReader reader = cmd.ExecuteReader();
            // For every entry we found
            while (reader.Read())
            {
                // Get the entry with the latest date
                //MessageBox.Show(reader["FileName"] + " -> " + reader["DateModified"]);

                // Save the entry with the latest date into the global
                MyGlobals.latestLargePatchFileName = reader["FileName"].ToString();

                // Delete all other entries that are now irrelevant
                string query3 = "DELETE FROM `FileList` WHERE `FileName`!=\"" + reader["FileName"] + "\" and `Type`=\"LARGE\";";
                MyGlobals.executeQuery(query3);
            }

            // Grab the latest Small Patch file entry, and then delete all other Small type file entries
            query2 = "SELECT * FROM `FileList` WHERE `Type`=\"SMALL\" ORDER BY `DateModified` DESC LIMIT 1;";
            cmd = new SQLiteCommand(query2, MyGlobals.dblink);
            reader = cmd.ExecuteReader();
            // For every entry we found
            while (reader.Read())
            {
                // Get the entry with the latest date
                //MessageBox.Show(reader["FileName"] + " -> " + reader["DateModified"]);

                // Save the entry with the latest date into the global
                MyGlobals.latestSmallPatchFileName = reader["fileName"].ToString();

                // Delete all other entries that are now irrelevant
                string query3 = "DELETE FROM `FileList` WHERE `FileName`!=\"" + reader["FileName"] + "\" and `Type`=\"SMALL\";";
                MyGlobals.executeQuery(query3);
            }

            compareLocalFiles();
        }

        private void compareLocalFiles()
        {
            // Grab all files in the small and large patch folders
            string[] largePatchFiles = Directory.GetFiles(MyGlobals.workDirectory + "data\\patch\\large\\");
            string[] smallPatchFiles = Directory.GetFiles(MyGlobals.workDirectory + "data\\patch\\small\\");

            // Here we'll check to make sure only 1 file is in both the
            // small and large patch folder
            int largeFilesOnFile = largePatchFiles.Length;
            int smallFilesOnFile = smallPatchFiles.Length;

            // Checks on the Large patch file
            if (largeFilesOnFile == 0)
            {
                // We have no large patch file yet, download it
                MyGlobals.downloadLargePatch = true;
                MyGlobals.filesToDownload++;

                // Update the Label
                largePatchStatus.Text = "Outdated";
            }
            else if (largeFilesOnFile > 1)
            {
                // If we have more than 1 file, we'll have to delete everything and redownload
                // the large patch
                MyGlobals.downloadLargePatch = true;
                MyGlobals.filesToDownload++;
                MyGlobals.deleteLargeFilesOnFile = true;

                // Update the Label
                largePatchStatus.Text = "Error, redownloading";

                
            }
            else if (largeFilesOnFile == 1)
            {
                // If we have a Large patch file, get the file name, and compare it to the
                // newest in the database we just pulled
                if (MyGlobals.latestLargePatchFileName == Path.GetFileName(largePatchFiles[0].ToString()))
                {
                    // Don't download anything, we're up to date
                    // Update the Label
                    largePatchStatus.Text = "Up to date";
                }
                else
                {
                    // Delete the local file and grab the newest file
                    MyGlobals.downloadLargePatch = true;
                    MyGlobals.filesToDownload++;
                    MyGlobals.deleteLargeFilesOnFile = true;

                    // Update the Label
                    largePatchStatus.Text = "Outdated";
                }
            }

            // Checks on the Small patch file
            if (smallFilesOnFile == 0)
            {
                // We have no large patch file yet, download it
                MyGlobals.downloadSmallPatch = true;
                MyGlobals.filesToDownload++;

                // Update the Label
                smallPatchStatus.Text = "Outdated";
            }
            else if (smallFilesOnFile > 1)
            {
                // If we have more than 1 file, we'll have to delete everything and redownload
                // the large patch
                MyGlobals.downloadSmallPatch = true;
                MyGlobals.filesToDownload++;
                MyGlobals.deleteSmallFilesOnFile = true;

                // Update the Label
                smallPatchStatus.Text = "Error, redownloading";
            }
            else if (smallFilesOnFile == 1)
            {
                // If we have a Large patch file, get the file name, and compare it to the
                // newest in the database we just pulled
                if (MyGlobals.latestSmallPatchFileName == Path.GetFileName(smallPatchFiles[0].ToString()))
                {
                    // Don't download anything, we're up to date
                    // Update the Label
                    smallPatchStatus.Text = "Up to date";
                }
                else
                {
                    // Delete the local file and grab the newest file
                    MyGlobals.downloadSmallPatch = true;
                    MyGlobals.filesToDownload++;
                    MyGlobals.deleteSmallFilesOnFile = true;

                    // Update the Label
                    smallPatchStatus.Text = "Outdated";
                }
            }
        }

        private void checkDownloader()
        {
            if (MyGlobals.downloadLargePatch || MyGlobals.downloadSmallPatch)
            {
                int transNotifySetting = MyGlobals.getIntFromString(MyGlobals.getSetting("transNotifySettings"));
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
                    if(MyGlobals.deleteLargeFilesOnFile == true)
                    {
                        MyGlobals.action = "LARGE";
                        MyGlobals.install = false;
                        Form installRevertWindow = new installRevertFiles();
                        installRevertWindow.ShowDialog(this);
                    }

                    if(MyGlobals.deleteSmallFilesOnFile == true)
                    {
                        MyGlobals.action = "SMALL";
                        MyGlobals.install = false;
                        Form installRevertWindow = new installRevertFiles();
                        installRevertWindow.ShowDialog(this);
                    }

                    // Launch the downloader
                    Form downloadWindow = new downloadWindow();
                    downloadWindow.ShowDialog(this);

                    // Unrar the files
                    // Handle the large patch
                    if (MyGlobals.downloadLargePatch)
                    {
                        MyGlobals.rarType = "LARGE";
                        unrar(MyGlobals.workDirectory + "data\\patch\\large\\" + MyGlobals.latestLargePatchFileName, MyGlobals.workDirectory + "data\\patch\\large\\files\\");
                    }

                    if (MyGlobals.downloadSmallPatch)
                    {
                        MyGlobals.rarType = "SMALL";
                        unrar(MyGlobals.workDirectory + "data\\patch\\small\\" + MyGlobals.latestSmallPatchFileName, MyGlobals.workDirectory + "data\\patch\\small\\files\\");
                    }

                    checkFolders();
                }
            }
        }

        private void unrar(string file, string location)
        {
            MyGlobals.rarExtractToLocation = location;
            MyGlobals.rarFile = file;
            unrarWindow unrarWindow = new unrarWindow();
            unrarWindow.ShowDialog(this);

            // Remove any readme's that may have been extracted
            removeReadmeIfExists();
        }
        
        private void removeReadmeIfExists()
        {
            // Small Patch
            if(File.Exists(MyGlobals.workDirectory + "data\\patch\\small\\files\\readme.txt")){ File.Delete(MyGlobals.workDirectory + "data\\patch\\small\\files\\readme.txt"); }
            if(File.Exists(MyGlobals.workDirectory + "data\\patch\\small\\files\\original\\readme.txt")){ File.Delete(MyGlobals.workDirectory + "data\\patch\\small\\files\\original\\readme.txt"); }

            // Story Patch
            if(File.Exists(MyGlobals.workDirectory + "data\\patch\\large\\files\\readme.txt")){ File.Delete(MyGlobals.workDirectory + "data\\patch\\large\\files\\readme.txt"); }
            if(File.Exists(MyGlobals.workDirectory + "data\\patch\\large\\files\\original\\readme.txt")){ File.Delete(MyGlobals.workDirectory + "data\\patch\\large\\files\\original\\readme.txt"); }

            // Small Patch
            if(File.Exists(MyGlobals.workDirectory + "data\\patch\\story\\files\\readme.txt")){ File.Delete(MyGlobals.workDirectory + "data\\patch\\story\\files\\readme.txt"); }
            if(File.Exists(MyGlobals.workDirectory + "data\\patch\\story\\files\\original\\readme.txt")){ File.Delete(MyGlobals.workDirectory + "data\\patch\\story\\files\\original\\readme.txt"); }
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
            string rarPath = "";
            if(MyGlobals.installStoryPatch)
            {
                MyGlobals.installStoryPatch = false;
                DirectoryInfo folderInformation = new DirectoryInfo(MyGlobals.workDirectory + "data\\patch\\story\\");
                foreach (FileInfo file in folderInformation.GetFiles())
                {
                    rarPath = Path.GetFileName(file.FullName);
                }
                
                MyGlobals.rarType = "STORY";
                //MessageBox.Show("Extracting: " + MyGlobals.workDirectory + "data\\patch\\story\\" + rarPath + "\n\nTo: " + MyGlobals.workDirectory + "data\\patch\\story\\files\\");
                unrar(MyGlobals.workDirectory + "data\\patch\\story\\" + rarPath, MyGlobals.workDirectory + "data\\patch\\story\\files\\");

                checkFolders();
            }
        }
        
        private void installPatch(string name)
        {
            bool proceedInstall = true;

            // If the client is outdated, and we're trying to install a patch
            if(MyGlobals.clientOutdated)
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
                    MyGlobals.action = name;
                    MyGlobals.install = true;
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
                MyGlobals.action = name;
                MyGlobals.install = false;
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
            Process.Start(MyGlobals.pso2Directory + "PSO2Launcher.exe");
            this.Close();
        }

        private void resizedForm(object sender, EventArgs e)
        {
            // Store the form's location
            MyGlobals.formX = this.Location.X;
            MyGlobals.formY = this.Location.Y;
        }
    }
}

public static class MyGlobals
{
    // The SQLite connection
    public static SQLiteConnection dblink = null;

    public static string oldWorkDirectory = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents") + "\\PSO2 Translation Patch Manager\\";
    public static string workDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\PSO2 Translation Patch Manager\\";
    public static string pso2Directory = "C:\\Program Files (x86)\\SEGA\\PHANTASYSTARONLINE2\\pso2_bin\\";

    public static int usingVersion = 0;

    public static string filename = "";
    public static string type = "";
    public static string processedDate = "";

    public static string latestLargePatchFileName = "";
    public static string latestSmallPatchFileName = "";
    public static string storyTransPatchFileName = "";
    public static bool deleteLargeFilesOnFile = false;
    public static bool deleteSmallFilesOnFile = false;

    public static string action = ""; // LARGE, SMALL, STORY
    public static bool install = false;

    public static bool installSmallPatch = false;
    public static bool installLargePatch = false;
    public static bool installStoryPatch = false;

    public static bool downloadLargePatch = false;
    public static bool downloadSmallPatch = false;
    public static int filesToDownload = 0;

    public static string rarFile = "";
    public static string rarExtractToLocation = "";
    public static string rarType = ""; // LARGE, SMALL, STORY

    public static int formX = 0;
    public static int formY = 0;

    public static bool clientOutdated = false;

    public static bool skipVerification = false; // whether to confirm, when skip update is pressed

    public static bool executeQuery(string query)
    {
        SQLiteCommand sqlcmd;
        sqlcmd = MyGlobals.dblink.CreateCommand();
        sqlcmd.CommandText = query;
        //MessageBox.Show("About to execute query: \r\n" + query);
        sqlcmd.ExecuteNonQuery();
        return true;
    }

    public static bool isNumber(string text)
    {
        Regex regex = new Regex("^[0-9]+$");
        return regex.IsMatch(text);
    }

    public static string getSetting(string name)
    {
        // Grab the latest Large Patch file entry, and then delete all other Large type file entries
        string query2 = "SELECT `Value` FROM `Settings` WHERE `Name`=\""+name+"\";";
        SQLiteCommand cmd = new SQLiteCommand(query2, MyGlobals.dblink);
        SQLiteDataReader reader = cmd.ExecuteReader();
        // For every entry we found
        while (reader.Read())
        {
            // Return the value we got
            return reader["Value"].ToString();
        }

        // Was not found, return NULL
        return "NULL";
    }

    public static void setSetting(string name, string value)
    {
        // Grab the latest Large Patch file entry, and then delete all other Large type file entries
        string query = "UPDATE `Settings` SET `Value`=\""+value+"\" WHERE `Name`=\""+name+"\";";
        executeQuery(query);
    }

    public static void addSetting(string name, string value)
    {
        // Grab the latest Large Patch file entry, and then delete all other Large type file entries
        string query = "INSERT INTO `Settings` (`Name`, `Value`) VALUES (\"" + name + "\", \"" + value + "\");";
        executeQuery(query);
    }

    public static bool settingExists(string name)
    {
        // Grab the latest Large Patch file entry, and then delete all other Large type file entries
        string query2 = "SELECT `Value` FROM `Settings` WHERE `Name`=\"" + name + "\";";
        SQLiteCommand cmd = new SQLiteCommand(query2, MyGlobals.dblink);
        SQLiteDataReader reader = cmd.ExecuteReader();
        // For every entry we found
        while(reader.Read())
        {
            // Setting exists
            return true;
        }

        // Setting didn't exist
        return false;
    }

    public static int getIntFromString(string text)
    {
        int i;
        if (Int32.TryParse(text, out i))
        {
           return i;
        }

        return -1;
    }

    public static void pingSiteWithData(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        //request.Credentials = CredentialCache.DefaultNetworkCredentials; // ??

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    }

    public static string GetMd5Hash(string input)
    {
        MD5 md5Hash = MD5.Create();
        // Convert the input string to a byte array and compute the hash.
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string.
        return sBuilder.ToString();
    }
}