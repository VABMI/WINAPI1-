using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MP3NetLib;

namespace PlayMp3Form
{
    public partial class Form1 : Form
    {
        private bool playing;
        private bool pause;
        private const int SLIDER_TIME_ELAPSE_MAX_POS = 10000;
        private MP3Player player = new MP3Player();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            playing = false;
            pause = false;

            trackBarVolume.Minimum = 0;
            trackBarVolume.Maximum = 10000;
            trackBarVolume.Value = 7000;

            trackBarDuration.Minimum = 0;
            trackBarDuration.Maximum = SLIDER_TIME_ELAPSE_MAX_POS;
            trackBarDuration.Value = 0;

            btnPlay.Enabled = true;
            btnPause.Enabled = false;
            btnStop.Enabled = false;

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open MP3 Files";
            openFileDialog1.Filter = "MP3 Files|*.mp3|All Files|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtMp3File.Text = openFileDialog1.FileName.ToString();
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (pause)
            {
                player.Play();
                pause = false;

                btnPlay.Enabled = false;
                btnPause.Enabled = true;
                btnStop.Enabled = true;
            }
            else
            {
                if (string.IsNullOrEmpty(txtMp3File.Text))
                {
                    MessageBox.Show("No MP3 file is specified!", "Error");
                    return;
                }

                if (File.Exists(txtMp3File.Text) == false)
                {
                    MessageBox.Show("MP3 file does not exists!", "Error");
                    return;
                }

                if (player.Load(txtMp3File.Text) && player.Play())
                {
                    playing = true;
                    pause = false;
                    timer1.Interval = 500;
                    timer1.Enabled = true;

                    player.SetVolume(GetVolume());

                    trackBarDuration.Value = 0;

                    btnPlay.Enabled = false;
                    btnPause.Enabled = true;
                    btnStop.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Cannot play this file!", "Error");
                    return;
                }
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (player.Pause())
            {
                pause = true;

                btnPlay.Enabled = true;
                btnPause.Enabled = false;
                btnStop.Enabled = false;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (player.Stop())
            {
                playing = false;
                pause = false;
                timer1.Enabled = false;

                btnPlay.Enabled = true;
                btnPause.Enabled = false;
                btnStop.Enabled = false;
            }
        }
        private int GetVolume()
        {
            int vol = trackBarVolume.Value;
            vol = 10000 - vol;
            vol = -vol;

            return vol;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (playing)
            {
                int EvCode = 0;
                bool b = player.WaitForCompletion(0, ref EvCode);

                if (b)
                {
                    playing = false;
                    timer1.Enabled = false;

                    btnPlay.Enabled = true;
                    btnPause.Enabled = false;
                    btnStop.Enabled = false;
                }

                if (pause == false)
                {
                    System.Int64 duration = player.GetDuration() / 10000000;
                    System.Int64 curPos = player.GetCurrentPosition();

                    System.Int64 timeElapsedSec = curPos / 10000000;
                    string sDuration = string.Format("{0}:{1:d2}/{2}:{3:d2}", timeElapsedSec / 60, timeElapsedSec % 60, duration / 60, duration % 60);
                    lblDuration.Text = sDuration;
                    System.Int64 pos = (curPos * SLIDER_TIME_ELAPSE_MAX_POS) / player.GetDuration();
                    trackBarDuration.Value = (int)(pos);
                }
            }

        }

        private void trackBarVolume_Scroll(object sender, EventArgs e)
        {
            player.SetVolume(GetVolume());
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (playing)
            {
                player.Stop();
            }

            player.Cleanup();
        }

        private void trackBarDuration_Scroll(object sender, EventArgs e)
        {
            if (playing && pause == false)
            {
                System.Int64 duration = player.GetDuration();
                System.Int64 pos = trackBarDuration.Value * duration / trackBarDuration.Maximum;
                player.SetPositions(ref pos, ref duration, true);
            }
            else 
            {
                System.Int64 duration = player.GetDuration();
                System.Int64 pos = trackBarDuration.Value * duration / trackBarDuration.Maximum;
                System.Int64 posStop = pos;

                if (pause == true)
                {
                    player.SetPositions(ref pos, ref duration, true);
                    player.Pause();
                }
                else
                {
                    player.SetPositions(ref pos, ref posStop, true);
                }
            }

            System.Int64 durationTemp = player.GetDuration() / 10000000;
            System.Int64 curPos = player.GetCurrentPosition();

            System.Int64 timeElapsedSec = curPos / 10000000;
            string sDuration = string.Format("{0}:{1:d2}/{2}:{3:d2}", timeElapsedSec / 60, timeElapsedSec % 60, durationTemp / 60, durationTemp % 60);
            lblDuration.Text = sDuration;

        }

    }
}
