#include <Windows.h>
#include <windowsx.h>

#define IDC_BUTTON   0


LRESULT CALLBACK WndProc (HWND, UINT, WPARAM, LPARAM) ;
LRESULT CALLBACK WndProcButton (HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam);

HWND    g_hwndButton            = NULL;
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

     hwnd = CreateWindow (szClassName,                // window class name
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

     ShowWindow (hwnd, iCmdShow) ;
     UpdateWindow (hwnd) ;

     while (GetMessage (&msg, NULL, 0, 0))
     {
          TranslateMessage (&msg) ;
          DispatchMessage (&msg) ;
     }
     return msg.wParam ;
}


LRESULT CALLBACK WndProcButton (HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    switch( message )
    {
    case WM_COMMAND:
        MessageBox( hwnd, TEXT( "Test box" ), TEXT( "Test box" ), MB_OK );
        SetFocus( g_hwndButton );
        break;
    default:
        if( !g_bSeeingMouse && GetCapture() == hwnd )
        {
            g_bSeeingMouse = TRUE;
            SetWindowText( hwnd, L"Ok +mouse" );
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

     switch (message)
     {
     case WM_CREATE:
         g_hwndButton = CreateWindow( L"Button",                                            // predefined class
                                      L"Ok",                                                // button text
                                      ( WS_VISIBLE | WS_CHILD | BS_DEFPUSHBUTTON ),         // styles
                                      // Size and poition values are given explicitly, because
                                      // the CW_USEDEFAULT constant gives zero values for buttons.
                                      10,                                                   // starting x position
                                      10,                                                   // starting y position
                                      100,                                                  // button width
                                      30,                                                   // button height
                                      hwnd,                                                 // parent window
                                      (HMENU)IDC_BUTTON,                                    // no menu
                                      (HINSTANCE)GetWindowLongPtr( hwnd, GWLP_HINSTANCE ),
                                      NULL );                                               // pointer not needed
         SetFocus( g_hwndButton );
         g_wndProcButtonOrigianl = (WNDPROC)SetWindowLongPtr( g_hwndButton, GWLP_WNDPROC, (LONG_PTR)WndProcButton );
          return 0 ;

     case WM_PAINT:
          hdc = BeginPaint (hwnd, &ps) ;

          GetClientRect (hwnd, &rect) ;

          DrawText (hdc, TEXT ("Hello, Windows 98!"), -1, &rect,
                    DT_SINGLELINE | DT_CENTER | DT_VCENTER) ;

          EndPaint (hwnd, &ps) ;
          return 0 ;

     case WM_DESTROY:
          PostQuitMessage (0) ;
          return 0 ;
     }
     return DefWindowProc (hwnd, message, wParam, lParam) ;
}