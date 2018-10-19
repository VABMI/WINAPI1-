#include <windows.h>
#include <stdio.h>

HFONT hfont_global;

#include "mouse_msg.cpp"
#include "kbd_msg.cpp"
#include "cmd_msg.cpp"
#include "paint.cpp"
#include "on_create.cpp"
 


//----------------------------------------------------------------

//----------------------------------------------------------------
long __stdcall window_main_function_chvenia(HWND hwnd,unsigned int message
					, unsigned int wparam,long lparam)
{
		void* ffsf;
	void *pp;
		LPDROPTARGET ddfh;

		if(message==WM_MOUSEMOVE)
		{


		 if(OleInitialize)
		 {


				DragAcceptFiles(hwnd,1);



		 }
			







		}








		if(message==WM_DROPFILES)
		{
			char kk[100];
					
			HDROP	hDrop =(HDROP) wparam;
				DragQueryFile(hDrop,0,kk,99);

    	MessageBox(hwnd,kk,kk,0);


		}


	switch(message)
	{
	case WM_DROPFILES:
		//	case WM_DRAGFILES:

			break;


		case WM_CREATE:
		on_create(hwnd,message,wparam,lparam);
		break;
		
		case WM_COMMAND:
		on_cmd(hwnd,message,wparam,lparam);
		break;
		
		case WM_RBUTTONDOWN:
		case WM_LBUTTONDOWN:
		case WM_MOUSEMOVE:
		case WM_RBUTTONDBLCLK:
		case WM_LBUTTONDBLCLK:
		on_mouse(hwnd,message,wparam,lparam);
		break;

		case WM_PAINT:
		on_paint(hwnd,message,wparam,lparam);
		break;

		case WM_KEYDOWN:
		on_kbd(hwnd,message,wparam,lparam);
		break;
	}
return DefWindowProc(hwnd,message,wparam,lparam);
}
//----------------------------------------------------------------

int __stdcall WinMain(HINSTANCE,HINSTANCE,char *,int)
	//int main()
{
HWND hwnd=0;
int X,Y,W,H;
ULONG style=0;

WNDCLASS wc;
ZeroMemory(&wc,sizeof(WNDCLASS));

wc.style=CS_DBLCLKS;
wc.lpfnWndProc=(WNDPROC)&window_main_function_chvenia;
wc.lpszClassName="12";
wc.hbrBackground=(HBRUSH)CreateSolidBrush(RGB(200,200,200));
wc.hIcon=(HICON)LoadImage(0,"c:\\1.ico",IMAGE_ICON,16,16,LR_LOADFROMFILE);

	if(RegisterClass(&wc)==0)
	{
	MessageBox(hwnd,"RegisterClass error",0,0);
	return 1;
	}


//mtavari fanjara:
style=WS_VISIBLE|WS_OVERLAPPEDWINDOW|WS_CLIPCHILDREN;
X=10;Y=30;W=700;H=500;
hwnd=CreateWindow(wc.lpszClassName,"Main",style,X,Y,W,H,0,0,0,0);

	
MSG msg;
int s=1;
	while(s!=0)
	{
	s=GetMessage(&msg,0,0,0);
	TranslateMessage(&msg);
	DispatchMessage(&msg);
	}
	return 0;
}