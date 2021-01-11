using System;
using System.Windows.Forms;

namespace InactivityLogger
{
    static class Program
    {
        public static readonly string Name = Application.ProductName;

        // The version of the application.
        public static readonly Version Version = typeof(Program).Assembly.GetName().Version;

        // The displayable version of the application.
        public static readonly string VersionDisplay = Version.ToString(4);

        /// The main entry point for the application.
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var inputMonitor = new InputMonitor())
            {
                Application.Run(new FrmMain(inputMonitor));
            }

            FontManager.CleanUp();
        }
    }
}
