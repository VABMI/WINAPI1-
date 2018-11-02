#include <windows.h>
#include <stdio.h>

HFONT hfont_global;
HDC hdc;
#include "cmd_msg.cpp"
#include "on_create.cpp"
 

HWND    g_hwndButton            = NULL;   
HWND       g_hwndButton1	=NULL;
WNDPROC g_wndProcButtonOrigianl = NULL;   
BOOL    g_bSeeingMouse          = FALSE;






//----------------------------------------------------------------

//----------------------------------------------------------------
long __stdcall window_main_function_chvenia(HWND hwnd,unsigned int message
					, unsigned int wparam,long lparam)
{
	HDC         hDC;
    PAINTSTRUCT Ps;
    HFONT	    font;




	switch(message)
	{
		case WM_CREATE:
			{
		//on_create(hwnd,message,wparam,lparam);
			
				create_font(GetDlgItem(hwnd,1));
				InvalidateRect(hwnd,0,1);
			}
			break;
		
		case WM_COMMAND:
		on_cmd(hwnd,message,wparam,lparam);
		
		break;
		
		case WM_RBUTTONDOWN:
		case WM_LBUTTONDOWN:
		case WM_MOUSEMOVE:
		case WM_RBUTTONDBLCLK:
		case WM_LBUTTONDBLCLK:
		//on_mouse(hwnd,message,wparam,lparam);
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
style=WS_VISIBLE|WS_POPUP|WS_CLIPCHILDREN;
X=1205;Y=3;W=70;H=30;


	hwnd=CreateWindow("12","main",style,X,Y,W,H,0,0,0,0);
	HWND hwnd1=CreateWindow("button","ShutDown",WS_VISIBLE|WS_CHILD,0,0,W,H,hwnd,(HMENU)1,0,0);
		create_font(hwnd1);



	HRGN hrgn;	
	RECT r;
	HDC hdc=GetDC(hwnd);
	GetClientRect(hwnd,&r);
	hrgn=CreateEllipticRgn(r.left,r.top,r.right,r.bottom);
	SetWindowRgn(hwnd,hrgn,1);




	hwnd=CreateWindow("12","main",style,X+80,Y,W,H,0,0,0,0);
	hwnd1=CreateWindow("button","RESTART",WS_VISIBLE|WS_CHILD,0,0,W,H,hwnd,(HMENU)2,0,0);
	hdc=GetDC(hwnd);
	GetClientRect(hwnd,&r);
	hrgn=CreateEllipticRgn(r.left,r.top,r.right,r.bottom);
	SetWindowRgn(hwnd,hrgn,1);


		create_font(hwnd1);
MSG msg;
int s=1;
	while(s!=0)
	{
	s=GetMessage(&msg,0,0,0);
	TranslateMessage(&msg);
	DispatchMessage(&msg);
	}
}