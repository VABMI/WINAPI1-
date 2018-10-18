#include "windows.h"
#include <math.h>
#include <CommCtrl.h>
	static bool b=1;
#define RED 3
#define GREEN 4
#define BLUE 5
#define SINUSOIDA 9
#define PROG 78

#if defined _M_IX86
#pragma comment(linker,"/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='x86' publicKeyToken='6595b64144ccf1df' language='*'\"")
#elif defined _M_IA64
#pragma comment(linker,"/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='ia64' publicKeyToken='6595b64144ccf1df' language='*'\"")
#elif defined _M_X64
#pragma comment(linker,"/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='amd64' publicKeyToken='6595b64144ccf1df' language='*'\"")
#else
#pragma comment(linker,"/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")
#endif

















	///////////////////////////////////////// PROGRESS BAR /////////////////////////////////////////////////
 
	///////////////////////////////////////////// END PROGRESS BAR /////////////////////////////////////////

HINSTANCE hInst;
HWND hwndTrack;
HWND hwndTrack2;
HWND progg;


HWND WINAPI CreateTrackbar(HWND hwnd, UINT iMin,UINT iMax,UINT iSelMin, UINT iSelMax,int,int,int,ULONG,int,int);


long __stdcall func(HWND hwnd,UINT msg,WPARAM wp,LPARAM lp)
{
	static	int	red,green,blue;

	if(msg==TRBN_THUMBPOSCHANGING)
	{

		MessageBox(hwnd,"TB_TOP","TB_TOP",0);
	}
	if(msg==WM_NOTIFY)
	{
		
					switch(((LPNMHDR)lp)->code)
					{
						//	case TB_RCLICK:
								MessageBox(hwnd,"TB_TOP","TB_TOP",0);

								//break;


					}
				

	}
	
	SCROLLINFO si;
	if(msg== WM_VSCROLL)
	{
		

		HWND hg1=(HWND)lp;

					///////////////// sinusoidallll ///////////
		if(GetWindowLong(hg1,-12)==SINUSOIDA)
		{
			
	
			
			float sixshire=SendMessage(hg1,TBM_GETPOS,0,0);
			float x,z,v,c;
			HDC hdc = GetDC(hwnd);
			for (x = 1; x < 1920; x += 0.5)
			{
				int y = sin(x * 0.09) * sixshire  + 500;
				SetPixel(hdc, x, y, RGB(0, 0, 200));
			}

			//MessageBox(hwnd,"TB_TOP","TB_TOP",0);
		}

		////////////////// sinusoidalll ////////////////////////////////////////


	}

		



	if(msg== WM_HSCROLL)
	{
		HWND hg=(HWND)lp;
		
		if(GetWindowLong(hg,-12)==PROG)
		{
			int gf=SendMessage(hg,TBM_GETPOS,0,0);
			SendMessage(progg,PBM_SETPOS,gf,0); // PBM_STEPIT
			//SendMessage(progg,PBM_STEPIT,(WPARAM)10,0);

		}

		
		if(GetWindowLong(hg,-12)==RED)
		{
			
				red=SendMessage(hg,TBM_GETPOS,0,0);
			SetClassLong(hwnd, GCL_HBRBACKGROUND, (LONG)CreateSolidBrush(RGB(red,green,blue)));
			InvalidateRect(hwnd,0,1);

		}

		if(GetWindowLong(hg,-12)==GREEN)
		{
				green=SendMessage(hg,TBM_GETPOS,0,0);
			SetClassLong(hwnd, GCL_HBRBACKGROUND, (LONG)CreateSolidBrush(RGB(red,green,blue)));
			InvalidateRect(hwnd,0,1);
		}
		
			if(GetWindowLong(hg,-12)==BLUE)
		{
					blue=SendMessage(hg,TBM_GETPOS,0,0);
			SetClassLong(hwnd, GCL_HBRBACKGROUND, (LONG)CreateSolidBrush(RGB(red,green,blue)));
			InvalidateRect(hwnd,0,1);

		}












		switch(LOWORD(wp)) {
		case	TB_BOTTOM:
			MessageBox(hwnd,"TB_BOTTOM","TB_BOTTOM",0);
			break;
		case TB_TOP:
			MessageBox(hwnd,"TB_TOP","TB_TOP",0);
			break;

		case TB_THUMBPOSITION:
			//MessageBox(hwnd,"TB_THUMBPOSITION","TB_THUMBPOSITION",0);
			break;

		    case SB_THUMBTRACK:
			
		//	MessageBox(hwnd,"SB_THUMBTRACK","SB_THUMBTRACK",0);
			/*
        case SB_THUMBTRACK:
          // Initialize SCROLLINFO structure
 
            ZeroMemory(&si, sizeof(si));
            si.cbSize = sizeof(si);
            si.fMask = SIF_TRACKPOS;
 
          // Call GetScrollInfo to get current tracking 
          //    position in si.nTrackPos
 
            if (!GetScrollInfo(hwndTrack, SB_HORZ, &si) ){
              MessageBox(hwnd,"error","sdssd",0);

			}
			*/
            break;
        
    }


		
	//	MessageBox(hwnd,"dsd","sdssd",0);

	}

	
	if(msg==WM_CREATE)
	{

		CreateTrackbar(hwnd,0,200,10,50,10,10,RED,TBS_HORZ,800,30);
			CreateTrackbar(hwnd,0,200,10,50,10,50,GREEN,TBS_HORZ,800,30);
				CreateTrackbar(hwnd,0,200,10,50,10,90,BLUE,TBS_HORZ,800,30);
				CreateTrackbar(hwnd,0,200,10,50,10,140,PROG,TBS_HORZ,800,30);
			//	CreateTrackbar(hwnd,0,50,200,50,820,10,SINUSOIDA,TBS_VERT,30,500);
						CreateTrackbar(hwnd,0,100,10,50,820,10,SINUSOIDA,TBS_VERT,30,200);

//hwndTrack=	CreateWindowEx(0,TRACKBAR_CLASS,"trackBar",WS_CHILD|WS_BORDER|WS_VISIBLE|TBS_AUTOTICKS|TBS_ENABLESELRANGE|TBS_VERT,820,10,50,500,hwnd,(HMENU)SINUSOIDA,hInst,0);
//		CreateTrackbar(hwnd,0,100,10,50,10,60,4);
	}

	if(msg==WM_LBUTTONDOWN)
	{
		
	HCURSOR hCursor=LoadCursorFromFile("C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\PixelatedMGC_LinkSelect.cur");
	SetCursor(hCursor);
	SetClassLong(hwndTrack, -12, (DWORD)hCursor);
	}

	if(msg==WM_LBUTTONUP)
	{		
		HCURSOR hCursor=LoadCursorFromFile("C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\PixelatedMGC_Move.cur");
	SetCursor(hCursor);
	SetClassLong(hwndTrack, -12, (DWORD)hCursor);

	}


	if(msg==WM_COMMAND)
	{		
	char chp[10];
	int d;
	HCURSOR hCursor=LoadCursorFromFile("C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\PixelatedMGC_Move.cur");
	SetCursor(hCursor);
	SetClassLong(hwndTrack, -12, (DWORD)hCursor);

			switch(wp)
			{

			case 1:
				d=SendMessage(hwndTrack,TBM_GETPOS,0,0);
				itoa(d,chp,9);
				MessageBox(hwnd,chp,chp,0);

			break;

			}

	}





return	DefWindowProc(hwnd,msg,wp,lp);
}
int WINAPI WinMain(HINSTANCE hInstance,HINSTANCE hPrevInst,LPSTR lpCmdLine,int nCmdShow)
{
	
	HWND hwnd;
	WNDCLASS wc={0};
	ZeroMemory(&wc,sizeof(WNDCLASS));
	wc.lpfnWndProc=(WNDPROC)&func;
	wc.lpszClassName="vaxo";
	wc.hbrBackground=(HBRUSH)CreateSolidBrush(RGB(12,55,66));
	if(!RegisterClass(&wc))
	{
		MessageBox(hwnd,"error","error",0);
		return 0;
	}

	hwnd=CreateWindow("vaxo","main",WS_VISIBLE|WS_BORDER|WS_OVERLAPPEDWINDOW,0,0,500,500,0,0,0,0);
	 progg=CreateWindowEx(0,PROGRESS_CLASS ,"WW",WS_CHILD|WS_BORDER|WS_VISIBLE,10,300,300,20,hwnd,0,0,0);



	HCURSOR hCursor=LoadCursorFromFile("C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\cur1091.ani");
	SetCursor(hCursor);
	SetClassLong(hwnd, -12, (DWORD)hCursor);
	CreateWindow("button","click",WS_VISIBLE|WS_BORDER|WS_CHILD,130,250,40,20,hwnd,(HMENU)1,0,0);
	MSG msg;
	while(GetMessage(&msg,0,0,0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);


	}
	return msg.message;


}

HWND WINAPI CreateTrackbar(HWND hwnd, UINT iMin,UINT iMax,UINT iSelMin, UINT iSelMax,int left,int top,int numm,ULONG style,int width,int height)
{

	hwndTrack=CreateWindowEx(0,TRACKBAR_CLASS,"trackBar",WS_CHILD|WS_BORDER|WS_VISIBLE|TBS_AUTOTICKS|TBS_ENABLESELRANGE|style,left,top,width,height,hwnd,(HMENU)numm,hInst,0);
	
	SendMessage(hwndTrack,TBM_SETPOS,50,(LPARAM)50);


	SendMessage(hwndTrack,TBM_SETRANGE,(WPARAM)1,(LPARAM)MAKELONG(iMin,iMax));
	SendMessage(hwndTrack,TBM_SETPAGESIZE,0,(LPARAM)30);
	SendMessage(hwndTrack,TBM_SETSEL,(WPARAM)0,(LPARAM)MAKELONG(iSelMin,iSelMax));

	SetFocus(hwndTrack);


	HCURSOR hCursor=LoadCursorFromFile("C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\PixelatedMGC_Move.cur");
	SetCursor(hCursor);
	SetClassLong(hwndTrack, -12, (DWORD)hCursor);





	return 0;
}