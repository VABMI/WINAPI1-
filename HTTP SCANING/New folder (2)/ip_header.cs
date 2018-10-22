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
namespace easy_socket.ip_header
{
    #region events
    /// <summary>
    /// used to raise Exception
    /// contains exception
    /// </summary>
    public class EventArgs_Exception:EventArgs
    {
        public readonly Exception exception;
        public EventArgs_Exception(Exception e)
        {
            this.exception=e;
        }
    }
    public class EventArgs_Exception_Packet:EventArgs_Exception
    {
        public readonly byte[] buffer;
        public EventArgs_Exception_Packet(Exception e,byte[] buffer):base(e)
        {
            this.buffer=buffer;
        }
    }
    public class EventArgs_FullPacket:EventArgs
    {
        public readonly byte[] buffer;
        public EventArgs_FullPacket(byte[] buffer)
        {
            this.buffer=buffer;
        }
    }
    public delegate void Socket_Error_EventHandler(ipv4_header sender, EventArgs_Exception e);
    public delegate void Socket_RcvPacket_Error_EventHandler(ipv4_header sender, EventArgs_Exception_Packet e);
    public delegate void Socket_Data_Arrival_EventHandler(ipv4_header sender, EventArgs_FullPacket e);
    #endregion

    public class ipv4_header
    {
        protected const byte default_ip_ttl=255;        
        #region rfc 791 fields
        /// <summary>
        /// version (4)
        /// </summary>
        public byte version;
        /// <summary>
        /// length of the header in 32 bit words (min 5)
        /// </summary>
        public byte internet_header_length;
        public bool b_comput_internet_header_length;
        /// <summary>
        /// Bits 0-2:  Precedence.
        /// Bit    3:  0 = Normal Delay,      1 = Low Delay.
        /// Bits   4:  0 = Normal Throughput, 1 = High Throughput.
        /// Bits   5:  0 = Normal Relibility, 1 = High Relibility.
        /// Bit  6-7:  Reserved for Future Use.
        ///     0     1     2     3     4     5     6     7
        /// +-----+-----+-----+-----+-----+-----+-----+-----+
        /// |                 |     |     |     |     |     |
        /// |   PRECEDENCE    |  D  |  T  |  R  |  0  |  0  |
        /// |                 |     |     |     |     |     |
        /// +-----+-----+-----+-----+-----+-----+-----+-----+
        ///        Precedence
        ///     111 - Network Control
        ///     110 - Internetwork Control
        ///     101 - CRITIC/ECP
        ///     100 - Flash Override
        ///     011 - Flash
        ///     010 - Immediate
        ///     001 - Priority
        ///     000 - Routine
        /// </summary>
        public byte type_of_service;
        public byte Delay
        {
            set
            {
                this.type_of_service&=0xEF;
                if (value>0)
                    this.type_of_service|=0x10;
            }
            get
            {
                if ((this.type_of_service&0x10)>0)
                    return 1;
                else
                    return 0;
            }
        }
        public byte Throughput
        {
            set
            {
                this.type_of_service&=0xF7;
                if (value>0)
                    this.type_of_service|=0x8;
            }
            get
            {
                if ((this.type_of_service&0x8)>0)
                    return 1;
                else
                    return 0;
            }
        }
        public byte Relibility
        {
            set
            {
                this.type_of_service&=0xFB;
                if (value>0)
                    this.type_of_service|=0x4;
            }
            get
            {
                if ((this.type_of_service&0x4)>0)
                    return 1;
                else
                    return 0;
            }
        }
        public byte Precedence
        {
            set
            {
                this.type_of_service&=0x1F;
                this.type_of_service+=(byte)((value&0x7)<<5);
            }
            get
            {
                return (byte)(this.type_of_service>>5);
            }
        }
        public bool b_comput_total_length;
        /// <summary>
        /// length of the datagram, measured in octets, including internet header and data
        /// In network byte order    
        /// </summary>
        public UInt16 total_length;
        /// <summary>
        /// total_length in host byte order
        /// </summary>
        public UInt16 TotalLength
        {
            set
            {
                this.total_length=easy_socket.network_convert.switch_ushort(value);
            }
            get
            {
                return easy_socket.network_convert.switch_ushort(this.total_length);
            }
        }

