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
    public class Cserver_whois
    {
        public string server;
        public string extension;
        public Cserver_whois()
        {
            this.server="";
            this.extension="";
        }
        public Cserver_whois(string extension,string server)
        {
            this.server=server;
            this.extension=extension;
        }
    }
    public class FormWhois : CommonTelnetForm
    {
        private const string WHOIS_SERVER_LIST_FILE="whois_servers.xml";

        #region whois server list
        private Cserver_whois[] whois_server_list=new Cserver_whois[116]
                            {
                            new Cserver_whois(".as","whois.nic.as"),
                            new Cserver_whois(".ac","whois.nic.ac"),
                            new Cserver_whois(".al","whois.ripe.net"),
                            new Cserver_whois(".am","whois.amnic.net"),
                            new Cserver_whois(".at","whois.ripe.net"),
                            new Cserver_whois(".net.au","whois.net.au"),//(.net.au uses its own whois server) 
                            new Cserver_whois(".au","whois.aunic.net"),//(for the rest of the .au domain) 
                            new Cserver_whois(".az","whois.ripe.net"),
                            new Cserver_whois(".ba","whois.ripe.net"),
                            new Cserver_whois(".be","whois.ripe.net"),
                            new Cserver_whois(".bg","whois.ripe.net"),
                            new Cserver_whois(".br","whois.nic.br"),
                            new Cserver_whois(".by","whois.ripe.net"),
                            new Cserver_whois(".ca","whois.cira.ca"),
                            new Cserver_whois(".cc","whois.nic.cc"),
                            new Cserver_whois(".ch","whois.nic.ch"),
                            new Cserver_whois(".cl","whois.nic.cl"),
                            new Cserver_whois(".cn","whois.cnnic.net.cn"),
                            new Cserver_whois(".cx","whois.nic.cx"),
                            new Cserver_whois(".cy","whois.ripe.net"),
                            new Cserver_whois(".cz","whois.ripe.net"),
                            new Cserver_whois(".de","whois.denic.de"),
                            new Cserver_whois(".dk","whois.dk-hostmaster.dk"),
                            new Cserver_whois(".dz","whois.ripe.net"),
                            new Cserver_whois(".ee","whois.ripe.net"),
                            new Cserver_whois(".eg","whois.ripe.net"),
                            new Cserver_whois(".es","whois.ripe.net"),
                            new Cserver_whois(".fi","whois.ripe.net"),
                            new Cserver_whois(".fo","whois.ripe.net"),
                            new Cserver_whois(".fr","whois.nic.fr"),
                            new Cserver_whois(".gb","whois.ripe.net"),
                            new Cserver_whois(".ge","whois.ripe.net"),
                            new Cserver_whois(".gr","whois.ripe.net"),
                            new Cserver_whois(".gs","whois.adamsnames.tc"),
                            new Cserver_whois(".hk","whois.apnic.net"),
                            new Cserver_whois(".hr","whois.ripe.net"),
                            new Cserver_whois(".hu","whois.ripe.net"),
                            new Cserver_whois(".ie","whois.domainregistry.ie"),
                            new Cserver_whois(".il","whois.ripe.net"),
                            new Cserver_whois(".in","whois.ncst.ernet.in"),
                            new Cserver_whois(".is","whois.ripe.net"),
                            new Cserver_whois(".it","whois.nic.it"),
                            new Cserver_whois(".jp","whois.nic.ad.jp"),
                            new Cserver_whois(".kh","whois.nic.net.kh"),
                            new Cserver_whois(".kr","whois.apnic.net"),
                            new Cserver_whois(".li","whois.nic.ch"),
                            new Cserver_whois(".lt","whois.ripe.net"),
                            new Cserver_whois(".lu","whois.dns.lu"),
                            new Cserver_whois(".lv","whois.ripe.net"),
                            new Cserver_whois(".ma","whois.ripe.net"),
                            new Cserver_whois(".md","whois.ripe.net"),
                            new Cserver_whois(".mk","whois.ripe.net"),
                            new Cserver_whois(".ms","whois.adamsnames.tc"),
                            new Cserver_whois(".mt","whois.ripe.net"),
                            new Cserver_whois(".mx","whois.nic.mx"),
                            new Cserver_whois(".nl","whois.domain-registry.nl"),
                            new Cserver_whois(".no","whois.norid.no"),
                            new Cserver_whois(".nu","whois.nic.nu"),
                            new Cserver_whois(".nz","whois.domainz.net.nz"),
                            new Cserver_whois(".pl","whois.ripe.net"),
                            new Cserver_whois(".pt","whois.ripe.net"),
                            new Cserver_whois(".ro","whois.ripe.net"),
                            new Cserver_whois(".ru","whois.ripn.ru"),
                            new Cserver_whois(".se","whois.nic-se.se"),
                            new Cserver_whois(".sg","whois.nic.net.sg"),
                            new Cserver_whois(".si","whois.ripe.net"),
                            new Cserver_whois(".sh","whois.nic.sh"),
                            new Cserver_whois(".sk","whois.ripe.net"),
                            new Cserver_whois(".sm","whois.ripe.net"),
                            new Cserver_whois(".su","whois.ripe.net"),
                            new Cserver_whois(".tc","whois.adamsnames.tc"),
                            new Cserver_whois(".tf","whois.adamsnames.tc"),
                            new Cserver_whois(".th","whois.thnic.net"),
                            new Cserver_whois(".tj","whois.nic.tj"),
                            new Cserver_whois(".tn","whois.ripe.net"),
                            new Cserver_whois(".to","whois.tonic.to"),
                            new Cserver_whois(".tr","whois.ripe.net"),
                            new Cserver_whois(".tw","whois.twnic.net"),
                            new Cserver_whois(".ua","whois.ripe.net"),
                            new Cserver_whois(".uk","whois.nic.uk"),
                            new Cserver_whois(".us","whois.isi.edu"),
                            new Cserver_whois(".va","whois.ripe.net"),
                            new Cserver_whois(".vg","whois.adamsnames.tc"),
                            new Cserver_whois(".ws","whois.nic.ws"),
                                //# centralnic pseudo-TLDs 
                            new Cserver_whois(".br.com","whois.centralnic.com"),
                            new Cserver_whois(".cn.com","whois.centralnic.com"),
                            new Cserver_whois(".de.com","whois.centralnic.com"),
                            new Cserver_whois(".eu.com","whois.centralnic.com"),
                            new Cserver_whois(".gb.com","whois.centralnic.com"),
                            new Cserver_whois(".gb.net","whois.centralnic.com"),
                            new Cserver_whois(".hu.com","whois.centralnic.com"),
                            new Cserver_whois(".no.com","whois.centralnic.com"),
                            new Cserver_whois(".qc.com","whois.centralnic.com"),
                            new Cserver_whois(".ru.com","whois.centralnic.com"),
                            new Cserver_whois(".sa.com","whois.centralnic.com"),
                            new Cserver_whois(".se.com","whois.centralnic.com"),
                            new Cserver_whois(".se.net","whois.centralnic.com"),
                            new Cserver_whois(".uk.com","whois.centralnic.com"),
                            new Cserver_whois(".uk.net","whois.centralnic.com"),
                            new Cserver_whois(".us.com","whois.centralnic.com"),
                            new Cserver_whois(".uy.com","whois.centralnic.com"),
                            new Cserver_whois(".za.com","whois.centralnic.com"),
                                //# Traditional GTLDs 
                            new Cserver_whois(".com","whois.crsnic.net"),
                            new Cserver_whois(".net","whois.crsnic.net"),
                            new Cserver_whois(".org","whois.crsnic.net"),
                            new Cserver_whois(".edu","whois.internic.net"),
                            new Cserver_whois(".gov","whois.nic.gov"),
                            new Cserver_whois(".int","whois.iana.org"),
                            new Cserver_whois(".mil","whois.nic.mil"),
                                //# New (2001/2002) GTLDs
                            new Cserver_whois(".us","whois.nic.us"),
                            new Cserver_whois(".biz","whois.neulevel.biz"),
                            new Cserver_whois(".info","whois.afilias.net"),
                            new Cserver_whois(".name","whois.nic.name"),
                            new Cserver_whois(".aero","whois.nic.aero"),
                            new Cserver_whois(".coop","whois.nic.coop"),
                            new Cserver_whois(".museum","whois.museum")
                            };
        #endregion

        private System.Windows.Forms.Button button_whois;
        private System.Windows.Forms.TextBox textBox_whois_ip;
        private System.Windows.Forms.Label label18;
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.TextBox textBox_whois_server;
        private System.Windows.Forms.RadioButton radioButton_whois_use_following;
        private System.Windows.Forms.RadioButton radioButton_whois_auto_find;

        private easy_socket.tcp.Socket_Data socket;
        private string data_to_send;
        public FormWhois(string ip,bool autofind,string other_server)
        {
            InitializeComponent();
            XPStyle.MakeXPStyle(this);

            this.textBox_whois_ip.Text=ip;
            this.radioButton_whois_auto_find.Checked=autofind;
            this.radioButton_whois_use_following.Checked=!autofind;
            this.textBox_whois_server.Text=other_server;
            this.textBox_whois_server.Enabled=!autofind;
            this.socket=new easy_socket.tcp.Socket_Data();
            this.socket.event_Socket_Data_Closed_by_Remote_Side+= new easy_socket.tcp.Socket_Data_Closed_by_Remote_Side_EventHandler(socket_closed_by_remote_side);
            this.socket.event_Socket_Data_Connected_To_Remote_Host +=new easy_socket.tcp.Socket_Data_Connected_To_Remote_Host_EventHandler(socket_connected_to_remote_host);
            this.socket.event_Socket_Data_DataArrival +=new easy_socket.tcp.Socket_Data_DataArrival_EventHandler(socket_data_arrival);
            this.socket.event_Socket_Data_Error+=new easy_socket.tcp.Socket_Data_Error_EventHandler(socket_error);

            this.data_to_send="";

            string filename=System.Environment.CurrentDirectory+"\\"+WHOIS_SERVER_LIST_FILE;
            this.load_server_list(filename);
            // send whois
            this.button_whois_Click(this,null);
        }
        
        protected override void Dispose( bool disposing )
        {
            this.socket.close();
            this.socket.event_Socket_Data_Closed_by_Remote_Side-= new easy_socket.tcp.Socket_Data_Closed_by_Remote_Side_EventHandler(socket_closed_by_remote_side);
            this.socket.event_Socket_Data_Connected_To_Remote_Host-=new easy_socket.tcp.Socket_Data_Connected_To_Remote_Host_EventHandler(socket_connected_to_remote_host);
            this.socket.event_Socket_Data_DataArrival-=new easy_socket.tcp.Socket_Data_DataArrival_EventHandler(socket_data_arrival);
            this.socket.event_Socket_Data_Error-=new easy_socket.tcp.Socket_Data_Error_EventHandler(socket_error);
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
            this.button_whois = new System.Windows.Forms.Button();
            this.textBox_whois_ip = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.textBox_whois_server = new System.Windows.Forms.TextBox();
            this.radioButton_whois_use_following = new System.Windows.Forms.RadioButton();
            this.radioButton_whois_auto_find = new System.Windows.Forms.RadioButton();
            this.panel.SuspendLayout();
            this.panel_control.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Size = new System.Drawing.Size(408, 230);
            this.panel.Visible = true;
            // 
            // textBox_editable
            // 
            this.textBox_editable.Location = new System.Drawing.Point(0, 226);
            this.textBox_editable.Size = new System.Drawing.Size(232, 75);
            // 
            // panel_control
            // 
            this.panel_control.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                        this.textBox_whois_server,
                                                                                        this.radioButton_whois_use_following,
                                                                                        this.radioButton_whois_auto_find,
                                                                                        this.button_whois,
                                                                                        this.textBox_whois_ip,
                                                                                        this.label18});
            this.panel_control.Size = new System.Drawing.Size(144, 230);
            this.panel_control.Visible = true;
            // 
            // textBox_telnet
            // 
            this.textBox_telnet.Size = new System.Drawing.Size(232, 226);
            this.textBox_telnet.Visible = true;
            // 
            // button_whois
            // 
            this.button_whois.Location = new System.Drawing.Point(32, 152);
            this.button_whois.Name = "button_whois";
            this.button_whois.TabIndex = 5;
            this.button_whois.Text = "Whois";
            this.button_whois.Click += new System.EventHandler(this.button_whois_Click);
            // 
            // textBox_whois_ip
            // 
            this.textBox_whois_ip.Location = new System.Drawing.Point(40, 16);
            this.textBox_whois_ip.Name = "textBox_whois_ip";
            this.textBox_whois_ip.TabIndex = 4;
            this.textBox_whois_ip.Text = "";
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(8, 16);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(40, 16);
            this.label18.TabIndex = 3;
            this.label18.Text = "IP";
            // 
            // textBox_whois_server
            // 
            this.textBox_whois_server.Enabled = false;
            this.textBox_whois_server.Location = new System.Drawing.Point(8, 120);
            this.textBox_whois_server.Name = "textBox_whois_server";
            this.textBox_whois_server.Size = new System.Drawing.Size(128, 20);
            this.textBox_whois_server.TabIndex = 8;
            this.textBox_whois_server.Text = "127.0.0.1";
            // 
            // radioButton_whois_use_following
            // 
            this.radioButton_whois_use_following.Location = new System.Drawing.Point(8, 88);
            this.radioButton_whois_use_following.Name = "radioButton_whois_use_following";
            this.radioButton_whois_use_following.Size = new System.Drawing.Size(128, 32);
            this.radioButton_whois_use_following.TabIndex = 7;
            this.radioButton_whois_use_following.Text = "Use the following server";
            this.radioButton_whois_use_following.CheckedChanged += new System.EventHandler(this.radioButton_whois_use_following_CheckedChanged);
            // 
            // radioButton_whois_auto_find
            // 
            this.radioButton_whois_auto_find.Checked = true;
            this.radioButton_whois_auto_find.Location = new System.Drawing.Point(8, 48);
            this.radioButton_whois_auto_find.Name = "radioButton_whois_auto_find";
            this.radioButton_whois_auto_find.Size = new System.Drawing.Size(128, 40);
            this.radioButton_whois_auto_find.TabIndex = 6;
            this.radioButton_whois_auto_find.TabStop = true;
            this.radioButton_whois_auto_find.Text = "Find whois server automatically";
            // 
            // FormWhois
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(408, 230);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.panel_control,
                                                                          this.textBox_telnet,
                                                                          this.panel});
            this.Name = "FormWhois";
            this.Text = "Whois";
            this.panel.ResumeLayout(false);
            this.panel_control.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private void load_server_list(string filename)
        {
            if (!System.IO.File.Exists(filename))
            {
                // save default servers
                this.save_server_list(filename);
                return;
            }
            this.whois_server_list=(Cserver_whois[])(XML_access.XMLDeserializeObject(filename,typeof(Cserver_whois[])));
        }
        private void save_server_list(string filename)
        {
            // save the default list to allow people to add their one server without retyping all list or build again soft
            XML_access.XMLSerializeObject(filename,this.whois_server_list,typeof(Cserver_whois[]));
        }

        protected void socket_closed_by_remote_side(easy_socket.tcp.Socket_Data sender, EventArgs e)
        {
            this.panel_control.Enabled=true;
            this.textBox_telnet_add("Connection closed by remote side.\r\n");
        }

        protected void socket_connected_to_remote_host(easy_socket.tcp.Socket_Data sender, EventArgs e)
        {
            
            if (socket.LocalIPEndPoint!=null)
            {
                this.Text+=" local port:"+socket.LocalIPEndPoint.Port.ToString();
            }

            this.textBox_telnet_add("Connected to remote host.\r\n");
            // host ip
            this.socket.send(this.data_to_send+"\r\n");
        }

        protected void socket_data_arrival(easy_socket.tcp.Socket_Data sender, easy_socket.tcp.EventArgs_ReceiveDataSocket e)
        {
            string strdata=System.Text.Encoding.Default.GetString(e.buffer , 0, e.buffer_size );
            this.textBox_telnet_add(strdata+"\r\n");
        }
        protected void socket_error(easy_socket.tcp.Socket_Data sender, easy_socket.tcp.EventArgs_Exception e)
        {
            this.textBox_telnet_add("Socket Error: "+e.exception.Message+"\r\n");
            this.panel_control.Enabled=true;
        }

        private string find_best_whois_server(string ip)
        {

            // as the whois_server_list is ordered we can return the first result
            for (int cpt=0;cpt<this.whois_server_list.GetLength(0);cpt++)
            {
                if (ip.EndsWith(this.whois_server_list[cpt].extension))
                    return this.whois_server_list[cpt].server;
            }

            // no result
            this.textBox_telnet_add("Can't find a whois server for this ip in common whois servers list.");
            return "";
        }
        string dns_resolve(string ip)
        {
            //if ip with number try to resolve
            try
            {
                System.Net.IPAddress.Parse(ip);
                // if ok resolve
                try
                {
                    System.Net.IPHostEntry iphe=System.Net.Dns.Resolve(ip);
                    this.textBox_telnet_set("Name:\t"+iphe.HostName+"\r\n");
                    this.textBox_telnet_add("Address:\r\n");
                    for (int cpt=0;cpt<iphe.AddressList.Length;cpt++)
                        this.textBox_telnet_add("\t"+iphe.AddressList[cpt]+"\r\n");
                    if (iphe.Aliases.Length>0)
                    {
                        this.textBox_telnet_add("Aliases:\r\n");
                        for (int cpt=0;cpt<iphe.Aliases.Length;cpt++)
                            this.textBox_telnet_add("\t"+iphe.Aliases[cpt]+"\r\n");
                    }
                    // take ip in string
                    return iphe.HostName;
                }
                catch (Exception ex)
                {
                    this.textBox_telnet_set("Error can't resolve.\r\n");
                    this.textBox_telnet_add(ex.Message);
                    return "";
                }
            }
            catch
            {}
            // at this point ip doesn't contain numbers
            return ip;
        }
        private void radioButton_whois_use_following_CheckedChanged(object sender, System.EventArgs e)
        {
            this.textBox_whois_server.Enabled=this.radioButton_whois_use_following.Checked;
        }

        private void button_whois_Click(object sender, System.EventArgs e)
        {
            this.textBox_telnet_set("");
            string ip=this.textBox_whois_ip.Text.Trim();
            if (ip=="")
                return;
            if (ip.StartsWith("http:")||ip.StartsWith("www.")||ip.StartsWith("ftp."))
            {
                if (MessageBox.Show(this,"Ip shouldn't in most of case begin with 'http:' or 'www.' or 'ftp.'.\r\nDo you want to continue","Warning",
                    MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes)
                    return;
            }
            this.panel_control.Enabled=false;
            ip=dns_resolve(ip);// get ip with name server
            if (ip=="")
                return;

            string whois_server;
            if (this.radioButton_whois_auto_find.Checked)
                whois_server=find_best_whois_server(ip);
            else
                whois_server=this.textBox_whois_server.Text;

            this.data_to_send=ip;
            if (whois_server=="")
            {
                this.panel_control.Enabled=true;
                return;
            }
            this.textBox_telnet_add("Whois server:\t"+whois_server+"\r\n");
            this.socket.connect(whois_server,43);
            this.panel_control.Enabled=false;
        }
    }
    
}
