
/*
UINT menu_bar(HWND hwnd)
{
	HMENU hmenu = CreateMenu();
	if (!hmenu)
		return GetLastError();
	HMENU menu_file = CreatePopupMenu();
	AppendMenu(hmenu, MF_POPUP, (UINT_PTR)menu_file, "&file");
	AppendMenu(menu_file, MF_STRING, HMB_COLOR, "&color");
	AppendMenu(menu_file, MF_STRING, HMB_EXIT, "&exit");

	HMENU menu_project = CreatePopupMenu();
	AppendMenu(hmenu, MF_POPUP, (UINT_PTR)menu_project, "&My Project");
	AppendMenu(menu_project, MF_STRING, HMB_NPAD, "&Npad");
	AppendMenu(menu_project, MF_STRING, NULL, "&culator");
	AppendMenu(menu_project, MF_STRING, NULL, "&paint");

	HMENU menu_help = CreatePopupMenu();
	AppendMenu(hmenu, MF_POPUP, (UINT_PTR)menu_help, "&Help");
	AppendMenu(menu_help, MF_STRING, HMB_HELP, "&Help");
	AppendMenu(menu_help, MF_STRING, HMB_COLOR, "&culator");
	AppendMenu(menu_help, MF_STRING, HMB_EXIT, "&paint");


	SetMenu(hwnd, hmenu);
	return 0;
}
*/
long __stdcall on_create(HWND hwnd, unsigned int message, unsigned int wparam, long lparam)
{
	int style = WS_VISIBLE | WS_CHILD | WS_BORDER;
//	menu_bar(hwnd);
	CreateWindow("static", "", style, NULL, NULL, 1920, 30, hwnd, (HMENU)H_STATUSBAR, NULL,NULL);
	CreateWindow("button", "sinus", style, 200, 200, 200, 200, hwnd, (HMENU)H_SINUS, NULL, NULL);
	CreateWindow("edit", "0", style, 10, 100, 30, 30, hwnd, (HMENU)H_EDIT1, NULL, NULL);
	CreateWindow("edit", "0", style, 50, 100, 30, 30, hwnd, (HMENU)H_EDIT2, NULL, NULL);
	CreateWindow("edit", "0", style, 90, 100, 30, 30, hwnd, (HMENU)H_EDIT3, NULL, NULL);

	HWND H1 = CreateWindow("msctls_trackbar32", "f", WS_VISIBLE | WS_CHILD| TBS_ENABLESELRANGE, 500, 100, 200, 50, hwnd, (HMENU)251, NULL, NULL);

	HWND H2 = CreateWindow("msctls_trackbar32", "f", WS_VISIBLE | WS_CHILD|TBS_VERT, 500, 200, 50, 200, hwnd, (HMENU)252, NULL, NULL);
		SendMessage(H1,TBM_SETPOS,50,50);
		SendMessage(H1,TBM_SETSEL,100,100);
	
	return 0;
}