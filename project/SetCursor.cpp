
	LPCWPSTRUCT cwps = (LPCWPSTRUCT)lparam;


	 HWND ff=GetDlgItem(hwnd,combo);




	 if(message==WM_ACTIVATE)
	 {  
	   
			if(wparam==111)
			{
			MessageBox(hwnd,"Asda","Asdas",0);
			}

	 }
	 if(message==WM_COMMAND)
	 {	 //MessageBox(hwnd,"Asda","Asdas",0);
		 if((wparam)== EN_CHANGE)
		 {
			 MessageBox(hwnd,"Asda","Asdas",0);

		 }
	
		// long elong = SendDlgItemMessage(hwnd,111,message,ewp,elp)
		 
		
	 }
	 
  if(message==WM_KEYDOWN)
  {

	  if(wparam==VK_TAB)
	  { static int cob=112; 	 MessageBox(hwnd,"Asda","Asdas",0);
		  HWND ff=GetDlgItem(hwnd,cob);
		  SetFocus(ff);
		  cob++;
		  if(cob==128)cob=111;
		 
	  }
//  char ssa[100];
		 
		  SendMessage(ff,WM_GETTEXT,80,(LPARAM)ssa);

	HWND	sdf= GetFocus();
		  if(sdf==ff){
			 MessageBox(hwnd,"Asda","Asdas",0);
		  }
 //SendMessage(sdf,WM_GETTEXT,80,(LPARAM)"sdsdsd");
		
  }