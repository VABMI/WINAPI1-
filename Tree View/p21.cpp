#include "head.h"
#include "notify_msg.cpp"
HFONT hfont_global;
HWND h;
HBITMAP bvb;
HCURSOR hCursor;
HWND hwnd=0;
			
	HWND tree;
	HINSTANCE hInst,g_hInst; // main function handler
#define WIN32_LEAN_AND_MEAN // this will assume smaller exe
TV_ITEM tvi;
HTREEITEM Selected;

TV_INSERTSTRUCT tvinsert;  // struct to config out tree control




HTREEITEM Parent;           // Tree item handle
HTREEITEM Before;           // .......
HTREEITEM Root;             // .......

HTREEITEM hitTarget;
static HTREEITEM hPrev=(HTREEITEM)TVI_FIRST;

HIMAGELIST hImageList;      // Image list array hadle
HIMAGELIST hImageList2;

HBITMAP hBitMap;            // bitmap handler
bool flagSelected=false;

HWND hTree;
TVHITTESTINFO tvht; 
POINTS Pos;
bool Dragging;

HWND hEdit;






	//=====================================================//
HWND CreateATreeView(HWND hwndParent);


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
	/*
hImageList=ImageList_Create(16,16,ILC_COLOR16,2,10);
	    HICON hIcon;
		 hBitMap = (HBITMAP)LoadImage(NULL, "C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\123.bmp", IMAGE_ICON, 32, 32, LR_LOADFROMFILE);
		 bvb= (HBITMAP)LoadImage(NULL,"C:\\Users\\vaxoa\\OneDrive\\Desktop\\LIST.BMP", IMAGE_BITMAP,10,10, LR_LOADFROMFILE);

		 		
		 ImageList_Add(hImageList,(HBITMAP)bvb,TVSIL_NORMAL);

		 		 SendDlgItemMessage(hwnd,123,TVM_SETIMAGELIST,0,(LPARAM)(HBITMAP)hImageList);

				     TreeView_SetImageList(hwndTV, hImageList, TVSIL_NORMAL); 

				  SendMessage(hwndTV, TVM_SETIMAGELIST, 0, (LPARAM) hImageList); 
	
   hBitMap=(HBITMAP)LoadImage(NULL, "C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\123.bmp", IMAGE_ICON, 32, 32, LR_LOADFROMFILE);
 
   
   
   ImageList_Add(hImageList,hBitMap,NULL);
   DeleteObject(hBitMap);

		 SendDlgItemMessage(hwnd,123,TVM_SETIMAGELIST,0,(LPARAM)(HBITMAP)hImageList);
 SendMessage(hwndTV, TVM_SETIMAGELIST, 0, 
         (LPARAM) hImageList); 

	
	 */





    HTREEITEM hti; 
 
    tvi.mask = TVIF_TEXT | TVIF_IMAGE| TVIF_SELECTEDIMAGE|TVIS_STATEIMAGEMASK|TVIS_OVERLAYMASK|TVIS_USERMASK ; 
 

    tvi.pszText = lpszItem; 
    tvi.cchTextMax = lstrlen(lpszItem); 
    tvi.lParam = (LPARAM) nLevel; 


    tvi.iImage =0;
    tvi.iSelectedImage =1;
	
	
    tvins.item = tvi; 
    tvins.hInsertAfter = hPrev; 
	tvins.item.mask=TVIF_TEXT | TVIF_IMAGE| TVIF_SELECTEDIMAGE;
	tvins.item.iImage=i;
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
	
    if (nLevel == 1) 
        hPrevRootItem = hPrev; 
    else if (nLevel == 2) 
	{
        hPrevLev2Item = hPrev;
	}
	  else if (nLevel == 3) 
        hPrevLev3Item = hPrev; 
	 else if (nLevel == 4) 
        hPrevLev4Item = hPrev;
	   else if (nLevel == 5) 
        hPrevLev5Item = hPrev;
	    else if (nLevel == 6) 
        hPrevLev6Item = hPrev;
		/*
    if (nLevel > 1)
	{ 
        hti = TreeView_GetParent(hwndTV, hPrev); 
        tvi.mask = TVIF_IMAGE | TVIF_SELECTEDIMAGE; 
        tvi.hItem = hti; 
        tvi.iImage =1; 
        tvi.iSelectedImage = 1; 
        TreeView_SetItem(hwndTV, &tvi); 
    } 
 */
    return hPrev; 
} 
 
 


