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
    /// Description résumée de FormAddIPEntry.
    /// </summary>
    public class FormAddIPEntry : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.TextBox textBox_MAC;
        private System.Windows.Forms.TextBox textBox_adaptater_index;
        private System.Windows.Forms.ComboBox comboBox_entry_type;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;

        private System.ComponentModel.Container components = null;


        private bool b_create;
        public FormAddIPEntry()
        {
            this.common_constructor();
            this.b_create=true;
        }
        public FormAddIPEntry(iphelper.MIB_IPNETROW mib_i)
        {
            this.common_constructor();
            this.b_create=false;
            this.textBox_IP.Text=new System.Net.IPAddress(mib_i.dwAddr).ToString();
            string str_mac_addr=easy_socket.hexa_convert.byte_to_hexa(mib_i.bPhysAddr);
            if (str_mac_addr.Length>mib_i.dwPhysAddrLen*3)
                str_mac_addr=str_mac_addr.Substring(0,(int)(mib_i.dwPhysAddrLen*3-1));//*3 for to ascii chars and separator, -1 to remove last separator
            this.textBox_MAC.Text=str_mac_addr;
            this.textBox_adaptater_index.Text=mib_i.dwIndex.ToString();
            switch (mib_i.dwType)
            {
                case iphelper.CMIB_IPNETROW.dwType_Dynamic:
                    this.comboBox_entry_type.Text="Dynamic";
                    break;
                case iphelper.CMIB_IPNETROW.dwType_Invalid:
                    this.comboBox_entry_type.Text="Invalid";
                    break;
                case iphelper.CMIB_IPNETROW.dwType_Other:
                    this.comboBox_entry_type.Text="Other";
                    break;
                case iphelper.CMIB_IPNETROW.dwType_Static:
                    this.comboBox_entry_type.Text="Static";
                    break;
                default:
                    this.comboBox_entry_type.Text="Dynamic";
                    break;
            }

        }
        private void common_constructor()
        {
            InitializeComponent();
            Tools.GUI.XPStyle.MakeXPStyle(this);
            if (this.comboBox_entry_type.Items.Count>0)
                this.comboBox_entry_type.SelectedIndex=0;
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormAddIPEntry));
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.textBox_MAC = new System.Windows.Forms.TextBox();
            this.textBox_adaptater_index = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox_entry_type = new System.Windows.Forms.ComboBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_IP
            // 
            this.textBox_IP.Location = new System.Drawing.Point(152, 8);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(104, 20);
            this.textBox_IP.TabIndex = 0;
            this.textBox_IP.Text = "10.0.0.1";
            // 
            // textBox_MAC
            // 
            this.textBox_MAC.Location = new System.Drawing.Point(152, 40);
            this.textBox_MAC.Name = "textBox_MAC";
            this.textBox_MAC.Size = new System.Drawing.Size(104, 20);
            this.textBox_MAC.TabIndex = 1;
            this.textBox_MAC.Text = "00-01-02-AF-A3-C0";
            // 
            // textBox_adaptater_index
            // 
            this.textBox_adaptater_index.Location = new System.Drawing.Point(224, 104);
            this.textBox_adaptater_index.Name = "textBox_adaptater_index";
            this.textBox_adaptater_index.Size = new System.Drawing.Size(24, 20);
            this.textBox_adaptater_index.TabIndex = 2;
            this.textBox_adaptater_index.Text = "1";
            this.textBox_adaptater_index.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "IP Address";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "Physical address (MAC)";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 24);
            this.label3.TabIndex = 6;
            this.label3.Text = "ARP entry type";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 24);
            this.label4.TabIndex = 7;
            this.label4.Text = "Adapter index";
            // 
            // comboBox_entry_type
            // 
            this.comboBox_entry_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_entry_type.Items.AddRange(new object[] {
                                                                     "Dynamic",
                                                                     "Invalid",
                                                                     "Other",
                                                                     "Static"});
            this.comboBox_entry_type.Location = new System.Drawing.Point(152, 72);
            this.comboBox_entry_type.Name = "comboBox_entry_type";
            this.comboBox_entry_type.Size = new System.Drawing.Size(104, 21);
            this.comboBox_entry_type.TabIndex = 9;
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(48, 128);
            this.button_ok.Name = "button_ok";
            this.button_ok.TabIndex = 10;
            this.button_ok.Text = "OK";
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(144, 128);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.TabIndex = 11;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // FormAddIPEntry
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(272, 160);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.comboBox_entry_type);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_adaptater_index);
            this.Controls.Add(this.textBox_MAC);
            this.Controls.Add(this.textBox_IP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormAddIPEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add IP Entry";
            this.ResumeLayout(false);

        }
        #endregion

        private void button_ok_Click(object sender, System.EventArgs e)
        {
            iphelper.MIB_IPNETROW mib_i=new iphelper.MIB_IPNETROW();


            // get bPhysAddr
            string mac_addr=this.textBox_MAC.Text;
            mac_addr=mac_addr.Replace(" ","");
            mac_addr=mac_addr.Replace("-","");
            mac_addr=mac_addr.Replace(".","");
            mac_addr=mac_addr.Replace(":","");
            if ((mac_addr.Length%2!=0)||(mac_addr.Length/2>iphelper.CMIB_IPNETROW.MAXLEN_PHYSADDR))
            {
                MessageBox.Show(this,"Error in Mac address","error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            mib_i.bPhysAddr=new byte[iphelper.CMIB_IPNETROW.MAXLEN_PHYSADDR];
            for(int cpt=0;cpt<mac_addr.Length/2;cpt++)
            {
                mib_i.bPhysAddr[cpt]=byte.Parse(mac_addr.Substring(2*cpt,2),
                    System.Globalization.NumberStyles.HexNumber);
            }
            // get dwPhysAddrLen
            mib_i.dwPhysAddrLen=(UInt32)(mac_addr.Length/2);

            // get dwIndex
            mib_i.dwIndex=System.Convert.ToUInt32(this.textBox_adaptater_index.Text,10);
            // get dwAddr
            try
            {
                mib_i.dwAddr=System.BitConverter.ToUInt32(System.Net.IPAddress.Parse(this.textBox_IP.Text.Trim()).GetAddressBytes(),0);
            }
            catch
            {
                MessageBox.Show(this,"Error in IP address","error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            // get dwtype
            switch (this.comboBox_entry_type.Text)
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
            string str_msg;
            UInt32 error_code;
            if (this.b_create)
            {
                error_code=iphelper.iphelper.CreateIpNetEntry(ref mib_i);
                str_msg="Entry successfully added";
            }
            else
            {
                error_code=iphelper.iphelper.SetIpNetEntry(ref mib_i);
                str_msg="Entry successfully modified";
            }
            if (error_code!=0)
            {
                str_msg=Tools.API.API_error.GetAPIErrorMessageDescription(error_code);
                System.Windows.Forms.MessageBox.Show(this,str_msg,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            this.Hide();
            System.Windows.Forms.MessageBox.Show(this,str_msg,"",MessageBoxButtons.OK,MessageBoxIcon.Information);
            this.Close();
        }

        private void button_cancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

    }
}
