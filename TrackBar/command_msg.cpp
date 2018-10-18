long __stdcall command_msg(HWND hwnd, unsigned int message, unsigned int wparam, long lparam)
{






	switch (LOWORD(wparam))
	{
	case HMB_COLOR:
						ULONG color;

						color = select_color(hwnd);
						if (color != 0)
						{
							SetClassLong(hwnd, GCL_HBRBACKGROUND, (LONG)CreateSolidBrush(RGB(25,225,96)));
							InvalidateRect(hwnd, 0, 1);
							SendMessage(hwnd, WM_PAINT, 0, 0);
							InvalidateRect(hwnd, 0, 0);
						}
						break;
	case HMB_HELP: WinExec("C:\\Users\\nikak\\OneDrive\\Desktop\\HELP\\winhlp32.exe",1); break;
	case HMB_EXIT: exit(1); break;
	case H_SINUS: 
			SetClassLong(hwnd, GCL_HBRBACKGROUND, (LONG)CreateSolidBrush(RGB(25,225,96)));
		float x,z,v,c;
		HDC hdc = GetDC(hwnd);
			for (x = 1; x < 1920; x += 0.5)
			{
			int y = sin(x * 0.09) * 400  + 500;
			SetPixel(hdc, x, y, RGB(0, 0, 200));
			}
		break;

	} 
	

	return 0;
}