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
	/// Summary description for CPlotInfoList.
	/// </summary>
	public class CPlotInfoList
	{
        #region members
        public delegate void RedrawHandler();
        private RedrawHandler redraw_handler=null;
        /// <summary>
        /// color of the graph
        /// </summary>
        private System.Drawing.Brush graph_brush=null;
        public System.Drawing.Brush GraphBrush
        {
            get
            {
                return this.graph_brush;
            }
            set
            {
                this.graph_brush=value;
                this.ReDraw();
            }
        }

        private string legend="";
        public String Legend
        {
            get
            {
                return this.legend;
            }
            set
            {
                this.legend=value;
                this.ReDraw();
            }
        }

        /// <summary>
        /// Set label angle in degree between horizontal and label direction for all the plots label
        /// </summary>
        public int LabelAngle
        {
            set
            {
                for (int cnt=0;cnt<this.al_plot_info_list.Count;cnt++)
                    ((CPlotInfo)this.al_plot_info_list[cnt]).Angle=value;
            }
        }
        /// <summary>
        /// Set label relative position for all the plots label
        /// </summary>
        public Tools.Drawing.CDrawString.TEXT_POSITION PositionRelativeToControl
        {
            set
            {
                for (int cnt=0;cnt<this.al_plot_info_list.Count;cnt++)
                    ((CPlotInfo)this.al_plot_info_list[cnt]).PositionRelativeToControl=value;
            }
        }
        /// <summary>
        /// set label brush for all points
        /// </summary>
        public System.Drawing.Brush LabelBrush
        {
            set
            {
                for (int cnt=0;cnt<this.al_plot_info_list.Count;cnt++)
                    ((CPlotInfo)this.al_plot_info_list[cnt]).Brush=value;
            }
        }

        private int fixed_length=0;
        /// <summary>
        /// allow the list to act like a FIFO of FixedLength size
        /// </summary>
        public int FixedLength
        {
            get
            {
                return this.fixed_length;
            }
            set
            {
                this.fixed_length=value;
                // assume fixed_length>=0
                if (this.fixed_length<0)
                    this.fixed_length=0;

                int sub=this.al_plot_info_list.Count-fixed_length;
                // if list conatins more value keep only the last one
                if (sub>0)
                {
                    // remove the first (oldest) values
                    for (int cnt=sub-1;cnt>=0;cnt--)
                        this.al_plot_info_list.RemoveAt(cnt);
                }
            }
        }
        private bool fixed_length_dont_forget_min_max=true;
        public bool FixedLengthDontForgetMinMax
        {
            get
            {
                return this.fixed_length_dont_forget_min_max;
            }
            set
            {
                this.fixed_length_dont_forget_min_max=value;

            }
        }
        /// <summary>
        /// get the number of plots in the current list
        /// </summary>
        /// <returns></returns>
        public int PlotsNumber
        {
            get
            {
                return this.al_plot_info_list.Count;
            }
        }
        private double min_value=0;
        public double MinValue
        {
            get
            {
                return this.min_value;
            }
        }
        private double max_value=0;
        public double MaxValue
        {
            get
            {
                return this.max_value;
            }
        }
        private System.Collections.ArrayList al_plot_info_list=null;
        #endregion

		public CPlotInfoList(RedrawHandler redraw_handler)
		{
            this.redraw_handler=redraw_handler;
            this.graph_brush=new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
            this.al_plot_info_list=new System.Collections.ArrayList();
		}

        #region methodes
        /// <summary>
        /// Clear plot list
        /// </summary>
        public void Clear()
        {
            this.al_plot_info_list.Clear();
            this.min_value=0;
            this.max_value=0;
        }
        public void AddPlot(double plot_value)
        {
            this.AddPlot(new CPlotInfo(new CLabel.RedrawHandler(this.redraw_handler),plot_value));
        }
        public void AddPlot(double plot_value,string plot_name)
        {
            this.AddPlot(new CPlotInfo(new CLabel.RedrawHandler(this.redraw_handler),plot_value,plot_name));
        }
        public void AddPlot(CPlotInfo plotinfo)
        {
            // if no rotation or list hasn't reach fixed_length, just add the value
            if ((this.fixed_length==0)||(this.al_plot_info_list.Count<this.fixed_length))
            {
                this.al_plot_info_list.Add(plotinfo);
                this.min_value=Math.Min(this.min_value,plotinfo.Value);
                this.max_value=Math.Max(this.max_value,plotinfo.Value);
            }
            else// replace the oldest value
            {
                if (!this.fixed_length_dont_forget_min_max)
                {
                    this.max_value=0;
                    this.min_value=0;
                }
                this.al_plot_info_list.RemoveAt(0);
                this.al_plot_info_list.Add(plotinfo);
                // as we have remove a component we have to check min/max value
                foreach(CPlotInfo plot in this.al_plot_info_list)
                {
                    this.min_value=Math.Min(this.min_value,plot.Value);
                    this.max_value=Math.Max(this.max_value,plot.Value);
                }
            }
            if (this.redraw_handler!=null)
                this.redraw_handler();
        }

        /// <summary>
        /// if index is not found a default CPlotInfo object will be returned
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CPlotInfo GetPlotInfo(int index)
        {
            if ((index<0)||(index>this.al_plot_info_list.Count))
                return new CPlotInfo(new CLabel.RedrawHandler(this.redraw_handler));
            return (CPlotInfo)this.al_plot_info_list[index];
        }

        private void ReDraw()
        {
            if (this.redraw_handler!=null)
                this.redraw_handler();
        }

        #endregion
	}
}
