/*
This class is a begin of telnet implementation. Not all options/commands are implemented,
but it work greats even with vi.


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

Parsing of telnet commands is based from the very good project under GPL licence
Telnet Win32
Copyright (C) 1996-1997, Brad Johnson <jbj@nounname.com>
Copyright (C) 1998 I.Ioannou
Copyright (C) 1999-2000 Paul Brannan.
Console Telnet's home page is
http://www.musc.edu/~brannanp/telnet/.  You can get the latest version from
ftp://argeas.cs-net.gr/Telnet-Win32 or from the web page.  Telnet is
available as full project (sources included) or as binaries only.  If you
would like to help to the development check the /devel directory on the ftp
site for a recent alpha version.
*/
using System;

namespace easy_socket.Telnet
{
    /// <summary>
    /// Summary description for Telnet.
    /// Small class used only to remove unwanted data and give command to remove echo
    /// To be improved for other functions
    /// </summary>
    public class Telnet
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool Beep(int frequency, int duration);


        #region members
        public bool b_allow_beep;

        private System.Windows.Forms.TextBox textbox;
        private System.Collections.ArrayList al_reply_cmds;
        private System.Collections.ArrayList al_telnet_options;

        private string str_local_buffer="";

        private const int default_foreground_color=0;
        private const int default_background_color=8;
        private bool b_insert_mode=false;
        public bool bInsertMode
        {
            get
            {
                 return this.b_insert_mode;
            }
        }
        private bool b_app_cursor_key=false;
        public bool bAppCursorKey
        {
            get
            {
                return this.b_app_cursor_key;
            }
        }
        private bool b_alternate_keypad_mode;
        public bool bAlternateKeypadMode
        {
            get
            {
                return this.b_alternate_keypad_mode;
            }
        }
        public bool bEcho
        {
            get
            {
                return this.should_option_be_implemented(TELNET_OPTIONS.ECHO,false);
            }
        }
        private int i_saved_cursor_Y;
        private int i_saved_cursor_X;
        private int i_saved_attributes;
        private string str_last_string;
        private int i_cursor_real_pos_x=0;
        private int i_secure_caret_pos=0;
        // used to store last sent size of our pseudo console to remote host
        private ushort nb_char_wide=0;
        private ushort nb_char_high=0;


        private int i_current_foreground_color;
        private int i_current_background_color;

        private System.Drawing.Color[] ANSIColors={System.Drawing.Color.Black,
                                                    System.Drawing.Color.Red,
                                                    System.Drawing.Color.Green,
                                                    System.Drawing.Color.Yellow,
                                                    System.Drawing.Color.Blue,
                                                    System.Drawing.Color.Magenta,
                                                    System.Drawing.Color.Cyan,
                                                    System.Drawing.Color.White};

        #endregion

        #region constructor / initializer
        public Telnet(System.Windows.Forms.TextBox textbox)
        {
            this.textbox=textbox;

            this.b_allow_beep=true;
            this.textbox.HideSelection=false;

            this.reset();
        }

        public void reset()
        {
            this.al_telnet_options=new System.Collections.ArrayList(10);
            this.i_saved_cursor_Y=0;
            this.i_saved_cursor_X=0;
            this.i_saved_attributes=0;
            this.str_last_string="";
            this.i_current_foreground_color=Telnet.default_foreground_color;
            this.i_current_background_color=Telnet.default_background_color;
            this.textbox.SelectionLength=1;
            this.textbox.Text="";
            this.b_app_cursor_key=false;
            this.b_alternate_keypad_mode=false;
            this.b_insert_mode=false;
            this.str_local_buffer="";
            this.i_secure_caret_pos=0;
            this.nb_char_wide=this.get_nb_char_wide();
            this.nb_char_high=this.get_nb_char_high();
        }
        #endregion

        #region telnet const
        /*
        Code    Option                                                      References 
        0       TRANSMIT-BINARY, Binary Transmission.                       RFC 856 
        1       ECHO, Echo.                                                 RFC 857 
        2       Reconnection.   
        3       SUPPRESS-GO-AHEAD, Suppress Go Ahead.                       RFC 858 
        4       Approx Message Size Negotiation.   
        5       STATUS.                                                     RFC 859 
        6       TIMING-MARK, Timing Mark                                    RFC 860 
        7       RCTE, Remote Controlled Trans and Echo.                     RFC 563, RFC 726 
        8       Output Line Width.   
        9       Output Page Size.   
        10       NAOCRD, Negotiate About Output Carriage-Return Disposition.RFC 652 
        11       NAOHTS, Negotiate About Output Horizontal Tabstops.        RFC 653 
        12       NAOHTD, Negotiate About Output Horizontal Tab Disposition. RFC 654 
        13       NAOFFD, Negotiate About Output Formfeed Disposition.       RFC 655 
        14       NAOVTS, Negotiate About Vertical Tabstops.                 RFC 656 
        15       NAOVTD, Negotiate About Output Vertcial Tab Disposition.   RFC 657 
        16       NAOLFD, Negotiate About Output Linefeed Disposition.       RFC 658. 
        17       Extended ASCII.                                            RFC 698 
        18       LOGOUT, Logout.                                            RFC 727 
        19       BM, Byte Macro.                                            RFC 735 
        20       Data Entry Terminal.                                       RFC 732, RFC 1043 
        21       SUPDUP.                                                    RFC 734, RFC 736 
        22       SUPDUP-OUTPUT, SUPDUP Output.                              RFC 749 
        23       SEND-LOCATION, Send Location.                              RFC 779 
        24       TERMINAL-TYPE, Terminal Type.                              RFC 1091 
        25       END-OF-RECORD, End of Record.                              RFC 885 
        26       TUID, TACACS User Identification.                          RFC 927 
        27       OUTMRK, Output Marking.                                    RFC 933 
        28       TTYLOC, Terminal Location Number.                          RFC 946 
        29       Telnet 3270 Regime.                                        RFC 1041 
        30       X.3 PAD.                                                   RFC 1053 
        31       NAWS, Negotiate About Window Size.                         RFC 1073 
        32       Terminal Speed.                                            RFC 1079 
        33       Remote Flow Control.                                       RFC 1372 
        34       Linemode.                                                  RFC 1184 
        35       X Display Location.                                        RFC 1096 
        36       Environment Option.                                        RFC 1408 
        37       AUTHENTICATION, Authentication Option.                     RFC 1416, RFC 2941, RFC 2942, RFC 2943, RFC 2951 
        38       Encryption Option.                                         RFC 2946 
        39       New Environment Option.                                    RFC 1572 
        40       TN3270E.                                                   RFC 2355 
        41       XAUTH.   
        42       CHARSET.                                                   RFC 2066 
        43       RSP, Telnet Remote Serial Port.   
        44       Com Port Control Option                                    RFC 2217 
        45       Telnet Suppress Local Echo   
        46       Telnet Start TLS   
        47       KERMIT                                                     RFC 2840 
        48       SEND-URL   
        49       FORWARD_X   
        50
        -
        137     
        138       TELOPT PRAGMA LOGON   
        139       TELOPT SSPI LOGON   
        140       TELOPT PRAGMA HEARTBEAT   
        141
        -
        254     
        255       EXOPL, Extended-Options-List.                             RFC 861 
            
        */
        public enum TELNET_OPTIONS:byte
        {
            BIN     =0,
            ECHO    =1,
            RECN    =2,
            SUPGA   =3,
            APRX    =4,
            STAT    =5,
            TIM     =6,
            REM     =7,
            OLW     =8,
            OPS     =9,
            OCRD   =10,
            OHT    =11,
            OHTD   =12,
            OFD    =13,
            OVT    =14,
            OVTD   =15,
            OLD    =16,
            EXT    =17,
            LOGO   =18,
            BYTE   =19,
            DATA   =20,
            SUP    =21,
            SUPO   =22,
            SNDL   =23,
            TERM   =24,
            EOR    =25,
            TACACS =26,
            OM     =27,
            TLN    =28,
            T3270  =29,
            X3     =30,
            NAWS   =31,
            TS     =32,
            RFC    =33,
            LINE   =34,
            XDL    =35,
            ENVIR  =36,
            AUTH   =37,
            ENCR   =38,
            ENVIR2 =39,
            TN3270E=40,
            XAUTH  =41,
            CHARSET=42,
            RSP    =43,
            CPC    =44,
            SLE    =45,
            TLS    =46,
            KERMIT =47,
            SENDURL=48,
            FORWARD_X=49,
            EXTOP  =255
        }
        public enum TELNET_COMMANDS:byte
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
        public enum TERMINAL_TYPE:byte
        {
            IS=0,
            SEND=1
        }
        #endregion

