#include <windows.h>
#include <string>
#include <fstream>
#include <winver.h>
#include <stdio.h>
#include "commctrl.h"
HINSTANCE hInst;
HWND hProgress;
DWORD IDC_TIMER;
DWORD nCounter;
BOOL WINAPI DlgProc(HWND,UINT,WPARAM,LPARAM);
int WINAPI WinMain(HINSTANCE hInstance,HINSTANCE HPrevInstance,LPSTR lpCmdLine,int nint)
{

	hInst=hInstance;
	DialogBox(hInstance,MAKEINTRESOURCE(IDD_DIALOG1),0,DlgProc);



}