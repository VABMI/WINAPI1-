




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


					MessageBox(hwnd,"LVN_COLUMNCLICK","LVN_COLUMNCLICK",0);
					NMLISTVIEW*    pListView = (NMLISTVIEW*)lparam;
     
				}

				if (lpnmh->code ==LVN_ITEMACTIVATE   )
				{
						int f=SendMessage(GetDlgItem(hwnd,369),LVM_GETNEXTITEM,-1,LVNI_FOCUSED);

						char k[20];
						sprintf(k,"%i",f);

						 LVITEM klo;
						static char gg[1000];
						ZeroMemory(&gg,strlen(gg));
						 for(int i=0;i<=4;i++)
						 { 
							strcat(gg, GetListViewItemText(GetDlgItem(hwnd,369),f,i));
							strcat(gg,"_");
							
						 }
						 MessageBox(hwnd,gg,gg,0);
					//	 int oi=	SendMessage(GetDlgItem(hwnd,369),LVM_GETITEM,0,(LPARAM)0);

						


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


			if(((LPNMHDR)lparam)->code ==NM_RCLICK) 	///rCLICK
					{
											  // display a menu created using CreateMenu()
						HMENU hMenu = ::CreateMenu();
	


					int f=SendMessage(GetDlgItem(hwnd,369),LVM_GETNEXTITEM,-1,LVNI_FOCUSED);

					HMENU hmenu_popup_file=CreatePopupMenu();
					if (NULL != hMenu)
					{
						POINT point;
						// add a few test items
        
		
						::AppendMenu(hmenu_popup_file, MF_STRING, 1, "Item 1");
						::AppendMenu(hmenu_popup_file, MF_STRING, 2, "Item 2");
						::AppendMenu(hmenu_popup_file, MF_STRING, 3, "Item 3");

						ClientToScreen(GetDlgItem(hwnd,369),&point);
						point.x = LOWORD (lparam);
						point.y = HIWORD (lparam);
						int sel;

							if (GetCursorPos(&point)&&f!=-1)
							{
								//cursor position now in p.x and p.y

								 sel = ::TrackPopupMenuEx(hmenu_popup_file, TPM_CENTERALIGN | TPM_RETURNCMD,point.x,point.y,GetDlgItem(hwnd,369),NULL);
								 f=-1;
								 SendMessage(GetDlgItem(hwnd,369),LVNI_FOCUSED,-1,-1);
							}
						//	int sel = ::TrackPopupMenuEx(hmenu_popup_file, TPM_CENTERALIGN | TPM_RETURNCMD,point.x,point.y,GetDlgItem(hwnd,369),NULL);


					   ::DestroyMenu(hmenu_popup_file);


	
					}
									
									//	MessageBox(hwnd,"NM_RCLICK","NM_RCLICK",0);
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

