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
using System.Data;
using System.Runtime.InteropServices;

namespace Network_Stuff
{
    public class FormMain : System.Windows.Forms.Form
    {

        private System.Windows.Forms.TabControl tabControlmain;
        private System.Windows.Forms.TabPage tabPage_telnet;
        private System.Windows.Forms.TabPage tabPage_icmp;
        private System.Windows.Forms.TabPage tabPage_dns;
        private System.Windows.Forms.TabPage tabPage_arp;
        private System.Windows.Forms.TabPage tabPage_stat;
        private System.Windows.Forms.TabPage tabPage_whois;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBox_UDP_Client_local_port;
        private System.Windows.Forms.CheckBox checkBox_UDP_Client_Specify_local_port;
        private System.Windows.Forms.CheckBox checkBox_UDP_Client_watch_for_reply;
        private System.Windows.Forms.TextBox textBox_UDP_Client_Port;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_UDP_Client_IP;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button_UDP_Client;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBox_UDP_Server_echo;
        private System.Windows.Forms.TextBox textBox_UDP_Server_Port;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_UDP_Server_IP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_UDP_Server;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_TCP_Server_Port;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_TCP_Server_IP;
        private System.Windows.Forms.Button button_TCP_Server;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_TCP_Client_local_port;
        private System.Windows.Forms.CheckBox checkBox_TCP_Client_Specify_local_port;
        private System.Windows.Forms.TextBox textBox_TCP_Client_Port;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_TCP_Client_IP;
        private System.Windows.Forms.Button button_TCP_Client;
        private System.Windows.Forms.Button button_dns;
        private System.Windows.Forms.Button button_arp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_arp_result;
        private System.Windows.Forms.TextBox textBox_arp_ip;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_dns_result;
        private System.Windows.Forms.TextBox textBox_dns_ip;
        private System.Windows.Forms.TextBox textBox_icmp_ip;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button_ping;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox_icmp_packet_ttl;
        private System.Windows.Forms.TextBox textBox_icmp_ping_number;
        private System.Windows.Forms.CheckBox checkBox_icmp_looping_ping;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox checkBox_icmp_resolve_adresses;
        private System.Windows.Forms.TextBox textBox_icmp_end_with_hop;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox_icmp_start_with_hop;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button button_trace;
        private System.Windows.Forms.TextBox textBox_icmp_delay_dor_reply;
        private System.Windows.Forms.CheckBox checkBox_icmp_may_broadcast;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBox_whois_ip;
        private System.Windows.Forms.Button button_whois;
        private System.Windows.Forms.RadioButton radioButton_whois_auto_find;
        private System.Windows.Forms.RadioButton radioButton_whois_use_following;
        private System.Windows.Forms.TextBox textBox_whois_server;
        private System.Windows.Forms.TabPage tabPageWakeOnLan;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBox_outside_ip_ip;
        private System.Windows.Forms.Button button_outside_ip_get;
        private System.Windows.Forms.TextBox textBox_outside_ip_server;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox textBox_local_ip;
        private System.Windows.Forms.Button button_local_ip_refresh;
        private System.Windows.Forms.TabPage tabPage_computerIp;
        private System.Windows.Forms.Button button_ipnet_stats;
        private System.Windows.Forms.Button button_ipnet_table;
        private System.Windows.Forms.Button button_icmp_stats;
        private System.Windows.Forms.Button button_udp_stats;
        private System.Windows.Forms.Button button_udp_table;
        private System.Windows.Forms.Button button_tcp_stat;
        private System.Windows.Forms.Button button_tcp_table;
        private System.Windows.Forms.TextBox textBox_WOL_mac_address;
        private System.Windows.Forms.TextBox textBox_WOL_broadcast_ip;
        private System.Windows.Forms.TextBox textBox_WOL_udp_port;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button button_WOL_wake_up;
        private System.Windows.Forms.ImageList imageList_tabpage_icons;
        private System.Windows.Forms.Panel panel_mdi_tab;
        private System.Windows.Forms.TabControl tabControl_mdichild;
        private System.Windows.Forms.Panel panel_tabcontrol_main;
        private System.Windows.Forms.TabPage tabPagePacket;
        private System.Windows.Forms.Button button_raw_packet_forge;
        private System.Windows.Forms.Button button_raw_packet_capture;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TabPage tabPageScan;
        private System.Windows.Forms.Button button_scan_tcp;
        private System.Windows.Forms.Button button_scan_udp;
        private System.Windows.Forms.Button button_scan_icmp;
        private System.Windows.Forms.Button button_scan_cgi;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox textBox_remote_shutdown_computer_name;
        private System.Windows.Forms.TextBox textBox_remote_shutdown_message;
        private System.Windows.Forms.CheckBox checkBox_remote_shutdown_force_close;
        private System.Windows.Forms.CheckBox checkBox_remote_shutdown_reset;
        private System.Windows.Forms.NumericUpDown numericUpDown_remote_shutdown_timeout;
        private System.Windows.Forms.Button button_remote_shutdown_initiate;
        private System.Windows.Forms.Button button_remote_shutdown_abort;
        private System.ComponentModel.IContainer components;


        public FormMain()
        {
            InitializeComponent();
            // update computer's local ip
            this.button_local_ip_refresh_Click(this,null);
            // hide tabControl_mdichild
            this.panel_mdi_tab.Visible=false;

            XPStyle.MakeXPStyle(this);
        }

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