void create(HWND hwnd,UINT msg,WPARAM wp,LPARAM lp)
{


	HWND tvHandle=CreateATreeView(hwnd);

	HIMAGELIST imageList = ::ImageList_Create(20,20,ILC_COLORDDB | ILC_MASK,2,500);

	HIMAGELIST imageList2= ::ImageList_Create(20,20,ILC_COLORDDB | ILC_MASK,2,100);




	HICON icon;

icon	= (HICON)LoadImage(NULL,"C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\Itzikgur-My-Seven-Downloads-2.ico", IMAGE_ICON,30,30, LR_LOADFROMFILE);
		
if(icon){


ImageList_AddIcon(imageList, icon);

}

icon	= (HICON)LoadImage(NULL,"C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\Itzikgur-My-Seven-Recycle-Bin-full.ico", IMAGE_ICON,30,30, LR_LOADFROMFILE);
if(icon){


ImageList_AddIcon(imageList, icon);
ImageList_AddIcon(imageList2, icon);

}



if(ImageList_GetImageCount(imageList) == 2)
{


	//MessageBox(hwnd,"Asdsa","Asdsad",0);
	SendMessage(tvHandle, TVM_SETIMAGELIST,(WPARAM)TVSIL_NORMAL, (LPARAM)imageList);



		}



if(ImageList_GetImageCount(imageList) == 2)
{
	//MessageBox(hwnd,"asdasd","AsdasD",0);

	
	//SendMessage(tvHandle, TVM_SETIMAGELIST,(WPARAM)1, (LPARAM)imageList);

}




// load icons and add them to ImageList

	//=====================================================//
	
	

	/*
	
	TVITEM tvitm;	
	tvitm.mask=TVIF_TEXT;
	tvitm.pszText="vaxo";
	tvitm.cchTextMax=strlen(tvitm.pszText);

	tvinsert.item=tvitm;
	tvinsert.hInsertAfter=TVI_ROOT;
	SendMessage(hwnd,TVM_INSERTITEM,(WPARAM)0,(LPARAM)&tvitm);

	
	tvitm.pszText="saqartvelo";
	tvitm.cchTextMax=lstrlen("saqartvelo");
	tvinsert.item=tvitm;
	SendMessage(hwnd,TVM_INSERTITEM,(WPARAM)0,(LPARAM)&tvinsert);




	tvitm.pszText="item3";
	tvitm.cchTextMax=lstrlen(tvitm.pszText);
	
	tvitm.hItem=TreeView_GetParent(hwnd,tvitm.hItem);



	*/


	//	CreateWindowEx(0, WC_TREEVIEW,TEXT("Tree View"), WS_VISIBLE | WS_CHILD,0, 0, 200, 500, hwnd, (HMENU)123, GetModuleHandle(NULL), NULL);
	//	if(InitTreeViewImageLists()

	/*
	
	tvi.mask=TVIF_TEXT|TVIF_IMAGE|TVIF_SELECTEDIMAGE|TVIF_PARAM;
	tvi.pszText="saqartvelo";
	tvi.cchTextMax=lstrlen("saqartvelo");
	tvi.iImage=1;
	tvi.iSelectedImage=1;
	tvi.lParam=(LPARAM)1;
	tvinsert.item=tvi;
	tvinsert.hInsertAfter=hPrev;
	
		TreeView_CreateDragImage(hwnd,hitTarget);	

		hImageList=ImageList_Create(16,16,ILC_COLOR16,2,10);
		
		hBitMap=(HBITMAP)LoadBitmap(NULL, MAKEINTRESOURCE("C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\icon\\BMP.png"));
		
		///// C:\Users\vaxoa\OneDrive\Desktop\icon\Itzikgur-My-Seven-Downloads-1.ico
		
		
		
		
		
		ImageList_Add(hImageList,hBitMap,NULL);	

		DeleteObject(hBitMap);	

		
		*/


	//	SendDlgItemMessage(hwnd,123,TVM_SETIMAGELIST,0,(LPARAM)&tvinsert);

		

//////////////////////////////// parents and children /////////////////////////////////
	HWND hwnd_tv=GetDlgItem(hwnd,123);
	/*
			tvinsert.hParent=NULL;			// top most level no need handle
			tvinsert.hInsertAfter=TVI_ROOT; // work as root level
            tvinsert.item.mask=TVIF_TEXT|TVIF_IMAGE;//|TVIF_CHILDREN;//|TVIF_SELECTEDIMAGE|
	        tvinsert.item.pszText="Parent";
			tvinsert.item.cchTextMax=128;
			tvinsert.item.iImage=0;
			tvinsert.item.iSelectedImage=1; 
			Parent=(HTREEITEM)SendMessage(hwnd_tv,TVM_INSERTITEM,0,(LPARAM)&tvinsert);	//SendDlgItemMessage(hwnd,123,TVM_INSERTITEM,0,(LPARAM)&tvinsert);

			
			
				tvinsert.hParent=Parent;         // handle of the above data
				tvinsert.hInsertAfter=TVI_LAST;  // below parent
				tvinsert.item.pszText="Child 1";
				tvinsert.item.iImage=0;
				tvinsert.item.iSelectedImage=0; 

					 Parent=(HTREEITEM)SendDlgItemMessage((HWND)Parent,123,TVM_INSERTITEM,0,(LPARAM)&tvinsert);
					*/
				 AddItemToTree(hwnd_tv,"C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\witeli.BMP",1);  
				 AddItemToTree(hwnd_tv,"C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\Tree View\\1200x900\\76.bmp",1);
				 AddItemToTree(hwnd_tv,"C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\Tree View\\1200x900\\92.bmp",1);
				 AddItemToTree(hwnd_tv,"C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\Tree View\\1200x900\\67.bmp",1);
					  

				 AddItemToTree(hwnd_tv,"C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\Tree View\\1200x900\\97.bmp",1);
				 AddItemToTree(hwnd_tv,"C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\Tree View\\1200x900\\23.bmp",1);
				 AddItemToTree(hwnd_tv,"C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\Tree View\\1200x900\\66.bmp",1);
		//				SendMessage(tvHandle, TVM_SETIMAGELIST,(WPARAM)0, (LPARAM)imageList2);
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				 
				 AddItemToTree(hwnd_tv,"C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\Tree View\\1200x900\\ukraina.bmp",1);
				 AddItemToTree(hwnd_tv,"C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\Tree View\\1200x900\\68.bmp",1);
				 AddItemToTree(hwnd_tv,"C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\Tree View\\1200x900\\87.bmp",1);
				 AddItemToTree(hwnd_tv,"C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\Tree View\\1200x900\\70.bmp",1);
					  SendMessage(hwnd_tv,TVM_SETTEXTCOLOR,0,(LPARAM)RGB(250,47,47));

				 AddItemToTree(hwnd_tv,"C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\Tree View\\1200x900\\8.bmp",1);
				 AddItemToTree(hwnd_tv,"C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\Tree View\\1200x900\\83.bmp",1);
				 AddItemToTree(hwnd_tv,"C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\Tree View\\1200x900\\69.bmp",1);
				 
				 AddItemToTree(hwnd_tv,"C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\Tree View\\1200x900\\64.bmp",1);
				  AddItemToTree(hwnd_tv,"C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\Tree View\\1200x900\\25.bmp",1);
				 AddItemToTree(hwnd_tv,"C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\Tree View\\1200x900\\43.bmp",1);

					//	 AddItemToTree(hwnd_tv,"saqartvelo21",2);
					//	  AddItemToTree(hwnd_tv,"saqartvelo21",2);
					//      AddItemToTree(hwnd_tv,"saqartvelo22",3); 
						
					 /*
////======================================================//////////////////////////////////////////////////////
			 tvinsert.hParent=NULL;			// top most level no need handle
			 tvinsert.hInsertAfter=TVI_LAST; // work as root level
	         tvinsert.item.pszText="Parent2";
             Parent=(HTREEITEM)SendDlgItemMessage(hwnd,123,TVM_INSERTITEM,0,(LPARAM)&tvinsert);
/////==================================================//////////////////////////////////////////////
	 tvinsert.hParent=NULL;			// top most level no need handle
			 tvinsert.hInsertAfter=TVI_LAST; // work as root level
	         tvinsert.item.pszText="Parent3";
             Parent=(HTREEITEM)SendDlgItemMessage(hwnd,123,TVM_INSERTITEM,0,(LPARAM)&tvinsert);
			 */

			 

}


