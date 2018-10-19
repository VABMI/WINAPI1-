#include <Windows.h>
#include <windowsx.h>
#include <stdlib.h>
#include <stdio.h>
#define IDC_BUTTON   0
HWND hwndglob;
int ID=1;
LRESULT CALLBACK WndProc (HWND, UINT, WPARAM, LPARAM) ;
LRESULT CALLBACK WndProcButton (HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam);

HWND    g_hwndButton            = NULL;   HWND       g_hwndButton1	=NULL;
WNDPROC g_wndProcButtonOrigianl = NULL;   
BOOL    g_bSeeingMouse          = FALSE;

int WINAPI WinMain (HINSTANCE hInstance, HINSTANCE hPrevInstance,
                    LPSTR szCmdLine, int iCmdShow)
{
     static TCHAR szClassName[] = TEXT ("HelloWin") ;
     HWND         hwnd ;
     MSG          msg ;
     WNDCLASS     wndclass ;

     wndclass.style         = CS_HREDRAW | CS_VREDRAW ;
     wndclass.lpfnWndProc   = WndProc ;
     wndclass.cbClsExtra    = 0 ;
     wndclass.cbWndExtra    = 0 ;
     wndclass.hInstance     = hInstance ;
     wndclass.hIcon         = LoadIcon (NULL, IDI_APPLICATION) ;
     wndclass.hCursor       = LoadCursor (NULL, IDC_ARROW) ;
     wndclass.hbrBackground = (HBRUSH) GetStockObject (WHITE_BRUSH) ;
     wndclass.lpszMenuName  = NULL ;
     wndclass.lpszClassName = szClassName ;

     if (!RegisterClass (&wndclass))
     {
          MessageBox (NULL, TEXT ("This program requires Windows NT!"), 
                      szClassName, MB_ICONERROR) ;
          return 0 ;
     }

     hwndglob = CreateWindow (szClassName,                // window class name
                          TEXT ("The Hello Program"), // window caption
                          WS_OVERLAPPEDWINDOW,        // window style
                          CW_USEDEFAULT,              // initial x position
                          CW_USEDEFAULT,              // initial y position
                          CW_USEDEFAULT,              // initial x size
                          CW_USEDEFAULT,              // initial y size
                          NULL,                       // parent window handle
                          NULL,                       // window menu handle
                          hInstance,                  // program instance handle
                          NULL) ;                     // creation parameters

     ShowWindow (hwndglob, iCmdShow) ;
     UpdateWindow (hwndglob) ;

     while (GetMessage (&msg, NULL, 0, 0))
     {
          TranslateMessage (&msg) ;
          DispatchMessage (&msg) ;
     }
     return msg.wParam ;
}


