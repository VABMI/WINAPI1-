
// InitListViewColumns: Adds columns to a list-view control.
// hWndListView:        Handle to the list-view control. 
// Returns TRUE if successful, and FALSE otherwise. 
BOOL InitListViewColumns(HWND hWndListView) 
{ 

	   LVCOLUMN lvc; /////// svetebis struqtura ..............


		HIMAGELIST imageList1 = ::ImageList_Create(40,40,ILC_COLORDDB | ILC_MASK,2,500); ///// imig listis hendeli 
		HICON icon;
		/////////////////// iconisssss
	icon	= (HICON)LoadImage(NULL,"C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\Itzikgur-My-Seven-Travel-BMV.ico", IMAGE_ICON,800,800, LR_LOADFROMFILE);
		
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
	char szText[]="asas";     // Temporary buffer.
 
    int iCol=0;

    // Initialize the LVCOLUMN structure.
    // The mask specifies that the format, width, text,
    // and subitem members of the structure are valid.
    lvc.mask = LVCF_FMT | LVCF_WIDTH | LVCF_TEXT | LVCF_SUBITEM|LVCF_IMAGE  ;
	
    // Add the columns.
    
 for(int iCol=0;iCol<=6;iCol++)
 {
	
	
		lvc.iImage=0;
		lvc.iSubItem = iCol;
		//lvc.iImage=0;
        lvc.pszText = (LPSTR)szText;
        lvc.cx = 100;               // Width of column in pixels.
		lvc.cxIdeal=300;
        if ( iCol < 2 )
            lvc.fmt = LVCFMT_LEFT;  // Left-aligned column.
        else
            lvc.fmt = LVCFMT_RIGHT; // Right-aligned column.

        // Load the names of the column headings from the string resources.

        // Insert the columns into the list view.
        ListView_InsertColumn(hWndListView, iCol, &lvc);
		
			char lll[100];



        
}

 ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    LVGROUP group = { 0 };
    group.cbSize = sizeof(LVGROUP);
    group.mask = LVGF_GROUPID|LVGF_HEADER;
    group.iGroupId = 11;//shown
	group.pszHeader=L"wewewe";
    ListView_InsertGroup(hWndListView, -1, &group);
    

	SendMessage(hWndListView, LVM_INSERTGROUP,-1,(LPARAM)&group);
 /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
 char g[100]="123151651";

	LVITEM item1;
   item1.mask = LVIF_TEXT|LVIF_IMAGE|LVIF_GROUPID;
   item1.cchTextMax = 256;
   item1.iImage=0;
  item1.iSubItem = 0;
   item1.pszText = g;
   item1.iItem = 0;
 //  ListView_InsertItem(hWndListView, &item1);



		item1.iGroupId=11;

	SendMessage(hWndListView,LVM_INSERTITEM,0,(LPARAM)&item1); // Send info to the Listview


	for(int i=1;i<=5;i++) // Add SubItems in a loop
{
   item1.iSubItem=i;
   sprintf(g,"SubItem %d",i);
   item1.pszText=g;
   SendMessage(hWndListView,LVM_SETITEM,0,(LPARAM)&item1); // Enter text to SubItems
}

	
	item1.iItem=1;           // choose item  
item1.iSubItem=0;        // Put in first coluom
item1.pszText="Item 1";  // Text to display 

SendMessage(hWndListView,LVM_INSERTITEM,0,(LPARAM)&item1); // Send to the Listview

for(int i=1;i<=5;i++) // Add SubItems in a loop
{
  item1.iSubItem=i;
  sprintf(g,"SubItem %d",i);
  item1.pszText=g;
  SendMessage(hWndListView,LVM_SETITEM,0,(LPARAM)&item1); // Enter text to SubItems
}


for(int d=1;d<=10;d++){




		
	item1.iItem=d;           // choose item  
item1.iSubItem=0;        // Put in first coluom
item1.pszText="Item 1";  // Text to display 

SendMessage(hWndListView,LVM_INSERTITEM,0,(LPARAM)&item1); // Send to the Listview

for(int i=1;i<=5;i++) // Add SubItems in a loop
{
  item1.iSubItem=i;
  sprintf(g,"SubItem %d",i);
  item1.pszText=g;
  SendMessage(hWndListView,LVM_SETITEM,0,(LPARAM)&item1); // Enter text to SubItems
}


}





//SendMessage(hWndListView,LVM_DELETEITEM,1,0); // delete the item 


///////////////////////////// iconebis chasmaaa ////////////////////////////



	item1.iItem=6;           // choose item  
item1.iSubItem=0;        // Put in first coluom
item1.pszText="Item 1";  // Text to display 

SendMessage(hWndListView,LVM_INSERTITEM,0,(LPARAM)&item1); // Send to the Listview
/////////////////////////////////////////////////////////////////////////////////////////




  

//////////////////////////////////////////////////////////////////////////
return TRUE;
} 