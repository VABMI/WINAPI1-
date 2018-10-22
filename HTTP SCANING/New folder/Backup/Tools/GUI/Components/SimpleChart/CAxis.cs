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

namespace Tools.GUI.Components.SimpleChart
{
	/// <summary>
	/// Summary description for CAxis.
	/// </summary>
	public class CAxis
	{
        #region members

        public delegate void RedrawHandler();
        protected RedrawHandler redraw_handler=null;

        private CLabel label=null;
        public CLabel Label
        {
            get
            {
                return this.label;
            }
            set
            {
                this.label=value;
                this.ReDraw();
            }
        }
        private bool visible=true;
        public bool Visible
        {
            get
            {
                return this.visible;
            }
            set
            {
                this.visible=value;
                this.ReDraw();
            }
        }
        private System.Drawing.Pen pen=null;
        public System.Drawing.Pen Pen
        {
            get
            {
                return this.pen;
            }
            set
            {
                this.pen=value;
                this.ReDraw();
            }
        }
        #endregion

        public CAxis(RedrawHandler redraw_handler)
        {
            this.pen=new System.Drawing.Pen(System.Drawing.Color.Black,2);
            this.pen.CustomEndCap = new System.Drawing.Drawing2D.AdjustableArrowCap(6, 6, false);
            this.label=new CLabel(new CLabel.RedrawHandler(redraw_handler));
        }
        protected void ReDraw()
        {
            if (this.redraw_handler!=null)
                this.redraw_handler();
        }
	}
}
