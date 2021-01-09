using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Runtime;

namespace InactivityLogger
{
    static class Program
    {
        // The version of the application.
        static Version version = typeof(Program).Assembly.GetName().Version;

        // The displayable version of the application.
        static string versionDisplay = version.ToString(4);

        // Returns a displayable version number.
        public static string GetVersionDisplay()
        {
            return versionDisplay;
        }

        /// The main entry point for the application.
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var inputMonitor = new InputMonitor();
            Application.Run(new FrmMain(inputMonitor));
        }
    }
}
