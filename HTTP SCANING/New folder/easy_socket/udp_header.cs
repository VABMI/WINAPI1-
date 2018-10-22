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

namespace easy_socket.udp_header
{
    #region events and events arg
    public class EventArgs_ipv4header_ReceiveData:EventArgs
    {
        public readonly easy_socket.ip_header.ipv4_header ipv4header;
        public readonly byte[] full_packet;
        public EventArgs_ipv4header_ReceiveData(easy_socket.ip_header.ipv4_header ipv4h,byte[] full_packet)
        {
            this.ipv4header=ipv4h;
            this.full_packet=full_packet;
        }
    }
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
    public delegate void Socket_Error_EventHandler(udp_header sender, EventArgs_Exception e);
    public delegate void Socket_Data_Arrival_EventHandler(udp_header_server sender, EventArgs_ipv4header_ReceiveData e);

    #endregion
    public class udp_header_client:udp_header
    {
        // events
        public event Socket_Error_EventHandler event_Socket_Error;
        public easy_socket.ip_header.ipv4_header_client ipv4headerclt;

        public udp_header_client()
        {
            this.ipv4headerclt=new easy_socket.ip_header.ipv4_header_client();
            this.add_events();
        }
        private void add_events()
        {
            this.ipv4headerclt.event_Socket_Error+=new easy_socket.ip_header.Socket_Error_EventHandler(ipv4header_event_Socket_Error);
        }
        private void remove_events()
        {
            this.ipv4headerclt.event_Socket_Error-=new easy_socket.ip_header.Socket_Error_EventHandler(ipv4header_event_Socket_Error);
        }
        private void set_ipv4_header_clt(easy_socket.ip_header.ipv4_header_client iphc)
        {
            this.remove_events();
            this.ipv4headerclt=iphc;
            this.add_events();
        }
        #region send
        public void send(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order)
        {
            easy_socket.ip_header.ipv4_header_client iphc=new easy_socket.ip_header.ipv4_header_client();
            iphc.source_address=ip_source_in_network_order;
            iphc.destination_address=ip_destination_in_network_order;
            this.send(iphc);
        }
        public void send(UInt32 ip_source_in_network_order,UInt32 ip_destination_in_network_order,byte[] data)
        {
            this.data=data;
            this.send(ip_source_in_network_order,ip_destination_in_network_order);
        }
        public void send(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order,ushort udp_length)
        {
            easy_socket.ip_header.ipv4_header_client iphc=new easy_socket.ip_header.ipv4_header_client();
            iphc.source_address=ip_source_in_network_order;
            iphc.destination_address=ip_destination_in_network_order;
            this.send(iphc,udp_length);
        }
        public void send(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order,byte[] data,ushort udp_length)
        {
            this.data=data;
            this.send(ip_source_in_network_order,ip_destination_in_network_order,udp_length);
        }
        
        public void send(string ip_source,string ip_destination)
        {
            easy_socket.ip_header.ipv4_header_client iphc=new easy_socket.ip_header.ipv4_header_client();
            iphc.SourceAddress=ip_source;
            iphc.DestinationAddress=ip_destination;
            this.send(iphc);
        }
        public void send(string ip_source,string ip_destination,ushort udp_length)
        {
            easy_socket.ip_header.ipv4_header_client iphc=new easy_socket.ip_header.ipv4_header_client();
            iphc.SourceAddress=ip_source;
            iphc.DestinationAddress=ip_destination;
            this.send(iphc,udp_length);
        }
        public void send(string ip_source,string ip_destination,byte[] data)
        {
            this.data=data;
            this.send(ip_source,ip_destination);
        }
        public void send(string ip_source,string ip_destination,byte[] data,ushort udp_length)
        {
            this.data=data;
            this.send(ip_source,ip_destination,udp_length);
        }
        
