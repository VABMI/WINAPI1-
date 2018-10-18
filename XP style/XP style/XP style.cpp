#pragma comment(linker,"\"/manifestdependency:type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")
//#define _WIN32_IE 0x0400
#include"stdafx.h"

#include <windows.h>
#include <commctrl.h>
#include <tchar.h>
//#include "resource.h"


int WINAPI WinMain (HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nShowCmd)
{
    INITCOMMONCONTROLSEX iccx;
    iccx.dwSize=sizeof(INITCOMMONCONTROLSEX);
    iccx.dwICC=ICC_ANIMATE_CLASS;
    InitCommonControlsEx(&iccx); 
    
    MessageBox (NULL, TEXT("Hello, Windows XP!"), TEXT("HelloMsg"), 0);
    return 0;
}