//======================== 22222222222222=====

BOOL InitTreeViewImageLists(HWND hwndTV,HWND hw) 
{ 
    HIMAGELIST himl;  // handle to image list 
    HBITMAP hbmp;     // handle to bitmap 

    // Create the image list. 
    if (hImageList=ImageList_Create(16,16,ILC_COLOR16,2,10)) return 0; 

    // Add the open file, closed file, and document bitmaps. 
    hbmp = (HBITMAP)LoadImage(NULL,"C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\MARBLES.BMP", IMAGE_BITMAP,300,300, LR_LOADFROMFILE);
	bvb= (HBITMAP)LoadImage(NULL,"C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\MARBLES.BMP", IMAGE_BITMAP,300,300, LR_LOADFROMFILE);
	if(!hbmp)
	{

	//	MessageBox(hw,"No Items in TrtryuturyueeView","Error",MB_OK|MB_ICONINFORMATION);
	}
 int   g_nOpen = ImageList_Add(hImageList, bvb, (HBITMAP)NULL); 

    DeleteObject(hbmp); 


    // Fail if not all of the images were added. 
  //if (ImageList_GetImageCount(hImageList) )
//		MessageBox(hw,"No Items in TrtryuturyueeView","Error",MB_OK);

    // Associate the image list with the tree-view control. 
    TreeView_SetImageList(hwndTV, hImageList, TVSIL_NORMAL); 

    return TRUE; 
}
long __stdcall window_main_function_chvenia(HWND hwnd,unsigned int message, unsigned int wparam,long lparam)
{	
	HBITMAP bvb;
	 char path[100];
	HWND hwndtree=GetDlgItem(hwnd,123);
	HWND hstatic=GetDlgItem(hwnd,31);
	RECT r;
	
	switch(message)
	{
			case WM_INITDIALOG: 
		{

	

		}
		break;
		case WM_CREATE:
		create(hwnd, message,wparam,lparam);	
			 CreateWindow("msctls_trackbar32", "f", WS_VISIBLE | WS_CHILD, 500, 100, 200, 50, hwnd, (HMENU)251, NULL, NULL);


		
		/*
		
			    resourcesTreeView.hwnd = GetDlgItem(hwnd, ID_RESOURCES_TREE_VIEW);
    resourcesTreeView.insert.hParent = NULL;
    resourcesTreeView.insert.hInsertAfter = TVI_ROOT;
    resourcesTreeView.insert.item.mask = TVIF_TEXT | TVIF_IMAGE | TVIF_SELECTEDIMAGE;
resourcesTreeView.insert.item.pszText = "Parent";
    resourcesTreeView.insert.item.iImage = 0;
    resourcesTreeView.insert.item.iSelectedImage = 1;
    resourcesTreeView.parent = (HTREEITEM)SendDlgItemMessage(hwnd, ID_RESOURCES_TREE_VIEW, TVM_INSERTITEM, 0, (LPARAM)&resourcesTreeView.insert);
    resourcesTreeView.root = resourcesTreeView.parent;
    resourcesTreeView.before = resourcesTreeView.parent;
    UpdateWindow(hwnd);

	*/


		break;
		
		case WM_COMMAND:
	switch(LOWORD(wparam))
	{

				

	}
//	MessageBox(hwnd,"No Items in TrtryuturyueeView","Error",MB_OK|MB_ICONINFORMATION);
		break;
		
		case WM_LBUTTONDBLCLK:
	
		break;

		case WM_PAINT:

		
		break;

		case WM_KEYDOWN:
				MessageBox(hwnd,"main fanjara","main fanjara",0);
		break;
		case WM_NOTIFY:




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

	//					MessageBox(hwnd,"asdsad","asdas",0);
						
						break;



						case TVN_KEYDOWN :

							MessageBox(hwnd,"TreeView ","TreeView ",0);
							
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
					
							// MessageBox(hwnd,"asdsad","asdas",0);
						
						break;
						case TVN_ITEMCHANGING:
							// MessageBox(hwnd,"asdsad","asdas",0);
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
			break;
		
	
			if(message== WM_NOTIFY||message== WM_VSCROLL||message== WM_HSCROLL){	MessageBox(hwnd,"main fanjara","main fanjara",0);//notify_msg(hwnd, message, wparam, lparam);		 
			}




}
return DefWindowProc(hwnd,message,wparam,lparam);
}

