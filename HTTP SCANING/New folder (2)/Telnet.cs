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

namespace Network_Stuff
{
    /// <summary>
    /// Summary description for Telnet.
    /// Small class used only to remove unwanted data and give command to remove echo
    /// To be improved for other functions
    /// </summary>
    public class Telnet
    {
        public static string COMMAND_TO_REMOVE_ECHO=new string(new char[6]{(char)(byte)TELNET_COMMANDS.IAC,(char)(byte)TELNET_COMMANDS.WONT,(char)(byte)TELNET_OPTIONS.ECHO,(char)(byte)TELNET_COMMANDS.IAC,(char)(byte)TELNET_COMMANDS.DONT,(char)(byte)TELNET_OPTIONS.ECHO});
        public bool b_clear_screen;
        public bool b_erase_char;
        public bool b_erase_line;
        public bool b_remote_echo;
        public bool b_local_echo;
        public bool b_remove_local_echo_and_ask_to_remove_remote_echo;
        public bool b_allow_beep;

        public char end_of_prompt;
        private string last_sent_command;
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool Beep(int frequency, int duration);

        public Telnet()
        {
            this.b_clear_screen=false;
            this.b_erase_char=false;
            this.b_erase_line=false;
            this.b_remote_echo=true;
            this.b_local_echo=false;
            this.b_allow_beep=true;
            this.last_sent_command="";
            this.b_remove_local_echo_and_ask_to_remove_remote_echo=true;
            this.end_of_prompt='>';
        }

        private void reset_command_members()
        {
            this.b_clear_screen=false;
            this.b_erase_char=false;
            this.b_erase_line=false;
        }

        #region telnet const
        private const byte TELNET_BEL=7;
        private const byte TELNET_BS=8;
        private const byte TELNET_HT=9;
        private const byte TELNET_VT=11;
        private const byte TELNET_FF=12;
        
        private enum TELNET_OPTIONS:byte
        {
            SUPPRESS_GO_AHEAD        =3,  // RFC 858 
            STATUS                    =5,  // RFC 859 
            ECHO                    =1,  // RFC 857 
            TIMING_MARK                =6,  // RFC 860 
            TERMINAL_TYPE            =24, // RFC 1091 
            WINDOW_SIZE                =31, // RFC 1073 
            TERMINAL_SPEED            =32, // RFC 1079 
            REMOTE_FLOW_CONTROL        =33, // RFC 1372 
            LINEMODE                =34, // RFC 1184 
            ENVIRONMENT_VARIABLES    =36  // RFC 1408 
        }
        private enum TELNET_COMMANDS:byte
        {
            SE=240,// End of subnegotiation parameters  
            NOP,//241 No operation  
            DM,// 242 Data mark Indicates the position of a Synch event within the data stream. This should always be accompanied by a TCP urgent notification. 
            BRK,// 243 Break Indicates that the "break" or "attention" key was hi.  
            IP,// 244 Suspend Interrupt or abort the process to which the NVT is connected. 
            AO,// 245 Abort output Allows the current process to run to completion but does not send its output to the user.  
            AYT,// 246 Are you there Send back to the NVT some visible evidence that the AYT was received. 
            EC,// 247 Erase character The receiver should delete the last preceding undeleted character from the data stream.  
            EL,// 248 Erase line Delete characters from the data stream back to but not including the previous CRLF.  
            GA,// 249 Go ahead Under certain circumstances used to tell the other end that it can transmit. 
            SB,// 250 Subnegotiation Subnegotiation of the indicated option follows. 
            WILL,// 251 will Indicates the desire to begin performing, or confirmation that you are now performing, the indicated option. 
            WONT,// 252 wont Indicates the refusal to perform, or continue performing, the indicated option. 
            DO,// 253 do Indicates the request that the other party perform, or confirmation that you are expecting the other party to perform, the indicated option. 
            DONT,// 254 dont Indicates the demand that the other party stop performing, or confirmation that you are no longer expecting the other party to perform, the indicated option.  
            IAC// 255 
        }
        #endregion

