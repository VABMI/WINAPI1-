#include<conio.h>
#include<stdlib.h>
#include<stdio.h>

void main()
{
	static char *person[100];
	FILE *file;
	char *h;
	file=fopen("C:\\Users\\vakho1\\Desktop\\text.txt","r");
	//fscanf(file, "%s %s %s ", person[0], person[1], person[2]);
	char str1[10], str2[10], str3[10];
	struct person{
		int  ID_NUMBER[20];
		char NAME[20];
		char LASTNAME[20];
		char EMAIL[20];
		char PASSWORD[20];


	};
	
	//  rewind(file);
   struct person per;
   
   while(fscanf(file, "%s %s %s %s %s",per.ID_NUMBER,per.EMAIL,per.NAME,per.LASTNAME,per.PASSWORD)!=EOF){

 
   printf("Read String1 |%s|\n", per.ID_NUMBER );
   printf("Read String2 |%s|\n", per.EMAIL );
   printf("Read String3 |%s|\n", per.NAME );

	}

	 
	 getch();

}