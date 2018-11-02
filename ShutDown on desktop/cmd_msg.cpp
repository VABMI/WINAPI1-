

long __stdcall on_cmd(HWND hwnd,unsigned int message,unsigned int wparam,long lparam)
{

int ctrl_id = (unsigned short)wparam;

	 if(ctrl_id==1)
	{
		WinExec("shutdown -s -t 1",1);
	

	}
	 	 
	 if(ctrl_id==2)
	{
			WinExec("shutdown -r -t 1",1);
	

	}
return 0;
}