        public void send(easy_socket.ip_header.ipv4_header_client iph_clt)
        {
            this.set_ipv4_header_clt(iph_clt);
            this.ipv4headerclt.data=this.encode(this.ipv4headerclt.source_address,this.ipv4headerclt.destination_address);
            this.ipv4headerclt.send(this.DestinationPort);
        }
        public void send(easy_socket.ip_header.ipv4_header_client iph_clt,ushort udp_length)
        {
            this.set_ipv4_header_clt(iph_clt);
            this.ipv4headerclt.data=this.encode(this.ipv4headerclt.source_address,this.ipv4headerclt.destination_address,this.data,udp_length);
            this.ipv4headerclt.send(this.DestinationPort);
        }

        #endregion

        protected void ipv4header_event_Socket_Error(easy_socket.ip_header.ipv4_header sender, easy_socket.ip_header.EventArgs_Exception e)
        {
            if (this.event_Socket_Error!=null)
                this.event_Socket_Error(this,new easy_socket.udp_header.EventArgs_Exception(e.exception));
        }
    }

    public class udp_header_server:udp_header_client
    {
        #region events
        //events
        public event Socket_Data_Arrival_EventHandler event_Data_Arrival;
        #endregion

        protected easy_socket.ip_header.ipv4_header_udp_server ipv4header_srv;
        public udp_header_server()
        {
            this.sniff_outgoing_packets=false;
            this.sniff_mode=false;
            this.ipv4header_srv=new easy_socket.ip_header.ipv4_header_udp_server();
            this.ipv4header_srv.event_Socket_Error+=new easy_socket.ip_header.Socket_Error_EventHandler(ipv4header_event_Socket_Error);
            this.ipv4header_srv.event_Socket_Data_Arrival+=new easy_socket.ip_header.Socket_Data_Arrival_EventHandler(socket_data_arrival);
        }
   
        private bool b_check_port;
        private bool b_check_ip;
        private ushort spy_port;
        private UInt32 spy_ip;
        /// <summary>
        /// only significant if sniff_mode=true
        /// </summary>
        public bool sniff_outgoing_packets;
        public bool sniff_mode;

