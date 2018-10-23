#include <windows.h>
#include <stdio.h>
#include "commctrl.h"
#include <Uxtheme.h>

HWND CreateListView (HWND hwndParent) ;
VOID SetView(HWND hWndListView, DWORD dwView) ;
#define IDM_CODE_SAMPLES 1

#pragma comment(lib,"comctl32.lib")

///==========================Defines ==============================//

//{{NO_DEPENDENCIES}}
// Microsoft Developer Studio generated include file.
// Used by listview.rsrc.rc
//
#define IDC_DIALOG                      101
#define IDI_ICON1                       102
#define IDR_MENU1                       106
#define IDC_LIST                        1000
#define IDC_DELSELITEM                  1001
#define IDC_DELALL                      1002
#define IDC_ADD                         1004
#define IDC_ADDSUB                      1005
#define IDC_ADDITEM                     1006
#define IDC_ADDSUBITEM                  1007
#define IDC_BOTH                        1008
#define IDC_RENAME                      40002
#define IDC_SELECT_ALL                  40004
#define IDC_LAST_ITEM                   40005

// Next default values for new objects
// 
#ifdef APSTUDIO_INVOKED
#ifndef APSTUDIO_READONLY_SYMBOLS
#define _APS_NEXT_RESOURCE_VALUE        108
#define _APS_NEXT_COMMAND_VALUE         40006
#define _APS_NEXT_CONTROL_VALUE         1013
#define _APS_NEXT_SYMED_VALUE           101
#endif
#endif
///==============================================================////
HINSTANCE hInst;
//////=========================================================////////////////////


// InitListViewColumns: Adds columns to a list-view control.
// hWndListView:        Handle to the list-view control. 
// Returns TRUE if successful, and FALSE otherwise. 
BOOL InitListViewColumns(HWND hWndListView) 
{ 
    char szText[]="asas";     // Temporary buffer.
    LVCOLUMN lvc;
    int iCol=0;

    // Initialize the LVCOLUMN structure.
    // The mask specifies that the format, width, text,
    // and subitem members of the structure are valid.
    lvc.mask = LVCF_FMT | LVCF_WIDTH | LVCF_TEXT | LVCF_SUBITEM;

    // Add the columns.
    
 for(int iCol=0;iCol<=5;iCol++)
 {
	
	
	
	lvc.iSubItem = iCol;
	
        lvc.pszText = (LPSTR)szText;
        lvc.cx = 100;               // Width of column in pixels.

        if ( iCol < 2 )
            lvc.fmt = LVCFMT_LEFT;  // Left-aligned column.
        else
            lvc.fmt = LVCFMT_RIGHT; // Right-aligned column.

        // Load the names of the column headings from the string resources.

        // Insert the columns into the list view.
        ListView_InsertColumn(hWndListView, iCol, &lvc);
		
			char lll[100];



        
}
 /*
   LVITEM item;
   item.mask = LVIF_TEXT;
   item.cchTextMax = 6;

   item.iSubItem = 1;
   item.pszText = TEXT("12345");
   item.iItem = 0;
   ListView_InsertItem(hWndListView, &item);

   item.iSubItem = 2; // zero based index of column
   item.pszText = TEXT("23456");
   ListView_InsertItem(hWndListView, &item);

   item.iSubItem = 3; // zero based index of column
   item.pszText = TEXT("34567");
   ListView_InsertItem(hWndListView, &item);
   */
 char g[100]="123151651";

	LVITEM item1;
   item1.mask = LVIF_TEXT;
   item1.cchTextMax = 256;

  item1.iSubItem = 0;
   item1.pszText = g;
   item1.iItem = 0;
 //  ListView_InsertItem(hWndListView, &item1);





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


SendMessage(hWndListView,LVM_DELETEITEM,1,0); // delete the item 





    return TRUE;
} 


/////// ==================================================== //////////////////

