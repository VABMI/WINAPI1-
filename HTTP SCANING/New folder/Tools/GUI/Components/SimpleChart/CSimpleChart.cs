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
using System.Windows.Forms;

namespace Tools.GUI.Components.SimpleChart
{
    /// <summary>
    /// Summary description for CSimpleChart.
    /// </summary>
    public class CSimpleChart:System.Windows.Forms.Panel
    {
        #region enums
        public enum GRAPH_TYPE
        {
            BAR,
            LINE,
            LINE_FILL
        }
        public enum BAR_TYPE
        {
            SPLITTED,
            MIXED,
        }
        #endregion

        #region members

        private int horizontal_axis_arrow_half_width=0;
        private int vertical_left_axis_arrow_half_width=0;
        private int vertical_right_axis_arrow_half_width=0;
        private int horizontal_axis_arrow_height=0;
        private int vertical_left_axis_arrow_height=0;
        private int vertical_right_axis_arrow_height=0;
        private bool b_is_drawn=false;
        private bool auto_redraw=true;
        /// <summary>
        /// Enable or disable auto redraw after an option change. Call ReDraw methode when put to true
        /// </summary>
        public bool AutoRedraw
        {
            get
            {
                return this.auto_redraw;
            }
            set
            {
                this.auto_redraw=value;
                if (this.auto_redraw)
                    this.ReDraw();
            }
        }
        private bool auto_tick_frequency=false;
        public bool AutoTickFrequency
        {
            get
            {
                return this.auto_tick_frequency;
            }
            set
            {
                this.auto_tick_frequency=value;
                this.ReDrawAfterOptionChange();
            }
        }
        private int auto_tick_frequency_number_of_ticks=8;
        public int AutoTickFrequencyNumberOfTicks
        {
            get
            {
                return this.auto_tick_frequency_number_of_ticks;
            }
            set
            {
                if (value<=0)
                    return;
                this.auto_tick_frequency_number_of_ticks=value;
                this.ReDrawAfterOptionChange();
            }
        }
        private bool vertical_axis_negative_value_seem_positive=false;
        /// <summary>
        /// If true values won't be signed (it allow to show up/down mixed graph with positive value only)
        /// </summary>
        public bool VerticalAxisNegativeValueSeemPositive
        {
            get
            {
                return this.vertical_axis_negative_value_seem_positive;
            }
            set
            {
                this.vertical_axis_negative_value_seem_positive=value;
                this.ReDrawAfterOptionChange();
            }
        }
        private Tools.Drawing.CDrawString.TEXT_POSITION legend_position=Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_TOP_RIGHT;
        public Tools.Drawing.CDrawString.TEXT_POSITION LegendPosition
        {
            get
            {
                return this.legend_position;
            }
            set
            {
                this.legend_position=value;
                this.ReDrawAfterOptionChange();
            }
        }

        private bool show_legend=true;
        public bool ShowLegend
        {
            get
            {
                return this.show_legend;
            }
            set
            {
                this.show_legend=value;
                this.ReDrawAfterOptionChange();
            }
        }

        private System.Drawing.Font legend_font=new System.Drawing.Font("Arial",10);
        public System.Drawing.Font LegendFont
        {
            get
            {
                return this.legend_font;
            }
            set
            {
                this.legend_font=value;
                this.ReDrawAfterOptionChange();
            }
        }

        private System.Drawing.SolidBrush legend_brush=new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        public System.Drawing.SolidBrush LegendBrush
        {
            get
            {
                return this.legend_brush;
            }
            set
            {
                this.legend_brush=value;
                this.ReDrawAfterOptionChange();
            }
        }

        private float vertical_axis_tick_frequency=2;
        public float VerticalAxisTickFrequency
        {
            get
            {
                return this.vertical_axis_tick_frequency;
            }
            set
            {
                if (value<=0)
                    return;
                this.vertical_axis_tick_frequency=value;
                this.ReDrawAfterOptionChange();
            }
        }
        private bool show_vertical_right_axis_values=false;
        public bool ShowVerticalRightAxisValues
        {
            get
            {
                return this.show_vertical_right_axis_values;
            }
            set
            {
                this.show_vertical_right_axis_values=value;
                this.ReDrawAfterOptionChange();
            }
        }

        private bool show_vertical_left_axis_values=true;
        public bool ShowVerticalLeftAxisValues
        {
            get
            {
                return this.show_vertical_left_axis_values;
            }
            set
            {
                this.show_vertical_left_axis_values=value;
                this.ReDrawAfterOptionChange();
            }
        }

        
        private int vertical_left_axis_values_angle=0;
        public int VerticalLeftAxisValuesAngle
        {
            get
            {
                return this.vertical_left_axis_values_angle;
            }
            set
            {
                this.vertical_left_axis_values_angle=value;
                this.ReDrawAfterOptionChange();
            }
        }
        private int vertical_right_axis_values_angle=0;
        public int VerticalRightAxisValuesAngle
        {
            get
            {
                return this.vertical_right_axis_values_angle;
            }
            set
            {
                this.vertical_right_axis_values_angle=value;
                this.ReDrawAfterOptionChange();
            }
        }

        

        private int space_between_plots=50;
        /// <summary>
        /// in % of plot with
        /// </summary>
        public int SpaceBetweenPlots
        {
            get
            {
                return this.space_between_plots;
            }
            set
            {
                this.space_between_plots=value;
                this.ReDrawAfterOptionChange();
            }
        }

        private bool show_plots_label=true;
        public bool ShowPlotsLabel
        {
            get
            {
                return this.show_plots_label;
            }
            set
            {
                this.show_plots_label=value;
                this.ReDrawAfterOptionChange();
            }
        }
        private System.Drawing.Font font_labels;
        public System.Drawing.Font LabelsFont
        {
            get
            {
                return this.font_labels;
            }
            set
            {
                this.font_labels=value;
                this.ReDrawAfterOptionChange();
            }
        }
        private GRAPH_TYPE graph_type=GRAPH_TYPE.BAR;
        public GRAPH_TYPE GraphType
        {
            get
            {
                return this.graph_type;
            }
            set
            {
                this.graph_type=value;
                this.ReDrawAfterOptionChange();
            }
        }
        private BAR_TYPE bar_type=BAR_TYPE.SPLITTED;
        public BAR_TYPE BarType
        {
            get
            {
                return this.bar_type;
            }
            set
            {
                this.bar_type=value;
                this.ReDrawAfterOptionChange();
            }
        }

        private bool autosize=true;
        public bool AutoSize
        {
            get
            {
                return this.autosize;
            }
            set
            {
                this.autosize=value;
                this.ReDrawAfterOptionChange();
            }
        }
        private bool autosize_increase_only=true;
        public bool AutoSizeIncreaseOnly
        {
            get
            {
                return this.autosize_increase_only;
            }
            set
            {
                this.autosize_increase_only=value;
                for (int cnt=0;cnt<this.al_plot_info_list.Count;cnt++)
                    ((CPlotInfoList)this.al_plot_info_list[cnt]).FixedLengthDontForgetMinMax=this.autosize_increase_only;
            }
        }
        private double autosize_min_value=0;
        private double autosize_max_value=0;
        private double autosize_max_plots=0;

        private double min_value=0;
        public double MinValue
        {
            get
            {
                return this.min_value;
            }
            set
            {
                this.min_value=value;
                this.ReDrawAfterOptionChange();
            }
        }

        private double max_value=50;
        public double MaxValue
        {
            get
            {
                return this.max_value;
            }
            set
            {
                this.max_value=value;
                this.ReDrawAfterOptionChange();
            }
        }

