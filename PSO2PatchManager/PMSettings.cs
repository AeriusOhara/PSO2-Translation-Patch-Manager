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
using System.Diagnostics;

namespace PSO2PatchManager
{
    public partial class PMSettings : Form
    {
        int selectedTransNotification = 0;
        int selectedPSO2Notification = 0;
        string PSO2Directory = "";
        bool firstRun = false;
        string[] skipVersionTextFile = new string[] { "0" };

        public PMSettings()
        {
            InitializeComponent();
        }

        private void PMSettings_Load(object sender, EventArgs e)
        {
            // Set Form location
            this.Location = new Point((MyGlobals.formX + 10), (MyGlobals.formY + 30));

            // Check if this is our first run or not, if yes, update the label
            string tmpString = MyGlobals.getSetting("firstRun");
            if(tmpString == "")
            {
                firstRun = true;
                titleLabel.Text = "PSO2 Patch Manager Settings (First Run Setup)";
                titleLabel2.Text = "PSO2 Patch Manager Settings (First Run Setup)";
                titleLabel3.Text = "PSO2 Patch Manager Settings (First Run Setup)";

                checkForUpdates.Enabled = false;
                usingVersion.Enabled = false;
                this.ControlBox = false;
            }
            else
            {
                titleLabel.Text = "PSO2 Patch Manager Settings";
                titleLabel2.Text = "PSO2 Patch Manager Settings";
                titleLabel3.Text = "PSO2 Patch Manager Settings";
            }

            // Check if any settings exist, if they do, apply them
            tmpString = MyGlobals.getSetting("pso2Directory");
            if(tmpString != "")
            {
                PSO2Directory = MyGlobals.getSetting("pso2Directory");
                pso2DirectoryTextBox.Text = PSO2Directory;
            }

            tmpString = MyGlobals.getSetting("transNotifySettings");
            if(tmpString != "" && tmpString != "0")
            {
                selectedTransNotification = MyGlobals.getIntFromString(MyGlobals.getSetting("transNotifySettings"));
                if(selectedTransNotification == 1){transNotify1.Checked = true;}
                else if(selectedTransNotification == 2){transNotify2.Checked = true;}
                else if(selectedTransNotification == 3){transNotify3.Checked = true;}
            }
            else
            {
                // No setting was found, select a default one
                transNotify1.Checked = true;
            }
            
            tmpString = MyGlobals.getSetting("pso2NotifySettings");
            if(tmpString != "" && tmpString != "0")
            {
                selectedPSO2Notification = MyGlobals.getIntFromString(MyGlobals.getSetting("pso2NotifySettings"));
                if(selectedPSO2Notification == 1){pso2Notify1.Checked = true;}
                else if(selectedPSO2Notification == 2){pso2Notify2.Checked = true;}
                else if(selectedPSO2Notification == 3){pso2Notify3.Checked = true;}
            }
            else
            {
                // No setting was found, select a default one
                pso2Notify1.Checked = true;
            }

            // Update the usingVersion combobox
            if(MyGlobals.usingVersion == 0){
                usingVersion.SelectedIndex = 0;
            }else{
                usingVersion.SelectedIndex = 1;
            }

            // Grab the skip confirmation checkbox value
            skipVersionTextFile = File.ReadAllLines(MyGlobals.workDirectory + "skip.dat");
            if (skipVersionTextFile[2] == "0")
            {
                MyGlobals.skipVerification = false;
            }
            else
            {
                MyGlobals.skipVerification = true;
                doNotAskCheckBox.Checked = true;
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "Browse for PSO2.exe";
            fDialog.Filter = "PSO2.exe|pso2.exe";
            fDialog.InitialDirectory = @"C:\";

            if(fDialog.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(fDialog.FileName.ToString());
                string directory = Path.GetDirectoryName(fDialog.FileName.ToString()) + "\\";
                PSO2Directory = directory;
                pso2DirectoryTextBox.Text = PSO2Directory;
            }
        }

        private void checkTransN()
        {
            if(transNotify1.Checked == false && transNotify2.Checked == false && transNotify3.Checked == false)
            {
                MessageBox.Show("You must have at least one option selected in this section.");
                transNotify1.Checked = true;
            }
        }

        private void checkPSO2N()
        {
            if(pso2Notify1.Checked == false && pso2Notify2.Checked == false && pso2Notify3.Checked == false)
            {
                MessageBox.Show("You must have at least one option selected in this section.");
                pso2Notify1.Checked = true;
            }
        }

        private void transNotify1_CheckedChanged(object sender, EventArgs e)
        {
            if(transNotify1.Checked == true)
            {
                transNotify2.Checked = false;
                transNotify3.Checked = false;

                selectedTransNotification = 1;
            }

            checkTransN();
        }

        private void transNotify2_CheckedChanged(object sender, EventArgs e)
        {
            if(transNotify2.Checked == true)
            {
                transNotify1.Checked = false;
                transNotify3.Checked = false;

                selectedTransNotification = 2;
            }

            checkTransN();
        }

        private void transNotify3_CheckedChanged(object sender, EventArgs e)
        {
            if(transNotify3.Checked == true)
            {
                transNotify1.Checked = false;
                transNotify2.Checked = false;

                selectedTransNotification = 3;
            }

            checkTransN();
        }

        private void pso2Notify1_CheckedChanged(object sender, EventArgs e)
        {
            if(pso2Notify1.Checked == true)
            {
                pso2Notify2.Checked = false;
                pso2Notify3.Checked = false;

                selectedPSO2Notification = 1;
            }

            checkPSO2N();
        }

        private void pso2Notify2_CheckedChanged(object sender, EventArgs e)
        {
            if(pso2Notify2.Checked == true)
            {
                pso2Notify1.Checked = false;
                pso2Notify3.Checked = false;

                selectedPSO2Notification = 2;
            }

            checkPSO2N();
        }

        private void pso2Notify3_CheckedChanged(object sender, EventArgs e)
        {
            if(pso2Notify3.Checked == true)
            {
                pso2Notify1.Checked = false;
                pso2Notify2.Checked = false;

                selectedPSO2Notification = 3;
            }

            checkPSO2N();
        }

        private void saveSettings_Click(object sender, EventArgs e)
        {
            if(checkForm())
            {
                if(firstRun == true){MyGlobals.setSetting("firstRun", "1");}
                MyGlobals.setSetting("transNotifySettings", selectedTransNotification.ToString());
                MyGlobals.setSetting("pso2NotifySettings", selectedPSO2Notification.ToString());
                MyGlobals.setSetting("pso2Directory", PSO2Directory);

                this.Close();
            }
        }

        private bool checkForm()
        {
            if(selectedTransNotification != 0 && selectedPSO2Notification != 0 && PSO2Directory != "")
            {
                return true;
            }
            else
            {
                // An error was found, check which one
                if(selectedTransNotification == 0)
                {
                    MessageBox.Show("You have to select at least one Notification option regarding the Translation Files.");
                }
                else if(selectedPSO2Notification == 0)
                {
                    MessageBox.Show("You have to select at least one Notification option regarding the PSO2 Update.");
                }
                else if(PSO2Directory == "")
                {
                    MessageBox.Show("You have to select the correct PSO2 Directory before this application can work.");
                }
            }

            return false;
        }

        private void checkForUpdates_Click(object sender, EventArgs e)
        {
            Process starUpdater = new Process();
            starUpdater.StartInfo.FileName = "StarUpdater.exe";
            starUpdater.StartInfo.Arguments = " -run -check";
            starUpdater.Start();
            Application.Exit();
        }

        private void usingVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = usingVersion.SelectedIndex;

            if(selectedIndex != MyGlobals.usingVersion){
                // Build-specific messages
                string[] specificMessages = {"", ""};
                specificMessages[0] = "The Stable build offers like the name suggests, stable builds where you are unlikely to come across any crashes.";
                specificMessages[1] = "The Developer build offers the newest updates that are experimental, there are chances of crashes or oddities, however.";

                string message = "Are you sure you wish to switch to the " + usingVersion.SelectedText + " build?\n" + specificMessages[selectedIndex];
                DialogResult dialog = MessageBox.Show(message, "Are you sure?", MessageBoxButtons.YesNo);
                if(dialog == DialogResult.Yes){
                    Form switchDevStable = new switchDevStable();
                    switchDevStable.ShowDialog(this);
                }else{
                    usingVersion.SelectedIndex = MyGlobals.usingVersion;
                }
            }
        }

        private void doNotAskCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Disable the checkbox
            doNotAskCheckBox.Enabled = false;

            // Handle the values
            string skipVerificationValue = "";
            if(doNotAskCheckBox.Checked == true)
            {
                MyGlobals.skipVerification = true;
                skipVerificationValue += "1";
            }
            else
            {
                MyGlobals.skipVerification = false;
                skipVerificationValue += "0";
            }

            // Write the values into the file
            string versionFileContents = skipVersionTextFile[0] + "\n";
            versionFileContents += skipVersionTextFile[1] + "\n";
            versionFileContents += skipVerificationValue;
            File.WriteAllText(@MyGlobals.workDirectory+"skip.dat", versionFileContents);

            // Enable the checkbox
            doNotAskCheckBox.Enabled = true;
        }
    }
}
