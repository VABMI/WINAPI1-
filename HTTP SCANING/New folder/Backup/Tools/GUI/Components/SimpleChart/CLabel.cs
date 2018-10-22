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
	/// Summary description for CLabel.
	/// </summary>
	public class CLabel
	{
        #region members
        public delegate void RedrawHandler();
        protected RedrawHandler redraw_handler=null;

        protected System.Drawing.Font font=null;
        public System.Drawing.Font Font
        {
            get
            {
                return this.font;
            }
            set
            {
                this.font=value;
                this.ReDraw();
            }
        }


        protected System.Drawing.Brush brush=null;
        public System.Drawing.Brush Brush
        {
            get
            {
                return this.brush;
            }
            set
            {
                this.brush=value;
                this.ReDraw();
            }
        }
        protected string label="";
        public string Label
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
        protected int angle=0;
        public int Angle
        {
            get
            {
                return this.angle;
            }
            set
            {
                this.angle=value;
                this.ReDraw();
            }
        }
        protected bool visible=true;
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
        protected Tools.Drawing.CDrawString.TEXT_POSITION position_relative_to_control=Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_BOTTOM;
        public Tools.Drawing.CDrawString.TEXT_POSITION PositionRelativeToControl
        {
            get
            {
                return this.position_relative_to_control;
            }
            set
            {
                this.position_relative_to_control=value;
                this.ReDraw();
            }
        }
        #endregion

        public CLabel(RedrawHandler redraw_handler)
        {
            this.redraw_handler=redraw_handler;
            this.brush=new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            this.font=new System.Drawing.Font("Arial",10);
        }

        protected void ReDraw()
        {
            if (this.redraw_handler!=null)
                this.redraw_handler();
        }
	}
}