        /// <summary>
        /// An identifying value assigned by the sender to aid in assembling the fragments of a datagram
        /// In network byte order
        /// </summary>
        public UInt16 identification;
        /// <summary>
        /// identification in host byte order
        /// </summary>
        public UInt16 Identification
        {
            set
            {
                this.identification=easy_socket.network_convert.switch_ushort(value);
            }
            get
            {
                return easy_socket.network_convert.switch_ushort(this.identification);
            }
        }

        /// <summary>
        ///     Bit 0: reserved, must be zero
        ///     Bit 1: (DF) 0 = May Fragment,  1 = Don't Fragment.
        ///     Bit 2: (MF) 0 = Last Fragment, 1 = More Fragments.
        ///         0   1   2
        ///         +---+---+---+
        ///         |   | D | M |
        ///         | 0 | F | F |
        ///         +---+---+---+
        /// </summary>
        public byte flags;
        public byte MayDontFragment
        {
            set
            {
                this.flags&=0x5;
                if (value>0)
                    this.flags|=0x2;
            }
            get
            {
                return (byte)((this.flags&0x2)>>1);
            }
        }
        public byte LastMoreFragment
        {
            set
            {
                this.flags&=0x6;
                if (value>0)
                    this.flags|=0x1;
            }
            get
            {
                return (byte)(this.flags&0x1);
            }
        }
        /// <summary>
        /// This field indicates where in the datagram this fragment belongs.
        /// The fragment offset is measured in units of 8 octets (64 bits).  The
        /// first fragment has offset zero.
        /// In network byte order
        /// </summary>
        public UInt16 fragment_offset;
        /// <summary>
        /// fragment_offset in host byte order
        /// </summary>
        public UInt16 FragmentOffset
        {
            set
            {
                this.fragment_offset=easy_socket.network_convert.switch_ushort(value);
            }
            get
            {
                return easy_socket.network_convert.switch_ushort(this.fragment_offset);
            }
        }
        public byte time_to_live;
        public byte protocol;
        public const byte protocol_icmp=1;
        public const byte protocol_Gateway_to_Gateway=3;
        public const byte protocol_tcp=6;
        public const byte protocol_telnet=14;
        public const byte protocol_udp=17;
        #region protocol numbers
        /*
                Decimal    Octal      Protocol Numbers                  References
                -------    -----      ----------------                  ----------
                    0       0         Reserved                              [JBP]
                    1       1         ICMP                               [53,JBP]
                    2       2         Unassigned                            [JBP]
                    3       3         Gateway-to-Gateway              [48,49,VMS]
                    4       4         CMCC Gateway Monitoring Message [18,19,DFP]
                    5       5         ST                                 [20,JWF]
                    6       6         TCP                                [34,JBP]
                    7       7         UCL                                    [PK]
                    8      10         Unassigned                            [JBP]
                    9      11         Secure                                [VGC]
                    10      12         BBN RCC Monitoring                    [VMS]
                    11      13         NVP                                 [12,DC]
                    12      14         PUP                                [4,EAT3]
                    13      15         Pluribus                             [RDB2]
                    14      16         Telenet                              [RDB2]
                    15      17         XNET                              [25,JFH2]
                    16      20         Chaos                                [MOON]
                    17      21         User Datagram                      [42,JBP]
                    18      22         Multiplexing                       [13,JBP]
                    19      23         DCN                                  [DLM1]
                    20      24         TAC Monitoring                     [55,RH6]
                21-62   25-76         Unassigned                            [JBP]
                    63      77         any local network                     [JBP]
                    64     100         SATNET and Backroom EXPAK            [DM11]
                    65     101         MIT Subnet Support                    [NC3]
                66-68 102-104         Unassigned                            [JBP]
                    69     105         SATNET Monitoring                    [DM11]
                    70     106         Unassigned                            [JBP]
                    71     107         Internet Packet Core Utility         [DM11]
                72-75 110-113         Unassigned                            [JBP]
                    76     114         Backroom SATNET Monitoring           [DM11]
                    77     115         Unassigned                            [JBP]
                    78     116         WIDEBAND Monitoring                  [DM11]
                    79     117         WIDEBAND EXPAK                       [DM11]
                80-254 120-376         Unassigned                            [JBP]
                    255     377         Reserved                              [JBP]
            */
        #endregion
        public bool b_comput_header_checksum;
        /// <summary>
        /// header_checksum in network byte order
        /// </summary>
        public UInt16 header_checksum;
        /// <summary>
        /// header_checksum in host byte order
        /// </summary>
        public UInt16 HeaderChecksum
        {
            set
            {
                this.header_checksum=easy_socket.network_convert.switch_ushort(value);
            }
            get
            {
                return easy_socket.network_convert.switch_ushort(this.header_checksum);
            }
        }
        /// <summary>
        /// source addres in network byte order
        /// </summary>
        public UInt32 source_address;
        /// <summary>
        /// destination addres in network byte order
        /// </summary>
        public UInt32 destination_address;
        public byte[] options_and_padding;// size=multiple of 32
        public byte[] data;
        #endregion
        #region constructor
        public ipv4_header()
        {
            this.version=4;
            this.internet_header_length=5;// non options
            this.type_of_service=0;
            this.total_length=20;// no data
            this.identification=0;
            this.flags=0;
            this.fragment_offset=0;// first fragment
            this.time_to_live=default_ip_ttl;
            this.protocol=ipv4_header.protocol_tcp;
            this.header_checksum=0;
            this.source_address=0x100007f;
            this.destination_address=0x100007f;
            this.b_comput_header_checksum=true;
            this.b_comput_total_length=true;
            this.b_comput_internet_header_length=true;
        }

