#include "globals.h"

	//	void date(HDC hdc,HWND	stat313,HWND stat314,HWND stat315,HWND stat317,HWND stat318,HWND stat319)
void date(HDC hdc,HWND	stat317,HWND stat313,HWND stat315,HWND stat314,HWND stat318,HWND stat319,int saati,char* city)
{	static bool bl=1;
	char m [100];
/////////////////===== I ===////////////
	static	 char MemoryCity[100];
	if(strlen(MemoryCity)!=strlen(city))
	{
		sprintf(MemoryCity,"%s","");
		bl=1;	strcat(MemoryCity,city);

			SetWindowText(stat319,NULL);
		SetWindowText(stat319,city);
	}
	
//////////////////////////////////////////////////



	
 




SYSTEMTIME st;
GetSystemTime(&st);




	// sprintf(m, "%ld-%ld-%ld==%ld-%ld-%ld",st.wYear,st.wMonth,st.wDay,st.wHour-8,st.wMinute,st.wSecond,1);
		 if(st.wSecond==58&&st.wMilliseconds>900||bl)
			{
					bl=0;
				if(st.wHour+4<10)
				{
					sprintf(m, "%ld",st.wHour+(saati),1);
 					SetWindowText(stat313,m);
				}
				else
				{
					sprintf(m, "%ld",st.wHour+(saati),1);
 					SetWindowText(stat313,m);
				}

				if(st.wMinute<10)
				{
					sprintf(m, "0%ld",st.wMinute,1);
 				//	SetWindowText(stat315,m);
				}
				else
				{
					sprintf(m, "%ld",st.wMinute,1);
 					SetWindowText(stat315,m);
				}

//			sprintf(m, "%ld",st.wMinute,1);
 //			SetWindowText(stat315,m);

				sprintf(m, "%ld-%ld-%ld",st.wYear,st.wMonth,st.wDay,1);
 				SetWindowText(stat318,m);
			}

		



	sprintf(m, "%ld %ld",st.wSecond,st.wMilliseconds,1);
	SetWindowText(stat317,m);	
	
	////////////// cimcimi   /////////
	static	int i=0;
	if((st.wSecond)%2==0)
	{
		i=st.wSecond;
	sprintf(m," ",0,0,1);
	SetWindowText(stat314,m);

	sprintf(m,":",0,0,1);
	SetWindowText(stat314,m);

	}	
	
	//		Sleep(1000);
///TextOut(hdc,0,0,m,strlen(m));
	
//	ZeroMemory(stat319,sizeof(stat319));
	 

}