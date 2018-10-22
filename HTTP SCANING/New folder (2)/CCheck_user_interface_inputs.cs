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

public class CCheck_user_interface_inputs
{

    public static bool check_int(string text)
    {
        try
        {
            System.Convert.ToInt32(text);
            return true;
        }
        catch (Exception e)
        {
            System.Windows.Forms.MessageBox.Show(e.Message,
                "Error",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Error);
            return false;
        }
    }
    public static bool check_byte(string text)
    {
        try
        {
            System.Convert.ToByte(text);
            return true;
        }
        catch (Exception e)
        {
            System.Windows.Forms.MessageBox.Show(e.Message,
                "Error",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Error);
            return false;
        }
    }
}
