
// InitListViewColumns: Adds columns to a list-view control.
// hWndListView:        Handle to the list-view control. 
// Returns TRUE if successful, and FALSE otherwise. 
BOOL InitListViewColumns(HWND hWndListView) 
{ 

	   LVCOLUMN lvc; /////// svetebis struqtura ..............


		HIMAGELIST imageList1 = ::ImageList_Create(20,20,ILC_COLORDDB | ILC_MASK,2,500); ///// imig listis hendeli 
		HICON icon;
		/////////////////// iconisssss
	icon	= (HICON)LoadImage(NULL,"F:\\WINAPI1-\\icon\\Debug\\Itzikgur-My-Seven-Downloads-2.ico", IMAGE_ICON,800,800, LR_LOADFROMFILE);
		
if(icon)
	{
	ImageList_AddIcon(imageList1, icon); ///// image listshi iconis chasma

	}

	
if(ImageList_GetImageCount(imageList1) == 1)
	{				
					///--- image listis gagzavna list views tvis /////////////////////
			SendMessage(hWndListView, LVM_SETIMAGELIST,(WPARAM)LVSIL_NORMAL, (LPARAM)imageList1);	
	

	}


/////////////////////////////////////////////// ar mushaobs ListView_SetImageList ?????????? ======//



if(ImageList_GetImageCount(imageList1) == 1)
	{
		if(ListView_SetImageList(hWndListView, imageList1,LVSIL_NORMAL))
		{

			SendMessage(hWndListView, LVM_SETIMAGELIST,(WPARAM)LVSIL_SMALL, (LPARAM)imageList1);


		}
		
	}
	
	
	
	
	
	
//////////====================== END SET IMAGE LIST ===============================//////////////////////////////////
char *szText[100]={"ID_NUMBER","NAME","LASTNAME","EMAIL","PASSWORD"};     // Temporary buffer.
 
    int iCol=4;
	

    // Initialize the LVCOLUMN structure.
    // The mask specifies that the format, width, text,
    // and subitem members of the structure are valid.
    lvc.mask = LVCF_FMT | LVCF_WIDTH | LVCF_TEXT | LVCF_SUBITEM|LVCF_IMAGE  ;
	
    // Add the columns.
    

	lvc.cx = 130;               // Width of column in pixels.
		lvc.cxIdeal=300;
        if ( iCol < 2 )
            lvc.fmt = LVCFMT_LEFT;  // Left-aligned column.
        else
            lvc.fmt = LVCFMT_RIGHT; // Right-aligned column.

		lvc.iImage=0;
 ////////////////////////=========================================/////////////////////////// 
	
		for(int i=0;i<=iCol;i++){
		lvc.iSubItem = i;
		
        lvc.pszText = (LPSTR)szText[i];
        ListView_InsertColumn(hWndListView, i, &lvc);

		}



		/////////////////////////// userebis chawera /////////////////


	FILE *file;
	file=fopen("C:\\Users\\vakho1\\Desktop\\text.txt","r");
	


		struct person{
		char  ID_NUMBER[20];
		char NAME[20];
		char LASTNAME[20];
		char EMAIL[20];
		char PASSWORD[20];


	};
	
		struct person per;	 

	LVITEM item1;
   item1.mask = LVIF_TEXT|LVIF_IMAGE;
   item1.cchTextMax = 256;
   item1.iImage=0;
  

 





		while(fscanf(file, "%s %s %s %s %s",per.ID_NUMBER,per.NAME,per.LASTNAME,per.EMAIL,per.PASSWORD)!=EOF)
		{
			
		static int x=0;
		static int y=0;
		item1.iSubItem = x;	   
		item1.iItem = y;
			 y++;
			 
			 
			 item1.pszText =per.ID_NUMBER ;
		   	SendMessage(hWndListView,LVM_INSERTITEM,0,(LPARAM)&item1); 
		 

			x++;
			
   item1.iSubItem=x;
   item1.pszText=per.NAME;
   SendMessage(hWndListView,LVM_SETITEM,x,(LPARAM)&item1); // Enter text to SubItems
   x++;
   item1.iSubItem=x;
   item1.pszText=per.LASTNAME;
   SendMessage(hWndListView,LVM_SETITEM,x,(LPARAM)&item1); 
   x++;
   item1.iSubItem=x;
   item1.pszText=per.EMAIL;
   SendMessage(hWndListView,LVM_SETITEM,x,(LPARAM)&item1);
   x++;
   item1.iSubItem=x;
   item1.pszText=per.PASSWORD;
   SendMessage(hWndListView,LVM_SETITEM,x,(LPARAM)&item1);
   x=0;
	}





// Send info to the Listview




	
	
//////////////////////////////////////////////////////////////////////////
return TRUE;
} 