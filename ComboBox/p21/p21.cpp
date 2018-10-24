#include <windows.h>
#include <stdio.h>

HFONT hfont_global;
#define IDD_COUNTRIES_CBO 55
//#include "mouse_msg.cpp"
	//#include "kbd_msg.cpp"
	//	#include "cmd_msg.cpp"
//#include "paint.cpp"
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

		 case WM_COMMAND: // Windows Controls processing
        switch(LOWORD(wparam)) // This switch identifies the control
        {
        case IDD_COUNTRIES_CBO: // If the combo box sent the message,
            switch(HIWORD(wparam)) // Find out what message it was
            {
            case CBN_DROPDOWN: // This means that the list is about to display
                MessageBox(hwnd, "CBN_DROPDOWN",
                           "Display Notification", MB_OK);
                break;

			case CBN_CLOSEUP :


				   MessageBox(hwnd, "CLOSEUP","CLOSEUP", MB_OK);

				break;
			case CBN_SELCHANGE:

				   MessageBox(hwnd, "Archeva","archeva", MB_OK);
				break;
			case CBN_DBLCLK:

				  MessageBox(hwnd, "CBN_DBLCLK","CBN_DBLCLK", MB_OK);
				break;
            }
        break;
        case IDCANCEL:
            EndDialog(hwnd, 0);
            return TRUE;
        }
        break;

		
		case WM_RBUTTONDOWN:
		case WM_LBUTTONDOWN:
		case WM_MOUSEMOVE:
		case WM_RBUTTONDBLCLK:
		case WM_LBUTTONDBLCLK:
		//on_mouse(hwnd,message,wparam,lparam);
		break;

		case WM_PAINT:
		//on_paint(hwnd,message,wparam,lparam);
		break;

		case WM_KEYDOWN:
		//on_kbd(hwnd,message,wparam,lparam);
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