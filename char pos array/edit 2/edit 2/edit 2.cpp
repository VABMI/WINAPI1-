// edit 2.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
char bufer1[1000];
char bufer2[1000];
int bufint[1000];
int i;
bool bn=0;
bool gn=1;
long __stdcall mainProc(HWND hwnd ,UINT sms,WPARAM wp,LPARAM lp)
{ HWND hwedit=GetDlgItem(hwnd,1);
	switch(sms)
	{
	case WM_PAINT:

		break;
	case WM_COMMAND:
		switch(wp){
	case 2: /// <
		if(gn){
		SendMessage(hwedit,WM_GETTEXT,999,(LPARAM)bufer1);
		gn=0;
		}
		
		i=strlen(bufer1);
		while(i--)
		{ 
			if(bufer1[i]==' '&&bn==0)
			{ 
				
				MessageBox(hwnd,"ASd","asd",0);
				bn=1;
			}
			 if(bn)
			{
			bufer2[i]=bufer1[i];

			}



		}bn=0;
			SendMessage(hwedit,WM_SETTEXT,99,(LPARAM)bufer2);
			
		break;
	case 3: /// >


		break;

		}



		break;


	case WM_KEYDOWN:


		if(wp==2)
		{MessageBox(hwnd,"Asdad","asdasd",0);}
		
		if(wp==3) // srifti
		{MessageBox(hwnd,"Asdad","asdasd",0);}


		break;

	}
	
	return DefWindowProc(hwnd,sms,wp,lp);
}

int _tmain(int argc, _TCHAR* argv[])
{	HWND hwnd;
    MSG msg;
	WNDCLASS wc={0};
	wc.lpfnWndProc=&mainProc;
	wc.hbrBackground=CreateSolidBrush(RGB(55,12,96));
	wc.lpszClassName="vaxo";
	wc.hCursor=LoadCursor(NULL,0);
	
	if(!RegisterClass(&wc)){MessageBox(0,"Asdsa","Asdasd",0);}

	hwnd=CreateWindow(wc.lpszClassName,"main",WS_VISIBLE|WS_BORDER|WS_CLIPCHILDREN|WS_OVERLAPPEDWINDOW,10,10,900,900,0,0,0,0);
	CreateWindow("Edit","",WS_VISIBLE|WS_BORDER|WS_CHILD,10,10,500,500,hwnd,(HMENU)1,0,0);

		CreateWindow("Button","<",WS_VISIBLE|WS_BORDER|WS_CHILD,620,10,50,50,hwnd,(HMENU)2,0,0);
		CreateWindow("Button",">",WS_VISIBLE|WS_BORDER|WS_CHILD,670,10,50,50,hwnd,(HMENU)3,0,0);
	while(GetMessage(&msg,0,0,0))
	{

		DispatchMessage(&msg);
		TranslateMessage(&msg); 

	}

	return 0;
}

