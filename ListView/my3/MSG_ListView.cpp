




if (message == WM_NOTIFY)
   {
		if ((((LPNMHDR)lparam)->hwndFrom) == GetDlgItem(hwnd, 1))
		{
			switch (((LPNMHDR)lparam)->code)
			{
				MessageBox(hwnd,"asdad","Asdasd",0);
			case LVN_GETEMPTYMARKUP:
			{
				NMLVEMPTYMARKUP *em = (NMLVEMPTYMARKUP *)lparam;
				em->dwFlags = EMF_CENTERED;
				//	wcscpy_s(em->szMarkup, 256, L"No Logs");
				return TRUE;
			}
			break;

			case LVN_GETDISPINFO:
			{
				NMLVDISPINFO* pDispInfo = (NMLVDISPINFO*)lparam;
				if (pDispInfo->item.mask & LVIF_TEXT)
				{
					
					switch (pDispInfo->item.iSubItem)
					{
				//	case 0: text = "text1"; break;
				//	case 1: text = "text2"; break;

					default: break;
					}
					//lstrcpyn(pDispInfo->item.pszText, text, pDispInfo->item.cchTextMax);
					//return TRUE;
				}
			}

		}
	}
}





	   if (message == WM_NOTIFY)
    {












		//////+++++++++++++ END GET ITEM ========================================///////////

				switch(LOWORD(wparam))
				{
				case 369:


				LPNMHDR lpnmh = (LPNMHDR)lparam;
			//	if (lpnmh->idFrom == 369)


				if (lpnmh->code == LVN_COLUMNCLICK)
				{


				//	MessageBox(hwnd,"LVN_COLUMNCLICK","LVN_COLUMNCLICK",0);
					NMLISTVIEW*    pListView = (NMLISTVIEW*)lparam;
     
				}

				if (lpnmh->code ==LVN_ITEMACTIVATE   )
				{
						int f=SendMessage(GetDlgItem(hwnd,369),LVM_GETNEXTITEM,-1,LVNI_FOCUSED);

						char k[20];
						sprintf(k,"%i",f);

						 LVITEM klo;
						 
						int oi=	SendMessage(GetDlgItem(hwnd,369),LVM_GETITEM,0,(LPARAM)0);

						MessageBox(hwnd,k,"LVN_ITEMACTIVATE",0);


				}
				if (lpnmh->code ==LVN_ODCACHEHINT)
				{

					MessageBox(hwnd,"LVN_ODCACHEHINT","LVN_ODCACHEHINT",0);

				}

				if (lpnmh->code ==LVN_ITEMCHANGED)
				{


						BOOL checked = IsDlgButtonChecked(GetDlgItem(hwnd,369), 0);
						if (checked) {
						 MessageBox(hwnd,"CHEKBOX","CHEKBOX",0);
						} 

					
				}
				if(((LPNMHDR)lparam)->code == NM_CLICK) 
					{
						int iSlected;
						iSlected=SendMessage(hList,LVM_GETNEXTITEM,-1,LVNI_SELECTED);
						    if(iSlected==NULL)
								{

							//	MessageBox(hwnd,"NM_CLICK","NM_CLICK",0);
					
							
							}
							
					}


			if(((LPNMHDR)lparam)->code == NM_RCLICK) 	
					{
						MessageBox(hwnd,"NM_RCLICK","NM_RCLICK",0);
					}



//////////////////////////// list vius editis mesigebi /////////////////////////////////////////////////////////////////////
			
			

				
			if(((LPNMHDR)lparam)->code == LVM_EDITLABEL)
			{
						MessageBox(hwnd,"LVM_EDITLABEL","LVM_EDITLABEL",0); /// editshi textis shecvlis dros 
		//	hEdit=ListView_GetEditControl(hList);
			}



			if(((LPNMHDR)lparam)->code == LVN_BEGINLABELEDIT)
			{
						MessageBox(hwnd,"LVN_BEGINLABELEDIT","LVN_BEGINLABELEDIT",0); /// editshi textis shecvlis dros 
		//	hEdit=ListView_GetEditControl(hList);
			}
			
			
			if(((LPNMHDR)lparam)->code == LVN_ENDLABELEDIT)
			{
				MessageBox(hwnd,"LVN_ENDLABELEDIT","LVN_ENDLABELEDIT",0); /// editshi textis shecvlis dros 
			}	

			if(((LPNMHDR)lparam)->code == LVN_SETDISPINFO)
			{
						MessageBox(hwnd,"LVN_SETDISPINFO","LVN_SETDISPINFO",0); /// editshi textis shecvlis dros 
		//	hEdit=ListView_GetEditControl(hList);
			}			
			
		
			
///////////////////////////////////////////////////////////////////
			
			
			
			
			
			
			
			
			if(((LPNMHDR)lparam)->code == NM_CUSTOMDRAW)
			{
			///				MessageBox(hwnd,"NM_CUSTOMDRAW","NM_CUSTOMDRAW",0); 
			}	


				break;


			}


	   }

