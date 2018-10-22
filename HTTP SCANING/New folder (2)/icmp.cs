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

namespace easy_socket.icmp
{
    #region events and events arg
    public class EventArgs_ipv4header_ReceiveData:EventArgs
    {
        public readonly easy_socket.ip_header.ipv4_header ipv4header;
        public EventArgs_ipv4header_ReceiveData(easy_socket.ip_header.ipv4_header ipv4h)
        {
            this.ipv4header=ipv4h;
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
    public delegate void Socket_Error_EventHandler(icmp sender, EventArgs_Exception e);
    public delegate void TimeOut_EventHandler(icmp sender, EventArgs e);//e=null
    public delegate void icmp_echo_reply_Data_Arrival_EventHandler(icmp_echo_reply sender, EventArgs_ipv4header_ReceiveData e);
    public delegate void icmp_destination_unreachable_Data_Arrival_EventHandler(icmp_destination_unreachable sender, EventArgs_ipv4header_ReceiveData e);
    public delegate void icmp_source_quench_Data_Arrival_EventHandler(icmp_source_quench sender, EventArgs_ipv4header_ReceiveData e);
    public delegate void icmp_redirect_Data_Arrival_EventHandler(icmp_redirect sender, EventArgs_ipv4header_ReceiveData e);
    public delegate void icmp_echo_Data_Arrival_EventHandler(icmp_echo sender, EventArgs_ipv4header_ReceiveData e);
    public delegate void icmp_time_exceeded_message_Data_Arrival_EventHandler(icmp_time_exceeded_message sender, EventArgs_ipv4header_ReceiveData e);
    public delegate void icmp_parameter_problem_Data_Arrival_EventHandler(icmp_parameter_problem sender, EventArgs_ipv4header_ReceiveData e);
    public delegate void icmp_timestamp_Data_Arrival_EventHandler(icmp_timestamp sender, EventArgs_ipv4header_ReceiveData e);
    public delegate void icmp_timestamp_reply_Data_Arrival_EventHandler(icmp_timestamp_reply sender, EventArgs_ipv4header_ReceiveData e);
    public delegate void icmp_information_request_Data_Arrival_EventHandler(icmp_information_request sender, EventArgs_ipv4header_ReceiveData e);
    public delegate void icmp_information_reply_Data_Arrival_EventHandler(icmp_information_reply sender, EventArgs_ipv4header_ReceiveData e);
    #endregion

    /// <summary>
    /// Summary description for icmp.
    /// </summary>
    public class icmp
    {
        
        #region members
        protected byte type;
        public byte Type
        {
            get
            {
                return this.type;
            }
        }
        public byte code;
        public bool b_comput_checksum;
        /// <summary>
        /// checksum in network order
        /// </summary>
        public UInt16 checksum;
        /// <summary>
        /// checksum in host order
        /// </summary>
        public UInt16 Checksum
        {
            set
            {
                this.checksum=easy_socket.network_convert.switch_ushort(value);
                this.b_comput_checksum=false;
            }
            get
            {
                return easy_socket.network_convert.switch_ushort(this.checksum);
            }
        }

        public const byte error_success=0;
        public const byte error_datagram_null=1;
        //public const byte error_datagram_internet_header_length_too_small=2;
        //public const byte error_datagram_total_length_too_small=3;
        public const byte error_datagram_not_complete=4;
        public const byte error_datagram_checksum=6;

        public const byte EchoReply                    =0;
        public const byte DestinationUnreachable    =3;
        public const byte SourceQuench                =4;
        public const byte Redirect                    =5;
        public const byte Echo                        =8;
        public const byte TimeExceeded                =11;
        public const byte ParameterProblem            =12;
        public const byte Timestamp                    =13;
        public const byte TimestampReply            =14;
        public const byte InformationRequest        =15;
        public const byte InformationReply            =16;


        #endregion
        public icmp()
        {
            this.code=0;
            this.checksum=0;
            this.b_comput_checksum=true;
        }
        virtual public byte decode(byte[] data){return error_success;}
        virtual public byte[] encode(){return null;}
        public static string[] get_available_codes(){return new string[]{"0"};}
    }

    public class icmp_client:icmp
    {
        // events
        public event Socket_Error_EventHandler event_Socket_Error;

        // protected members
        protected easy_socket.ip_header.ipv4_header_client ipv4header;

        public icmp_client()
        {
            this.ipv4header=new easy_socket.ip_header.ipv4_header_client();
            this.ipv4header.event_Socket_Error+=new easy_socket.ip_header.Socket_Error_EventHandler(socket_error);
        }
        protected void socket_error(easy_socket.ip_header.ipv4_header sender,easy_socket.ip_header.EventArgs_Exception e)
        {
            if (this.event_Socket_Error!=null)
                event_Socket_Error(this,new EventArgs_Exception(e.exception));
        }
        #region send + broadcast
        public void broadcast(byte ttl)
        {
            this.ipv4header.b_allow_broadcast=true;
            this.ipv4header.send_without_forging("255.255.255.255",System.Net.Sockets.ProtocolType.Icmp,ttl,this.encode());
        }
        public void send(string ip)
        {
            this.send(ip,255);
        }
        public void send(string ip,byte ttl)
        {
            if (ip=="255.255.255.255")
                this.ipv4header.b_allow_broadcast=true;
            this.ipv4header.send_without_forging(ip,System.Net.Sockets.ProtocolType.Icmp,ttl,this.encode());
        }
        public void send(easy_socket.ip_header.ipv4_header_client ipv4h)
        {
            // adding icmp data
            ipv4h.data=this.encode();
            // set icmp type
            ipv4h.protocol=easy_socket.ip_header.ipv4_header.protocol_icmp;
            // allow broadcast by default
            ipv4h.b_allow_broadcast=true;
            // send data
            ipv4h.send();
        }
        #endregion

    }
    public class icmp_client_with_identifier_and_sequence_number:icmp_client
    {
        #region members
        /// <summary>
        /// identifier in network order
        /// </summary>
        public UInt16 identifier;
        /// <summary>
        /// identifier in host order
        /// </summary>
        public UInt16 Identifier
        {
            set
            {
                this.identifier=easy_socket.network_convert.switch_ushort(value);
            }
            get
            {
                return easy_socket.network_convert.switch_ushort(this.identifier);
            }
        }
        /// <summary>
        /// sequence_number in network order
        /// </summary>
        public UInt16 sequence_number;
        /// <summary>
        /// sequence_number in host order
        /// </summary>
        public UInt16 SequenceNumber
        {
            set
            {
                this.sequence_number=easy_socket.network_convert.switch_ushort(value);
            }
            get
            {
                return easy_socket.network_convert.switch_ushort(this.sequence_number);
            }
        }
        #endregion
        public icmp_client_with_identifier_and_sequence_number()
        {
            this.identifier=0;
            this.sequence_number=0;
        }
    }
    public class icmp_server:icmp
    {
        #region events
        //events
        public event Socket_Error_EventHandler event_Socket_Error;
        public event TimeOut_EventHandler event_TimeOut;
        public event icmp_echo_reply_Data_Arrival_EventHandler event_icmp_echo_reply_Data_Arrival;
        public event icmp_destination_unreachable_Data_Arrival_EventHandler event_icmp_destination_unreachable_Data_Arrival;
        public event icmp_source_quench_Data_Arrival_EventHandler event_icmp_source_quench_Data_Arrival;
        public event icmp_redirect_Data_Arrival_EventHandler event_icmp_redirect_Data_Arrival;
        public event icmp_echo_Data_Arrival_EventHandler event_icmp_echo_Data_Arrival;
        public event icmp_time_exceeded_message_Data_Arrival_EventHandler event_icmp_time_exceeded_message_Data_Arrival;
        public event icmp_parameter_problem_Data_Arrival_EventHandler event_icmp_parameter_problem_Data_Arrival;
        public event icmp_timestamp_Data_Arrival_EventHandler event_icmp_timestamp_Data_Arrival;
        public event icmp_timestamp_reply_Data_Arrival_EventHandler event_icmp_timestamp_reply_Data_Arrival;
        public event icmp_information_request_Data_Arrival_EventHandler event_icmp_information_request_Data_Arrival;
        public event icmp_information_reply_Data_Arrival_EventHandler event_icmp_information_reply_Data_Arrival;
        #endregion

        // protected members
        protected easy_socket.ip_header.ipv4_header_icmp_server ipv4header;
        protected const UInt16 remote_port=0;// just for creating IPEndPoint 

        protected System.Timers.Timer timer;

        public icmp_server()
        {
            this.ipv4header=new easy_socket.ip_header.ipv4_header_icmp_server();
            this.ipv4header.event_Socket_Error+=new easy_socket.ip_header.Socket_Error_EventHandler(socket_error);
            this.ipv4header.event_Socket_Data_Arrival+=new easy_socket.ip_header.Socket_Data_Arrival_EventHandler(socket_data_arrival);
            this.timer=new System.Timers.Timer();
            this.timer.Elapsed+=new System.Timers.ElapsedEventHandler(timer_event);
            this.timer.AutoReset=false;
        }

        /// <summary>
        /// start an icmp server. icmp server spy all incoming icmp messages.
        /// you MUST call stop method to avoid remaing runing thread after having close your application.
        /// </summary>
        public void start()
        {
            this.ipv4header.start();
        }

        #region timeout
        private void timer_event(object sender,System.Timers.ElapsedEventArgs e)
        {
            if (this.event_TimeOut!=null)
                this.event_TimeOut(this,null);
        }
        /// <summary>
        /// raise event event_TimeOut in "time_out_for_replies_in_ms" ms (if >=0).
        /// event_TimeOut is raised only one time. Call this method again to raise other event_TimeOut.
        /// </summary>
        /// <param name="time_out_for_replies_in_ms"></param>
        public void set_wait_timeout(int time_out_for_replies_in_ms)
        {
            this.clear_wait_timeout();// stop previous timer if there is one
            this.timer.Interval=time_out_for_replies_in_ms;
            this.timer.Start();
        }
        /// <summary>
        /// Cancel the raising of event_TimeOut
        /// </summary>
        public void clear_wait_timeout()
        {
            this.timer.Stop();
        }
        #endregion

        #region stop
        /// <summary>
        /// stop icmp server
        /// </summary>
        public void stop()
        {
            // clear wait_timeout
            this.clear_wait_timeout();
            // close socket
            this.ipv4header.stop();
        }
        #endregion    

        #region events management
        protected void socket_data_arrival(easy_socket.ip_header.ipv4_header sender,easy_socket.ip_header.EventArgs_FullPacket e)
        {
            // get only icmp message
            if (sender.protocol!=easy_socket.ip_header.ipv4_header.protocol_icmp)
                return;
            icmp_client ret;
            switch (sender.data[0])// switch type
            {
                case icmp.EchoReply:
                    ret=new icmp_echo_reply();
                    if (event_icmp_echo_reply_Data_Arrival!=null)
                        if (ret.decode(sender.data)==icmp.error_success)
                            event_icmp_echo_reply_Data_Arrival((icmp_echo_reply)ret,new EventArgs_ipv4header_ReceiveData(sender));
                    break;
                case icmp.DestinationUnreachable:
                    ret=new icmp_destination_unreachable();
                    if (event_icmp_destination_unreachable_Data_Arrival!=null)
                        if (ret.decode(sender.data)==icmp.error_success)
                            event_icmp_destination_unreachable_Data_Arrival((icmp_destination_unreachable)ret,new EventArgs_ipv4header_ReceiveData(sender));
                    break;
                case icmp.SourceQuench:
                    ret=new icmp_source_quench();
                    if (event_icmp_source_quench_Data_Arrival!=null)
                        if (ret.decode(sender.data)==icmp.error_success)
                            event_icmp_source_quench_Data_Arrival((icmp_source_quench)ret,new EventArgs_ipv4header_ReceiveData(sender));
                    break;
                case icmp.Redirect:
                    ret=new icmp_redirect();
                    if (event_icmp_redirect_Data_Arrival!=null)
                        if (ret.decode(sender.data)==icmp.error_success)
                            event_icmp_redirect_Data_Arrival((icmp_redirect)ret,new EventArgs_ipv4header_ReceiveData(sender));
                    break;
                case icmp.Echo:
                    ret=new icmp_echo();
                    if (event_icmp_echo_Data_Arrival!=null)
                        if (ret.decode(sender.data)==icmp.error_success)
                            event_icmp_echo_Data_Arrival((icmp_echo)ret,new EventArgs_ipv4header_ReceiveData(sender));
                    break;
                case icmp.TimeExceeded:
                    ret=new icmp_time_exceeded_message();
                    if (event_icmp_time_exceeded_message_Data_Arrival!=null)
                        if (ret.decode(sender.data)==icmp.error_success)
                            event_icmp_time_exceeded_message_Data_Arrival((icmp_time_exceeded_message)ret,new EventArgs_ipv4header_ReceiveData(sender));
                    break;
                case icmp.ParameterProblem:
                    ret=new icmp_parameter_problem();
                    if (event_icmp_parameter_problem_Data_Arrival!=null)
                        if (ret.decode(sender.data)==icmp.error_success)
                            event_icmp_parameter_problem_Data_Arrival((icmp_parameter_problem)ret,new EventArgs_ipv4header_ReceiveData(sender));
                    break;
                case icmp.Timestamp:
                    ret=new icmp_timestamp();
                    if (event_icmp_timestamp_Data_Arrival!=null)
                        if (ret.decode(sender.data)==icmp.error_success)
                            event_icmp_timestamp_Data_Arrival((icmp_timestamp)ret,new EventArgs_ipv4header_ReceiveData(sender));
                    break;
                case icmp.TimestampReply:
                    ret=new icmp_timestamp_reply();
                    if (event_icmp_timestamp_reply_Data_Arrival!=null)
                        if (ret.decode(sender.data)==icmp.error_success)
                            event_icmp_timestamp_reply_Data_Arrival((icmp_timestamp_reply)ret,new EventArgs_ipv4header_ReceiveData(sender));
                    break;
                case icmp.InformationRequest:
                    ret=new icmp_information_request();
                    if (event_icmp_information_request_Data_Arrival!=null)
                        if (ret.decode(sender.data)==icmp.error_success)
                            event_icmp_information_request_Data_Arrival((icmp_information_request)ret,new EventArgs_ipv4header_ReceiveData(sender));
                    break;
                case icmp.InformationReply:
                    ret=new icmp_information_reply();
                    if (event_icmp_information_reply_Data_Arrival!=null)
                        if (ret.decode(sender.data)==icmp.error_success)
                            event_icmp_information_reply_Data_Arrival((icmp_information_reply)ret,new EventArgs_ipv4header_ReceiveData(sender));
                    break;
                default:
                    break;
            }
            
        }
        protected void socket_error(easy_socket.ip_header.ipv4_header sender,easy_socket.ip_header.EventArgs_Exception e)
        {
            // cancel our time out
            this.clear_wait_timeout();
            if (this.event_Socket_Error!=null)
                event_Socket_Error(this,new EventArgs_Exception(e.exception));
        }
        #endregion    
    }

    #region destination unreachable
    public class icmp_destination_unreachable:icmp_client
    {
        #region members
        //code: 0-->5
        public const byte code_net_unreachable=0;
        public const byte code_host_unreachable=1;
        public const byte code_protocol_unreachable=2;
        public const byte code_port_unreachable=3;
        public const byte code_fragmentation_needed_and_DF_set=4;
        public const byte code_source_route_failed=5;
        /// <summary>
        /// return code message
        /// </summary>
        public string Code
        {
            get
            {
                switch (this.code)
                {
                    case 0:
                        return "net unreachable";
                    case 1:
                        return "host unreachable";
                    case 2:
                        return "protocol unreachable";
                    case 3:
                        return "port unreachable";
                    case 4:
                        return "fragmentation needed and DF set";
                    case 5:
                        return "source route failed";
                    default: 
                        return "Unknown ("+this.code+")";
                }        
            }
        }
        public UInt32 unused;
        public byte[] ih_and_original_dd;
        #endregion

        public icmp_destination_unreachable()
        {
            this.type=3;
            this.unused=0;
            this.code=icmp_destination_unreachable.code_net_unreachable;
        }
        public static string[] get_available_codes()
        {
            return new string[]{"0 = net unreachable",
                                "1 = host unreachable",
                                "2 = protocol unreachable",
                                "3 = port unreachable",
                                "4 = fragmentation needed and DF set",
                                "5 = source route failed"
                               };
        }

        #region encode/decode
        public override byte[] encode()
        {
            int ih_and_original_dd_size=0;
            if (this.ih_and_original_dd!=null)
                ih_and_original_dd_size=this.ih_and_original_dd.Length;
            byte[] ret=new byte[8+ih_and_original_dd_size];

            ret[0]=this.type;
            ret[1]=this.code;
            if (!this.b_comput_checksum)
            {
                System.Array.Copy(System.BitConverter.GetBytes(this.checksum),0,ret,2,2);
            }
            else
            {
                // checksum to 0
                ret[2]=0;
                ret[3]=0;
            }

            // unused (4 bytes)
            System.Array.Copy(System.BitConverter.GetBytes(this.unused),0,ret,4,4);
            if (ih_and_original_dd_size>0)
                System.Array.Copy(this.ih_and_original_dd,0,ret,8,ih_and_original_dd_size);
            if (this.b_comput_checksum)
            {
                // comput checksum
                this.checksum=Cchecksum.checksum(ret,true);
                System.Array.Copy(System.BitConverter.GetBytes(this.checksum),0,ret,2,2);
            }
            return ret;
        }
        public override byte decode(byte[] data)
        {
            if (data==null)
                return icmp.error_datagram_null;
            if (data.Length<8)
                return icmp.error_datagram_not_complete;
            // this.type=data[0]; // already known
            this.code=data[1];
            this.checksum=System.BitConverter.ToUInt16(data,2);
            this.ih_and_original_dd=null;
            int size=data.Length-8;
            if (size>0)
            {
                this.ih_and_original_dd=new byte[size];
                System.Array.Copy(data,8,this.ih_and_original_dd,0,size);
            }
            // verify checksum
            byte b2=data[2];
            byte b3=data[3];
            data[2]=0;
            data[3]=0;
            UInt16 ui_computed_checksum=easy_socket.Cchecksum.checksum(data,true);
            // restore data
            data[2]=b2;
            data[3]=b3;
            if (this.checksum!=ui_computed_checksum)
                return icmp.error_datagram_checksum;

            // no error
            return icmp.error_success;
        }
        #endregion
    }
    #endregion
    #region time exceeded
    public class icmp_time_exceeded_message:icmp_client
    {
        #region members
        public const byte code_time_to_live_exceeded_in_transit=0;
        public const byte code_fragment_reassembly_time_exceeded=1;
        /// <summary>
        /// return code message
        /// </summary>
        public string Code
        {
            get
            {
                switch (this.code)
                {
                    case 0:
                        return "time to live exceeded in transit";
                    case 1:
                        return "fragment reassembly time exceeded";
                    default: 
                        return "Unknown ("+this.code+")";
                }        
            }
        }
        public UInt32 unused;
        public byte[] ih_and_original_dd;
        #endregion

        public icmp_time_exceeded_message()
        {
            this.type=11;
            this.code=icmp_time_exceeded_message.code_time_to_live_exceeded_in_transit;
            this.unused=0;
        }
        public static string[] get_available_codes()
        {
            return new string[]{"0 = time to live exceeded in transit",
                                "1 = fragment reassembly time exceeded"
                               };
        }
        #region encode/decode
        public override byte[] encode()
        {
            int ih_and_original_dd_size=0;
            if (this.ih_and_original_dd!=null)
                ih_and_original_dd_size=this.ih_and_original_dd.Length;
            byte[] ret=new byte[8+ih_and_original_dd_size];

            ret[0]=this.type;
            ret[1]=this.code;
            if (!this.b_comput_checksum)
            {
                System.Array.Copy(System.BitConverter.GetBytes(this.checksum),0,ret,2,2);
            }
            else
            {
                // checksum to 0
                ret[2]=0;
                ret[3]=0;
            }

            // unused
            System.Array.Copy(System.BitConverter.GetBytes(this.unused),0,ret,4,4);
            if (ih_and_original_dd_size>0)
                System.Array.Copy(this.ih_and_original_dd,0,ret,8,ih_and_original_dd_size);
            if (this.b_comput_checksum)
            {
                // comput checksum
                this.checksum=Cchecksum.checksum(ret,true);
                System.Array.Copy(System.BitConverter.GetBytes(this.checksum),0,ret,2,2);
            }
            return ret;
        }
        public override byte decode(byte[] data)
        {
            if (data==null)
                return icmp.error_datagram_null;
            if (data.Length<8)
                return icmp.error_datagram_not_complete;
            // this.type=data[0]; // already known
            this.code=data[1];
            this.checksum=System.BitConverter.ToUInt16(data,2);
            //unused
            this.ih_and_original_dd=null;
            int size=data.Length-8;
            if (size>0)
            {
                this.ih_and_original_dd=new byte[size];
                System.Array.Copy(data,8,this.ih_and_original_dd,0,size);
            }
            // verify checksum
            byte b2=data[2];
            byte b3=data[3];
            data[2]=0;
            data[3]=0;
            UInt16 ui_computed_checksum=easy_socket.Cchecksum.checksum(data,true);
            // restore data
            data[2]=b2;
            data[3]=b3;
            if (this.checksum!=ui_computed_checksum)
                return icmp.error_datagram_checksum;

            // no error
            return icmp.error_success;
        }
        #endregion
    }
    #endregion
    #region parameter problem
    public class icmp_parameter_problem:icmp_client
    {
        #region members
        public byte pointer;
        public UInt32 unused; // 3 bytes only
        public byte[] ih_and_original_dd;
        #endregion
        public icmp_parameter_problem()
        {
            this.type=12;
            this.pointer=0;
            this.unused=0;
        }
        #region encode/decode
        public override byte[] encode()
        {
            int ih_and_original_dd_size=0;
            if (this.ih_and_original_dd!=null)
                ih_and_original_dd_size=this.ih_and_original_dd.Length;
            byte[] ret=new byte[8+ih_and_original_dd_size];

            ret[0]=this.type;
            ret[1]=this.code;
            if (!this.b_comput_checksum)
            {
                System.Array.Copy(System.BitConverter.GetBytes(this.checksum),0,ret,2,2);
            }
            else
            {
                // checksum to 0
                ret[2]=0;
                ret[3]=0;
            }

            ret[4]=this.pointer;
            // unused
            System.Array.Copy(System.BitConverter.GetBytes(this.unused),1,ret,5,3);
            if (ih_and_original_dd_size>0)
                System.Array.Copy(this.ih_and_original_dd,0,ret,8,ih_and_original_dd_size);
            if (this.b_comput_checksum)
            {
                // comput checksum
                this.checksum=Cchecksum.checksum(ret,true);
                System.Array.Copy(System.BitConverter.GetBytes(this.checksum),0,ret,2,2);
            }
            return ret;
        }
        public override byte decode(byte[] data)
        {
            if (data==null)
                return icmp.error_datagram_null;
            if (data.Length<8)
                return icmp.error_datagram_not_complete;
            // this.type=data[0]; // already known
            this.code=data[1];
            this.checksum=System.BitConverter.ToUInt16(data,2);
            this.pointer=data[4];
            this.ih_and_original_dd=null;
            int size=data.Length-8;
            if (size>0)
            {
                this.ih_and_original_dd=new byte[size];
                System.Array.Copy(data,8,this.ih_and_original_dd,0,size);
            }
            // verify checksum
            byte b2=data[2];
            byte b3=data[3];
            data[2]=0;
            data[3]=0;
            UInt16 ui_computed_checksum=easy_socket.Cchecksum.checksum(data,true);
            // restore data
            data[2]=b2;
            data[3]=b3;
            if (this.checksum!=ui_computed_checksum)
                return icmp.error_datagram_checksum;

            // no error
            return icmp.error_success;
        }
        #endregion
    }
    #endregion
    #region source quench
    public class icmp_source_quench:icmp_client
    {
        #region members
        public UInt32 unused;
        public byte[] ih_and_original_dd;
        #endregion
        public icmp_source_quench()
        {
            this.unused=0;
            this.type=4;
        }
        #region encode/decode
        public override byte[] encode()
        {
            int ih_and_original_dd_size=0;
            if (this.ih_and_original_dd!=null)
                ih_and_original_dd_size=this.ih_and_original_dd.Length;
            byte[] ret=new byte[8+ih_and_original_dd_size];

            ret[0]=this.type;
            ret[1]=this.code;
            if (!this.b_comput_checksum)
            {
                System.Array.Copy(System.BitConverter.GetBytes(this.checksum),0,ret,2,2);
            }
            else
            {
                // checksum to 0
                ret[2]=0;
                ret[3]=0;
            }

            // unused
            System.Array.Copy(System.BitConverter.GetBytes(this.unused),0,ret,4,4);
            if (ih_and_original_dd_size>0)
                System.Array.Copy(this.ih_and_original_dd,0,ret,8,ih_and_original_dd_size);
            if (this.b_comput_checksum)
            {
                // comput checksum
                this.checksum=Cchecksum.checksum(ret,true);
                System.Array.Copy(System.BitConverter.GetBytes(this.checksum),0,ret,2,2);
            }
            return ret;
        }
        public override byte decode(byte[] data)
        {
            if (data==null)
                return icmp.error_datagram_null;
            if (data.Length<8)
                return icmp.error_datagram_not_complete;
            // this.type=data[0]; // already known
            this.code=data[1];
            this.checksum=System.BitConverter.ToUInt16(data,2);
            this.ih_and_original_dd=null;
            int size=data.Length-8;
            if (size>0)
            {
                this.ih_and_original_dd=new byte[size];
                System.Array.Copy(data,8,this.ih_and_original_dd,0,size);
            }
            // verify checksum
            byte b2=data[2];
            byte b3=data[3];
            data[2]=0;
            data[3]=0;
            UInt16 ui_computed_checksum=easy_socket.Cchecksum.checksum(data,true);
            // restore data
            data[2]=b2;
            data[3]=b3;
            if (this.checksum!=ui_computed_checksum)
                return icmp.error_datagram_checksum;

            // no error
            return icmp.error_success;
        }
        #endregion
    }
    #endregion
    #region redirect
    public class icmp_redirect:icmp_client
    {
        #region members
        public const byte code_redirect_datagram_for_network=0;
        public const byte code_redirect_datagram_for_host=1;
        public const byte code_redirect_datagram_for_type_of_service_and_network=2;
        public const byte code_redirect_datagram_for_type_of_service_and_host=3;
        /// <summary>
        /// return code message
        /// </summary>
        public string Code
        {
            get
            {
                switch (this.code)
                {
                    case 0:
                        return "for the Network";
                    case 1:
                        return "for the Host";
                    case 2:
                        return "for the Type of Service and Network";
                    case 3:
                        return "for the Type of Service and Host";
                    default: 
                        return "Unknown ("+this.code+")";
                }        
            }
        }
        public UInt32 gateway_internet_address;
        public string GatewayInternetAddress
        {
            set
            {
                try
                {
                    this.gateway_internet_address=(UInt32)System.Net.IPAddress.Parse(value).Address;
                }
                catch
                {
                    this.gateway_internet_address=(UInt32)System.Net.IPAddress.Parse("127.0.0.1").Address;
                }
            }
            get
            {
                return new System.Net.IPAddress(this.gateway_internet_address).ToString();
            }
        }
        public byte[] ih_and_original_dd;
        #endregion
        public icmp_redirect()
        {
            this.code=icmp_redirect.code_redirect_datagram_for_network;
            this.type=5;
            this.gateway_internet_address=0;
        }
        public static string[] get_available_codes()
        {
            return new string[]{"0 = Redirect datagrams for the Network",
                                 "1 = Redirect datagrams for the Host",
                                 "2 = Redirect datagrams for the Type of Service and Network",
                                 "3 = Redirect datagrams for the Type of Service and Host"};

        }

        #region encode/decode
        public override byte[] encode()
        {
            int ih_and_original_dd_size=0;
            if (this.ih_and_original_dd!=null)
                ih_and_original_dd_size=this.ih_and_original_dd.Length;
            byte[] ret=new byte[8+ih_and_original_dd_size];

            ret[0]=this.type;
            ret[1]=this.code;
            if (!this.b_comput_checksum)
            {
                System.Array.Copy(System.BitConverter.GetBytes(this.checksum),0,ret,2,2);
            }
            else
            {
                // checksum to 0
                ret[2]=0;
                ret[3]=0;
            }

            System.Array.Copy(System.BitConverter.GetBytes(this.gateway_internet_address),0,ret,4,4);
            if (ih_and_original_dd_size>0)
                System.Array.Copy(this.ih_and_original_dd,0,ret,8,ih_and_original_dd_size);
            if (this.b_comput_checksum)
            {
                // comput checksum
                this.checksum=Cchecksum.checksum(ret,true);
                System.Array.Copy(System.BitConverter.GetBytes(this.checksum),0,ret,2,2);
            }

            return ret;
        }
        public override byte decode(byte[] data)
        {
            if (data==null)
                return icmp.error_datagram_null;
            if (data.Length<8)
                return icmp.error_datagram_not_complete;
            // this.type=data[0]; // already known
            this.code=data[1];
            this.checksum=System.BitConverter.ToUInt16(data,2);
            this.gateway_internet_address=System.BitConverter.ToUInt32(data,4);
            this.ih_and_original_dd=null;
            int size=data.Length-8;
            if (size>0)
            {
                this.ih_and_original_dd=new byte[size];
                System.Array.Copy(data,8,this.ih_and_original_dd,0,size);
            }
            // verify checksum
            byte b2=data[2];
            byte b3=data[3];
            data[2]=0;
            data[3]=0;
            UInt16 ui_computed_checksum=easy_socket.Cchecksum.checksum(data,true);
            // restore data
            data[2]=b2;
            data[3]=b3;
            if (this.checksum!=ui_computed_checksum)
                return icmp.error_datagram_checksum;

            // no error
            return icmp.error_success;
        }
        #endregion
    }
    #endregion
    #region echo
    public class icmp_echo:icmp_client_with_identifier_and_sequence_number
    {
        #region members
        public byte[] data;
        /// <summary>
        /// set or get data in ASCII format
        /// </summary>
        public string Data
        {
            set
            {
                this.data=System.Text.Encoding.ASCII.GetBytes(value);
            }
            get
            {
                if (this.data==null)
                    return "";
                return System.Text.Encoding.ASCII.GetString(this.data, 0,this.data.Length);
            }
        }
        #endregion
        public icmp_echo()
        {
            this.type=8;
        }
        #region encode/decode
        public override byte[] encode()
        {
            int data_size=0;
            if (this.data!=null)
                data_size=this.data.Length;
            byte[] ret=new byte[8+data_size];

            ret[0]=this.type;
            ret[1]=this.code;
            if (!this.b_comput_checksum)
            {
                System.Array.Copy(System.BitConverter.GetBytes(this.checksum),0,ret,2,2);
            }
            else
            {
                // checksum to 0
                ret[2]=0;
                ret[3]=0;
            }
            System.Array.Copy(System.BitConverter.GetBytes(this.identifier),0,ret,4,2);
            System.Array.Copy(System.BitConverter.GetBytes(this.sequence_number),0,ret,6,2);
            if (data_size>0)
                System.Array.Copy(this.data,0,ret,8,data_size);

            if (this.b_comput_checksum)
            {
                // comput checksum
                this.checksum=Cchecksum.checksum(ret,true);
                System.Array.Copy(System.BitConverter.GetBytes(this.checksum),0,ret,2,2);
            }
            return ret;
        }
        public override byte decode(byte[] data)
        {
            if (data==null)
                return icmp.error_datagram_null;
            if (data.Length<8)
                return icmp.error_datagram_not_complete;
            // this.type=data[0]; // already known
            this.code=data[1];
            this.checksum=System.BitConverter.ToUInt16(data,2);
            this.identifier=System.BitConverter.ToUInt16(data,4);
            this.sequence_number=System.BitConverter.ToUInt16(data,6);

            this.data=null;
            int size=data.Length-8;
            if (size>0)
            {
                this.data=new byte[size];
                System.Array.Copy(data,8,this.data,0,size);
            }
            // verify checksum
            byte b2=data[2];
            byte b3=data[3];
            data[2]=0;
            data[3]=0;
            UInt16 ui_computed_checksum=easy_socket.Cchecksum.checksum(data,true);
            // restore data
            data[2]=b2;
            data[3]=b3;
            if (this.checksum!=ui_computed_checksum)
                return icmp.error_datagram_checksum;

            // no error
            return icmp.error_success;
        }
        #endregion
    }
    public class icmp_echo_reply:icmp_echo
    {
        public icmp_echo_reply()
        {
            this.type=0;
        }
    }
    #endregion
    #region timestamp
    public class icmp_timestamp:icmp_client_with_identifier_and_sequence_number
    {
        #region members
        /// <summary>
        /// originate_timestamp in network order
        /// </summary>
        public UInt32 originate_timestamp;
        /// <summary>
        /// originate_timestamp in host order
        /// </summary>
        public UInt32 OriginateTimestamp
        {
            set
            {
                this.originate_timestamp=easy_socket.network_convert.switch_UInt32(value);
            }
            get
            {
                return easy_socket.network_convert.switch_UInt32(this.originate_timestamp);
            }
        }
        /// <summary>
        /// receive_timestamp in network order
        /// </summary>
        public UInt32 receive_timestamp;
        /// <summary>
        /// receive_timestamp in host order
        /// </summary>
        public UInt32 ReceiveTimestamp
        {
            set
            {
                this.receive_timestamp=easy_socket.network_convert.switch_UInt32(value);
            }
            get
            {
                return easy_socket.network_convert.switch_UInt32(this.receive_timestamp);
            }
        }
        /// <summary>
        /// transmit_timestamp in network order
        /// </summary>
        public UInt32 transmit_timestamp;
        /// <summary>
        /// transmit_timestamp in host order
        /// </summary>
        public UInt32 TransmitTimestamp
        {
            set
            {
                this.transmit_timestamp=easy_socket.network_convert.switch_UInt32(value);
            }
            get
            {
                return easy_socket.network_convert.switch_UInt32(this.transmit_timestamp);
            }
        }
        #endregion
        public icmp_timestamp()
        {
            this.type=13;
            this.originate_timestamp=0;
            this.receive_timestamp=0;
            this.transmit_timestamp=0;
        }
        #region encode/decode
        public override byte[] encode()
        {
            byte[] ret=new byte[20];
            ret[0]=this.type;
            ret[1]=this.code;
            if (!this.b_comput_checksum)
            {
                System.Array.Copy(System.BitConverter.GetBytes(this.checksum),0,ret,2,2);
            }
            else
            {
                // checksum to 0
                ret[2]=0;
                ret[3]=0;
            }


            System.Array.Copy(System.BitConverter.GetBytes(this.identifier),0,ret,4,2);
            System.Array.Copy(System.BitConverter.GetBytes(this.sequence_number),0,ret,6,2);
            System.Array.Copy(System.BitConverter.GetBytes(this.originate_timestamp),0,ret,8,4);
            System.Array.Copy(System.BitConverter.GetBytes(this.receive_timestamp),0,ret,12,4);
            System.Array.Copy(System.BitConverter.GetBytes(this.transmit_timestamp),0,ret,16,4);

            if (this.b_comput_checksum)
            {
                // comput checksum
                this.checksum=Cchecksum.checksum(ret,true);
                System.Array.Copy(System.BitConverter.GetBytes(this.checksum),0,ret,2,2);
            }
            return ret;
        }
        public override byte decode(byte[] data)
        {
            if (data==null)
                return icmp.error_datagram_null;
            if (data.Length<20)
                return icmp.error_datagram_not_complete;
            // this.type=data[0]; // already known
            this.code=data[1];
            this.checksum=System.BitConverter.ToUInt16(data,2);
            int size=data.Length-8;
            this.identifier=System.BitConverter.ToUInt16(data,4);
            this.sequence_number=System.BitConverter.ToUInt16(data,6);

            this.originate_timestamp=System.BitConverter.ToUInt32(data,8);
            this.receive_timestamp=System.BitConverter.ToUInt32(data,12);
            this.transmit_timestamp=System.BitConverter.ToUInt32(data,16);

            // verify checksum
            byte b2=data[2];
            byte b3=data[3];
            data[2]=0;
            data[3]=0;
            UInt16 ui_computed_checksum=easy_socket.Cchecksum.checksum(data,true);
            // restore data
            data[2]=b2;
            data[3]=b3;
            if (this.checksum!=ui_computed_checksum)
                return icmp.error_datagram_checksum;

            // no error
            return icmp.error_success;
        }
        #endregion
    }
    public class icmp_timestamp_reply:icmp_timestamp
    {
        public icmp_timestamp_reply()
        {
            this.type=14;
        }
    }
    #endregion
    #region information
    public class icmp_information_request:icmp_client_with_identifier_and_sequence_number
    {
        public icmp_information_request()
        {
            this.type=15;
        }
        #region encode/decode
        public override byte[] encode()
        {
            byte[] ret=new byte[8];

            ret[0]=this.type;
            ret[1]=this.code;
            if (!this.b_comput_checksum)
            {
                System.Array.Copy(System.BitConverter.GetBytes(this.checksum),0,ret,2,2);
            }
            else
            {
                // checksum to 0
                ret[2]=0;
                ret[3]=0;
            }

            System.Array.Copy(System.BitConverter.GetBytes(this.identifier),0,ret,4,2);
            System.Array.Copy(System.BitConverter.GetBytes(this.sequence_number),0,ret,6,2);

            if (this.b_comput_checksum)
            {
                // comput checksum
                this.checksum=Cchecksum.checksum(ret,true);
                System.Array.Copy(System.BitConverter.GetBytes(this.checksum),0,ret,2,2);
            }
            
            System.Array.Copy(System.BitConverter.GetBytes(this.checksum),0,ret,2,2);
            return ret;
        }
        public override byte decode(byte[] data)
        {
            if (data==null)
                return icmp.error_datagram_null;
            if (data.Length<8)
                return icmp.error_datagram_not_complete;
            // this.type=data[0]; // already known
            this.code=data[1];
            this.checksum=System.BitConverter.ToUInt16(data,2);
            this.identifier=System.BitConverter.ToUInt16(data,4);
            this.sequence_number=System.BitConverter.ToUInt16(data,6);

            // verify checksum
            byte b2=data[2];
            byte b3=data[3];
            data[2]=0;
            data[3]=0;
            UInt16 ui_computed_checksum=easy_socket.Cchecksum.checksum(data,true);
            // restore data
            data[2]=b2;
            data[3]=b3;
            if (this.checksum!=ui_computed_checksum)
                return icmp.error_datagram_checksum;

            // no error
            return icmp.error_success;
        }
        #endregion
    }

    public class icmp_information_reply:icmp_information_request
    {
        public icmp_information_reply()
        {
            this.type=16;
        }
    }
    #endregion

}