        private bool show_horizontal_grid=false;
        public bool ShowHorizontalGrid
        {
            get
            {
                return this.show_horizontal_grid;
            }
            set
            {
                this.show_horizontal_grid=value;
                this.ReDrawAfterOptionChange();
            }
        }
        private System.Drawing.Pen horizontal_grid_pen=null;
        public System.Drawing.Pen HorizontalGridPen
        {
            get
            {
                return this.horizontal_grid_pen;
            }
            set
            {
                this.horizontal_grid_pen=value;
                this.ReDrawAfterOptionChange();
            }
        }

        private CAxis horizontal_axis=null;
        public CAxis HorizontalAxis
        {
            get
            {
                return this.horizontal_axis;
            }
            set
            {
                this.horizontal_axis=value;
                this.ReDrawAfterOptionChange();
            }
        }
        private CAxis vertical_left_axis=null;
        public CAxis VerticalLeftAxis
        {
            get
            {
                return this.vertical_left_axis;
            }
            set
            {
                this.vertical_left_axis=value;
                this.ReDrawAfterOptionChange();
            }
        }
        private CAxis vertical_right_axis=null;
        public CAxis VerticalRightAxis
        {
            get
            {
                return this.vertical_right_axis;
            }
            set
            {
                this.vertical_right_axis=value;
                this.ReDrawAfterOptionChange();
            }
        }

        private System.Collections.ArrayList al_plot_info_list=null;

        /// <summary>
        /// return the number of defined plot lists
        /// </summary>
        public int ListsNumber
        {
            get
            {
                return this.al_plot_info_list.Count;
            }
        }        

        #endregion

        #region const
        private const int LEGEND_SPACE=5;
        private const int MAX_IF_NO_VALUE=10;
        #endregion

        public CSimpleChart()
        {
            this.font_labels=new System.Drawing.Font( new System.Drawing.FontFamily( "Arial" ), 10);
            this.HorizontalAxis=new CAxis(new CAxis.RedrawHandler(this.ReDrawAfterOptionChange));
            this.HorizontalAxis.Label.PositionRelativeToControl=Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_RIGHT;
            this.VerticalLeftAxis=new CAxis(new CAxis.RedrawHandler(this.ReDrawAfterOptionChange));
            this.VerticalLeftAxis.Label.PositionRelativeToControl=Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_TOP;
            this.VerticalRightAxis=new CAxis(new CAxis.RedrawHandler(this.ReDrawAfterOptionChange));
            this.VerticalRightAxis.Label.PositionRelativeToControl=Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_TOP;
            this.VerticalRightAxis.Visible=false;
            this.al_plot_info_list=new System.Collections.ArrayList();
            this.horizontal_grid_pen=new System.Drawing.Pen(new System.Drawing.SolidBrush(System.Drawing.Color.Black));
        }


        #region methodes

        /// <summary>
        /// remove all points and point lists
        /// </summary>
        public void Clear()
        {
            this.RemoveAllPoints();
            this.al_plot_info_list.Clear();
        }
        /// <summary>
        /// remove only points. point list and their options are kept
        /// </summary>
        public void RemoveAllPoints()
        {
            for(int cnt=0;cnt<this.al_plot_info_list.Count;cnt++)
                ((CPlotInfoList)this.al_plot_info_list[cnt]).Clear();
            this.ReDrawAfterOptionChange();
        }
        /// <summary>
        /// Add plot to Chart
        /// </summary>
        /// <param name="list_index">0 based index. value can be in the inferior or egal to NumberOfLists (if it's egal, a new list is added)</param>
        /// <param name="plot_value"></param>
        /// <returns>false if bad list index value</returns>
        public bool AddPlot(int list_index,float plot_value)
        {
            return this.AddPlot(list_index,plot_value,"");
        }
        /// <summary>
        /// Add plot to Chart
        /// </summary>
        /// <param name="list_index">0 based index. value can be in the inferior or egal to NumberOfLists (if it's egal, a new list is added)</param>
        /// <param name="plot_value"></param>
        /// <param name="plot_label"></param>
        /// <returns>false if bad list index value</returns>
        public bool AddPlot(int list_index,float plot_value,string plot_label)
        {
            int list_size=this.al_plot_info_list.Count;
            if (list_index>list_size)
                return false;
            if (list_index==list_size)
                this.AddPlotInfoList();

            ((CPlotInfoList)this.al_plot_info_list[list_index]).AddPlot(plot_value,plot_label);
            return true;
        }

        public CPlotInfoList GetPlotInfoList(int index)
        {
            if (index>=this.al_plot_info_list.Count)
                return null;
            return ((CPlotInfoList)this.al_plot_info_list[index]);
        }

        /// <summary>
        /// Optional call to create a new list.
        /// Use it to set CPlotInfoList members before adding plots
        /// </summary>
        /// <returns></returns>
        public CPlotInfoList AddPlotInfoList()
        {
            CPlotInfoList plot_info_list=new CPlotInfoList(new CPlotInfoList.RedrawHandler(this.ReDrawAfterOptionChange));
            plot_info_list.FixedLengthDontForgetMinMax=this.autosize_increase_only;
            this.al_plot_info_list.Add(plot_info_list);
            return plot_info_list;
        }
        #endregion

        #region end cap width height computing
        private void comput_end_cap_needed_size()
        {
            float cap_width;
            cap_width=0;
            this.horizontal_axis_arrow_height=0;
            try
            {
                cap_width=((System.Drawing.Drawing2D.AdjustableArrowCap)this.horizontal_axis.Pen.CustomEndCap).Width;
                this.horizontal_axis_arrow_height=(int)((System.Drawing.Drawing2D.AdjustableArrowCap)this.horizontal_axis.Pen.CustomEndCap).Height;;
            }
            catch{}// if no CustomEndCap is defined an error is thrown as CustomEndCap is not reachable
            this.horizontal_axis_arrow_half_width=this.get_end_cap_needed_size(cap_width,this.horizontal_axis.Pen.Width);

            cap_width=0;
            this.vertical_left_axis_arrow_height=0;
            try
            {
                cap_width=((System.Drawing.Drawing2D.AdjustableArrowCap)this.vertical_left_axis.Pen.CustomEndCap).Width;
                this.vertical_left_axis_arrow_height=(int)((System.Drawing.Drawing2D.AdjustableArrowCap)this.vertical_left_axis.Pen.CustomEndCap).Height;
            }
            catch{}// if no CustomEndCap is defined an error is thrown as CustomEndCap is not reachable
            this.vertical_left_axis_arrow_half_width=this.get_end_cap_needed_size(cap_width,this.vertical_left_axis.Pen.Width);

            cap_width=0;
            this.vertical_right_axis_arrow_height=0;
            try
            {
                cap_width=((System.Drawing.Drawing2D.AdjustableArrowCap)this.vertical_right_axis.Pen.CustomEndCap).Width;
                this.vertical_right_axis_arrow_height=(int)((System.Drawing.Drawing2D.AdjustableArrowCap)this.vertical_right_axis.Pen.CustomEndCap).Height;
            }
            catch{}// if no CustomEndCap is defined an error is thrown as CustomEndCap is not reachable
            this.vertical_right_axis_arrow_half_width=this.get_end_cap_needed_size(cap_width,this.vertical_right_axis.Pen.Width);
        }
        private int get_end_cap_needed_size(float cap_width,float pen_width)
        {
            return (int)(cap_width/2*pen_width+pen_width);
        }
        #endregion

