/******************************************************************************
\-----------------------------------------------------------------------------/
/-------Mike C----------------------------------------------------------------\
\-----------------------------------------------------------------------------/
/-----------------------------------------------------------------------------\
\-------Jan 18, 2002----------------------------------------------------------/
/-----------------------------------------------------------------------------\
\-------Windows Programming in WIN32API --------------------------------------/
/-----------------------------------------------------------------------------\
******************************************************************************/

/******************************************************************************
\-----------------------------------------------------------------------------/
        -Toolbar creation.
        -This example only shows hot to access and create the pre-made toolbar
        icons in the SDK and make their tooltips.
        -DONT FORGET GUI...
/-----------------------------------------------------------------------------\
******************************************************************************/

#include <windows.h>
#include <windowsx.h>
#include <commctrl.h>
#include <stdio.h>


#pragma comment(lib,"comctl32.lib")


#define WIN_CLASS_NAME "Main Window"

HWND main_window_handle;             //globally track the main window object
HINSTANCE hinstance_main;            //globally track hinstance of the window

#define GCP_TOOLBAR_ID 1300
#define IDM_NEW 1700
#define IDM_OPEN 1701
#define IDM_SAVE 1702
#define IDM_GO 1703
#define IDM_FINDE 1704
#define IDM_PRINT 1705
#define IDM_REPLACE 1706
#define IDM_UNDO 1707
#define IDM_PROPETIES 1708  
#define IDM_folder 1709  
HWND tbar;               

static TBBUTTON tbButtons[] = {
  { {0}, {0}, {TBSTATE_ENABLED}, {TBSTYLE_SEP}, {0}, {0}, {0}},
  { {STD_FILENEW}, {IDM_NEW}, {TBSTATE_ENABLED}, {TBSTYLE_BUTTON}, {0}, {0}, {0}} ,
  { {STD_FILEOPEN}, {IDM_OPEN}, {TBSTATE_ENABLED}, {TBSTYLE_BUTTON}, {0}, {0}, {0}},
  { {STD_FILESAVE}, {IDM_SAVE}, {TBSTATE_ENABLED}, {TBSTYLE_BUTTON}, {0}, {0}, {0}},
    { {STD_HELP }, {IDM_GO}, {TBSTATE_ENABLED}, {TBSTYLE_BUTTON}, {0}, {0}, {0}},


	  { {STD_FIND }, {IDM_FINDE}, {TBSTATE_ENABLED}, {TBSTYLE_BUTTON}, {0}, {0}, {0}},
	    { {STD_PRINT }, {IDM_PRINT}, {TBSTATE_ENABLED}, {TBSTYLE_BUTTON}, {0}, {0}, {0}},

		
	  { {STD_REPLACE }, {IDM_REPLACE}, {TBSTATE_ENABLED}, {TBSTYLE_BUTTON}, {0}, {0}, {0}},
	    { {STD_UNDO }, {IDM_UNDO}, {TBSTATE_ENABLED}, {TBSTYLE_BUTTON}, {0}, {0}, {0}},
		  { {STD_PROPERTIES }, {IDM_PROPETIES}, {TBSTATE_ENABLED}, {TBSTYLE_BUTTON}, {0}, {0}, {0}},

		    { {VIEW_NEWFOLDER }, {IDM_folder}, {TBSTATE_ENABLED}, {TBSTYLE_BUTTON}, {0}, {0}, {0}},




  { {0}, {0}, {TBSTATE_ENABLED}, {TBSTYLE_SEP}, {0}, {0}, {0}}
};


LRESULT CALLBACK MainWindowProc(HWND hwnd,UINT msg,WPARAM wparam,LPARAM lparam);

