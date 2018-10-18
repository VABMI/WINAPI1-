long _stdcall notify_msg(HWND hwnd, unsigned int message, unsigned int wparam, long lparam)
{

	switch(LOWORD (wparam))
	{

			case TB_THUMBPOSITION:
	MessageBox(hwnd,"main fanjara","main fanjara",0);
	}

	/*

	switch (LOWORD(wparam)) {
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

	*/







	return 0;
}