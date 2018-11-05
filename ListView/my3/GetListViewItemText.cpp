
char * GetListViewItemText( HWND a_hWnd, int a_Item, int a_SubItem) {
     char buffer[1025];
	
	 LVITEM lvi={0} ;
    lvi.mask = LVIF_TEXT;  // Only required when using LVM_GETITEM
    lvi.pszText = buffer;
    lvi.cchTextMax =1024;
    lvi.iItem = a_Item;    // Only required when using LVM_GETITEM
    lvi.iSubItem = a_SubItem;
	//for(int i=1;i<=5;i++){
	
	
		//lvi.iItem = i; 
    SendMessage( a_hWnd, LVM_GETITEM, 0, reinterpret_cast<LPARAM>( &lvi ) );
	
	//}
	//lvi.pszText;
	char *hh=(char*)malloc(strlen(buffer));
	strcpy(hh,buffer);
	
   return  hh;
}
