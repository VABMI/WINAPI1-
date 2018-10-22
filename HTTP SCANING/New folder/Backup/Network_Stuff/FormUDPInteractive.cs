using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Network_Stuff
{
	/// <summary>
	/// Summary description for FormUDPInteractive.
	/// </summary>
	public class FormUDPInteractive : Network_Stuff.CommonInteractiveForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormUDPInteractive()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
            this.remove_events();
            this.socket.stop();
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
            // 
            // panel_interactive
            // 
            this.panel_interactive.Name = "panel_interactive";
            // 
            // panel_cmd
            // 
            this.panel_cmd.Name = "panel_cmd";
            // 
            // panel1
            // 
            this.panel1.Name = "panel1";
            // 
            // textBox_editable
            // 
            this.textBox_editable.Name = "textBox_editable";
            // 
            // panel2
            // 
            this.panel2.Name = "panel2";
            // 
            // button_clear
            // 
            this.button_clear.Name = "button_clear";
            // 
            // groupBox_send_data_options
            // 
            this.groupBox_send_data_options.Name = "groupBox_send_data_options";
            // 
            // radioButton_send_to_clt
            // 
            this.radioButton_send_to_clt.Name = "radioButton_send_to_clt";
            // 
            // radioButton_send_to_srv
            // 
            this.radioButton_send_to_srv.Name = "radioButton_send_to_srv";
            // 
            // groupBox_clt_to_srv_options
            // 
            this.groupBox_clt_to_srv_options.Name = "groupBox_clt_to_srv_options";
            // 
            // radioButton_clt_to_srv_ask
            // 
            this.radioButton_clt_to_srv_ask.Name = "radioButton_clt_to_srv_ask";
            // 
            // radioButton_clt_to_srv_block
            // 
            this.radioButton_clt_to_srv_block.Name = "radioButton_clt_to_srv_block";
            // 
            // radioButton_clt_to_srv_allow
            // 
            this.radioButton_clt_to_srv_allow.Name = "radioButton_clt_to_srv_allow";
            // 
            // groupBox_srv_to_clt_options
            // 
            this.groupBox_srv_to_clt_options.Name = "groupBox_srv_to_clt_options";
            // 
            // radioButton_srv_to_clt_ask
            // 
            this.radioButton_srv_to_clt_ask.Name = "radioButton_srv_to_clt_ask";
            // 
            // radioButton_srv_to_clt_block
            // 
            this.radioButton_srv_to_clt_block.Name = "radioButton_srv_to_clt_block";
            // 
            // radioButton_srv_to_clt_allow
            // 
            this.radioButton_srv_to_clt_allow.Name = "radioButton_srv_to_clt_allow";
            // 
            // button_close
            // 
            this.button_close.Name = "button_close";
            this.button_close.Text = "Stop";
            // 
            // button_send
            // 
            this.button_send.Name = "button_send";
            // 
            // checkBox_send_hexa_data
            // 
            this.checkBox_send_hexa_data.Name = "checkBox_send_hexa_data";
            // 
            // checkBox_transmit_close
            // 
            this.checkBox_transmit_close.Name = "checkBox_transmit_close";
            this.checkBox_transmit_close.Visible = false;
            // 
            // groupBox_close
            // 
            this.groupBox_close.Name = "groupBox_close";
            this.groupBox_close.Visible = false;
            // 
            // checkBox_close_server_socket
            // 
            this.checkBox_close_server_socket.Name = "checkBox_close_server_socket";
            // 
            // checkBox_close_client_socket
            // 
            this.checkBox_close_client_socket.Name = "checkBox_close_client_socket";
            // 
            // FormUDPInteractive
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(560, 406);
            this.Name = "FormUDPInteractive";

        }
		#endregion

        #region members
        private easy_socket.udp.Socket_Server socket;
        string remote_server_ip="127.0.0.1";
        int remote_server_port=6500;
        string last_client_ip="127.0.0.1";
        int last_client_port=6501;
        bool b_last_client=false;

        // events are here to avoid mixing comment and data of differents events
        private System.Threading.AutoResetEvent evtNotWrittingData;
        private System.Threading.ManualResetEvent evtStop;
        private System.Threading.WaitHandle[] HexaViewWaitHandles;


        #endregion

        public void new_udp_interactive(string local_ip,int local_port,string remote_ip,int remote_port)
        {
            this.evtNotWrittingData=new System.Threading.AutoResetEvent(true);
            this.evtStop=new System.Threading.ManualResetEvent(false);
            this.HexaViewWaitHandles=new System.Threading.WaitHandle[]{this.evtStop,this.evtNotWrittingData};

            this.remote_server_ip=remote_ip;
            this.remote_server_port=remote_port;            
            this.Text="Transfer to UDP server "+this.remote_server_ip+":"+this.remote_server_port.ToString();

            this.socket=new easy_socket.udp.Socket_Server(local_ip,local_port);
            this.add_events();
            this.socket.start();
        }

        protected void add_events()
        {
            this.socket.event_Socket_Server_Started+=new easy_socket.udp.Socket_Server_Started_EventHandler(socket_event_Socket_Server_Started);
            this.socket.event_Socket_Server_Stopped+=new easy_socket.udp.Socket_Server_Stopped_EventHandler(socket_event_Socket_Server_Stopped);
            this.socket.event_Socket_Server_Remote_Host_Unreachable+=new easy_socket.udp.Socket_Server_Remote_Host_Unreachable(socket_event_Socket_Server_Remote_Host_Unreachable);
            this.socket.event_Socket_Server_Error+=new easy_socket.udp.Socket_Server_Error_EventHandler(socket_event_Socket_Server_Error);
            this.socket.event_Socket_Server_Data_Arrival+=new easy_socket.udp.Socket_Server_Data_Arrival_EventHandler(socket_event_Socket_Server_Data_Arrival);
        }

        protected void remove_events()
        {
            this.socket.event_Socket_Server_Started-=new easy_socket.udp.Socket_Server_Started_EventHandler(socket_event_Socket_Server_Started);
            this.socket.event_Socket_Server_Stopped-=new easy_socket.udp.Socket_Server_Stopped_EventHandler(socket_event_Socket_Server_Stopped);
            this.socket.event_Socket_Server_Remote_Host_Unreachable-=new easy_socket.udp.Socket_Server_Remote_Host_Unreachable(socket_event_Socket_Server_Remote_Host_Unreachable);
            this.socket.event_Socket_Server_Error-=new easy_socket.udp.Socket_Server_Error_EventHandler(socket_event_Socket_Server_Error);
            this.socket.event_Socket_Server_Data_Arrival-=new easy_socket.udp.Socket_Server_Data_Arrival_EventHandler(socket_event_Socket_Server_Data_Arrival);
        }

        #region GUI events

        protected override void button_close_Click(object sender, System.EventArgs e)
        {
            this.socket.stop();
        }

        protected override void button_send_Click(object sender, System.EventArgs e)
        {
            int res=System.Threading.WaitHandle.WaitAny(this.HexaViewWaitHandles);
            if (res==0) // close event
                return;

            byte[] data=this.get_data();
            if (data==null)
                return;
            if (this.radioButton_send_to_clt.Checked)
            {
                if (!this.b_last_client)
                {
                    MessageBox.Show(this,"No client yet","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {
                    this.socket.send(data,this.last_client_ip,this.last_client_port);
                    this.add_info("Data send to "+this.last_client_ip+":"+this.last_client_port.ToString());
                    this.add_data(data,System.Drawing.Color.Gray);
                }
            }
            else
            {
                this.socket.send(data,this.remote_server_ip,this.remote_server_port);
                this.add_info("Data send to "+this.remote_server_ip+":"+this.remote_server_port.ToString());
                this.add_data(data,System.Drawing.Color.Cyan);
            }
            this.textBox_editable.Text="";
            this.refresh_data();
            this.evtNotWrittingData.Set();
        }

        protected override void FormTCPInteractive_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.evtStop.Set();
        }
        #endregion

        private void socket_event_Socket_Server_Started(easy_socket.udp.Socket_Server sender, EventArgs e)
        {
            int res=System.Threading.WaitHandle.WaitAny(this.HexaViewWaitHandles);
            if (res==0) // close event
                return;
            this.add_info("Server Started ("+sender.local_ip+":"+sender.local_port.ToString()+")");
            this.refresh_data();
            this.evtNotWrittingData.Set();
        }

        private void socket_event_Socket_Server_Stopped(easy_socket.udp.Socket_Server sender, EventArgs e)
        {
            int res=System.Threading.WaitHandle.WaitAny(this.HexaViewWaitHandles);
            if (res==0) // close event
                return;
            this.add_info("Server Stopped");
            this.refresh_data();
            this.evtNotWrittingData.Set();
        }

        private void socket_event_Socket_Server_Remote_Host_Unreachable(easy_socket.udp.Socket_Server sender, EventArgs e)
        {
            int res=System.Threading.WaitHandle.WaitAny(this.HexaViewWaitHandles);
            if (res==0) // close event
                return;
            this.add_info("Remote Host Unreachable");
            this.refresh_data();
            this.evtNotWrittingData.Set();
        }

        private void socket_event_Socket_Server_Error(easy_socket.udp.Socket_Server sender, easy_socket.udp.EventArgs_Exception e)
        {
            int res=System.Threading.WaitHandle.WaitAny(this.HexaViewWaitHandles);
            if (res==0) // close event
                return;

            this.add_info("Socket Error: "+e.exception.Message);
            this.refresh_data();
            this.evtNotWrittingData.Set();
        }

        private void socket_event_Socket_Server_Data_Arrival(easy_socket.udp.Socket_Server sender, easy_socket.udp.EventArgs_ReceiveDataSocketUDP e)
        {
            int res=System.Threading.WaitHandle.WaitAny(this.HexaViewWaitHandles);
            if (res==0) // close event
                return;

            System.Drawing.Color color=System.Drawing.Color.Black;
            bool b_transmit=false;
            System.Net.IPEndPoint ipep=(System.Net.IPEndPoint)e.remote_host_EndPoint;
            // if data comes from remote server, transfer data to last client
            if ((ipep.Address.ToString()==this.remote_server_ip)&&(ipep.Port==this.remote_server_port))
            {
                if (b_last_client)
                {
                    // transfer data if required
                    if (this.radioButton_srv_to_clt_allow.Checked)
                        b_transmit=true;
                    else if (this.radioButton_srv_to_clt_block.Checked)
                        b_transmit=false;
                    else// query
                    {
                        string msg="Do you want to allow the transfer of following data from "+ipep.Address+":"+ipep.Port.ToString()+"?\r\n"
                            +"Hexa Data:\r\n"
                            +this.split_in_multiple_lines(easy_socket.hexa_convert.byte_to_hexa(e.buffer),50)
                            +"Text Data:\r\n"
                            +this.split_in_multiple_lines(System.Text.Encoding.Default.GetString(e.buffer),50);
                                
                        b_transmit=(MessageBox.Show(this,msg,"Tcp Interactive",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes);
                    }
                    if (b_transmit)
                    {
                        this.socket.send(e.buffer,this.last_client_ip,this.last_client_port);
                        color=System.Drawing.Color.Black;
                    }
                    else
                        color=System.Drawing.Color.Red;
                }
                else
                {
                    color=System.Drawing.Color.Red;
                    b_transmit=false;
                }
            }
            else// data is from a client, transfer it to remote server
            {
                // update info about last clt
                this.last_client_ip=ipep.Address.ToString();
                this.last_client_port=ipep.Port;
                if(!this.b_last_client)
                    this.b_last_client=true;

                // transfer data if required
                if (this.radioButton_clt_to_srv_allow.Checked)
                    b_transmit=true;
                else if (this.radioButton_clt_to_srv_block.Checked)
                    b_transmit=false;
                else// query
                {
                    string msg="Do you want to allow the transfer of following data from "+ipep.Address+":"+ipep.Port.ToString()+"?\r\n"
                        +"Hexa Data:\r\n"
                        +this.split_in_multiple_lines(easy_socket.hexa_convert.byte_to_hexa(e.buffer),50)
                        +"Text Data:\r\n"
                        +this.split_in_multiple_lines(System.Text.Encoding.Default.GetString(e.buffer),50);
                    b_transmit=(MessageBox.Show(this,msg,"Tcp Interactive",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes);
                }
                if (b_transmit)
                {
                    this.socket.send(e.buffer,this.remote_server_ip,this.remote_server_port);
                    color=System.Drawing.Color.Blue;
                }
                else
                    color=System.Drawing.Color.Violet;
            }
            if (b_transmit)
            {
                this.add_info("Data from "+ipep.Address+":"+ipep.Port.ToString().ToString());
            }
            else
                this.add_info("Data from "+ipep.Address+":"+ipep.Port.ToString()+"(Blocked)");
            // color depends of sender && user action
            this.add_data(e.buffer,color);
            this.refresh_data();
            this.evtNotWrittingData.Set();
        }
    }
}
