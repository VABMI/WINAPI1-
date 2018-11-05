
void open_file(HWND );




long __stdcall on_cmd(HWND hwnd,unsigned int message
					,unsigned int wparam,long lparam)

{

int ctrl_id = (unsigned short)wparam;
	if(ctrl_id==200)
	{
	//SendMessage(hwnd,WM_SETFONT,(UINT)hfont,0);
	}

	else if(ctrl_id==10)
	{
	//WinExec("calc.exe",1);
	//PlaySound(TEXT("temo.wav"), NULL, SND_ASYNC|SND_FILENAME|SND_LOOP|SND_NOWAIT);
//																								waushalei
						open_file(hwnd);
	//PlaySound(TEXT("temo.wav"),NULL,SND_SYNC);
	//PlaySound(TEXT("temo.wav"), NULL, SND_FILENAME);


/*
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
	*/

	}
return 0;
}