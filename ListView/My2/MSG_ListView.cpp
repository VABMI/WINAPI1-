
	   if (message == WM_NOTIFY)
    {


				switch(LOWORD(wparam))
				{
				case 369:


				LPNMHDR lpnmh = (LPNMHDR)lparam;
				if (lpnmh->idFrom == 369)
				if (lpnmh->code == LVN_COLUMNCLICK)
				{


					MessageBox(hwnd,"LVN_COLUMNCLICK","LVN_COLUMNCLICK",0);
					NMLISTVIEW*    pListView = (NMLISTVIEW*)lparam;
     
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

