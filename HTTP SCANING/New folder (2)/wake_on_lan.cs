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
namespace easy_socket
{
    /// <summary>
    /// Summary description for wake_on_lan.
    /// </summary>
    public class Cwake_on_lan
    {
        private System.Threading.AutoResetEvent evt_error=new System.Threading.AutoResetEvent(false);
        private System.Threading.AutoResetEvent evt_success=new System.Threading.AutoResetEvent(false);

        private void Socket_Data_Error_EventHandler(easy_socket.udp.Socket_Data sender, easy_socket.udp.EventArgs_Exception e)
        {
            System.Windows.Forms.MessageBox.Show("Error sending WOL packet:\r\n"+e.exception.Message,"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
            this.evt_error.Set();
        }
        private void Socket_Data_Send_Completed_EventHandler(easy_socket.udp.Socket_Data sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("WOL packet successfully sent","Information",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information);
            this.evt_success.Set();
        }

        public void wake_on_lan(string mac_addr,string ip,int port)
        {
            mac_addr=mac_addr.Replace("-","");
            mac_addr=mac_addr.Replace(".","");
            mac_addr=mac_addr.Replace(":","");

            easy_socket.udp.Socket_Data s=new easy_socket.udp.Socket_Data(ip,port);
            s.event_Socket_Data_Error+=new easy_socket.udp.Socket_Data_Error_EventHandler(Socket_Data_Error_EventHandler);
            s.event_Socket_Data_Send_Completed+=new easy_socket.udp.Socket_Data_Send_Completed_EventHandler(Socket_Data_Send_Completed_EventHandler);
            s.allow_broadcast=true;
/*
 * AMD Publication# 20213
If the IEEE address for a particular node on the network
was 11h 22h 33h 44h 55h 66h, then the LAN controller
would be scanning for the data sequence (assuming an
Ethernet Frame):
DESTINATION SOURCE MISC FF FF FF FF FF
FF 11 22 33 44 55 66 11 22 33 44 55 66 11 22 33 44
55 66 11 22 33 44 55 66 11 22 33 44 55 66 11 22 33
44 55 66 11 22 33 44 55 66 11 22 33 44 55 66 11 22
33 44 55 66 11 22 33 44 55 66 11 22 33 44 55 66 11
22 33 44 55 66 11 22 33 44 55 66 11 22 33 44 55 66
11 22 33 44 55 66 11 22 33 44 55 66 MISC CRC 
*/
            byte[] bytes=new byte[6+6*16];
            //first 6 bytes should be 0xff
            for(int cpt=0;cpt<6;cpt++)
                bytes[cpt]=0xff;
            //now repeate MAC 16 times
            for(int cpt=0;cpt<16;cpt++)
            {
                for(int cpt2=0;cpt2<6;cpt2++)
                {
                    bytes[6+cpt*6+cpt2]=byte.Parse(mac_addr.Substring(2*cpt2,2),
                                        System.Globalization.NumberStyles.HexNumber);
                }
            }
            s.send(bytes);
            // wait for multiple handles (error and send completed)
            System.Threading.WaitHandle[] waithandles=new System.Threading.WaitHandle[2];
            waithandles[0]=evt_success;
            waithandles[1]=evt_error;
            System.Threading.WaitHandle.WaitAny(waithandles,10000,true);
        }
    }
}
