using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Network_Stuff
{
	/// <summary>
	/// Summary description for CommonInteractiveForm.
	/// </summary>
	public class CommonInteractiveForm : System.Windows.Forms.Form
	{

        #region design
        protected System.Windows.Forms.Panel panel_interactive;
        protected System.Windows.Forms.Panel panel_cmd;
        protected System.Windows.Forms.Panel panel1;
        protected System.Windows.Forms.TextBox textBox_editable;
        protected System.Windows.Forms.Panel panel2;
        protected System.Windows.Forms.Button button_clear;
        protected System.Windows.Forms.GroupBox groupBox_send_data_options;
        protected System.Windows.Forms.RadioButton radioButton_send_to_clt;
        protected System.Windows.Forms.RadioButton radioButton_send_to_srv;
        protected System.Windows.Forms.GroupBox groupBox_clt_to_srv_options;
        protected System.Windows.Forms.RadioButton radioButton_clt_to_srv_ask;
        protected System.Windows.Forms.RadioButton radioButton_clt_to_srv_block;
        protected System.Windows.Forms.RadioButton radioButton_clt_to_srv_allow;
        protected System.Windows.Forms.GroupBox groupBox_srv_to_clt_options;
        protected System.Windows.Forms.RadioButton radioButton_srv_to_clt_ask;
        protected System.Windows.Forms.RadioButton radioButton_srv_to_clt_block;
        protected System.Windows.Forms.RadioButton radioButton_srv_to_clt_allow;
        protected System.Windows.Forms.Button button_close;
        protected System.Windows.Forms.Button button_send;
        protected System.Windows.Forms.CheckBox checkBox_send_hexa_data;
        protected System.Windows.Forms.CheckBox checkBox_transmit_close;
        protected System.Windows.Forms.GroupBox groupBox_close;
        protected System.Windows.Forms.CheckBox checkBox_close_server_socket;
        protected System.Windows.Forms.CheckBox checkBox_close_client_socket;
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CommonInteractiveForm));
            this.panel_interactive = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_editable = new System.Windows.Forms.TextBox();
            this.panel_cmd = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox_close = new System.Windows.Forms.GroupBox();
            this.checkBox_close_client_socket = new System.Windows.Forms.CheckBox();
            this.checkBox_close_server_socket = new System.Windows.Forms.CheckBox();
            this.checkBox_transmit_close = new System.Windows.Forms.CheckBox();
            this.button_clear = new System.Windows.Forms.Button();
            this.groupBox_send_data_options = new System.Windows.Forms.GroupBox();
            this.radioButton_send_to_clt = new System.Windows.Forms.RadioButton();
            this.radioButton_send_to_srv = new System.Windows.Forms.RadioButton();
            this.groupBox_clt_to_srv_options = new System.Windows.Forms.GroupBox();
            this.radioButton_clt_to_srv_ask = new System.Windows.Forms.RadioButton();
            this.radioButton_clt_to_srv_block = new System.Windows.Forms.RadioButton();
            this.radioButton_clt_to_srv_allow = new System.Windows.Forms.RadioButton();
            this.groupBox_srv_to_clt_options = new System.Windows.Forms.GroupBox();
            this.radioButton_srv_to_clt_ask = new System.Windows.Forms.RadioButton();
            this.radioButton_srv_to_clt_block = new System.Windows.Forms.RadioButton();
            this.radioButton_srv_to_clt_allow = new System.Windows.Forms.RadioButton();
            this.button_close = new System.Windows.Forms.Button();
            this.button_send = new System.Windows.Forms.Button();
            this.checkBox_send_hexa_data = new System.Windows.Forms.CheckBox();
            this.hexview = new Tools.GUI.Controls.HexViewer.HexViewer();
            this.panel_interactive.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel_cmd.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox_close.SuspendLayout();
            this.groupBox_send_data_options.SuspendLayout();
            this.groupBox_clt_to_srv_options.SuspendLayout();
            this.groupBox_srv_to_clt_options.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_interactive
            // 
            this.panel_interactive.Controls.Add(this.panel1);
            this.panel_interactive.Controls.Add(this.panel_cmd);
            this.panel_interactive.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_interactive.Location = new System.Drawing.Point(0, 246);
            this.panel_interactive.Name = "panel_interactive";
            this.panel_interactive.Size = new System.Drawing.Size(560, 160);
            this.panel_interactive.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox_editable);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 80);
            this.panel1.TabIndex = 15;
            // 
            // textBox_editable
            // 
            this.textBox_editable.AcceptsReturn = true;
            this.textBox_editable.AcceptsTab = true;
            this.textBox_editable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_editable.Location = new System.Drawing.Point(0, 0);
            this.textBox_editable.Multiline = true;
            this.textBox_editable.Name = "textBox_editable";
            this.textBox_editable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_editable.Size = new System.Drawing.Size(560, 80);
            this.textBox_editable.TabIndex = 12;
            this.textBox_editable.Text = "";
            // 
            // panel_cmd
            // 
            this.panel_cmd.Controls.Add(this.panel2);
            this.panel_cmd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_cmd.Location = new System.Drawing.Point(0, 80);
            this.panel_cmd.Name = "panel_cmd";
            this.panel_cmd.Size = new System.Drawing.Size(560, 80);
            this.panel_cmd.TabIndex = 14;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox_close);
            this.panel2.Controls.Add(this.checkBox_transmit_close);
            this.panel2.Controls.Add(this.button_clear);
            this.panel2.Controls.Add(this.groupBox_send_data_options);
            this.panel2.Controls.Add(this.groupBox_clt_to_srv_options);
            this.panel2.Controls.Add(this.groupBox_srv_to_clt_options);
            this.panel2.Controls.Add(this.button_close);
            this.panel2.Controls.Add(this.button_send);
            this.panel2.Controls.Add(this.checkBox_send_hexa_data);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(-24, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(584, 80);
            this.panel2.TabIndex = 0;
            // 
            // groupBox_close
            // 
            this.groupBox_close.Controls.Add(this.checkBox_close_client_socket);
            this.groupBox_close.Controls.Add(this.checkBox_close_server_socket);
            this.groupBox_close.Location = new System.Drawing.Point(224, 24);
            this.groupBox_close.Name = "groupBox_close";
            this.groupBox_close.Size = new System.Drawing.Size(152, 56);
            this.groupBox_close.TabIndex = 30;
            this.groupBox_close.TabStop = false;
            this.groupBox_close.Text = "Close button action";
            // 
            // checkBox_close_client_socket
            // 
            this.checkBox_close_client_socket.Checked = true;
            this.checkBox_close_client_socket.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_close_client_socket.Location = new System.Drawing.Point(8, 32);
            this.checkBox_close_client_socket.Name = "checkBox_close_client_socket";
            this.checkBox_close_client_socket.Size = new System.Drawing.Size(128, 16);
            this.checkBox_close_client_socket.TabIndex = 1;
            this.checkBox_close_client_socket.Text = "Close Client Socket";
            // 
            // checkBox_close_server_socket
            // 
            this.checkBox_close_server_socket.Checked = true;
            this.checkBox_close_server_socket.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_close_server_socket.Location = new System.Drawing.Point(8, 16);
            this.checkBox_close_server_socket.Name = "checkBox_close_server_socket";
            this.checkBox_close_server_socket.Size = new System.Drawing.Size(128, 16);
            this.checkBox_close_server_socket.TabIndex = 0;
            this.checkBox_close_server_socket.Text = "Close Server Socket";
            // 
            // checkBox_transmit_close
            // 
            this.checkBox_transmit_close.Checked = true;
            this.checkBox_transmit_close.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_transmit_close.Location = new System.Drawing.Point(224, 8);
            this.checkBox_transmit_close.Name = "checkBox_transmit_close";
            this.checkBox_transmit_close.Size = new System.Drawing.Size(152, 16);
            this.checkBox_transmit_close.TabIndex = 29;
            this.checkBox_transmit_close.Text = "Transmit socket closing";
            // 
            // button_clear
            // 
            this.button_clear.CausesValidation = false;
            this.button_clear.Location = new System.Drawing.Point(496, 32);
            this.button_clear.Name = "button_clear";
            this.button_clear.TabIndex = 28;
            this.button_clear.Text = "Clear";
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
            // 
            // groupBox_send_data_options
            // 
            this.groupBox_send_data_options.Controls.Add(this.radioButton_send_to_clt);
            this.groupBox_send_data_options.Controls.Add(this.radioButton_send_to_srv);
            this.groupBox_send_data_options.Location = new System.Drawing.Point(376, 24);
            this.groupBox_send_data_options.Name = "groupBox_send_data_options";
            this.groupBox_send_data_options.Size = new System.Drawing.Size(112, 56);
            this.groupBox_send_data_options.TabIndex = 27;
            this.groupBox_send_data_options.TabStop = false;
            this.groupBox_send_data_options.Text = "Send data to";
            // 
            // radioButton_send_to_clt
            // 
            this.radioButton_send_to_clt.Location = new System.Drawing.Point(16, 32);
            this.radioButton_send_to_clt.Name = "radioButton_send_to_clt";
            this.radioButton_send_to_clt.Size = new System.Drawing.Size(64, 16);
            this.radioButton_send_to_clt.TabIndex = 1;
            this.radioButton_send_to_clt.Text = "Client";
            // 
            // radioButton_send_to_srv
            // 
            this.radioButton_send_to_srv.Checked = true;
            this.radioButton_send_to_srv.Location = new System.Drawing.Point(16, 16);
            this.radioButton_send_to_srv.Name = "radioButton_send_to_srv";
            this.radioButton_send_to_srv.Size = new System.Drawing.Size(64, 16);
            this.radioButton_send_to_srv.TabIndex = 0;
            this.radioButton_send_to_srv.TabStop = true;
            this.radioButton_send_to_srv.Text = "Server";
            // 
            // groupBox_clt_to_srv_options
            // 
            this.groupBox_clt_to_srv_options.Controls.Add(this.radioButton_clt_to_srv_ask);
            this.groupBox_clt_to_srv_options.Controls.Add(this.radioButton_clt_to_srv_block);
            this.groupBox_clt_to_srv_options.Controls.Add(this.radioButton_clt_to_srv_allow);
            this.groupBox_clt_to_srv_options.Location = new System.Drawing.Point(32, 40);
            this.groupBox_clt_to_srv_options.Name = "groupBox_clt_to_srv_options";
            this.groupBox_clt_to_srv_options.Size = new System.Drawing.Size(189, 40);
            this.groupBox_clt_to_srv_options.TabIndex = 26;
            this.groupBox_clt_to_srv_options.TabStop = false;
            this.groupBox_clt_to_srv_options.Text = "Client to Server data transfert";
            // 
            // radioButton_clt_to_srv_ask
            // 
            this.radioButton_clt_to_srv_ask.Location = new System.Drawing.Point(72, 16);
            this.radioButton_clt_to_srv_ask.Name = "radioButton_clt_to_srv_ask";
            this.radioButton_clt_to_srv_ask.Size = new System.Drawing.Size(56, 16);
            this.radioButton_clt_to_srv_ask.TabIndex = 2;
            this.radioButton_clt_to_srv_ask.Text = "Ask";
            // 
            // radioButton_clt_to_srv_block
            // 
            this.radioButton_clt_to_srv_block.Location = new System.Drawing.Point(128, 16);
            this.radioButton_clt_to_srv_block.Name = "radioButton_clt_to_srv_block";
            this.radioButton_clt_to_srv_block.Size = new System.Drawing.Size(56, 16);
            this.radioButton_clt_to_srv_block.TabIndex = 1;
            this.radioButton_clt_to_srv_block.Text = "Block";
            // 
            // radioButton_clt_to_srv_allow
            // 
            this.radioButton_clt_to_srv_allow.Checked = true;
            this.radioButton_clt_to_srv_allow.Location = new System.Drawing.Point(8, 16);
            this.radioButton_clt_to_srv_allow.Name = "radioButton_clt_to_srv_allow";
            this.radioButton_clt_to_srv_allow.Size = new System.Drawing.Size(56, 16);
            this.radioButton_clt_to_srv_allow.TabIndex = 0;
            this.radioButton_clt_to_srv_allow.TabStop = true;
            this.radioButton_clt_to_srv_allow.Text = "Allow";
            // 
            // groupBox_srv_to_clt_options
            // 
            this.groupBox_srv_to_clt_options.Controls.Add(this.radioButton_srv_to_clt_ask);
            this.groupBox_srv_to_clt_options.Controls.Add(this.radioButton_srv_to_clt_block);
            this.groupBox_srv_to_clt_options.Controls.Add(this.radioButton_srv_to_clt_allow);
            this.groupBox_srv_to_clt_options.Location = new System.Drawing.Point(32, 0);
            this.groupBox_srv_to_clt_options.Name = "groupBox_srv_to_clt_options";
            this.groupBox_srv_to_clt_options.Size = new System.Drawing.Size(189, 40);
            this.groupBox_srv_to_clt_options.TabIndex = 25;
            this.groupBox_srv_to_clt_options.TabStop = false;
            this.groupBox_srv_to_clt_options.Text = "Server to Client data transfert";
            // 
            // radioButton_srv_to_clt_ask
            // 
            this.radioButton_srv_to_clt_ask.Location = new System.Drawing.Point(72, 16);
            this.radioButton_srv_to_clt_ask.Name = "radioButton_srv_to_clt_ask";
            this.radioButton_srv_to_clt_ask.Size = new System.Drawing.Size(56, 16);
            this.radioButton_srv_to_clt_ask.TabIndex = 2;
            this.radioButton_srv_to_clt_ask.Text = "Ask";
            // 
            // radioButton_srv_to_clt_block
            // 
            this.radioButton_srv_to_clt_block.Location = new System.Drawing.Point(128, 16);
            this.radioButton_srv_to_clt_block.Name = "radioButton_srv_to_clt_block";
            this.radioButton_srv_to_clt_block.Size = new System.Drawing.Size(56, 16);
            this.radioButton_srv_to_clt_block.TabIndex = 1;
            this.radioButton_srv_to_clt_block.Text = "Block";
            // 
            // radioButton_srv_to_clt_allow
            // 
            this.radioButton_srv_to_clt_allow.Checked = true;
            this.radioButton_srv_to_clt_allow.Location = new System.Drawing.Point(8, 16);
            this.radioButton_srv_to_clt_allow.Name = "radioButton_srv_to_clt_allow";
            this.radioButton_srv_to_clt_allow.Size = new System.Drawing.Size(56, 16);
            this.radioButton_srv_to_clt_allow.TabIndex = 0;
            this.radioButton_srv_to_clt_allow.TabStop = true;
            this.radioButton_srv_to_clt_allow.Text = "Allow";
            // 
            // button_close
            // 
            this.button_close.Location = new System.Drawing.Point(496, 56);
            this.button_close.Name = "button_close";
            this.button_close.TabIndex = 24;
            this.button_close.Text = "Close";
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // button_send
            // 
            this.button_send.CausesValidation = false;
            this.button_send.Location = new System.Drawing.Point(496, 8);
            this.button_send.Name = "button_send";
            this.button_send.TabIndex = 23;
            this.button_send.Text = "Send";
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // checkBox_send_hexa_data
            // 
            this.checkBox_send_hexa_data.Location = new System.Drawing.Point(376, 8);
            this.checkBox_send_hexa_data.Name = "checkBox_send_hexa_data";
            this.checkBox_send_hexa_data.Size = new System.Drawing.Size(104, 16);
            this.checkBox_send_hexa_data.TabIndex = 22;
            this.checkBox_send_hexa_data.Text = "Send hexa data";
            this.checkBox_send_hexa_data.CheckedChanged += new System.EventHandler(this.checkBox_send_hexa_data_CheckedChanged);
            // 
            // hexview
            // 
            this.hexview.AutoRefresh = true;
            this.hexview.BlockLength = 2;
            this.hexview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hexview.Location = new System.Drawing.Point(0, 0);
            this.hexview.Name = "hexview";
            this.hexview.SelectionLength = 0;
            this.hexview.SelectionStart = 0;
            this.hexview.Size = new System.Drawing.Size(560, 246);
            this.hexview.TabIndex = 1;
            this.hexview.VisibleAddr = true;
            // 
            // CommonInteractiveForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(560, 406);
            this.Controls.Add(this.hexview);
            this.Controls.Add(this.panel_interactive);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CommonInteractiveForm";
            this.Text = "TCP Interactive";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FormTCPInteractive_Closing);
            this.panel_interactive.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel_cmd.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox_close.ResumeLayout(false);
            this.groupBox_send_data_options.ResumeLayout(false);
            this.groupBox_clt_to_srv_options.ResumeLayout(false);
            this.groupBox_srv_to_clt_options.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        public void set_mdi_parent(System.Windows.Forms.Form MDIparent)
        {
            this.MdiParent=MDIparent;
        }

        protected void enable_state(bool enable)
        {
            this.button_send.Enabled=enable;
            this.textBox_editable.Enabled=enable;
            this.button_clear.Enabled=enable;
        }

        #endregion

        #region constructor / destructor
        public CommonInteractiveForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            Tools.GUI.XPStyle.MakeXPStyle(this);
            this.hexview.AutoRefresh=false;
            this.hexview.ScrollToEnd=true;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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

        #endregion

        #region members
        private Tools.GUI.Controls.HexViewer.HexViewer hexview;

        #endregion

        protected string split_in_multiple_lines(string str,int lines_length)
        {
            string str_res="";
            int cnt;
            int nb_lines=str.Length/lines_length;
            for (cnt=0;cnt<nb_lines;cnt++)
                str_res+=str.Substring(cnt*lines_length,lines_length)+"\r\n";
            if (str.Length%lines_length!=0)
                str_res+=str.Substring(cnt*lines_length)+"\r\n";
            return str_res;
        }

        protected void add_info(string info)
        {
            this.hexview.AddComment(info,System.Drawing.Color.Green);
        }
        protected void add_data(byte[] data,System.Drawing.Color color)
        {
            this.hexview.AddData(data,color);
        }
        protected void refresh_data()
        {
            this.hexview.RefreshData();
        }

        #region GUI events
        protected void checkBox_send_hexa_data_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.checkBox_send_hexa_data.Checked)
                this.textBox_editable.Text=easy_socket.hexa_convert.string_to_hexa(this.textBox_editable.Text);
            else
                this.textBox_editable.Text=easy_socket.hexa_convert.hexa_to_string(this.textBox_editable.Text);        
        }

        protected virtual void button_close_Click(object sender, System.EventArgs e)
        {

        }

        protected byte[] get_data()
        {
            if (this.textBox_editable.Text.Length==0)
                return null;
            if(this.checkBox_send_hexa_data.Checked)
                return easy_socket.hexa_convert.hexa_to_byte(this.textBox_editable.Text);
            //else
            return System.Text.Encoding.ASCII.GetBytes(this.textBox_editable.Text);
        }

        protected virtual void button_send_Click(object sender, System.EventArgs e)
        {

        }

        protected virtual  void button_clear_Click(object sender, System.EventArgs e)
        {
            this.hexview.Clear();
        }
        protected virtual  void FormTCPInteractive_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        #endregion

	}
}
