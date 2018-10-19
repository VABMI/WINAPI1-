	////////// back to login form /////
	if(wparam==55)
	{

		SetWindowLong(hwnd,GWL_STYLE,WS_VISIBLE|WS_OVERLAPPEDWINDOW|WS_CLIPCHILDREN);  



		for(int i=111;i<=128;i++)
		{
			  HWND reg=GetDlgItem(hwnd,i);

			   ShowWindow(reg,SW_HIDE);
		}

		HWND reg1=GetDlgItem(hwnd,55);
		   ShowWindow(reg1,SW_HIDE);


		for(int i=1;i<=5;i++)
		{
			HWND log=GetDlgItem(hwnd,i);
			ShowWindow(log,SW_SHOW);

		}


		SendMessage(hwnd,WM_SIZE,1,1);

		    	

	}

	////////// end back to login form /////
	//// registracion button ////////////////
	  if(wparam==5)
		{
	//		RECT r;
			GetWindowRect(hwnd,&r);
				SetWindowLong(hwnd,GWL_STYLE,WS_VISIBLE|WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU  |WS_CLIPCHILDREN);  
			   SetWindowPos(hwnd,0,160,100,590,500,SWP_ASYNCWINDOWPOS);
     
		for(int i=111;i<=128;i++){HWND reg=GetDlgItem(hwnd,i);ShowWindow(reg,SW_SHOW);}
		
			   HWND reg1=GetDlgItem(hwnd,55);
			   ShowWindow(reg1,SW_SHOW);
		   	   L:
			   ShowWindow(edit1,SW_HIDE);
			   ShowWindow(edit2,SW_HIDE);
			   ShowWindow(but1,SW_HIDE);
			   ShowWindow(but2,SW_HIDE);
			   ShowWindow(static1,SW_HIDE);
			


		}