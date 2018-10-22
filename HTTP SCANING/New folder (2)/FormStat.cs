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
    /// Description résumée de FormStat.
    /// </summary>
    public class FormStat : System.Windows.Forms.Form
    {
        // image list should not be cleared
        private System.Collections.Specialized.StringCollection str_col;
        private byte last_action;
        private ListViewItemComparer lvic;
        private System.Timers.Timer timer;
        private iphelper.CMIB_TCPEXTABLE mmib_tcp_ext_table;// used for connection closing and process ending (only if is os xp)
        private iphelper.CMIB_TCPTABLE mmib_tcp_table;
        private iphelper.CMIB_IPNETTABLE mmib_ip_net_table;// used to remove ipnetentry

        private System.Windows.Forms.ListView listView_stats;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Button button_tcp_table;
        private System.Windows.Forms.Button button_udp_table;
        private System.Windows.Forms.Button button_tcp_stat;
        private System.Windows.Forms.Button button_udp_stats;
        private System.Windows.Forms.Button button_icmp_stats;
        private System.Windows.Forms.Button button_ipnet_table;
        private System.Windows.Forms.Button button_ipnet_stats;
        private System.Windows.Forms.Button button_refresh_now;
        private System.Windows.Forms.CheckBox checkBox_auto_refresh;
        private System.Windows.Forms.TextBox textBox_refresh_interval;
        private System.Windows.Forms.ContextMenu contextMenu;
        private System.Windows.Forms.MenuItem menuItem_close_connection;
        private System.Windows.Forms.MenuItem menuItem_end_process;
        private System.Windows.Forms.MenuItem menuItem_remove_entry;
        private System.Windows.Forms.MenuItem menuItem_add_entry;
        private System.Windows.Forms.MenuItem menuItem_edit_entry;
        private System.Windows.Forms.MenuItem menuItem_copy_remote_ip; 
        private string default_system_app="System";// wathever exists
        public FormStat()
        {
            InitializeComponent();
            XPStyle.MakeXPStyle(this);
            this.listView_stats.SmallImageList=this.imageList;
            this.str_col=new System.Collections.Specialized.StringCollection();// str cols contains the name in the same order as images in imagelist
            this.str_col.Add(default_system_app);// this.imageList must contain a default icon for the system

            this.lvic=new ListViewItemComparer();
            this.last_action=(byte)Elast_action.no_action;
            this.timer=new System.Timers.Timer();
            this.timer.Elapsed+=new System.Timers.ElapsedEventHandler(timer_event);
        }


        protected override void Dispose( bool disposing )
        {
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
            this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormStat));
            this.listView_stats = new System.Windows.Forms.ListView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.button_tcp_table = new System.Windows.Forms.Button();
            this.button_tcp_stat = new System.Windows.Forms.Button();
            this.button_udp_table = new System.Windows.Forms.Button();
            this.button_udp_stats = new System.Windows.Forms.Button();
            this.button_icmp_stats = new System.Windows.Forms.Button();
            this.button_ipnet_table = new System.Windows.Forms.Button();
            this.button_ipnet_stats = new System.Windows.Forms.Button();
            this.button_refresh_now = new System.Windows.Forms.Button();
            this.checkBox_auto_refresh = new System.Windows.Forms.CheckBox();
            this.textBox_refresh_interval = new System.Windows.Forms.TextBox();
            this.contextMenu = new System.Windows.Forms.ContextMenu();
            this.menuItem_close_connection = new System.Windows.Forms.MenuItem();
            this.menuItem_end_process = new System.Windows.Forms.MenuItem();
            this.menuItem_copy_remote_ip = new System.Windows.Forms.MenuItem();
            this.menuItem_remove_entry = new System.Windows.Forms.MenuItem();
            this.menuItem_add_entry = new System.Windows.Forms.MenuItem();
            this.menuItem_edit_entry = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // listView_stats
            // 
            this.listView_stats.FullRowSelect = true;
            this.listView_stats.GridLines = true;
            this.listView_stats.Location = new System.Drawing.Point(112, 0);
            this.listView_stats.Name = "listView_stats";
            this.listView_stats.Size = new System.Drawing.Size(400, 344);
            this.listView_stats.TabIndex = 0;
            this.listView_stats.View = System.Windows.Forms.View.Details;
            this.listView_stats.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView_stats_MouseUp);
            this.listView_stats.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_stats_ColumnClick);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // button_tcp_table
            // 
            this.button_tcp_table.Location = new System.Drawing.Point(8, 8);
            this.button_tcp_table.Name = "button_tcp_table";
            this.button_tcp_table.TabIndex = 1;
            this.button_tcp_table.Text = "TCP Table";
            this.button_tcp_table.Click += new System.EventHandler(this.button_tcp_table_Click);
            // 
            // button_tcp_stat
            // 
            this.button_tcp_stat.Location = new System.Drawing.Point(8, 56);
            this.button_tcp_stat.Name = "button_tcp_stat";
            this.button_tcp_stat.TabIndex = 3;
            this.button_tcp_stat.Text = "TCP Stats";
            this.button_tcp_stat.Click += new System.EventHandler(this.button_tcp_stats_Click);
            // 
            // button_udp_table
            // 
            this.button_udp_table.Location = new System.Drawing.Point(8, 32);
            this.button_udp_table.Name = "button_udp_table";
            this.button_udp_table.TabIndex = 2;
            this.button_udp_table.Text = "UDP Table";
            this.button_udp_table.Click += new System.EventHandler(this.button_udp_table_Click);
            // 
            // button_udp_stats
            // 
            this.button_udp_stats.Location = new System.Drawing.Point(8, 80);
            this.button_udp_stats.Name = "button_udp_stats";
            this.button_udp_stats.TabIndex = 4;
            this.button_udp_stats.Text = "UDP Stats";
            this.button_udp_stats.Click += new System.EventHandler(this.button_udp_stats_Click);
            // 
            // button_icmp_stats
            // 
            this.button_icmp_stats.Location = new System.Drawing.Point(8, 104);
            this.button_icmp_stats.Name = "button_icmp_stats";
            this.button_icmp_stats.TabIndex = 5;
            this.button_icmp_stats.Text = "ICMP Stats";
            this.button_icmp_stats.Click += new System.EventHandler(this.button_icmp_stats_Click);
            // 
            // button_ipnet_table
            // 
            this.button_ipnet_table.Location = new System.Drawing.Point(8, 128);
            this.button_ipnet_table.Name = "button_ipnet_table";
            this.button_ipnet_table.TabIndex = 6;
            this.button_ipnet_table.Text = "IP Table";
            this.button_ipnet_table.Click += new System.EventHandler(this.button_ipnet_table_Click);
            // 
            // button_ipnet_stats
            // 
            this.button_ipnet_stats.Location = new System.Drawing.Point(8, 152);
            this.button_ipnet_stats.Name = "button_ipnet_stats";
            this.button_ipnet_stats.TabIndex = 7;
            this.button_ipnet_stats.Text = "IP Stats";
            this.button_ipnet_stats.Click += new System.EventHandler(this.button_ipnet_stats_Click);
            // 
            // button_refresh_now
            // 
            this.button_refresh_now.Location = new System.Drawing.Point(8, 208);
            this.button_refresh_now.Name = "button_refresh_now";
            this.button_refresh_now.Size = new System.Drawing.Size(80, 23);
            this.button_refresh_now.TabIndex = 8;
            this.button_refresh_now.Text = "Refresh Now";
            this.button_refresh_now.Click += new System.EventHandler(this.button_refresh_now_Click);
            // 
            // checkBox_auto_refresh
            // 
            this.checkBox_auto_refresh.Location = new System.Drawing.Point(8, 240);
            this.checkBox_auto_refresh.Name = "checkBox_auto_refresh";
            this.checkBox_auto_refresh.Size = new System.Drawing.Size(104, 40);
            this.checkBox_auto_refresh.TabIndex = 9;
            this.checkBox_auto_refresh.Text = "Auto Refresh every (in ms)";
            this.checkBox_auto_refresh.CheckedChanged += new System.EventHandler(this.checkBox_auto_refresh_CheckedChanged);
            // 
            // textBox_refresh_interval
            // 
            this.textBox_refresh_interval.Location = new System.Drawing.Point(32, 280);
            this.textBox_refresh_interval.Name = "textBox_refresh_interval";
            this.textBox_refresh_interval.Size = new System.Drawing.Size(40, 20);
            this.textBox_refresh_interval.TabIndex = 10;
            this.textBox_refresh_interval.Text = "5000";
            this.textBox_refresh_interval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_refresh_interval.TextChanged += new System.EventHandler(this.textBox_refresh_interval_TextChanged);
            // 
            // contextMenu
            // 
            this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                        this.menuItem_close_connection,
                                                                                        this.menuItem_end_process,
                                                                                        this.menuItem_copy_remote_ip,
                                                                                        this.menuItem_remove_entry,
                                                                                        this.menuItem_add_entry,
                                                                                        this.menuItem_edit_entry});
            // 
            // menuItem_close_connection
            // 
            this.menuItem_close_connection.Index = 0;
            this.menuItem_close_connection.Text = "Close Connection";
            this.menuItem_close_connection.Click += new System.EventHandler(this.menuItem_close_connection_Click);
            // 
            // menuItem_end_process
            // 
            this.menuItem_end_process.Index = 1;
            this.menuItem_end_process.Text = "End Process";
            this.menuItem_end_process.Click += new System.EventHandler(this.menuItem_end_process_Click);
            // 
            // menuItem_copy_remote_ip
            // 
            this.menuItem_copy_remote_ip.Index = 2;
            this.menuItem_copy_remote_ip.Text = "Copy Selected Remote IP";
            this.menuItem_copy_remote_ip.Click += new System.EventHandler(this.menuItem_copy_remote_ip_Click);
            // 
            // menuItem_remove_entry
            // 
            this.menuItem_remove_entry.Index = 3;
            this.menuItem_remove_entry.Text = "Remove Entry";
            this.menuItem_remove_entry.Click += new System.EventHandler(this.menuItem_remove_entry_Click);
            // 
            // menuItem_add_entry
            // 
            this.menuItem_add_entry.Index = 4;
            this.menuItem_add_entry.Text = "Add Entry";
            this.menuItem_add_entry.Click += new System.EventHandler(this.menuItem_add_entry_Click);
            // 
            // menuItem_edit_entry
            // 
            this.menuItem_edit_entry.Index = 5;
            this.menuItem_edit_entry.Text = "Edit Entry";
            this.menuItem_edit_entry.Click += new System.EventHandler(this.menuItem_edit_entry_Click);
            // 
            // FormStat
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(512, 342);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.textBox_refresh_interval,
                                                                          this.checkBox_auto_refresh,
                                                                          this.button_refresh_now,
                                                                          this.button_ipnet_stats,
                                                                          this.button_ipnet_table,
                                                                          this.button_icmp_stats,
                                                                          this.button_udp_stats,
                                                                          this.button_udp_table,
                                                                          this.button_tcp_stat,
                                                                          this.button_tcp_table,
                                                                          this.listView_stats});
            this.Name = "FormStat";
            this.Text = "Stats";
            this.SizeChanged += new System.EventHandler(this.m_SizeChanged);
            this.ResumeLayout(false);

        }
        #endregion

        #region resize
        private void m_SizeChanged(object sender, System.EventArgs e)
        {
            this.resize_list();
        }
        private void resize_list()
        {
            int i_old_size=this.listView_stats.Width;
            int i_new_size=this.ClientSize.Width-this.listView_stats.Left;
            this.listView_stats.Height=this.ClientSize.Height;
            if(i_old_size==i_new_size)
                return;
            if (i_new_size>200)
            {
                this.listView_stats.Width=i_new_size;

                // make proportionnal growth of columns width
                if (this.listView_stats.Columns.Count==0)
                    return;
                int full_size=0;
                int c_size;
                for (int cpt=0;cpt<this.listView_stats.Columns.Count;cpt++)
                {
                    c_size=this.listView_stats.Columns[cpt].Width*i_new_size/i_old_size;
                    this.listView_stats.Columns[cpt].Width=c_size;
                    full_size+=c_size;
                }
                if (full_size<this.listView_stats.Width-25)// error compasation
                {
                    c_size=(this.listView_stats.Width-25-full_size)/this.listView_stats.Columns.Count;
                    for (int cpt=0;cpt<this.listView_stats.Columns.Count;cpt++)
                    {
                        this.listView_stats.Columns[cpt].Width+=c_size;
                    }
                }
            }
        }
        #endregion
        private void set_columns_header(string[] pstr)
        {
            this.listView_stats.Columns.Clear();
            if (pstr.Length==0)
                return;
            int i_columns_size=this.listView_stats.Width-10;
            i_columns_size/=pstr.Length;
            for (int cpt=0;cpt<pstr.Length;cpt++)
            {
                this.listView_stats.Columns.Add(pstr[cpt],i_columns_size,HorizontalAlignment.Left);
            }
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

        private int get_image_list_index(string full_path)
        {
            for (int cpt =0;cpt<this.str_col.Count;cpt++)
            {
                if (this.str_col[cpt]==full_path)
                    return cpt;
            }
            // not found --> add to string collection and image list
            this.str_col.Add(full_path);
            this.imageList.Images.Add(CIcon.GetIcon(full_path,false));
            return this.imageList.Images.Count-1;
        }

        #region get_tcp_table
        public void get_tcp_table()
        {
            this.listView_stats.SuspendLayout();
            this.SuspendLayout();
            ListViewItem li;
            this.listView_stats.Items.Clear();
            if (this.is_xp_os())
            {
                iphelper.CMIB_TCPEXTABLE table=iphelper.iphelper.GetTcpExTable();
                if (table==null)
                    return;
                string[] pstr={"Process Name","Process Id","Local Addr","Local Port","Remote Addr","Remote Port","State","Process path"};
                this.set_columns_header(pstr);
                int img_index;
                string filename;
                for (int cpt=0;cpt<table.dwNumEntries;cpt++)
                {
                    if (table.table[cpt].pProcess==null)
                        continue;// don't show informations
                    try
                    {
                        filename=table.table[cpt].pProcess.MainModule.FileName;
                        img_index=this.get_image_list_index(filename);
                    }
                    catch
                    {
                        filename="";
                        img_index=this.get_image_list_index(default_system_app);
                    }
                    li=this.listView_stats.Items.Add(table.table[cpt].pProcess.ProcessName,img_index);
                    li.SubItems.AddRange(new String[]{
                            table.table[cpt].dwProcessId.ToString(),
                            table.table[cpt].get_localip(),
                            table.table[cpt].get_local_port().ToString(),
                            table.table[cpt].get_remoteip(),
                            table.table[cpt].get_remote_port().ToString(),
                            table.table[cpt].get_state(),
                            filename});
                }
                this.mmib_tcp_ext_table=table;
            }
            else
            {
                iphelper.CMIB_TCPTABLE table=iphelper.iphelper.GetTcpTable();
                if (table==null)
                    return;
                string[] pstr={"Local Addr","Local Port","Remote Addr","Remote Port","State"};
                this.set_columns_header(pstr);
                for (int cpt=0;cpt<table.dwNumEntries;cpt++)
                {
                    li=this.listView_stats.Items.Add(table.table[cpt].get_localip());
                    li.SubItems.AddRange(new String[]{     table.table[cpt].get_local_port().ToString(),
                                                         table.table[cpt].get_remoteip(),
                                                         table.table[cpt].get_remote_port().ToString(),
                                                         table.table[cpt].get_state()
                                                     });
                    
                }
                this.mmib_tcp_table=table;
            }
            this.listView_stats.ResumeLayout(false);
            this.ResumeLayout(false);
            this.last_action=(byte)Elast_action.get_tcp_table;
        }
        #endregion
        #region tcp_stats
        private const byte MIB_TCP_RTO_CONSTANT=2;
        private const byte MIB_TCP_RTO_RSRE=3;
        private const byte MIB_TCP_RTO_VANJ=4;
        private const byte MIB_TCP_RTO_OTHER=1;

        public void get_tcp_stats()
        {
            ListViewItem li;
            this.listView_stats.Items.Clear();
            iphelper.MIB_TCPSTATS table=iphelper.iphelper.GetTcpStatisticsEx();

            string[] pstr={"Name","Value"};
            this.set_columns_header(pstr);

            li=this.listView_stats.Items.Add("active opens");
            li.SubItems.Add(table.dwActiveOpens.ToString());

            li=this.listView_stats.Items.Add("failed attempts");
            li.SubItems.Add(table.dwAttemptFails.ToString());

            li=this.listView_stats.Items.Add("established connections");
            li.SubItems.Add(table.dwCurrEstab.ToString());

            li=this.listView_stats.Items.Add("established connections reset");
            li.SubItems.Add(table.dwEstabResets.ToString());

            li=this.listView_stats.Items.Add("incoming errors");
            li.SubItems.Add(table.dwInErrs.ToString());

            li=this.listView_stats.Items.Add("segments received");
            li.SubItems.Add(table.dwInSegs.ToString());
            
            li=this.listView_stats.Items.Add("maximum connections");
            if (table.dwMaxConn==0xffffffff)
                li.SubItems.Add("variable");
            else
                li.SubItems.Add(table.dwMaxConn.ToString());

            li=this.listView_stats.Items.Add("cumulative connections");
            li.SubItems.Add(table.dwNumConns.ToString());

            li=this.listView_stats.Items.Add("outgoing resets");
            li.SubItems.Add(table.dwOutRsts.ToString());

            li=this.listView_stats.Items.Add("segment sent");
            li.SubItems.Add(table.dwOutSegs.ToString());

            li=this.listView_stats.Items.Add("passive opens");
            li.SubItems.Add(table.dwPassiveOpens.ToString());

            li=this.listView_stats.Items.Add("segments retransmitted");
            li.SubItems.Add(table.dwRetransSegs.ToString());

            li=this.listView_stats.Items.Add("time-out algorithm");
            string str_algo="";
            switch (table.dwRtoAlgorithm)
            {
                case MIB_TCP_RTO_CONSTANT:
                    str_algo="Constant Time-out";
                    break;
                case MIB_TCP_RTO_RSRE:
                    str_algo="MIL-STD-1778 Appendix B";
                    break;
                case MIB_TCP_RTO_VANJ:
                    str_algo="Van Jacobson's Algorithm";
                    break;
                case MIB_TCP_RTO_OTHER:
                    str_algo="Other";
                    break;
            }
            li.SubItems.Add(str_algo);

            li=this.listView_stats.Items.Add("maximum time-out");
            li.SubItems.Add(table.dwRtoMax.ToString());

            li=this.listView_stats.Items.Add("minimum time-out");
            li.SubItems.Add(table.dwRtoMin.ToString());
            this.last_action=(byte)Elast_action.get_tcp_stats;
        }
        #endregion
        #region udp_table
        public void get_udp_table()
        {
            this.listView_stats.SuspendLayout();
            this.SuspendLayout();
            ListViewItem li;
            this.listView_stats.Items.Clear();
            if (this.is_xp_os())
            {
                iphelper.CMIB_UDPEXTABLE table=iphelper.iphelper.GetUdpExTable();
                if (table==null)
                    return;
                string[] pstr={"Process Name","Process Id","Local Addr","Local Port","Process path"};
                this.set_columns_header(pstr);
                int img_index;
                string filename;
                for (int cpt=0;cpt<table.dwNumEntries;cpt++)
                {
                    if (table.table[cpt].pProcess==null)
                        continue;// don't show informations
                    try
                    {
                        filename=table.table[cpt].pProcess.MainModule.FileName;
                        img_index=this.get_image_list_index(filename);
                    }
                    catch
                    {
                        filename="";
                        img_index=this.get_image_list_index(default_system_app);
                    }
                    li=this.listView_stats.Items.Add(table.table[cpt].pProcess.ProcessName,img_index);
                    li.SubItems.AddRange(new String[]{
                                                         table.table[cpt].dwProcessId.ToString(),
                                                         table.table[cpt].get_ip(),
                                                         table.table[cpt].get_port().ToString(),
                                                         filename});
                    
                }
            }
            else
            {
                iphelper.CMIB_UDPTABLE table=iphelper.iphelper.GetUdpTable();
                if (table==null)
                    return;
                string[] pstr={"Local Addr","Local Port"};
                this.set_columns_header(pstr);
                for (int cpt=0;cpt<table.dwNumEntries;cpt++)
                {
                    li=this.listView_stats.Items.Add(table.table[cpt].get_ip());
                    li.SubItems.Add(table.table[cpt].get_port().ToString());
                    
                }
            }
            this.listView_stats.ResumeLayout(false);
            this.ResumeLayout(false);
            this.last_action=(byte)Elast_action.get_udp_table;
        }
        #endregion
        #region udp stats
        public void get_udp_stats()
        {
            ListViewItem li;
            this.listView_stats.Items.Clear();
            iphelper.MIB_UDPSTATS table=iphelper.iphelper.GetUdpStatisticsEx();

            string[] pstr={"Name","Value"};
            this.set_columns_header(pstr);

            li=this.listView_stats.Items.Add("received datagrams");
            li.SubItems.Add(table.dwInDatagrams.ToString());

            li=this.listView_stats.Items.Add("datagrams for which no port exists ");
            li.SubItems.Add(table.dwNoPorts.ToString());

            li=this.listView_stats.Items.Add("errors on received datagrams");
            li.SubItems.Add(table.dwInErrors.ToString());

            li=this.listView_stats.Items.Add("sent datagrams");
            li.SubItems.Add(table.dwOutDatagrams.ToString());

            li=this.listView_stats.Items.Add("number of entries in UDP listener table");
            li.SubItems.Add(table.dwNumAddrs.ToString());

            this.last_action=(byte)Elast_action.get_udp_stats;
        }
        #endregion
        #region icmp stats
        public void get_icmp_stats()
        {
            ListViewItem li;
            this.listView_stats.Items.Clear();
            iphelper.MIB_ICMP table=iphelper.iphelper.GetIcmpStatistics();

            string[] pstr={"Name","Value"};
            this.set_columns_header(pstr);

            iphelper.MIBICMPSTATS stats=table.stats.icmpInStats;
            this.listView_stats.Items.Add("ICMP In");
            for (int cpt=0;cpt<2;cpt++)
            {

                li=this.listView_stats.Items.Add("number of messages");
                li.SubItems.Add(stats.dwMsgs.ToString());

                li=this.listView_stats.Items.Add("number of errors");
                li.SubItems.Add(stats.dwErrors.ToString());

                li=this.listView_stats.Items.Add("destination unreachable messages");
                li.SubItems.Add(stats.dwDestUnreachs.ToString());

                li=this.listView_stats.Items.Add("time-to-live exceeded messages");
                li.SubItems.Add(stats.dwTimeExcds.ToString());

                li=this.listView_stats.Items.Add("parameter problem messages");
                li.SubItems.Add(stats.dwParmProbs.ToString());

                li=this.listView_stats.Items.Add("source quench messages");
                li.SubItems.Add(stats.dwSrcQuenchs.ToString());

                li=this.listView_stats.Items.Add("redirection messages");
                li.SubItems.Add(stats.dwRedirects.ToString());

                li=this.listView_stats.Items.Add("echo requests");
                li.SubItems.Add(stats.dwEchos.ToString());

                li=this.listView_stats.Items.Add("echo replies");
                li.SubItems.Add(stats.dwEchoReps.ToString());

                li=this.listView_stats.Items.Add("time-stamp requests");
                li.SubItems.Add(stats.dwTimestamps.ToString());

                li=this.listView_stats.Items.Add("time-stamp replies");
                li.SubItems.Add(stats.dwTimestampReps.ToString());

                li=this.listView_stats.Items.Add("address mask requests");
                li.SubItems.Add(stats.dwAddrMasks.ToString());

                li=this.listView_stats.Items.Add("address mask replies");
                li.SubItems.Add(stats.dwAddrMaskReps.ToString());

                if (cpt==0)
                {
                    stats=table.stats.icmpOutStats;
                    this.listView_stats.Items.Add("");
                    this.listView_stats.Items.Add("ICMP Out");
                }
            }
            this.last_action=(byte)Elast_action.get_icmp_stats;
        }
        #endregion
        #region ipnet table
        public void get_ipnet_table()
        {
            this.SuspendLayout();
            ListViewItem li;
            this.listView_stats.Items.Clear();
            iphelper.CMIB_IPNETTABLE table=iphelper.iphelper.GetIpNetTable();
            if (table==null)
                return;
            string[] pstr={"IP Address","physical address","ARP entry type","adapter index","physical address length"};
            this.set_columns_header(pstr);
            for (int cpt=0;cpt<table.dwNumEntries;cpt++)
            {
                li=this.listView_stats.Items.Add(table.table[cpt].get_ip());
                li.SubItems.AddRange(new string[]{table.table[cpt].get_mac(),
                                    table.table[cpt].get_type(),
                                    table.table[cpt].dwIndex.ToString(),
                                    table.table[cpt].dwPhysAddrLen.ToString(),
                                    });
            }
            this.mmib_ip_net_table=table;
            this.ResumeLayout(false);
            this.last_action=(byte)Elast_action.get_ipnet_table;
        }
        #endregion
        #region ip_stats
        public void get_ip_stats()
        {
            ListViewItem li;
            this.listView_stats.Items.Clear();
            iphelper.MIB_IPSTATS table=iphelper.iphelper.GetIpStatisticsEx();

            string[] pstr={"Name","Value"};
            this.set_columns_header(pstr);

            li=this.listView_stats.Items.Add("IP forwarding enabled or disabled");
            li.SubItems.Add(table.dwForwarding.ToString());

            li=this.listView_stats.Items.Add("default time-to-live");
            li.SubItems.Add(table.dwDefaultTTL.ToString());

            li=this.listView_stats.Items.Add("datagrams received");
            li.SubItems.Add(table.dwInReceives.ToString());


            li=this.listView_stats.Items.Add("received header errors");
            li.SubItems.Add(table.dwInHdrErrors.ToString());

            li=this.listView_stats.Items.Add("received address errors");
            li.SubItems.Add(table.dwInAddrErrors.ToString());

            li=this.listView_stats.Items.Add("datagrams forwarded");
            li.SubItems.Add(table.dwForwDatagrams.ToString());

            li=this.listView_stats.Items.Add("datagrams with unknown protocol");
            li.SubItems.Add(table.dwInUnknownProtos.ToString());

            li=this.listView_stats.Items.Add("received datagrams discarded");
            li.SubItems.Add(table.dwInDiscards.ToString());

            li=this.listView_stats.Items.Add("received datagrams delivered");
            li.SubItems.Add(table.dwInDelivers.ToString());

            li=this.listView_stats.Items.Add("sent datagrams discarded");
            li.SubItems.Add(table.dwOutRequests.ToString());

            li=this.listView_stats.Items.Add("datagrams for which no route exists");
            li.SubItems.Add(table.dwRoutingDiscards.ToString());

            li=this.listView_stats.Items.Add("datagrams for which all frags did not arrive");
            li.SubItems.Add(table.dwOutDiscards.ToString());

            li=this.listView_stats.Items.Add("number of outgoing datagrams that IP is requested to transmit. This number does not include forwarded datagrams");
            li.SubItems.Add(table.dwOutNoRoutes.ToString());

            li=this.listView_stats.Items.Add("number of outgoing datagrams discarded");
            li.SubItems.Add(table.dwReasmTimeout.ToString());

            li=this.listView_stats.Items.Add("datagrams requiring reassembly");
            li.SubItems.Add(table.dwReasmReqds.ToString());

            li=this.listView_stats.Items.Add("successful reassemblies");
            li.SubItems.Add(table.dwReasmOks.ToString());

            li=this.listView_stats.Items.Add("failed reassemblies");
            li.SubItems.Add(table.dwReasmFails.ToString());

            li=this.listView_stats.Items.Add("successful fragmentations");
            li.SubItems.Add(table.dwFragOks.ToString());

            li=this.listView_stats.Items.Add("failed fragmentations");
            li.SubItems.Add(table.dwFragFails.ToString());

            li=this.listView_stats.Items.Add("datagrams fragmented");
            li.SubItems.Add(table.dwFragCreates.ToString());

            li=this.listView_stats.Items.Add("number of interfaces on computer");
            li.SubItems.Add(table.dwNumIf.ToString());

            li=this.listView_stats.Items.Add("number of IP address on computer");
            li.SubItems.Add(table.dwNumAddr.ToString());

            li=this.listView_stats.Items.Add("number of routes in routing table");
            li.SubItems.Add(table.dwNumRoutes.ToString());
            this.last_action=(byte)Elast_action.get_ip_stats;
        }
        #endregion

        
        private enum Elast_action
        {
            no_action,
            get_tcp_table,
            get_tcp_stats,
            get_udp_table,
            get_udp_stats,
            get_icmp_stats,
            get_ipnet_table,
            get_ip_stats
        }
        private void button_tcp_table_Click(object sender, System.EventArgs e)
        {
            this.get_tcp_table();
        }

        private void button_tcp_stats_Click(object sender, System.EventArgs e)
        {
            this.get_tcp_stats();
        }

        private void button_udp_table_Click(object sender, System.EventArgs e)
        {
            this.get_udp_table();
        }
        private void button_udp_stats_Click(object sender, System.EventArgs e)
        {
            this.get_udp_stats();
        }

        private void button_icmp_stats_Click(object sender, System.EventArgs e)
        {
            this.get_icmp_stats();
        }

        private void button_ipnet_table_Click(object sender, System.EventArgs e)
        {
            this.get_ipnet_table();
        }

        private void button_ipnet_stats_Click(object sender, System.EventArgs e)
        {
            this.get_ip_stats();
        }

        private void listView_stats_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            lvic.set_column_number(e.Column);
            this.listView_stats.ListViewItemSorter=lvic;
            this.listView_stats.Sort();
            this.listView_stats.ListViewItemSorter=null;// avoid to organize when changing data
        }

        private void checkBox_auto_refresh_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.checkBox_auto_refresh.Checked)
            {
                if (!CCheck_user_interface_inputs.check_int(this.textBox_refresh_interval.Text))
                    return;
                this.timer.Interval=Math.Abs(System.Convert.ToUInt32(this.textBox_refresh_interval.Text,10));
                this.timer.Start();
            }
            else
                this.timer.Stop();
        }
        private void textBox_refresh_interval_TextChanged(object sender, System.EventArgs e)
        {
            if (this.checkBox_auto_refresh.Checked)        
            {
                if (this.textBox_refresh_interval.Text!="")
                {
                    this.timer.Stop();
                    if (!CCheck_user_interface_inputs.check_int(this.textBox_refresh_interval.Text))
                        return;
                    this.timer.Interval=Math.Abs(System.Convert.ToUInt32(this.textBox_refresh_interval.Text,10));
                    this.timer.Start();
                }
            }
        }

        private void call_last_action()
        {
            switch (this.last_action)
            {
                case (byte)Elast_action.get_icmp_stats:
                    this.get_icmp_stats();
                    break;
                case (byte)Elast_action.get_ip_stats:
                    this.get_ip_stats();
                    break;
                case (byte)Elast_action.get_ipnet_table:
                    this.get_ipnet_table();
                    break;
                case (byte)Elast_action.get_tcp_stats:
                    this.get_tcp_stats();
                    break;
                case (byte)Elast_action.get_tcp_table:
                    this.get_tcp_table();
                    break;
                case (byte)Elast_action.get_udp_stats:
                    this.get_udp_stats();
                    break;
                case (byte)Elast_action.get_udp_table:
                    this.get_udp_table();
                    break;
            }
        }

        private void button_refresh_now_Click(object sender, System.EventArgs e)
        {
            this.call_last_action();
        }
        private void timer_event(object sender,System.Timers.ElapsedEventArgs e)
        {
            this.call_last_action();
        }
        private void menuItem_close_connection_Click(object sender, System.EventArgs e)
        {
            iphelper.MIB_TCPROW TcpRow;
            int nb_entry;
            bool b_xp_os=this.is_xp_os();
            if (b_xp_os)
                nb_entry=(int)this.mmib_tcp_ext_table.dwNumEntries;
            else
                nb_entry=(int)this.mmib_tcp_table.dwNumEntries;
            UInt32 ret=0;
            ListViewItem lvi;
            for (int cpt=0;cpt<this.listView_stats.Items.Count;cpt++)
            {
                lvi=this.listView_stats.Items[cpt];
                if (lvi.Selected)
                {
                    if (lvi.Index>this.mmib_tcp_ext_table.dwNumEntries)
                        continue;

                    if (b_xp_os)
                        TcpRow=this.mmib_tcp_ext_table.table[lvi.Index].get_MIB_TCPROW_struct();
                    else
                        TcpRow=this.mmib_tcp_table.table[lvi.Index].get_MIB_TCPROW_struct();

                    TcpRow.dwState=iphelper.CMIB_TCPROW.MIB_TCP_STATE_DELETE_TCB;

                    //MSDN:"Currently, the only state to which a TCP connection can be set is MIB_TCP_STATE_DELETE_TCB"
                    //return 65 - User has no sufficient privilege to execute this API successfully 
                    //return 87 - Specified port is not in state to be closed down.
                    ret=iphelper.iphelper.SetTcpEntry(ref TcpRow);
                    if (ret==65)
                        MessageBox.Show(this,
                                        "User has no sufficient privilege to execute this API successfully",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error
                                        );
                    else
                        if (ret==65)
                                MessageBox.Show(this,
                                "Specified port is not in state to be closed down.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                }
            }
            // refresh list
            this.get_tcp_table();
        }

        private void menuItem_end_process_Click(object sender, System.EventArgs e)
        {
            ListViewItem lvi;
            for (int cpt=0;cpt<this.listView_stats.Items.Count;cpt++)
            {
                lvi=this.listView_stats.Items[cpt];

                if (lvi.Selected)
                {
                    if (lvi.Index>this.mmib_tcp_ext_table.dwNumEntries)
                        continue;

                    this.mmib_tcp_ext_table.table[lvi.Index].pProcess.Kill();
                }
            }
            // refresh list
            this.get_tcp_table();
        }

        private void menuItem_remove_entry_Click(object sender, System.EventArgs e)
        {
            ListViewItem lvi;
            iphelper.MIB_IPNETROW mib_ipnet_r;
            for (int cpt=0;cpt<this.listView_stats.Items.Count;cpt++)
            {
                lvi=this.listView_stats.Items[cpt];

                if (lvi.Selected)
                {
                    if (lvi.Index>this.mmib_ip_net_table.dwNumEntries)
                        continue;
                    mib_ipnet_r=this.mmib_ip_net_table.table[lvi.Index].get_MIB_IPNETROW();
                    UInt32 ret=iphelper.iphelper.DeleteIpNetEntry(ref mib_ipnet_r);
                    if (ret!=0)

                        MessageBox.Show(this,
                            API_error.GetAPIErrorMessageDescription(ret),
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                            );
                }
            }
            // refresh list
            this.get_ipnet_table();        
        }

        private void menuItem_add_entry_Click(object sender, System.EventArgs e)
        {
            FormAddIPEntry f=new FormAddIPEntry();
            f.ShowDialog(this);
            // refresh list
            this.get_ipnet_table();
        }
        private void menuItem_edit_entry_Click(object sender, System.EventArgs e)
        {
            iphelper.MIB_IPNETROW mib_i=new iphelper.MIB_IPNETROW();

            // ip mac type index physAddrLen
            // get bPhysAddr
            string mac_addr=this.listView_stats.SelectedItems[0].SubItems[1].Text;
            mac_addr=mac_addr.Replace(" ","");
            mac_addr=mac_addr.Replace("-","");
            mac_addr=mac_addr.Replace(".","");
            mac_addr=mac_addr.Replace(":","");
            mib_i.bPhysAddr=new byte[iphelper.CMIB_IPNETROW.MAXLEN_PHYSADDR];
            for(int cpt=0;cpt<mac_addr.Length/2;cpt++)
            {
                mib_i.bPhysAddr[cpt]=byte.Parse(mac_addr.Substring(2*cpt,2),
                    System.Globalization.NumberStyles.HexNumber);
            }
            // get dwPhysAddrLen
            mib_i.dwPhysAddrLen=(UInt32)(mac_addr.Length/2);

            // get dwIndex
            mib_i.dwIndex=System.Convert.ToUInt32(this.listView_stats.SelectedItems[0].SubItems[3].Text,10);
            // get dwAddr
            try
            {
                mib_i.dwAddr=(UInt32)(System.Net.IPAddress.Parse(this.listView_stats.SelectedItems[0].Text).Address);
            }
            catch
            {
                MessageBox.Show(this,"Error in IP address","error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            // get dwtype
            switch (this.listView_stats.SelectedItems[0].SubItems[2].Text)
            {
                case "Dynamic":
                    mib_i.dwType=iphelper.CMIB_IPNETROW.dwType_Dynamic;
                    break;
                case "Invalid":
                    mib_i.dwType=iphelper.CMIB_IPNETROW.dwType_Invalid;
                    break;
                case "Other":
                    mib_i.dwType=iphelper.CMIB_IPNETROW.dwType_Other;
                    break;
                case "Static":
                    mib_i.dwType=iphelper.CMIB_IPNETROW.dwType_Static;
                    break;
                default:
                    mib_i.dwType=iphelper.CMIB_IPNETROW.dwType_Dynamic;
                    break;
            }



            FormAddIPEntry f=new FormAddIPEntry(mib_i);
            f.ShowDialog(this);
            // refresh list
            this.get_ipnet_table();        
        }

        private void listView_stats_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Right)
            {
                switch (this.last_action)
                {
                    case (byte)Elast_action.get_ipnet_table:
                        this.change_enabled_menu_ipnet_table(true);
                        this.change_enabled_menu_tcp_table(false);
                        this.contextMenu.Show((ListView) sender,new System.Drawing.Point(e.X,e.Y));
                        break;
                    case (byte)Elast_action.get_tcp_table:
                        this.change_enabled_menu_ipnet_table(false);
                        this.change_enabled_menu_tcp_table(true);
                        this.contextMenu.Show((ListView)sender,new System.Drawing.Point(e.X,e.Y));
                        break;
                }
            }        
        }
        private void change_enabled_menu_tcp_table(bool enable)
        {
            this.menuItem_end_process.Visible=enable;
            if (this.menuItem_end_process.Visible)
                this.menuItem_end_process.Visible=this.is_xp_os();
            this.menuItem_close_connection.Visible=enable;
            this.menuItem_copy_remote_ip.Visible=enable;
        }
        private void change_enabled_menu_ipnet_table(bool enable)
        {
            this.menuItem_add_entry.Visible=enable;
            this.menuItem_remove_entry.Visible=enable;
            this.menuItem_edit_entry.Visible=enable;
        }

        private void menuItem_copy_remote_ip_Click(object sender, System.EventArgs e)
        {
            string str=this.listView_stats.SelectedItems[0].SubItems[4].Text;
            Clipboard.SetDataObject(str,true);
        }


    }
}
