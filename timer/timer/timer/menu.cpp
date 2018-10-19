#include "head.h"

long __stdcall menu(HWND hwnd,UINT sms,WPARAM wp,LPARAM lp)
{
	HMENU main=CreateMenu();
	HMENU new1=CreateMenu();
	AppendMenu(new1,MF_STRING,1,L"SetTimer");
		AppendMenu(new1,MF_STRING,2,L"Kill Timer");
	AppendMenu(main,MF_POPUP,(UINT_PTR)new1,L"file");
	SetMenu(hwnd,main);
	return 0;
}