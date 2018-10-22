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

namespace easy_socket
{
    public class RemoteShutDown
    {

        [DllImport("advapi32.dll", EntryPoint="InitiateSystemShutdown",SetLastError=true)]
        private static extern 
            bool InitiateSystemShutdown(
                string lpMachineName,      // computer name // null for local computer
                string lpMessage,          // message to display //This parameter can be NULL if no message is required. 
                UInt32 dwTimeout,          // length of time to display
                bool bForceAppsClosed,     // force closed option
                bool bRebootAfterShutdown  // reboot option
                );

        [DllImport("advapi32.dll", EntryPoint="AbortSystemShutdown",SetLastError=true)]
        private static extern 
        bool AbortSystemShutdown(
            string lpMachineName   // computer name // null for local computer
            );

        /// <summary>
        /// Initiate local or remote system shutdown
        /// </summary>
        /// <param name="MachineName">computer name, "" for local computer</param>
        /// <param name="Message">message to display</param>
        /// <param name="Timeout">length of time to display</param>
        /// <param name="ForceAppsClosed">force closed option</param>
        /// <param name="RebootAfterShutdown">reboot option</param>
        public static void InitiateShutdown(string MachineName,string Message,UInt32 Timeout,bool ForceAppsClosed,bool RebootAfterShutdown)
        {
            API_error.GetLastError();// clear previous error msg if any
            if (!InitiateSystemShutdown(MachineName,Message,Timeout,ForceAppsClosed,RebootAfterShutdown))
                System.Windows.Forms.MessageBox.Show(API_error.GetAPIErrorMessageDescription(API_error.GetLastError()),"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
            else
                System.Windows.Forms.MessageBox.Show("Shutdown successfully initiated","Information",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information);
        }

        /// <summary>
        /// Abort a local or remote system shutdown previously initiated
        /// </summary>
        /// <param name="MachineName">computer name, "" for local computer</param>
        public static void AbortShutdown(string MachineName)
        {
            API_error.GetLastError();// clear previous error msg if any
            if (!AbortSystemShutdown(MachineName))
            {
                uint ui=API_error.GetLastError();
                string msg=API_error.GetAPIErrorMessageDescription(ui);
                if (ui==53)// because of beautiful api error msg :)
                {
                    msg+="\r\nOr shutdown not initiated";
                }
                System.Windows.Forms.MessageBox.Show(msg,"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
                System.Windows.Forms.MessageBox.Show("Shutdown successfully aborted","Information",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information);
        }
    }
}
