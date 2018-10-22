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
    public class FormTrace : Network_Stuff.CommonTelnetForm
    {
        private System.Windows.Forms.Button button_trace;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox_icmp_start_with_hop;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox_icmp_delay_for_reply;
        private System.Windows.Forms.CheckBox checkBox_icmp_resolve_adresses;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox_icmp_end_with_hop;
        private System.Windows.Forms.TextBox textBox_icmp_ip;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel_data;

        private System.ComponentModel.Container components = null;

        public FormTrace(string ip,string delay,string ttl_start,string ttl_stop,bool dns_resolve)
        {
            InitializeComponent();
            Tools.GUI.XPStyle.MakeXPStyle(this);

            this.textBox_icmp_ip.Text=ip;
            this.textBox_icmp_delay_for_reply.Text=delay;
            this.textBox_icmp_start_with_hop.Text=ttl_start;
            this.textBox_icmp_end_with_hop.Text=ttl_stop;
            this.checkBox_icmp_resolve_adresses.Checked=dns_resolve;

            al=new System.Collections.ArrayList();
            // create echo
            this.icmp_echo=new easy_socket.icmp.icmp_echo();
            // create echo server
            this.icmp_server=new easy_socket.icmp.icmp_server();
            // add events
            this.add_events();
        }

        protected override void Dispose( bool disposing )
        {
            this.icmp_server.stop();
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormTrace));
            this.button_trace = new System.Windows.Forms.Button();
            this.panel_data = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox_icmp_start_with_hop = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox_icmp_delay_for_reply = new System.Windows.Forms.TextBox();
            this.checkBox_icmp_resolve_adresses = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox_icmp_end_with_hop = new System.Windows.Forms.TextBox();
            this.textBox_icmp_ip = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel_control.SuspendLayout();
            this.panel_data.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(416, 214);
            // 
            // textBox_editable
            // 
            this.textBox_editable.Location = new System.Drawing.Point(0, 226);
            this.textBox_editable.Name = "textBox_editable";
            this.textBox_editable.Size = new System.Drawing.Size(184, 75);
            // 
            // panel_control
            // 
            this.panel_control.Controls.Add(this.panel_data);
            this.panel_control.Controls.Add(this.button_trace);
            this.panel_control.Location = new System.Drawing.Point(224, 0);
            this.panel_control.Name = "panel_control";
            this.panel_control.Size = new System.Drawing.Size(192, 214);
            // 
            // textBox_telnet
            // 
            this.textBox_telnet.Name = "textBox_telnet";
            this.textBox_telnet.Size = new System.Drawing.Size(184, 226);
            // 
            // button_trace
            // 
            this.button_trace.Location = new System.Drawing.Point(56, 168);
            this.button_trace.Name = "button_trace";
            this.button_trace.TabIndex = 31;
            this.button_trace.Text = "Trace";
            this.button_trace.Click += new System.EventHandler(this.button_trace_Click);
            // 
            // panel_data
            // 
            this.panel_data.Controls.Add(this.label16);
            this.panel_data.Controls.Add(this.textBox_icmp_start_with_hop);
            this.panel_data.Controls.Add(this.label17);
            this.panel_data.Controls.Add(this.textBox_icmp_delay_for_reply);
            this.panel_data.Controls.Add(this.checkBox_icmp_resolve_adresses);
            this.panel_data.Controls.Add(this.label14);
            this.panel_data.Controls.Add(this.textBox_icmp_end_with_hop);
            this.panel_data.Controls.Add(this.textBox_icmp_ip);
            this.panel_data.Controls.Add(this.label11);
            this.panel_data.Location = new System.Drawing.Point(0, 8);
            this.panel_data.Name = "panel_data";
            this.panel_data.Size = new System.Drawing.Size(200, 152);
            this.panel_data.TabIndex = 36;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(12, 92);
            this.label16.Name = "label16";
            this.label16.TabIndex = 44;
            this.label16.Text = "End with hop";
            // 
            // textBox_icmp_start_with_hop
            // 
            this.textBox_icmp_start_with_hop.Location = new System.Drawing.Point(148, 60);
            this.textBox_icmp_start_with_hop.Name = "textBox_icmp_start_with_hop";
            this.textBox_icmp_start_with_hop.Size = new System.Drawing.Size(40, 20);
            this.textBox_icmp_start_with_hop.TabIndex = 38;
            this.textBox_icmp_start_with_hop.Text = "1";
            this.textBox_icmp_start_with_hop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(12, 68);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(120, 16);
            this.label17.TabIndex = 43;
            this.label17.Text = "Start with hop";
            // 
            // textBox_icmp_delay_for_reply
            // 
            this.textBox_icmp_delay_for_reply.Location = new System.Drawing.Point(140, 36);
            this.textBox_icmp_delay_for_reply.Name = "textBox_icmp_delay_for_reply";
            this.textBox_icmp_delay_for_reply.Size = new System.Drawing.Size(48, 20);
            this.textBox_icmp_delay_for_reply.TabIndex = 37;
            this.textBox_icmp_delay_for_reply.Text = "3000";
            this.textBox_icmp_delay_for_reply.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // checkBox_icmp_resolve_adresses
            // 
            this.checkBox_icmp_resolve_adresses.Checked = true;
            this.checkBox_icmp_resolve_adresses.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_icmp_resolve_adresses.Location = new System.Drawing.Point(20, 124);
            this.checkBox_icmp_resolve_adresses.Name = "checkBox_icmp_resolve_adresses";
            this.checkBox_icmp_resolve_adresses.Size = new System.Drawing.Size(132, 16);
            this.checkBox_icmp_resolve_adresses.TabIndex = 40;
            this.checkBox_icmp_resolve_adresses.Text = "Resolve adresses";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(12, 44);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(128, 16);
            this.label14.TabIndex = 42;
            this.label14.Text = "Delay for reply (in ms)";
            // 
            // textBox_icmp_end_with_hop
            // 
            this.textBox_icmp_end_with_hop.Location = new System.Drawing.Point(148, 84);
            this.textBox_icmp_end_with_hop.Name = "textBox_icmp_end_with_hop";
            this.textBox_icmp_end_with_hop.Size = new System.Drawing.Size(40, 20);
            this.textBox_icmp_end_with_hop.TabIndex = 39;
            this.textBox_icmp_end_with_hop.Text = "20";
            this.textBox_icmp_end_with_hop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox_icmp_ip
            // 
            this.textBox_icmp_ip.Location = new System.Drawing.Point(92, 12);
            this.textBox_icmp_ip.Name = "textBox_icmp_ip";
            this.textBox_icmp_ip.Size = new System.Drawing.Size(96, 20);
            this.textBox_icmp_ip.TabIndex = 36;
            this.textBox_icmp_ip.Text = "127.0.0.1";
            this.textBox_icmp_ip.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(12, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 16);
            this.label11.TabIndex = 41;
            this.label11.Text = "Host";
            // 
            // FormTrace
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(416, 214);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormTrace";
            this.Text = "Trace";
            this.panel_control.ResumeLayout(false);
            this.panel_data.ResumeLayout(false);

        }
        #endregion

        private string s_remote_ip;
        private int i_delay_for_reply;
        
        private easy_socket.icmp.icmp_echo icmp_echo;
        private easy_socket.icmp.icmp_server icmp_server;
        private bool b_stop;
        private bool b_is_user_interface_allowed;
        private System.Collections.ArrayList al;
        private byte b_ttl_start;
        private byte b_ttl_end;
        private byte b_current_hop;
        /// <summary>
        /// used to start trace
        /// </summary>
        public void trace()
        {
            if (!this.get_values())
                return;
            this.b_stop=false;
            this.allow_user_interface(false);
            this.b_current_hop=this.b_ttl_start;
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
            // watch replies
            this.icmp_server.set_wait_timeout(this.i_delay_for_reply);
            // send ping
            this.icmp_echo.send(this.s_remote_ip,this.b_current_hop);
            this.b_current_hop++;
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
            // check hop stat
            if (this.b_current_hop<this.b_ttl_end)
            {
                this.send();
                return;
            }
            // trace is done
            this.icmp_server.stop();// stop watching replies
            this.allow_user_interface(true);
        }
        private bool get_values()
        {
            this.s_remote_ip=this.textBox_icmp_ip.Text.Trim();
            if (!Tools.GUI.CCheck_user_interface_inputs.check_int(this.textBox_icmp_delay_for_reply.Text))
                return false;
            if (!Tools.GUI.CCheck_user_interface_inputs.check_byte(this.textBox_icmp_start_with_hop.Text))
                return false;
            if (!Tools.GUI.CCheck_user_interface_inputs.check_byte(this.textBox_icmp_end_with_hop.Text))
                return false;
            this.i_delay_for_reply=System.Convert.ToInt32(this.textBox_icmp_delay_for_reply.Text,10);
            this.b_ttl_start=System.Convert.ToByte(this.textBox_icmp_start_with_hop.Text,10);
            this.b_ttl_end=System.Convert.ToByte(this.textBox_icmp_end_with_hop.Text,10);
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
            // we stop 
            this.allow_user_interface(true);
            this.icmp_server.stop();
        }
        private void ev_socket_error(easy_socket.icmp.icmp sender, easy_socket.icmp.EventArgs_Exception e)
        {
            this.textBox_telnet_add("Socket error:.\r\n"+e.exception.Message+"\r\n");
            // we stop 
            this.allow_user_interface(true);
            this.icmp_server.stop();
        }
        private void ev_time_exceeded(easy_socket.icmp.icmp_time_exceeded_message sender, easy_socket.icmp.EventArgs_ipv4header_ReceiveData e)
        {
            if (!this.check_if_one_of_our_packets(sender.ih_and_original_dd))
                return;
            this.icmp_server.clear_wait_timeout();
            easy_socket.ip_header.ipv4_header initial_iph=new easy_socket.ip_header.ipv4_header();
            initial_iph.decode(sender.ih_and_original_dd);// may return the error error_datagram_not_complete but try to decode at least ip header
            this.textBox_telnet_add("Icmp time exceeded message from: "+e.ipv4header.SourceAddress+ " for ip: "+
                initial_iph.DestinationAddress+
                " (packet ttl: "+
                initial_iph.time_to_live.ToString()
                +")\r\n");
            this.textBox_telnet_add("Initial TTL:"+(this.b_current_hop-1)+"\r\n");//this.b_current_hop has already been increased
            if (this.checkBox_icmp_resolve_adresses.Checked)
                this.dns_resolve(e.ipv4header.SourceAddress);
            this.send_if_necessary();
        }
        private void ev_source_quench(easy_socket.icmp.icmp_source_quench sender, easy_socket.icmp.EventArgs_ipv4header_ReceiveData e)
        {
            if (!this.check_if_one_of_our_packets(sender.ih_and_original_dd))
                return;
            this.textBox_telnet_add("Icmp source quench message from: "+
                e.ipv4header.SourceAddress+"\r\n");
            // we stop 
            this.allow_user_interface(true);
            this.icmp_server.stop();
        }
        private void ev_parameter_problem(easy_socket.icmp.icmp_parameter_problem sender, easy_socket.icmp.EventArgs_ipv4header_ReceiveData e)
        {
            if (!this.check_if_one_of_our_packets(sender.ih_and_original_dd))
                return;
            this.textBox_telnet_add("Icmp parameter problem message from: "+
                e.ipv4header.SourceAddress+"\r\n");
            // we stop 
            this.allow_user_interface(true);
            this.icmp_server.stop();
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
            // we stop
            this.allow_user_interface(true);
            this.icmp_server.stop();
        }
        private void ev_echo_reply(easy_socket.icmp.icmp_echo_reply sender, easy_socket.icmp.EventArgs_ipv4header_ReceiveData e)
        {
            int old_time=(int)((sender.identifier<<16)+sender.sequence_number);
            // if echo reply was not one of our
            if (!this.check_identifier(old_time))
                return;
            this.icmp_server.clear_wait_timeout();
            // else comput time needed
            int time=System.Environment.TickCount;
            time-=old_time;
            if (time==0)
                time=1;
            this.textBox_telnet_add(e.ipv4header.SourceAddress+" reply in time<"+
                time.ToString()+" ms\r\n");
            if (this.checkBox_icmp_resolve_adresses.Checked)
                this.dns_resolve(e.ipv4header.SourceAddress);

            // we stop trace is finished
            this.allow_user_interface(true);
            this.icmp_server.stop();
        }

        private void dns_resolve(string ip)
        {
            int cpt;
            string str;
            try
            {
                System.Net.IPHostEntry iphe=System.Net.Dns.Resolve(ip);
                str="Name:\t"+iphe.HostName+"\r\n";
                str+="Address:\r\n";
                for (cpt=0;cpt<iphe.AddressList.Length;cpt++)
                    str+="\t"+iphe.AddressList[cpt]+"\r\n";
                if (iphe.Aliases.Length>0)
                {
                    str+="Aliases:\r\n";
                    for (cpt=0;cpt<iphe.Aliases.Length;cpt++)
                        str+="\t"+iphe.Aliases[cpt]+"\r\n";
                }
                this.textBox_telnet_add(str);
            }
            catch (Exception ex)
            {
                this.textBox_telnet_add(ex.Message);
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

        private void button_trace_Click(object sender, System.EventArgs e)
        {
            if (this.b_is_user_interface_allowed) 
            {// run
                this.textBox_telnet_set("");
                this.al.Clear();
                this.trace();
            }
            else// trace in action --> stop
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
                this.button_trace.Text="Trace";
                this.panel_data.Enabled=true;
            }
            else
            {
                this.button_trace.Text="Stop";
                this.panel_data.Enabled=false;// disable user interaction during trace
                this.Refresh();
            }
        }

    }
}