        #region start
        /// <summary>
        /// start an tcp server. 
        /// you MUST call stop method to avoid remaing runing thread after having close your application.
        /// </summary>
        public void start()
        {
            this.sniff_mode=true;
            this.b_check_port=false;
            this.b_check_ip=false;
            string local_addr;
            System.Net.IPHostEntry iphe=System.Net.Dns.Resolve(System.Net.Dns.GetHostName());
            if (iphe.AddressList.Length==0)
            {
                System.Windows.Forms.MessageBox.Show("No network adaptater found.\r\nCan't start server","Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            // sniff on first network adaptater --> see ip_header_tcp_server limitations
            local_addr=iphe.AddressList[0].ToString();
            System.Windows.Forms.MessageBox.Show("Server started on network of ip"+local_addr,"Information",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information);
            this.ipv4header_srv.sniff_mode=this.sniff_mode;
            this.ipv4header_srv.start(local_addr,0,easy_socket.ip_header.ipv4_header_server.protocol_udp);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="local_port">in host byte order</param>
        public void start(ushort local_port)
        {
            this.b_check_port=true;
            this.b_check_ip=false;
            this.spy_port=easy_socket.network_convert.switch_ushort(local_port);
            string local_addr;
            System.Net.IPHostEntry iphe=System.Net.Dns.Resolve(System.Net.Dns.GetHostName());
            if (iphe.AddressList.Length==0)
            {
                System.Windows.Forms.MessageBox.Show("No network adaptater found.\r\nCan't start server","Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            // bind on first network adaptater
            local_addr=iphe.AddressList[0].ToString();
            System.Windows.Forms.MessageBox.Show("Server started on network of ip"+local_addr,"Information",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information);

            this.ipv4header_srv.sniff_mode=this.sniff_mode;
            this.ipv4header_srv.start(local_addr,local_port,easy_socket.ip_header.ipv4_header_server.protocol_udp);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="local_addr"></param>
        /// <param name="local_port">in host byte order</param>
        public void start(string local_addr,ushort local_port)
        {
            this.sniff_mode=true;
            this.b_check_port=true;
            this.b_check_ip=true;
            this.spy_port=easy_socket.network_convert.switch_ushort(local_port);
            this.spy_ip=System.BitConverter.ToUInt32(System.Net.IPAddress.Parse(local_addr).GetAddressBytes(),0);
            this.ipv4header_srv.sniff_mode=this.sniff_mode;
            if (this.sniff_mode)
            {
                System.Net.IPHostEntry iphe=System.Net.Dns.Resolve(System.Net.Dns.GetHostName());
                if (iphe.AddressList.Length==0)
                {
                    System.Windows.Forms.MessageBox.Show("No network adaptater found.\r\nCan't start server","Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }
                // bind on first network adaptater // allow spoofing
                this.ipv4header_srv.start(iphe.AddressList[0].ToString(),0,easy_socket.ip_header.ipv4_header_server.protocol_udp);
            }
            else
                this.ipv4header_srv.start(local_addr,0,easy_socket.ip_header.ipv4_header_server.protocol_udp);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="local_addr"></param>
        public void start(string local_addr)
        {
            this.b_check_port=false;
            this.b_check_ip=true;
            this.spy_ip=System.BitConverter.ToUInt32(System.Net.IPAddress.Parse(local_addr).GetAddressBytes(),0);
            this.ipv4header_srv.sniff_mode=this.sniff_mode;
            if (this.sniff_mode)
            {
                System.Net.IPHostEntry iphe=System.Net.Dns.Resolve(System.Net.Dns.GetHostName());
                if (iphe.AddressList.Length==0)
                {
                    System.Windows.Forms.MessageBox.Show("No network adaptater found.\r\nCan't start server","Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }
                // bind on first network adaptater // allow spoofing
                this.ipv4header_srv.start(iphe.AddressList[0].ToString(),0,easy_socket.ip_header.ipv4_header_server.protocol_udp);
            }
            else
                this.ipv4header_srv.start(local_addr,0,easy_socket.ip_header.ipv4_header_server.protocol_udp);
        }
        #endregion
        /// <summary>
        /// stop server
        /// </summary>
        public void stop()
        {
            // close socket
            this.ipv4header_srv.stop();
        }
        #region events management
        protected void socket_data_arrival(easy_socket.ip_header.ipv4_header sender,easy_socket.ip_header.EventArgs_FullPacket e)
        {
            if (this.sniff_mode)
            {
                // ip_header_server has checked that these are tcp packets see the arguments of ip_header_server.start
                if (this.b_check_ip)// check before decoding for speed reasons
                {
                    // if ip must be checked, check it
                    if (sender.destination_address!=this.spy_ip)
                    {
                        if (!this.sniff_outgoing_packets)
                            return;// packet is not satisfying condition
                        // else check outgoing condition
                        if (sender.source_address!=this.spy_ip)
                            return;
                    }
                }
            }
            if (this.decode(sender.source_address,sender.destination_address,sender.data)!=easy_socket.tcp_header.tcp_header.error_success)
                // here decoding error can be catched
                return;
            if (this.sniff_mode)
            {
                if (this.b_check_port)
                {
                    // if port must be checked, check it
                    if (this.destination_port!=this.spy_port)
                    {
                        if (!this.sniff_outgoing_packets)
                            return;// packet is not satisfying condition
                        // else check outgoing condition
                        if (this.source_port!=this.spy_port)
                            return;
                    }
                }
            }
            // send data arrival event
            if (this.event_Data_Arrival!=null)
                this.event_Data_Arrival(this,new EventArgs_ipv4header_ReceiveData(sender,e.buffer));
        }
        #endregion    
    }
    public class udp_header
    {
        #region members
        public const byte size=8;
        public byte[] data;
        /// <summary>
        /// source_port in network order 
        /// </summary>
        public ushort source_port;
        /// <summary>
        /// source_port in host order 
        /// </summary>
        public ushort SourcePort
        {
            get
            {
                return network_convert.switch_ushort(this.source_port);
            }
            set
            {
                this.source_port=network_convert.switch_ushort(value);
            }
        }
        /// <summary>
        /// destination_port in network order 
        /// </summary>
        public ushort destination_port;
        /// <summary>
        /// destination_port in host order 
        /// </summary>
        public ushort DestinationPort
        {
            get
            {
                return network_convert.switch_ushort(this.destination_port);
            }
            set
            {
                this.destination_port=network_convert.switch_ushort(value);
            }
        }
        /// <summary>
        /// flag used during encoding if udp_length is not specified, data.Length will be take. 
        /// </summary>
        protected bool b_udp_length_set;
        /// <summary>
        /// udp_length in network order 
        /// </summary>
        public ushort udp_length;
        /// <summary>
        /// udp_length in host order 
        /// </summary>
        public ushort UdpLength
        {
            get
            {
                return easy_socket.network_convert.switch_ushort(this.udp_length);
            }
            set
            {
                this.b_udp_length_set=true;
                this.udp_length=easy_socket.network_convert.switch_ushort(value);
            }

        }

        /// <summary>
        /// flag used during encoding if checksum is specified, it will not be computed 
        /// </summary>
        protected bool b_checksum_set;
        /// <summary>
        /// checksum in network order
        /// </summary>
        protected ushort checksum;
        /// <summary>
        /// checksum in host order,if specified, it will not be computed at encoding
        /// </summary>
        public ushort Checksum
        {
            get
            {
                return network_convert.switch_ushort(this.checksum);
            }
            set
            {
                this.checksum=network_convert.switch_ushort(value);
                this.b_checksum_set=true;
            }
        }
        #endregion

        #region constructor
        public udp_header()
        {
            this.data=null;
            this.b_udp_length_set=false;
            this.b_checksum_set=false;
            this.source_port=0;
            this.DestinationPort=1100;
            this.udp_length=udp_header.size;// at min udp header
            this.checksum=0;
        }
        #endregion

        #region encode
        public byte[] encode(string ip_source,string ip_destination)
        {
            return this.encode(System.BitConverter.ToUInt32(System.Net.IPAddress.Parse(ip_source).GetAddressBytes(),0),
                System.BitConverter.ToUInt32(System.Net.IPAddress.Parse(ip_destination).GetAddressBytes(),0));
        }
        public byte[] encode(string ip_source,string ip_destination,byte[] data)
        {
            this.data=data;
            return this.encode(ip_source,ip_destination);
        }
        public byte[] encode(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order,byte[] data)
        {
            this.data=data;
            return this.encode(ip_source_in_network_order,ip_destination_in_network_order);
        }
        public byte[] encode(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order)
        {
            int data_size=0;
            if (data!=null)
                data_size=this.data.Length;

            byte[] ret=new byte[udp_header.size+data_size];
            System.Array.Copy(System.BitConverter.GetBytes(this.source_port),0,ret,0,2);
            System.Array.Copy(System.BitConverter.GetBytes(this.destination_port),0,ret,2,2);
            // if udp_length not set comput it if this.data
            if (!this.b_udp_length_set)
                this.UdpLength=(ushort)(udp_header.size+data_size);
            System.Array.Copy(System.BitConverter.GetBytes(this.udp_length),0,ret,4,2);
            // if checksum not set put it to 0
            if(!this.b_checksum_set)
                this.checksum=0;
            System.Array.Copy(System.BitConverter.GetBytes(this.checksum),0,ret,6,2);
            // copy data
            if (data_size>0)
                System.Array.Copy(this.data,0,ret,8,data_size);

            if(!this.b_checksum_set)
            {
                // comput checksum
                // create udp_pseudo_header
                udp_pseudo_header udpph=new udp_pseudo_header();
                udpph.destination_address=ip_destination_in_network_order;
                udpph.source_address=ip_source_in_network_order;
                udpph.udp_length=this.udp_length;
                // create a byte array containing udp_pseudo_header and the datagram
                byte[] full_array_for_checksum=new byte[udp_pseudo_header.size+udp_header.size+data_size];
                System.Array.Copy(udpph.encode(),0,full_array_for_checksum,0,udp_pseudo_header.size);
                System.Array.Copy(ret,0,full_array_for_checksum,udp_pseudo_header.size,udp_header.size+data_size);
                // comput checksum
                this.checksum=easy_socket.Cchecksum.checksum(full_array_for_checksum,true);
                // put new check some in udp datagram
                System.Array.Copy(System.BitConverter.GetBytes(this.checksum),0,ret,6,2);
            }
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip_source_in_network_order"></param>
        /// <param name="ip_destination_in_network_order"></param>
        /// <param name="data"></param>
        /// <param name="udp_length">not in network byte order</param>
        /// <returns></returns>
        public byte[] encode(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order,byte[] data,ushort udp_length)
        {
            this.data=data;
            this.UdpLength=udp_length;
            return this.encode(ip_source_in_network_order,ip_destination_in_network_order);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip_source_in_network_order"></param>
        /// <param name="ip_destination_in_network_order"></param>
        /// <param name="udp_length">not in network byte order</param>
        /// <returns></returns>
        public byte[] encode(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order,ushort udp_length)
        {
            this.UdpLength=udp_length;
            return this.encode(ip_source_in_network_order,ip_destination_in_network_order);
        }
        #endregion

        #region decode
        public const byte error_success=0;
        public const byte error_datagram_null=1;
        //public const byte error_datagram_internet_header_length_too_small=2;
        public const byte error_datagram_total_length_too_small=3;
        public const byte error_datagram_not_complete=4;
        public const byte error_datagram_checksum=6;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip_source_in_network_order"></param>
        /// <param name="ip_destination_in_network_order"></param>
        /// <param name="array">array.Length must match the udp_length</param>
        /// <returns></returns>
        public UInt32 decode(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order,byte[] array)
        {
            return this.decode(ip_source_in_network_order, ip_destination_in_network_order,array,false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip_source_in_network_order"></param>
        /// <param name="ip_destination_in_network_order"></param>
        /// <param name="protocol"></param>
        /// <param name="array">array.Length must match the udp_length</param>
        /// <param name="b_check_checksum"></param>
        /// <returns></returns>
        public UInt32 decode(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order,byte[] array,bool b_check_checksum)
        {
            if (array==null)
                return udp_header.error_datagram_null;
            return this.decode(ip_source_in_network_order,ip_destination_in_network_order,array,array.Length,b_check_checksum);
        }
        public UInt32 decode(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order,byte[] array,int array_size,bool b_check_checksum)
        {
            if (array==null)
                return udp_header.error_datagram_null;
            if (array_size<udp_header.size)
                return udp_header.error_datagram_total_length_too_small;

            // decoding header
            this.source_port=System.BitConverter.ToUInt16(array,0);
            this.destination_port=System.BitConverter.ToUInt16(array,2);
            this.udp_length=System.BitConverter.ToUInt16(array,4);
            this.checksum=System.BitConverter.ToUInt16(array,6);

            // getting data
            int data_size=Math.Min(this.UdpLength,array_size-udp_header.size);// decode max info before return
            if (data_size<=0)
            {
                this.data=null;
            }
            else
            {
                this.data=new byte[data_size];
                System.Array.Copy(array,udp_header.size,this.data,0,data_size);
            }
            // better not check checksum
            if (b_check_checksum)
                if (!this.check_checksum(ip_source_in_network_order,ip_destination_in_network_order,ref array,array_size))
                    return udp_header.error_datagram_checksum;

            return udp_header.error_success;
        }
        #endregion

        #region check checksum

        protected bool check_checksum(UInt32 ip_source_in_network_order,UInt32 ip_destination_in_network_order,ref byte[] incoming_udp_data,int incoming_udp_data_size)
        {
            if (incoming_udp_data==null)
                return false;
            if (incoming_udp_data_size<udp_header.size)
                return false;
            UInt16 packet_checksum=System.BitConverter.ToUInt16(incoming_udp_data,6);
            byte[] udp_packet_and_pseudo_header=new byte[incoming_udp_data_size+udp_pseudo_header.size];

            udp_pseudo_header uph=new udp_pseudo_header();
            uph.destination_address=ip_source_in_network_order;
            uph.source_address=ip_destination_in_network_order;
            uph.protocol=easy_socket.ip_header.ipv4_header.protocol_udp;
            uph.UdpLength=(ushort)incoming_udp_data_size;
            byte[] encoded_pseudo_header=uph.encode();
            System.Array.Copy(encoded_pseudo_header,0,udp_packet_and_pseudo_header,0,udp_pseudo_header.size);
            System.Array.Copy(incoming_udp_data,0,udp_packet_and_pseudo_header,udp_pseudo_header.size,incoming_udp_data_size);
            // putting the checksum to 0
            udp_packet_and_pseudo_header[6+udp_pseudo_header.size]=0;
            udp_packet_and_pseudo_header[7+udp_pseudo_header.size]=0;
            UInt16 computed_checksum=easy_socket.Cchecksum.checksum(udp_packet_and_pseudo_header,true);
            return (computed_checksum==packet_checksum);
        }

        #endregion
    }
    public class udp_pseudo_header
    {
        #region members
        /*
        +--------+--------+--------+--------+
        |           Source Address          |
        +--------+--------+--------+--------+
        |         Destination Address       |
        +--------+--------+--------+--------+
        |  zero  |  PTCL  |    UDP Length   |
        +--------+--------+--------+--------+
        */
        public const byte size=12;
        /// <summary>
        /// source_address in network order
        /// </summary>
        public UInt32 source_address;
        /// <summary>
        /// source_address in host order
        /// </summary>
        public string SourceAddress
        {
            set
            {
                this.source_address=System.BitConverter.ToUInt32(System.Net.IPAddress.Parse(value).GetAddressBytes(),0);
            }
            get
            {
                return new System.Net.IPAddress(this.source_address).ToString();
            }
        }
        /// <summary>
        /// destination_address in network order
        /// </summary>
        public UInt32 destination_address;
        /// <summary>
        /// destination_address in host order
        /// </summary>
        public string DestinationAddress
        {
            set
            {
                this.source_address=System.BitConverter.ToUInt32(System.Net.IPAddress.Parse(value).GetAddressBytes(),0);
            }
            get
            {
                return new System.Net.IPAddress(this.destination_address).ToString();
            }
        }
        public byte protocol;
        public byte zero;
        // udp_length in network order
        public ushort udp_length;
        /// <summary>
        /// udp_length in host order
        /// </summary>
        public ushort UdpLength
        {
            get
            {
                return network_convert.switch_ushort(this.udp_length);
            }
            set
            {
                this.udp_length=network_convert.switch_ushort(value);
            }
        }
        #endregion

        #region constructor
        public udp_pseudo_header()
        {
            this.protocol=easy_socket.ip_header.ipv4_header.protocol_udp;
            this.zero=0;
            this.destination_address=0x100007f;//"127.0.0.1"
            this.source_address=0x100007f;//"127.0.0.1"
            this.udp_length=8; // at least udp header length
        }
        #endregion

        #region encode
        public byte[] encode(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order, ushort udplength,bool is_udplength_in_network_order)
        {
            this.source_address=ip_source_in_network_order;
            this.destination_address=ip_destination_in_network_order;
            if (is_udplength_in_network_order)
                this.udp_length=udplength;
            else
                this.UdpLength=udplength;
            return this.encode();
        }
        public byte[] encode(string ip_source, string ip_destination, ushort udplength)
        {
            this.DestinationAddress=ip_source;
            this.SourceAddress=ip_destination;
            this.udp_length=udplength;
            return this.encode();
        }
        public byte[] encode()
        {
            byte[] ret=new byte[udp_pseudo_header.size];
            System.Array.Copy(System.BitConverter.GetBytes(this.source_address),0,ret,0,4);
            System.Array.Copy(System.BitConverter.GetBytes(this.destination_address),0,ret,4,4);
            System.Array.Copy(System.BitConverter.GetBytes(this.zero),0,ret,8,1);
            System.Array.Copy(System.BitConverter.GetBytes(this.protocol),0,ret,9,1);
            System.Array.Copy(System.BitConverter.GetBytes(this.udp_length),0,ret,10,2);
            return ret;
        }
        #endregion
    }
}
