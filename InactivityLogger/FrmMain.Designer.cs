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
            this.NumUpDownIdlePeriod = new System.Windows.Forms.NumericUpDown();
            this.LblIdlePeriod = new System.Windows.Forms.Label();
            this.LblIdleMinutes = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDownIdlePeriod)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtLog
            // 
            this.TxtLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtLog.Location = new System.Drawing.Point(12, 12);
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
            this.BtnClear.Location = new System.Drawing.Point(450, 359);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(75, 23);
            this.BtnClear.TabIndex = 3;
            this.BtnClear.Text = "Clear";
            this.BtnClear.UseVisualStyleBackColor = true;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // NumUpDownIdlePeriod
            // 
            this.NumUpDownIdlePeriod.DecimalPlaces = 1;
            this.NumUpDownIdlePeriod.Location = new System.Drawing.Point(259, 362);
            this.NumUpDownIdlePeriod.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.NumUpDownIdlePeriod.Name = "NumUpDownIdlePeriod";
            this.NumUpDownIdlePeriod.Size = new System.Drawing.Size(85, 20);
            this.NumUpDownIdlePeriod.TabIndex = 4;
            this.NumUpDownIdlePeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NumUpDownIdlePeriod.ThousandsSeparator = true;
            this.NumUpDownIdlePeriod.ValueChanged += new System.EventHandler(this.NumUpDownIdlePeriod_ValueChanged);
            // 
            // LblIdlePeriod
            // 
            this.LblIdlePeriod.AutoSize = true;
            this.LblIdlePeriod.Location = new System.Drawing.Point(197, 364);
            this.LblIdlePeriod.Name = "LblIdlePeriod";
            this.LblIdlePeriod.Size = new System.Drawing.Size(60, 13);
            this.LblIdlePeriod.TabIndex = 5;
            this.LblIdlePeriod.Text = "Idle Period:";
            // 
            // LblIdleMinutes
            // 
            this.LblIdleMinutes.AutoSize = true;
            this.LblIdleMinutes.Location = new System.Drawing.Point(350, 364);
            this.LblIdleMinutes.Name = "LblIdleMinutes";
            this.LblIdleMinutes.Size = new System.Drawing.Size(44, 13);
            this.LblIdleMinutes.TabIndex = 6;
            this.LblIdleMinutes.Text = "Minutes";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 428);
            this.Controls.Add(this.LblIdleMinutes);
            this.Controls.Add(this.LblIdlePeriod);
            this.Controls.Add(this.NumUpDownIdlePeriod);
            this.Controls.Add(this.BtnClear);
            this.Controls.Add(this.BtnStop);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.TxtLog);
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.Text = "Inactivity Logger";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDownIdlePeriod)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtLog;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Button BtnStop;
        private System.Windows.Forms.Button BtnClear;
        private System.Windows.Forms.NumericUpDown NumUpDownIdlePeriod;
        private System.Windows.Forms.Label LblIdlePeriod;
        private System.Windows.Forms.Label LblIdleMinutes;
    }
}