        #endregion
        #region encoding decoding
        public byte[] encode()
        {
            int option_size=0;
                
            if (this.options_and_padding!=null)
                option_size=this.options_and_padding.Length;
            int data_size=0;
            if (this.data!=null)
                data_size=this.data.Length;
            if (this.b_comput_internet_header_length)
                this.internet_header_length=(byte)(5+option_size/4);
            ushort real_total_length=(UInt16)(20+option_size+data_size);
            if (this.b_comput_total_length)
                this.total_length=real_total_length;
            byte[] ret=new byte[this.total_length];

            ret[0]=(byte)((this.version<<4)+this.internet_header_length);
            ret[1]=this.type_of_service;
            System.Array.Copy(System.BitConverter.GetBytes(this.total_length),0,ret,2,2);
            System.Array.Copy(System.BitConverter.GetBytes(this.identification),0,ret,4,2);
            ret[6]=(byte)((this.flags<<5)+((this.fragment_offset>>8)&0x1f));
            ret[7]=(byte)(this.fragment_offset&0xff);
            ret[8]=this.time_to_live;
            ret[9]=this.protocol;
            if (this.b_comput_header_checksum)
                this.header_checksum=0;
            System.Array.Copy(System.BitConverter.GetBytes(this.header_checksum),0,ret,10,2);
            System.Array.Copy(System.BitConverter.GetBytes(this.source_address),0,ret,12,4);
            System.Array.Copy(System.BitConverter.GetBytes(this.destination_address),0,ret,16,4);

            // adding options
            if (option_size>0)
            {
                System.Array.Copy(this.options_and_padding,0,ret,20,option_size);
            }
            if (this.b_comput_header_checksum)
            {
                // comput checksum
                this.header_checksum=Cchecksum.checksum(ret,true);
                System.Array.Copy(System.BitConverter.GetBytes(this.header_checksum),0,ret,10,2);
            }
            // adding data
            if (data_size>0)
            {
                System.Array.Copy(this.data,0,ret,20+option_size,data_size);
            }
            return ret;
        }

        public const byte error_success=0;
        public const byte error_datagram_null=1;
        public const byte error_datagram_internet_header_length_too_small=2;
        public const byte error_datagram_total_length_too_small=3;
        public const byte error_datagram_not_complete=4;
        public const byte error_datagram_length_not_matching=5;

