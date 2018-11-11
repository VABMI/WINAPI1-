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
	switch(message)
	{
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




		case WM_CTLCOLORSTATIC:
	case WM_CTLCOLORBTN:
		{	HWND hgf=GetDlgItem(hwnd,10);
			PAINTSTRUCT paintStruct;	
			HDC	hdc = BeginPaint(hwnd, &paintStruct);
				hdc = reinterpret_cast<HDC>(wparam);
			/*
		
	
			SetBkMode(hdc, TRANSPARENT);
			EndPaint(hgf, &paintStruct);

		//	return (LRESULT)GetStockObject(NULL_BRUSH);

		*/

						hdc = BeginPaint(hgf, &paintStruct);
			SetBkMode(hdc, TRANSPARENT);
			EndPaint(hgf, &paintStruct);

			return (LRESULT)GetStockObject(NULL_BRUSH);





		}





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
wc.hbrBackground=(HBRUSH)CreateSolidBrush(RGB(221,200,148));
wc.hIcon=(HICON)LoadImage(0,"c:\\1.ico",IMAGE_ICON,16,16,LR_LOADFROMFILE);

	if(RegisterClass(&wc)==0)
	{
	MessageBox(hwnd,"RegisterClass error",0,0);
	return 1;
	}


//mtavari fanjara:
style=WS_VISIBLE|WS_OVERLAPPEDWINDOW|WS_CLIPCHILDREN|WS_VSCROLL;
X=10;Y=10;W=500;H=500;
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