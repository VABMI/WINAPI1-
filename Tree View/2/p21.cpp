#include <windows.h>
#include <stdio.h>
#define WIN32_LEAN_AND_MEAN

#include <Windows.h>
#include <windowsx.h>
#include <CommCtrl.h>
#include <string>
#include <fstream>
#include <winver.h>
#pragma comment(lib, "ComCtl32.Lib")
//#pragma comment(linker, "\"/manifestdependency:type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")
//CreateWindowEx(WS_EX_CLIENTEDGE, WC_TREEVIEW, TEXT("TreeView"), WS_CHILD | WS_VISIBLE | TVS_HASBUTTONS | TVS_LINESATROOT, 10, 10, 400, 400, hWnd, reinterpret_cast<HMENU>(ID_TREEVIEW), GetModuleHandle(NULL), NULL);

#include "on_create.cpp"
#include "addItem.cpp"

long __stdcall window_main_function_chvenia(HWND hwnd,unsigned int message, unsigned int wparam,long lparam)
{
	switch(message)
	{
		case WM_CREATE:
			{
		HWND TREE=CreateATreeView(hwnd);
	//	AddItemToTree(TREE,"PIRVELI",1);
	//	AddItemToTree(TREE,"MEORE",1);
		 static HTREEITEM hPrev ;
		insertItem("PIRVELI",hPrev,0,0,1,TREE);
			
			}
			
			
		break;
		
		case WM_COMMAND:
		break;
		
		case WM_PAINT:
		
		break;

		case WM_KEYDOWN:
	
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
}