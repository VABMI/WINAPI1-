using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Network_Stuff
{
	/// <summary>
	/// Summary description for FormTCPInteractive.
	/// </summary>
	public class FormTCPInteractive : Network_Stuff.CommonInteractiveForm
	{
        #region design
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;



		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

        }
		#endregion

        #endregion

        #region constructor / destructor
		public FormTCPInteractive()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            this.remove_events();
            this.socket_to_clt.close();
            this.socket_to_srv.close();
            if( disposing )
            {
                if(components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

        #endregion

        #region members
        private easy_socket.tcp.Socket_Data socket_to_clt;
        private easy_socket.tcp.Socket_Data socket_to_srv;

        // events are here to avoid mixing comment and data of differents events
        private System.Threading.AutoResetEvent evtNotWrittingData;
        private System.Threading.ManualResetEvent evtStop;
        private System.Threading.WaitHandle[] HexaViewWaitHandles;


        #endregion

        public void new_tcp_interactive(System.Net.Sockets.Socket s,string remote_srv_ip,int remote_srv_port)
        {
            this.evtNotWrittingData=new System.Threading.AutoResetEvent(true);
            this.evtStop=new System.Threading.ManualResetEvent(false);
            this.HexaViewWaitHandles=new System.Threading.WaitHandle[]{this.evtStop,this.evtNotWrittingData};
            
            System.Net.IPEndPoint ipep=(System.Net.IPEndPoint)s.RemoteEndPoint;
            this.Text="Transfer between client "+ipep.Address.ToString()+":"+ipep.Port+" and server "+remote_srv_ip+":"+remote_srv_port.ToString();
            this.socket_to_clt=new easy_socket.tcp.Socket_Data(s);
            this.socket_to_srv=new easy_socket.tcp.Socket_Data();
            this.socket_to_srv.connect(remote_srv_ip,remote_srv_port);
            this.add_events();
        }

        protected void add_events()
        {
            this.socket_to_clt.event_Socket_Data_Closed_by_Remote_Side+= new easy_socket.tcp.Socket_Data_Closed_by_Remote_Side_EventHandler(socket_closed_by_remote_side);
            this.socket_to_clt.event_Socket_Data_Connected_To_Remote_Host +=new easy_socket.tcp.Socket_Data_Connected_To_Remote_Host_EventHandler(socket_connected_to_remote_host);
            this.socket_to_clt.event_Socket_Data_DataArrival +=new easy_socket.tcp.Socket_Data_DataArrival_EventHandler(socket_data_arrival);
            this.socket_to_clt.event_Socket_Data_Error+=new easy_socket.tcp.Socket_Data_Error_EventHandler(socket_error);

            this.socket_to_srv.event_Socket_Data_Closed_by_Remote_Side+= new easy_socket.tcp.Socket_Data_Closed_by_Remote_Side_EventHandler(socket_closed_by_remote_side);
            this.socket_to_srv.event_Socket_Data_Connected_To_Remote_Host +=new easy_socket.tcp.Socket_Data_Connected_To_Remote_Host_EventHandler(socket_connected_to_remote_host);
            this.socket_to_srv.event_Socket_Data_DataArrival +=new easy_socket.tcp.Socket_Data_DataArrival_EventHandler(socket_data_arrival);
            this.socket_to_srv.event_Socket_Data_Error+=new easy_socket.tcp.Socket_Data_Error_EventHandler(socket_error);
        }

        protected void remove_events()
        {
            this.socket_to_clt.event_Socket_Data_Closed_by_Remote_Side-= new easy_socket.tcp.Socket_Data_Closed_by_Remote_Side_EventHandler(socket_closed_by_remote_side);
            this.socket_to_clt.event_Socket_Data_Connected_To_Remote_Host-=new easy_socket.tcp.Socket_Data_Connected_To_Remote_Host_EventHandler(socket_connected_to_remote_host);
            this.socket_to_clt.event_Socket_Data_DataArrival-=new easy_socket.tcp.Socket_Data_DataArrival_EventHandler(socket_data_arrival);
            this.socket_to_clt.event_Socket_Data_Error-=new easy_socket.tcp.Socket_Data_Error_EventHandler(socket_error);

            this.socket_to_srv.event_Socket_Data_Closed_by_Remote_Side-= new easy_socket.tcp.Socket_Data_Closed_by_Remote_Side_EventHandler(socket_closed_by_remote_side);
            this.socket_to_srv.event_Socket_Data_Connected_To_Remote_Host-=new easy_socket.tcp.Socket_Data_Connected_To_Remote_Host_EventHandler(socket_connected_to_remote_host);
            this.socket_to_srv.event_Socket_Data_DataArrival-=new easy_socket.tcp.Socket_Data_DataArrival_EventHandler(socket_data_arrival);
            this.socket_to_srv.event_Socket_Data_Error-=new easy_socket.tcp.Socket_Data_Error_EventHandler(socket_error);
        }

        protected void socket_error(easy_socket.tcp.Socket_Data sender, easy_socket.tcp.EventArgs_Exception e)
        {
            int res=System.Threading.WaitHandle.WaitAny(this.HexaViewWaitHandles);
            if (res==0) // close event
                return;

            this.add_info("Socket Error: "+e.exception.Message);
            this.check_close(sender);
            this.refresh_data();
            this.evtNotWrittingData.Set();
        }

        protected void socket_closed_by_remote_side(easy_socket.tcp.Socket_Data sender, EventArgs e)
        {
            int res=System.Threading.WaitHandle.WaitAny(this.HexaViewWaitHandles);
            if (res==0) // close event
                return;

            this.add_info("Connection closed by "+sender.RemoteIP+":"+sender.RemotePort);
            this.check_close(sender);
            this.refresh_data();
            this.evtNotWrittingData.Set();
        }

        protected void check_close(easy_socket.tcp.Socket_Data sender)
        {
            if (this.checkBox_transmit_close.Checked)
            {
                this.enable_state(false);
                if (sender==this.socket_to_clt)
                    this.socket_to_srv.close();
                else
                    this.socket_to_clt.close();
            }
            else
            {
                if (sender==this.socket_to_clt)
                {
                    this.radioButton_send_to_srv.Checked=true;
                    this.radioButton_send_to_clt.Enabled=false;
                }
                else
                {
                    this.radioButton_send_to_clt.Checked=true;
                    this.radioButton_send_to_srv.Enabled=false;
                }
            }
        }
        protected void socket_connected_to_remote_host(easy_socket.tcp.Socket_Data sender, EventArgs e)
        {
            int res=System.Threading.WaitHandle.WaitAny(this.HexaViewWaitHandles);
            if (res==0) // close event
                return;

            this.add_info("Connected to "+sender.RemoteIP+":"+sender.RemotePort);
            this.refresh_data();
            this.evtNotWrittingData.Set();
        }

        protected void socket_data_arrival(easy_socket.tcp.Socket_Data sender, easy_socket.tcp.EventArgs_ReceiveDataSocket e)
        {
            int res=System.Threading.WaitHandle.WaitAny(this.HexaViewWaitHandles);
            if (res==0) // close event
                return;

            System.Drawing.Color color=System.Drawing.Color.Black;
            bool b_transmit=false;
            if (sender==this.socket_to_clt)
            {
                // transfer data if required
                if (this.radioButton_clt_to_srv_allow.Checked)
                    b_transmit=true;
                else if (this.radioButton_clt_to_srv_block.Checked)
                    b_transmit=false;
                else// query
                {
                    string msg="Do you want to allow the transfer of following data from "+sender.RemoteIP+":"+sender.RemotePort.ToString()+"?\r\n"
                                +"Hexa Data:\r\n"
                                +this.split_in_multiple_lines(easy_socket.hexa_convert.byte_to_hexa(e.buffer),50)
                                +"Text Data:\r\n"
                                +this.split_in_multiple_lines(System.Text.Encoding.Default.GetString(e.buffer),50);
                                
                    b_transmit=(MessageBox.Show(this,msg,"Tcp Interactive",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes);
                }
                if (b_transmit)
                {
                    this.socket_to_srv.send(e.buffer);
                    color=System.Drawing.Color.Blue;
                }
                else
                    color=System.Drawing.Color.Violet;
            }
            else
            {
                // transfer data if required
                if (this.radioButton_srv_to_clt_allow.Checked)
                    b_transmit=true;
                else if (this.radioButton_srv_to_clt_block.Checked)
                    b_transmit=false;
                else// query
                {
                    string msg="Do you want to allow the transfer of following data from "+sender.RemoteIP+":"+sender.RemotePort.ToString()+"?\r\n"
                        +"Hexa Data:\r\n"
                        +this.split_in_multiple_lines(easy_socket.hexa_convert.byte_to_hexa(e.buffer),50)
                        +"Text Data:\r\n"
                        +this.split_in_multiple_lines(System.Text.Encoding.Default.GetString(e.buffer),50);
                    b_transmit=(MessageBox.Show(this,msg,"Tcp Interactive",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes);
                }
                if (b_transmit)
                {
                    this.socket_to_clt.send(e.buffer);
                    color=System.Drawing.Color.Black;
                }
                else
                    color=System.Drawing.Color.Red;
            }
            if (b_transmit)
                this.add_info("Data from "+sender.RemoteIP+":"+sender.RemotePort.ToString());
            else
                this.add_info("Data from "+sender.RemoteIP+":"+sender.RemotePort.ToString()+"(Blocked)");
            // color depends of sender && user action
            this.add_data(e.buffer,color);
            this.refresh_data();
            this.evtNotWrittingData.Set();
        }

        #region GUI events

        protected override void button_close_Click(object sender, System.EventArgs e)
        {
            if (this.checkBox_close_client_socket.Checked)
                this.socket_to_clt.close();
            if (this.checkBox_close_server_socket.Checked)
                this.socket_to_srv.close();
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
                this.socket_to_clt.send(data);
                this.add_info("Data send to "+this.socket_to_clt.RemoteIP+":"+this.socket_to_clt.RemotePort.ToString());
                this.add_data(data,System.Drawing.Color.Gray);
            }
            else
            {
                this.socket_to_srv.send(data);
                this.add_info("Data send to "+this.socket_to_srv.RemoteIP+":"+this.socket_to_srv.RemotePort.ToString());
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

	}
}
