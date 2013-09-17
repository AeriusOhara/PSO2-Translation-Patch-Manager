using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

namespace PSO2PatchManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // Check the received parameters, if the correct one was
            // received, run the app, if none were received, go
            // back to StarBoot.exe
            if(args.Count() != 0)
            {
                foreach (var arg in args)
                {
                    if(arg != "-run")
                    {
                        MessageBox.Show("Wrong Parameter received. The application will now quit.");
                        Process.GetCurrentProcess().Kill();
                    }
                }
            }
            else
            {
                // Shutdown this app and do the loop of:
                // StarBoot->StarUpdater->PSO2 Translation Patch Manager
                // That way the user can shortcut this main .exe and not
                // have to shortcut StarBoot.exe
                Process starUpdater = new Process();
                starUpdater.StartInfo.FileName = "StarBoot.exe";
                starUpdater.Start();
                Process.GetCurrentProcess().Kill();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new mainForm());
        }
    }
}
