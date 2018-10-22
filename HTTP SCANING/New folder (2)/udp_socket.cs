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
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace easy_socket.udp
{

    #region Socket_Server events
    public delegate void Socket_Server_Error_EventHandler(Socket_Server sender, EventArgs_Exception e);
    public delegate void Socket_Server_Data_Arrival_EventHandler(Socket_Server sender, EventArgs_ReceiveDataSocketUDP e);
    public delegate void Socket_Server_Stopped_EventHandler(Socket_Server sender, EventArgs e);// no event args
    public delegate void Socket_Server_Started_EventHandler(Socket_Server sender, EventArgs e);// no event args
    public delegate void Socket_Server_Send_Completed_EventHandler(Socket_Server sender, EventArgs_EndPoint e);
    public delegate void Socket_Server_Remote_Host_Unreachable(Socket_Server sender, EventArgs e);// no event args
    #endregion

    #region Socket_Data events

    public delegate void Socket_Data_Error_EventHandler(Socket_Data sender, EventArgs_Exception e);
    public delegate void Socket_Data_Send_Completed_EventHandler(Socket_Data sender, EventArgs e);// no event args

    #endregion

    #region eventArgs object


    public class EventArgs_EndPoint:EventArgs
    {
        public readonly EndPoint remote_host_EndPoint;
        public EventArgs_EndPoint(EndPoint remote_host_EndPoint)
        {
            this.remote_host_EndPoint=remote_host_EndPoint;
        }
    }

    /// <summary>
    /// used by Socket_Data_DataArrival_EventHandler
    /// contains buffer,buffer_size
    /// </summary>
    public class EventArgs_ReceiveDataSocketUDP:EventArgs
    {
        /*
        /// <summary>
        /// data in string
        /// </summary>
        public readonly string data;
        */
        /// <summary>
        /// data in byte[]
        /// </summary>
        public readonly byte[] buffer;
        /// <summary>
        /// data length for data or buffer
        /// </summary>
        public readonly int buffer_size;

        public readonly EndPoint remote_host_EndPoint;
        //public EventArgs_ReceiveDataSocket(string strdata,byte[] bytebuffer,int intdata_size)
        public EventArgs_ReceiveDataSocketUDP(byte[] buffer,int data_size,EndPoint remote_host_EndPoint)
        {
            //data=strdata;
            this.buffer=buffer;
            this.buffer_size=data_size;
            this.remote_host_EndPoint=remote_host_EndPoint;
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
            exception=e;
        }
    }
#endregion

    public class udp_send_object
    {
        public readonly byte[] buffer;
        public readonly EndPoint remote_endpoint;

        public udp_send_object(byte[] buffer,EndPoint remote_endpoint)
        {
            this.buffer=buffer;
            this.remote_endpoint=remote_endpoint;
        }
    }


    public class Socket_Server
    {
        #region events
        
        public event Socket_Server_Error_EventHandler event_Socket_Server_Error;
        public event Socket_Server_Data_Arrival_EventHandler event_Socket_Server_Data_Arrival;
        public event Socket_Server_Stopped_EventHandler event_Socket_Server_Stopped;
        public event Socket_Server_Started_EventHandler event_Socket_Server_Started;
        public event Socket_Server_Send_Completed_EventHandler event_Socket_Server_Send_Completed;
        public event Socket_Server_Remote_Host_Unreachable event_Socket_Server_Remote_Host_Unreachable;

        #endregion

        #region const
        
        private const int default_port=0;//11000;
        private const int default_buffer_size=4096;

        #endregion

        #region members

        private int port;
        private string ip;
        private int buffer_size;
        private Socket srvsocket;
        private System.Collections.Queue send_queue;
        private bool b_started;
        private System.Threading.ManualResetEvent evt_more_data_to_send=new System.Threading.ManualResetEvent(false);
        private System.Threading.AutoResetEvent evt_close=new System.Threading.AutoResetEvent(false);
        private System.Threading.AutoResetEvent evt_unlocked_send_queue=new System.Threading.AutoResetEvent(true);

        #endregion

        #region member access

        public int local_port
        {
            get
            {
                return port;
            }
        }
        public string local_ip
        {
            get
            {
                return ip;
            }
        }

        #endregion

        #region constructors

        private void common_constructor()
        {
            this.b_started=false;
            this.send_queue=new System.Collections.Queue();
        }

        public Socket_Server()
        {
            this.ip="";
            this.port=default_port;
            this.buffer_size=default_buffer_size;
            this.common_constructor();
        }


        public Socket_Server(int port)
        {
            this.ip="";
            this.port=port;
            this.buffer_size=default_buffer_size;
            this.common_constructor();
        }


        public Socket_Server(string ip)
        {
            this.ip=ip;
            this.port=default_port;
            this.buffer_size=default_buffer_size;
            this.common_constructor();
        }

        public Socket_Server(string ip,int port)
        {
            this.ip=ip;
            this.port=port;
            this.buffer_size=default_buffer_size;
            this.common_constructor();
        }

        public Socket_Server(int port,int buffer_size)
        {
            this.port=port;
            this.buffer_size=buffer_size;
            this.common_constructor();
        }

        public Socket_Server(string ip,int port,int buffer_size)
        {
            this.ip=ip;
            this.port=port;
            this.buffer_size=buffer_size;
            this.common_constructor();
        }


    #endregion

        #region methodes

        /// <summary>
        /// Start the server
        /// </summary>
        public void start()
        {
            try
            {
                //stop server if it was runing
                if (this.b_started)
                {
                    this.stop();
                }

                // make local end point
                IPHostEntry iphe;

                System.Net.IPAddress ipaddr;
                // resolve ip
                if (this.ip=="" || this.ip==null) 
                {
                    ipaddr=System.Net.IPAddress.Any;
                }
                else
                {
                    try
                    {
                        // don't resolve if ip is like xxx.yyy.uuu.vvv
                        ipaddr=System.Net.IPAddress.Parse(this.ip);
                    }
                    catch
                    {
                        // else resolve
                        try
                        {
                            iphe=System.Net.Dns.Resolve(this.ip);
                        }
                        catch(Exception e)
                        {
                            this.event_Socket_Server_Error(this,new EventArgs_Exception(e));
                            return;
                        }
                        ipaddr=iphe.AddressList[0];
                    }
                }
                // make end point
                System.Net.IPEndPoint ipep = new IPEndPoint(ipaddr,this.port);
                EndPoint ep = (EndPoint)ipep;

                // create socket
                this.srvsocket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp); //Socket s = new Socket(lep.Address.AddressFamily,SocketType.Dgram,ProtocolType.Udp);
                this.b_started=true;

                // bind to create server
                this.srvsocket.Bind(ep);
                this.port=((IPEndPoint)this.srvsocket.LocalEndPoint).Port;// in case of null port given

                // clear send queue
                this.send_queue.Clear();

                // start thread to send data
                ThreadStart myThreadStart = new ThreadStart(my_send);
                Thread myThread =new Thread(myThreadStart);
                myThread.Start();

                // start thread to reveive deata
                this.run_serveur();

                // send event_Socket_Server_Started
                if (this.event_Socket_Server_Started!=null)
                    this.event_Socket_Server_Started(this,new EventArgs());
                
            }

            catch (Exception e)
            {
                if (e is SocketException)
                {
                    SocketException se=(SocketException)e;
                    if (se.ErrorCode==10004) // close methode as been called
                    {
                        return;
                    }
                }
                // send event_Socket_Server_Error
                if (this.event_Socket_Server_Error!=null)
                    this.event_Socket_Server_Error(this,new EventArgs_Exception(e));
                // close socket
                this.stop(); // send event_Socket_Server_Stopped
            }
            
        }
        /// <summary>
        /// Stop the server
        /// </summary>
        public void stop()
        {
            try
            {
                this.evt_close.Set();
                if (this.b_started) 
                {
                    this.b_started=false;
                    this.srvsocket.Close();
                    if (this.event_Socket_Server_Stopped!=null)
                        this.event_Socket_Server_Stopped(this,new System.EventArgs());
                }
            }
            catch (Exception e)
            {
                if (this.event_Socket_Server_Error!=null)
                    this.event_Socket_Server_Error(this,new EventArgs_Exception(e));
            }
        }

        private void run_serveur()
        {
            ThreadStart myThreadStart = new ThreadStart(this.receive);
            Thread myThread =new Thread(myThreadStart);
            myThread.Start();
        }
        private void receive()
        {
            if (!this.b_started)// solve synchronisation troubles (when socket is closed in event_Socket_Server_Remote_Host_Unreachable)
                return;
            int bytes_received_size;
            //Declare IPEndPoint and EndPoint to capture the identity of the sending host
            IPEndPoint sender;
            EndPoint tempRemoteEP;
            // Creates a byte buffer to receive the message.
            byte[] buffer = new byte[buffer_size];
            // Creates an IpEndPoint to capture the identity of the sending host.
            sender = new IPEndPoint(IPAddress.Any, 0);
            tempRemoteEP = (EndPoint)sender;
            try
            {

                while (true)
                {
                    // Receives datagram from a remote host.  This call blocks.
                    bytes_received_size=this.srvsocket.ReceiveFrom(buffer, 0, buffer_size, SocketFlags.None,  ref tempRemoteEP);

                    //System.Windows.Forms.MessageBox.Show("srv SampleClient is connected through UDP."+ System.Text.Encoding.Default.GetString(buffer));

                    byte[] sized_buffer=new byte[bytes_received_size];
                    System.Array.Copy(buffer,sized_buffer,bytes_received_size);
                    // send event_Socket_Server_Data_Arrival
                    if (this.event_Socket_Server_Data_Arrival!=null)
                        try// avoid to close server on some programmer's error in the eventhandler function
                        {
                            this.event_Socket_Server_Data_Arrival(this,new EventArgs_ReceiveDataSocketUDP(sized_buffer,bytes_received_size,tempRemoteEP)); 
                        }
                        catch{}
                }

            }

            catch (Exception e)
            {
                if (e is SocketException)
                {
                    SocketException se=(SocketException)e;
                    if (se.ErrorCode==10004) // close methode as been called
                    {
                        return;
                    }
                    if (se.ErrorCode==10054) // connection reset by peer this error don't close socket
                    {
                        this.run_serveur(); // restart a receive loop
                        // send event_Socket_Server_Remote_Host_Unreachable
                        if (this.event_Socket_Server_Remote_Host_Unreachable!=null)
                            this.event_Socket_Server_Remote_Host_Unreachable(this,null);
                        return;
                    }
                }
                // send event_Socket_Server_Error
                if (this.event_Socket_Server_Error!=null)
                    this.event_Socket_Server_Error(this,new EventArgs_Exception(e));
                // close socket
                this.stop(); // send event_Socket_Server_Stopped
            }
        }

        #region send

        /// <summary>
        /// send data to remote host dequeueing send_queue
        /// send event_Socket_Data_Send_Completed
        /// </summary>
        private void my_send()
        {
            udp_send_object obj_send=null;

            try
            {
                byte[] buffer;
                System.Threading.WaitHandle[] wait_handles=new System.Threading.WaitHandle[2];
                wait_handles[0]=this.evt_close;
                wait_handles[1]=this.evt_more_data_to_send;
                int res_wait;

                for(;;)
                {
                    res_wait=System.Threading.WaitHandle.WaitAny(wait_handles);
                    if (res_wait==0)
                        // exit thread
                        return;
                    this.evt_more_data_to_send.Reset();
                    while(send_queue.Count>0) // while there's data to send
                    {
                        // dequeue data of send_queue
                        this.evt_unlocked_send_queue.WaitOne();
                        obj_send=(udp_send_object)send_queue.Dequeue();
                        this.evt_unlocked_send_queue.Set();
                        buffer=obj_send.buffer;
                        this.srvsocket.SendTo(buffer,0,buffer.Length, SocketFlags.None, obj_send.remote_endpoint);
                        if (this.event_Socket_Server_Send_Completed!=null)
                            this.event_Socket_Server_Send_Completed(this,new EventArgs_EndPoint(obj_send.remote_endpoint));
                    }
                }
            }
            catch (Exception e)
            {
                if (event_Socket_Server_Error!=null)
                    event_Socket_Server_Error(this,new EventArgs_Exception(e));

                // clear send queue
                this.evt_unlocked_send_queue.WaitOne();
                this.send_queue.Clear();
                this.evt_unlocked_send_queue.Set();
                // restart thread to send data
                ThreadStart myThreadStart = new ThreadStart(this.my_send);
                Thread myThread =new Thread(myThreadStart);
                myThread.Start();
            }
        }

        /// <summary>
        /// used for sending bytes
        /// launch thread for sending or enqueue data if a thread is allready used for sending
        /// </summary>
        /// <param name="buffer"></param>
        public void send(byte[] buffer,EndPoint remote_endpoint)
        {
            this.evt_unlocked_send_queue.WaitOne();
            this.send_queue.Enqueue(new udp_send_object(buffer,remote_endpoint));
            this.evt_unlocked_send_queue.Set();
            this.evt_more_data_to_send.Set();
        }
        /// <summary>
        /// used for sending bytes
        /// launch thread for sending or enqueue data if a thread is allready used for sending
        /// </summary>
        /// <param name="buffer"></param>
        public void send(byte[] buffer,string ip, int port)
        {
            
            IPEndPoint remote_ipep;
            System.Net.IPAddress ipaddr;
            // Create endpoint
            try
            {
                // don't resolve if ip is like xxx.yyy.uuu.vvv
                ipaddr=System.Net.IPAddress.Parse(ip);
            }
            catch
            {// else resolve
                IPHostEntry remote_iphe;
                try
                {
                    remote_iphe=System.Net.Dns.Resolve(ip);
                }
                catch(Exception e)
                {
                    this.event_Socket_Server_Error(this,new EventArgs_Exception(e));
                    return;
                }
                ipaddr=remote_iphe.AddressList[0];
            }
            remote_ipep = new IPEndPoint(ipaddr, port);
            EndPoint remote_ep=(EndPoint)remote_ipep;

            this.send(buffer,remote_ep);
        }
        /// <summary>
        /// used for sending string
        /// launch thread for sending or enqueue data if a thread is allready used for sending
        /// </summary>
        /// <param name="buffer"></param>
        public void send(string buffer,EndPoint remote_endpoint)
        {
            byte[] sendBytes;
            sendBytes = Encoding.Default.GetBytes(buffer);
            this.send(sendBytes,remote_endpoint);
        }

        /// <summary>
        /// used for sending string
        /// launch thread for sending or enqueue data if a thread is allready used for sending
        /// </summary>
        /// <param name="buffer"></param>
        public void send(string buffer,string ip,int port)
        {
            byte[] sendBytes;
            sendBytes = Encoding.Default.GetBytes(buffer);
            this.send(sendBytes,ip,port);
        }
        #endregion

    #endregion
        
    }

    public class Socket_Data
    {
        private const int TIMEOUT_SEND=30000;//30s
        #region events
        
        public event Socket_Data_Error_EventHandler event_Socket_Data_Error;
        public event Socket_Data_Send_Completed_EventHandler event_Socket_Data_Send_Completed;

        #endregion

        #region members

        private Socket data_socket;
        private System.Collections.Queue send_queue;
        private int local_port;
        private bool b_allow_broadcast=false;
        private bool b_start_send_thread=true;
        private EndPoint remote_ep;
        private System.Threading.ManualResetEvent evt_more_data_to_send=new System.Threading.ManualResetEvent(false);
        private System.Threading.AutoResetEvent evt_close=new System.Threading.AutoResetEvent(false);
        private System.Threading.AutoResetEvent evt_unlocked_send_queue=new System.Threading.AutoResetEvent(true);

        #endregion

        #region member access
        public int remote_port
        {
            get
            {
                return ((IPEndPoint)remote_ep).Port;
            }
        }

        public string remote_host
        {
            get
            {
                return ((IPEndPoint)remote_ep).Address.ToString();
            }
        }
        
        public bool allow_broadcast
        {
            set
            {
                this.b_allow_broadcast=value;
                if (this.b_allow_broadcast)
                {
                    try // needs administrative rights
                    {
                        // enable broadcast
                        this.data_socket.SetSocketOption( System.Net.Sockets.SocketOptionLevel.Socket, System.Net.Sockets.SocketOptionName.Broadcast,1);
                    }
                    catch (Exception e)
                    {
                        System.Windows.Forms.MessageBox.Show(e.Message,"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
                else
                {
                    try // needs administrative rights
                    {
                        // disable broadcast
                        this.data_socket.SetSocketOption( System.Net.Sockets.SocketOptionLevel.Socket, System.Net.Sockets.SocketOptionName.Broadcast,0);
                    }
                    catch (Exception e)
                    {
                        System.Windows.Forms.MessageBox.Show(e.Message,"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
            }
            get
            {
                return this.b_allow_broadcast;
            }
        }
        #endregion

        #region constructors

        private void common_constructor()
        {
            data_socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
            // create new send queue
            send_queue=new System.Collections.Queue();
        }

        private void common_constructor_local_port_specified()
        {
            common_constructor();
            try
            {
                IPEndPoint ipep = new IPEndPoint(System.Net.IPAddress.Any,local_port);
                EndPoint ep = (EndPoint)ipep;
            
                data_socket.Bind(ep);
            }
            catch (Exception e)
            {
                if (event_Socket_Data_Error!=null)
                    event_Socket_Data_Error(this,new EventArgs_Exception(e));
            }
        }

        public Socket_Data(string remote_host,int remote_port)
        {
            System.Net.IPAddress ipaddr;
            // Create endpoint
            try
            {
                // don't resolve if ip is like xxx.yyy.uuu.vvv
                ipaddr=System.Net.IPAddress.Parse(remote_host);
            }
            catch
            {// else resolve
                IPHostEntry remote_iphe;
                try
                {
                    remote_iphe=System.Net.Dns.Resolve(remote_host);
                }
                catch(Exception e)
                {
                    this.event_Socket_Data_Error(this,new EventArgs_Exception(e));
                    return;
                }
                ipaddr=remote_iphe.AddressList[0];
            }
            IPEndPoint remote_ipep = new IPEndPoint(ipaddr, remote_port);
            remote_ep=(EndPoint)remote_ipep;

            common_constructor();
        }

        public Socket_Data(IPEndPoint remote_ipep)
        {
            // Create endpoint
            remote_ep=(EndPoint)remote_ipep;
            common_constructor();
        }

        public Socket_Data(EndPoint remote_endpoint)
        {
            // get endpoint
            remote_ep=remote_endpoint;
            common_constructor();
        }

        /// <summary>
        /// Warning: Errors are not catch
        /// </summary>
        /// <param name="local_port"></param>
        /// <param name="remote_host"></param>
        /// <param name="remote_port"></param>
        public Socket_Data(int local_port,string remote_host,int remote_port)
        {
            // Create endpoint
            System.Net.IPAddress ipaddr;
            // Create endpoint
            try
            {
                // don't resolve if ip is like xxx.yyy.uuu.vvv
                ipaddr=System.Net.IPAddress.Parse(remote_host);
            }
            catch
            {// else resolve
                IPHostEntry remote_iphe;
                try
                {
                    remote_iphe=System.Net.Dns.Resolve(remote_host);
                }
                catch(Exception e)
                {
                    this.event_Socket_Data_Error(this,new EventArgs_Exception(e));
                    return;
                }
                ipaddr=remote_iphe.AddressList[0];
            }
            IPEndPoint remote_ipep = new IPEndPoint(ipaddr, remote_port);
            remote_ep=(EndPoint)remote_ipep;

            this.local_port=local_port;
            common_constructor_local_port_specified();
        }

        /// <summary>
        /// Warning: Errors are not catch
        /// </summary>
        /// <param name="local_port"></param>
        /// <param name="remote_ipep"></param>
        public Socket_Data(int local_port,IPEndPoint remote_ipep)
        {
            // Create endpoint
            remote_ep=(EndPoint)remote_ipep;
            this.local_port=local_port;
            common_constructor_local_port_specified();
        }
        /// <summary>
        /// Warning: Errors are not catch
        /// </summary>
        /// <param name="local_port"></param>
        /// <param name="remote_endpoint"></param>
        public Socket_Data(int local_port,EndPoint remote_endpoint)
        {
            // get endpoint
            remote_ep=remote_endpoint;
            this.local_port=local_port;
            common_constructor_local_port_specified();
        }

        #endregion

        #region methodes

            #region send

            /// <summary>
            /// send data to remote host dequeueing send_queue
            /// send event_Socket_Data_Send_Completed
            /// </summary>
            private void my_send()
            {
                
                try
                {
                    byte[] buffer;
                    System.Threading.WaitHandle[] wait_handles=new System.Threading.WaitHandle[2];
                    wait_handles[0]=this.evt_close;
                    wait_handles[1]=this.evt_more_data_to_send;
                    int res_wait;

                    for(;;)
                    {
                        res_wait=System.Threading.WaitHandle.WaitAny(wait_handles,TIMEOUT_SEND,true);
                        switch (res_wait)
                        {
                            case 0:
                            case System.Threading.WaitHandle.WaitTimeout:
                                this.b_start_send_thread=true;
                                // exit thread
                                return;
                        }
                        this.evt_more_data_to_send.Reset();
                        while(send_queue.Count>0) // while there's data to send
                        {
                            // dequeue data of send_queue
                            this.evt_unlocked_send_queue.WaitOne();
                            buffer=(byte[])send_queue.Dequeue();
                            this.evt_unlocked_send_queue.Set();

                            this.data_socket.SendTo(buffer,0,buffer.Length,System.Net.Sockets.SocketFlags.DontRoute, remote_ep);

                            if (this.event_Socket_Data_Send_Completed!=null)
                                this.event_Socket_Data_Send_Completed(this,new EventArgs());
                        }
                    }
                }
                catch (Exception e)
                {
                    if (this.event_Socket_Data_Error!=null)
                        this.event_Socket_Data_Error(this,new EventArgs_Exception(e));
                    // clear send queue
                    this.evt_unlocked_send_queue.WaitOne();
                    this.send_queue.Clear();
                    this.evt_unlocked_send_queue.Set();
                    // restart thread to send data
                    ThreadStart myThreadStart = new ThreadStart(this.my_send);
                    Thread myThread =new Thread(myThreadStart);
                    myThread.Start();
                }
            }

            /// <summary>
            /// used for sending bytes
            /// launch thread for sending or enqueue data if a thread is allready used for sending
            /// </summary>
            /// <param name="buffer"></param>
            public void send(byte[] buffer)
            {
                if (this.b_start_send_thread)
                {
                    this.b_start_send_thread=false;
                    // start thread to send data
                    ThreadStart myThreadStart = new ThreadStart(my_send);
                    Thread myThread =new Thread(myThreadStart);
                    myThread.Start();
                }
                this.evt_unlocked_send_queue.WaitOne();
                this.send_queue.Enqueue(buffer);
                this.evt_unlocked_send_queue.Set();
                this.evt_more_data_to_send.Set();
            }

            /// <summary>
            /// used for sending string
            /// launch thread for sending or enqueue data if a thread is allready used for sending
            /// </summary>
            /// <param name="buffer"></param>
            public void send(string buffer)
            {
                byte[] sendBytes;
                sendBytes = Encoding.Default.GetBytes(buffer);
                this.send(sendBytes);
            }


            #endregion

        #endregion
    }

}
