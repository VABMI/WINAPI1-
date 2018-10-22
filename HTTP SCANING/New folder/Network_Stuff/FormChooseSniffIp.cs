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
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace easy_socket
{
    /// <summary>
    /// Summary description for FormChooseSniffIp.
    /// </summary>
    public class FormChooseSniffIp : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ListBox listBox_ipaddr;
        public int i_ipaddr_index;
        private System.Windows.Forms.Label label1;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public FormChooseSniffIp()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            Tools.GUI.XPStyle.MakeXPStyle(this);
            this.i_ipaddr_index=0;
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
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
            this.listBox_ipaddr = new System.Windows.Forms.ListBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBox_ipaddr
            // 
            this.listBox_ipaddr.Location = new System.Drawing.Point(16, 40);
            this.listBox_ipaddr.Name = "listBox_ipaddr";
            this.listBox_ipaddr.Size = new System.Drawing.Size(136, 95);
            this.listBox_ipaddr.TabIndex = 0;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(48, 144);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "Ok";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Choose Ip (and so adaptater) to sniff";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormChooseSniffIp
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(168, 174);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.listBox_ipaddr);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormChooseSniffIp";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }
        #endregion

        public void set_ip_list(System.Net.IPAddress[] ipaddr)
        {
            for (int cpt=0;cpt<ipaddr.Length;cpt++)
                this.listBox_ipaddr.Items.Add(ipaddr[cpt].ToString());
            if (ipaddr.Length>0)
                this.listBox_ipaddr.SelectedIndex=0;
        }
        public int get_ip_list_index()
        {
            this.ShowDialog();
            return this.i_ipaddr_index;
        }
        private void buttonOK_Click(object sender, System.EventArgs e)
        {
            this.i_ipaddr_index=this.listBox_ipaddr.SelectedIndex;
            this.Close();
        }
    }
}

