.#include <windows.h>
#include <string>
#include <fstream>
#include <winver.h>
#include <stdio.h>
#include "commctrl.h"

#include "defines.h"
#include "globals.h"
#include "functions.cpp"

#include "mouse_msg.cpp"
#include "kbd_msg.cpp"
#include "cmd_msg.cpp"
#include "paint_msg.cpp"
#include "on_create_msg.cpp"
#include "on_destroy_msg.cpp"



#pragma comment(lib,"comctl32.lib")

#define MYBITMAP L"C:\\Users\\vaxoa\\OneDrive\\Desktop\\MARBLES.BMP";

#define IDR_MYMENU 101
#define IDI_MYICON 201

#define ID_FILE_EXIT 9001
#define ID_STUFF_GO 9002




HWND hlogom,buton;
HBITMAP hlogo,hgener;
//HICON hIconSm;

HINSTANCE   hInst;
HICON       main_icon;

HBITMAP hImage;
HCURSOR hCursor;

///////////// bitmap /////////
	HWND statica;
HWND container, picture;
HBITMAP mybitmap,mybut;
//----------------------------------------------------------------
void img(){
//	hlogo=LoadImage(NULL, (LPCSTR)L"C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\icon\\BMP.png", IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE | LR_LOADTRANSPARENT);


}
//----------------------------------------------------------------
long __stdcall window_main_function_chvenia(HWND hwnd,unsigned int message, unsigned int wparam,long lparam)
{
	HWND btn;
	switch(message)
	{
		case WM_CREATE:

	/*img();
		//	SendMessageW(hlogom,STM_SETIMAGE,IMAGE_BITMAP,(LPARAM)hlogo); 
			
			
		///	 main_icon = LoadIcon(hInst, MAKEINTRESOURCE("C:\\Users\vaxoa\OneDrive\Desktop\Itzikgur-My-Seven-Downloads-2.icns"));

					//main_icon = LoadIcon(NULL,IDI_MYICON);
			
			SendMessage(hwnd, WM_SETICON, ICON_BIG, (LPARAM)main_icon);
			hIconSm = (HICON) LoadIcon(NULL, "C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\icon\\ico.ico", IMAGE_ICON, 16, 16, LR_LOADFROMFILE);
			   SendMessage(hwnd, WM_SETICON, ICON_SMALL, (LPARAM)hIconSm);
	//	on_create(hwnd,message,wparam,lparam);
	
	   ///   hMenu = CreateMenu();


			// HMENU hMenu, hSubMenu;
	
	*/
			/////////////////////////////////// aqedan 




	///////////////////////////////////// iconebi 
  
        HICON hIcon, hIconSm;

	   hIcon = (HICON)LoadImage(NULL, "C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\icon\\Debug\\Itzikgur-My-Seven-Downloads-2.ico", IMAGE_ICON, 32, 32, LR_LOADFROMFILE);
        if(hIcon)
            SendMessage(hwnd, WM_SETICON, ICON_BIG, (LPARAM)hIcon);
        else
            MessageBox(hwnd, "Could not load large icon!", "Error", MB_OK | MB_ICONERROR);

        hIconSm = (HICON)LoadImage(NULL,"Itzikgur-My-Seven-Downloads-2.ico", IMAGE_ICON, 16, 16, LR_LOADFROMFILE);
        if(hIconSm)
            SendMessage(hwnd, WM_SETICON, ICON_SMALL, (LPARAM)hIconSm);
        else
            MessageBox(hwnd, "Could not load small icon!", "Error", MB_OK | MB_ICONERROR);

		///////////////////////////// end icon 
		/////////////    cursorebi
		hCursor = LoadCursorFromFile("C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\icon\\cur197.ani");
		SetCursor(hCursor);
  

		
		hCursor = LoadCursorFromFile("cur197.ani");
		SetCursor(hCursor);

	

	//	int GCL_Hcursor = -12; //GCL_HCURSOR
hCursor = LoadCursorFromFile("C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\icon\\cur197.ani");
SetCursor(hCursor);
SetClassLong(hwnd, -12, (DWORD)hCursor);
//////////////////////////////////// end cursor
		break;
		
		case WM_COMMAND:
	
	//	on_cmd(hwnd,message,wparam,lparam);
		break;
		
		case WM_RBUTTONDOWN:
		case WM_LBUTTONDOWN:
		case WM_MOUSEMOVE:
		case WM_RBUTTONDBLCLK:

		case WM_LBUTTONDBLCLK:
	//	on_mouse(hwnd,message,wparam,lparam);
		break;

		case WM_PAINT:
		on_paint(hwnd,message,wparam,lparam);
		break;

		case WM_KEYDOWN:

		
		break;
		case WM_SIZE:
			/*
				    statica=GetDlgItem(hwnd,123);
			RECT r;
			GetClientRect(hwnd,&r);
			MoveWindow(statica,10,10,r.right-20,r.bottom-20,1);
			InvalidateRect(statica,0,1);
			*/
			break;
		case WM_CTLCOLOREDIT:
		{
		HWND ctrl=(HWND)lparam;
		HDC hdc=(HDC)wparam;
		HBRUSH brush = CreateSolidBrush(color);
		SetBkMode(hdc,1);
		SetTextColor(hdc,(COLORREF)brush);
		return (long)brush;
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

int __stdcall WinMain(HINSTANCE hinstance,HINSTANCE,char *,int)
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
  //wc.hbrBackground = GetSysColorBrush(COLOR_3DFACE);
wc.hCursor=LoadCursor(NULL,IDC_WAIT);

//wc.hIcon = LoadIcon(NULL,IDI_QUESTION);
///wc.hIcon=(HICON)LoadImage(0,"c:\\1.ico",IMAGE_ICON,16,16,LR_LOADFROMFILE);


 // wc.lpszMenuName  = MAKEINTRESOURCE(IDR_MYMENU);
 //   wc.hIcon  = LoadIcon(GetModuleHandle(NULL), MAKEINTRESOURCE(IDI_MYICON));
  // wc.hIconSm  = (HICON)LoadImage(GetModuleHandle(NULL), MAKEINTRESOURCE(IDI_MYICON), IMAGE_ICON, 16, 16, 0);

if(RegisterClass(&wc)==0)
	{
	MessageBox(hwnd,"RegisterClass error",0,0);
	return 0;
	}
  

//mtavari fanjara:
style=WS_VISIBLE|WS_OVERLAPPEDWINDOW|WS_CLIPCHILDREN;
X=10;Y=30;W=750;H=500;
hwnd=CreateWindow(wc.lpszClassName,"Main",style,X,Y,W,H,0,0,0,0);
 RECT r;
GetClientRect(hwnd,&r);


hlogom=CreateWindowW(L"static",NULL,WS_VISIBLE|WS_CHILD|SS_BITMAP|WS_BORDER,10,10, 300,300,hwnd,(HMENU)123,NULL,NULL);
buton=CreateWindowW(L"Button",NULL,BS_BITMAP|WS_VISIBLE|WS_CHILD,350,10, 320,320,hwnd,(HMENU)124,NULL,NULL);






//DialogBox(hinstance,MAKEINTRESOURCE(IDB_DIALOG1),0,window_main_function_chvenia);

/*
hImage = (HBITMAP)LoadBitmap(NULL,"C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\icon\\BMP.png", IMAGE_ICON, 100, 100, LR_LOADFROMFILE);
SendMessage(hlogom, STM_SETIMAGE, IMAGE_BITMAP, (LPARAM)hImage);




 mybitmap = (BITMAP)LoadBitmap(hInst, MAKEINTRESOURCE("C:\\Users\\vaxoa\\OneDrive\\Desktop\\MARBLES.BMP"));
     SendDlgItemMessage(container, PICTURE, STM_SETIMAGE, (WPARAM)"C:\\Users\\vaxoa\\OneDrive\\Desktop\\MARBLES.BMP", (LPARAM)mybitmap);
	 */



//////////////////////////////////// -------- BMP suratis chasma main window------------- ///////////////////////

		mybitmap= (HBITMAP)LoadImage(NULL,"C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\MARBLES.BMP", IMAGE_BITMAP,300,300, LR_LOADFROMFILE);
      mybut= (HBITMAP)LoadImage(NULL,"C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\MARBLES.BMP", IMAGE_BITMAP,300,300, LR_LOADFROMFILE);
//	mybut= (HBITMAP)LoadImage(GetModuleHandle(NULL),MAKEINTRESOURCE("C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\MARBLES.BMP"),IMAGE_BITMAP,NULL,NULL,LR_DEFAULTCOLOR);
//  mybitmap=(HBITMAP)LoadBitmap(NULL, MAKEINTRESOURCE("C:\\Users\\vaxoa\\OneDrive\\Desktop\\MARBLES.BMP"));
        if(mybitmap)
		{
            SendMessage(buton, (UINT)BM_SETIMAGE,   (WPARAM)IMAGE_BITMAP, (LPARAM)mybut);
			SendMessage(hlogom,STM_SETIMAGE,   (WPARAM)IMAGE_BITMAP, (LPARAM)mybut);
		}
		
		else{
			 MessageBox(hwnd, "Could not load Image !", "Error", MB_OK | MB_ICONERROR);
			}
/////////////////---------------- end BMP ------------------////////////////////////////////
		///////////////////////////////////////////////////////////////////
		////// -------------gilakistvis kursoris chasma -----------------///////////////
		hCursor = LoadCursorFromFile("too994.ani");

		SendMessage(buton, WM_SETCURSOR, 0, (LPARAM) hCursor);
		SetWindowLong(buton, GCL_HCURSOR, (LONG)hCursor);
		SetWindowLongPtr(buton, GCL_HCURSOR, (LONG_PTR)hCursor);

		// SetClassLong (buton, GCL_HCURSOR, (LONG) LoadCursor (NULL, IDC_CROSS)); //// standartuli kursoris chasma
		 SetClassLong (buton, GCL_HCURSOR, (LONG) hCursor);



			 ///// ---------- end ------------////////////
		/////////////////////////////////////////////////


	//	ShowWindow(buton,SW_HIDE);
	//	ShowWindow(buton,SW_SHOW);

		//HRGN hrgn;
		//	hrgn=CreateEllipticRgn(10,10,10,10);
	//		SetWindowRgn(buton,hrgn,1);

	MSG msg;
	while(GetMessage(&msg,0,0,0))
	{
	TranslateMessage(&msg);
	DispatchMessage(&msg);
	}
}