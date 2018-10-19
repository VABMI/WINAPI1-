





long __stdcall on_cmd(HWND hwnd,unsigned int message,unsigned int wparam,long lparam)

{  int ctrl_id = (unsigned short)wparam;

	void *ganuleba;
	

   int id = (unsigned short)wparam;

     	HWND hwn=GetDlgItem(hwnd,70000);
		EDITGLOBAL=GetDlgItem(hwnd,editc);
////======================================///
	
		int edtc=70001;
	    int ibhwn2=50001;
////////////////// DELETE EDIT //////////////
		int but=10,widthButton1=90;
		static int ButWithCreateFunc;
		int countdelete=10;
		int prev;bool b=false;
		









		//	char *buferEdit=(char*)malloc(1000 * sizeof(char));
		char buferEdit[99000];
		//	SendMessage(EDITGLOBAL,WM_GETTEXT,999,buferEdit);






	if(id==5002)
	{
		
		
		if(strlen(path)>0)
		{

			if(MessageBox(hwnd,"Save It?","save It?",1)==1)
			{

				GetWindowText(EDITGLOBAL,buferEdit,strlen(buferEdit));
				
				write(hwnd,buferEdit);
				open(hwnd);
				
				void *buferEdit1=read(hwnd);
				SendMessage(EDITGLOBAL,WM_SETTEXT,99,(LPARAM)buferEdit1);
			}
			else
			{
				open(hwnd);
				void *buferEdit1=read(hwnd);
				SendMessage(EDITGLOBAL,WM_SETTEXT,99,(LPARAM)buferEdit1);
			}

		
		}
		
		else if(open(hwnd))
		{
	
			
			void *buferEdit1=read(hwnd);
			SendMessage(EDITGLOBAL,WM_SETTEXT,99,(LPARAM)buferEdit1);
		}	
	






	}

	#include "BACK_NEXT.cpp"
 switch(id)
 
 {
	 ///////////////////// NEW /////////////////////


 case 5001:




	 	
		if(strlen(path)>0)
		{
			if(MessageBox(hwnd,"Save old file ?","save olde file?",1)==1)
				{

				GetWindowText(EDITGLOBAL,buferEdit,strlen(buferEdit));
				
				write(hwnd,buferEdit);
				
				

				SendMessage(EDITGLOBAL,WM_SETTEXT,1,(LPARAM)NULL);

				
				}
		}
		else
				SendMessage(EDITGLOBAL,WM_SETTEXT,1,(LPARAM)NULL);







		break;


	 ///////////////////////////////////////////////


	case 5003:
			GetWindowText(EDITGLOBAL,buferEdit,sizeof(buferEdit));



		if(strlen(path)==0)
		{

			 if(save(hwnd))
			 {


			 write(hwnd,buferEdit);
			 }
		}
		else 
		{
	    write(hwnd,buferEdit);
		}
	 break;


/////////// gamosvla ///////
	case 5005:
		
		static char bufs[6];
			for(int i=0;i<6;i++)
			{
			
			bufs[i]=szFindWhat[i];
			
			}
			MessageBox(hwnd,"dfgdg",bufs,0);
	// exit(1);
	 break;
 /////////////////////////////////////////
	case 5006:
		hfont_glob=select_font(hwnd);
		SendMessage(EDITGLOBAL,WM_SETFONT,(UINT)hfont_glob,1);
		InvalidateRect(hwnd,0,1);
	break;
	case 5007:

		edittextferi=choose_color(hwnd);
		InvalidateRect(hwn,0,1);

	  break;
	case 5008:

	bckferi=choose_color(hwnd);
	InvalidateRect(hwn,0,1);

	  break;
	case 5009:
	textlineferi=choose_color(hwnd);
	InvalidateRect(hwn,0,1);
	  break;
	//////////////// TExt align //////
	case 5010:


		//editstyle=WS_VISIBLE|WS_CHILD|WS_BORDER|ES_MULTILINE|ES_CENTER;

		//SendMessage(hwn, BM_SETSTYLE, (WPARAM)(ES_CENTER), (LPARAM)TRUE);
	
		///EM_SETSEL

//SetWindowLong(hwn,GWL_ID,0);
//SetWindowLong(hwn,GWL_HINSTANCE,0);
 //SetWindowLong(hwn,GWL_EXSTYLE,0); 
 ///SetWindowLong(hwn,GWL_HWNDPARENT,0); 
		SetWindowLong(EDITGLOBAL,GWL_STYLE,WS_VISIBLE|WS_CHILD|WS_BORDER|ES_MULTILINE|ES_CENTER|WS_VSCROLL);	
		
	//	SendMessage(hwn,EM_SETSEL,1,2);
		InvalidateRect(hwn,0,1);

		    //////  GetWindowLong();

		break;
	case 5011:
				SetWindowLong(EDITGLOBAL,GWL_STYLE,WS_VISIBLE|WS_CHILD|WS_BORDER|ES_MULTILINE|ES_RIGHT|WS_VSCROLL);	

						InvalidateRect(EDITGLOBAL,0,1);
		break;
	case 5012:
	
				SetWindowLong(EDITGLOBAL,GWL_STYLE,WS_VISIBLE|WS_CHILD|WS_BORDER|ES_MULTILINE|ES_LEFT|WS_VSCROLL);	

				InvalidateRect(EDITGLOBAL,0,1);
		break;


   }










 



 LPBYTE l;

 switch(id)
 
	{


	



	case IDM_NEW :
		
		if(strlen(path)>0)
		{
			if(MessageBox(hwnd,"Save old file ?","save olde file?",1)==1)
				{

				GetWindowText(EDITGLOBAL,buferEdit,strlen(buferEdit));
				
				write(hwnd,buferEdit);
				
				

				SendMessage(EDITGLOBAL,WM_SETTEXT,1,(LPARAM)NULL);

				
				}
		}
		else
				SendMessage(EDITGLOBAL,WM_SETTEXT,1,(LPARAM)NULL);

		break;
	case IDM_OPEN :
		
		if(strlen(path)>0)
		{

			if(MessageBox(hwnd,"Save olde file?","save olde file?",1)==1)
			{
				if(open(hwnd))
				{

				GetWindowText(EDITGLOBAL,buferEdit,strlen(buferEdit));
				
				write(hwnd,buferEdit);
				
				
				void *buferEdit1=read(hwnd);
				SendMessage(EDITGLOBAL,WM_SETTEXT,99,(LPARAM)buferEdit1);
		
				  SendMessage(StatusBar, SB_SETTEXT, 1,(LPARAM)path);
				}

			}
			else
			{
				if( open(hwnd))
				{
				void *buferEdit1=read(hwnd);
				SendMessage(EDITGLOBAL,WM_SETTEXT,99,(LPARAM)buferEdit1);
				 SendMessage(StatusBar, SB_SETTEXT, 1,(LPARAM)path);
				}
			}

		
		}
		
		else
		{
	
			if( open(hwnd))
			{
			void *buferEdit1=read(hwnd);
			SendMessage(EDITGLOBAL,WM_SETTEXT,99,(LPARAM)buferEdit1);
			 SendMessage(StatusBar, SB_SETTEXT, 1,(LPARAM)path);
			}

		}	
		

		
		break;


	case IDM_SAVE :

	//	MessageBox(hwnd,buferEdit,buferEdit,0);
		SendMessage(StatusBar, SB_SETTEXT, 1,(LPARAM)"SAVING");
		
		GetWindowText(EDITGLOBAL,buferEdit,sizeof(buferEdit));



		if(strlen(path)==0)
		{

			 if(save(hwnd))
			 {


			 write(hwnd,buferEdit);
			  SendMessage(StatusBar, SB_SETTEXT, 1,(LPARAM)path);
			 }
		}
		else 
		{
	    write(hwnd,buferEdit);
		}
		SendMessage(StatusBar, SB_SETTEXT, 1,(LPARAM)"SAVED");
		break;
	case IDM_GO :
		break;
	case IDM_FINDE :
		 find_text(hwnd);


	
		break;
	case IDM_PRINT :
		 RawDataToPrinter(hwnd);
		break;
	case IDM_REPLACE :
		break;
	case IDM_UNDO :
		break;
	case IDM_PROPETIES   :
		break;
	case IDM_folder  :
		break;
		

	 }







	////////////////////////////


	



 if(ctrl_id==ID_MENU_COLOR)
	{
	ULONG color;
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


	 else if(ctrl_id==ID_MENU_PRIMES)
	 {
		 WinExec("calc.exe",1);





	 }
	
	else if(ctrl_id==ID_MENU_FINDTEXT)
	{
	//WinExec("calc.exe",1);
	find_text(hwnd);
	}
	 else if(ctrl_id==ID_MENU_REPLACETEXT)
	{
	//WinExec("calc.exe",1);
		replace_text( hwnd);
	}
	else if(ctrl_id==ID_MENU_WINAPI)
		WinExec("HELP\\winhlp32.exe progtech.hlp",1);
	else if(ctrl_id==ID_MENU_C_CPP)
		WinExec("HELP\\winhlp32.exe bcpp.hlp",1);
	else if(ctrl_id==ID_MENU_ALL)
		WinExec("HELP\\winhlp32.exe",1);
	
	else if(ctrl_id==ID_MENU_ABOUT)
		MessageBox(hwnd,"VAXO","VAXO",0);
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



return 0;
}