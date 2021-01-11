namespace InactivityLogger
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                    components = null;
                }

                if (idleTimer != null)
                {
                    idleTimer.Tick -= OnIdleTimerTick;
                    idleTimer.Dispose();
                    idleTimer = null;
                }

                if (inputMonitor != null)
                {
                    inputMonitor.InputChanged -= OnInputChanged;
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TxtLog = new System.Windows.Forms.TextBox();
            this.BtnStart = new System.Windows.Forms.Button();
            this.BtnStop = new System.Windows.Forms.Button();
            this.BtnClear = new System.Windows.Forms.Button();
            this.NumUpDownIdleInterval = new System.Windows.Forms.NumericUpDown();
            this.LblGoIdleAfter = new System.Windows.Forms.Label();
            this.LblMinutes = new System.Windows.Forms.Label();
            this.BtnSaveLog = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDownIdleInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtLog
            // 
            this.TxtLog.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.TxtLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtLog.Location = new System.Drawing.Point(12, 15);
            this.TxtLog.Multiline = true;
            this.TxtLog.Name = "TxtLog";
            this.TxtLog.ReadOnly = true;
            this.TxtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtLog.Size = new System.Drawing.Size(513, 341);
            this.TxtLog.TabIndex = 0;
            this.TxtLog.WordWrap = false;
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(12, 359);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(75, 23);
            this.BtnStart.TabIndex = 1;
            this.BtnStart.Text = "Start";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // BtnStop
            // 
            this.BtnStop.Location = new System.Drawing.Point(93, 359);
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.Size = new System.Drawing.Size(75, 23);
            this.BtnStop.TabIndex = 2;
            this.BtnStop.Text = "Stop";
            this.BtnStop.UseVisualStyleBackColor = true;
            this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // BtnClear
            // 
            this.BtnClear.Location = new System.Drawing.Point(450, 388);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(75, 23);
            this.BtnClear.TabIndex = 3;
            this.BtnClear.Text = "Clear";
            this.BtnClear.UseVisualStyleBackColor = true;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // NumUpDownIdleInterval
            // 
            this.NumUpDownIdleInterval.DecimalPlaces = 1;
            this.NumUpDownIdleInterval.Location = new System.Drawing.Point(268, 362);
            this.NumUpDownIdleInterval.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.NumUpDownIdleInterval.Name = "NumUpDownIdleInterval";
            this.NumUpDownIdleInterval.Size = new System.Drawing.Size(85, 20);
            this.NumUpDownIdleInterval.TabIndex = 4;
            this.NumUpDownIdleInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NumUpDownIdleInterval.ThousandsSeparator = true;
            this.NumUpDownIdleInterval.ValueChanged += new System.EventHandler(this.NumUpDownIdlePeriod_ValueChanged);
            // 
            // LblGoIdleAfter
            // 
            this.LblGoIdleAfter.AutoSize = true;
            this.LblGoIdleAfter.Location = new System.Drawing.Point(198, 364);
            this.LblGoIdleAfter.Name = "LblGoIdleAfter";
            this.LblGoIdleAfter.Size = new System.Drawing.Size(64, 13);
            this.LblGoIdleAfter.TabIndex = 5;
            this.LblGoIdleAfter.Text = "Go idle after";
            // 
            // LblMinutes
            // 
            this.LblMinutes.AutoSize = true;
            this.LblMinutes.Location = new System.Drawing.Point(359, 364);
            this.LblMinutes.Name = "LblMinutes";
            this.LblMinutes.Size = new System.Drawing.Size(52, 13);
            this.LblMinutes.TabIndex = 6;
            this.LblMinutes.Text = "minute(s).";
            // 
            // BtnSaveLog
            // 
            this.BtnSaveLog.Location = new System.Drawing.Point(450, 359);
            this.BtnSaveLog.Name = "BtnSaveLog";
            this.BtnSaveLog.Size = new System.Drawing.Size(75, 23);
            this.BtnSaveLog.TabIndex = 7;
            this.BtnSaveLog.Text = "Save Log";
            this.BtnSaveLog.UseVisualStyleBackColor = true;
            this.BtnSaveLog.Click += new System.EventHandler(this.BtnSaveLog_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 419);
            this.Controls.Add(this.BtnSaveLog);
            this.Controls.Add(this.LblMinutes);
            this.Controls.Add(this.LblGoIdleAfter);
            this.Controls.Add(this.NumUpDownIdleInterval);
            this.Controls.Add(this.BtnClear);
            this.Controls.Add(this.BtnStop);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.TxtLog);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(553, 458);
            this.Name = "FrmMain";
            this.Text = "Inactivity Logger";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDownIdleInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtLog;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Button BtnStop;
        private System.Windows.Forms.Button BtnClear;
        private System.Windows.Forms.NumericUpDown NumUpDownIdleInterval;
        private System.Windows.Forms.Label LblGoIdleAfter;
        private System.Windows.Forms.Label LblMinutes;
        private System.Windows.Forms.Button BtnSaveLog;
    }
}

