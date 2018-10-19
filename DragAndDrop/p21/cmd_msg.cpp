
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
	hfont_global=select_font(hwnd);
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