LRESULT CALLBACK WndProcButton (HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{

	   WNDPROC wpOld = (WNDPROC)GetWindowLongPtr(g_hwndButton, GWLP_USERDATA);

	   if(wpOld)
	   {
			   switch(message)
			   {
				             case WM_ERASEBKGND:

				   	 MessageBox(hwndglob,L"WM_ERASEBKGND",L"WM_ERASEBKGND",0);


					 break;
					case WM_NCDESTROY:
						 MessageBox(hwndglob,L"WM_NCDESTROY",L"WM_NCDESTROY",0);
					 break;
					    case WM_GETDLGCODE:

								 MessageBox(hwndglob,L"WM_GETDLGCODE",L"WM_GETDLGCODE",0);
							break;
			   }
			    

	   }










	 switch( message )
    {
   
      break;


	      case WM_KEYDOWN:

			  	
			//   g_wndProcButtonOrigianl = (WNDPROC)SetWindowLongPtr( g_hwndButton, GWLP_WNDPROC, (LONG_PTR)WndProcButton );
			   
			     SetFocus( GetDlgItem(hwndglob,ID));
				 if(ID==5)
				 {


					ID=0;
				 }
				 ID++;
			  break;

    }









    switch( message )
    {
    case WM_COMMAND:
		if(wParam==5)
		{

			MessageBox(hwndglob,L"button",L"asada",0);
		}
      //  SetFocus( g_hwndButton );
        break;
    default:
        if( !g_bSeeingMouse && GetCapture() == hwnd )
        {
            g_bSeeingMouse = TRUE;
            SetWindowText( hwnd, L"Ok +mwwwouse" );
        }
        else if( g_bSeeingMouse && GetCapture() != hwnd )
        {
            g_bSeeingMouse = FALSE;
            SetWindowText( hwnd, L"Ok" );
        }
        break;
    }
    return CallWindowProc( g_wndProcButtonOrigianl, hwnd, message, wParam, lParam );
}

LRESULT CALLBACK WndProc (HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
     HDC         hdc ;
     PAINTSTRUCT ps ;
     RECT        rect ;

	char hh[100];
     switch (message)
     {

	        case WM_ACTIVATE:

				    if(LOWORD(wParam) == WA_INACTIVE)
					{
				//	 MessageBox(hwndglob,L"WA_INACTIVE",L"WA_INACTIVE",0);
					}


		 break;


		         case WM_SETFOCUS:

					 
			
					sprintf(hh,"%i",ID);
					//	  MessageBox(hwndglob,(LPCWSTR)hh,(LPCWSTR)hh,0);
					  if(ID==5)
					  { 
						  
					//	  MessageBox(hwndglob,L"WA_INACTIVE",L"WA_INACTIVE",0);
					  
				 
					  }

					
						 break;




     case WM_CREATE:
				
	///     g_hwndButton1 =      CreateWindow( L"Button",L"Ok",( WS_VISIBLE | WS_CHILD  |WS_BORDER),10,180+70,100,30,hwnd,(HMENU)5,(HINSTANCE)GetWindowLongPtr( hwnd, GWLP_HINSTANCE ),NULL );  
	///	g_wndProcButtonOrigianl = (WNDPROC)SetWindowLongPtr( g_hwndButton1, GWLP_WNDPROC, (LONG_PTR)WndProcButton );




				 g_hwndButton =      CreateWindow( L"Edit",L"Ok",( WS_VISIBLE | WS_CHILD  |WS_BORDER),10,10,500,500,hwnd,(HMENU)1,(HINSTANCE)GetWindowLongPtr( hwnd, GWLP_HINSTANCE ),NULL );    
				g_wndProcButtonOrigianl = (WNDPROC)SetWindowLongPtr( g_hwndButton, GWLP_WNDPROC, (LONG_PTR)WndProcButton );
				   SetFocus( g_hwndButton );
		 /*

			       g_hwndButton =      CreateWindow( L"Edit",L"Ok",( WS_VISIBLE | WS_CHILD  |WS_BORDER),10,60,100,30,hwnd,(HMENU)2,(HINSTANCE)GetWindowLongPtr( hwnd, GWLP_HINSTANCE ),NULL );    
				    g_wndProcButtonOrigianl = (WNDPROC)SetWindowLongPtr( g_hwndButton, GWLP_WNDPROC, (LONG_PTR)WndProcButton );



				   		 
		       g_hwndButton =      CreateWindow( L"Edit",L"Ok",( WS_VISIBLE | WS_CHILD  |WS_BORDER),10,120,100,30,hwnd,(HMENU)3,(HINSTANCE)GetWindowLongPtr( hwnd, GWLP_HINSTANCE ),NULL );  
		      g_wndProcButtonOrigianl = (WNDPROC)SetWindowLongPtr( g_hwndButton, GWLP_WNDPROC, (LONG_PTR)WndProcButton );
		 

			       g_hwndButton =      CreateWindow( L"Edit",L"Ok",( WS_VISIBLE | WS_CHILD  |WS_BORDER),10,180,100,30,hwnd,(HMENU)4,(HINSTANCE)GetWindowLongPtr( hwnd, GWLP_HINSTANCE ),NULL );  
		      g_wndProcButtonOrigianl = (WNDPROC)SetWindowLongPtr( g_hwndButton, GWLP_WNDPROC, (LONG_PTR)WndProcButton );
		 
		 */

			     CreateWindow( L"Button",L"Ok",( WS_VISIBLE | WS_CHILD  |WS_BORDER),10,10,100,30,g_hwndButton,(HMENU)5,(HINSTANCE)GetWindowLongPtr( hwnd, GWLP_HINSTANCE ),NULL ); 

			   
				    // pointer not needed
			
      
          return 0 ;

     case WM_PAINT:
          hdc = BeginPaint (hwnd, &ps) ;

          GetClientRect (hwnd, &rect) ;

          DrawText (hdc, TEXT ("Hello, Windows 98!"), -1, &rect,DT_SINGLELINE | DT_CENTER | DT_VCENTER) ;

          EndPaint (hwnd, &ps) ;
          return 0 ;

     case WM_DESTROY:
          PostQuitMessage (0) ;
          return 0 ;
     }
     return DefWindowProc (hwnd, message, wParam, lParam) ;
}