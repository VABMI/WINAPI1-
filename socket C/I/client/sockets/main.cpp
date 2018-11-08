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
char *ipin;
//scanf("%s",&ipin);
int port = 80;
string ipAddress;//=ipin;
cout<<endl<<"________________\n";
cout<<"sheiyvane IP->: ";
cin>>ipAddress;
cout<<endl<<"sheiyvane PORT_N->:";
cin>>port;
cout<<endl<<"________________\n";
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
inet_pton(AF_INET,ipAddress.c_str(),&hint.sin_addr); /// ver gavigee


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
	if(userInput.size()<=0)
	{
		userInput="sheitane striqoni";
	}
	if(userInput.size() > 0) ///    c_str utitebs  userInput ze misamartze rac weria anu send agzavnis orobitshi da cherez operatiulida akitxebs ororbitshi
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
 /// aq itisheba kavshiri da wyvets mushaobas orive programa
//closesocket(sock);

//WSACleanup();
}