#include "header.h"

#include "defines.h"
#include "globals.h"


#include "functions.cpp"






#include "mouse_msg.cpp"
#include "kbd_msg.cpp"
#include "cmd_msg.cpp"
#include "on_create_msg.cpp"



#pragma comment(lib,"comctl32.lib")

//----------------------------------------------------------------
	
//----------------------------------------------------------------
long __stdcall window_main_function_chvenia(HWND hwnd,unsigned int message, unsigned int wparam,long lparam)
{
	HWND stat5=GetDlgItem(hwnd,251);
	HDC vv=GetDC(stat5);

	PAINTSTRUCT ps;
	HDC hdc = BeginPaint(hwnd, &ps);
	HPEN hPen;
	
	bool t;
	switch(message)
	{ 
		/*
		case WM_CTLCOLOREDIT:
		{
		 HDC hdcStatic = (HDC) wparam;
		SetTextColor(hdcStatic, RGB(20,200,123));
		 SetBkColor(vv, RGB(20,200,123));
	     return (INT_PTR)CreateSolidBrush( RGB(20,200,123));
		
		}

		*/



			

		case WM_CREATE:
		on_create(hwnd,message,wparam,lparam);
		break;
		
		case WM_COMMAND:
	
		on_cmd(hwnd,message,wparam,lparam);
		break;
		case WM_LBUTTONDBLCLK:
		
		break;


		case WM_KEYDOWN:

		break;
		case WM_SIZE:
		//	I_IIdiagram(vv);
		//	IIdiagram((void*)zeda);
			break;

		case WM_DESTROY:
		exit(1);
		//PostQuitMessage(0);
		break;
	}
return DefWindowProc(hwnd,message,wparam,lparam);
}
//----------------------------------------------------------------

int __stdcall WinMain(HINSTANCE,HINSTANCE,char *,int)
	//	void main()
{
HWND hwnd=0;
int X,Y,W,H;
ULONG style=0;

WNDCLASS wc;
ZeroMemory(&wc,sizeof(WNDCLASS));

wc.style=CS_DBLCLKS;
wc.lpfnWndProc=(WNDPROC)&window_main_function_chvenia;
wc.lpszClassName="12";
wc.hbrBackground=(HBRUSH)CreateSolidBrush(RGB(200,200,20));
wc.hIcon=(HICON)LoadImage(0,"c:\\1.ico",IMAGE_ICON,16,16,LR_LOADFROMFILE);

	if(RegisterClass(&wc)==0)
	{
	MessageBox(hwnd,"RegisterClass error",0,0);
	//	return 0;
	}


//mtavari fanjara:
style=WS_VISIBLE|WS_OVERLAPPEDWINDOW|WS_CLIPCHILDREN;
X=10;Y=30;W=750;H=500;
hwnd=CreateWindow(wc.lpszClassName,"Main",style,X,Y,W,H,0,0,0,0);

	
MSG msg;
	while(GetMessage(&msg,0,0,0))
	{
	TranslateMessage(&msg);
	DispatchMessage(&msg);

	}
}