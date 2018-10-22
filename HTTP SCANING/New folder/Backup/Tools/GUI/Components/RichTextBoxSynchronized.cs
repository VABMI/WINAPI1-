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
using System.Runtime.InteropServices;

namespace Tools.GUI.Components
{
    /// <summary>
    /// Summary description for Tools.GUI.Components.RichTextBoxSynchronized.
    /// </summary>
    /// 
    public class RichTextBoxSynchronized:System.Windows.Forms.RichTextBox
    {
        public const int WM_HSCROLL = 0x114;
        public const int WM_VSCROLL = 0x115;
        public const int WM_MOUSEWHEEL = 0x20A;
        public const int SB_LINEUP = 0;
        public const int SB_LINEDOWN = 1;

        // wait for message processing
        [ DllImport( "User32" ,SetLastError=true)]
        private static extern 
            Int32 SendMessage(IntPtr hWnd,
            UInt32 Msg,
            IntPtr wParam,
            IntPtr lParam
            );

        System.Collections.ArrayList al_synchronized_control;
        public bool b_is_being_synchronized;
        public RichTextBoxSynchronized()
        {
            this.al_synchronized_control=new System.Collections.ArrayList();
            this.b_is_being_synchronized=false;
        }
        public void AddSynchronizedControl(Tools.GUI.Components.RichTextBoxSynchronized ctl)
        {
            this.al_synchronized_control.Add(ctl);
        }

        protected override void WndProc(ref Message uMsg)
        {
            switch(uMsg.Msg)
            {
                case WM_VSCROLL:
                case WM_HSCROLL:
                    if (!this.b_is_being_synchronized)
                    {
                        foreach (Tools.GUI.Components.RichTextBoxSynchronized ctl in this.al_synchronized_control)
                        {
                            ctl.b_is_being_synchronized=true;
                            SendMessage(ctl.Handle, (UInt32)uMsg.Msg,uMsg.WParam,uMsg.LParam);
                            ctl.b_is_being_synchronized=false;
                        }
                    }
                    break;
                case WM_MOUSEWHEEL: 
                    // error (no scroll) if ScrollBar not visible 
                    // --> send message WM_VSCROLL with SB_LINEUP or SB_LINEDOWN, SystemInformation.MouseWheelScrollLines times
                    if ((this.ScrollBars!=System.Windows.Forms.RichTextBoxScrollBars.Both)
                        &&(this.ScrollBars!=System.Windows.Forms.RichTextBoxScrollBars.Vertical)
                        &&(this.ScrollBars!=System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth)
                        &&(this.ScrollBars!=System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical))
                    {
                        Message m;
                        int i32=(int)((uMsg.WParam.ToInt32())&0xffff0000);
                        i32>>=16;
                        short s=(short)i32;
                        if (s>0)
                            m = Message.Create(System.IntPtr.Zero, WM_VSCROLL,new System.IntPtr(SB_LINEUP), System.IntPtr.Zero);
                        else
                            m = Message.Create(System.IntPtr.Zero, WM_VSCROLL,new System.IntPtr(SB_LINEDOWN), System.IntPtr.Zero);

                        // store current synchro state
                        bool b=this.b_is_being_synchronized;
                        // avoid the dispatching of event to over components
                        this.b_is_being_synchronized=true;
                        // send event to current control
                        for (int cnt=0;cnt<SystemInformation.MouseWheelScrollLines;cnt++)
                            SendMessage(this.Handle,(UInt32)m.Msg,m.WParam,m.LParam);
                        // restore synchro state
                        this.b_is_being_synchronized=b;
                    }

                    if (!this.b_is_being_synchronized)
                    {
                        foreach (Tools.GUI.Components.RichTextBoxSynchronized ctl in this.al_synchronized_control)
                        {
                            ctl.b_is_being_synchronized=true;
                            SendMessage(ctl.Handle, (UInt32)uMsg.Msg,uMsg.WParam,uMsg.LParam);
                            ctl.b_is_being_synchronized=false;
                        }
                    }
                    
                    break;
            }
            base.WndProc(ref uMsg);
        }
    }
}