//int WINAPI WinMain(HINSTANCE hinstance,HINSTANCE hprevinstance,
//							LPSTR lpcmdline,int ncmdshow)
int main()
{
	HWND	   hwnd;
	MSG		   msg;

	WNDCLASSEX winclass;

	winclass.cbSize         = sizeof(WNDCLASSEX);
	winclass.style		   	  = CS_DBLCLKS;
	winclass.lpfnWndProc   	= MainWindowProc;
	winclass.cbClsExtra		  = 0;
	winclass.cbWndExtra		  = 0;
	winclass.hInstance		  = 0;
	winclass.hIcon		   	  = LoadIcon(NULL, IDI_APPLICATION);
	winclass.hCursor	    	= LoadCursor(NULL, IDC_ARROW);
	winclass.hbrBackground	= (HBRUSH)GetStockObject(GRAY_BRUSH);
	winclass.lpszMenuName	  = NULL;
	winclass.lpszClassName	= WIN_CLASS_NAME;
	winclass.hIconSm        = LoadIcon(NULL, IDI_APPLICATION);

	hinstance_main = 0;

	if (!RegisterClassEx(&winclass))
		return(0);

	if (!(hwnd = CreateWindowEx(NULL,WIN_CLASS_NAME,"Tutorial Window",WS_TILEDWINDOW | WS_MAXIMIZEBOX | WS_SIZEBOX ,0 ,0 ,800,500,NULL,NULL,0,NULL)))
		return(0);

  InitCommonControls();

	ShowWindow(hwnd, SW_SHOW);

  main_window_handle = hwnd;

	while(GetMessage(&msg, NULL, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}

  return(msg.wParam);
}



LRESULT CALLBACK MainWindowProc(HWND hwnd,UINT msg,WPARAM wparam,LPARAM lparam)
{
  switch(msg)
	{
	  case WM_CREATE:
    {
      /*-------------Create the button----------------------------------------*/
      //create the tool bar:
      tbar = CreateToolbarEx(hwnd,
        WS_CHILD | WS_BORDER | WS_VISIBLE | TBSTYLE_WRAPABLE | TBSTYLE_TOOLTIPS,
        GCP_TOOLBAR_ID, 11, (HINSTANCE)HINST_COMMCTRL,
        IDB_STD_SMALL_COLOR, (LPCTBBUTTON)&tbButtons,
       12, 24, 22, 0, 0, sizeof(TBBUTTON));
      /*
      Parameters:
      GCP_TOOLBAR_ID             -UINT defined above
      11                         -number of bitmaps in (HINSTANCE)HINST_COMMCTRL
                                 and IDB_STD_SMALL_COLOR
      (HINSTANCE)HINST_COMMCTRL  -Specifications for the bitmap styles and where
      and IDB_STD_SMALL_COLOR    they are found.
      (LPCTBBUTTON)&tbButtons    -the structure we filled above, this is what
      													 appears on the toolbar
      5                          -number of buttons (seperations included)
      24, 22, 0, 0,              -dimensions of the button and bitmaps added
      													 to the bar (0,0 used in this case for bitmap
                                 size because they will be overwrdden by
                                 IDB_STD_SMALL_COLOR.)
      sizeof(TBBUTTON)           -size of the structure, always use this
      */
      //tell the toolbar to size itself:
      SendMessage(tbar, TB_AUTOSIZE, 0, 0);
      //show the toolbar:
      ShowWindow(tbar, SW_SHOW);
      /*----------------------------------------------------------------------*/
      /************************************************************************/
    }break;
    case WM_SIZE:
    {
      /*
      The following is done so that when the window is resized, the toolbar
      will resize. Just a one-liner.
      */
      SendMessage(tbar, TB_AUTOSIZE, 0, 0);
    }break;
    case WM_COMMAND:
    {
      //toolbar capture:
      if(LOWORD(wparam) == IDM_NEW)
      {
        MessageBox(hwnd, "NEW", "Button Depressed:", MB_OK);
        break;
      }
      if(LOWORD(wparam) == IDM_OPEN)
      {
        MessageBox(hwnd, "OPEN", "Button Depressed:", MB_OK);
        break;
      }
      if(LOWORD(wparam) == IDM_SAVE)
      {
        MessageBox(hwnd, "SAVE", "Button Depressed:", MB_OK);
        break;
      }
	    if(LOWORD(wparam) == IDM_GO)
      {
        MessageBox(hwnd, "GO", "Button Depressed:", MB_OK);
        break;
      }
		if(LOWORD(wparam) == IDM_FINDE)
      {
        MessageBox(hwnd, "FINDE", "Button Depressed:", MB_OK);
        break;
      }
		if(LOWORD(wparam) == IDM_PRINT)
      {
        MessageBox(hwnd, "PRINT", "Button Depressed:", MB_OK);
        break;
      }
			if(LOWORD(wparam) == IDM_REPLACE)
      {
        MessageBox(hwnd, "IDM_REPLACE", "Button Depressed:", MB_OK);
        break;
      }
			if(LOWORD(wparam) == IDM_PROPETIES)
      {
        MessageBox(hwnd, "IDM_PROPETIES", "Button Depressed:", MB_OK);
        break;
      }

	
	if(LOWORD(wparam) == IDM_UNDO)
      {
        MessageBox(hwnd, "UNDO", "Button Depressed:", MB_OK);
        break;
      }


	if(LOWORD(wparam) ==IDM_folder)
      {
        MessageBox(hwnd, "UNDO", "Button Depressed:", MB_OK);
        break;
      }

    }break;
    /*========================================================================*/
    /*
    What follows in WM_NOTIFY is a merger of algorithms I found that has the
    effect of tooltipping the toolbar (text that appears when the mouse is over
    a button in the tool bar).
    Check the NMHDR structure for more info.
    */
    /*========================================================================*/
    case WM_NOTIFY:
    {
      switch (((LPNMHDR) lparam)->code)   //switch statement for the value code
      																		//in (LPNMHDR) lparam.
      {
        case TTN_NEEDTEXT:                //if this is TTN_NEEDTEXT...
        {
          LPTOOLTIPTEXT lptttext;         //tooltip structure
          lptttext = (LPTOOLTIPTEXT) lparam;    //fill the structure with values
          																			//already in the lparam
          lptttext->hinst = hinstance_main;//set the value of mamber hinst in
          															   //the structure to hinstance_main.
          switch(lptttext->hdr.idFrom)    //switch for the id the called this
          {
            case IDM_NEW:
            {
              lptttext->lpszText = "New";
            }break;
            case IDM_OPEN:
            {
              lptttext->lpszText = "Open";
            }break;
            case IDM_SAVE:
            {
              lptttext->lpszText = "Save";
            }break;

			case IDM_GO:
            {
        //      lptttext->lpszText = "Go";
            }break;
			
			case IDM_PRINT:
            {
              lptttext->lpszText = "PRINT";
            }break;
						
			case IDM_FINDE:
            {
              lptttext->lpszText = "FINDE";
            }break;

			case IDM_UNDO:
            {
              lptttext->lpszText = "UNDO";
            }break;
			case IDM_REPLACE:
            {
              lptttext->lpszText = "REPLACE";
            }break;

			case IDM_PROPETIES:
			{
              lptttext->lpszText = "IDM_PROPETIES";
            }break;

			case IDM_folder:
				{
              lptttext->lpszText = "IDM_folder";
            }break;

          }
        }
      }
    }break;
    /*========================================================================*/
    case WM_DESTROY:
    {
      PostQuitMessage(0);

      return(0);
    }break;
  }
  return (DefWindowProc(hwnd, msg, wparam, lparam));
}


/******************************************************************************
\-----------------------------------------------------------------------------/
        -Toolbar creation.
        -This example only shows hot to access and create the pre-made toolbar
        icons in the SDK and make their tooltips.
        -DONT FORGET GUI...
/-----------------------------------------------------------------------------\
******************************************************************************/
