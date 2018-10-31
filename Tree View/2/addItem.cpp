




HTREEITEM AddItemToTree(HWND hwndTV, LPSTR lpszItem, int nLevel) 

{ 



	static int i=0;
    TV_ITEM tvi; 
    TV_INSERTSTRUCT tvins; 
    static HTREEITEM hPrev = (HTREEITEM) TVI_FIRST; 
    static HTREEITEM hPrevRootItem = NULL; 
    static HTREEITEM hPrevLev2Item = NULL; 
	static HTREEITEM hPrevLev3Item = NULL; 
	static HTREEITEM hPrevLev4Item = NULL;
	static HTREEITEM hPrevLev5Item = NULL;
	static HTREEITEM hPrevLev6Item = NULL;

 
  //  tvi.mask = TVIF_TEXT | TVIF_IMAGE| TVIF_SELECTEDIMAGE|TVIS_STATEIMAGEMASK|TVIS_OVERLAYMASK|TVIS_USERMASK ; 
 

	tvins.item.mask=TVIF_TEXT | TVIF_IMAGE| TVIF_SELECTEDIMAGE;
	tvins.item.lParam=(LPARAM) nLevel;
	tvins.item.pszText=lpszItem;
	tvins.item.cchTextMax=lstrlen(lpszItem);
  //  tvins.item = tvi; 
    tvins.hInsertAfter = hPrev; 
	tvins.item.iImage=0;
	tvins.item.iSelectedImage=1;
	
    if (nLevel == 1) 
        tvins.hParent = TVI_ROOT; 
    else if (nLevel == 2) 
	{
        tvins.hParent = hPrevRootItem;
	}
     else if(nLevel==3)
        tvins.hParent = hPrevLev2Item; 
	 else if (nLevel == 4) 
			 tvins.hParent=hPrevLev3Item;
     else if (nLevel == 5) 
		   tvins.hParent = hPrevLev4Item; 
	 else if (nLevel == 6) 
			   tvins.hParent = hPrevLev5Item; 
    hPrev = (HTREEITEM) SendMessage(hwndTV, TVM_INSERTITEM,0,(LPARAM) (LPTV_INSERTSTRUCT) &tvins); 
	
 
    return hPrev; 
} 
 
 
HTREEITEM insertItem(const char* str, HTREEITEM parent, HTREEITEM insertAfter,
                     int imageIndex, int selectedImageIndex,HWND handle)
{

		HIMAGELIST imageList1 = ::ImageList_Create(20,20,ILC_COLORDDB | ILC_MASK,2,500);

//================= MXOLOD iconis Misamarti unda shecvalo da chveulebrivad mushaobs /////////
	
	
	
		HICON icon;

//icon	= (HICON)LoadImage(NULL,"‪C:\\Users\\vakho1\\Desktop\\2.ico", IMAGE_ICON,800,800, LR_LOADFROMFILE);
	icon	= (HICON)LoadImage(NULL,"F:\\WINAPI1-\\icon\\Debug\\Itzikgur-My-Seven-Downloads-2.ico", IMAGE_ICON,800,800, LR_LOADFROMFILE);

	
	
	if(icon)
	{


	int u=ImageList_AddIcon(imageList1, icon);

	
}


	  	icon	= (HICON)LoadImage(NULL,"C:\\Users\\vakho1\\Desktop\\icons8_yard_work_filled_50_Y5I_icon.ico", IMAGE_ICON,800,800, LR_LOADFROMFILE);



			if(icon)
	{


	int u=ImageList_AddIcon(imageList1, icon);

	
}




	
		if(ImageList_GetImageCount(imageList1) == 2)
		{


	//MessageBox(hwnd,"Asdsa","Asdsad",0);
	SendMessage(handle, TVM_SETIMAGELIST,(WPARAM)0, (LPARAM)imageList1);



		}


	
	
	
	
	
	
	
	// build TVINSERTSTRUCT
    TVINSERTSTRUCT insertStruct;
    insertStruct.hParent = parent;
   // insertStruct.hInsertAfter = insertAfter;
    insertStruct.item.mask = TVIF_TEXT | TVIF_IMAGE | TVIF_SELECTEDIMAGE;
    insertStruct.item.pszText = (LPSTR)str;
    insertStruct.item.cchTextMax = sizeof(str)/sizeof(str[0]);
    insertStruct.item.iImage = imageIndex;
    insertStruct.item.iSelectedImage = selectedImageIndex;

    // insert the item
    return (HTREEITEM)::SendMessage(handle, TVM_INSERTITEM,0, (LPARAM)&insertStruct);
}