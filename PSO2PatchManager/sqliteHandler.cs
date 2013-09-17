using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Data.SQLite;

namespace PSO2PatchManager
{
    class sqliteHandler
    {
        public void initSqlite()
        {
            // If the Large patch directory doesn't exist, create it
            bool workDirectoryExists = Directory.Exists(Globals.workDirectory);
            if (!workDirectoryExists)
            {
                if(Globals.createDirectory(Globals.workDirectory))
                {
                    workDirectoryExists = true;
                }
            }

            // Check if our database exixts, if not, create it
            Globals.dblink = null;
            try
            {
                Globals.dblink = new SQLiteConnection("Data Source=" + Globals.workDirectory + "data\\data.s3db;Version=3;FailIfMissing=True;");
            }
            catch (SQLiteException ex)
            {
                 //MessageBox.Show(ex.ToString());
            }

            try
            {
                //MessageBox.Show("Opening Database File");
                Globals.dblink.Open();

                initTables();
            }
            catch (SQLiteException ex)
            {
                //MessageBox.Show(ex.ToString());
                
                // Create the folder 'data'
                Globals.createDirectory(Globals.workDirectory + "data");

                //MessageBox.Show("Creating Database File");
                SQLiteConnection.CreateFile(Globals.workDirectory + "data\\data.s3db");

                // Try to open the file once more, now we've created it
                try
                {
                    // Try opening the file
                    Globals.dblink.Open();

                    initTables();
                }
                catch (SQLiteException ex3)
                {
                    // Couldn't open the database file, which means it wasn't
                    // created, no administrative priviledges perhaps? Throw
                    // an error
                    MessageBox.Show("Could not create the database file directory!\r\nTry running this application with Administrative Privileges.");
                    Application.Exit();
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
            Globals.executeQuery(query);

            // Clear the file list
            query = "DELETE FROM `FileList`;";
            Globals.executeQuery(query);

            // Settings table
            query = "CREATE TABLE IF NOT EXISTS [Settings](";
            query += "[Name] TEXT NULL,";
            query += "[Value] TEXT NULL";
            query += ");";
            Globals.executeQuery(query);

            // Set up the values
            if(!Globals.settingExists("firstRun"))              { Globals.addSetting("firstRun", ""); }
            if(!Globals.settingExists("workDirectory"))         { Globals.addSetting("workDirectory", ""); }
            if(!Globals.settingExists("pso2Directory"))         { Globals.addSetting("pso2Directory", ""); }
            if(!Globals.settingExists("largePatchOnFile"))      { Globals.addSetting("largePatchOnFile", ""); }
            if(!Globals.settingExists("smallPatchOnFile"))      { Globals.addSetting("smallPatchOnFile", ""); }
            if(!Globals.settingExists("transNotifySettings"))   { Globals.addSetting("transNotifySettings", ""); }
            if(!Globals.settingExists("pso2NotifySettings"))    { Globals.addSetting("pso2NotifySettings", ""); }
        }
    }
}
