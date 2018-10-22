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

    public class Cchecksum
    {
        public static UInt16 checksum(byte[] buffer)
        {
            return Cchecksum.checksum(buffer,false);
        }
        /// <summary>
        ///    comput checksum 
        /// </summary>
        public static UInt16 checksum(byte[] buffer,bool returned_value_in_network_order)
        {
            int iCheckSum = 0;
            if (buffer!=null)
            {
                if (buffer.Length%2==0)
                {
                    for (int i= 0; i < buffer.Length; i+= 2) 
                        iCheckSum += BitConverter.ToUInt16(buffer,i);
                }
                else
                {
                    for (int i= 0; i < buffer.Length-1; i+= 2) 
                        iCheckSum += BitConverter.ToUInt16(buffer,i);
                    iCheckSum += buffer[buffer.Length-1];
                }
            }
            iCheckSum = (iCheckSum >> 16) + (iCheckSum & 0xffff);
            iCheckSum += (iCheckSum >> 16);
            iCheckSum=~iCheckSum;
            if (!returned_value_in_network_order)
            {
                byte MSB=(byte)((iCheckSum>>8)&0xff);
                byte LSB=(byte)(iCheckSum&0xff);
                iCheckSum=(LSB<<8)+MSB;
            }
            return (UInt16)iCheckSum;
        }
    }
}
