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
                //MessageBox.Show("Please do not run this application manually.\n\nRun StarBoot.exe instead.");
                //Process.GetCurrentProcess().Kill();

                // Do the below so they can just shortcut this .exe, less confusing
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
