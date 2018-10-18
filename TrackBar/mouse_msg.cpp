void coordinate(HWND hwnd,long lparam)
{


	char str[256];
	int x = (unsigned short)lparam;
	int y = HIWORD(lparam);
	HWND hW = GetDlgItem(hwnd, H_STATUSBAR);
	HDC hdc = GetDC(hW);
	sprintf_s(str, "%ld : %ld", x, y);
	HDC h = GetDC(hW);
	SetBkMode(hdc, 1); //poni textis
	Rectangle(hdc, 10, 5, 90, 20);
	TextOut(hdc, 10, 5, str, strlen(str));
}


long __stdcall mouse_msg(HWND hwnd, unsigned int message, unsigned int wparam, long lparam)
{
	coordinate(hwnd, lparam);
	/*
	ULONG thrd_id = 0;
	struct THRD_DATA
	{
		HWND hwnd;
		long lparam;
	}thrd_data;
	thrd_data.hwnd = hwnd;
	thrd_data.lparam = lparam;

	CreateThread(NULL, NULL, (PTHREAD_START_ROUTINE)coordinate, (void *)&thrd_data, NULL, &thrd_id);
	*/
	return 0;
}