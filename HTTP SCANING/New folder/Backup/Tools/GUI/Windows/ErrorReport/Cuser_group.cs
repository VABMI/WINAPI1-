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
using System.Security.Principal;

namespace Tools.GUI.Windows.ErrorReport
{
    /// <summary>
    /// Summary description for Cuser_member.
    /// </summary>
    public class Cuser_group
    {
        public static string[] get_groups()
        {
            System.Collections.ArrayList al=new System.Collections.ArrayList(10);
            WindowsPrincipal id=new WindowsPrincipal(WindowsIdentity.GetCurrent());
            Array wbirFields = Enum.GetValues(typeof(WindowsBuiltInRole));
            foreach (object roleName in wbirFields)
            {
                try
                {
                    if (id.IsInRole((WindowsBuiltInRole)roleName))
                        al.Add(roleName.ToString());
                } 
                catch (Exception)
                {
                    // System.Windows.Forms.MessageBox.Show("Could not obtain role for "+roleName+" RID");
                }
            }
            if (al.Count==0)
                return null;
            return (string[])al.ToArray(typeof(string));
        }
    }
}