        #region drawing

        /// <summary>
        /// Enable 
        /// </summary>
        public void Draw()
        {
            this.b_is_drawn=true;
            this.ReDraw();
        }

        private void ReDrawAfterOptionChange()
        {
            if (!this.auto_redraw)
                return;
            this.ReDraw();
        }

        private void ReDraw()
        {
            if (!this.b_is_drawn)
                return;
            this.comput_end_cap_needed_size();
            this.Refresh();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.ReDraw();
        }

        // declare used for the OnPaint event (avoid reallocating memory on each call)
        #region OnPaint vars
        private CPlotInfo OnPaint_plot;
        private CPlotInfoList OnPaint_plot_list;
        private int OnPaint_cnt;
        private int OnPaint_cnt2;
        private float OnPaint_cnt_float;
        private int OnPaint_tmp;
        private int OnPaint_remaining_height;
        private int OnPaint_remaining_width;
        private double OnPaint_x_scale;
        private double OnPaint_y_scale;
        private int OnPaint_x_origin;
        private int OnPaint_y_origin;
        private float OnPaint_top_height;
        private float OnPaint_bottom_height;
        private float OnPaint_left_width;
        private float OnPaint_right_width;
        private float OnPaint_plot_x;
        private float OnPaint_plot_y;
        private float OnPaint_previous_plot_x;
        private float OnPaint_previous_plot_y;
        private string OnPaint_str;
        private string OnPaint_str2;
        private System.Drawing.SizeF OnPaint_used_size;
        private System.Drawing.SizeF OnPaint_legend_used_size=new System.Drawing.SizeF(0,0);
        private float OnPaint_legend_left_width;
        private float OnPaint_legend_right_width;
        private float OnPaint_legend_top_height;
        private float OnPaint_legend_bottom_height;
        private float OnPaint_legend_x_delta;
        private float OnPaint_legend_y_delta;
        private System.Drawing.Point OnPaint_point;
        private System.Drawing.Graphics OnPaint_graph;
        private CAxis OnPaint_axis;
        private int OnPaint_arrow_required_sized;
        #endregion

        #region get axis label used size
        /// <summary>
        /// must be call only in OnPaint event
        /// </summary>
        /// <param name="?"></param>
        private void OnPaint_get_axis_used_size()
        {
            this.OnPaint_tmp=(int)Math.Max(this.OnPaint_arrow_required_sized,this.OnPaint_axis.Pen.Width);
            if ((this.OnPaint_axis.Visible)&&(this.OnPaint_axis.Label.Visible)&&(this.OnPaint_axis.Label.Label!=""))
            {
                this.OnPaint_used_size=Tools.Drawing.CDrawString.UsedSize(
                    this.OnPaint_graph,
                    this.OnPaint_axis.Label.Label,
                    this.OnPaint_axis.Label.Angle,
                    this.OnPaint_axis.Label.Font
                    );
                if (this.autosize_min_value>=0)
                {
                    switch (this.OnPaint_axis.Label.PositionRelativeToControl)
                    {
                        case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_BOTTOM:
                        case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_BOTTOM_RIGHT:
                        case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_BOTTOM_LEFT:
                        case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_RIGHT:
                        case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_LEFT:
                            this.OnPaint_bottom_height=Math.Max(this.OnPaint_bottom_height,this.OnPaint_used_size.Height);
                            break;
                    }
                }
                if (this.autosize_max_value<=0)
                {
                    switch (this.OnPaint_axis.Label.PositionRelativeToControl)
                    {
                        case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_TOP:
                        case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_TOP_RIGHT:
                        case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_TOP_LEFT:
                        case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_RIGHT:
                        case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_LEFT:
                            this.OnPaint_top_height=Math.Max(this.OnPaint_top_height,this.OnPaint_used_size.Height+this.OnPaint_tmp);
                            break;
                    }
                }
                switch (this.OnPaint_axis.Label.PositionRelativeToControl)
                {
                    case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_TOP_RIGHT:
                    case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_RIGHT:
                    case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_BOTTOM_RIGHT:
                        this.OnPaint_right_width=Math.Max(this.OnPaint_right_width,this.OnPaint_used_size.Width+this.OnPaint_tmp);
                        break;
                    case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_TOP_LEFT:
                    case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_LEFT:
                    case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_BOTTOM_LEFT:
                        this.OnPaint_left_width=Math.Max(this.OnPaint_left_width,this.OnPaint_used_size.Width+this.OnPaint_tmp);
                        break;
                    case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_TOP:
                        this.OnPaint_top_height=Math.Max(this.OnPaint_top_height,this.OnPaint_used_size.Height+this.OnPaint_tmp);
                        this.OnPaint_left_width=Math.Max(this.OnPaint_left_width,this.OnPaint_used_size.Width/2);
                        this.OnPaint_right_width=Math.Max(this.OnPaint_right_width,this.OnPaint_used_size.Width/2);
                        break;
                    case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_BOTTOM:
                        this.OnPaint_bottom_height=Math.Max(this.OnPaint_bottom_height,this.OnPaint_used_size.Height);
                        this.OnPaint_left_width=Math.Max(this.OnPaint_left_width,this.OnPaint_used_size.Width/2);
                        this.OnPaint_right_width=Math.Max(this.OnPaint_right_width,this.OnPaint_used_size.Width/2);
                        break;
                }
            }
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!this.b_is_drawn)// just to earn some time
                return;
            base.OnPaint (e);
            
            #region init
            this.OnPaint_x_origin=0;
            this.OnPaint_y_origin=0;
            this.OnPaint_plot_x=0;
            this.OnPaint_plot_y=0;
            this.OnPaint_previous_plot_x=0;
            this.OnPaint_previous_plot_y=0;
            this.OnPaint_legend_used_size.Width=0;
            this.OnPaint_legend_used_size.Height=0;

            this.OnPaint_top_height=0;
            this.OnPaint_bottom_height=0;
            this.OnPaint_left_width=0;
            this.OnPaint_right_width=0;
            this.autosize_max_plots=0;
            this.autosize_max_value=0;
            this.autosize_min_value=0;

            this.OnPaint_legend_left_width=0;
            this.OnPaint_legend_right_width=0;
            this.OnPaint_legend_top_height=0;
            this.OnPaint_legend_bottom_height=0;
            this.OnPaint_legend_x_delta=0;
            this.OnPaint_legend_y_delta=0;
            #endregion

