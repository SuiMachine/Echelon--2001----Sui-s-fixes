using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace RegisterGame
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Get current directory
            var localDirectory = Directory.GetCurrentDirectory();

            //Build up registry paths (don't need to care about Wow6432Node, since that's taken care of by operating system when running 32bit exe)
            var regMadia = string.Join("\\", Registry.LocalMachine.ToString(), "SOFTWARE", "Madia", "Echelon");
            var regPathUninstall = string.Join("\\", Registry.LocalMachine.ToString(), "SOFTWARE", "Microsoft", "Windows", "CurrentVersion", "Uninstall", "Echelon");
            try
            {
                //Try to set up registry keys with current directory
                Registry.SetValue(regMadia, "Path1", Path.Combine(localDirectory, "Data") + "\\", RegistryValueKind.String);
                Registry.SetValue(regMadia, "Path2", Path.Combine(localDirectory, "Data") + "\\", RegistryValueKind.String);

                Registry.SetValue(regPathUninstall, "InstallLocation", localDirectory + "\\", RegistryValueKind.String);
                Registry.SetValue(regPathUninstall, "InstallSource", localDirectory + "\\", RegistryValueKind.String);
            }
            catch(Exception e)
            {
                //Error... blahblahblah. Exit.
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            //Success
            MessageBox.Show("Done! Registry keys added succesfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
