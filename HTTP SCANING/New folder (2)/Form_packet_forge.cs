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
    /// Description résumée de Form_packet_forge.
    /// </summary>
    public class Form_packet_forge : System.Windows.Forms.Form
    {
        private easy_socket.tcp_header.tcp_connection tcpc;
        private easy_socket.tcp_header.tcp_header_server tcps;
        private bool b_stop;
        private ToolTip tt;


        private System.Windows.Forms.GroupBox groupBox_ip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox comboBox_ip_tos_precedence;
        private System.Windows.Forms.NumericUpDown numericUpDown_ip_version;
        private System.Windows.Forms.NumericUpDown numericUpDown_ip_header_length;
        private System.Windows.Forms.ComboBox comboBox_ip_tos_relibility;
        private System.Windows.Forms.ComboBox comboBox_ip_tos_throughtput;
        private System.Windows.Forms.ComboBox comboBox_ip_tos_delay;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.NumericUpDown numericUpDown_ip_total_length;
        private System.Windows.Forms.NumericUpDown numericUpDown_ip_identification;
        private System.Windows.Forms.NumericUpDown numericUpDown_ip_flag_bit0;
        private System.Windows.Forms.NumericUpDown numericUpDown_ip_tos_bit7;
        private System.Windows.Forms.NumericUpDown numericUpDown_ip_tos_bit6;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox comboBox_ip_flags_fragment_pos;
        private System.Windows.Forms.ComboBox comboBox_ip_flags_fragment_type;
        private System.Windows.Forms.NumericUpDown numericUpDown_ip_fragment_offset;
        private System.Windows.Forms.TextBox textBox_ip_options_padding;
        private System.Windows.Forms.TextBox textBox_ip_dest;
        private System.Windows.Forms.TextBox textBox_ip_src;
        private System.Windows.Forms.CheckBox checkBox_ip_IHL_auto;
        private System.Windows.Forms.CheckBox checkBox_ip_total_length_auto;
        private System.Windows.Forms.NumericUpDown numericUpDown_ip_header_checksum;
        private System.Windows.Forms.NumericUpDown numericUpDown_ip_protocol;
        private System.Windows.Forms.NumericUpDown numericUpDown_ip_ttl;
        private System.Windows.Forms.CheckBox checkBox_ip_header_checksum_auto;
        private System.Windows.Forms.CheckBox checkBox_tcp_fin;
        private System.Windows.Forms.CheckBox checkBox_tcp_syn;
        private System.Windows.Forms.CheckBox checkBox_tcp_rst;
        private System.Windows.Forms.CheckBox checkBox_tcp_push;
        private System.Windows.Forms.CheckBox checkBox_tcp_ack;
        private System.Windows.Forms.CheckBox checkBox_tcp_urg;
        private System.Windows.Forms.NumericUpDown numericUpDown_tcp_window;
        private System.Windows.Forms.NumericUpDown numericUpDown_tcp_data_offset;
        private System.Windows.Forms.NumericUpDown numericUpDown_tcp_ack_num;
        private System.Windows.Forms.NumericUpDown numericUpDown_tcp_seq_num;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.NumericUpDown numericUpDown_tcp_port_dest;
        private System.Windows.Forms.NumericUpDown numericUpDown_tcp_port_source;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.NumericUpDown numericUpDown_tcp_header_checksum;
        private System.Windows.Forms.CheckBox checkBox_tcp_header_checksum_auto;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.CheckBox checkBox_tcp_src_port_random;
        private System.Windows.Forms.CheckBox checkBox_tcp_sequence_number_random;
        private System.Windows.Forms.CheckBox checkBox_ip_ttl_random;
        private System.Windows.Forms.CheckBox checkBox_ip_identification_random;
        private System.Windows.Forms.NumericUpDown numericUpDown_ip_ttl_min;
        private System.Windows.Forms.CheckBox checkBox_tcp_connection_helper;
        private System.Windows.Forms.GroupBox groupBox_data;
        private System.Windows.Forms.GroupBox groupBox_tcp;
        private System.Windows.Forms.GroupBox groupBox_udp;
        private System.Windows.Forms.GroupBox groupBox_icmp;
        private System.Windows.Forms.CheckBox checkBox_udp_src_port_random;
        private System.Windows.Forms.NumericUpDown numericUpDown_udp_src_port;
        private System.Windows.Forms.NumericUpDown numericUpDown_udp_dest_port;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.NumericUpDown numericUpDown_udp_checksum;
        private System.Windows.Forms.CheckBox checkBox_udp_checksum_auto;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.NumericUpDown numericUpDown_udp_length;
        private System.Windows.Forms.CheckBox checkBox_udp_length_auto;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox textBox_data_value;
        private System.Windows.Forms.CheckBox checkBox_data_value_hexa;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.NumericUpDown numericUpDown_data_number_of_packets;
        private System.Windows.Forms.CheckBox checkBox_data_looping_packets;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.ComboBox comboBox_icmp_icmp_message;
        private System.Windows.Forms.ComboBox comboBox_icmp_code;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.NumericUpDown numericUpDown_icmp_unused;
        private System.Windows.Forms.NumericUpDown numericUpDown_icmp_checksum;
        private System.Windows.Forms.CheckBox checkBox_icmp_checksum_auto;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.NumericUpDown numericUpDown_icmp_pointer;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.NumericUpDown numericUpDown_icmp_identifier;
        private System.Windows.Forms.NumericUpDown numericUpDown_icmp_sequence_number;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.NumericUpDown numericUpDown_icmp_orig_timestamp;
        private System.Windows.Forms.NumericUpDown numericUpDown_icmp_recv_timestamp;
        private System.Windows.Forms.NumericUpDown numericUpDown_icmp_trans_timestamp;
        private System.Windows.Forms.NumericUpDown numericUpDown_tcp_urgent_pointer;
        private System.Windows.Forms.NumericUpDown numericUpDown_tcp_reserved;
        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.TextBox textBox_tcp_options_padding;
        private System.Windows.Forms.CheckBox checkBox_ip_src_random;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.CheckBox checkBox_tcp_data_offset_auto;
        private System.Windows.Forms.TextBox textBox_icmp_gateway;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Panel panel_connection_helper;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label_tcp_connection_state;
        private Network_Stuff.UserControlPacketsView userControlPacketsView;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.CheckBox checkBox_tcp_packet_details;

        private System.ComponentModel.Container components = null;

        public Form_packet_forge()
        {
            InitializeComponent();
            XPStyle.MakeXPStyle(this);
            this.set_stop_state(true);

            this.comboBox_ip_flags_fragment_pos.SelectedIndex=0;
            this.comboBox_ip_flags_fragment_type.SelectedIndex=0;
            this.comboBox_ip_tos_delay.SelectedIndex=0;
            this.comboBox_ip_tos_precedence.SelectedIndex=0;
            this.comboBox_ip_tos_relibility.SelectedIndex=0;
            this.comboBox_ip_tos_throughtput.SelectedIndex=0;

            this.comboBox_icmp_icmp_message.SelectedIndex=0;
            this.comboBox_icmp_code.Items.AddRange(easy_socket.icmp.icmp_echo.get_available_codes());
            this.comboBox_icmp_code.SelectedIndex=0;
            
            // show tcp forge by default
            this.numericUpDown_ip_protocol.Value=6;
            this.show_tcp();

            this.tcpc=new easy_socket.tcp_header.tcp_connection();
            this.tcps=new easy_socket.tcp_header.tcp_header_server();
            this.tcps.event_Data_Arrival+=new easy_socket.tcp_header.Socket_Data_Arrival_EventHandler(tcps_data_arrival);

            // connection helper is disabled at start
            this.panel_connection_helper.Enabled=false;
            // hide ip src, ip dst, protocol ,src port, dst port
            this.userControlPacketsView.listView_capture.Columns[1].Width=0;// ip src
            this.userControlPacketsView.listView_capture.Columns[2].Width=0;// ip dst
            this.userControlPacketsView.listView_capture.Columns[3].Width=0;// protocol
            this.userControlPacketsView.listView_capture.Columns[5].Width=0;// port src
            this.userControlPacketsView.listView_capture.Columns[6].Width=0;// port dst
            this.userControlPacketsView.listView_capture.Columns[7].Width=this.userControlPacketsView.listView_capture.ClientSize.Width
                                                                            -this.userControlPacketsView.listView_capture.Columns[0].Width
                                                                            -this.userControlPacketsView.listView_capture.Columns[4].Width
                                                                            -this.userControlPacketsView.listView_capture.Columns[8].Width
                                                                            -this.userControlPacketsView.listView_capture.Columns[9].Width;
            this.tt=new ToolTip();
            tt.AutoPopDelay=int.MaxValue;
            tt.InitialDelay=100;
            tt.ReshowDelay=100;
            tt.SetToolTip(this.checkBox_tcp_connection_helper,"Auto fill fields for a reply in a connection oriented sequence\r\n(Warning windows send RST packets that should be stopped by a firewall)");
        }


        protected override void Dispose( bool disposing )
        {
            this.tcps.stop();
            this.tcps.event_Data_Arrival-=new easy_socket.tcp_header.Socket_Data_Arrival_EventHandler(tcps_data_arrival);
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
            this.groupBox_ip = new System.Windows.Forms.GroupBox();
            this.label51 = new System.Windows.Forms.Label();
            this.checkBox_ip_src_random = new System.Windows.Forms.CheckBox();
            this.numericUpDown_ip_ttl_min = new System.Windows.Forms.NumericUpDown();
            this.checkBox_ip_identification_random = new System.Windows.Forms.CheckBox();
            this.checkBox_ip_ttl_random = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_ip_options_padding = new System.Windows.Forms.TextBox();
            this.textBox_ip_dest = new System.Windows.Forms.TextBox();
            this.textBox_ip_src = new System.Windows.Forms.TextBox();
            this.numericUpDown_ip_header_length = new System.Windows.Forms.NumericUpDown();
            this.checkBox_ip_IHL_auto = new System.Windows.Forms.CheckBox();
            this.checkBox_ip_total_length_auto = new System.Windows.Forms.CheckBox();
            this.numericUpDown_ip_header_checksum = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_ip_protocol = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_ip_ttl = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_ip_fragment_offset = new System.Windows.Forms.NumericUpDown();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.numericUpDown_ip_identification = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_ip_total_length = new System.Windows.Forms.NumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.numericUpDown_ip_version = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.checkBox_ip_header_checksum_auto = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox_ip_flags_fragment_pos = new System.Windows.Forms.ComboBox();
            this.comboBox_ip_flags_fragment_type = new System.Windows.Forms.ComboBox();
            this.numericUpDown_ip_flag_bit0 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_ip_tos_bit7 = new System.Windows.Forms.NumericUpDown();
            this.comboBox_ip_tos_relibility = new System.Windows.Forms.ComboBox();
            this.numericUpDown_ip_tos_bit6 = new System.Windows.Forms.NumericUpDown();
            this.comboBox_ip_tos_throughtput = new System.Windows.Forms.ComboBox();
            this.comboBox_ip_tos_delay = new System.Windows.Forms.ComboBox();
            this.comboBox_ip_tos_precedence = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox_data = new System.Windows.Forms.GroupBox();
            this.button_stop = new System.Windows.Forms.Button();
            this.button_send = new System.Windows.Forms.Button();
            this.checkBox_data_looping_packets = new System.Windows.Forms.CheckBox();
            this.numericUpDown_data_number_of_packets = new System.Windows.Forms.NumericUpDown();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.textBox_data_value = new System.Windows.Forms.TextBox();
            this.checkBox_data_value_hexa = new System.Windows.Forms.CheckBox();
            this.groupBox_tcp = new System.Windows.Forms.GroupBox();
            this.checkBox_tcp_packet_details = new System.Windows.Forms.CheckBox();
            this.panel_connection_helper = new System.Windows.Forms.Panel();
            this.label40 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label_tcp_connection_state = new System.Windows.Forms.Label();
            this.userControlPacketsView = new Network_Stuff.UserControlPacketsView();
            this.checkBox_tcp_data_offset_auto = new System.Windows.Forms.CheckBox();
            this.label52 = new System.Windows.Forms.Label();
            this.checkBox_tcp_connection_helper = new System.Windows.Forms.CheckBox();
            this.checkBox_tcp_sequence_number_random = new System.Windows.Forms.CheckBox();
            this.checkBox_tcp_src_port_random = new System.Windows.Forms.CheckBox();
            this.numericUpDown_tcp_urgent_pointer = new System.Windows.Forms.NumericUpDown();
            this.label32 = new System.Windows.Forms.Label();
            this.numericUpDown_tcp_header_checksum = new System.Windows.Forms.NumericUpDown();
            this.checkBox_tcp_header_checksum_auto = new System.Windows.Forms.CheckBox();
            this.label31 = new System.Windows.Forms.Label();
            this.numericUpDown_tcp_reserved = new System.Windows.Forms.NumericUpDown();
            this.label30 = new System.Windows.Forms.Label();
            this.textBox_tcp_options_padding = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.numericUpDown_tcp_port_source = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_tcp_port_dest = new System.Windows.Forms.NumericUpDown();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_tcp_fin = new System.Windows.Forms.CheckBox();
            this.checkBox_tcp_syn = new System.Windows.Forms.CheckBox();
            this.checkBox_tcp_rst = new System.Windows.Forms.CheckBox();
            this.checkBox_tcp_push = new System.Windows.Forms.CheckBox();
            this.checkBox_tcp_ack = new System.Windows.Forms.CheckBox();
            this.checkBox_tcp_urg = new System.Windows.Forms.CheckBox();
            this.numericUpDown_tcp_window = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_tcp_data_offset = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_tcp_ack_num = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_tcp_seq_num = new System.Windows.Forms.NumericUpDown();
            this.groupBox_udp = new System.Windows.Forms.GroupBox();
            this.label36 = new System.Windows.Forms.Label();
            this.checkBox_udp_length_auto = new System.Windows.Forms.CheckBox();
            this.numericUpDown_udp_length = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_udp_checksum = new System.Windows.Forms.NumericUpDown();
            this.checkBox_udp_checksum_auto = new System.Windows.Forms.CheckBox();
            this.label35 = new System.Windows.Forms.Label();
            this.checkBox_udp_src_port_random = new System.Windows.Forms.CheckBox();
            this.numericUpDown_udp_src_port = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_udp_dest_port = new System.Windows.Forms.NumericUpDown();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.groupBox_icmp = new System.Windows.Forms.GroupBox();
            this.textBox_icmp_gateway = new System.Windows.Forms.TextBox();
            this.numericUpDown_icmp_trans_timestamp = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_icmp_recv_timestamp = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_icmp_orig_timestamp = new System.Windows.Forms.NumericUpDown();
            this.label50 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.numericUpDown_icmp_sequence_number = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_icmp_identifier = new System.Windows.Forms.NumericUpDown();
            this.label47 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.numericUpDown_icmp_pointer = new System.Windows.Forms.NumericUpDown();
            this.label44 = new System.Windows.Forms.Label();
            this.numericUpDown_icmp_checksum = new System.Windows.Forms.NumericUpDown();
            this.checkBox_icmp_checksum_auto = new System.Windows.Forms.CheckBox();
            this.label43 = new System.Windows.Forms.Label();
            this.numericUpDown_icmp_unused = new System.Windows.Forms.NumericUpDown();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.comboBox_icmp_code = new System.Windows.Forms.ComboBox();
            this.comboBox_icmp_icmp_message = new System.Windows.Forms.ComboBox();
            this.label39 = new System.Windows.Forms.Label();
            this.groupBox_ip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_ttl_min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_header_length)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_header_checksum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_protocol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_ttl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_fragment_offset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_identification)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_total_length)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_version)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_flag_bit0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_tos_bit7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_tos_bit6)).BeginInit();
            this.groupBox_data.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_data_number_of_packets)).BeginInit();
            this.groupBox_tcp.SuspendLayout();
            this.panel_connection_helper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_urgent_pointer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_header_checksum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_reserved)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_port_source)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_port_dest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_window)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_data_offset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_ack_num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_seq_num)).BeginInit();
            this.groupBox_udp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_udp_length)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_udp_checksum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_udp_src_port)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_udp_dest_port)).BeginInit();
            this.groupBox_icmp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_icmp_trans_timestamp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_icmp_recv_timestamp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_icmp_orig_timestamp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_icmp_sequence_number)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_icmp_identifier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_icmp_pointer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_icmp_checksum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_icmp_unused)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_ip
            // 
            this.groupBox_ip.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                      this.label51,
                                                                                      this.checkBox_ip_src_random,
                                                                                      this.numericUpDown_ip_ttl_min,
                                                                                      this.checkBox_ip_identification_random,
                                                                                      this.checkBox_ip_ttl_random,
                                                                                      this.label5,
                                                                                      this.textBox_ip_options_padding,
                                                                                      this.textBox_ip_dest,
                                                                                      this.textBox_ip_src,
                                                                                      this.numericUpDown_ip_header_length,
                                                                                      this.checkBox_ip_IHL_auto,
                                                                                      this.checkBox_ip_total_length_auto,
                                                                                      this.numericUpDown_ip_header_checksum,
                                                                                      this.numericUpDown_ip_protocol,
                                                                                      this.numericUpDown_ip_ttl,
                                                                                      this.numericUpDown_ip_fragment_offset,
                                                                                      this.label23,
                                                                                      this.label22,
                                                                                      this.numericUpDown_ip_identification,
                                                                                      this.numericUpDown_ip_total_length,
                                                                                      this.label21,
                                                                                      this.label20,
                                                                                      this.label19,
                                                                                      this.label18,
                                                                                      this.numericUpDown_ip_version,
                                                                                      this.label16,
                                                                                      this.label15,
                                                                                      this.label14,
                                                                                      this.checkBox_ip_header_checksum_auto,
                                                                                      this.label13,
                                                                                      this.label12,
                                                                                      this.label11,
                                                                                      this.label10,
                                                                                      this.comboBox_ip_flags_fragment_pos,
                                                                                      this.comboBox_ip_flags_fragment_type,
                                                                                      this.numericUpDown_ip_flag_bit0,
                                                                                      this.numericUpDown_ip_tos_bit7,
                                                                                      this.comboBox_ip_tos_relibility,
                                                                                      this.numericUpDown_ip_tos_bit6,
                                                                                      this.comboBox_ip_tos_throughtput,
                                                                                      this.comboBox_ip_tos_delay,
                                                                                      this.comboBox_ip_tos_precedence,
                                                                                      this.label9,
                                                                                      this.label8,
                                                                                      this.label7,
                                                                                      this.label6,
                                                                                      this.label4,
                                                                                      this.label3,
                                                                                      this.label2});
            this.groupBox_ip.Name = "groupBox_ip";
            this.groupBox_ip.Size = new System.Drawing.Size(832, 200);
            this.groupBox_ip.TabIndex = 0;
            this.groupBox_ip.TabStop = false;
            this.groupBox_ip.Text = "Ip header";
            // 
            // label51
            // 
            this.label51.Location = new System.Drawing.Point(632, 176);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(100, 16);
            this.label51.TabIndex = 108;
            this.label51.Text = "(in hexa)";
            // 
            // checkBox_ip_src_random
            // 
            this.checkBox_ip_src_random.Location = new System.Drawing.Point(224, 152);
            this.checkBox_ip_src_random.Name = "checkBox_ip_src_random";
            this.checkBox_ip_src_random.Size = new System.Drawing.Size(72, 16);
            this.checkBox_ip_src_random.TabIndex = 107;
            this.checkBox_ip_src_random.Text = "Random";
            // 
            // numericUpDown_ip_ttl_min
            // 
            this.numericUpDown_ip_ttl_min.Location = new System.Drawing.Point(264, 128);
            this.numericUpDown_ip_ttl_min.Maximum = new System.Decimal(new int[] {
                                                                                     255,
                                                                                     0,
                                                                                     0,
                                                                                     0});
            this.numericUpDown_ip_ttl_min.Name = "numericUpDown_ip_ttl_min";
            this.numericUpDown_ip_ttl_min.Size = new System.Drawing.Size(48, 20);
            this.numericUpDown_ip_ttl_min.TabIndex = 106;
            // 
            // checkBox_ip_identification_random
            // 
            this.checkBox_ip_identification_random.Location = new System.Drawing.Point(208, 96);
            this.checkBox_ip_identification_random.Name = "checkBox_ip_identification_random";
            this.checkBox_ip_identification_random.Size = new System.Drawing.Size(72, 16);
            this.checkBox_ip_identification_random.TabIndex = 105;
            this.checkBox_ip_identification_random.Text = "Random";
            // 
            // checkBox_ip_ttl_random
            // 
            this.checkBox_ip_ttl_random.Location = new System.Drawing.Point(176, 128);
            this.checkBox_ip_ttl_random.Name = "checkBox_ip_ttl_random";
            this.checkBox_ip_ttl_random.Size = new System.Drawing.Size(96, 16);
            this.checkBox_ip_ttl_random.TabIndex = 104;
            this.checkBox_ip_ttl_random.Text = "Random  Min";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(616, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "Total Length (in octet)";
            // 
            // textBox_ip_options_padding
            // 
            this.textBox_ip_options_padding.Location = new System.Drawing.Point(120, 176);
            this.textBox_ip_options_padding.Name = "textBox_ip_options_padding";
            this.textBox_ip_options_padding.Size = new System.Drawing.Size(512, 20);
            this.textBox_ip_options_padding.TabIndex = 24;
            this.textBox_ip_options_padding.Text = "";
            // 
            // textBox_ip_dest
            // 
            this.textBox_ip_dest.Location = new System.Drawing.Point(432, 152);
            this.textBox_ip_dest.Name = "textBox_ip_dest";
            this.textBox_ip_dest.TabIndex = 22;
            this.textBox_ip_dest.Text = "10.0.0.2";
            // 
            // textBox_ip_src
            // 
            this.textBox_ip_src.Location = new System.Drawing.Point(120, 152);
            this.textBox_ip_src.Name = "textBox_ip_src";
            this.textBox_ip_src.TabIndex = 21;
            this.textBox_ip_src.Text = "10.0.0.1";
            // 
            // numericUpDown_ip_header_length
            // 
            this.numericUpDown_ip_header_length.Location = new System.Drawing.Point(304, 24);
            this.numericUpDown_ip_header_length.Maximum = new System.Decimal(new int[] {
                                                                                           15,
                                                                                           0,
                                                                                           0,
                                                                                           0});
            this.numericUpDown_ip_header_length.Name = "numericUpDown_ip_header_length";
            this.numericUpDown_ip_header_length.Size = new System.Drawing.Size(37, 20);
            this.numericUpDown_ip_header_length.TabIndex = 3;
            this.numericUpDown_ip_header_length.Value = new System.Decimal(new int[] {
                                                                                         5,
                                                                                         0,
                                                                                         0,
                                                                                         0});
            // 
            // checkBox_ip_IHL_auto
            // 
            this.checkBox_ip_IHL_auto.Checked = true;
            this.checkBox_ip_IHL_auto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_ip_IHL_auto.Location = new System.Drawing.Point(304, 8);
            this.checkBox_ip_IHL_auto.Name = "checkBox_ip_IHL_auto";
            this.checkBox_ip_IHL_auto.Size = new System.Drawing.Size(56, 16);
            this.checkBox_ip_IHL_auto.TabIndex = 2;
            this.checkBox_ip_IHL_auto.Text = "auto";
            // 
            // checkBox_ip_total_length_auto
            // 
            this.checkBox_ip_total_length_auto.Checked = true;
            this.checkBox_ip_total_length_auto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_ip_total_length_auto.Location = new System.Drawing.Point(736, 40);
            this.checkBox_ip_total_length_auto.Name = "checkBox_ip_total_length_auto";
            this.checkBox_ip_total_length_auto.Size = new System.Drawing.Size(56, 16);
            this.checkBox_ip_total_length_auto.TabIndex = 10;
            this.checkBox_ip_total_length_auto.Text = "auto";
            // 
            // numericUpDown_ip_header_checksum
            // 
            this.numericUpDown_ip_header_checksum.Location = new System.Drawing.Point(680, 136);
            this.numericUpDown_ip_header_checksum.Maximum = new System.Decimal(new int[] {
                                                                                             65535,
                                                                                             0,
                                                                                             0,
                                                                                             0});
            this.numericUpDown_ip_header_checksum.Name = "numericUpDown_ip_header_checksum";
            this.numericUpDown_ip_header_checksum.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown_ip_header_checksum.TabIndex = 20;
            // 
            // numericUpDown_ip_protocol
            // 
            this.numericUpDown_ip_protocol.Location = new System.Drawing.Point(520, 128);
            this.numericUpDown_ip_protocol.Maximum = new System.Decimal(new int[] {
                                                                                      255,
                                                                                      0,
                                                                                      0,
                                                                                      0});
            this.numericUpDown_ip_protocol.Name = "numericUpDown_ip_protocol";
            this.numericUpDown_ip_protocol.Size = new System.Drawing.Size(48, 20);
            this.numericUpDown_ip_protocol.TabIndex = 18;
            this.numericUpDown_ip_protocol.Value = new System.Decimal(new int[] {
                                                                                    6,
                                                                                    0,
                                                                                    0,
                                                                                    0});
            this.numericUpDown_ip_protocol.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_ip_protocol_KeyUp);
            this.numericUpDown_ip_protocol.ValueChanged += new System.EventHandler(this.numericUpDown_ip_protocol_ValueChanged);
            // 
            // numericUpDown_ip_ttl
            // 
            this.numericUpDown_ip_ttl.Location = new System.Drawing.Point(120, 128);
            this.numericUpDown_ip_ttl.Maximum = new System.Decimal(new int[] {
                                                                                 255,
                                                                                 0,
                                                                                 0,
                                                                                 0});
            this.numericUpDown_ip_ttl.Name = "numericUpDown_ip_ttl";
            this.numericUpDown_ip_ttl.Size = new System.Drawing.Size(48, 20);
            this.numericUpDown_ip_ttl.TabIndex = 17;
            this.numericUpDown_ip_ttl.Value = new System.Decimal(new int[] {
                                                                               255,
                                                                               0,
                                                                               0,
                                                                               0});
            // 
            // numericUpDown_ip_fragment_offset
            // 
            this.numericUpDown_ip_fragment_offset.Location = new System.Drawing.Point(664, 96);
            this.numericUpDown_ip_fragment_offset.Maximum = new System.Decimal(new int[] {
                                                                                             8191,
                                                                                             0,
                                                                                             0,
                                                                                             0});
            this.numericUpDown_ip_fragment_offset.Name = "numericUpDown_ip_fragment_offset";
            this.numericUpDown_ip_fragment_offset.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown_ip_fragment_offset.TabIndex = 16;
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(552, 80);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(96, 16);
            this.label23.TabIndex = 40;
            this.label23.Text = "Fragment position";
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(448, 80);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(88, 16);
            this.label22.TabIndex = 39;
            this.label22.Text = "Fragment type";
            // 
            // numericUpDown_ip_identification
            // 
            this.numericUpDown_ip_identification.Location = new System.Drawing.Point(120, 96);
            this.numericUpDown_ip_identification.Maximum = new System.Decimal(new int[] {
                                                                                            65535,
                                                                                            0,
                                                                                            0,
                                                                                            0});
            this.numericUpDown_ip_identification.Name = "numericUpDown_ip_identification";
            this.numericUpDown_ip_identification.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown_ip_identification.TabIndex = 12;
            // 
            // numericUpDown_ip_total_length
            // 
            this.numericUpDown_ip_total_length.Location = new System.Drawing.Point(736, 56);
            this.numericUpDown_ip_total_length.Maximum = new System.Decimal(new int[] {
                                                                                          65535,
                                                                                          0,
                                                                                          0,
                                                                                          0});
            this.numericUpDown_ip_total_length.Name = "numericUpDown_ip_total_length";
            this.numericUpDown_ip_total_length.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown_ip_total_length.TabIndex = 11;
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(504, 40);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(120, 16);
            this.label21.TabIndex = 36;
            this.label21.Text = "Reserved (bit 6 and 7)";
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(424, 40);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(80, 16);
            this.label20.TabIndex = 35;
            this.label20.Text = "Relibility";
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(344, 40);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(80, 16);
            this.label19.TabIndex = 34;
            this.label19.Text = "Throughput";
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(264, 40);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(56, 16);
            this.label18.TabIndex = 33;
            this.label18.Text = "Delay";
            // 
            // numericUpDown_ip_version
            // 
            this.numericUpDown_ip_version.Location = new System.Drawing.Point(56, 16);
            this.numericUpDown_ip_version.Maximum = new System.Decimal(new int[] {
                                                                                     15,
                                                                                     0,
                                                                                     0,
                                                                                     0});
            this.numericUpDown_ip_version.Name = "numericUpDown_ip_version";
            this.numericUpDown_ip_version.Size = new System.Drawing.Size(37, 20);
            this.numericUpDown_ip_version.TabIndex = 1;
            this.numericUpDown_ip_version.Value = new System.Decimal(new int[] {
                                                                                   4,
                                                                                   0,
                                                                                   0,
                                                                                   0});
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(8, 176);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(112, 16);
            this.label16.TabIndex = 29;
            this.label16.Text = "Options and padding";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(344, 152);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 16);
            this.label15.TabIndex = 28;
            this.label15.Text = "IP destination";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(8, 152);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 24);
            this.label14.TabIndex = 27;
            this.label14.Text = "IP source";
            // 
            // checkBox_ip_header_checksum_auto
            // 
            this.checkBox_ip_header_checksum_auto.Checked = true;
            this.checkBox_ip_header_checksum_auto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_ip_header_checksum_auto.Location = new System.Drawing.Point(680, 120);
            this.checkBox_ip_header_checksum_auto.Name = "checkBox_ip_header_checksum_auto";
            this.checkBox_ip_header_checksum_auto.Size = new System.Drawing.Size(56, 16);
            this.checkBox_ip_header_checksum_auto.TabIndex = 19;
            this.checkBox_ip_header_checksum_auto.Text = "auto";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(576, 128);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 16);
            this.label13.TabIndex = 25;
            this.label13.Text = "Header checksum";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(344, 128);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(176, 16);
            this.label12.TabIndex = 24;
            this.label12.Text = "Protocol (icmp=1, tcp=6, udp=17)";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(8, 128);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 16);
            this.label11.TabIndex = 23;
            this.label11.Text = "Time to Live";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(656, 80);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(160, 16);
            this.label10.TabIndex = 22;
            this.label10.Text = "Fragment Offset (8 bytes unit)";
            // 
            // comboBox_ip_flags_fragment_pos
            // 
            this.comboBox_ip_flags_fragment_pos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ip_flags_fragment_pos.Items.AddRange(new object[] {
                                                                                "0 = Last Fragment",
                                                                                "1 = More Fragments"});
            this.comboBox_ip_flags_fragment_pos.Location = new System.Drawing.Point(552, 96);
            this.comboBox_ip_flags_fragment_pos.Name = "comboBox_ip_flags_fragment_pos";
            this.comboBox_ip_flags_fragment_pos.Size = new System.Drawing.Size(112, 21);
            this.comboBox_ip_flags_fragment_pos.TabIndex = 15;
            // 
            // comboBox_ip_flags_fragment_type
            // 
            this.comboBox_ip_flags_fragment_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ip_flags_fragment_type.Items.AddRange(new object[] {
                                                                                 "0 = May Fragment",
                                                                                 "1 = Don\'t Fragment"});
            this.comboBox_ip_flags_fragment_type.Location = new System.Drawing.Point(440, 96);
            this.comboBox_ip_flags_fragment_type.Name = "comboBox_ip_flags_fragment_type";
            this.comboBox_ip_flags_fragment_type.Size = new System.Drawing.Size(112, 21);
            this.comboBox_ip_flags_fragment_type.TabIndex = 14;
            // 
            // numericUpDown_ip_flag_bit0
            // 
            this.numericUpDown_ip_flag_bit0.Location = new System.Drawing.Point(384, 96);
            this.numericUpDown_ip_flag_bit0.Maximum = new System.Decimal(new int[] {
                                                                                       1,
                                                                                       0,
                                                                                       0,
                                                                                       0});
            this.numericUpDown_ip_flag_bit0.Name = "numericUpDown_ip_flag_bit0";
            this.numericUpDown_ip_flag_bit0.Size = new System.Drawing.Size(32, 20);
            this.numericUpDown_ip_flag_bit0.TabIndex = 13;
            // 
            // numericUpDown_ip_tos_bit7
            // 
            this.numericUpDown_ip_tos_bit7.Location = new System.Drawing.Point(552, 56);
            this.numericUpDown_ip_tos_bit7.Maximum = new System.Decimal(new int[] {
                                                                                      1,
                                                                                      0,
                                                                                      0,
                                                                                      0});
            this.numericUpDown_ip_tos_bit7.Name = "numericUpDown_ip_tos_bit7";
            this.numericUpDown_ip_tos_bit7.Size = new System.Drawing.Size(32, 20);
            this.numericUpDown_ip_tos_bit7.TabIndex = 9;
            // 
            // comboBox_ip_tos_relibility
            // 
            this.comboBox_ip_tos_relibility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ip_tos_relibility.Items.AddRange(new object[] {
                                                                            "0 = Normal",
                                                                            "1 = High"});
            this.comboBox_ip_tos_relibility.Location = new System.Drawing.Point(424, 56);
            this.comboBox_ip_tos_relibility.Name = "comboBox_ip_tos_relibility";
            this.comboBox_ip_tos_relibility.Size = new System.Drawing.Size(80, 21);
            this.comboBox_ip_tos_relibility.TabIndex = 7;
            // 
            // numericUpDown_ip_tos_bit6
            // 
            this.numericUpDown_ip_tos_bit6.Location = new System.Drawing.Point(512, 56);
            this.numericUpDown_ip_tos_bit6.Maximum = new System.Decimal(new int[] {
                                                                                      1,
                                                                                      0,
                                                                                      0,
                                                                                      0});
            this.numericUpDown_ip_tos_bit6.Name = "numericUpDown_ip_tos_bit6";
            this.numericUpDown_ip_tos_bit6.Size = new System.Drawing.Size(32, 20);
            this.numericUpDown_ip_tos_bit6.TabIndex = 8;
            // 
            // comboBox_ip_tos_throughtput
            // 
            this.comboBox_ip_tos_throughtput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ip_tos_throughtput.Items.AddRange(new object[] {
                                                                             "0 = Normal",
                                                                             "1 = High"});
            this.comboBox_ip_tos_throughtput.Location = new System.Drawing.Point(344, 56);
            this.comboBox_ip_tos_throughtput.Name = "comboBox_ip_tos_throughtput";
            this.comboBox_ip_tos_throughtput.Size = new System.Drawing.Size(80, 21);
            this.comboBox_ip_tos_throughtput.TabIndex = 6;
            // 
            // comboBox_ip_tos_delay
            // 
            this.comboBox_ip_tos_delay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ip_tos_delay.Items.AddRange(new object[] {
                                                                       "0 = Normal",
                                                                       "1 = Low"});
            this.comboBox_ip_tos_delay.Location = new System.Drawing.Point(264, 56);
            this.comboBox_ip_tos_delay.Name = "comboBox_ip_tos_delay";
            this.comboBox_ip_tos_delay.Size = new System.Drawing.Size(80, 21);
            this.comboBox_ip_tos_delay.TabIndex = 5;
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
            this.comboBox_ip_tos_precedence.Location = new System.Drawing.Point(120, 56);
            this.comboBox_ip_tos_precedence.Name = "comboBox_ip_tos_precedence";
            this.comboBox_ip_tos_precedence.Size = new System.Drawing.Size(144, 21);
            this.comboBox_ip_tos_precedence.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(120, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 16);
            this.label9.TabIndex = 12;
            this.label9.Text = "Precedence";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(368, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 16);
            this.label8.TabIndex = 9;
            this.label8.Text = "Reserved (bit 0)";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(344, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 16);
            this.label7.TabIndex = 8;
            this.label7.Text = "Flags";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "Identification";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 48);
            this.label4.Name = "label4";
            this.label4.TabIndex = 5;
            this.label4.Text = "Type of Service";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(120, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(192, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Internet Header Length (32bits unit)";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Vesion";
            // 
            // groupBox_data
            // 
            this.groupBox_data.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                        this.button_stop,
                                                                                        this.button_send,
                                                                                        this.checkBox_data_looping_packets,
                                                                                        this.numericUpDown_data_number_of_packets,
                                                                                        this.label38,
                                                                                        this.label37,
                                                                                        this.textBox_data_value,
                                                                                        this.checkBox_data_value_hexa});
            this.groupBox_data.Location = new System.Drawing.Point(0, 712);
            this.groupBox_data.Name = "groupBox_data";
            this.groupBox_data.Size = new System.Drawing.Size(832, 104);
            this.groupBox_data.TabIndex = 1;
            this.groupBox_data.TabStop = false;
            this.groupBox_data.Text = "Data";
            // 
            // button_stop
            // 
            this.button_stop.Location = new System.Drawing.Point(736, 72);
            this.button_stop.Name = "button_stop";
            this.button_stop.TabIndex = 32;
            this.button_stop.Text = "Stop";
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // button_send
            // 
            this.button_send.Location = new System.Drawing.Point(648, 72);
            this.button_send.Name = "button_send";
            this.button_send.TabIndex = 31;
            this.button_send.Text = "Send";
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // checkBox_data_looping_packets
            // 
            this.checkBox_data_looping_packets.Location = new System.Drawing.Point(168, 72);
            this.checkBox_data_looping_packets.Name = "checkBox_data_looping_packets";
            this.checkBox_data_looping_packets.Size = new System.Drawing.Size(72, 16);
            this.checkBox_data_looping_packets.TabIndex = 30;
            this.checkBox_data_looping_packets.Text = "Looping";
            // 
            // numericUpDown_data_number_of_packets
            // 
            this.numericUpDown_data_number_of_packets.Location = new System.Drawing.Point(120, 72);
            this.numericUpDown_data_number_of_packets.Maximum = new System.Decimal(new int[] {
                                                                                                 500,
                                                                                                 0,
                                                                                                 0,
                                                                                                 0});
            this.numericUpDown_data_number_of_packets.Minimum = new System.Decimal(new int[] {
                                                                                                 1,
                                                                                                 0,
                                                                                                 0,
                                                                                                 0});
            this.numericUpDown_data_number_of_packets.Name = "numericUpDown_data_number_of_packets";
            this.numericUpDown_data_number_of_packets.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_data_number_of_packets.TabIndex = 29;
            this.numericUpDown_data_number_of_packets.Value = new System.Decimal(new int[] {
                                                                                               1,
                                                                                               0,
                                                                                               0,
                                                                                               0});
            // 
            // label38
            // 
            this.label38.Location = new System.Drawing.Point(16, 24);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(88, 16);
            this.label38.TabIndex = 28;
            this.label38.Text = "Data";
            // 
            // label37
            // 
            this.label37.Location = new System.Drawing.Point(16, 72);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(100, 16);
            this.label37.TabIndex = 27;
            this.label37.Text = "Number of packets";
            // 
            // textBox_data_value
            // 
            this.textBox_data_value.AcceptsReturn = true;
            this.textBox_data_value.AcceptsTab = true;
            this.textBox_data_value.Location = new System.Drawing.Point(120, 24);
            this.textBox_data_value.Multiline = true;
            this.textBox_data_value.Name = "textBox_data_value";
            this.textBox_data_value.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_data_value.Size = new System.Drawing.Size(504, 48);
            this.textBox_data_value.TabIndex = 26;
            this.textBox_data_value.Text = "";
            // 
            // checkBox_data_value_hexa
            // 
            this.checkBox_data_value_hexa.Checked = true;
            this.checkBox_data_value_hexa.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_data_value_hexa.Location = new System.Drawing.Point(632, 24);
            this.checkBox_data_value_hexa.Name = "checkBox_data_value_hexa";
            this.checkBox_data_value_hexa.Size = new System.Drawing.Size(96, 16);
            this.checkBox_data_value_hexa.TabIndex = 25;
            this.checkBox_data_value_hexa.Text = "Hexa values";
            this.checkBox_data_value_hexa.CheckedChanged += new System.EventHandler(this.checkBox_data_value_hexa_CheckedChanged);
            // 
            // groupBox_tcp
            // 
            this.groupBox_tcp.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                       this.checkBox_tcp_packet_details,
                                                                                       this.panel_connection_helper,
                                                                                       this.checkBox_tcp_data_offset_auto,
                                                                                       this.label52,
                                                                                       this.checkBox_tcp_connection_helper,
                                                                                       this.checkBox_tcp_sequence_number_random,
                                                                                       this.checkBox_tcp_src_port_random,
                                                                                       this.numericUpDown_tcp_urgent_pointer,
                                                                                       this.label32,
                                                                                       this.numericUpDown_tcp_header_checksum,
                                                                                       this.checkBox_tcp_header_checksum_auto,
                                                                                       this.label31,
                                                                                       this.numericUpDown_tcp_reserved,
                                                                                       this.label30,
                                                                                       this.textBox_tcp_options_padding,
                                                                                       this.label29,
                                                                                       this.numericUpDown_tcp_port_source,
                                                                                       this.numericUpDown_tcp_port_dest,
                                                                                       this.label28,
                                                                                       this.label27,
                                                                                       this.label26,
                                                                                       this.label25,
                                                                                       this.label24,
                                                                                       this.label17,
                                                                                       this.label1,
                                                                                       this.checkBox_tcp_fin,
                                                                                       this.checkBox_tcp_syn,
                                                                                       this.checkBox_tcp_rst,
                                                                                       this.checkBox_tcp_push,
                                                                                       this.checkBox_tcp_ack,
                                                                                       this.checkBox_tcp_urg,
                                                                                       this.numericUpDown_tcp_window,
                                                                                       this.numericUpDown_tcp_data_offset,
                                                                                       this.numericUpDown_tcp_ack_num,
                                                                                       this.numericUpDown_tcp_seq_num});
            this.groupBox_tcp.Location = new System.Drawing.Point(0, 200);
            this.groupBox_tcp.Name = "groupBox_tcp";
            this.groupBox_tcp.Size = new System.Drawing.Size(832, 272);
            this.groupBox_tcp.TabIndex = 28;
            this.groupBox_tcp.TabStop = false;
            this.groupBox_tcp.Text = "Tcp Header";
            // 
            // checkBox_tcp_packet_details
            // 
            this.checkBox_tcp_packet_details.Location = new System.Drawing.Point(688, 112);
            this.checkBox_tcp_packet_details.Name = "checkBox_tcp_packet_details";
            this.checkBox_tcp_packet_details.Size = new System.Drawing.Size(112, 16);
            this.checkBox_tcp_packet_details.TabIndex = 112;
            this.checkBox_tcp_packet_details.Text = "Packet details";
            // 
            // panel_connection_helper
            // 
            this.panel_connection_helper.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                                  this.label40,
                                                                                                  this.label53,
                                                                                                  this.label_tcp_connection_state,
                                                                                                  this.userControlPacketsView});
            this.panel_connection_helper.Location = new System.Drawing.Point(8, 160);
            this.panel_connection_helper.Name = "panel_connection_helper";
            this.panel_connection_helper.Size = new System.Drawing.Size(816, 104);
            this.panel_connection_helper.TabIndex = 111;
            // 
            // label40
            // 
            this.label40.Location = new System.Drawing.Point(240, 8);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(416, 16);
            this.label40.TabIndex = 4;
            this.label40.Text = "Connection State  (or connection state after the send of auto-filled packet)";
            this.label40.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label53
            // 
            this.label53.Location = new System.Drawing.Point(0, 8);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(100, 16);
            this.label53.TabIndex = 3;
            this.label53.Text = "Recieved Packets";
            // 
            // label_tcp_connection_state
            // 
            this.label_tcp_connection_state.Location = new System.Drawing.Point(664, 8);
            this.label_tcp_connection_state.Name = "label_tcp_connection_state";
            this.label_tcp_connection_state.Size = new System.Drawing.Size(144, 16);
            this.label_tcp_connection_state.TabIndex = 2;
            // 
            // userControlPacketsView
            // 
            this.userControlPacketsView.Location = new System.Drawing.Point(2, 24);
            this.userControlPacketsView.Name = "userControlPacketsView";
            this.userControlPacketsView.Size = new System.Drawing.Size(812, 80);
            this.userControlPacketsView.TabIndex = 0;
            // 
            // checkBox_tcp_data_offset_auto
            // 
            this.checkBox_tcp_data_offset_auto.Checked = true;
            this.checkBox_tcp_data_offset_auto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_tcp_data_offset_auto.Location = new System.Drawing.Point(240, 56);
            this.checkBox_tcp_data_offset_auto.Name = "checkBox_tcp_data_offset_auto";
            this.checkBox_tcp_data_offset_auto.Size = new System.Drawing.Size(56, 16);
            this.checkBox_tcp_data_offset_auto.TabIndex = 110;
            this.checkBox_tcp_data_offset_auto.Text = "auto";
            // 
            // label52
            // 
            this.label52.Location = new System.Drawing.Point(712, 136);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(72, 16);
            this.label52.TabIndex = 109;
            this.label52.Text = "(in hexa)";
            // 
            // checkBox_tcp_connection_helper
            // 
            this.checkBox_tcp_connection_helper.AccessibleDescription = "";
            this.checkBox_tcp_connection_helper.Location = new System.Drawing.Point(656, 88);
            this.checkBox_tcp_connection_helper.Name = "checkBox_tcp_connection_helper";
            this.checkBox_tcp_connection_helper.Size = new System.Drawing.Size(144, 16);
            this.checkBox_tcp_connection_helper.TabIndex = 104;
            this.checkBox_tcp_connection_helper.Tag = "";
            this.checkBox_tcp_connection_helper.Text = "Use Connection Helper";
            this.checkBox_tcp_connection_helper.CheckedChanged += new System.EventHandler(this.checkBox_tcp_connection_helper_CheckedChanged);
            // 
            // checkBox_tcp_sequence_number_random
            // 
            this.checkBox_tcp_sequence_number_random.Location = new System.Drawing.Point(328, 32);
            this.checkBox_tcp_sequence_number_random.Name = "checkBox_tcp_sequence_number_random";
            this.checkBox_tcp_sequence_number_random.Size = new System.Drawing.Size(72, 16);
            this.checkBox_tcp_sequence_number_random.TabIndex = 103;
            this.checkBox_tcp_sequence_number_random.Text = "Random";
            // 
            // checkBox_tcp_src_port_random
            // 
            this.checkBox_tcp_src_port_random.Location = new System.Drawing.Point(280, 8);
            this.checkBox_tcp_src_port_random.Name = "checkBox_tcp_src_port_random";
            this.checkBox_tcp_src_port_random.Size = new System.Drawing.Size(72, 16);
            this.checkBox_tcp_src_port_random.TabIndex = 102;
            this.checkBox_tcp_src_port_random.Text = "Random";
            // 
            // numericUpDown_tcp_urgent_pointer
            // 
            this.numericUpDown_tcp_urgent_pointer.Location = new System.Drawing.Point(552, 104);
            this.numericUpDown_tcp_urgent_pointer.Maximum = new System.Decimal(new int[] {
                                                                                             65535,
                                                                                             0,
                                                                                             0,
                                                                                             0});
            this.numericUpDown_tcp_urgent_pointer.Name = "numericUpDown_tcp_urgent_pointer";
            this.numericUpDown_tcp_urgent_pointer.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown_tcp_urgent_pointer.TabIndex = 101;
            // 
            // label32
            // 
            this.label32.Location = new System.Drawing.Point(456, 104);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(100, 16);
            this.label32.TabIndex = 100;
            this.label32.Text = "Urgent pointer";
            // 
            // numericUpDown_tcp_header_checksum
            // 
            this.numericUpDown_tcp_header_checksum.Location = new System.Drawing.Point(376, 112);
            this.numericUpDown_tcp_header_checksum.Maximum = new System.Decimal(new int[] {
                                                                                              65535,
                                                                                              0,
                                                                                              0,
                                                                                              0});
            this.numericUpDown_tcp_header_checksum.Name = "numericUpDown_tcp_header_checksum";
            this.numericUpDown_tcp_header_checksum.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown_tcp_header_checksum.TabIndex = 98;
            // 
            // checkBox_tcp_header_checksum_auto
            // 
            this.checkBox_tcp_header_checksum_auto.Checked = true;
            this.checkBox_tcp_header_checksum_auto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_tcp_header_checksum_auto.Location = new System.Drawing.Point(376, 96);
            this.checkBox_tcp_header_checksum_auto.Name = "checkBox_tcp_header_checksum_auto";
            this.checkBox_tcp_header_checksum_auto.Size = new System.Drawing.Size(56, 16);
            this.checkBox_tcp_header_checksum_auto.TabIndex = 97;
            this.checkBox_tcp_header_checksum_auto.Text = "auto";
            // 
            // label31
            // 
            this.label31.Location = new System.Drawing.Point(272, 104);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(100, 16);
            this.label31.TabIndex = 99;
            this.label31.Text = "Header checksum";
            // 
            // numericUpDown_tcp_reserved
            // 
            this.numericUpDown_tcp_reserved.Location = new System.Drawing.Point(600, 56);
            this.numericUpDown_tcp_reserved.Maximum = new System.Decimal(new int[] {
                                                                                       63,
                                                                                       0,
                                                                                       0,
                                                                                       0});
            this.numericUpDown_tcp_reserved.Name = "numericUpDown_tcp_reserved";
            this.numericUpDown_tcp_reserved.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_tcp_reserved.TabIndex = 96;
            // 
            // label30
            // 
            this.label30.Location = new System.Drawing.Point(456, 56);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(100, 16);
            this.label30.TabIndex = 95;
            this.label30.Text = "Reserved";
            // 
            // textBox_tcp_options_padding
            // 
            this.textBox_tcp_options_padding.Location = new System.Drawing.Point(200, 136);
            this.textBox_tcp_options_padding.Name = "textBox_tcp_options_padding";
            this.textBox_tcp_options_padding.Size = new System.Drawing.Size(512, 20);
            this.textBox_tcp_options_padding.TabIndex = 93;
            this.textBox_tcp_options_padding.Text = "";
            // 
            // label29
            // 
            this.label29.Location = new System.Drawing.Point(88, 136);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(112, 16);
            this.label29.TabIndex = 94;
            this.label29.Text = "Options and padding";
            // 
            // numericUpDown_tcp_port_source
            // 
            this.numericUpDown_tcp_port_source.Location = new System.Drawing.Point(200, 8);
            this.numericUpDown_tcp_port_source.Maximum = new System.Decimal(new int[] {
                                                                                          65535,
                                                                                          0,
                                                                                          0,
                                                                                          0});
            this.numericUpDown_tcp_port_source.Name = "numericUpDown_tcp_port_source";
            this.numericUpDown_tcp_port_source.Size = new System.Drawing.Size(72, 20);
            this.numericUpDown_tcp_port_source.TabIndex = 91;
            // 
            // numericUpDown_tcp_port_dest
            // 
            this.numericUpDown_tcp_port_dest.Location = new System.Drawing.Point(600, 8);
            this.numericUpDown_tcp_port_dest.Maximum = new System.Decimal(new int[] {
                                                                                        65535,
                                                                                        0,
                                                                                        0,
                                                                                        0});
            this.numericUpDown_tcp_port_dest.Name = "numericUpDown_tcp_port_dest";
            this.numericUpDown_tcp_port_dest.Size = new System.Drawing.Size(72, 20);
            this.numericUpDown_tcp_port_dest.TabIndex = 90;
            // 
            // label28
            // 
            this.label28.Location = new System.Drawing.Point(88, 80);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(100, 16);
            this.label28.TabIndex = 89;
            this.label28.Text = "Control options";
            // 
            // label27
            // 
            this.label27.Location = new System.Drawing.Point(88, 104);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(100, 16);
            this.label27.TabIndex = 88;
            this.label27.Text = "Window";
            // 
            // label26
            // 
            this.label26.Location = new System.Drawing.Point(88, 56);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(100, 16);
            this.label26.TabIndex = 87;
            this.label26.Text = "Data Offset";
            // 
            // label25
            // 
            this.label25.Location = new System.Drawing.Point(456, 32);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(144, 16);
            this.label25.TabIndex = 86;
            this.label25.Text = "Acknowledgment Number";
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(88, 32);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(100, 16);
            this.label24.TabIndex = 85;
            this.label24.Text = "Sequence Number";
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(88, 8);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(100, 16);
            this.label17.TabIndex = 84;
            this.label17.Text = "Source Port";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(456, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 83;
            this.label1.Text = "Destination Port";
            // 
            // checkBox_tcp_fin
            // 
            this.checkBox_tcp_fin.Location = new System.Drawing.Point(520, 80);
            this.checkBox_tcp_fin.Name = "checkBox_tcp_fin";
            this.checkBox_tcp_fin.Size = new System.Drawing.Size(64, 16);
            this.checkBox_tcp_fin.TabIndex = 82;
            this.checkBox_tcp_fin.Text = "FIN";
            // 
            // checkBox_tcp_syn
            // 
            this.checkBox_tcp_syn.Location = new System.Drawing.Point(456, 80);
            this.checkBox_tcp_syn.Name = "checkBox_tcp_syn";
            this.checkBox_tcp_syn.Size = new System.Drawing.Size(72, 16);
            this.checkBox_tcp_syn.TabIndex = 81;
            this.checkBox_tcp_syn.Text = "SYN";
            // 
            // checkBox_tcp_rst
            // 
            this.checkBox_tcp_rst.Location = new System.Drawing.Point(400, 80);
            this.checkBox_tcp_rst.Name = "checkBox_tcp_rst";
            this.checkBox_tcp_rst.Size = new System.Drawing.Size(56, 16);
            this.checkBox_tcp_rst.TabIndex = 80;
            this.checkBox_tcp_rst.Text = "RST";
            // 
            // checkBox_tcp_push
            // 
            this.checkBox_tcp_push.Location = new System.Drawing.Point(336, 80);
            this.checkBox_tcp_push.Name = "checkBox_tcp_push";
            this.checkBox_tcp_push.Size = new System.Drawing.Size(64, 16);
            this.checkBox_tcp_push.TabIndex = 79;
            this.checkBox_tcp_push.Text = "PSH";
            // 
            // checkBox_tcp_ack
            // 
            this.checkBox_tcp_ack.Location = new System.Drawing.Point(264, 80);
            this.checkBox_tcp_ack.Name = "checkBox_tcp_ack";
            this.checkBox_tcp_ack.Size = new System.Drawing.Size(72, 16);
            this.checkBox_tcp_ack.TabIndex = 78;
            this.checkBox_tcp_ack.Text = "ACK";
            // 
            // checkBox_tcp_urg
            // 
            this.checkBox_tcp_urg.Location = new System.Drawing.Point(200, 80);
            this.checkBox_tcp_urg.Name = "checkBox_tcp_urg";
            this.checkBox_tcp_urg.Size = new System.Drawing.Size(64, 16);
            this.checkBox_tcp_urg.TabIndex = 77;
            this.checkBox_tcp_urg.Text = "URG";
            // 
            // numericUpDown_tcp_window
            // 
            this.numericUpDown_tcp_window.Location = new System.Drawing.Point(200, 104);
            this.numericUpDown_tcp_window.Maximum = new System.Decimal(new int[] {
                                                                                     65535,
                                                                                     0,
                                                                                     0,
                                                                                     0});
            this.numericUpDown_tcp_window.Name = "numericUpDown_tcp_window";
            this.numericUpDown_tcp_window.Size = new System.Drawing.Size(56, 20);
            this.numericUpDown_tcp_window.TabIndex = 74;
            this.numericUpDown_tcp_window.Value = new System.Decimal(new int[] {
                                                                                   4096,
                                                                                   0,
                                                                                   0,
                                                                                   0});
            // 
            // numericUpDown_tcp_data_offset
            // 
            this.numericUpDown_tcp_data_offset.Location = new System.Drawing.Point(200, 56);
            this.numericUpDown_tcp_data_offset.Maximum = new System.Decimal(new int[] {
                                                                                          15,
                                                                                          0,
                                                                                          0,
                                                                                          0});
            this.numericUpDown_tcp_data_offset.Name = "numericUpDown_tcp_data_offset";
            this.numericUpDown_tcp_data_offset.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_tcp_data_offset.TabIndex = 72;
            // 
            // numericUpDown_tcp_ack_num
            // 
            this.numericUpDown_tcp_ack_num.Location = new System.Drawing.Point(600, 32);
            this.numericUpDown_tcp_ack_num.Maximum = new System.Decimal(new int[] {
                                                                                      -1,
                                                                                      0,
                                                                                      0,
                                                                                      0});
            this.numericUpDown_tcp_ack_num.Name = "numericUpDown_tcp_ack_num";
            this.numericUpDown_tcp_ack_num.TabIndex = 71;
            // 
            // numericUpDown_tcp_seq_num
            // 
            this.numericUpDown_tcp_seq_num.Location = new System.Drawing.Point(200, 32);
            this.numericUpDown_tcp_seq_num.Maximum = new System.Decimal(new int[] {
                                                                                      -1,
                                                                                      0,
                                                                                      0,
                                                                                      0});
            this.numericUpDown_tcp_seq_num.Name = "numericUpDown_tcp_seq_num";
            this.numericUpDown_tcp_seq_num.TabIndex = 70;
            // 
            // groupBox_udp
            // 
            this.groupBox_udp.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                       this.label36,
                                                                                       this.checkBox_udp_length_auto,
                                                                                       this.numericUpDown_udp_length,
                                                                                       this.numericUpDown_udp_checksum,
                                                                                       this.checkBox_udp_checksum_auto,
                                                                                       this.label35,
                                                                                       this.checkBox_udp_src_port_random,
                                                                                       this.numericUpDown_udp_src_port,
                                                                                       this.numericUpDown_udp_dest_port,
                                                                                       this.label33,
                                                                                       this.label34});
            this.groupBox_udp.Location = new System.Drawing.Point(0, 480);
            this.groupBox_udp.Name = "groupBox_udp";
            this.groupBox_udp.Size = new System.Drawing.Size(832, 80);
            this.groupBox_udp.TabIndex = 29;
            this.groupBox_udp.TabStop = false;
            this.groupBox_udp.Text = "Udp header";
            // 
            // label36
            // 
            this.label36.Location = new System.Drawing.Point(104, 48);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(100, 16);
            this.label36.TabIndex = 113;
            this.label36.Text = "Length";
            // 
            // checkBox_udp_length_auto
            // 
            this.checkBox_udp_length_auto.Checked = true;
            this.checkBox_udp_length_auto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_udp_length_auto.Location = new System.Drawing.Point(216, 40);
            this.checkBox_udp_length_auto.Name = "checkBox_udp_length_auto";
            this.checkBox_udp_length_auto.Size = new System.Drawing.Size(56, 16);
            this.checkBox_udp_length_auto.TabIndex = 112;
            this.checkBox_udp_length_auto.Text = "auto";
            // 
            // numericUpDown_udp_length
            // 
            this.numericUpDown_udp_length.Location = new System.Drawing.Point(216, 56);
            this.numericUpDown_udp_length.Maximum = new System.Decimal(new int[] {
                                                                                     65535,
                                                                                     0,
                                                                                     0,
                                                                                     0});
            this.numericUpDown_udp_length.Name = "numericUpDown_udp_length";
            this.numericUpDown_udp_length.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown_udp_length.TabIndex = 111;
            // 
            // numericUpDown_udp_checksum
            // 
            this.numericUpDown_udp_checksum.Location = new System.Drawing.Point(616, 56);
            this.numericUpDown_udp_checksum.Maximum = new System.Decimal(new int[] {
                                                                                       65535,
                                                                                       0,
                                                                                       0,
                                                                                       0});
            this.numericUpDown_udp_checksum.Name = "numericUpDown_udp_checksum";
            this.numericUpDown_udp_checksum.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown_udp_checksum.TabIndex = 109;
            // 
            // checkBox_udp_checksum_auto
            // 
            this.checkBox_udp_checksum_auto.Checked = true;
            this.checkBox_udp_checksum_auto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_udp_checksum_auto.Location = new System.Drawing.Point(616, 40);
            this.checkBox_udp_checksum_auto.Name = "checkBox_udp_checksum_auto";
            this.checkBox_udp_checksum_auto.Size = new System.Drawing.Size(56, 16);
            this.checkBox_udp_checksum_auto.TabIndex = 108;
            this.checkBox_udp_checksum_auto.Text = "auto";
            // 
            // label35
            // 
            this.label35.Location = new System.Drawing.Point(472, 48);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(100, 16);
            this.label35.TabIndex = 110;
            this.label35.Text = "Header checksum";
            // 
            // checkBox_udp_src_port_random
            // 
            this.checkBox_udp_src_port_random.Location = new System.Drawing.Point(296, 16);
            this.checkBox_udp_src_port_random.Name = "checkBox_udp_src_port_random";
            this.checkBox_udp_src_port_random.Size = new System.Drawing.Size(72, 16);
            this.checkBox_udp_src_port_random.TabIndex = 107;
            this.checkBox_udp_src_port_random.Text = "Random";
            // 
            // numericUpDown_udp_src_port
            // 
            this.numericUpDown_udp_src_port.Location = new System.Drawing.Point(216, 16);
            this.numericUpDown_udp_src_port.Maximum = new System.Decimal(new int[] {
                                                                                       65535,
                                                                                       0,
                                                                                       0,
                                                                                       0});
            this.numericUpDown_udp_src_port.Name = "numericUpDown_udp_src_port";
            this.numericUpDown_udp_src_port.Size = new System.Drawing.Size(72, 20);
            this.numericUpDown_udp_src_port.TabIndex = 106;
            // 
            // numericUpDown_udp_dest_port
            // 
            this.numericUpDown_udp_dest_port.Location = new System.Drawing.Point(616, 16);
            this.numericUpDown_udp_dest_port.Maximum = new System.Decimal(new int[] {
                                                                                        65535,
                                                                                        0,
                                                                                        0,
                                                                                        0});
            this.numericUpDown_udp_dest_port.Name = "numericUpDown_udp_dest_port";
            this.numericUpDown_udp_dest_port.Size = new System.Drawing.Size(72, 20);
            this.numericUpDown_udp_dest_port.TabIndex = 105;
            // 
            // label33
            // 
            this.label33.Location = new System.Drawing.Point(104, 16);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(100, 16);
            this.label33.TabIndex = 104;
            this.label33.Text = "Source Port";
            // 
            // label34
            // 
            this.label34.Location = new System.Drawing.Point(472, 16);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(96, 16);
            this.label34.TabIndex = 103;
            this.label34.Text = "Destination Port";
            // 
            // groupBox_icmp
            // 
            this.groupBox_icmp.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                        this.textBox_icmp_gateway,
                                                                                        this.numericUpDown_icmp_trans_timestamp,
                                                                                        this.numericUpDown_icmp_recv_timestamp,
                                                                                        this.numericUpDown_icmp_orig_timestamp,
                                                                                        this.label50,
                                                                                        this.label49,
                                                                                        this.label48,
                                                                                        this.numericUpDown_icmp_sequence_number,
                                                                                        this.numericUpDown_icmp_identifier,
                                                                                        this.label47,
                                                                                        this.label46,
                                                                                        this.label45,
                                                                                        this.numericUpDown_icmp_pointer,
                                                                                        this.label44,
                                                                                        this.numericUpDown_icmp_checksum,
                                                                                        this.checkBox_icmp_checksum_auto,
                                                                                        this.label43,
                                                                                        this.numericUpDown_icmp_unused,
                                                                                        this.label42,
                                                                                        this.label41,
                                                                                        this.comboBox_icmp_code,
                                                                                        this.comboBox_icmp_icmp_message,
                                                                                        this.label39});
            this.groupBox_icmp.Location = new System.Drawing.Point(0, 560);
            this.groupBox_icmp.Name = "groupBox_icmp";
            this.groupBox_icmp.Size = new System.Drawing.Size(832, 144);
            this.groupBox_icmp.TabIndex = 30;
            this.groupBox_icmp.TabStop = false;
            this.groupBox_icmp.Text = "Icmp";
            // 
            // textBox_icmp_gateway
            // 
            this.textBox_icmp_gateway.Location = new System.Drawing.Point(464, 72);
            this.textBox_icmp_gateway.Name = "textBox_icmp_gateway";
            this.textBox_icmp_gateway.Size = new System.Drawing.Size(120, 20);
            this.textBox_icmp_gateway.TabIndex = 43;
            this.textBox_icmp_gateway.Text = "";
            // 
            // numericUpDown_icmp_trans_timestamp
            // 
            this.numericUpDown_icmp_trans_timestamp.Location = new System.Drawing.Point(616, 120);
            this.numericUpDown_icmp_trans_timestamp.Maximum = new System.Decimal(new int[] {
                                                                                               -1,
                                                                                               15,
                                                                                               0,
                                                                                               0});
            this.numericUpDown_icmp_trans_timestamp.Name = "numericUpDown_icmp_trans_timestamp";
            this.numericUpDown_icmp_trans_timestamp.TabIndex = 42;
            // 
            // numericUpDown_icmp_recv_timestamp
            // 
            this.numericUpDown_icmp_recv_timestamp.Location = new System.Drawing.Point(360, 120);
            this.numericUpDown_icmp_recv_timestamp.Maximum = new System.Decimal(new int[] {
                                                                                              -1,
                                                                                              15,
                                                                                              0,
                                                                                              0});
            this.numericUpDown_icmp_recv_timestamp.Name = "numericUpDown_icmp_recv_timestamp";
            this.numericUpDown_icmp_recv_timestamp.TabIndex = 41;
            // 
            // numericUpDown_icmp_orig_timestamp
            // 
            this.numericUpDown_icmp_orig_timestamp.Location = new System.Drawing.Point(120, 120);
            this.numericUpDown_icmp_orig_timestamp.Maximum = new System.Decimal(new int[] {
                                                                                              -1,
                                                                                              15,
                                                                                              0,
                                                                                              0});
            this.numericUpDown_icmp_orig_timestamp.Name = "numericUpDown_icmp_orig_timestamp";
            this.numericUpDown_icmp_orig_timestamp.TabIndex = 40;
            // 
            // label50
            // 
            this.label50.Location = new System.Drawing.Point(504, 120);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(112, 16);
            this.label50.TabIndex = 39;
            this.label50.Text = "Transmit Timestamp";
            // 
            // label49
            // 
            this.label49.Location = new System.Drawing.Point(256, 120);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(112, 16);
            this.label49.TabIndex = 38;
            this.label49.Text = "Receive Timestamp";
            // 
            // label48
            // 
            this.label48.Location = new System.Drawing.Point(8, 120);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(120, 16);
            this.label48.TabIndex = 37;
            this.label48.Text = "Originate Timestamp";
            // 
            // numericUpDown_icmp_sequence_number
            // 
            this.numericUpDown_icmp_sequence_number.Location = new System.Drawing.Point(504, 96);
            this.numericUpDown_icmp_sequence_number.Maximum = new System.Decimal(new int[] {
                                                                                               65535,
                                                                                               0,
                                                                                               0,
                                                                                               0});
            this.numericUpDown_icmp_sequence_number.Name = "numericUpDown_icmp_sequence_number";
            this.numericUpDown_icmp_sequence_number.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown_icmp_sequence_number.TabIndex = 36;
            // 
            // numericUpDown_icmp_identifier
            // 
            this.numericUpDown_icmp_identifier.Location = new System.Drawing.Point(312, 96);
            this.numericUpDown_icmp_identifier.Maximum = new System.Decimal(new int[] {
                                                                                          65535,
                                                                                          0,
                                                                                          0,
                                                                                          0});
            this.numericUpDown_icmp_identifier.Name = "numericUpDown_icmp_identifier";
            this.numericUpDown_icmp_identifier.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown_icmp_identifier.TabIndex = 35;
            // 
            // label47
            // 
            this.label47.Location = new System.Drawing.Point(392, 96);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(100, 16);
            this.label47.TabIndex = 34;
            this.label47.Text = "Sequence Number";
            // 
            // label46
            // 
            this.label46.Location = new System.Drawing.Point(256, 96);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(56, 16);
            this.label46.TabIndex = 33;
            this.label46.Text = "Identifier";
            // 
            // label45
            // 
            this.label45.Location = new System.Drawing.Point(256, 72);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(136, 16);
            this.label45.TabIndex = 32;
            this.label45.Text = "Gateway Internet Address";
            // 
            // numericUpDown_icmp_pointer
            // 
            this.numericUpDown_icmp_pointer.Location = new System.Drawing.Point(304, 48);
            this.numericUpDown_icmp_pointer.Maximum = new System.Decimal(new int[] {
                                                                                       255,
                                                                                       0,
                                                                                       0,
                                                                                       0});
            this.numericUpDown_icmp_pointer.Name = "numericUpDown_icmp_pointer";
            this.numericUpDown_icmp_pointer.Size = new System.Drawing.Size(48, 20);
            this.numericUpDown_icmp_pointer.TabIndex = 30;
            // 
            // label44
            // 
            this.label44.Location = new System.Drawing.Point(256, 48);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(56, 16);
            this.label44.TabIndex = 29;
            this.label44.Text = "Pointer";
            // 
            // numericUpDown_icmp_checksum
            // 
            this.numericUpDown_icmp_checksum.Location = new System.Drawing.Point(96, 56);
            this.numericUpDown_icmp_checksum.Maximum = new System.Decimal(new int[] {
                                                                                        65535,
                                                                                        0,
                                                                                        0,
                                                                                        0});
            this.numericUpDown_icmp_checksum.Name = "numericUpDown_icmp_checksum";
            this.numericUpDown_icmp_checksum.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown_icmp_checksum.TabIndex = 27;
            // 
            // checkBox_icmp_checksum_auto
            // 
            this.checkBox_icmp_checksum_auto.Checked = true;
            this.checkBox_icmp_checksum_auto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_icmp_checksum_auto.Location = new System.Drawing.Point(96, 40);
            this.checkBox_icmp_checksum_auto.Name = "checkBox_icmp_checksum_auto";
            this.checkBox_icmp_checksum_auto.Size = new System.Drawing.Size(56, 16);
            this.checkBox_icmp_checksum_auto.TabIndex = 26;
            this.checkBox_icmp_checksum_auto.Text = "auto";
            // 
            // label43
            // 
            this.label43.Location = new System.Drawing.Point(8, 48);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(80, 16);
            this.label43.TabIndex = 28;
            this.label43.Text = "Checksum";
            // 
            // numericUpDown_icmp_unused
            // 
            this.numericUpDown_icmp_unused.Location = new System.Drawing.Point(464, 48);
            this.numericUpDown_icmp_unused.Maximum = new System.Decimal(new int[] {
                                                                                      -1,
                                                                                      15,
                                                                                      0,
                                                                                      0});
            this.numericUpDown_icmp_unused.Name = "numericUpDown_icmp_unused";
            this.numericUpDown_icmp_unused.TabIndex = 7;
            // 
            // label42
            // 
            this.label42.Location = new System.Drawing.Point(392, 48);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(72, 16);
            this.label42.TabIndex = 6;
            this.label42.Text = "Unused";
            // 
            // label41
            // 
            this.label41.Location = new System.Drawing.Point(256, 16);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(40, 16);
            this.label41.TabIndex = 5;
            this.label41.Text = "Code";
            // 
            // comboBox_icmp_code
            // 
            this.comboBox_icmp_code.Location = new System.Drawing.Point(304, 16);
            this.comboBox_icmp_code.Name = "comboBox_icmp_code";
            this.comboBox_icmp_code.Size = new System.Drawing.Size(312, 21);
            this.comboBox_icmp_code.TabIndex = 4;
            // 
            // comboBox_icmp_icmp_message
            // 
            this.comboBox_icmp_icmp_message.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_icmp_icmp_message.Items.AddRange(new object[] {
                                                                            "0 Echo Reply",
                                                                            "3 Destination Unreachable",
                                                                            "4 Source Quench",
                                                                            "5 Redirect",
                                                                            "8 Echo",
                                                                            "11 Time Exceeded",
                                                                            "12 Parameter Problem",
                                                                            "13 Timestamp",
                                                                            "14 Timestamp Reply",
                                                                            "15 Information Request",
                                                                            "16 Information Reply"});
            this.comboBox_icmp_icmp_message.Location = new System.Drawing.Point(96, 16);
            this.comboBox_icmp_icmp_message.Name = "comboBox_icmp_icmp_message";
            this.comboBox_icmp_icmp_message.Size = new System.Drawing.Size(152, 21);
            this.comboBox_icmp_icmp_message.TabIndex = 1;
            this.comboBox_icmp_icmp_message.SelectedIndexChanged += new System.EventHandler(this.comboBox_icmp_icmp_message_SelectedIndexChanged);
            // 
            // label39
            // 
            this.label39.Location = new System.Drawing.Point(8, 16);
            this.label39.Name = "label39";
            this.label39.TabIndex = 0;
            this.label39.Text = "Icmp message";
            // 
            // Form_packet_forge
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(832, 862);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.groupBox_data,
                                                                          this.groupBox_icmp,
                                                                          this.groupBox_udp,
                                                                          this.groupBox_tcp,
                                                                          this.groupBox_ip});
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form_packet_forge";
            this.Text = "Packets Forge";
            this.groupBox_ip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_ttl_min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_header_length)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_header_checksum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_protocol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_ttl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_fragment_offset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_identification)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_total_length)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_version)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_flag_bit0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_tos_bit7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ip_tos_bit6)).EndInit();
            this.groupBox_data.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_data_number_of_packets)).EndInit();
            this.groupBox_tcp.ResumeLayout(false);
            this.panel_connection_helper.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_urgent_pointer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_header_checksum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_reserved)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_port_source)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_port_dest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_window)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_data_offset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_ack_num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tcp_seq_num)).EndInit();
            this.groupBox_udp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_udp_length)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_udp_checksum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_udp_src_port)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_udp_dest_port)).EndInit();
            this.groupBox_icmp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_icmp_trans_timestamp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_icmp_recv_timestamp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_icmp_orig_timestamp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_icmp_sequence_number)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_icmp_identifier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_icmp_pointer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_icmp_checksum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_icmp_unused)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        #region methodes to show/hide groupbox depending protocol
        private void show_raw()
        {
            this.groupBox_icmp.Visible=false;
            this.groupBox_udp.Visible=false;
            this.groupBox_tcp.Visible=false;
            this.groupBox_data.Top=this.groupBox_ip.Top+this.groupBox_ip.Height;
            this.Height=this.groupBox_data.Top+this.groupBox_data.Height+this.Height-this.ClientSize.Height;
        }
        private void show_icmp()
        {
            this.groupBox_icmp.Visible=true;
            this.groupBox_udp.Visible=false;
            this.groupBox_tcp.Visible=false;
            this.groupBox_icmp.Top=this.groupBox_ip.Top+this.groupBox_ip.Height;
            this.groupBox_data.Top=this.groupBox_icmp.Top+this.groupBox_icmp.Height;
            this.Height=this.groupBox_data.Top+this.groupBox_data.Height+this.Height-this.ClientSize.Height;
        }
        private void show_udp()
        {
            this.groupBox_icmp.Visible=false;
            this.groupBox_udp.Visible=true;
            this.groupBox_tcp.Visible=false;
            this.groupBox_udp.Top=this.groupBox_ip.Top+this.groupBox_ip.Height;
            this.groupBox_data.Top=this.groupBox_udp.Top+this.groupBox_udp.Height;
            this.Height=this.groupBox_data.Top+this.groupBox_data.Height+this.Height-this.ClientSize.Height;
        }
        private void show_tcp()
        {
            this.groupBox_icmp.Visible=false;
            this.groupBox_udp.Visible=false;
            this.groupBox_tcp.Visible=true;
            this.groupBox_tcp.Top=this.groupBox_ip.Top+this.groupBox_ip.Height;
            this.groupBox_data.Top=this.groupBox_tcp.Top+this.groupBox_tcp.Height;
            this.Height=this.groupBox_data.Top+this.groupBox_data.Height+this.Height-this.ClientSize.Height;
        }
        #endregion

        private void button_stop_Click(object sender, System.EventArgs e)
        {
            set_stop_state(true);
        }
        /// <summary>
        /// set stop button state and reset b_stop value
        /// </summary>
        /// <param name="b">true if must be stopped</param>
        private void set_stop_state(bool b)
        {
            this.b_stop=b;
            this.button_stop.Enabled=!b;
            this.button_send.Enabled=b;
            this.enable_interface(b);
        }
        private void enable_interface(bool b)
        {
            this.groupBox_icmp.Enabled=b;
            this.groupBox_ip.Enabled=b;
            this.groupBox_tcp.Enabled=b;
            this.groupBox_udp.Enabled=b;
            // data group (let button send and stop to their state)
            this.textBox_data_value.Enabled=b;
            this.checkBox_data_value_hexa.Enabled=b;
            this.checkBox_data_looping_packets.Enabled=b;
            this.numericUpDown_data_number_of_packets.Enabled=b;
        }

        private void numericUpDown_ip_protocol_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.numericUpDown_ip_protocol_ValueChanged(sender,null);
        }
        private void numericUpDown_ip_protocol_ValueChanged(object sender, System.EventArgs e)
        {
            switch((int)this.numericUpDown_ip_protocol.Value)
            {
                case easy_socket.ip_header.ipv4_header.protocol_icmp:
                    this.show_icmp();
                    break;
                case easy_socket.ip_header.ipv4_header.protocol_udp:
                    this.show_udp();
                    break;
                case easy_socket.ip_header.ipv4_header.protocol_tcp:
                    this.show_tcp();
                    break;
                default:
                    this.show_raw();
                    break;
            }
        }
        private byte get_string_value(string str)
        {
            str=str.Trim();
            int pos=str.IndexOf(" ");
            if (pos>-1)
                str=str.Substring(0,pos);
            byte b=0;
            try
            {
                b=System.Convert.ToByte(str);
            }
            catch
            {
            }
            return b;
        }
        private byte get_icmp_code()
        {
            return this.get_string_value(this.comboBox_icmp_code.Text);
        }
        private byte get_icmp_type()
        {
            return this.get_string_value(this.comboBox_icmp_icmp_message.Text);
        }
        private void comboBox_icmp_icmp_message_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            byte icmp_type=this.get_icmp_type();
            this.comboBox_icmp_code.Items.Clear();

            #region enable disabe icmp fields
            switch (icmp_type)
            {
                case easy_socket.icmp.icmp.DestinationUnreachable:
                    this.comboBox_icmp_code.Items.AddRange(easy_socket.icmp.icmp_destination_unreachable.get_available_codes());
                    this.numericUpDown_icmp_unused.Maximum=0xffffffff;
                    this.numericUpDown_icmp_unused.Enabled=true;
                    this.numericUpDown_icmp_pointer.Enabled=false;
                    this.textBox_icmp_gateway.Enabled=false;
                    this.numericUpDown_icmp_identifier.Enabled=false;
                    this.numericUpDown_icmp_sequence_number.Enabled=false;
                    this.numericUpDown_icmp_orig_timestamp.Enabled=false;
                    this.numericUpDown_icmp_recv_timestamp.Enabled=false;
                    this.numericUpDown_icmp_trans_timestamp.Enabled=false;
                    break;
                case easy_socket.icmp.icmp.Echo:
                    this.comboBox_icmp_code.Items.AddRange(easy_socket.icmp.icmp_echo.get_available_codes());
                    this.numericUpDown_icmp_unused.Enabled=false;
                    this.numericUpDown_icmp_pointer.Enabled=false;
                    this.textBox_icmp_gateway.Enabled=false;
                    this.numericUpDown_icmp_identifier.Enabled=true;
                    this.numericUpDown_icmp_sequence_number.Enabled=true;
                    this.numericUpDown_icmp_orig_timestamp.Enabled=false;
                    this.numericUpDown_icmp_recv_timestamp.Enabled=false;
                    this.numericUpDown_icmp_trans_timestamp.Enabled=false;
                    break;
                case easy_socket.icmp.icmp.EchoReply:
                    this.comboBox_icmp_code.Items.AddRange(easy_socket.icmp.icmp_echo_reply.get_available_codes());
                    this.numericUpDown_icmp_unused.Enabled=false;
                    this.numericUpDown_icmp_pointer.Enabled=false;
                    this.textBox_icmp_gateway.Enabled=false;
                    this.numericUpDown_icmp_identifier.Enabled=true;
                    this.numericUpDown_icmp_sequence_number.Enabled=true;
                    this.numericUpDown_icmp_orig_timestamp.Enabled=false;
                    this.numericUpDown_icmp_recv_timestamp.Enabled=false;
                    this.numericUpDown_icmp_trans_timestamp.Enabled=false;
                    break;
                case easy_socket.icmp.icmp.InformationReply:
                    this.comboBox_icmp_code.Items.AddRange(easy_socket.icmp.icmp_information_reply.get_available_codes());
                    this.numericUpDown_icmp_unused.Enabled=false;
                    this.numericUpDown_icmp_pointer.Enabled=false;
                    this.textBox_icmp_gateway.Enabled=false;
                    this.numericUpDown_icmp_identifier.Enabled=true;
                    this.numericUpDown_icmp_sequence_number.Enabled=true;
                    this.numericUpDown_icmp_orig_timestamp.Enabled=false;
                    this.numericUpDown_icmp_recv_timestamp.Enabled=false;
                    this.numericUpDown_icmp_trans_timestamp.Enabled=false;
                    break;
                case easy_socket.icmp.icmp.InformationRequest:
                    this.comboBox_icmp_code.Items.AddRange(easy_socket.icmp.icmp_information_request.get_available_codes());
                    this.numericUpDown_icmp_unused.Enabled=false;
                    this.numericUpDown_icmp_pointer.Enabled=false;
                    this.textBox_icmp_gateway.Enabled=false;
                    this.numericUpDown_icmp_identifier.Enabled=true;
                    this.numericUpDown_icmp_sequence_number.Enabled=true;
                    this.numericUpDown_icmp_orig_timestamp.Enabled=false;
                    this.numericUpDown_icmp_recv_timestamp.Enabled=false;
                    this.numericUpDown_icmp_trans_timestamp.Enabled=false;
                    break;
                case easy_socket.icmp.icmp.ParameterProblem:
                    this.comboBox_icmp_code.Items.AddRange(easy_socket.icmp.icmp_parameter_problem.get_available_codes());
                    this.numericUpDown_icmp_unused.Maximum=0xffffff;
                    this.numericUpDown_icmp_unused.Enabled=true;
                    this.numericUpDown_icmp_pointer.Enabled=true;
                    this.textBox_icmp_gateway.Enabled=false;
                    this.numericUpDown_icmp_identifier.Enabled=false;
                    this.numericUpDown_icmp_sequence_number.Enabled=false;
                    this.numericUpDown_icmp_orig_timestamp.Enabled=false;
                    this.numericUpDown_icmp_recv_timestamp.Enabled=false;
                    this.numericUpDown_icmp_trans_timestamp.Enabled=false;
                    break;
                case easy_socket.icmp.icmp.Redirect:
                    this.comboBox_icmp_code.Items.AddRange(easy_socket.icmp.icmp_redirect.get_available_codes());
                    this.numericUpDown_icmp_unused.Enabled=false;
                    this.numericUpDown_icmp_pointer.Enabled=false;
                    this.textBox_icmp_gateway.Enabled=true;
                    this.numericUpDown_icmp_identifier.Enabled=false;
                    this.numericUpDown_icmp_sequence_number.Enabled=false;
                    this.numericUpDown_icmp_orig_timestamp.Enabled=false;
                    this.numericUpDown_icmp_recv_timestamp.Enabled=false;
                    this.numericUpDown_icmp_trans_timestamp.Enabled=false;
                    break;
                case easy_socket.icmp.icmp.SourceQuench:
                    this.comboBox_icmp_code.Items.AddRange(easy_socket.icmp.icmp_source_quench.get_available_codes());
                    this.numericUpDown_icmp_unused.Maximum=0xffffffff;
                    this.numericUpDown_icmp_unused.Enabled=true;
                    this.numericUpDown_icmp_pointer.Enabled=false;
                    this.textBox_icmp_gateway.Enabled=false;
                    this.numericUpDown_icmp_identifier.Enabled=false;
                    this.numericUpDown_icmp_sequence_number.Enabled=false;
                    this.numericUpDown_icmp_orig_timestamp.Enabled=false;
                    this.numericUpDown_icmp_recv_timestamp.Enabled=false;
                    this.numericUpDown_icmp_trans_timestamp.Enabled=false;
                    break;
                case easy_socket.icmp.icmp.TimeExceeded:
                    this.comboBox_icmp_code.Items.AddRange(easy_socket.icmp.icmp_time_exceeded_message.get_available_codes());
                    this.numericUpDown_icmp_unused.Maximum=0xffffffff;
                    this.numericUpDown_icmp_unused.Enabled=true;
                    this.numericUpDown_icmp_pointer.Enabled=false;
                    this.textBox_icmp_gateway.Enabled=false;
                    this.numericUpDown_icmp_identifier.Enabled=false;
                    this.numericUpDown_icmp_sequence_number.Enabled=false;
                    this.numericUpDown_icmp_orig_timestamp.Enabled=false;
                    this.numericUpDown_icmp_recv_timestamp.Enabled=false;
                    this.numericUpDown_icmp_trans_timestamp.Enabled=false;
                    break;
                case easy_socket.icmp.icmp.Timestamp:
                    this.comboBox_icmp_code.Items.AddRange(easy_socket.icmp.icmp_timestamp.get_available_codes());
                    this.numericUpDown_icmp_unused.Enabled=false;
                    this.numericUpDown_icmp_pointer.Enabled=false;
                    this.textBox_icmp_gateway.Enabled=false;
                    this.numericUpDown_icmp_identifier.Enabled=true;
                    this.numericUpDown_icmp_sequence_number.Enabled=true;
                    this.numericUpDown_icmp_orig_timestamp.Enabled=true;
                    this.numericUpDown_icmp_recv_timestamp.Enabled=true;
                    this.numericUpDown_icmp_trans_timestamp.Enabled=true;
                    break;
                case easy_socket.icmp.icmp.TimestampReply:
                    this.comboBox_icmp_code.Items.AddRange(easy_socket.icmp.icmp_timestamp_reply.get_available_codes());
                    this.numericUpDown_icmp_unused.Enabled=false;
                    this.numericUpDown_icmp_pointer.Enabled=false;
                    this.textBox_icmp_gateway.Enabled=false;
                    this.numericUpDown_icmp_identifier.Enabled=true;
                    this.numericUpDown_icmp_sequence_number.Enabled=true;
                    this.numericUpDown_icmp_orig_timestamp.Enabled=true;
                    this.numericUpDown_icmp_recv_timestamp.Enabled=true;
                    this.numericUpDown_icmp_trans_timestamp.Enabled=true;
                    break;
                default:
                    this.comboBox_icmp_code.Items.AddRange(easy_socket.icmp.icmp.get_available_codes());
                    break;
            }
            #endregion

            this.comboBox_icmp_code.SelectedIndex=0;
        }


        private byte[] get_data()
        {
            if (this.textBox_data_value.Text.Length==0)
                return null;
            if(this.checkBox_data_value_hexa.Checked)
                return easy_socket.hexa_convert.hexa_to_byte(this.textBox_data_value.Text);
            //else
            return System.Text.Encoding.ASCII.GetBytes(this.textBox_data_value.Text);
        }
        private void button_send_Click(object sender, System.EventArgs e)
        {
            System.Threading.ThreadStart myThreadStart = new System.Threading.ThreadStart(this.my_send);
            System.Threading.Thread myThread =new System.Threading.Thread(myThreadStart);
            myThread.Start();
        }
        private void my_send()
        {
            this.set_stop_state(false);
            if (this.checkBox_tcp_connection_helper.Checked)
            {
                this.set_tcp_connection_helper();
            }

            System.Random random=new System.Random();
            bool b_remote_port=false;
            ushort us_remote_port=0;
            easy_socket.ip_header.ipv4_header_client iph;
            easy_socket.udp_header.udp_header udph=new easy_socket.udp_header.udp_header();
            byte[] ip_data=null;
            easy_socket.tcp_header.tcp_header tcph=new easy_socket.tcp_header.tcp_header();
            // get data of ip_header
            switch((int)this.numericUpDown_ip_protocol.Value)
            {
                case easy_socket.ip_header.ipv4_header.protocol_icmp:
                    iph=new easy_socket.ip_header.ipv4_header_icmp_server();
                    // icmp is encoded here as there's no random parts
                    #region icmp filling
                    switch (this.get_icmp_type())
                    {
                        case easy_socket.icmp.icmp.DestinationUnreachable:
                            easy_socket.icmp.icmp_destination_unreachable icmphdu=new easy_socket.icmp.icmp_destination_unreachable();
                            icmphdu.code=this.get_icmp_code();
                            if (!this.checkBox_icmp_checksum_auto.Checked)
                                icmphdu.Checksum=(UInt16)this.numericUpDown_icmp_checksum.Value;
                            icmphdu.unused=easy_socket.network_convert.switch_UInt32((UInt32)this.numericUpDown_icmp_unused.Value);
                            icmphdu.ih_and_original_dd=this.get_data();
                            ip_data=icmphdu.encode();
                            break;
                        case easy_socket.icmp.icmp.Echo:
                            easy_socket.icmp.icmp_echo icmphe=new easy_socket.icmp.icmp_echo();
                            icmphe.code=this.get_icmp_code();
                            if (!this.checkBox_icmp_checksum_auto.Checked)
                                icmphe.Checksum=(UInt16)this.numericUpDown_icmp_checksum.Value;
                            icmphe.Identifier=(UInt16)this.numericUpDown_icmp_identifier.Value;
                            icmphe.SequenceNumber=(UInt16)this.numericUpDown_icmp_sequence_number.Value;
                            icmphe.data=this.get_data();
                            ip_data=icmphe.encode();
                            break;
                        case easy_socket.icmp.icmp.EchoReply:
                            easy_socket.icmp.icmp_echo_reply icmpher=new easy_socket.icmp.icmp_echo_reply();
                            icmpher.code=this.get_icmp_code();
                            if (!this.checkBox_icmp_checksum_auto.Checked)
                                icmpher.Checksum=(UInt16)this.numericUpDown_icmp_checksum.Value;
                            icmpher.Identifier=(UInt16)this.numericUpDown_icmp_identifier.Value;
                            icmpher.SequenceNumber=(UInt16)this.numericUpDown_icmp_sequence_number.Value;
                            icmpher.data=this.get_data();
                            ip_data=icmpher.encode();
                            break;
                        case easy_socket.icmp.icmp.InformationReply:
                            easy_socket.icmp.icmp_information_reply icmphir=new easy_socket.icmp.icmp_information_reply();
                            icmphir.code=this.get_icmp_code();
                            if (!this.checkBox_icmp_checksum_auto.Checked)
                                icmphir.Checksum=(UInt16)this.numericUpDown_icmp_checksum.Value;
                            icmphir.Identifier=(UInt16)this.numericUpDown_icmp_identifier.Value;
                            icmphir.SequenceNumber=(UInt16)this.numericUpDown_icmp_sequence_number.Value;
                            ip_data=icmphir.encode();
                            break;
                        case easy_socket.icmp.icmp.InformationRequest:
                            easy_socket.icmp.icmp_information_request icmphireq=new easy_socket.icmp.icmp_information_request();
                            icmphireq.code=this.get_icmp_code();
                            if (!this.checkBox_icmp_checksum_auto.Checked)
                                icmphireq.Checksum=(UInt16)this.numericUpDown_icmp_checksum.Value;
                            icmphireq.Identifier=(UInt16)this.numericUpDown_icmp_identifier.Value;
                            icmphireq.SequenceNumber=(UInt16)this.numericUpDown_icmp_sequence_number.Value;
                            ip_data=icmphireq.encode();
                            break;
                        case easy_socket.icmp.icmp.ParameterProblem:
                            easy_socket.icmp.icmp_parameter_problem icmphpp=new easy_socket.icmp.icmp_parameter_problem();
                            icmphpp.code=this.get_icmp_code();
                            if (!this.checkBox_icmp_checksum_auto.Checked)
                                icmphpp.Checksum=(UInt16)this.numericUpDown_icmp_checksum.Value;
                            icmphpp.pointer=(byte)this.numericUpDown_icmp_pointer.Value;
                            icmphpp.unused=easy_socket.network_convert.switch_UInt32((UInt32)this.numericUpDown_icmp_unused.Value);
                            icmphpp.ih_and_original_dd=this.get_data();
                            ip_data=icmphpp.encode();
                            break;
                        case easy_socket.icmp.icmp.Redirect:
                            easy_socket.icmp.icmp_redirect icmphr=new easy_socket.icmp.icmp_redirect();
                            icmphr.code=this.get_icmp_code();
                            if (!this.checkBox_icmp_checksum_auto.Checked)
                                icmphr.Checksum=(UInt16)this.numericUpDown_icmp_checksum.Value;
                            icmphr.GatewayInternetAddress=this.textBox_icmp_gateway.Text;
                            icmphr.ih_and_original_dd=this.get_data();
                            ip_data=icmphr.encode();
                            break;
                        case easy_socket.icmp.icmp.SourceQuench:
                            easy_socket.icmp.icmp_source_quench icmphsq=new easy_socket.icmp.icmp_source_quench();
                            icmphsq.code=this.get_icmp_code();
                            if (!this.checkBox_icmp_checksum_auto.Checked)
                                icmphsq.Checksum=(UInt16)this.numericUpDown_icmp_checksum.Value;
                            icmphsq.unused=easy_socket.network_convert.switch_UInt32((UInt32)this.numericUpDown_icmp_unused.Value);
                            icmphsq.ih_and_original_dd=this.get_data();
                            ip_data=icmphsq.encode();
                            break;
                        case easy_socket.icmp.icmp.TimeExceeded:
                            easy_socket.icmp.icmp_time_exceeded_message icmphte=new easy_socket.icmp.icmp_time_exceeded_message();
                            icmphte.code=this.get_icmp_code();
                            if (!this.checkBox_icmp_checksum_auto.Checked)
                                icmphte.Checksum=(UInt16)this.numericUpDown_icmp_checksum.Value;
                            icmphte.unused=easy_socket.network_convert.switch_UInt32((UInt32)this.numericUpDown_icmp_unused.Value);
                            icmphte.ih_and_original_dd=this.get_data();
                            ip_data=icmphte.encode();
                            break;
                        case easy_socket.icmp.icmp.Timestamp:
                            easy_socket.icmp.icmp_timestamp icmpht=new easy_socket.icmp.icmp_timestamp();
                            icmpht.code=this.get_icmp_code();
                            if (!this.checkBox_icmp_checksum_auto.Checked)
                                icmpht.Checksum=(UInt16)this.numericUpDown_icmp_checksum.Value;
                            icmpht.Identifier=(UInt16)this.numericUpDown_icmp_identifier.Value;
                            icmpht.SequenceNumber=(UInt16)this.numericUpDown_icmp_sequence_number.Value;
                            icmpht.OriginateTimestamp=(UInt32)this.numericUpDown_icmp_orig_timestamp.Value;
                            icmpht.ReceiveTimestamp=(UInt32)this.numericUpDown_icmp_recv_timestamp.Value;
                            icmpht.TransmitTimestamp=(UInt32)this.numericUpDown_icmp_trans_timestamp.Value;
                            ip_data=icmpht.encode();
                            break;
                        case easy_socket.icmp.icmp.TimestampReply:
                            easy_socket.icmp.icmp_timestamp_reply icmphtr=new easy_socket.icmp.icmp_timestamp_reply();
                            icmphtr.code=this.get_icmp_code();
                            if (!this.checkBox_icmp_checksum_auto.Checked)
                                icmphtr.Checksum=(UInt16)this.numericUpDown_icmp_checksum.Value;
                            icmphtr.Identifier=(UInt16)this.numericUpDown_icmp_identifier.Value;
                            icmphtr.SequenceNumber=(UInt16)this.numericUpDown_icmp_sequence_number.Value;
                            icmphtr.OriginateTimestamp=(UInt32)this.numericUpDown_icmp_orig_timestamp.Value;
                            icmphtr.ReceiveTimestamp=(UInt32)this.numericUpDown_icmp_recv_timestamp.Value;
                            icmphtr.TransmitTimestamp=(UInt32)this.numericUpDown_icmp_trans_timestamp.Value;
                            ip_data=icmphtr.encode();
                            break;
                            
                        default:
                            break;
                    }
                    iph.data=ip_data;
                    #endregion
                    break;
                case easy_socket.ip_header.ipv4_header.protocol_udp:
                    //iph=new easy_socket.ip_header.ipv4_header_udp_server();
                    iph=new easy_socket.ip_header.ipv4_header_client();
                    break;
                case easy_socket.ip_header.ipv4_header.protocol_tcp:
                    //iph=new easy_socket.ip_header.ipv4_header_tcp_server();
                    iph=new easy_socket.ip_header.ipv4_header_client();
                    break;
                default:
                    iph=new easy_socket.ip_header.ipv4_header_client();
                    break;
            }


            int cpt=0;
            while((cpt<this.numericUpDown_data_number_of_packets.Value)&&(!this.b_stop))
            {
                System.Threading.Thread.Sleep(1);
                // included in loop because of random parts
                #region ip header filling
                iph.version=(byte)this.numericUpDown_ip_version.Value;
                iph.b_comput_internet_header_length=this.checkBox_ip_IHL_auto.Checked;
                if (!iph.b_comput_internet_header_length)
                    iph.internet_header_length=(byte)this.numericUpDown_ip_header_length.Value;
                iph.Precedence=(byte)easy_socket.bin_convert.strbit_to_byte(this.comboBox_ip_tos_precedence.Text.Substring(0,3));
                iph.Delay=(byte)easy_socket.bin_convert.strbit_to_byte(this.comboBox_ip_tos_delay.Text.Substring(0,1));
                iph.Throughput=(byte)easy_socket.bin_convert.strbit_to_byte(this.comboBox_ip_tos_throughtput.Text.Substring(0,1));
                iph.Relibility=(byte)easy_socket.bin_convert.strbit_to_byte(this.comboBox_ip_tos_delay.Text.Substring(0,1));
                iph.type_of_service=(byte)(iph.type_of_service+(((byte)this.numericUpDown_ip_tos_bit6.Value)<<1)+(byte)this.numericUpDown_ip_tos_bit7.Value);
                iph.b_comput_total_length=this.checkBox_ip_total_length_auto.Checked;
                if (!iph.b_comput_total_length)
                    iph.TotalLength=(ushort)this.numericUpDown_ip_total_length.Value;
                if (this.checkBox_ip_identification_random.Checked)
                    iph.Identification=(UInt16)random.Next(UInt16.MinValue,UInt16.MaxValue);
                else
                    iph.Identification=(UInt16)this.numericUpDown_ip_identification.Value;

                iph.MayDontFragment=(byte)easy_socket.bin_convert.strbit_to_byte(this.comboBox_ip_flags_fragment_type.Text);
                iph.LastMoreFragment=(byte)easy_socket.bin_convert.strbit_to_byte(this.comboBox_ip_flags_fragment_pos.Text);
                iph.FragmentOffset=(UInt16)this.numericUpDown_ip_fragment_offset.Value;
                iph.flags+=(byte)((byte)(this.numericUpDown_ip_flag_bit0.Value)<<7);

                if (this.checkBox_ip_ttl_random.Checked)
                    iph.time_to_live=(byte)random.Next((int)this.numericUpDown_ip_ttl_min.Value,255);
                else
                    iph.time_to_live=(byte)this.numericUpDown_ip_ttl.Value;

                iph.protocol=(byte)this.numericUpDown_ip_protocol.Value;
                iph.b_comput_header_checksum=this.checkBox_ip_header_checksum_auto.Checked;
                if (!iph.b_comput_header_checksum)
                    iph.HeaderChecksum=(UInt16)this.numericUpDown_ip_header_checksum.Value;

                if (this.checkBox_ip_src_random.Checked)
                    iph.source_address=(UInt32)random.Next();
                else
                    iph.SourceAddress=this.textBox_ip_src.Text;
                iph.DestinationAddress=this.textBox_ip_dest.Text;
                iph.options_and_padding=easy_socket.hexa_convert.hexa_to_byte(this.textBox_ip_options_padding.Text);
                #endregion

                // get data of ip_header
                switch((int)this.numericUpDown_ip_protocol.Value)
                {
                    case easy_socket.ip_header.ipv4_header.protocol_icmp:
                        break;
                    case easy_socket.ip_header.ipv4_header.protocol_udp:
                    #region udp filling
                        udph.DestinationPort=(ushort)this.numericUpDown_udp_dest_port.Value;
                        if (this.checkBox_udp_src_port_random.Checked)
                            udph.SourcePort=(ushort)random.Next(ushort.MinValue,ushort.MaxValue);
                        else
                            udph.SourcePort=(ushort)this.numericUpDown_udp_src_port.Value;
                        if (!this.checkBox_udp_length_auto.Checked)
                            udph.UdpLength=(ushort)this.numericUpDown_udp_length.Value;
                        if (!this.checkBox_udp_checksum_auto.Checked)
                            udph.Checksum=(ushort)this.numericUpDown_udp_checksum.Value;
                        udph.data=this.get_data();
                        iph.data=udph.encode(iph.source_address,iph.destination_address);
                        b_remote_port=true;
                        us_remote_port=udph.DestinationPort;
                    #endregion

                        break;
                    case easy_socket.ip_header.ipv4_header.protocol_tcp:
                    #region tcp header filling
                        if (this.checkBox_tcp_src_port_random.Checked)
                            tcph.SourcePort=(ushort)random.Next(ushort.MinValue,ushort.MaxValue);
                        else
                            tcph.SourcePort=(ushort)this.numericUpDown_tcp_port_source.Value;
                        tcph.DestinationPort=(ushort)this.numericUpDown_tcp_port_dest.Value;
                        if (this.checkBox_tcp_sequence_number_random.Checked)
                            tcph.sequence_number=(UInt32)random.Next();
                        else
                            tcph.SequenceNumber=(UInt32)this.numericUpDown_tcp_seq_num.Value;
                        tcph.AcknowledgmentNumber=(UInt32)this.numericUpDown_tcp_ack_num.Value;
                        if (!this.checkBox_tcp_data_offset_auto.Checked)
                            tcph.DataOffset=(byte)this.numericUpDown_tcp_data_offset.Value;
                        tcph.reserved=(byte)this.numericUpDown_tcp_reserved.Value;
                        tcph.URG=this.checkBox_tcp_urg.Checked;
                        tcph.ACK=this.checkBox_tcp_ack.Checked;
                        tcph.PSH=this.checkBox_tcp_push.Checked;
                        tcph.RST=this.checkBox_tcp_rst.Checked;
                        tcph.SYN=this.checkBox_tcp_syn.Checked;
                        tcph.FIN=this.checkBox_tcp_fin.Checked;
                        tcph.Window=(ushort)this.numericUpDown_tcp_window.Value;
                        if(!this.checkBox_tcp_header_checksum_auto.Checked)
                            tcph.Checksum=(ushort)this.numericUpDown_tcp_header_checksum.Value;
                        tcph.UrgentPointer=(ushort)this.numericUpDown_tcp_urgent_pointer.Value;
                        tcph.options=easy_socket.hexa_convert.hexa_to_byte(this.textBox_tcp_options_padding.Text);
                        tcph.data=this.get_data();
                        iph.data=tcph.encode(iph.source_address,iph.destination_address);
                        b_remote_port=true;
                        us_remote_port=tcph.DestinationPort;
                    #endregion
                        if (this.checkBox_tcp_connection_helper.Checked)
                        {
                            if (tcph.SYN&&!tcph.ACK)
                            {
                                // clear listview
                                this.userControlPacketsView.listView_capture.Items.Clear();

                                // stop previous server if any
                                this.tcps.stop();
                                // start an raw tcp server on specified ip/port
                                this.tcps.start(iph.SourceAddress,tcph.SourcePort);
                                // set connection state
                                this.tcpc.state=easy_socket.tcp_header.tcp_connection.STATE_SYN_SENT;

                                // show state
                                this.label_tcp_connection_state.Text=this.tcpc.State;
                            }
                            if(tcph.RST)
                            {
                                this.tcpc.state=easy_socket.tcp_header.tcp_connection.STATE_CLOSED;
                                // show state
                                this.label_tcp_connection_state.Text=this.tcpc.State;
                            }
                        }
                        break;
                    default:
                        iph.data=this.get_data();
                        break;
                }
         

                // send full packet
                if (b_remote_port)
                    iph.send(us_remote_port);
                else
                    iph.send();

                if (!this.checkBox_data_looping_packets.Checked)
                    cpt++;
            }
            this.set_stop_state(true);
        }

        private void tcps_data_arrival(easy_socket.tcp_header.tcp_header_server tcph, easy_socket.tcp_header.EventArgs_ipv4header_ReceiveData ipv4h)
        {
            if (this.tcpc.receive(ipv4h.ipv4header.source_address,ipv4h.ipv4header.destination_address,tcph)!=easy_socket.tcp_header.tcp_header.error_success)
            {
                MessageBox.Show(this,"Error in received packet, connection helper can't be used","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            this.numericUpDown_ip_identification.Value=(this.numericUpDown_ip_identification.Value+1)%this.numericUpDown_ip_identification.Maximum;
            this.numericUpDown_tcp_seq_num.Value=this.tcpc.tcp_replyh.SequenceNumber;
            this.numericUpDown_tcp_ack_num.Value=this.tcpc.tcp_replyh.AcknowledgmentNumber;
            this.checkBox_tcp_ack.Checked=this.tcpc.tcp_replyh.ACK;
            this.checkBox_tcp_rst.Checked=this.tcpc.tcp_replyh.RST;
            this.checkBox_tcp_syn.Checked=this.tcpc.tcp_replyh.SYN;
            this.checkBox_tcp_fin.Checked=this.tcpc.tcp_replyh.FIN;
            // add to listview
            string[] data_items=new string[10];
            easy_socket.ip_header.ipv4_header mipv4h=ipv4h.ipv4header;// ipv4header is readonly
            string[] data=easy_socket.packet_ToStringArray.tcp(ref mipv4h,ref this.tcpc.tcph_incoming,!this.checkBox_tcp_packet_details.Checked);
            data_items[0]=System.DateTime.Now.TimeOfDay.ToString();
            // ip infos
            System.Array.Copy(data,0,data_items,1,8);
            // full packet data
            data_items[9]=easy_socket.hexa_convert.byte_to_hexa(ipv4h.full_packet);
            ListViewItem lvi=new ListViewItem(data_items);
            this.userControlPacketsView.listView_capture.Items.Add(lvi);

            // change connection state
            // show state
            this.label_tcp_connection_state.Text=this.tcpc.State;
        }

        private void set_tcp_connection_helper()
        {
            this.checkBox_ip_src_random.Checked=false;
            this.checkBox_tcp_sequence_number_random.Checked=false;
            this.checkBox_tcp_src_port_random.Checked=false;
        }

        private void checkBox_tcp_connection_helper_CheckedChanged(object sender, System.EventArgs e)
        {
            this.panel_connection_helper.Enabled=this.checkBox_tcp_connection_helper.Checked;
            if (this.checkBox_tcp_connection_helper.Checked)
            {
                this.set_tcp_connection_helper();
            }
        }

        private void checkBox_data_value_hexa_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.checkBox_data_value_hexa.Checked)
                this.textBox_data_value.Text=easy_socket.hexa_convert.string_to_hexa(this.textBox_data_value.Text);
            else
                this.textBox_data_value.Text=easy_socket.hexa_convert.hexa_to_string(this.textBox_data_value.Text);        
        }

    }
}
