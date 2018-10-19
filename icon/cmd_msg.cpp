


long __stdcall on_cmd(HWND hwnd,unsigned int message
					,unsigned int wparam,long lparam)

{

int ctrl_id = (unsigned short)wparam;
	if(ctrl_id==ID_MENU_FONT)
	{
	//hfont=select_font(hwnd);
	
	ZeroMemory(&cf_glob,sizeof(cf_glob));
	cf_glob.lStructSize = sizeof (cf_glob);
	cf_glob.hwndOwner = hwnd;
	cf_glob.lpLogFont = &lf_glob;
	cf_glob.rgbColors = rgb_glob;
	cf_glob.Flags = CF_SCREENFONTS | CF_EFFECTS;
		if(ChooseFont(&cf_glob))
		{
		hfont_glob = CreateFontIndirect(cf_glob.lpLogFont);
		rgb_glob=cf_glob.rgbColors;
		InvalidateRect(hwnd,0,1);
		SendMessage(hwnd,WM_PAINT,0,1);
		InvalidateRect(hwnd,0,0);
		}
	}
	else if(ctrl_id==ID_MENU_COLOR)
	{

		if(color=choose_color(hwnd))
		{
		SetClassLong(hwnd,GCL_HBRBACKGROUND,(LONG)CreateSolidBrush(color));  //// backgraundze feris dasma 
		
		
		//ULONG err=GetLastError();

		/*
		InvalidateRect(hwnd,0,1);
		SendMessage(hwnd,WM_PAINT,0,0);
		InvalidateRect(hwnd,0,0);
		*/
		HWND h=GetDlgItem(hwnd,30);
		SetClassLong(h,GCL_HBRBACKGROUND,(LONG)CreateSolidBrush(color));
		InvalidateRect(h,0,1);
		SendMessage(h,WM_PAINT,0,0);
		//InvalidateRect(h,0,0);
		
		}
	}
	else if(ctrl_id==ID_MENU_TOOLBAR)
	{
	CreateAToolBar(hwnd,1234,0);
	}

	else if(ctrl_id==ID_MENU_REGION_ELIPS)
	{
	RECT r;
	HDC hdc=GetDC(hwnd);
	GetClientRect(hwnd,&r);
	HRGN hrgn;// = CreateRectRgnIndirect(&r);
	//HBRUSH hbrush = CreateSolidBrush(RGB(200,200,200));
	//FillRgn(hdc,hrgn, hbrush);
	//HPEN hPen = CreatePen(PS_DOT,1,RGB(0,255,0));


	
	hrgn=CreateEllipticRgn(r.left,r.top,r.right,r.bottom);
	/*
	//SelectClipRgn(hdc,hrgn);

	int w=r.right-r.left;
	int h=r.bottom-r.left;
	FrameRgn(GetDC(hwnd),hrgn,CreateSolidBrush(RGB(200,0,0)),w,h);
	*/
	SetWindowRgn(hwnd,hrgn,1);
	}

	else if(ctrl_id==ID_MENU_LOADIMAGE)
	{
	char fname[1024]="abc.bmp";
	HANDLE hBitmap=(HBITMAP)LoadImage(GetModuleHandle(NULL),fname,IMAGE_BITMAP,0,0,LR_LOADFROMFILE);
	BITMAP bmp;
	HDC hdcMem;
	GetObject(hBitmap,sizeof(bmp),&bmp);
	HGDIOBJ hGdiObj = SelectObject(hdcMem,hBitmap);
	HRGN hRgn=CreateRectRgn(0,0,0,0);
	COLORREF transp_clr = RGB(255, 255, 255);

	int X = 0;
	int iRet = 0;
		for(int Y=0;Y<bmp.bmHeight;Y++)
		{
			do
			{
				while(X<bmp.bmWidth && GetPixel(hdcMem,X,Y)==transp_clr)
				X++;
			int iLeftX = X;
				
				while(X<bmp.bmWidth && GetPixel(hdcMem,X,Y)!=transp_clr)
				++X;

			HRGN hRgnTemp = CreateRectRgn(iLeftX, Y, X, Y+1);
			iRet = CombineRgn(hRgn, hRgn, hRgnTemp, RGN_OR);

				if(iRet == ERROR)
				return 0;
				
			DeleteObject(hRgnTemp);
			}
			while(X<bmp.bmWidth);
		X=0;
		}
	iRet=SetWindowRgn(hwnd,hRgn,TRUE);
	}

	else if(ctrl_id==ID_MENU_FINDTEXT)
	{
	WinExec("calc.exe",1);
	//find_text(hwnd);
	}
	else if(ctrl_id==ID_MENU_WINAPI)
		WinExec("HELP\\winhlp32.exe progtech.hlp",1);
	else if(ctrl_id==ID_MENU_C_CPP)
		WinExec("HELP\\winhlp32.exe bcpp.hlp",1);
	else if(ctrl_id==ID_MENU_ALL)
		WinExec("HELP\\winhlp32.exe",1);
	
	else if(ctrl_id==ID_MENU_ABOUT)
		MessageBox(hwnd,"Copyright Quantum LTD","Quantum Labs",0);
	else if(ctrl_id==10)
	{
	//WinExec("calc.exe",1);
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
	else if(ctrl_id==ID_MENU_PRIMES)
	{
	__int64 number=2342342364564564;
	char str[1000]="";
	HWND hedit;
		for(__int64 k=0;k<number;k++)
			if(isprime(k))
			{
			sprintf(str,"%ld",k);
			SendMessage(hedit,EM_REPLACESEL,0,(LPARAM)str);
			}
	}



return 0;
}