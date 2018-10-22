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
using System.Runtime.InteropServices;

namespace Tools.API
{
    /// <summary>
    /// Summary description for Tools.API.API_error.
    /// </summary>
    public class API_error
    {
        private const int FORMAT_MESSAGE_FROM_SYSTEM  = 0x00001000;
        [ DllImport( "kernel32" ,SetLastError=true)]
        private static extern UInt32 FormatMessage( UInt32 flags, IntPtr source, UInt32 messageId,
            UInt32 languageId, System.Text.StringBuilder buffer, UInt32 size, IntPtr arguments ); 


        public static string GetAPIErrorMessageDescription(UInt32 ApiErrNumber ) 
        {
            System.Text.StringBuilder sError = new System.Text.StringBuilder(512); 
            UInt32 lErrorMessageLength; 
            lErrorMessageLength = FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM,(IntPtr)0, ApiErrNumber, 0, sError, (UInt32)sError.Capacity, (IntPtr)0) ;
            
            if(lErrorMessageLength > 0)
            { 
                string strgError = sError.ToString();
                strgError=strgError.Substring(0,strgError.Length-2);
                return strgError+" ("+ApiErrNumber.ToString()+")";
            }
            return "none";

        }
        [DllImport("kernel32",EntryPoint="GetLastError",SetLastError=true)]
        public static extern uint GetLastError();
    }
}