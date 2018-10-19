#include "globals.h"







#define PACKVERSION(major,minor) MAKELONG(minor,major)

DWORD GetVersion(LPCTSTR lpszDllName)
{

	OSVERSIONINFOEXA info1;


    HINSTANCE hinstDll;
    DWORD dwVersion = 0;

    // For security purposes, LoadLibrary should be provided with a fully qualified 
    // path to the DLL. The lpszDllName variable should be tested to ensure that it 
    // is a fully qualified path before it is used. 
    hinstDll = LoadLibrary(lpszDllName);
    
    if(hinstDll)
    {
        DLLGETVERSIONPROC pDllGetVersion;
        pDllGetVersion = (DLLGETVERSIONPROC)GetProcAddress(hinstDll, "DllGetVersion");

        // Because some DLLs might not implement this function, you must test for 
        // it explicitly. Depending on the particular DLL, the lack of a DllGetVersion 
        // function can be a useful indicator of the version. 

        if(pDllGetVersion)
        {
            DLLVERSIONINFO dvi;
            HRESULT hr;

            ZeroMemory(&dvi, sizeof(dvi));
            dvi.cbSize = sizeof(dvi);
		
            hr = (*pDllGetVersion)(&dvi);
			
            if(SUCCEEDED(hr))
            {
               dwVersion = PACKVERSION(dvi.dwMajorVersion, dvi.dwMinorVersion);
            }
        }
        FreeLibrary(hinstDll);
    }
    return dwVersion;
}



















//#include "combo func.cpp"
	HWND hwnd=0;
HDC hdc;
bool wmsize=false;
unsigned int msg1;UINT msg2;
LPMSG msl;
long __stdcall window_main_function_chvenia(HWND hwnd,unsigned int message, unsigned int wparam,long lparam);
DWORD WINAPI mainn();


int __stdcall WinMain(HINSTANCE,HINSTANCE,char *,int)
{
	 mainn();
	//  CreateThread( NULL, 0, mainn,(void*) hwnd, 0, NULL);  
}
///// smses glob //////
HWND hw2,hw1,hw4;
#include "kbd_msg.cpp"
//////////////////////








long __stdcall window_main_function_chvenia(HWND hwnd,unsigned int message, unsigned int wparam,long lparam)
{ 
 #include "hdd.cpp"
 #include "SetCursor.cpp"
	RECT r;
 if(message==WM_COMMAND)
  {



//////////////////////////////// suratis archeva   //////////////////////////////////////////
#include "suratisarcheva.cpp"
//////////////////////////////// damtavreba suratis asarchevaa /////////////////////////////////////////

/////////////// button login ///////////////
	#include"login3.cpp"			////////
/////////////// REG BUTTON /////////////////
	#include"login5.cpp"			////////
///////////	END REG BUTTON /////////////////
/////////////// BUTTON LOGOUT //////////////
	#include"logout.cpp"			////////
									////////
////////////////////////////////////////////






  }










  if(message==WM_CREATE)
  {
	
	
	  page(hwnd);
	  login(hwnd);

	
  }






 
	switch(message)
	{


		   case WM_WINDOWPOSCHANGED : 
           	//	MessageBox(hwnd,"ssa","ssa",1);
		 
            break; 

	case WM_COMMAND:

		switch(wparam)
	{	

	/////////////////////////////////////// registraciis Save gilaki ////////////////////////////////////////////////////////////////////////
		case 124:
		
				ssdsf(hwnd,mamrobMdedrob,reinterpret_cast<void*>(BufImagePath));

		break;


		////////////////////////////////////////////
	    	case 121:
			mamrobMdedrob=1;
	
			break;


			case 122:
			mamrobMdedrob=0;
		
			break;

///////////////////////////////////////////////////////////////////////////////



	}





	
		break;
	case WM_CREATE:
	//				 DoCreateStatusBar(hwnd,6,0,7);
	//	CreateThread(0,0,reading,hwnd,0,0);
		reg(hwnd);

	break;

	case WM_SIZE:
		if(wmsize)
		{ DoCreateStatusBar(hwnd,6,0,7);
				
			SendMessage(GetDlgItem(hwnd,6),SB_SETTEXT,0,(LPARAM)"Asdad");
		}
		
	RECT r;
	GetClientRect(hwnd,&r);
	
#include "MoveWindow.cpp"
		






	
		InvalidateRect(edit1,0,1);
		InvalidateRect(edit2,0,1);
		InvalidateRect(but2,0,1);
		MoveWindow(edit1,r.right/2-125,r.bottom/2-50,250,30,1);
		MoveWindow(edit2,r.right/2-125,r.bottom/2,250,30,1);

		MoveWindow(but1,r.right/2-125,r.bottom/2+50,100,30,1);
		MoveWindow(but2,r.right/2,r.bottom/2+50,125,30,1);

		MoveWindow(static1,r.right/2-50,r.bottom/2-155,100,100,1);
		InvalidateRect(hwnd,0,1);
	
	//	
		break;
	case WM_PAINT:


		break;
	case WM_SYSCOMMAND:
		switch(wparam)
		{
			case SC_MAXIMIZE :
	

			break;

		}
	break;

	case WM_CLOSE:
		
		exit(1);
		break;

	}

	

return DefWindowProc(hwnd,message,wparam,lparam);
}

DWORD WINAPI mainn()
{



int X,Y,W,H;
ULONG style=0;

WNDCLASS wc;
ZeroMemory(&wc,sizeof(WNDCLASS));

wc.style=CS_DBLCLKS;
wc.lpfnWndProc=(WNDPROC)&window_main_function_chvenia;
wc.lpszClassName="12";
wc.hbrBackground=(HBRUSH)CreateSolidBrush(RGB(51, 103, 0));
	//	wc.hbrBackground = (HBRUSH)(COLOR_BTNFACE + 1);
wc.hIcon=(HICON)LoadImage(0,"c:\\1.ico",IMAGE_ICON,16,16,LR_LOADFROMFILE);

	if(RegisterClass(&wc)==0)
	{
	MessageBox(hwnd,"RegisterClass error",0,0);
	return 0;
	}


//mtavari fanjara:
style=WS_VISIBLE|WS_OVERLAPPEDWINDOW|WS_CLIPCHILDREN;
X=10;Y=30;W=1250;H=900;
hwnd=CreateWindow(wc.lpszClassName,"Main",style,X,Y,W,H,0,0,0,0);



//CreateWindow("MDICLIENT","asas",WS_CHILD|WS_HSCROLL ,100,100,0,100,hwnd,0,0,0);
	//ShowWindow(hwnd,SW_MAXIMIZE);


MSG msg;
	while(GetMessage(&msg,0,0,0))
	{
	TranslateMessage(&msg);
	DispatchMessage(&msg);
	}

}