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
    public class FormTCPClient: Network_Stuff.CommonTelnetForm
    {
        protected System.ComponentModel.IContainer components = null;
        protected Network_Stuff.Telnet telnet;
        private System.Windows.Forms.CheckBox checkBox_telnet;
        private System.Windows.Forms.CheckBox checkBox_beep_on_bell;
        private System.Windows.Forms.CheckBox checkBox_send_data_on_return;
        protected bool b_telnet_protocol;

        public FormTCPClient()
        {
            
            InitializeComponent();
            XPStyle.MakeXPStyle(this);
            this.enable_textBox_editable();
            this.enable_state(true);
            this.textBox_editable.Focus();
            this.b_telnet_protocol=false;
            this.telnet=new Network_Stuff.Telnet();
            this.checkBox_beep_on_bell.Checked=this.telnet.b_allow_beep;
        }

        protected override void Dispose( bool disposing )
        {
            this.clt.close();
            clt.event_Socket_Data_Closed_by_Remote_Side-= new easy_socket.tcp.Socket_Data_Closed_by_Remote_Side_EventHandler(socket_closed_by_remote_side);
            clt.event_Socket_Data_Connected_To_Remote_Host-=new easy_socket.tcp.Socket_Data_Connected_To_Remote_Host_EventHandler(socket_connected_to_remote_host);
            clt.event_Socket_Data_DataArrival-=new easy_socket.tcp.Socket_Data_DataArrival_EventHandler(socket_data_arrival);
            clt.event_Socket_Data_Error-=new easy_socket.tcp.Socket_Data_Error_EventHandler(socket_error);
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
        protected void InitializeComponent()
        {
            this.button_close = new System.Windows.Forms.Button();
            this.button_connect = new System.Windows.Forms.Button();
            this.button_send = new System.Windows.Forms.Button();
            this.checkBox_crlf = new System.Windows.Forms.CheckBox();
            this.checkBox_telnet = new System.Windows.Forms.CheckBox();
            this.checkBox_beep_on_bell = new System.Windows.Forms.CheckBox();
            this.checkBox_send_data_on_return = new System.Windows.Forms.CheckBox();
            this.panel_control.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(416, 342);
            // 
            // textBox_editable
            // 
            this.textBox_editable.Location = new System.Drawing.Point(0, 256);
            this.textBox_editable.Name = "textBox_editable";
            this.textBox_editable.Size = new System.Drawing.Size(296, 85);
            this.textBox_editable.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_editable_KeyPress);
            this.textBox_editable.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_editable_KeyUp);
            // 
            // panel_control
            // 
            this.panel_control.Controls.Add(this.button_send);
            this.panel_control.Controls.Add(this.checkBox_send_data_on_return);
            this.panel_control.Controls.Add(this.checkBox_beep_on_bell);
            this.panel_control.Controls.Add(this.checkBox_telnet);
            this.panel_control.Controls.Add(this.checkBox_crlf);
            this.panel_control.Controls.Add(this.button_connect);
            this.panel_control.Controls.Add(this.button_close);
            this.panel_control.Location = new System.Drawing.Point(296, 0);
            this.panel_control.Name = "panel_control";
            this.panel_control.Size = new System.Drawing.Size(120, 342);
            // 
            // textBox_telnet
            // 
            this.textBox_telnet.Name = "textBox_telnet";
            this.textBox_telnet.Size = new System.Drawing.Size(296, 256);
            // 
            // button_close
            // 
            this.button_close.Location = new System.Drawing.Point(19, 104);
            this.button_close.Name = "button_close";
            this.button_close.TabIndex = 4;
            this.button_close.Text = "Close";
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(19, 72);
            this.button_connect.Name = "button_connect";
            this.button_connect.TabIndex = 3;
            this.button_connect.Text = "Reconnect";
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // button_send
            // 
            this.button_send.CausesValidation = false;
            this.button_send.Location = new System.Drawing.Point(19, 40);
            this.button_send.Name = "button_send";
            this.button_send.TabIndex = 2;
            this.button_send.Text = "Send";
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // checkBox_crlf
            // 
            this.checkBox_crlf.Checked = true;
            this.checkBox_crlf.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_crlf.Location = new System.Drawing.Point(8, 144);
            this.checkBox_crlf.Name = "checkBox_crlf";
            this.checkBox_crlf.Size = new System.Drawing.Size(96, 32);
            this.checkBox_crlf.TabIndex = 5;
            this.checkBox_crlf.Text = "send \\r\\n after data";
            // 
            // checkBox_telnet
            // 
            this.checkBox_telnet.Location = new System.Drawing.Point(8, 184);
            this.checkBox_telnet.Name = "checkBox_telnet";
            this.checkBox_telnet.Size = new System.Drawing.Size(104, 16);
            this.checkBox_telnet.TabIndex = 6;
            this.checkBox_telnet.Text = "Telnet protocol";
            this.checkBox_telnet.Click += new System.EventHandler(this.checkBox_telnet_Click);
            // 
            // checkBox_beep_on_bell
            // 
            this.checkBox_beep_on_bell.Location = new System.Drawing.Point(16, 208);
            this.checkBox_beep_on_bell.Name = "checkBox_beep_on_bell";
            this.checkBox_beep_on_bell.Size = new System.Drawing.Size(96, 16);
            this.checkBox_beep_on_bell.TabIndex = 7;
            this.checkBox_beep_on_bell.Text = "Beep on BELL";
            this.checkBox_beep_on_bell.Click += new System.EventHandler(this.checkBox_beep_on_bell_Click);
            // 
            // checkBox_send_data_on_return
            // 
            this.checkBox_send_data_on_return.Checked = true;
            this.checkBox_send_data_on_return.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_send_data_on_return.Location = new System.Drawing.Point(8, 264);
            this.checkBox_send_data_on_return.Name = "checkBox_send_data_on_return";
            this.checkBox_send_data_on_return.Size = new System.Drawing.Size(104, 48);
            this.checkBox_send_data_on_return.TabIndex = 1;
            this.checkBox_send_data_on_return.Text = "Send data when return key is pressed";
            // 
            // FormTCPClient
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(416, 342);
            this.Name = "FormTCPClient";
            this.panel_control.ResumeLayout(false);

        }
        #endregion

        protected System.Windows.Forms.Button button_close;
        protected System.Windows.Forms.Button button_connect;
        protected string myip;
        protected int myport;
        protected System.Windows.Forms.Button button_send;
        protected System.Windows.Forms.CheckBox checkBox_crlf;

        private void set_telnet_protocol(bool b_is_telnet_protocol)
        {
            this.b_telnet_protocol=b_is_telnet_protocol;
            if (b_is_telnet_protocol)
            {
                // resize window to standart console size
                if (this.Width<730)
                {
                    this.Width=730;
                    this.Height=430;
                }
                // fonts as console fonts
                this.textBox_telnet.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
                this.textBox_editable.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            }
            else // restore default fonts
            {
                this.textBox_telnet.Font=new System.Drawing.Font("Microsoft Sans Serif",8.25F);
                this.textBox_editable.Font=new System.Drawing.Font("Microsoft Sans Serif",8.25F);
            }
        }
        easy_socket.tcp.Socket_Data clt;
        public void new_tcp_client(System.Net.Sockets.Socket s,int port)
        {
            System.Net.IPEndPoint ipep=(System.Net.IPEndPoint)s.RemoteEndPoint;

            this.Text="Remote host (" +ipep.Address.ToString()+ ":" +ipep.Port + ") connected on port "+port.ToString();
            clt=new easy_socket.tcp.Socket_Data(s);
            add_events();
            // no reconnection available: this socket is from server
            this.button_connect.Visible=false;

            // for telnet protocol
            #region telnet protocol
            if (port==23)
            {
                this.checkBox_telnet.Checked=true;
                this.set_telnet_protocol(true);
            }
            this.checkBox_beep_on_bell.Enabled=this.checkBox_telnet.Checked;
            #endregion
        }

        public void new_tcp_client(string ip,int port,bool force_local_port,int local_port)
        {
            myip=ip;
            myport=port;
            this.Text="Telnet to "+myip+":"+myport.ToString();
            if (force_local_port)
            {
                
                try
                {
                    this.Text+=" local port:"+local_port.ToString();
                    clt=new easy_socket.tcp.Socket_Data(1024,local_port);//error are not catch
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
                
                clt=new easy_socket.tcp.Socket_Data();
            }
            add_events();
            clt.connect(ip,port);

            // for telnet protocol
            #region telnet protocol
            if (port==23)
            {
                this.checkBox_telnet.Checked=true;
                this.set_telnet_protocol(true);
            }            
            this.checkBox_beep_on_bell.Enabled=this.checkBox_telnet.Checked;
            #endregion
        }

        protected void add_events()
        {
            clt.event_Socket_Data_Closed_by_Remote_Side+= new easy_socket.tcp.Socket_Data_Closed_by_Remote_Side_EventHandler(socket_closed_by_remote_side);
            clt.event_Socket_Data_Connected_To_Remote_Host +=new easy_socket.tcp.Socket_Data_Connected_To_Remote_Host_EventHandler(socket_connected_to_remote_host);
            clt.event_Socket_Data_DataArrival +=new easy_socket.tcp.Socket_Data_DataArrival_EventHandler(socket_data_arrival);
            clt.event_Socket_Data_Error+=new easy_socket.tcp.Socket_Data_Error_EventHandler(socket_error);
            //clt.event_Socket_Data_Send_Completed 
            //clt.event_Socket_Data_Send_Progress 

        }

        protected void socket_closed_by_remote_side(easy_socket.tcp.Socket_Data sender, EventArgs e)
        {
            this.enable_state(false);
            this.textBox_telnet_add("Connection closed by remote side.\r\n");
        }

        protected void socket_connected_to_remote_host(easy_socket.tcp.Socket_Data sender, EventArgs e)
        {
            if (clt.LocalIPEndPoint!=null)
            {
                this.Text="Telnet to "+myip+":"+myport.ToString();
                this.Text+=" local port:"+clt.LocalIPEndPoint.Port.ToString();
            }
            this.textBox_telnet_add("Connected to remote host.\r\n");

            // if telnet protocol
            #region telnet protocol
            if (this.b_telnet_protocol)
            {
                this.telnet.set_sent_command("");
                if (this.telnet.b_remove_local_echo_and_ask_to_remove_remote_echo)
                    // send command to remove local echo and ask to remove remote echo
                    clt.send(Telnet.COMMAND_TO_REMOVE_ECHO);
            }
            #endregion
        }

        protected void socket_data_arrival(easy_socket.tcp.Socket_Data sender, easy_socket.tcp.EventArgs_ReceiveDataSocket e)
        {
            string strdata=System.Text.Encoding.Default.GetString(e.buffer , 0, e.buffer_size );
            // if telnet protocol
            #region telnet protocol
            if (this.b_telnet_protocol)
            {
                if (this.telnet.b_remote_echo)
                {
                    // remove echo
                    this.telnet.remove_echo_from_command(ref strdata);
                    if (strdata.Length==0)
                        return;
                }
                if (this.telnet.b_local_echo)
                {
                    // echo command
                    for (int cpt=1;cpt<=strdata.Length;cpt++)
                        this.clt.send(strdata.Substring(0,cpt));
                }
                // data to show
                this.telnet.parse(ref strdata);
                if (this.telnet.b_clear_screen)
                    this.textBox_telnet.Text="";
                if (this.telnet.b_erase_char)
                    if (this.textBox_telnet.Text.Length>0)
                        this.textBox_telnet.Text=this.textBox_telnet.Text.Substring(0,this.textBox_telnet.Text.Length-1);
                if (this.telnet.b_erase_line)
                {
                    int pos=this.textBox_telnet.Text.LastIndexOf("\r\n");
                    if (pos>-1)
                        this.textBox_telnet.Text=this.textBox_telnet.Text.Substring(0,pos);
                }
            }
            #endregion
            this.textBox_telnet_add(strdata);
        }
        protected void socket_error(easy_socket.tcp.Socket_Data sender, easy_socket.tcp.EventArgs_Exception e)
        {
            this.textBox_telnet_add("Socket Error: "+e.exception.Message+"\r\n");
            this.enable_state(false);
        }

        protected void button_close_Click(object sender, System.EventArgs e)
        {
            clt.close();
            this.enable_state(false);
            this.textBox_telnet_add("Connection closed.\r\n");
        }

        protected void button_connect_Click(object sender, System.EventArgs e)
        {
            clt.reconnect();
            this.enable_state(true);
        }

        protected void button_send_Click(object sender, System.EventArgs e)
        {
            send();
            if (!this.textBox_telnet.Text.EndsWith("\r\n")) this.textBox_telnet_add("\r\n");
        }
        protected void send()
        {
            string strdata;
            strdata=this.textBox_editable.Text;
            if (this.b_telnet_protocol)
            {
                if (this.telnet.b_remote_echo)
                    if (strdata.Length>0)
                        this.telnet.set_sent_command(strdata);
            }
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
        private void enable_state(bool b_connected)
        {
            this.button_close.Enabled=b_connected;
            this.button_connect.Enabled=!b_connected;
            this.button_send.Enabled=b_connected;
            this.textBox_editable.Enabled=b_connected;
        }

        private void checkBox_telnet_Click(object sender, System.EventArgs e)
        {
            this.set_telnet_protocol(this.checkBox_telnet.Checked);
            this.checkBox_beep_on_bell.Enabled=this.checkBox_telnet.Checked;
        }

        private void checkBox_beep_on_bell_Click(object sender, System.EventArgs e)
        {
            this.telnet.b_allow_beep=this.checkBox_beep_on_bell.Checked;
        }
    }

}

