
					static char gg[3000];
								//	ZeroMemory(&gg,strlen(gg));
					int iPos = ListView_GetNextItem(GetDlgItem(hwnd,369), -1, LVNI_SELECTED);


					 for(int i=0;i<=4;i++)
						 { 
							 if(iPos!=-1){
							strcat(gg, GetListViewItemText(GetDlgItem(hwnd,369),iPos,i));
							strcat(gg,"_");
							 }
							
						 }
						 strcat(gg,"\r\n");
				while (iPos != -1) {
			
			   iPos = ListView_GetNextItem(GetDlgItem(hwnd,369), iPos, LVNI_SELECTED);


		
						
						 for(int i=0;i<=4;i++)
						 { 
							 if(iPos!=-1){
							strcat(gg, GetListViewItemText(GetDlgItem(hwnd,369),iPos,i));
							strcat(gg,"_");
							 }
							
						 }
						 strcat(gg,"\r\n");
				
						}
						 MessageBox(hwnd,gg,gg,0);