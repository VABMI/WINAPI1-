
#include<sys/types.h>
#include<sys/socket.h>
#include<netinet/in.h>
#include<stdio.h>
#include<stdlib.h>

#include<string.h>
#include<sys/netdb.h>
#include <iostream>
#include <WS2tcpip.h>
#include <unistd.h>
#pragma comment (lib,"WS2_32.lib")
int main(void) 
{
int serverSocket,client_connected,len;
struct sockaddr_in client_addr,server_addr;
struct hostent *ptrh;
int n=0; 
char message[100],received[100];

serverSocket=socket(AF_INET, SOCK_STREAM, 0);

memset((char*)&server_addr,0,sizeof(server_addr));

server_addr.sin_family = AF_INET;
server_addr.sin_port = htons(10000);

server_addr.sin_addr.s_addr = htonl(INADDR_ANY);

if(bind(serverSocket,
(struct sockaddr*)&server_addr,sizeof(server_addr)) == -1)
printf("Bind Failure\n");
else
printf("Bind Success:<%u>\n", serverSocket);




while(1)
{   
     listen(serverSocket,5);
     len=sizeof(struct sockaddr_in);

    client_connected=accept(serverSocket,
    (struct sockaddr*)&client_addr,&len);
if (-1 != client_connected)
  printf("Connection accepted:<%u>\n", client_connected);

    while(1)
    {
    n = read(client_connected, received, sizeof(received));
    if( (strcmp(received,"q") == 0 ) || (strcmp(received,"Q") == 0 ))
    {
       printf("Wrong place...Socket Closed of Client\n");
       close(client_connected);
       break;
    }
    else{
    printf("\nUser:-%s", received);}
    printf("\nServer:-");
  //  memset(message, '\0', 10);
    gets(message);             
    write(client_connected, message, sizeof(message));
    if( (strcmp(message,"q") == 0 ) || (strcmp(message,"Q") == 0 ))
    {
       printf("Wrong place...Socket Closed of Client\n");
       close(client_connected);
       break;
    }  
    }
}

close(serverSocket); printf("\nServer Socket Closed !!\n");

return 0;
}