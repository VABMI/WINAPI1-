/*
Copyright (C) 2004 Jacquelin POTIER <jacquelin.potier@free.fr>
Dynamic aspect ratio code Copyright (C) 2004 Jacquelin POTIER <jacquelin.potier@free.fr>

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; version 2 of the License.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
*/

// require system.web.dll reference
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Tools.GUI.Windows.ErrorReport
{
    /// <summary>
    /// Summary description for FormMainCatch.
    /// </summary>
    public class FormMainCatch : System.Windows.Forms.Form
    {
        private string str_email_author;
        private System.Windows.Forms.TextBox textBox_report_data;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_send_report;
        private System.Windows.Forms.Button button_restart_application;
        private System.Windows.Forms.Button button_stop_application;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public FormMainCatch(Exception e)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            Tools.GUI.XPStyle.MakeXPStyle(this);

            this.str_email_author="jacquelin.potier@free.fr";

            this.textBox_report_data.Text+="Application Name: "+System.Windows.Forms.Application.ProductName+"\r\n";
            this.textBox_report_data.Text+="Application Version: "+System.Windows.Forms.Application.ProductVersion;
            this.textBox_report_data.Text+="\t"+new System.IO.FileInfo(System.Windows.Forms.Application.ExecutablePath).LastWriteTime.ToString()+"\r\n";
            this.textBox_report_data.Text+="Framework Version: "+System.Environment.Version.ToString()+"\r\n";
            this.textBox_report_data.Text+="OS Version: "+System.Environment.OSVersion.ToString()+"\r\n";
            string[] str=Cuser_group.get_groups();
            if (str!=null)
                this.textBox_report_data.Text+="User groups: "+String.Join(", ",str)+"\r\n";
            this.textBox_report_data.Text+="Error message: "+e.Message+"\r\n";
            this.textBox_report_data.Text+="Error TargetSite: "+e.TargetSite+"\r\n";
            this.textBox_report_data.Text+="Error Full Description: \r\n"+e.ToString();
            this.textBox_report_data.SelectionLength=0;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if(components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_report_data = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_send_report = new System.Windows.Forms.Button();
            this.button_restart_application = new System.Windows.Forms.Button();
            this.button_stop_application = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_report_data
            // 
            this.textBox_report_data.Location = new System.Drawing.Point(24, 32);
            this.textBox_report_data.Multiline = true;
            this.textBox_report_data.Name = "textBox_report_data";
            this.textBox_report_data.ReadOnly = true;
            this.textBox_report_data.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_report_data.Size = new System.Drawing.Size(344, 192);
            this.textBox_report_data.TabIndex = 0;
            this.textBox_report_data.Text = "";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "The following error has occured :";
            // 
            // button_send_report
            // 
            this.button_send_report.Location = new System.Drawing.Point(296, 232);
            this.button_send_report.Name = "button_send_report";
            this.button_send_report.TabIndex = 2;
            this.button_send_report.Text = "Send";
            this.button_send_report.Click += new System.EventHandler(this.button_send_report_Click);
            // 
            // button_restart_application
            // 
            this.button_restart_application.Location = new System.Drawing.Point(296, 256);
            this.button_restart_application.Name = "button_restart_application";
            this.button_restart_application.TabIndex = 3;
            this.button_restart_application.Text = "Restart";
            this.button_restart_application.Click += new System.EventHandler(this.button_restart_application_Click);
            // 
            // button_stop_application
            // 
            this.button_stop_application.Location = new System.Drawing.Point(296, 280);
            this.button_stop_application.Name = "button_stop_application";
            this.button_stop_application.TabIndex = 4;
            this.button_stop_application.Text = "Stop";
            this.button_stop_application.Click += new System.EventHandler(this.button_stop_application_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(24, 232);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Send report to author";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(24, 256);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Restart application";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(24, 280);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(176, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Stop application";
            // 
            // FormMainCatch
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(394, 312);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_stop_application);
            this.Controls.Add(this.button_restart_application);
            this.Controls.Add(this.button_send_report);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_report_data);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMainCatch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Application Error Report";
            this.ResumeLayout(false);

        }
        #endregion

        private void button_stop_application_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void button_restart_application_Click(object sender, System.EventArgs e)
        {
            // launch new process with current application
            System.Diagnostics.Process p=new System.Diagnostics.Process();
            p.StartInfo.UseShellExecute=true;
            p.StartInfo.FileName=System.Windows.Forms.Application.ExecutablePath;
            p.Start();
            this.Close();
        }
        private void button_send_report_Click(object sender, System.EventArgs e)
        {
            // send mail

            System.Web.Mail.MailMessage Message = new System.Web.Mail.MailMessage();
            Message.To = this.str_email_author;
            // Message.From not needed take default account
            Message.Subject = "Reporting error for "+System.Windows.Forms.Application.ProductName+" version "+System.Windows.Forms.Application.ProductVersion;
            Message.Body = this.textBox_report_data.Text;

            try
            {
                // System.Web.Mail.SmtpMail.SmtpServer not needed take default mail server
                System.Web.Mail.SmtpMail.Send(Message);
                MessageBox.Show(this,"Report successfully sent","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch(System.Web.HttpException ehttp)
            {
                MessageBox.Show(this,"The following error has appeared during the sending of the mail:\r\n"+ehttp.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
        }
    }
}