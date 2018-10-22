using System;


namespace easy_socket.proxy
{
    public delegate void ClassProxy_Error_EventHandler(ClassProxy sender, easy_socket.tcp.EventArgs_Exception e);
    public delegate void ClassProxy_DataArrival_EventHandler(ClassProxy sender, easy_socket.tcp.EventArgs_ReceiveDataSocket e);
    public delegate void ClassProxy_Closed_by_Remote_Side_EventHandler(ClassProxy sender, EventArgs e);// no event args
    public delegate void ClassProxy_Connected_To_Remote_Host_EventHandler(ClassProxy sender, EventArgs e);// no event args
    public delegate void ClassProxy_Send_Progress_EventHandler(ClassProxy sender, easy_socket.tcp.EventArgs_Send_Progress_Socket_Data e);
    public delegate void ClassProxy_Send_Completed_EventHandler(ClassProxy sender, EventArgs e);// no event args
    /// <summary>
    /// Summary description for ClassProxy.
    /// </summary>
    public class ClassProxy
    {
        #region events
        public event ClassProxy_Error_EventHandler event_ClassProxy_Error;
        public event ClassProxy_DataArrival_EventHandler event_ClassProxy_DataArrival;
        public event ClassProxy_Closed_by_Remote_Side_EventHandler event_ClassProxy_Closed_by_Remote_Side;
        public event ClassProxy_Connected_To_Remote_Host_EventHandler event_ClassProxy_Connected_To_Remote_Host;
        public event ClassProxy_Send_Progress_EventHandler event_ClassProxy_Send_Progress;
        public event ClassProxy_Send_Completed_EventHandler event_ClassProxy_Send_Completed;
        #endregion

        #region enum
        private enum PROXY_CONNECTION_STATE:byte
        {
            CLOSED=0,
            CONNECTED,
            PROXY_NEGOCIATION,
            READY_TO_SEND_USER_DATA
        }

        public enum PROXY_TYPE:byte
        {
            NONE=0,
            HTTP_GET,
            HTTP_CONNECT,
            SOCKS4,
            SOCKS5
        }
        #endregion

        #region private members

        private System.Threading.AutoResetEvent hevt_connection_state_unlocked;
        private System.Threading.AutoResetEvent hevt_connected;
        private System.Threading.AutoResetEvent hevt_error;
        private System.Threading.AutoResetEvent hevt_remotely_closed;
        private System.Threading.AutoResetEvent hevt_data_arrival;
        
        private PROXY_CONNECTION_STATE connection_state;
        private easy_socket.tcp.Socket_Data socket;
        private byte[] last_data;

        #endregion

        #region public members
        public PROXY_TYPE type=PROXY_TYPE.NONE;
        public string target_ip="127.0.0.1";
        public ushort target_port=80;
        public string proxy_ip="127.0.0.1";
        public ushort proxy_port=80;
        public long proxy_time_out=20;// timeout in sec

        #endregion

        #region constructors

        public ClassProxy()
        {
            this.hevt_connected=new System.Threading.AutoResetEvent(false);
            this.hevt_data_arrival=new System.Threading.AutoResetEvent(false);
            this.hevt_remotely_closed=new System.Threading.AutoResetEvent(false);
            this.hevt_error=new System.Threading.AutoResetEvent(false);
            this.hevt_connection_state_unlocked=new System.Threading.AutoResetEvent(true);

            this.connection_state=PROXY_CONNECTION_STATE.CLOSED;
            this.socket=new easy_socket.tcp.Socket_Data();
            this.socket.event_Socket_Data_Error+=new easy_socket.tcp.Socket_Data_Error_EventHandler(socket_event_Socket_Data_Error);
            this.socket.event_Socket_Data_DataArrival+=new easy_socket.tcp.Socket_Data_DataArrival_EventHandler(socket_event_Socket_Data_DataArrival);
            this.socket.event_Socket_Data_Connected_To_Remote_Host+=new easy_socket.tcp.Socket_Data_Connected_To_Remote_Host_EventHandler(socket_event_Socket_Data_Connected_To_Remote_Host);
            this.socket.event_Socket_Data_Closed_by_Remote_Side+=new easy_socket.tcp.Socket_Data_Closed_by_Remote_Side_EventHandler(socket_event_Socket_Data_Closed_by_Remote_Side);
            this.socket.event_Socket_Data_Send_Completed+=new easy_socket.tcp.Socket_Data_Send_Completed_EventHandler(socket_event_Socket_Data_Send_Completed);
            this.socket.event_Socket_Data_Send_Progress+=new easy_socket.tcp.Socket_Data_Send_Progress_EventHandler(socket_event_Socket_Data_Send_Progress);
        }
        public ClassProxy(PROXY_TYPE type,string proxy_ip,ushort proxy_port):this()
        {
            this.type=type;
            this.proxy_ip=proxy_ip;
            this.proxy_port=proxy_port;
        }
        #endregion

