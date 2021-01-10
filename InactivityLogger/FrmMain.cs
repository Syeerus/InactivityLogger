using System;
using System.Globalization;
using System.Windows.Forms;

namespace InactivityLogger
{
    public partial class FrmMain : Form
    {
        // Default number of minutes for the idle timer interval.
        public const double DefaultIdleTimerIntervalInMinutes = 0.5;

        // Number of milliseconds that equals to a minute.
        private const int oneMinuteInMilliseconds = 60000;

        // Minimum allowed interval for the idle timer.
        private const int minIdleTimerInterval = 1000;

        // Enligsh US formatting.
        private static readonly CultureInfo englishUSCultureInfo = new CultureInfo("en-US");

        // Gets or sets the idle timer interval in minutes.
        public double IdleTimerIntervalInMinutes
        {
            get
            {
                return idleTimerIntervalInMinutes;
            }

            set
            {
                int milliseconds = (int)Math.Round(value * oneMinuteInMilliseconds);
                if (milliseconds < minIdleTimerInterval)
                {
                    // User supplied value is too low.
                    milliseconds = minIdleTimerInterval;
                    idleTimerIntervalInMinutes = (double)milliseconds / oneMinuteInMilliseconds;
                }
                else
                {
                    idleTimerIntervalInMinutes = value;
                }

                idleTimer.Interval = milliseconds;
                if (idleTimer.Enabled)
                {
                    // Reset if enabled.
                    ResetIdleTimer();
                }
            }
        }

        // The display value for the idle timer interval.
        private double idleTimerIntervalInMinutes = 0.0;

        // Timer used for determining idleness.
        private Timer idleTimer = new Timer();

        // Reference to the input monitor.
        private InputMonitor inputMonitor;

        // Set to true when the idle timer reaches its interval and false on input change.
        private bool isIdle = false;

        public FrmMain(InputMonitor inputMonitor)
        {
            IdleTimerIntervalInMinutes = DefaultIdleTimerIntervalInMinutes;
            idleTimer.Tick += OnIdleTimerTick;

            InitializeComponent();
            this.inputMonitor = inputMonitor;
        }
        
        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Text += " v" + Program.VersionDisplay;
            BtnStart.Enabled = true;
            BtnStop.Enabled = false;
        }

        // Adds a message to the log textbox.
        private void AddToTxtLog(EventType type)
        {
            string typeName = "";
            switch (type)
            {
                case EventType.Started:
                {
                    typeName = "Logging.Started";
                    break;
                }
                case EventType.Stopped:
                {
                    typeName = "Logging.Stopped";
                    break;
                }
                case EventType.WentIdle:
                {
                    typeName = "User.Idle";
                    break;
                }
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
                case EventType.KeyDown:
                {
                    typeName = "Keyboard.KeyDown";
                    break;
                }
            }

            if (typeName != "")
            {
                string date = DateTime.Now.ToString(englishUSCultureInfo);
                TxtLog.AppendText(String.Format("[ {0} ]  {1}\r\n", date, typeName));
            }
        }

        // Resets the idle timer.
        private void ResetIdleTimer()
        {
            idleTimer.Stop();
            idleTimer.Start();
        }

        // Event handler for when input changed.
        private void OnInputChanged(object sender, EventType type)
        {
            if (isIdle)
            {
                AddToTxtLog(type);
                isIdle = false;
            }

            ResetIdleTimer();
        }

        // Event handler for the idle timer's tick event.
        private void OnIdleTimerTick(object sender, EventArgs e)
        {
            isIdle = true;
            AddToTxtLog(EventType.WentIdle);
            idleTimer.Stop();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            AddToTxtLog(EventType.Started);

            inputMonitor.InputChanged += OnInputChanged;
            inputMonitor.Start();

            idleTimer.Start();

            BtnStart.Enabled = false;
            BtnStop.Enabled = true;
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            AddToTxtLog(EventType.Stopped);

            inputMonitor.InputChanged -= OnInputChanged;
            inputMonitor.Stop();

            idleTimer.Stop();
            isIdle = false;

            BtnStart.Enabled = true;
            BtnStop.Enabled = false;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            TxtLog.Clear();
        }
    }
}
