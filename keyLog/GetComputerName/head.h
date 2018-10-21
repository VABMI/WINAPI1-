#include <windows.h>
#include <stdio.h>
#define WIN32_LEAN_AND_MEAN

#include <Windows.h>
#include <windowsx.h>
#include <CommCtrl.h>
#include <commctrl.h>

#pragma comment(lib, "ComCtl32.Lib")
//#pragma comment(linker, "\"/manifestdependency:type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")
//CreateWindowEx(WS_EX_CLIENTEDGE, WC_TREEVIEW, TEXT("TreeView"), WS_CHILD | WS_VISIBLE | TVS_HASBUTTONS | TVS_LINESATROOT, 10, 10, 400, 400, hWnd, reinterpret_cast<HMENU>(ID_TREEVIEW), GetModuleHandle(NULL), NULL);


#include <string>
#include <fstream>
#include <winver.h>
#include <stdio.h>
#include "commctrl.h"