void main()
{
	
int X,Y,W,H;
ULONG style=0;
WNDCLASS wc;
ZeroMemory(&wc,sizeof(WNDCLASS));
wc.style=CS_DBLCLKS|SW_MAXIMIZE;
wc.lpfnWndProc=(WNDPROC)&window_main_function_chvenia;
wc.lpszClassName="12";
wc.hbrBackground=(HBRUSH)CreateSolidBrush(RGB(200,200,200));
wc.hIcon=(HICON)LoadImage(0,"c:\\1.ico",IMAGE_ICON,16,16,LR_LOADFROMFILE);




	if(RegisterClass(&wc)==0)
	{
	MessageBox(hwnd,"RegisterClass error",0,0);
	return;
	}
//mtavari fanjara:
style=WS_VISIBLE|WS_OVERLAPPED|WS_CLIPCHILDREN;
X=10;Y=30;W=700;H=500;
hwnd=CreateWindow(wc.lpszClassName,"Main",style|SW_MAXIMIZE,X,Y,W,H,0,0,0,0);

 HWND buton;//=CreateWindow("button","Main",WS_VISIBLE|WS_CHILD|BS_BITMAP,300,Y,80,80,hwnd,(HMENU)0,0,0);

 bvb= (HBITMAP)LoadImage(NULL,"C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\MARBLES.BMP", IMAGE_BITMAP,300,300, LR_LOADFROMFILE);

		//	 SendMessage(bvb, STM_SETIMAGE, IMAGE_BITMAP, (LPARAM)hImage);

//SendMessage(buton, (UINT)BM_SETIMAGE,   (WPARAM)IMAGE_BITMAP, (LPARAM)bvb);






HCURSOR Cur = LoadCursorFromFile("C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\icon\\Debug\\too994.ani");
//SendMessage(buton, WM_SETCURSOR, 0, (LPARAM) Cur);
//SetClassLong (buton, GCL_HCURSOR, (LONG) Cur);

	CreateWindow("Static","asdasda",WS_VISIBLE|WS_BORDER|WS_CHILD|SS_BITMAP,202,0,300,300,hwnd,(HMENU)31,0,0);




MSG msg;
int s=1;
	while(s!=0)
	{
	s=GetMessage(&msg,0,0,0);
	TranslateMessage(&msg);
	DispatchMessage(&msg);
	}
}