            ///////////////////////////////
            /// get max number of plots, min and max values if not autosized, and necessary space for plots labels drawing
            ///////////////////////////////
            #region
            // for each list
            for(this.OnPaint_cnt=0;this.OnPaint_cnt<this.al_plot_info_list.Count;this.OnPaint_cnt++)
            {
                this.OnPaint_plot_list=(CPlotInfoList)this.al_plot_info_list[this.OnPaint_cnt];
                // number of plots
                this.autosize_max_plots=Math.Max(this.autosize_max_plots,this.OnPaint_plot_list.PlotsNumber);
                this.autosize_max_plots=Math.Max(this.autosize_max_plots,this.OnPaint_plot_list.FixedLength);
                // min and max values if not autosized
                if (this.autosize)
                {
                    this.autosize_max_value=Math.Max(this.autosize_max_value,this.OnPaint_plot_list.MaxValue);
                    this.autosize_min_value=Math.Min(this.autosize_min_value,this.OnPaint_plot_list.MinValue);
                }
                // necessary space for plots labels drawing
                if (this.show_plots_label)
                {
                    // for each plot
                    for (this.OnPaint_cnt2=0;this.OnPaint_cnt2<this.OnPaint_plot_list.PlotsNumber;this.OnPaint_cnt2++)
                    {
                        this.OnPaint_plot=this.OnPaint_plot_list.GetPlotInfo(this.OnPaint_cnt2);
                        if (this.OnPaint_plot.Visible)
                        {
                            if (this.OnPaint_plot.Label!="")
                            {
                                this.OnPaint_used_size=Tools.Drawing.CDrawString.UsedSize(e.Graphics,this.OnPaint_plot.Label,this.OnPaint_plot.Angle,this.OnPaint_plot.Font);
                                switch(this.OnPaint_plot.PositionRelativeToControl)
                                {
                                    case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_TOP:
                                    case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_TOP_RIGHT:
                                    case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_TOP_LEFT:
                                        // this is max may used value. This space is may not fully necessary
                                        this.OnPaint_top_height=Math.Max(this.OnPaint_top_height,this.OnPaint_used_size.Height);
                                        break;
                                    case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_BOTTOM:
                                    case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_BOTTOM_RIGHT:
                                    case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_BOTTOM_LEFT:
                                        // this is max may used value. This space is may not necessary if there's negative values
                                        this.OnPaint_bottom_height=Math.Max(this.OnPaint_bottom_height,this.OnPaint_used_size.Height);
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            // min and max values if autosized
            if (!this.autosize)
            {
                this.autosize_max_value=this.max_value;
                this.autosize_min_value=this.min_value;
            }
            #endregion

            ///////////////////////////////
            /// get necesary space for axis labels and values
            ///////////////////////////////
            #region
            this.OnPaint_graph=e.Graphics;
            this.OnPaint_axis=this.horizontal_axis;
            this.OnPaint_arrow_required_sized=this.horizontal_axis_arrow_half_width;
            this.OnPaint_get_axis_used_size();
            this.OnPaint_axis=this.vertical_left_axis;
            this.OnPaint_arrow_required_sized=this.vertical_left_axis_arrow_half_width;
            this.OnPaint_get_axis_used_size();
            this.OnPaint_axis=this.vertical_right_axis;
            this.OnPaint_arrow_required_sized=this.vertical_right_axis_arrow_half_width;
            this.OnPaint_get_axis_used_size();
            if (this.show_vertical_left_axis_values&&this.vertical_left_axis.Visible)
            {
                this.OnPaint_str=System.Convert.ToInt32(this.autosize_min_value).ToString();
                this.OnPaint_str2=System.Convert.ToInt32(this.autosize_max_value).ToString();
                if (this.OnPaint_str.Length<this.OnPaint_str2.Length)
                    this.OnPaint_str=this.OnPaint_str2;
                this.OnPaint_str=new string('0',this.OnPaint_str.Length);// 0 use most char size
                if (this.autosize_max_value-this.autosize_min_value<10)// there could be ,0 more
                    this.OnPaint_str+="00";
                this.OnPaint_used_size=Tools.Drawing.CDrawString.UsedSize(
                    e.Graphics,
                    this.OnPaint_str,
                    this.vertical_left_axis_values_angle,
                    this.vertical_left_axis.Label.Font);
                // max is for position left
                this.OnPaint_left_width=Math.Max(this.OnPaint_left_width,this.OnPaint_used_size.Width+this.vertical_left_axis_arrow_half_width);
                // for min max value if they are joined to limits
                this.OnPaint_top_height=Math.Max(this.OnPaint_top_height,this.OnPaint_used_size.Height/2);
                this.OnPaint_bottom_height=Math.Max(this.OnPaint_bottom_height,this.OnPaint_used_size.Height/2);
            }
            if (this.show_vertical_right_axis_values&&this.vertical_right_axis.Visible)
            {
                this.OnPaint_str=this.autosize_min_value.ToString();
                this.OnPaint_str2=this.autosize_max_value.ToString();
                if (this.OnPaint_str.Length<this.OnPaint_str2.Length)
                    this.OnPaint_str=this.OnPaint_str2;
                this.OnPaint_used_size=Tools.Drawing.CDrawString.UsedSize(
                    e.Graphics,
                    this.OnPaint_str,
                    this.vertical_right_axis_values_angle,
                    this.vertical_left_axis.Label.Font);
                // max is for position right
                this.OnPaint_right_width=Math.Max(this.OnPaint_right_width,this.OnPaint_used_size.Width+this.vertical_right_axis_arrow_half_width);
                // for min max value if they are joined to limits
                this.OnPaint_top_height=Math.Max(this.OnPaint_top_height,this.OnPaint_used_size.Height/2);
                this.OnPaint_bottom_height=Math.Max(this.OnPaint_bottom_height,this.OnPaint_used_size.Height/2);
            }
            if (this.vertical_left_axis.Visible)
            {
                this.OnPaint_top_height=Math.Max(this.vertical_left_axis.Pen.Width+this.vertical_left_axis_arrow_height,this.OnPaint_top_height);
                this.OnPaint_left_width=Math.Max(this.vertical_left_axis_arrow_half_width,this.OnPaint_left_width);
            }
            if (this.vertical_right_axis.Visible)
            {
                this.OnPaint_top_height=Math.Max(this.vertical_right_axis.Pen.Width+this.vertical_right_axis_arrow_height,this.OnPaint_top_height);
                this.OnPaint_right_width=Math.Max(this.vertical_right_axis_arrow_half_width,this.OnPaint_right_width);
            }
            if (this.horizontal_axis.Visible)
            {
                this.OnPaint_right_width=Math.Max(this.horizontal_axis.Pen.Width+this.horizontal_axis_arrow_height,this.OnPaint_right_width);
                this.OnPaint_top_height=Math.Max(this.horizontal_axis_arrow_half_width,this.OnPaint_top_height);
                this.OnPaint_bottom_height=Math.Max(this.horizontal_axis_arrow_half_width,this.OnPaint_bottom_height);
            }
            #endregion

            ///////////////////////////////
            /// get necesary space for legend
            ///////////////////////////////
            #region 
            if (this.show_legend)
            {
                // for each list
                for(this.OnPaint_cnt=0;this.OnPaint_cnt<this.al_plot_info_list.Count;this.OnPaint_cnt++)
                {
                    this.OnPaint_plot_list=(CPlotInfoList)this.al_plot_info_list[this.OnPaint_cnt];
                    // get text size
                    if (this.OnPaint_plot_list.Legend=="")
                        this.OnPaint_str=" ";
                    else
                        this.OnPaint_str=this.OnPaint_plot_list.Legend;
                    this.OnPaint_used_size=Tools.Drawing.CDrawString.UsedSize(e.Graphics,this.OnPaint_str,0,this.legend_font);
                    this.OnPaint_legend_used_size.Height+=Math.Max(this.legend_font.Size,this.OnPaint_used_size.Height)+LEGEND_SPACE;
                    this.OnPaint_legend_used_size.Width=Math.Max(this.OnPaint_legend_used_size.Width,this.OnPaint_used_size.Width+this.legend_font.Size)+LEGEND_SPACE;
                }
                switch (this.legend_position)
                {
                    case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_BOTTOM:
                        this.OnPaint_legend_bottom_height=this.OnPaint_legend_used_size.Height;
                        this.OnPaint_bottom_height+=this.OnPaint_legend_used_size.Height;
                        this.OnPaint_legend_x_delta=-this.OnPaint_legend_used_size.Width/2;
                        break;
                    case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_TOP:
                        this.OnPaint_legend_top_height=this.OnPaint_legend_used_size.Height;
                        this.OnPaint_top_height+=this.OnPaint_legend_used_size.Height;
                        this.OnPaint_legend_x_delta=-this.OnPaint_legend_used_size.Width/2;
                        this.OnPaint_legend_y_delta=-this.OnPaint_legend_used_size.Height;
                        break;
                    case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_BOTTOM_LEFT:
                        this.OnPaint_legend_x_delta=LEGEND_SPACE-this.OnPaint_legend_used_size.Width;
                        this.OnPaint_legend_y_delta=-this.OnPaint_legend_used_size.Height;
                        this.OnPaint_legend_left_width=this.OnPaint_legend_used_size.Width;
                        this.OnPaint_left_width+=this.OnPaint_legend_used_size.Width;
                        break;
                    case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_TOP_LEFT:
                        this.OnPaint_legend_x_delta=LEGEND_SPACE-this.OnPaint_legend_used_size.Width;
                        this.OnPaint_legend_left_width=this.OnPaint_legend_used_size.Width;
                        this.OnPaint_left_width+=this.OnPaint_legend_used_size.Width;
                        break;
                    case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_LEFT:
                        this.OnPaint_legend_y_delta=-this.OnPaint_legend_used_size.Height/2;
                        this.OnPaint_legend_x_delta=LEGEND_SPACE-this.OnPaint_legend_used_size.Width;
                        this.OnPaint_legend_left_width=this.OnPaint_legend_used_size.Width;
                        this.OnPaint_left_width+=this.OnPaint_legend_used_size.Width;
                        break;
                    case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_RIGHT:
                        this.OnPaint_legend_y_delta=-this.OnPaint_legend_used_size.Height/2;
                        this.OnPaint_legend_right_width=this.OnPaint_legend_used_size.Width;
                        this.OnPaint_right_width+=this.OnPaint_legend_used_size.Width;
                        break;
                    case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_BOTTOM_RIGHT:
                        this.OnPaint_legend_y_delta=-this.OnPaint_legend_used_size.Height;
                        this.OnPaint_legend_right_width=this.OnPaint_legend_used_size.Width;
                        this.OnPaint_right_width+=this.OnPaint_legend_used_size.Width;
                        break;
                    case Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_TOP_RIGHT:
                        this.OnPaint_legend_right_width=this.OnPaint_legend_used_size.Width;
                        this.OnPaint_right_width+=this.OnPaint_legend_used_size.Width;
                        break;
                    case Tools.Drawing.CDrawString.TEXT_POSITION.INSIDE_BOTTOM:
                        this.OnPaint_legend_y_delta=-this.OnPaint_legend_used_size.Height;
                        this.OnPaint_legend_x_delta=-this.OnPaint_legend_used_size.Width/2;
                        break;
                    case Tools.Drawing.CDrawString.TEXT_POSITION.INSIDE_TOP:
                        this.OnPaint_legend_x_delta=-this.OnPaint_legend_used_size.Width/2;
                        break;
                    case Tools.Drawing.CDrawString.TEXT_POSITION.INSIDE_MIDDLE:
                        this.OnPaint_legend_x_delta=-this.OnPaint_legend_used_size.Width/2;
                        this.OnPaint_legend_y_delta=-this.OnPaint_legend_used_size.Height/2;
                        break;
                    case Tools.Drawing.CDrawString.TEXT_POSITION.INSIDE_TOP_RIGHT:
                        this.OnPaint_legend_x_delta=-LEGEND_SPACE-this.OnPaint_legend_used_size.Width-this.OnPaint_right_width;
                        break;
                    case Tools.Drawing.CDrawString.TEXT_POSITION.INSIDE_TOP_LEFT:
                        this.OnPaint_legend_x_delta=LEGEND_SPACE+this.OnPaint_left_width;
                        break;
                    case Tools.Drawing.CDrawString.TEXT_POSITION.INSIDE_BOTTOM_RIGHT:
                        this.OnPaint_legend_y_delta=-this.OnPaint_legend_used_size.Height-this.OnPaint_bottom_height;
                        this.OnPaint_legend_x_delta=-LEGEND_SPACE-this.OnPaint_legend_used_size.Width-this.OnPaint_right_width;
                        break;
                    case Tools.Drawing.CDrawString.TEXT_POSITION.INSIDE_BOTTOM_LEFT:
                        this.OnPaint_legend_y_delta=-this.OnPaint_legend_used_size.Height-this.OnPaint_bottom_height;
                        this.OnPaint_legend_x_delta=LEGEND_SPACE+this.OnPaint_left_width;
                        break;
                    case Tools.Drawing.CDrawString.TEXT_POSITION.INSIDE_MIDDLE_LEFT:
                        this.OnPaint_legend_x_delta=LEGEND_SPACE+this.OnPaint_left_width;
                        this.OnPaint_legend_y_delta=-this.OnPaint_legend_used_size.Height/2;
                        break;
                    case Tools.Drawing.CDrawString.TEXT_POSITION.INSIDE_MIDDLE_RIGHT:
                        this.OnPaint_legend_x_delta=-LEGEND_SPACE-this.OnPaint_legend_used_size.Width-this.OnPaint_right_width;
                        this.OnPaint_legend_y_delta=-this.OnPaint_legend_used_size.Height/2;
                        break;
                }
            }
            #endregion

            ///////////////////////////////
            /// get the remaining chart available space
            ///////////////////////////////
            this.OnPaint_remaining_height=(int)(this.Height-this.OnPaint_top_height-this.OnPaint_bottom_height);
            this.OnPaint_remaining_width=(int)(this.Width-this.OnPaint_left_width-this.OnPaint_right_width);

            // if not enought remaining size for drawing chart
            if ((this.OnPaint_remaining_height<=0)||(this.OnPaint_remaining_width<=0))
                return;

            ///////////////////////////////
            /// comput x and y scale
            ///////////////////////////////
            if (this.autosize_max_value==this.autosize_min_value)
            {
                if (this.autosize_min_value==0)
                    this.autosize_max_value=MAX_IF_NO_VALUE;
                else
                {
                    if (this.autosize_min_value<0)
                        this.autosize_max_value=0;
                    else
                        this.autosize_min_value=0;
                }
            }
            this.OnPaint_y_scale=(double)this.OnPaint_remaining_height/(this.autosize_max_value-this.autosize_min_value);
            if ((this.graph_type==GRAPH_TYPE.BAR)&&(this.bar_type==BAR_TYPE.SPLITTED))
            {
                // act like if there is this.autosize_max_plots*this.al_plot_info_list.Count plots
                this.OnPaint_x_scale=(double)this.OnPaint_remaining_width/(this.autosize_max_plots*this.al_plot_info_list.Count+((double)this.space_between_plots*(this.autosize_max_plots-1)/100));
            }
            else
            {
                if ((this.graph_type==GRAPH_TYPE.BAR)&&(this.bar_type==BAR_TYPE.MIXED))
                    this.OnPaint_x_scale=(double)this.OnPaint_remaining_width/(this.autosize_max_plots+((double)this.space_between_plots*(this.autosize_max_plots-1)/100));
                else
                    this.OnPaint_x_scale=(double)this.OnPaint_remaining_width/(this.autosize_max_plots-1+((double)this.space_between_plots*(this.autosize_max_plots-1)/100));
            }

            this.OnPaint_x_origin=(int)this.OnPaint_left_width;
            this.OnPaint_y_origin=(int)(this.OnPaint_top_height+this.autosize_max_value*this.OnPaint_y_scale);

            // comput tick frequency
            if (this.auto_tick_frequency)
            {
                this.vertical_axis_tick_frequency=(float)(this.autosize_max_value-this.autosize_min_value)/this.auto_tick_frequency_number_of_ticks;
                if(this.vertical_axis_tick_frequency<=0)
                    this.vertical_axis_tick_frequency=1;
                else
                {
                    this.OnPaint_tmp=(int)Math.Floor(Math.Log10(this.autosize_max_value-this.autosize_min_value));
                    this.vertical_axis_tick_frequency=(float)(Math.Ceiling(this.vertical_axis_tick_frequency/Math.Pow(10,this.OnPaint_tmp-1))*Math.Pow(10,this.OnPaint_tmp-1));
                }
            }

            ///////////////////////////////
            /// Draw grid before plots (it's under)
            ///////////////////////////////
            #region
            if (this.show_horizontal_grid)
            {
                for (this.OnPaint_cnt_float=(float)(this.autosize_min_value-this.autosize_min_value%this.vertical_axis_tick_frequency);this.OnPaint_cnt_float<=this.autosize_max_value;this.OnPaint_cnt_float+=this.vertical_axis_tick_frequency)
                {
                    if (this.OnPaint_cnt_float==0)
                        continue;
                    e.Graphics.DrawLine(
                        this.horizontal_grid_pen,
                        new System.Drawing.Point(this.OnPaint_x_origin,(int)(this.OnPaint_y_origin-this.OnPaint_cnt_float*this.OnPaint_y_scale)),
                        new System.Drawing.Point(this.OnPaint_x_origin+this.OnPaint_remaining_width,(int)(this.OnPaint_y_origin-this.OnPaint_cnt_float*this.OnPaint_y_scale))
                        );
                }
            }
            #endregion

            ///////////////////////////////
            /// Draw axis marks and values before plots (it's under)
            ///////////////////////////////
            #region
            // draw axis values if required
            if (this.vertical_left_axis.Visible&&this.show_vertical_left_axis_values)
            {
                for (this.OnPaint_cnt_float=(float)(this.autosize_min_value-this.autosize_min_value%this.vertical_axis_tick_frequency);this.OnPaint_cnt_float<=this.autosize_max_value;this.OnPaint_cnt_float+=this.vertical_axis_tick_frequency)
                {
                    if (this.OnPaint_cnt_float==0)
                        continue;
                    // draw value mark
                    e.Graphics.DrawLine(
                        this.horizontal_grid_pen,
                        new System.Drawing.Point(this.OnPaint_x_origin-(int)(this.vertical_left_axis.Pen.Width*2),(int)(this.OnPaint_y_origin-this.OnPaint_cnt_float*this.OnPaint_y_scale)),
                        new System.Drawing.Point(this.OnPaint_x_origin+(int)(this.vertical_left_axis.Pen.Width*2),(int)(this.OnPaint_y_origin-this.OnPaint_cnt_float*this.OnPaint_y_scale))
                        );
                    if (this.vertical_axis_negative_value_seem_positive)
                    {
                        // draw value string
                        Tools.Drawing.CDrawString.DrawLegend(
                            e.Graphics,
                            Math.Abs(this.OnPaint_cnt_float).ToString(),
                            this.vertical_left_axis_values_angle,
                            Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_LEFT,
                            0,
                            this.vertical_left_axis.Label.Brush,
                            this.vertical_left_axis.Label.Font,
                            (int)(this.OnPaint_x_origin-this.vertical_left_axis.Pen.Width*2),
                            (int)(this.OnPaint_y_origin-this.OnPaint_cnt_float*this.OnPaint_y_scale),
                            (int)(this.OnPaint_x_scale),
                            0
                            );                        
                    }
                    else
                    {
                        // draw value string
                        Tools.Drawing.CDrawString.DrawLegend(
                            e.Graphics,
                            this.OnPaint_cnt_float.ToString(),
                            this.vertical_left_axis_values_angle,
                            Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_LEFT,
                            0,
                            this.vertical_left_axis.Label.Brush,
                            this.vertical_left_axis.Label.Font,
                            (int)(this.OnPaint_x_origin-this.vertical_left_axis.Pen.Width*2),
                            (int)(this.OnPaint_y_origin-this.OnPaint_cnt_float*this.OnPaint_y_scale),
                            (int)(this.OnPaint_x_scale),
                            0
                            ); 
                    }
                }
            }
            // draw axis values if required
            if (this.vertical_right_axis.Visible&&this.show_vertical_right_axis_values)
            {
                for (this.OnPaint_cnt_float=(float)(this.autosize_min_value-this.autosize_min_value%this.vertical_axis_tick_frequency);this.OnPaint_cnt_float<=this.autosize_max_value;this.OnPaint_cnt_float+=this.vertical_axis_tick_frequency)
                {
                    if (this.OnPaint_cnt_float==0)
                        continue;
                    // draw value mark
                    e.Graphics.DrawLine(
                        this.horizontal_grid_pen,
                        new System.Drawing.Point(this.OnPaint_x_origin+this.OnPaint_remaining_width-(int)(this.vertical_right_axis.Pen.Width*2),(int)(this.OnPaint_y_origin-this.OnPaint_cnt_float*this.OnPaint_y_scale)),
                        new System.Drawing.Point(this.OnPaint_x_origin+this.OnPaint_remaining_width+(int)(this.vertical_right_axis.Pen.Width*2),(int)(this.OnPaint_y_origin-this.OnPaint_cnt_float*this.OnPaint_y_scale))
                        );
                    if (this.vertical_axis_negative_value_seem_positive)
                    {
                        // draw value string
                        Tools.Drawing.CDrawString.DrawLegend(
                            e.Graphics,
                            Math.Abs(this.OnPaint_cnt_float).ToString(),
                            this.vertical_right_axis_values_angle,
                            Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_RIGHT,
                            0,
                            this.vertical_right_axis.Label.Brush,
                            this.vertical_right_axis.Label.Font,
                            (int)(this.OnPaint_x_origin+this.OnPaint_remaining_width+this.vertical_right_axis.Pen.Width*2),
                            (int)(this.OnPaint_y_origin-this.OnPaint_cnt_float*this.OnPaint_y_scale),
                            0,
                            0
                            );                        
                    }
                    else
                    {
                        // draw value string
                        Tools.Drawing.CDrawString.DrawLegend(
                            e.Graphics,
                            this.OnPaint_cnt_float.ToString(),
                            this.vertical_right_axis_values_angle,
                            Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_RIGHT,
                            0,
                            this.vertical_right_axis.Label.Brush,
                            this.vertical_right_axis.Label.Font,
                            (int)(this.OnPaint_x_origin+this.OnPaint_remaining_width+this.vertical_right_axis.Pen.Width*2),
                            (int)(this.OnPaint_y_origin-this.OnPaint_cnt_float*this.OnPaint_y_scale),
                            0,
                            0
                            );
                    }
                }
            }
            #endregion

            ///////////////////////////////
            /// Draw plots
            ///////////////////////////////
            #region
            // for each list
            for(this.OnPaint_cnt=0;this.OnPaint_cnt<this.al_plot_info_list.Count;this.OnPaint_cnt++)
            {
                this.OnPaint_plot_list=(CPlotInfoList)this.al_plot_info_list[this.OnPaint_cnt];
                // for each plot
                for (this.OnPaint_cnt2=0;this.OnPaint_cnt2<this.OnPaint_plot_list.PlotsNumber;this.OnPaint_cnt2++)
                {
                    this.OnPaint_plot=this.OnPaint_plot_list.GetPlotInfo(this.OnPaint_cnt2);
                    
                    switch (this.graph_type)
                    {
                        case GRAPH_TYPE.BAR:
                        switch (this.bar_type)
                        {
                            case BAR_TYPE.MIXED:
                                this.OnPaint_plot_x=(float)(this.OnPaint_x_origin+(((double)this.space_between_plots/100+1)*this.OnPaint_cnt2)*this.OnPaint_x_scale);
                                this.OnPaint_plot_y=(float)(this.OnPaint_y_origin-(this.OnPaint_plot.Value*this.OnPaint_y_scale));
                                break;
                            case BAR_TYPE.SPLITTED:
                            default:
                                this.OnPaint_plot_x=(float)(this.OnPaint_x_origin+((this.OnPaint_cnt+(this.al_plot_info_list.Count+(double)this.space_between_plots/100)*this.OnPaint_cnt2)*this.OnPaint_x_scale));
                                this.OnPaint_plot_y=(float)(this.OnPaint_y_origin-(this.OnPaint_plot.Value*this.OnPaint_y_scale));
                                break;
                        }
                            ///////////////////////////////
                            /// Draw bars
                            ///////////////////////////////
                                    
                            // FillRectangle height must be > 0
                            if (this.OnPaint_plot.Value>=0)
                            {
                                e.Graphics.FillRectangle(
                                    ((CPlotInfoList)this.al_plot_info_list[this.OnPaint_cnt]).GraphBrush,
                                    this.OnPaint_plot_x,
                                    this.OnPaint_plot_y,
                                    (float)this.OnPaint_x_scale,
                                    (float)(this.OnPaint_plot.Value*this.OnPaint_y_scale)
                                    );
                            }
                            else
                            {
                                e.Graphics.FillRectangle(
                                    ((CPlotInfoList)this.al_plot_info_list[this.OnPaint_cnt]).GraphBrush,
                                    this.OnPaint_plot_x,
                                    (float)this.OnPaint_y_origin,
                                    (float)this.OnPaint_x_scale,
                                    (float)(-this.OnPaint_plot.Value*this.OnPaint_y_scale)
                                    );
                            }
                            break;
                        case GRAPH_TYPE.LINE:
                        case GRAPH_TYPE.LINE_FILL:
                        default:
                            this.OnPaint_plot_x=(float)(this.OnPaint_x_origin+(((double)this.space_between_plots/100+1)*this.OnPaint_cnt2)*this.OnPaint_x_scale);
                            this.OnPaint_plot_y=(float)(this.OnPaint_y_origin-(this.OnPaint_plot.Value*this.OnPaint_y_scale));
                            if (this.OnPaint_cnt2>0)// To draw line we need the previous plot
                            {
                                if (this.graph_type==GRAPH_TYPE.LINE)
                                    e.Graphics.DrawLine(
                                        new System.Drawing.Pen(((CPlotInfoList)this.al_plot_info_list[this.OnPaint_cnt]).GraphBrush),
                                        new System.Drawing.Point((int)this.OnPaint_previous_plot_x,(int)this.OnPaint_previous_plot_y),
                                        new System.Drawing.Point((int)this.OnPaint_plot_x,(int)this.OnPaint_plot_y)
                                        );
                                else
                                    e.Graphics.FillPolygon(
                                        ((CPlotInfoList)this.al_plot_info_list[this.OnPaint_cnt]).GraphBrush,
                                        new System.Drawing.Point[]
                                        {
                                            new System.Drawing.Point((int)this.OnPaint_previous_plot_x,(int)this.OnPaint_y_origin),
                                            new System.Drawing.Point((int)this.OnPaint_previous_plot_x,(int)this.OnPaint_previous_plot_y),
                                            new System.Drawing.Point((int)this.OnPaint_plot_x,(int)this.OnPaint_plot_y),
                                            new System.Drawing.Point((int)this.OnPaint_plot_x,(int)this.OnPaint_y_origin)
                                        }
                                        );
                            }
                            this.OnPaint_previous_plot_x=this.OnPaint_plot_x;
                            this.OnPaint_previous_plot_y=this.OnPaint_plot_y;
                            break;
                    }

                }
            }// end of plots drawing
            #endregion

            ///////////////////////////////
            /// Draw plots label after plots (it's upper)
            ///////////////////////////////
            #region
            if (this.show_plots_label)
            {
                // for each list
                for(this.OnPaint_cnt=0;this.OnPaint_cnt<this.al_plot_info_list.Count;this.OnPaint_cnt++)
                {
                    this.OnPaint_plot_list=(CPlotInfoList)this.al_plot_info_list[this.OnPaint_cnt];
                    // for each plot
                    for (this.OnPaint_cnt2=0;this.OnPaint_cnt2<this.OnPaint_plot_list.PlotsNumber;this.OnPaint_cnt2++)
                    {
                        this.OnPaint_plot=this.OnPaint_plot_list.GetPlotInfo(this.OnPaint_cnt2);
                    
                        switch (this.graph_type)
                        {
                            case GRAPH_TYPE.BAR:
                            switch (this.bar_type)
                            {
                                case BAR_TYPE.MIXED:
                                    this.OnPaint_plot_x=(float)(this.OnPaint_x_origin+(((double)this.space_between_plots/100+1)*this.OnPaint_cnt2)*this.OnPaint_x_scale);
                                    break;
                                case BAR_TYPE.SPLITTED:
                                default:
                                    this.OnPaint_plot_x=(float)(this.OnPaint_x_origin+((this.OnPaint_cnt+(this.al_plot_info_list.Count+(double)this.space_between_plots/100)*this.OnPaint_cnt2)*this.OnPaint_x_scale));
                                    break;
                            }
                                ///////////////////////////////
                                /// Draw plots labels
                                ///////////////////////////////
                                if (this.OnPaint_plot.Visible&&(this.OnPaint_plot.Label!=""))
                                {
                                    Tools.Drawing.CDrawString.DrawLegend(
                                        e.Graphics,
                                        this.OnPaint_plot.Label,
                                        this.OnPaint_plot.Angle,
                                        this.OnPaint_plot.PositionRelativeToControl,
                                        0,
                                        this.OnPaint_plot.Brush,
                                        this.OnPaint_plot.Font,
                                        (int)this.OnPaint_plot_x,
                                        this.OnPaint_y_origin,
                                        (int)this.OnPaint_x_scale,
                                        (int)-(this.OnPaint_plot.Value*this.OnPaint_y_scale)
                                        );

                                } 
                                break;
                            case GRAPH_TYPE.LINE:
                            case GRAPH_TYPE.LINE_FILL:
                            default:
                                this.OnPaint_plot_x=(float)(this.OnPaint_x_origin+(((double)this.space_between_plots/100+1)*this.OnPaint_cnt2)*this.OnPaint_x_scale);
                                ///////////////////////////////
                                /// Draw plots labels
                                ///////////////////////////////
                                if (this.OnPaint_plot.Visible&&(this.OnPaint_plot.Label!=""))
                                {
                                    Tools.Drawing.CDrawString.DrawLegend(
                                        e.Graphics,
                                        this.OnPaint_plot.Label,
                                        this.OnPaint_plot.Angle,
                                        this.OnPaint_plot.PositionRelativeToControl,
                                        0,
                                        this.OnPaint_plot.Brush,
                                        this.OnPaint_plot.Font,
                                        (int)this.OnPaint_plot_x,
                                        this.OnPaint_y_origin,
                                        0,
                                        (int)-(this.OnPaint_plot.Value*this.OnPaint_y_scale)
                                        );

                                } 
                                break;
                        }

                    }
                }// end of label drawing
            }
            #endregion
            ///////////////////////////////
            /// Draw axis after plots (it's upper)
            ///////////////////////////////
            #region drawing axis
            if (this.horizontal_axis.Visible)
            {
                // draw axis
                e.Graphics.DrawLine(
                    this.horizontal_axis.Pen,
                    this.OnPaint_x_origin,
                    this.OnPaint_y_origin,
                    this.OnPaint_x_origin+this.OnPaint_remaining_width+this.horizontal_axis_arrow_height-this.horizontal_axis.Pen.Width,
                    this.OnPaint_y_origin
                    );
                // axis legend
                if (this.horizontal_axis.Label.Visible&&(this.horizontal_axis.Label.Label!=""))
                {
                    Tools.Drawing.CDrawString.DrawLegend(
                        e.Graphics,
                        this.horizontal_axis.Label.Label,
                        this.horizontal_axis.Label.Angle,
                        this.horizontal_axis.Label.PositionRelativeToControl,
                        0,
                        this.horizontal_axis.Label.Brush,
                        this.horizontal_axis.Label.Font,
                        this.OnPaint_x_origin,
                        this.OnPaint_y_origin,
                        this.OnPaint_remaining_width+this.horizontal_axis_arrow_height,
                        this.horizontal_axis_arrow_half_width*2
                        );
                }
            }

            // vertical left axis drawing
            if (this.vertical_left_axis.Visible)
            {
                // draw axis
                e.Graphics.DrawLine(
                    this.vertical_left_axis.Pen,
                    this.OnPaint_x_origin,
                    (float)(this.OnPaint_y_origin-this.autosize_min_value*this.OnPaint_y_scale),
                    this.OnPaint_x_origin,
                    (float)(this.OnPaint_y_origin-this.autosize_min_value*this.OnPaint_y_scale-this.OnPaint_remaining_height-this.vertical_left_axis_arrow_height-this.vertical_left_axis.Pen.Width)
                    );
                // axis legend
                if (this.vertical_left_axis.Label.Visible&&(this.vertical_left_axis.Label.Label!=""))
                {
                    Tools.Drawing.CDrawString.DrawLegend(
                        e.Graphics,
                        this.vertical_left_axis.Label.Label,
                        this.vertical_left_axis.Label.Angle,
                        this.vertical_left_axis.Label.PositionRelativeToControl,
                        0,
                        this.vertical_left_axis.Label.Brush,
                        this.vertical_left_axis.Label.Font,
                        this.OnPaint_x_origin,
                        (int)(this.OnPaint_y_origin-this.autosize_min_value*this.OnPaint_y_scale-this.OnPaint_remaining_height-this.vertical_left_axis_arrow_height),
                        this.vertical_left_axis_arrow_half_width*2,
                        this.OnPaint_remaining_height+this.vertical_left_axis_arrow_height
                        );
                }

            }
       
            // vertical right axis drawing
            if (this.vertical_right_axis.Visible)
            {
                // draw axis
                e.Graphics.DrawLine(
                    this.vertical_right_axis.Pen,
                    this.OnPaint_x_origin+this.OnPaint_remaining_width,
                    (float)(this.OnPaint_y_origin-this.autosize_min_value*this.OnPaint_y_scale),
                    this.OnPaint_x_origin+this.OnPaint_remaining_width,
                    (float)(this.OnPaint_y_origin-this.autosize_min_value*this.OnPaint_y_scale-this.OnPaint_remaining_height-this.vertical_right_axis_arrow_height-this.vertical_right_axis.Pen.Width)
                    );
                // axis legend
                if (this.vertical_right_axis.Label.Visible&&(this.vertical_right_axis.Label.Label!=""))
                {
                    Tools.Drawing.CDrawString.DrawLegend(
                        e.Graphics,
                        this.vertical_right_axis.Label.Label,
                        this.vertical_right_axis.Label.Angle,
                        this.vertical_right_axis.Label.PositionRelativeToControl,
                        0,
                        this.vertical_right_axis.Label.Brush,
                        this.vertical_right_axis.Label.Font,
                        this.OnPaint_x_origin+this.OnPaint_remaining_width,
                        (int)(this.OnPaint_y_origin-this.autosize_min_value*this.OnPaint_y_scale-this.OnPaint_remaining_height-this.vertical_right_axis_arrow_height),
                        this.vertical_right_axis_arrow_half_width,
                        this.OnPaint_remaining_height+this.vertical_right_axis_arrow_height
                        );
                }
            }
            #endregion

            ///////////////////////////////
            /// Draw legend after plots (it's upper)
            ///////////////////////////////
            #region 
            if (this.show_legend)
            {
                this.OnPaint_previous_plot_y=this.OnPaint_legend_top_height;

                // for each list
                for(this.OnPaint_cnt=0;this.OnPaint_cnt<this.al_plot_info_list.Count;this.OnPaint_cnt++)
                {
                    this.OnPaint_plot_list=(CPlotInfoList)this.al_plot_info_list[this.OnPaint_cnt];

                    // fake text to get text Height if > this.legend_font.Size
                    if (this.OnPaint_plot_list.Legend=="")
                        this.OnPaint_str=" ";
                    else
                        this.OnPaint_str=this.OnPaint_plot_list.Legend;
                        
                    // get text size
                    this.OnPaint_used_size=Tools.Drawing.CDrawString.UsedSize(e.Graphics,this.OnPaint_str,0,this.legend_font);
                    // get position
                    this.OnPaint_point=Tools.Drawing.CDrawString.GetPosition(
                        new System.Drawing.SizeF(0,0),
                        this.legend_position,
                        0,
                        (int)(this.OnPaint_legend_left_width),
                        (int)(this.OnPaint_previous_plot_y),
                        (int)(this.OnPaint_remaining_width+this.OnPaint_left_width-this.OnPaint_legend_left_width+this.OnPaint_right_width-this.OnPaint_legend_right_width),
                        (int)(this.OnPaint_remaining_height+this.OnPaint_top_height-this.OnPaint_legend_top_height+this.OnPaint_bottom_height-this.OnPaint_legend_bottom_height)
                        );
                    this.OnPaint_point.X+=(int)this.OnPaint_legend_x_delta;
                    this.OnPaint_point.Y+=(int)this.OnPaint_legend_y_delta;
                    // draw color square
                    e.Graphics.FillRectangle(
                        this.OnPaint_plot_list.GraphBrush,
                        this.OnPaint_point.X,
                        this.OnPaint_point.Y+LEGEND_SPACE/2,
                        this.OnPaint_used_size.Height-LEGEND_SPACE,
                        this.OnPaint_used_size.Height-LEGEND_SPACE
                        );

                    // Draw legend
                    if (this.OnPaint_plot_list.Legend!="")
                    {
                        Tools.Drawing.CDrawString.DrawString(
                            e.Graphics,
                            this.OnPaint_plot_list.Legend,
                            (int)(this.OnPaint_point.X+this.legend_font.Size+LEGEND_SPACE+this.OnPaint_used_size.Width/2),
                            (int)(this.OnPaint_point.Y+this.OnPaint_used_size.Height/2),
                            0,
                            this.legend_brush,
                            this.legend_font
                            );
                    }

                    this.OnPaint_previous_plot_y+=Math.Max(this.legend_font.Size,this.OnPaint_used_size.Height)+LEGEND_SPACE;
                }

            }
            #endregion
        }

        #endregion
    }
}
