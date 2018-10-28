#include <iostream>
#include <WS2tcpip.h>
#include <string>
#include <sstream>
#pragma comment (lib,"WS2_32.lib")
using namespace std;
void main()
{

	WSADATA wsData;
	WORD ver = MAKEWORD(2,2);
	int WsOK = WSAStartup(ver,&wsData);
	if(WsOK !=0)
	{
		cerr <<"Can't Initialize Wins"<<endl;
		return;
	}


	SOCKET listening = socket(AF_INET,SOCK_STREAM,0);
	if(listening == INVALID_SOCKET)
	{

		cerr << "Can't create a socket! "<<endl;
		return;

	}






	sockaddr_in hint;
	hint.sin_family=AF_INET;
	hint.sin_port=htons(54000);
	hint.sin_addr.S_un.S_addr=INADDR_ANY; /// ver gavige



	bind(listening,(sockaddr*)&hint,sizeof(hint));
	listen(listening,SOMAXCONN);




	fd_set master;
	FD_ZERO(&master);
	FD_SET(listening,&master);
	while(true)
	{

		fd_set copy=master;
		int socketCount=select(0,&copy,nullptr,nullptr,nullptr);

		for(int i=0;i<socketCount;i++)
		{
			SOCKET sock=copy.fd_array[i];
			if(sock==listening)
			{

				////accept a new connection
				SOCKET client=accept(listening,nullptr,nullptr);
				//Add the new connection tothelisof connected clients

				FD_SET(client,&master);

				string wlcomeMsg="chat server";
				send(client,wlcomeMsg.c_str(),wlcomeMsg.size()+1,0);

			}

			else
			{
				char buf[4096];
				ZeroMemory(buf,4096);
				int bytesIn = recv(sock,buf,4096,0);
				if(bytesIn<=0)
				{
					closesocket(sock);
					FD_CLR(sock,&master);


				}
				else
				{

					for(int i=0;i<master.fd_count;i++)
					{
						SOCKET outSock = master.fd_array[i];
						if(outSock != listening && outSock !=sock)
						{
							send(outSock,buf,bytesIn,0);


						}

					}

				}
			
			}

		}

	}

	WSACleanup();
}




	



