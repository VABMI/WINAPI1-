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

namespace easy_socket
{
    /// <summary>
    /// convert header and packet data into string array (with a view to add them to listview)
    /// </summary>
    public class packet_ToStringArray
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipv4h"></param>
        /// <param name="b_most_important_info_only"></param>
        /// <returns>IP source, IP dest, proto, other infos,data</returns>
        public static string[] ip_raw(ref easy_socket.ip_header.ipv4_header ipv4h,bool b_most_important_info_only)
        {
            bool b_all_info=!b_most_important_info_only;
            string str_global_info="";
            if (b_all_info)
            {
                str_global_info="Version:"+ipv4h.version+", IHL:"+ipv4h.internet_header_length+", ";
                str_global_info+="TOS value:"+ipv4h.type_of_service;

                string str_precedence="";
                switch (ipv4h.Precedence)
                {
                    case 0:
                        str_precedence="Routine";
                        break;
                    case 1:
                        str_precedence="Priority";
                        break;
                    case 2:
                        str_precedence="Immediate";
                        break;
                    case 3:
                        str_precedence="Flash";
                        break;
                    case 4:
                        str_precedence="Flash Override";
                        break;
                    case 5:
                        str_precedence="CRITIC/ECP";
                        break;
                    case 6:
                        str_precedence="Internetwork Control";
                        break;
                    case 7:
                        str_precedence="Network Control";
                        break;
                }
                str_global_info+=", precedence:"+str_precedence+", ";
            }
            if (ipv4h.Delay>0)
                str_global_info+="Low Delay, ";
            if (ipv4h.Throughput>0)
                str_global_info+="High Throughput, ";
            if (ipv4h.Relibility>0)
                str_global_info+="High Relibility, ";
            str_global_info+="Total Length:"+ipv4h.TotalLength+", ";
            str_global_info+="Id:"+ipv4h.Identification+", ";
            if (b_all_info)
            {
                str_global_info+="flags:"+ipv4h.flags+", ";
            }
            if (ipv4h.MayDontFragment>0)
                str_global_info+="Don't Fragment, ";
            else
                str_global_info+="May Fragment, ";
            if (ipv4h.LastMoreFragment>0)
                str_global_info+="More Fragment, ";
            else
                str_global_info+="Last Fragment, ";
            str_global_info+="TTL:"+ipv4h.time_to_live;
            if (b_all_info)
            {
                str_global_info+=" ,Header cheksum:"+ipv4h.HeaderChecksum;
                str_global_info+=" ,Options:"+easy_socket.hexa_convert.byte_to_hexa(ipv4h.options_and_padding);
            }
            // IP source, IP dest, proto, other infos,data
            string[] ret=new string[5]{
                                          ipv4h.SourceAddress,
                                          ipv4h.DestinationAddress,
                                          ipv4h.protocol.ToString(),
                                          str_global_info,
                                          easy_socket.hexa_convert.byte_to_hexa(ipv4h.data)
                                      };
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipv4h"></param>
        /// <param name="tcph"></param>
        /// <param name="b_most_important_info_only"></param>
        /// <returns>ip info-ip data(length=4),source port,destination port,infos,data</returns>
        public static string[] tcp(ref easy_socket.ip_header.ipv4_header ipv4h,ref easy_socket.tcp_header.tcp_header tcph,bool b_most_important_info_only)
        {
            bool b_all_info=!b_most_important_info_only;
            string str_global_info;

            str_global_info="SeqNum:"+tcph.SequenceNumber;
            str_global_info+=", AckNum:"+tcph.AcknowledgmentNumber;
            if (b_all_info)
            {
                str_global_info+=", DataOffset:"+tcph.DataOffset;
                str_global_info+=", Reserved:"+tcph.reserved;
            }
            if (tcph.URG)
                str_global_info+=", URG";
            if (tcph.ACK)
                str_global_info+=", ACK";
            if (tcph.PSH)
                str_global_info+=", PSH";
            if (tcph.RST)
                str_global_info+=", RST";
            if (tcph.SYN)
                str_global_info+=", SYN";
            if (tcph.FIN)
                str_global_info+=", FIN";
            if (str_global_info.StartsWith(", "))
                str_global_info=str_global_info.Substring(2);
            if (b_all_info)
            {
                str_global_info+=", Window:"+tcph.Window;
                str_global_info+=", Checksum:"+tcph.Checksum;
            }
            if (b_all_info||tcph.URG)
            {
                str_global_info+=", UrgPointer:"+tcph.UrgentPointer;
            }
            if (b_all_info)
                str_global_info+=", Options:"+easy_socket.hexa_convert.byte_to_hexa(tcph.options);
            string[] ret=new string[4+4];
            System.Array.Copy(packet_ToStringArray.ip_raw(ref ipv4h,b_most_important_info_only),0,ret,0,4);
            ret[4]=tcph.SourcePort.ToString();
            ret[5]=tcph.DestinationPort.ToString();
            ret[6]=str_global_info;
            ret[7]=easy_socket.hexa_convert.byte_to_hexa(tcph.data);
                
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipv4h"></param>
        /// <param name="udph"></param>
        /// <param name="b_most_important_info_only"></param>
        /// <returns>ip info-ip data(length=4),source port,destination port,infos,data</returns>
        public static string[] udp(ref easy_socket.ip_header.ipv4_header ipv4h,ref easy_socket.udp_header.udp_header udph,bool b_most_important_info_only)
        {
            string str_global_info="Length:"+udph.UdpLength;
            if (!b_most_important_info_only)
                str_global_info+=", Checksum:"+udph.Checksum;
            string[] ret=new string[4+4];
            System.Array.Copy(packet_ToStringArray.ip_raw(ref ipv4h,b_most_important_info_only),0,ret,0,4);
            ret[4]=udph.SourcePort.ToString();
            ret[5]=udph.DestinationPort.ToString();
            ret[6]=str_global_info;
            ret[7]=easy_socket.hexa_convert.byte_to_hexa(udph.data);
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipv4h"></param>
        /// <param name="icmp"></param>
        /// <param name="b_most_important_info_only"></param>
        /// <returns>ip info-ip data(length=4),"","",info,data</returns>
        public static string[] icmp_echo(ref easy_socket.ip_header.ipv4_header ipv4h,ref easy_socket.icmp.icmp_echo icmp,bool b_most_important_info_only)
        {
            string str_global_info;
            str_global_info="Echo";
            if(!b_most_important_info_only)
            {
                str_global_info+=", Type:"+icmp.Type;
                str_global_info+=", Code:"+icmp.code;
                str_global_info+=", Checksum:"+icmp.Checksum;
            }
            str_global_info+=", Identifier:"+icmp.Identifier;
            str_global_info+=", SeqNum:"+icmp.SequenceNumber;

            string[] ret=new string[4+4];
            System.Array.Copy(packet_ToStringArray.ip_raw(ref ipv4h,b_most_important_info_only),0,ret,0,4);
            ret[4]="";
            ret[5]="";
            ret[6]=str_global_info;
            ret[7]=easy_socket.hexa_convert.byte_to_hexa(icmp.data);
            return ret;
        }

        public static string[] icmp_echo_reply(ref easy_socket.ip_header.ipv4_header ipv4h,ref easy_socket.icmp.icmp_echo_reply icmp,bool b_most_important_info_only)
        {
            string str_global_info;
            str_global_info="Echo Reply";
            if(!b_most_important_info_only)
            {
                str_global_info+=", Type:"+icmp.Type;
                str_global_info+=", Code:"+icmp.code;
                str_global_info+=", Checksum:"+icmp.Checksum;
            }
            str_global_info+=", Identifier:"+icmp.Identifier;
            str_global_info+=", SeqNum:"+icmp.SequenceNumber;

            string[] ret=new string[4+4];
            System.Array.Copy(packet_ToStringArray.ip_raw(ref ipv4h,b_most_important_info_only),0,ret,0,4);
            ret[4]="";
            ret[5]="";
            ret[6]=str_global_info;
            ret[7]=easy_socket.hexa_convert.byte_to_hexa(icmp.data);
            return ret;
        }
        public static string[] icmp_destination_unreachable(ref easy_socket.ip_header.ipv4_header ipv4h,ref easy_socket.icmp.icmp_destination_unreachable icmp,bool b_most_important_info_only)
        {
            string str_global_info;
            str_global_info="Destination Unreachable";
            str_global_info+=", Code:"+icmp.Code;
            if(!b_most_important_info_only)
            {
                str_global_info+=", Type:"+icmp.Type;
                str_global_info+=", Checksum:"+icmp.Checksum;
            }

            string[] ret=new string[4+4];
            System.Array.Copy(packet_ToStringArray.ip_raw(ref ipv4h,b_most_important_info_only),0,ret,0,4);
            ret[4]="";
            ret[5]="";
            ret[6]=str_global_info;
            ret[7]=easy_socket.packet_ToStringArray.decode_internet_header_and_64_bits_of_original_datagram(icmp.ih_and_original_dd,b_most_important_info_only);
            return ret;
        }
        public static string[] icmp_information_reply(ref easy_socket.ip_header.ipv4_header ipv4h,ref easy_socket.icmp.icmp_information_reply icmp,bool b_most_important_info_only)
        {
            string str_global_info;
            str_global_info="Information Reply";
            if(!b_most_important_info_only)
            {
                str_global_info+=", Type:"+icmp.Type;
                str_global_info+=", Code:"+icmp.code;
                str_global_info+=", Checksum:"+icmp.Checksum;
            }
            str_global_info+=", Identifier:"+icmp.Identifier;
            str_global_info+=", SeqNum:"+icmp.SequenceNumber;

            string[] ret=new string[4+4];
            System.Array.Copy(packet_ToStringArray.ip_raw(ref ipv4h,b_most_important_info_only),0,ret,0,4);
            ret[4]="";
            ret[5]="";
            ret[6]=str_global_info;
            ret[7]="";
            return ret;
        }
        public static string[] icmp_information_request(ref easy_socket.ip_header.ipv4_header ipv4h,ref easy_socket.icmp.icmp_information_request icmp,bool b_most_important_info_only)
        {
            string str_global_info;
            str_global_info="Information Request";
            if(!b_most_important_info_only)
            {
                str_global_info+=", Type:"+icmp.Type;
                str_global_info+=", Code:"+icmp.code;
                str_global_info+=", Checksum:"+icmp.Checksum;
            }
            str_global_info+=", Identifier:"+icmp.Identifier;
            str_global_info+=", SeqNum:"+icmp.SequenceNumber;

            string[] ret=new string[4+4];
            System.Array.Copy(packet_ToStringArray.ip_raw(ref ipv4h,b_most_important_info_only),0,ret,0,4);
            ret[4]="";
            ret[5]="";
            ret[6]=str_global_info;
            ret[7]="";
            return ret;
        }
        public static string[] icmp_parameter_problem(ref easy_socket.ip_header.ipv4_header ipv4h,ref easy_socket.icmp.icmp_parameter_problem icmp,bool b_most_important_info_only)
        {
            string str_global_info;
            str_global_info="Parameter Problem";
            if(!b_most_important_info_only)
            {
                str_global_info+=", Type:"+icmp.Type;
                str_global_info+=", Code:"+icmp.code;
                str_global_info+=", Checksum:"+icmp.Checksum;
            }
            str_global_info+=", Pointer:"+icmp.pointer;
            string[] ret=new string[4+4];
            System.Array.Copy(packet_ToStringArray.ip_raw(ref ipv4h,b_most_important_info_only),0,ret,0,4);
            ret[4]="";
            ret[5]="";
            ret[6]=str_global_info;
            ret[7]=easy_socket.packet_ToStringArray.decode_internet_header_and_64_bits_of_original_datagram(icmp.ih_and_original_dd,b_most_important_info_only);
            return ret;
        }
        public static string[] icmp_redirect(ref easy_socket.ip_header.ipv4_header ipv4h,ref easy_socket.icmp.icmp_redirect icmp,bool b_most_important_info_only)
        {
            string str_global_info;
            str_global_info="Time Exceeded Message";
            str_global_info+=", Code:"+icmp.Code;
            if(!b_most_important_info_only)
            {
                str_global_info+=", Type:"+icmp.Type;
                str_global_info+=", Checksum:"+icmp.Checksum;
            }
            str_global_info+=", Gateway:"+icmp.GatewayInternetAddress;
            string[] ret=new string[4+4];
            System.Array.Copy(packet_ToStringArray.ip_raw(ref ipv4h,b_most_important_info_only),0,ret,0,4);
            ret[4]="";
            ret[5]="";
            ret[6]=str_global_info;
            ret[7]=easy_socket.packet_ToStringArray.decode_internet_header_and_64_bits_of_original_datagram(icmp.ih_and_original_dd,b_most_important_info_only);
            return ret;
        }
        public static string[] icmp_source_quench(ref easy_socket.ip_header.ipv4_header ipv4h,ref easy_socket.icmp.icmp_source_quench icmp,bool b_most_important_info_only)
        {
            string str_global_info;
            str_global_info="Source Quench";
            if(!b_most_important_info_only)
            {
                str_global_info+=", Type:"+icmp.Type;
                str_global_info+=", Code:"+icmp.code;
                str_global_info+=", Checksum:"+icmp.Checksum;
            }

            string[] ret=new string[4+4];
            System.Array.Copy(packet_ToStringArray.ip_raw(ref ipv4h,b_most_important_info_only),0,ret,0,4);
            ret[4]="";
            ret[5]="";
            ret[6]=str_global_info;
            ret[7]=easy_socket.hexa_convert.byte_to_hexa(icmp.ih_and_original_dd);
            return ret;
        }
        public static string[] icmp_time_exceeded_message(ref easy_socket.ip_header.ipv4_header ipv4h,ref easy_socket.icmp.icmp_time_exceeded_message icmp,bool b_most_important_info_only)
        {
            string str_global_info;
            str_global_info="Time Exceeded Message";
            str_global_info+=", Code:"+icmp.Code;
            if(!b_most_important_info_only)
            {
                str_global_info+=", Type:"+icmp.Type;
                str_global_info+=", Checksum:"+icmp.Checksum;
            }

            string[] ret=new string[5+4];
            System.Array.Copy(packet_ToStringArray.ip_raw(ref ipv4h,b_most_important_info_only),0,ret,0,5);
            ret[5]="";
            ret[6]="";
            ret[7]=str_global_info;
            ret[8]=easy_socket.packet_ToStringArray.decode_internet_header_and_64_bits_of_original_datagram(icmp.ih_and_original_dd,b_most_important_info_only);
            return ret;
        }
        public static string[] icmp_timestamp(ref easy_socket.ip_header.ipv4_header ipv4h,ref easy_socket.icmp.icmp_timestamp icmp,bool b_most_important_info_only)
        {
            string str_global_info;
            str_global_info="Timestamp";
            if(!b_most_important_info_only)
            {
                str_global_info+=", Type:"+icmp.Type;
                str_global_info+=", Code:"+icmp.code;
                str_global_info+=", Checksum:"+icmp.Checksum;
            }
            str_global_info+=", Identifier:"+icmp.Identifier;
            str_global_info+=", SeqNum:"+icmp.SequenceNumber;
            str_global_info+=", Originate:"+icmp.OriginateTimestamp;
            str_global_info+=", Receive:"+icmp.ReceiveTimestamp;
            str_global_info+=", Transmit:"+icmp.TransmitTimestamp;

            string[] ret=new string[4+4];
            System.Array.Copy(packet_ToStringArray.ip_raw(ref ipv4h,b_most_important_info_only),0,ret,0,4);
            ret[4]="";
            ret[5]="";
            ret[6]=str_global_info;
            ret[7]="";
            return ret;
        }
        public static string[] icmp_timestamp_reply(ref easy_socket.ip_header.ipv4_header ipv4h,ref easy_socket.icmp.icmp_timestamp_reply icmp,bool b_most_important_info_only)
        {
            string str_global_info;
            str_global_info="Timestamp Reply";
            if(!b_most_important_info_only)
            {
                str_global_info+=", Type:"+icmp.Type;
                str_global_info+=", Code:"+icmp.code;
                str_global_info+=", Checksum:"+icmp.Checksum;
            }
            str_global_info+=", Identifier:"+icmp.Identifier;
            str_global_info+=", SeqNum:"+icmp.SequenceNumber;
            str_global_info+=", Originate:"+icmp.OriginateTimestamp;
            str_global_info+=", Receive:"+icmp.ReceiveTimestamp;
            str_global_info+=", Transmit:"+icmp.TransmitTimestamp;

            string[] ret=new string[4+4];
            System.Array.Copy(packet_ToStringArray.ip_raw(ref ipv4h,b_most_important_info_only),0,ret,0,4);
            ret[4]="";
            ret[5]="";
            ret[6]=str_global_info;
            ret[7]="";
            return ret;
        }
        /// <summary>
        /// used to get original datagram info on icmp troubles msg. the icmp msg should contain internet header and 64 bits of original datagram
        /// </summary>
        /// <param name="raw_databool"></param>
        /// <param name="b_most_important_info_only"></param>
        /// <returns></returns>
        private static string decode_internet_header_and_64_bits_of_original_datagram(byte[] raw_data,bool b_most_important_info_only)
        {
            if (raw_data==null)
                return "";
            string ret="";
            byte ret_decode;
            // decode 
            easy_socket.ip_header.ipv4_header ipv4h=new easy_socket.ip_header.ipv4_header();
            ret_decode=ipv4h.decode(raw_data);
            if ((ret_decode!=easy_socket.ip_header.ipv4_header.error_success)&&(ret_decode!=easy_socket.ip_header.ipv4_header.error_datagram_not_complete)&&(ret_decode!=easy_socket.ip_header.ipv4_header.error_datagram_length_not_matching))
                return easy_socket.hexa_convert.byte_to_hexa(raw_data);
            string[] data=null;
            // case of no data in original datagram
            if (ipv4h.data==null)
                data=packet_ToStringArray.ip_raw(ref ipv4h,b_most_important_info_only);
            else if (ipv4h.data.Length<1)
                data=packet_ToStringArray.ip_raw(ref ipv4h,b_most_important_info_only);
                // assume there's data in original ip datagram
            else// data !=null && length>1
            {
                switch (ipv4h.protocol)
                {
                        // if icmp protocol
                    case easy_socket.ip_header.ipv4_header.protocol_icmp:
                    switch (ipv4h.data[0])// switch type (see icmp protocol)
                    {
                        case easy_socket.icmp.icmp.EchoReply:
                            easy_socket.icmp.icmp_echo_reply icmper=new easy_socket.icmp.icmp_echo_reply();
                            if (icmper.decode(ipv4h.data)==easy_socket.icmp.icmp.error_success)
                                data=packet_ToStringArray.icmp_echo_reply(ref ipv4h,ref icmper,b_most_important_info_only);
                            break;
                        case easy_socket.icmp.icmp.DestinationUnreachable:
                            easy_socket.icmp.icmp_destination_unreachable icmpdu=new easy_socket.icmp.icmp_destination_unreachable();
                            if (icmpdu.decode(ipv4h.data)==easy_socket.icmp.icmp.error_success)
                                data=packet_ToStringArray.icmp_destination_unreachable(ref ipv4h,ref icmpdu,b_most_important_info_only);
                            break;
                        case easy_socket.icmp.icmp.SourceQuench:
                            easy_socket.icmp.icmp_source_quench icmpsq=new easy_socket.icmp.icmp_source_quench();
                            if (icmpsq.decode(ipv4h.data)==easy_socket.icmp.icmp.error_success)
                                data=packet_ToStringArray.icmp_source_quench(ref ipv4h,ref icmpsq,b_most_important_info_only);
                            break;
                        case easy_socket.icmp.icmp.Redirect:
                            easy_socket.icmp.icmp_redirect icmpr=new easy_socket.icmp.icmp_redirect();
                            if (icmpr.decode(ipv4h.data)==easy_socket.icmp.icmp.error_success)
                                data=packet_ToStringArray.icmp_redirect(ref ipv4h,ref icmpr,b_most_important_info_only);
                            break;
                        case easy_socket.icmp.icmp.Echo:
                            easy_socket.icmp.icmp_echo icmpe=new easy_socket.icmp.icmp_echo();
                            if (icmpe.decode(ipv4h.data)==easy_socket.icmp.icmp.error_success)
                                data=packet_ToStringArray.icmp_echo(ref ipv4h,ref icmpe,b_most_important_info_only);
                            break;
                        case easy_socket.icmp.icmp.TimeExceeded:
                            easy_socket.icmp.icmp_time_exceeded_message icmptem=new easy_socket.icmp.icmp_time_exceeded_message();
                            if (icmptem.decode(ipv4h.data)==easy_socket.icmp.icmp.error_success)
                                data=packet_ToStringArray.icmp_time_exceeded_message(ref ipv4h,ref icmptem,b_most_important_info_only);
                            break;
                        case easy_socket.icmp.icmp.ParameterProblem:
                            easy_socket.icmp.icmp_parameter_problem icmppp=new easy_socket.icmp.icmp_parameter_problem();
                            if (icmppp.decode(ipv4h.data)==easy_socket.icmp.icmp.error_success)
                                data=packet_ToStringArray.icmp_parameter_problem(ref ipv4h,ref icmppp,b_most_important_info_only);
                            break;
                        case easy_socket.icmp.icmp.Timestamp:
                            easy_socket.icmp.icmp_timestamp icmpt=new easy_socket.icmp.icmp_timestamp();
                            if (icmpt.decode(ipv4h.data)==easy_socket.icmp.icmp.error_success)
                                data=packet_ToStringArray.icmp_timestamp(ref ipv4h,ref icmpt,b_most_important_info_only);
                            break;
                        case easy_socket.icmp.icmp.TimestampReply:
                            easy_socket.icmp.icmp_timestamp_reply icmptr=new easy_socket.icmp.icmp_timestamp_reply();
                            if (icmptr.decode(ipv4h.data)==easy_socket.icmp.icmp.error_success)
                                data=packet_ToStringArray.icmp_timestamp_reply(ref ipv4h,ref icmptr,b_most_important_info_only);
                            break;
                        case easy_socket.icmp.icmp.InformationRequest:
                            easy_socket.icmp.icmp_information_request icmpirequest=new easy_socket.icmp.icmp_information_request();
                            if (icmpirequest.decode(ipv4h.data)==easy_socket.icmp.icmp.error_success)
                                data=packet_ToStringArray.icmp_information_request(ref ipv4h,ref icmpirequest,b_most_important_info_only);
                            break;
                        case easy_socket.icmp.icmp.InformationReply:
                            easy_socket.icmp.icmp_information_reply icmpireply=new easy_socket.icmp.icmp_information_reply();
                            if (icmpireply.decode(ipv4h.data)==easy_socket.icmp.icmp.error_success)
                                data=packet_ToStringArray.icmp_information_reply(ref ipv4h,ref icmpireply,b_most_important_info_only);
                            break;
                            //default: 

                    }// end of icmp switch
                        break;
                        // if udp protocol
                    case easy_socket.ip_header.ipv4_header.protocol_udp:
                        easy_socket.udp_header.udp_header udph=new easy_socket.udp_header.udp_header();
                        udph.decode(ipv4h.source_address,ipv4h.destination_address,ipv4h.data,false);// don't check checksum
                        data=packet_ToStringArray.udp(ref ipv4h,ref udph,false);// show max info of original packet 
                        break;

                        // if tcp protocol
                    case easy_socket.ip_header.ipv4_header.protocol_tcp:
                        easy_socket.tcp_header.tcp_header tcph=new easy_socket.tcp_header.tcp_header();
                        tcph.decode(ipv4h.source_address,ipv4h.destination_address,ipv4h.data,false);// don't check checksum
                        data=packet_ToStringArray.tcp(ref ipv4h,ref tcph,b_most_important_info_only);// show max info of original packet 
                        break;
                        // other protocol
                    default:
                        data=packet_ToStringArray.ip_raw(ref ipv4h,b_most_important_info_only);
                        break;
                }// end of protocol switch
            }// end of ip data test
            // Join data (string[]) to a string 
            if (data!=null)
                ret=System.String.Join(" ",data);
            return ret;
        }
    }
}
