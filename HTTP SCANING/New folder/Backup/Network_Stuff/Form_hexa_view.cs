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
        private Tools.GUI.Controls.HexViewer.HexViewer hexview;
        private System.ComponentModel.Container components = null;

        public bool no_data;
        public Form_hexa_view(string hexa_data)
        {

            if (hexa_data.Length==0)
            {
                this.no_data=true;
                MessageBox.Show(null,"No data","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            InitializeComponent();
            this.no_data=false;
            // convert to ascii
            string ascii_data=easy_socket.hexa_convert.hexa_to_string(hexa_data);

            if (ascii_data.Length==0)//hexa_data!="" --> error in call ascii data was sent instead of hexa data
            {
                ascii_data=hexa_data;
                hexa_data=easy_socket.hexa_convert.string_to_hexa(ascii_data);
                if (hexa_data.Length==0)// another error occurs
                {
                    this.no_data=true;
                    MessageBox.Show(null,"Error can't convert data","error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
            }
            hexview.Data=easy_socket.hexa_convert.hexa_to_byte(hexa_data);
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
            this.hexview = new Tools.GUI.Controls.HexViewer.HexViewer();
            this.SuspendLayout();
            // 
            // hexview
            // 
            this.hexview.BlockLength = 2;
            this.hexview.Data = null;
            this.hexview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hexview.Location = new System.Drawing.Point(0, 0);
            this.hexview.Name = "hexview";
            this.hexview.SelectionLength = 0;
            this.hexview.SelectionStart = 0;
            this.hexview.Size = new System.Drawing.Size(560, 174);
            this.hexview.TabIndex = 0;
            // 
            // Form_hexa_view
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(568, 222);
            this.Controls.Add(this.hexview);
            this.Name = "Form_hexa_view";
            this.Text = "Hexa View";
            this.ResumeLayout(false);

        }
        #endregion

    }
}
