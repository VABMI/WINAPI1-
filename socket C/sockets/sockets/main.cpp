#include <iostream>
#include <stdlib.h>
#include <stdio.h>
#include <sys/types.h>
#include <string>
#include <winsock2.h>
#include <WS2tcpip.h>
#pragma comment (lib,"WS2_32.lib")
using namespace std;

void  main()
{


string ipAddress="127.0.0.1";
int port = 54000;
WSAData data;
WORD ver = MAKEWORD(2,2);
int wsResult = WSAStartup(ver,&data);
if(wsResult != 0)
{

	cerr<<"can't start Winsock, err "<<wsResult<<endl;
	return;
}


SOCKET sock = socket(AF_INET,SOCK_STREAM,0);
if(sock == INVALID_SOCKET)
{


	cerr <<"can't create socket,Err #"<<WSAGetLastError()<<endl;
	WSACleanup();
	return;
}

sockaddr_in hint;
hint.sin_family = AF_INET;
hint.sin_port = htons(port);
inet_pton(AF_INET,ipAddress.c_str(),&hint.sin_addr);


int connResult = connect(sock,(sockaddr*)&hint,sizeof(hint));

if(connResult == SOCKET_ERROR)
{

	cerr <<"Can't connect to server ,err "<<WSAGetLastError()<<endl;
	closesocket(sock);
	WSACleanup();
	return;

}


char buf[4096];
string userInput;
do
{
	cout<< "> ";
	getline(cin,userInput);
	if(userInput.size() > 0)
	{
		int sendResult = send(sock,userInput.c_str(),userInput.size()+1,0);
		if(sendResult != SOCKET_ERROR)
		{
			ZeroMemory(buf,4096);
			int bytesReceived = recv(sock,buf,4096,0);
			if(bytesReceived>0)
			{

				cout<< "SERVER> " <<string(buf,0,bytesReceived)<<endl;

			}



		}
	}

}while(userInput.size()>0);

closesocket(sock);

WSACleanup();
}