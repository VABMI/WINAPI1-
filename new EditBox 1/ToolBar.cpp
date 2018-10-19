     
#include "head.h"


	
static TBBUTTON tbButtons[] = {
  { {0}, {0}, {TBSTATE_ENABLED}, {TBSTYLE_SEP}, {0}, {0}, {0}},

  { {STD_FILENEW}, {1700}, {TBSTATE_ENABLED}, {TBSTYLE_BUTTON}, {0}, {0}, {0}} ,

  { {STD_FILEOPEN}, {1701}, {TBSTATE_ENABLED}, {TBSTYLE_BUTTON}, {0}, {0}, {0}},
  { {STD_FILESAVE}, {1702}, {TBSTATE_ENABLED}, {TBSTYLE_BUTTON}, {0}, {0}, {0}},
    { {STD_HELP }, {1703}, {TBSTATE_ENABLED}, {TBSTYLE_BUTTON}, {0}, {0}, {0}},


	  { {STD_FIND }, {IDM_FINDE}, {TBSTATE_ENABLED}, {TBSTYLE_BUTTON}, {0}, {0}, {0}},
	    { {STD_PRINT }, {IDM_PRINT}, {TBSTATE_ENABLED}, {TBSTYLE_BUTTON}, {0}, {0}, {0}},

		 
	  
	    { {STD_UNDO }, {IDM_UNDO}, {TBSTATE_ENABLED}, {TBSTYLE_BUTTON}, {0}, {0}, {0}},
		 { { STD_REDOW  }, {IDM_NEXT}, {TBSTATE_ENABLED}, {TBSTYLE_BUTTON}, {0}, {0}, {0}},







};






HWND Toolar(HWND HwndGlob)
{




	HWND	tolbar = CreateToolbarEx(HwndGlob,
        WS_CHILD | WS_BORDER | WS_VISIBLE | TBSTYLE_WRAPABLE | TBSTYLE_TOOLTIPS,669, 10, (HINSTANCE)HINST_COMMCTRL,
        IDB_STD_SMALL_COLOR, (LPCTBBUTTON)&tbButtons,
       9, 24, 22,0, 0, sizeof(TBBUTTON));

		  //    SendMessage(tolbar, TB_AUTOSIZE, 0, 0);
			//  ShowWindow(tolbar, SW_SHOW);
return  tolbar;
}