        #region options management
        private void add_telnet_option(Ctelnet_option obj_option)
        {
            // check if option exist with a different cmd do/will/wont/dont
            int index=this.get_option_index((TELNET_OPTIONS)obj_option.option);
            if (index>=0)
                ((Ctelnet_option)this.al_telnet_options[index]).cmd=obj_option.cmd;
            // if not already added
            else
                this.al_telnet_options.Add(obj_option);
        }

        private int get_option_index(TELNET_OPTIONS option)
        {
            for (int cnt=0;cnt<this.al_telnet_options.Count;cnt++)
            {
                // if it's the case change cmd associated with option
                if ((byte)option==((Ctelnet_option)this.al_telnet_options[cnt]).option)
                {
                    return cnt;
                }
            }
            return -1;
        }
        private bool should_option_be_implemented(TELNET_OPTIONS option,bool default_value)
        {
            int index=this.get_option_index(option);
            if (index>=0)
                return ((Ctelnet_option)this.al_telnet_options[index]).should_option_be_implemented();
            // not found
            return default_value;
        }
        private bool should_we_send_reply(TELNET_OPTIONS option,TELNET_COMMANDS new_command)
        {
            // get our current state
            bool b_should_option_be_implemented=this.should_option_be_implemented(option,false);
            // compare it to wanted new state
            switch (new_command)
            {
                case Telnet.TELNET_COMMANDS.WILL:
                case Telnet.TELNET_COMMANDS.DO:
                    return !b_should_option_be_implemented;
                case Telnet.TELNET_COMMANDS.WONT:
                case Telnet.TELNET_COMMANDS.DONT:
                    return b_should_option_be_implemented;
                default:
                    return false;
            }
        }
        #endregion

        #region cursor positioning
        /// <summary>
        /// 
        /// </summary>
        /// <param name="num_line">0 based line number</param>
        /// <param name="add_lines_if_necessary">true to add lines</param>
        /// <returns></returns>
        int num_line_to_selection_start(int num_line,bool add_lines_if_necessary)
        {
            int pos=0;
            int old_pos=0;
            string str=this.textbox.Text;
            // search for \r\n
            for (int cnt=0;cnt<num_line;cnt++)
            {
                pos=str.IndexOf("\r\n",old_pos);
                if (pos<0)
                {
                    if (add_lines_if_necessary)
                    {
                        // add necessary \r\n
                        for (int cnt2=cnt;cnt2<num_line;cnt2++)
                        {
                            str+="\r\n";
                        }
                        // save selection start
                        int i_caret_position=this.iSelectionStart;
                        // update textbox content
                        this.textbox.Text=str;
                        // restore selection start
                        this.iSelectionStart=i_caret_position;
                    }
                    return str.Length;
                }
                else
                {
                    old_pos=Math.Min(pos+2,str.Length);
                }
            }
            return old_pos;
        }

