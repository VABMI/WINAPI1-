if(message==findmsgReplace)
{


	
	LPFINDREPLACE lpfr =(LPFINDREPLACE) lparam;

		
		static	char bufs2[100];
				char bufs[100];


	static char  bufg[10000]; //////////////// enacvlebian ertmanets mashi rodesac sxvadasxva sadziebo sityva shedis fanjris gautishvelad 
	static char bufg2[10000]; // anu tu chavwer raime sadziebo sityvas da mere sxva sadziebo sityvas 
    UINT ff;
	  

	
		

	if(lpfr->Flags&FR_FINDNEXT)
	{


		
	
		static bool bml1=1;
		static bool bml=1;
		static char edit[20000];
		static char edit1[20000];
		static char preStrWhat[10];
		charReplAl=szFindWhat2; ////
		if(charReplAl!=charReplAl)
		{


			charReplAl=charReplAl;
				bml=1;
		}


		if(bml)
		{

			GetWindowText(GetDlgItem(hwnd,editc),edit,9999);
			sprintf(edit1,"%s",edit);
				bml=0;
		}
	



			const char *position2=strstr(edit,szFindWhat2);
		
		
			unsigned int	f1f=(position2-edit);



			SendMessage(GetDlgItem(hwnd,editc),EM_SETSEL,f1f,f1f+strlen(szFindWhat2));
		if(position2){
					for(int i=f1f;i<=f1f+strlen(szFindWhat2);i++)
					{


						edit[i]=(char)-52;

					}
		
		}
		else
		{

			sprintf(edit,"%s",edit1);


		}
			
		
	}


	
	if(lpfr->Flags&FR_REPLACE)
	{
			char edit[20000];
				GetWindowText(GetDlgItem(hwnd,editc),edit,9999);
			char *FindeWhat=lpfr->lpstrFindWhat;
			char*edstr=str_replace1(edit, szFindWhat2, ReplaceWith);

			//	char *ReplaceWhat=lpfr->lpstrReplaceWith;


			SendMessage(GetDlgItem(hwnd,editc),WM_SETTEXT,strlen(edstr),(LPARAM)edstr);

					//	MessageBox(hwnd,FindeWhat,FindeWhat,0);


	}


	
	if(lpfr->Flags&FR_REPLACEALL)
	{
		char edit[20000];
		//SendMessage(GetDlgItem(hwnd,editc),WM_GETTEXT,0,(LPARAM)edit);

		GetWindowText(GetDlgItem(hwnd,editc),edit,9999);


	




	char*edstr=str_replace(edit,  szFindWhat2,ReplaceWith);



				SendMessage(GetDlgItem(hwnd,editc),WM_SETTEXT,strlen(edstr),(LPARAM)edstr);




	}













}


