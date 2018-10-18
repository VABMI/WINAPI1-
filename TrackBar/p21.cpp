#include<Windows.h>
#include<stdio.h>
#include<math.h>
#include "commctrl.h"
#include "define.h"
#include "select_color.cpp"
#include "on_create.cpp"
#include "command_msg.cpp"
#include "notify_msg.cpp"
#include "mouse_msg.cpp"

#pragma comment(lib,"comctl32.lib")



long __stdcall project(HWND hwnd, unsigned int message, unsigned int wparam,long lparam)
{
	static int gg=0;
	switch (message)
	{
		case WM_VSCROLL: break;
	case WM_CREATE: on_create(hwnd, message, wparam, lparam); break;
	case WM_COMMAND: command_msg(hwnd, message, wparam, lparam); break;
	case WM_HSCROLL: 
	
		break;
	case WM_NOTIFY: notify_msg(hwnd, message, wparam, lparam); break;
	case WM_MOUSEMOVE: mouse_msg(hwnd, message, wparam, lparam); break;
	case WM_SIZE: MoveWindow(GetDlgItem(hwnd, H_STATUSBAR), 5, 5, LOWORD(lparam)-10, 30, 1); break;
	case WM_DESTROY: exit(1); break;
	

	
	}


	return DefWindowProc(hwnd, message, wparam, lparam);
}
//getclientrect


void main()
{
	InitCommonControls();
	unsigned long style =
		WS_VISIBLE |
		WS_OVERLAPPED |
		WS_CAPTION |
		WS_SYSMENU |
		WS_THICKFRAME |
		WS_MINIMIZEBOX |
		WS_MAXIMIZEBOX;

	WNDCLASS wc;
	ZeroMemory(&wc,sizeof(WNDCLASS));
	wc.lpfnWndProc = (WNDPROC)&project;
	wc.lpszClassName = "nyx";
	wc.hbrBackground = CreateSolidBrush(RGB(200, 200, 200));
	wc.hIcon = LoadIcon(NULL, IDI_APPLICATION);
	wc.hCursor = LoadCursor(NULL, IDC_ARROW);
	RegisterClass(&wc);

	HWND hwnd = CreateWindow("nyx", "NYX", style, 10, 10, 1500, 800, NULL, NULL, NULL, NULL);




	MSG msg;
	while (GetMessage(&msg, NULL, NULL, NULL))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}

//	return 0;
}