HWND CreateATreeView(HWND hwndParent)
{ 
    RECT rcClient;  // dimensions of client area 
    HWND hwndTV;    // handle to tree-view control 
    InitCommonControls(); 
  
	///// | 0x0008|0x0010

	hwndTV = CreateWindow( WC_TREEVIEW,TEXT("Tree View"),0x5010|TVIF_IMAGE| TVS_DISABLEDRAGDROP|TVS_TRACKSELECT|TVS_LINESATROOT|TVS_HASBUTTONS|TVS_NONEVENHEIGHT|WS_VISIBLE |TVS_TRACKSELECT|TVS_INFOTIP| TVS_TRACKSELECT|WS_CHILD|WS_BORDER|TVS_HASLINES|TVS_EDITLABELS ,1,0, 200, 700, hwndParent, (HMENU)123, NULL, NULL);


	  
						HBITMAP	bvb= (HBITMAP)LoadImage(NULL,"C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\Tree View\\1200x900\\43.bmp", IMAGE_BITMAP,200,100, LR_LOADFROMFILE);
								
							

							SendMessage(hwndTV,STM_SETIMAGE,IMAGE_BITMAP,(LPARAM)bvb);

    {  //InitTreeViewImageLists(hwndTV,hwndParent)
    //    DestroyWindow(hwndTV); 
       // return FALSE; 
    } 



//	SendDlgItemMessage(hwndParent,123,TVM_SETIMAGELIST,0,(LPARAM)hImageList);
    return hwndTV;
} 