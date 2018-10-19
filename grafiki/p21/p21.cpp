#include <windows.h>
#include <stdio.h>

HFONT hfont_global;

#include "mouse_msg.cpp"
#include "kbd_msg.cpp"
#include "cmd_msg.cpp"
#include "paint.cpp"
#include "on_create.cpp"
 
HWND h;

//----------------------------------------------------------------

//----------------------------------------------------------------
long __stdcall window_main_function_chvenia(HWND hwnd,unsigned int message
					, unsigned int wparam,long lparam)
{	
	switch(message)
	{
		case WM_CREATE:
		on_create(hwnd,message,wparam,lparam);
		on_create2(hwnd,message,wparam,lparam);
		break;
		
		case WM_COMMAND:
		on_cmd(hwnd,message,wparam,lparam);
		if(wparam==3){


		
		}
		break;
		
		case WM_LBUTTONDBLCLK:
	//	on_mouse(hwnd,message,wparam,lparam);
		break;

		case WM_PAINT:
		//on_paint(hwnd,message,wparam,lparam);
		
		break;

		case WM_KEYDOWN:
		on_kbd(hwnd,message,wparam,lparam);
		break;

			case WM_CTLCOLORSTATIC:
		{
			
		 HDC hdcStatic = (HDC) wparam;
		
		 SetBkColor(hdcStatic,RGB(250,0,0));
	     return (INT_PTR)CreateSolidBrush(RGB(250,0,0));
		 
		
		}
		break;
	}
return DefWindowProc(hwnd,message,wparam,lparam);
}
//----------------------------------------------------------------

//int __stdcall WinMain(HINSTANCE,HINSTANCE,char *,int)
void main()
{
HWND hwnd=0;
int X,Y,W,H;
ULONG style=0;

WNDCLASS wc;
ZeroMemory(&wc,sizeof(WNDCLASS));

wc.style=CS_DBLCLKS|SW_MAXIMIZE;
wc.lpfnWndProc=(WNDPROC)&window_main_function_chvenia;
wc.lpszClassName="12";
wc.hbrBackground=(HBRUSH)CreateSolidBrush(RGB(200,200,200));
wc.hIcon=(HICON)LoadImage(0,"c:\\1.ico",IMAGE_ICON,16,16,LR_LOADFROMFILE);

	if(RegisterClass(&wc)==0)
	{
	MessageBox(hwnd,"RegisterClass error",0,0);
	return;
	}


//mtavari fanjara:
style=WS_VISIBLE|WS_OVERLAPPEDWINDOW|WS_CLIPCHILDREN;
X=10;Y=30;W=700;H=500;
hwnd=CreateWindow(wc.lpszClassName,"Main",style|SW_MAXIMIZE,X,Y,W,H,0,0,0,0);
ShowWindow(hwnd, SW_MAXIMIZE);
	
MSG msg;
int s=1;
	while(s!=0)
	{
	s=GetMessage(&msg,0,0,0);
	TranslateMessage(&msg);
	DispatchMessage(&msg);
	}
}