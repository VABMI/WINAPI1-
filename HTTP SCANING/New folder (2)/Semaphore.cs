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
using System.Text;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct SecurityAttributes
{
    public uint nLegnth;//Specifies the size, in bytes, of this structure. Set this value to the size of the SECURITY_ATTRIBUTES structure.
    public int lpSecurityDescriptor;//Pointer to a security descriptor for the object that controls the sharing of it. If NULL is specified for this member, the object is assigned the default security descriptor of the calling process.
    public bool bInheritHandle;//Specifies whether the returned handle is inherited when a new process is created. If this member is TRUE, the new process inherits the handle.
}

public class Semaphore
{
    public const uint INFINITE = 0xFFFFFFFF;
    public const int WAIT_ABANDONED = 128;
    public const int WAIT_OBJECT_0 = 0;
    public const int WAIT_TIMEOUT = 258;

    private const uint STANDARD_RIGHTS_REQUIRED=0x000F0000;
    public enum DESIRED_ACCESS:uint
    {
        SYNCHRONIZE=0x00100000,// Windows NT/2000/XP: Enables use of the semaphore handle in any of the wait functions to wait for the semaphore's state to be signaled. 
        SEMAPHORE_MODIFY_STATE=0x0002, // Enables use of the semaphore handle in the ReleaseSemaphore function to modify the semaphore's count. 
        SEMAPHORE_ALL_ACCESS=STANDARD_RIGHTS_REQUIRED|SYNCHRONIZE|0x3// Specifies all possible access flags for the semaphore object. 
    }

    [DllImport("kernel32",EntryPoint="CreateSemaphore",SetLastError=true,CharSet=CharSet.Unicode)]
    public static extern uint CreateSemaphore(
        ref SecurityAttributes lpSemaphoreAttributes, // SD //If lpSemaphoreAttributes is null, the semaphore gets a default security descriptor
        int lInitialCount,                          // initial count
        int lMaximumCount,                          // maximum count
        string lpName                               // object name
        );
    public static uint CreateSemaphore(
        int lInitialCount,                          // initial count
        int lMaximumCount,                          // maximum count
        string lpName                               // object name
        )
    {
        SecurityAttributes sa=new SecurityAttributes();
        return CreateSemaphore(ref sa,lInitialCount,lMaximumCount,lpName);
    }

    [DllImport("kernel32",EntryPoint="OpenSemaphore",SetLastError=true,CharSet=CharSet.Unicode)]
    public static extern uint OpenSemaphore(
                                         uint dwDesiredAccess,  // access
                                         bool bInheritHandle,    // inheritance option
                                         string lpName          // object name
                                         );

    [DllImport("kernel32",EntryPoint="ReleaseSemaphore",SetLastError=true,CharSet=CharSet.Unicode)]
    public static extern int ReleaseSemaphore(
        UInt32 hSemaphore,       // handle to semaphore
        Int32 lReleaseCount,      // count increment amount
        ref UInt32 lpPreviousCount   // previous count
        );

    public static int ReleaseSemaphore(
        uint hSemaphore,       // handle to semaphore
        int lReleaseCount)      // count increment amount
    {
        UInt32 lpPreviousCount=0;
        return ReleaseSemaphore(hSemaphore,lReleaseCount,ref lpPreviousCount);
    }

    [DllImport("kernel32",EntryPoint="CloseHandle",SetLastError=true,CharSet=CharSet.Unicode)]
    public static extern int CloseHandle(uint hHandle);

    [DllImport("kernel32",EntryPoint="WaitForSingleObject",SetLastError=true,CharSet=CharSet.Unicode)]
    public static extern uint WaitForSingleObject(uint hHandle, uint dwMilliseconds);
}