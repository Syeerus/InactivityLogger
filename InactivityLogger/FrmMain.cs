using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace InactivityLogger
{
    public partial class FrmMain : Form
    {
        // Default number of minutes for the idle timer interval.
        public const decimal DefaultIdleTimerIntervalInMinutes = 0.5M;

        // Number of milliseconds that equals to a minute.
        private const int oneMinuteInMilliseconds = 60000;

        // Minimum allowed interval for the idle timer.
        private const int minIdleTimerInterval = 6000;

        // Enligsh US formatting.
        private static readonly CultureInfo englishUSCultureInfo = new CultureInfo("en-US");

        // Timer used for determining idleness.
        private Timer idleTimer = new Timer();

        // Reference to the input monitor.
        private InputMonitor inputMonitor;

        // Set to true when the idle timer reaches its interval and false on input change.
        private bool isIdle = false;

        // The previous size of FrmMain before resizing.
        private Size prevFrmMainSize;

        // The time the user began to idle.
        private DateTime idleStartTime;

        // Change this to not add a message to the log for certain events.
        private bool doNotAddToLog = false;

        public FrmMain(InputMonitor inputMonitor)
        {
            InitializeComponent();
            this.inputMonitor = inputMonitor;
        }
        
        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Text = Program.Name + " v" + Program.VersionDisplay;
            BtnStart.Enabled = true;
            BtnStop.Enabled = false;

            idleTimer.Tick += OnIdleTimerTick;

            prevFrmMainSize = this.Size;

            // Disable the save log button because the log is empty.
            EnableBtnSaveLog(false);

            TxtLog.Font = new Font(FontManager.Get("OpenSans-Regular"), 10.3f, FontStyle.Regular);

            doNotAddToLog = true;
            NumUpDownIdlePeriod.Value = DefaultIdleTimerIntervalInMinutes;
        }

        // Adds a message to the log textbox.
        private void AddToTxtLog(EventType type)
        {
            string typeName = "";
            string extraData = "";
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
                case EventType.IdlePeriodSet:
                {
                    typeName = "Config.Changed";
                    extraData = String.Format("- Idle period changed to {0} minutes.", NumUpDownIdlePeriod.Value);
                    break;
                }
            }

            if (isIdle && type != EventType.WentIdle)
            {
                // Add how long they have been idle for.
                TimeSpan timeDiff = DateTime.Now.Subtract(idleStartTime);
                extraData += " (Idle for " + GetTimeSpanString(timeDiff) + ")";
            }

            if (typeName != "")
            {
                DateTime now = DateTime.Now;
                string dateText = now.ToString("MMM dd, yyyy ", englishUSCultureInfo) +
                    now.ToString("T", englishUSCultureInfo);
                string message = (extraData != "" ? (typeName + " " + extraData) : typeName);
                TxtLog.AppendText(String.Format("[ {0} ]  {1}\r\n", dateText, message));
            }
        }

        // Resets the idle timer and resets idleStartTime.
        private void ResetIdleTimer()
        {
            idleTimer.Stop();
            idleTimer.Start();
            idleStartTime = DateTime.Now;
        }

        // Returns a string of the amount of time given from a TimeSpan object.
        private string GetTimeSpanString(TimeSpan timeDiff)
        {
            string timeFormat = "";
            object[] formatArgs;
            if (timeDiff.Days > 0)
            {
                timeFormat = "{0} days, {1} hours, {2} minutes, {3} seconds";
                formatArgs = new object[]
                {
                    timeDiff.Days,
                    timeDiff.Hours,
                    timeDiff.Minutes,
                    timeDiff.Seconds
                };
            }
            else if (timeDiff.Hours > 0)
            {
                timeFormat = "{0} hours, {1} minutes, {2} seconds";
                formatArgs = new object[]
                {
                    timeDiff.Hours,
                    timeDiff.Minutes,
                    timeDiff.Seconds
                };
            }
            else if (timeDiff.Minutes > 0)
            {
                timeFormat = "{0} minutes, {1} seconds";
                formatArgs = new object[]
                {
                    timeDiff.Minutes,
                    timeDiff.Seconds
                };
            }
            else
            {
                timeFormat = "{0} seconds";
                formatArgs = new object[]
                {
                    timeDiff.Seconds
                };
            }

            return String.Format(timeFormat, formatArgs);
        }

        // Enables or disables the save log button, and adds or removes the TextChanged event handler on TxtLog.
        private void EnableBtnSaveLog(bool enabled)
        {
            BtnSaveLog.Enabled = enabled;
            if (enabled)
            {
                TxtLog.TextChanged -= TxtLog_TextChanged;
            }
            else
            {
                TxtLog.TextChanged += TxtLog_TextChanged;
            }
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

            // The log is empty, so it can't be saved.
            EnableBtnSaveLog(false);
        }

        private void NumUpDownIdlePeriod_ValueChanged(object sender, EventArgs e)
        {
            int milliseconds = 0;
            if (!int.TryParse(Math.Round(NumUpDownIdlePeriod.Value * oneMinuteInMilliseconds).ToString(), out milliseconds) ||
                milliseconds < minIdleTimerInterval)
            {
                // Couldn't parse the integer or it's too low.
                milliseconds = minIdleTimerInterval;
                NumUpDownIdlePeriod.Value = (decimal)milliseconds / oneMinuteInMilliseconds;
                // Return because the above statement will raise another event.
                return;
            }

            idleTimer.Interval = milliseconds;
            if (idleTimer.Enabled)
            {
                // Reset if enabled.
                ResetIdleTimer();
            }

            if (!doNotAddToLog)
            {
                AddToTxtLog(EventType.IdlePeriodSet);
            }
            else
            {
                // Reset.
                doNotAddToLog = false;
            }
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            int widthDiff = this.Size.Width - prevFrmMainSize.Width;
            int heightDiff = this.Size.Height - prevFrmMainSize.Height;

            SuspendLayout();

            // TxtLog should resize with the right and bottom edges.
            TxtLog.Width += widthDiff;
            TxtLog.Height += heightDiff;

            // BtnClear and BtnSaveLog should follow the right and bottom edges.
            BtnClear.Left += widthDiff;
            BtnClear.Top += heightDiff;
            BtnSaveLog.Left += widthDiff;
            BtnSaveLog.Top += heightDiff;

            // The rest of the controls should follow the bottom edge.
            BtnStart.Top += heightDiff;
            BtnStop.Top += heightDiff;
            LblIdlePeriod.Top += heightDiff;
            NumUpDownIdlePeriod.Top += heightDiff;
            LblIdleMinutes.Top += heightDiff;

            ResumeLayout();

            prevFrmMainSize = this.Size;
        }

        private void BtnSaveLog_Click(object sender, EventArgs e)
        {
            string filename = DateTime.Now.ToString("F", englishUSCultureInfo).Replace(':', '-') + ".log";
            var saveLogFileDialog = new SaveFileDialog
            {
                AddExtension = false,
                DefaultExt = "log",
                FileName = filename,
                Filter = "Log files|*.log|Text Files|*.txt|All files|*"
            };

            if (saveLogFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var writer = new StreamWriter(saveLogFileDialog.OpenFile()) )
                    {
                        writer.Write(TxtLog.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured: " + ex.Message);
                }
            }
        }

        private void TxtLog_TextChanged(object sender, EventArgs e)
        {
            // The log isn't empty, so it can be saved.
            EnableBtnSaveLog(true);
        }
    }
}
