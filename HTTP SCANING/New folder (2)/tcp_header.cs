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

namespace easy_socket.tcp_header
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
    public delegate void Socket_Error_EventHandler(tcp_header sender, EventArgs_Exception e);
    public delegate void Socket_Data_Arrival_EventHandler(tcp_header_server sender, EventArgs_ipv4header_ReceiveData e);

    #endregion

    public class tcp_header_client:tcp_header
    {
        // events
        public event Socket_Error_EventHandler event_Socket_Error;
        public easy_socket.ip_header.ipv4_header_client ipv4headerclt;

        public tcp_header_client()
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
        // Can't be sent without forging (socket throw an error because you must be connected first)
        
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
        public void send(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order,byte[] options,byte[] data)
        {
            this.options=options;
            this.data=data;
            this.send(ip_source_in_network_order,ip_destination_in_network_order);
        }
        
        public void send(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order,ushort tcp_length)
        {
            easy_socket.ip_header.ipv4_header_client iphc=new easy_socket.ip_header.ipv4_header_client();
            iphc.source_address=ip_source_in_network_order;
            iphc.destination_address=ip_destination_in_network_order;
            this.send(iphc,tcp_length);
        }
        public void send(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order,byte[] options,byte[] data,ushort tcp_length)
        {
            this.options=options;
            this.data=data;
            this.send(ip_source_in_network_order,ip_destination_in_network_order,tcp_length);
        }
        
        public void send(string ip_source,string ip_destination)
        {
            easy_socket.ip_header.ipv4_header_client iphc=new easy_socket.ip_header.ipv4_header_client();
            iphc.SourceAddress=ip_source;
            iphc.DestinationAddress=ip_destination;
            this.send(iphc);
        }
        public void send(string ip_source,string ip_destination,ushort tcp_length)
        {
            easy_socket.ip_header.ipv4_header_client iphc=new easy_socket.ip_header.ipv4_header_client();
            iphc.SourceAddress=ip_source;
            iphc.DestinationAddress=ip_destination;
            this.send(iphc,tcp_length);
        }
        public void send(string ip_source,string ip_destination,byte[] data)
        {
            this.data=data;
            this.send(ip_source,ip_destination);
        }
        public void send(string ip_source,string ip_destination,byte[] options,byte[] data)
        {
            this.options=options;
            this.data=data;
            this.send(ip_source,ip_destination);        
        }
        public void send(string ip_source,string ip_destination,byte[] options,byte[] data,ushort tcp_length)
        {
            this.options=options;
            this.data=data;
            this.send(ip_source,ip_destination,tcp_length);
        }
        public void send(easy_socket.ip_header.ipv4_header_client iph_clt,byte[] options,byte[] data,ushort tcp_length)
        {
            this.options=options;
            this.data=data;
            this.send(iph_clt,tcp_length);
        }
        public void send(easy_socket.ip_header.ipv4_header_client iph_clt)
        {
            this.set_ipv4_header_clt(iph_clt);
            this.ipv4headerclt.data=this.encode(this.ipv4headerclt.source_address,this.ipv4headerclt.destination_address);
            this.ipv4headerclt.send(this.DestinationPort);
        }
        public void send(easy_socket.ip_header.ipv4_header_client iph_clt,ushort tcp_length)
        {
            this.set_ipv4_header_clt(iph_clt);
            this.ipv4headerclt.data=this.encode(this.ipv4headerclt.source_address,this.ipv4headerclt.destination_address,this.options,this.data,tcp_length);
            this.ipv4headerclt.send(this.DestinationPort);
        }

        #endregion

        protected void ipv4header_event_Socket_Error(easy_socket.ip_header.ipv4_header sender, easy_socket.ip_header.EventArgs_Exception e)
        {
            if (this.event_Socket_Error!=null)
                this.event_Socket_Error(this,new easy_socket.tcp_header.EventArgs_Exception(e.exception));
        }
    }

    public class tcp_header_server:tcp_header_client
    {
        #region events
        //events
        public event Socket_Data_Arrival_EventHandler event_Data_Arrival;
        #endregion
        protected easy_socket.ip_header.ipv4_header_tcp_server ipv4header_srv;
        public tcp_header_server()
        {
            this.sniff_outgoing_packets=false;
            this.ipv4header_srv=new easy_socket.ip_header.ipv4_header_tcp_server();
            this.ipv4header_srv.event_Socket_Error+=new easy_socket.ip_header.Socket_Error_EventHandler(ipv4header_event_Socket_Error);
            this.ipv4header_srv.event_Socket_Data_Arrival+=new easy_socket.ip_header.Socket_Data_Arrival_EventHandler(socket_data_arrival);
        }
        
        private bool b_check_port;
        private bool b_check_ip;
        private ushort spy_port;
        private UInt32 spy_ip;
        public bool sniff_outgoing_packets;

        #region start
        /// <summary>
        /// start an tcp server. 
        /// you MUST call stop method to avoid remaing runing thread after having close your application.
        /// </summary>
        public void start()
        {
            this.b_check_port=false;
            this.b_check_ip=false;
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
            this.ipv4header_srv.start(local_addr,0,easy_socket.ip_header.ipv4_header_server.protocol_tcp);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="local_port">in host byte order</param>
        public void start(ushort local_port)
        {
            this.b_check_port=true;
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
            this.spy_port=easy_socket.network_convert.switch_ushort(local_port);
            this.ipv4header_srv.start(local_addr,local_port,easy_socket.ip_header.ipv4_header_server.protocol_tcp);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="local_addr"></param>
        /// <param name="local_port">in host byte order</param>
        public void start(string local_addr,ushort local_port)
        {
            
            this.b_check_port=true;
            this.b_check_ip=true;
            this.spy_port=easy_socket.network_convert.switch_ushort(local_port);
            this.spy_ip=(UInt32)System.Net.IPAddress.Parse(local_addr).Address;
            System.Net.IPHostEntry iphe=System.Net.Dns.Resolve(System.Net.Dns.GetHostName());
            if (iphe.AddressList.Length==0)
            {
                System.Windows.Forms.MessageBox.Show("No network adaptater found.\r\nCan't start server","Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            // bind on first network adaptater (allow spoofing)
            this.ipv4header_srv.start(iphe.AddressList[0].ToString(),local_port,easy_socket.ip_header.ipv4_header_server.protocol_tcp);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="local_addr"></param>
        public void start(string local_addr)
        {
            
            this.b_check_port=false;
            this.b_check_ip=true;
            this.spy_ip=(UInt32)System.Net.IPAddress.Parse(local_addr).Address;
            System.Net.IPHostEntry iphe=System.Net.Dns.Resolve(System.Net.Dns.GetHostName());
            if (iphe.AddressList.Length==0)
            {
                System.Windows.Forms.MessageBox.Show("No network adaptater found.\r\nCan't start server","Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            // bind on first network adaptater (allow spoofing)
            this.ipv4header_srv.start(iphe.AddressList[0].ToString(),0,easy_socket.ip_header.ipv4_header_server.protocol_tcp);
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
            if (this.decode(sender.source_address,sender.destination_address,sender.data)!=easy_socket.tcp_header.tcp_header.error_success)
                // here decoding error can be catched
                return;
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
            // send data arrival event
            if (this.event_Data_Arrival!=null)
                this.event_Data_Arrival(this,new EventArgs_ipv4header_ReceiveData(sender,e.buffer));
        }
        #endregion    
    }

    
    public class tcp_header
    {
        #region members (rfc 793)
        /*
            0                   1                   2                   3   
            0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 
           +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
           |          Source Port          |       Destination Port        |
           +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
           |                        Sequence Number                        |
           +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
           |                    Acknowledgment Number                      |
           +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
           |  Data |           |U|A|P|R|S|F|                               |
           | Offset| Reserved  |R|C|S|S|Y|I|            Window             |
           |       |           |G|K|H|T|N|N|                               |
           +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
           |           Checksum            |         Urgent Pointer        |
           +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
           |                    Options                    |    Padding    |
           +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
           |                             data                              |
           +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
        */
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
        /// sequence_number in network order
        /// </summary>
        public UInt32 sequence_number;
        /// <summary>
        /// sequence_number in host order
        /// </summary>
        public UInt32 SequenceNumber
        {
            get
            {
                return network_convert.switch_UInt32(this.sequence_number);
            }
            set
            {
                this.sequence_number=network_convert.switch_UInt32(value);
            }
        }
        /// <summary>
        /// acknowledgment_number in network order 
        /// </summary>
        public UInt32 acknowledgment_number;
        /// <summary>
        /// acknowledgment_number in host order
        /// </summary>
        public UInt32 AcknowledgmentNumber
        {
            get
            {
                return network_convert.switch_UInt32(this.acknowledgment_number);
            }
            set
            {
                this.acknowledgment_number=network_convert.switch_UInt32(value);
            }
        }
        // flag used during encoding if DataOffset is specified, it will not be computed at encoding
        protected bool b_data_offset_set;
        protected byte data_offset;
        /// <summary>
        /// if specified, it will not be computed at encoding
        /// </summary>
        public byte DataOffset
        {
            get
            {
                return this.data_offset;
            }
            set
            {
                this.data_offset=(byte)(value&0xF);
                this.b_data_offset_set=true;
            }
        }
        public byte reserved;
        /// <summary>
        /// URG:  Urgent Pointer field significant 
        /// </summary>
        public bool URG;
        /// <summary>
        /// ACK:  Acknowledgment field significant
        /// </summary>
        public bool ACK;
        /// <summary>
        /// PSH:  Push Function
        /// </summary>
        public bool PSH;
        /// <summary>
        /// RST:  Reset the connection
        /// </summary>
        public bool RST;
        /// <summary>
        /// SYN:  Synchronize sequence numbers
        /// </summary>
        public bool SYN;
        /// <summary>
        /// FIN:  No more data from sender
        /// </summary>
        public bool FIN;
    
        /// <summary>
        /// window in network order
        /// </summary>
        public ushort window;
        /// <summary>
        /// window in host order 
        /// </summary>
        public ushort Window
        {
            get
            {
                return network_convert.switch_ushort(this.window);
            }
            set
            {
                this.window=network_convert.switch_ushort(value);
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
        /// <summary>
        /// urgent_pointer in network order 
        /// </summary>
        public ushort urgent_pointer;
        /// <summary>
        /// urgent_pointer in host order 
        /// </summary>
        public ushort UrgentPointer
        {
            get
            {
                return network_convert.switch_ushort(this.urgent_pointer);
            }
            set
            {
                this.urgent_pointer=network_convert.switch_ushort(value);
            }
        }
        public byte[] options;
        public byte[] data;
        protected byte[] padding;

        public tcp_pseudo_header tcp_p_header;
        #endregion

        #region constructor
        public tcp_header()
        {
            this.SourcePort=80;
            this.DestinationPort=80;
            this.sequence_number=0;
            this.acknowledgment_number=0;
            this.DataOffset=0;
            this.reserved=0;
            this.URG=false;
            this.ACK=false;
            this.PSH=false;
            this.RST=false;
            this.SYN=false;
            this.FIN=false;
            this.Window=1024;
            this.checksum=0;
            this.b_data_offset_set=false;
            this.b_checksum_set=false;
            this.urgent_pointer=0;
            this.options=null;
            this.padding=null;
            this.data=null;
            this.tcp_p_header=new tcp_pseudo_header();
        }
        #endregion

        #region encode
        public byte[] encode(string ip_source,string ip_destination)
        {
            return this.encode((UInt32)System.Net.IPAddress.Parse(ip_source).Address,
                (UInt32)System.Net.IPAddress.Parse(ip_destination).Address);
        }
        public byte[] encode(string ip_source,string ip_destination,byte[] data)
        {
            this.data=data;
            return this.encode(ip_source,ip_destination);
        }
        public byte[] encode(string ip_source,string ip_destination,byte[] options,byte[] data)
        {
            this.options=options;
            this.data=data;
            return this.encode(ip_source,ip_destination);
        }
        public byte[] encode(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order,byte[] options,byte[] data)
        {
            this.options=options;
            this.data=data;
            return this.encode(ip_source_in_network_order,ip_destination_in_network_order);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip_source_in_network_order"></param>
        /// <param name="ip_destination_in_network_order"></param>
        /// <param name="options"></param>
        /// <param name="data"></param>
        /// <param name="tcp_length">not in network byte order</param>
        /// <returns></returns>
        public byte[] encode(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order,byte[] options,byte[] data,ushort tcp_length)
        {
            this.options=options;
            this.data=data;
            return this.encode(ip_source_in_network_order,ip_destination_in_network_order,tcp_length);
        }
        public byte[] encode(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order,byte[] data)
        {
            this.data=data;
            return this.encode(ip_source_in_network_order,ip_destination_in_network_order);
        }
        public byte[] encode(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order)
        {
            
            int data_length=0;
            if (this.data!=null) 
                data_length=this.data.Length;
            int options_length=0;
            int padding_length=0;
            if (this.options!=null)
            {
                options_length=this.options.Length;
                padding_length=this.options.Length%4;
            }
            ushort tcp_length=(ushort)(20+options_length+padding_length+data_length);
            return this.encode(ip_source_in_network_order,ip_destination_in_network_order,tcp_length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip_source_in_network_order"></param>
        /// <param name="ip_destination_in_network_order"></param>
        /// <param name="tcp_length">not in network byte order</param>
        /// <returns></returns>
        public byte[] encode(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order,ushort tcp_length)
        {
            byte[] ret;
            int options_length=0;
            int padding_length=0;
            int data_length=0;
            if (this.options!=null)
            {
                options_length=this.options.Length;
                padding_length=this.options.Length%4;
            }
            if (this.data!=null)
            {
                data_length=this.data.Length;
            }
            ret=new byte[20+options_length+padding_length+data_length];
            System.Array.Copy(System.BitConverter.GetBytes(this.source_port),0,ret,0,2);
            System.Array.Copy(System.BitConverter.GetBytes(this.destination_port),0,ret,2,2);
            System.Array.Copy(System.BitConverter.GetBytes(this.sequence_number),0,ret,4,4);
            System.Array.Copy(System.BitConverter.GetBytes(this.acknowledgment_number),0,ret,8,4);
            ushort us;
            us=0;
            if (this.FIN)
                us+=1;
            if (this.SYN)
                us+=2;
            if (this.RST)
                us+=4;
            if (this.PSH)
                us+=8;
            if (this.ACK)
                us+=16;
            if (this.URG)
                us+=32;
            us+=(ushort)(this.reserved<<6);
            if     (!this.b_data_offset_set)
            {
                // comput header length
                this.data_offset=(byte)((20+options_length+padding_length)/4);
            }
            us+=(ushort)(this.data_offset<<12);
            us=easy_socket.network_convert.switch_ushort(us);
            System.Array.Copy(System.BitConverter.GetBytes(us),0,ret,12,2);
            System.Array.Copy(System.BitConverter.GetBytes(this.window),0,ret,14,2);
            if (!this.b_checksum_set)
                this.checksum=0;
            System.Array.Copy(System.BitConverter.GetBytes(this.checksum),0,ret,16,2);
            System.Array.Copy(System.BitConverter.GetBytes(this.urgent_pointer),0,ret,18,2);

            if (this.options!=null)
            {
                System.Array.Copy(this.options,0,ret,20,options_length);
                if (padding_length!=0)
                {
                    this.padding=new byte[padding_length];
                    System.Array.Copy(this.padding,0,ret,20+options_length,padding_length);
                }
            }
            if (this.data!=null)
            {
                System.Array.Copy(this.data,0,ret,20+options_length+padding_length,data_length);
            }
            // comput checksum
            if (!this.b_checksum_set)
            {
                // get pseudo header
                byte[] barray_pseudo_header=this.tcp_p_header.encode(ip_source_in_network_order,
                    ip_destination_in_network_order,
                    tcp_length,
                    false);
                byte[] full_array=new byte[tcp_pseudo_header.size+20+options_length+padding_length+data_length];
                System.Array.Copy(barray_pseudo_header,0,full_array,0,tcp_pseudo_header.size);
                System.Array.Copy(ret,0,full_array,tcp_pseudo_header.size,20+options_length+padding_length+data_length);
                this.checksum=easy_socket.Cchecksum.checksum(full_array,true);
                System.Array.Copy(System.BitConverter.GetBytes(this.checksum),0,ret,16,2);
            }
            return ret;
        }
        #endregion

        #region decode
        public const byte error_success=0;
        public const byte error_datagram_null=1;
        //public const byte error_datagram_internet_header_length_too_small=2;
        public const byte error_datagram_total_length_too_small=3;
        public const byte error_datagram_not_complete=4;
        public const byte error_datagram_checksum=6;
        public UInt32 decode(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order,byte[] array)
        {

            return this.decode(ip_source_in_network_order, ip_destination_in_network_order,array,false);
        }
        public UInt32 decode(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order,byte[] array,bool b_check_checksum)
        {
            if (array==null)
                return tcp_header.error_datagram_null;
            return this.decode(ip_source_in_network_order, ip_destination_in_network_order,array,array.Length,b_check_checksum);
        }
        public UInt32 decode(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order,byte[] array,int array_size,bool b_check_checksum)
        {
            if (array==null)
                return tcp_header.error_datagram_null;
            if (array_size<20)
                return tcp_header.error_datagram_total_length_too_small;
            this.source_port=System.BitConverter.ToUInt16(array,0);
            this.destination_port=System.BitConverter.ToUInt16(array,2);
            this.sequence_number=System.BitConverter.ToUInt32(array,4);
            this.acknowledgment_number=System.BitConverter.ToUInt32(array,8);
            ushort us=System.BitConverter.ToUInt16(array,12);
            us=easy_socket.network_convert.switch_ushort(us);
            this.data_offset=(byte)(us>>12);
            this.reserved=(byte)((us>>6)&0x3f);
            this.URG=(((us>>5)&0x1)==1)?true:false;
            this.ACK=(((us>>4)&0x1)==1)?true:false;
            this.PSH=(((us>>3)&0x1)==1)?true:false;
            this.RST=(((us>>2)&0x1)==1)?true:false;
            this.SYN=(((us>>1)&0x1)==1)?true:false;
            this.FIN=((us&0x1)==1)?true:false;
            this.window=System.BitConverter.ToUInt16(array,14);
            this.checksum=System.BitConverter.ToUInt16(array,16);
            this.urgent_pointer=System.BitConverter.ToUInt16(array,18);
            // put options and padding in the option array (it should be decoded not implemented yet)
            this.options=null;
            this.padding=null;
            int options_length=0;
            if (this.DataOffset>5)
            {
                if (this.DataOffset*4>array_size)
                    return tcp_header.error_datagram_total_length_too_small;
                options_length=this.DataOffset*4-20;
                this.options=new byte[options_length];
                System.Array.Copy(array,20,this.options,0,options_length);
            }
            else
            {
                this.options=null;
            }
            // get tcp data
            int data_size=array_size-options_length-20;
            if (data_size<=0)
            {
                this.data=null;
            }
            else
            {
                this.data=new byte[data_size];
                System.Array.Copy(array,20+options_length,this.data,0,data_size);
            }

            // checksum better not be checked (too much packets can have a bad checksum and rfc793 is a bit old :) )
            if (b_check_checksum)
                if (!this.check_checksum(ip_source_in_network_order,ip_destination_in_network_order,ref array,array_size))
                    return tcp_header.error_datagram_checksum;
            return tcp_header.error_success;
        }
        #endregion

        #region check checksum

        protected bool check_checksum(UInt32 ip_source_in_network_order,UInt32 ip_destination_in_network_order,ref byte[] incoming_tcp_data,int incoming_tcp_data_size)
        {
            if (incoming_tcp_data==null)
                return false;
            if (incoming_tcp_data_size<20)
                return false;
            UInt16 packet_checksum=System.BitConverter.ToUInt16(incoming_tcp_data,16);
            byte[] tcp_packet_and_pseudo_header=new byte[incoming_tcp_data_size+tcp_pseudo_header.size];
            UInt16 computed_checksum;
            tcp_pseudo_header tph=new tcp_pseudo_header();

            tph.protocol=easy_socket.ip_header.ipv4_header.protocol_tcp;
            tph.TcpLength=(ushort)incoming_tcp_data_size;
            // don't care the order of ip_dest or ip src checksum is the same
            tph.destination_address=ip_destination_in_network_order;
            tph.source_address=ip_source_in_network_order;
            byte[] encoded_pseudo_header=tph.encode();
            System.Array.Copy(encoded_pseudo_header,0,tcp_packet_and_pseudo_header,0,tcp_pseudo_header.size);
            System.Array.Copy(incoming_tcp_data,0,tcp_packet_and_pseudo_header,tcp_pseudo_header.size,incoming_tcp_data_size);
            // putting the checksum to 0
            tcp_packet_and_pseudo_header[16+tcp_pseudo_header.size]=0;
            tcp_packet_and_pseudo_header[17+tcp_pseudo_header.size]=0;

            computed_checksum=easy_socket.Cchecksum.checksum(tcp_packet_and_pseudo_header,true);

            return (computed_checksum==packet_checksum);
        }

        #endregion
    }
    public class tcp_pseudo_header
    {
        #region members
        /*
        +--------+--------+--------+--------+
        |           Source Address          |
        +--------+--------+--------+--------+
        |         Destination Address       |
        +--------+--------+--------+--------+
        |  zero  |  PTCL  |    TCP Length   |
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
                this.source_address=(UInt32)System.Net.IPAddress.Parse(value).Address;
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
                this.source_address=(UInt32)System.Net.IPAddress.Parse(value).Address;
            }
            get
            {
                return new System.Net.IPAddress(this.destination_address).ToString();
            }
        }
        public byte protocol;
        public byte zero;
        // tcp_length in network order
        public ushort tcp_length;
        /// <summary>
        /// tcp_length in host order
        /// </summary>
        public ushort TcpLength
        {
            get
            {
                return network_convert.switch_ushort(this.tcp_length);
            }
            set
            {
                this.tcp_length=network_convert.switch_ushort(value);
            }
        }
        #endregion

        #region constructor
        public tcp_pseudo_header()
        {
            this.protocol=easy_socket.ip_header.ipv4_header.protocol_tcp;
            this.zero=0;
            this.destination_address=0x100007f;//"127.0.0.1"
            this.source_address=0x100007f;//"127.0.0.1"
            this.tcp_length=20; // at least tcp header length
        }
        #endregion

        #region encode
        public byte[] encode(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order, ushort tcplength,bool is_tcplength_in_network_order)
        {
            this.source_address=ip_source_in_network_order;
            this.destination_address=ip_destination_in_network_order;
            if (is_tcplength_in_network_order)
                this.tcp_length=tcplength;
            else
                this.TcpLength=tcplength;
            return this.encode();
        }
        public byte[] encode(string ip_source, string ip_destination, ushort tcplength)
        {
            this.DestinationAddress=ip_source;
            this.SourceAddress=ip_destination;
            this.tcp_length=tcplength;
            return this.encode();
        }
        public byte[] encode()
        {
            byte[] ret=new byte[tcp_pseudo_header.size];
            System.Array.Copy(System.BitConverter.GetBytes(this.source_address),0,ret,0,4);
            System.Array.Copy(System.BitConverter.GetBytes(this.destination_address),0,ret,4,4);
            System.Array.Copy(System.BitConverter.GetBytes(this.zero),0,ret,8,1);
            System.Array.Copy(System.BitConverter.GetBytes(this.protocol),0,ret,9,1);
            System.Array.Copy(System.BitConverter.GetBytes(this.tcp_length),0,ret,10,2);
            return ret;
        }
        #endregion
    }

    public class tcp_reply_header:tcp_header_client
    {
        #region members
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
                this.source_address=(UInt32)System.Net.IPAddress.Parse(value).Address;
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
                this.source_address=(UInt32)System.Net.IPAddress.Parse(value).Address;
            }
            get
            {
                return new System.Net.IPAddress(this.destination_address).ToString();
            }
        }
        public tcp_header tcp_header_received;
        #endregion

        #region constructors
        public tcp_reply_header()
        {

        }
        #endregion

        #region receive
        /// <summary>
        /// fill and comput source_port, destination_port, sequence_number, acknoledgement_number (tcp_header members)
        /// fill source_address, destination_address (tcp_reply_header members)
        /// </summary>
        /// <param name="ip_source_in_network_order"></param>
        /// <param name="ip_destination_in_network_order"></param>
        /// <param name="incoming_tcp_header"></param>
        public void receive(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order,tcp_header incoming_tcp_header)
        {
            // switch ip source and ip destination
            this.source_address=ip_destination_in_network_order;
            this.destination_address=ip_source_in_network_order;
            this.tcp_header_received=incoming_tcp_header;

            // switch source port and destination port
            this.source_port=this.tcp_header_received.destination_port;
            this.destination_port=this.tcp_header_received.source_port;

            // comput sequence and acknolegment number
            if (this.tcp_header_received.SYN||this.tcp_header_received.FIN)
                this.AcknowledgmentNumber=(this.tcp_header_received.SequenceNumber+1)%UInt32.MaxValue;
            else
            {
                int data_length=0;
                if (this.tcp_header_received.data!=null)
                    data_length=this.tcp_header_received.data.Length;
                this.AcknowledgmentNumber=(UInt32)(this.tcp_header_received.SequenceNumber+data_length)%UInt32.MaxValue;
            }
            this.sequence_number=this.tcp_header_received.acknowledgment_number;
        }
        #endregion
    }
    /// <summary>
    /// This class is only made to fill ACK and SYN flags (and RST if error).
    /// no data check is done.
    /// order of SequenceNumber and AcknoledgmentNumber is not check 
    /// (as SequenceNumber or AcknoledgmentNumber lacks).
    /// It just help to go from a connection state to another.
    /// Througth tcp_reply_header it fill and comput source_port, destination_port, sequence_number, acknoledgement_number (tcp_header members)
    /// and source_address, destination_address (tcp_reply_header members) of the reply packet.
    /// </summary>
    public class tcp_connection
    {
        #region const
        public const byte STATE_CLOSED = 1;
        public const byte STATE_LISTEN = 2;
        public const byte STATE_SYN_SENT = 3;
        public const byte STATE_SYN_RCVD = 4;
        public const byte STATE_ESTAB = 5;
        public const byte STATE_FIN_WAIT1 = 6;
        public const byte STATE_FIN_WAIT2 = 7;
        public const byte STATE_CLOSE_WAIT = 8;
        public const byte STATE_CLOSING = 9;
        public const byte STATE_LAST_ACK = 10;
        public const byte STATE_TIME_WAIT = 11;
        public const byte STATE_DELETE_TCB = 12;
        #endregion

        #region members
        private bool b_remote_host_wait_reply;
        /// <summary>
        /// specify if remote host needs a reply to respect tcp protocol
        /// </summary>
        public bool bRemoteHostWaitReply
        {
            get 
            {
                return this.b_remote_host_wait_reply;
            }
        }
        /// <summary>
        /// header received
        /// </summary>
        public tcp_header tcph_incoming;
        /// <summary>
        /// tcp header reply (filled after a call to receive)
        /// </summary>
        public tcp_reply_header tcp_replyh;
        public byte state;
        public string State
        {
            get
            {
                switch(this.state)
                {
                    
                    case STATE_CLOSED:
                        return "Closed";
                    case STATE_LISTEN:
                        return "Listen";
                    case STATE_SYN_SENT:
                        return "SYN Sent";
                    case STATE_SYN_RCVD:
                        return "SYN Received";
                    case STATE_ESTAB:
                        return "Estabished";
                    case STATE_FIN_WAIT1:
                        return "FIN Wait 1";
                    case STATE_FIN_WAIT2 :
                        return "FIN Wait 2";
                    case STATE_CLOSE_WAIT:
                        return "Close wait";
                    case STATE_CLOSING:
                        return "Closing";
                    case STATE_LAST_ACK:
                        return "Last ACK";
                    case STATE_TIME_WAIT:
                        return "Time wait";
                    case STATE_DELETE_TCB:
                        return "Delete TCB";
                    default:
                        return "Unknown";
                }
            }
        }

        #endregion

        #region constructors
        public tcp_connection()
        {
            this.tcph_incoming=new tcp_header();
            this.tcp_replyh=new tcp_reply_header();
            this.state=tcp_connection.STATE_CLOSED;
            this.b_remote_host_wait_reply=false;
        }
        #endregion

        #region receive

        public const byte error_unexpected_flag_received=101;
        /// <summary>
        /// fill tcp header reply (tcp_replyh)
        /// </summary>
        /// <param name="ip_source_in_network_order"></param>
        /// <param name="ip_destination_in_network_order"></param>
        /// <param name="tcp_header_and_data"></param>
        /// <returns>error status</returns>
        public UInt32 receive(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order,byte[] tcp_header_and_data)
        {
            UInt32 ret;
            ret=this.tcph_incoming.decode(ip_source_in_network_order, ip_destination_in_network_order,tcp_header_and_data);
            if (ret!=tcp_header.error_success)
                return ret;
            return this.receive(ip_source_in_network_order, ip_destination_in_network_order);
        }
        /// <summary>
        /// fill tcp header reply (tcp_replyh)
        /// </summary>
        /// <param name="ip_source_in_network_order"></param>
        /// <param name="ip_destination_in_network_order"></param>
        /// <param name="tcp_header"></param>
        /// <returns></returns>
        public UInt32 receive(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order,tcp_header tcp_header)
        {
            this.tcph_incoming=tcp_header;
            return this.receive(ip_source_in_network_order, ip_destination_in_network_order);
        }
        /// <summary>
        /// fill tcp header reply (tcp_replyh)
        /// </summary>
        /// <param name="ip_source_in_network_order"></param>
        /// <param name="ip_destination_in_network_order"></param>
        /// <returns></returns>
        public UInt32 receive(UInt32 ip_source_in_network_order, UInt32 ip_destination_in_network_order)
        {
            UInt32 ret=tcp_header.error_success;
            this.tcp_replyh.receive(ip_source_in_network_order, ip_destination_in_network_order,this.tcph_incoming);

            #region connection diagram
            /*
                                          +---------+ ---------\      active OPEN  
                                          |  CLOSED |            \    -----------  
                                          +---------+<---------\   \   create TCB  
                                            |     ^              \   \  snd SYN    
                               passive OPEN |     |   CLOSE        \   \           
                               ------------ |     | ----------       \   \         
                                create TCB  |     | delete TCB         \   \       
                                            V     |                      \   \     
                                          +---------+            CLOSE    |    \   
                                          |  LISTEN |          ---------- |     |  
                                          +---------+          delete TCB |     |  
                               rcv SYN      |     |     SEND              |     |  
                              -----------   |     |    -------            |     V  
             +---------+      snd SYN,ACK  /       \   snd SYN          +---------+
             |         |<-----------------           ------------------>|         |
             |   SYN   |                    rcv SYN                     |   SYN   |
             |   RCVD  |<-----------------------------------------------|   SENT  |
             |         |                    snd ACK                     |         |
             |         |------------------           -------------------|         |
             +---------+   rcv ACK of SYN  \       /  rcv SYN,ACK       +---------+
               |           --------------   |     |   -----------                  
               |                  x         |     |     snd ACK                    
               |                            V     V                                
               |  CLOSE                   +---------+                              
               | -------                  |  ESTAB  |                              
               | snd FIN                  +---------+                              
               |                   CLOSE    |     |    rcv FIN                     
               V                  -------   |     |    -------                     
             +---------+          snd FIN  /       \   snd ACK          +---------+
             |  FIN    |<-----------------           ------------------>|  CLOSE  |
             | WAIT-1  |------------------                              |   WAIT  |
             +---------+          rcv FIN  \                            +---------+
               | rcv ACK of FIN   -------   |                            CLOSE  |  
               | --------------   snd ACK   |                           ------- |  
               V        x                   V                           snd FIN V  
             +---------+                  +---------+                   +---------+
             |FINWAIT-2|                  | CLOSING |                   | LAST-ACK|
             +---------+                  +---------+                   +---------+
               |                rcv ACK of FIN |                 rcv ACK of FIN |  
               |  rcv FIN       -------------- |    Timeout=2MSL -------------- |  
               |  -------              x       V    ------------        x       V  
                \ snd ACK                 +---------+delete TCB         +---------+
                 ------------------------>|TIME WAIT|------------------>| CLOSED  |
                                          +---------+                   +---------+

                                  TCP Connection State Diagram (rfc793)
            */
            #endregion

            this.b_remote_host_wait_reply=true;
            // put flags in their default state
            this.tcp_replyh.URG=false;
            this.tcp_replyh.ACK=false;
            this.tcp_replyh.PSH=false;
            this.tcp_replyh.RST=false;
            this.tcp_replyh.SYN=false;
            this.tcp_replyh.FIN=false;
            if (this.tcph_incoming.RST)
            {
                this.b_remote_host_wait_reply=false;
                this.state=tcp_connection.STATE_CLOSED;
                return tcp_header.error_success;
            }
            if (this.tcph_incoming.FIN)
            {
                this.tcp_replyh.ACK=true;
                this.state=tcp_connection.STATE_CLOSE_WAIT;
                return tcp_header.error_success;
            }
            ret=tcp_header.error_success;
            switch (this.state)
            {
                case tcp_connection.STATE_CLOSED:
                case tcp_connection.STATE_DELETE_TCB:
                    this.state=tcp_connection.STATE_CLOSED;
                    break;
                case tcp_connection.STATE_LISTEN:
                    if (this.tcph_incoming.SYN)
                    {
                        this.tcp_replyh.SYN=true;
                        this.tcp_replyh.ACK=true;
                        this.state=tcp_connection.STATE_SYN_RCVD;
                    }
                    ret=error_unexpected_flag_received;
                    // error close connection
                    this.close_connection();
                    break;
                case tcp_connection.STATE_SYN_RCVD:
                    if (this.tcph_incoming.SYN && this.tcph_incoming.ACK)
                    {
                        ret=error_unexpected_flag_received;
                        // error close connection
                        this.close_connection();
                        break;
                    }
                    if (this.tcph_incoming.SYN)
                    {
                        this.tcp_replyh.SYN=true;
                        this.tcp_replyh.ACK=true;
                        break;
                    }
                    if (this.tcph_incoming.ACK)
                    {
                        this.tcp_replyh.ACK=true;
                        this.state=tcp_connection.STATE_ESTAB;
                        break;
                    }
                    ret=error_unexpected_flag_received;
                    // error close connection
                    this.close_connection();
                    break;
                case tcp_connection.STATE_SYN_SENT:
                    if (this.tcph_incoming.SYN && this.tcph_incoming.ACK)
                    {
                        this.tcp_replyh.ACK=true;
                        this.state=tcp_connection.STATE_ESTAB;
                        break;
                    }
                    if (this.tcph_incoming.SYN)
                    {
                        this.tcp_replyh.ACK=true;
                        this.state=tcp_connection.STATE_SYN_RCVD;
                        break;
                    }
                    ret=error_unexpected_flag_received;
                    // error close connection
                    this.close_connection();
                    break;
                case tcp_connection.STATE_FIN_WAIT1:
                    if (this.tcph_incoming.ACK)
                    {
                        this.b_remote_host_wait_reply=false;
                        this.state=tcp_connection.STATE_FIN_WAIT2;
                        break;
                    }
                    if (this.tcph_incoming.FIN)
                    {
                        this.tcp_replyh.ACK=true;
                        this.state=tcp_connection.STATE_CLOSING;
                        break;
                    }
                    ret=error_unexpected_flag_received;
                    // error close connection
                    this.close_connection();
                    break;
                case tcp_connection.STATE_CLOSING:
                    if (this.tcph_incoming.ACK)
                    {
                        this.b_remote_host_wait_reply=false;
                        this.state=tcp_connection.STATE_CLOSED;// tcp_connection.STATE_TIME_WAIT
                    }
                    ret=error_unexpected_flag_received;
                    // error close connection
                    this.close_connection();
                    break;
                case tcp_connection.STATE_FIN_WAIT2:
                    if (this.tcph_incoming.FIN)
                    {
                        this.tcp_replyh.ACK=true;
                        this.state=tcp_connection.STATE_CLOSED;//STATE_TIME_WAIT
                        break;
                    }
                    ret=error_unexpected_flag_received;
                    // error close connection
                    this.close_connection();
                    break;
                case tcp_connection.STATE_LAST_ACK:
                    if (this.tcph_incoming.ACK)
                    {
                        this.state=tcp_connection.STATE_CLOSED;
                        this.b_remote_host_wait_reply=false;
                        break;
                    }
                    ret=error_unexpected_flag_received;
                    // error close connection
                    this.close_connection();
                    break;
                case tcp_connection.STATE_ESTAB:
                    if (this.tcph_incoming.FIN)
                    {
                        this.tcp_replyh.ACK=true;
                        this.state=tcp_connection.STATE_CLOSE_WAIT;
                        break;
                    }
                    if (this.tcph_incoming.SYN || this.tcph_incoming.ACK)
                    {
                        this.close_connection();
                    }
                    this.b_remote_host_wait_reply=false;
                    break;
            }
            return ret;
        }
        /// <summary>
        /// allow to have a common way of connection closing when error occurs
        /// </summary>
        private void close_connection()
        {
            this.tcp_replyh.RST=true;
            this.state=tcp_connection.STATE_CLOSED;
        }
        #endregion
    }
}
