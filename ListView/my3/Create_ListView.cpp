

HWND CreateListView (HWND hwndParent) ;






HWND CreateListView (HWND hwndParent) 
{
    INITCOMMONCONTROLSEX icex;           // Structure for control initialization.
    icex.dwICC = ICC_LISTVIEW_CLASSES;
    InitCommonControlsEx(&icex);

    RECT rcClient;                       // The parent window's client area.

    GetClientRect (hwndParent, &rcClient); 
	HINSTANCE hInst;
    // Create the list-view window in report view with label editing enabled.
  //  HWND hWndListView = CreateWindow( WC_LISTVIEW, "asas",WS_CHILD | LVS_REPORT | LVS_EDITLABELS,0, 0,1000,1000,(HMENU)200,0,NULL); 





	InitCommonControls();
HWND hwndList = CreateWindow(WC_LISTVIEW, "", 
         WS_VISIBLE|WS_BORDER|WS_CHILD | LVS_REPORT | LVS_EDITLABELS|LVS_ALIGNTOP|LVS_SHAREIMAGELISTS|LVS_AUTOARRANGE , 
		 10, 40,rcClient.right-20 ,rcClient.bottom-20, 
         hwndParent, (HMENU)369, 0, 0);


	ListView_SetExtendedListViewStyle(hwndList, LVS_EX_FULLROWSELECT|LVS_EX_TWOCLICKACTIVATE|LVS_EX_SUBITEMIMAGES|LVS_EX_GRIDLINES|LVS_EX_CHECKBOXES);

	ListView_SetItemState (hwndList, 0,0, LVIS_OVERLAYMASK);

    return (hwndList);
}