LRESULT ProcessCustomDraw (LPARAM lParam)
{
    LPNMLVCUSTOMDRAW lplvcd = (LPNMLVCUSTOMDRAW)lParam;

    switch(lplvcd->nmcd.dwDrawStage) 
    {
        case CDDS_PREPAINT : //Before the paint cycle begins
            //request notifications for individual listview items
            return CDRF_NOTIFYITEMDRAW;
            
        case CDDS_ITEMPREPAINT: //Before an item is drawn
            {
                return CDRF_NOTIFYSUBITEMDRAW;
            }
            break;
    
        case CDDS_SUBITEM | CDDS_ITEMPREPAINT: //Before a subitem is drawn
            {
                switch(lplvcd->iSubItem)
                {
                    case 0:
                    {
                      lplvcd->clrText   = RGB(255,255,255);
                      lplvcd->clrTextBk = RGB(240,55,23);
                      return CDRF_NEWFONT;
                    }
                    break;
                    
                    case 1:
                    {
                      lplvcd->clrText   = RGB(255,255,0);
                      lplvcd->clrTextBk = RGB(0,0,0);
                      return CDRF_NEWFONT;
                    }
                    break;  

                    case 2:
                    {
                      lplvcd->clrText   = RGB(20,26,158);
                      lplvcd->clrTextBk = RGB(200,200,10);
                      return CDRF_NEWFONT;
                    }
                    break;

                    case 3:
                    {
                      lplvcd->clrText   = RGB(12,15,46);
                      lplvcd->clrTextBk = RGB(200,200,200);
                      return CDRF_NEWFONT;
                    }
                    break;

                    case 4:
                    {
                      lplvcd->clrText   = RGB(120,0,128);
                      lplvcd->clrTextBk = RGB(20,200,200);
                      return CDRF_NEWFONT;
                    }
                    break;

                    case 5:
                    {
                      lplvcd->clrText   = RGB(255,255,255);
                      lplvcd->clrTextBk = RGB(0,0,150);
                      return CDRF_NEWFONT;
                    }
                    break;

                }
 
            }
    }
    return CDRF_DODEFAULT;
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////












//----------------------------------------------------------------
	//	LVITEM LvItem; 
//----------------------------------------------------------------

//==============Global Vatriabls===================
static HWND hList=NULL;  // List View identifier
LVCOLUMN LvCol; // Make Coluom struct for ListView
LVITEM LvItem;  // ListView Item struct
LV_DISPINFO lvd;
int iSelect=0;
int index=0;
int flag=0;
HWND hEdit;
bool escKey=0;
char tempstr[100]="";
TCHAR tchar;
MSG msg;
//===================================================
long __stdcall window_main_function_chvenia(HWND hwnd,unsigned int message, unsigned int wparam,long lparam)
{
	
	HWND Lhwnd;
	switch(message)
	{
		case WM_CREATE:
			{


				

	Lhwnd=CreateListView(hwnd);
	InitListViewColumns(Lhwnd);
			}
			break;
		case WM_NOTIFY:
	

			break;

		case WM_COMMAND:
	
		break;
		
		

		case WM_PAINT:

		break;

		case WM_KEYDOWN:
	
		break;

		case WM_CTLCOLOREDIT:
		{
	
		}
		break;

		case WM_DESTROY:
		exit(1);
		//PostQuitMessage(0);
		break;
	}


/*


  switch(message)
  {
        
         // This Window Message will close the dialog  //
		//============================================//
  case WM_CREATE:
	  hList=CreateListView(hwnd);
	  break;
        case WM_CLOSE:
        {
          PostQuitMessage(0);
          EndDialog(hwnd,0); // kill dialog
        }
        break;

		case WM_NOTIFY:
		{
			switch(LOWORD(wparam))
			{
			    case IDC_LIST: 
				LPNMLISTVIEW pnm = (LPNMLISTVIEW)lparam;

                if(pnm->hdr.hwndFrom == hList &&pnm->hdr.code == NM_CUSTOMDRAW)
                {
                    SetWindowLong(hwnd, DWL_MSGRESULT, (LONG)ProcessCustomDraw(lparam));
                    return TRUE;
                }
				
                if(((LPNMHDR)lparam)->code == NM_DBLCLK)
				{
				  char Text[255]={0};  
				  char Temp[255]={0};
				  char Temp1[255]={0};
				  int iSlected=0;
				  int j=0;

				  iSlected=SendMessage(hList,LVM_GETNEXTITEM,-1,LVNI_FOCUSED);
				  
				  if(iSlected==-1)
				  {
                    MessageBox(hwnd,"No Items in ListView","Error",MB_OK|MB_ICONINFORMATION);
					break;
				  }

				  memset(&LvItem,0,sizeof(LvItem));
                  LvItem.mask=LVIF_TEXT;
				  LvItem.iSubItem=0;
				  LvItem.pszText=Text;
				  LvItem.cchTextMax=256;
				  LvItem.iItem=iSlected;
                  
				  SendMessage(hList,LVM_GETITEMTEXT, iSlected, (LPARAM)&LvItem);
				  
				  sprintf(Temp1,Text);
				  
				  for(j=1;j<=5;j++)
				  {
					LvItem.iSubItem=j;
				    SendMessage(hList,LVM_GETITEMTEXT, iSlected, (LPARAM)&LvItem);
				    sprintf(Temp," %s",Text);
					lstrcat(Temp1,Temp);
				  }

				  MessageBox(hwnd,Temp1,"test",MB_OK);

				}
				if(((LPNMHDR)lparam)->code == NM_CLICK)
				{
					iSelect=SendMessage(hList,LVM_GETNEXTITEM,-1,LVNI_FOCUSED);
				    
					if(iSelect==-1)
					{                      
					  break;
					}
					index=iSelect;
					flag=1;
				}

                if(((LPNMHDR)lparam)->code == LVN_BEGINLABELEDIT)
                {
                  //Editing=1;
                  hEdit=ListView_GetEditControl(hList);
                  GetWindowText(hEdit, tempstr, sizeof(tempstr));
                }
				
                if(((LPNMHDR)lparam)->code == LVN_ENDLABELEDIT)
                {
                    int iIndex;
                    char text[255]="";

                    tchar = (TCHAR)msg.wParam;
                    if(tchar == 0x1b)
                          escKey=1;

                    iIndex=SendMessage(hList,LVM_GETNEXTITEM,-1,LVNI_FOCUSED);
                    if(iIndex==-1)
					   break;
					
					LvItem.iSubItem=0;
                                       
                    if(escKey==0)
					{
						LvItem.pszText=text; 
						GetWindowText(hEdit, text, sizeof(text));
                        SendMessage(hList,LVM_SETITEMTEXT,(WPARAM)iIndex,(LPARAM)&LvItem);
					}
                    else{
                        LvItem.pszText=tempstr;
                        SendMessage(hList,LVM_SETITEMTEXT,(WPARAM)iIndex,(LPARAM)&LvItem);
                        escKey=0;
                    }
                    //Editing=0;
                }
                break;
			}
		}

		case WM_PAINT:
			{
				return 0;
			}
			break;

		// This Window Message is the heart of the dialog  //
		//================================================//
		case WM_INITDIALOG:
			{
                int i;
				char Temp[255];
				LVBKIMAGE plvbki={0};
				char url[]="C:\\a.jpg";
				InitCommonControls();
				hList=GetDlgItem(hwnd,IDC_LIST); // get the ID of the ListView				 
				
				memset(&plvbki,0,sizeof(plvbki));
				plvbki.ulFlags=LVBKIF_SOURCE_URL;
				plvbki.pszImage=url;
				plvbki.xOffsetPercent=40;
				plvbki.yOffsetPercent=15;
				OleInitialize(NULL);
				
				SendMessage(hList,LVM_SETTEXTBKCOLOR, 0,(LPARAM)CLR_NONE);
				SendMessage(hList,LVM_SETBKIMAGE,0,(LPARAM)(LPLVBKIMAGE)&plvbki);
				
                SendMessage(hList,LVM_SETEXTENDEDLISTVIEWSTYLE,0,LVS_EX_FULLROWSELECT); // Set style
				
				SendMessageA(hwnd,WM_SETICON,(WPARAM) 1,(LPARAM) LoadIconA(hInst,MAKEINTRESOURCE(IDI_ICON1)));
			
				// Here we put the info on the Coulom headers
				// this is not data, only name of each header we like
                memset(&LvCol,0,sizeof(LvCol)); // Reset Coluom
				LvCol.mask=LVCF_TEXT|LVCF_WIDTH|LVCF_SUBITEM; // Type of mask
				LvCol.cx=0x28;                                // width between each coloum
				LvCol.pszText="Item";                     // First Header
 				LvCol.cx=0x42;

				// Inserting Couloms as much as we want
				SendMessage(hList,LVM_INSERTCOLUMN,0,(LPARAM)&LvCol); // Insert/Show the coloum
				LvCol.pszText="Sub Item1";                          // Next coloum
                SendMessage(hList,LVM_INSERTCOLUMN,1,(LPARAM)&LvCol); // ...
				LvCol.pszText="Sub Item2";                       //
                SendMessage(hList,LVM_INSERTCOLUMN,2,(LPARAM)&LvCol); //
				LvCol.pszText="Sub Item3";                              //
                SendMessage(hList,LVM_INSERTCOLUMN,3,(LPARAM)&LvCol); //
				LvCol.pszText="Sub Item4";                            //
                SendMessage(hList,LVM_INSERTCOLUMN,4,(LPARAM)&LvCol); //
				LvCol.pszText="Sub Item5";                      //
                SendMessage(hList,LVM_INSERTCOLUMN,5,(LPARAM)&LvCol); // ...same as above

                memset(&LvItem,0,sizeof(LvItem)); // Reset Item Struct
				
				//  Setting properties Of Items:

				LvItem.mask=LVIF_TEXT;   // Text Style
				LvItem.cchTextMax = 256; // Max size of test
                
				LvItem.iItem=0;          // choose item  
				LvItem.iSubItem=0;       // Put in first coluom
				LvItem.pszText="Item 0"; // Text to display (can be from a char variable) (Items)
                
				SendMessage(hList,LVM_INSERTITEM,0,(LPARAM)&LvItem); // Send to the Listview
				
				for(i=1;i<=5;i++) // Add SubItems in a loop
				{
					LvItem.iSubItem=i;
					sprintf(Temp,"SubItem %d",i);
					LvItem.pszText=Temp;
					SendMessage(hList,LVM_SETITEM,0,(LPARAM)&LvItem); // Enter text to SubItems
				}
				
				// lets add a new Item:
                LvItem.iItem=1;            // choose item  
				LvItem.iSubItem=0;         // Put in first coluom
				LvItem.pszText="Item 1";   // Text to display (can be from a char variable) (Items)
                SendMessage(hList,LVM_INSERTITEM,0,(LPARAM)&LvItem); // Send to the Listview

				for(i=1;i<=5;i++) // Add SubItems in a loop
				{
					LvItem.iSubItem=i;
					sprintf(Temp,"SubItem %d",i);
					LvItem.pszText=Temp;
					SendMessage(hList,LVM_SETITEM,0,(LPARAM)&LvItem); // Enter etxt to SubItems
				}
					                
                //ListView_SetItemState(hList,0,LVIS_SELECTED	,LVIF_STATE);
                ShowWindow(hwnd,SW_NORMAL); 
                UpdateWindow(hwnd); 

                while(TRUE)
                {
                
                    if(PeekMessage(&msg,NULL,0,0,PM_REMOVE))
                    {   
                        /*
                        if(msg.message==WM_CHAR)
                        {
                            tchar = (TCHAR)msg.wParam;
                            if(tchar == 0x1b)
                                escKey=1;

                        }
                        
                        if(msg.message==WM_QUIT)// killing while looking for a message
                        {
                                break;
                        }
                        
                        
                        TranslateMessage(&msg);
                        DispatchMessage(&msg);
                    }                   
                }
			}
			break;

     // This Window Message will control the dialog  //
	//==============================================//
        case WM_COMMAND:
		{
                 switch(LOWORD(wparam)) // what we press on?
				 {

					   case IDC_ADDITEM:
						 {
                           int iItem;
						   char ItemText[100];

						   iItem=SendMessage(hList,LVM_GETITEMCOUNT,0,0);
                           
						   GetDlgItemText(hwnd,IDC_ADD,ItemText,100);

						   if((lstrlen(ItemText))==0)
						   {
							   MessageBox(hwnd,"Please Write Some Text","Error",MB_OK|MB_ICONINFORMATION);
						       break;
						   }

                           LvItem.iItem=iItem;            // choose item  
				           LvItem.iSubItem=0;         // Put in first coluom
				           LvItem.pszText=ItemText;   // Text to display (can be from a char variable) (Items)
                           SendMessage(hList,LVM_INSERTITEM,0,(LPARAM)&LvItem); // Send to the Listview
						 }
						 break;

					case IDC_ADDSUBITEM:
						{
                           int Item,i;
						   char SubItemText[100];

						   Item=SendMessage(hList,LVM_GETITEMCOUNT,0,0);
                           
						   GetDlgItemText(hwnd,IDC_ADDSUB,SubItemText,100);
						   
						   if((lstrlen(SubItemText))==0)
						   {
							   MessageBox(hwnd,"Please Write Some Text","Error",MB_OK|MB_ICONINFORMATION);
						       break;
						   }

						   LvItem.iItem=Item-1;            // choose item  
				           
						   for(i=1;i<=5;i++)
						   {
				              LvItem.pszText=SubItemText;   // Text to display (can be from a char variable) (Items)
                              LvItem.iSubItem=i;         // Put in first coluom
						      SendMessage(hList,LVM_SETITEM,0,(LPARAM)&LvItem);
						   }
						}
						break;

					case IDC_BOTH:
						{
                           int itemIndex,j;
						   char iSubItemText[100]="";
						   char iItemText[100]="";

						   itemIndex=SendMessage(hList,LVM_GETITEMCOUNT,0,0);
                           
						   GetDlgItemText(hwnd,IDC_ADD,iItemText,100);
						   GetDlgItemText(hwnd,IDC_ADDSUB,iSubItemText,100);
                           
						   if((lstrlen(iSubItemText) && lstrlen(iItemText))==0)
						   {
							   MessageBox(hwnd,"Please Write Some Text","Error",MB_OK|MB_ICONINFORMATION);
						       break;
						   }

						   LvItem.iItem=itemIndex;            // choose item  
				           LvItem.iSubItem=0;
						   LvItem.pszText=iItemText;
						   SendMessage(hList,LVM_INSERTITEM,0,(LPARAM)&LvItem);
						   
						   for(j=1;j<=5;j++)
						   {
				              LvItem.pszText=iSubItemText;   // Text to display (can be from a char variable) (Items)
                              LvItem.iSubItem=j;         // Put in first coluom
						      SendMessage(hList,LVM_SETITEM,0,(LPARAM)&LvItem);
						   }
						}
						break;

					case IDC_DELALL:
						SendMessage(hList,LVM_DELETEALLITEMS,0,0);
						break;

					case IDC_DELSELITEM:
						if(flag)
                           SendMessage(hList,LVM_DELETEITEM,iSelect,0);
						flag=0;
						break;

					case IDC_RENAME:
					{
						if(index==-1)
						{
						   MessageBox(hwnd,"Nothing to rename","error",MB_OK);
						}
						else{
							//Editing=1;
							SendMessage(hList,LVM_EDITLABEL ,(WPARAM)index,(LPARAM)0);
						}
					}
					break;

                    case IDC_SELECT_ALL:
                    {
                        ListView_SetItemState(hList, -1, 0, LVIS_SELECTED); // deselect all
                        SendMessage(hList,LVM_ENSUREVISIBLE ,(WPARAM)-1,FALSE); // Send to the Listview                        
                        ListView_SetItemState(hList,-1,LVIS_SELECTED ,LVIS_SELECTED);                
                    }
                    break;

                    case IDC_LAST_ITEM:
                    {            
                        int items;
                        items = SendMessage(hList,LVM_GETITEMCOUNT ,(WPARAM)0,(LPARAM)0);
                        items--;
                        ListView_SetItemState(hList, -1, 0, LVIS_SELECTED); // deselect all
                        SendMessage(hList,LVM_ENSUREVISIBLE ,(WPARAM)items,FALSE); // Send to the Listview
                        ListView_SetItemState(hList,items,LVIS_SELECTED ,LVIS_SELECTED);
                        ListView_SetItemState(hList,items,LVIS_FOCUSED ,LVIS_FOCUSED);
                        
                    }
                    break;

				 }
		}
        break;
    
	    default:
		{
             return FALSE;
		}
    }


	*/







return DefWindowProc(hwnd,message,wparam,lparam);
}
//----------------------------------------------------------------

int __stdcall WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, PSTR szCmdLine, int iCmdShow)
//void main()
{
HWND hwnd=0;
int X,Y,W,H;
ULONG style=0;

WNDCLASS wc;
ZeroMemory(&wc,sizeof(WNDCLASS));

wc.style=CS_DBLCLKS;
wc.lpfnWndProc=(WNDPROC)&window_main_function_chvenia;
wc.lpszClassName="12";
wc.hbrBackground=(HBRUSH)CreateSolidBrush(RGB(200,200,20));
wc.hIcon=(HICON)LoadImage(0,"c:\\1.ico",IMAGE_ICON,16,16,LR_LOADFROMFILE);

	if(RegisterClass(&wc)==0)
	{
	MessageBox(hwnd,"RegisterClass error",0,0);
	return 0;
	}


//mtavari fanjara:
style=WS_VISIBLE|WS_OVERLAPPEDWINDOW|WS_CLIPCHILDREN;
X=10;Y=30;W=750;H=500;
hwnd=CreateWindow(wc.lpszClassName,"Main",style,X,Y,W,H,0,0,0,0);

	
//////////////// list Vieew //
  

/////////////////////////////////////////////////


MSG msg;
	while(GetMessage(&msg,0,0,0))
	{
	TranslateMessage(&msg);
	DispatchMessage(&msg);
	}
}



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
         WS_VISIBLE|WS_BORDER|WS_CHILD | LVS_REPORT | LVS_EDITLABELS, 
		 10, 10,rcClient.right-20 ,rcClient.bottom-20, 
         hwndParent, (HMENU)369, 0, 0);





    return (hwndList);
}


// SetView: Sets a list-view's window style to change the view.
// hWndListView: A handle to the list-view control. 
// dwView:       A value specifying the new view style.
//
VOID SetView(HWND hWndListView, DWORD dwView) 
{ 
    // Retrieve the current window style. 
    DWORD dwStyle = GetWindowLong(hWndListView, GWL_STYLE); 
    
    // Set the window style only if the view bits changed.
    if ((dwStyle & LVS_TYPEMASK) != dwView) 
    {
        SetWindowLong(hWndListView,GWL_STYLE,(dwStyle & ~LVS_TYPEMASK) | dwView);
    }          
}              