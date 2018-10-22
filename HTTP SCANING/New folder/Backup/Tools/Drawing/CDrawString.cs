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

namespace Tools.Drawing
{
	public class CDrawString
	{
        public enum TEXT_POSITION
        {
            INSIDE_TOP,
            INSIDE_TOP_RIGHT,
            INSIDE_TOP_LEFT,
            INSIDE_MIDDLE,
            INSIDE_MIDDLE_LEFT,
            INSIDE_MIDDLE_RIGHT,
            INSIDE_BOTTOM,
            INSIDE_BOTTOM_RIGHT,
            INSIDE_BOTTOM_LEFT,
            OUTSIDE_TOP,
            OUTSIDE_TOP_RIGHT,
            OUTSIDE_TOP_LEFT,
            OUTSIDE_BOTTOM,
            OUTSIDE_BOTTOM_RIGHT,
            OUTSIDE_BOTTOM_LEFT,
            OUTSIDE_LEFT,
            OUTSIDE_RIGHT
        }

        public static SizeF UsedSize(Graphics graph,string str,float angle_deg,Font font)
        {
            if (str=="")
                return new SizeF(0,0);
            // comput used size
            SizeF drawStringSize=graph.MeasureString(str, font);
            SizeF used_size;
            if (angle_deg!=0)
            {
                used_size=new SizeF(
                    (float)(Math.Abs(drawStringSize.Width*Math.Cos(angle_deg*Math.PI/180))+Math.Abs(drawStringSize.Height*Math.Sin(angle_deg*Math.PI/180))),
                    (float)(Math.Abs(drawStringSize.Width*Math.Sin(angle_deg*Math.PI/180))+Math.Abs(drawStringSize.Height*Math.Cos(angle_deg*Math.PI/180)))
                    );
            }
            else// try to earn some time
                used_size= drawStringSize;
            return used_size;
        }
        public static void DrawLegend(Graphics graph,string str,float angle_deg,TEXT_POSITION pos,int component_x,int component_y,int component_width,int component_height)
        {
            DrawLegend(graph,
                        str,
                        angle_deg,
                        pos,
                        0,
                        new SolidBrush(Color.Black),
                        new Font("Arial",12),
                        component_x,
                        component_y,
                        component_width,
                        component_height
                        );
        }
        public static Point GetPosition(SizeF used_size,TEXT_POSITION pos,int space,int component_x,int component_y,int component_width,int component_height)
        {
            if (component_width<0)
            {
                component_width=-component_width;
                component_x-=component_width;
            }
            if (component_height<0)
            {
                component_height=-component_height;
                component_y-=component_height;
            }

            int x;
            int y;

            switch(pos)
            {
                case TEXT_POSITION.INSIDE_TOP:
                    x=component_x+component_width/2;
                    y=component_y+space+(int)(used_size.Height)/2;
                    break;
                case TEXT_POSITION.INSIDE_MIDDLE:
                    x=component_x+component_width/2;
                    y=component_y+component_height/2;
                    break;
                case TEXT_POSITION.INSIDE_MIDDLE_LEFT:
                    x=component_x+space+(int)(used_size.Width)/2;
                    y=component_y+component_height/2;
                    break;
                case TEXT_POSITION.INSIDE_MIDDLE_RIGHT:
                    x=component_x+component_width-space-(int)(used_size.Width)/2;
                    y=component_y+component_height/2;
                    break;
                case TEXT_POSITION.INSIDE_BOTTOM:
                    x=component_x+component_width/2;
                    y=component_y+component_height-space-(int)(used_size.Height)/2;
                    break;
                case TEXT_POSITION.OUTSIDE_LEFT:
                    x=component_x-space-(int)(used_size.Width)/2;
                    y=component_y+component_height/2;
                    break;
                case TEXT_POSITION.OUTSIDE_RIGHT:
                    x=component_x+component_width+space+(int)(used_size.Width)/2;
                    y=component_y+component_height/2;
                    break;
                case TEXT_POSITION.OUTSIDE_TOP:
                    x=component_x+component_width/2;
                    y=component_y-space-(int)(used_size.Height)/2;
                    break;
                case TEXT_POSITION.OUTSIDE_TOP_RIGHT:
                    x=component_x+component_width+space+(int)(used_size.Width)/2;
                    y=component_y+space+(int)(used_size.Height)/2;
                    break;
                case TEXT_POSITION.OUTSIDE_TOP_LEFT:
                    x=component_x-space-(int)(used_size.Width)/2;
                    y=component_y+space+(int)(used_size.Height)/2;
                    break;
                case TEXT_POSITION.INSIDE_TOP_LEFT:
                    x=component_x+space+(int)(used_size.Width)/2;
                    y=component_y+space+(int)(used_size.Height)/2;
                    break;
                case TEXT_POSITION.INSIDE_TOP_RIGHT:
                    x=component_x+component_width-space-(int)(used_size.Width)/2;
                    y=component_y+space+(int)(used_size.Height)/2;
                    break;
                case TEXT_POSITION.OUTSIDE_BOTTOM_RIGHT:
                    x=component_x+component_width+space+(int)(used_size.Width)/2;
                    y=component_y+component_height-space-(int)(used_size.Height)/2;
                    break;
                case TEXT_POSITION.OUTSIDE_BOTTOM_LEFT:
                    x=component_x-space-(int)(used_size.Width)/2;
                    y=component_y+component_height-space-(int)(used_size.Height)/2;
                    break;
                case TEXT_POSITION.INSIDE_BOTTOM_LEFT:
                    x=component_x+space+(int)(used_size.Width)/2;
                    y=component_y+component_height-space-(int)(used_size.Height)/2;
                    break;
                case TEXT_POSITION.INSIDE_BOTTOM_RIGHT:
                    x=component_x+component_width-space-(int)(used_size.Width)/2;
                    y=component_y+component_height-space-(int)(used_size.Height)/2;
                    break;
                case TEXT_POSITION.OUTSIDE_BOTTOM:
                default:
                    x=component_x+component_width/2;
                    y=component_y+component_height+space+(int)(used_size.Height)/2;
                    break;
            }
            return new Point(x,y);
        }
        public static void DrawLegend(Graphics graph,string str,float angle_deg,TEXT_POSITION pos,int space,Brush brush,Font font,int component_x,int component_y,int component_width,int component_height)
        {
            Point p=GetPosition(UsedSize(graph,str,angle_deg,font),pos,space,component_x,component_y,component_width,component_height);
            DrawString(graph,str,p.X,p.Y,angle_deg,brush,font);
        }

