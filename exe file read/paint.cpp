
long __stdcall on_paint(HWND hwnd,unsigned int message
					,unsigned int wparam,long lparam)

{
	
	char text[32];
	PAINTSTRUCT ps;
	HDC hdc=GetDC(hwnd);
	static int counter=0;
	counter++;
	sprintf(text,"Counter = %d",counter);

	
	SetBkMode(hdc,1);
	
	SetTextColor(hdc,RGB(240,23,4));
	TextOut(hdc,0,200,"1523126351237",12);

	SetTextColor(hdc,RGB(24,223,4));
	TextOut(hdc,0,220,"1523126351237",12);

	TextOut(GetDC(GetDesktopWindow()),100,10,text,strlen(text));
	//MessageBox(0,"WM_PAINT=0x000F",0,0);

	hdc = BeginPaint(hwnd, &ps);
        // TODO: Add any drawing code here...
	HDC hMemDC=CreateCompatibleDC(hdc); 
	::SelectObject(hMemDC,bitmap);           
	BitBlt(hdc,0,0,200,400,hMemDC,0,0,SRCCOPY); 
	::DeleteDC(hMemDC) ;    
	EndPaint(hwnd, &ps);

	
return 0;
}