        public byte decode(byte[] full_datagram)
        {
            if (full_datagram==null)
                return ipv4_header.error_datagram_null;
            return this.decode(full_datagram,full_datagram.Length);
        }
        public byte decode(byte[] full_datagram,int full_datagram_size)
        {
            if (full_datagram==null)
                return ipv4_header.error_datagram_null;
            if (full_datagram_size<20)
                return ipv4_header.error_datagram_not_complete;
            this.version=(byte)((full_datagram[0]>>4)&0xf);
            this.internet_header_length=(byte)(full_datagram[0]&0xf);
            if (this.internet_header_length<5)
                return ipv4_header.error_datagram_internet_header_length_too_small;
            this.type_of_service=full_datagram[1];
            this.total_length=System.BitConverter.ToUInt16(full_datagram,2);
            if (this.TotalLength<20)
                return ipv4_header.error_datagram_total_length_too_small;
            this.identification=System.BitConverter.ToUInt16(full_datagram,4);
            this.flags=(byte)((full_datagram[6]>>5)&0x7);
            this.fragment_offset=(UInt16)((full_datagram[6]&0x1F)+full_datagram[7]);
            this.time_to_live=full_datagram[8];
            this.protocol=full_datagram[9];
            this.header_checksum=System.BitConverter.ToUInt16(full_datagram,10);
            this.source_address=System.BitConverter.ToUInt32(full_datagram,12);
            this.destination_address=System.BitConverter.ToUInt32(full_datagram,16);
            // decode options
            this.options_and_padding=null;
            int options_length=Math.Min(this.internet_header_length*4-20,full_datagram_size-20);// decode max info before return
            if (options_length>0)
            {
                this.options_and_padding=new byte[options_length];
                System.Array.Copy(full_datagram,20,this.options_and_padding,0,options_length);
            }
            // decode data
            this.data=null;
            int data_length=Math.Min(this.TotalLength-20-options_length,full_datagram_size-20-options_length);// decode max info before return
            if (data_length>0)
            {
                this.data=new byte[data_length];
                System.Array.Copy(full_datagram,20+options_length,data,0,data_length);
            }
            if (full_datagram_size<this.TotalLength)
                return ipv4_header.error_datagram_not_complete;

            if (full_datagram_size!=this.TotalLength)
                return error_datagram_length_not_matching;
            // checksum can't be checked since some header fields change (e.g., time to live)
            return ipv4_header.error_success;
        }
        #endregion
        #region getters setters for source and destination ip in string format
        public string SourceAddress
        {
            set
            {
                try
                {
                    this.source_address=(UInt32)System.Net.IPAddress.Parse(value).Address;
                }
                catch
                {
                    this.source_address=(UInt32)System.Net.IPAddress.Parse("127.0.0.1").Address;
                }
            }
            get
            {
                return new System.Net.IPAddress(this.source_address).ToString();
            }
        }
        public string DestinationAddress
        {
            set
            {
                try
                {
                    this.destination_address=(UInt32)System.Net.IPAddress.Parse(value).Address;
                }
                catch
                {
                    this.destination_address=(UInt32)System.Net.IPAddress.Parse("127.0.0.1").Address;
                }
            }
            get
            {
                return new System.Net.IPAddress(this.destination_address).ToString();
            }
        }
        #endregion
    }

    #region ipv4_header_client
    public class ipv4_header_client:ipv4_header
    {
        public event Socket_Error_EventHandler event_Socket_Error;

        protected System.Net.Sockets.Socket socket;
        protected const ushort default_remote_client_port=80;
        public bool b_allow_broadcast;

