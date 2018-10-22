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

    public class Form_hexa_view : System.Windows.Forms.Form
    {
        private System.Windows.Forms.RichTextBox richtextBox;
        private System.ComponentModel.Container components = null;

        public bool no_data;
        public Form_hexa_view(string hexa_data)
        {

            if (hexa_data.Length==0)
            {
                this.no_data=true;
                MessageBox.Show(this,"No data","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            InitializeComponent();
            this.no_data=false;
            byte nb_char_per_line=16;
            char replace_chr=' ';
            // convert to ascii
            string ascii_data=easy_socket.hexa_convert.hexa_to_string(hexa_data);

            if (ascii_data.Length==0)//hexa_data!="" --> error in call ascii data was sent instead of hexa data
            {
                ascii_data=hexa_data;
                hexa_data=easy_socket.hexa_convert.string_to_hexa(ascii_data);
                if (hexa_data.Length==0)// another error occurs
                {
                    this.no_data=true;
                    MessageBox.Show(this,"Error can't convert data","error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
            }


            hexa_data=hexa_data.Replace("-"," ");
            hexa_data+=" ";// add a separator like to get the same size as other lines ending with separator            
            char[] pc_ascii=ascii_data.ToCharArray();
            // check for char
            for (int cpt=0;cpt<pc_ascii.Length;cpt++)
            {
                if (pc_ascii[cpt]<0x1e)
                    pc_ascii[cpt]=replace_chr;
            }
            ascii_data=new string(pc_ascii);
            // put the number of desired char per line
            int nb_lines=(int)System.Math.Floor(((double)hexa_data.Length)/nb_char_per_line/3);
            string str="";
            string addr;
            for (int cpt=0;cpt<nb_lines;cpt++)
            {
                addr=easy_socket.hexa_convert.byte_to_hexa(System.BitConverter.GetBytes(easy_socket.network_convert.switch_UInt32((UInt32)(cpt*nb_char_per_line))),"");
                str+=addr+": ";
                // hexa data
                str+=hexa_data.Substring(cpt*nb_char_per_line*3,nb_char_per_line*3);
                str+="\t";
                // ascii data
                str+=ascii_data.Substring(cpt*nb_char_per_line,nb_char_per_line);
                str+="\r\n";
            }
            // for last line
            addr=easy_socket.hexa_convert.byte_to_hexa(System.BitConverter.GetBytes(easy_socket.network_convert.switch_UInt32((UInt32)((nb_lines)*nb_char_per_line))),"");
            str+=addr+": ";
            // hexa data
            string str_last_line=hexa_data.Substring((nb_lines)*nb_char_per_line*3);
            // if last line not full, add space
            if (str_last_line.Length<nb_char_per_line*3)
                str_last_line+=new String(' ',(int)(nb_char_per_line*3-str_last_line.Length));
            str+=str_last_line;
            str+="\t";
            // ascii data
            str+=ascii_data.Substring((nb_lines)*nb_char_per_line);
            this.richtextBox.Text=str;
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
        private void InitializeComponent()
        {
            this.richtextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richtextBox
            // 
            this.richtextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richtextBox.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.richtextBox.Location = new System.Drawing.Point(0, 0);
            this.richtextBox.Name = "richtextBox";
            this.richtextBox.ReadOnly = true;
            this.richtextBox.Size = new System.Drawing.Size(568, 222);
            this.richtextBox.TabIndex = 0;
            this.richtextBox.Text = "";
            this.richtextBox.WordWrap = false;
            // 
            // Form_hexa_view
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(568, 222);
            this.Controls.Add(this.richtextBox);
            this.Name = "Form_hexa_view";
            this.Text = "Hexa View";
            this.ResumeLayout(false);

        }
        #endregion

    }
}
