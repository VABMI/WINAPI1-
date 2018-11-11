
HFONT select_font(HWND hwnd)
{
HDC hdc;                  // display device context of owner window
CHOOSEFONT cf;            // common dialog box structure
static LOGFONT lf;        // logical font structure
static DWORD rgbCurrent;  // current text color
HFONT hfont, hfontPrev;
DWORD rgbPrev;

// Initialize CHOOSEFONT
ZeroMemory(&cf, sizeof(cf));
cf.lStructSize = sizeof (cf);
cf.hwndOwner = hwnd;
cf.lpLogFont = &lf;
cf.rgbColors = rgbCurrent;
cf.Flags = CF_SCREENFONTS | CF_EFFECTS;

	if (ChooseFont(&cf)==TRUE)
	{
	hfont = CreateFontIndirect(cf.lpLogFont);
	//hfontPrev = SelectObject(hdc, hfont);
	rgbCurrent= cf.rgbColors;
	rgbPrev = SetTextColor(hdc, rgbCurrent);
	}
return hfont;
}





long __stdcall on_cmd(HWND hwnd,unsigned int message
					,unsigned int wparam,long lparam)

{

int ctrl_id = (unsigned short)wparam;
	if(ctrl_id==200)
	{

	 HDC hdc=GetDC(GetDesktopWindow());
    while(1)
	{
		SendMessage(GetDesktopWindow(),WM_PAINT,0,1);
		InvalidateRect(GetDesktopWindow(),0,1);
		Sleep(1000);
		    int gd = 0,gm;
    int angle = 0;
    double x, y;
 
  //  initgraph(&gd, &gm, "C:\\TC\\BGI");
 
// line(0, getmaxy() / 2, getmaxx(), getmaxy() / 2);
 /* generate a sine wave */
 for(x = 0; x < 1300; x+=1) {
 
     /* calculate y value given x */
     y = 50*sin(angle*3.141/180);
     y = 5/2 - y;
 
	 y+=500;
//	 x+=1000;
	for(int i=0;i<=1;i++)
		{
					
				for(int j=0;j<=0;j++)
				{
					SetPixel(hdc,x+j,y+i,RGB(8, 130, 211));
					SetPixel(hdc,x-j,y-i,RGB(8, 130, 211));
					SetPixel(hdc,x-j,y+i,RGB(8, 130, 211));
					SetPixel(hdc,x+j,y-i,RGB(8, 130, 211));
				}

		}
	Sleep(10);

 // delay(100);
  y-=500;
//	 x-=1000;
  /* increment angle */
  angle+=9;
 }
 







	}
	 //   ReleaseDC(GetDesktopWindow(),hdc);
	//SendMessage(hwnd,WM_SETFONT,(UINT)hfont,0);
	}

	else if(ctrl_id==10)
	{
	WinExec("calc.exe",1);
	HWND h=0;
	char str[500];
	h=GetDlgItem(hwnd,50);
	SendMessage(h,WM_GETTEXT,500,(long)str);
	strcat(str,"1");
	SendMessage(h,WM_SETTEXT,0,(long)str);
	
	h=GetDlgItem(hwnd,20);
	SendMessage(h,LB_DELETESTRING,0,0);

	h=GetDlgItem(hwnd,30);
	SendMessage(h,CB_DELETESTRING,0,0);

	}
return 0;
}