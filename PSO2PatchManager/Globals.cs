using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Net;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Diagnostics;

namespace PSO2PatchManager
{
    public static class Globals
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
            sqlcmd = Globals.dblink.CreateCommand();
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
            SQLiteCommand cmd = new SQLiteCommand(query2, Globals.dblink);
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
            SQLiteCommand cmd = new SQLiteCommand(query2, Globals.dblink);
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
            if(Int32.TryParse(text, out i))
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
            for(int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static bool createDirectory(string directory)
        {
            try
            {
                Directory.CreateDirectory(directory);

                return true;
            }
            catch (System.Exception excpt)
            {
                MessageBox.Show("Could not create the folder " + directory + ", please run this application with Administrative Privileges if this problem persists.");
                Application.Exit();

                return false;
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
}
