
HWND CreateATreeView(HWND hwndParent)
{ 
    RECT rcClient;  // dimensions of client area 
    HWND hwndTV;    // handle to tree-view control 
    InitCommonControls(); 
  
	///// | 0x0008|0x0010

	hwndTV = CreateWindow( WC_TREEVIEW,TEXT("Tree View"),TVIF_IMAGE|WS_VISIBLE |WS_CHILD|WS_BORDER|TVS_HASLINES|TVS_EDITLABELS |TVS_LINESATROOT,1,0, 200, 700, hwndParent, (HMENU)123, NULL, NULL);


	  
    return hwndTV;
} 