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
        protected easy_socket.Telnet.Telnet telnet;
        private System.Windows.Forms.CheckBox checkBox_telnet;
        private System.Windows.Forms.CheckBox checkBox_beep_on_bell;
        private System.Windows.Forms.CheckBox checkBox_send_data_on_return;
        private System.Windows.Forms.CheckBox checkBox_rcv_hexa_data;
        private System.Windows.Forms.CheckBox checkBox_send_hexa_data;
        private System.Windows.Forms.CheckBox checkBox_send_function_key;
        private System.Windows.Forms.CheckBox checkBox_no_buffer;
        protected bool b_telnet_protocol;

        public FormTCPClient()
        {
            InitializeComponent();
            Tools.GUI.XPStyle.MakeXPStyle(this);
            this.enable_textBox_editable();
            this.enable_state(true);
            this.textBox_editable.Focus();
            this.b_telnet_protocol=false;
            this.telnet=new easy_socket.Telnet.Telnet(this.textBox_telnet);
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
            this.checkBox_rcv_hexa_data = new System.Windows.Forms.CheckBox();
            this.checkBox_send_hexa_data = new System.Windows.Forms.CheckBox();
            this.checkBox_send_function_key = new System.Windows.Forms.CheckBox();
            this.checkBox_no_buffer = new System.Windows.Forms.CheckBox();
            this.panel_control.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(432, 366);
            // 
            // textBox_editable
            // 
            this.textBox_editable.Location = new System.Drawing.Point(0, 274);
            this.textBox_editable.Name = "textBox_editable";
            this.textBox_editable.Size = new System.Drawing.Size(312, 91);
            this.textBox_editable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_editable_KeyDown);
            this.textBox_editable.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_editable_KeyPress);
            this.textBox_editable.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_editable_KeyUp);
            // 
            // panel_control
            // 
            this.panel_control.Controls.Add(this.checkBox_no_buffer);
            this.panel_control.Controls.Add(this.checkBox_send_function_key);
            this.panel_control.Controls.Add(this.checkBox_send_hexa_data);
            this.panel_control.Controls.Add(this.checkBox_rcv_hexa_data);
            this.panel_control.Controls.Add(this.button_send);
            this.panel_control.Controls.Add(this.checkBox_send_data_on_return);
            this.panel_control.Controls.Add(this.checkBox_beep_on_bell);
            this.panel_control.Controls.Add(this.checkBox_telnet);
            this.panel_control.Controls.Add(this.checkBox_crlf);
            this.panel_control.Controls.Add(this.button_connect);
            this.panel_control.Controls.Add(this.button_close);
            this.panel_control.Location = new System.Drawing.Point(312, 0);
            this.panel_control.Name = "panel_control";
            this.panel_control.Size = new System.Drawing.Size(120, 366);
            // 
            // textBox_telnet
            // 
            this.textBox_telnet.Name = "textBox_telnet";
            this.textBox_telnet.Size = new System.Drawing.Size(312, 274);
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
            this.checkBox_crlf.Location = new System.Drawing.Point(8, 232);
            this.checkBox_crlf.Name = "checkBox_crlf";
            this.checkBox_crlf.Size = new System.Drawing.Size(96, 32);
            this.checkBox_crlf.TabIndex = 5;
            this.checkBox_crlf.Text = "send \\r\\n after data";
            // 
            // checkBox_telnet
            // 
            this.checkBox_telnet.Location = new System.Drawing.Point(8, 136);
            this.checkBox_telnet.Name = "checkBox_telnet";
            this.checkBox_telnet.Size = new System.Drawing.Size(104, 16);
            this.checkBox_telnet.TabIndex = 6;
            this.checkBox_telnet.Text = "Telnet protocol";
            this.checkBox_telnet.Click += new System.EventHandler(this.checkBox_telnet_Click);
            // 
            // checkBox_beep_on_bell
            // 
            this.checkBox_beep_on_bell.Location = new System.Drawing.Point(16, 152);
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
            this.checkBox_send_data_on_return.Location = new System.Drawing.Point(8, 312);
            this.checkBox_send_data_on_return.Name = "checkBox_send_data_on_return";
            this.checkBox_send_data_on_return.Size = new System.Drawing.Size(104, 48);
            this.checkBox_send_data_on_return.TabIndex = 1;
            this.checkBox_send_data_on_return.Text = "Send data when return key is pressed";
            // 
            // checkBox_rcv_hexa_data
            // 
            this.checkBox_rcv_hexa_data.Location = new System.Drawing.Point(8, 280);
            this.checkBox_rcv_hexa_data.Name = "checkBox_rcv_hexa_data";
            this.checkBox_rcv_hexa_data.Size = new System.Drawing.Size(104, 32);
            this.checkBox_rcv_hexa_data.TabIndex = 8;
            this.checkBox_rcv_hexa_data.Text = "Receive hexa data";
            // 
            // checkBox_send_hexa_data
            // 
            this.checkBox_send_hexa_data.Location = new System.Drawing.Point(8, 264);
            this.checkBox_send_hexa_data.Name = "checkBox_send_hexa_data";
            this.checkBox_send_hexa_data.Size = new System.Drawing.Size(104, 16);
            this.checkBox_send_hexa_data.TabIndex = 9;
            this.checkBox_send_hexa_data.Text = "Send hexa data";
            this.checkBox_send_hexa_data.CheckedChanged += new System.EventHandler(this.checkBox_send_hexa_data_CheckedChanged);
            // 
            // checkBox_send_function_key
            // 
            this.checkBox_send_function_key.Location = new System.Drawing.Point(16, 168);
            this.checkBox_send_function_key.Name = "checkBox_send_function_key";
            this.checkBox_send_function_key.Size = new System.Drawing.Size(104, 32);
            this.checkBox_send_function_key.TabIndex = 10;
            this.checkBox_send_function_key.Text = "Send function keys";
            // 
            // checkBox_no_buffer
            // 
            this.checkBox_no_buffer.Location = new System.Drawing.Point(16, 200);
            this.checkBox_no_buffer.Name = "checkBox_no_buffer";
            this.checkBox_no_buffer.Size = new System.Drawing.Size(104, 32);
            this.checkBox_no_buffer.TabIndex = 11;
            this.checkBox_no_buffer.Text = "Send data after each key pressed";
            this.checkBox_no_buffer.CheckedChanged += new System.EventHandler(this.checkBox_no_buffer_CheckedChanged);
            // 
            // FormTCPClient
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(432, 366);
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
        }

        public void new_tcp_client(string ip,int port,bool force_local_port,int local_port,bool telnet_protocol)
        {
            myip=ip;
            myport=port;
            this.Text="Connected to "+myip+":"+myport.ToString();
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
                        if (se.ErrorCode==10048) 
                        {
                            // return to a new line (split != send and send/receive)
                            if (!this.textBox_telnet.Text.EndsWith("\r\n")) this.textBox_telnet_add("\r\n");
                            this.textBox_telnet_add("Please wait few seconds before retry.\r\n");
                        }
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
            if (telnet_protocol)
            {
                this.set_telnet_protocol(true);
            }            
            this.checkBox_telnet.Checked=telnet_protocol;
            this.checkBox_beep_on_bell.Enabled=telnet_protocol;
            this.checkBox_send_function_key.Enabled=telnet_protocol;
            this.checkBox_no_buffer.Enabled=telnet_protocol;
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
            // return to a new line (split != send and send/receive)
            if (!this.textBox_telnet.Text.EndsWith("\r\n")) this.textBox_telnet_add("\r\n");
            this.textBox_telnet_add("Connection closed by remote side.\r\n");
        }

        protected void socket_connected_to_remote_host(easy_socket.tcp.Socket_Data sender, EventArgs e)
        {
            if (clt.LocalIPEndPoint!=null)
            {
                this.Text="Connected to "+myip+":"+myport.ToString();
                this.Text+=" local port:"+clt.LocalIPEndPoint.Port.ToString();
            }
            if (this.textBox_telnet.Text!="")
                // return to a new line (split != send and send/receive)
                if (!this.textBox_telnet.Text.EndsWith("\r\n")) this.textBox_telnet_add("\r\n");
            this.textBox_telnet_add("Connected to remote host.\r\n");
            if (this.b_telnet_protocol)
                this.telnet.reset();
        }

        protected void socket_data_arrival(easy_socket.tcp.Socket_Data sender, easy_socket.tcp.EventArgs_ReceiveDataSocket e)
        {
            if (this.checkBox_rcv_hexa_data.Checked)
            {
                this.textBox_telnet_add(easy_socket.hexa_convert.byte_to_hexa(e.buffer," ")+" ");
            }
            else
            {
                string strdata=System.Text.Encoding.Default.GetString(e.buffer , 0, e.buffer_size );
                // if telnet protocol
                #region telnet protocol
                if (this.b_telnet_protocol)
                {
                    // data to show
                    byte[][]reply_array=this.telnet.parse(strdata);
                    if (reply_array!=null)
                        for (int cnt=0;cnt<=reply_array.GetUpperBound(0);cnt++)
                            this.clt.send(reply_array[cnt]);
                }
                #endregion
                else if (strdata.Length>0)
                    this.textBox_telnet_add(strdata);
            }
        }
        protected void socket_error(easy_socket.tcp.Socket_Data sender, easy_socket.tcp.EventArgs_Exception e)
        {
            if (this.textBox_telnet.Text!="")
                if (!this.textBox_telnet.Text.EndsWith("\r\n")) this.textBox_telnet_add("\r\n");
            this.textBox_telnet_add("Socket Error: "+e.exception.Message+"\r\n");
            this.enable_state(false);
        }

        protected void button_close_Click(object sender, System.EventArgs e)
        {
            clt.close();
            this.enable_state(false);
            // return to a new line (split != send and send/receive)
            if (!this.textBox_telnet.Text.EndsWith("\r\n")) this.textBox_telnet_add("\r\n");
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
        }
        protected void send()
        {
            string strdata;
            strdata=this.textBox_editable.Text;
            if ((strdata=="")&&(!this.checkBox_crlf.Checked))
                return;
            if (this.checkBox_send_hexa_data.Checked)
            {
                byte[] b=easy_socket.hexa_convert.hexa_to_byte(strdata);
                if (b==null)
                {
                    MessageBox.Show(this,"Please enter hexa data","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    clt.send(b);
                }
            }
            else
            {
                if (this.checkBox_crlf.Checked)
                    strdata+="\r\n";
                clt.send(strdata);
            }
            this.textBox_editable.Text="";
            if ((!this.b_telnet_protocol))
            {
                // return to a new line (split != send and send/receive)
                if (!this.textBox_telnet.Text.EndsWith("\r\n"))
                    this.textBox_telnet_add("\r\n");
                this.textBox_telnet_add(strdata);
                // return to a new line (split != send and send/receive)
                if (!this.textBox_telnet.Text.EndsWith("\r\n"))
                    this.textBox_telnet_add("\r\n");
            }
        }
        private void textBox_editable_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
           // send special key
           byte[] b=null;
           switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Left:
                case Keys.Insert:
                case Keys.Home:
                case Keys.End:
                case Keys.PageUp:
                case Keys.PageDown:
                case Keys.Pause:
                case Keys.F1:
                case Keys.F2:
                case Keys.F3:
                case Keys.F4:
                case Keys.F5:
                case Keys.F6:
                case Keys.F7:
                case Keys.F8:
                case Keys.F9:
                case Keys.F10:
                case Keys.F11:
                case Keys.F12:
                    if (this.checkBox_send_function_key.Checked)
                        b=this.telnet.get_function_key_bytes(e);
                    break;
                case Keys.Enter:
                    if (this.checkBox_no_buffer.Checked)
                    {
                        b=this.telnet.get_function_key_bytes(e);
                        if ((!this.b_telnet_protocol))
                            this.textBox_telnet_add("\r\n");
                    }
                    break;
                case Keys.Delete:
                    if (this.checkBox_no_buffer.Checked)
                        b=this.telnet.get_function_key_bytes(e);
                    break;
            }
            if (b!=null)
                clt.send(b);
        }

        private void textBox_editable_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if(this.checkBox_no_buffer.Checked)
            {
                if (e.KeyChar==0x0D)
                    return;// sent by keydown
                clt.send(new byte[1]{(byte)e.KeyChar});
                return;
            }
            if (e.KeyChar==0x0D)
            {
                if (this.checkBox_send_data_on_return.Checked)
                    this.send();
            }
            else // assume KeyChar is not 13
            {// send function key like ctrl^c
                if (this.checkBox_send_function_key.Checked)
                    if ((e.KeyChar<0x20)&&(e.KeyChar!=0x08))// don't send back key
                        clt.send(new byte[1]{(byte)e.KeyChar});
            }        
        }

        private void textBox_editable_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyValue==13) 
                if (this.checkBox_send_data_on_return.Checked)
                    this.textBox_editable.Text="";
            if(this.checkBox_no_buffer.Checked)
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
            this.checkBox_send_function_key.Enabled=this.checkBox_telnet.Checked;
            this.checkBox_no_buffer.Enabled=this.checkBox_telnet.Checked;
        }

        private void checkBox_beep_on_bell_Click(object sender, System.EventArgs e)
        {
            this.telnet.b_allow_beep=this.checkBox_beep_on_bell.Checked;
        }

        private void checkBox_send_hexa_data_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.checkBox_send_hexa_data.Checked)
            {
                // uncheck \r\n
                this.checkBox_crlf.Checked=false;
                this.textBox_editable.Text=easy_socket.hexa_convert.string_to_hexa(this.textBox_editable.Text);
            }
            else
                this.textBox_editable.Text=easy_socket.hexa_convert.hexa_to_string(this.textBox_editable.Text); 
        }

        private void checkBox_no_buffer_CheckedChanged(object sender, System.EventArgs e)
        {
            this.checkBox_send_data_on_return.Checked=!this.checkBox_no_buffer.Checked;
            this.checkBox_crlf.Checked=!this.checkBox_no_buffer.Checked;
            this.checkBox_send_data_on_return.Enabled=!this.checkBox_no_buffer.Checked;
            this.checkBox_crlf.Enabled=!this.checkBox_no_buffer.Checked;
        }

    }

}

