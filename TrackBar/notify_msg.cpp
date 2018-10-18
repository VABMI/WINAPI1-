long _stdcall notify_msg(HWND hwnd, unsigned int message, unsigned int wparam, long lparam)
{


	
	if(lparam == TRBN_THUMBPOSCHANGING)
	{
	MessageBox(hwnd,"WM_NOTIFY LPARAM","WM_NOTIFY",0);

	  int Pos1 = SendMessage(GetDlgItem(hwnd, 252), TBM_GETPOS, 0, 0);




	}



	switch (LOWORD(wparam)) {
	MessageBox(hwnd,"WM_NOTIFY WPARAM","WM_NOTIFY",0);
	case WM_KEYUP:

		MessageBox(hwnd,"KeyUp","KeyUp",0);

		break;

	case TB_THUMBTRACK:
		MessageBox(hwnd,"TB_THUMBTRACK","TB_THUMBTRACK",0);
		break;
	case TB_THUMBPOSITION:
		char p[50];
		int Po = SendMessage(GetDlgItem(hwnd, 251), TBM_GETPOS, 0, 0);
		Po *= 2;
		Po += 500;
		HDC hdc = GetDC(hwnd);
		for (int x = 500; x < Po; x++)
		{
			SetPixel(hdc, x, 160, RGB(0, 0, 200));
		}

		//sprintf_s(p, " %d ", Po);
		//MessageBox(NULL, p, NULL, NULL);

		break;

	}



	return 0;
}