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
    public class FormPing : Network_Stuff.CommonTelnetForm
    {
        private string s_remote_ip;
        private int i_delay_for_reply;
        private byte b_ttl;
        private int i_nb_pings;
        private bool b_looping;


        private easy_socket.icmp.icmp_echo icmp_echo;
        private easy_socket.icmp.icmp_server icmp_server;
        private int i_remaining_pings;
        private bool b_stop;
        private bool b_is_user_interface_allowed;
        private System.Collections.ArrayList al;

        private System.Windows.Forms.Button button_ping;
        private System.Windows.Forms.TextBox textBox_icmp_ping_number;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox_icmp_delay_dor_reply;
        private System.Windows.Forms.TextBox textBox_icmp_packet_ttl;
        private System.Windows.Forms.CheckBox checkBox_icmp_looping_ping;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox_icmp_ip;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel_data;
        private System.Windows.Forms.CheckBox checkBox_may_broadcast;

        private System.ComponentModel.Container components = null;

        protected override void Dispose( bool disposing )
        {
            this.icmp_server.stop();//stop server if open
            this.remove_events();
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
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_ping = new System.Windows.Forms.Button();
            this.panel_data = new System.Windows.Forms.Panel();
            this.checkBox_may_broadcast = new System.Windows.Forms.CheckBox();
            this.textBox_icmp_ping_number = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox_icmp_delay_dor_reply = new System.Windows.Forms.TextBox();
            this.textBox_icmp_packet_ttl = new System.Windows.Forms.TextBox();
            this.checkBox_icmp_looping_ping = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox_icmp_ip = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel.SuspendLayout();
            this.panel_control.SuspendLayout();
            this.panel_data.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Size = new System.Drawing.Size(424, 238);
            this.panel.Visible = true;
            // 
            // panel_control
            // 
            this.panel_control.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                        this.panel_data,
                                                                                        this.button_ping});
            this.panel_control.Location = new System.Drawing.Point(216, 0);
            this.panel_control.Size = new System.Drawing.Size(208, 238);
            this.panel_control.Visible = true;
            // 
            // button_ping
            // 
            this.button_ping.Location = new System.Drawing.Point(64, 192);
            this.button_ping.Name = "button_ping";
            this.button_ping.TabIndex = 26;
            this.button_ping.Text = "Ping";
            this.button_ping.Click += new System.EventHandler(this.button_ping_Click);
            // 
            // panel_data
            // 
            this.panel_data.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                     this.checkBox_may_broadcast,
                                                                                     this.textBox_icmp_ping_number,
                                                                                     this.label14,
                                                                                     this.label15,
                                                                                     this.textBox_icmp_delay_dor_reply,
                                                                                     this.textBox_icmp_packet_ttl,
                                                                                     this.checkBox_icmp_looping_ping,
                                                                                     this.label13,
                                                                                     this.textBox_icmp_ip,
                                                                                     this.label11});
            this.panel_data.Location = new System.Drawing.Point(0, 8);
            this.panel_data.Name = "panel_data";
            this.panel_data.Size = new System.Drawing.Size(208, 176);
            this.panel_data.TabIndex = 31;
            // 
            // checkBox_may_broadcast
            // 
            this.checkBox_may_broadcast.Location = new System.Drawing.Point(8, 120);
            this.checkBox_may_broadcast.Name = "checkBox_may_broadcast";
            this.checkBox_may_broadcast.Size = new System.Drawing.Size(192, 48);
            this.checkBox_may_broadcast.TabIndex = 40;
            this.checkBox_may_broadcast.Text = "More than one host can reply (wait delay for reply before sending another ping)";
            // 
            // textBox_icmp_ping_number
            // 
            this.textBox_icmp_ping_number.Location = new System.Drawing.Point(152, 80);
            this.textBox_icmp_ping_number.Name = "textBox_icmp_ping_number";
            this.textBox_icmp_ping_number.Size = new System.Drawing.Size(48, 20);
            this.textBox_icmp_ping_number.TabIndex = 34;
            this.textBox_icmp_ping_number.Text = "3";
            this.textBox_icmp_ping_number.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(8, 32);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(128, 16);
            this.label14.TabIndex = 38;
            this.label14.Text = "Delay for reply (in ms)";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(8, 80);
            this.label15.Name = "label15";
            this.label15.TabIndex = 39;
            this.label15.Text = "Number of ping";
            // 
            // textBox_icmp_delay_dor_reply
            // 
            this.textBox_icmp_delay_dor_reply.Location = new System.Drawing.Point(152, 32);
            this.textBox_icmp_delay_dor_reply.Name = "textBox_icmp_delay_dor_reply";
            this.textBox_icmp_delay_dor_reply.Size = new System.Drawing.Size(48, 20);
            this.textBox_icmp_delay_dor_reply.TabIndex = 32;
            this.textBox_icmp_delay_dor_reply.Text = "3000";
            this.textBox_icmp_delay_dor_reply.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox_icmp_packet_ttl
            // 
            this.textBox_icmp_packet_ttl.Location = new System.Drawing.Point(152, 56);
            this.textBox_icmp_packet_ttl.Name = "textBox_icmp_packet_ttl";
            this.textBox_icmp_packet_ttl.Size = new System.Drawing.Size(48, 20);
            this.textBox_icmp_packet_ttl.TabIndex = 33;
            this.textBox_icmp_packet_ttl.Text = "128";
            this.textBox_icmp_packet_ttl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // checkBox_icmp_looping_ping
            // 
            this.checkBox_icmp_looping_ping.Location = new System.Drawing.Point(8, 104);
            this.checkBox_icmp_looping_ping.Name = "checkBox_icmp_looping_ping";
            this.checkBox_icmp_looping_ping.Size = new System.Drawing.Size(104, 16);
            this.checkBox_icmp_looping_ping.TabIndex = 35;
            this.checkBox_icmp_looping_ping.Text = "Looping pings";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(8, 56);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(120, 16);
            this.label13.TabIndex = 37;
            this.label13.Text = "Packet TTL (max 255)";
            // 
            // textBox_icmp_ip
            // 
            this.textBox_icmp_ip.Location = new System.Drawing.Point(104, 8);
            this.textBox_icmp_ip.Name = "textBox_icmp_ip";
            this.textBox_icmp_ip.Size = new System.Drawing.Size(96, 20);
            this.textBox_icmp_ip.TabIndex = 31;
            this.textBox_icmp_ip.Text = "127.0.0.1";
            this.textBox_icmp_ip.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(8, 8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 16);
            this.label11.TabIndex = 36;
            this.label11.Text = "Host";
            // 
            // FormPing
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(424, 238);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.panel_control,
                                                                          this.textBox_telnet,
                                                                          this.panel});
            this.Name = "FormPing";
            this.Text = "Ping";
            this.panel.ResumeLayout(false);
            this.panel_control.ResumeLayout(false);
            this.panel_data.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        public FormPing(string host,string delay,string ttl,string nb_ping,bool looping,bool b_may_multiple_replies)
        {
            InitializeComponent();

            XPStyle.MakeXPStyle(this);

            this.textBox_icmp_ip.Text=host;
            this.textBox_icmp_delay_dor_reply.Text=delay;
            this.textBox_icmp_packet_ttl.Text=ttl;
            this.textBox_icmp_ping_number.Text=nb_ping;
            this.checkBox_icmp_looping_ping.Checked=looping;
            this.checkBox_may_broadcast.Checked=b_may_multiple_replies;
            this.textBox_icmp_ping_number.Enabled=!this.checkBox_icmp_looping_ping.Checked;
            al=new System.Collections.ArrayList(9);
            // create echo
            this.icmp_echo=new easy_socket.icmp.icmp_echo();
            // create echo server
            this.icmp_server=new easy_socket.icmp.icmp_server();
            // add events
            this.add_events();
         }
        /// <summary>
        /// used to start ping sequence
        /// </summary>
        public void send_ping()
        {
            if (!this.get_values())
                return;
            this.b_stop=false;
            this.allow_user_interface(false);
            this.i_remaining_pings=this.i_nb_pings;
            this.icmp_server.start();
            this.send();
        }
        /// <summary>
        /// used to send one echo request
        /// </summary>
        private void send()
        {
            // identify message
            int t_ini=System.Environment.TickCount;
            this.add_identifier(t_ini);
            this.icmp_echo.identifier=(UInt16)(t_ini>>16);
            this.icmp_echo.sequence_number=(UInt16)(t_ini&0xffffffff);
            // decrease ping number
            if (!this.b_looping)
                this.i_remaining_pings--;
            // watch replies
            this.icmp_server.set_wait_timeout(this.i_delay_for_reply);
            // send ping
            this.icmp_echo.send(this.s_remote_ip,this.b_ttl);
        }
        /// <summary>
        /// check if it's needed to send another echo request and do it if it is the case
        /// </summary>
        private void send_if_necessary()
        {
            // if user had cancel operations
            if (this.b_stop)
            {
                this.icmp_server.stop();// stop watching replies
                this.allow_user_interface(true);
                return;
            }
            // if looping ping
            if (this.b_looping)
            {
                this.send();
                return;
            }
            // not looping --> count remaining ping
            if (this.i_remaining_pings>0)
            {
                this.send();
                return;
            }
            // all echo have been sended
            this.icmp_server.stop();// stop watching replies
            this.allow_user_interface(true);
        }
        private bool get_values()
        {
            this.b_looping=this.checkBox_icmp_looping_ping.Checked;
            this.s_remote_ip=this.textBox_icmp_ip.Text.Trim();
            if (!CCheck_user_interface_inputs.check_int(this.textBox_icmp_delay_dor_reply.Text))
                return false;
            if (!CCheck_user_interface_inputs.check_int(this.textBox_icmp_ping_number.Text))
                return false;
            if (!CCheck_user_interface_inputs.check_byte(this.textBox_icmp_packet_ttl.Text))
                return false;
            this.i_delay_for_reply=System.Convert.ToInt32(this.textBox_icmp_delay_dor_reply.Text,10);
            this.b_ttl=System.Convert.ToByte(this.textBox_icmp_packet_ttl.Text,10);
            this.i_nb_pings=System.Convert.ToInt32(this.textBox_icmp_ping_number.Text,10);
            // resolve ip if necessary
            System.Net.IPAddress ipaddr;
            int cpt;
            try
            {
                // don't resolve if ip is like xxx.yyy.uuu.vvv
                ipaddr=System.Net.IPAddress.Parse(this.s_remote_ip);
                return true;
            }
            catch
            {
                // else resolve
                try
                {
                    System.Net.IPHostEntry iphe=System.Net.Dns.Resolve(this.s_remote_ip);
                    this.textBox_telnet_set("Name:\t"+iphe.HostName+"\r\n");
                    this.textBox_telnet_add("Address:\r\n");
                    for (cpt=0;cpt<iphe.AddressList.Length;cpt++)
                        this.textBox_telnet_add("\t"+iphe.AddressList[cpt]+"\r\n");
                    if (iphe.Aliases.Length>0)
                    {
                        this.textBox_telnet_add("Aliases:\r\n");
                        for (cpt=0;cpt<iphe.Aliases.Length;cpt++)
                            this.textBox_telnet_add("\t"+iphe.Aliases[cpt]+"\r\n");
                    }
                    this.textBox_telnet.SelectionStart=this.textBox_telnet.Text.Length;
                    this.s_remote_ip=iphe.AddressList[0].ToString();
                    return true;
                }
                catch (Exception ex)
                {
                    this.textBox_telnet_set(ex.Message);
                    this.textBox_telnet.SelectionStart=this.textBox_telnet.Text.Length;
                    return false;
                }
            }
        }
        private void add_events()
        {
            // client events
            this.icmp_echo.event_Socket_Error+=new easy_socket.icmp.Socket_Error_EventHandler(ev_socket_error); // same event for server and client
            // server events
            // icmp_destination_unreachable
            this.icmp_server.event_icmp_destination_unreachable_Data_Arrival+=new easy_socket.icmp.icmp_destination_unreachable_Data_Arrival_EventHandler(ev_destination_unreachable);
            // icmp_reply event
            this.icmp_server.event_icmp_echo_reply_Data_Arrival+=new easy_socket.icmp.icmp_echo_reply_Data_Arrival_EventHandler(ev_echo_reply);
            // icmp_parameter_problem
            this.icmp_server.event_icmp_parameter_problem_Data_Arrival+=new easy_socket.icmp.icmp_parameter_problem_Data_Arrival_EventHandler(ev_parameter_problem);
            // icmp_source_quench
            this.icmp_server.event_icmp_source_quench_Data_Arrival+=new easy_socket.icmp.icmp_source_quench_Data_Arrival_EventHandler(ev_source_quench);
            // icmp_time_exceeded
            this.icmp_server.event_icmp_time_exceeded_message_Data_Arrival+=new easy_socket.icmp.icmp_time_exceeded_message_Data_Arrival_EventHandler(ev_time_exceeded);
            // Error event
            this.icmp_server.event_Socket_Error+=new easy_socket.icmp.Socket_Error_EventHandler(ev_socket_error);
            // TimeOut event
            this.icmp_server.event_TimeOut+=new easy_socket.icmp.TimeOut_EventHandler(ev_time_out);
        }
        private void remove_events()
        {
            // client events
            this.icmp_echo.event_Socket_Error-=new easy_socket.icmp.Socket_Error_EventHandler(ev_socket_error); // same event for server and client
            // server events
            // icmp_destination_unreachable
            this.icmp_server.event_icmp_destination_unreachable_Data_Arrival-=new easy_socket.icmp.icmp_destination_unreachable_Data_Arrival_EventHandler(ev_destination_unreachable);
            // icmp_reply event
            this.icmp_server.event_icmp_echo_reply_Data_Arrival-=new easy_socket.icmp.icmp_echo_reply_Data_Arrival_EventHandler(ev_echo_reply);
            // icmp_parameter_problem
            this.icmp_server.event_icmp_parameter_problem_Data_Arrival-=new easy_socket.icmp.icmp_parameter_problem_Data_Arrival_EventHandler(ev_parameter_problem);
            // icmp_source_quench
            this.icmp_server.event_icmp_source_quench_Data_Arrival-=new easy_socket.icmp.icmp_source_quench_Data_Arrival_EventHandler(ev_source_quench);
            // icmp_time_exceeded
            this.icmp_server.event_icmp_time_exceeded_message_Data_Arrival-=new easy_socket.icmp.icmp_time_exceeded_message_Data_Arrival_EventHandler(ev_time_exceeded);
            // Error event
            this.icmp_server.event_Socket_Error-=new easy_socket.icmp.Socket_Error_EventHandler(ev_socket_error);
            // TimeOut event
            this.icmp_server.event_TimeOut-=new easy_socket.icmp.TimeOut_EventHandler(ev_time_out);
        }
        private void ev_time_out(easy_socket.icmp.icmp sender,EventArgs e)
        {
            // forget precedent echo messages
            this.al.Clear();
            this.textBox_telnet_add("Timeout.\r\n");
            this.send_if_necessary();
        }
        private void ev_socket_error(easy_socket.icmp.icmp sender, easy_socket.icmp.EventArgs_Exception e)
        {
            this.textBox_telnet_add("Socket error:.\r\n"+e.exception.Message+"\r\n");
            if (sender is easy_socket.icmp.icmp_server)
            {
                this.allow_user_interface(true);
            }
        }
        private void ev_time_exceeded(easy_socket.icmp.icmp_time_exceeded_message sender, easy_socket.icmp.EventArgs_ipv4header_ReceiveData e)
        {
            if (!this.check_if_one_of_our_packets(sender.ih_and_original_dd))
                return;
            easy_socket.ip_header.ipv4_header initial_iph=new easy_socket.ip_header.ipv4_header();
            initial_iph.decode(sender.ih_and_original_dd);// may return the error error_datagram_not_complete but try to decode at least ip header
            this.textBox_telnet_add("Icmp time exceeded message from: "+e.ipv4header.SourceAddress+ " for ip: "+
                                        initial_iph.DestinationAddress+
                                        " (packet ttl: "+
                                        initial_iph.time_to_live.ToString()
                                        +")\r\n");
            // in case of broadcast wait time out before sending another ping
            if (!this.checkBox_may_broadcast.Checked)
                this.send_if_necessary();
        }
        private void ev_source_quench(easy_socket.icmp.icmp_source_quench sender, easy_socket.icmp.EventArgs_ipv4header_ReceiveData e)
        {
            if (!this.check_if_one_of_our_packets(sender.ih_and_original_dd))
                return;
            this.textBox_telnet_add("Icmp source quench message from: "+
                                       e.ipv4header.SourceAddress+"\r\n");
            // in case of broadcast wait time out before sending another ping
            if (!this.checkBox_may_broadcast.Checked)
                this.send_if_necessary();
        }
        private void ev_parameter_problem(easy_socket.icmp.icmp_parameter_problem sender, easy_socket.icmp.EventArgs_ipv4header_ReceiveData e)
        {
            if (!this.check_if_one_of_our_packets(sender.ih_and_original_dd))
                return;
            this.textBox_telnet_add("Icmp parameter problem message from: "+
                e.ipv4header.SourceAddress+"\r\n");
            // in case of broadcast wait time out before sending another ping
            if (!this.checkBox_may_broadcast.Checked)
                this.send_if_necessary();
        }
        private void ev_destination_unreachable(easy_socket.icmp.icmp_destination_unreachable sender, easy_socket.icmp.EventArgs_ipv4header_ReceiveData e)
        {
            if (!this.check_if_one_of_our_packets(sender.ih_and_original_dd))
                return;
            easy_socket.ip_header.ipv4_header initial_iph=new easy_socket.ip_header.ipv4_header();
            initial_iph.decode(sender.ih_and_original_dd);// may return the error error_datagram_not_complete but decode at least ip header
            this.textBox_telnet_add("Icmp destination unreachable from: "+
                e.ipv4header.SourceAddress+
                " for destination: "+initial_iph.DestinationAddress+"\r\n");
            // in case of broadcast wait time out before sending another ping
            if (!this.checkBox_may_broadcast.Checked)
                this.send_if_necessary();
        }
        private void ev_echo_reply(easy_socket.icmp.icmp_echo_reply sender, easy_socket.icmp.EventArgs_ipv4header_ReceiveData e)
        {
            int old_time=(int)((sender.identifier<<16)+sender.sequence_number);
            // if echo reply was not one of our
            if (!this.check_identifier(old_time))
                return;
            // else comput time needed
            int time=System.Environment.TickCount;
            time-=old_time;
            if (time==0)
                time=1;
            this.textBox_telnet_add(e.ipv4header.SourceAddress+" reply in time<"+
                time.ToString()+" ms\r\n");
            // in case of broadcast wait time out before sending another ping
            // else 
            if (!this.checkBox_may_broadcast.Checked)
            {
                this.icmp_server.clear_wait_timeout();
                this.send_if_necessary();// send echo
            }
        }

        #region check id of echo replies
        
        private void add_identifier(int id)
        {
            this.al.Add(id);
        }
        private bool check_identifier(int id)
        {
            if (this.al.IndexOf(id)>-1)
                return true;
            return false;
        }
        #endregion

        protected bool check_if_one_of_our_packets(byte[] data)
        {
            easy_socket.ip_header.ipv4_header iph=new easy_socket.ip_header.ipv4_header();
            /*
            error_success=0;
            error_datagram_null=1;
            error_datagram_internet_header_length_too_small=2;
            error_datagram_total_length_too_small=3;
            error_datagram_not_complete=4;
            */
            byte b=iph.decode(data);
            if ((b==easy_socket.ip_header.ipv4_header.error_datagram_null )
                ||(b==easy_socket.ip_header.ipv4_header.error_datagram_internet_header_length_too_small)
                ||(b==easy_socket.ip_header.ipv4_header.error_datagram_total_length_too_small))
                return false;
            if (iph.protocol!=easy_socket.ip_header.ipv4_header.protocol_icmp)
                return false;
            // error_success || error_datagram_not_complete
            if (iph.data==null)
                return false;
            if (iph.data.Length<8) // 8=icmp_echo header size
                return false;
            if (iph.data[0]!=8) // it's not a reply to an echo msg
                return false;
            easy_socket.icmp.icmp_echo ie=new easy_socket.icmp.icmp_echo();
            ie.decode(iph.data);

            int id=(ie.identifier<<16)+ie.sequence_number;
            return this.check_identifier(id);
        }
    
        private void checkBox_icmp_looping_ping_CheckedChanged(object sender, System.EventArgs e)
        {
            this.textBox_icmp_ping_number.Enabled=!this.checkBox_icmp_looping_ping.Checked;
        }

        private void button_ping_Click(object sender, System.EventArgs e)
        {
            if (this.b_is_user_interface_allowed) 
            {// run
                this.textBox_telnet_set("");
                this.al.Clear();
                this.send_ping();
            }
            else// pings in action --> stop
            {
                this.icmp_server.stop();
                this.b_stop=true;
                this.allow_user_interface(true);
            }
        }
        private void allow_user_interface(bool b)
        {
            this.b_is_user_interface_allowed=b;
            if (b)
            {
                this.button_ping.Text="Ping";
                this.panel_data.Enabled=true;
            }
            else
            {
                this.button_ping.Text="Stop";
                this.panel_data.Enabled=false;// disable user interaction during ping
                this.Refresh();
            }
        }
    }
}
