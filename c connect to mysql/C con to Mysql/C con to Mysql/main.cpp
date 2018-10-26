#include<stdio.h>
#include<stdlib.h>
#include<mysql/mysql.h>
static char *host="localhost";
static char *user="root";
static char *dbname="test";
unsigned int port = 3306;
static char *unix_socket=NULL;
unsigned int flad=0;

int main()
{


	MYSQL *conn;
	conn=mysql_init(NULL);
	if(!(mysql_real_connect(conn,host,user,pass,dbname,port,unix_socket,flag)))

	{

		fprintf(stderr,"\nError: %s [%d] \n",mysql_error(conn),mysql_errno(conn));

	}

	printf("Connection Successful!\n\n");


}
