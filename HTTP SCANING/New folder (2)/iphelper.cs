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
namespace iphelper
{
    class iphelper_Process
    {
    #region GetProcessById
        public static System.Diagnostics.Process GetProcessById(UInt32 processID)
        {
            try
            {
                return System.Diagnostics.Process.GetProcessById((int)processID);
            }
            catch
            {
                return null;
            }
        }
    #endregion
    };
    class iphelper
    {
        public const UInt32 StatisticsEx_dwFamily_AF_INET=2; // UInt32ernetwork: UDP, TCP, etc.
        public const UInt32 StatisticsEx_dwFamily_AF_INET6=23; // UInt32ernetwork Version 6

    #region GetProcessHeap
        [ DllImport( "kernel32" ,SetLastError=true)]
        private static extern IntPtr GetProcessHeap();
    #endregion

    #region GetIpNetTable
        [DllImport("iphlpapi.dll",SetLastError=true)]
        private static extern 
            UInt32 GetIpNetTable(
            byte[] pIpNetTable,//CMIB_IPNETTABLE pIpNetTable,
            ref UInt32 pdwSize,
            bool bOrder
            );

        public static CMIB_IPNETTABLE GetIpNetTable()
        {
            UInt32 error_code=0;
            return iphelper.GetIpNetTable(true,ref error_code);
        }
        public static CMIB_IPNETTABLE GetIpNetTable(bool b_verbose,ref UInt32 error_code)
        {
            UInt32 size=CMIB_IPNETROW.size*16+4;// allow memory for 16 ipnetrow (+4 for dwNumEntries)
            byte[] buffer=new byte[size];
            error_code=GetIpNetTable(buffer,ref size,true);
            if (error_code==122)//not enougth memory
            {
                buffer=new byte[size];
                error_code=GetIpNetTable(buffer,ref size,true);
            }
            if (error_code==0)// no error
            {
                CMIB_IPNETTABLE ipnettable=new CMIB_IPNETTABLE();
                ipnettable.decode(buffer);
                return ipnettable;
            }
            // else
            if (b_verbose)
            {
                string str_msg=API_error.GetAPIErrorMessageDescription(error_code);
                System.Windows.Forms.MessageBox.Show( str_msg,"Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
            return null;
        }
    #endregion

    #region GetIpStatisticsEx
        [DllImport("iphlpapi.dll",SetLastError=true)]
        private static extern 
            UInt32 GetIpStatisticsEx(
            ref MIB_IPSTATS pmips,
            UInt32 dwFamily
            );

        public static MIB_IPSTATS GetIpStatisticsEx()
        {
            UInt32 error_code=0;
            return iphelper.GetIpStatisticsEx(iphelper.StatisticsEx_dwFamily_AF_INET,true,ref error_code);
        }
        public static MIB_IPSTATS GetIpStatisticsEx(UInt32 family_type,bool b_verbose,ref UInt32 error_code)
        {
            MIB_IPSTATS mips=new MIB_IPSTATS();
            error_code=GetIpStatisticsEx(ref mips,family_type);
            if ((error_code!=0) && b_verbose)// error
            {
                string str_msg=API_error.GetAPIErrorMessageDescription(error_code);
                System.Windows.Forms.MessageBox.Show( str_msg,"Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);            
            }
            return mips;
        }
    #endregion

    #region GetIcmpStatistics
        [DllImport("iphlpapi.dll",SetLastError=true)]
        private static extern 
            UInt32 GetIcmpStatistics(
            ref MIB_ICMP pmi
            );
        public static MIB_ICMP GetIcmpStatistics()
        {
            UInt32 error_code=0;
            return iphelper.GetIcmpStatistics(true,ref error_code);
        }
        public static MIB_ICMP GetIcmpStatistics(bool b_verbose,ref UInt32 error_code)
        {
            MIB_ICMP mi=new MIB_ICMP();
            error_code=GetIcmpStatistics(ref mi);
            if ((error_code!=0) && b_verbose)// error
            {
                string str_msg=API_error.GetAPIErrorMessageDescription(error_code);
                System.Windows.Forms.MessageBox.Show( str_msg,"Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
            return mi;
        }
    #endregion

    #region GetUdpStatisticsEx
        [DllImport("iphlpapi.dll",SetLastError=true)]
        private static extern 
            UInt32 GetUdpStatisticsEx(
            ref MIB_UDPSTATS pStats,
            UInt32 dwFamily
            );
        public static MIB_UDPSTATS GetUdpStatisticsEx()
        {
            UInt32 error_code=0;
            return iphelper.GetUdpStatisticsEx(iphelper.StatisticsEx_dwFamily_AF_INET,true,ref error_code);
        }
        public static MIB_UDPSTATS GetUdpStatisticsEx(UInt32 dwFamily,bool b_verbose,ref UInt32 error_code)
        {
            MIB_UDPSTATS m=new MIB_UDPSTATS();
            error_code=GetUdpStatisticsEx(ref m,dwFamily);
            if ((error_code!=0) && b_verbose)// error
            {
                string str_msg=API_error.GetAPIErrorMessageDescription(error_code);
                System.Windows.Forms.MessageBox.Show( str_msg,"Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
            return m;
        }
    #endregion

    #region GetUdpTable
        [DllImport("iphlpapi.dll",SetLastError=true)]
        private static extern
            UInt32 GetUdpTable(
            byte[] pUdpTable,//PCMIB_UDPTABLE pUdpTable,
            ref UInt32 pdwSize,
            bool bOrder
            );
        public static CMIB_UDPTABLE GetUdpTable()
        {
            UInt32 error_code=0;
            return iphelper.GetUdpTable(true,ref error_code);
        }
        public static CMIB_UDPTABLE GetUdpTable(bool b_verbose,ref UInt32 error_code)
        {
            UInt32 size=CMIB_UDPROW.size*100+4;// allow memory for 100 udprow (+4 for dwNumEntries)
            byte[] buffer=new byte[size];
            error_code=GetUdpTable(buffer,ref size,true);
            if (error_code==122)//not enougth memory
            {
                buffer=new byte[size];
                error_code=GetUdpTable(buffer,ref size,true);
            }
            if (error_code==0)// no error
            {
                CMIB_UDPTABLE udptable=new CMIB_UDPTABLE();
                udptable.decode(buffer);
                return udptable;
            }
            // else
            if (b_verbose)
            {
                string str_msg=API_error.GetAPIErrorMessageDescription(error_code);
                System.Windows.Forms.MessageBox.Show( str_msg,"Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
            return null;
        }
    #endregion

    #region GetUdpExTable
        [DllImport("iphlpapi.dll",SetLastError=true)]
        private extern static 
            UInt32 AllocateAndGetUdpExTableFromStack(
            ref IntPtr pUdpTable,
            bool bOrder,
            IntPtr heap,
            UInt32 zero,
            UInt32 flags
            ); 
        public static CMIB_UDPEXTABLE GetUdpExTable()
        {
            UInt32 error_code=0;
            return iphelper.GetUdpExTable(true,ref error_code);
        }
        public static CMIB_UDPEXTABLE GetUdpExTable(bool b_verbose,ref UInt32 error_code)
        {
            // allocate a dump memory space in order to retrieve nb of connexion
            int BufferSize = 100*CMIB_UDPEXROW.size_ex+4;//NumEntries*CMIB_UDPEXROW.size_ex+4
            IntPtr lpTable = Marshal.AllocHGlobal(BufferSize);
            //getting infos
            error_code= AllocateAndGetUdpExTableFromStack(ref lpTable, true, GetProcessHeap(),0,2);
            if (error_code!=0)
            {
                if (b_verbose)
                {
                    string str_msg=API_error.GetAPIErrorMessageDescription(error_code);
                    System.Windows.Forms.MessageBox.Show( str_msg,"Error",
                        System.Windows.Forms.MessageBoxButtons.OK,
                        System.Windows.Forms.MessageBoxIcon.Error);
                }
                return null;
            }
            //get the number of entries in the table
            int NumEntries= (int)Marshal.ReadIntPtr(lpTable);
            int real_buffer_size=NumEntries*CMIB_UDPEXROW.size_ex+4;
            // check if memory was enougth (needed buffer size: NumEntries*MIB_UDPEXROW.size_ex +4 (for dwNumEntries))
            if (BufferSize<real_buffer_size)
            {// if it wasn't the case call function another time

                // free the buffer
                Marshal.FreeHGlobal(lpTable);
                // get the needed buffer size: NumEntries*MIB_UDPEXROW.size_ex +4 (for dwNumEntries)
                BufferSize = real_buffer_size;
                // Allocate memory
                lpTable = Marshal.AllocHGlobal(BufferSize);
                error_code=AllocateAndGetUdpExTableFromStack(ref lpTable,true,GetProcessHeap(),0,2);
                if (error_code!=0)
                {
                    if (b_verbose)
                    {
                        string str_msg=API_error.GetAPIErrorMessageDescription(error_code);
                        System.Windows.Forms.MessageBox.Show( str_msg,"Error",
                            System.Windows.Forms.MessageBoxButtons.OK,
                            System.Windows.Forms.MessageBoxIcon.Error);
                    }
                    return null;
                }
            }
            BufferSize=real_buffer_size;
            byte[] array=new byte[BufferSize];
            Marshal.Copy(lpTable,array,0,BufferSize);
            // free the buffer
            Marshal.FreeHGlobal(lpTable);
        
            CMIB_UDPEXTABLE muet = new CMIB_UDPEXTABLE();
            muet.decode(array);
            return muet;
        }

    #endregion

    #region GetTcpStatisticsEx
        [DllImport("iphlpapi.dll",SetLastError=true)]
        private static extern 
            UInt32 GetTcpStatisticsEx(
            ref MIB_TCPSTATS pStats,
            UInt32 dwFamily
            );
        public static MIB_TCPSTATS GetTcpStatisticsEx()
        {
            UInt32 error_code=0;
            return iphelper.GetTcpStatisticsEx(iphelper.StatisticsEx_dwFamily_AF_INET,true,ref error_code);
        }
        public static MIB_TCPSTATS GetTcpStatisticsEx(UInt32 dwFamily,bool b_verbose,ref UInt32 error_code)
        {
            MIB_TCPSTATS m=new MIB_TCPSTATS();
            error_code=GetTcpStatisticsEx(ref m,dwFamily);
            if (error_code!=0)
            {
                if (b_verbose)
                {
                    string str_msg=API_error.GetAPIErrorMessageDescription(error_code);
                    System.Windows.Forms.MessageBox.Show( str_msg,"Error",
                        System.Windows.Forms.MessageBoxButtons.OK,
                        System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            return m;
        }
    #endregion

    #region GetTcpTable
        [DllImport("iphlpapi.dll",SetLastError=true)]
        private static extern
            UInt32 GetTcpTable(
            byte[] pTcpTable,//MIB_TCPTABLE pTcpTable,
            ref UInt32 pdwSize,
            bool bOrder
            );
        public static CMIB_TCPTABLE GetTcpTable()
        {
            UInt32 error_code=0;
            return iphelper.GetTcpTable(true,ref error_code);
        }
        public static CMIB_TCPTABLE GetTcpTable(bool b_verbose,ref UInt32 error_code)
        {
            UInt32 size=CMIB_TCPROW.size*300+4;// allow memory for 300 tcprow (+4 for dwNumEntries)
            byte[] buffer=new byte[size];
            error_code=GetTcpTable(buffer,ref size,true);
            if (error_code==122)//not enougth memory
            {
                buffer=new byte[size];
                error_code=GetTcpTable(buffer,ref size,true);
            }
            if (error_code==0)// no error
            {
                CMIB_TCPTABLE tcptable=new CMIB_TCPTABLE();
                tcptable.decode(buffer);
                return tcptable;
            }
            // else
            if (b_verbose)
            {
                string str_msg=API_error.GetAPIErrorMessageDescription(error_code);
                System.Windows.Forms.MessageBox.Show( str_msg,"Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
            return null;
        }
    #endregion

    #region GetTcpExTable
        [DllImport("iphlpapi.dll",SetLastError=true)]
        private extern static 
            UInt32 AllocateAndGetTcpExTableFromStack(
            ref IntPtr pTcpTable,
            bool bOrder,
            IntPtr heap,
            UInt32 zero,
            UInt32 flags
            ); 
        public static CMIB_TCPEXTABLE GetTcpExTable()
        {
            UInt32 error_code=0;
            return iphelper.GetTcpExTable(true,ref error_code);
        }
        public static CMIB_TCPEXTABLE GetTcpExTable(bool b_verbose,ref UInt32 error_code)
        {
            // allocate a dump memory space in order to retrieve nb of connexion
            int BufferSize = 300*CMIB_TCPEXROW.size_ex+4;//NumEntries*CMIB_TCPEXROW.size_ex+4
            IntPtr lpTable = Marshal.AllocHGlobal(BufferSize);
            //getting infos
            error_code= AllocateAndGetTcpExTableFromStack(ref lpTable, true, GetProcessHeap(),0,2);
            if (error_code!=0)
            {
                if (b_verbose)
                {
                    string str_msg=API_error.GetAPIErrorMessageDescription(error_code);
                    System.Windows.Forms.MessageBox.Show( str_msg,"Error",
                        System.Windows.Forms.MessageBoxButtons.OK,
                        System.Windows.Forms.MessageBoxIcon.Error);
                }
                return null;
            }
            //get the number of entries in the table
            int NumEntries= (int)Marshal.ReadIntPtr(lpTable);
            int real_buffer_size=NumEntries*CMIB_TCPEXROW.size_ex+4;
            // check if memory was enougth (needed buffer size: NumEntries*MIB_UDPEXROW.size_ex +4 (for dwNumEntries))
            if (BufferSize<real_buffer_size)
            {
                // free the buffer
                Marshal.FreeHGlobal(lpTable);
                
                // get the needed buffer size: NumEntries*MIB_TCPEXROW.size_ex +4 (for dwNumEntries)
                BufferSize = real_buffer_size;
                // Allocate memory
                lpTable = Marshal.AllocHGlobal(BufferSize);

                error_code=AllocateAndGetTcpExTableFromStack(ref lpTable,true,GetProcessHeap(),0,2);
                if (error_code!=0)
                {
                    if (b_verbose)
                    {
                        string str_msg=API_error.GetAPIErrorMessageDescription(error_code);
                        System.Windows.Forms.MessageBox.Show( str_msg,"Error",
                            System.Windows.Forms.MessageBoxButtons.OK,
                            System.Windows.Forms.MessageBoxIcon.Error);
                    }
                    return null;
                }
            }
            BufferSize=real_buffer_size;
            byte[] array=new byte[BufferSize];
            Marshal.Copy(lpTable,array,0,BufferSize);
            // free the buffer
            Marshal.FreeHGlobal(lpTable);
            CMIB_TCPEXTABLE mtet = new CMIB_TCPEXTABLE();
            mtet.decode(array);
            return mtet;
        }
    #endregion

        //MSDN:"Currently, the only state to which a TCP connection can be set is MIB_TCP_STATE_DELETE_TCB"
        //return 65 - User has no sufficient privilege to execute this API successfully 
        //return 87 - Specified port is not in state to be closed down.
        [DllImport("iphlpapi.dll",SetLastError=true)]
        public extern static 
            UInt32 SetTcpEntry(
            ref MIB_TCPROW pTcpRow
            );

    #region sendarp
        [DllImport("Iphlpapi.dll",SetLastError=true)]
        private static extern UInt32 SendARP(
            UInt32 DestIP,     // destination IP address
            UInt32 SrcIP,      // IP address of sender
            ref UInt32 pMacAddr,   // returned physical address
            ref UInt32 PhyAddrLen  // length of returned physical addr.
            );

        public static string SendArp(string ip)
        {
            UInt32 error_code=0;
            return iphelper.SendArp(ip,true,ref error_code);
        }
        public static string SendArp(string ip,bool b_verbose,ref UInt32 error_code)
        {
            // Windows NT/2000/XP: Included in Windows 2000; Windows XP Pro; and Windows .NET Server.
            // Windows 95/98/Me: Unsupported.
            try
            {

                UInt32 DestIP;     // destination IP address
                UInt32 SrcIP;      // IP address of sender
                UInt32[] pMacAddr=new UInt32[2];   // returned physical address
                UInt32 PhyAddrLen; // length of returned physical addr.

            
                DestIP=(UInt32)(System.Net.IPAddress.Parse(ip).Address);
                SrcIP=0;
                pMacAddr[0]=255;
                pMacAddr[1]=255;
                PhyAddrLen=6;
                error_code=SendARP(DestIP,SrcIP,ref pMacAddr[0],ref PhyAddrLen);
                if (error_code!=0)
                {
                    if (b_verbose)
                    {
                        string str_msg="Error retrieving MAC address of "+ip;//+"\r\n"+API_error.GetAPIErrorMessageDescription(error_code);
                        System.Windows.Forms.MessageBox.Show( str_msg,"Error",
                            System.Windows.Forms.MessageBoxButtons.OK,
                            System.Windows.Forms.MessageBoxIcon.Error);
                    }
                    return "";
                }

                byte[] bMacAddr=new byte[6];
                bMacAddr[3]=(byte)((pMacAddr[0]>>24)&0xFF);
                bMacAddr[2]=(byte)((pMacAddr[0]>>16)&0xFF);
                bMacAddr[1]=(byte)((pMacAddr[0]>>8)&0xFF);
                bMacAddr[0]=(byte)((pMacAddr[0])&0xFF);
                bMacAddr[5]=(byte)((pMacAddr[1]>>8)&0xFF);
                bMacAddr[4]=(byte)((pMacAddr[1])&0xFF);
                
                string str_mac_addr=System.BitConverter.ToString(bMacAddr);
                return str_mac_addr;
            }
            catch(Exception ex)
            {
                if (b_verbose)
                {
                    System.Windows.Forms.MessageBox.Show( ex.Message,"Error",
                        System.Windows.Forms.MessageBoxButtons.OK,
                        System.Windows.Forms.MessageBoxIcon.Error);
                }
                return "";
            }
        }
    #endregion

        [DllImport("iphlpapi.dll",SetLastError=true)]
        public extern static
        UInt32 CreateIpNetEntry(
            ref MIB_IPNETROW pArpEntry  // pointer to info for new entry
            );
        [DllImport("iphlpapi.dll",SetLastError=true)]
        public extern static
        UInt32 DeleteIpNetEntry(
            ref MIB_IPNETROW pArpEntry  // info identifying entry to delete
            );
        [DllImport("iphlpapi.dll",SetLastError=true)]
        public extern static
            UInt32 SetIpNetEntry(
            ref MIB_IPNETROW pArpEntry  // pointer to new information
            );
    };
#region GetIpNetTable class/structs
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_IPNETROW
    {
        //Specifies the index of the adapter. 
        public UInt32 dwIndex;
        //Specifies the length of the physical address. 
        public UInt32 dwPhysAddrLen;
        //Specifies the physical address.
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]//MAXLEN_PHYSADDR
        public byte[] bPhysAddr;
        //Specifies the IP address in networkhost order
        public UInt32 dwAddr;
        //Specifies the type of ARP entry. This type can be one of the following values. Value Meaning 4 Static; 3 Dynamic; 2 Invalid; 1 Other
        public UInt32 dwType;
    }
    public class CMIB_IPNETROW 
    {
        public const byte dwType_Static=4;
        public const byte dwType_Dynamic=3; 
        public const byte dwType_Invalid=2; 
        public const byte dwType_Other=1;

        public const byte MAXLEN_PHYSADDR=8;
        public const byte size=MAXLEN_PHYSADDR+16;
        //Specifies the index of the adapter. 
        public UInt32 dwIndex=0;
        //Specifies the length of the physical address. 
        public UInt32 dwPhysAddrLen=0;
        //Specifies the physical address.
        public byte[] bPhysAddr=new byte[MAXLEN_PHYSADDR];
        //Specifies the IP address in networkhost order
        public UInt32 dwAddr=0;
        //Specifies the type of ARP entry. This type can be one of the following values. Value Meaning 4 Static; 3 Dynamic; 2 Invalid; 1 Other
        public UInt32 dwType=0;

        public bool decode(byte[] array)
        {
            if (array==null)
                return false;
            if (array.Length<24)
                return false;
            this.dwIndex=System.BitConverter.ToUInt32(array,0);
            this.dwPhysAddrLen=System.BitConverter.ToUInt32(array,4);
            System.Array.Copy(array,8,this.bPhysAddr,0,MAXLEN_PHYSADDR);
            this.dwAddr=System.BitConverter.ToUInt32(array,8+MAXLEN_PHYSADDR);
            this.dwType=System.BitConverter.ToUInt32(array,8+MAXLEN_PHYSADDR+4);
            return true;
        }
        /// <summary>
        /// get ip in standard notation
        /// </summary>
        /// <returns></returns>
        public string get_ip()
        {
            System.Net.IPAddress ipa=new System.Net.IPAddress(this.dwAddr);
            string ret=ipa.ToString();
            return ret;
        }
        /// <summary>
        /// get mac address of the host
        /// </summary>
        /// <returns></returns>
        public string get_mac()
        {
            return System.BitConverter.ToString(this.bPhysAddr,0,(int)this.dwPhysAddrLen);
        }
        public string get_type()
        {
            string ret="";
            switch (this.dwType)
            {
                case CMIB_IPNETROW.dwType_Dynamic:
                    ret="Dynamic";
                    break;
                case CMIB_IPNETROW.dwType_Invalid:
                    ret="Invalid";
                    break;
                case CMIB_IPNETROW.dwType_Other:
                    ret="Other";
                    break;
                case CMIB_IPNETROW.dwType_Static:
                    ret="Static";
                    break;
            }
            return ret;
        }
        public MIB_IPNETROW get_MIB_IPNETROW()
        {
            MIB_IPNETROW ret=new MIB_IPNETROW();
            ret.bPhysAddr=new byte[MAXLEN_PHYSADDR];
            System.Array.Copy(this.bPhysAddr,ret.bPhysAddr,MAXLEN_PHYSADDR);
            ret.dwAddr=this.dwAddr;
            ret.dwIndex=this.dwIndex;
            ret.dwPhysAddrLen=this.dwPhysAddrLen;
            ret.dwType=this.dwType;
            return ret;
        }
    };
        
    public class CMIB_IPNETTABLE 
    {
        public UInt32 dwNumEntries=0;
        public CMIB_IPNETROW[] table;//ANY_SIZE
        public bool decode(byte[] array)
        {
            byte[] pb_tmp=new byte[CMIB_IPNETROW.size];
            if (array==null)
                return false;
            if (array.Length<4)
                return false;
            this.dwNumEntries=System.BitConverter.ToUInt32(array,0);
            if (array.Length<this.dwNumEntries)
                return false;
            this.table=new CMIB_IPNETROW[this.dwNumEntries];
            for (UInt32 cpt=0;cpt<this.dwNumEntries;cpt++)
            {
                System.Array.Copy(array,(int)(cpt*CMIB_IPNETROW.size+4),pb_tmp,0,CMIB_IPNETROW.size);
                this.table[cpt]=new CMIB_IPNETROW();
                if (!this.table[cpt].decode(pb_tmp))
                    return false;
            }
            return true;
        }
    };
#endregion

#region GetIpStatisticsEx struct
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_IPSTATS
    {
        //Specifies whether IP forwarding is enabled or disabled. 
        public UInt32 dwForwarding;
        //Specifies the default initial time to live (TTL) for datagrams originating on a particular computer. 
        public UInt32 dwDefaultTTL;
        //Specifies the number of datagrams received. 
        public UInt32 dwInReceives;
        //Specifies the number of datagrams received that have header errors. 
        public UInt32 dwInHdrErrors;
        //Specifies the number of datagrams received that have address errors. 
        public UInt32 dwInAddrErrors;
        //Specifies the number of datagrams forwarded. 
        public UInt32 dwForwDatagrams;
        //Specifies the number of datagrams received that have an unknown protocol. 
        public UInt32 dwInUnknownProtos;
        //Specifies the number of received datagrams discarded. 
        public UInt32 dwInDiscards;
        //Specifies the number of received datagrams delivered. 
        public UInt32 dwInDelivers;
        //Specifies the number of outgoing datagrams that IP is requested to transmit. This number does not include forwarded datagrams. 
        public UInt32 dwOutRequests;
        //Specifies the number of outgoing datagrams discarded. 
        public UInt32 dwRoutingDiscards;
        //Specifies the number of transmitted datagrams discarded. 
        public UInt32 dwOutDiscards;
        //Specifies the number of datagrams for which this computer did not have a route to the destination IP address. These datagrams were discarded. 
        public UInt32 dwOutNoRoutes;
        //Specifies the amount of time allowed for all pieces of a fragmented datagram to arrive. If all pieces do not arrive within this time, the datagram is discarded. 
        public UInt32 dwReasmTimeout;
        //Specifies the number of datagrams that require re-assembly. 
        public UInt32 dwReasmReqds;
        //Specifies the number of datagrams that were successfully reassembled. 
        public UInt32 dwReasmOks;
        //Specifies the number of datagrams that cannot be reassembled. 
        public UInt32 dwReasmFails;
        //Specifies the number of datagrams that were fragmented successfully. 
        public UInt32 dwFragOks;
        //Specifies the number of datagrams that have not been fragmented because the IP header specifies no fragmentation. These datagrams are discarded. 
        public UInt32 dwFragFails;
        //Specifies the number of fragments created. 
        public UInt32 dwFragCreates;
        //Specifies the number of interfaces. 
        public UInt32 dwNumIf;
        //Specifies the number of IP addresses associated with this computer. 
        public UInt32 dwNumAddr;
        //Specifies the number of routes in the IP routing table. 
        public UInt32 dwNumRoutes;
    };
#endregion

#region GetIcmpStatistics struct
    [StructLayout(LayoutKind.Sequential)]
    public struct MIBICMPSTATS
    {
        public UInt32 dwMsgs;
        public UInt32 dwErrors;
        public UInt32 dwDestUnreachs;
        public UInt32 dwTimeExcds;
        public UInt32 dwParmProbs;
        public UInt32 dwSrcQuenchs;
        public UInt32 dwRedirects;
        public UInt32 dwEchos;
        public UInt32 dwEchoReps;
        public UInt32 dwTimestamps;
        public UInt32 dwTimestampReps;
        public UInt32 dwAddrMasks;
        public UInt32 dwAddrMaskReps;
    };
    [StructLayout(LayoutKind.Sequential)]
    public struct MIBICMPINFO
    {
        public MIBICMPSTATS icmpInStats;
        public MIBICMPSTATS icmpOutStats;
    };
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_ICMP 
    {
        public MIBICMPINFO stats;
    };
#endregion

#region GetUdpStatistics(Ex) struct
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_UDPSTATS
    {
        public UInt32 dwInDatagrams;
        public UInt32 dwNoPorts;
        public UInt32 dwInErrors;
        public UInt32 dwOutDatagrams;
        public UInt32 dwNumAddrs;
    }
#endregion

#region GetUdpTable class
    public class CMIB_UDPTABLE 
    {
        public UInt32 dwNumEntries=0;
        public CMIB_UDPROW[] table;
        public bool decode(byte[] array)
        {
            byte[] pb_tmp=new byte[CMIB_UDPROW.size];
            if (array==null)
                return false;
            if (array.Length<4)
                return false;
            this.dwNumEntries=System.BitConverter.ToUInt32(array,0);
            if (array.Length<this.dwNumEntries)
                return false;
            this.table=new CMIB_UDPROW[this.dwNumEntries];
            for (UInt32 cpt=0;cpt<this.dwNumEntries;cpt++)
            {
                System.Array.Copy(array,(int)(cpt*CMIB_UDPROW.size+4),pb_tmp,0,CMIB_UDPROW.size);
                this.table[cpt]=new CMIB_UDPROW();
                if (!this.table[cpt].decode(pb_tmp))
                    return false;
            }
            return true;
        }
    }

    public class CMIB_UDPROW
    {
        public const byte size=8;
        // in networkhost order
        public UInt32 dwLocalAddr=0;
        // in networkhost order
        public UInt32 dwLocalPort=0;
        public bool decode(byte[] array)
        {
            if (array==null)
                return false;
            if (array.Length<CMIB_UDPROW.size)
                return false;
            this.dwLocalAddr=System.BitConverter.ToUInt32(array,0);
            this.dwLocalPort=System.BitConverter.ToUInt32(array,4);
            return true;
        }
        /// <summary>
        /// get ip in standard notation
        /// </summary>
        /// <returns></returns>
        public string get_ip()
        {
            System.Net.IPAddress ipa=new System.Net.IPAddress(this.dwLocalAddr);
            string ret=ipa.ToString();
            return ret;
        }
        public UInt32 get_port()
        {
            UInt32 ret=((this.dwLocalPort>>8)&0xff) +((this.dwLocalPort&0xff)<<8);
            return ret;
        }
    }
#endregion

#region GetUdpExTable class
    public class CMIB_UDPEXTABLE
    {
        public UInt32 dwNumEntries=0;  
        public CMIB_UDPEXROW[] table;
        public bool decode(byte[] array)
        {
            byte[] pb_tmp=new byte[CMIB_UDPEXROW.size_ex];
            if (array==null)
                return false;
            if (array.Length<4)
                return false;
            this.dwNumEntries=System.BitConverter.ToUInt32(array,0);
            if (array.Length<this.dwNumEntries)
                return false;
            this.table=new CMIB_UDPEXROW[this.dwNumEntries];
            for (UInt32 cpt=0;cpt<this.dwNumEntries;cpt++)
            {
                System.Array.Copy(array,(int)(cpt*CMIB_UDPEXROW.size_ex+4),pb_tmp,0,CMIB_UDPEXROW.size_ex);
                this.table[cpt]=new CMIB_UDPEXROW();
                if (!this.table[cpt].decode_ex(pb_tmp))
                    return false;
            }
            return true;
        }
    }

    public class CMIB_UDPEXROW:CMIB_UDPROW
    {
        public const byte size_ex=CMIB_UDPROW.size+4;
        public UInt32 dwProcessId=0;
        public System.Diagnostics.Process pProcess=null;
        public bool decode_ex(byte[] array)
        {
            if (!this.decode(array))
                return false;
            // array is not null (see decode())
            if (array.Length<CMIB_UDPEXROW.size_ex)
                return false;
            this.dwProcessId=System.BitConverter.ToUInt32(array,CMIB_UDPROW.size);
            this.pProcess=iphelper_Process.GetProcessById(this.dwProcessId);
            return true;
        }
    }

#endregion

#region GetTcpStatistics(Ex) struct
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_TCPSTATS
    {
        public UInt32 dwRtoAlgorithm;
        public UInt32 dwRtoMin;
        public UInt32 dwRtoMax;
        public UInt32 dwMaxConn;
        public UInt32 dwActiveOpens;
        public UInt32 dwPassiveOpens;
        public UInt32 dwAttemptFails;
        public UInt32 dwEstabResets;
        public UInt32 dwCurrEstab;
        public UInt32 dwInSegs;
        public UInt32 dwOutSegs;
        public UInt32 dwRetransSegs;
        public UInt32 dwInErrs;
        public UInt32 dwOutRsts;
        public UInt32 dwNumConns;
    }
#endregion

#region GetTcpTable
    public class CMIB_TCPTABLE 
    {
        public UInt32 dwNumEntries=0;
        public CMIB_TCPROW[] table=null;
        public bool decode(byte[] array)
        {
            byte[] pb_tmp=new byte[CMIB_TCPROW.size];
            if (array==null)
                return false;
            if (array.Length<4)
                return false;
            this.dwNumEntries=System.BitConverter.ToUInt32(array,0);
            if (array.Length<this.dwNumEntries)
                return false;
            this.table=new CMIB_TCPROW[this.dwNumEntries];
            for (UInt32 cpt=0;cpt<this.dwNumEntries;cpt++)
            {
                System.Array.Copy(array,(int)(cpt*CMIB_TCPROW.size+4),pb_tmp,0,CMIB_TCPROW.size);
                this.table[cpt]=new CMIB_TCPROW();
                if (!this.table[cpt].decode(pb_tmp))
                    return false;
            }
            return true;
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_TCPROW 
    {
        public UInt32 dwState;
        public UInt32 dwLocalAddr;
        public UInt32 dwLocalPort;
        public UInt32 dwRemoteAddr;
        public UInt32 dwRemotePort;
    }
    public class CMIB_TCPROW
    {
        public const byte MIB_TCP_STATE_CLOSED = 1;
        public const byte MIB_TCP_STATE_LISTEN = 2;
        public const byte MIB_TCP_STATE_SYN_SENT = 3;
        public const byte MIB_TCP_STATE_SYN_RCVD = 4;
        public const byte MIB_TCP_STATE_ESTAB = 5;
        public const byte MIB_TCP_STATE_FIN_WAIT1 = 6;
        public const byte MIB_TCP_STATE_FIN_WAIT2 = 7;
        public const byte MIB_TCP_STATE_CLOSE_WAIT = 8;
        public const byte MIB_TCP_STATE_CLOSING = 9;
        public const byte MIB_TCP_STATE_LAST_ACK = 10;
        public const byte MIB_TCP_STATE_TIME_WAIT = 11;
        public const byte MIB_TCP_STATE_DELETE_TCB = 12;

        public const byte size=5*4;
        public UInt32 dwState=0;
        // in networkhost order
        public UInt32 dwLocalAddr=0;
        // in networkhost order
        public UInt32 dwLocalPort=0;
        // in networkhost order
        public UInt32 dwRemoteAddr=0;
        // in networkhost order
        public UInt32 dwRemotePort=0;
        public bool decode(byte[] array)
        {
            if (array==null)
                return false;
            if (array.Length<CMIB_TCPROW.size)
                return false;
            this.dwState=System.BitConverter.ToUInt32(array,0);
            this.dwLocalAddr=System.BitConverter.ToUInt32(array,4);
            this.dwLocalPort=System.BitConverter.ToUInt32(array,8);
            this.dwRemoteAddr=System.BitConverter.ToUInt32(array,12);
            if (this.dwState==CMIB_TCPROW.MIB_TCP_STATE_LISTEN)
                this.dwRemotePort=0;
            else
                this.dwRemotePort=System.BitConverter.ToUInt32(array,16);
            return true;
        }
        public string get_localip()
        {
            System.Net.IPAddress ipa=new System.Net.IPAddress(this.dwLocalAddr);
            string ret=ipa.ToString();
            return ret;
        }
        public string get_remoteip()
        {
            System.Net.IPAddress ipa=new System.Net.IPAddress(this.dwRemoteAddr);
            string ret=ipa.ToString();
            return ret;
        }
        public UInt32 get_local_port()
        {
            UInt32 ret=((this.dwLocalPort>>8)&0xff) +((this.dwLocalPort&0xff)<<8);
            return ret;
        }
        public UInt32 get_remote_port()
        {
            UInt32 ret=((this.dwRemotePort>>8)&0xff) +((this.dwRemotePort&0xff)<<8);
            return ret;
        }
        public string get_state()
        {
            string ret="Unknown";
            switch (this.dwState)
            {
                case MIB_TCP_STATE_CLOSED: ret = "CLOSED"; break;
                case MIB_TCP_STATE_LISTEN: ret = "LISTENING"; break;
                case MIB_TCP_STATE_SYN_SENT: ret = "SYN_SENT"; break;
                case MIB_TCP_STATE_SYN_RCVD: ret = "SYN_RCVD"; break;
                case MIB_TCP_STATE_ESTAB: ret = "ESTABLISHED"; break;
                case MIB_TCP_STATE_FIN_WAIT1: ret = "FIN_WAIT1"; break;
                case MIB_TCP_STATE_FIN_WAIT2: ret = "FIN_WAIT2"; break;
                case MIB_TCP_STATE_CLOSE_WAIT: ret = "CLOSE_WAIT"; break;
                case MIB_TCP_STATE_CLOSING: ret = "CLOSING"; break;
                case MIB_TCP_STATE_LAST_ACK: ret = "LAST_ACK"; break;
                case MIB_TCP_STATE_TIME_WAIT: ret = "TIME_WAIT"; break;
                case MIB_TCP_STATE_DELETE_TCB: ret = "DELETE_TCB"; break;
            }
            return ret;
        }
        /// <summary>
        /// used for SetTcpEntry
        /// </summary>
        /// <returns></returns>
        public MIB_TCPROW get_MIB_TCPROW_struct()
        {
            MIB_TCPROW ret=new MIB_TCPROW();
            ret.dwLocalAddr=this.dwLocalAddr;
            ret.dwLocalPort=this.dwLocalPort;
            ret.dwRemoteAddr=this.dwRemoteAddr;
            ret.dwRemotePort=this.dwRemotePort;
            ret.dwState=this.dwState;
            return ret;
        }
    }
#endregion

#region GetTcpExTable class

    public class CMIB_TCPEXTABLE
    {
        public UInt32 dwNumEntries=0;  
        public CMIB_TCPEXROW[] table;
        public bool decode(byte[] array)
        {
            byte[] pb_tmp=new byte[CMIB_TCPEXROW.size_ex];
            if (array==null)
                return false;
            if (array.Length<4)
                return false;
            this.dwNumEntries=System.BitConverter.ToUInt32(array,0);
            if (array.Length<this.dwNumEntries)
                return false;
            this.table=new CMIB_TCPEXROW[this.dwNumEntries];
            for (UInt32 cpt=0;cpt<this.dwNumEntries;cpt++)
            {
                System.Array.Copy(array,(int)(cpt*CMIB_TCPEXROW.size_ex+4),pb_tmp,0,CMIB_TCPEXROW.size_ex);
                this.table[cpt]=new CMIB_TCPEXROW();
                if (!this.table[cpt].decode_ex(pb_tmp))
                    return false;
            }
            return true;
        }
    }

    public class CMIB_TCPEXROW:CMIB_TCPROW
    {
        public const byte size_ex=CMIB_TCPROW.size+4;
        public UInt32 dwProcessId=0;
        public System.Diagnostics.Process pProcess=null;
        public bool decode_ex(byte[] array)
        {
            if (!this.decode(array))
                return false;
            // array is not null (see decode())
            if (array.Length<CMIB_TCPEXROW.size_ex)
                return false;
            this.dwProcessId=System.BitConverter.ToUInt32(array,CMIB_TCPEXROW.size);
            this.pProcess=iphelper_Process.GetProcessById(this.dwProcessId);
            return true;
        }
    }
#endregion
}