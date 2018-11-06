#include <windows.h>
#include <stdio.h>
#include <CommCtrl.h>

HFONT hfont_global;
HDC hdc;
#include "cmd_msg.cpp"
#include "on_create.cpp"
#include "HBRUSHES.cpp"

HWND    g_hwndButton            = NULL;   
HWND       g_hwndButton1	=NULL;
WNDPROC g_wndProcButtonOrigianl = NULL;   
BOOL    g_bSeeingMouse          = FALSE;




#define SHUTDOWN 1
#define  RESTART 2

//----------------------------------------------------------------

LRESULT CALLBACK WndProcButton (HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam);

HWND    g_hwndButton            = NULL;  
HWND       g_hwndButton1	=NULL;
WNDPROC g_wndProcButtonOrigianl = NULL;   
BOOL    g_bSeeingMouse          = FALSE;

//----------------------------------------------------------------

LRESULT CALLBACK WndProcButton (HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{

	   WNDPROC wpOld = (WNDPROC)GetWindowLongPtr(g_hwndButton, GWLP_USERDATA);

	   if(wpOld)
	   {
			   switch(message)
			   {
				   

	      case WM_RBUTTONDOWN:

			  	MessageBox(hwnd,"asdasd","asdasd",0);
			
			  break;
			   
			   
			   
			   case WM_ERASEBKGND:

			


					 break;
					case WM_NCDESTROY:
					
					 break;
					    case WM_GETDLGCODE:

								
							break;
			   }
			    

	   }










	 switch( message )
    {
   
      


	      case WM_RBUTTONDOWN:

			  	MessageBox(hwnd,"asdasd","asdasd",0);
			
			  break;

    }









    switch( message )
    {
		

	      case WM_RBUTTONDOWN:

			  	MessageBox(hwnd,"asdasd","asdasd",0);
			
			  break;





    case WM_COMMAND:
		if(wParam==1)
		{

			
		}
      //  SetFocus( g_hwndButton );
        break;
    default:
        
        break;
    }
    return CallWindowProc( g_wndProcButtonOrigianl, hwnd, message, wParam, lParam );
}

//----------------------------------------------------------------
long __stdcall window_main_function_chvenia(HWND hwnd,unsigned int message
					, unsigned int wparam,long lparam)
{
	HDC         hDC;
    PAINTSTRUCT Ps;
    HFONT	    font;




	switch(message)
	{

#include "Wm_notify.cpp"






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
	HWND hwnd1=CreateWindow("button","ShutDown",WS_VISIBLE|WS_CHILD,0,0,W,H,hwnd,(HMENU)SHUTDOWN,0,0);
		create_font(hwnd1);



	HRGN hrgn;	
	RECT r;
	HDC hdc=GetDC(hwnd);
	GetClientRect(hwnd,&r);
	hrgn=CreateEllipticRgn(r.left,r.top,r.right,r.bottom);
	SetWindowRgn(hwnd,hrgn,1);




	hwnd=CreateWindow("12","main",style,X+80,Y,W,H,0,0,0,0);
	hwnd1=CreateWindow("button","RESTART",WS_VISIBLE|WS_CHILD,0,0,W,H,hwnd,(HMENU)RESTART,0,0);
	hdc=GetDC(hwnd);
	GetClientRect(hwnd,&r);
	hrgn=CreateEllipticRgn(r.left,r.top,r.right,r.bottom);
	SetWindowRgn(hwnd,hrgn,1);


		create_font(hwnd1);


		

		HCURSOR	hCursor = LoadCursorFromFile("Glove Normal.cur");

		SendMessage(hwnd1, WM_SETCURSOR, 0, (LPARAM) hCursor);
		SetWindowLong(hwnd1, GCL_HCURSOR, (LONG)hCursor);
		SetWindowLongPtr(hwnd1, GCL_HCURSOR, (LONG_PTR)hCursor);

		// SetClassLong (buton, GCL_HCURSOR, (LONG) LoadCursor (NULL, IDC_CROSS)); //// standartuli kursoris chasma
		 SetClassLong (hwnd1, GCL_HCURSOR, (LONG) hCursor);
		 


MSG msg;
int s=1;
	while(s!=0)
	{
	s=GetMessage(&msg,0,0,0);
	TranslateMessage(&msg);
	DispatchMessage(&msg);
	}
}