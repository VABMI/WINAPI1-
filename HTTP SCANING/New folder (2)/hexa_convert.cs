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
    public class hexa_convert
    {
        public static string hexa_to_string(string str_hexa_data,System.Text.Encoding encoding)
        {
            byte[] b=hexa_convert.hexa_to_byte(str_hexa_data);
            if (b==null)
                return "";
            return encoding.GetString(b,0,b.Length);
        }
        public static string hexa_to_string(string str_hexa_data)
        {
            return hexa_convert.hexa_to_string(str_hexa_data,System.Text.Encoding.ASCII);
        }
        
        public static byte[] hexa_to_byte(string s)
        {
            try
            {
                if (s.Length==0)
                    return null;
                // remove separators if any
                s=s.Replace(" ","");
                s=s.Replace("-","");
                s=s.Replace(".","");
                s=s.Replace(":","");
                // make string size a multiple of 2
                if ((s.Length%2!=0))
                {
                    s="0"+s;
                }
                int size=s.Length/2;
                byte[] b_ret=new byte[size];
                for(int cpt=0;cpt<size;cpt++)
                {
                    b_ret[cpt]=byte.Parse(s.Substring(2*cpt,2),
                        System.Globalization.NumberStyles.HexNumber);
                }
                return b_ret;
            }
            catch
            {
                return null;
            }
        }
        public static string byte_to_hexa(byte[] b,string separator)
        {
            string s=byte_to_hexa(b);
            return s.Replace("-",separator);
        }
        public static string byte_to_hexa(byte[] b)
        {
            if (b==null)
                return "";
            if (b.Length==0)
                return "";
            return System.BitConverter.ToString(b);
        }
        public static string string_to_hexa(string str_data)
        {
            return byte_to_hexa(System.Text.Encoding.Default.GetBytes(str_data));
        }
        public static string string_to_hexa(string str_data,string separator)
        {
            return byte_to_hexa(System.Text.Encoding.Default.GetBytes(str_data),separator);
        }
    }
}
