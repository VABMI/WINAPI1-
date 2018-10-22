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
    public class FormUDPServer : Network_Stuff.CommonTelnetForm
    {
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Button button_restart;
        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.CheckBox checkBox_crlf;
        private System.Windows.Forms.CheckBox checkBox_send_data_on_return;
        private System.Windows.Forms.CheckBox checkBox_send_hexa_data;
        private System.Windows.Forms.CheckBox checkBox_rcv_hexa_data;
        private System.ComponentModel.IContainer components = null;

        public FormUDPServer()
        {
            InitializeComponent();
            Tools.GUI.XPStyle.MakeXPStyle(this);

            this.enable_textBox_editable();
            this.textBox_editable.Focus();
        }

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            
            this.srv.stop();
            srv.event_Socket_Server_Error-=new easy_socket.udp.Socket_Server_Error_EventHandler(server_error);
            srv.event_Socket_Server_Data_Arrival-=new easy_socket.udp.Socket_Server_Data_Arrival_EventHandler(server_data_arrival);
            srv.event_Socket_Server_Started-=new easy_socket.udp.Socket_Server_Started_EventHandler(server_started);
            srv.event_Socket_Server_Stopped-=new easy_socket.udp.Socket_Server_Stopped_EventHandler(server_stopped);
            srv.event_Socket_Server_Remote_Host_Unreachable-=new easy_socket.udp.Socket_Server_Remote_Host_Unreachable(remote_host_unreachable);
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
            this.button_stop = new System.Windows.Forms.Button();
            this.button_restart = new System.Windows.Forms.Button();
            this.button_send = new System.Windows.Forms.Button();
            this.checkBox_crlf = new System.Windows.Forms.CheckBox();
            this.checkBox_send_data_on_return = new System.Windows.Forms.CheckBox();
            this.checkBox_send_hexa_data = new System.Windows.Forms.CheckBox();
            this.checkBox_rcv_hexa_data = new System.Windows.Forms.CheckBox();
            this.panel_control.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(368, 342);
            // 
            // textBox_editable
            // 
            this.textBox_editable.Location = new System.Drawing.Point(0, 226);
            this.textBox_editable.Name = "textBox_editable";
            this.textBox_editable.Size = new System.Drawing.Size(264, 75);
            this.textBox_editable.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_editable_KeyPress);
            this.textBox_editable.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_editable_KeyUp);
            // 
            // panel_control
            // 
            this.panel_control.Controls.Add(this.checkBox_send_hexa_data);
            this.panel_control.Controls.Add(this.checkBox_rcv_hexa_data);
            this.panel_control.Controls.Add(this.checkBox_send_data_on_return);
            this.panel_control.Controls.Add(this.checkBox_crlf);
            this.panel_control.Controls.Add(this.button_send);
            this.panel_control.Controls.Add(this.button_stop);
            this.panel_control.Controls.Add(this.button_restart);
            this.panel_control.Location = new System.Drawing.Point(256, 0);
            this.panel_control.Name = "panel_control";
            this.panel_control.Size = new System.Drawing.Size(112, 342);
            // 
            // textBox_telnet
            // 
            this.textBox_telnet.Name = "textBox_telnet";
            this.textBox_telnet.Size = new System.Drawing.Size(264, 226);
            // 
            // button_stop
            // 
            this.button_stop.Location = new System.Drawing.Point(20, 24);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(72, 23);
            this.button_stop.TabIndex = 0;
            this.button_stop.Text = "Stop";
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // button_restart
            // 
            this.button_restart.Location = new System.Drawing.Point(20, 56);
            this.button_restart.Name = "button_restart";
            this.button_restart.Size = new System.Drawing.Size(72, 23);
            this.button_restart.TabIndex = 1;
            this.button_restart.Text = "Restart";
            this.button_restart.Click += new System.EventHandler(this.button_restart_Click);
            // 
            // button_send
            // 
            this.button_send.Location = new System.Drawing.Point(20, 88);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(72, 23);
            this.button_send.TabIndex = 2;
            this.button_send.Text = "Send";
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // checkBox_crlf
            // 
            this.checkBox_crlf.Checked = true;
            this.checkBox_crlf.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_crlf.Location = new System.Drawing.Point(8, 128);
            this.checkBox_crlf.Name = "checkBox_crlf";
            this.checkBox_crlf.Size = new System.Drawing.Size(96, 32);
            this.checkBox_crlf.TabIndex = 3;
            this.checkBox_crlf.Text = "send \\r\\n after data";
            // 
            // checkBox_send_data_on_return
            // 
            this.checkBox_send_data_on_return.Checked = true;
            this.checkBox_send_data_on_return.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_send_data_on_return.Location = new System.Drawing.Point(8, 224);
            this.checkBox_send_data_on_return.Name = "checkBox_send_data_on_return";
            this.checkBox_send_data_on_return.Size = new System.Drawing.Size(104, 48);
            this.checkBox_send_data_on_return.TabIndex = 8;
            this.checkBox_send_data_on_return.Text = "Send data when return key is pressed";
            // 
            // checkBox_send_hexa_data
            // 
            this.checkBox_send_hexa_data.Location = new System.Drawing.Point(8, 160);
            this.checkBox_send_hexa_data.Name = "checkBox_send_hexa_data";
            this.checkBox_send_hexa_data.Size = new System.Drawing.Size(104, 16);
            this.checkBox_send_hexa_data.TabIndex = 11;
            this.checkBox_send_hexa_data.Text = "Send hexa data";
            this.checkBox_send_hexa_data.CheckedChanged += new System.EventHandler(this.checkBox_send_hexa_data_CheckedChanged);
            // 
            // checkBox_rcv_hexa_data
            // 
            this.checkBox_rcv_hexa_data.Location = new System.Drawing.Point(8, 176);
            this.checkBox_rcv_hexa_data.Name = "checkBox_rcv_hexa_data";
            this.checkBox_rcv_hexa_data.Size = new System.Drawing.Size(104, 32);
            this.checkBox_rcv_hexa_data.TabIndex = 10;
            this.checkBox_rcv_hexa_data.Text = "Receive hexa data";
            // 
            // FormUDPServer
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(368, 342);
            this.Name = "FormUDPServer";
            this.panel_control.ResumeLayout(false);

        }
        #endregion


        private int myport;
        private string myremoteip;
        private int myremoteport;
        private bool echo_server;
        private bool b_server_only;

        private easy_socket.udp.Socket_Server srv;

        public void new_udp_server(string remote_ip,int remote_port,int local_port)
        {
            echo_server=false;
            
            this.Text="UDP Client/Server Remote host IP: "+remote_ip+" Port: "+remote_port.ToString();

            myremoteip=remote_ip;
            myremoteport=remote_port;
            myport=local_port;
            srv=new easy_socket.udp.Socket_Server(local_port);
            this.b_server_only=false;
            add_events();
            srv.start();
            this.enable_state(true);
        }

        public void new_udp_server(string ip,int port,bool echo)
        {
            this.button_send.Text="Reply";
            echo_server=echo;

            this.Text="UDP Server IP: "+ip+" Port: "+port.ToString();
            myremoteip="127.0.0.1";
            myport=port;
            myremoteport=6000;
            this.b_server_only=true;
            srv=new easy_socket.udp.Socket_Server(ip,port);
            add_events();
            srv.start();
            this.enable_state(true);
        }

        private void add_events()
        {
            srv.event_Socket_Server_Error+=new easy_socket.udp.Socket_Server_Error_EventHandler(server_error);
            srv.event_Socket_Server_Data_Arrival+=new easy_socket.udp.Socket_Server_Data_Arrival_EventHandler(server_data_arrival);
            srv.event_Socket_Server_Started +=new easy_socket.udp.Socket_Server_Started_EventHandler(server_started);
            srv.event_Socket_Server_Stopped +=new easy_socket.udp.Socket_Server_Stopped_EventHandler(server_stopped);
            srv.event_Socket_Server_Remote_Host_Unreachable+=new easy_socket.udp.Socket_Server_Remote_Host_Unreachable(remote_host_unreachable);
        }

        private void server_error(easy_socket.udp.Socket_Server s,easy_socket.udp.EventArgs_Exception e)
        {
            if (this.textBox_telnet.Text!="")
                if (!this.textBox_telnet.Text.EndsWith("\r\n")) this.textBox_telnet_add("\r\n");
            this.textBox_telnet_add("Error: " + e.exception.Message+ "\r\n");
            this.enable_state(false);
        }

        private void server_data_arrival(easy_socket.udp.Socket_Server s,easy_socket.udp.EventArgs_ReceiveDataSocketUDP e)
        {
            System.Net.IPEndPoint ipep=(System.Net.IPEndPoint)e.remote_host_EndPoint;
            if (!this.textBox_telnet.Text.EndsWith("\r\n")) this.textBox_telnet_add("\r\n");
            this.textBox_telnet_add("Data from "+ ipep.Address +" port " + ipep.Port.ToString() +" :\r\n");
            if (this.checkBox_rcv_hexa_data.Checked)
            {
                this.textBox_telnet_add(easy_socket.hexa_convert.byte_to_hexa(e.buffer," ")+" ");
            }
            else
            {
                string strdata=System.Text.Encoding.Default.GetString(e.buffer , 0, e.buffer_size );
                this.textBox_telnet_add(strdata);
            }
            if (this.b_server_only)
            {
                //update remote host
                this.myremoteip=((System.Net.IPEndPoint)e.remote_host_EndPoint).Address.ToString();
                this.myremoteport=((System.Net.IPEndPoint)e.remote_host_EndPoint).Port;
            }
            if (echo_server)
                srv.send(e.buffer,e.remote_host_EndPoint);
        }

        private void server_started(easy_socket.udp.Socket_Server s,System.EventArgs e)
        {
            if (this.textBox_telnet.Text!="")
                if (!this.textBox_telnet.Text.EndsWith("\r\n")) this.textBox_telnet_add("\r\n");
            this.textBox_telnet_add("Server started.\r\n");
            if (srv!=null && this.button_send.Text=="Send")// only if client/server
            {
                this.Text+=" Local port: "+srv.local_port.ToString();
            }
        }
        private void server_stopped(easy_socket.udp.Socket_Server s,System.EventArgs e)
        {
            if (!this.textBox_telnet.Text.EndsWith("\r\n")) this.textBox_telnet_add("\r\n");
            this.textBox_telnet_add("Server stopped.\r\n");
            this.enable_state(false);
        }

        private void button_stop_Click(object sender, System.EventArgs e)
        {
            srv.stop();
            this.enable_state(false);
        }

        private void button_restart_Click(object sender, System.EventArgs e)
        {
            srv.start();
            this.enable_state(true);
        }

        private void button_send_Click(object sender, System.EventArgs e)
        {
            send();
        }
        private void send()
        {
            string strdata;
            strdata=this.textBox_editable.Text;

            if (strdata=="")
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
                    srv.send(b,this.myremoteip,this.myremoteport);
            }
            else
            {
                if (this.checkBox_crlf.Checked)
                    strdata+="\r\n";
                srv.send(strdata,this.myremoteip,this.myremoteport);
            }

            if (!this.textBox_telnet.Text.EndsWith("\r\n")) this.textBox_telnet_add("\r\n");
            this.textBox_telnet_add(strdata);
            this.textBox_editable.Text="";
            if (!this.textBox_telnet.Text.EndsWith("\r\n")) this.textBox_telnet_add("\r\n");
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
            this.button_restart.Enabled=!b_connected;
            this.button_stop.Enabled=b_connected;
            this.button_send.Enabled=b_connected;
            this.textBox_editable.Enabled=b_connected;
        }
        private void remote_host_unreachable(easy_socket.udp.Socket_Server s,System.EventArgs e)
        {
            if (this.textBox_telnet.Text!="")
                if (!this.textBox_telnet.Text.EndsWith("\r\n")) this.textBox_telnet_add("\r\n");
            this.textBox_telnet_add("Remote host unreachable.\r\n");
        }

        private void checkBox_send_hexa_data_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.checkBox_send_hexa_data.Checked)
            {
                // disable incompatible options (\r\n)
                this.checkBox_crlf.Checked=false;
                this.checkBox_crlf.Enabled=false;
            }
            else
                this.checkBox_crlf.Enabled=true;        
        }
    }
}

