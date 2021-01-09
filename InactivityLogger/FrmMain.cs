using System;
using System.Windows.Forms;
using System.Globalization;

namespace InactivityLogger
{
    public partial class FrmMain : Form
    {
        // Enligsh US formatting.
        private static readonly CultureInfo englishUSCultureInfo = new CultureInfo("en-US");

        // Reference to the input monitor.
        private InputMonitor inputMonitor;

        public FrmMain(InputMonitor inputMonitor)
        {
            InitializeComponent();
            this.inputMonitor = inputMonitor;
        }
        
        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Text += " v" + Program.GetVersionDisplay();
            BtnStart.Enabled = true;
            BtnStop.Enabled = false;
        }

        // Event handler for when input changed.
        private void OnInputChanged(object sender, EventType type)
        {
            string typeName = "";
            switch (type)
            {
                case EventType.MouseLeftButtonDown:
                {
                    typeName = "Mouse.LeftDown";
                    break;
                }
                case EventType.MouseRightButtonDown:
                {
                    typeName = "Mouse.RightDown";
                    break;
                }
                case EventType.MouseMove:
                {
                    typeName = "Mouse.Move";
                    break;
                }
                case EventType.MouseWheel:
                {
                    typeName = "Mouse.Wheel";
                    break;
                }
                case EventType.MouseHorizontalWheel:
                {
                    typeName = "Mouse.HorizontalWheel";
                    break;
                }
            }
            
            string date = DateTime.Now.ToString(englishUSCultureInfo);
            TxtLog.AppendText(String.Format("[ {0} ]  {1}\r\n", date, typeName));
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            inputMonitor.InputChanged += OnInputChanged;
            inputMonitor.Start();
            BtnStart.Enabled = false;
            BtnStop.Enabled = true;
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            inputMonitor.InputChanged -= OnInputChanged;
            inputMonitor.Stop();
            BtnStart.Enabled = true;
            BtnStop.Enabled = false;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            TxtLog.Clear();
        }
    }
}
