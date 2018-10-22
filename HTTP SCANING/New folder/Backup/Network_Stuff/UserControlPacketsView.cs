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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Network_Stuff
{
    public class UserControlPacketsView : System.Windows.Forms.UserControl
    {
        private const string TEXT_SEPARATOR="\t";
        private Tools.GUI.ListViewItemComparer lvic;

        public System.Windows.Forms.ListView listView_capture;
        private System.Windows.Forms.ColumnHeader columnHeader_time;
        private System.Windows.Forms.ColumnHeader columnHeader_ip_src;
        private System.Windows.Forms.ColumnHeader columnHeader_ip_dest;
        private System.Windows.Forms.ColumnHeader columnHeader_protocol;
        private System.Windows.Forms.ColumnHeader columnHeader_iph_infos;
        private System.Windows.Forms.ColumnHeader columnHeader_src_port;
        private System.Windows.Forms.ColumnHeader columnHeader_dst_port;
        private System.Windows.Forms.ColumnHeader columnHeader_protocol_infos;
        private System.Windows.Forms.ColumnHeader columnHeader_protocol_data;
        private System.Windows.Forms.ContextMenu contextMenu_list_view;
        private System.Windows.Forms.MenuItem menuItem_clear;
        private System.Windows.Forms.MenuItem menuItem_copy_selected;
        private System.Windows.Forms.MenuItem menuItem_save_selected;
        private System.Windows.Forms.MenuItem menuItem_save_all;
        private System.Windows.Forms.MenuItem menuItem_protocol_data;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ColumnHeader columnHeader_fullpacket;
        private System.Windows.Forms.MenuItem menuItem_fullpacket;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ColumnHeader columnHeader_free;
        private System.ComponentModel.Container components = null;

        public UserControlPacketsView()
        {
            InitializeComponent();
            this.lvic=new Tools.GUI.ListViewItemComparer();
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

        #region Component Designer generated code
        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.listView_capture = new System.Windows.Forms.ListView();
            this.columnHeader_time = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_ip_src = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_ip_dest = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_protocol = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_iph_infos = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_src_port = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_dst_port = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_protocol_infos = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_protocol_data = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_fullpacket = new System.Windows.Forms.ColumnHeader();
            this.contextMenu_list_view = new System.Windows.Forms.ContextMenu();
            this.menuItem_clear = new System.Windows.Forms.MenuItem();
            this.menuItem_copy_selected = new System.Windows.Forms.MenuItem();
            this.menuItem_save_selected = new System.Windows.Forms.MenuItem();
            this.menuItem_save_all = new System.Windows.Forms.MenuItem();
            this.menuItem_protocol_data = new System.Windows.Forms.MenuItem();
            this.menuItem_fullpacket = new System.Windows.Forms.MenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.columnHeader_free = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // listView_capture
            // 
            this.listView_capture.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                               this.columnHeader_time,
                                                                                               this.columnHeader_ip_src,
                                                                                               this.columnHeader_ip_dest,
                                                                                               this.columnHeader_protocol,
                                                                                               this.columnHeader_iph_infos,
                                                                                               this.columnHeader_src_port,
                                                                                               this.columnHeader_dst_port,
                                                                                               this.columnHeader_protocol_infos,
                                                                                               this.columnHeader_protocol_data,
                                                                                               this.columnHeader_fullpacket,
                                                                                               this.columnHeader_free});
            this.listView_capture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_capture.FullRowSelect = true;
            this.listView_capture.GridLines = true;
            this.listView_capture.Name = "listView_capture";
            this.listView_capture.Size = new System.Drawing.Size(816, 184);
            this.listView_capture.TabIndex = 17;
            this.listView_capture.View = System.Windows.Forms.View.Details;
            this.listView_capture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView_capture_MouseUp);
            this.listView_capture.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_capture_ColumnClick);
            // 
            // columnHeader_time
            // 
            this.columnHeader_time.Text = "Time";
            this.columnHeader_time.Width = 80;
            // 
            // columnHeader_ip_src
            // 
            this.columnHeader_ip_src.Text = "Ip Src";
            this.columnHeader_ip_src.Width = 90;
            // 
            // columnHeader_ip_dest
            // 
            this.columnHeader_ip_dest.Text = "Ip Dest";
            this.columnHeader_ip_dest.Width = 90;
            // 
            // columnHeader_protocol
            // 
            this.columnHeader_protocol.Text = "Protocol";
            this.columnHeader_protocol.Width = 55;
            // 
            // columnHeader_iph_infos
            // 
            this.columnHeader_iph_infos.Text = "Ip h infos";
            this.columnHeader_iph_infos.Width = 80;
            // 
            // columnHeader_src_port
            // 
            this.columnHeader_src_port.Text = "Src Port";
            // 
            // columnHeader_dst_port
            // 
            this.columnHeader_dst_port.Text = "Dst port";
            // 
            // columnHeader_protocol_infos
            // 
            this.columnHeader_protocol_infos.Text = "Protocol infos";
            this.columnHeader_protocol_infos.Width = 100;
            // 
            // columnHeader_protocol_data
            // 
            this.columnHeader_protocol_data.Text = "Protocol data";
            this.columnHeader_protocol_data.Width = 100;
            // 
            // columnHeader_fullpacket
            // 
            this.columnHeader_fullpacket.Text = "Full packet";
            this.columnHeader_fullpacket.Width = 90;
            // 
            // contextMenu_list_view
            // 
            this.contextMenu_list_view.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                                  this.menuItem_clear,
                                                                                                  this.menuItem_copy_selected,
                                                                                                  this.menuItem_save_selected,
                                                                                                  this.menuItem_save_all,
                                                                                                  this.menuItem_protocol_data,
                                                                                                  this.menuItem_fullpacket});
            // 
            // menuItem_clear
            // 
            this.menuItem_clear.Index = 0;
            this.menuItem_clear.Text = "Clear";
            this.menuItem_clear.Click += new System.EventHandler(this.menuItem_clear_Click);
            // 
            // menuItem_copy_selected
            // 
            this.menuItem_copy_selected.Index = 1;
            this.menuItem_copy_selected.Text = "Copy selected";
            this.menuItem_copy_selected.Click += new System.EventHandler(this.menuItem_copy_selected_Click);
            // 
            // menuItem_save_selected
            // 
            this.menuItem_save_selected.Index = 2;
            this.menuItem_save_selected.Text = "Save selected";
            this.menuItem_save_selected.Click += new System.EventHandler(this.menuItem_save_selected_Click);
            // 
            // menuItem_save_all
            // 
            this.menuItem_save_all.Index = 3;
            this.menuItem_save_all.Text = "Save all";
            this.menuItem_save_all.Click += new System.EventHandler(this.menuItem_save_all_Click);
            // 
            // menuItem_protocol_data
            // 
            this.menuItem_protocol_data.Index = 4;
            this.menuItem_protocol_data.Text = "Protocol data";
            this.menuItem_protocol_data.Click += new System.EventHandler(this.menuItem_protocol_data_Click);
            // 
            // menuItem_fullpacket
            // 
            this.menuItem_fullpacket.Index = 5;
            this.menuItem_fullpacket.Text = "Full packet";
            this.menuItem_fullpacket.Click += new System.EventHandler(this.menuItem_fullpacket_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "xml";
            this.saveFileDialog.Filter = "XML files|*.xml|Text files*.txt|*.txt|All files|*.*";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "XML files|*.xml|Text files*.txt|*.txt|All files|*.*";
            // 
            // columnHeader_free
            // 
            this.columnHeader_free.Text = "";
            this.columnHeader_free.Width = 0;
            // 
            // UserControlPacketsView
            // 
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.listView_capture});
            this.Name = "UserControlPacketsView";
            this.Size = new System.Drawing.Size(816, 184);
            this.ResumeLayout(false);

        }
        #endregion

        private void listView_capture_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            lvic.set_column_number(e.Column);
            this.listView_capture.ListViewItemSorter=lvic;
            this.listView_capture.Sort();
            this.listView_capture.ListViewItemSorter=null;// avoid to organize when changing data        
        }

        private void button_clear_list_view_Click(object sender, System.EventArgs e)
        {
            this.listView_capture.Items.Clear(); 
        }

        private void listView_capture_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Right)
            {
                this.contextMenu_list_view.Show((ListView) sender,new System.Drawing.Point(e.X,e.Y));
            }        
        }

        private void menuItem_clear_Click(object sender, System.EventArgs e)
        {
            this.listView_capture.Items.Clear();        
        }

        private void menuItem_save_all_Click(object sender, System.EventArgs e)
        {
            if (this.saveFileDialog.ShowDialog(this)!=DialogResult.OK)
                return;

            string str="";
            int cpt2;
            if (this.is_xml_ext(this.saveFileDialog.FileName))
            {
                // xml saving
                System.Collections.ArrayList al=new System.Collections.ArrayList();
                for (int cpt=0;cpt<this.listView_capture.Items.Count;cpt++)
                    al.Add(new packet_view(this.listView_capture.Items[cpt].SubItems[0].Text,
                                            this.listView_capture.Items[cpt].SubItems[1].Text,
                                            this.listView_capture.Items[cpt].SubItems[2].Text,
                                            this.listView_capture.Items[cpt].SubItems[3].Text,
                                            this.listView_capture.Items[cpt].SubItems[4].Text,
                                            this.listView_capture.Items[cpt].SubItems[5].Text,
                                            this.listView_capture.Items[cpt].SubItems[6].Text,
                                            this.listView_capture.Items[cpt].SubItems[7].Text,
                                            this.listView_capture.Items[cpt].SubItems[8].Text,
                                            this.listView_capture.Items[cpt].SubItems[9].Text
                                            )
                        );
                this.save_xml(this.saveFileDialog.FileName,al);
            }
            else
            {
                // text saving
                for (int cpt=0;cpt<this.listView_capture.Items.Count;cpt++)
                {
                    str+=this.listView_capture.Items[cpt].Text;
                    for (cpt2=1;cpt2<this.listView_capture.Items[cpt].SubItems.Count;cpt2++)// subitems[0].text=item.text
                        str+=TEXT_SEPARATOR+this.listView_capture.Items[cpt].SubItems[cpt2].Text;
                    str+="\r\n";
                }
                Tools.IO.file_access.write(this.saveFileDialog.FileName,str);
            }
        }

        private void menuItem_save_selected_Click(object sender, System.EventArgs e)
        {
            if (this.saveFileDialog.ShowDialog(this)!=DialogResult.OK)
                return;
            string str="";
            int cpt2;
            if (this.is_xml_ext(this.saveFileDialog.FileName))
            {
                // xml saving
                System.Collections.ArrayList al=new System.Collections.ArrayList();
                for (int cpt=0;cpt<this.listView_capture.SelectedItems.Count;cpt++)
                    al.Add(new packet_view(this.listView_capture.SelectedItems[cpt].SubItems[0].Text,
                        this.listView_capture.SelectedItems[cpt].SubItems[1].Text,
                        this.listView_capture.SelectedItems[cpt].SubItems[2].Text,
                        this.listView_capture.SelectedItems[cpt].SubItems[3].Text,
                        this.listView_capture.SelectedItems[cpt].SubItems[4].Text,
                        this.listView_capture.SelectedItems[cpt].SubItems[5].Text,
                        this.listView_capture.SelectedItems[cpt].SubItems[6].Text,
                        this.listView_capture.SelectedItems[cpt].SubItems[7].Text,
                        this.listView_capture.SelectedItems[cpt].SubItems[8].Text,
                        this.listView_capture.SelectedItems[cpt].SubItems[9].Text
                        )
                        );
                this.save_xml(this.saveFileDialog.FileName,al);
            }
            else
            {
                // text saving
                for (int cpt=0;cpt<this.listView_capture.SelectedItems.Count;cpt++)
                {
                    str+=this.listView_capture.SelectedItems[cpt].Text;
                    for (cpt2=1;cpt2<this.listView_capture.SelectedItems[cpt].SubItems.Count;cpt2++)// subitems[0].text=item.text
                        str+=TEXT_SEPARATOR+this.listView_capture.SelectedItems[cpt].SubItems[cpt2].Text;
                    str+="\r\n";
                }        
                Tools.IO.file_access.write(this.saveFileDialog.FileName,str);
            }
        }

        private void menuItem_protocol_data_Click(object sender, System.EventArgs e)
        {
            if (this.listView_capture.SelectedItems.Count<=0)
                return;
            string hex_data=this.listView_capture.SelectedItems[0].SubItems[8].Text;
            Form_hexa_view fhv=new Form_hexa_view(hex_data);
            if (fhv.no_data)
                return;

            fhv.MdiParent=this.ParentForm.MdiParent;
            fhv.Show();
        }
        private void menuItem_fullpacket_Click(object sender, System.EventArgs e)
        {
            if (this.listView_capture.SelectedItems.Count<=0)
                return;
            string hex_data=this.listView_capture.SelectedItems[0].SubItems[9].Text;
            Form_hexa_view fhv=new Form_hexa_view(hex_data);
            if (fhv.no_data)
                return;

            fhv.MdiParent=this.ParentForm.MdiParent;
            fhv.Show();        
        }
        private void menuItem_copy_selected_Click(object sender, System.EventArgs e)
        {
            string str="";
            int cpt2;
            for (int cpt=0;cpt<this.listView_capture.SelectedItems.Count;cpt++)
            {
                str+=this.listView_capture.SelectedItems[cpt].Text;
                for (cpt2=1;cpt2<this.listView_capture.SelectedItems[cpt].SubItems.Count;cpt2++)// subitems[0].text=item.text
                    str+=TEXT_SEPARATOR+this.listView_capture.SelectedItems[cpt].SubItems[cpt2].Text;
                str+="\r\n";
            } 
            Clipboard.SetDataObject(str,true);
        }

        private bool is_xml_ext(string filename)
        {
            string strex=System.IO.Path.GetExtension(filename);
            return (strex.ToLower()==".xml");
        }
        private void save_xml(string filename,System.Collections.ArrayList al)
        {
            Tools.Xml.XML_access.XMLSerializeObject(filename,(packet_view[])al.ToArray(typeof(packet_view)),typeof(packet_view[]));
        }
        public void load_file()
        {
            if (this.openFileDialog.ShowDialog(this)!=DialogResult.OK)
                return;
            if (is_xml_ext(this.openFileDialog.FileName))
                load_xml(this.openFileDialog.FileName);
            else
                load_text(this.openFileDialog.FileName);
        }
        private void load_text(string filename)
        {
            string str_file_content=Tools.IO.file_access.read(filename);
            if (str_file_content=="")
                return;
            // split on \r\n
            string[] lines=str_file_content.Split("\r\n".ToCharArray());
            string[] fields;
            // for each line
            for (int cpt=0;cpt<lines.Length;cpt++)
            {
                if (lines[cpt]=="")
                    continue;
                // split on TEXT_SEPARATOR to get different fields
                fields=lines[cpt].Split(TEXT_SEPARATOR.ToCharArray());
                if (fields.Length!=10)
                {
                    System.Windows.Forms.MessageBox.Show(this,"Error parsing file.","Error",System.Windows.Forms.MessageBoxButtons.OK,
                        System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }
                // add fields to listview
                this.listView_capture.Items.Add(new ListViewItem(fields));
            }

        }
        private void load_xml(string filename)
        {
            try
            {
                // loader could be improved to decode again full packet but is this really necessary ?
                packet_view[] pv=(packet_view[])Tools.Xml.XML_access.XMLDeserializeObject(filename,typeof(packet_view[]));
                for(int cpt=0;cpt<pv.Length;cpt++)
                    this.listView_capture.Items.Add(new ListViewItem(new string[]
                                                {
                                                    pv[cpt].time,
                                                    pv[cpt].ip_src,
                                                    pv[cpt].ip_dst,
                                                    pv[cpt].protocol,
                                                    pv[cpt].iph_infos,
                                                    pv[cpt].src_port,
                                                    pv[cpt].dst_port,
                                                    pv[cpt].protocol_infos,
                                                    pv[cpt].protocol_data,
                                                    pv[cpt].full_packet
                                                }));
            }
            catch (Exception e)
            {
                MessageBox.Show(this,"Error parsing file.\r\nError: "+e.Message,"Error",MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    public class packet_view
    {
        public string time;
        public string ip_src;
        public string ip_dst;
        public string protocol;
        public string iph_infos;
        public string src_port;
        public string dst_port;
        public string protocol_infos;
        public string protocol_data;
        public string full_packet;

        public packet_view()
        {
        }
        public packet_view(string time,string ip_src,string ip_dst,
                            string protocol,string iph_infos,
                            string src_port,string dst_port,string protocol_infos,
                            string protocol_data,string full_packet)
        {
            this.time=time;
            this.ip_src=ip_src;
            this.ip_dst=ip_dst;
            this.protocol=protocol;
            this.iph_infos=iph_infos;
            this.src_port=src_port;
            this.dst_port=dst_port;
            this.protocol_infos=protocol_infos;
            this.protocol_data=protocol_data;
            this.full_packet=full_packet;
        }
    }
}