        #region Windows Form Designer generated code
        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormMain));
            this.tabControlmain = new System.Windows.Forms.TabControl();
            this.tabPage_telnet = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox_UDP_Client_local_port = new System.Windows.Forms.TextBox();
            this.checkBox_UDP_Client_Specify_local_port = new System.Windows.Forms.CheckBox();
            this.checkBox_UDP_Client_watch_for_reply = new System.Windows.Forms.CheckBox();
            this.textBox_UDP_Client_Port = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_UDP_Client_IP = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.button_UDP_Client = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox_UDP_Server_echo = new System.Windows.Forms.CheckBox();
            this.textBox_UDP_Server_Port = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_UDP_Server_IP = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button_UDP_Server = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_TCP_Server_Port = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_TCP_Server_IP = new System.Windows.Forms.TextBox();
            this.button_TCP_Server = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_TCP_Client_local_port = new System.Windows.Forms.TextBox();
            this.checkBox_TCP_Client_Specify_local_port = new System.Windows.Forms.CheckBox();
            this.textBox_TCP_Client_Port = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_TCP_Client_IP = new System.Windows.Forms.TextBox();
            this.button_TCP_Client = new System.Windows.Forms.Button();
            this.tabPage_icmp = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button_trace = new System.Windows.Forms.Button();
            this.checkBox_icmp_resolve_adresses = new System.Windows.Forms.CheckBox();
            this.textBox_icmp_end_with_hop = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox_icmp_start_with_hop = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkBox_icmp_may_broadcast = new System.Windows.Forms.CheckBox();
            this.checkBox_icmp_looping_ping = new System.Windows.Forms.CheckBox();
            this.textBox_icmp_ping_number = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.button_ping = new System.Windows.Forms.Button();
            this.textBox_icmp_packet_ttl = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox_icmp_delay_dor_reply = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox_icmp_ip = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tabPage_dns = new System.Windows.Forms.TabPage();
            this.textBox_dns_result = new System.Windows.Forms.TextBox();
            this.textBox_dns_ip = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button_dns = new System.Windows.Forms.Button();
            this.tabPage_whois = new System.Windows.Forms.TabPage();
            this.textBox_whois_server = new System.Windows.Forms.TextBox();
            this.radioButton_whois_use_following = new System.Windows.Forms.RadioButton();
            this.radioButton_whois_auto_find = new System.Windows.Forms.RadioButton();
            this.button_whois = new System.Windows.Forms.Button();
            this.textBox_whois_ip = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.tabPage_arp = new System.Windows.Forms.TabPage();
            this.textBox_arp_result = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_arp_ip = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button_arp = new System.Windows.Forms.Button();
            this.tabPage_stat = new System.Windows.Forms.TabPage();
            this.button_ipnet_stats = new System.Windows.Forms.Button();
            this.button_ipnet_table = new System.Windows.Forms.Button();
            this.button_icmp_stats = new System.Windows.Forms.Button();
            this.button_udp_stats = new System.Windows.Forms.Button();
            this.button_udp_table = new System.Windows.Forms.Button();
            this.button_tcp_stat = new System.Windows.Forms.Button();
            this.button_tcp_table = new System.Windows.Forms.Button();
            this.tabPageScan = new System.Windows.Forms.TabPage();
            this.button_scan_cgi = new System.Windows.Forms.Button();
            this.button_scan_icmp = new System.Windows.Forms.Button();
            this.button_scan_udp = new System.Windows.Forms.Button();
            this.button_scan_tcp = new System.Windows.Forms.Button();
            this.tabPagePacket = new System.Windows.Forms.TabPage();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.button_raw_packet_capture = new System.Windows.Forms.Button();
            this.button_raw_packet_forge = new System.Windows.Forms.Button();
            this.tabPageWakeOnLan = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label29 = new System.Windows.Forms.Label();
            this.button_remote_shutdown_abort = new System.Windows.Forms.Button();
            this.button_remote_shutdown_initiate = new System.Windows.Forms.Button();
            this.numericUpDown_remote_shutdown_timeout = new System.Windows.Forms.NumericUpDown();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.checkBox_remote_shutdown_reset = new System.Windows.Forms.CheckBox();
            this.checkBox_remote_shutdown_force_close = new System.Windows.Forms.CheckBox();
            this.textBox_remote_shutdown_message = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.textBox_remote_shutdown_computer_name = new System.Windows.Forms.TextBox();
            this.button_WOL_wake_up = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.textBox_WOL_udp_port = new System.Windows.Forms.TextBox();
            this.textBox_WOL_broadcast_ip = new System.Windows.Forms.TextBox();
            this.textBox_WOL_mac_address = new System.Windows.Forms.TextBox();
            this.tabPage_computerIp = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.button_local_ip_refresh = new System.Windows.Forms.Button();
            this.textBox_local_ip = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBox_outside_ip_ip = new System.Windows.Forms.TextBox();
            this.button_outside_ip_get = new System.Windows.Forms.Button();
            this.textBox_outside_ip_server = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.imageList_tabpage_icons = new System.Windows.Forms.ImageList(this.components);
            this.panel_mdi_tab = new System.Windows.Forms.Panel();
            this.tabControl_mdichild = new System.Windows.Forms.TabControl();
            this.panel_tabcontrol_main = new System.Windows.Forms.Panel();
            this.tabControlmain.SuspendLayout();
            this.tabPage_telnet.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage_icmp.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPage_dns.SuspendLayout();
            this.tabPage_whois.SuspendLayout();
            this.tabPage_arp.SuspendLayout();
            this.tabPage_stat.SuspendLayout();
            this.tabPageScan.SuspendLayout();
            this.tabPagePacket.SuspendLayout();
            this.tabPageWakeOnLan.SuspendLayout();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_remote_shutdown_timeout)).BeginInit();
            this.tabPage_computerIp.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.panel_mdi_tab.SuspendLayout();
            this.panel_tabcontrol_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlmain
            // 
            this.tabControlmain.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                         this.tabPage_telnet,
                                                                                         this.tabPage_icmp,
                                                                                         this.tabPage_dns,
                                                                                         this.tabPage_whois,
                                                                                         this.tabPage_arp,
                                                                                         this.tabPage_stat,
                                                                                         this.tabPageScan,
                                                                                         this.tabPagePacket,
                                                                                         this.tabPageWakeOnLan,
                                                                                         this.tabPage_computerIp});
            this.tabControlmain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControlmain.Name = "tabControlmain";
            this.tabControlmain.SelectedIndex = 0;
            this.tabControlmain.Size = new System.Drawing.Size(880, 154);
            this.tabControlmain.TabIndex = 11;
            // 
            // tabPage_telnet
            // 
            this.tabPage_telnet.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                         this.groupBox4,
                                                                                         this.groupBox3,
                                                                                         this.groupBox2,
                                                                                         this.groupBox1});
            this.tabPage_telnet.Location = new System.Drawing.Point(4, 22);
            this.tabPage_telnet.Name = "tabPage_telnet";
            this.tabPage_telnet.Size = new System.Drawing.Size(872, 128);
            this.tabPage_telnet.TabIndex = 0;
            this.tabPage_telnet.Text = "Telnet";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.textBox_UDP_Client_local_port,
                                                                                    this.checkBox_UDP_Client_Specify_local_port,
                                                                                    this.checkBox_UDP_Client_watch_for_reply,
                                                                                    this.textBox_UDP_Client_Port,
                                                                                    this.label10,
                                                                                    this.textBox_UDP_Client_IP,
                                                                                    this.label12,
                                                                                    this.button_UDP_Client});
            this.groupBox4.Location = new System.Drawing.Point(312, 64);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(312, 64);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "UDP Client";
            // 
            // textBox_UDP_Client_local_port
            // 
            this.textBox_UDP_Client_local_port.Location = new System.Drawing.Point(128, 40);
            this.textBox_UDP_Client_local_port.Name = "textBox_UDP_Client_local_port";
            this.textBox_UDP_Client_local_port.Size = new System.Drawing.Size(48, 20);
            this.textBox_UDP_Client_local_port.TabIndex = 16;
            this.textBox_UDP_Client_local_port.Text = "53";
            this.textBox_UDP_Client_local_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // checkBox_UDP_Client_Specify_local_port
            // 
            this.checkBox_UDP_Client_Specify_local_port.Location = new System.Drawing.Point(8, 40);
            this.checkBox_UDP_Client_Specify_local_port.Name = "checkBox_UDP_Client_Specify_local_port";
            this.checkBox_UDP_Client_Specify_local_port.Size = new System.Drawing.Size(112, 16);
            this.checkBox_UDP_Client_Specify_local_port.TabIndex = 15;
            this.checkBox_UDP_Client_Specify_local_port.Text = "Specify local port";
            // 
            // checkBox_UDP_Client_watch_for_reply
            // 
            this.checkBox_UDP_Client_watch_for_reply.Location = new System.Drawing.Point(192, 40);
            this.checkBox_UDP_Client_watch_for_reply.Name = "checkBox_UDP_Client_watch_for_reply";
            this.checkBox_UDP_Client_watch_for_reply.Size = new System.Drawing.Size(104, 16);
            this.checkBox_UDP_Client_watch_for_reply.TabIndex = 17;
            this.checkBox_UDP_Client_watch_for_reply.Text = "Watch for reply";
            // 
            // textBox_UDP_Client_Port
            // 
            this.textBox_UDP_Client_Port.Location = new System.Drawing.Point(176, 16);
            this.textBox_UDP_Client_Port.Name = "textBox_UDP_Client_Port";
            this.textBox_UDP_Client_Port.Size = new System.Drawing.Size(48, 20);
            this.textBox_UDP_Client_Port.TabIndex = 13;
            this.textBox_UDP_Client_Port.Text = "7000";
            this.textBox_UDP_Client_Port.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(144, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 16);
            this.label10.TabIndex = 59;
            this.label10.Text = "Port";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_UDP_Client_IP
            // 
            this.textBox_UDP_Client_IP.Location = new System.Drawing.Point(40, 16);
            this.textBox_UDP_Client_IP.Name = "textBox_UDP_Client_IP";
            this.textBox_UDP_Client_IP.Size = new System.Drawing.Size(96, 20);
            this.textBox_UDP_Client_IP.TabIndex = 12;
            this.textBox_UDP_Client_IP.Text = "127.0.0.1";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(16, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(24, 16);
            this.label12.TabIndex = 57;
            this.label12.Text = "IP";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // button_UDP_Client
            // 
            this.button_UDP_Client.Location = new System.Drawing.Point(232, 16);
            this.button_UDP_Client.Name = "button_UDP_Client";
            this.button_UDP_Client.Size = new System.Drawing.Size(72, 23);
            this.button_UDP_Client.TabIndex = 14;
            this.button_UDP_Client.Text = "Start";
            this.button_UDP_Client.Click += new System.EventHandler(this.button_UDP_Client_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.checkBox_UDP_Server_echo,
                                                                                    this.textBox_UDP_Server_Port,
                                                                                    this.label4,
                                                                                    this.textBox_UDP_Server_IP,
                                                                                    this.label6,
                                                                                    this.button_UDP_Server});
            this.groupBox3.Location = new System.Drawing.Point(312, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(312, 64);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "UDP Server";
            // 
            // checkBox_UDP_Server_echo
            // 
            this.checkBox_UDP_Server_echo.Location = new System.Drawing.Point(8, 40);
            this.checkBox_UDP_Server_echo.Name = "checkBox_UDP_Server_echo";
            this.checkBox_UDP_Server_echo.Size = new System.Drawing.Size(120, 16);
            this.checkBox_UDP_Server_echo.TabIndex = 11;
            this.checkBox_UDP_Server_echo.Text = "send echo";
            // 
            // textBox_UDP_Server_Port
            // 
            this.textBox_UDP_Server_Port.Location = new System.Drawing.Point(176, 16);
            this.textBox_UDP_Server_Port.Name = "textBox_UDP_Server_Port";
            this.textBox_UDP_Server_Port.Size = new System.Drawing.Size(48, 20);
            this.textBox_UDP_Server_Port.TabIndex = 9;
            this.textBox_UDP_Server_Port.Text = "7000";
            this.textBox_UDP_Server_Port.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(136, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 16);
            this.label4.TabIndex = 44;
            this.label4.Text = "Port";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_UDP_Server_IP
            // 
            this.textBox_UDP_Server_IP.Location = new System.Drawing.Point(40, 16);
            this.textBox_UDP_Server_IP.Name = "textBox_UDP_Server_IP";
            this.textBox_UDP_Server_IP.Size = new System.Drawing.Size(96, 20);
            this.textBox_UDP_Server_IP.TabIndex = 8;
            this.textBox_UDP_Server_IP.Text = "127.0.0.1";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(16, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 16);
            this.label6.TabIndex = 42;
            this.label6.Text = "IP";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // button_UDP_Server
            // 
            this.button_UDP_Server.Location = new System.Drawing.Point(232, 16);
            this.button_UDP_Server.Name = "button_UDP_Server";
            this.button_UDP_Server.TabIndex = 10;
            this.button_UDP_Server.Text = "Start";
            this.button_UDP_Server.Click += new System.EventHandler(this.button_UDP_Server_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.label1,
                                                                                    this.textBox_TCP_Server_Port,
                                                                                    this.label3,
                                                                                    this.textBox_TCP_Server_IP,
                                                                                    this.button_TCP_Server});
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(312, 64);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "TCP Server";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 20);
            this.label1.TabIndex = 66;
            this.label1.Text = "IP";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_TCP_Server_Port
            // 
            this.textBox_TCP_Server_Port.Location = new System.Drawing.Point(176, 16);
            this.textBox_TCP_Server_Port.Name = "textBox_TCP_Server_Port";
            this.textBox_TCP_Server_Port.Size = new System.Drawing.Size(48, 20);
            this.textBox_TCP_Server_Port.TabIndex = 1;
            this.textBox_TCP_Server_Port.Text = "6500";
            this.textBox_TCP_Server_Port.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(136, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 38;
            this.label3.Text = "Port";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_TCP_Server_IP
            // 
            this.textBox_TCP_Server_IP.Location = new System.Drawing.Point(40, 16);
            this.textBox_TCP_Server_IP.Name = "textBox_TCP_Server_IP";
            this.textBox_TCP_Server_IP.Size = new System.Drawing.Size(96, 20);
            this.textBox_TCP_Server_IP.TabIndex = 0;
            this.textBox_TCP_Server_IP.Text = "127.0.0.1";
            // 
            // button_TCP_Server
            // 
            this.button_TCP_Server.Location = new System.Drawing.Point(232, 16);
            this.button_TCP_Server.Name = "button_TCP_Server";
            this.button_TCP_Server.Size = new System.Drawing.Size(72, 23);
            this.button_TCP_Server.TabIndex = 2;
            this.button_TCP_Server.Text = "Start";
            this.button_TCP_Server.Click += new System.EventHandler(this.button_TCP_Server_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.label2,
                                                                                    this.textBox_TCP_Client_local_port,
                                                                                    this.checkBox_TCP_Client_Specify_local_port,
                                                                                    this.textBox_TCP_Client_Port,
                                                                                    this.label7,
                                                                                    this.textBox_TCP_Client_IP,
                                                                                    this.button_TCP_Client});
            this.groupBox1.Location = new System.Drawing.Point(0, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 64);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TCP Client";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 20);
            this.label2.TabIndex = 65;
            this.label2.Text = "IP";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_TCP_Client_local_port
            // 
            this.textBox_TCP_Client_local_port.Location = new System.Drawing.Point(136, 40);
            this.textBox_TCP_Client_local_port.Name = "textBox_TCP_Client_local_port";
            this.textBox_TCP_Client_local_port.Size = new System.Drawing.Size(48, 20);
            this.textBox_TCP_Client_local_port.TabIndex = 7;
            this.textBox_TCP_Client_local_port.Text = "1443";
            this.textBox_TCP_Client_local_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // checkBox_TCP_Client_Specify_local_port
            // 
            this.checkBox_TCP_Client_Specify_local_port.Location = new System.Drawing.Point(24, 40);
            this.checkBox_TCP_Client_Specify_local_port.Name = "checkBox_TCP_Client_Specify_local_port";
            this.checkBox_TCP_Client_Specify_local_port.Size = new System.Drawing.Size(112, 16);
            this.checkBox_TCP_Client_Specify_local_port.TabIndex = 6;
            this.checkBox_TCP_Client_Specify_local_port.Text = "Specify local port";
            // 
            // textBox_TCP_Client_Port
            // 
            this.textBox_TCP_Client_Port.Location = new System.Drawing.Point(176, 16);
            this.textBox_TCP_Client_Port.Name = "textBox_TCP_Client_Port";
            this.textBox_TCP_Client_Port.Size = new System.Drawing.Size(48, 20);
            this.textBox_TCP_Client_Port.TabIndex = 4;
            this.textBox_TCP_Client_Port.Text = "6500";
            this.textBox_TCP_Client_Port.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(136, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 16);
            this.label7.TabIndex = 61;
            this.label7.Text = "Port";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_TCP_Client_IP
            // 
            this.textBox_TCP_Client_IP.Location = new System.Drawing.Point(40, 16);
            this.textBox_TCP_Client_IP.Name = "textBox_TCP_Client_IP";
            this.textBox_TCP_Client_IP.Size = new System.Drawing.Size(96, 20);
            this.textBox_TCP_Client_IP.TabIndex = 3;
            this.textBox_TCP_Client_IP.Text = "127.0.0.1";
            // 
            // button_TCP_Client
            // 
            this.button_TCP_Client.Location = new System.Drawing.Point(232, 16);
            this.button_TCP_Client.Name = "button_TCP_Client";
            this.button_TCP_Client.Size = new System.Drawing.Size(72, 23);
            this.button_TCP_Client.TabIndex = 5;
            this.button_TCP_Client.Text = "Connect";
            this.button_TCP_Client.Click += new System.EventHandler(this.button_TCP_Client_Click);
            // 
            // tabPage_icmp
            // 
            this.tabPage_icmp.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                       this.groupBox6,
                                                                                       this.groupBox5,
                                                                                       this.textBox_icmp_delay_dor_reply,
                                                                                       this.label14,
                                                                                       this.textBox_icmp_ip,
                                                                                       this.label11});
            this.tabPage_icmp.Location = new System.Drawing.Point(4, 22);
            this.tabPage_icmp.Name = "tabPage_icmp";
            this.tabPage_icmp.Size = new System.Drawing.Size(872, 128);
            this.tabPage_icmp.TabIndex = 1;
            this.tabPage_icmp.Text = "Icmp";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.button_trace,
                                                                                    this.checkBox_icmp_resolve_adresses,
                                                                                    this.textBox_icmp_end_with_hop,
                                                                                    this.label16,
                                                                                    this.textBox_icmp_start_with_hop,
                                                                                    this.label17});
            this.groupBox6.Location = new System.Drawing.Point(480, 0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(232, 120);
            this.groupBox6.TabIndex = 24;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "TraceRoute";
            // 
            // button_trace
            // 
            this.button_trace.Location = new System.Drawing.Point(144, 88);
            this.button_trace.Name = "button_trace";
            this.button_trace.TabIndex = 9;
            this.button_trace.Text = "Trace";
            this.button_trace.Click += new System.EventHandler(this.button_trace_Click);
            // 
            // checkBox_icmp_resolve_adresses
            // 
            this.checkBox_icmp_resolve_adresses.Checked = true;
            this.checkBox_icmp_resolve_adresses.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_icmp_resolve_adresses.Location = new System.Drawing.Point(8, 72);
            this.checkBox_icmp_resolve_adresses.Name = "checkBox_icmp_resolve_adresses";
            this.checkBox_icmp_resolve_adresses.Size = new System.Drawing.Size(132, 16);
            this.checkBox_icmp_resolve_adresses.TabIndex = 8;
            this.checkBox_icmp_resolve_adresses.Text = "Resolve adresses";
            // 
            // textBox_icmp_end_with_hop
            // 
            this.textBox_icmp_end_with_hop.Location = new System.Drawing.Point(120, 48);
            this.textBox_icmp_end_with_hop.Name = "textBox_icmp_end_with_hop";
            this.textBox_icmp_end_with_hop.Size = new System.Drawing.Size(40, 20);
            this.textBox_icmp_end_with_hop.TabIndex = 7;
            this.textBox_icmp_end_with_hop.Text = "20";
            this.textBox_icmp_end_with_hop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(8, 48);
            this.label16.Name = "label16";
            this.label16.TabIndex = 25;
            this.label16.Text = "End with hop";
            // 
            // textBox_icmp_start_with_hop
            // 
            this.textBox_icmp_start_with_hop.Location = new System.Drawing.Point(120, 24);
            this.textBox_icmp_start_with_hop.Name = "textBox_icmp_start_with_hop";
            this.textBox_icmp_start_with_hop.Size = new System.Drawing.Size(40, 20);
            this.textBox_icmp_start_with_hop.TabIndex = 6;
            this.textBox_icmp_start_with_hop.Text = "1";
            this.textBox_icmp_start_with_hop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(8, 26);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(120, 16);
            this.label17.TabIndex = 23;
            this.label17.Text = "Start with hop";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.checkBox_icmp_may_broadcast,
                                                                                    this.checkBox_icmp_looping_ping,
                                                                                    this.textBox_icmp_ping_number,
                                                                                    this.label15,
                                                                                    this.button_ping,
                                                                                    this.textBox_icmp_packet_ttl,
                                                                                    this.label13});
            this.groupBox5.Location = new System.Drawing.Point(208, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(264, 120);
            this.groupBox5.TabIndex = 19;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Ping";
            // 
            // checkBox_icmp_may_broadcast
            // 
            this.checkBox_icmp_may_broadcast.Location = new System.Drawing.Point(8, 80);
            this.checkBox_icmp_may_broadcast.Name = "checkBox_icmp_may_broadcast";
            this.checkBox_icmp_may_broadcast.Size = new System.Drawing.Size(160, 32);
            this.checkBox_icmp_may_broadcast.TabIndex = 41;
            this.checkBox_icmp_may_broadcast.Text = "More than one host can reply";
            // 
            // checkBox_icmp_looping_ping
            // 
            this.checkBox_icmp_looping_ping.Location = new System.Drawing.Point(8, 64);
            this.checkBox_icmp_looping_ping.Name = "checkBox_icmp_looping_ping";
            this.checkBox_icmp_looping_ping.Size = new System.Drawing.Size(104, 16);
            this.checkBox_icmp_looping_ping.TabIndex = 4;
            this.checkBox_icmp_looping_ping.Text = "Looping pings";
            this.checkBox_icmp_looping_ping.CheckedChanged += new System.EventHandler(this.checkBox_icmp_looping_ping_CheckedChanged);
            // 
            // textBox_icmp_ping_number
            // 
            this.textBox_icmp_ping_number.Location = new System.Drawing.Point(128, 40);
            this.textBox_icmp_ping_number.Name = "textBox_icmp_ping_number";
            this.textBox_icmp_ping_number.Size = new System.Drawing.Size(40, 20);
            this.textBox_icmp_ping_number.TabIndex = 3;
            this.textBox_icmp_ping_number.Text = "3";
            this.textBox_icmp_ping_number.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(8, 40);
            this.label15.Name = "label15";
            this.label15.TabIndex = 20;
            this.label15.Text = "Number of ping";
            // 
            // button_ping
            // 
            this.button_ping.Location = new System.Drawing.Point(176, 88);
            this.button_ping.Name = "button_ping";
            this.button_ping.TabIndex = 5;
            this.button_ping.Text = "Ping";
            this.button_ping.Click += new System.EventHandler(this.button_ping_Click);
            // 
            // textBox_icmp_packet_ttl
            // 
            this.textBox_icmp_packet_ttl.Location = new System.Drawing.Point(128, 16);
            this.textBox_icmp_packet_ttl.Name = "textBox_icmp_packet_ttl";
            this.textBox_icmp_packet_ttl.Size = new System.Drawing.Size(40, 20);
            this.textBox_icmp_packet_ttl.TabIndex = 2;
            this.textBox_icmp_packet_ttl.Text = "128";
            this.textBox_icmp_packet_ttl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(8, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(120, 16);
            this.label13.TabIndex = 17;
            this.label13.Text = "Packet TTL (max 255)";
            // 
            // textBox_icmp_delay_dor_reply
            // 
            this.textBox_icmp_delay_dor_reply.Location = new System.Drawing.Point(144, 32);
            this.textBox_icmp_delay_dor_reply.Name = "textBox_icmp_delay_dor_reply";
            this.textBox_icmp_delay_dor_reply.Size = new System.Drawing.Size(48, 20);
            this.textBox_icmp_delay_dor_reply.TabIndex = 1;
            this.textBox_icmp_delay_dor_reply.Text = "3000";
            this.textBox_icmp_delay_dor_reply.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(8, 32);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(128, 16);
            this.label14.TabIndex = 17;
            this.label14.Text = "Delay for reply (in ms)";
            // 
            // textBox_icmp_ip
            // 
            this.textBox_icmp_ip.Location = new System.Drawing.Point(96, 8);
            this.textBox_icmp_ip.Name = "textBox_icmp_ip";
            this.textBox_icmp_ip.Size = new System.Drawing.Size(96, 20);
            this.textBox_icmp_ip.TabIndex = 0;
            this.textBox_icmp_ip.Text = "127.0.0.1";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(8, 8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 16);
            this.label11.TabIndex = 13;
            this.label11.Text = "Host";
            // 
            // tabPage_dns
            // 
            this.tabPage_dns.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                      this.textBox_dns_result,
                                                                                      this.textBox_dns_ip,
                                                                                      this.label9,
                                                                                      this.button_dns});
            this.tabPage_dns.Location = new System.Drawing.Point(4, 22);
            this.tabPage_dns.Name = "tabPage_dns";
            this.tabPage_dns.Size = new System.Drawing.Size(872, 128);
            this.tabPage_dns.TabIndex = 2;
            this.tabPage_dns.Text = "DNS";
            // 
            // textBox_dns_result
            // 
            this.textBox_dns_result.Location = new System.Drawing.Point(8, 32);
            this.textBox_dns_result.Multiline = true;
            this.textBox_dns_result.Name = "textBox_dns_result";
            this.textBox_dns_result.ReadOnly = true;
            this.textBox_dns_result.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_dns_result.Size = new System.Drawing.Size(216, 88);
            this.textBox_dns_result.TabIndex = 2;
            this.textBox_dns_result.Text = "";
            // 
            // textBox_dns_ip
            // 
            this.textBox_dns_ip.Location = new System.Drawing.Point(120, 8);
            this.textBox_dns_ip.Name = "textBox_dns_ip";
            this.textBox_dns_ip.Size = new System.Drawing.Size(102, 20);
            this.textBox_dns_ip.TabIndex = 0;
            this.textBox_dns_ip.Text = "127.0.0.1";
            this.textBox_dns_ip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_dns_ip_KeyPress);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(8, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 16);
            this.label9.TabIndex = 11;
            this.label9.Text = "Host to resolve";
            // 
            // button_dns
            // 
            this.button_dns.Location = new System.Drawing.Point(240, 8);
            this.button_dns.Name = "button_dns";
            this.button_dns.Size = new System.Drawing.Size(88, 23);
            this.button_dns.TabIndex = 1;
            this.button_dns.Text = "DNS Resolve";
            this.button_dns.Click += new System.EventHandler(this.button_dns_Click);
            // 
            // tabPage_whois
            // 
            this.tabPage_whois.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                        this.textBox_whois_server,
                                                                                        this.radioButton_whois_use_following,
                                                                                        this.radioButton_whois_auto_find,
                                                                                        this.button_whois,
                                                                                        this.textBox_whois_ip,
                                                                                        this.label18});
            this.tabPage_whois.Location = new System.Drawing.Point(4, 22);
            this.tabPage_whois.Name = "tabPage_whois";
            this.tabPage_whois.Size = new System.Drawing.Size(872, 128);
            this.tabPage_whois.TabIndex = 3;
            this.tabPage_whois.Text = "Whois";
            // 
            // textBox_whois_server
            // 
            this.textBox_whois_server.Enabled = false;
            this.textBox_whois_server.Location = new System.Drawing.Point(192, 80);
            this.textBox_whois_server.Name = "textBox_whois_server";
            this.textBox_whois_server.Size = new System.Drawing.Size(152, 20);
            this.textBox_whois_server.TabIndex = 5;
            this.textBox_whois_server.Text = "127.0.0.1";
            // 
            // radioButton_whois_use_following
            // 
            this.radioButton_whois_use_following.Location = new System.Drawing.Point(32, 80);
            this.radioButton_whois_use_following.Name = "radioButton_whois_use_following";
            this.radioButton_whois_use_following.Size = new System.Drawing.Size(160, 24);
            this.radioButton_whois_use_following.TabIndex = 4;
            this.radioButton_whois_use_following.Text = "Use the following server";
            // 
            // radioButton_whois_auto_find
            // 
            this.radioButton_whois_auto_find.Checked = true;
            this.radioButton_whois_auto_find.Location = new System.Drawing.Point(32, 56);
            this.radioButton_whois_auto_find.Name = "radioButton_whois_auto_find";
            this.radioButton_whois_auto_find.Size = new System.Drawing.Size(232, 24);
            this.radioButton_whois_auto_find.TabIndex = 3;
            this.radioButton_whois_auto_find.TabStop = true;
            this.radioButton_whois_auto_find.Text = "Find whois server automatically";
            this.radioButton_whois_auto_find.CheckedChanged += new System.EventHandler(this.radioButton_whois_auto_find_CheckedChanged);
            // 
            // button_whois
            // 
            this.button_whois.Location = new System.Drawing.Point(272, 24);
            this.button_whois.Name = "button_whois";
            this.button_whois.TabIndex = 2;
            this.button_whois.Text = "Whois";
            this.button_whois.Click += new System.EventHandler(this.button_whois_Click);
            // 
            // textBox_whois_ip
            // 
            this.textBox_whois_ip.Location = new System.Drawing.Point(56, 24);
            this.textBox_whois_ip.Name = "textBox_whois_ip";
            this.textBox_whois_ip.Size = new System.Drawing.Size(144, 20);
            this.textBox_whois_ip.TabIndex = 1;
            this.textBox_whois_ip.Text = "";
            this.textBox_whois_ip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_whois_ip_KeyPress);
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(16, 24);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(40, 16);
            this.label18.TabIndex = 0;
            this.label18.Text = "IP";
            // 
            // tabPage_arp
            // 
            this.tabPage_arp.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                      this.textBox_arp_result,
                                                                                      this.label8,
                                                                                      this.textBox_arp_ip,
                                                                                      this.label5,
                                                                                      this.button_arp});
            this.tabPage_arp.Location = new System.Drawing.Point(4, 22);
            this.tabPage_arp.Name = "tabPage_arp";
            this.tabPage_arp.Size = new System.Drawing.Size(872, 128);
            this.tabPage_arp.TabIndex = 4;
            this.tabPage_arp.Text = "ARP";
            // 
            // textBox_arp_result
            // 
            this.textBox_arp_result.Location = new System.Drawing.Point(56, 72);
            this.textBox_arp_result.Name = "textBox_arp_result";
            this.textBox_arp_result.ReadOnly = true;
            this.textBox_arp_result.Size = new System.Drawing.Size(136, 20);
            this.textBox_arp_result.TabIndex = 2;
            this.textBox_arp_result.Text = "";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 48);
            this.label8.Name = "label8";
            this.label8.TabIndex = 12;
            this.label8.Text = "MAC Address";
            // 
            // textBox_arp_ip
            // 
            this.textBox_arp_ip.Location = new System.Drawing.Point(56, 16);
            this.textBox_arp_ip.Name = "textBox_arp_ip";
            this.textBox_arp_ip.Size = new System.Drawing.Size(88, 20);
            this.textBox_arp_ip.TabIndex = 0;
            this.textBox_arp_ip.Text = "10.0.0.1";
            this.textBox_arp_ip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_arp_ip_KeyPress);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "IP";
            // 
            // button_arp
            // 
            this.button_arp.Location = new System.Drawing.Point(152, 16);
            this.button_arp.Name = "button_arp";
            this.button_arp.TabIndex = 1;
            this.button_arp.Text = "Send ARP";
            this.button_arp.Click += new System.EventHandler(this.button_arp_Click);
            // 
            // tabPage_stat
            // 
            this.tabPage_stat.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                       this.button_ipnet_stats,
                                                                                       this.button_ipnet_table,
                                                                                       this.button_icmp_stats,
                                                                                       this.button_udp_stats,
                                                                                       this.button_udp_table,
                                                                                       this.button_tcp_stat,
                                                                                       this.button_tcp_table});
            this.tabPage_stat.Location = new System.Drawing.Point(4, 22);
            this.tabPage_stat.Name = "tabPage_stat";
            this.tabPage_stat.Size = new System.Drawing.Size(872, 128);
            this.tabPage_stat.TabIndex = 5;
            this.tabPage_stat.Text = "Stats";
            // 
            // button_ipnet_stats
            // 
            this.button_ipnet_stats.Location = new System.Drawing.Point(184, 40);
            this.button_ipnet_stats.Name = "button_ipnet_stats";
            this.button_ipnet_stats.TabIndex = 15;
            this.button_ipnet_stats.Text = "IP Stats";
            this.button_ipnet_stats.Click += new System.EventHandler(this.button_ipnet_stats_Click);
            // 
            // button_ipnet_table
            // 
            this.button_ipnet_table.Location = new System.Drawing.Point(184, 8);
            this.button_ipnet_table.Name = "button_ipnet_table";
            this.button_ipnet_table.TabIndex = 14;
            this.button_ipnet_table.Text = "IP Table";
            this.button_ipnet_table.Click += new System.EventHandler(this.button_ipnet_table_Click);
            // 
            // button_icmp_stats
            // 
            this.button_icmp_stats.Location = new System.Drawing.Point(272, 8);
            this.button_icmp_stats.Name = "button_icmp_stats";
            this.button_icmp_stats.TabIndex = 13;
            this.button_icmp_stats.Text = "ICMP Stats";
            this.button_icmp_stats.Click += new System.EventHandler(this.button_icmp_stats_Click);
            // 
            // button_udp_stats
            // 
            this.button_udp_stats.Location = new System.Drawing.Point(96, 40);
            this.button_udp_stats.Name = "button_udp_stats";
            this.button_udp_stats.TabIndex = 12;
            this.button_udp_stats.Text = "UDP Stats";
            this.button_udp_stats.Click += new System.EventHandler(this.button_udp_stats_Click);
            // 
            // button_udp_table
            // 
            this.button_udp_table.Location = new System.Drawing.Point(96, 8);
            this.button_udp_table.Name = "button_udp_table";
            this.button_udp_table.TabIndex = 10;
            this.button_udp_table.Text = "UDP Table";
            this.button_udp_table.Click += new System.EventHandler(this.button_udp_table_Click);
            // 
            // button_tcp_stat
            // 
            this.button_tcp_stat.Location = new System.Drawing.Point(8, 40);
            this.button_tcp_stat.Name = "button_tcp_stat";
            this.button_tcp_stat.TabIndex = 11;
            this.button_tcp_stat.Text = "TCP Stats";
            this.button_tcp_stat.Click += new System.EventHandler(this.button_tcp_stat_Click);
            // 
            // button_tcp_table
            // 
            this.button_tcp_table.Location = new System.Drawing.Point(8, 8);
            this.button_tcp_table.Name = "button_tcp_table";
            this.button_tcp_table.TabIndex = 9;
            this.button_tcp_table.Text = "TCP Table";
            this.button_tcp_table.Click += new System.EventHandler(this.button_tcp_table_Click);
            // 
            // tabPageScan
            // 
            this.tabPageScan.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                      this.button_scan_cgi,
                                                                                      this.button_scan_icmp,
                                                                                      this.button_scan_udp,
                                                                                      this.button_scan_tcp});
            this.tabPageScan.Location = new System.Drawing.Point(4, 22);
            this.tabPageScan.Name = "tabPageScan";
            this.tabPageScan.Size = new System.Drawing.Size(872, 128);
            this.tabPageScan.TabIndex = 6;
            this.tabPageScan.Text = "Scan";
            // 
            // button_scan_cgi
            // 
            this.button_scan_cgi.Location = new System.Drawing.Point(272, 8);
            this.button_scan_cgi.Name = "button_scan_cgi";
            this.button_scan_cgi.TabIndex = 3;
            this.button_scan_cgi.Text = "Cgi";
            this.button_scan_cgi.Click += new System.EventHandler(this.button_scan_cgi_Click);
            // 
            // button_scan_icmp
            // 
            this.button_scan_icmp.Location = new System.Drawing.Point(184, 8);
            this.button_scan_icmp.Name = "button_scan_icmp";
            this.button_scan_icmp.TabIndex = 2;
            this.button_scan_icmp.Text = "Icmp";
            this.button_scan_icmp.Click += new System.EventHandler(this.button_scan_icmp_Click);
            // 
            // button_scan_udp
            // 
            this.button_scan_udp.Location = new System.Drawing.Point(96, 8);
            this.button_scan_udp.Name = "button_scan_udp";
            this.button_scan_udp.TabIndex = 1;
            this.button_scan_udp.Text = "Udp";
            this.button_scan_udp.Click += new System.EventHandler(this.button_scan_udp_Click);
            // 
            // button_scan_tcp
            // 
            this.button_scan_tcp.Location = new System.Drawing.Point(8, 8);
            this.button_scan_tcp.Name = "button_scan_tcp";
            this.button_scan_tcp.TabIndex = 0;
            this.button_scan_tcp.Text = "Tcp";
            this.button_scan_tcp.Click += new System.EventHandler(this.button_scan_tcp_Click);
            // 
            // tabPagePacket
            // 
            this.tabPagePacket.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                        this.label25,
                                                                                        this.label24,
                                                                                        this.button_raw_packet_capture,
                                                                                        this.button_raw_packet_forge});
            this.tabPagePacket.Location = new System.Drawing.Point(4, 22);
            this.tabPagePacket.Name = "tabPagePacket";
            this.tabPagePacket.Size = new System.Drawing.Size(872, 128);
            this.tabPagePacket.TabIndex = 7;
            this.tabPagePacket.Text = "Raw Packets";
            // 
            // label25
            // 
            this.label25.Location = new System.Drawing.Point(128, 64);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(256, 23);
            this.label25.TabIndex = 4;
            this.label25.Text = "Allow you to forge any packet based on IP stack";
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(128, 32);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(256, 23);
            this.label24.TabIndex = 3;
            this.label24.Text = "Allow you to capture any packet using IP stack";
            // 
            // button_raw_packet_capture
            // 
            this.button_raw_packet_capture.Location = new System.Drawing.Point(32, 32);
            this.button_raw_packet_capture.Name = "button_raw_packet_capture";
            this.button_raw_packet_capture.TabIndex = 1;
            this.button_raw_packet_capture.Text = "Capture";
            this.button_raw_packet_capture.Click += new System.EventHandler(this.button_raw_packet_capture_Click);
            // 
            // button_raw_packet_forge
            // 
            this.button_raw_packet_forge.Location = new System.Drawing.Point(32, 64);
            this.button_raw_packet_forge.Name = "button_raw_packet_forge";
            this.button_raw_packet_forge.TabIndex = 2;
            this.button_raw_packet_forge.Text = "Forge";
            this.button_raw_packet_forge.Click += new System.EventHandler(this.button_raw_packet_forge_Click);
            // 
            // tabPageWakeOnLan
            // 
            this.tabPageWakeOnLan.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                           this.groupBox9,
                                                                                           this.button_WOL_wake_up,
                                                                                           this.label23,
                                                                                           this.label22,
                                                                                           this.label21,
                                                                                           this.textBox_WOL_udp_port,
                                                                                           this.textBox_WOL_broadcast_ip,
                                                                                           this.textBox_WOL_mac_address});
            this.tabPageWakeOnLan.Location = new System.Drawing.Point(4, 22);
            this.tabPageWakeOnLan.Name = "tabPageWakeOnLan";
            this.tabPageWakeOnLan.Size = new System.Drawing.Size(872, 128);
            this.tabPageWakeOnLan.TabIndex = 8;
            this.tabPageWakeOnLan.Text = "Wake On Lan";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.label29,
                                                                                    this.button_remote_shutdown_abort,
                                                                                    this.button_remote_shutdown_initiate,
                                                                                    this.numericUpDown_remote_shutdown_timeout,
                                                                                    this.label28,
                                                                                    this.label27,
                                                                                    this.checkBox_remote_shutdown_reset,
                                                                                    this.checkBox_remote_shutdown_force_close,
                                                                                    this.textBox_remote_shutdown_message,
                                                                                    this.label26,
                                                                                    this.textBox_remote_shutdown_computer_name});
            this.groupBox9.Location = new System.Drawing.Point(264, 0);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(584, 120);
            this.groupBox9.TabIndex = 7;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Windows NT/2000/XP shutdown";
            // 
            // label29
            // 
            this.label29.Location = new System.Drawing.Point(384, 24);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(192, 40);
            this.label29.TabIndex = 11;
            this.label29.Text = "Note: This will work only if you have sufficient privilege on the remote computer" +
                "";
            // 
            // button_remote_shutdown_abort
            // 
            this.button_remote_shutdown_abort.Location = new System.Drawing.Point(488, 72);
            this.button_remote_shutdown_abort.Name = "button_remote_shutdown_abort";
            this.button_remote_shutdown_abort.TabIndex = 6;
            this.button_remote_shutdown_abort.Text = "Abort";
            this.button_remote_shutdown_abort.Click += new System.EventHandler(this.button_remote_shutdown_abort_Click);
            // 
            // button_remote_shutdown_initiate
            // 
            this.button_remote_shutdown_initiate.Location = new System.Drawing.Point(400, 72);
            this.button_remote_shutdown_initiate.Name = "button_remote_shutdown_initiate";
            this.button_remote_shutdown_initiate.TabIndex = 5;
            this.button_remote_shutdown_initiate.Text = "Initiate";
            this.button_remote_shutdown_initiate.Click += new System.EventHandler(this.button_remote_shutdown_initiate_Click);
            // 
            // numericUpDown_remote_shutdown_timeout
            // 
            this.numericUpDown_remote_shutdown_timeout.Location = new System.Drawing.Point(232, 64);
            this.numericUpDown_remote_shutdown_timeout.Maximum = new System.Decimal(new int[] {
                                                                                                  -1,
                                                                                                  0,
                                                                                                  0,
                                                                                                  0});
            this.numericUpDown_remote_shutdown_timeout.Name = "numericUpDown_remote_shutdown_timeout";
            this.numericUpDown_remote_shutdown_timeout.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown_remote_shutdown_timeout.TabIndex = 2;
            this.numericUpDown_remote_shutdown_timeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_remote_shutdown_timeout.Value = new System.Decimal(new int[] {
                                                                                                20,
                                                                                                0,
                                                                                                0,
                                                                                                0});
            // 
            // label28
            // 
            this.label28.Location = new System.Drawing.Point(8, 64);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(168, 16);
            this.label28.TabIndex = 7;
            this.label28.Text = "Timeout before shutdown (in s)";
            // 
            // label27
            // 
            this.label27.Location = new System.Drawing.Point(8, 40);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(120, 16);
            this.label27.TabIndex = 6;
            this.label27.Text = "Message to display";
            // 
            // checkBox_remote_shutdown_reset
            // 
            this.checkBox_remote_shutdown_reset.Location = new System.Drawing.Point(232, 88);
            this.checkBox_remote_shutdown_reset.Name = "checkBox_remote_shutdown_reset";
            this.checkBox_remote_shutdown_reset.Size = new System.Drawing.Size(136, 16);
            this.checkBox_remote_shutdown_reset.TabIndex = 4;
            this.checkBox_remote_shutdown_reset.Text = "Reset after shutdown";
            // 
            // checkBox_remote_shutdown_force_close
            // 
            this.checkBox_remote_shutdown_force_close.Checked = true;
            this.checkBox_remote_shutdown_force_close.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_remote_shutdown_force_close.Location = new System.Drawing.Point(8, 88);
            this.checkBox_remote_shutdown_force_close.Name = "checkBox_remote_shutdown_force_close";
            this.checkBox_remote_shutdown_force_close.Size = new System.Drawing.Size(208, 16);
            this.checkBox_remote_shutdown_force_close.TabIndex = 3;
            this.checkBox_remote_shutdown_force_close.Text = "Force applications to close";
            // 
            // textBox_remote_shutdown_message
            // 
            this.textBox_remote_shutdown_message.Location = new System.Drawing.Point(128, 40);
            this.textBox_remote_shutdown_message.Name = "textBox_remote_shutdown_message";
            this.textBox_remote_shutdown_message.Size = new System.Drawing.Size(248, 20);
            this.textBox_remote_shutdown_message.TabIndex = 1;
            this.textBox_remote_shutdown_message.Text = "Administrator is going to shutdown your computer";
            // 
            // label26
            // 
            this.label26.Location = new System.Drawing.Point(8, 16);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(144, 16);
            this.label26.TabIndex = 1;
            this.label26.Text = "Computer\'s name or IP";
            // 
            // textBox_remote_shutdown_computer_name
            // 
            this.textBox_remote_shutdown_computer_name.Location = new System.Drawing.Point(192, 16);
            this.textBox_remote_shutdown_computer_name.Name = "textBox_remote_shutdown_computer_name";
            this.textBox_remote_shutdown_computer_name.Size = new System.Drawing.Size(184, 20);
            this.textBox_remote_shutdown_computer_name.TabIndex = 0;
            this.textBox_remote_shutdown_computer_name.Text = "\\\\computer_name\\";
            // 
            // button_WOL_wake_up
            // 
            this.button_WOL_wake_up.Location = new System.Drawing.Point(136, 88);
            this.button_WOL_wake_up.Name = "button_WOL_wake_up";
            this.button_WOL_wake_up.TabIndex = 6;
            this.button_WOL_wake_up.Text = "Wake";
            this.button_WOL_wake_up.Click += new System.EventHandler(this.button_WOL_wake_up_Click);
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(8, 56);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(100, 24);
            this.label23.TabIndex = 5;
            this.label23.Text = "UDP port";
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(8, 32);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(88, 24);
            this.label22.TabIndex = 4;
            this.label22.Text = "Broadcast IP";
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(8, 8);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(100, 24);
            this.label21.TabIndex = 3;
            this.label21.Text = "Mac address";
            // 
            // textBox_WOL_udp_port
            // 
            this.textBox_WOL_udp_port.Location = new System.Drawing.Point(156, 56);
            this.textBox_WOL_udp_port.Name = "textBox_WOL_udp_port";
            this.textBox_WOL_udp_port.Size = new System.Drawing.Size(56, 20);
            this.textBox_WOL_udp_port.TabIndex = 2;
            this.textBox_WOL_udp_port.Text = "1111";
            this.textBox_WOL_udp_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox_WOL_broadcast_ip
            // 
            this.textBox_WOL_broadcast_ip.Location = new System.Drawing.Point(112, 32);
            this.textBox_WOL_broadcast_ip.Name = "textBox_WOL_broadcast_ip";
            this.textBox_WOL_broadcast_ip.TabIndex = 1;
            this.textBox_WOL_broadcast_ip.Text = "255.255.255.255";
            // 
            // textBox_WOL_mac_address
            // 
            this.textBox_WOL_mac_address.Location = new System.Drawing.Point(112, 8);
            this.textBox_WOL_mac_address.Name = "textBox_WOL_mac_address";
            this.textBox_WOL_mac_address.TabIndex = 0;
            this.textBox_WOL_mac_address.Text = "00-01-02-AF-A3-C0";
            // 
            // tabPage_computerIp
            // 
            this.tabPage_computerIp.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                             this.groupBox8,
                                                                                             this.groupBox7});
            this.tabPage_computerIp.Location = new System.Drawing.Point(4, 22);
            this.tabPage_computerIp.Name = "tabPage_computerIp";
            this.tabPage_computerIp.Size = new System.Drawing.Size(872, 128);
            this.tabPage_computerIp.TabIndex = 9;
            this.tabPage_computerIp.Text = "Computer\'s IP";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.button_local_ip_refresh,
                                                                                    this.textBox_local_ip});
            this.groupBox8.Location = new System.Drawing.Point(384, 0);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(288, 96);
            this.groupBox8.TabIndex = 6;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Local IP(s)";
            // 
            // button_local_ip_refresh
            // 
            this.button_local_ip_refresh.Location = new System.Drawing.Point(200, 16);
            this.button_local_ip_refresh.Name = "button_local_ip_refresh";
            this.button_local_ip_refresh.TabIndex = 1;
            this.button_local_ip_refresh.Text = "Refresh";
            this.button_local_ip_refresh.Click += new System.EventHandler(this.button_local_ip_refresh_Click);
            // 
            // textBox_local_ip
            // 
            this.textBox_local_ip.Location = new System.Drawing.Point(8, 16);
            this.textBox_local_ip.Multiline = true;
            this.textBox_local_ip.Name = "textBox_local_ip";
            this.textBox_local_ip.ReadOnly = true;
            this.textBox_local_ip.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_local_ip.Size = new System.Drawing.Size(184, 72);
            this.textBox_local_ip.TabIndex = 0;
            this.textBox_local_ip.Text = "";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.label20,
                                                                                    this.textBox_outside_ip_ip,
                                                                                    this.button_outside_ip_get,
                                                                                    this.textBox_outside_ip_server,
                                                                                    this.label19});
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(376, 96);
            this.groupBox7.TabIndex = 5;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Outside IP";
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(16, 48);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(120, 16);
            this.label20.TabIndex = 9;
            this.label20.Text = "Your outside IP";
            // 
            // textBox_outside_ip_ip
            // 
            this.textBox_outside_ip_ip.Location = new System.Drawing.Point(152, 48);
            this.textBox_outside_ip_ip.Name = "textBox_outside_ip_ip";
            this.textBox_outside_ip_ip.ReadOnly = true;
            this.textBox_outside_ip_ip.Size = new System.Drawing.Size(120, 20);
            this.textBox_outside_ip_ip.TabIndex = 8;
            this.textBox_outside_ip_ip.Text = "";
            // 
            // button_outside_ip_get
            // 
            this.button_outside_ip_get.Location = new System.Drawing.Point(288, 16);
            this.button_outside_ip_get.Name = "button_outside_ip_get";
            this.button_outside_ip_get.TabIndex = 7;
            this.button_outside_ip_get.Text = "Get";
            this.button_outside_ip_get.Click += new System.EventHandler(this.button_outside_ip_get_Click);
            // 
            // textBox_outside_ip_server
            // 
            this.textBox_outside_ip_server.Location = new System.Drawing.Point(152, 16);
            this.textBox_outside_ip_server.Name = "textBox_outside_ip_server";
            this.textBox_outside_ip_server.Size = new System.Drawing.Size(120, 20);
            this.textBox_outside_ip_server.TabIndex = 6;
            this.textBox_outside_ip_server.Text = "checkip.dyndns.org";
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(8, 16);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(136, 24);
            this.label19.TabIndex = 5;
            this.label19.Text = "Server used to check IP";
            // 
            // imageList_tabpage_icons
            // 
            this.imageList_tabpage_icons.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList_tabpage_icons.ImageSize = new System.Drawing.Size(10, 10);
            this.imageList_tabpage_icons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_tabpage_icons.ImageStream")));
            this.imageList_tabpage_icons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel_mdi_tab
            // 
            this.panel_mdi_tab.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                        this.tabControl_mdichild});
            this.panel_mdi_tab.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_mdi_tab.Location = new System.Drawing.Point(0, 422);
            this.panel_mdi_tab.Name = "panel_mdi_tab";
            this.panel_mdi_tab.Size = new System.Drawing.Size(880, 24);
            this.panel_mdi_tab.TabIndex = 1;
            // 
            // tabControl_mdichild
            // 
            this.tabControl_mdichild.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl_mdichild.ImageList = this.imageList_tabpage_icons;
            this.tabControl_mdichild.Name = "tabControl_mdichild";
            this.tabControl_mdichild.SelectedIndex = 0;
            this.tabControl_mdichild.Size = new System.Drawing.Size(880, 24);
            this.tabControl_mdichild.TabIndex = 15;
            this.tabControl_mdichild.Click += new System.EventHandler(this.tabControl_mdichild_Click);
            this.tabControl_mdichild.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tabControl_mdichild_MouseUp);
            this.tabControl_mdichild.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tabControl_mdichild_MouseMove);
            this.tabControl_mdichild.MouseLeave += new System.EventHandler(this.tabControl_mdichild_MouseLeave);
            // 
            // panel_tabcontrol_main
            // 
            this.panel_tabcontrol_main.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                                this.tabControlmain});
            this.panel_tabcontrol_main.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_tabcontrol_main.Name = "panel_tabcontrol_main";
            this.panel_tabcontrol_main.Size = new System.Drawing.Size(880, 154);
            this.panel_tabcontrol_main.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(880, 446);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.panel_tabcontrol_main,
                                                                          this.panel_mdi_tab});
            this.IsMdiContainer = true;
            this.Name = "FormMain";
            this.Text = "Network Stuff";
            this.MdiChildActivate += new System.EventHandler(this.FormMain_MdiChildActivate);
            this.tabControlmain.ResumeLayout(false);
            this.tabPage_telnet.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabPage_icmp.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.tabPage_dns.ResumeLayout(false);
            this.tabPage_whois.ResumeLayout(false);
            this.tabPage_arp.ResumeLayout(false);
            this.tabPage_stat.ResumeLayout(false);
            this.tabPageScan.ResumeLayout(false);
            this.tabPagePacket.ResumeLayout(false);
            this.tabPageWakeOnLan.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_remote_shutdown_timeout)).EndInit();
            this.tabPage_computerIp.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.panel_mdi_tab.ResumeLayout(false);
            this.panel_tabcontrol_main.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        [STAThread]
        static void Main() 
        {
            #if (DEBUG) // let ide catch error
                Application.Run(new FormMain());
            #else
            try
            {
                Application.Run(new FormMain());
            }
            catch(Exception e)
            {
                FormMainCatch fmc=new FormMainCatch(e);
                fmc.ShowDialog();
            }
            #endif
        }

        #region tab for children windows
        private void FormMain_MdiChildActivate(object sender, System.EventArgs e)
        {
            System.Windows.Forms.Form frm=this.MdiChildren[this.MdiChildren.Length-1];
            // if Mdiform creation
            if (frm.Tag==null)// add_Tab
            {
                // search an unique caption for the form and the tabpage
                string frm_caption=frm.Text;
                int cpt=2;
                while(this.check_if_tab_exist(frm_caption))
                {
                    frm_caption=frm.Text+" ("+cpt+")";
                    cpt++;
                }
                frm.Text=frm_caption;
                TabPage tp=new TabPage(frm.Text);
                // add close image on the tabpage
                tp.ImageIndex=0;// unselected state
                frm.Tag=tp;// associate mdi child window and tabpage
                // add tab page
                this.tabControl_mdichild.TabPages.Add(tp);
                // add event handler on the mdi child
                frm.Closed+=new EventHandler(MdiChild_Closed);
                frm.TextChanged+=new EventHandler(MdiChild_TextChanged);
                // check if it is the first mdichild
                if (this.MdiChildren.Length==1)
                {
                    // show tabcontrol
                    this.panel_mdi_tab.Visible=true;
                }
                this.tabControl_mdichild.SelectedTab=tp;
            }
            else // activate corresponding tabpage
            {
                if (this.ActiveMdiChild==null)
                    return;
                frm=this.ActiveMdiChild;
                if (!(frm.Tag is TabPage))
                    return;
                // unactivate old tabpage image selection
                this.tabControl_set_close_image_state(false);
                // activate associated tabpage
                TabPage tp=(TabPage)frm.Tag;
                this.tabControl_mdichild.SelectedTab=tp;
            }
        }
        /// <summary>
        /// check if caption already exist for another tab/mdiChildForm
        /// </summary>
        /// <param name="caption"></param>
        /// <returns></returns>
        private bool check_if_tab_exist(string caption)
        {
            for (int cpt=0;cpt<this.tabControl_mdichild.TabPages.Count;cpt++)
                if (this.tabControl_mdichild.TabPages[cpt].Text==caption)
                    return true;
            return false;
        }
        private void MdiChild_Closed(object sender, EventArgs e)
        {
            if (!(sender is System.Windows.Forms.Form))
                return;
            // remove tab
            this.tabControl_mdichild.TabPages.Remove((TabPage)((System.Windows.Forms.Form)sender).Tag);
            if(this.tabControl_mdichild.TabPages.Count==0)
                //hide tab control
                this.panel_mdi_tab.Visible=false;
        }
        private void MdiChild_TextChanged(object sender, EventArgs e)
        {
            if (!(sender is System.Windows.Forms.Form))
                return;
            System.Windows.Forms.Form frm=(System.Windows.Forms.Form)sender;
            if (!(frm.Tag is TabPage))
                return;
            TabPage tp=(TabPage)frm.Tag;
            tp.Text=frm.Text;
        }
        private void tabControl_mdichild_Click(object sender, EventArgs e)
        {
            // activate mdi child associated with the tabpage
            TabPage tp=this.tabControl_mdichild.SelectedTab;
            System.Windows.Forms.Form mdic=this.tabControl_get_associated_form(tp);
            if (mdic==null)
                return;
            mdic.Activate();
        }
        /// <summary>
        /// get the mdi child form associated with the TabPage
        /// </summary>
        /// <param name="tp"></param>
        /// <returns></returns>
        private System.Windows.Forms.Form tabControl_get_associated_form(TabPage tp)
        {
            for (int cpt=0;cpt<this.MdiChildren.Length;cpt++)
            {
                if ((TabPage)this.MdiChildren[cpt].Tag==tp)
                {
                    return this.MdiChildren[cpt];
                }
            }
            return null;
        }
        /// <summary>
        /// used as mouseclick. MouseUp is used to get mouse position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl_mdichild_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // close associated form if exist
            if (!this.tabControl_is_mouse_on_close_image(e))
                return;
            System.Windows.Forms.Form mdic=this.tabControl_get_associated_form(this.tabControl_mdichild.SelectedTab);
            if (mdic==null)
                return;
            mdic.Close();
        }
        private void tabControl_mdichild_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // check if mouse is over the close image, and if it is, change close image
            this.tabControl_set_close_image_state(this.tabControl_is_mouse_on_close_image(e));
        }
        private void tabControl_set_close_image_state(bool b_active)
        {
            if (this.tabControl_mdichild.SelectedTab==null)
                return;
            // if active state
            if (b_active)
            {
                if (this.tabControl_mdichild.SelectedTab.ImageIndex!=1)
                    this.tabControl_mdichild.SelectedTab.ImageIndex=1;
            }
            // else
            else if (this.tabControl_mdichild.SelectedTab.ImageIndex!=0)
                this.tabControl_mdichild.SelectedTab.ImageIndex=0;
        }
        private void tabControl_mdichild_MouseLeave(object sender, System.EventArgs e)
        {
            // restore unselected image
            this.tabControl_set_close_image_state(false);        
        }
        private bool tabControl_is_mouse_on_close_image(System.Windows.Forms.MouseEventArgs e)
        {
            // position of image in tab control
            byte image_left=6;
            byte image_top=1;
            // check if mouse is other the image
            if (
                (e.X<image_left+this.imageList_tabpage_icons.ImageSize.Width+this.tabControl_mdichild.GetTabRect(this.tabControl_mdichild.SelectedIndex).X)
                &&(e.X>image_left+this.tabControl_mdichild.GetTabRect(this.tabControl_mdichild.SelectedIndex).X)
                &&(e.Y<image_top+this.imageList_tabpage_icons.ImageSize.Height+this.tabControl_mdichild.GetTabRect(this.tabControl_mdichild.SelectedIndex).Y)
                &&(e.Y>image_top+this.tabControl_mdichild.GetTabRect(this.tabControl_mdichild.SelectedIndex).Y)
                )
                return true;
            else
                return false;
        }
        #endregion

        #region tcp/udp
        private int get_port(string port)
        {
            try
            {
                int p;
                p=System.Convert.ToInt32(port);
                if (p>=0 && p<=65535) return p;
                return -1;
            }
            catch
            {
                return -1;
            }
        }
        private void button_TCP_Server_Click(object sender, System.EventArgs e)
        {
            int port;
            port=get_port(this.textBox_TCP_Server_Port.Text);
            if (port==-1)
            {
                MessageBox.Show(this,"Invalid port number");
                return;
            }
            
            FormTCPServer frm_srv=new FormTCPServer();
            frm_srv.set_mdi_parent(this);
            frm_srv.Show();
            frm_srv.new_tcp_server(this.textBox_TCP_Server_IP.Text.Trim(),port);
        }

        private void button_TCP_Client_Click(object sender, System.EventArgs e)
        {
            int port;
            int local_port;
            port=get_port(this.textBox_TCP_Client_Port.Text);
            local_port=get_port(this.textBox_TCP_Client_local_port.Text);
            if (port==-1 || local_port==-1)
            {
                MessageBox.Show(this,"Invalid port number");
                return;
            }
            FormTCPClient frm_clt=new FormTCPClient();
            frm_clt.set_mdi_parent(this);
            frm_clt.Show();
            frm_clt.new_tcp_client(this.textBox_TCP_Client_IP.Text.Trim(),port,this.checkBox_TCP_Client_Specify_local_port.Checked,local_port);
        }

        private void button_UDP_Server_Click(object sender, System.EventArgs e)
        {
            int port;
            port=get_port(this.textBox_UDP_Server_Port.Text);
            if (port==-1)
            {
                MessageBox.Show(this,"Invalid port number");
                return;
            }
            FormUDPServer frm_srv=new FormUDPServer();
            frm_srv.set_mdi_parent(this);
            frm_srv.Show();
            frm_srv.new_udp_server(this.textBox_UDP_Server_IP.Text.Trim(),port,this.checkBox_UDP_Server_echo.Checked);        
        }

        private void button_UDP_Client_Click(object sender, System.EventArgs e)
        {
            int port;
            int local_port;
            port=get_port(this.textBox_UDP_Client_Port.Text.Trim());
            local_port=get_port(this.textBox_UDP_Client_local_port.Text);
            if (port==-1 || local_port==-1)
            {
                MessageBox.Show(this,"Invalid port number");
                return;
            }
            if (this.checkBox_UDP_Client_watch_for_reply.Checked)
                //make udp server
            {
                FormUDPServer frm_srv=new FormUDPServer();
                frm_srv.set_mdi_parent(this);
                frm_srv.Show();
                if (this.checkBox_UDP_Client_Specify_local_port.Checked)
                    frm_srv.new_udp_server(this.textBox_UDP_Client_IP.Text.Trim(),port,local_port);
                else
                    frm_srv.new_udp_server(this.textBox_UDP_Client_IP.Text.Trim(),port,0);
            }
            else
                //make udp clt only
            {
                FormUDPClient frm_clt=new FormUDPClient();
                frm_clt.set_mdi_parent(this);
                frm_clt.Show();
                frm_clt.new_udp_client(this.textBox_UDP_Client_IP.Text.Trim(),port,this.checkBox_UDP_Client_Specify_local_port.Checked,local_port);
            }

        }
        #endregion
        #region arp

        private void button_arp_Click(object sender, System.EventArgs e)
        {
            this.textBox_arp_result.Text="";
            string str_ip=this.textBox_arp_ip.Text.Trim();
            this.textBox_arp_result.Text=iphelper.iphelper.SendArp(str_ip);
        }
        private void textBox_arp_ip_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar==13) button_arp_Click(sender,null);
        }
        #endregion
        #region dns
        private void button_dns_Click(object sender, System.EventArgs e)
        {
            this.button_dns.Enabled=false;
            int cpt;
            try
            {
                System.Net.IPHostEntry iphe=System.Net.Dns.Resolve(this.textBox_dns_ip.Text.Trim());
                this.textBox_dns_result.Text="Name:\t"+iphe.HostName+"\r\n";
                this.textBox_dns_result.Text+="Address:\r\n";
                for (cpt=0;cpt<iphe.AddressList.Length;cpt++)
                    this.textBox_dns_result.Text+="\t"+iphe.AddressList[cpt]+"\r\n";
                if (iphe.Aliases.Length>0)
                {
                    this.textBox_dns_result.Text+="Aliases:\r\n";
                    for (cpt=0;cpt<iphe.Aliases.Length;cpt++)
                        this.textBox_dns_result.Text+="\t"+iphe.Aliases[cpt]+"\r\n";
                }
                this.textBox_dns_result.SelectionStart=this.textBox_dns_result.Text.Length;
            }
            catch (Exception ex)
            {
                this.textBox_dns_result.Text=ex.Message;
                this.textBox_dns_result.SelectionStart=this.textBox_dns_result.Text.Length;
            }
            this.button_dns.Enabled=true;
        }

        private void textBox_dns_ip_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar==13) button_dns_Click(sender,null);
        }
        #endregion
        #region icmp
        private void checkBox_icmp_looping_ping_CheckedChanged(object sender, System.EventArgs e)
        {
            this.textBox_icmp_ping_number.Enabled=!this.checkBox_icmp_looping_ping.Checked;
        }
        
        private void button_ping_Click(object sender, System.EventArgs e)
        {
            FormPing fp=new FormPing(this.textBox_icmp_ip.Text.Trim(),this.textBox_icmp_delay_dor_reply.Text,
                this.textBox_icmp_packet_ttl.Text,this.textBox_icmp_ping_number.Text,
                this.checkBox_icmp_looping_ping.Checked,this.checkBox_icmp_may_broadcast.Checked);
            fp.set_mdi_parent(this);
            fp.Show();
            fp.send_ping();// avoid another user button push
        }

        private void button_trace_Click(object sender, System.EventArgs e)
        {
            FormTrace ft=new FormTrace(this.textBox_icmp_ip.Text.Trim(),this.textBox_icmp_delay_dor_reply.Text,
                this.textBox_icmp_start_with_hop.Text,this.textBox_icmp_end_with_hop.Text,
                this.checkBox_icmp_resolve_adresses.Checked);
            ft.set_mdi_parent(this);
            ft.Show();
            ft.trace();
        }
        #endregion
        #region whois
        private void button_whois_Click(object sender, System.EventArgs e)
        {
            FormWhois fw=new FormWhois(this.textBox_whois_ip.Text.Trim(),this.radioButton_whois_auto_find.Checked,this.textBox_whois_server.Text);
            fw.set_mdi_parent(this);
            fw.Show();
        }

        private void textBox_whois_ip_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar==13) button_whois_Click(sender,null);        
        }
        
        private void radioButton_whois_auto_find_CheckedChanged(object sender, System.EventArgs e)
        {
            this.textBox_whois_server.Enabled=this.radioButton_whois_use_following.Checked;
        }
        #endregion
        #region outside ip
        string outside_ip_http_data;
        private void button_outside_ip_get_Click(object sender, System.EventArgs e)
        {
            this.outside_ip_http_data="";
            this.textBox_outside_ip_ip.Text="";
            this.button_outside_ip_get.Enabled=false;
            
            easy_socket.tcp.Socket_Data outside_ip_socket=new easy_socket.tcp.Socket_Data();
            outside_ip_socket.event_Socket_Data_DataArrival+=new easy_socket.tcp.Socket_Data_DataArrival_EventHandler(outside_ip_data_arrival);
            outside_ip_socket.event_Socket_Data_Connected_To_Remote_Host+=new easy_socket.tcp.Socket_Data_Connected_To_Remote_Host_EventHandler(outside_ip_connected);
            outside_ip_socket.event_Socket_Data_Error+=new easy_socket.tcp.Socket_Data_Error_EventHandler(outside_ip_error);
            outside_ip_socket.event_Socket_Data_Closed_by_Remote_Side+=new easy_socket.tcp.Socket_Data_Closed_by_Remote_Side_EventHandler(outside_ip_closed_by_remote_side);
            outside_ip_socket.connect(this.textBox_outside_ip_server.Text.Trim(),80);
        }
        private void outside_ip_data_arrival(easy_socket.tcp.Socket_Data sender,easy_socket.tcp.EventArgs_ReceiveDataSocket eargs)
        {
            this.outside_ip_http_data+=System.Text.Encoding.Default.GetString(eargs.buffer , 0, eargs.buffer_size );
            // data are treat only after the arrival of all data (remote host deconnection)
        }
        private void outside_ip_closed_by_remote_side(easy_socket.tcp.Socket_Data sender,EventArgs e)
        {
            this.button_outside_ip_get.Enabled=true;
            /* sample with checkip.dyndns.org
            HTTP/1.0 200 OK
            Server: Cherokee/0.4.6
            Pragma: no-cache
            Cache-Control: no-cache
            Content-Type: text/html
            Connection: close


            <html><head><title>Current IP Check</title></head><body>Current IP Address: xx.yy.zzz.uuu</body></html>
            */

            string data=this.outside_ip_http_data;
            if ((data.IndexOf("200")==-1)|| (data.IndexOf("200")>data.IndexOf("\n")))
            {
                if (data=="")
                    MessageBox.Show(this,"Http error, no data received","Error",MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                else
                    MessageBox.Show(this,"Http error, reicieved data are:\r\n"+data,"Error",MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return;
            }
            // getting data part of http response
            int pos=data.IndexOf("\r\n\r\n");
            if (pos<0)
            {
                MessageBox.Show(this,"Http error, can't find header error","Error",MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            data=data.Substring(pos+4);
            // getting body
            pos=data.IndexOf("<body>");
            if (pos<0)
            {
                // supposed to be only ip in data
                this.textBox_outside_ip_ip.Text=data;
                this.textBox_outside_ip_ip.SelectionStart=this.textBox_outside_ip_ip.Text.Length;
                return;
            }
            data=data.Substring(pos+"<body>".Length);
            pos=data.IndexOf("</body>");
            if (pos<0)
            {
                // supposed to be only ip in data
                this.textBox_outside_ip_ip.Text=data;
                this.textBox_outside_ip_ip.SelectionStart=this.textBox_outside_ip_ip.Text.Length;
                return;
            }
            data=data.Substring(0,pos);
            // getting result here behind ": "
            pos=data.IndexOf(": ");
            if (pos<0)
            {
                // supposed to be only ip in body
                this.textBox_outside_ip_ip.Text=data;
                this.textBox_outside_ip_ip.SelectionStart=this.textBox_outside_ip_ip.Text.Length;
                return;
            }    
            // ok 
            data=data.Substring(pos+2);
            this.textBox_outside_ip_ip.Text=data;
            this.textBox_outside_ip_ip.SelectionStart=this.textBox_outside_ip_ip.Text.Length;
            return;
        }
        private void outside_ip_connected(easy_socket.tcp.Socket_Data sender,EventArgs eargs)
        {
            sender.send("GET HTTP/1.0\r\n\r\n");
        }
        private void outside_ip_error(easy_socket.tcp.Socket_Data sender,easy_socket.tcp.EventArgs_Exception eargs)
        {
            MessageBox.Show(this,"A socket error as occured:\r\n"+eargs.exception.Message,"Error",MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            this.button_outside_ip_get.Enabled=true;
        }

        private void textBox_outside_ip_server_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar==13) button_outside_ip_get_Click(sender,null);        
        }
        #endregion
        #region local ip
        private void button_local_ip_refresh_Click(object sender, System.EventArgs e)
        {
            int cpt;
            System.Net.IPHostEntry iphe=System.Net.Dns.Resolve(System.Net.Dns.GetHostName());
            this.textBox_local_ip.Text="Name:\t"+iphe.HostName+"\r\n";
            this.textBox_local_ip.Text+="Address:\r\n";
            for (cpt=0;cpt<iphe.AddressList.Length;cpt++)
                this.textBox_local_ip.Text+="\t"+iphe.AddressList[cpt]+"\r\n";
            if (iphe.Aliases.Length>0)
            {
                this.textBox_local_ip.Text+="Aliases:\r\n";
                for (cpt=0;cpt<iphe.Aliases.Length;cpt++)
                    this.textBox_local_ip.Text+="\t"+iphe.Aliases[cpt]+"\r\n";
            }
            this.textBox_local_ip.SelectionStart=this.textBox_local_ip.Text.Length;
        }
        #endregion
        #region stats
        private void button_tcp_table_Click(object sender, System.EventArgs e)
        {
            FormStat fstat=new FormStat();
            fstat.MdiParent=this;
            fstat.Show();
            fstat.get_tcp_table();
        }

        private void button_tcp_stat_Click(object sender, System.EventArgs e)
        {
            FormStat fstat=new FormStat();
            fstat.MdiParent=this;
            fstat.Show();
            fstat.get_tcp_stats();
        }

        private void button_udp_table_Click(object sender, System.EventArgs e)
        {
            FormStat fstat=new FormStat();
            fstat.MdiParent=this;
            fstat.Show();
            fstat.get_udp_table();
        }

        private void button_udp_stats_Click(object sender, System.EventArgs e)
        {
            FormStat fstat=new FormStat();
            fstat.MdiParent=this;
            fstat.Show();
            fstat.get_udp_stats();        
        }

        private void button_ipnet_table_Click(object sender, System.EventArgs e)
        {
            FormStat fstat=new FormStat();
            fstat.MdiParent=this;
            fstat.Show();
            fstat.get_ipnet_table();        
        }

        private void button_ipnet_stats_Click(object sender, System.EventArgs e)
        {
            FormStat fstat=new FormStat();
            fstat.MdiParent=this;
            fstat.Show();
            fstat.get_tcp_stats();        
        }

        private void button_icmp_stats_Click(object sender, System.EventArgs e)
        {
            FormStat fstat=new FormStat();
            fstat.MdiParent=this;
            fstat.Show();
            fstat.get_icmp_stats();        
        }
        #endregion
        #region wake on lan
        private void button_WOL_wake_up_Click(object sender, System.EventArgs e)
        {
            if (!CCheck_user_interface_inputs.check_int(this.textBox_WOL_udp_port.Text))
                return;
            new easy_socket.Cwake_on_lan().wake_on_lan(this.textBox_WOL_mac_address.Text,
                this.textBox_WOL_broadcast_ip.Text,System.Convert.ToInt32(this.textBox_WOL_udp_port.Text,10));
        }
        #endregion
        #region remote shutdown
        private void button_remote_shutdown_initiate_Click(object sender, System.EventArgs e)
        {
            easy_socket.RemoteShutDown.InitiateShutdown(
                this.textBox_remote_shutdown_computer_name.Text,
                this.textBox_remote_shutdown_message.Text,
                (UInt32)this.numericUpDown_remote_shutdown_timeout.Value,
                this.checkBox_remote_shutdown_force_close.Checked,
                this.checkBox_remote_shutdown_reset.Checked
                );
        }

        private void button_remote_shutdown_abort_Click(object sender, System.EventArgs e)
        {
            easy_socket.RemoteShutDown.AbortShutdown(
                this.textBox_remote_shutdown_computer_name.Text);        
        }
        #endregion
        #region rawpackets
        private void button_raw_packet_capture_Click(object sender, System.EventArgs e)
        {
            Form_packet_capture f=new Form_packet_capture();
            f.MdiParent=this;
            f.Show();
        }

        private void button_raw_packet_forge_Click(object sender, System.EventArgs e)
        {
            // check if user has accept agreement
            Microsoft.Win32.RegistryKey rk=System.Windows.Forms.Application.UserAppDataRegistry;
            int i=(int)rk.GetValue("agreement_forge", 0);
            bool b_agreement = (i==1);
            // if he hasn't
            if (!b_agreement)
            {
                // ask for agreement
                Form_Agreement fa=new Form_Agreement();
                b_agreement=fa.get_agreement();
                // if he declines
                if (!b_agreement)
                    return;// don't allow user to use this part of software
                // else
                rk.SetValue("agreement_forge", 1);
            }

            Form_packet_forge f=new Form_packet_forge();
            f.MdiParent=this;
            f.Show();
        }
        #endregion
        #region scan
        private void show_form_scan(FormScan.FormScan.SCAN_TYPE type)
        {
            // check if user has accept agreement
            Microsoft.Win32.RegistryKey rk=System.Windows.Forms.Application.UserAppDataRegistry;
            int i=(int)rk.GetValue("agreement_scan", 0);
            bool b_agreement = (i==1);
            // if he hasn't
            if (!b_agreement)
            {
                // ask for agreement
                Form_Agreement fa=new Form_Agreement();
                b_agreement=fa.get_agreement();
                // if he declines
                if (!b_agreement)
                    return;// don't allow user to use this part of software
                // else
                rk.SetValue("agreement_scan", 1);
            }
            FormScan.FormScan f=new FormScan.FormScan();
            f.set_scan_type(type);
            f.MdiParent=this;
            f.Show();
        }
        private void button_scan_tcp_Click(object sender, System.EventArgs e)
        {
            this.show_form_scan(FormScan.FormScan.SCAN_TYPE.TCP);
        }

        private void button_scan_udp_Click(object sender, System.EventArgs e)
        {
            this.show_form_scan(FormScan.FormScan.SCAN_TYPE.UDP);
        }

        private void button_scan_icmp_Click(object sender, System.EventArgs e)
        {
            this.show_form_scan(FormScan.FormScan.SCAN_TYPE.ICMP);
        }

        private void button_scan_cgi_Click(object sender, System.EventArgs e)
        {
            this.show_form_scan(FormScan.FormScan.SCAN_TYPE.CGI);
        }
        #endregion

    }
}
