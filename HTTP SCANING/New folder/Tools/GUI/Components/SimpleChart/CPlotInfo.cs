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
	/// Summary description for CPlotInfo.
	/// </summary>
	public class CPlotInfo:CLabel
	{
        private double plot_value=0;
        public double Value
        {
            get
            {
                return this.plot_value;
            }
            set
            {
                this.plot_value=value;
                this.ReDraw();
            }
        }

		public CPlotInfo(RedrawHandler redraw_handler):base(redraw_handler)
		{
		}
        public CPlotInfo(RedrawHandler redraw_handler,double plot_value):base(redraw_handler)
        {
            this.plot_value=plot_value;
        }
        public CPlotInfo(RedrawHandler redraw_handler,double plot_value,string plot_label):base(redraw_handler)
        {
            this.label=plot_label;
            this.plot_value=plot_value;
        }
	}
}