        public static void DrawString(Graphics graph,string str,int x, int y)
        {
            DrawString(graph,str,x,y,0);
        }

        public static void DrawString(Graphics graph,string str,int x, int y, float angle_deg)
        {
            DrawString(graph,
                        str,
                        x,y,angle_deg,
                        new SolidBrush(Color.Black),
                        new Font("Arial",12)
                        );
        }
        public static void DrawString(Graphics graph,string str,int x, int y, float angle_deg,Brush brush,Font font)
        {
            // store current transform
            System.Drawing.Drawing2D.Matrix saved_matrix=graph.Transform;
            // comput used size
            SizeF drawStringSize=graph.MeasureString(str, font);
            // translate before rotating (easiest way)
            graph.TranslateTransform(
                x-(float)((drawStringSize.Width*Math.Cos(angle_deg*Math.PI/180)+drawStringSize.Height*Math.Sin(angle_deg*Math.PI/180))/2),
                y-(float)((-drawStringSize.Width*Math.Sin(angle_deg*Math.PI/180)+drawStringSize.Height*Math.Cos(angle_deg*Math.PI/180))/2)
                );
            // rotate
            graph.RotateTransform(-angle_deg);
            // draw string
            graph.DrawString(str, font, brush, 0, 0);
            // restore graph coords
            graph.Transform=saved_matrix;
        }
	}
}
