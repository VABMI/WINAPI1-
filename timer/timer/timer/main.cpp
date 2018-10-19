#include "head.h"
long __stdcall vaxo(HWND hwnd,unsigned int sms,WPARAM wp,LPARAM lp)
{HDC hdc=GetDC(hwnd);
static int c=0;
	wchar_t m[100];
	switch(sms)
	{
	case WM_TIMER:
		switch(wp){
		case 101:
		/*
		c++;
		swprintf_s(m, L"%d",c,1);
		TextOut(hdc,60,10,m,wcslen(m));
		*/

		date(hdc);
	  
		//DeleteObject(hdc);
		
		
		break;

		}
		break;

	case WM_CREATE:
		menu(hwnd,sms,wp,lp);
		break;
	case WM_COMMAND:
		switch(wp)
		{

		case 1:
			MessageBeep(MB_ICONSTOP);
			SetTimer(hwnd,101,300,NULL);
			break;
		case 2:
			MessageBeep(MB_ICONASTERISK);
			KillTimer(hwnd,101);
			break;

		}
		break;
	case WM_PAINT:
		break;


	}

	return DefWindowProc(hwnd,sms,wp,lp);
}

int __stdcall WinMain(HINSTANCE,HINSTANCE,char *,int)
{ 
	
	HWND hwnd;
	WNDCLASS wc;
	ZeroMemory(&wc,sizeof(WNDCLASS));
	wc.style=CS_DBLCLKS;
	wc.lpfnWndProc=(WNDPROC)&vaxo;
	wc.lpszClassName=(LPCWSTR)"vaxo";
	wc.hbrBackground=CreateSolidBrush(RGB(200,120,30));
	if(RegisterClass(&wc)==0)
	{
		MessageBox(hwnd,(LPCWSTR)"Error",(LPCWSTR)"error",0);
			
	}

		hwnd=CreateWindow(wc.lpszClassName,(LPCTSTR)&"MAIN",WS_VISIBLE|WS_CLIPCHILDREN|WS_OVERLAPPEDWINDOW,100,100,700,100,0,0,0,0);

	MSG msg;
	while(GetMessage(&msg,0,0,0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
}