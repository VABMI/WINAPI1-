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
    /// 
    /// </summary>
    public class bin_convert
    {
        public static byte strbit_to_byte(string bit)
        {
            byte ret=0;
            string bit_value="0";
            byte b=0;
            for (int cpt=1;cpt<=bit.Length;cpt++)
            {
                bit_value=bit.Substring(bit.Length-cpt,1);
                if ((bit_value!="0")||(bit_value!="1"))
                    break;
                b=System.Convert.ToByte(bit_value);
                ret|=(byte)((b<<(cpt-1))&0xFF);
            }
            return ret;
        }
    }
}
