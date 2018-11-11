#include <windows.h>
#include <stdio.h>
#include <conio.h>
#include <math.h>
	//#include <graphics.h>
#include <dos.h>
HFONT hfont_global;

#include "mouse_msg.cpp"
#include "kbd_msg.cpp"
#include "cmd_msg.cpp"
#include "paint.cpp"
#include "on_create.cpp"
 


//----------------------------------------------------------------

//----------------------------------------------------------------
long __stdcall window_main_function_chvenia(HWND hwnd,unsigned int message, unsigned int wparam,long lparam)
{
	switch(message)
	{
		case WM_CREATE:

			{	 HDC hdc=GetDC(GetDesktopWindow());
					    int gd = 0,gm;
    int angle = 0;
    double x, y;
  //  while(1)
	{
		SendMessage(GetDesktopWindow(),WM_PAINT,0,1);
		InvalidateRect(GetDesktopWindow(),0,1);
		Sleep(1000);

 
  //  initgraph(&gd, &gm, "C:\\TC\\BGI");
 
// line(0, getmaxy() / 2, getmaxx(), getmaxy() / 2);
 /* generate a sine wave */
 for(x = 0; x < 1350; x+=1) {
 
     /* calculate y value given x */
     y = 50*sin(angle*3.141/180);
     y = 5/2 - y;
 
	 y+=500;
//	 x+=1000;
	for(int i=0;i<=1;i++)
		{
					
				for(int j=0;j<=0;j++)
				{
					SetPixel(hdc,x+j,y+i,RGB(8, 130, 211));
					SetPixel(hdc,x-j,y-i,RGB(8, 130, 211));
					SetPixel(hdc,x-j,y+i,RGB(8, 130, 211));
					SetPixel(hdc,x+j,y-i,RGB(8, 130, 211));
				}

		}
	//Sleep(1);

 // delay(100);
  y-=500;
//	 x-=1000;
  /* increment angle */
  angle+=9;
 }
 







	}
	 //   ReleaseDC(GetDesktopWindow(),hdc);
	//SendMessage(hwnd,WM_SETFONT,(UINT)hfont,0);

		on_create(hwnd,message,wparam,lparam);
	
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
}