        #region parse
        public void parse(ref string txt)
        {
            int pos;
            int pos2;
            string buffer;
            this.reset_command_members();

            //char[] c1=txt.ToCharArray(); // for debug only

            // clear screen
            pos=txt.LastIndexOf((char)TELNET_FF);
            if (pos>-1)
            {
                // put clear_screen flag at true (the caller should clear data)
                this.b_clear_screen=true;
                // removes that is before last pos
                if (txt.Length<pos+1)
                {
                    txt="";
                    // non more data --> nothing to do
                    return;
                }
                txt=txt.Substring(pos+1);
            }

            // BELL
            if (txt.IndexOf((char)TELNET_BEL)>-1)
            {
                if (this.b_allow_beep)
                    Beep(400,100);
                txt=txt.Replace(new string((char)TELNET_BEL,1),"");
            }

            // Vertical Tabulation
            if (txt.IndexOf((char)TELNET_VT)>-1)
            {
                txt=txt.Replace(new string((char)TELNET_VT,1),"\r\n");
            }

            // Horizontal tabulation
            if (txt.IndexOf((char)TELNET_HT)>-1)
            {
                txt=txt.Replace(new string((char)TELNET_HT,1),"\t");
            }

            // Erase char
            pos=txt.IndexOf((char)((byte)TELNET_COMMANDS.EC));// byte cast because of compilation error on .NET 2002
            while (pos>-1)
            {
                buffer="";
                if (pos>1)
                    buffer=txt.Substring(0,pos-1);
                else
                    this.b_erase_char=true;// the caller should remove previous char
                buffer+=txt.Substring(pos+1);
                txt=buffer;
                pos=txt.IndexOf((char)((byte)TELNET_COMMANDS.EC));
            }

            // Erase line
            pos=txt.IndexOf((char)((byte)TELNET_COMMANDS.EL));
            while (pos>-1)
            {
                buffer="";
                if (pos>1)
                    buffer=txt.Substring(0,pos-1);
                pos2=buffer.LastIndexOf("\r\n");
                if (pos2>-1)
                    buffer=buffer.Substring(0,pos2+2);// let \r\n
                else
                {
                    buffer="";
                    this.b_erase_line=true;// the caller should remove previous line
                }
                buffer+=txt.Substring(pos+1);
                txt=buffer;
                pos=txt.IndexOf((char)((byte)TELNET_COMMANDS.EL));
            }

            pos=txt.IndexOf((char)((byte)TELNET_COMMANDS.IAC));
            while (pos>-1)
            {
                switch ((byte)(System.Convert.ToChar(txt.Substring(pos+1,1))))
                {
                    case (byte)TELNET_COMMANDS.SB:
                        // remove data between sb and se
                        // look for se
                        pos2=txt.IndexOf((char)(byte)TELNET_COMMANDS.SE,pos);
                        if (pos2<0)
                        {
                            txt=txt.Substring(0,pos);
                            return;
                        }
                        txt=txt.Substring(0,pos)+txt.Substring(pos2);
                        break;
                    case (byte)TELNET_COMMANDS.DO:
                        if ((byte)(System.Convert.ToChar(txt.Substring(pos+2,1)))==(byte)TELNET_OPTIONS.ECHO)
                            this.b_local_echo=true;
                        // remove the following command
                        txt=txt.Substring(0,pos)+txt.Substring(pos+3);
                        break;
                    case (byte)TELNET_COMMANDS.DONT:
                        if ((byte)(System.Convert.ToChar(txt.Substring(pos+2,1)))==(byte)TELNET_OPTIONS.ECHO)
                            this.b_local_echo=false;
                        // remove the following command
                        txt=txt.Substring(0,pos)+txt.Substring(pos+3);
                        break;
                    case (byte)TELNET_COMMANDS.WILL:
                        if ((byte)(System.Convert.ToChar(txt.Substring(pos+2,1)))==(byte)TELNET_OPTIONS.ECHO)
                            this.b_remote_echo=true;
                        // remove the following command
                        txt=txt.Substring(0,pos)+txt.Substring(pos+3);
                        break;
                    case (byte)TELNET_COMMANDS.WONT:
                        if ((byte)(System.Convert.ToChar(txt.Substring(pos+2,1)))==(byte)TELNET_OPTIONS.ECHO)
                            this.b_remote_echo=false;
                        // remove the following command
                        txt=txt.Substring(0,pos)+txt.Substring(pos+3);
                        break;
                    default:
                        txt=txt.Substring(0,pos)+txt.Substring(pos+2);
                        break;
                }
                pos=txt.IndexOf((char)((byte)TELNET_COMMANDS.IAC));
            }

            // remove all non 7 bits characters
            for (pos=127;pos<256;pos++)
                txt=txt.Replace(new string((char)pos,1),"");
            // remove other control chars
            for (pos=0;pos<30;pos++)
            {
                if ((pos==10) || (pos==13))
                    continue;
                txt=txt.Replace(new string((char)pos,1),"");
            }

            // char[] c2=txt.ToCharArray(); // for debug only
        }
        #endregion

        #region remove echo
        public void set_sent_command(string cmd)
        {
            this.last_sent_command=cmd;
        }
        public void remove_echo_from_command(ref string cmd)
        {
            int prompt_size=0;
            if (this.last_sent_command.Length==0)
                return;
            
            System.Collections.ArrayList al=new System.Collections.ArrayList();
            
            // split cmd on \r and \n
            string[] str_array=cmd.Split(new char[2]{'\r','\n'});
            // for each line
            for (int cpt=0;cpt<str_array.Length;cpt++)
            {
                if (str_array[cpt].Length==0)
                    continue;
                prompt_size=str_array[cpt].IndexOf(this.end_of_prompt)+1;
                if (prompt_size<=0)// end of prompt not found
                    prompt_size=0;
                // if line ends with the begin of the first not echoed command and line length <command length(+prompt size)--> echo
                if (
                    (this.cmd_ends_with_begin_of_not_echoed_cmd(str_array[cpt],this.last_sent_command,prompt_size))
                    && (str_array[cpt].Length-prompt_size<=this.last_sent_command.Length)
                    )
                {
                    // --> remove line
                    str_array[cpt]="";
                }
                if (str_array[cpt].Length!=0)
                    al.Add(str_array[cpt]);
            }
            // join not empty elements of Array
            str_array=(string[])al.ToArray(typeof(string));
            cmd=System.String.Join("\n",str_array);
        }
        private bool cmd_ends_with_begin_of_not_echoed_cmd(string cmd,string not_echoed_cmd,int prompt_size)
        {
            if (prompt_size>0)
                cmd=cmd.Substring(prompt_size);
            if (not_echoed_cmd.StartsWith(cmd))
                return true;
            return false;
        }
        #endregion
    }
}
