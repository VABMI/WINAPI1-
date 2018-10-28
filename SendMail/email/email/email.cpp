// email.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include<stdio.h>
#include<stdlib.h>
#include<sys/socket.h>
#include<netinet\in.h>

int _tmain(int argc, _TCHAR* argv[])
{







	return (EXIT_SUCCESS);
}

int ConnectToServer(const char *server_address){
	int socket_fd=socket(AF_INET,SOCK_STREAM,IPPROTO_IP);
	struct sockaddr_in addr;
	addr.sin_family=AF_INET;
	addr.sin_port=htons(25);
	
	
	
	return socket_fd;
}


const char*GetIPAddress( const char* target_domain)
{
	const char* target_ip;
	struct in_addr *host_address;
	
	struct hostent *raw_list=gethostbyname(target_domain);
	int i=0;

	for(i;raw_list->h_addr_list[i]!=0;i++){
		host_address=raw_list->h_addr_list[i];
		target_ip=inet_ntoa(host_address);
	}
	return target_ip;

}