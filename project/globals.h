#include <windows.h>

#include <WinUser.h>

#include <stdio.h>
#include <dos.h>
#include <stdio.h>

#include "windef.h"
#include "winbase.h"
#include "shlwapi.h"
#include "commctrl.h"
#include "defines.h"
#pragma comment(lib,"comctl32.lib")





#if defined _M_IX86
#pragma comment(linker,"/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='x86' publicKeyToken='6595b64144ccf1df' language='*'\"")
#elif defined _M_IA64
#pragma comment(linker,"/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='ia64' publicKeyToken='6595b64144ccf1df' language='*'\"")
#elif defined _M_X64
#pragma comment(linker,"/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='amd64' publicKeyToken='6595b64144ccf1df' language='*'\"")
#else
#pragma comment(linker,"/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")
#endif




#define LOGOUT 1233




//////////////////////////////////
#define mtavari 1323
#define combo 1324
#define RightTableID 1325
#define RightTableID2 1326
#define statiki_satis_marjvniv1 6548
#define Dakecvis_Damalvis_gilaki1 6698
#define statiki_satis_marjvniv2 6549
#define Dakecvis_Damalvis_gilaki2 6699
///////////////////////////////



void login(HWND hwnd);
void reg(HWND hwnd);
void page(HWND hwnd);
void MessangerForm(HWND);

HWND DoCreateStatusBar(HWND hwndParent, int idStatus, HINSTANCE hinst, int cParts);
void ssdsf(HWND hwnd,bool,void*);
char *open1(HWND hwnd);
void date(HDC hdc,HWND,HWND,HWND,HWND,HWND,HWND,int,char *);
//void(HWND hwnd);

	static int uuo=0;
	static int tre=0;
	static 	bool blsize=0;
	static 	bool ShowHideBL=0;
	static 	bool ShowHideBL2=0;
	static 	bool ShowHideBL3=0;
	static 	bool ShowHideBL22=0; /// dasalagebelia numeracia 
	static bool dacileba=0;
	