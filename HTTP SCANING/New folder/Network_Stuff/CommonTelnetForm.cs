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

namespace Network_Stuff
{
    /// <summary>
    /// Description résumée de CommonTelnetForm.
    /// </summary>
    public class CommonTelnetForm : System.Windows.Forms.Form
    {
        protected System.Windows.Forms.Panel panel;
        protected System.Windows.Forms.TextBox textBox_editable;
        protected System.Windows.Forms.Panel panel_control;
        protected System.Windows.Forms.TextBox textBox_telnet;

        private System.ComponentModel.Container components = null;
        public CommonTelnetForm()
        {
            InitializeComponent();

            this.textBox_telnet.Top=0;
            this.textBox_telnet.Left=0;
            this.textBox_editable.Left=0;
            this.resize(false);
        }
        protected override void Dispose( bool disposing )
        {
            try
            {
                if( disposing )
                {
                    if(components != null)
                    {
                        components.Dispose();
                    }
                }
                base.Dispose( disposing );// can throw errors
            }
            catch
            {
            }
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CommonTelnetForm));
            this.panel = new System.Windows.Forms.Panel();
            this.textBox_telnet = new System.Windows.Forms.TextBox();
            this.panel_control = new System.Windows.Forms.Panel();
            this.textBox_editable = new System.Windows.Forms.TextBox();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.textBox_telnet);
            this.panel.Controls.Add(this.panel_control);
            this.panel.Controls.Add(this.textBox_editable);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(376, 302);
            this.panel.TabIndex = 0;
            // 
            // textBox_telnet
            // 
            this.textBox_telnet.Location = new System.Drawing.Point(0, 0);
            this.textBox_telnet.Multiline = true;
            this.textBox_telnet.Name = "textBox_telnet";
            this.textBox_telnet.ReadOnly = true;
            this.textBox_telnet.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_telnet.Size = new System.Drawing.Size(240, 104);
            this.textBox_telnet.TabIndex = 4;
            this.textBox_telnet.Text = "";
            // 
            // panel_control
            // 
            this.panel_control.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_control.Location = new System.Drawing.Point(264, 0);
            this.panel_control.Name = "panel_control";
            this.panel_control.Size = new System.Drawing.Size(112, 302);
            this.panel_control.TabIndex = 0;
            // 
            // textBox_editable
            // 
            this.textBox_editable.AcceptsReturn = true;
            this.textBox_editable.AcceptsTab = true;
            this.textBox_editable.Location = new System.Drawing.Point(0, 176);
            this.textBox_editable.Multiline = true;
            this.textBox_editable.Name = "textBox_editable";
            this.textBox_editable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_editable.Size = new System.Drawing.Size(240, 120);
            this.textBox_editable.TabIndex = 1;
            this.textBox_editable.Text = "";
            this.textBox_editable.Visible = false;
            // 
            // CommonTelnetForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(376, 302);
            this.Controls.Add(this.panel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CommonTelnetForm";
            this.Text = "CommonTelnetForm";
            this.SizeChanged += new System.EventHandler(this.CommonTelnetForm_SizeChanged);
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        protected void enable_textBox_editable()
        {
            this.textBox_editable.Visible=true;
            this.resize(true);
        }
        private void resize(bool b)
        {
            int w=this.panel.Width-this.panel_control.Width;
            if (w>20)
            {
                this.textBox_telnet.Width = w;
                if (this.textBox_editable.Visible || b)// visible=false during a call in a constructor 
                    this.textBox_editable.Width=w;
            }
            if (this.textBox_editable.Visible || b)// visible=false during a call in a constructor 
            {
                if (this.panel.Height>80)
                {
                    this.textBox_telnet.Height=(int)(this.panel.Height*.75);
                    this.textBox_editable.Top=this.textBox_telnet.Height;
                    this.textBox_editable.Height=(int)(this.panel.Height*.25);
                }
            }
            else
            {
                this.textBox_telnet.Height=this.panel.Height;
            }
        }
        public void set_mdi_parent(System.Windows.Forms.Form MDIparent)
        {
            this.MdiParent=MDIparent;
        }
        public void textBox_telnet_set(string txt)
        {
            this.textBox_telnet.Text="";
            this.textBox_telnet_add(txt);
        }
        public void textBox_telnet_add(string txt)
        {
            try// serveur stopped / connection closed can occur when textBox is disposing
            {
                //replace \r\n with \n
                txt=txt.Replace("\r\n","\n");
                //replace \r with \n
                txt=txt.Replace("\r","\n");
                //replace \n with \r\n
                txt=txt.Replace("\n","\r\n");
                this.textBox_telnet.Text+=txt;

                this.textBox_telnet.SelectionLength=0;
                this.textBox_telnet.SelectionStart=this.textBox_telnet.Text.Length;
                this.textBox_telnet.ScrollToCaret();

                this.textBox_editable.Focus();
            }
            catch{}
        }
        private void CommonTelnetForm_SizeChanged(object sender, System.EventArgs e)
        {
            this.resize(false);        
        }
    }
}
