namespace PlayMp3Form
{
    partial class Form1
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
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.components = new System.ComponentModel.Container();
            this.lblMP3File = new System.Windows.Forms.Label();
            this.txtMp3File = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.trackBarDuration = new System.Windows.Forms.TrackBar();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblVolume = new System.Windows.Forms.Label();
            this.trackBarVolume = new System.Windows.Forms.TrackBar();
            this.lblDuration = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMP3File
            // 
            this.lblMP3File.AutoSize = true;
            this.lblMP3File.Location = new System.Drawing.Point(12, 15);
            this.lblMP3File.Name = "lblMP3File";
            this.lblMP3File.Size = new System.Drawing.Size(51, 13);
            this.lblMP3File.TabIndex = 0;
            this.lblMP3File.Text = "MP3 File:";
            // 
            // txtMp3File
            // 
            this.txtMp3File.Location = new System.Drawing.Point(60, 12);
            this.txtMp3File.Name = "txtMp3File";
            this.txtMp3File.Size = new System.Drawing.Size(376, 20);
            this.txtMp3File.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(442, 10);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(39, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // trackBarDuration
            // 
            this.trackBarDuration.Location = new System.Drawing.Point(15, 39);
            this.trackBarDuration.Maximum = 100000;
            this.trackBarDuration.Name = "trackBarDuration";
            this.trackBarDuration.Size = new System.Drawing.Size(463, 45);
            this.trackBarDuration.TabIndex = 3;
            this.trackBarDuration.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarDuration.Scroll += new System.EventHandler(this.trackBarDuration_Scroll);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(12, 90);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 23);
            this.btnPlay.TabIndex = 4;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(102, 90);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 5;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(192, 90);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblVolume
            // 
            this.lblVolume.AutoSize = true;
            this.lblVolume.Location = new System.Drawing.Point(273, 95);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(45, 13);
            this.lblVolume.TabIndex = 7;
            this.lblVolume.Text = "Volume:";
            // 
            // trackBarVolume
            // 
            this.trackBarVolume.Location = new System.Drawing.Point(324, 90);
            this.trackBarVolume.Maximum = 10000;
            this.trackBarVolume.Name = "trackBarVolume";
            this.trackBarVolume.Size = new System.Drawing.Size(154, 45);
            this.trackBarVolume.TabIndex = 8;
            this.trackBarVolume.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarVolume.Value = 7000;
            this.trackBarVolume.Scroll += new System.EventHandler(this.trackBarVolume_Scroll);
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(424, 71);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(54, 13);
            this.lblDuration.TabIndex = 9;
            this.lblDuration.Text = "0:00/0:00";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 129);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.trackBarVolume);
            this.Controls.Add(this.lblVolume);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.trackBarDuration);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtMp3File);
            this.Controls.Add(this.lblMP3File);
            this.Name = "Form1";
            this.Text = "Play MP3!";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMP3File;
        private System.Windows.Forms.TextBox txtMp3File;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TrackBar trackBarDuration;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.TrackBar trackBarVolume;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Timer timer1;
    }
}