        public ipv4_header_client()
        {
            this.b_allow_broadcast=false;
        }
        #region send
        /// <summary>
        /// send to the specified ip without using parameters of the current object (classical use of socket for sending)
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="protocol"></param>
        public void send_without_forging(string ip,System.Net.Sockets.ProtocolType protocol,byte[] data)
        {
            this.send_without_forging(ip,protocol,default_ip_ttl,data);
        }
        /// <summary>
        /// send to the specified ip without using parameters of the current object (classical use of socket for sending)
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="protocol"></param>
        /// <param name="ttl"></param>
        /// <param name="data"></param>
        public void send_without_forging(string ip,System.Net.Sockets.ProtocolType protocol,byte ttl,byte[] data)
        {
            this.send_without_forging(ip,default_remote_client_port,protocol,ttl,data);
        }
        /// <summary>
        /// send to the specified ip without using parameters of the current object (classical use of socket for sending)
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="protocol"></param>
        /// <param name="ttl"></param>
        /// <param name="data"></param>
        public void send_without_forging(string ip,ushort port,System.Net.Sockets.ProtocolType protocol,byte ttl,byte[] data)
        {
            System.Net.IPAddress ipaddr;
            // making end point
            try
            {
                // don't resolve if ip is like xxx.yyy.uuu.vvv
                ipaddr=System.Net.IPAddress.Parse(ip);
            }
            catch
            {
                System.Net.IPHostEntry iphe;
                // else resolve
                try
                {
                    iphe=System.Net.Dns.Resolve(ip);
                }
                catch(Exception e)
                {
                    this.event_Socket_Error(this,new EventArgs_Exception(e));
                    return;
                }
                ipaddr=iphe.AddressList[0];
            }
            this.send_without_forging(ipaddr,port,protocol,ttl,data);
        }
        public void send_without_forging(UInt32 ip_dest_in_network_order,ushort port,System.Net.Sockets.ProtocolType protocol,byte ttl,byte[] data)
        {
            this.send_without_forging(new System.Net.IPAddress((long)ip_dest_in_network_order),port,protocol,ttl,data);
        }
        public void send_without_forging(System.Net.IPAddress ipaddr,ushort port,System.Net.Sockets.ProtocolType protocol,byte ttl,byte[] data)
        {
            System.Net.IPEndPoint remoteipep;
            try
            {
                remoteipep = new System.Net.IPEndPoint(ipaddr,port);
                this.socket=new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork,System.Net.Sockets.SocketType.Raw ,protocol);
                this.socket.SetSocketOption(System.Net.Sockets.SocketOptionLevel.IP, System.Net.Sockets.SocketOptionName.IpTimeToLive,ttl);
                // in case of broadcast
                if (this.b_allow_broadcast)
                    this.socket.SetSocketOption( System.Net.Sockets.SocketOptionLevel.Socket, System.Net.Sockets.SocketOptionName.Broadcast,1);
                if (ipaddr.Address==0xFFFFFFFF)
                    this.socket.SendTo(data,0,data.Length,System.Net.Sockets.SocketFlags.DontRoute,remoteipep);
                else
                    this.socket.SendTo(data,remoteipep);
            }
            catch (Exception e)
            {
                // send event_Socket_Error
                if (this.event_Socket_Error!=null)
                    this.event_Socket_Error(this,new EventArgs_Exception(e));
                // close socket
                this.close_clt_socket();
            }
        }
        /// <summary>
        /// send other the net packet with the current object as ip header
        /// </summary>
        public void send()
        {
            this.send(true,default_remote_client_port);
        }
        /// <summary>
        /// send other the net packet with the current object as ip header
        /// </summary>
        /// <param name="remote_port">remote port if any (for tcp or udp)</param>
        public void send(ushort remote_port)
        {
            this.send(true,remote_port);
        }
        /// <summary>
        /// send over the net packet with the current object as ip header
        /// </summary>
        /// <param name="close_previous">if true close previous socket object and use new one</param>
        /// <param name="remote_port">remote port if any (for tcp or udp)</param>
        public void send(bool close_previous,ushort remote_port)
        {
            try
            {
                if ((close_previous) || (this.socket==null))
                {
                    if (this.socket!=null)
                        this.close_clt_socket();
                    this.socket=new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork,System.Net.Sockets.SocketType.Raw ,System.Net.Sockets.ProtocolType.IP);
                    // allow broadcast
                    socket.SetSocketOption( System.Net.Sockets.SocketOptionLevel.Socket, System.Net.Sockets.SocketOptionName.Broadcast,1);
                    // allow to fill ip header
                    socket.SetSocketOption( System.Net.Sockets.SocketOptionLevel.IP, System.Net.Sockets.SocketOptionName.HeaderIncluded,1);
                }
                socket.SendTo(this.encode(),new System.Net.IPEndPoint(new System.Net.IPAddress(network_convert.switch_UInt32(this.destination_address)),remote_port));
            }
            catch (Exception e)
            {
                if (e is System.Net.Sockets.SocketException)
                {
                    // send event_Socket_Server_Error
                    if (this.event_Socket_Error!=null)
                        this.event_Socket_Error(this,new EventArgs_Exception(e));
                }
            }
        }
        protected void close_clt_socket()
        {
            try
            {
                if (this.socket!=null)
                {
                    this.socket.Close();
                }
            }
            catch
            {
            }
        }
        #endregion

        /// <summary>
        /// for ipv4_header_server avoid CS0070
        /// </summary>
        /// <param name="e"></param>
        protected void send_socket_error_event(Exception e)
        {
            if (this.event_Socket_Error!=null)
                this.event_Socket_Error(this,new EventArgs_Exception(e));
        }
    }
    #endregion

    #region ipv4_header_server
    public class ipv4_header_server:ipv4_header_client
    {
        public ushort us_receive_buffer_size;

        protected System.Net.Sockets.Socket srv_socket;
        protected const ushort default_local_server_port=0;
        protected const int default_server_timeout=0;
        protected ushort spyed_protocol;
        protected const ushort PROTOCOL_ALL=256;
        public bool b_sniff;
        
        public event Socket_RcvPacket_Error_EventHandler event_Socket_RcvPacket_Error;
        public event Socket_Data_Arrival_EventHandler event_Socket_Data_Arrival;
        public ipv4_header_server()
        {
            this.us_receive_buffer_size=8192;//4096;
            this.b_sniff=false;
            this.spyed_protocol=PROTOCOL_ALL;
        }
     
        #region stop
        /// <summary>
        /// stop server
        /// </summary>
        public void stop()
        {
            this.close_socket();
            // allow the watching thread to shutdown before to return
            System.Threading.Thread.Sleep(1);
        }
        protected void close_socket()
        {
            try
            {
                if (this.srv_socket!=null)
                {
                    this.srv_socket.Close();
                }
            }
            catch
            {
            }
        }
        #endregion
        #region start

        /// <summary>
        /// start server you MUST call stop method before to exit your application 
        /// else there will be a thread still running
        /// </summary>
        /// <param name="protocol"></param>
        public void start()
        {
            this.start(this.spyed_protocol);
        }
        /// <summary>
        /// start server you MUST call stop method before to exit your application 
        /// else there will be a thread still running
        /// </summary>
        /// <param name="protocol"></param>
        public void start(ushort protocol)
        {
            this.start(new System.Net.IPEndPoint(System.Net.IPAddress.Any, default_local_server_port),
                protocol
                );
        }
        /// <summary>
        /// start server you MUST call stop method before to exit your application 
        /// else there will be a thread still running
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="protocol"></param>
        public void start(string ip,ushort port,ushort protocol)
        {
            System.Net.IPAddress ipaddr;
            // making end point
            try
            {
                // don't resolve if ip is like xxx.yyy.uuu.vvv
                ipaddr=System.Net.IPAddress.Parse(ip);
            }
            catch
            {
                System.Net.IPHostEntry iphe;
                // else resolve
                try
                {
                    iphe=System.Net.Dns.Resolve(ip);
                }
                catch(Exception e)
                {
                    this.send_socket_error_event(e);
                    return;
                }
                ipaddr=iphe.AddressList[0];
            }
            this.start(new System.Net.IPEndPoint(ipaddr, port),
                protocol
                );
        }

        /// <summary>
        /// start server you MUST call stop method before to exit your application 
        /// else there will be a thread still running
        /// </summary>
        /// <param name="ipep"></param>
        /// <param name="protocol"></param>
        public void start(System.Net.IPEndPoint ipep,ushort protocol)
        {
            bool b_res;
            this.spyed_protocol=protocol;
            if ((this.spyed_protocol==PROTOCOL_ALL)||(this.spyed_protocol==ipv4_header.protocol_tcp))
                this.b_sniff=true;
            try
            {
                // close previous server socket if there was one
                if (this.srv_socket!=null)
                    this.close_socket();
                // create new socket
                if (this.b_sniff)
                    b_res=this.socket_creator_sniff(ipep);
                else
                    b_res=this.socket_creator(ipep);
                if (!b_res)
                {
                    this.send_socket_error_event(new System.Exception("Error at socket creation."));
                    return;
                }

                // create and launch thread
                System.Threading.ThreadStart myThreadStart = new System.Threading.ThreadStart(thread_watching_reply);
                System.Threading.Thread myThread =new System.Threading.Thread(myThreadStart);
                myThread.Start();
            }
            catch (Exception e)
            {
                if (e is System.Net.Sockets.SocketException)
                {
                    // send event_Socket_Server_Error
                    /*
                    if (this.event_Socket_Error!=null)
                        this.event_Socket_Error(this,new EventArgs_Exception(e));
                    */
                    this.send_socket_error_event(e);
                }
            }
        }
        protected virtual bool socket_creator(System.Net.IPEndPoint ipep)
        {
            this.srv_socket=new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork,System.Net.Sockets.SocketType.Raw,System.Net.Sockets.ProtocolType.IP);
            // bind server
            this.srv_socket.Bind(ipep);
            return true;
        }
        protected virtual bool check_wanted_packet(ref ipv4_header iph)
        {
            if (this.spyed_protocol==ipv4_header_server.PROTOCOL_ALL)
                return true;
            return iph.protocol==this.spyed_protocol;
        }
        protected void thread_watching_reply()
        {
            int nBytes;
            byte[] ReceiveBuffer=new byte[this.us_receive_buffer_size];

            ipv4_header iph=new ipv4_header();
            byte[] data_buffer;
            byte b_ret;
            try
            {

                while (true)
                {
                    nBytes = this.srv_socket.Receive(ReceiveBuffer,0,this.us_receive_buffer_size,System.Net.Sockets.SocketFlags.None);
                    if (nBytes>0)
                    {
                        // allow to have an array matching data rcv size (avoid to send the size as parameter for each next methodes)
                        data_buffer=new byte[nBytes];
                        System.Array.Copy(ReceiveBuffer,0,data_buffer,0,nBytes);
                        // end allow to have an array matching data rcv size (avoid to send the size as parameter for each next methodes)

                        // decode ipheader
                        b_ret=iph.decode(data_buffer,nBytes);
                        // if decoding was successful
                        if (b_ret==ipv4_header.error_success)
                        {
                            if (this.check_wanted_packet(ref iph))
                            {
                                // throw the data arrival event
                                if (this.event_Socket_Data_Arrival!=null)
                                {
                                    try // avoid to close server if error in the event handler code
                                    {
                                        this.event_Socket_Data_Arrival(iph,new EventArgs_FullPacket(data_buffer));
                                    }
                                    catch{}
                                }
                            }
                        }
                        else
                        {
                            if ((b_ret==ipv4_header.error_datagram_internet_header_length_too_small)||
                                (b_ret==ipv4_header.error_datagram_total_length_too_small))
                            {
                                // send 
                                if (this.event_Socket_RcvPacket_Error!=null)
                                    this.event_Socket_RcvPacket_Error(this,new EventArgs_Exception_Packet(new Exception("Error in Ipv4 header"),data_buffer));
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                // close socket
                this.close_socket();
                

                if (e is System.Net.Sockets.SocketException)
                {
                    System.Net.Sockets.SocketException se=(System.Net.Sockets.SocketException)e;
                    if (se.ErrorCode==10004) // close methode as been called
                    {
                        return;
                    }
                }
                if (e is System.ObjectDisposedException)//throw by Receive (socket has been closed)
                    return;
                if (e is System.NullReferenceException)//throw by Receive if we put socket=null after socket.close
                    return;
                // send event_Socket_Server_Error
                
                //if (this.event_Socket_Error!=null)
                //    this.event_Socket_Error(iph,new EventArgs_Exception(e));
                
                this.send_socket_error_event(e);
            }

        }


        #endregion

        protected bool socket_creator_sniff(System.Net.IPEndPoint ipep)
        {
            if (ipep.Address==System.Net.IPAddress.Any)
            {
                System.Net.IPHostEntry iphe=System.Net.Dns.Resolve(System.Net.Dns.GetHostName());
                // if no network adaptater --> can't sniff
                if (iphe.AddressList.Length==0)
                {
                    System.Windows.Forms.MessageBox.Show("No network adaptater found.\r\nCan't start server","Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
                    // if only one adaptater don't ask to select one
                else if (iphe.AddressList.Length==1)
                {
                    ipep.Address=iphe.AddressList[0];
                }
                else// multiple adaptaters
                {
                    // choose adaptater
                    FormChooseSniffIp f=new FormChooseSniffIp();
                    f.set_ip_list(iphe.AddressList);
                    int index=f.get_ip_list_index();
                    // check index validity
                    if ((index<0)||(index>iphe.AddressList.Length-1))
                        index=0;
                    ipep.Address=iphe.AddressList[index];
                }
            }
            if (System.Windows.Forms.MessageBox.Show("Your Adaptater with ip "+ipep.Address.ToString()+" is going to pass in promiscious mode.\r\n Do You want to continue ?",
                "Warning",
                System.Windows.Forms.MessageBoxButtons.YesNo,
                System.Windows.Forms.MessageBoxIcon.Warning)
                ==System.Windows.Forms.DialogResult.No)
                return false;

            this.srv_socket=new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork,System.Net.Sockets.SocketType.Raw,System.Net.Sockets.ProtocolType.IP);
            // bind server
            this.srv_socket.Bind(ipep);
            //put server in sniffing mode
            byte[] optionInValue=new byte[4]{1, 0, 0, 0};
            byte[] optionOutValue=new byte[4];
            int SIO_RCVALL = unchecked((int)0x98000001);
            int ret;
            try // user must have administrator privilege
            {
                ret = this.srv_socket.IOControl(SIO_RCVALL, optionInValue,optionOutValue);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message,"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
            ret=optionOutValue[0] + optionOutValue[1] + optionOutValue[2] + optionOutValue[3];
            if(ret != 0)
                return false;
            return true;
        }
    }
    #endregion

    #region ipv4_header_icmp_server
    public class ipv4_header_icmp_server:ipv4_header_server
    {
        public ipv4_header_icmp_server()
        {
            this.spyed_protocol=ipv4_header.protocol_icmp;
        }

        protected override bool socket_creator(System.Net.IPEndPoint ipep)
        {
            this.srv_socket=new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork,System.Net.Sockets.SocketType.Raw,System.Net.Sockets.ProtocolType.Icmp);
            // bind server
            this.srv_socket.Bind(ipep);
            return true;
        }
    }
    #endregion

    #region ipv4_header_udp_server
    public class ipv4_header_udp_server:ipv4_header_server
    {
        public bool sniff_mode;
        public ipv4_header_udp_server()
        {
            this.spyed_protocol=ipv4_header.protocol_udp;
        }
        protected override bool socket_creator(System.Net.IPEndPoint ipep)
        {
            if (!this.b_sniff)
            {
                this.srv_socket=new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork,System.Net.Sockets.SocketType.Raw,System.Net.Sockets.ProtocolType.IP);
                // bind server
                this.srv_socket.Bind(ipep);
                return true;
            }
            else
            {
                return socket_creator_sniff(ipep);
            }
        }
    }
    #endregion

    #region ipv4_header_tcp_server
    public class ipv4_header_tcp_server:ipv4_header_server
    {
        public ipv4_header_tcp_server()
        {
            this.b_sniff=true;
            this.spyed_protocol=ipv4_header_server.protocol_tcp;
        }
    }
    #endregion




}

