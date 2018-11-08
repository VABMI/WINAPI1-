#include <iostream>
#include <WS2tcpip.h>

#pragma comment (lib,"WS2_32.lib")
using namespace std;
void main()
{
	L:
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
	hint.sin_port=htons(80);
	hint.sin_addr.S_un.S_addr=INADDR_ANY; /// ver gavige



	bind(listening,(sockaddr*)&hint,sizeof(hint));
	listen(listening,SOMAXCONN);
	////////////wait for a connection 

	sockaddr_in client;
	int clientSize=sizeof(client);
	SOCKET clientSocket = accept(listening,(sockaddr*)&client,&clientSize);


	char host[NI_MAXHOST];
	char service[NI_MAXHOST];


	ZeroMemory(host,NI_MAXHOST);
	ZeroMemory(service,NI_MAXHOST);

	if(getnameinfo((sockaddr*)&client,sizeof(client),host,NI_MAXHOST,service,NI_MAXSERV,0)==0)
	{

		cout << host << " :connected on port " << service <<endl;

	}
		else 
		{

			inet_ntop(AF_INET,&client.sin_addr,host,NI_MAXHOST);
			cout<<host <<" :connected on port " <<ntohs(client.sin_port) << endl;
		}

	closesocket(listening);
	char buf[4096];

	while(true)
	{
		ZeroMemory(buf,4096);

		int bytesReceived = recv(clientSocket,buf,4096,0);
		if(bytesReceived == SOCKET_ERROR)
		{
			cerr << "error  in recv(). quitting" << endl;
				cout<<"client disconnected " << endl; ////
			break;

		}
		if(strlen(buf)>0)
		{
		cout<<"CLIENT> "<<buf<<endl;

		}

		if(bytesReceived ==0)
		{
			cout<<"client disconnected " << endl;
			break;

		}




		send(clientSocket,buf,bytesReceived + 1,0);

		}
	goto L;
	closesocket(clientSocket);

	WSACleanup();
	}




	



