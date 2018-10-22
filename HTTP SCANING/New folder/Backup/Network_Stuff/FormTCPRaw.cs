using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Network_Stuff
{
    /// <summary>
    /// Description résumée de FormTCPRaw.
    /// </summary>
    public class FormTCPRaw : System.Windows.Forms.Form
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox_raw_data;
        private easy_socket.tcp_header.tcp_header_server tcp_hs;
        public FormTCPRaw()
        {
            //
            // Requis pour la prise en charge du Concepteur Windows Forms
            //
            InitializeComponent();
            Tools.GUI.XPStyle.MakeXPStyle(this);
            //
            // TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
            //
            tcp_hs=new easy_socket.tcp_header.tcp_header_server();
            tcp_hs.event_Data_Arrival+=new easy_socket.tcp_header.Socket_Data_Arrival_EventHandler(data_arrival);
            tcp_hs.event_Socket_Error+=new easy_socket.tcp_header.Socket_Error_EventHandler(data_error);
            tcp_hs.sniff_outgoing_packets=true;
            tcp_hs.start("10.0.0.1",80);

        }

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            tcp_hs.event_Data_Arrival-=new easy_socket.tcp_header.Socket_Data_Arrival_EventHandler(data_arrival);
            tcp_hs.event_Socket_Error-=new easy_socket.tcp_header.Socket_Error_EventHandler(data_error);
            tcp_hs.stop();
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox_raw_data = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(336, 8);
            this.button1.Name = "button1";
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox_raw_data
            // 
            this.textBox_raw_data.Multiline = true;
            this.textBox_raw_data.Name = "textBox_raw_data";
            this.textBox_raw_data.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_raw_data.Size = new System.Drawing.Size(304, 312);
            this.textBox_raw_data.TabIndex = 0;
            this.textBox_raw_data.Text = "";
            // 
            // FormTCPRaw
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(544, 318);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.button1,
                                                                          this.textBox_raw_data});
            this.Name = "FormTCPRaw";
            this.Text = "FormTCPRaw";
            this.ResumeLayout(false);

        }
        #endregion
        bool b_;
        private void data_arrival(easy_socket.tcp_header.tcp_header_server sender, easy_socket.tcp_header.EventArgs_ipv4header_ReceiveData e)
        {
            if (b_)
            {
                if (e.ipv4header.DestinationAddress=="10.0.0.1")
                {
                    easy_socket.tcp_header.tcp_reply_header tcprh=new easy_socket.tcp_header.tcp_reply_header();
                    tcprh.receive(e.ipv4header.source_address,e.ipv4header.destination_address,sender);
                    //byte[] b=tcprh.encode(e.ipv4header.destination_address,e.ipv4header.source_address);
                    tcprh.URG=true;
                    tcprh.send(e.ipv4header.destination_address,e.ipv4header.source_address);
                    b_=false;
                }
            }

            this.textBox_raw_data.Text+="\r\nsrc_addr "+ e.ipv4header.SourceAddress;
            this.textBox_raw_data.Text+="\r\ndst_addr "+ e.ipv4header.DestinationAddress;
            this.textBox_raw_data.Text+="\r\nsrc_port "+ sender.SourcePort.ToString();
            this.textBox_raw_data.Text+="\r\ndst port "+ sender.DestinationPort.ToString();
            this.textBox_raw_data.Text+="\r\nsyn "+ sender.SYN.ToString();
            this.textBox_raw_data.Text+="\r\nack "+ sender.ACK.ToString();
            this.textBox_raw_data.Text+="\r\nreset "+ sender.RST.ToString();
            this.textBox_raw_data.Text+="\r\nfin "+ sender.FIN.ToString();
            this.textBox_raw_data.Text+="\r\npush "+ sender.PSH.ToString();
            this.textBox_raw_data.Text+="\r\nurgent "+ sender.URG.ToString();
            this.textBox_raw_data.Text+="\r\nseq_num "+ sender.SequenceNumber.ToString();
            this.textBox_raw_data.Text+="\r\nackno_num "+ sender.AcknowledgmentNumber.ToString();
            this.textBox_raw_data.Text+="\r\nwindow_size "+ sender.Window.ToString();

            string strdata=System.Text.Encoding.Default.GetString(sender.data);
            this.textBox_raw_data.Text+="\r\ndata "+ strdata;
        }
        private void data_error(easy_socket.tcp_header.tcp_header sender, easy_socket.tcp_header.EventArgs_Exception e)
        {
            this.textBox_raw_data.Text+=e.exception.Message;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            easy_socket.tcp_header.tcp_header_client c=new easy_socket.tcp_header.tcp_header_client();
            c.SYN=true;
            c.SequenceNumber=(uint)System.Environment.TickCount;
            c.DestinationPort=80;
            c.SourcePort=80;
            c.URG=true;
            c.send("10.0.0.1","10.0.0.138");//"216.239.59.104");
            b_=true;
        }

    }
}
