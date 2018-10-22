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

namespace Tools.Text
{
    /// <summary>
    /// done to give similare use as php
    /// </summary>
	public class ClassEreg
	{
        public static bool ereg(string pattern,string input_string,ref string[] strregs)
        {
            if (input_string=="")
                return false;
            if (!System.Text.RegularExpressions.Regex.IsMatch(input_string,pattern,System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                return false;
            System.Text.RegularExpressions.Match m=System.Text.RegularExpressions.Regex.Match(input_string,pattern,System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            strregs=new string[m.Groups.Count];
            for (int cpt=0;cpt<m.Groups.Count;cpt++)
            {
                if (m.Groups[cpt].Captures.Count>0)
                    strregs[cpt]=m.Groups[cpt].Captures[0].Value;
                else
                    strregs[cpt]="";
            }

            return true;
        }
        public static bool ereg(string pattern,string input_string)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input_string,pattern,System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }
	}
}