        private PROXY_CONNECTION_STATE ConnectionState
        {
            get
            {
                PROXY_CONNECTION_STATE cs;
                // wait until connection state is unlocked
                this.hevt_connection_state_unlocked.WaitOne();
                //read
                cs=this.connection_state;
                // release connection state
                this.hevt_connection_state_unlocked.Set();
                return cs;
            }
            set
            {
                // wait until connection state is unlocked
                this.hevt_connection_state_unlocked.WaitOne();
                //read
                this.connection_state=value;
                // release connection state
                this.hevt_connection_state_unlocked.Set();
            }
        }

        #region send
        public bool send(string target_ip,ushort target_port,string query)
        {
            this.target_ip=target_ip;
            this.target_port=target_port;
            return this.send(query);
        }
        public bool send(string query)
        {
            long time_ticks;
            int data_size;
            int iret_waitany=-1;
            int pos_end_of_header=-1;
            string str="";
            byte[] data;
            byte[] tmp_data;
            System.Net.IPAddress ipaddr;
            System.Net.IPHostEntry iphe;
            this.hevt_connected.Reset();
            this.hevt_data_arrival.Reset();
            this.hevt_remotely_closed.Reset();
            this.hevt_error.Reset();
            System.Threading.WaitHandle[] waithandles={this.hevt_connected,this.hevt_data_arrival,this.hevt_remotely_closed,this.hevt_error};
            switch(this.type)
            {
                case PROXY_TYPE.NONE:
                    if (this.ConnectionState!=PROXY_CONNECTION_STATE.READY_TO_SEND_USER_DATA)
                    {
                        this.socket.connect(this.target_ip,this.target_port);
                        iret_waitany=System.Threading.WaitHandle.WaitAny(waithandles);
                        if (iret_waitany!=0)// connected
                            return false;
                        this.ConnectionState=PROXY_CONNECTION_STATE.READY_TO_SEND_USER_DATA;
                    }
                    this.socket.send(query);
                    break;
                case PROXY_TYPE.HTTP_GET:
                    if (this.ConnectionState!=PROXY_CONNECTION_STATE.READY_TO_SEND_USER_DATA)
                    {
                        this.socket.connect(this.proxy_ip,this.proxy_port);
                        iret_waitany=System.Threading.WaitHandle.WaitAny(waithandles);
                        if (iret_waitany!=0)// connected
                            return false;
                        this.ConnectionState=PROXY_CONNECTION_STATE.READY_TO_SEND_USER_DATA;
                    }
                    this.socket.send(this.modify_query(query));
                    break;
                case PROXY_TYPE.HTTP_CONNECT:
                    if (this.ConnectionState!=PROXY_CONNECTION_STATE.READY_TO_SEND_USER_DATA)
                    {
                        this.socket.connect(this.proxy_ip,this.proxy_port);
                        iret_waitany=System.Threading.WaitHandle.WaitAny(waithandles);
                        if (iret_waitany!=0)// connected
                            return false;
                        this.ConnectionState=PROXY_CONNECTION_STATE.PROXY_NEGOCIATION;
                        this.socket.send("CONNECT "+this.target_ip+":"+this.target_port+" HTTP/1.1\r\n"+"HOST:"+this.target_ip+":"+this.target_port+"\r\n\r\n");
                        data=null;
                        time_ticks=System.Environment.TickCount;
                        // loop until full header is received or error occurs
                        while (pos_end_of_header<0)
                        {
                            iret_waitany=System.Threading.WaitHandle.WaitAny(waithandles);
                            if (iret_waitany!=1)// data arrival
                                return false;
                            if (data==null)
                                data=this.last_data;
                            else
                            {
                                tmp_data=data;
                                data=new byte[tmp_data.Length+this.last_data.Length];
                                System.Array.Copy(tmp_data,0,data,0,tmp_data.Length);
                                System.Array.Copy(this.last_data,0,data,tmp_data.Length,this.last_data.Length);
                            }
                            str=System.Text.Encoding.ASCII.GetString(data, 0,data.Length).ToUpper();
                            pos_end_of_header=str.IndexOf("\r\n\r\n");
                            if ((System.Environment.TickCount-time_ticks)>(proxy_time_out*1000))
                            {
                                if(this.event_ClassProxy_Error!=null)
                                    this.event_ClassProxy_Error(this,new easy_socket.tcp.EventArgs_Exception(new Exception("Proxy Time Out")));
                                return false;
                            }
                        }
                        string str_header=str.Substring(0,pos_end_of_header);
                        
                        if (!System.Text.RegularExpressions.Regex.IsMatch(str_header,"HTTP/[0-9]+.[0-9]+ 2[0-9]{2}",System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                        {
                            if(this.event_ClassProxy_Error!=null)
                                this.event_ClassProxy_Error(this,new easy_socket.tcp.EventArgs_Exception(new Exception("Bad proxy reply: "+str)));
                            return false;
                        }
                        this.ConnectionState=PROXY_CONNECTION_STATE.READY_TO_SEND_USER_DATA;
                    }
                    this.socket.send(query);
                    break;
                case PROXY_TYPE.SOCKS4:
                    if (this.ConnectionState!=PROXY_CONNECTION_STATE.READY_TO_SEND_USER_DATA)
                    {
                        this.socket.connect(this.proxy_ip,this.proxy_port);
                        iret_waitany=System.Threading.WaitHandle.WaitAny(waithandles);
                        if (iret_waitany!=0)// connected
                            return false;
                        this.ConnectionState=PROXY_CONNECTION_STATE.PROXY_NEGOCIATION;
                        data=new byte[9];
                        // version number 4
                        data[0]=4;
                        // command code 1 (connect)
                        data[1]=1;
                        // dest port
                        System.Array.Copy(System.BitConverter.GetBytes(easy_socket.network_convert.switch_ushort(this.target_port)),0,data,2,2);
                        // dest ip
                        try
                        {
                            // don't resolve if ip is like xxx.yyy.uuu.vvv
                            ipaddr=System.Net.IPAddress.Parse(this.target_ip);
                        }
                        catch
                        {
                            // else resolve
                            try
                            {
                                iphe=System.Net.Dns.Resolve(this.target_ip);
                            }
                            catch(Exception e)
                            {
                                if(this.event_ClassProxy_Error!=null)
                                    this.event_ClassProxy_Error(this,new easy_socket.tcp.EventArgs_Exception(e));
                                return false;
                            }
                            ipaddr=iphe.AddressList[0];
                        }
                        System.Array.Copy(ipaddr.GetAddressBytes(),0,data,4,4);
                        // user id (none)
                        // null char
                        data[8]=0;
                        this.socket.send(data);
                        data_size=0;

                        time_ticks=System.Environment.TickCount;
                        data=null;
                        // loop until full header is received or error occurs
                        while (data_size<8)
                        {
                            iret_waitany=System.Threading.WaitHandle.WaitAny(waithandles);
                            if (iret_waitany!=1)// data arrival
                                return false;
                            if (data==null)
                                data=this.last_data;
                            else
                            {
                                tmp_data=data;
                                data=new byte[tmp_data.Length+this.last_data.Length];
                                System.Array.Copy(tmp_data,0,data,0,tmp_data.Length);
                                System.Array.Copy(this.last_data,0,data,tmp_data.Length,this.last_data.Length);
                            }
                            data_size=data.Length;
                            if ((System.Environment.TickCount-time_ticks)>(proxy_time_out*1000))
                            {
                                if(this.event_ClassProxy_Error!=null)
                                    this.event_ClassProxy_Error(this,new easy_socket.tcp.EventArgs_Exception(new Exception("Proxy Time Out")));
                                return false;
                            }
                        }
                        // vn should = 0
                        if (data[0]!=0)
                        {
                            if(this.event_ClassProxy_Error!=null)
                                this.event_ClassProxy_Error(this,new easy_socket.tcp.EventArgs_Exception(new Exception("Bad proxy handshake")));
                            return false;
                        }

                        // code should be 90
                        if (data[1]!=90)
                        {
                            if(this.event_ClassProxy_Error!=null)
                                this.event_ClassProxy_Error(this,new easy_socket.tcp.EventArgs_Exception(new Exception("Proxy error code :"+data[1]+". ")));
                            return false;
                        }
                        // dst port
                        // dst ip
                        this.ConnectionState=PROXY_CONNECTION_STATE.READY_TO_SEND_USER_DATA;
                    }
                    this.socket.send(query);

                    break;
                case PROXY_TYPE.SOCKS5:
                    if (this.ConnectionState!=PROXY_CONNECTION_STATE.READY_TO_SEND_USER_DATA)
                    {
                        this.socket.connect(this.proxy_ip,this.proxy_port);
                        iret_waitany=System.Threading.WaitHandle.WaitAny(waithandles);
                        if (iret_waitany!=0)// connected
                            return false;
                        this.ConnectionState=PROXY_CONNECTION_STATE.PROXY_NEGOCIATION;
                        data=new byte[3];
                        // version number 5
                        data[0]=5;
                        data[1]=1;// number of methods
                        data[2]=0;// ask for NO AUTHENTICATION REQUIRED 
                        this.socket.send(data);

                        data_size=0;
                        time_ticks=System.Environment.TickCount;
                        data=null;
                        // loop until full header is received or error occurs
                        while (data_size<2)
                        {
                            iret_waitany=System.Threading.WaitHandle.WaitAny(waithandles);
                            if (iret_waitany!=1)// data arrival
                                return false;
                            if (data==null)
                                data=this.last_data;
                            else
                            {
                                tmp_data=data;
                                data=new byte[tmp_data.Length+this.last_data.Length];
                                System.Array.Copy(tmp_data,0,data,0,tmp_data.Length);
                                System.Array.Copy(this.last_data,0,data,tmp_data.Length,this.last_data.Length);
                            }
                            data_size=data.Length;
                            if ((System.Environment.TickCount-time_ticks)>(proxy_time_out*1000))
                            {
                                if(this.event_ClassProxy_Error!=null)
                                    this.event_ClassProxy_Error(this,new easy_socket.tcp.EventArgs_Exception(new Exception("Proxy Time Out")));
                                return false;
                            }
                        }

                        // vn should = 5
                        if (data[0]!=5)
                        {
                            if(this.event_ClassProxy_Error!=null)
                                this.event_ClassProxy_Error(this,new easy_socket.tcp.EventArgs_Exception(new Exception("Bad proxy handshake")));
                            return false;
                        }
                        //X'00' NO AUTHENTICATION REQUIRED 
                        //X'01' GSSAPI 
                        //X'02' USERNAME/PASSWORD 
                        //X'03' to X'7F' IANA ASSIGNED 
                        //X'80' to X'FE' RESERVED FOR PRIVATE METHODS 
                        //X'FF' NO ACCEPTABLE METHODS 
                        if (data[1]!=0)
                        {
                            if(this.event_ClassProxy_Error!=null)
                                this.event_ClassProxy_Error(this,new easy_socket.tcp.EventArgs_Exception(new Exception("No authentication not allowed")));
                            return false;
                        }
                        // No Authentication accepted
                        data=new byte[10];
                        data[0]=5;// version
                        data[1]=1;// connect cmd
                        data[2]=0;// reserved
                        data[3]=1;// adress type=ipv4
                        try
                        {
                            // don't resolve if ip is like xxx.yyy.uuu.vvv
                            ipaddr=System.Net.IPAddress.Parse(this.target_ip);
                        }
                        catch
                        {
                            // else resolve
                            try
                            {
                                iphe=System.Net.Dns.Resolve(this.target_ip);
                            }
                            catch(Exception e)
                            {
                                if(this.event_ClassProxy_Error!=null)
                                    this.event_ClassProxy_Error(this,new easy_socket.tcp.EventArgs_Exception(e));
                                return false;
                            }
                            ipaddr=iphe.AddressList[0];
                        }
                        // dest ip
                        System.Array.Copy(ipaddr.GetAddressBytes(),0,data,4,4);
                        // dest port
                        System.Array.Copy(System.BitConverter.GetBytes(easy_socket.network_convert.switch_ushort(this.target_port)),0,data,8,2);// warning 8 only because ipv4
                        this.socket.send(data);

                        data_size=0;
                        time_ticks=System.Environment.TickCount;
                        data=null;
                        // loop until full header is received or error occurs
                        while (data_size<10)
                        {
                            iret_waitany=System.Threading.WaitHandle.WaitAny(waithandles);
                            if (iret_waitany!=1)// data arrival
                                return false;
                            if (data==null)
                                data=this.last_data;
                            else
                            {
                                tmp_data=data;
                                data=new byte[tmp_data.Length+this.last_data.Length];
                                System.Array.Copy(tmp_data,0,data,0,tmp_data.Length);
                                System.Array.Copy(this.last_data,0,data,tmp_data.Length,this.last_data.Length);
                            }
                            data_size=data.Length;
                            if ((System.Environment.TickCount-time_ticks)>(proxy_time_out*1000))
                            {
                                if(this.event_ClassProxy_Error!=null)
                                    this.event_ClassProxy_Error(this,new easy_socket.tcp.EventArgs_Exception(new Exception("Proxy Time Out")));
                                return false;
                            }
                        }
                        // vn should = 5
                        if (data[0]!=5)
                        {
                            if(this.event_ClassProxy_Error!=null)
                                this.event_ClassProxy_Error(this,new easy_socket.tcp.EventArgs_Exception(new Exception("Bad proxy handshake")));
                            return false;
                        }

                        switch (data[1])
                        {
                            case 0://successfull
                                //"Successfully Connected to remote host"
                                this.ConnectionState=PROXY_CONNECTION_STATE.READY_TO_SEND_USER_DATA;
                                break;
                            case 1:
                                if(this.event_ClassProxy_Error!=null)
                                    this.event_ClassProxy_Error(this,new easy_socket.tcp.EventArgs_Exception(new Exception("General SOCKS server failure.")));
                                return false;
                            case 2:
                                if(this.event_ClassProxy_Error!=null)
                                    this.event_ClassProxy_Error(this,new easy_socket.tcp.EventArgs_Exception(new Exception("Connection not allowed by ruleset.")));
                                return false;
                            case 3:
                                if(this.event_ClassProxy_Error!=null)
                                    this.event_ClassProxy_Error(this,new easy_socket.tcp.EventArgs_Exception(new Exception("Network unreachable.")));
                                return false;
                            case 4:
                                if(this.event_ClassProxy_Error!=null)
                                    this.event_ClassProxy_Error(this,new easy_socket.tcp.EventArgs_Exception(new Exception("Host unreachable.")));
                                return false;
                            case 5:
                                if(this.event_ClassProxy_Error!=null)
                                    this.event_ClassProxy_Error(this,new easy_socket.tcp.EventArgs_Exception(new Exception("Connection refused.")));
                                return false;
                            case 6:
                                if(this.event_ClassProxy_Error!=null)
                                    this.event_ClassProxy_Error(this,new easy_socket.tcp.EventArgs_Exception(new Exception("TTL expired.")));
                                return false;
                            case 7:
                                if(this.event_ClassProxy_Error!=null)
                                    this.event_ClassProxy_Error(this,new easy_socket.tcp.EventArgs_Exception(new Exception("Command not supported.")));
                                return false;
                            case 8:
                                if(this.event_ClassProxy_Error!=null)
                                    this.event_ClassProxy_Error(this,new easy_socket.tcp.EventArgs_Exception(new Exception("Address type not supported.")));
                                return false;
                            default:
                                if(this.event_ClassProxy_Error!=null)
                                    this.event_ClassProxy_Error(this,new easy_socket.tcp.EventArgs_Exception(new Exception("Unknown error code.")));
                                return false;
                        }
                    }
                    this.socket.send(query);
                    break;
            }
            return true;
        }
        private string modify_query(string query)
        {
            bool b_need_get;
            bool b_need_http;
            bool b_need_2crlf;
            bool b_full_header_only;
            bool b_host_is_in_header;
            string str_get="GET ";
            string str_http=" HTTP/1.1\r\n";
            string str_host="HOST:"+this.target_ip+":"+this.target_port+"\r\n";
            string str_request;
            string str_head="";
            int pos_http;
            int pos_end_of_first_line;
            int pos_end_of_header;
            int pos_host;
            int pos_end_of_next_line;
                    
            string str=query.ToUpper();

            // look for 'GET ' HTTP/ and "\r\n\r\n"
            b_need_get=!str.StartsWith("GET ");
            pos_http=str.IndexOf(" HTTP/");
            // get pos of end of header
            pos_end_of_header=query.IndexOf("\r\n\r\n");
            b_full_header_only=((pos_end_of_header+4)==str.Length);
            // get pos of first end of line
            pos_end_of_first_line=query.IndexOf("\r\n");
            b_need_http=((pos_http<0)||((pos_http>pos_end_of_first_line)&&(pos_end_of_first_line>0)));//http is present before \r\n\r\n
            b_need_2crlf=(pos_end_of_header<0);

            // remove "host:" if any in the request
            pos_host=str.IndexOf("HOST");
            if (pos_end_of_header<0)
                b_host_is_in_header=(pos_host>0);
            else
                b_host_is_in_header=((pos_host>0)&&(pos_host<pos_end_of_header));
            if (b_host_is_in_header)
            {
                pos_end_of_next_line=query.IndexOf("\r\n",pos_host);
                if ((pos_end_of_next_line<0)||(pos_end_of_next_line==pos_end_of_header))
                    query=query.Substring(0,pos_host);
                else
                    query=query.Substring(0,pos_host)+query.Substring(pos_end_of_next_line+2);
                // comput pos_end_of_header and pos_end_of_first_line
                // get pos of end of header
                pos_end_of_header=query.IndexOf("\r\n\r\n");
                b_full_header_only=((pos_end_of_header+4)==str.Length);
                // get pos of first end of line
                pos_end_of_first_line=query.IndexOf("\r\n");
            }

            //as header is in ascii format
            if (b_need_get)
                str_head+=str_get;
            if (b_need_2crlf)
                pos_end_of_header=query.Length;
            else
            {
                // there's header and data
                // split on "\r\n\r\n to keep header only
                str=str.Substring(0,pos_end_of_header);
                // get pos of first end of line
                pos_end_of_first_line=str.IndexOf("\r\n");
            }
                    
            if (pos_end_of_first_line<0) // only 1 line
            {
                str_request=query;
                if (str_request.Trim()=="")
                    str_request="/";
                str_head+=str_request;
                if (b_need_http)
                    str_head+=str_http;
                else
                    str_head+="\r\n";
                str_head+=str_host;
            }
            else
            {
                str_request=query.Substring(0,pos_end_of_first_line);
                if (str_request.Trim()=="")
                    str_request="/";
                str_head+=str_request;
                if (b_need_http)
                    str_head+=str_http;
                else
                {
                    if (!str_head.EndsWith("\r\n"))
                        str_head+="\r\n";
                }
                str_head+=str_host;
                str_head+=query.Substring(pos_end_of_first_line+2,pos_end_of_header-pos_end_of_first_line-2);
            }
            // ensure header ends with 2 and only 2 crlf
            if (!str_head.EndsWith("\r\n"))
                str_head+="\r\n";
            str_head+="\r\n";

            return str_head+query.Substring(pos_end_of_header+4);
        }
        #endregion

        /// <summary>
        /// usefull if http keep alive, or not http connection (proxy socks or direct connection)
        /// </summary>
        public void close()
        {
            this.socket.close();
            this.ConnectionState=PROXY_CONNECTION_STATE.CLOSED;
        }

        #region tcp socket events handlers

        private void socket_event_Socket_Data_Error(easy_socket.tcp.Socket_Data sender, easy_socket.tcp.EventArgs_Exception e)
        {
            this.ConnectionState=PROXY_CONNECTION_STATE.CLOSED;
            this.hevt_error.Set();
            if(this.event_ClassProxy_Error!=null)
                this.event_ClassProxy_Error(this,e);
        }

        private void socket_event_Socket_Data_DataArrival(easy_socket.tcp.Socket_Data sender, easy_socket.tcp.EventArgs_ReceiveDataSocket e)
        {
            this.hevt_data_arrival.Set();
            this.last_data=e.buffer;
            if (this.ConnectionState==PROXY_CONNECTION_STATE.READY_TO_SEND_USER_DATA)
                if (this.event_ClassProxy_DataArrival!=null)
                    this.event_ClassProxy_DataArrival(this,e);
        }

        private void socket_event_Socket_Data_Connected_To_Remote_Host(easy_socket.tcp.Socket_Data sender, EventArgs e)
        {
            this.ConnectionState=PROXY_CONNECTION_STATE.CONNECTED;
            this.hevt_connected.Set();
            if (this.event_ClassProxy_Connected_To_Remote_Host!=null)
                this.event_ClassProxy_Connected_To_Remote_Host(this,e);
        }

        private void socket_event_Socket_Data_Closed_by_Remote_Side(easy_socket.tcp.Socket_Data sender, EventArgs e)
        {
            this.ConnectionState=PROXY_CONNECTION_STATE.CLOSED;
            this.hevt_remotely_closed.Set();
            if (this.event_ClassProxy_Closed_by_Remote_Side!=null)
                this.event_ClassProxy_Closed_by_Remote_Side(this,e);
        }

        private void socket_event_Socket_Data_Send_Completed(easy_socket.tcp.Socket_Data sender, EventArgs e)
        {
            if (this.ConnectionState==PROXY_CONNECTION_STATE.READY_TO_SEND_USER_DATA)
                if (this.event_ClassProxy_Send_Completed!=null)
                    this.event_ClassProxy_Send_Completed(this,e);
        }

        private void socket_event_Socket_Data_Send_Progress(easy_socket.tcp.Socket_Data sender, easy_socket.tcp.EventArgs_Send_Progress_Socket_Data e)
        {
            if (this.ConnectionState==PROXY_CONNECTION_STATE.READY_TO_SEND_USER_DATA)
                if (this.event_ClassProxy_Send_Progress!=null)
                    this.event_ClassProxy_Send_Progress(this,e);
        }
        #endregion
    }
}