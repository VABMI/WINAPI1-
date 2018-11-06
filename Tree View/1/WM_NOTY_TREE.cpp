	switch(wparam)
			{


			case 123:
				{
				
					switch(((LPNMHDR)lparam)->code)
					{
					case NM_RCLICK:
						MessageBox(hwnd,"RRRR clicked button","RRR clicked button",0);
						break;
					case NM_HOVER:
						
						MessageBox(hwnd,"HOVER","HOVER",0);
						break;

					case NM_SETFOCUS :

							 MessageBox(hwnd,"SETFOCUS","SETFOCUS",0);
						break;
					case TVN_ITEMEXPANDING :

						MessageBox(hwnd,"asdsad","asdas",0);
						
						break;



						case TVN_KEYDOWN :

					//		MessageBox(hwnd,"TreeView ","TreeView ",0);
							
							break;

							
					case TVN_GETDISPINFO:
						{
							
							/*
						
						TV_DISPINFO FAR *ptvdi =(TV_DISPINFO FAR *)lparam;
						TV_DISPINFO *pTVDispInfo(TV_DISPINFO*)lparam;
						pTVDispInfo->item->pszText;
						MessageBox(0,pTVDispInfo->item->pszText,0,0);
						*/
						///	MessageBox(hwnd,"asdsad","asdas",0);
						}
						
						break;
						case TVN_DELETEITEM:
					
							 MessageBox(hwnd,"TVN_DELETEITEM","TVN_DELETEITEM",0);
						
						break;
						case TVN_ITEMCHANGING:
							 MessageBox(hwnd,"TVN_ITEMCHANGING","TVN_ITEMCHANGING",0);
							break;

						case TVN_SELCHANGED:
						{

							char str[455];
							NMTREEVIEW* pnmtv =(LPNMTREEVIEW)lparam;
							TVITEM item;
							item.hItem=pnmtv->itemNew.hItem;
							item.mask=TVIF_TEXT;
							item.pszText=str;
							item.cchTextMax=455;
							SendMessage(GetDlgItem(hwnd,123),TVM_GETITEM,0,(LPARAM)&item);

							GetClientRect(hwnd,&r);

						//		MessageBox(0,str,0,0);
				  
							 int vco=0;
							for(int i=0;i<=strlen(str);i++)

							{
								
								if(str[i]==(char)92)
								{   
									path[vco]=(char)92;
									vco++;
									
								}

								path[vco]=str[i];
								vco++;
							}

						  
							bvb= (HBITMAP)LoadImage(NULL,path, IMAGE_BITMAP,r.right-200,r.bottom-1, LR_LOADFROMFILE);
								
							

							SendMessage(hstatic,STM_SETIMAGE,IMAGE_BITMAP,(LPARAM)bvb);
						}

						break;

					}
				
				
				}
				break;

			}

					

	//		if(wparam==10)
				switch(LOWORD(wparam))
				{
					
				//5	MessageBox(hwnd,"No Items in TreeView","Error",MB_OK|MB_ICONINFORMATION);


				}
			if(((LPNMHDR)lparam)->code == NM_CLICK) 
			{

			

		//	MessageBox(hwnd,"No Items in TreeView","Error",MB_OK|MB_ICONINFORMATION);

			Selected=(HTREEITEM)SendDlgItemMessage (hwndtree,1,TVM_GETNEXTITEM,TVGN_DROPHILITE,0);
				if(Selected==NULL)
				{
				//		MessageBox(hwnd,"No Items in TreeView","Error",MB_OK|MB_ICONINFORMATION);
					break;
				}	



			}