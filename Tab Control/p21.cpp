#include <windows.h>
#include <stdio.h>
#include <commctrl.h>


#pragma comment(lib,"comctl32.lib")
HFONT hfont_global;

#include "on_create.cpp"
 


//----------------------------------------------------------------
 HWND DoCreateStatusBar(HWND hwndParent, int idStatus, HINSTANCE hinst, int cParts);
//----------------------------------------------------------------
long __stdcall window_main_function_chvenia(HWND hwnd,unsigned int message
					, unsigned int wparam,long lparam)
{
	switch(message)
	{

	case WM_SIZE:
		 DoCreateStatusBar(hwnd,6,0,5);
	//	 SendMessage(hwnd,WM_PAINT,0,1);
		
		 InvalidateRect(hwnd,0,1);
		 
		 break;
		case WM_CREATE:
	//	on_create(hwnd,message,wparam,lparam);
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
	}
return DefWindowProc(hwnd,message,wparam,lparam);
}
//----------------------------------------------------------------
HWND DoCreateStatusBar(HWND hwndParent, int idStatus, HINSTANCE
                      hinst, int cParts)
{
	RECT rcClient, rcTool, rcTab;
TCHAR tabTitleTmp[256]="AsdadasD"; // Temp string buffer

//HWND hTool = GetDlgItem(hwndParent, IDC_MAIN_TOOL);
GetWindowRect(hwndParent, &rcTool);
int iToolHeight = rcTool.bottom - rcTool.top;

// Get parent's client rect
GetClientRect(hwndParent, &rcClient); 

// Create tab control
 HWND hwndTab = CreateWindowEx(NULL, WC_TABCONTROL, NULL, WS_CHILD | WS_CLIPSIBLINGS | WS_VISIBLE, 0, 50, 300, 300, hwndParent, (HMENU) 44,
     0, NULL);

// Create tab items
TCITEM tie; 
tie.mask = TCIF_TEXT | TCIF_IMAGE; 
tie.iImage = -1; 
tie.pszText = tabTitleTmp; 

// Set up tabs
for(int i = 0; i < 2; i++) {
    LoadString(0, 44 + i, tabTitleTmp, sizeof(tabTitleTmp) / sizeof(tabTitleTmp[0]));
    TabCtrl_InsertItem(hwndTab, i, &tie);
}
    return hwndTab;
}  
//int __stdcall WinMain(HINSTANCE,HINSTANCE,char *,int)
void main()
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
	return;
	}


//mtavari fanjara:
style=WS_VISIBLE|WS_OVERLAPPEDWINDOW|WS_CLIPCHILDREN;
X=10;Y=30;W=1000;H=600;
hwnd=CreateWindow(wc.lpszClassName,"Main",style,X,Y,W,H,0,0,0,0);
RECT r;
GetClientRect(hwnd,&r);
//	 CreateStatusWindow(WS_CHILD|WS_VISIBLE|SBARS_SIZEGRIP,"text",hwnd,100);
 int widths[5]={100,150,200,-1};
 
 SendDlgItemMessage(hwnd,100,SB_SETPARTS,4,(LPARAM)widths);
 SendDlgItemMessage(hwnd,100,SB_SETTEXT,2,(LPARAM)"part 2");
 SendDlgItemMessage(hwnd,100,SB_SETTEXT,3,(LPARAM)"last part");
 
 


 

 
 MSG msg;
int s=1;
	while(s!=0)
	{
	s=GetMessage(&msg,0,0,0);
	TranslateMessage(&msg);
	DispatchMessage(&msg);
	}
}