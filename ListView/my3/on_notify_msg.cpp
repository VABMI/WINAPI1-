

long __stdcall on_notify(HWND hwnd,unsigned int message
					,unsigned int wparam,long lparam)

{
	switch(wparam)
	{
		case 369:
		{
			switch(((LPNMHDR)lparam)->code)
			{
				case TVN_GETDISPINFO:
				{
				TV_DISPINFO FAR *ptvdi = (TV_DISPINFO FAR *) lparam;
				TV_DISPINFO* pTVDispInfo = (TV_DISPINFO*)lparam;
				pTVDispInfo->item.pszText;// = (char *)(pTVDispInfo->item.lParam)->Lfn;
				MessageBox(0,pTVDispInfo->item.pszText,0,0);
				}
				break;

				case TVN_SELCHANGED:
				{
				char str[256]="2234";
				NMTREEVIEW* pnmtv = (LPNMTREEVIEW)lparam;
				TVITEM item;
				item.hItem = pnmtv->itemNew.hItem;
				item.mask = TVIF_TEXT;
				item.pszText = str;
				item.cchTextMax =256;
				SendMessage(GetDlgItem(hwnd,1000),TVM_GETITEM,0,(LPARAM)&item);
				MessageBox(0,str,0,0);
				
				//LPNM_TREEVIEW ptv=new NM_TREEVIEW;
				////TV_ITEM tviNew = ((LPNM_TREEVIEW)lParam)->itemNew;
				
				//GetWindowText(( (LPNMHDR)lparam )->hwndFrom,str,256);
				//((LPNM_TREEVIEW)lparam)->itemNew.pszText;
				//MessageBox(0,((LPNM_TREEVIEW)lparam)->itemNew.pszText,str,0);
				}
				break;
	
				case NM_DBLCLK:
				{
				MessageBox(0,"TV","NM_DBLCLK",0);
				}
				break;
				
				case NM_RCLICK:    			break;
				case TVN_ENDLABELEDIT:    	break;
				case TVN_BEGINLABELEDIT:  	break;
				case TVN_BEGINDRAG:     	break;

				case TVN_KEYDOWN:
				{
					switch((((TV_KEYDOWN FAR *)lparam)->wVKey))
					{
						case VK_RETURN:
						MessageBox(0,"TV","VK_RETURN",0);
						break;

						case VK_DELETE:
						SendMessage(GetDlgItem(hwnd,369),WM_SETTEXT,0,(LPARAM)(LPCSTR)"delt");
						break;
					}
				}
				break;

				case TVN_ITEMEXPANDED:
				{
				MessageBox(0,"TV","TVN_ITEMEXPANDED",0);
				}
				break;

			}
		}
		break;


		case 1212://listview
		break;
	}
return 0;
}