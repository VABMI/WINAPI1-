#include <windows.h>
#include <stdio.h>
#include "commctrl.h"
#include <Uxtheme.h>
#pragma comment(lib,"comctl32.lib")



#include"Create_ListView.cpp"
VOID SetView(HWND hWndListView, DWORD dwView) ;
#define IDM_CODE_SAMPLES 1



///==========================Defines ==============================//

//{{NO_DEPENDENCIES}}
// Microsoft Developer Studio generated include file.
// Used by listview.rsrc.rc
//
#define IDC_DIALOG                      101
#define IDI_ICON1                       102
#define IDR_MENU1                       106
#define IDC_LIST                        1000
#define IDC_DELSELITEM                  1001
#define IDC_DELALL                      1002
#define IDC_ADD                         1004
#define IDC_ADDSUB                      1005
#define IDC_ADDITEM                     1006
#define IDC_ADDSUBITEM                  1007
#define IDC_BOTH                        1008
#define IDC_RENAME                      40002
#define IDC_SELECT_ALL                  40004
#define IDC_LAST_ITEM                   40005

// Next default values for new objects
// 
#ifdef APSTUDIO_INVOKED
#ifndef APSTUDIO_READONLY_SYMBOLS
#define _APS_NEXT_RESOURCE_VALUE        108
#define _APS_NEXT_COMMAND_VALUE         40006
#define _APS_NEXT_CONTROL_VALUE         1013
#define _APS_NEXT_SYMED_VALUE           101
#endif
#endif
///==============================================================////
HINSTANCE hInst;
//////=========================================================////////////////////
#include"InitListViewColumns.cpp"

/////// ==================================================== //////////////////
#include "ProcessCustomDraw.cpp"
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//----------------------------------------------------------------
	//	LVITEM LvItem; 
//----------------------------------------------------------------

//==============Global Vatriabls===================
static HWND hList=NULL;  // List View identifier
LVCOLUMN LvCol; // Make Coluom struct for ListView
LVITEM LvItem;  // ListView Item struct
LV_DISPINFO lvd;
int iSelect=0;
int index=0;
int flag=0;
HWND hEdit;
bool escKey=0;
char tempstr[100]="";
TCHAR tchar;
MSG msg;
//===================================================
long __stdcall window_main_function_chvenia(HWND hwnd,unsigned int message, unsigned int wparam,long lparam)
{
	
	HWND Lhwnd;





	switch(message)
	{
		case WM_CREATE:
			{


				

	Lhwnd=CreateListView(hwnd);
	InitListViewColumns(Lhwnd);
			}
			break;
		
		case WM_COMMAND:

	

	
		break;
		
		

		case WM_PAINT:

		break;


		case WM_DESTROY:
		exit(1);
		//PostQuitMessage(0);
		break;
	}


////////////// WM_NOTIFY //////////
#include"MSG_ListView.cpp"  //////
/////////////////////////////////



return DefWindowProc(hwnd,message,wparam,lparam);
}
//----------------------------------------------------------------

int __stdcall WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, PSTR szCmdLine, int iCmdShow)
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

MSG msg;
	while(GetMessage(&msg,0,0,0))
	{
	TranslateMessage(&msg);
	DispatchMessage(&msg);
	}
}




// SetView: Sets a list-view's window style to change the view.
// hWndListView: A handle to the list-view control. 
// dwView:       A value specifying the new view style.
//
VOID SetView(HWND hWndListView, DWORD dwView) 
{ 
    // Retrieve the current window style. 
    DWORD dwStyle = GetWindowLong(hWndListView, GWL_STYLE); 
    
    // Set the window style only if the view bits changed.
    if ((dwStyle & LVS_TYPEMASK) != dwView) 
    {
        SetWindowLong(hWndListView,GWL_STYLE,(dwStyle & ~LVS_TYPEMASK) | dwView);
    }          
}              