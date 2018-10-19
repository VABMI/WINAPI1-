#include <windows.h>
#include <stdio.h>
#include "commctrl.h"




#pragma comment(lib,"comctl32.lib")

//----------------------------------------------------------------

//----------------------------------------------------------------
long __stdcall window_main_function_chvenia(HWND hwnd,unsigned int message, unsigned int wparam,long lparam)
{
	switch(message)
	{
		case WM_CREATE:
	
		break;
		
		case WM_COMMAND:
	
		break;
		
		case WM_RBUTTONDOWN:
		case WM_LBUTTONDOWN:
		case WM_MOUSEMOVE:
		case WM_RBUTTONDBLCLK:

		case WM_LBUTTONDBLCLK:

		break;

		case WM_PAINT:

		break;

		case WM_KEYDOWN:
	
		break;

		case WM_CTLCOLOREDIT:
		{
	
		}
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
//void main()
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
	return 0;
	}


//mtavari fanjara:
style=WS_VISIBLE|WS_OVERLAPPEDWINDOW|WS_CLIPCHILDREN;
X=10;Y=30;W=750;H=500;
hwnd=CreateWindow(wc.lpszClassName,"Main",style,X,Y,W,H,0,0,0,0);

	
//////////////// list Vieew //

InitCommonControls(); // Force the common controls DLL to be loaded.
HWND list;

// window is a handle to my window that is already created.
list = CreateWindowEx(0, (LPCSTR) WC_LISTVIEWW, NULL, WS_VISIBLE | WS_CHILD | WS_BORDER | LVS_SHOWSELALWAYS | LVS_REPORT, 0, 0, 250, 400, hwnd, NULL, NULL, NULL);

LVCOLUMN lvc; 
lvc.mask = LVCF_FMT | LVCF_WIDTH | LVCF_TEXT | LVCF_SUBITEM;
lvc.iSubItem = 0;
lvc.pszText = "Title";
lvc.cx = 50;
lvc.fmt = LVCFMT_LEFT;
ListView_InsertColumn(list, 0, &lvc);
/////////////////////////////////////////////////


MSG msg;
	while(GetMessage(&msg,0,0,0))
	{
	TranslateMessage(&msg);
	DispatchMessage(&msg);
	}
}