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

// soory for this file is quite badely code :(
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Network_Stuff.FormScan
{

    public class FormScan : System.Windows.Forms.Form
    {
        public const string SEPARATOR_ID_TIME=" : ";

        public uint h_semaphore;
        private ListViewItemComparer lvic;
        private OPEN_FILE_TYPE last_openfile_type;
        private bool b_do_icmp_scan_before;
        private System.Collections.ArrayList al_full_scan_infos;
        private System.Collections.ArrayList al_pinged_servers;
        private System.Collections.ArrayList al_servers_being_ping;
        private System.Collections.ArrayList al_proxy_being_checked;
        private System.Collections.ArrayList al_proxy;
        private easy_socket.icmp.icmp_server icmp_srv;
        private System.Collections.ArrayList al_icmp_identifiers;
        private SCAN_TYPE current_scan_type;
        public int i_time_out;
        public int nb_digits;
        public byte[] b_data;
        private System.Threading.ManualResetEvent evtPause;
        public System.Threading.ManualResetEvent evtStop;
        private System.Threading.AutoResetEvent evtScanFinished;
        private System.Threading.AutoResetEvent evtThreadStart;
        private System.Threading.AutoResetEvent evtListviewItemUnLocked;
        private System.Threading.AutoResetEvent evtal_icmp_identifiersUnLocked;
        private System.Threading.AutoResetEvent evtal_pinged_serversUnLocked;
        private System.Threading.AutoResetEvent evtal_servers_being_pingUnLocked;
        private System.Threading.AutoResetEvent evtal_proxy_UnLocked;
        private System.Threading.AutoResetEvent evtal_proxy_being_checkUnLocked;
        
        private bool b_pause;
        private bool b_scan_running;
        private int nb_threads;
        private int scan_position;
        private int al_ini_size;
        private System.Random rnd;
        private string str_saving_file;
        private bool b_no_more_proxy_signaled;
        

        private System.Windows.Forms.GroupBox groupBox_scan_type;
        private System.Windows.Forms.RadioButton radioButton_tcp_scan;
        private System.Windows.Forms.RadioButton radioButton_udp_scan;
        private System.Windows.Forms.RadioButton radioButton_icmp_scan;
        private System.Windows.Forms.GroupBox groupBox_ip;
        private System.Windows.Forms.RadioButton radioButton_following_ip;
        private System.Windows.Forms.RadioButton radioButton_ip_file;
        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.GroupBox groupBox_port;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.RadioButton radioButton_port_file;
        private System.Windows.Forms.RadioButton radioButton_following_port;
        private System.Windows.Forms.Button button_browse_ip_file;
        private System.Windows.Forms.Button button_browse_port_file;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button_pause;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.ListView listView_results;
        private System.Windows.Forms.ColumnHeader columnHeader_ip;
        private System.Windows.Forms.ColumnHeader columnHeader_port;
        private System.Windows.Forms.ColumnHeader columnHeader_Result;
        private System.Windows.Forms.Button button_browse_cgi_file;
        private System.Windows.Forms.TextBox textBox_cgi_file;
        private System.Windows.Forms.GroupBox groupBox_cgi;
        private System.Windows.Forms.RadioButton radioButton_cgi_scan;
        private System.Windows.Forms.GroupBox groupBox_options;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown_nb_threads;
        private System.Windows.Forms.CheckBox checkBox_random_order;
        private System.Windows.Forms.CheckBox checkBox_tcp_wait_for_data;
        private System.Windows.Forms.Label label_timeout;
        private System.Windows.Forms.NumericUpDown numericUpDown_timeout;
        private System.Windows.Forms.ColumnHeader columnHeader_id_date;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ContextMenu contextMenu_list_view;
        private System.Windows.Forms.MenuItem menuItem_save_all;
        private System.Windows.Forms.MenuItem menuItem_save_selected;
        private System.Windows.Forms.CheckBox checkBox_icmp_scan_before;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_same_ip_port_list;
        private System.Windows.Forms.TextBox textBox_same_ip_port_file;
        private System.Windows.Forms.Button button_browse_same_ipport_list_file;
        private System.Windows.Forms.RadioButton radioButton_splited_ip_port_list;
        private System.Windows.Forms.MenuItem menuItem_copy_selected_ip_port;
        private System.Windows.Forms.MenuItem menuItem_copy_selected_ip;
        private System.Windows.Forms.Panel panel_proxy;
        private System.Windows.Forms.ComboBox comboBox_proxy_type;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_browse_proxy_file;
        private System.Windows.Forms.TextBox textBox_proxy_list_file;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_ip_to_check_proxy;
        private System.Windows.Forms.CheckBox checkBox_check_proxy;
        private System.Windows.Forms.CheckBox checkBox_use_proxy;
        private System.Windows.Forms.Panel panel_data;
        private System.Windows.Forms.TextBox textBox_data;
        private System.Windows.Forms.CheckBox checkBox_hexa_data;
        private System.Windows.Forms.Label label_data;
        private System.Windows.Forms.Label label2;

        private System.ComponentModel.Container components = null;

        public FormScan()
        {
            InitializeComponent();
            XPStyle.MakeXPStyle(this);
            this.lvic=new ListViewItemComparer();
            this.al_full_scan_infos=new System.Collections.ArrayList(255);
            this.al_pinged_servers=new System.Collections.ArrayList(255);
            this.al_servers_being_ping=new System.Collections.ArrayList(255);
            this.al_proxy_being_checked=new System.Collections.ArrayList(255);
            this.al_proxy=new System.Collections.ArrayList(255);
            
            this.al_icmp_identifiers=new System.Collections.ArrayList();
            this.i_time_out=3000;
            this.b_data=null;
            this.icmp_srv=new easy_socket.icmp.icmp_server();
            // add icmp server events
            this.add_events(this.icmp_srv);

            this.b_do_icmp_scan_before=false;
            this.b_no_more_proxy_signaled=false;
            this.b_scan_running=false;
            this.b_pause=false;
            this.evtPause=new System.Threading.ManualResetEvent(true);
            this.evtStop=new System.Threading.ManualResetEvent(false);
            this.evtScanFinished=new System.Threading.AutoResetEvent(false);
            this.evtThreadStart=new System.Threading.AutoResetEvent(false);
            this.evtListviewItemUnLocked=new System.Threading.AutoResetEvent(true);
            this.evtal_icmp_identifiersUnLocked=new System.Threading.AutoResetEvent(true);
            this.evtal_pinged_serversUnLocked=new System.Threading.AutoResetEvent(true);
            this.evtal_servers_being_pingUnLocked=new System.Threading.AutoResetEvent(true);
            this.evtal_proxy_UnLocked=new System.Threading.AutoResetEvent(true);
            this.evtal_proxy_being_checkUnLocked=new System.Threading.AutoResetEvent(true);


            if (this.comboBox_proxy_type.Items.Count>0)
                this.comboBox_proxy_type.SelectedIndex=0;

            this.scan_position=0;
            this.al_ini_size=0;
            this.nb_digits=2;
            this.rnd=new System.Random();
        }
        protected override void Dispose( bool disposing )
        {
            this.stop();
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
        private void InitializeComponent()
        {
            this.groupBox_scan_type = new System.Windows.Forms.GroupBox();
            this.checkBox_icmp_scan_before = new System.Windows.Forms.CheckBox();
            this.radioButton_cgi_scan = new System.Windows.Forms.RadioButton();
            this.radioButton_icmp_scan = new System.Windows.Forms.RadioButton();
            this.radioButton_udp_scan = new System.Windows.Forms.RadioButton();
            this.radioButton_tcp_scan = new System.Windows.Forms.RadioButton();
            this.groupBox_ip = new System.Windows.Forms.GroupBox();
            this.button_browse_ip_file = new System.Windows.Forms.Button();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.radioButton_ip_file = new System.Windows.Forms.RadioButton();
            this.radioButton_following_ip = new System.Windows.Forms.RadioButton();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox_port = new System.Windows.Forms.GroupBox();
            this.button_browse_port_file = new System.Windows.Forms.Button();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.radioButton_port_file = new System.Windows.Forms.RadioButton();
            this.radioButton_following_port = new System.Windows.Forms.RadioButton();
            this.button_start = new System.Windows.Forms.Button();
            this.button_pause = new System.Windows.Forms.Button();
            this.button_stop = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.listView_results = new System.Windows.Forms.ListView();
            this.columnHeader_id_date = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_ip = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_port = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_Result = new System.Windows.Forms.ColumnHeader();
            this.groupBox_cgi = new System.Windows.Forms.GroupBox();
            this.button_browse_cgi_file = new System.Windows.Forms.Button();
            this.textBox_cgi_file = new System.Windows.Forms.TextBox();
            this.groupBox_options = new System.Windows.Forms.GroupBox();
            this.panel_data = new System.Windows.Forms.Panel();
            this.textBox_data = new System.Windows.Forms.TextBox();
            this.checkBox_hexa_data = new System.Windows.Forms.CheckBox();
            this.label_data = new System.Windows.Forms.Label();
            this.panel_proxy = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_proxy_type = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button_browse_proxy_file = new System.Windows.Forms.Button();
            this.textBox_proxy_list_file = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_ip_to_check_proxy = new System.Windows.Forms.TextBox();
            this.checkBox_check_proxy = new System.Windows.Forms.CheckBox();
            this.checkBox_use_proxy = new System.Windows.Forms.CheckBox();
            this.label_timeout = new System.Windows.Forms.Label();
            this.numericUpDown_timeout = new System.Windows.Forms.NumericUpDown();
            this.checkBox_tcp_wait_for_data = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_nb_threads = new System.Windows.Forms.NumericUpDown();
            this.checkBox_random_order = new System.Windows.Forms.CheckBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.contextMenu_list_view = new System.Windows.Forms.ContextMenu();
            this.menuItem_copy_selected_ip = new System.Windows.Forms.MenuItem();
            this.menuItem_copy_selected_ip_port = new System.Windows.Forms.MenuItem();
            this.menuItem_save_selected = new System.Windows.Forms.MenuItem();
            this.menuItem_save_all = new System.Windows.Forms.MenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_splited_ip_port_list = new System.Windows.Forms.RadioButton();
            this.button_browse_same_ipport_list_file = new System.Windows.Forms.Button();
            this.textBox_same_ip_port_file = new System.Windows.Forms.TextBox();
            this.radioButton_same_ip_port_list = new System.Windows.Forms.RadioButton();
            this.groupBox_scan_type.SuspendLayout();
            this.groupBox_ip.SuspendLayout();
            this.groupBox_port.SuspendLayout();
            this.groupBox_cgi.SuspendLayout();
            this.groupBox_options.SuspendLayout();
            this.panel_data.SuspendLayout();
            this.panel_proxy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_timeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_nb_threads)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_scan_type
            // 
            this.groupBox_scan_type.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                             this.checkBox_icmp_scan_before,
                                                                                             this.radioButton_cgi_scan,
                                                                                             this.radioButton_icmp_scan,
                                                                                             this.radioButton_udp_scan,
                                                                                             this.radioButton_tcp_scan});
            this.groupBox_scan_type.Location = new System.Drawing.Point(8, 0);
            this.groupBox_scan_type.Name = "groupBox_scan_type";
            this.groupBox_scan_type.Size = new System.Drawing.Size(352, 48);
            this.groupBox_scan_type.TabIndex = 0;
            this.groupBox_scan_type.TabStop = false;
            this.groupBox_scan_type.Text = "Scan";
            // 
            // checkBox_icmp_scan_before
            // 
            this.checkBox_icmp_scan_before.Location = new System.Drawing.Point(232, 8);
            this.checkBox_icmp_scan_before.Name = "checkBox_icmp_scan_before";
            this.checkBox_icmp_scan_before.Size = new System.Drawing.Size(112, 32);
            this.checkBox_icmp_scan_before.TabIndex = 4;
            this.checkBox_icmp_scan_before.Text = "scan only pingable servers";
            // 
            // radioButton_cgi_scan
            // 
            this.radioButton_cgi_scan.Location = new System.Drawing.Point(184, 16);
            this.radioButton_cgi_scan.Name = "radioButton_cgi_scan";
            this.radioButton_cgi_scan.Size = new System.Drawing.Size(64, 16);
            this.radioButton_cgi_scan.TabIndex = 3;
            this.radioButton_cgi_scan.Text = "Cgi";
            this.radioButton_cgi_scan.CheckedChanged += new System.EventHandler(this.radioButton_cgi_scan_CheckedChanged);
            // 
            // radioButton_icmp_scan
            // 
            this.radioButton_icmp_scan.Location = new System.Drawing.Point(128, 16);
            this.radioButton_icmp_scan.Name = "radioButton_icmp_scan";
            this.radioButton_icmp_scan.Size = new System.Drawing.Size(56, 16);
            this.radioButton_icmp_scan.TabIndex = 2;
            this.radioButton_icmp_scan.Text = "Icmp";
            this.radioButton_icmp_scan.CheckedChanged += new System.EventHandler(this.radioButton_icmp_scan_CheckedChanged);
            // 
            // radioButton_udp_scan
            // 
            this.radioButton_udp_scan.Location = new System.Drawing.Point(64, 16);
            this.radioButton_udp_scan.Name = "radioButton_udp_scan";
            this.radioButton_udp_scan.Size = new System.Drawing.Size(64, 18);
            this.radioButton_udp_scan.TabIndex = 1;
            this.radioButton_udp_scan.Text = "Udp";
            // 
            // radioButton_tcp_scan
            // 
            this.radioButton_tcp_scan.Checked = true;
            this.radioButton_tcp_scan.Location = new System.Drawing.Point(8, 16);
            this.radioButton_tcp_scan.Name = "radioButton_tcp_scan";
            this.radioButton_tcp_scan.Size = new System.Drawing.Size(56, 16);
            this.radioButton_tcp_scan.TabIndex = 0;
            this.radioButton_tcp_scan.TabStop = true;
            this.radioButton_tcp_scan.Text = "Tcp";
            this.radioButton_tcp_scan.CheckedChanged += new System.EventHandler(this.radioButton_tcp_scan_CheckedChanged);
            // 
            // groupBox_ip
            // 
            this.groupBox_ip.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                      this.button_browse_ip_file,
                                                                                      this.textBox_ip,
                                                                                      this.radioButton_ip_file,
                                                                                      this.radioButton_following_ip});
            this.groupBox_ip.Location = new System.Drawing.Point(24, 64);
            this.groupBox_ip.Name = "groupBox_ip";
            this.groupBox_ip.Size = new System.Drawing.Size(320, 72);
            this.groupBox_ip.TabIndex = 1;
            this.groupBox_ip.TabStop = false;
            this.groupBox_ip.Text = "Ip List";
            // 
            // button_browse_ip_file
            // 
            this.button_browse_ip_file.Location = new System.Drawing.Point(288, 40);
            this.button_browse_ip_file.Name = "button_browse_ip_file";
            this.button_browse_ip_file.Size = new System.Drawing.Size(24, 23);
            this.button_browse_ip_file.TabIndex = 3;
            this.button_browse_ip_file.Text = "...";
            this.button_browse_ip_file.Click += new System.EventHandler(this.button_browse_ip_file_Click);
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(8, 40);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(272, 20);
            this.textBox_ip.TabIndex = 2;
            this.textBox_ip.Text = "127.0.0.1";
            // 
            // radioButton_ip_file
            // 
            this.radioButton_ip_file.Location = new System.Drawing.Point(56, 24);
            this.radioButton_ip_file.Name = "radioButton_ip_file";
            this.radioButton_ip_file.Size = new System.Drawing.Size(200, 16);
            this.radioButton_ip_file.TabIndex = 1;
            this.radioButton_ip_file.Text = "Ip in file (ip split by \\r\\n)";
            // 
            // radioButton_following_ip
            // 
            this.radioButton_following_ip.Checked = true;
            this.radioButton_following_ip.Location = new System.Drawing.Point(56, 8);
            this.radioButton_following_ip.Name = "radioButton_following_ip";
            this.radioButton_following_ip.Size = new System.Drawing.Size(200, 16);
            this.radioButton_following_ip.TabIndex = 0;
            this.radioButton_following_ip.TabStop = true;
            this.radioButton_following_ip.Text = "Following ips";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // groupBox_port
            // 
            this.groupBox_port.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                        this.button_browse_port_file,
                                                                                        this.textBox_port,
                                                                                        this.radioButton_port_file,
                                                                                        this.radioButton_following_port});
            this.groupBox_port.Location = new System.Drawing.Point(24, 136);
            this.groupBox_port.Name = "groupBox_port";
            this.groupBox_port.Size = new System.Drawing.Size(320, 72);
            this.groupBox_port.TabIndex = 3;
            this.groupBox_port.TabStop = false;
            this.groupBox_port.Text = "Port List";
            // 
            // button_browse_port_file
            // 
            this.button_browse_port_file.Location = new System.Drawing.Point(288, 40);
            this.button_browse_port_file.Name = "button_browse_port_file";
            this.button_browse_port_file.Size = new System.Drawing.Size(24, 23);
            this.button_browse_port_file.TabIndex = 3;
            this.button_browse_port_file.Text = "...";
            this.button_browse_port_file.Click += new System.EventHandler(this.button_browse_port_file_Click);
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(8, 40);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(272, 20);
            this.textBox_port.TabIndex = 2;
            this.textBox_port.Text = "80";
            // 
            // radioButton_port_file
            // 
            this.radioButton_port_file.Location = new System.Drawing.Point(56, 24);
            this.radioButton_port_file.Name = "radioButton_port_file";
            this.radioButton_port_file.Size = new System.Drawing.Size(200, 16);
            this.radioButton_port_file.TabIndex = 1;
            this.radioButton_port_file.Text = "Port in file (ip split by \\r\\n)";
            // 
            // radioButton_following_port
            // 
            this.radioButton_following_port.Checked = true;
            this.radioButton_following_port.Location = new System.Drawing.Point(56, 8);
            this.radioButton_following_port.Name = "radioButton_following_port";
            this.radioButton_following_port.Size = new System.Drawing.Size(200, 16);
            this.radioButton_following_port.TabIndex = 0;
            this.radioButton_following_port.TabStop = true;
            this.radioButton_following_port.Text = "Following ports";
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(232, 312);
            this.button_start.Name = "button_start";
            this.button_start.TabIndex = 5;
            this.button_start.Text = "Start";
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // button_pause
            // 
            this.button_pause.Enabled = false;
            this.button_pause.Location = new System.Drawing.Point(320, 312);
            this.button_pause.Name = "button_pause";
            this.button_pause.TabIndex = 6;
            this.button_pause.Text = "Pause";
            this.button_pause.Click += new System.EventHandler(this.button_pause_Click);
            // 
            // button_stop
            // 
            this.button_stop.Enabled = false;
            this.button_stop.Location = new System.Drawing.Point(408, 312);
            this.button_stop.Name = "button_stop";
            this.button_stop.TabIndex = 7;
            this.button_stop.Text = "Stop";
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(8, 344);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(728, 16);
            this.progressBar.TabIndex = 9;
            // 
            // listView_results
            // 
            this.listView_results.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                               this.columnHeader_id_date,
                                                                                               this.columnHeader_ip,
                                                                                               this.columnHeader_port,
                                                                                               this.columnHeader_Result});
            this.listView_results.FullRowSelect = true;
            this.listView_results.GridLines = true;
            this.listView_results.Location = new System.Drawing.Point(8, 360);
            this.listView_results.Name = "listView_results";
            this.listView_results.Size = new System.Drawing.Size(728, 200);
            this.listView_results.TabIndex = 8;
            this.listView_results.View = System.Windows.Forms.View.Details;
            this.listView_results.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView_results_MouseUp);
            this.listView_results.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_results_ColumnClick);
            // 
            // columnHeader_id_date
            // 
            this.columnHeader_id_date.Text = "Id : Date";
            this.columnHeader_id_date.Width = 100;
            // 
            // columnHeader_ip
            // 
            this.columnHeader_ip.Text = "Ip";
            this.columnHeader_ip.Width = 120;
            // 
            // columnHeader_port
            // 
            this.columnHeader_port.Text = "Port";
            this.columnHeader_port.Width = 73;
            // 
            // columnHeader_Result
            // 
            this.columnHeader_Result.Text = "Result";
            this.columnHeader_Result.Width = 429;
            // 
            // groupBox_cgi
            // 
            this.groupBox_cgi.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                       this.button_browse_cgi_file,
                                                                                       this.textBox_cgi_file});
            this.groupBox_cgi.Location = new System.Drawing.Point(8, 256);
            this.groupBox_cgi.Name = "groupBox_cgi";
            this.groupBox_cgi.Size = new System.Drawing.Size(352, 48);
            this.groupBox_cgi.TabIndex = 2;
            this.groupBox_cgi.TabStop = false;
            this.groupBox_cgi.Text = "Cgi file list";
            // 
            // button_browse_cgi_file
            // 
            this.button_browse_cgi_file.Location = new System.Drawing.Point(312, 16);
            this.button_browse_cgi_file.Name = "button_browse_cgi_file";
            this.button_browse_cgi_file.Size = new System.Drawing.Size(24, 23);
            this.button_browse_cgi_file.TabIndex = 3;
            this.button_browse_cgi_file.Text = "...";
            this.button_browse_cgi_file.Click += new System.EventHandler(this.button_browse_cgi_file_Click);
            // 
            // textBox_cgi_file
            // 
            this.textBox_cgi_file.Location = new System.Drawing.Point(32, 16);
            this.textBox_cgi_file.Name = "textBox_cgi_file";
            this.textBox_cgi_file.Size = new System.Drawing.Size(272, 20);
            this.textBox_cgi_file.TabIndex = 2;
            this.textBox_cgi_file.Text = "Cgi.txt";
            // 
            // groupBox_options
            // 
            this.groupBox_options.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                           this.panel_data,
                                                                                           this.panel_proxy,
                                                                                           this.label_timeout,
                                                                                           this.numericUpDown_timeout,
                                                                                           this.checkBox_tcp_wait_for_data,
                                                                                           this.label1,
                                                                                           this.numericUpDown_nb_threads,
                                                                                           this.checkBox_random_order});
            this.groupBox_options.Location = new System.Drawing.Point(368, 0);
            this.groupBox_options.Name = "groupBox_options";
            this.groupBox_options.Size = new System.Drawing.Size(368, 304);
            this.groupBox_options.TabIndex = 4;
            this.groupBox_options.TabStop = false;
            this.groupBox_options.Text = "Options";
            // 
            // panel_data
            // 
            this.panel_data.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                     this.textBox_data,
                                                                                     this.checkBox_hexa_data,
                                                                                     this.label_data});
            this.panel_data.Location = new System.Drawing.Point(8, 96);
            this.panel_data.Name = "panel_data";
            this.panel_data.Size = new System.Drawing.Size(352, 56);
            this.panel_data.TabIndex = 31;
            // 
            // textBox_data
            // 
            this.textBox_data.AcceptsReturn = true;
            this.textBox_data.AcceptsTab = true;
            this.textBox_data.Location = new System.Drawing.Point(100, 0);
            this.textBox_data.Multiline = true;
            this.textBox_data.Name = "textBox_data";
            this.textBox_data.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_data.Size = new System.Drawing.Size(248, 56);
            this.textBox_data.TabIndex = 23;
            this.textBox_data.Text = "";
            // 
            // checkBox_hexa_data
            // 
            this.checkBox_hexa_data.Location = new System.Drawing.Point(20, 16);
            this.checkBox_hexa_data.Name = "checkBox_hexa_data";
            this.checkBox_hexa_data.Size = new System.Drawing.Size(80, 16);
            this.checkBox_hexa_data.TabIndex = 22;
            this.checkBox_hexa_data.Text = "hexa data";
            this.checkBox_hexa_data.CheckedChanged += new System.EventHandler(this.checkBox_hexa_data_CheckedChanged);
            // 
            // label_data
            // 
            this.label_data.Location = new System.Drawing.Point(4, 0);
            this.label_data.Name = "label_data";
            this.label_data.Size = new System.Drawing.Size(64, 16);
            this.label_data.TabIndex = 24;
            this.label_data.Text = "Data";
            // 
            // panel_proxy
            // 
            this.panel_proxy.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                      this.label2,
                                                                                      this.comboBox_proxy_type,
                                                                                      this.label4,
                                                                                      this.button_browse_proxy_file,
                                                                                      this.textBox_proxy_list_file,
                                                                                      this.label3,
                                                                                      this.textBox_ip_to_check_proxy,
                                                                                      this.checkBox_check_proxy,
                                                                                      this.checkBox_use_proxy});
            this.panel_proxy.Location = new System.Drawing.Point(8, 152);
            this.panel_proxy.Name = "panel_proxy";
            this.panel_proxy.Size = new System.Drawing.Size(352, 144);
            this.panel_proxy.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(48, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(304, 56);
            this.label2.TabIndex = 38;
            this.label2.Text = "Warning some fake http proxy reply with successfull code showing there one web pa" +
                "ge without caring of proxy request. This make proxy checking fault (it considere" +
                "d proxy as a good one since it\'s not the case)";
            // 
            // comboBox_proxy_type
            // 
            this.comboBox_proxy_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_proxy_type.Items.AddRange(new object[] {
                                                                     "HTTP GET",
                                                                     "HTTP CONNECT",
                                                                     "SOCKS 4",
                                                                     "SOCKS 5"});
            this.comboBox_proxy_type.Location = new System.Drawing.Point(188, 14);
            this.comboBox_proxy_type.Name = "comboBox_proxy_type";
            this.comboBox_proxy_type.Size = new System.Drawing.Size(121, 21);
            this.comboBox_proxy_type.TabIndex = 37;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(20, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 16);
            this.label4.TabIndex = 36;
            this.label4.Text = "Proxy type";
            // 
            // button_browse_proxy_file
            // 
            this.button_browse_proxy_file.Location = new System.Drawing.Point(324, 38);
            this.button_browse_proxy_file.Name = "button_browse_proxy_file";
            this.button_browse_proxy_file.Size = new System.Drawing.Size(24, 23);
            this.button_browse_proxy_file.TabIndex = 35;
            this.button_browse_proxy_file.Text = "...";
            this.button_browse_proxy_file.Click += new System.EventHandler(this.button_browse_proxy_file_Click);
            // 
            // textBox_proxy_list_file
            // 
            this.textBox_proxy_list_file.Location = new System.Drawing.Point(188, 38);
            this.textBox_proxy_list_file.Name = "textBox_proxy_list_file";
            this.textBox_proxy_list_file.Size = new System.Drawing.Size(120, 20);
            this.textBox_proxy_list_file.TabIndex = 34;
            this.textBox_proxy_list_file.Text = "proxy_list_file.txt";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(20, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 16);
            this.label3.TabIndex = 33;
            this.label3.Text = "Proxy file list (IP:port split by \\r\\n)";
            // 
            // textBox_ip_to_check_proxy
            // 
            this.textBox_ip_to_check_proxy.Location = new System.Drawing.Point(188, 62);
            this.textBox_ip_to_check_proxy.Name = "textBox_ip_to_check_proxy";
            this.textBox_ip_to_check_proxy.Size = new System.Drawing.Size(120, 20);
            this.textBox_ip_to_check_proxy.TabIndex = 32;
            this.textBox_ip_to_check_proxy.Text = "google.com";
            // 
            // checkBox_check_proxy
            // 
            this.checkBox_check_proxy.Location = new System.Drawing.Point(20, 62);
            this.checkBox_check_proxy.Name = "checkBox_check_proxy";
            this.checkBox_check_proxy.Size = new System.Drawing.Size(168, 16);
            this.checkBox_check_proxy.TabIndex = 31;
            this.checkBox_check_proxy.Text = "Check proxy on following ip";
            // 
            // checkBox_use_proxy
            // 
            this.checkBox_use_proxy.Location = new System.Drawing.Point(4, 6);
            this.checkBox_use_proxy.Name = "checkBox_use_proxy";
            this.checkBox_use_proxy.Size = new System.Drawing.Size(80, 16);
            this.checkBox_use_proxy.TabIndex = 30;
            this.checkBox_use_proxy.Text = "Use proxy";
            this.checkBox_use_proxy.CheckedChanged += new System.EventHandler(this.checkBox_use_proxy_CheckedChanged);
            // 
            // label_timeout
            // 
            this.label_timeout.Location = new System.Drawing.Point(8, 32);
            this.label_timeout.Name = "label_timeout";
            this.label_timeout.Size = new System.Drawing.Size(88, 16);
            this.label_timeout.TabIndex = 20;
            this.label_timeout.Text = "Timeout (in ms)";
            // 
            // numericUpDown_timeout
            // 
            this.numericUpDown_timeout.Location = new System.Drawing.Point(120, 32);
            this.numericUpDown_timeout.Maximum = new System.Decimal(new int[] {
                                                                                  500000,
                                                                                  0,
                                                                                  0,
                                                                                  0});
            this.numericUpDown_timeout.Name = "numericUpDown_timeout";
            this.numericUpDown_timeout.Size = new System.Drawing.Size(56, 20);
            this.numericUpDown_timeout.TabIndex = 3;
            this.numericUpDown_timeout.Value = new System.Decimal(new int[] {
                                                                                5000,
                                                                                0,
                                                                                0,
                                                                                0});
            // 
            // checkBox_tcp_wait_for_data
            // 
            this.checkBox_tcp_wait_for_data.Location = new System.Drawing.Point(8, 72);
            this.checkBox_tcp_wait_for_data.Name = "checkBox_tcp_wait_for_data";
            this.checkBox_tcp_wait_for_data.Size = new System.Drawing.Size(296, 16);
            this.checkBox_tcp_wait_for_data.TabIndex = 2;
            this.checkBox_tcp_wait_for_data.Text = "Wait for incoming data until timeout after tcp connect";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "Number of threads :";
            // 
            // numericUpDown_nb_threads
            // 
            this.numericUpDown_nb_threads.Location = new System.Drawing.Point(120, 16);
            this.numericUpDown_nb_threads.Maximum = new System.Decimal(new int[] {
                                                                                     500,
                                                                                     0,
                                                                                     0,
                                                                                     0});
            this.numericUpDown_nb_threads.Name = "numericUpDown_nb_threads";
            this.numericUpDown_nb_threads.Size = new System.Drawing.Size(56, 20);
            this.numericUpDown_nb_threads.TabIndex = 1;
            this.numericUpDown_nb_threads.Value = new System.Decimal(new int[] {
                                                                                   50,
                                                                                   0,
                                                                                   0,
                                                                                   0});
            // 
            // checkBox_random_order
            // 
            this.checkBox_random_order.Location = new System.Drawing.Point(8, 56);
            this.checkBox_random_order.Name = "checkBox_random_order";
            this.checkBox_random_order.Size = new System.Drawing.Size(104, 16);
            this.checkBox_random_order.TabIndex = 0;
            this.checkBox_random_order.Text = "Random order";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // contextMenu_list_view
            // 
            this.contextMenu_list_view.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                                  this.menuItem_copy_selected_ip,
                                                                                                  this.menuItem_copy_selected_ip_port,
                                                                                                  this.menuItem_save_selected,
                                                                                                  this.menuItem_save_all});
            // 
            // menuItem_copy_selected_ip
            // 
            this.menuItem_copy_selected_ip.Index = 0;
            this.menuItem_copy_selected_ip.Text = "Copy Selcetd IP";
            this.menuItem_copy_selected_ip.Click += new System.EventHandler(this.menuItem_copy_selected_ip_Click);
            // 
            // menuItem_copy_selected_ip_port
            // 
            this.menuItem_copy_selected_ip_port.Index = 1;
            this.menuItem_copy_selected_ip_port.Text = "Copy Selected IP:Port";
            this.menuItem_copy_selected_ip_port.Click += new System.EventHandler(this.menuItem_copy_selected_ip_port_Click);
            // 
            // menuItem_save_selected
            // 
            this.menuItem_save_selected.Index = 2;
            this.menuItem_save_selected.Text = "Save Selected";
            this.menuItem_save_selected.Click += new System.EventHandler(this.menuItem_save_selected_Click);
            // 
            // menuItem_save_all
            // 
            this.menuItem_save_all.Index = 3;
            this.menuItem_save_all.Text = "Save All";
            this.menuItem_save_all.Click += new System.EventHandler(this.menuItem_save_all_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.radioButton_splited_ip_port_list,
                                                                                    this.button_browse_same_ipport_list_file,
                                                                                    this.textBox_same_ip_port_file,
                                                                                    this.radioButton_same_ip_port_list,
                                                                                    this.groupBox_ip,
                                                                                    this.groupBox_port});
            this.groupBox1.Location = new System.Drawing.Point(8, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(352, 208);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // radioButton_splited_ip_port_list
            // 
            this.radioButton_splited_ip_port_list.Location = new System.Drawing.Point(8, 48);
            this.radioButton_splited_ip_port_list.Name = "radioButton_splited_ip_port_list";
            this.radioButton_splited_ip_port_list.Size = new System.Drawing.Size(176, 16);
            this.radioButton_splited_ip_port_list.TabIndex = 6;
            this.radioButton_splited_ip_port_list.Text = "Use separted  Ip and Port list";
            // 
            // button_browse_same_ipport_list_file
            // 
            this.button_browse_same_ipport_list_file.Location = new System.Drawing.Point(312, 24);
            this.button_browse_same_ipport_list_file.Name = "button_browse_same_ipport_list_file";
            this.button_browse_same_ipport_list_file.Size = new System.Drawing.Size(24, 23);
            this.button_browse_same_ipport_list_file.TabIndex = 5;
            this.button_browse_same_ipport_list_file.Text = "...";
            this.button_browse_same_ipport_list_file.Click += new System.EventHandler(this.button_browse_same_ipport_list_file_Click);
            // 
            // textBox_same_ip_port_file
            // 
            this.textBox_same_ip_port_file.Location = new System.Drawing.Point(32, 24);
            this.textBox_same_ip_port_file.Name = "textBox_same_ip_port_file";
            this.textBox_same_ip_port_file.Size = new System.Drawing.Size(272, 20);
            this.textBox_same_ip_port_file.TabIndex = 4;
            this.textBox_same_ip_port_file.Text = "IP:PORT_file.txt";
            // 
            // radioButton_same_ip_port_list
            // 
            this.radioButton_same_ip_port_list.Checked = true;
            this.radioButton_same_ip_port_list.Location = new System.Drawing.Point(8, 8);
            this.radioButton_same_ip_port_list.Name = "radioButton_same_ip_port_list";
            this.radioButton_same_ip_port_list.Size = new System.Drawing.Size(168, 16);
            this.radioButton_same_ip_port_list.TabIndex = 0;
            this.radioButton_same_ip_port_list.TabStop = true;
            this.radioButton_same_ip_port_list.Text = "Use Ip:Port list (split by \\r\\n)";
            // 
            // FormScan
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(744, 566);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.groupBox1,
                                                                          this.groupBox_options,
                                                                          this.groupBox_cgi,
                                                                          this.listView_results,
                                                                          this.progressBar,
                                                                          this.button_stop,
                                                                          this.button_pause,
                                                                          this.button_start,
                                                                          this.groupBox_scan_type});
            this.Name = "FormScan";
            this.Text = "Scan";
            this.Resize += new System.EventHandler(this.FormScan_Resize);
            this.groupBox_scan_type.ResumeLayout(false);
            this.groupBox_ip.ResumeLayout(false);
            this.groupBox_port.ResumeLayout(false);
            this.groupBox_cgi.ResumeLayout(false);
            this.groupBox_options.ResumeLayout(false);
            this.panel_data.ResumeLayout(false);
            this.panel_proxy.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_timeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_nb_threads)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private void FormScan_Resize(object sender, System.EventArgs e)
        {
            // resize and put the same space between left bottom and right of the listview
            if (this.ClientSize.Height>this.listView_results.Top+100)
                this.listView_results.Height=this.ClientSize.Height-this.listView_results.Top-this.listView_results.Left;
            if (this.ClientSize.Width>this.listView_results.Left+100)
                this.listView_results.Width=this.ClientSize.Width-2*this.listView_results.Left;
        }
        private void listView_results_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            this.lvic.set_column_number(e.Column);
            this.listView_results.ListViewItemSorter=this.lvic;
            this.listView_results.Sort();
            this.listView_results.ListViewItemSorter=null;// avoid to organize when changing data        
        }
        private void radioButton_tcp_scan_CheckedChanged(object sender, System.EventArgs e)
        {
            this.checkBox_tcp_wait_for_data.Enabled=(this.radioButton_tcp_scan.Checked&&!this.checkBox_use_proxy.Checked);
            this.panel_proxy.Enabled=this.radioButton_cgi_scan.Checked||this.radioButton_tcp_scan.Checked;        
        }
        private void radioButton_cgi_scan_CheckedChanged(object sender, System.EventArgs e)
        {
            this.panel_proxy.Enabled=this.radioButton_cgi_scan.Checked||this.radioButton_tcp_scan.Checked;
            this.panel_data.Enabled=!this.radioButton_cgi_scan.Checked;
        }
        private void radioButton_icmp_scan_CheckedChanged(object sender, System.EventArgs e)
        {
            this.checkBox_icmp_scan_before.Enabled=!this.radioButton_icmp_scan.Checked;
            this.menuItem_copy_selected_ip_port.Visible=!this.radioButton_icmp_scan.Checked;
        }
        private void checkBox_use_proxy_CheckedChanged(object sender, System.EventArgs e)
        {
            this.checkBox_tcp_wait_for_data.Enabled=(this.radioButton_tcp_scan.Checked&&!this.checkBox_use_proxy.Checked);        
            if (this.checkBox_use_proxy.Checked)
                this.checkBox_tcp_wait_for_data.Checked=true;// we must wait for data when using proxy
        }
        private void checkBox_hexa_data_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.checkBox_hexa_data.Checked)
                this.textBox_data.Text=easy_socket.hexa_convert.string_to_hexa(this.textBox_data.Text);
            else
                this.textBox_data.Text=easy_socket.hexa_convert.hexa_to_string(this.textBox_data.Text);
        }
        public byte[] get_data_to_send()
        {
            if (this.checkBox_hexa_data.Checked)
                return easy_socket.hexa_convert.hexa_to_byte(this.textBox_data.Text);
            return System.Text.Encoding.ASCII.GetBytes(this.textBox_data.Text);
        }
        public string uint_to_fixed_size_string(uint ui)
        {
            string str2;
            string str=ui.ToString();
            int nb_space_to_add=this.nb_digits-str.Length;
            if (nb_space_to_add>0)
            {
                str2=new string(' ',nb_space_to_add);
                str=str2+str;
            }
            return str;
        }

        private void menuItem_copy_selected_ip_Click(object sender, System.EventArgs e)
        {
            string str="";
            for (int cpt=0;cpt<this.listView_results.SelectedItems.Count;cpt++)
            {
                str+=this.listView_results.SelectedItems[cpt].SubItems[1].Text;//ip
                str+="\r\n";
            } 
            Clipboard.SetDataObject(str,true);
        }
        private void menuItem_copy_selected_ip_port_Click(object sender, System.EventArgs e)
        {
            string str="";
            for (int cpt=0;cpt<this.listView_results.SelectedItems.Count;cpt++)
            {
                str+=this.listView_results.SelectedItems[cpt].SubItems[1].Text;//ip
                str+=":";
                str+=this.listView_results.SelectedItems[cpt].SubItems[2].Text;//port
                str+="\r\n";
            } 
            Clipboard.SetDataObject(str,true);        
        }

        #region browse file
        private enum OPEN_FILE_TYPE:byte
        {
            CGI,
            IP,
            PORT,
            IP_PORT,
            PROXY
        }

        private void button_browse_cgi_file_Click(object sender, System.EventArgs e)
        {
            this.last_openfile_type=OPEN_FILE_TYPE.CGI;
            this.openFileDialog.ShowDialog(this);
        }

        private void button_browse_ip_file_Click(object sender, System.EventArgs e)
        {
            this.last_openfile_type=OPEN_FILE_TYPE.IP;
            this.openFileDialog.ShowDialog(this);
        }

        private void button_browse_port_file_Click(object sender, System.EventArgs e)
        {
            this.last_openfile_type=OPEN_FILE_TYPE.PORT;
            this.openFileDialog.ShowDialog(this);
        }

        private void button_browse_same_ipport_list_file_Click(object sender, System.EventArgs e)
        {
            this.last_openfile_type=OPEN_FILE_TYPE.IP_PORT;
            this.openFileDialog.ShowDialog(this);        
        }

        private void button_browse_proxy_file_Click(object sender, System.EventArgs e)
        {
            this.last_openfile_type=OPEN_FILE_TYPE.PROXY;
            this.openFileDialog.ShowDialog(this);         
        }

        private void openFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string str=this.openFileDialog.FileName;
            switch (this.last_openfile_type)
            {
                case OPEN_FILE_TYPE.CGI:
                    this.textBox_cgi_file.Text=str;
                    break;
                case OPEN_FILE_TYPE.IP:
                    this.textBox_ip.Text=str;
                    break;
                case OPEN_FILE_TYPE.PORT:
                    this.textBox_port.Text=str;
                    break;
                case OPEN_FILE_TYPE.IP_PORT:
                    this.textBox_same_ip_port_file.Text=str;
                    break;
                case OPEN_FILE_TYPE.PROXY:
                    this.textBox_proxy_list_file.Text=str;
                    break;
            }
        }

        #endregion
        #region saving
                    private void listView_results_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
                    {
                              if (e.Button==MouseButtons.Right)
                              {
                                        this.contextMenu_list_view.Show((ListView) sender,new System.Drawing.Point(e.X,e.Y));
                              }        
                    }
                    private bool ask_saving_file()
                    {
                              DialogResult dr=this.saveFileDialog.ShowDialog(this);
                              if (dr==DialogResult.OK)
                                        return true;
                              return false;
                    }
                    private void saveFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
                    {
                              this.str_saving_file=this.saveFileDialog.FileName;
                    }
                    private void menuItem_save_all_Click(object sender, System.EventArgs e)
                    {
                              if (!this.ask_saving_file())
                                        return;
                              string str="";
                              int cpt2;
                              for (int cpt=0;cpt<this.listView_results.Items.Count;cpt++)
                              {
                                        str+=this.listView_results.Items[cpt].Text;
                                        for (cpt2=1;cpt2<this.listView_results.Items[cpt].SubItems.Count;cpt2++)// subitems[0].text=item.text
                                                  str+="\t"+this.listView_results.Items[cpt].SubItems[cpt2].Text;
                                        str+="\r\n";
                              }
                              file_access.write(this.str_saving_file,str);
                    }

                    private void menuItem_save_selected_Click(object sender, System.EventArgs e)
                    {
                              if (!this.ask_saving_file())
                                        return;
                              string str="";
                              int cpt2;
                              for (int cpt=0;cpt<this.listView_results.SelectedItems.Count;cpt++)
                              {
                                        str+=this.listView_results.SelectedItems[cpt].Text;
                                        for (cpt2=1;cpt2<this.listView_results.SelectedItems[cpt].SubItems.Count;cpt2++)// subitems[0].text=item.text
                                                  str+="\t"+this.listView_results.SelectedItems[cpt].SubItems[cpt2].Text;
                                        str+="\r\n";
                              }        
                              file_access.write(this.str_saving_file,str);
                    }
        #endregion
        #region add remove events
        private void add_events(easy_socket.icmp.icmp_server icmp_srv)
        {
            // icmp_destination_unreachable
            this.icmp_srv.event_icmp_destination_unreachable_Data_Arrival+=new easy_socket.icmp.icmp_destination_unreachable_Data_Arrival_EventHandler(ev_destination_unreachable);
            // icmp_reply event
            this.icmp_srv.event_icmp_echo_reply_Data_Arrival+=new easy_socket.icmp.icmp_echo_reply_Data_Arrival_EventHandler(ev_echo_reply);
            // icmp_parameter_problem
            this.icmp_srv.event_icmp_parameter_problem_Data_Arrival+=new easy_socket.icmp.icmp_parameter_problem_Data_Arrival_EventHandler(ev_parameter_problem);
            // icmp_source_quench
            this.icmp_srv.event_icmp_source_quench_Data_Arrival+=new easy_socket.icmp.icmp_source_quench_Data_Arrival_EventHandler(ev_source_quench);
            // icmp_time_exceeded
            this.icmp_srv.event_icmp_time_exceeded_message_Data_Arrival+=new easy_socket.icmp.icmp_time_exceeded_message_Data_Arrival_EventHandler(ev_time_exceeded);
            // Error event
            this.icmp_srv.event_Socket_Error+=new easy_socket.icmp.Socket_Error_EventHandler(ev_socket_error);
        }
        
        public void add_events(tcp_scan tcp_clt)
        {
            tcp_clt.event_Socket_Data_Error+=new easy_socket.tcp.Socket_Data_Error_EventHandler(tcp_socket_error);
            tcp_clt.event_Socket_Data_Connected_To_Remote_Host+=new easy_socket.tcp.Socket_Data_Connected_To_Remote_Host_EventHandler(tcp_connected);
            tcp_clt.event_Socket_Data_DataArrival+=new easy_socket.tcp.Socket_Data_DataArrival_EventHandler(tcp_socket_data_arrival);
            tcp_clt.event_Socket_Data_Closed_by_Remote_Side+=new easy_socket.tcp.Socket_Data_Closed_by_Remote_Side_EventHandler(tcp_socket_closed_by_remote_side);
        }
        public void remove_events(tcp_scan tcp_clt)
        {
            tcp_clt.event_Socket_Data_Error-=new easy_socket.tcp.Socket_Data_Error_EventHandler(tcp_socket_error);
            tcp_clt.event_Socket_Data_Connected_To_Remote_Host-=new easy_socket.tcp.Socket_Data_Connected_To_Remote_Host_EventHandler(tcp_connected);
            tcp_clt.event_Socket_Data_DataArrival-=new easy_socket.tcp.Socket_Data_DataArrival_EventHandler(tcp_socket_data_arrival);
            tcp_clt.event_Socket_Data_Closed_by_Remote_Side-=new easy_socket.tcp.Socket_Data_Closed_by_Remote_Side_EventHandler(tcp_socket_closed_by_remote_side);
        }
        private void add_events(udp_scan udp_srv)
        {
            udp_srv.event_Socket_Server_Remote_Host_Unreachable+=new easy_socket.udp.Socket_Server_Remote_Host_Unreachable(udp_socket_remote_host_unreachable);
            udp_srv.event_Socket_Server_Error+=new easy_socket.udp.Socket_Server_Error_EventHandler(udp_socket_error);
            udp_srv.event_Socket_Server_Data_Arrival+=new easy_socket.udp.Socket_Server_Data_Arrival_EventHandler(udp_socket_data_arrival);
        }
        public void remove_events(udp_scan udp_srv)
        {
            udp_srv.event_Socket_Server_Remote_Host_Unreachable-=new easy_socket.udp.Socket_Server_Remote_Host_Unreachable(udp_socket_remote_host_unreachable);
            udp_srv.event_Socket_Server_Error-=new easy_socket.udp.Socket_Server_Error_EventHandler(udp_socket_error);
            udp_srv.event_Socket_Server_Data_Arrival-=new easy_socket.udp.Socket_Server_Data_Arrival_EventHandler(udp_socket_data_arrival);
        }
        #endregion
        #region get/set scan type
        public enum SCAN_TYPE:byte
        {
            TCP,
            UDP,
            ICMP,
            CGI
        }
        public void set_scan_type(SCAN_TYPE type)
        {
            switch (type)
            {
                case SCAN_TYPE.TCP:
                    this.radioButton_tcp_scan.Checked=true;
                    break;
                case SCAN_TYPE.UDP:
                    this.radioButton_udp_scan.Checked=true;
                    break;
                case SCAN_TYPE.ICMP:
                    this.radioButton_icmp_scan.Checked=true;
                    break;
                case SCAN_TYPE.CGI:
                    this.radioButton_cgi_scan.Checked=true;
                    break;
            }
        }
        private SCAN_TYPE get_scan_type()
        {
            if (this.radioButton_tcp_scan.Checked)
                return SCAN_TYPE.TCP;
            if (this.radioButton_udp_scan.Checked)
                return SCAN_TYPE.UDP;
            if (this.radioButton_icmp_scan.Checked)
                return SCAN_TYPE.ICMP;
            if (this.radioButton_cgi_scan.Checked)
                return SCAN_TYPE.CGI;
            // return icmp scan as default
            return SCAN_TYPE.ICMP;
        }
        #endregion
        #region get proxy type
        public enum PROXY_TYPE:byte
        {// must be in the same order than in combobox
            HTTP_GET=0,
            HTTP_CONNECT,
            SOCKS4,
            SOCKS5
        }
        public PROXY_TYPE get_proxy_type()
        {
            return (PROXY_TYPE)this.comboBox_proxy_type.SelectedIndex;
        }
        #endregion
        #region server ping status 
        public bool is_server_pinged(string ip,ref bool ping_success)
        {
            ping_success=false;
            this.evtal_pinged_serversUnLocked.WaitOne();
            this.evtal_pinged_serversUnLocked.Reset();
            for (int cpt=0;cpt<this.al_pinged_servers.Count;cpt++)
            {
                if (((ping_result)this.al_pinged_servers[cpt]).ip==ip)
                {
                    ping_success=((ping_result)this.al_pinged_servers[cpt]).success;
                    this.evtal_pinged_serversUnLocked.Set();
                    return true;
                }
            }
            this.evtal_pinged_serversUnLocked.Set();
            return false;
        }
        public void add_pinged_server(string ip,bool success)
        {
            this.evtal_pinged_serversUnLocked.WaitOne();
            this.evtal_pinged_serversUnLocked.Reset();
            this.al_pinged_servers.Add(new ping_result(ip,success));
            this.evtal_pinged_serversUnLocked.Set();
        }
        public void remove_server_being_ping(string ip)
        {
            this.evtal_servers_being_pingUnLocked.WaitOne();
            this.evtal_servers_being_pingUnLocked.Reset();
            this.al_servers_being_ping.Remove(ip);
            this.evtal_servers_being_pingUnLocked.Set();
        }
        /// <summary>
        /// check ping state and if server was not already pinged, add ip to al_servers_being_ping
        /// caller MUST send ping if this function return false
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public bool is_server_being_ping(string ip)
        {
            bool b=false;
            this.evtal_servers_being_pingUnLocked.WaitOne();
            this.evtal_servers_being_pingUnLocked.Reset();
            for (int cpt=0;cpt<this.al_servers_being_ping.Count;cpt++)
            {
                if ((string)this.al_servers_being_ping[cpt]==ip)
                {
                    this.evtal_servers_being_pingUnLocked.Set();
                    return true;
                }
            }
            // add must be done in the same lock as checking to avoid multithreading troubles
            if (!this.is_server_pinged(ip,ref b))
                this.al_servers_being_ping.Add(ip);// add is done in locked state
            this.evtal_servers_being_pingUnLocked.Set();
            return false;
        }
        #endregion
        #region proxy status 

        public void no_more_proxy()
        {
            // if already signaled 
            if (this.b_no_more_proxy_signaled)
                return;// do nothing
            this.b_no_more_proxy_signaled=true;
            MessageBox.Show(this,"No valid proxy in proxy list.\r\nScan wil be stopped","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            this.stop();
        }

        public void add_checked_proxy(proxy_information proxy)
        {
            this.evtal_proxy_UnLocked.WaitOne();
            this.evtal_proxy_UnLocked.Reset();
            proxy.b_checked=true;
            this.evtal_proxy_UnLocked.Set();
        }
        public void remove_proxy_being_checked(proxy_information proxy)
        {
            this.evtal_proxy_being_checkUnLocked.WaitOne();
            this.evtal_proxy_being_checkUnLocked.Reset();
            this.al_proxy_being_checked.Remove(proxy);
            this.evtal_proxy_being_checkUnLocked.Set();
        }
        /// <summary>
        /// check proxy state and if proxy was not already checked, add proxy to al_proxy_being_checked
        /// caller MUST check proxy if this function return false
        /// </summary>
        public bool is_proxy_being_checked(proxy_information proxy)
        {
            this.evtal_proxy_being_checkUnLocked.WaitOne();
            this.evtal_proxy_being_checkUnLocked.Reset();
            for (int cpt=0;cpt<this.al_proxy_being_checked.Count;cpt++)
            {
                if (((proxy_information)this.al_proxy_being_checked[cpt])==proxy)
                {
                    this.evtal_proxy_being_checkUnLocked.Set();
                    return true;
                }
            }
            if (!proxy.b_checked)
                this.al_proxy_being_checked.Add(proxy);// add is done in locked state
            this.evtal_proxy_being_checkUnLocked.Set();
            return false;
        }
        public proxy_information get_random_proxy()
        {
            if (this.al_proxy.Count==0)
                return null;
            proxy_information proxy;
            System.Random rnd;
            rnd=new System.Random();
            this.evtal_proxy_UnLocked.WaitOne();
            this.evtal_proxy_UnLocked.Reset();
            proxy=(proxy_information)this.al_proxy[rnd.Next(this.al_proxy.Count)];
            this.evtal_proxy_UnLocked.Set();
            return proxy;
        }
        public void remove_proxy(proxy_information proxy)
        {
            this.evtal_proxy_UnLocked.WaitOne();
            this.evtal_proxy_UnLocked.Reset();
            this.al_proxy.Remove(proxy);
            this.evtal_proxy_UnLocked.Set();
        }
        #endregion

        #region start pause stop
        private void enable_interface(bool new_state)
        {
            this.groupBox_cgi.Enabled=new_state;
            this.groupBox_ip.Enabled=new_state;
            this.groupBox_port.Enabled=new_state;
            this.groupBox_scan_type.Enabled=new_state;
            this.groupBox_options.Enabled=new_state;
            this.groupBox1.Enabled=new_state;
            this.button_start.Enabled=new_state;
            this.button_pause.Enabled=!new_state;
            this.button_stop.Enabled=!new_state;
        }
        private void button_start_Click(object sender, System.EventArgs e)
        {
            // disable interface
            this.enable_interface(false);

            // clear array lists
            this.al_servers_being_ping.Clear();
            this.al_pinged_servers.Clear();

            // clear list view
            this.listView_results.Items.Clear();

            // clear ArrayList of connection/data
            this.al_full_scan_infos=new System.Collections.ArrayList(255);// as al_full_scan_infos can be replaced by another object it could be null --> Clear() failed

            this.nb_threads=(int)this.numericUpDown_nb_threads.Value;
            this.i_time_out=(int)this.numericUpDown_timeout.Value;

            // get scan type
            this.current_scan_type=this.get_scan_type();

            // fill ArrayList
            if (!this.fill_scan_arraylist())
            {
                // enable interface
                this.enable_interface(true);
                return;
            }
            // fill proxy list
            if (this.checkBox_use_proxy.Checked)
            {
                this.b_no_more_proxy_signaled=false;
                this.al_proxy.Clear();
                this.al_proxy_being_checked.Clear();
                string[] lines=file_access.read(this.textBox_proxy_list_file.Text).Split("\r\n".ToCharArray());
                CHost ipe;
                for (int cpt=0;cpt<lines.Length;cpt++)
                {
                    if (lines[cpt]=="")
                        continue;
                    ipe=Cmultiple_elements_parsing.Parse_IP_two_points_Port(lines[cpt]);
                    if (ipe==null)
                        break;
                    this.al_proxy.Add(new proxy_information(ipe.ip,ipe.port));
                }
                if (this.al_proxy.Count==0)
                {
                    MessageBox.Show(this,"No proxy in proxy list","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    // enable interface
                    this.enable_interface(true);
                    return;
                }
            }
            // set progress bar max value
            this.progressBar.Maximum=this.al_full_scan_infos.Count;
            // set initial ArrayList size
            this.al_ini_size=this.al_full_scan_infos.Count;
            // get number of needed digits
            this.nb_digits=this.al_ini_size.ToString().Length;
            // get number of threads
            this.nb_threads=Math.Min(this.nb_threads,this.al_full_scan_infos.Count);
            if (this.nb_threads<=0)
            {
                // enable interface
                this.enable_interface(true);
                return;
            }
            // set scan position
            this.scan_position=-this.nb_threads;

            this.b_data=get_data_to_send();

            this.b_do_icmp_scan_before=(!this.radioButton_icmp_scan.Checked)&&(this.checkBox_icmp_scan_before.Checked);
            if ((this.current_scan_type==SCAN_TYPE.ICMP)||this.b_do_icmp_scan_before)
            {
                // clear previous icmp identifiers (if any were remaining)
                this.al_icmp_identifiers.Clear();
                // start icmp server
                this.icmp_srv.start();
            }
            // start scan
            this.send_remaining();// create named semaphore
            if (this.evtThreadStart.WaitOne(3000,false))
            {
                // thread is started
                // increase number of signaled event by nb_threads
                Semaphore.ReleaseSemaphore(this.h_semaphore,nb_threads);
                // string strerr=API_error.GetAPIErrorMessageDescription(API_error.GetLastError()); // DEBUG ONLY
                this.b_scan_running=true;
            }
            else// thread creation or handle creation error
            {
                this.stop();
            }
        }
    
        private bool fill_scan_arraylist()
        {
            string[] lines;
            string[] ip;
            string[] str_port=null;
            ushort[] port=null;
            int cpt2;
            int cpt;

            if (this.radioButton_splited_ip_port_list.Checked)
            {
                if (this.radioButton_following_ip.Checked)
                {
                    // if following ip
                    ip=Cmultiple_elements_parsing.Parse_ip(this.textBox_ip.Text);
                    if (ip==null)
                        return false;
                }
                else
                {
                    // if ip in file
                    ip=file_access.read(this.textBox_ip.Text).Split("\r\n".ToCharArray());
                    if ((ip.Length==1)&&(ip[0]==""))
                    {
                        MessageBox.Show(this,"Empty ip file. Scan aborted","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return false;
                    }
                }
                if (this.radioButton_following_port.Checked)
                {
                    // if following port
                    port=Cmultiple_elements_parsing.Parse_ushort(this.textBox_port.Text);
                    if (port==null)
                        return false;
                }
                else
                {
                    // if port in file
                    str_port=file_access.read(this.textBox_port.Text).Split("\r\n".ToCharArray());
                    if ((str_port.Length==1)&&(str_port[0]==""))
                    {
                        MessageBox.Show(this,"Empty port file. Scan aborted","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        return false;
                    }
                }    
                // fill ArrayList with ip port
                for (cpt=0;cpt<ip.Length;cpt++)
                {
                    if (ip[cpt]=="")
                        continue;
                    if (this.radioButton_icmp_scan.Checked)// icmp --> don't fill port
                        this.al_full_scan_infos.Add(new scan_information(ip[cpt],(uint)(this.al_full_scan_infos.Count+1)));
                    else // not icmp should fill port
                    {
                        if (this.radioButton_following_port.Checked)
                        {
                            for (cpt2=0;cpt2<port.Length;cpt2++)
                            {
                                this.al_full_scan_infos.Add(new scan_information(ip[cpt],port[cpt2],(uint)(this.al_full_scan_infos.Count+1)));
                            }
                        }
                        else
                        {
                            for (cpt2=0;cpt2<str_port.Length;cpt2++)
                            {
                                if (str_port[cpt]=="")
                                    continue;
                                this.al_full_scan_infos.Add(new scan_information(ip[cpt],System.Convert.ToUInt16(str_port[cpt2]),(uint)(this.al_full_scan_infos.Count+1)));
                            }
                        }
                    }
                }
            }
            else // same ip list
            {
                lines=file_access.read(this.textBox_same_ip_port_file.Text).Split("\r\n".ToCharArray());
                CHost ipe;
                for (cpt=0;cpt<lines.Length;cpt++)
                {
                    if (lines[cpt]=="")
                        continue;
                    ipe=Cmultiple_elements_parsing.Parse_IP_two_points_Port(lines[cpt]);
                    if (ipe==null)
                        break;
                    this.al_full_scan_infos.Add(new scan_information(ipe.ip,ipe.port,(uint)(this.al_full_scan_infos.Count+1)));
                }
            }
            // in case of cgi scan
            if (this.current_scan_type==SCAN_TYPE.CGI)
            {
                string[] cgi_lines;
                // get cgi list from file
                string strdata=file_access.read(this.textBox_cgi_file.Text);
                scan_information si;
                scan_information tmpsi;
                if (strdata=="")
                    return false;
                // get lines
                cgi_lines=strdata.Split("\r\n".ToCharArray());
                System.Collections.ArrayList tmpal=new System.Collections.ArrayList(this.al_full_scan_infos.Count*cgi_lines.Length);
                // for each ip/port add full cgi list
                for (int alcpt=0;alcpt<this.al_full_scan_infos.Count;alcpt++)
                {
                    // get the already defined scan_information (contains ip and port)
                    si=(scan_information)this.al_full_scan_infos[alcpt];
                    // for each cgi
                    for(cpt=0;cpt<cgi_lines.Length;cpt++)
                    {
                        // if line empty
                        if (cgi_lines[cpt]=="")
                            continue;
                        // line not empty
                        tmpsi=new scan_information(si.ip,si.port,cgi_lines[cpt],(uint)(tmpal.Count+1));
                        tmpal.Add(tmpsi);
                    }
                }
                // put the new list of scan_information in this.al_full_scan_infos
                this.al_full_scan_infos=tmpal;
            }
            return true;
        }
        private void button_pause_Click(object sender, System.EventArgs e)
        {
            // change state of pause (resume or pause)
            this.b_pause=!this.b_pause;
            if (this.b_pause)
                this.pause();
            else
                this.resume();
        }
        private void pause()
        {
            // pause
            this.evtPause.Reset();// wait result of sending packets but don't send new
            this.button_pause.Text="Resume";
        }
        private void resume()
        {
            // resume
            this.evtPause.Set();
            this.button_pause.Text="Pause";
        }
        private void button_stop_Click(object sender, System.EventArgs e)
        {
            this.stop();
        }

        private void stop()
        {

            if (!this.b_scan_running)
                return;
            // clear ArrayList
            this.al_full_scan_infos.Clear();

            // set the stop event before releasing semaphore
            this.evtStop.Set();

            // send event to release wait infinite
            // increase number of signaled event by one
            Semaphore.ReleaseSemaphore(this.h_semaphore,1);

            // wait for event
            this.evtScanFinished.WaitOne(this.i_time_out,false);

            // reset stop event
            this.evtStop.Reset();
            // resume pause if any (restore interface information)
            this.resume();
            
        }
        #endregion

        /// <summary>
        /// solve multithreading troubles
        /// </summary>
        /// <param name="lvi"></param>
        public void add_listview_item(ListViewItem lvi)
        {
            this.evtListviewItemUnLocked.WaitOne();
            this.listView_results.Items.Add(lvi);
            this.evtListviewItemUnLocked.Set();
        }

        #region send remaining
        /// <summary>
        /// returns the next scan to do
        /// </summary>
        /// <returns>return null if no more scan to do</returns>
        private scan_information get_remaining()
        {
            if (this.al_full_scan_infos.Count==0)
                return null;
            int index=0;
            if (this.checkBox_random_order.Checked)// if random order (else take the first element
                index=(int)System.Math.Floor(this.rnd.Next(this.al_full_scan_infos.Count));
            // get member
            scan_information si=(scan_information)this.al_full_scan_infos[index];
            // remove it
            this.al_full_scan_infos.RemoveAt(index);
            // return member
            return si;
        }

        private void send_remaining()
        {
            System.Threading.ThreadStart myThreadStart = new System.Threading.ThreadStart(my_send_remaining);
            System.Threading.Thread myThread =new System.Threading.Thread(myThreadStart);
            myThread.Start();
        }

        private void my_send_remaining()
        {
            string str_msg;
            uint res_wait_for_single_object;
            int index_table_wait_handle;

            this.h_semaphore=Semaphore.CreateSemaphore(0,this.nb_threads+1,"");
            if (this.h_semaphore==0)
            {
                MessageBox.Show(this,"Error can't create semaphore","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            this.evtThreadStart.Set();
            System.Threading.WaitHandle[] table_wait_handle=new System.Threading.WaitHandle[2];
            table_wait_handle[0]=this.evtStop;
            table_wait_handle[1]=this.evtPause;
            
            API_error.GetLastError();// clear previous error msg if any

            // while scan is not finished
            while(this.al_ini_size>this.scan_position)
            {
                // could be improved with multiple WaitForMultipleObjects on event close and stop. Now we should open semaphore and increase it
                res_wait_for_single_object=Semaphore.WaitForSingleObject(this.h_semaphore,Semaphore.INFINITE);
                switch(res_wait_for_single_object)
                {
                    case Semaphore.WAIT_OBJECT_0:
                        // MessageBox.Show(this,"wait_object_0");

                        // check pause and stop event
                        index_table_wait_handle=System.Threading.WaitHandle.WaitAny(table_wait_handle);
                        if (index_table_wait_handle==0)//stop event
                        {
                            Semaphore.CloseHandle(this.h_semaphore);
                            // stop icmp timer
                            if ((this.current_scan_type==SCAN_TYPE.ICMP)||(this.b_do_icmp_scan_before))
                            {
                                this.icmp_srv.stop();
                            }

                            // scan is no more running
                            this.b_scan_running=false;

                            // enable interface
                            this.enable_interface(true);

                            // set finished event
                            this.evtScanFinished.Set();
                            MessageBox.Show(this,"Scan Stopped","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            return;
                        }
                        this.send_next();
                        // count the number of time send_next function is called and then number of finished threads
                        this.scan_position++;
                        if ((this.scan_position>=this.progressBar.Minimum)&&(this.scan_position<=this.progressBar.Maximum))
                            this.progressBar.Value=this.scan_position;

                        break;
                    // case Semaphore.WAIT_TIMEOUT:
                    //    MessageBox.Show(this,"Timeout occurs in WaitForSingleObject","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    //    break;
                    case Semaphore.WAIT_ABANDONED:
                        str_msg=API_error.GetAPIErrorMessageDescription(API_error.GetLastError());
                        MessageBox.Show(this,"Error in WaitForSingleObject: WAIT_ABANDONED signal\r\n"+str_msg,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        Semaphore.CloseHandle(this.h_semaphore);
                        return;
                }
            }
            Semaphore.CloseHandle(this.h_semaphore);
            // stop icmp timer
            if ((this.current_scan_type==SCAN_TYPE.ICMP)||this.b_do_icmp_scan_before)
            {
                this.icmp_srv.stop();
            }
            // scan is no more running
            this.b_scan_running=false;
            // enable interface
            this.enable_interface(true);
            // set finished event
            this.evtScanFinished.Set();
            // sort result
            this.lvic.Reset();
            this.listView_results_ColumnClick(this.listView_results,new ColumnClickEventArgs(0));

            MessageBox.Show(this,"Scan Finished","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
            this.progressBar.Value=0;
        }

        private void send_next()
        {
            System.Threading.ThreadStart myThreadStart = new System.Threading.ThreadStart(my_send_next);
            System.Threading.Thread myThread =new System.Threading.Thread(myThreadStart);
            myThread.Start();
        }

        /// <summary>
        /// started in new thread in case of ping servers before trying connection
        /// </summary>
        private void my_send_next()
        {
            tcp_scan tcp_clt;
            cgi_scan cgi_clt;
            udp_scan udp_srv;
            icmp_scan icmp_s;

            scan_information scan_info=get_remaining();
            if (scan_info==null)
                return;

            switch (this.current_scan_type)
            {
                case SCAN_TYPE.TCP:
                    tcp_clt=new tcp_scan(this,this.checkBox_use_proxy.Checked,this.checkBox_check_proxy.Checked,this.textBox_ip_to_check_proxy.Text,80);
                    this.add_events(tcp_clt);
                    tcp_clt.id=scan_info.id;
                    tcp_clt.connect(scan_info.ip,scan_info.port,this.b_do_icmp_scan_before);
                    break;
                case SCAN_TYPE.CGI:
                    cgi_clt=new cgi_scan(this,this.checkBox_use_proxy.Checked,this.checkBox_check_proxy.Checked,this.textBox_ip_to_check_proxy.Text,80);
                    this.add_events(cgi_clt);
                    cgi_clt.cgi=scan_info.data;
                    cgi_clt.id=scan_info.id;
                    cgi_clt.connect(scan_info.ip,scan_info.port,this.b_do_icmp_scan_before);
                    break;
                case SCAN_TYPE.UDP:
                    udp_srv=new udp_scan(this);
                    udp_srv.remote_ip=scan_info.ip;
                    udp_srv.remote_port=scan_info.port;
                    this.add_events(udp_srv);
                    udp_srv.start();
                    udp_srv.id=scan_info.id;
                    udp_srv.set_time_out(this.i_time_out);
                    udp_srv.send(this.b_data,scan_info.ip,scan_info.port,this.b_do_icmp_scan_before);
                    break;
                case SCAN_TYPE.ICMP:
                    int t_ini=System.Environment.TickCount;
                    icmp_s=new icmp_scan(this,scan_info.ip,t_ini);
                    icmp_s.event_Socket_Error+=new easy_socket.icmp.Socket_Error_EventHandler(ev_socket_error);
                    icmp_s.data=this.b_data;
                    this.add_icmp_identifier(icmp_s);
                    icmp_s.identifier=(UInt16)(t_ini>>16);
                    icmp_s.sequence_number=(UInt16)(t_ini&0xffffffff);
                    icmp_s.set_time_out(this.i_time_out);
                    icmp_s.send(scan_info.ip);
                    break;
            }
        }
        #endregion

        #region udp

        private void udp_socket_data_arrival(easy_socket.udp.Socket_Server socket, easy_socket.udp.EventArgs_ReceiveDataSocketUDP e)
        {
            System.Net.IPEndPoint ipep=(System.Net.IPEndPoint)e.remote_host_EndPoint;
            this.add_listview_item(new ListViewItem(new string[]{this.uint_to_fixed_size_string(((udp_scan)socket).id)+SEPARATOR_ID_TIME+System.DateTime.Now.TimeOfDay.ToString(),
                                                                 ((udp_scan)socket).remote_ip,
                                                                 ((udp_scan)socket).remote_port.ToString(),
                                                                 "Data: "+System.Text.Encoding.ASCII.GetString(e.buffer)+" from "+ipep.Address+":"+ipep.Port.ToString()}));
        }
        private void udp_socket_error(easy_socket.udp.Socket_Server socket, easy_socket.udp.EventArgs_Exception e)
        {
            this.add_listview_item(new ListViewItem(new string[]{this.uint_to_fixed_size_string(((udp_scan)socket).id)+SEPARATOR_ID_TIME+System.DateTime.Now.TimeOfDay.ToString(),
                                                                    ((udp_scan)socket).remote_ip,
                                                                    ((udp_scan)socket).remote_port.ToString(),
                                                                    "Error :"+e.exception.Message}));
            ((udp_scan)socket).hevt_error.Set();
        }
        private void udp_socket_remote_host_unreachable(easy_socket.udp.Socket_Server socket, System.EventArgs e)
        {
            this.add_listview_item(new ListViewItem(new string[]{this.uint_to_fixed_size_string(((udp_scan)socket).id)+SEPARATOR_ID_TIME+System.DateTime.Now.TimeOfDay.ToString(),
                                                                    ((udp_scan)socket).remote_ip,((udp_scan)socket).remote_port.ToString(),
                                                                    "Destination Unreachable"}));
            ((udp_scan)socket).hevt_unreachable.Set();
        }
        #endregion
        #region tcp/cgi
        private void tcp_socket_error(easy_socket.tcp.Socket_Data socket, easy_socket.tcp.EventArgs_Exception e)
        {
            ((tcp_scan)(socket)).hevt_error.Set();
            socket.close();
            if (socket is proxy_scan)
            {
                proxy_scan proxy_s=((proxy_scan)socket);
                if (proxy_s.b_checking_proxy)
                {
                    this.add_listview_item(new ListViewItem(new string[]{new string(' ',this.nb_digits)+System.DateTime.Now.TimeOfDay.ToString(),
                                                                            socket.RemoteIP,
                                                                            socket.RemotePort.ToString(),
                                                                            "Proxy checking socket error :"+e.exception.Message}));
                }
                else
                {
                    this.add_listview_item(new ListViewItem(new string[]{this.uint_to_fixed_size_string(proxy_s.id)+SEPARATOR_ID_TIME+System.DateTime.Now.TimeOfDay.ToString(),
                                                                            proxy_s.target_ip,
                                                                            proxy_s.target_port.ToString(),
                                                                            "Error using proxy "+socket.RemoteIP+":"+socket.RemotePort.ToString()+" : "+e.exception.Message}));
                }
            }
            else
            {
                this.add_listview_item(new ListViewItem(new string[]{this.uint_to_fixed_size_string(((tcp_scan)socket).id)+SEPARATOR_ID_TIME+System.DateTime.Now.TimeOfDay.ToString(),
                                                                        socket.RemoteIP,
                                                                        socket.RemotePort.ToString(),
                                                                        "Error :"+e.exception.Message}));
            }
        }
        private void tcp_socket_closed_by_remote_side(easy_socket.tcp.Socket_Data socket, System.EventArgs e)
        {
            ((tcp_scan)(socket)).hevt_closed_by_remote_side.Set();
            if (socket is proxy_scan)
            {
                proxy_scan proxy_s=((proxy_scan)socket);
                if (proxy_s.b_checking_proxy)
                {
                    this.add_listview_item(new ListViewItem(new string[]{new string(' ',this.nb_digits)+System.DateTime.Now.TimeOfDay.ToString(),
                                                                            socket.RemoteIP,
                                                                            socket.RemotePort.ToString(),
                                                                            "Proxy checking: Connection closed by remote side"}));
                }
                else
                {
                    this.add_listview_item(new ListViewItem(new string[]{this.uint_to_fixed_size_string(proxy_s.id)+SEPARATOR_ID_TIME+System.DateTime.Now.TimeOfDay.ToString(),
                                                                            proxy_s.target_ip,
                                                                            proxy_s.target_port.ToString(),
                                                                            "Connection closed by remote side using proxy "+socket.RemoteIP+":"+socket.RemotePort.ToString()}));
                }
            }
            else
            {
                this.add_listview_item(new ListViewItem(new string[]{this.uint_to_fixed_size_string(((tcp_scan)socket).id)+SEPARATOR_ID_TIME+System.DateTime.Now.TimeOfDay.ToString(),
                                                                        socket.RemoteIP,
                                                                        socket.RemotePort.ToString(),
                                                                        "Connection closed by remote side"}));
            }
        }
        private void tcp_socket_data_arrival(easy_socket.tcp.Socket_Data socket, easy_socket.tcp.EventArgs_ReceiveDataSocket e)
        {
            if (socket is proxy_scan)
            {
                byte[] data=new byte[e.buffer.Length];
                System.Array.Copy(e.buffer,data,e.buffer.Length);
                proxy_scan proxy_s=((proxy_scan)socket);
                bool b_successfull_reply=proxy_s.is_good_reply(ref data);
                if (proxy_s.b_checking_proxy)
                {
                    if (b_successfull_reply)
                    {
                        if ((proxy_s.b_all_socks5_headers_rcv)||(proxy_s.type!=PROXY_TYPE.SOCKS5))
                        {
                            this.add_listview_item(new ListViewItem(new string[]{new string(' ',this.nb_digits)+System.DateTime.Now.TimeOfDay.ToString(),
                                                                                    socket.RemoteIP,
                                                                                    socket.RemotePort.ToString(),
                                                                                    "Proxy successfully checked"}));
                            proxy_s.hevt_proxy_checking_success.Set();
                            proxy_s.close();
                        }
                    }
                    else// unsuccessful reply
                    {
                        this.add_listview_item(new ListViewItem(new string[]{new string(' ',this.nb_digits)+System.DateTime.Now.TimeOfDay.ToString(),
                                                                                socket.RemoteIP,
                                                                                socket.RemotePort.ToString(),
                                                                                "Proxy checking error (bad reply) :"+System.Text.Encoding.ASCII.GetString(data)}));
                        proxy_s.hevt_error.Set();
                        proxy_s.close();
                    }
                }
                else// not proxy checking
                {
                    if (b_successfull_reply)
                    {
                        switch (this.current_scan_type)
                        {
                            case SCAN_TYPE.TCP:
                                this.add_listview_item(new ListViewItem(new string[]{this.uint_to_fixed_size_string(proxy_s.id)+SEPARATOR_ID_TIME+System.DateTime.Now.TimeOfDay.ToString(),
                                                                                        proxy_s.target_ip,
                                                                                        proxy_s.target_port.ToString(),
                                                                                        "Data from proxy "+socket.RemoteIP+":"+socket.RemotePort.ToString()+" :"+System.Text.Encoding.ASCII.GetString(data)}));
                                break;
                            case SCAN_TYPE.CGI:
                                this.add_listview_item(new ListViewItem(new string[]{this.uint_to_fixed_size_string(proxy_s.id)+SEPARATOR_ID_TIME+System.DateTime.Now.TimeOfDay.ToString(),
                                                                                        proxy_s.target_ip,
                                                                                        proxy_s.target_port.ToString(),
                                                                                        "Cgi query: "+System.Text.Encoding.ASCII.GetString(proxy_s.data)+" \tReply from proxy "+socket.RemoteIP+":"+socket.RemotePort.ToString()+" :"+System.Text.Encoding.ASCII.GetString(data)}));
                                break;
                        }
                    }
                    else// unsuccessful reply
                    {
                        this.add_listview_item(new ListViewItem(new string[]{new string(' ',this.nb_digits)+System.DateTime.Now.TimeOfDay.ToString(),
                                                                                socket.RemoteIP,
                                                                                socket.RemotePort.ToString(),
                                                                                "Proxy bad reply :"+System.Text.Encoding.ASCII.GetString(data)}));
                        proxy_s.hevt_error.Set();
                        proxy_s.close();
                    }
                }
            }
            else // not using proxy
            {
                switch (this.current_scan_type)
                {
                    case SCAN_TYPE.TCP:
                        this.add_listview_item(new ListViewItem(new string[]{this.uint_to_fixed_size_string(((tcp_scan)socket).id)+SEPARATOR_ID_TIME+System.DateTime.Now.TimeOfDay.ToString(),
                                                                                socket.RemoteIP,
                                                                                socket.RemotePort.ToString(),
                                                                                "Data:"+System.Text.Encoding.ASCII.GetString(e.buffer)}));
                        break;
                    case SCAN_TYPE.CGI:
                        if (socket is cgi_scan)
                            this.add_listview_item(new ListViewItem(new string[]{this.uint_to_fixed_size_string(((tcp_scan)socket).id)+SEPARATOR_ID_TIME+System.DateTime.Now.TimeOfDay.ToString(),
                                                                                    socket.RemoteIP,
                                                                                    socket.RemotePort.ToString(),
                                                                                    "Cgi query: "+((cgi_scan)socket).cgi+" \tReply:"+System.Text.Encoding.ASCII.GetString(e.buffer)}));
                        break;
                }
            }
        }
        private void tcp_connected(easy_socket.tcp.Socket_Data socket, System.EventArgs e)
        {
            if (socket is proxy_scan)
            {
                proxy_scan proxy_s=((proxy_scan)socket);
                byte[] data=proxy_s.get_data_for_proxy();
                if (data==null)// error getting data
                    return;
                proxy_s.send(data);
            }
            else
            {
                this.add_listview_item(new ListViewItem(new string[]{this.uint_to_fixed_size_string(((tcp_scan)socket).id)+SEPARATOR_ID_TIME+System.DateTime.Now.TimeOfDay.ToString(),
                                                                        socket.RemoteIP,
                                                                        socket.RemotePort.ToString(),
                                                                        "Connected"}));
                switch (this.current_scan_type)
                {
                    case SCAN_TYPE.TCP:
                        if (this.checkBox_tcp_wait_for_data.Checked)
                        {
                            socket.send(this.b_data);
                        }
                        else
                        {
                            // close socket
                            socket.close();
                            // continue scan
                            ((tcp_scan)(socket)).hevt_closed.Set();
                        }
                        break;
                    case SCAN_TYPE.CGI:
                        if (socket is cgi_scan)
                        {
                            ((cgi_scan)socket).send_cgi();
                        }
                        break;
                }
            }
        }
        #endregion
        #region icmp

        public void ev_socket_error(easy_socket.icmp.icmp sender, easy_socket.icmp.EventArgs_Exception e)
        {
            if (sender is easy_socket.icmp.icmp_server)
            {
                this.add_listview_item(new ListViewItem(new string[]{new string(' ',this.nb_digits)+System.DateTime.Now.TimeOfDay.ToString(),"","","Error in icmp server: "+e.exception.Message}));
                // restart icmp server
                this.icmp_srv.start();
            }
            else
            {
                if (this.check_icmp_identifier((icmp_scan)sender,true))
                    this.add_listview_item(new ListViewItem(new string[]{new string(' ',this.nb_digits)+System.DateTime.Now.TimeOfDay.ToString(),"","","icmp error: "+e.exception.Message}));
            }
        }
        private void ev_time_exceeded(easy_socket.icmp.icmp_time_exceeded_message sender, easy_socket.icmp.EventArgs_ipv4header_ReceiveData e)
        {
            if (!this.check_if_one_of_our_packets(sender.ih_and_original_dd))
                return;
            easy_socket.ip_header.ipv4_header initial_iph=new easy_socket.ip_header.ipv4_header();
            initial_iph.decode(sender.ih_and_original_dd);// may return the error error_datagram_not_complete but decode at least ip header
            this.add_listview_item(new ListViewItem(new string[]{new string(' ',this.nb_digits)+System.DateTime.Now.TimeOfDay.ToString(),initial_iph.DestinationAddress,"","Icmp time exceeded message from: "+e.ipv4header.SourceAddress}));
        }
        private void ev_source_quench(easy_socket.icmp.icmp_source_quench sender, easy_socket.icmp.EventArgs_ipv4header_ReceiveData e)
        {
            if (!this.check_if_one_of_our_packets(sender.ih_and_original_dd))
                return;
            easy_socket.ip_header.ipv4_header initial_iph=new easy_socket.ip_header.ipv4_header();
            initial_iph.decode(sender.ih_and_original_dd);// may return the error error_datagram_not_complete but decode at least ip header
            this.add_listview_item(new ListViewItem(new string[]{new string(' ',this.nb_digits)+System.DateTime.Now.TimeOfDay.ToString(),initial_iph.DestinationAddress,"","Icmp source quench message from: "+e.ipv4header.SourceAddress}));
        }
        private void ev_parameter_problem(easy_socket.icmp.icmp_parameter_problem sender, easy_socket.icmp.EventArgs_ipv4header_ReceiveData e)
        {
            if (!this.check_if_one_of_our_packets(sender.ih_and_original_dd))
                return;
            easy_socket.ip_header.ipv4_header initial_iph=new easy_socket.ip_header.ipv4_header();
            initial_iph.decode(sender.ih_and_original_dd);// may return the error error_datagram_not_complete but decode at least ip header
            this.add_listview_item(new ListViewItem(new string[]{new string(' ',this.nb_digits)+System.DateTime.Now.TimeOfDay.ToString(),initial_iph.DestinationAddress,"","Icmp parameter problem message from: "+e.ipv4header.SourceAddress}));
        }
        private void ev_destination_unreachable(easy_socket.icmp.icmp_destination_unreachable sender, easy_socket.icmp.EventArgs_ipv4header_ReceiveData e)
        {
            if (!this.check_if_one_of_our_packets(sender.ih_and_original_dd))
                return;
            easy_socket.ip_header.ipv4_header initial_iph=new easy_socket.ip_header.ipv4_header();
            initial_iph.decode(sender.ih_and_original_dd);// may return the error error_datagram_not_complete but decode at least ip header
            this.add_listview_item(new ListViewItem(new string[]{new string(' ',this.nb_digits)+System.DateTime.Now.TimeOfDay.ToString(),initial_iph.DestinationAddress,"","Icmp destination unreachable from "+e.ipv4header.SourceAddress}));
        }
        private void ev_echo_reply(easy_socket.icmp.icmp_echo_reply sender, easy_socket.icmp.EventArgs_ipv4header_ReceiveData e)
        {
            int old_time=(int)((sender.identifier<<16)+sender.sequence_number);
            if (!this.check_icmp_identifier(new icmp_scan(this,e.ipv4header.SourceAddress,old_time),false))
                return;
            // comput time needed
            int time=System.Environment.TickCount;

            time-=old_time;
            if (time==0)
                time=1;
            this.add_listview_item(new ListViewItem(new string[]{new string(' ',this.nb_digits)+System.DateTime.Now.TimeOfDay.ToString(),e.ipv4header.SourceAddress,"","reply in time<"+time.ToString()+" ms"}));
        }

        #region check id of echo replies
        
        public void add_icmp_identifier(icmp_scan icmp_scan_info)
        {
            // solve synchronization troubles
            this.evtal_icmp_identifiersUnLocked.WaitOne();
            this.al_icmp_identifiers.Add(icmp_scan_info);
            this.evtal_icmp_identifiersUnLocked.Set();
        }
        public void remove_icmp_scan_info(icmp_scan icmp_scan_info)
        {
            // solve synchronization troubles
            this.evtal_icmp_identifiersUnLocked.WaitOne();
            this.al_icmp_identifiers.Remove(icmp_scan_info);
            this.evtal_icmp_identifiersUnLocked.Set();
        }
        private bool check_icmp_identifier(icmp_scan icmp_scan_info,bool b_error_msg)
        {
            this.evtal_icmp_identifiersUnLocked.WaitOne();
            for (int cpt=0;cpt<this.al_icmp_identifiers.Count;cpt++)
            {
                if (icmp_scan_info.Equals((icmp_scan)this.al_icmp_identifiers[cpt]))
                {
                    if (b_error_msg)
                        ((icmp_scan)this.al_icmp_identifiers[cpt]).hevt_error.Set();
                    else
                        ((icmp_scan)this.al_icmp_identifiers[cpt]).hevt_echo_reply.Set();
                    this.evtal_icmp_identifiersUnLocked.Set();
                    return true;
                }
            }
            this.evtal_icmp_identifiersUnLocked.Set();
            return false;
        }
        private bool check_if_one_of_our_packets(byte[] data)
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
            return this.check_icmp_identifier(new icmp_scan(this,iph.SourceAddress,id),true);
        }
        #endregion


        #endregion

    }

    #region udp_scan
    public class udp_scan:easy_socket.udp.Socket_Server
    {
        public string remote_ip;
        public ushort remote_port;
        private int i_timeout_in_ms;
        private System.Threading.WaitHandle[] wait_handles;
        public System.Threading.AutoResetEvent hevt_error;
        public System.Threading.AutoResetEvent hevt_unreachable;
        private FormScan formscan;
        public uint id;

        public udp_scan(FormScan formscan)
        {
            this.remote_ip="Not Defined";
            this.remote_port=0;
            this.formscan=formscan;
            this.i_timeout_in_ms=3000;
            this.hevt_error=new System.Threading.AutoResetEvent(false);
            this.hevt_unreachable=new System.Threading.AutoResetEvent(false);
            this.wait_handles=new System.Threading.WaitHandle[3];
            this.wait_handles[0]=this.formscan.evtStop;
            this.wait_handles[1]=this.hevt_error;
            this.wait_handles[2]=this.hevt_unreachable;
            
            this.id=0;
        }
        public void set_time_out(int time_in_ms)
        {
            this.i_timeout_in_ms=time_in_ms;
        }
        private void thread_start()
        {
            int ret_wait=System.Threading.WaitHandle.WaitAny(this.wait_handles,this.i_timeout_in_ms,true);
            // case time out add timeout in listview
            if (ret_wait==System.Threading.WaitHandle.WaitTimeout)
                this.formscan.add_listview_item(new ListViewItem(new string[]{this.formscan.uint_to_fixed_size_string(this.id)+FormScan.SEPARATOR_ID_TIME+System.DateTime.Now.TimeOfDay.ToString(),this.remote_ip,this.remote_port.ToString(),"Timeout"}));
            this.formscan.remove_events(this);
            this.stop();
            // signal end of scan for this object
            // increase number of signaled event by one
            Semaphore.ReleaseSemaphore(this.formscan.h_semaphore,1);
        }
        public void send(byte[] buffer,string ip,int port,bool b_do_icmp_scan_before)
        {
            #region ping server before
            if (b_do_icmp_scan_before)
            {
                bool b_ping_success=false;
                // if server is being check wait checking is finished
                if (this.formscan.is_server_being_ping(ip))
                {
                    if (this.formscan.evtStop.WaitOne(this.formscan.i_time_out,false))
                        return;
                    while(!this.formscan.is_server_pinged(ip,ref b_ping_success))
                    {
                        if (this.formscan.evtStop.WaitOne(100,false))
                            return;
                    }
                }
                // if check is not done
                if (!this.formscan.is_server_pinged(ip,ref b_ping_success))
                {
                    System.Threading.WaitHandle[] wait_icmp_handles=new System.Threading.WaitHandle[3];

                    int t_ini=System.Environment.TickCount;
                    icmp_scan icmp_s=new icmp_scan(this.formscan,ip,t_ini);
                    wait_icmp_handles[0]=this.formscan.evtStop;
                    wait_icmp_handles[1]=icmp_s.hevt_error;
                    wait_icmp_handles[2]=icmp_s.hevt_echo_reply;
                    icmp_s.event_Socket_Error+=new easy_socket.icmp.Socket_Error_EventHandler(this.formscan.ev_socket_error);
                    icmp_s.data=this.formscan.b_data;
                    this.formscan.add_icmp_identifier(icmp_s);
                    icmp_s.identifier=(UInt16)(t_ini>>16);
                    icmp_s.sequence_number=(UInt16)(t_ini&0xffffffff);
                    icmp_s.set_time_out(this.formscan.i_time_out,false);
                    icmp_s.send(ip);
                    // wait for ping reply or timeout
                    int ret_wait=System.Threading.WaitHandle.WaitAny(wait_icmp_handles,this.formscan.i_time_out,true);
                    // case time out add timeout in listview
                    if (ret_wait==2)// echo_reply
                        b_ping_success=true;

                    this.formscan.remove_server_being_ping(ip);
                    this.formscan.add_pinged_server(ip,b_ping_success);
                }
                if (!b_ping_success)
                {
                    this.formscan.add_listview_item((new ListViewItem(new string[]{this.formscan.uint_to_fixed_size_string(this.id)+FormScan.SEPARATOR_ID_TIME+System.DateTime.Now.TimeOfDay.ToString(),ip,port.ToString(),"Not scan because no ping reply"})));
                    // do the next scan
                    Semaphore.ReleaseSemaphore(this.formscan.h_semaphore,1);
                    return;// don't scan this ip/port
                }
            }
            #endregion
            System.Threading.ThreadStart myThreadStart = new System.Threading.ThreadStart(thread_start);
            System.Threading.Thread myThread =new System.Threading.Thread(myThreadStart);
            myThread.Start();

            this.send(buffer,ip,port);
        }
    }
    #endregion
    #region tcp_scan
    public class tcp_scan:easy_socket.tcp.Socket_Data
    {
        protected int i_timeout_in_ms;
        protected System.Threading.WaitHandle[] wait_handles;
        public System.Threading.AutoResetEvent hevt_error;
        public System.Threading.AutoResetEvent hevt_closed_by_remote_side;
        public System.Threading.AutoResetEvent hevt_closed;
        public FormScan formscan;
        public uint id;
        private bool b_use_proxy;
        private bool b_check_proxy;
        private string ip_for_checking_proxy;
        private ushort port_for_checking_proxy;

        public tcp_scan(FormScan formscan)
        {
            this.common_constructor(formscan,false,false,"",0);
        }

        public tcp_scan(FormScan formscan,bool b_use_proxy,bool b_check_proxy,string ip_for_checking,ushort port_for_checking)
        {
            this.common_constructor(formscan,b_use_proxy,b_check_proxy,ip_for_checking,port_for_checking);
        }

        private void common_constructor(FormScan formscan,bool b_use_proxy,bool b_check_proxy,string ip_for_checking,ushort port_for_checking)
        {
            this.formscan=formscan;
            this.b_use_proxy=b_use_proxy;
            this.b_check_proxy=b_check_proxy;
            this.ip_for_checking_proxy=ip_for_checking;
            this.port_for_checking_proxy=port_for_checking;
            this.i_timeout_in_ms=3000;
            this.hevt_closed_by_remote_side=new System.Threading.AutoResetEvent(false);
            this.hevt_closed=new System.Threading.AutoResetEvent(false);
            this.hevt_error=new System.Threading.AutoResetEvent(false);
            this.wait_handles=new System.Threading.WaitHandle[4];
            this.wait_handles[0]=this.formscan.evtStop;
            this.wait_handles[1]=this.hevt_closed_by_remote_side;
            this.wait_handles[2]=this.hevt_closed;
            this.wait_handles[3]=this.hevt_error;
            this.id=0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="b_send_icmp_first">if true send icmp before do the scan (Icmp server must be started before)</param>
        public void connect(string ip, ushort port,bool b_send_icmp_first)
        {
            #region ping server before
            if (b_send_icmp_first)
            {
                bool b_ping_success=false;
                // if server is being check wait checking is finished
                if (this.formscan.is_server_being_ping(ip))
                {
                    if (this.formscan.evtStop.WaitOne(this.formscan.i_time_out,false))
                        return;
                    while(!this.formscan.is_server_pinged(ip,ref b_ping_success))
                    {
                        if (this.formscan.evtStop.WaitOne(100,false))
                            return;
                    }
                }
                // if check is not done
                if (!this.formscan.is_server_pinged(ip,ref b_ping_success))
                {
                    System.Threading.WaitHandle[] wait_icmp_handles=new System.Threading.WaitHandle[3];

                    int t_ini=System.Environment.TickCount;
                    icmp_scan icmp_s=new icmp_scan(this.formscan,ip,t_ini);
                    wait_icmp_handles[0]=this.formscan.evtStop;
                    wait_icmp_handles[1]=icmp_s.hevt_error;
                    wait_icmp_handles[2]=icmp_s.hevt_echo_reply;
                    icmp_s.event_Socket_Error+=new easy_socket.icmp.Socket_Error_EventHandler(this.formscan.ev_socket_error);
                    icmp_s.data=this.formscan.b_data;
                    this.formscan.add_icmp_identifier(icmp_s);
                    icmp_s.identifier=(UInt16)(t_ini>>16);
                    icmp_s.sequence_number=(UInt16)(t_ini&0xffffffff);
                    icmp_s.set_time_out(this.formscan.i_time_out,false);
                    icmp_s.send(ip);
                    // wait for ping reply or timeout
                    int ret_wait=System.Threading.WaitHandle.WaitAny(wait_icmp_handles,this.formscan.i_time_out,true);
                    // case time out add timeout in listview
                    if (ret_wait==2)// echo_reply
                        b_ping_success=true;

                    this.formscan.remove_server_being_ping(ip);
                    this.formscan.add_pinged_server(ip,b_ping_success);
                }
                if (!b_ping_success)
                {
                    this.formscan.add_listview_item((new ListViewItem(new string[]{this.formscan.uint_to_fixed_size_string(this.id)+FormScan.SEPARATOR_ID_TIME+System.DateTime.Now.TimeOfDay.ToString(),ip,port.ToString(),"Not scan because no ping reply"})));
                    // do the next scan
                    Semaphore.ReleaseSemaphore(this.formscan.h_semaphore,1);
                    return;// don't scan this ip/port
                }
            }
            #endregion

            #region proxy
            if (this.b_use_proxy)
            {
                proxy_information proxy;
                proxy_scan proxy_s;
                if (this.b_check_proxy)
                #region proxy check proxy before
                {
                    // get random proxy working proxy 
                    // while error checking proxy and it remains proxy in proxy list
                    while((proxy=this.formscan.get_random_proxy())!=null)
                    {
                        // if proxy has already been checked and is working
                        if (proxy.b_checked)
                            break;// keep it
                        bool b_check_success=false;
                        // if server is being check wait checking is finished
                        if (this.formscan.is_proxy_being_checked(proxy))
                        {
                            if(this.formscan.evtStop.WaitOne(this.formscan.i_time_out,false))
                                return;

                            while(this.formscan.is_proxy_being_checked(proxy))// add proxy to being checked list if it is not the case
                            {
                                if (this.formscan.evtStop.WaitOne(100,false))
                                    return;
                            }
                            // if proxy is working
                            if (proxy.b_checked)
                                break;// keep it
                            // else proxy has been removed from proxy list
                            // get new proxy
                            continue;
                        }
                        // else check is not done --> do it
                        proxy_s=new proxy_scan(this.formscan,proxy,this.ip_for_checking_proxy,this.port_for_checking_proxy,this.formscan.get_proxy_type());
                        b_check_success=proxy_s.check();
                        if (b_check_success)
                        {
                            this.formscan.add_checked_proxy(proxy);
                            b_check_success=true;
                        }
                        else
                        {
                            // remove proxy from proxy list
                            this.formscan.remove_proxy(proxy);
                        }
                        this.formscan.remove_proxy_being_checked(proxy);
                        // if proxy is working
                        if (b_check_success)
                            break;// keep it
                        // else 
                        // bad proxy is already signal (result has been written in listview at this state)
                        // get new proxy with while
                    }
                }
                #endregion
                else // don't check proxy --> get a random one
                    proxy=this.formscan.get_random_proxy();
                // if no more available proxy
                if (proxy==null)
                {
                    // stop scaning
                    this.formscan.no_more_proxy();
                    return;
                }
                // else make scan with current proxy

                proxy_s=new proxy_scan(this.formscan,proxy,ip,port,this.formscan.get_proxy_type());
                proxy_s.id=this.id;
                proxy_s.data=this.get_data();
                // add socket callback handler
                this.formscan.add_events(proxy_s);
                proxy_s.set_time_out(this.formscan.i_time_out);
                proxy_s.connect(proxy.ip,proxy.port);
                return;
            }
            #endregion

            // start time out thread only now and only if no proxy
            System.Threading.ThreadStart myThreadStart = new System.Threading.ThreadStart(thread_start);
            System.Threading.Thread myThread =new System.Threading.Thread(myThreadStart);
            myThread.Start();

            this.connect(ip,port);
        }
        virtual protected byte[] get_data()
        {
            return this.formscan.get_data_to_send();
        }
        virtual public void set_time_out(int time_in_ms)
        {
            // don't start time out now because of ping scan and proxy checking
            this.i_timeout_in_ms=time_in_ms;
        }
        virtual protected void thread_start()
        {
            int ret_wait=System.Threading.WaitHandle.WaitAny(this.wait_handles,this.i_timeout_in_ms,true);
            // case time out add timeout in listview
            if (ret_wait==System.Threading.WaitHandle.WaitTimeout)
                this.formscan.add_listview_item(new ListViewItem(new string[]{this.formscan.uint_to_fixed_size_string(this.id)+FormScan.SEPARATOR_ID_TIME+System.DateTime.Now.TimeOfDay.ToString(),this.RemoteIP,this.RemotePort.ToString(),"Timeout"}));
            this.formscan.remove_events(this);
            this.close();
            // signal end of scan for this object
            // increase number of signaled event by one
            Semaphore.ReleaseSemaphore(this.formscan.h_semaphore,1);
        }
    }
    #endregion
    #region cgi_scan
    public class cgi_scan:tcp_scan
    {
        public string cgi;
        public cgi_scan(FormScan formscan):base(formscan)
        {
            this.cgi="";
        }
        public cgi_scan(FormScan formscan,bool b_use_proxy,bool b_check_proxy,string ip_for_checking,ushort port_for_checking):base(formscan,b_use_proxy,b_check_proxy,ip_for_checking,port_for_checking)
        {
            this.cgi="";
        }
        public void send_cgi()
        {
            this.send("Get "+this.cgi+" HTTP/1.0\r\n\r\n");
        }
        protected override byte[] get_data()
        {
            return System.Text.Encoding.ASCII.GetBytes("GET "+this.cgi+" HTTP/1.0\r\n\r\n");
        }
    }
    #endregion
    #region proxy scan
    public class proxy_scan:tcp_scan
    {
        public FormScan.PROXY_TYPE type;
        public System.Threading.AutoResetEvent hevt_proxy_checking_success;
        public proxy_information proxy;
        public string target_ip;
        public ushort target_port;
        public bool b_checking_proxy;
        public byte[] data;
        private bool b_first_packet;
        private bool b_valid_connection;
        private bool b_timeout_set;
        public bool b_all_socks5_headers_rcv;
        private bool b_socks5_command_accepted;
        public proxy_scan(FormScan formscan,proxy_information proxy,string target_ip,ushort target_port,FormScan.PROXY_TYPE type):base(formscan)
        {
            this.b_timeout_set=false;
            this.target_ip=target_ip;
            this.target_port=target_port;
            this.data=System.Text.Encoding.ASCII.GetBytes("GET / HTTP/1.0\r\n\r\n");
            this.proxy=proxy;
            this.type=type;
            this.hevt_proxy_checking_success=new System.Threading.AutoResetEvent(false);
            this.b_checking_proxy=false;
            this.b_first_packet=true;
            this.b_valid_connection=false;
            this.b_all_socks5_headers_rcv=false;
            this.b_socks5_command_accepted=false;
        }

        /// <summary>
        /// return data to be send to the proxy
        /// </summary>
        /// <returns>data on success, null if an error has occured</returns>
        public byte[] get_data_for_proxy()
        {
            byte[] ret;
            byte[] tmp;
            string str;
            int data_len=0;
            System.Net.IPHostEntry iphe;
            System.Net.IPAddress ipaddr;
            // if there's data
            if (this.data!=null)
                // get data length
                data_len=this.data.Length;
            switch (this.type)
            {
                case FormScan.PROXY_TYPE.HTTP_CONNECT:
                    // connect lines
                    str="CONNECT "+this.target_ip+":"+this.target_port+" HTTP/1.0\r\n"+"HOST:"+this.target_ip+":"+this.target_port+"\r\n\r\n";
                    // convert to byte[]
                    return System.Text.Encoding.ASCII.GetBytes(str);
                case FormScan.PROXY_TYPE.HTTP_GET:
                    bool b_need_get;
                    bool b_need_http;
                    bool b_need_2crlf;
                    bool b_full_header_only;
                    string str_get="GET ";
                    string str_http=" HTTP/1.0\r\n";
                    string str_host="HOST:"+this.target_ip+":"+this.target_port+"\r\n";
                    string str_request;
                    string str_head="";
                    int pos_http;
                    int pos_end_of_first_line;
                    int pos_end_of_header;
                    
                    str=System.Text.Encoding.ASCII.GetString(this.data, 0,this.data.Length).ToUpper();

                    // look for 'GET ' HTTP/ and "\r\n\r\n"
                    b_need_get=!str.StartsWith("GET ");
                    pos_http=str.IndexOf(" HTTP/");
                    // get pos of end of header
                    pos_end_of_header=str.IndexOf("\r\n\r\n");
                    b_full_header_only=((pos_end_of_header+4)==str.Length);
                    // get pos of first end of line
                    pos_end_of_first_line=str.IndexOf("\r\n");
                    b_need_http=((pos_http<0)||((pos_http>pos_end_of_first_line)&&(pos_end_of_first_line>0)));//http is present before \r\n\r\n
                    b_need_2crlf=(pos_end_of_header<0);
                    //as header is in ascii format
                    if (b_need_get)
                        str_head+=str_get;
                    if (b_need_2crlf)
                        pos_end_of_header=this.data.Length;
                    else
                    {
                        // there's header and data
                        // split on "\r\n\r\n to keep header only
                        str=str.Substring(0,pos_end_of_header);
                        // get pos of first end of line
                        pos_end_of_first_line=str.IndexOf("\r\n");
                    }
                    
                    if (pos_end_of_first_line<0) // only 1 line
                    {
                        str_request=System.Text.Encoding.ASCII.GetString(this.data, 0,pos_end_of_header);
                        if (str_request.Trim()=="")
                            str_request="/";
                        str_head+=str_request;
                        if (b_need_http)
                            str_head+=str_http;
                        else
                            str_head+="\r\n";
                        str_head+=str_host;
                    }
                    else
                    {
                        str_request=System.Text.Encoding.ASCII.GetString(this.data, 0,pos_end_of_first_line);
                        if (str_request.Trim()=="")
                            str_request="/";
                        str_head+=str_request;
                        if (b_need_http)
                            str_head+=str_http;
                        else
                        {
                            if (!str_head.EndsWith("\r\n"))
                                str_head+="\r\n";
                        }
                        str_head+=str_host;
                        str_head+=System.Text.Encoding.ASCII.GetString(this.data, pos_end_of_first_line+2,pos_end_of_header);
                    }
                    // ensure header ends with 2 and only 2 crlf
                    if (!str_head.EndsWith("\r\n"))
                        str_head+="\r\n";
                    str_head+="\r\n";
                    tmp=System.Text.Encoding.ASCII.GetBytes(str_head);
                    ret =new byte[tmp.Length+this.data.Length-pos_end_of_header];
                    System.Array.Copy(tmp,0,ret,0,tmp.Length);
                    if ((!b_need_2crlf)&&(!b_full_header_only))// if there's data behind \r\n\r\n
                        System.Array.Copy(this.data,pos_end_of_header+4,ret,tmp.Length,this.data.Length-pos_end_of_header);
                    return ret;

                    //break;
                case FormScan.PROXY_TYPE.SOCKS4:
                    ret=new byte[9];
                    // version number 4
                    ret[0]=4;
                    // command code 1 (connect)
                    ret[1]=1;
                    // dest port
                    System.Array.Copy(System.BitConverter.GetBytes(easy_socket.network_convert.switch_ushort(this.target_port)),0,ret,2,2);
                    // dest ip
                    try
                    {
                        // don't resolve if ip is like xxx.yyy.uuu.vvv
                        ipaddr=System.Net.IPAddress.Parse(this.target_ip);
                    }
                    catch
                    {
                        // else resolve
                        try
                        {
                            iphe=System.Net.Dns.Resolve(this.target_ip);
                        }
                        catch(Exception e)
                        {
                            if (this.b_checking_proxy)
                            {
                                this.formscan.add_listview_item(new ListViewItem(new string[]{this.formscan.uint_to_fixed_size_string(this.id)+FormScan.SEPARATOR_ID_TIME+System.DateTime.Now.TimeOfDay.ToString(),this.target_ip,this.target_port.ToString(),e.Message}));
                            }
                            else
                            {
                                this.formscan.add_listview_item(new ListViewItem(new string[]{new string(' ',this.formscan.nb_digits)+System.DateTime.Now.TimeOfDay.ToString(),this.target_ip,this.target_port.ToString(),e.Message}));
                            }
                            this.hevt_error.Set();
                            return null;
                        }
                        ipaddr=iphe.AddressList[0];
                    }
                    System.Array.Copy(System.BitConverter.GetBytes(ipaddr.Address),0,ret,4,4);
                    // user id (none)
                    // null char
                    ret[8]=0;
                    return ret;
                case FormScan.PROXY_TYPE.SOCKS5:
                    ret=new byte[3];
                    // version number 5
                    ret[0]=5;
                    ret[1]=1;// number of methods
                    ret[2]=0;// ask for NO AUTHENTICATION REQUIRED 

                    return ret;
            }
            // default
            this.formscan.add_listview_item(new ListViewItem(new string[]{this.formscan.uint_to_fixed_size_string(this.id)+FormScan.SEPARATOR_ID_TIME+System.DateTime.Now.TimeOfDay.ToString(),this.target_ip,this.target_port.ToString(),"Unknown proxy type"}));
            this.hevt_error.Set();
            return null;
        }
        /// <summary>
        /// return true if proxy is checked
        /// </summary>
        /// <returns></returns>
        public bool check()
        {
            System.Threading.WaitHandle[] wait_proxy_handles=new System.Threading.WaitHandle[5];
            wait_proxy_handles[0]=this.formscan.evtStop;
            wait_proxy_handles[1]=this.hevt_proxy_checking_success;
            wait_proxy_handles[2]=this.hevt_error;
            wait_proxy_handles[3]=this.hevt_closed_by_remote_side;
            wait_proxy_handles[4]=this.hevt_closed;
            this.b_checking_proxy=true;
            this.formscan.add_events(this);
            this.target_ip=this.target_ip;
            this.target_port=this.target_port;
            this.connect(proxy.ip,proxy.port);
            // wait for reply or timeout
            int ret_wait=System.Threading.WaitHandle.WaitAny(wait_proxy_handles,this.formscan.i_time_out,true);
            this.formscan.remove_events(this);
            this.close();
            this.b_checking_proxy=false;
            // case time out add timeout in listview
            if (ret_wait==1)// successfull reply
            {
                return true;
            }
            if (ret_wait==System.Threading.WaitHandle.WaitTimeout)
                this.formscan.add_listview_item((new ListViewItem(new string[]{new string(' ',this.formscan.nb_digits)+System.DateTime.Now.TimeOfDay.ToString(),this.proxy.ip,this.proxy.port.ToString(),"Proxy checking error : Timeout"})));
            return false;
        }

        public override void set_time_out(int time_in_ms)
        {
            if (this.b_timeout_set)
                return;
            this.i_timeout_in_ms=time_in_ms;
            System.Threading.ThreadStart myThreadStart = new System.Threading.ThreadStart(thread_start);
            System.Threading.Thread myThread =new System.Threading.Thread(myThreadStart);
            myThread.Start();
        }

        protected override void thread_start()
        {
            int ret_wait=System.Threading.WaitHandle.WaitAny(this.wait_handles,this.i_timeout_in_ms,true);
            // case time out add timeout in listview
            if (ret_wait==System.Threading.WaitHandle.WaitTimeout)
                this.formscan.add_listview_item(new ListViewItem(new string[]{this.formscan.uint_to_fixed_size_string(this.id)+FormScan.SEPARATOR_ID_TIME+System.DateTime.Now.TimeOfDay.ToString(),this.target_ip,this.target_port.ToString(),"Timeout using proxy "+this.RemoteIP+":"+this.RemotePort.ToString()}));
            this.formscan.remove_events(this);
            this.close();
            if (!this.b_checking_proxy)
                // signal end of scan for this object
                // increase number of signaled event by one
                Semaphore.ReleaseSemaphore(this.formscan.h_semaphore,1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="incoming_data">recieved data can be modified in case of proxy socks to remove binary fields</param>
        /// <returns></returns>
        public bool is_good_reply(ref byte[] incoming_data)
        {
            if (this.b_first_packet)// check only on first packet
            {
                this.b_first_packet=false;
                this.b_valid_connection=false;
                switch(this.type)
                {
                    case FormScan.PROXY_TYPE.HTTP_GET:
                    case FormScan.PROXY_TYPE.HTTP_CONNECT:
                        // check http reply (number after space behind HTTP/x.x in header)
                        string str=System.Text.Encoding.ASCII.GetString(incoming_data);
                        bool b_reply_code_found=false;
                        string str_reply_code;
                        int reply_code=0;
                        int pos;
                        int pos2;

                        pos=str.ToUpper().IndexOf("HTTP/");
                        if (pos<0)
                            return false;
                        pos2=str.IndexOf("\r\n\r\n");
                        if ((pos2>-1)&&(pos>pos2))
                            return false;
                        // get first space after HTTP/
                        pos=str.IndexOf(" ",pos+5);
                        // get space after reply code number
                        pos2=str.IndexOf(" ",pos+1);
                        while(pos2>0)
                        {
                            str_reply_code=str.Substring(pos,pos2-pos).Trim();
                            if (str_reply_code!="")
                            {
                                try
                                {
                                    reply_code=System.Convert.ToInt32(str_reply_code);
                                    b_reply_code_found=true;
                                    break;
                                }
                                catch
                                {
                                    return false;
                                }
                            }
                            pos2=str.IndexOf(" ",pos+1);
                        }
                        if (!b_reply_code_found)
                            return false;
                        if (reply_code>=400)
                            return false;
                        // don't check content Content-Location because proxy who lies replying 200ok make a fake good Content-Location
                        this.b_valid_connection=true;
                        // connect specific
                        if (this.type==FormScan.PROXY_TYPE.HTTP_CONNECT)
                        {
                            // send data
                            this.send(this.data);
                        }
                        break;
                    case FormScan.PROXY_TYPE.SOCKS4:
                        byte[] tmp;
                        if (incoming_data.Length<8)
                            return false;
                        // vn should = 0
                        if (incoming_data[0]!=0)
                            return false;

                        // code should be 90
                        if (incoming_data[1]!=90)
                        {
                            string error_code="proxy error code :"+incoming_data[1]+". ";
                            tmp=new byte[error_code.Length+incoming_data.Length-8];
                            System.Array.Copy(System.Text.Encoding.ASCII.GetBytes(error_code),0,tmp,0,error_code.Length);
                            if (incoming_data.Length>8)
                                System.Array.Copy(incoming_data,9,tmp,error_code.Length,incoming_data.Length-8);
                            incoming_data=tmp;
                            return false;
                        }
                        // dst port
                        // dst ip
                        this.b_valid_connection=true;
                        // request accepted
                        string request_accepted="Request accepted. ";
                        tmp=new byte[request_accepted.Length+incoming_data.Length-8];
                        System.Array.Copy(System.Text.Encoding.ASCII.GetBytes(request_accepted),0,tmp,0,request_accepted.Length);
                        if (incoming_data.Length>8)
                            System.Array.Copy(incoming_data,9,tmp,request_accepted.Length,incoming_data.Length-8);
                        incoming_data=tmp;

                        if (!this.b_checking_proxy)
                            // send data
                            this.send(this.data);
                        break;
                    case FormScan.PROXY_TYPE.SOCKS5:
                        byte[] ret;
                        if (!this.b_socks5_command_accepted)
                        {
                            if (incoming_data.Length<2)
                                return false;
                            // vn should = 5
                            if (incoming_data[0]!=5)
                                return false;
                            //X'00' NO AUTHENTICATION REQUIRED 
                            //X'01' GSSAPI 
                            //X'02' USERNAME/PASSWORD 
                            //X'03' to X'7F' IANA ASSIGNED 
                            //X'80' to X'FE' RESERVED FOR PRIVATE METHODS 
                            //X'FF' NO ACCEPTABLE METHODS 
                            if (incoming_data[1]!=0)
                            {
                                incoming_data=System.Text.Encoding.ASCII.GetBytes(easy_socket.hexa_convert.byte_to_hexa(incoming_data));
                                return false;
                            }
                            incoming_data=System.Text.Encoding.ASCII.GetBytes("No Authentication accepted.");
                            ret=new byte[10];
                            ret[0]=5;// version
                            ret[1]=1;// connect cmd
                            ret[2]=0;// reserved
                            ret[3]=1;// adress type=ipv4
                            System.Net.IPHostEntry iphe;
                            System.Net.IPAddress ipaddr;
                            try
                            {
                                // don't resolve if ip is like xxx.yyy.uuu.vvv
                                ipaddr=System.Net.IPAddress.Parse(this.target_ip);
                            }
                            catch
                            {
                                // else resolve
                                try
                                {
                                    iphe=System.Net.Dns.Resolve(this.target_ip);
                                }
                                catch(Exception e)
                                {
                                    if (this.b_checking_proxy)
                                    {
                                        this.formscan.add_listview_item(new ListViewItem(new string[]{new string(' ',this.formscan.nb_digits)+System.DateTime.Now.TimeOfDay.ToString(),this.target_ip,this.target_port.ToString(),e.Message}));
                                    }
                                    else
                                    {
                                        this.formscan.add_listview_item(new ListViewItem(new string[]{this.formscan.uint_to_fixed_size_string(this.id)+FormScan.SEPARATOR_ID_TIME+System.DateTime.Now.TimeOfDay.ToString(),this.target_ip,this.target_port.ToString(),e.Message}));                                        
                                    }
                                    return false;
                                }
                                ipaddr=iphe.AddressList[0];
                            }
                            // dest ip
                            System.Array.Copy(System.BitConverter.GetBytes(ipaddr.Address),0,ret,4,4);
                            // dest port
                            System.Array.Copy(System.BitConverter.GetBytes(easy_socket.network_convert.switch_ushort(this.target_port)),0,ret,8,2);// warning 8 only because ipv4
                            
                            this.send(ret);
                            this.b_socks5_command_accepted=true;
                            this.b_first_packet=true;
                            return true;
                        }
                        else
                        {
                            if (incoming_data.Length<10)
                                return false;
                            // vn should = 5
                            if (incoming_data[0]!=5)
                                return false;
                            switch (incoming_data[1])
                            {
                                case 0://successfull
                                    incoming_data=System.Text.Encoding.ASCII.GetBytes("Successfully Connected to remote host");
                                    break;
                                case 1:
                                    incoming_data=System.Text.Encoding.ASCII.GetBytes("General SOCKS server failure.");
                                    return false;
                                case 2:
                                    incoming_data=System.Text.Encoding.ASCII.GetBytes("Connection not allowed by ruleset.");
                                    return false;
                                case 3:
                                    incoming_data=System.Text.Encoding.ASCII.GetBytes("Network unreachable.");
                                    return false;
                                case 4:
                                    incoming_data=System.Text.Encoding.ASCII.GetBytes("Host unreachable.");
                                    return false;
                                case 5:
                                    incoming_data=System.Text.Encoding.ASCII.GetBytes("Connection refused.");
                                    return false;
                                case 6:
                                    incoming_data=System.Text.Encoding.ASCII.GetBytes("TTL expired.");
                                    return false;
                                case 7:
                                    incoming_data=System.Text.Encoding.ASCII.GetBytes("Command not supported.");
                                    return false;
                                case 8:
                                    incoming_data=System.Text.Encoding.ASCII.GetBytes("Address type not supported.");
                                    return false;
                                default:
                                    incoming_data=System.Text.Encoding.ASCII.GetBytes("Unknown error code.");
                                    return false;
                            }
                            this.b_all_socks5_headers_rcv=true;
                            this.b_valid_connection=true;
                            if (!this.b_checking_proxy)
                                // send data
                                this.send(this.data);
                        }
                    break;
                }
            }
            return this.b_valid_connection;
        }
    }
    #endregion
    #region icmp_scan
    public class icmp_scan:easy_socket.icmp.icmp_echo
    {
        private int i_timeout_in_ms;
        private bool b_timeout_set;
        private bool b_increase_semaphore;
        private System.Threading.WaitHandle[] wait_handles;
        public System.Threading.ManualResetEvent hevt_error;
        public System.Threading.ManualResetEvent hevt_echo_reply;
        private FormScan formscan;

        public int id;
        public string ip;

        public icmp_scan(FormScan formscan,string ip, int id)
        {
            this.ip=ip;
            this.id=id;

            this.formscan=formscan;
            this.i_timeout_in_ms=3000;
            this.b_timeout_set=false;
            this.hevt_error=new System.Threading.ManualResetEvent(false);
            this.hevt_echo_reply=new System.Threading.ManualResetEvent(false);
            this.wait_handles=new System.Threading.WaitHandle[3];

            this.wait_handles[0]=this.formscan.evtStop;
            this.wait_handles[1]=this.hevt_error;
            this.wait_handles[2]=this.hevt_echo_reply;

            // assume ip is like xxx.yyy.uuu.vvv (for identifer and host checking)
            try
            {
                // if ip is like xxx.yyy.uuu.vvv
                System.Net.IPAddress.Parse(this.ip);
            }
            catch
            {
                // else resolve
                try
                {
                    this.ip=System.Net.Dns.Resolve(this.ip).AddressList[0].ToString();
                }
                catch
                {
                    // an error will be send when packt will be sent (see ip_header_client.send_without_forging)
                }
            }

        }
        public bool Equals(icmp_scan obj)
        {
            return ((obj.id==this.id)&&(obj.ip==this.ip));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time_in_ms"></param>
        /// <param name="b_increase_semaphore">true to go to next scan step</param>
        public void set_time_out(int time_in_ms,bool b_increase_semaphore)
        {
            if (this.b_timeout_set)
                return;
            this.i_timeout_in_ms=time_in_ms;
            this.b_increase_semaphore=b_increase_semaphore;
            System.Threading.ThreadStart myThreadStart = new System.Threading.ThreadStart(thread_start);
            System.Threading.Thread myThread =new System.Threading.Thread(myThreadStart);
            myThread.Start();
        }
        public void set_time_out(int time_in_ms)
        {
            this.set_time_out(time_in_ms,true);
        }
        private void thread_start()
        {
            int ret_wait=System.Threading.WaitHandle.WaitAny(this.wait_handles,this.i_timeout_in_ms,true);
            // case time out add timeout in listview
            if (ret_wait==System.Threading.WaitHandle.WaitTimeout)
                this.formscan.add_listview_item(new ListViewItem(new string[]{new string(' ',this.formscan.nb_digits)+System.DateTime.Now.TimeOfDay.ToString(),this.ip,"","Timeout"}));
            // remove object
            this.formscan.remove_icmp_scan_info(this);

            if (this.b_increase_semaphore)
                // increase number of signaled event by one
                Semaphore.ReleaseSemaphore(this.formscan.h_semaphore,1);
        }
    }
    #endregion

    #region scan information class
    public class scan_information
    {
        public string ip;
        public ushort port;
        public string data;
        public uint id;

        public scan_information(string ip,ushort port,string data,uint id)
        {
            this.ip=ip;
            this.port=port;
            this.data=data;
            this.id=id;
        }
        public scan_information(string ip,ushort port,uint id)
        {
            this.ip=ip;
            this.port=port;
            this.data="";
            this.id=id;
        }
        public scan_information(string ip,uint id)
        {
            this.ip=ip;
            this.port=0;
            this.data="";
            this.id=id;
        }
    }
    #endregion

    public class ping_result
    {
        public string ip;
        public bool success;
        public ping_result(string ip,bool success)
        {
            this.ip=ip;
            this.success=success;
        }
    }
    public class proxy_information:CHost
    {
        public bool b_checked=false;
        public proxy_information(string ip,ushort port):base(ip,port)
        {
        }
    }
}
