#pragma comment(linker,"\"/manifestdependency:type='win32' \
name='Microsoft.Windows.Common-Controls' version='6.0.0.0' \
processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"") 

#include <Windows.h>
#include <Commctrl.h>

#define IDC_EXIT_BUTTON 101
#define IDC_PUSHLIKE_BUTTON 102
#define IDC_PUSHLIKE_BUTTON1 103
#define IDC_PUSHLIKE_BUTTON2 104
///////////////// CreateGradientBrush ////////////////////////
#include "CreateGradientBrush.cpp"
LRESULT CALLBACK MainWindow(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{ 
///////////////////////////////////// HBRUSH ///////////////////////////////////////////
			#include "HBRUSHES.cpp"


    switch (msg)
    {


		/////////////////////////// WM_CREATE //////////////////////////////////////////
			#include "WM_CREATE.cpp"
		////////////////////////// WMWM_COMMAND ///////////////////////////////////////
			#include "WM_COMMAND.cpp"
 

/////////////////////////////////// WM_NOTIFY /////////////////////////
		#include "Wm_notify.cpp"



        
        case WM_CTLCOLORBTN: //In order to make those edges invisble when we use RoundRect(),
            {                //we make the color of our button's background match window's background



				    HBRUSH hBrushbtn;
					hBrushbtn = (HBRUSH)GetStockObject(NULL_BRUSH);
					SetBkMode((HDC) wParam, TRANSPARENT);
					return ((LRESULT)hBrushbtn);


            //  return (LRESULT)GetSysColorBrush(COLOR_3DDKSHADOW);
            }
        break;
        case WM_CLOSE:
            {
                DestroyWindow(hwnd);
                return 0;
            }
        break;


/////////////////////////////////////////	WM_DESTROY ///////////////////////////////////////
#include "WM_DESTROY.cpp"
 
        default:
            return DefWindowProc(hwnd, msg, wParam, lParam);
    }
    return 0;
}

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
    WNDCLASSEX wc;
    HWND hwnd;
    MSG msg;
    const wchar_t ClassName[] = L"Main_Window";

    wc.cbSize        = sizeof(WNDCLASSEX);
    wc.style         = 0;
    wc.lpfnWndProc   = MainWindow;
    wc.cbClsExtra    = 0;
    wc.cbWndExtra    = 0;
    wc.hInstance     = hInstance;
    wc.hIcon         = LoadIcon(NULL, IDI_APPLICATION);
    wc.hCursor       = LoadCursor(NULL, IDC_ARROW);
    wc.hbrBackground = (HBRUSH)CreateSolidBrush(RGB(127,127,127));
    wc.lpszMenuName  = NULL;
    wc.lpszClassName = ClassName;
    wc.hIconSm       = LoadIcon(NULL, IDI_APPLICATION);

    if(!RegisterClassEx(&wc))
    {
        MessageBox(NULL, L"Window Registration Failed!", L"Error", MB_ICONEXCLAMATION | MB_OK);
        exit(EXIT_FAILURE);
    }

    hwnd = CreateWindowEx(WS_EX_CLIENTEDGE, ClassName, L"Window", WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT, 368, 248, NULL, NULL, hInstance, NULL);

    if(hwnd == NULL)
    {
        MessageBox(NULL, L"Window Creation Failed!", L"Error!", MB_ICONEXCLAMATION | MB_OK);
        exit(EXIT_FAILURE);
    }

    ShowWindow(hwnd, nCmdShow);
    UpdateWindow(hwnd);

    while(GetMessage(&msg, NULL, 0, 0) > 0)
    {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    return msg.message;
}