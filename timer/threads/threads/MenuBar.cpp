
#include "stdafx.h"


void menubar(HWND hwnd)
{
	HMENU main=CreateMenu();
	HMENU childe=CreateMenu();
	

	AppendMenu(childe,MF_STRING,1,"SetTimer");
	

	AppendMenu(childe,MF_STRING,2,"KillTimer");
	
	AppendMenu(main,MF_POPUP,(UINT_PTR)childe,"timer");




	SetMenu(hwnd,main);

}