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
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace Network_Stuff
{
    public class FormUDPClient : Network_Stuff.CommonTelnetForm
    {
        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.CheckBox checkBox_crlf;
        private System.Windows.Forms.CheckBox checkBox_send_data_on_return;
        private System.ComponentModel.IContainer components = null;

        public FormUDPClient()
        {
            InitializeComponent();
            XPStyle.MakeXPStyle(this);
            this.enable_textBox_editable();
            this.textBox_editable.Focus();
        }

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if (components != null) 
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

        #region Designer generated code
        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_send = new System.Windows.Forms.Button();
            this.checkBox_crlf = new System.Windows.Forms.CheckBox();
            this.checkBox_send_data_on_return = new System.Windows.Forms.CheckBox();
            this.panel.SuspendLayout();
            this.panel_control.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Size = new System.Drawing.Size(376, 358);
            this.panel.Visible = true;
            // 
            // textBox_editable
            // 
            this.textBox_editable.Location = new System.Drawing.Point(0, 226);
            this.textBox_editable.Size = new System.Drawing.Size(264, 75);
            this.textBox_editable.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_editable_KeyPress);
            this.textBox_editable.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_editable_KeyUp);
            // 
            // panel_control
            // 
            this.panel_control.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                        this.checkBox_send_data_on_return,
                                                                                        this.checkBox_crlf,
                                                                                        this.button_send});
            this.panel_control.Size = new System.Drawing.Size(112, 358);
            this.panel_control.Visible = true;
            // 
            // textBox_telnet
            // 
            this.textBox_telnet.Size = new System.Drawing.Size(264, 226);
            this.textBox_telnet.Visible = true;
            // 
            // button_send
            // 
            this.button_send.Location = new System.Drawing.Point(19, 16);
            this.button_send.Name = "button_send";
            this.button_send.TabIndex = 1;
            this.button_send.Text = "Send";
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // checkBox_crlf
            // 
            this.checkBox_crlf.Checked = true;
            this.checkBox_crlf.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_crlf.Location = new System.Drawing.Point(8, 48);
            this.checkBox_crlf.Name = "checkBox_crlf";
            this.checkBox_crlf.Size = new System.Drawing.Size(96, 32);
            this.checkBox_crlf.TabIndex = 4;
            this.checkBox_crlf.Text = "send \\r\\n after data";
            // 
            // checkBox_send_data_on_return
            // 
            this.checkBox_send_data_on_return.Checked = true;
            this.checkBox_send_data_on_return.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_send_data_on_return.Location = new System.Drawing.Point(4, 155);
            this.checkBox_send_data_on_return.Name = "checkBox_send_data_on_return";
            this.checkBox_send_data_on_return.Size = new System.Drawing.Size(104, 48);
            this.checkBox_send_data_on_return.TabIndex = 8;
            this.checkBox_send_data_on_return.Text = "Send data when return key is pressed";
            // 
            // FormUDPClient
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(376, 358);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.panel_control,
                                                                          this.textBox_telnet,
                                                                          this.panel});
            this.Name = "FormUDPClient";
            this.panel.ResumeLayout(false);
            this.panel_control.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion


        easy_socket.udp.Socket_Data clt;
        public void new_udp_client(string ip,int port,bool force_local_port,int local_port)
        {
            this.Text="UDP Send to "+ip+":"+port.ToString();
            if (force_local_port)
            {
                try
                {
                    this.Text+=" local port:"+local_port.ToString();
                    clt=new easy_socket.udp.Socket_Data(local_port,ip,port);//error are not catch
                }
                catch (Exception e)
                {
                    this.textBox_telnet_add(e.Message+"\r\n");
                    if (e is System.Net.Sockets.SocketException)
                    {
                        System.Net.Sockets.SocketException se=(System.Net.Sockets.SocketException)e;
                        if (se.ErrorCode==10048) this.textBox_telnet_add("Please wait few seconds before retry.\r\n");
                    }
                    return;
                }
            }
            else
            {
                clt=new easy_socket.udp.Socket_Data(ip,port);
            }
            clt.event_Socket_Data_Error+=new easy_socket.udp.Socket_Data_Error_EventHandler(client_error);
        }

        private void client_error(easy_socket.udp.Socket_Data s,easy_socket.udp.EventArgs_Exception e)
        {
            this.textBox_telnet_add("Error: " + e.exception.Message+ "\r\n");
        }

        private void button_send_Click(object sender, System.EventArgs e)
        {
            send();
            if (!this.textBox_telnet.Text.EndsWith("\r\n"))    this.textBox_telnet_add("\r\n");
        }
        private void send()
        {
            string strdata;
            strdata=this.textBox_editable.Text;

            if (this.checkBox_crlf.Checked)
                strdata+="\r\n";
            this.textBox_telnet_add(strdata);
            this.textBox_editable.Text="";
            clt.send(strdata);
        }

        protected void textBox_editable_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!this.checkBox_send_data_on_return.Checked)
                return;
            if (e.KeyChar==13) 
                send();
        }
        private void textBox_editable_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (!this.checkBox_send_data_on_return.Checked)
                return;
            if (e.KeyValue==13) 
                this.textBox_editable.Text="";
        }

    }
}

