							

if(wparam==LOGOUT){
for(int i=1231;i<=12312;i++)
									{

										ShowWindow(GetDlgItem(hwnd,i),SW_HIDE);

									}



		for(int i=1;i<=5;i++)
		{
			HWND log=GetDlgItem(hwnd,i);
			ShowWindow(log,SW_SHOW);

		}


			wmsize=0;
			DestroyWindow(GetDlgItem(hwnd,6));

}