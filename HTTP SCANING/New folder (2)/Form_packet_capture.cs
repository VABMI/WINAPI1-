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
    /// Description résumée de Form_packet_capture.
    /// </summary>
    public class Form_packet_capture : System.Windows.Forms.Form
    {
        private easy_socket.ip_header.ipv4_header_server iph_server;
        private easy_socket.tcp_header.tcp_header tcph;
        private easy_socket.udp_header.udp_header udph;
        private easy_socket.icmp.icmp_echo_reply icmper;
        private easy_socket.icmp.icmp_destination_unreachable icmpdu;
        private easy_socket.icmp.icmp_source_quench icmpsq;
        private easy_socket.icmp.icmp_redirect icmpr;
        private easy_socket.icmp.icmp_echo icmpe;
        private easy_socket.icmp.icmp_time_exceeded_message icmptem;
        private easy_socket.icmp.icmp_parameter_problem icmppp;
        private easy_socket.icmp.icmp_timestamp icmpt;
        private easy_socket.icmp.icmp_timestamp_reply icmptr;
        private easy_socket.icmp.icmp_information_request icmpirequest;
        private easy_socket.icmp.icmp_information_reply icmpireply;

        private PACKET_TYPE last_packet_type;
        private string last_application_name;
        private string[] spyed_applications_name;
        private byte[] last_full_packet;
        private string[] ip_ipsrc_list;
        private string[] ip_ipdst_list;

        private string[] other_ipsrc_list;
        private string[] other_ipdst_list;
        private ushort[] other_protocol_list;

        private string[] icmp_ipsrc_list;
        private string[] icmp_ipdst_list;

        private string[] udp_ipsrc_list;
        private string[] udp_ipdst_list;
        private ushort[] udp_portsrc_list;
        private ushort[] udp_portdst_list;

        private string[] tcp_ipsrc_list;
        private string[] tcp_ipdst_list;
        private ushort[] tcp_portsrc_list;
        private ushort[] tcp_portdst_list;

        // design
        private System.Windows.Forms.CheckBox checkBox_icmp;
        private System.Windows.Forms.CheckBox checkBox_udp;
        private System.Windows.Forms.CheckBox checkBox_tcp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl_proto_capture;
        private System.Windows.Forms.TextBox textBox_tcp_port_source;
        private System.Windows.Forms.TextBox textBox_tcp_port_dest;
        private System.Windows.Forms.CheckBox checkBox_tcp_port_source;
        private System.Windows.Forms.CheckBox checkBox_tcp_port_dest;
        private System.Windows.Forms.TextBox textBox_tcp_ip_source;
        private System.Windows.Forms.TextBox textBox_tcp_ip_dest;
        private System.Windows.Forms.CheckBox checkBox_tcp_ip_source;
        private System.Windows.Forms.CheckBox checkBox_tcp_ip_dest;
        private System.Windows.Forms.TextBox textBox_udp_port_source;
        private System.Windows.Forms.TextBox textBox_udp_port_dest;
        private System.Windows.Forms.CheckBox checkBox_udp_port_source;
        private System.Windows.Forms.CheckBox checkBox_udp_port_dest;
        private System.Windows.Forms.TextBox textBox_udp_ip_source;
        private System.Windows.Forms.CheckBox checkBox_udp_ip_source;
        private System.Windows.Forms.CheckBox checkBox_udp_ip_dest;
        private System.Windows.Forms.CheckBox checkBox_icmp_timestamp_reply;
        private System.Windows.Forms.CheckBox checkBox_icmp_timestamp;
        private System.Windows.Forms.CheckBox checkBox_icmp_source_quench;
        private System.Windows.Forms.CheckBox checkBox_icmp_destination_unreachable;
        private System.Windows.Forms.CheckBox checkBox_icmp_redirect;
        private System.Windows.Forms.CheckBox checkBox_icmp_parameter_problem;
        private System.Windows.Forms.CheckBox checkBox_icmp_information_reply;
        private System.Windows.Forms.CheckBox checkBox_icmp_information_request;
        private System.Windows.Forms.CheckBox checkBox_icmp_echo_reply;
        private System.Windows.Forms.CheckBox checkBox_icmp_echo;
        private System.Windows.Forms.TextBox textBox_icmp_ip_source;
        private System.Windows.Forms.TextBox textBox_icmp_ip_dest;
        private System.Windows.Forms.CheckBox checkBox_icmp_ip_source;
        private System.Windows.Forms.CheckBox checkBox_icmp_ip_dest;
        private System.Windows.Forms.TextBox textBox_other_protocol;
        private System.Windows.Forms.CheckBox checkBox_other_protocol;
        private System.Windows.Forms.TextBox textBox_other_ip_source;
        private System.Windows.Forms.TextBox textBox_other_ip_dest;
        private System.Windows.Forms.CheckBox checkBox_other_ip_source;
        private System.Windows.Forms.CheckBox checkBox_other_ip_dest;
        private System.Windows.Forms.CheckBox checkBox_other;
        private System.Windows.Forms.RadioButton radioButton_ip_filter_or;
        private System.Windows.Forms.RadioButton radioButton_ip_filter_and;
        private System.Windows.Forms.NumericUpDown numericUpDown_ip_identification;
        private System.Windows.Forms.ComboBox comboBox_ip_flags_fragment_pos;
        private System.Windows.Forms.ComboBox comboBox_ip_flags_fragment_type;
        private System.Windows.Forms.ComboBox comboBox_ip_tos_relibility;
        private System.Windows.Forms.ComboBox comboBox_ip_tos_throughtput;
        private System.Windows.Forms.ComboBox comboBox_ip_tos_delay;
        private System.Windows.Forms.ComboBox comboBox_ip_tos_precedence;
        private System.Windows.Forms.CheckBox checkBox_ip_Precedence;
        private System.Windows.Forms.CheckBox checkBox_ip_Delay;
        private System.Windows.Forms.CheckBox checkBox_ip_Throughput;
        private System.Windows.Forms.CheckBox checkBox_ip_Relibility;
        private System.Windows.Forms.CheckBox checkBox_ip_Identification;
        private System.Windows.Forms.CheckBox checkBox_ip_Fragment_type;
        private System.Windows.Forms.CheckBox checkBox_ip_Fragment_position;
        private System.Windows.Forms.TextBox textBox_ip_ipsource;
        private System.Windows.Forms.TextBox textBox_ip_ipdest;
        private System.Windows.Forms.CheckBox checkBox_ip_ipsource;
        private System.Windows.Forms.CheckBox checkBox_ip_ipdest;
        private System.Windows.Forms.CheckBox checkBox_udp_length;
        private System.Windows.Forms.NumericUpDown numericUpDown_udp_length_min;
        private System.Windows.Forms.NumericUpDown numericUpDown_udp_length_max;
        private System.Windows.Forms.CheckBox checkBox_tcp_seq_num;
        private System.Windows.Forms.CheckBox checkBox_tcp_ack_num;
        private System.Windows.Forms.CheckBox checkBox_tcp_data_offset;
        private System.Windows.Forms.CheckBox checkBox_tcp_window;
        private System.Windows.Forms.NumericUpDown numericUpDown_tcp_data_offset_min;
        private System.Windows.Forms.NumericUpDown numericUpDown_tcp_data_offset_max;
        private System.Windows.Forms.NumericUpDown numericUpDown_tcp_seq_num;
        private System.Windows.Forms.NumericUpDown numericUpDown_tcp_window_max;
        private System.Windows.Forms.NumericUpDown numericUpDown_tcp_window_min;
        private System.Windows.Forms.NumericUpDown numericUpDown_tcp_ack_num;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox_tcp_control_options;
        private System.Windows.Forms.CheckBox checkBox_tcp_fin;
        private System.Windows.Forms.CheckBox checkBox_tcp_syn;
        private System.Windows.Forms.CheckBox checkBox_tcp_rst;
        private System.Windows.Forms.CheckBox checkBox_tcp_push;
        private System.Windows.Forms.CheckBox checkBox_tcp_ack;
        private System.Windows.Forms.CheckBox checkBox_tcp_urg;
        private System.Windows.Forms.RadioButton radioButton_tcp_and_all_control;
        private System.Windows.Forms.RadioButton radioButton_tcp_or_all_control;
        private System.Windows.Forms.RadioButton radioButton_tcp_and_any_control;
        private System.Windows.Forms.RadioButton radioButton_tcp_or_any_control;
        private System.Windows.Forms.CheckBox checkBox_icmp_time_exceeded_message;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.TextBox textBox_udp_ip_dest;
        private System.Windows.Forms.CheckBox checkBox_packets_details;
        private System.Windows.Forms.CheckBox checkBox_IP;
        private System.Windows.Forms.Button button_clear_list_view;
        private System.Windows.Forms.ContextMenu contextMenu_list_view;
        private System.Windows.Forms.MenuItem menuItem_clear;
        private System.Windows.Forms.MenuItem menuItem_save_all;
        private System.Windows.Forms.MenuItem menuItem_save_selected;
        private System.Windows.Forms.MenuItem menuItem_protocol_data;
        private System.Windows.Forms.MenuItem menuItem_copy_selected;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.CheckBox checkBox_all_packets;
        private Network_Stuff.UserControlPacketsView userControlPacketsView;
        private System.Windows.Forms.Panel panel_tcp_ipport_source_and_or_dest;
        private System.Windows.Forms.RadioButton radioButton_tcp_ipport_source_or_dest;
        private System.Windows.Forms.RadioButton radioButton_tcp_ipport_source_and_dest;
        private System.Windows.Forms.RadioButton radioButton_udp_ipport_source_and_dest;
        private System.Windows.Forms.RadioButton radioButton_udp_ipport_source_or_dest;
        private System.Windows.Forms.RadioButton radioButton_ip_ip_source_and_ip_dest;
        private System.Windows.Forms.RadioButton radioButton_ip_ip_source_or_ip_dest;
        private System.Windows.Forms.RadioButton radioButton_other_ip_source_and_ip_dest;
        private System.Windows.Forms.RadioButton radioButton_other_ip_source_or_ip_dest;
        private System.Windows.Forms.RadioButton radioButton_icmp_ip_source_and_ip_dest;
        private System.Windows.Forms.RadioButton radioButton_icmp_ip_source_or_ip_dest;
        private System.Windows.Forms.Panel panel_udp_ipport_source_and_or_dest;
        private System.Windows.Forms.Panel panel_ip_ip_source_and_or_dest;
        private System.Windows.Forms.Panel panel_other_ip_source_and_or_dest;
        private System.Windows.Forms.Panel panel_icmp_ip_source_and_or_dest;
        private System.Windows.Forms.Button button_load_capture;
        private System.Windows.Forms.TabPage tabPage_tcp;
        private System.Windows.Forms.TabPage tabPage_other;
        private System.Windows.Forms.TabPage tabPage_icmp;
        private System.Windows.Forms.TabPage tabPage_udp;
        private System.Windows.Forms.TabPage tabPage_ip;
        private System.Windows.Forms.TabPage tabPage_app_name;
        private System.Windows.Forms.CheckBox checkBox_app_name_filter_app_name;
        private System.Windows.Forms.CheckBox checkBox_app_name_get_all_udp_clt;
        private System.Windows.Forms.CheckBox checkBox_app_name_get_app_name;
        private System.Windows.Forms.ListBox listBox_app_name;
        private System.Windows.Forms.Button button_app_name_add;
        private System.Windows.Forms.Button button_app_name_remove;
        private System.Windows.Forms.OpenFileDialog openFileDialog;

        private System.ComponentModel.Container components = null;

        private enum PACKET_TYPE:byte
        {
            none=0,
            raw,
            udp,
            tcp,
            icmp_echo_reply,
            icmp_destination_unreachable,
            icmp_source_quench,
            icmp_redirect,
            icmp_echo,
            icmp_time_exceeded_message,
            icmp_parameter_problem,
            icmp_timestamp,
            icmp_timestamp_reply,
            icmp_information_request,
            icmp_information_reply
        }

        public Form_packet_capture()
        {
            InitializeComponent();
            XPStyle.MakeXPStyle(this);
            this.OnResize(null);

            this.iph_server=new easy_socket.ip_header.ipv4_header_server();;
            this.tcph=new easy_socket.tcp_header.tcp_header();
            this.udph=new easy_socket.udp_header.udp_header();

            this.icmper=new easy_socket.icmp.icmp_echo_reply();
            this.icmpdu=new easy_socket.icmp.icmp_destination_unreachable();
            this.icmpsq=new easy_socket.icmp.icmp_source_quench();
            this.icmpr=new easy_socket.icmp.icmp_redirect();
            this.icmpe=new easy_socket.icmp.icmp_echo();
            this.icmptem=new easy_socket.icmp.icmp_time_exceeded_message();
            this.icmppp=new easy_socket.icmp.icmp_parameter_problem();
            this.icmpt=new easy_socket.icmp.icmp_timestamp();
            this.icmptr=new easy_socket.icmp.icmp_timestamp_reply();
            this.icmpirequest=new easy_socket.icmp.icmp_information_request();
            this.icmpireply=new easy_socket.icmp.icmp_information_reply();

            this.last_packet_type=PACKET_TYPE.none;
            this.last_application_name="";

            this.iph_server.event_Socket_Data_Arrival+=new easy_socket.ip_header.Socket_Data_Arrival_EventHandler(iph_server_event_Socket_Data_Arrival);
            this.iph_server.event_Socket_Error+=new easy_socket.ip_header.Socket_Error_EventHandler(iph_server_event_Socket_Error);
            this.iph_server.event_Socket_RcvPacket_Error+=new easy_socket.ip_header.Socket_RcvPacket_Error_EventHandler(iph_server_event_Socket_RcvPacket_Error);

            // interface
            this.comboBox_ip_flags_fragment_pos.SelectedIndex=0;
            this.comboBox_ip_flags_fragment_type.SelectedIndex=0;
            this.comboBox_ip_tos_delay.SelectedIndex=0;
            this.comboBox_ip_tos_precedence.SelectedIndex=0;
            this.comboBox_ip_tos_relibility.SelectedIndex=0;
            this.comboBox_ip_tos_throughtput.SelectedIndex=0;

            this.userControlPacketsView.listView_capture.Columns[10].Text="Application name";

            if (!this.is_xp_os())
                this.tabPage_app_name.Enabled=false;

        }

        protected override void Dispose( bool disposing )
        {
            this.iph_server.stop();
            this.iph_server.event_Socket_Data_Arrival-=new easy_socket.ip_header.Socket_Data_Arrival_EventHandler(iph_server_event_Socket_Data_Arrival);
            this.iph_server.event_Socket_Error-=new easy_socket.ip_header.Socket_Error_EventHandler(iph_server_event_Socket_Error);
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
            this.checkBox_icmp = new System.Windows.Forms.CheckBox();
            this.checkBox_udp = new System.Windows.Forms.CheckBox();
            this.checkBox_tcp = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl_proto_capture = new System.Windows.Forms.TabControl();
            this.tabPage_tcp = new System.Windows.Forms.TabPage();
            this.textBox_tcp_port_dest = new System.Windows.Forms.TextBox();
            this.textBox_tcp_ip_dest = new System.Windows.Forms.TextBox();
            this.panel_tcp_ipport_source_and_or_dest = new System.Windows.Forms.Panel();
            this.radioButton_tcp_ipport_source_and_dest = new System.Windows.Forms.RadioButton();
            this.radioButton_tcp_ipport_source_or_dest = new System.Windows.Forms.RadioButton();
            this.checkBox_tcp_fin = new System.Windows.Forms.CheckBox();
            this.checkBox_tcp_syn = new System.Windows.Forms.CheckBox();
            this.checkBox_tcp_rst = new System.Windows.Forms.CheckBox();
            this.checkBox_tcp_push = new System.Windows.Forms.CheckBox();
            this.checkBox_tcp_ack = new System.Windows.Forms.CheckBox();
            this.checkBox_tcp_urg = new System.Windows.Forms.CheckBox();
            this.radioButton_tcp_and_all_control = new System.Windows.Forms.RadioButton();
            this.radioButton_tcp_or_all_control = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButton_tcp_and_any_control = new System.Windows.Forms.RadioButton();
            this.radioButton_tcp_or_any_control = new System.Windows.Forms.RadioButton();
            this.checkBox_tcp_control_options = new System.Windows.Forms.CheckBox();
            this.numericUpDown_tcp_window_max = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_tcp_window_min = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_tcp_data_offset_max = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_tcp_data_offset_min = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_tcp_ack_num = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_tcp_seq_num = new System.Windows.Forms.NumericUpDown();
            this.checkBox_tcp_window = new System.Windows.Forms.CheckBox();
            this.checkBox_tcp_data_offset = new System.Windows.Forms.CheckBox();
            this.checkBox_tcp_ack_num = new System.Windows.Forms.CheckBox();
            this.checkBox_tcp_seq_num = new System.Windows.Forms.CheckBox();
            this.textBox_tcp_port_source = new System.Windows.Forms.TextBox();
            this.checkBox_tcp_port_source = new System.Windows.Forms.CheckBox();
            this.checkBox_tcp_port_dest = new System.Windows.Forms.CheckBox();
            this.textBox_tcp_ip_source = new System.Windows.Forms.TextBox();
            this.checkBox_tcp_ip_source = new System.Windows.Forms.CheckBox();
            this.checkBox_tcp_ip_dest = new System.Windows.Forms.CheckBox();
            this.tabPage_udp = new System.Windows.Forms.TabPage();
            this.panel_udp_ipport_source_and_or_dest = new System.Windows.Forms.Panel();
            this.radioButton_udp_ipport_source_and_dest = new System.Windows.Forms.RadioButton();
            this.radioButton_udp_ipport_source_or_dest = new System.Windows.Forms.RadioButton();
            this.numericUpDown_udp_length_max = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_udp_length_min = new System.Windows.Forms.NumericUpDown();
            this.textBox_udp_port_source = new System.Windows.Forms.TextBox();
            this.textBox_udp_port_dest = new System.Windows.Forms.TextBox();
            this.checkBox_udp_port_source = new System.Windows.Forms.CheckBox();
            this.checkBox_udp_port_dest = new System.Windows.Forms.CheckBox();
            this.textBox_udp_ip_source = new System.Windows.Forms.TextBox();
            this.textBox_udp_ip_dest = new System.Windows.Forms.TextBox();
            this.checkBox_udp_ip_source = new System.Windows.Forms.CheckBox();
            this.checkBox_udp_ip_dest = new System.Windows.Forms.CheckBox();
            this.checkBox_udp_length = new System.Windows.Forms.CheckBox();
            this.tabPage_icmp = new System.Windows.Forms.TabPage();
            this.panel_icmp_ip_source_and_or_dest = new System.Windows.Forms.Panel();
            this.radioButton_icmp_ip_source_and_ip_dest = new System.Windows.Forms.RadioButton();
            this.radioButton_icmp_ip_source_or_ip_dest = new System.Windows.Forms.RadioButton();
            this.checkBox_icmp_timestamp_reply = new System.Windows.Forms.CheckBox();
            this.checkBox_icmp_timestamp = new System.Windows.Forms.CheckBox();
            this.checkBox_icmp_time_exceeded_message = new System.Windows.Forms.CheckBox();
            this.checkBox_icmp_source_quench = new System.Windows.Forms.CheckBox();
            this.checkBox_icmp_destination_unreachable = new System.Windows.Forms.CheckBox();
            this.checkBox_icmp_redirect = new System.Windows.Forms.CheckBox();
            this.checkBox_icmp_parameter_problem = new System.Windows.Forms.CheckBox();
            this.checkBox_icmp_information_reply = new System.Windows.Forms.CheckBox();
            this.checkBox_icmp_information_request = new System.Windows.Forms.CheckBox();
            this.checkBox_icmp_echo_reply = new System.Windows.Forms.CheckBox();
            this.checkBox_icmp_echo = new System.Windows.Forms.CheckBox();
            this.textBox_icmp_ip_source = new System.Windows.Forms.TextBox();
            this.textBox_icmp_ip_dest = new System.Windows.Forms.TextBox();
            this.checkBox_icmp_ip_source = new System.Windows.Forms.CheckBox();
            this.checkBox_icmp_ip_dest = new System.Windows.Forms.CheckBox();
            this.tabPage_other = new System.Windows.Forms.TabPage();
            this.panel_other_ip_source_and_or_dest = new System.Windows.Forms.Panel();
            this.radioButton_other_ip_source_and_ip_dest = new System.Windows.Forms.RadioButton();
            this.radioButton_other_ip_source_or_ip_dest = new System.Windows.Forms.RadioButton();
            this.textBox_other_protocol = new System.Windows.Forms.TextBox();
            this.checkBox_other_protocol = new System.Windows.Forms.CheckBox();
            this.textBox_other_ip_source = new System.Windows.Forms.TextBox();
            this.textBox_other_ip_dest = new System.Windows.Forms.TextBox();
            this.checkBox_other_ip_source = new System.Windows.Forms.CheckBox();
            this.checkBox_other_ip_dest = new System.Windows.Forms.CheckBox();
            this.tabPage_ip = new System.Windows.Forms.TabPage();
            this.panel_ip_ip_source_and_or_dest = new System.Windows.Forms.Panel();
            this.radioButton_ip_ip_source_and_ip_dest = new System.Windows.Forms.RadioButton();
            this.radioButton_ip_ip_source_or_ip_dest = new System.Windows.Forms.RadioButton();
            this.textBox_ip_ipsource = new System.Windows.Forms.TextBox();
            this.textBox_ip_ipdest = new System.Windows.Forms.TextBox();
            this.checkBox_ip_ipsource = new System.Windows.Forms.CheckBox();
            this.checkBox_ip_ipdest = new System.Windows.Forms.CheckBox();
            this.checkBox_ip_Fragment_position = new System.Windows.Forms.CheckBox();
            this.checkBox_ip_Fragment_type = new System.Windows.Forms.CheckBox();
            this.checkBox_ip_Identification = new System.Windows.Forms.CheckBox();
            this.checkBox_ip_Relibility = new System.Windows.Forms.CheckBox();
            this.checkBox_ip_Throughput = new System.Windows.Forms.CheckBox();
            this.checkBox_ip_Delay = new System.Windows.Forms.CheckBox();
            this.checkBox_ip_Precedence = new System.Windows.Forms.CheckBox();
            this.numericUpDown_ip_identification = new System.Windows.Forms.NumericUpDown();
            this.comboBox_ip_flags_fragment_pos = new System.Windows.Forms.ComboBox();
            this.comboBox_ip_flags_fragment_type = new System.Windows.Forms.ComboBox();
            this.comboBox_ip_tos_relibility = new System.Windows.Forms.ComboBox();
            this.comboBox_ip_tos_throughtput = new System.Windows.Forms.ComboBox();
            this.comboBox_ip_tos_delay = new System.Windows.Forms.ComboBox();
            this.comboBox_ip_tos_precedence = new System.Windows.Forms.ComboBox();
            this.radioButton_ip_filter_and = new System.Windows.Forms.RadioButton();
            this.radioButton_ip_filter_or = new System.Windows.Forms.RadioButton();
            this.tabPage_app_name = new System.Windows.Forms.TabPage();
            this.button_app_name_remove = new System.Windows.Forms.Button();
            this.button_app_name_add = new System.Windows.Forms.Button();
            this.listBox_app_name = new System.Windows.Forms.ListBox();
            this.checkBox_app_name_get_app_name = new System.Windows.Forms.CheckBox();
            this.checkBox_app_name_get_all_udp_clt = new System.Windows.Forms.CheckBox();
            this.checkBox_app_name_filter_app_name = new System.Windows.Forms.CheckBox();
            this.checkBox_other = new System.Windows.Forms.CheckBox();
            this.button_start = new System.Windows.Forms.Button();
            this.button_stop = new System.Windows.Forms.Button();
            this.checkBox_packets_details = new System.Windows.Forms.CheckBox();
            this.checkBox_IP = new System.Windows.Forms.CheckBox();
            this.button_clear_list_view = new System.Windows.Forms.Button();
            this.contextMenu_list_view = new System.Windows.Forms.ContextMenu();
            this.menuItem_clear = new System.Windows.Forms.MenuItem();
            this.menuItem_copy_selected = new System.Windows.Forms.MenuItem();
            this.menuItem_save_selected = new System.Windows.Forms.MenuItem();
            this.menuItem_save_all = new System.Windows.Forms.MenuItem();
            this.menuItem_protocol_data = new System.Windows.Forms.MenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.checkBox_all_packets = new System.Windows.Forms.CheckBox();
            this.userControlPacketsView = new Network_Stuff.UserControlPacketsView();
            this.button_load_capture = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControl_proto_capture.SuspendLayout();
            this.tabPage_tcp.SuspendLayout();
            this.panel_tcp_ipport_source_and_or_dest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_window_max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_window_min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_data_offset_max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_data_offset_min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_ack_num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_seq_num)).BeginInit();
            this.tabPage_udp.SuspendLayout();
            this.panel_udp_ipport_source_and_or_dest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_udp_length_max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_udp_length_min)).BeginInit();
            this.tabPage_icmp.SuspendLayout();
            this.panel_icmp_ip_source_and_or_dest.SuspendLayout();
            this.tabPage_other.SuspendLayout();
            this.panel_other_ip_source_and_or_dest.SuspendLayout();
            this.tabPage_ip.SuspendLayout();
            this.panel_ip_ip_source_and_or_dest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_identification)).BeginInit();
            this.tabPage_app_name.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBox_icmp
            // 
            this.checkBox_icmp.Location = new System.Drawing.Point(120, 32);
            this.checkBox_icmp.Name = "checkBox_icmp";
            this.checkBox_icmp.Size = new System.Drawing.Size(56, 16);
            this.checkBox_icmp.TabIndex = 14;
            this.checkBox_icmp.Text = "Icmp";
            // 
            // checkBox_udp
            // 
            this.checkBox_udp.Location = new System.Drawing.Point(64, 32);
            this.checkBox_udp.Name = "checkBox_udp";
            this.checkBox_udp.Size = new System.Drawing.Size(56, 16);
            this.checkBox_udp.TabIndex = 13;
            this.checkBox_udp.Text = "Udp";
            // 
            // checkBox_tcp
            // 
            this.checkBox_tcp.Location = new System.Drawing.Point(8, 32);
            this.checkBox_tcp.Name = "checkBox_tcp";
            this.checkBox_tcp.Size = new System.Drawing.Size(56, 16);
            this.checkBox_tcp.TabIndex = 12;
            this.checkBox_tcp.Text = "Tcp";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 19;
            this.label2.Text = "Filters rules";
            // 
            // tabControl_proto_capture
            // 
            this.tabControl_proto_capture.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                                   this.tabPage_tcp,
                                                                                                   this.tabPage_udp,
                                                                                                   this.tabPage_icmp,
                                                                                                   this.tabPage_other,
                                                                                                   this.tabPage_ip,
                                                                                                   this.tabPage_app_name});
            this.tabControl_proto_capture.Location = new System.Drawing.Point(184, 0);
            this.tabControl_proto_capture.Name = "tabControl_proto_capture";
            this.tabControl_proto_capture.SelectedIndex = 0;
            this.tabControl_proto_capture.Size = new System.Drawing.Size(640, 168);
            this.tabControl_proto_capture.TabIndex = 17;
            // 
            // tabPage_tcp
            // 
            this.tabPage_tcp.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                      this.textBox_tcp_port_dest,
                                                                                      this.textBox_tcp_ip_dest,
                                                                                      this.panel_tcp_ipport_source_and_or_dest,
                                                                                      this.checkBox_tcp_fin,
                                                                                      this.checkBox_tcp_syn,
                                                                                      this.checkBox_tcp_rst,
                                                                                      this.checkBox_tcp_push,
                                                                                      this.checkBox_tcp_ack,
                                                                                      this.checkBox_tcp_urg,
                                                                                      this.radioButton_tcp_and_all_control,
                                                                                      this.radioButton_tcp_or_all_control,
                                                                                      this.label3,
                                                                                      this.radioButton_tcp_and_any_control,
                                                                                      this.radioButton_tcp_or_any_control,
                                                                                      this.checkBox_tcp_control_options,
                                                                                      this.numericUpDown_tcp_window_max,
                                                                                      this.numericUpDown_tcp_window_min,
                                                                                      this.numericUpDown_tcp_data_offset_max,
                                                                                      this.numericUpDown_tcp_data_offset_min,
                                                                                      this.numericUpDown_tcp_ack_num,
                                                                                      this.numericUpDown_tcp_seq_num,
                                                                                      this.checkBox_tcp_window,
                                                                                      this.checkBox_tcp_data_offset,
                                                                                      this.checkBox_tcp_ack_num,
                                                                                      this.checkBox_tcp_seq_num,
                                                                                      this.textBox_tcp_port_source,
                                                                                      this.checkBox_tcp_port_source,
                                                                                      this.checkBox_tcp_port_dest,
                                                                                      this.textBox_tcp_ip_source,
                                                                                      this.checkBox_tcp_ip_source,
                                                                                      this.checkBox_tcp_ip_dest});
            this.tabPage_tcp.Location = new System.Drawing.Point(4, 22);
            this.tabPage_tcp.Name = "tabPage_tcp";
            this.tabPage_tcp.Size = new System.Drawing.Size(632, 142);
            this.tabPage_tcp.TabIndex = 0;
            this.tabPage_tcp.Text = "Tcp";
            // 
            // textBox_tcp_port_dest
            // 
            this.textBox_tcp_port_dest.Location = new System.Drawing.Point(80, 16);
            this.textBox_tcp_port_dest.Name = "textBox_tcp_port_dest";
            this.textBox_tcp_port_dest.TabIndex = 14;
            this.textBox_tcp_port_dest.Text = "";
            // 
            // textBox_tcp_ip_dest
            // 
            this.textBox_tcp_ip_dest.Location = new System.Drawing.Point(80, 0);
            this.textBox_tcp_ip_dest.Name = "textBox_tcp_ip_dest";
            this.textBox_tcp_ip_dest.TabIndex = 10;
            this.textBox_tcp_ip_dest.Text = "";
            // 
            // panel_tcp_ipport_source_and_or_dest
            // 
            this.panel_tcp_ipport_source_and_or_dest.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                                              this.radioButton_tcp_ipport_source_and_dest,
                                                                                                              this.radioButton_tcp_ipport_source_or_dest});
            this.panel_tcp_ipport_source_and_or_dest.Location = new System.Drawing.Point(384, 0);
            this.panel_tcp_ipport_source_and_or_dest.Name = "panel_tcp_ipport_source_and_or_dest";
            this.panel_tcp_ipport_source_and_or_dest.Size = new System.Drawing.Size(224, 32);
            this.panel_tcp_ipport_source_and_or_dest.TabIndex = 62;
            // 
            // radioButton_tcp_ipport_source_and_dest
            // 
            this.radioButton_tcp_ipport_source_and_dest.Checked = true;
            this.radioButton_tcp_ipport_source_and_dest.Location = new System.Drawing.Point(0, 16);
            this.radioButton_tcp_ipport_source_and_dest.Name = "radioButton_tcp_ipport_source_and_dest";
            this.radioButton_tcp_ipport_source_and_dest.Size = new System.Drawing.Size(232, 16);
            this.radioButton_tcp_ipport_source_and_dest.TabIndex = 1;
            this.radioButton_tcp_ipport_source_and_dest.TabStop = true;
            this.radioButton_tcp_ipport_source_and_dest.Text = "(Ip and port source) and (Ip and port dest)";
            // 
            // radioButton_tcp_ipport_source_or_dest
            // 
            this.radioButton_tcp_ipport_source_or_dest.Name = "radioButton_tcp_ipport_source_or_dest";
            this.radioButton_tcp_ipport_source_or_dest.Size = new System.Drawing.Size(224, 16);
            this.radioButton_tcp_ipport_source_or_dest.TabIndex = 0;
            this.radioButton_tcp_ipport_source_or_dest.Text = "(Ip and port source) or (Ip and port dest) ";
            // 
            // checkBox_tcp_fin
            // 
            this.checkBox_tcp_fin.Location = new System.Drawing.Point(432, 88);
            this.checkBox_tcp_fin.Name = "checkBox_tcp_fin";
            this.checkBox_tcp_fin.Size = new System.Drawing.Size(64, 16);
            this.checkBox_tcp_fin.TabIndex = 61;
            this.checkBox_tcp_fin.Text = "FIN";
            // 
            // checkBox_tcp_syn
            // 
            this.checkBox_tcp_syn.Location = new System.Drawing.Point(368, 88);
            this.checkBox_tcp_syn.Name = "checkBox_tcp_syn";
            this.checkBox_tcp_syn.Size = new System.Drawing.Size(72, 16);
            this.checkBox_tcp_syn.TabIndex = 60;
            this.checkBox_tcp_syn.Text = "SYN";
            // 
            // checkBox_tcp_rst
            // 
            this.checkBox_tcp_rst.Location = new System.Drawing.Point(312, 88);
            this.checkBox_tcp_rst.Name = "checkBox_tcp_rst";
            this.checkBox_tcp_rst.Size = new System.Drawing.Size(56, 16);
            this.checkBox_tcp_rst.TabIndex = 59;
            this.checkBox_tcp_rst.Text = "RST";
            // 
            // checkBox_tcp_push
            // 
            this.checkBox_tcp_push.Location = new System.Drawing.Point(248, 88);
            this.checkBox_tcp_push.Name = "checkBox_tcp_push";
            this.checkBox_tcp_push.Size = new System.Drawing.Size(64, 16);
            this.checkBox_tcp_push.TabIndex = 58;
            this.checkBox_tcp_push.Text = "PSH";
            // 
            // checkBox_tcp_ack
            // 
            this.checkBox_tcp_ack.Location = new System.Drawing.Point(176, 88);
            this.checkBox_tcp_ack.Name = "checkBox_tcp_ack";
            this.checkBox_tcp_ack.Size = new System.Drawing.Size(72, 16);
            this.checkBox_tcp_ack.TabIndex = 57;
            this.checkBox_tcp_ack.Text = "ACK";
            // 
            // checkBox_tcp_urg
            // 
            this.checkBox_tcp_urg.Location = new System.Drawing.Point(112, 88);
            this.checkBox_tcp_urg.Name = "checkBox_tcp_urg";
            this.checkBox_tcp_urg.Size = new System.Drawing.Size(64, 16);
            this.checkBox_tcp_urg.TabIndex = 56;
            this.checkBox_tcp_urg.Text = "URG";
            // 
            // radioButton_tcp_and_all_control
            // 
            this.radioButton_tcp_and_all_control.Location = new System.Drawing.Point(376, 120);
            this.radioButton_tcp_and_all_control.Name = "radioButton_tcp_and_all_control";
            this.radioButton_tcp_and_all_control.Size = new System.Drawing.Size(152, 16);
            this.radioButton_tcp_and_all_control.TabIndex = 55;
            this.radioButton_tcp_and_all_control.Text = "AND all control options";
            // 
            // radioButton_tcp_or_all_control
            // 
            this.radioButton_tcp_or_all_control.Location = new System.Drawing.Point(376, 104);
            this.radioButton_tcp_or_all_control.Name = "radioButton_tcp_or_all_control";
            this.radioButton_tcp_or_all_control.Size = new System.Drawing.Size(152, 16);
            this.radioButton_tcp_or_all_control.TabIndex = 54;
            this.radioButton_tcp_or_all_control.Text = "OR all control options";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(40, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 16);
            this.label3.TabIndex = 53;
            this.label3.Text = "Packets must agree with options ";
            // 
            // radioButton_tcp_and_any_control
            // 
            this.radioButton_tcp_and_any_control.Location = new System.Drawing.Point(208, 120);
            this.radioButton_tcp_and_any_control.Name = "radioButton_tcp_and_any_control";
            this.radioButton_tcp_and_any_control.Size = new System.Drawing.Size(160, 16);
            this.radioButton_tcp_and_any_control.TabIndex = 46;
            this.radioButton_tcp_and_any_control.Text = "AND any control options";
            // 
            // radioButton_tcp_or_any_control
            // 
            this.radioButton_tcp_or_any_control.Checked = true;
            this.radioButton_tcp_or_any_control.Location = new System.Drawing.Point(208, 104);
            this.radioButton_tcp_or_any_control.Name = "radioButton_tcp_or_any_control";
            this.radioButton_tcp_or_any_control.Size = new System.Drawing.Size(152, 16);
            this.radioButton_tcp_or_any_control.TabIndex = 45;
            this.radioButton_tcp_or_any_control.TabStop = true;
            this.radioButton_tcp_or_any_control.Text = "OR any control options";
            // 
            // checkBox_tcp_control_options
            // 
            this.checkBox_tcp_control_options.Location = new System.Drawing.Point(8, 88);
            this.checkBox_tcp_control_options.Name = "checkBox_tcp_control_options";
            this.checkBox_tcp_control_options.Size = new System.Drawing.Size(112, 16);
            this.checkBox_tcp_control_options.TabIndex = 44;
            this.checkBox_tcp_control_options.Text = "Control options";
            // 
            // numericUpDown_tcp_window_max
            // 
            this.numericUpDown_tcp_window_max.Location = new System.Drawing.Point(488, 64);
            this.numericUpDown_tcp_window_max.Maximum = new System.Decimal(new int[] {
                                                                                         65535,
                                                                                         0,
                                                                                         0,
                                                                                         0});
            this.numericUpDown_tcp_window_max.Name = "numericUpDown_tcp_window_max";
            this.numericUpDown_tcp_window_max.Size = new System.Drawing.Size(64, 20);
            this.numericUpDown_tcp_window_max.TabIndex = 40;
            // 
            // numericUpDown_tcp_window_min
            // 
            this.numericUpDown_tcp_window_min.Location = new System.Drawing.Point(392, 64);
            this.numericUpDown_tcp_window_min.Maximum = new System.Decimal(new int[] {
                                                                                         65535,
                                                                                         0,
                                                                                         0,
                                                                                         0});
            this.numericUpDown_tcp_window_min.Name = "numericUpDown_tcp_window_min";
            this.numericUpDown_tcp_window_min.Size = new System.Drawing.Size(56, 20);
            this.numericUpDown_tcp_window_min.TabIndex = 39;
            // 
            // numericUpDown_tcp_data_offset_max
            // 
            this.numericUpDown_tcp_data_offset_max.Location = new System.Drawing.Point(200, 64);
            this.numericUpDown_tcp_data_offset_max.Maximum = new System.Decimal(new int[] {
                                                                                              15,
                                                                                              0,
                                                                                              0,
                                                                                              0});
            this.numericUpDown_tcp_data_offset_max.Name = "numericUpDown_tcp_data_offset_max";
            this.numericUpDown_tcp_data_offset_max.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_tcp_data_offset_max.TabIndex = 38;
            // 
            // numericUpDown_tcp_data_offset_min
            // 
            this.numericUpDown_tcp_data_offset_min.Location = new System.Drawing.Point(104, 64);
            this.numericUpDown_tcp_data_offset_min.Maximum = new System.Decimal(new int[] {
                                                                                              15,
                                                                                              0,
                                                                                              0,
                                                                                              0});
            this.numericUpDown_tcp_data_offset_min.Name = "numericUpDown_tcp_data_offset_min";
            this.numericUpDown_tcp_data_offset_min.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_tcp_data_offset_min.TabIndex = 37;
            // 
            // numericUpDown_tcp_ack_num
            // 
            this.numericUpDown_tcp_ack_num.Location = new System.Drawing.Point(448, 40);
            this.numericUpDown_tcp_ack_num.Maximum = new System.Decimal(new int[] {
                                                                                      -1,
                                                                                      0,
                                                                                      0,
                                                                                      0});
            this.numericUpDown_tcp_ack_num.Name = "numericUpDown_tcp_ack_num";
            this.numericUpDown_tcp_ack_num.TabIndex = 36;
            // 
            // numericUpDown_tcp_seq_num
            // 
            this.numericUpDown_tcp_seq_num.Location = new System.Drawing.Point(144, 40);
            this.numericUpDown_tcp_seq_num.Maximum = new System.Decimal(new int[] {
                                                                                      -1,
                                                                                      0,
                                                                                      0,
                                                                                      0});
            this.numericUpDown_tcp_seq_num.Name = "numericUpDown_tcp_seq_num";
            this.numericUpDown_tcp_seq_num.TabIndex = 35;
            // 
            // checkBox_tcp_window
            // 
            this.checkBox_tcp_window.Location = new System.Drawing.Point(304, 64);
            this.checkBox_tcp_window.Name = "checkBox_tcp_window";
            this.checkBox_tcp_window.Size = new System.Drawing.Size(192, 16);
            this.checkBox_tcp_window.TabIndex = 34;
            this.checkBox_tcp_window.Text = "Window  Min                         Max";
            // 
            // checkBox_tcp_data_offset
            // 
            this.checkBox_tcp_data_offset.Location = new System.Drawing.Point(8, 64);
            this.checkBox_tcp_data_offset.Name = "checkBox_tcp_data_offset";
            this.checkBox_tcp_data_offset.Size = new System.Drawing.Size(200, 16);
            this.checkBox_tcp_data_offset.TabIndex = 33;
            this.checkBox_tcp_data_offset.Text = "Data Offset Min                        Max";
            // 
            // checkBox_tcp_ack_num
            // 
            this.checkBox_tcp_ack_num.Location = new System.Drawing.Point(304, 40);
            this.checkBox_tcp_ack_num.Name = "checkBox_tcp_ack_num";
            this.checkBox_tcp_ack_num.Size = new System.Drawing.Size(168, 16);
            this.checkBox_tcp_ack_num.TabIndex = 32;
            this.checkBox_tcp_ack_num.Text = "Acknowledgment Number";
            // 
            // checkBox_tcp_seq_num
            // 
            this.checkBox_tcp_seq_num.Location = new System.Drawing.Point(8, 40);
            this.checkBox_tcp_seq_num.Name = "checkBox_tcp_seq_num";
            this.checkBox_tcp_seq_num.Size = new System.Drawing.Size(152, 16);
            this.checkBox_tcp_seq_num.TabIndex = 31;
            this.checkBox_tcp_seq_num.Text = "Sequence Number";
            // 
            // textBox_tcp_port_source
            // 
            this.textBox_tcp_port_source.Location = new System.Drawing.Point(280, 16);
            this.textBox_tcp_port_source.Name = "textBox_tcp_port_source";
            this.textBox_tcp_port_source.TabIndex = 15;
            this.textBox_tcp_port_source.Text = "";
            // 
            // checkBox_tcp_port_source
            // 
            this.checkBox_tcp_port_source.Location = new System.Drawing.Point(192, 24);
            this.checkBox_tcp_port_source.Name = "checkBox_tcp_port_source";
            this.checkBox_tcp_port_source.Size = new System.Drawing.Size(104, 16);
            this.checkBox_tcp_port_source.TabIndex = 13;
            this.checkBox_tcp_port_source.Text = "Port source";
            // 
            // checkBox_tcp_port_dest
            // 
            this.checkBox_tcp_port_dest.Location = new System.Drawing.Point(8, 24);
            this.checkBox_tcp_port_dest.Name = "checkBox_tcp_port_dest";
            this.checkBox_tcp_port_dest.Size = new System.Drawing.Size(104, 16);
            this.checkBox_tcp_port_dest.TabIndex = 12;
            this.checkBox_tcp_port_dest.Text = "Port dest";
            // 
            // textBox_tcp_ip_source
            // 
            this.textBox_tcp_ip_source.Location = new System.Drawing.Point(280, 0);
            this.textBox_tcp_ip_source.Name = "textBox_tcp_ip_source";
            this.textBox_tcp_ip_source.TabIndex = 11;
            this.textBox_tcp_ip_source.Text = "";
            // 
            // checkBox_tcp_ip_source
            // 
            this.checkBox_tcp_ip_source.Location = new System.Drawing.Point(192, 0);
            this.checkBox_tcp_ip_source.Name = "checkBox_tcp_ip_source";
            this.checkBox_tcp_ip_source.Size = new System.Drawing.Size(104, 16);
            this.checkBox_tcp_ip_source.TabIndex = 9;
            this.checkBox_tcp_ip_source.Text = "Ip Source";
            // 
            // checkBox_tcp_ip_dest
            // 
            this.checkBox_tcp_ip_dest.Location = new System.Drawing.Point(8, 0);
            this.checkBox_tcp_ip_dest.Name = "checkBox_tcp_ip_dest";
            this.checkBox_tcp_ip_dest.Size = new System.Drawing.Size(104, 16);
            this.checkBox_tcp_ip_dest.TabIndex = 8;
            this.checkBox_tcp_ip_dest.Text = "Ip dest";
            // 
            // tabPage_udp
            // 
            this.tabPage_udp.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                      this.panel_udp_ipport_source_and_or_dest,
                                                                                      this.numericUpDown_udp_length_max,
                                                                                      this.numericUpDown_udp_length_min,
                                                                                      this.textBox_udp_port_source,
                                                                                      this.textBox_udp_port_dest,
                                                                                      this.checkBox_udp_port_source,
                                                                                      this.checkBox_udp_port_dest,
                                                                                      this.textBox_udp_ip_source,
                                                                                      this.textBox_udp_ip_dest,
                                                                                      this.checkBox_udp_ip_source,
                                                                                      this.checkBox_udp_ip_dest,
                                                                                      this.checkBox_udp_length});
            this.tabPage_udp.Location = new System.Drawing.Point(4, 22);
            this.tabPage_udp.Name = "tabPage_udp";
            this.tabPage_udp.Size = new System.Drawing.Size(632, 142);
            this.tabPage_udp.TabIndex = 1;
            this.tabPage_udp.Text = "Udp";
            this.tabPage_udp.Visible = false;
            // 
            // panel_udp_ipport_source_and_or_dest
            // 
            this.panel_udp_ipport_source_and_or_dest.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                                              this.radioButton_udp_ipport_source_and_dest,
                                                                                                              this.radioButton_udp_ipport_source_or_dest});
            this.panel_udp_ipport_source_and_or_dest.Location = new System.Drawing.Point(8, 56);
            this.panel_udp_ipport_source_and_or_dest.Name = "panel_udp_ipport_source_and_or_dest";
            this.panel_udp_ipport_source_and_or_dest.Size = new System.Drawing.Size(248, 32);
            this.panel_udp_ipport_source_and_or_dest.TabIndex = 63;
            // 
            // radioButton_udp_ipport_source_and_dest
            // 
            this.radioButton_udp_ipport_source_and_dest.Location = new System.Drawing.Point(0, 16);
            this.radioButton_udp_ipport_source_and_dest.Name = "radioButton_udp_ipport_source_and_dest";
            this.radioButton_udp_ipport_source_and_dest.Size = new System.Drawing.Size(232, 16);
            this.radioButton_udp_ipport_source_and_dest.TabIndex = 1;
            this.radioButton_udp_ipport_source_and_dest.Text = "(Ip and port source) and (Ip and port dest)";
            // 
            // radioButton_udp_ipport_source_or_dest
            // 
            this.radioButton_udp_ipport_source_or_dest.Checked = true;
            this.radioButton_udp_ipport_source_or_dest.Name = "radioButton_udp_ipport_source_or_dest";
            this.radioButton_udp_ipport_source_or_dest.Size = new System.Drawing.Size(224, 16);
            this.radioButton_udp_ipport_source_or_dest.TabIndex = 0;
            this.radioButton_udp_ipport_source_or_dest.TabStop = true;
            this.radioButton_udp_ipport_source_or_dest.Text = "(Ip and port source) or (Ip and port dest) ";
            // 
            // numericUpDown_udp_length_max
            // 
            this.numericUpDown_udp_length_max.Location = new System.Drawing.Point(256, 104);
            this.numericUpDown_udp_length_max.Maximum = new System.Decimal(new int[] {
                                                                                         65535,
                                                                                         0,
                                                                                         0,
                                                                                         0});
            this.numericUpDown_udp_length_max.Name = "numericUpDown_udp_length_max";
            this.numericUpDown_udp_length_max.Size = new System.Drawing.Size(56, 20);
            this.numericUpDown_udp_length_max.TabIndex = 18;
            this.numericUpDown_udp_length_max.Value = new System.Decimal(new int[] {
                                                                                       65535,
                                                                                       0,
                                                                                       0,
                                                                                       0});
            // 
            // numericUpDown_udp_length_min
            // 
            this.numericUpDown_udp_length_min.Location = new System.Drawing.Point(120, 104);
            this.numericUpDown_udp_length_min.Maximum = new System.Decimal(new int[] {
                                                                                         65535,
                                                                                         0,
                                                                                         0,
                                                                                         0});
            this.numericUpDown_udp_length_min.Name = "numericUpDown_udp_length_min";
            this.numericUpDown_udp_length_min.Size = new System.Drawing.Size(56, 20);
            this.numericUpDown_udp_length_min.TabIndex = 17;
            // 
            // textBox_udp_port_source
            // 
            this.textBox_udp_port_source.Location = new System.Drawing.Point(360, 32);
            this.textBox_udp_port_source.Name = "textBox_udp_port_source";
            this.textBox_udp_port_source.TabIndex = 15;
            this.textBox_udp_port_source.Text = "";
            // 
            // textBox_udp_port_dest
            // 
            this.textBox_udp_port_dest.Location = new System.Drawing.Point(120, 32);
            this.textBox_udp_port_dest.Name = "textBox_udp_port_dest";
            this.textBox_udp_port_dest.TabIndex = 14;
            this.textBox_udp_port_dest.Text = "";
            // 
            // checkBox_udp_port_source
            // 
            this.checkBox_udp_port_source.Location = new System.Drawing.Point(256, 32);
            this.checkBox_udp_port_source.Name = "checkBox_udp_port_source";
            this.checkBox_udp_port_source.TabIndex = 13;
            this.checkBox_udp_port_source.Text = "Port source";
            // 
            // checkBox_udp_port_dest
            // 
            this.checkBox_udp_port_dest.Location = new System.Drawing.Point(8, 32);
            this.checkBox_udp_port_dest.Name = "checkBox_udp_port_dest";
            this.checkBox_udp_port_dest.TabIndex = 12;
            this.checkBox_udp_port_dest.Text = "Port dest";
            // 
            // textBox_udp_ip_source
            // 
            this.textBox_udp_ip_source.Location = new System.Drawing.Point(360, 8);
            this.textBox_udp_ip_source.Name = "textBox_udp_ip_source";
            this.textBox_udp_ip_source.TabIndex = 11;
            this.textBox_udp_ip_source.Text = "";
            // 
            // textBox_udp_ip_dest
            // 
            this.textBox_udp_ip_dest.Location = new System.Drawing.Point(120, 8);
            this.textBox_udp_ip_dest.Name = "textBox_udp_ip_dest";
            this.textBox_udp_ip_dest.TabIndex = 10;
            this.textBox_udp_ip_dest.Text = "";
            // 
            // checkBox_udp_ip_source
            // 
            this.checkBox_udp_ip_source.Location = new System.Drawing.Point(256, 8);
            this.checkBox_udp_ip_source.Name = "checkBox_udp_ip_source";
            this.checkBox_udp_ip_source.TabIndex = 9;
            this.checkBox_udp_ip_source.Text = "Ip Source";
            // 
            // checkBox_udp_ip_dest
            // 
            this.checkBox_udp_ip_dest.Location = new System.Drawing.Point(8, 8);
            this.checkBox_udp_ip_dest.Name = "checkBox_udp_ip_dest";
            this.checkBox_udp_ip_dest.TabIndex = 8;
            this.checkBox_udp_ip_dest.Text = "Ip dest";
            // 
            // checkBox_udp_length
            // 
            this.checkBox_udp_length.Location = new System.Drawing.Point(8, 104);
            this.checkBox_udp_length.Name = "checkBox_udp_length";
            this.checkBox_udp_length.Size = new System.Drawing.Size(264, 24);
            this.checkBox_udp_length.TabIndex = 16;
            this.checkBox_udp_length.Text = "Length           Min                                       Max";
            // 
            // tabPage_icmp
            // 
            this.tabPage_icmp.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                       this.panel_icmp_ip_source_and_or_dest,
                                                                                       this.checkBox_icmp_timestamp_reply,
                                                                                       this.checkBox_icmp_timestamp,
                                                                                       this.checkBox_icmp_time_exceeded_message,
                                                                                       this.checkBox_icmp_source_quench,
                                                                                       this.checkBox_icmp_destination_unreachable,
                                                                                       this.checkBox_icmp_redirect,
                                                                                       this.checkBox_icmp_parameter_problem,
                                                                                       this.checkBox_icmp_information_reply,
                                                                                       this.checkBox_icmp_information_request,
                                                                                       this.checkBox_icmp_echo_reply,
                                                                                       this.checkBox_icmp_echo,
                                                                                       this.textBox_icmp_ip_source,
                                                                                       this.textBox_icmp_ip_dest,
                                                                                       this.checkBox_icmp_ip_source,
                                                                                       this.checkBox_icmp_ip_dest});
            this.tabPage_icmp.Location = new System.Drawing.Point(4, 22);
            this.tabPage_icmp.Name = "tabPage_icmp";
            this.tabPage_icmp.Size = new System.Drawing.Size(632, 142);
            this.tabPage_icmp.TabIndex = 2;
            this.tabPage_icmp.Text = "Icmp";
            this.tabPage_icmp.Visible = false;
            // 
            // panel_icmp_ip_source_and_or_dest
            // 
            this.panel_icmp_ip_source_and_or_dest.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                                           this.radioButton_icmp_ip_source_and_ip_dest,
                                                                                                           this.radioButton_icmp_ip_source_or_ip_dest});
            this.panel_icmp_ip_source_and_or_dest.Location = new System.Drawing.Point(448, 8);
            this.panel_icmp_ip_source_and_or_dest.Name = "panel_icmp_ip_source_and_or_dest";
            this.panel_icmp_ip_source_and_or_dest.Size = new System.Drawing.Size(144, 32);
            this.panel_icmp_ip_source_and_or_dest.TabIndex = 68;
            // 
            // radioButton_icmp_ip_source_and_ip_dest
            // 
            this.radioButton_icmp_ip_source_and_ip_dest.Location = new System.Drawing.Point(0, 16);
            this.radioButton_icmp_ip_source_and_ip_dest.Name = "radioButton_icmp_ip_source_and_ip_dest";
            this.radioButton_icmp_ip_source_and_ip_dest.Size = new System.Drawing.Size(136, 16);
            this.radioButton_icmp_ip_source_and_ip_dest.TabIndex = 1;
            this.radioButton_icmp_ip_source_and_ip_dest.Text = "Ip source and Ip dest";
            // 
            // radioButton_icmp_ip_source_or_ip_dest
            // 
            this.radioButton_icmp_ip_source_or_ip_dest.Checked = true;
            this.radioButton_icmp_ip_source_or_ip_dest.Name = "radioButton_icmp_ip_source_or_ip_dest";
            this.radioButton_icmp_ip_source_or_ip_dest.Size = new System.Drawing.Size(136, 16);
            this.radioButton_icmp_ip_source_or_ip_dest.TabIndex = 0;
            this.radioButton_icmp_ip_source_or_ip_dest.TabStop = true;
            this.radioButton_icmp_ip_source_or_ip_dest.Text = "Ip source or Ip dest";
            // 
            // checkBox_icmp_timestamp_reply
            // 
            this.checkBox_icmp_timestamp_reply.Checked = true;
            this.checkBox_icmp_timestamp_reply.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_icmp_timestamp_reply.Location = new System.Drawing.Point(144, 96);
            this.checkBox_icmp_timestamp_reply.Name = "checkBox_icmp_timestamp_reply";
            this.checkBox_icmp_timestamp_reply.Size = new System.Drawing.Size(120, 24);
            this.checkBox_icmp_timestamp_reply.TabIndex = 19;
            this.checkBox_icmp_timestamp_reply.Text = "Timestamp Reply";
            // 
            // checkBox_icmp_timestamp
            // 
            this.checkBox_icmp_timestamp.Checked = true;
            this.checkBox_icmp_timestamp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_icmp_timestamp.Location = new System.Drawing.Point(8, 96);
            this.checkBox_icmp_timestamp.Name = "checkBox_icmp_timestamp";
            this.checkBox_icmp_timestamp.TabIndex = 18;
            this.checkBox_icmp_timestamp.Text = "Timestamp";
            // 
            // checkBox_icmp_time_exceeded_message
            // 
            this.checkBox_icmp_time_exceeded_message.Checked = true;
            this.checkBox_icmp_time_exceeded_message.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_icmp_time_exceeded_message.Location = new System.Drawing.Point(352, 72);
            this.checkBox_icmp_time_exceeded_message.Name = "checkBox_icmp_time_exceeded_message";
            this.checkBox_icmp_time_exceeded_message.Size = new System.Drawing.Size(168, 24);
            this.checkBox_icmp_time_exceeded_message.TabIndex = 17;
            this.checkBox_icmp_time_exceeded_message.Text = "Time Exceeded Message";
            // 
            // checkBox_icmp_source_quench
            // 
            this.checkBox_icmp_source_quench.Checked = true;
            this.checkBox_icmp_source_quench.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_icmp_source_quench.Location = new System.Drawing.Point(240, 72);
            this.checkBox_icmp_source_quench.Name = "checkBox_icmp_source_quench";
            this.checkBox_icmp_source_quench.Size = new System.Drawing.Size(128, 24);
            this.checkBox_icmp_source_quench.TabIndex = 16;
            this.checkBox_icmp_source_quench.Text = "Source Quench";
            // 
            // checkBox_icmp_destination_unreachable
            // 
            this.checkBox_icmp_destination_unreachable.Checked = true;
            this.checkBox_icmp_destination_unreachable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_icmp_destination_unreachable.Location = new System.Drawing.Point(8, 48);
            this.checkBox_icmp_destination_unreachable.Name = "checkBox_icmp_destination_unreachable";
            this.checkBox_icmp_destination_unreachable.Size = new System.Drawing.Size(160, 24);
            this.checkBox_icmp_destination_unreachable.TabIndex = 15;
            this.checkBox_icmp_destination_unreachable.Text = "Destination Unreachable";
            // 
            // checkBox_icmp_redirect
            // 
            this.checkBox_icmp_redirect.Checked = true;
            this.checkBox_icmp_redirect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_icmp_redirect.Location = new System.Drawing.Point(144, 72);
            this.checkBox_icmp_redirect.Name = "checkBox_icmp_redirect";
            this.checkBox_icmp_redirect.Size = new System.Drawing.Size(96, 24);
            this.checkBox_icmp_redirect.TabIndex = 14;
            this.checkBox_icmp_redirect.Text = "Redirect";
            // 
            // checkBox_icmp_parameter_problem
            // 
            this.checkBox_icmp_parameter_problem.Checked = true;
            this.checkBox_icmp_parameter_problem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_icmp_parameter_problem.Location = new System.Drawing.Point(8, 72);
            this.checkBox_icmp_parameter_problem.Name = "checkBox_icmp_parameter_problem";
            this.checkBox_icmp_parameter_problem.Size = new System.Drawing.Size(144, 24);
            this.checkBox_icmp_parameter_problem.TabIndex = 13;
            this.checkBox_icmp_parameter_problem.Text = "Parameter Problem";
            // 
            // checkBox_icmp_information_reply
            // 
            this.checkBox_icmp_information_reply.Checked = true;
            this.checkBox_icmp_information_reply.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_icmp_information_reply.Location = new System.Drawing.Point(480, 48);
            this.checkBox_icmp_information_reply.Name = "checkBox_icmp_information_reply";
            this.checkBox_icmp_information_reply.Size = new System.Drawing.Size(120, 24);
            this.checkBox_icmp_information_reply.TabIndex = 12;
            this.checkBox_icmp_information_reply.Text = "Information Reply";
            // 
            // checkBox_icmp_information_request
            // 
            this.checkBox_icmp_information_request.Checked = true;
            this.checkBox_icmp_information_request.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_icmp_information_request.Location = new System.Drawing.Point(352, 48);
            this.checkBox_icmp_information_request.Name = "checkBox_icmp_information_request";
            this.checkBox_icmp_information_request.Size = new System.Drawing.Size(128, 24);
            this.checkBox_icmp_information_request.TabIndex = 11;
            this.checkBox_icmp_information_request.Text = "Information Request";
            // 
            // checkBox_icmp_echo_reply
            // 
            this.checkBox_icmp_echo_reply.Checked = true;
            this.checkBox_icmp_echo_reply.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_icmp_echo_reply.Location = new System.Drawing.Point(240, 48);
            this.checkBox_icmp_echo_reply.Name = "checkBox_icmp_echo_reply";
            this.checkBox_icmp_echo_reply.TabIndex = 10;
            this.checkBox_icmp_echo_reply.Text = "Echo Reply";
            // 
            // checkBox_icmp_echo
            // 
            this.checkBox_icmp_echo.Checked = true;
            this.checkBox_icmp_echo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_icmp_echo.Location = new System.Drawing.Point(168, 48);
            this.checkBox_icmp_echo.Name = "checkBox_icmp_echo";
            this.checkBox_icmp_echo.Size = new System.Drawing.Size(72, 24);
            this.checkBox_icmp_echo.TabIndex = 9;
            this.checkBox_icmp_echo.Text = "Echo";
            // 
            // textBox_icmp_ip_source
            // 
            this.textBox_icmp_ip_source.Location = new System.Drawing.Point(336, 8);
            this.textBox_icmp_ip_source.Name = "textBox_icmp_ip_source";
            this.textBox_icmp_ip_source.TabIndex = 8;
            this.textBox_icmp_ip_source.Text = "";
            // 
            // textBox_icmp_ip_dest
            // 
            this.textBox_icmp_ip_dest.Location = new System.Drawing.Point(120, 8);
            this.textBox_icmp_ip_dest.Name = "textBox_icmp_ip_dest";
            this.textBox_icmp_ip_dest.TabIndex = 7;
            this.textBox_icmp_ip_dest.Text = "";
            // 
            // checkBox_icmp_ip_source
            // 
            this.checkBox_icmp_ip_source.Location = new System.Drawing.Point(232, 8);
            this.checkBox_icmp_ip_source.Name = "checkBox_icmp_ip_source";
            this.checkBox_icmp_ip_source.TabIndex = 6;
            this.checkBox_icmp_ip_source.Text = "Ip Source";
            // 
            // checkBox_icmp_ip_dest
            // 
            this.checkBox_icmp_ip_dest.Location = new System.Drawing.Point(8, 8);
            this.checkBox_icmp_ip_dest.Name = "checkBox_icmp_ip_dest";
            this.checkBox_icmp_ip_dest.TabIndex = 5;
            this.checkBox_icmp_ip_dest.Text = "Ip dest";
            // 
            // tabPage_other
            // 
            this.tabPage_other.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                        this.panel_other_ip_source_and_or_dest,
                                                                                        this.textBox_other_protocol,
                                                                                        this.checkBox_other_protocol,
                                                                                        this.textBox_other_ip_source,
                                                                                        this.textBox_other_ip_dest,
                                                                                        this.checkBox_other_ip_source,
                                                                                        this.checkBox_other_ip_dest});
            this.tabPage_other.Location = new System.Drawing.Point(4, 22);
            this.tabPage_other.Name = "tabPage_other";
            this.tabPage_other.Size = new System.Drawing.Size(632, 142);
            this.tabPage_other.TabIndex = 3;
            this.tabPage_other.Text = "Other";
            this.tabPage_other.Visible = false;
            // 
            // panel_other_ip_source_and_or_dest
            // 
            this.panel_other_ip_source_and_or_dest.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                                            this.radioButton_other_ip_source_and_ip_dest,
                                                                                                            this.radioButton_other_ip_source_or_ip_dest});
            this.panel_other_ip_source_and_or_dest.Location = new System.Drawing.Point(448, 8);
            this.panel_other_ip_source_and_or_dest.Name = "panel_other_ip_source_and_or_dest";
            this.panel_other_ip_source_and_or_dest.Size = new System.Drawing.Size(144, 32);
            this.panel_other_ip_source_and_or_dest.TabIndex = 67;
            // 
            // radioButton_other_ip_source_and_ip_dest
            // 
            this.radioButton_other_ip_source_and_ip_dest.Location = new System.Drawing.Point(0, 16);
            this.radioButton_other_ip_source_and_ip_dest.Name = "radioButton_other_ip_source_and_ip_dest";
            this.radioButton_other_ip_source_and_ip_dest.Size = new System.Drawing.Size(136, 16);
            this.radioButton_other_ip_source_and_ip_dest.TabIndex = 1;
            this.radioButton_other_ip_source_and_ip_dest.Text = "Ip source and Ip dest";
            // 
            // radioButton_other_ip_source_or_ip_dest
            // 
            this.radioButton_other_ip_source_or_ip_dest.Checked = true;
            this.radioButton_other_ip_source_or_ip_dest.Name = "radioButton_other_ip_source_or_ip_dest";
            this.radioButton_other_ip_source_or_ip_dest.Size = new System.Drawing.Size(136, 16);
            this.radioButton_other_ip_source_or_ip_dest.TabIndex = 0;
            this.radioButton_other_ip_source_or_ip_dest.TabStop = true;
            this.radioButton_other_ip_source_or_ip_dest.Text = "Ip source or Ip dest";
            // 
            // textBox_other_protocol
            // 
            this.textBox_other_protocol.Location = new System.Drawing.Point(120, 48);
            this.textBox_other_protocol.Name = "textBox_other_protocol";
            this.textBox_other_protocol.TabIndex = 11;
            this.textBox_other_protocol.Text = "";
            // 
            // checkBox_other_protocol
            // 
            this.checkBox_other_protocol.Location = new System.Drawing.Point(8, 48);
            this.checkBox_other_protocol.Name = "checkBox_other_protocol";
            this.checkBox_other_protocol.TabIndex = 10;
            this.checkBox_other_protocol.Text = "Protocol";
            // 
            // textBox_other_ip_source
            // 
            this.textBox_other_ip_source.Location = new System.Drawing.Point(336, 8);
            this.textBox_other_ip_source.Name = "textBox_other_ip_source";
            this.textBox_other_ip_source.TabIndex = 9;
            this.textBox_other_ip_source.Text = "";
            // 
            // textBox_other_ip_dest
            // 
            this.textBox_other_ip_dest.Location = new System.Drawing.Point(120, 8);
            this.textBox_other_ip_dest.Name = "textBox_other_ip_dest";
            this.textBox_other_ip_dest.TabIndex = 8;
            this.textBox_other_ip_dest.Text = "";
            // 
            // checkBox_other_ip_source
            // 
            this.checkBox_other_ip_source.Location = new System.Drawing.Point(232, 8);
            this.checkBox_other_ip_source.Name = "checkBox_other_ip_source";
            this.checkBox_other_ip_source.TabIndex = 7;
            this.checkBox_other_ip_source.Text = "Ip Source";
            // 
            // checkBox_other_ip_dest
            // 
            this.checkBox_other_ip_dest.Location = new System.Drawing.Point(8, 8);
            this.checkBox_other_ip_dest.Name = "checkBox_other_ip_dest";
            this.checkBox_other_ip_dest.TabIndex = 6;
            this.checkBox_other_ip_dest.Text = "Ip dest";
            // 
            // tabPage_ip
            // 
            this.tabPage_ip.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                     this.panel_ip_ip_source_and_or_dest,
                                                                                     this.textBox_ip_ipsource,
                                                                                     this.textBox_ip_ipdest,
                                                                                     this.checkBox_ip_ipsource,
                                                                                     this.checkBox_ip_ipdest,
                                                                                     this.checkBox_ip_Fragment_position,
                                                                                     this.checkBox_ip_Fragment_type,
                                                                                     this.checkBox_ip_Identification,
                                                                                     this.checkBox_ip_Relibility,
                                                                                     this.checkBox_ip_Throughput,
                                                                                     this.checkBox_ip_Delay,
                                                                                     this.checkBox_ip_Precedence,
                                                                                     this.numericUpDown_ip_identification,
                                                                                     this.comboBox_ip_flags_fragment_pos,
                                                                                     this.comboBox_ip_flags_fragment_type,
                                                                                     this.comboBox_ip_tos_relibility,
                                                                                     this.comboBox_ip_tos_throughtput,
                                                                                     this.comboBox_ip_tos_delay,
                                                                                     this.comboBox_ip_tos_precedence,
                                                                                     this.radioButton_ip_filter_and,
                                                                                     this.radioButton_ip_filter_or});
            this.tabPage_ip.Location = new System.Drawing.Point(4, 22);
            this.tabPage_ip.Name = "tabPage_ip";
            this.tabPage_ip.Size = new System.Drawing.Size(632, 142);
            this.tabPage_ip.TabIndex = 4;
            this.tabPage_ip.Text = "IP";
            // 
            // panel_ip_ip_source_and_or_dest
            // 
            this.panel_ip_ip_source_and_or_dest.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                                         this.radioButton_ip_ip_source_and_ip_dest,
                                                                                                         this.radioButton_ip_ip_source_or_ip_dest});
            this.panel_ip_ip_source_and_or_dest.Location = new System.Drawing.Point(464, 40);
            this.panel_ip_ip_source_and_or_dest.Name = "panel_ip_ip_source_and_or_dest";
            this.panel_ip_ip_source_and_or_dest.Size = new System.Drawing.Size(144, 32);
            this.panel_ip_ip_source_and_or_dest.TabIndex = 66;
            // 
            // radioButton_ip_ip_source_and_ip_dest
            // 
            this.radioButton_ip_ip_source_and_ip_dest.Location = new System.Drawing.Point(0, 16);
            this.radioButton_ip_ip_source_and_ip_dest.Name = "radioButton_ip_ip_source_and_ip_dest";
            this.radioButton_ip_ip_source_and_ip_dest.Size = new System.Drawing.Size(136, 16);
            this.radioButton_ip_ip_source_and_ip_dest.TabIndex = 1;
            this.radioButton_ip_ip_source_and_ip_dest.Text = "Ip source and Ip dest";
            // 
            // radioButton_ip_ip_source_or_ip_dest
            // 
            this.radioButton_ip_ip_source_or_ip_dest.Checked = true;
            this.radioButton_ip_ip_source_or_ip_dest.Name = "radioButton_ip_ip_source_or_ip_dest";
            this.radioButton_ip_ip_source_or_ip_dest.Size = new System.Drawing.Size(136, 16);
            this.radioButton_ip_ip_source_or_ip_dest.TabIndex = 0;
            this.radioButton_ip_ip_source_or_ip_dest.TabStop = true;
            this.radioButton_ip_ip_source_or_ip_dest.Text = "Ip source or Ip dest";
            // 
            // textBox_ip_ipsource
            // 
            this.textBox_ip_ipsource.Location = new System.Drawing.Point(336, 40);
            this.textBox_ip_ipsource.Name = "textBox_ip_ipsource";
            this.textBox_ip_ipsource.TabIndex = 65;
            this.textBox_ip_ipsource.Text = "";
            // 
            // textBox_ip_ipdest
            // 
            this.textBox_ip_ipdest.Location = new System.Drawing.Point(120, 40);
            this.textBox_ip_ipdest.Name = "textBox_ip_ipdest";
            this.textBox_ip_ipdest.TabIndex = 64;
            this.textBox_ip_ipdest.Text = "";
            // 
            // checkBox_ip_ipsource
            // 
            this.checkBox_ip_ipsource.Location = new System.Drawing.Point(232, 40);
            this.checkBox_ip_ipsource.Name = "checkBox_ip_ipsource";
            this.checkBox_ip_ipsource.Size = new System.Drawing.Size(104, 16);
            this.checkBox_ip_ipsource.TabIndex = 63;
            this.checkBox_ip_ipsource.Text = "Ip Source";
            // 
            // checkBox_ip_ipdest
            // 
            this.checkBox_ip_ipdest.Location = new System.Drawing.Point(8, 40);
            this.checkBox_ip_ipdest.Name = "checkBox_ip_ipdest";
            this.checkBox_ip_ipdest.Size = new System.Drawing.Size(104, 16);
            this.checkBox_ip_ipdest.TabIndex = 62;
            this.checkBox_ip_ipdest.Text = "Ip dest";
            // 
            // checkBox_ip_Fragment_position
            // 
            this.checkBox_ip_Fragment_position.Location = new System.Drawing.Point(264, 104);
            this.checkBox_ip_Fragment_position.Name = "checkBox_ip_Fragment_position";
            this.checkBox_ip_Fragment_position.Size = new System.Drawing.Size(120, 16);
            this.checkBox_ip_Fragment_position.TabIndex = 61;
            this.checkBox_ip_Fragment_position.Text = "Fragment position";
            // 
            // checkBox_ip_Fragment_type
            // 
            this.checkBox_ip_Fragment_type.Location = new System.Drawing.Point(152, 104);
            this.checkBox_ip_Fragment_type.Name = "checkBox_ip_Fragment_type";
            this.checkBox_ip_Fragment_type.Size = new System.Drawing.Size(104, 16);
            this.checkBox_ip_Fragment_type.TabIndex = 60;
            this.checkBox_ip_Fragment_type.Text = "Fragment type";
            // 
            // checkBox_ip_Identification
            // 
            this.checkBox_ip_Identification.Location = new System.Drawing.Point(8, 104);
            this.checkBox_ip_Identification.Name = "checkBox_ip_Identification";
            this.checkBox_ip_Identification.Size = new System.Drawing.Size(104, 16);
            this.checkBox_ip_Identification.TabIndex = 59;
            this.checkBox_ip_Identification.Text = "Identification";
            // 
            // checkBox_ip_Relibility
            // 
            this.checkBox_ip_Relibility.Location = new System.Drawing.Point(312, 64);
            this.checkBox_ip_Relibility.Name = "checkBox_ip_Relibility";
            this.checkBox_ip_Relibility.Size = new System.Drawing.Size(104, 16);
            this.checkBox_ip_Relibility.TabIndex = 58;
            this.checkBox_ip_Relibility.Text = "Relibility";
            // 
            // checkBox_ip_Throughput
            // 
            this.checkBox_ip_Throughput.Location = new System.Drawing.Point(232, 64);
            this.checkBox_ip_Throughput.Name = "checkBox_ip_Throughput";
            this.checkBox_ip_Throughput.Size = new System.Drawing.Size(104, 16);
            this.checkBox_ip_Throughput.TabIndex = 57;
            this.checkBox_ip_Throughput.Text = "Throughput";
            // 
            // checkBox_ip_Delay
            // 
            this.checkBox_ip_Delay.Location = new System.Drawing.Point(152, 64);
            this.checkBox_ip_Delay.Name = "checkBox_ip_Delay";
            this.checkBox_ip_Delay.Size = new System.Drawing.Size(104, 16);
            this.checkBox_ip_Delay.TabIndex = 56;
            this.checkBox_ip_Delay.Text = "Delay";
            // 
            // checkBox_ip_Precedence
            // 
            this.checkBox_ip_Precedence.Location = new System.Drawing.Point(8, 64);
            this.checkBox_ip_Precedence.Name = "checkBox_ip_Precedence";
            this.checkBox_ip_Precedence.Size = new System.Drawing.Size(104, 16);
            this.checkBox_ip_Precedence.TabIndex = 55;
            this.checkBox_ip_Precedence.Text = "Precedence";
            // 
            // numericUpDown_ip_identification
            // 
            this.numericUpDown_ip_identification.Location = new System.Drawing.Point(8, 120);
            this.numericUpDown_ip_identification.Maximum = new System.Decimal(new int[] {
                                                                                            65535,
                                                                                            0,
                                                                                            0,
                                                                                            0});
            this.numericUpDown_ip_identification.Name = "numericUpDown_ip_identification";
            this.numericUpDown_ip_identification.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown_ip_identification.TabIndex = 46;
            // 
            // comboBox_ip_flags_fragment_pos
            // 
            this.comboBox_ip_flags_fragment_pos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ip_flags_fragment_pos.Items.AddRange(new object[] {
                                                                                "0 = Last Fragment",
                                                                                "1 = More Fragments"});
            this.comboBox_ip_flags_fragment_pos.Location = new System.Drawing.Point(264, 120);
            this.comboBox_ip_flags_fragment_pos.Name = "comboBox_ip_flags_fragment_pos";
            this.comboBox_ip_flags_fragment_pos.Size = new System.Drawing.Size(112, 21);
            this.comboBox_ip_flags_fragment_pos.TabIndex = 49;
            // 
            // comboBox_ip_flags_fragment_type
            // 
            this.comboBox_ip_flags_fragment_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ip_flags_fragment_type.Items.AddRange(new object[] {
                                                                                 "0 = May Fragment",
                                                                                 "1 = Don\'t Fragment"});
            this.comboBox_ip_flags_fragment_type.Location = new System.Drawing.Point(152, 120);
            this.comboBox_ip_flags_fragment_type.Name = "comboBox_ip_flags_fragment_type";
            this.comboBox_ip_flags_fragment_type.Size = new System.Drawing.Size(112, 21);
            this.comboBox_ip_flags_fragment_type.TabIndex = 48;
            // 
            // comboBox_ip_tos_relibility
            // 
            this.comboBox_ip_tos_relibility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ip_tos_relibility.Items.AddRange(new object[] {
                                                                            "0 = Normal",
                                                                            "1 = High"});
            this.comboBox_ip_tos_relibility.Location = new System.Drawing.Point(312, 80);
            this.comboBox_ip_tos_relibility.Name = "comboBox_ip_tos_relibility";
            this.comboBox_ip_tos_relibility.Size = new System.Drawing.Size(80, 21);
            this.comboBox_ip_tos_relibility.TabIndex = 45;
            // 
            // comboBox_ip_tos_throughtput
            // 
            this.comboBox_ip_tos_throughtput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ip_tos_throughtput.Items.AddRange(new object[] {
                                                                             "0 = Normal",
                                                                             "1 = High"});
            this.comboBox_ip_tos_throughtput.Location = new System.Drawing.Point(232, 80);
            this.comboBox_ip_tos_throughtput.Name = "comboBox_ip_tos_throughtput";
            this.comboBox_ip_tos_throughtput.Size = new System.Drawing.Size(80, 21);
            this.comboBox_ip_tos_throughtput.TabIndex = 43;
            // 
            // comboBox_ip_tos_delay
            // 
            this.comboBox_ip_tos_delay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ip_tos_delay.Items.AddRange(new object[] {
                                                                       "0 = Normal",
                                                                       "1 = Low"});
            this.comboBox_ip_tos_delay.Location = new System.Drawing.Point(152, 80);
            this.comboBox_ip_tos_delay.Name = "comboBox_ip_tos_delay";
            this.comboBox_ip_tos_delay.Size = new System.Drawing.Size(80, 21);
            this.comboBox_ip_tos_delay.TabIndex = 42;
            // 
            // comboBox_ip_tos_precedence
            // 
            this.comboBox_ip_tos_precedence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ip_tos_precedence.Items.AddRange(new object[] {
                                                                            "111 - Network Control",
                                                                            "110 - Internetwork Control",
                                                                            "101 - CRITIC/ECP",
                                                                            "100 - Flash Override",
                                                                            "011 - Flash",
                                                                            "010 - Immediate",
                                                                            "001 - Priority",
                                                                            "000 - Routine"});
            this.comboBox_ip_tos_precedence.Location = new System.Drawing.Point(8, 80);
            this.comboBox_ip_tos_precedence.Name = "comboBox_ip_tos_precedence";
            this.comboBox_ip_tos_precedence.Size = new System.Drawing.Size(144, 21);
            this.comboBox_ip_tos_precedence.TabIndex = 41;
            // 
            // radioButton_ip_filter_and
            // 
            this.radioButton_ip_filter_and.Location = new System.Drawing.Point(8, 24);
            this.radioButton_ip_filter_and.Name = "radioButton_ip_filter_and";
            this.radioButton_ip_filter_and.Size = new System.Drawing.Size(344, 16);
            this.radioButton_ip_filter_and.TabIndex = 1;
            this.radioButton_ip_filter_and.Text = "Packets must agree with other filters AND these ip filters";
            // 
            // radioButton_ip_filter_or
            // 
            this.radioButton_ip_filter_or.Checked = true;
            this.radioButton_ip_filter_or.Location = new System.Drawing.Point(8, 8);
            this.radioButton_ip_filter_or.Name = "radioButton_ip_filter_or";
            this.radioButton_ip_filter_or.Size = new System.Drawing.Size(344, 16);
            this.radioButton_ip_filter_or.TabIndex = 0;
            this.radioButton_ip_filter_or.TabStop = true;
            this.radioButton_ip_filter_or.Text = "Packets must agree with other filters OR these ip filters";
            // 
            // tabPage_app_name
            // 
            this.tabPage_app_name.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                           this.button_app_name_remove,
                                                                                           this.button_app_name_add,
                                                                                           this.listBox_app_name,
                                                                                           this.checkBox_app_name_get_app_name,
                                                                                           this.checkBox_app_name_get_all_udp_clt,
                                                                                           this.checkBox_app_name_filter_app_name});
            this.tabPage_app_name.Location = new System.Drawing.Point(4, 22);
            this.tabPage_app_name.Name = "tabPage_app_name";
            this.tabPage_app_name.Size = new System.Drawing.Size(632, 142);
            this.tabPage_app_name.TabIndex = 5;
            this.tabPage_app_name.Text = "Application Name";
            // 
            // button_app_name_remove
            // 
            this.button_app_name_remove.Location = new System.Drawing.Point(552, 96);
            this.button_app_name_remove.Name = "button_app_name_remove";
            this.button_app_name_remove.TabIndex = 5;
            this.button_app_name_remove.Text = "Remove";
            this.button_app_name_remove.Click += new System.EventHandler(this.button_app_name_remove_Click);
            // 
            // button_app_name_add
            // 
            this.button_app_name_add.Location = new System.Drawing.Point(552, 64);
            this.button_app_name_add.Name = "button_app_name_add";
            this.button_app_name_add.TabIndex = 4;
            this.button_app_name_add.Text = "Add";
            this.button_app_name_add.Click += new System.EventHandler(this.button_app_name_add_Click);
            // 
            // listBox_app_name
            // 
            this.listBox_app_name.Location = new System.Drawing.Point(8, 56);
            this.listBox_app_name.Name = "listBox_app_name";
            this.listBox_app_name.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox_app_name.Size = new System.Drawing.Size(536, 82);
            this.listBox_app_name.TabIndex = 3;
            // 
            // checkBox_app_name_get_app_name
            // 
            this.checkBox_app_name_get_app_name.Name = "checkBox_app_name_get_app_name";
            this.checkBox_app_name_get_app_name.Size = new System.Drawing.Size(392, 16);
            this.checkBox_app_name_get_app_name.TabIndex = 2;
            this.checkBox_app_name_get_app_name.Text = "Get application name for all tcp connections and udp server data";
            this.checkBox_app_name_get_app_name.CheckedChanged += new System.EventHandler(this.checkBox_app_name_get_app_name_CheckedChanged);
            // 
            // checkBox_app_name_get_all_udp_clt
            // 
            this.checkBox_app_name_get_all_udp_clt.Location = new System.Drawing.Point(24, 32);
            this.checkBox_app_name_get_all_udp_clt.Name = "checkBox_app_name_get_all_udp_clt";
            this.checkBox_app_name_get_all_udp_clt.Size = new System.Drawing.Size(376, 16);
            this.checkBox_app_name_get_all_udp_clt.TabIndex = 1;
            this.checkBox_app_name_get_all_udp_clt.Text = "Capture all client udp data (else no client udp data will be catched)";
            // 
            // checkBox_app_name_filter_app_name
            // 
            this.checkBox_app_name_filter_app_name.Location = new System.Drawing.Point(0, 16);
            this.checkBox_app_name_filter_app_name.Name = "checkBox_app_name_filter_app_name";
            this.checkBox_app_name_filter_app_name.Size = new System.Drawing.Size(424, 16);
            this.checkBox_app_name_filter_app_name.TabIndex = 0;
            this.checkBox_app_name_filter_app_name.Text = "Capture only tcp client/server and udp server data of the following applications";
            this.checkBox_app_name_filter_app_name.CheckedChanged += new System.EventHandler(this.checkBox_app_name_filter_app_name_CheckedChanged);
            // 
            // checkBox_other
            // 
            this.checkBox_other.Location = new System.Drawing.Point(8, 48);
            this.checkBox_other.Name = "checkBox_other";
            this.checkBox_other.Size = new System.Drawing.Size(56, 16);
            this.checkBox_other.TabIndex = 15;
            this.checkBox_other.Text = "Other";
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(8, 88);
            this.button_start.Name = "button_start";
            this.button_start.TabIndex = 20;
            this.button_start.Text = "Start";
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // button_stop
            // 
            this.button_stop.Location = new System.Drawing.Point(96, 88);
            this.button_stop.Name = "button_stop";
            this.button_stop.TabIndex = 21;
            this.button_stop.Text = "Stop";
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // checkBox_packets_details
            // 
            this.checkBox_packets_details.Location = new System.Drawing.Point(40, 120);
            this.checkBox_packets_details.Name = "checkBox_packets_details";
            this.checkBox_packets_details.Size = new System.Drawing.Size(128, 16);
            this.checkBox_packets_details.TabIndex = 22;
            this.checkBox_packets_details.Text = "Packet\'s details";
            // 
            // checkBox_IP
            // 
            this.checkBox_IP.Location = new System.Drawing.Point(64, 48);
            this.checkBox_IP.Name = "checkBox_IP";
            this.checkBox_IP.Size = new System.Drawing.Size(56, 16);
            this.checkBox_IP.TabIndex = 23;
            this.checkBox_IP.Text = "Ip";
            // 
            // button_clear_list_view
            // 
            this.button_clear_list_view.Location = new System.Drawing.Point(8, 144);
            this.button_clear_list_view.Name = "button_clear_list_view";
            this.button_clear_list_view.TabIndex = 24;
            this.button_clear_list_view.Text = "Clear";
            this.button_clear_list_view.Click += new System.EventHandler(this.button_clear_list_view_Click);
            // 
            // contextMenu_list_view
            // 
            this.contextMenu_list_view.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                                  this.menuItem_clear,
                                                                                                  this.menuItem_copy_selected,
                                                                                                  this.menuItem_save_selected,
                                                                                                  this.menuItem_save_all,
                                                                                                  this.menuItem_protocol_data});
            // 
            // menuItem_clear
            // 
            this.menuItem_clear.Index = 0;
            this.menuItem_clear.Text = "Clear";
            // 
            // menuItem_copy_selected
            // 
            this.menuItem_copy_selected.Index = 1;
            this.menuItem_copy_selected.Text = "Copy selected";
            // 
            // menuItem_save_selected
            // 
            this.menuItem_save_selected.Index = 2;
            this.menuItem_save_selected.Text = "Save selected";
            // 
            // menuItem_save_all
            // 
            this.menuItem_save_all.Index = 3;
            this.menuItem_save_all.Text = "Save all";
            // 
            // menuItem_protocol_data
            // 
            this.menuItem_protocol_data.Index = 4;
            this.menuItem_protocol_data.Text = "Protocol data";
            // 
            // checkBox_all_packets
            // 
            this.checkBox_all_packets.Location = new System.Drawing.Point(8, 64);
            this.checkBox_all_packets.Name = "checkBox_all_packets";
            this.checkBox_all_packets.Size = new System.Drawing.Size(168, 16);
            this.checkBox_all_packets.TabIndex = 25;
            this.checkBox_all_packets.Text = "None (capture all packets)";
            this.checkBox_all_packets.CheckedChanged += new System.EventHandler(this.checkBox_all_packets_CheckedChanged);
            // 
            // userControlPacketsView
            // 
            this.userControlPacketsView.Location = new System.Drawing.Point(10, 176);
            this.userControlPacketsView.Name = "userControlPacketsView";
            this.userControlPacketsView.Size = new System.Drawing.Size(814, 104);
            this.userControlPacketsView.TabIndex = 26;
            // 
            // button_load_capture
            // 
            this.button_load_capture.Location = new System.Drawing.Point(96, 144);
            this.button_load_capture.Name = "button_load_capture";
            this.button_load_capture.TabIndex = 27;
            this.button_load_capture.Text = "Load";
            this.button_load_capture.Click += new System.EventHandler(this.button_load_capture_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Applications (*.exe)|*.exe|all(*.*)|*.*";
            this.openFileDialog.Multiselect = true;
            // 
            // Form_packet_capture
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(832, 286);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.button_load_capture,
                                                                          this.userControlPacketsView,
                                                                          this.checkBox_all_packets,
                                                                          this.button_clear_list_view,
                                                                          this.checkBox_IP,
                                                                          this.checkBox_packets_details,
                                                                          this.button_stop,
                                                                          this.button_start,
                                                                          this.checkBox_icmp,
                                                                          this.checkBox_udp,
                                                                          this.checkBox_tcp,
                                                                          this.label2,
                                                                          this.tabControl_proto_capture,
                                                                          this.checkBox_other});
            this.Name = "Form_packet_capture";
            this.Text = "Packets Capture";
            this.Resize += new System.EventHandler(this.Form_packet_capture_Resize);
            this.tabControl_proto_capture.ResumeLayout(false);
            this.tabPage_tcp.ResumeLayout(false);
            this.panel_tcp_ipport_source_and_or_dest.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_window_max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_window_min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_data_offset_max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_data_offset_min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_ack_num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_seq_num)).EndInit();
            this.tabPage_udp.ResumeLayout(false);
            this.panel_udp_ipport_source_and_or_dest.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_udp_length_max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_udp_length_min)).EndInit();
            this.tabPage_icmp.ResumeLayout(false);
            this.panel_icmp_ip_source_and_or_dest.ResumeLayout(false);
            this.tabPage_other.ResumeLayout(false);
            this.panel_other_ip_source_and_or_dest.ResumeLayout(false);
            this.tabPage_ip.ResumeLayout(false);
            this.panel_ip_ip_source_and_or_dest.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_identification)).EndInit();
            this.tabPage_app_name.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private void Form_packet_capture_Resize(object sender, System.EventArgs e)
        {
            // resize and put the same space between left bottom and right of the listview
            if (this.ClientSize.Height>this.userControlPacketsView.Top+100)
                this.userControlPacketsView.Height=this.ClientSize.Height-this.userControlPacketsView.Top-this.userControlPacketsView.Left;
            if (this.ClientSize.Width>this.userControlPacketsView.Left+100)
                this.userControlPacketsView.Width=this.ClientSize.Width-2*this.userControlPacketsView.Left;
        }

        private void button_start_Click(object sender, System.EventArgs e)
        {
            this.enable_disable_start_stop_button(true);
            // fill ip port dest source array to avoid parsing at each rcv packet
            ip_ipsrc_list=Cmultiple_elements_parsing.Parse_ip(this.textBox_ip_ipsource.Text);
            ip_ipdst_list=Cmultiple_elements_parsing.Parse_ip(this.textBox_ip_ipdest.Text);

            other_ipsrc_list=Cmultiple_elements_parsing.Parse_ip(this.textBox_other_ip_source.Text);
            other_ipdst_list=Cmultiple_elements_parsing.Parse_ip(this.textBox_other_ip_dest.Text);
            other_protocol_list=Cmultiple_elements_parsing.Parse_ushort(this.textBox_other_protocol.Text);

            icmp_ipsrc_list=Cmultiple_elements_parsing.Parse_ip(this.textBox_icmp_ip_source.Text);
            icmp_ipdst_list=Cmultiple_elements_parsing.Parse_ip(this.textBox_icmp_ip_dest.Text);

            udp_ipsrc_list=Cmultiple_elements_parsing.Parse_ip(this.textBox_udp_ip_source.Text);
            udp_ipdst_list=Cmultiple_elements_parsing.Parse_ip(this.textBox_udp_ip_dest.Text);
            udp_portsrc_list=Cmultiple_elements_parsing.Parse_ushort(this.textBox_udp_port_source.Text);
            udp_portdst_list=Cmultiple_elements_parsing.Parse_ushort(this.textBox_udp_port_dest.Text);

            tcp_ipsrc_list=Cmultiple_elements_parsing.Parse_ip(this.textBox_tcp_ip_source.Text);
            tcp_ipdst_list=Cmultiple_elements_parsing.Parse_ip(this.textBox_tcp_ip_dest.Text);
            tcp_portsrc_list=Cmultiple_elements_parsing.Parse_ushort(this.textBox_tcp_port_source.Text);
            tcp_portdst_list=Cmultiple_elements_parsing.Parse_ushort(this.textBox_tcp_port_dest.Text);
            // start sniffing
            this.iph_server.start();
        }

        private void button_stop_Click(object sender, System.EventArgs e)
        {
            this.enable_disable_start_stop_button(false);        
            this.iph_server.stop();
        }
        private void enable_disable_start_stop_button(bool b_started)
        {
            this.button_start.Enabled=!b_started;
            this.button_stop.Enabled=b_started;
        }
        private void iph_server_event_Socket_Error(easy_socket.ip_header.ipv4_header sender, easy_socket.ip_header.EventArgs_Exception e)
        {
            MessageBox.Show(this,e.exception.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            this.enable_disable_start_stop_button(false);
        }
        private void iph_server_event_Socket_RcvPacket_Error(easy_socket.ip_header.ipv4_header sender, easy_socket.ip_header.EventArgs_Exception_Packet e)
        {
            byte[] b=e.buffer;// because e.buffer is readonly and can't be passed by ref
            this.add_boggus_packet(ref sender,ref b);
        }

        private void iph_server_event_Socket_Data_Arrival(easy_socket.ip_header.ipv4_header sender, easy_socket.ip_header.EventArgs_FullPacket e)
        {
            this.last_full_packet=e.buffer;
            this.last_application_name="";
            this.last_packet_type=PACKET_TYPE.none;
            // ipv4h has already been decoded in ip_header_server
            if (!this.filter_packets(ref sender))
                return;

            if (this.checkBox_app_name_filter_app_name.Checked)
            {
                if (!this.filter_application(ref sender,ref this.last_application_name))
                    return;
            }
            else if(this.checkBox_app_name_get_app_name.Checked)
            {
                this.filter_application(ref sender,ref this.last_application_name);// don't care the return : only to get app name
            }

            this.show_packet_info(ref sender);
        }
        #region application filter
        /// <summary>
        /// filter packets from application name
        /// </summary>
        /// <remarks>ipv4h must be decoded, tcph or udph must be decoded</remarks>
        /// <param name="ipv4h"></param>
        /// <param name="filename"></param>
        /// <returns>true if packet is a wanted packet</returns>
        private bool filter_application(ref easy_socket.ip_header.ipv4_header ipv4h,ref string filename)
        {
            switch(this.last_packet_type)
            {
                case PACKET_TYPE.tcp:
                    iphelper.CMIB_TCPEXTABLE tcp_table;
                    tcp_table=iphelper.iphelper.GetTcpExTable();
                    for (int cpt=0;cpt<tcp_table.dwNumEntries;cpt++)
                    {
                        if (tcp_table.table[cpt].pProcess==null)
                            continue;// don't show informations

                        if (tcp_table.table[cpt].dwLocalAddr==ipv4h.destination_address)// incoming packet
                        {
                            if(tcp_table.table[cpt].dwRemoteAddr==ipv4h.source_address)
                                if (tcp_table.table[cpt].dwLocalPort==this.tcph.destination_port)
                                    if (tcp_table.table[cpt].dwRemotePort==this.tcph.source_port)
                                    {
                                        try
                                        {
                                            filename=tcp_table.table[cpt].pProcess.MainModule.FileName;    
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                        return check_application_name(filename);
                                    }
                        }
                        // not else if in case of ip dest==ip source
                        if(tcp_table.table[cpt].dwLocalAddr==ipv4h.source_address)// outgoing packet
                            if(tcp_table.table[cpt].dwRemoteAddr==ipv4h.destination_address)
                                if (tcp_table.table[cpt].dwLocalPort==this.tcph.source_port)
                                    if (tcp_table.table[cpt].dwRemotePort==this.tcph.destination_port)
                                    {
                                        try
                                        {
                                            filename=tcp_table.table[cpt].pProcess.MainModule.FileName;    
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                        return check_application_name(filename);
                                    }
                    }
                    break;
                case PACKET_TYPE.udp:
                    iphelper.CMIB_UDPEXTABLE udp_table;
                    udp_table=iphelper.iphelper.GetUdpExTable();
                    for (int cpt=0;cpt<udp_table.dwNumEntries;cpt++)
                    {
                        if (udp_table.table[cpt].pProcess==null)
                            continue;// don't show informations

                        if (udp_table.table[cpt].dwLocalAddr==ipv4h.destination_address)// incoming packet
                        {
                            if (udp_table.table[cpt].dwLocalPort==this.udph.destination_port)
                            {
                                try
                                {
                                    filename=udp_table.table[cpt].pProcess.MainModule.FileName;    
                                }
                                catch
                                {
                                    continue;
                                }
                                return check_application_name(filename);
                            }
                        }
                        // not else if in case of ip dest==ip source
                        if(udp_table.table[cpt].dwLocalAddr==ipv4h.source_address)// outgoing packet
                            if (udp_table.table[cpt].dwLocalPort==this.udph.source_port)
                            {
                                try
                                {
                                    filename=udp_table.table[cpt].pProcess.MainModule.FileName;    
                                }
                                catch
                                {
                                    continue;
                                }
                                return check_application_name(filename);
                            }
                        // get udp clt data if specified
                        if (checkBox_app_name_get_all_udp_clt.Checked)
                        {
                            return true;
                        }
                    }
                    break;
                default:// iphelper gives us only tcp and udp table 
                    return true;
            }
            return false;
        }
        #endregion

        #region packet filter
        /// <summary>
        /// filter packets
        /// </summary>
        /// <remarks>ipv4h must be decoded</remarks>
        /// <param name="ipv4h"></param>
        /// <returns>true if packet is a wanted packet</returns>
        private bool filter_packets(ref easy_socket.ip_header.ipv4_header ipv4h)
        {
            if (this.checkBox_all_packets.Checked)
            {
                this.find_packet_type(ref ipv4h);
                return true;
            }
            bool b_ret_iph=true;
            bool b_ret=true;

            // check ip header data

            switch (ipv4h.protocol)
            {
                // if icmp protocol
                case easy_socket.ip_header.ipv4_header.protocol_icmp:
                    if (this.checkBox_icmp.Checked)
                        b_ret=this.filter_packets_icmp_flags(ref ipv4h);
                    else
                        b_ret=false;
                    break;
                // if udp protocol
                case easy_socket.ip_header.ipv4_header.protocol_udp:
                    if (this.checkBox_udp.Checked)
                        b_ret=this.filter_packets_udp_flags(ref ipv4h);
                    else
                        b_ret=false;
                    break;
                // if tcp protocol
                case easy_socket.ip_header.ipv4_header.protocol_tcp:
                    if (this.checkBox_tcp.Checked)
                        b_ret=this.filter_packets_tcp_flags(ref ipv4h);
                    else
                        b_ret=false;
                    break;
            }
            // other protocol
            if (this.checkBox_other.Checked)
                // show packet if it agree (tcp||udp||icmp)||other
                b_ret=(b_ret||this.filter_packets_other_flags(ref ipv4h));

            if (!this.checkBox_IP.Checked)
            {
                return b_ret;
            }
            else
            {
                if (this.radioButton_ip_filter_and.Checked&&(b_ret==false))
                    return false;
                b_ret_iph=this.filter_packets_ip_flags(ref ipv4h);
                if (this.radioButton_ip_filter_or.Checked)
                    return (b_ret||b_ret_iph);
                else// this.radioButton_ip_filter_and.Checked
                    return (b_ret&&b_ret_iph);
            }
        }
        #endregion

        #region ip filters
        /// <summary>
        /// filter ip packets
        /// </summary>
        /// <param name="ipv4h"></param>
        /// <returns>true if packet is a wanted packet</returns>
        private bool filter_packets_ip_flags(ref easy_socket.ip_header.ipv4_header ipv4h)
        {
            bool b_ip_src_checked=true;
            bool b_ip_dst_checked=true;
            // ip check
            if (this.checkBox_ip_ipdest.Checked)
                b_ip_dst_checked=this.check_ip(ipv4h.DestinationAddress,ref this.ip_ipdst_list);
            if (this.checkBox_ip_ipsource.Checked)
                b_ip_src_checked=this.check_ip(ipv4h.SourceAddress,ref this.ip_ipsrc_list);
            if (radioButton_ip_ip_source_and_ip_dest.Checked)
            {
                if (!(b_ip_dst_checked&&b_ip_src_checked))
                    return false;
            }
            else
            {
                if (!(b_ip_dst_checked||b_ip_src_checked))
                    return false;
            }
            // control flags
            if(this.checkBox_ip_Fragment_type.Checked)
                if(ipv4h.MayDontFragment!=System.Convert.ToByte(this.comboBox_ip_flags_fragment_type.Text.Substring(0,1)))
                    return false;
            if(this.checkBox_ip_Fragment_position.Checked)
                if(ipv4h.LastMoreFragment!=System.Convert.ToByte(this.comboBox_ip_flags_fragment_pos.Text.Substring(0,1)))
                    return false;
            // type of service
            if(this.checkBox_ip_Precedence.Checked)
                if(ipv4h.Precedence!=easy_socket.bin_convert.strbit_to_byte(this.comboBox_ip_tos_precedence.Text.Substring(0,3)))
                    return false;
            if(this.checkBox_ip_Delay.Checked)
                if(ipv4h.Delay!=System.Convert.ToByte(this.comboBox_ip_tos_delay.Text.Substring(0,1)))
                    return false;
            if(this.checkBox_ip_Throughput.Checked)
                if(ipv4h.Throughput!=System.Convert.ToByte(this.comboBox_ip_tos_throughtput.Text.Substring(0,1)))
                    return false;
            if(this.checkBox_ip_Relibility.Checked)
                if(ipv4h.Relibility!=System.Convert.ToByte(this.comboBox_ip_tos_relibility.Text.Substring(0,1)))
                    return false;
            // all is ok
            if (this.last_packet_type==PACKET_TYPE.none)// packet doesn't meet other filter --> it wasn't decoded
                this.find_packet_type(ref ipv4h);
            // ip filters are ok --> return true
            return true;
        }
        #endregion

        #region tcp filters
        /// <summary>
        /// filter tcp packets
        /// </summary>
        /// <param name="ipv4h"></param>
        /// <returns>true if packet is a wanted packet</returns>
        private bool filter_packets_tcp_flags(ref easy_socket.ip_header.ipv4_header ipv4h)
        {
            bool b_ipport_src_checked=true;
            bool b_ipport_dst_checked=true;
            bool b_ret=false;
            bool b_ret_control_flags=false;
            // check non control options
            if (this.tcph.decode(ipv4h.source_address,ipv4h.destination_address,ipv4h.data)!=easy_socket.tcp_header.tcp_header.error_success)
                return false;
            this.last_packet_type=PACKET_TYPE.tcp;
            for(;;)
            {
                if (this.checkBox_tcp_ip_source.Checked)
                    if (!this.check_ip(ipv4h.SourceAddress,ref this.tcp_ipsrc_list))
                        b_ipport_src_checked=false;
                if (this.checkBox_tcp_ip_dest.Checked)
                    if (!this.check_ip(ipv4h.DestinationAddress,ref this.tcp_ipdst_list))
                        b_ipport_dst_checked=false;
                if (this.checkBox_tcp_port_source.Checked)
                    if (!this.check_port(this.tcph.SourcePort,ref this.tcp_portsrc_list))
                        b_ipport_src_checked=false;
                if (this.checkBox_tcp_port_dest.Checked)
                    if (!this.check_port(this.tcph.DestinationPort,ref this.tcp_portdst_list))
                        b_ipport_dst_checked=false;

                if (this.radioButton_tcp_ipport_source_and_dest.Checked)
                {
                    if (!(b_ipport_dst_checked&&b_ipport_src_checked))
                        break;
                }
                else
                {
                    if (!(b_ipport_dst_checked||b_ipport_src_checked))
                        break;
                }

                if (this.checkBox_tcp_seq_num.Checked)
                    if (this.tcph.SequenceNumber!=this.numericUpDown_tcp_seq_num.Value)
                        break;
                if (this.checkBox_tcp_ack_num.Checked)
                    if (this.tcph.AcknowledgmentNumber!=this.numericUpDown_tcp_ack_num.Value)
                        break;
                if (this.checkBox_tcp_data_offset.Checked)
                    if ((this.tcph.DataOffset<this.numericUpDown_tcp_data_offset_min.Value)
                        ||(this.tcph.DataOffset>this.numericUpDown_tcp_data_offset_max.Value))
                        break;
                if (this.checkBox_tcp_window.Checked)
                    if ((this.tcph.Window<this.numericUpDown_tcp_window_min.Value)
                        ||(this.tcph.Window>this.numericUpDown_tcp_window_max.Value))
                        break;
                b_ret=true;
                break;
            }
            // check control options
            if (this.checkBox_tcp_control_options.Checked)
            {
                if (this.radioButton_tcp_or_any_control.Checked||this.radioButton_tcp_and_any_control.Checked)
                {
                    b_ret_control_flags=true;
                    for(;;)
                    {
                        if (this.checkBox_tcp_urg.Checked)
                            if (this.tcph.URG)
                                break;
                        if (this.checkBox_tcp_ack.Checked)
                            if (this.tcph.ACK)
                                break;
                        if (this.checkBox_tcp_push.Checked)
                            if (this.tcph.PSH)
                                break;
                        if (this.checkBox_tcp_rst.Checked)
                            if (this.tcph.RST)
                                break;
                        if (this.checkBox_tcp_syn.Checked)
                            if (this.tcph.SYN)
                                break;
                        if (this.checkBox_tcp_fin.Checked)
                            if (this.tcph.FIN)
                                break;
                        // if no control is selected
                        if (!(this.checkBox_tcp_urg.Checked||this.checkBox_tcp_ack.Checked||this.checkBox_tcp_push.Checked||this.checkBox_tcp_rst.Checked||this.checkBox_tcp_syn.Checked||this.checkBox_tcp_fin.Checked))
                            // if packet has no control
                            if (!(this.tcph.URG||this.tcph.ACK||this.tcph.PSH||this.tcph.RST||this.tcph.SYN||this.tcph.FIN))
                                break;

                        b_ret_control_flags=false;
                        break;
                    }
                }
                else //this.radioButton_tcp_or_all_control.Checked||this.radioButton_tcp_and_all_control.Checked
                {
                    b_ret_control_flags=false;
                    for(;;)
                    {
                        if (this.checkBox_tcp_urg.Checked)
                            if (!this.tcph.URG)
                                break;
                        if (this.checkBox_tcp_ack.Checked)
                            if (!this.tcph.ACK)
                                break;
                        if (this.checkBox_tcp_push.Checked)
                            if (!this.tcph.PSH)
                                break;
                        if (this.checkBox_tcp_rst.Checked)
                            if (!this.tcph.RST)
                                break;
                        if (this.checkBox_tcp_syn.Checked)
                            if (!this.tcph.SYN)
                                break;
                        if (this.checkBox_tcp_fin.Checked)
                            if (!this.tcph.FIN)
                                break;
                        // if no control is selected
                        if (!(this.checkBox_tcp_urg.Checked||this.checkBox_tcp_ack.Checked||this.checkBox_tcp_push.Checked||this.checkBox_tcp_rst.Checked||this.checkBox_tcp_syn.Checked||this.checkBox_tcp_fin.Checked))
                            // if packet has control
                            if (this.tcph.URG||this.tcph.ACK||this.tcph.PSH||this.tcph.RST||this.tcph.SYN||this.tcph.FIN)
                                break;

                        b_ret_control_flags=true;
                        break;
                    }
                }
            }
            if (this.radioButton_tcp_and_any_control.Checked||this.radioButton_tcp_and_all_control.Checked)
                return (b_ret&&b_ret_control_flags);
            else// this.radioButton_tcp_or_any_control.Checked||this.radioButton_tcp_or_all_control.Checked
                return (b_ret||b_ret_control_flags);
        }
        #endregion

        #region udp filters
        /// <summary>
        /// filter udp packets
        /// </summary>
        /// <param name="ipv4h"></param>
        /// <returns>true if packet is a wanted packet</returns>
        private bool filter_packets_udp_flags(ref easy_socket.ip_header.ipv4_header ipv4h)
        {
            bool b_ipport_src_checked=true;
            bool b_ipport_dst_checked=true;
            if (this.udph.decode(ipv4h.source_address,ipv4h.destination_address,ipv4h.data)!=easy_socket.udp_header.udp_header.error_success)
                return false;
            this.last_packet_type=PACKET_TYPE.udp;
            if (this.checkBox_udp_ip_source.Checked)
                if (!this.check_ip(ipv4h.SourceAddress,ref this.udp_ipsrc_list))
                    b_ipport_src_checked=false;
            if (this.checkBox_udp_ip_dest.Checked)
                if (!this.check_ip(ipv4h.DestinationAddress,ref this.udp_ipdst_list))
                    b_ipport_dst_checked=false;
            if (this.checkBox_udp_port_source.Checked)
                if (!this.check_port(this.udph.SourcePort,ref this.udp_portsrc_list))
                    b_ipport_src_checked=false;
            if (this.checkBox_udp_port_dest.Checked)
                if (!this.check_port(this.udph.DestinationPort,ref this.udp_portdst_list))
                    b_ipport_dst_checked=false;
            if (this.radioButton_tcp_ipport_source_and_dest.Checked)
            {
                if (!(b_ipport_dst_checked&&b_ipport_src_checked))
                    return false;
            }
            else
            {
                if (!(b_ipport_dst_checked||b_ipport_src_checked))
                    return false;
            }
            if (this.checkBox_udp_length.Checked)
                if ((this.numericUpDown_udp_length_min.Value>this.udph.UdpLength)
                    ||(this.numericUpDown_udp_length_max.Value<this.udph.UdpLength))
                    return false;
            // all is ok
            return true;
        }
        #endregion

        #region icmp filters
        /// <summary>
        /// filter icmp packets
        /// </summary>
        /// <param name="ipv4h"></param>
        /// <returns>true if packet is a wanted packet</returns>
        private bool filter_packets_icmp_flags(ref easy_socket.ip_header.ipv4_header ipv4h)
        {
            bool b_ip_src_checked=true;
            bool b_ip_dst_checked=true;
            if (this.checkBox_icmp_ip_source.Checked)
                b_ip_src_checked=this.check_ip(ipv4h.SourceAddress,ref this.icmp_ipsrc_list);
            if (this.checkBox_icmp_ip_dest.Checked)
                b_ip_dst_checked=this.check_ip(ipv4h.DestinationAddress,ref this.icmp_ipdst_list);

            if (radioButton_icmp_ip_source_and_ip_dest.Checked)
            {
                if (!(b_ip_dst_checked&&b_ip_src_checked))
                    return false;
            }
            else
            {
                if (!(b_ip_dst_checked||b_ip_src_checked))
                    return false;
            }
            return this.decode_icmp(ref ipv4h,true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipv4h"></param>
        /// <param name="b_check_filters"></param>
        /// <returns>false if bogus packet, or if don't check filter (if b_check_filters==true)</returns>
        private bool decode_icmp(ref easy_socket.ip_header.ipv4_header ipv4h,bool b_check_filters)
        {
            if (ipv4h.data==null)
                return false;
            if (ipv4h.data.Length<=0)
                return false;
            switch (ipv4h.data[0])// switch type (see icmp protocol)
            {
                case easy_socket.icmp.icmp.EchoReply:
                    if ((!this.checkBox_icmp_echo_reply.Checked)&&b_check_filters)
                        return false;
                    if (this.icmper.decode(ipv4h.data)!=easy_socket.icmp.icmp.error_success)
                    {
                        this.add_boggus_packet(ref ipv4h,ref this.last_full_packet);
                        return false;
                    }
                    this.last_packet_type=PACKET_TYPE.icmp_echo_reply;
                    break;
                case easy_socket.icmp.icmp.DestinationUnreachable:
                    if ((!this.checkBox_icmp_destination_unreachable.Checked)&&b_check_filters)
                        return false;
                    if (this.icmpdu.decode(ipv4h.data)!=easy_socket.icmp.icmp.error_success)
                    {
                        this.add_boggus_packet(ref ipv4h,ref this.last_full_packet);
                        return false;
                    }
                    this.last_packet_type=PACKET_TYPE.icmp_destination_unreachable;
                    break;
                case easy_socket.icmp.icmp.SourceQuench:
                    if ((!this.checkBox_icmp_source_quench.Checked)&&b_check_filters)
                        return false;
                    if (this.icmpsq.decode(ipv4h.data)!=easy_socket.icmp.icmp.error_success)
                    {
                        this.add_boggus_packet(ref ipv4h,ref this.last_full_packet);
                        return false;
                    }
                    this.last_packet_type=PACKET_TYPE.icmp_source_quench;
                    break;
                case easy_socket.icmp.icmp.Redirect:
                    if ((!this.checkBox_icmp_redirect.Checked)&&b_check_filters)
                        return false;
                    if (this.icmpr.decode(ipv4h.data)!=easy_socket.icmp.icmp.error_success)
                    {
                        this.add_boggus_packet(ref ipv4h,ref this.last_full_packet);
                        return false;
                    }
                    this.last_packet_type=PACKET_TYPE.icmp_redirect;
                    break;
                case easy_socket.icmp.icmp.Echo:
                    if ((!this.checkBox_icmp_echo.Checked)&&b_check_filters)
                        return false;
                    if (this.icmpe.decode(ipv4h.data)!=easy_socket.icmp.icmp.error_success)
                    {
                        this.add_boggus_packet(ref ipv4h,ref this.last_full_packet);
                        return false;
                    }
                    this.last_packet_type=PACKET_TYPE.icmp_echo;
                    break;
                case easy_socket.icmp.icmp.TimeExceeded:
                    if ((!this.checkBox_icmp_time_exceeded_message.Checked)&&b_check_filters)
                        return false;
                    if (this.icmptem.decode(ipv4h.data)!=easy_socket.icmp.icmp.error_success)
                    {
                        this.add_boggus_packet(ref ipv4h,ref this.last_full_packet);
                        return false;
                    }
                    this.last_packet_type=PACKET_TYPE.icmp_time_exceeded_message;
                    break;
                case easy_socket.icmp.icmp.ParameterProblem:
                    if ((!this.checkBox_icmp_parameter_problem.Checked)&&b_check_filters)
                        return false;
                    if (this.icmppp.decode(ipv4h.data)!=easy_socket.icmp.icmp.error_success)
                    {
                        this.add_boggus_packet(ref ipv4h,ref this.last_full_packet);
                        return false;
                    }
                    this.last_packet_type=PACKET_TYPE.icmp_parameter_problem;
                    break;
                case easy_socket.icmp.icmp.Timestamp:
                    if ((!this.checkBox_icmp_timestamp.Checked)&&b_check_filters)
                        return false;
                    if (this.icmpt.decode(ipv4h.data)!=easy_socket.icmp.icmp.error_success)
                    {
                        this.add_boggus_packet(ref ipv4h,ref this.last_full_packet);
                        return false;
                    }
                    this.last_packet_type=PACKET_TYPE.icmp_timestamp;
                    break;
                case easy_socket.icmp.icmp.TimestampReply:
                    if ((!this.checkBox_icmp_timestamp_reply.Checked)&&b_check_filters)
                        return false;
                    if (this.icmptr.decode(ipv4h.data)!=easy_socket.icmp.icmp.error_success)
                    {
                        this.add_boggus_packet(ref ipv4h,ref this.last_full_packet);
                        return false;
                    }
                    this.last_packet_type=PACKET_TYPE.icmp_timestamp_reply;
                    break;
                case easy_socket.icmp.icmp.InformationRequest:
                    if ((!this.checkBox_icmp_information_request.Checked)&&b_check_filters)
                        return false;
                    if (this.icmpirequest.decode(ipv4h.data)!=easy_socket.icmp.icmp.error_success)
                    {
                        this.add_boggus_packet(ref ipv4h,ref this.last_full_packet);
                        return false;
                    }
                    this.last_packet_type=PACKET_TYPE.icmp_information_request;
                    break;
                case easy_socket.icmp.icmp.InformationReply:
                    if ((!this.checkBox_icmp_information_reply.Checked)&&b_check_filters)
                        return false;
                    if (this.icmpireply.decode(ipv4h.data)!=easy_socket.icmp.icmp.error_success)
                    {
                        this.add_boggus_packet(ref ipv4h,ref this.last_full_packet);
                        return false;
                    }
                    this.last_packet_type=PACKET_TYPE.icmp_information_reply;
                    break;
                default:
                    return false;
            }            
            return true;
        }
        #endregion

        #region other filters
        /// <summary>
        /// filter other protocols packets
        /// </summary>
        /// <param name="ipv4h"></param>
        /// <returns>true if packet is a wanted packet</returns>
        private bool filter_packets_other_flags(ref easy_socket.ip_header.ipv4_header ipv4h)
        {
            bool b_ip_src_checked=true;
            bool b_ip_dst_checked=true;
            if (this.checkBox_other_ip_dest.Checked)
                b_ip_dst_checked=this.check_ip(ipv4h.DestinationAddress,ref this.other_ipdst_list);
            if (this.checkBox_other_ip_source.Checked)
                b_ip_src_checked=this.check_ip(ipv4h.SourceAddress,ref this.other_ipsrc_list);

            if (radioButton_icmp_ip_source_and_ip_dest.Checked)
            {
                if (!(b_ip_dst_checked&&b_ip_src_checked))
                    return false;
            }
            else
            {
                if (!(b_ip_dst_checked||b_ip_src_checked))
                    return false;
            }
            if (this.checkBox_other_protocol.Checked)
                if (!this.check_port(ipv4h.protocol,ref this.other_protocol_list))// not port but the checking is the same
                    return false;
            // all is ok
            if (this.last_packet_type==PACKET_TYPE.none)// packet doesn't meet other filter --> it wasn't decoded
                this.find_packet_type(ref ipv4h);
            return true;
        }
        #endregion

        #region check ip/ check port / filename
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="ip_list">ip list split by ;</param>
        /// <returns>true if ip is ok</returns>
        private bool check_ip(string ip,ref string[] ip_list)
        {
            if (ip_list==null)
                return false;
            for (int cpt=0; cpt<ip_list.Length;cpt++)
                if(ip==ip_list[cpt])
                    return true;
            // ip was not found
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        /// <param name="port_list">port list split by ;</param>
        /// <returns>true if port is ok</returns>
        private bool check_port(ushort port,ref ushort[] port_list)
        {
            if (port_list==null)
                return false;
            for (int cpt=0; cpt<port_list.Length;cpt++)
                if(port==port_list[cpt])
                    return true;
            // port was not found
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>true if filename is ok</returns>
        private bool check_application_name(string filename)
        {
            if (this.spyed_applications_name==null)
                return false;
            for (int cpt=0; cpt<this.spyed_applications_name.Length;cpt++)
                if(filename==this.spyed_applications_name[cpt])
                    return true;
            // ip was not found
            return false;
        }
        #endregion

        private void find_packet_type(ref easy_socket.ip_header.ipv4_header ipv4h)
        {
            bool b_error=false;
            // if raw packet (ip or other filter) try to find type
            switch (ipv4h.protocol)
            {
                case easy_socket.ip_header.ipv4_header.protocol_icmp:
                    this.decode_icmp(ref ipv4h,false);
                    break;
                    // if udp protocol
                case easy_socket.ip_header.ipv4_header.protocol_udp:
                    if (this.udph.decode(ipv4h.source_address,ipv4h.destination_address,ipv4h.data)==easy_socket.udp_header.udp_header.error_success)
                        this.last_packet_type=PACKET_TYPE.udp;
                    else
                        b_error=true;
                    break;
                    // if tcp protocol
                case easy_socket.ip_header.ipv4_header.protocol_tcp:
                    if (this.tcph.decode(ipv4h.source_address,ipv4h.destination_address,ipv4h.data)==easy_socket.tcp_header.tcp_header.error_success)
                        this.last_packet_type=PACKET_TYPE.tcp;
                    else
                        b_error=true;                
                    break;
                default:
                    this.last_packet_type=PACKET_TYPE.raw;
                    break;
            }
            if (b_error)
            {
                this.add_boggus_packet(ref ipv4h,ref this.last_full_packet);
            }
        }

        #region show packet
        private void show_packet_info(ref easy_socket.ip_header.ipv4_header ipv4h)
        {
            string[] data=null;
            bool b_most_important_info_only=!this.checkBox_packets_details.Checked;
            // put packet fields to string
            switch (this.last_packet_type)
            {
                case PACKET_TYPE.raw:
                    data=easy_socket.packet_ToStringArray.ip_raw(ref ipv4h,b_most_important_info_only);
                    break;
                case PACKET_TYPE.tcp:
                    data=easy_socket.packet_ToStringArray.tcp(ref ipv4h,ref this.tcph,b_most_important_info_only);
                    break;
                case PACKET_TYPE.udp:
                    data=easy_socket.packet_ToStringArray.udp(ref ipv4h,ref this.udph,b_most_important_info_only);
                    break;
                case PACKET_TYPE.icmp_echo:
                    data=easy_socket.packet_ToStringArray.icmp_echo(ref ipv4h,ref this.icmpe,b_most_important_info_only);
                    break;
                case PACKET_TYPE.icmp_echo_reply:
                    data=easy_socket.packet_ToStringArray.icmp_echo_reply(ref ipv4h,ref this.icmper,b_most_important_info_only);
                    break;
                case PACKET_TYPE.icmp_information_reply:
                    data=easy_socket.packet_ToStringArray.icmp_information_reply(ref ipv4h,ref this.icmpireply,b_most_important_info_only);
                    break;
                case PACKET_TYPE.icmp_information_request:
                    data=easy_socket.packet_ToStringArray.icmp_information_request(ref ipv4h,ref this.icmpirequest,b_most_important_info_only);
                    break;
                case PACKET_TYPE.icmp_parameter_problem:
                    data=easy_socket.packet_ToStringArray.icmp_parameter_problem(ref ipv4h,ref this.icmppp,b_most_important_info_only);
                    break;
                case PACKET_TYPE.icmp_redirect:
                    data=easy_socket.packet_ToStringArray.icmp_redirect(ref ipv4h,ref this.icmpr,b_most_important_info_only);
                    break;
                case PACKET_TYPE.icmp_source_quench:
                    data=easy_socket.packet_ToStringArray.icmp_source_quench(ref ipv4h,ref this.icmpsq,b_most_important_info_only);
                    break;
                case PACKET_TYPE.icmp_time_exceeded_message:
                    data=easy_socket.packet_ToStringArray.icmp_time_exceeded_message(ref ipv4h,ref this.icmptem,b_most_important_info_only);
                    break;
                case PACKET_TYPE.icmp_timestamp:
                    data=easy_socket.packet_ToStringArray.icmp_timestamp(ref ipv4h,ref this.icmpt,b_most_important_info_only);
                    break;
                case PACKET_TYPE.icmp_timestamp_reply:
                    data=easy_socket.packet_ToStringArray.icmp_timestamp_reply(ref ipv4h,ref this.icmptr,b_most_important_info_only);
                    break;
                default:
                    break;
            }
            if (data==null)
                return;
            string[] data_items=new String[11];
            // add time
            data_items[0]=System.DateTime.Now.TimeOfDay.ToString();
            // if raw packet
            if (this.last_packet_type==PACKET_TYPE.raw)
            {
                System.Array.Copy(data,0,data_items,1,data.Length-1);
                // put ip data in protocol data 
                data_items[8]=data_items[data.Length-1];
            }
            else
            {
                System.Array.Copy(data,0,data_items,1,data.Length);
            }
            // add full packet data
            data_items[9]=easy_socket.hexa_convert.byte_to_hexa(this.last_full_packet);

            if (this.checkBox_app_name_get_app_name.Checked||this.checkBox_app_name_filter_app_name.Checked)
            {
                // add application name if possible
                data_items[10]=this.last_application_name;
            }

            ListViewItem lvi=new ListViewItem(data_items);
            this.userControlPacketsView.listView_capture.Items.Add(lvi);
        }
        #endregion

        private void add_boggus_packet(ref easy_socket.ip_header.ipv4_header ipv4h,ref byte[] full_packet)
        {
            string[] data_items=new string[10];
            string[] data=easy_socket.packet_ToStringArray.ip_raw(ref ipv4h,!this.checkBox_packets_details.Checked);
            data_items[0]=System.DateTime.Now.TimeOfDay.ToString();
            // ip infos
            System.Array.Copy(data,0,data_items,1,4);
            // ip data
            data_items[8]=data_items[5];
            // full packet data
            data_items[9]=easy_socket.hexa_convert.byte_to_hexa(full_packet);
            ListViewItem lvi=new ListViewItem(data_items);
            lvi.BackColor=System.Drawing.Color.Red;
            this.userControlPacketsView.listView_capture.Items.Add(lvi);
        }

        private void button_clear_list_view_Click(object sender, System.EventArgs e)
        {
            this.userControlPacketsView.listView_capture.Items.Clear(); 
        }
        private void button_load_capture_Click(object sender, System.EventArgs e)
        {
            this.userControlPacketsView.load_file();
        }

        private void checkBox_all_packets_CheckedChanged(object sender, System.EventArgs e)
        {
            this.checkBox_icmp.Enabled=!this.checkBox_all_packets.Checked;
            this.checkBox_IP.Enabled=!this.checkBox_all_packets.Checked;
            this.checkBox_other.Enabled=!this.checkBox_all_packets.Checked;
            this.checkBox_tcp.Enabled=!this.checkBox_all_packets.Checked;
            this.checkBox_udp.Enabled=!this.checkBox_all_packets.Checked;
        }

        private void checkBox_app_name_get_app_name_CheckedChanged(object sender, System.EventArgs e)
        {
            this.show_hide_application_name_column();
        }

        private void checkBox_app_name_filter_app_name_CheckedChanged(object sender, System.EventArgs e)
        {
            this.show_hide_application_name_column();        
        }
        private void show_hide_application_name_column()
        {
            if (this.checkBox_app_name_get_app_name.Checked||this.checkBox_app_name_filter_app_name.Checked)
            {
                this.userControlPacketsView.listView_capture.Columns[10].Width=150;
            }
            else
            {
                this.userControlPacketsView.listView_capture.Columns[10].Width=0;
            }
        }

        private void button_app_name_add_Click(object sender, System.EventArgs e)
        {
            this.openFileDialog.ShowDialog(this);
            this.listBox_app_name.Items.AddRange(this.openFileDialog.FileNames);
            this.spyed_applications_name=new string[this.listBox_app_name.Items.Count];
            for (int cpt=0;cpt<this.listBox_app_name.Items.Count;cpt++)
                this.spyed_applications_name[cpt]=this.listBox_app_name.Items[cpt].ToString();
        }

        private void button_app_name_remove_Click(object sender, System.EventArgs e)
        {
            for (int cpt=this.listBox_app_name.SelectedItems.Count-1;cpt>=0;cpt--)
            {
                this.listBox_app_name.Items.Remove(this.listBox_app_name.SelectedItems[cpt]);
            }
            this.spyed_applications_name=new string[this.listBox_app_name.Items.Count];
            for (int cpt=0;cpt<this.listBox_app_name.Items.Count;cpt++)
                this.spyed_applications_name[cpt]=this.listBox_app_name.Items[cpt].ToString();
        }
        private bool is_xp_os()
        {
            if (System.Environment.OSVersion.Platform==System.PlatformID.Win32NT && 
                ((System.Environment.OSVersion.Version.Major>5)
                || (System.Environment.OSVersion.Version.Major==5 && System.Environment.OSVersion.Version.Minor>0)
                )
                )
                return true;
            return false;
        }
    }
}