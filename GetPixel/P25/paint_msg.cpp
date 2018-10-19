
long __stdcall on_paint(HWND hwnd,unsigned int message
					,unsigned int wparam,long lparam)

{
	{
	char text[32];
	HDC hdc=GetDC(hwnd);
	static int counter=0;
	counter++;
	sprintf(text,"Counter = %d",counter);

	SelectObject(hdc,hfont_glob);
	SetTextColor(hdc,rgb_glob);

	//SetBkColor(hdc,RGB(240,23,4));
	TextOut(hdc,0,200,"1523126351237",12);

	//SelectObject(hdc,hfont_global1);

	SetBkMode(hdc,1);
	//SetTextColor(hdc,RGB(24,223,4));
	TextOut(hdc,0,220,"1523126351237",12);

	TextOut(GetDC(GetDesktopWindow()),100,10,text,strlen(text));
	//MessageBox(0,"WM_PAINT=0x000F",0,0);
	}




	for(int y=0;y<=100;y++)
		{
			for(int i=60;i<=78;i++)
			{
			SetPixel(hdc,1+y,i,RGB(53,19,13));
			SetPixel(hdc,0+y,i,RGB(535,19,13));
			}
	
		}


return 0;
}