        int selection_start_to_num_line(int sel_start)
        {
            int begin_of_line=0;
            return this.selection_start_to_num_line(sel_start,ref begin_of_line);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sel_start"></param>
        /// <param name="begin_of_line_pos"></param>
        /// <returns>0 based line number</returns>
        int selection_start_to_num_line(int sel_start,ref int begin_of_line_pos)
        {
            int pos=0;
            begin_of_line_pos=0;
            string str=this.textbox.Text;
            // count \r\n
            for(int cnt=0;;cnt++)
            {
                pos=str.IndexOf("\r\n",begin_of_line_pos);
                if ((pos<0)||(pos>=sel_start))
                    return cnt;
                else
                    begin_of_line_pos=Math.Min(pos+2,str.Length);
            }
        }
       
        private int iSelectionStart
        {
            set
            {
                int i_start=value;

                if (i_start<0)
                    i_start=0;
                else
                {
                    int len=this.textbox.Text.Length;
                    if (i_start>len)
                    {
                        i_start=len;
                    }
                }

                this.textbox.SelectionStart=i_start;
                this.textbox.SelectionLength=1;
                this.textbox.ScrollToCaret();
            }
            get
            {
                return this.textbox.SelectionStart;
            }
        }

        int get_cursor_Y()
        {
            return this.selection_start_to_num_line(this.iSelectionStart);
        }

        /// <summary>
        /// return x position of the cursor in the console mode (that means it can be greater than the number of char in the current line)
        /// </summary>
        /// <returns></returns>
        int get_cursor_X()
        {
            return this.i_cursor_real_pos_x;
        }

        /// <summary>
        /// return x position of the cursor in the textbox mode (cant be greater than the number of char of the current line) 
        /// </summary>
        /// <returns></returns>
        int get_line_cursor_X()
        {
            int begin_of_line=0;
            this.selection_start_to_num_line(this.iSelectionStart,ref begin_of_line);
            return this.iSelectionStart-begin_of_line;
        }
        void set_cursor_pos(int x, int y) 
        {
            int i_start=this.num_line_to_selection_start(y,true);
            int new_x=this.assume_x_is_in_line(x,y);
            this.iSelectionStart=new_x+i_start;
            // store real x pos
            this.i_cursor_real_pos_x=x;
        }

        void move_cursor_pos(int x, int y)
        {
            int istart=this.iSelectionStart;
            string str=this.textbox.Text;

            // get old line number
            int old_line=this.selection_start_to_num_line(istart);
            int new_line=Math.Max(0,old_line+y);
            // store real x pos
            this.i_cursor_real_pos_x+=x;

            int new_x=this.assume_x_is_in_line(this.i_cursor_real_pos_x,new_line);
            int sel_start_new_line=this.num_line_to_selection_start(new_line,true);
            this.iSelectionStart=sel_start_new_line+new_x;

        }
        /// <summary>
        /// assume that x is in the line line_number
        /// </summary>
        /// <param name="x">x value to test</param>
        /// <param name="line_number">0 based number line</param>
        /// <returns>x modified if necessary</returns>
        int assume_x_is_in_line(int x,int line_number)
        {
            string str=this.textbox.Text;
            // get begin of new line pos
            int sel_start_new_line=this.num_line_to_selection_start(line_number,false);
            // assume that new_x will be in the line
            int pos_eol=str.IndexOf("\r\n",sel_start_new_line);
            if (pos_eol<0)
                pos_eol=str.Length;
            int new_x=x;
            new_x=Math.Min(new_x,pos_eol-sel_start_new_line);
            new_x=Math.Max(new_x,0);

            return new_x;
        }

        void go_home()
        {
            this.set_cursor_pos(0, 0);// not for vt100
            this.textbox.ScrollToCaret();
            this.textbox.SelectionLength=1;
        }
        void go_end()
        {
            this.iSelectionStart=this.textbox.Text.Length;
            // store real x pos
            this.i_cursor_real_pos_x=this.get_line_cursor_X();
            this.textbox.SelectionLength=0;
            this.textbox.ScrollToCaret();
        }

        /// <summary>
        /// in telnet mode, scroll = replacing lines
        /// </summary>
        /// <param name="nb_lines_down"></param>
        void scroll(int nb_lines_down)
        {
            if (nb_lines_down==0)
                return;
            // save cursor pos
            int istart=this.iSelectionStart;

            string str=this.textbox.Text;
            str=str.Replace("\r\n","\n");
            string[] str_array=str.Split(new char[]{'\n'});
            if (nb_lines_down>0)
            {
                if (nb_lines_down<str_array.Length)
                    str=string.Join("\r\n",str_array,nb_lines_down,str_array.Length-nb_lines_down);
                else
                    str="";
            }
            else
            {
                int nb_lines_up=-nb_lines_down;
                if (nb_lines_up<str_array.Length)
                {
                    for (int cnt=0;cnt<nb_lines_up;cnt++)
                    {
                        str+="\r\n";
                    }
                    str+=string.Join("\r\n",str_array,0,str_array.Length-nb_lines_up);
                }
                else
                    str="";
            }

            // restore cursor pos
            this.iSelectionStart=istart;
            this.textbox.Text=str;
        }
        #endregion

        #region text management

        private void write(char c)
        {
            string s=new string(c,1);
            this.write(s);
        }

        private void write(string s)
        {
            string str=this.textbox.Text;
            int pos;
            int i_caret_position=this.iSelectionStart;

            this.str_last_string=s;
            if (!this.b_insert_mode)
            {
                // replace char at cursor pos
                if (i_caret_position+s.Length<str.Length)
                {
                    // erase only until \r\n
                    int replace_data_len;
                    pos=str.IndexOf("\r\n",i_caret_position);
                    if (pos<0)
                    {
                        replace_data_len=str.Length-i_caret_position;
                    }
                    else
                    {
                        replace_data_len=pos-i_caret_position;
                    }
                    replace_data_len=Math.Min(replace_data_len,s.Length);
                    this.textbox.Text=str.Substring(0,i_caret_position)+s+str.Substring(i_caret_position+replace_data_len);
                }
                else
                    this.textbox.Text=str.Substring(0,i_caret_position)+s;
            }
            else
                // insert text
                this.textbox.Text=str.Insert(i_caret_position,s);

            // change caret position
            this.iSelectionStart=i_caret_position+s.Length;

            this.i_cursor_real_pos_x=this.get_line_cursor_X();
        }

        private void reset_terminal()
        {
            this.clear_screen();
        }

        private void clear_screen()
        {
            this.textbox.Clear();
            this.set_cursor_pos(0,0);
        }
        /// <summary>
        /// erase current selected char
        /// </summary>
        private void erase_char()
        {
            this.erase_char(1);
        }
        /// <summary>
        /// erase current selected char and nb_char-1 char before the selected one
        /// </summary>
        /// <param name="nb_char"></param>
        private void erase_char(int nb_char)
        {
            int i_caret_position=this.iSelectionStart+1;
            string str=this.textbox.Text;

            if (i_caret_position<nb_char)
                nb_char=i_caret_position;
            for (int cnt=0;cnt<nb_char;cnt++)
            {
                if (i_caret_position-1<0)
                    break;
                if (str.Substring(i_caret_position-1,1)=="\n")
                {
                    // we can't do it --> stop erasing char
                    break;
                }
                str=str.Remove(i_caret_position-1,1);
                i_caret_position--;
            }
            this.textbox.Text=str;
            this.iSelectionStart=i_caret_position;
            this.i_cursor_real_pos_x=this.get_line_cursor_X();
        }
        private void back_space()
        {
            int i_caret_position=this.iSelectionStart;
            if (this.textbox.Text.Substring(i_caret_position-1,1)!="\n")
            {
                this.move_cursor_pos(-1,0);
            }
        }

        private void erase_line()
        {
            int i_caret_position=this.iSelectionStart;
            bool b_line_removed=false;
            string str=this.textbox.Text;

            // look for previous \r\n
            int pos=str.Substring(0,i_caret_position).LastIndexOf("\r\n");
            // look for next \r\n
            int pos2=str.IndexOf("\r\n",i_caret_position);
            string s="";
            if (pos>-1)
            {
                s=str.Substring(0,pos);
                b_line_removed=true;
            }
            if (pos2>-1)
            {
                if (b_line_removed)
                    s+=str.Substring(pos2);
                else if (pos2+2<str.Length)
                    s=str.Substring(pos2+2);
            }
            this.textbox.Text=s;
            // restore carret position after having change textbox content
            this.iSelectionStart=i_caret_position;
            this.textbox.SelectionLength=1;
        }

        private const int HORIZONTAL_TABULATION_SIZE=8;// based on freebsd 5x
        private void horizontal_tabulation()
        {
            // quite like move_cursor_pos(HORIZONTAL_TABULATION_SIZE,0) but adding space if not enought chars 
            int istart=this.iSelectionStart;
            string str=this.textbox.Text;
            // find next \r\n
            int eol_pos=str.IndexOf("\r\n",istart);
            if (eol_pos<0)
                eol_pos=str.Length;
            int nb_char_to_eol=eol_pos-istart;
            int pos_x=this.get_cursor_X();
            int current_tab=pos_x/HORIZONTAL_TABULATION_SIZE;
            int nb_char_to_move=(current_tab+1)*HORIZONTAL_TABULATION_SIZE-pos_x;
            // if enought place just move cursor
            if (nb_char_to_eol>=nb_char_to_move)
            {
                this.move_cursor_pos(nb_char_to_move,0);
                // all is done return
                return;
            }
            // else we have to add blanks
            this.textbox.Text=str.Insert(eol_pos,new string(' ',nb_char_to_move));
            // store real x pos
            this.i_cursor_real_pos_x+=nb_char_to_move;
            this.iSelectionStart=istart+nb_char_to_move;
        }

        private void clear_line()
        {
            this.clear_begin_of_line();
            this.clear_end_of_line();
        }
        private void clear_end_of_line()
        {
            int i_caret_position=this.iSelectionStart;
            string str=this.textbox.Text;

            // look for next \r\n
            int pos=str.IndexOf("\r\n",i_caret_position);
            if (pos>-1)
                this.textbox.Text=str.Substring(0,i_caret_position)+str.Substring(pos);
            else
                this.textbox.Text=str.Substring(0,i_caret_position);

            // restore lost caret position
            this.iSelectionStart=i_caret_position;
        }
        private void clear_begin_of_line()
        {
            int i_caret_position=this.iSelectionStart;

            string str=this.textbox.Text;
            // look for previous \r\n
            int pos=str.Substring(0,i_caret_position).LastIndexOf("\r\n");
            if (pos>-1)
                this.textbox.Text=str.Substring(0,pos+2)+str.Substring(i_caret_position);
            else
                this.textbox.Text=str.Substring(i_caret_position);

            // restore carret position
            this.iSelectionStart=i_caret_position;
        }

        private void line_feed()
        {
            this.move_cursor_pos(0,1);
        }

        private void carriage_return()
        {
            int i_caret_position=this.iSelectionStart;

            int pos=this.textbox.Text.Substring(0,i_caret_position).LastIndexOf("\r\n");
            if (pos>-1)
                this.iSelectionStart=pos+2;
            else
                this.iSelectionStart=0;
            this.i_cursor_real_pos_x=0;
        }

        private void insert_char(char c,int nb_chars)
        {
            string s=new string(c,nb_chars);
            bool b_insert_mode_backup=this.b_insert_mode;
            this.b_insert_mode=true;
            this.write(s);
            this.b_insert_mode=b_insert_mode_backup;
        }
        private void clear_end_of_screen()
        {
            int i_caret_position=this.iSelectionStart;
            this.textbox.Text=this.textbox.Text.Substring(0,this.iSelectionStart);
            // restore caret pos
            this.iSelectionStart=i_caret_position;
        }
        private void clear_begin_of_screen()
        {
            int i_caret_position=this.iSelectionStart;
            this.textbox.Text=this.textbox.Text.Substring(this.iSelectionStart);
            // restore caret pos
            this.iSelectionStart=i_caret_position;
        }


        #endregion

        // not implemented yet (limitation of textbox)
        #region color / background / font / console attribute
        void set_default_background(int i)
        {
            this.set_background_color(Telnet.default_background_color);
        }
        void set_dark_background()
        {
            this.i_current_background_color=0;
        }
        void set_light_background()
        {
            this.i_current_background_color=8;
        }
        void set_normal_color()
        {
            this.i_current_background_color=8;
            this.i_current_foreground_color=0;
        }
        void set_foreground_color(int i)
        {
            if (i<0)
                i=0;
            else
                if (i>=this.ANSIColors.Length)
                    i=this.ANSIColors.Length-1;
            this.i_current_foreground_color=i;
        }
        void set_background_color(int i)
        {
            if (i<0)
                i=0;
            else
                if (i>=this.ANSIColors.Length)
                i=this.ANSIColors.Length-1;
            this.i_current_background_color=i;
        }
        void set_underline_mode(bool b_on)
        {

        }

        int get_attributes()
        {
return 0;
        }
        void set_attributes(int i)
        {

        }
        private void set_console_attribute(int i)
        {
            byte TextAttrib=(byte)(i&0xFF);
            // Paul Brannan 5/8/98
            // Made this go a little bit faster by changing from switch{} to an array
            // for the colors
            if(TextAttrib >= 30)
            {
                if(TextAttrib <= 37)
                {
                    this.set_foreground_color(TextAttrib-30);
                    return;
                } 
                else if((TextAttrib >= 40) && (TextAttrib <= 47))
                {
                    this.set_background_color(TextAttrib-40);
                    return;
                }
            }
            
            switch (TextAttrib)// Text Attributes
            {
                case 0: break;    // Normal video
                case 1: break;    // High video
                case 2: break;    // Low video
                case 4: this.set_underline_mode(true);// Underline on (I.Ioannou)
                    break;
                case 5: break;    // Blink video
                case 7: break;    // Reverse video
                case 8: break;    // hidden
                    // All from 10 thru 27 are hacked from linux kernel
                    // I.Ioannou 06 April, 1997
                case 10:
                    break; // ANSI X3.64-1979 (SCO-ish?)
                    // Select primary font,
                    // don't display control chars
                    // if defined, don't set
                    // bit 8 on output (normal)
                case 11:
                    break; // ANSI X3.64-1979 (SCO-ish?)
                    // Select first alternate font,
                    // let chars < 32 be displayed
                    // as ROM chars
                case 12:
                    break; // ANSI X3.64-1979 (SCO-ish?)
                    // Select second alternate font,
                    // toggle high bit before
                    // displaying as ROM char.
                    
                case 21: // not really Low video
                case 22: // but this works good also                        
                    break;    
                case 24: // Underline off
                    break;    
                case 25: // blink off
                    break;
                case 27: //Reverse video off
                    break;

                // Mutt needs this (Paul Brannan, Peter Jordan 12/31/98)
                // This is from the Linux kernel source
                case 38: /* ANSI X3.64-1979 (SCO-ish?)
                        * Enables underscore, white foreground
                        * with white underscore (Linux - use
                        * default foreground).
                        */
                        this.set_underline_mode(true);
                        this.set_foreground_color(Telnet.default_foreground_color);
                        break;
                case 39: /* ANSI X3.64-1979 (SCO-ish?)
                        * Disable underline option.
                        * Reset colour to default? It did this
                        * before...
                        */
                        this.set_underline_mode(false);
                        this.set_foreground_color(Telnet.default_foreground_color);
                        break;
                case 49:
                        this.set_background_color(Telnet.default_background_color);
                        break;

            }
        }
        #endregion

        private string get_terminal_ID()
        {
            return "\033[?1;2c";
        }

        private ushort get_nb_char_wide()
        {
            return (ushort)(this.textbox.ClientSize.Width/7);// 7 is font width
        }
        private ushort get_nb_char_high()
        {
            return (ushort)(this.textbox.ClientSize.Height/11);
        }

        /// <summary>
        /// only use the ansi keymap yet
        /// </summary>
        /// <param name="key">KeyEventArgs to get info about alt,shift and ctrl states</param>
        /// <returns></returns>
        public byte[] get_function_key_bytes(System.Windows.Forms.KeyEventArgs key)
        {
            byte[] b=null;
            switch (key.KeyCode)
            {
                case System.Windows.Forms.Keys.Up:
                    b=new byte[3]{0x1B,0x5B,0x41};
                    break;
                case System.Windows.Forms.Keys.Down:
                    b=new byte[3]{0x1B,0x5B,0x42};
                    break;
                case System.Windows.Forms.Keys.Right:
                    b=new byte[3]{0x1B,0x5B,0x43};
                    break;
                case System.Windows.Forms.Keys.Left:
                    b=new byte[3]{0x1B,0x5B,0x44};
                    break;
                case System.Windows.Forms.Keys.Insert:
                    b=new byte[4]{0x1B,0x5B,0x32,0x7E};
                    break;
                case System.Windows.Forms.Keys.Home:
                    b=new byte[4]{0x1B,0x5B,0x31,0x7E};
                    break;
                case System.Windows.Forms.Keys.End:
                    b=new byte[4]{0x1B,0x5B,0x34,0x7E};
                    break;
                case System.Windows.Forms.Keys.PageUp:
                    b=new byte[4]{0x1B,0x5B,0x35,0x7E};
                    break;
                case System.Windows.Forms.Keys.PageDown:
                    b=new byte[4]{0x1B,0x5B,0x36,0x7E};
                    break;
                case System.Windows.Forms.Keys.Pause:
                    b=new byte[3]{0x1B,0x5B,0x50};
                    break;
                case System.Windows.Forms.Keys.Enter:
                    b=new byte[2]{0x0D,0x0A};
                    break;
                case System.Windows.Forms.Keys.Delete:
                    b=new byte[1]{0x7F};
                    break;
                case System.Windows.Forms.Keys.F1:
                    b=new byte[3]{0x1B,0x4F,0x50};
                    break;
                case System.Windows.Forms.Keys.F2:
                    b=new byte[3]{0x1B,0x4F,0x51};
                    break;
                case System.Windows.Forms.Keys.F3:
                    b=new byte[3]{0x1B,0x4F,0x52};
                    break;
                case System.Windows.Forms.Keys.F4:
                    b=new byte[3]{0x1B,0x4F,0x53};
                    break;
                case System.Windows.Forms.Keys.F5:
                    b=new byte[5]{0x1B,0x5B,0x31,0x35,0x7E};
                    break;
                case System.Windows.Forms.Keys.F6:
                    b=new byte[5]{0x1B,0x5B,0x31,0x37,0x7E};
                    break;
                case System.Windows.Forms.Keys.F7:
                    b=new byte[5]{0x1B,0x5B,0x31,0x38,0x7E};
                    break;
                case System.Windows.Forms.Keys.F8:
                    b=new byte[5]{0x1B,0x5B,0x31,0x39,0x7E};
                    break;
                case System.Windows.Forms.Keys.F9:
                    b=new byte[5]{0x1B,0x5B,0x32,0x30,0x7E};
                    break;
                case System.Windows.Forms.Keys.F10:
                    b=new byte[5]{0x1B,0x5B,0x32,0x31,0x7E};
                    break;
                case System.Windows.Forms.Keys.F11:
                    b=new byte[5]{0x1B,0x5B,0x32,0x33,0x7E};
                    break;
                case System.Windows.Forms.Keys.F12:
                    b=new byte[5]{0x1B,0x5B,0x32,0x34,0x7E};
                    break;
            }
            return b;
        }

        #region parsing
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="txt"></param>
        /// <returns>command byte array to return to host</returns>
        public byte[][] parse(string txt)
        {
            int pos;
            byte cmd_value;
            byte option_value;
            this.al_reply_cmds=new System.Collections.ArrayList(10);
            char[] txt_array=txt.ToCharArray();
            string str_buff="";// to earn speed
            int index;
            // lock textbox control to avoid user caret change during the parsing
            this.textbox.Enabled=false;
            // restore caret position in case of user change (like copy & paste)
            this.iSelectionStart=this.i_secure_caret_pos;

            if (this.str_local_buffer!="")
                txt=this.str_local_buffer+txt;
            for (int cnt=0;cnt<txt_array.Length;)
            {
                switch((byte)txt_array[cnt])
                {
                        // check IAC commands first
                        #region IAC
                    case (byte)TELNET_COMMANDS.IAC:
                        if (str_buff!="")
                        {
                            this.write(str_buff);
                            str_buff="";
                        }
                        cnt++;
                        cmd_value=(byte)txt_array[cnt];
                    switch (cmd_value)
                    {
                        case (byte)TELNET_COMMANDS.SB:
                            // packet is like IAC-SB-OPT-PARAM_1-PARAM_2-...-PARAM_N-IAC-SE
                            // check for sufficent buffer size
                            if (txt_array.Length-cnt<3)
                            {
                                cnt=txt_array.Length;
                                this.str_local_buffer=txt.Substring(cnt-1);
                                break;
                            }

                            option_value=(byte)txt_array[cnt+1];
                            index=this.get_option_index((TELNET_OPTIONS)option_value);

                            // retrive data between sb and se
                            // look for se
                            pos=txt.IndexOf(new string(new char[]{(char)(byte)TELNET_COMMANDS.IAC,(char)(byte)TELNET_COMMANDS.SE}),cnt+1);
                            if (pos<0)
                            {
                                cnt=txt_array.Length;
                                this.str_local_buffer=txt.Substring(cnt-1);
                                break;
                            }

                            if (index<0)// option have not be queried to be implemented
                            {
                                cnt=pos+2;
                                break;
                            }
                            Ctelnet_option opt=(Ctelnet_option)this.al_telnet_options[index];

                            byte[] b_param_option_array=null;
                            // if not empty parameter option add it to last option
                            if ((cnt+2)<(pos-2))//pos-1 is IAC
                            {
                                int i_array_len=pos-2-(cnt+1)+1;
                                b_param_option_array=new byte[i_array_len];
                                System.Array.Copy(b_param_option_array,0,txt.Substring(cnt+1,pos-2).ToCharArray(),0,i_array_len);
                                opt.set_parameter(b_param_option_array);
                            }

                            #region option parameter processing

                                if (opt.should_option_be_implemented())
                                {
                                    switch(opt.option)
                                    {
                                        case (byte)TELNET_OPTIONS.TERM:
                                            string str_term_type="ANSI";// only ansi term type is implemented
                                            byte[] term_type_reply=new byte[6+str_term_type.Length];
                                            term_type_reply[0]=(byte)TELNET_COMMANDS.IAC;
                                            term_type_reply[1]=(byte)TELNET_COMMANDS.SB;
                                            term_type_reply[2]=(byte)TELNET_OPTIONS.TERM;
                                            term_type_reply[3]=(byte)TERMINAL_TYPE.IS;
                                            byte[] b_term_type=System.Text.Encoding.ASCII.GetBytes(str_term_type);
                                            System.Array.Copy(b_term_type,0,term_type_reply,4,b_term_type.Length);
                                            term_type_reply[term_type_reply.Length-2]=(byte)TELNET_COMMANDS.IAC;
                                            term_type_reply[term_type_reply.Length-1]=(byte)TELNET_COMMANDS.SE;
                                            this.al_reply_cmds.Add(term_type_reply);
                                            break;
                                    }
                                }
                            #endregion

                            cnt=pos+2;
                            break;
                        case (byte)TELNET_COMMANDS.WILL:
                        // server offers us some service
                            cnt++;
                            option_value=(byte)txt_array[cnt];
                            switch (option_value)
                            {
                                case (byte)TELNET_OPTIONS.ECHO:
                                case (byte)TELNET_OPTIONS.SUPGA:
                                    this.al_reply_cmds.Add(new Ctelnet_option(TELNET_COMMANDS.DO,option_value).to_byte_array());
                                    this.add_telnet_option(new Ctelnet_option(TELNET_COMMANDS.DO,option_value));
                                    break;
                                default:
                                    if (this.should_we_send_reply((TELNET_OPTIONS)option_value,TELNET_COMMANDS.WILL))
                                    {
                                        this.al_reply_cmds.Add(new Ctelnet_option(TELNET_COMMANDS.DONT,option_value).to_byte_array());
                                        this.add_telnet_option(new Ctelnet_option(TELNET_COMMANDS.DONT,option_value));
                                    }
                                    break;
                            }
                            cnt++;
                            break;
                        case (byte)TELNET_COMMANDS.WONT:
                        // server tell us it don't offer a service
                            cnt++;
                            option_value=(byte)txt_array[cnt];
                            this.add_telnet_option(new Ctelnet_option(TELNET_COMMANDS.WONT,option_value));
                            cnt++;
                            break;
                        case (byte)TELNET_COMMANDS.DO:
                        // server ask us to do something
                            //in case of query we have to reply with WILL or WONT
                            //in case of reply of a previous WILL
                            cnt++;
                            option_value=(byte)txt_array[cnt];
                            switch (option_value)
                            {
                                case (byte)TELNET_OPTIONS.NAWS:
                                    this.al_reply_cmds.Add(new Ctelnet_option(TELNET_COMMANDS.WILL,option_value).to_byte_array());
                                    // send IAC SB NAWS 0 80 0 24 IAC SE for a 80 characters wide, 24 characters high window
                                    this.nb_char_wide=this.get_nb_char_wide();
                                    this.nb_char_high=this.get_nb_char_high();
                                    this.al_reply_cmds.Add(new byte[]{(byte)TELNET_COMMANDS.IAC,(byte)TELNET_COMMANDS.SB,
                                                                         (byte)TELNET_OPTIONS.NAWS,
                                                                         (byte)(nb_char_wide>>8),(byte)(nb_char_wide&0xff),
                                                                         (byte)(nb_char_high>>8),(byte)(nb_char_high&0xff),
                                                                         (byte)TELNET_COMMANDS.IAC,(byte)TELNET_COMMANDS.SE});
                                    this.add_telnet_option(new Ctelnet_option(TELNET_COMMANDS.WILL,option_value));
                                    break;
                                case (byte)TELNET_OPTIONS.TERM:
                                    this.al_reply_cmds.Add(new Ctelnet_option(TELNET_COMMANDS.WILL,option_value).to_byte_array());
                                    this.add_telnet_option(new Ctelnet_option(TELNET_COMMANDS.WILL,option_value));
                                    break;
                                default:
                                    if (this.should_we_send_reply((TELNET_OPTIONS)option_value,TELNET_COMMANDS.DO))
                                    {
                                        this.al_reply_cmds.Add(new Ctelnet_option(TELNET_COMMANDS.WONT,option_value).to_byte_array());
                                        this.add_telnet_option(new Ctelnet_option(TELNET_COMMANDS.WONT,option_value));
                                    }
                                    break;
                            }
                            cnt++;
                            break;
                        case (byte)TELNET_COMMANDS.DONT:
                        // server ask us to stop do something
                            cnt++;
                            option_value=(byte)txt_array[cnt];
                            this.add_telnet_option(new Ctelnet_option(TELNET_COMMANDS.DONT,option_value));
                            cnt++;
                            break;

                        case (byte)TELNET_COMMANDS.AYT:
                            this.al_reply_cmds.Add(new byte[3]{0x3E,0x0D,0x0A});// reply with ">\r\n"
                            cnt++;
                            break;
                        default:
                            cnt+=2;
                            break;
                    }
                        break;
                        #endregion

                        #region command
                    case 0:// no operation
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        cnt++;
                        break;
                        // BELL
                    case 7:
                        if (this.b_allow_beep)
                            Beep(400,100);
                        cnt++;
                        break;
                        // back space
                    case 8:
                        if (str_buff!="")
                        {
                            this.write(str_buff);
                            str_buff="";
                        }
                        this.back_space();
                        cnt++;
                        break;
                        // Horizontal tabulation
                    case 9:
                        if (str_buff!="")
                        {
                            this.write(str_buff);
                            str_buff="";
                        }
                        this.horizontal_tabulation();
                        cnt++;
                        break;
                    case 10:
                        if (str_buff!="")
                        {
                            this.write(str_buff);
                            str_buff="";
                        }
                        this.line_feed();
                        cnt++;
                        break;
                        // Vertical Tabulation
                    case 11:
                        if (str_buff!="")
                        {
                            this.write(str_buff);
                            str_buff="";
                        }
                        this.line_feed();// ?? is this the best way ?
                        cnt++;
                        break;
                        // FF
                    case 12:
                        if (str_buff!="")
                        {
                            // write even if clear screen ?
                            this.write(str_buff);
                            str_buff="";
                        }
                        this.clear_screen();
                        this.set_cursor_pos(this.get_cursor_X(), 1); // changed fm 1
                        cnt++;
                        break;                   
                    case 13:
                        if (str_buff!="")
                        {
                            this.write(str_buff);
                            str_buff="";
                        }
                        this.carriage_return();
                        cnt++;
                        break;
                        // Erase char
                    case (byte)TELNET_COMMANDS.EC:
                        if (str_buff!="")
                        {
                            this.write(str_buff);
                            str_buff="";
                        }
                        this.erase_char();
                        cnt++;
                        break;
                        // clear line
                    case (byte)TELNET_COMMANDS.EL:
                        if (str_buff!="")
                        {
                            this.write(str_buff);
                            str_buff="";
                        }
                        this.clear_line();
                        cnt++;
                        break;

                        #endregion

                        #region escape
                    case 0x1b:
                        if (str_buff!="")
                        {
                            this.write(str_buff);
                            str_buff="";
                        }
                        cnt++;
                        this.ParseEscape(txt_array,ref cnt,txt_array.Length-cnt);
                        break;

                        #endregion

                    default:
                        //this.write(txt_array[cnt]);
                        str_buff+=txt_array[cnt].ToString();
                        cnt++;
                        break;
                }
            }
            if (str_buff!="")
                this.write(str_buff);

            //char[] c2=txt.ToCharArray(); // for debug only

            // store caret position
            this.i_secure_caret_pos=this.iSelectionStart;
            // unlock textbox control to avoid user caret change during the parsing
            this.textbox.Enabled=true;

            // return reply commands if any
            return (byte[][])al_reply_cmds.ToArray(typeof(byte[]));
        }

        // Use this for the VT100 flags (Paul Brannan 12/2/98)
        private const int FLAG_DOLLAR=       0x0001;
        private const int FLAG_QMARK=        0x0002;
        private const int FLAG_GREATER=      0x0004;
        private const int FLAG_LESS=         0x0008;
        private const int FLAG_EXCLAM=       0x0010;
        private const int FLAG_AMPERSAND=    0x0020;
        private const int FLAG_SLASH=        0x0040;
        private const int FLAG_EQUAL=        0x0080;
        private const int FLAG_QUOTE=        0x0100;
        private const int FLAG_OTHER=        0x8000;

        void ParseEscapeANSI(char[] txt_array,ref int pos,int escape_length)//string pszBuffer, string pszBufferEnd)
        {
    
            //    The buffer contains something like <ESC>[pA
            //    where p is an optional decimal number specifying the count by which the
            //    appropriate action should take place.
            //    The pointer pszBuffer points us to the p, <ESC> and [ are
            //    already 'consumed'
    
            //    TITUS: Simplification of the code: Assume default count of 1 in case
            //    there are no parameters.
            char tmpc;
            bool b_missing_param=true;
            const int nParam = 10;    // Maximum number of parameters
            int[]    iParam =new int[nParam]{1,0,0,0,0,0,0,0,0,0};    // Assume 1 Parameter, Default 1

            int iCurrentParam = 0;
            int flag = 0;
            int end_of_escape=escape_length+pos;
            // Get parameters from escape sequence.
            while (((tmpc = txt_array[pos])<='?')&&(pos<end_of_escape))
            {
                if(tmpc<'0'||tmpc >'9') 
                {
                    // Check for parameter delimiter.
                    if(tmpc == ';') 
                    {
                        if(txt_array[pos - 1] == '[')
                            b_missing_param=true;
                        pos++;
                        continue;
                    }
                    // It is legal to have control characters inside ANSI sequences
                    // (Paul Brannan 6/26/98)
                    if(tmpc < 0x32) 
                    {
                        this.write(tmpc);
                        pos++;
                        continue;
                    }

                    // A new way of handling flags (Paul Brannan 12/2/98)
                    switch(tmpc) 
                    {
                        case '$': flag |= FLAG_DOLLAR; break;
                        case '?': flag |= FLAG_QMARK; break;
                        case '>': flag |= FLAG_GREATER; break;
                        case '<': flag |= FLAG_LESS; break;
                        case '!': flag |= FLAG_EXCLAM; break;
                        case '&': flag |= FLAG_AMPERSAND; break;
                        case '/': flag |= FLAG_SLASH; break;
                        case '=': flag |= FLAG_EQUAL; break;
                        case '\"': flag |= FLAG_QUOTE; break;
                        default: flag |= FLAG_OTHER; break;
                    }
                    pos++;
                }
                //  Got Numerical Parameter.
                char[] tmp_array=new char[escape_length];
                System.Array.Copy(txt_array,pos,tmp_array,0,Math.Min(escape_length,txt_array.Length-pos));
                string s=new string(tmp_array);
                s=System.Text.RegularExpressions.Regex.Match(s,"^[0-9]*").Value;
                iParam[iCurrentParam] = System.Convert.ToInt32(s);
                pos+=s.Length;
                if (iCurrentParam < nParam)
                    iCurrentParam++;
            }
    
            //~~~ TITUS: Apparently the digit is optional (look at termcap or terminfo)
            // So: If there is no digit, assume a count of 1
            switch (txt_array[pos]) 
            {
                    // Insert Character
                case '@':
                    if(iParam[0] == 0)
                        iParam[0] = 1; // Paul Brannan 9/1/98
                    pos++;
                    this.insert_char(txt_array[pos],iParam[0]);
                    pos++;
                    break;
                    // Move cursor up.
                case 'A':
                    if(iParam[0] == 0)
                        iParam[0] = 1;
                    this.move_cursor_pos(0, -iParam[0]);
                    pos++;
                    break;
                    // Move cursor down.
                    // Added by I.Ioannou 06 April, 1997
                case 'B':
                case 'e':
                    if(iParam[0] == 0)
                        iParam[0] = 1;
                    this.move_cursor_pos(0, iParam[0]);
                    pos++;
                    break;
                    // Move cursor right.
                    // Added by I.Ioannou 06 April, 1997
                case 'C':
                case 'a':
                    // Handle cursor size sequences (Jose Cesar Otero Rodriquez and
                    // Paul Brannan, 3/27/1999)
                    if((flag & FLAG_EQUAL)!=0)
                    {
                        switch(iParam[0]) 
                        {
                            case 7: //this.SetCursorSize(50);
                                break;
                            case 11: //this.SetCursorSize(6);
                                break;
                            case 32: //this.SetCursorSize(0);
                                break;
                            default: //this.SetCursorSize(13);
                                break;
                        }
                    } 
                    else 
                    {
                        if(iParam[0] == 0)
                            iParam[0] = 1;
                        this.move_cursor_pos(iParam[0], 0);
                    }
                    pos++;
                    break;
                case 'D':// Move cursor left.
                    if(iParam[0] == 0)
                        iParam[0] = 1;
                    this.move_cursor_pos(-iParam[0], 0);
                    pos++;
                    break;
                    // Move cursor to beginning of line, p lines down.
                    // Added by I.Ioannou 06 April, 1997
                case 'E': 
                    this.move_cursor_pos(0,this.get_cursor_Y()+iParam[0]);
                    pos++;
                    break;
                    // Moves active position to beginning of line, p lines up
                    // Added by I.Ioannou 06 April, 1997
                    // With '=' this changes the default fg color (Paul Brannan 6/27/98)
                case 'F':
                    if((flag & FLAG_EQUAL)!=0)
                        this.set_default_background(iParam[0]);
                    else
                        this.move_cursor_pos(0,this.get_cursor_Y() -iParam[0]);
                    pos++;
                    break;
                    // Go to column p
                    // Added by I.Ioannou 06 April, 1997
                    // With '=' this changes the default bg color (Paul Brannan 6/27/98)
                case '`':
                case 'G': // 'G' is from Linux kernel sources
                    if((flag & FLAG_EQUAL)!=0)
                    {
                        this.set_default_background(iParam[0]);
                    } 
                    else 
                    {
                        if (iCurrentParam < 1)            // Alter Default
                            iParam[0] = 0;
                        // this was backward, and we should subtract 1 from x
                        // (Paul Brannan 5/27/98)
                        this.set_cursor_pos(iParam[0] - 1, this.get_cursor_Y());
                    }
                    pos++;
                    break;
                    // Set cursor position.
                case 'f': 
                case 'H':
                    if (iCurrentParam < 2 || iParam[1] < 1)
                        iParam[1] = 1;
                    this.set_cursor_pos(iParam[1] - 1, iParam[0] - 1);
                    pos++;
                    break;
                    // Clear screen
                case 'J': 
                    if ( iCurrentParam < 1 )
                        iParam[0] = 0;    // Alter Default
                    switch (iParam[0]) 
                    {
                        case 0:
                            this.clear_end_of_screen();
                            break;
                        case 1:
                            this.clear_begin_of_screen();
                            break;
                        case 2:
                            this.clear_screen();
                            this.set_cursor_pos(0, 0);
                            break;
                    }
                    pos++;
                    break;
                    // Clear line
                case 'K': 

                    if (iCurrentParam < 1)            // Alter Default
                        iParam[0] = 0;
                    switch (iParam[0]) 
                    {
                        case 0:
                            this.clear_end_of_line();
                            break;
                        case 1:
                            this.clear_begin_of_line();
                            break;
                        case 2:
                            this.clear_line();
                            break;
                    }
                    pos++;
                    break;
                case 'L': //  Insert p new, blank lines.
                {
                    bool b_insert_mode_backup=this.b_insert_mode;
                    this.b_insert_mode=true;
                    string s="";
                    for(int cnt=0;cnt<iParam[0];cnt++)
                        s+="\r\n";
                    this.write(s);
                    this.b_insert_mode=b_insert_mode_backup;
                    pos++;
                    break;
                }
                    
                    
                case 'M': //  Delete p lines.
                {
                    for (int cnt=0;cnt<iParam[0];cnt++)
                        this.erase_line();
                    pos++;
                    break;
                }
                case 'P': // DELETE CHAR
                    this.erase_char(iParam[0]);
                    pos++;
                    break;
                    // Scrolls screen up (down? -- PB) p lines,
                    // Added by I.Ioannou 06 April, 1997
                    // ANSI X3.64-1979 references this but I didn't
                    // found it in any telnet implementation
                    // note 05 Oct 97  : but SCO terminfo uses them, so uncomment them !!
                case 'S': 
                {
                    this.scroll(-iParam[0]);
                    pos++;
                    break;
                }
                    // Scrolls screen up p lines,
                    // Added by I.Ioannou 06 April, 1997
                    // ANSI X3.64-1979 references this but I didn't
                    // found it in any telnet implementation
                    // note 05 Oct 97  : but SCO terminfo uses them, so uncomment them !!
                case 'T': 
                {
                    this.scroll(iParam[0]);
                    pos++;
                    break;
                }
                    //  Erases p characters up to the end of line
                    // Added by I.Ioannou 06 April, 1997
                case 'X': 
                {
                    this.iSelectionStart+=iParam[0];
                    this.erase_char(iParam[0]);
                    pos++;
                    break;
                }
                case 'Z':// Go back p tab stops
                    pos++;
                    break;
                case 'c': // Get Terminal ID
                {   
                    this.al_reply_cmds.Add(hexa_convert.string_to_byte(this.get_terminal_ID()));
                    pos++;
                    break;
                }
                case 'b':// TITUS++ 2. November 1998: Repeat Character.
                    string last_char;
                    if (this.str_last_string.EndsWith("\r\n"))
                        last_char="\r\n";
                    else
                        last_char=this.str_last_string.Substring(this.str_last_string.Length-1,1);
                    for (int cnt=0;cnt<iParam[0];cnt++)
                        this.write(last_char);
                    pos++;
                    break;
                case 'd': // Go to line p Added by I.Ioannou 06 April, 1997
                    if (iCurrentParam < 1)// Alter Default
                        iParam[0] = 0;
                    // this was backward, and we should subtract 1 from y
                    // (Paul Brannan 5/27/98)
                    this.set_cursor_pos(this.get_cursor_X(), iParam[0] - 1);
                    pos++;
                    break;
                case 'g': // iBCS2 tab erase
                    if (iCurrentParam < 1)// Alter Default
                        iParam[0] = 0;
                    switch (iParam[0]) 
                    {
                        case 0:
                            // Clear the horizontal tab stop at the current active position
                            break;
                        case 2:
                            // I think this might be "set as default?"
                            break;
                        case 3:
                            // Clear all tab stops
                            break;
                    }
                    pos++;
                    break;
                case 'h': // Set extended mode
                    for (int i = 0; i < iCurrentParam; i++) 
                    {
                        // Changed to a switch statement (Paul Brannan 5/27/98)
                        if((flag & FLAG_QMARK)!=0)
                        {
                            switch(iParam[i]) 
                            {
                                case 1: // App cursor keys
                                    this.b_app_cursor_key=true;
                                    break;
                                case 2: // VT102 mode
                                    break;
                                case 3: // 132 columns
                                    break;
                                case 4: // smooth scrolling
                                    break;
                                case 5: // Light background
                                    this.set_light_background();
                                    break;
                                case 6: // Stay in margins
                                    break;
                                case 7: // set line wrap
                                    break;
                                case 8:    // Auto-repeat keys
                                    break;
                                case 18: // Send FF to printer
                                    break;
                                case 19: // Entire screen legal for printer
                                    break;
                                case 25: // Visible cursor
                                    break;
                                case 66: // Application numeric keypad
                                    break;
                                default:
                                    break;
                            }
                        } 
                        else 
                        {
                            switch(iParam[i]) 
                            {
                                case 2: // Lock keyboard
                                    break;
                                case 3: // Act upon control codes (PB 12/5/98)
                                    break;
                                case 4: // Set insert mode
                                    this.b_insert_mode=true;
                                    break;
                                case 12: // Local echo off
                                    break;
                                case 20: // Newline sends cr/lf
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    pos++;
                    break;
                case 'i':// Print Screen
                    if (iCurrentParam < 1)
                        iParam[0]=0;
                    switch (iParam[0])
                    {
                        case 0: break; // Print Screen
                        case 1: break; // Print Line
                        case 4: break; // Stop Print Log
                        case 5: break; // Start Print Log
                    }
                    pos++;
                    break;
                case 'l': // Unset extended mode
                    for (int i = 0; i < iCurrentParam; i++) 
                    {
                        // Changed to a switch statement (Paul Brannan 5/27/98)
                        if((flag & FLAG_QMARK)!=0)
                        {
                            switch(iParam[i]) 
                            {
                                case 1: // Numeric cursor keys / out of App cursor keys
                                    this.b_app_cursor_key=false;
                                    break;
                                case 2: // VT52 mode
                                    break;
                                case 3: // 80 columns
                                    break;
                                case 4: // jump scrolling
                                    break;
                                case 5: // Dark background
                                    this.set_dark_background();
                                    break;
                                case 6: // Ignore margins
                                    break;
                                case 7: // unset line wrap
                                    break;
                                case 8:    // Auto-repeat keys
                                    break;
                                case 19: // Only send scrolling region to printer
                                    break;
                                case 25: // Invisible cursor
                                    break;
                                case 66: // Numeric keypad
                                    break;
                                default:
                                    break;
                            }
                        } 
                        else 
                        {
                            switch(iParam[i]) 
                            {
                                case 2: // Unlock keyboard
                                    break;
                                case 3: // Display control codes (PB 12/5/98)
                                    break;
                                case 4: // Set overtype mode
                                    this.b_insert_mode=false;
                                    break;
                                case 12: // Local echo on
                                    break;
                                case 20: // sends lf only
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    pos++;
                    break;
                case 'm':// Set color
                    if(b_missing_param)
                        this.set_normal_color();
                    if(iCurrentParam == 0) 
                    {
                        this.set_normal_color();
                    } 
                    else 
                    {
                        for(int i = 0; i < iCurrentParam; i++)
                            this.set_console_attribute(iParam[i]);
                    }
                    pos++;
                    break;
                case 'n': // report cursor position Row X Col
                    if (iCurrentParam == 1 && iParam[0]==5) 
                    {
                        // report the cursor position
                        this.al_reply_cmds.Add(hexa_convert.string_to_byte("\x1B[0n"));
                        break;
                    }
                    if (iCurrentParam == 1 && iParam[0]==6)
                    {
                        // report the cursor position
                        // The cursor position needs to be sent as a single string
                        // (Paul Brannan 6/27/98)
                        string szCursorReport=String.Format("\x1B[{0};{1}R",this.get_cursor_Y() + 1,this.get_cursor_X() + 1);
                        this.al_reply_cmds.Add(hexa_convert.string_to_byte(szCursorReport));
                    }
                    pos++;
                    break;
                case 'p':// Miscellaneous weird sequences (Paul Brannan 6/27/98)
                    // Set conformance level
                    if((flag & FLAG_QUOTE)!=0)
                    {
                        pos++;
                        break;
                    }
                    // Soft terminal reset
                    if((flag & FLAG_EXCLAM)!=0)
                    {
                        pos++;
                        break;
                    }
                    // Report mode settings
                    if((flag & FLAG_DOLLAR)!=0)
                    {
                        pos++;
                        break;
                    }
                    pos++;
                    break;
                case 'r': // Scroll Screen
                    if (iCurrentParam < 1) 
                    {
                        // Enable scrolling for entire display
                        pos++;
                        break;
                    }
                    if (iCurrentParam >1) 
                    {
                        // Enable scrolling from row1 to row2
                    }
                    // Move the cursor to the home position (Paul Brannan 12/2/98)
                    this.set_cursor_pos(0, 0);
                    pos++;
                    break;
                    // Save cursor position
                case 's': 
                    this.i_saved_cursor_Y=this.get_cursor_Y();
                    this.i_saved_cursor_X=this.get_cursor_X();
                    pos++;
                    break;
                    // Restore cursor position
                case 'u': 
                    this.set_cursor_pos(this.i_saved_cursor_Y, this.i_saved_cursor_Y);
                    pos++;
                    break;
                    // DEC terminal report (Paul Brannan 6/28/98)
                case 'x':
                    if(iParam[0]!=0)
                        this.al_reply_cmds.Add(hexa_convert.string_to_byte("\033[3;1;1;128;128;1;0x"));
                    else
                        this.al_reply_cmds.Add(hexa_convert.string_to_byte("\033[2;1;1;128;128;1;0x"));
                    pos++;
                    break;
                default:
                    pos++;
                    break;
            }
        }


        private void ParseEscape(char[] txt_array,ref int pos,int escape_length)
        {
            // Check if we have enough characters in buffer.
            if (escape_length < 2)
                return;
            int end_of_escape=escape_length+pos;

            // Decode the command.
            switch (txt_array[pos]) 
            {
                case 'A': // Cursor up
                    this.move_cursor_pos(0, -1);
                    break;
                case 'B': // Cursor down
                    this.move_cursor_pos(0, 1);
                    break;
                case 'C':// Cursor right
                    this.move_cursor_pos(1, 0);
                    break;
                case 'D':// LF *or* cursor left (Paul Brannan 6/27/98)
                    /*
                    if(vt52_mode)
                        this.move_cursor_pos(-1, 0);
                    else
                    */
                        this.write("\n");
                    break;
                case 'E':// CR/LF (Paul Brannan 6/26/98)
                    this.write("\r\n");
                    break;
                case 'F':// Special graphics char set (Paul Brannan 6/27/98)
                    break;
                case 'G':// ASCII char set (Paul Brannan 6/27/98)
                    break;
                case 'H': // Home cursor/tab set
                    this.go_home();
                    break;
                case 'I':// Reverse line feed
                    this.go_end();
                    break;
                case 'J': // Erase end of screen
                    this.clear_end_of_screen();
                    break;
                case 'K':// Erase EOL
                    this.clear_end_of_line();
                    break;
                case 'M':// Scroll Up one line //Reverse index
                    this.go_home();
                    break;
                case 'Y':// Direct cursor addressing
                    if ((end_of_escape - pos) >= 2)
                    {
                        // if we subtract '\x1F', then we may end up with a negative
                        // cursor position! (Paul Brannan 6/26/98)
                        set_cursor_pos((int)(txt_array[pos+1] - ' '),(int)(txt_array[pos] - ' '));
                        pos+=2;
                    } 
                    else 
                    {
                        pos--; // Paul Brannan 6/26/98
                    }
                    break;
                case 'Z':// Terminal ID Request
                {
                    this.al_reply_cmds.Add(hexa_convert.string_to_byte(this.get_terminal_ID()));
                    break;
                }
                case 'c':// reset terminal to defaults
                    this.reset_terminal();
                    break;
                case '=':// Enter alternate keypad mode
                    this.b_alternate_keypad_mode=true;
                    break;
                case '>':// Exit alternate keypad mode
                    this.b_alternate_keypad_mode=false;
                    break;
                case '<':// Enter ANSI mode
                    break;
                case '1':// Graphics processor on
                    break;
                case '#'://Line size commands
                    // (Paul Brannan 6/26/98)
                    if (pos < end_of_escape) 
                    {
                        pos++;
                        switch(txt_array[pos]) 
                        {
                            case '3': break; // top half of a double-height line
                            case '4': break; // bottom half of a double-height line
                            case '6': break; // current line becomes double-width
                            case '8': this.clear_screen();//this.clear_screen('E');
                                break;
                        }
                    } 
                    else 
                    {
                        pos--;
                    }            
                    break;
                case '2':// Graphics processor off
                    break;
                case '7':// Save cursor and attribs
                    this.i_saved_cursor_Y=this.get_cursor_Y();
                    this.i_saved_cursor_X=this.get_cursor_X();
                    this.i_saved_attributes = this.get_attributes();
                    break;
                case '8':// Restore cursor position and attribs
                    this.set_cursor_pos(this.i_saved_cursor_X, this.i_saved_cursor_Y);
                    this.set_attributes(this.i_saved_attributes);
                    break;
                case '(':// Set G0 map (Paul Brannan 6/25/98)
                    if (pos < end_of_escape) 
                    {
                        // G0_map=txt_array[pos]
                        pos++;
                    } 
                    else 
                    {
                        pos--;
                    }
                    break;
                case ')':// Set G1 map (Paul Brannan 6/25/98)
                    if (pos < end_of_escape) 
                    {
                        // G1_map=txt_array[pos]
                        pos++;
                    } 
                    else 
                    {
                        pos--;
                    }
                    break;
                    // This doesn't do anything, as far as I can tell, but it does take
                    // a parameter (Paul Brannan 6/27/98)
                case '%':
                    if (pos < end_of_escape) 
                    {
                        pos++;
                    } 
                    else 
                    {
                        pos--;
                    }
                    break;
                    
                case '[':// ANSI escape sequence
                case ']':
                case '~':// Meridian Terminal Emulator extension same as ANSI
                    pos++;
                    int pos2=pos;
                    while (((txt_array[pos2])<='?')&&(pos2<end_of_escape))
                        pos2++;
                    if (pos2 == end_of_escape)
                        pos2-=2;
                    else
                        this.ParseEscapeANSI(txt_array, ref pos,pos2-pos+1);
                    break;
                default:// can appear for \
                    if (txt_array[pos]!=0x21)
                    {
                        this.write(txt_array[pos]);
                        pos++;
                    }
                    break;
            }
        }

        #endregion
    }

    #region Ctelnet_option
    public class Ctelnet_option
    {
        public Telnet.TELNET_COMMANDS cmd=Telnet.TELNET_COMMANDS.DONT;
        public byte option=0;
        public byte[] param=null;
        public Ctelnet_option(Telnet.TELNET_COMMANDS cmd,byte option)
        {
            this.cmd=cmd;
            this.option=option;
        }
        public byte[] to_byte_array()
        {
            byte[] ret;
            if (param==null)
            {
                ret=new byte[3];
                ret[0]=(byte)Telnet.TELNET_COMMANDS.IAC;
                ret[1]=(byte)this.cmd;
                ret[2]=this.option;
            }
            else
            {
                ret=new byte[7+param.Length];
                ret[0]=(byte)Telnet.TELNET_COMMANDS.IAC;
                ret[1]=(byte)this.cmd;
                ret[2]=this.option;
                ret[3]=(byte)Telnet.TELNET_COMMANDS.IAC;
                ret[4]=(byte)Telnet.TELNET_COMMANDS.SB;
                System.Array.Copy(this.param,0,ret,5,param.Length);
                ret[5+this.param.Length]=(byte)Telnet.TELNET_COMMANDS.IAC;
                ret[6+this.param.Length]=(byte)Telnet.TELNET_COMMANDS.SE;
            }
            return ret;
        }
        public bool should_option_be_implemented()
        {
            switch(this.cmd)
            {
                case Telnet.TELNET_COMMANDS.WILL:
                case Telnet.TELNET_COMMANDS.DO:
                    return true;
                case Telnet.TELNET_COMMANDS.WONT:
                case Telnet.TELNET_COMMANDS.DONT:
                    return false;
                default:
                    return false;
            }
        }
        public void set_parameter(byte[] param)
        {
            // add param to last option
            System.Array.Copy(param,0,this.param,0,param.Length);
        }
    }
    #endregion
}
