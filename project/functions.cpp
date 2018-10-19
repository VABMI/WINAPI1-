


//---------------------------------------------
UINT create_menu(HWND hwnd)
{
HMENU hmenu=CreateMenu();
	if(!hmenu)
	return GetLastError();

HMENU hmenu_popup_file=CreatePopupMenu();
AppendMenu(hmenu,MF_POPUP,(UINT_PTR)hmenu_popup_file,"&Common Dialogs");
AppendMenu(hmenu_popup_file,MF_STRING,ID_MENU_COLOR,"&Color");
AppendMenu(hmenu_popup_file,MF_STRING,ID_MENU_FONT,"&Font");
AppendMenu(hmenu_popup_file,MF_STRING,ID_MENU_FINDTEXT,"&Find Text");


HMENU hmenu_popup_wndclass=CreatePopupMenu();
AppendMenu(hmenu,MF_POPUP,(UINT_PTR)hmenu_popup_wndclass,"&Wnd Classes");
AppendMenu(hmenu_popup_wndclass,MF_STRING,ID_MENU_TOOLBAR,"&Tool Bar");
AppendMenu(hmenu_popup_wndclass,MF_STRING,123,"&F");
AppendMenu(hmenu_popup_wndclass,MF_STRING,1234,"&");


HMENU hmenu_popup_options=CreatePopupMenu();
AppendMenu(hmenu, MF_POPUP, (UINT_PTR)hmenu_popup_options, "&Options");
AppendMenu(hmenu_popup_options,MF_STRING,ID_MENU_ABOUT,"&About");

HMENU hmenu_popup_help=CreatePopupMenu();
AppendMenu(hmenu_popup_options, MF_POPUP, (UINT_PTR)hmenu_popup_help, "&Help");
AppendMenu(hmenu_popup_help,MF_STRING,ID_MENU_C_CPP,"&c,c++");
AppendMenu(hmenu_popup_help,MF_STRING,ID_MENU_WINAPI,"&Winapi");
AppendMenu(hmenu_popup_help,MF_STRING,ID_MENU_ALL,"&All");


HMENU hmenu_popup_regions=CreatePopupMenu();
AppendMenu(hmenu,MF_POPUP,(UINT_PTR)hmenu_popup_regions,"&Regions");
AppendMenu(hmenu_popup_regions,MF_STRING,ID_MENU_REGION_ELIPS,"&Circle");
AppendMenu(hmenu_popup_regions,MF_STRING,ID_MENU_REGION_ROUNRECT,"&Round Rect");
AppendMenu(hmenu_popup_regions,MF_STRING,ID_MENU_REGION_RECT,"&Rect");


HMENU hmenu_popup_algorithms=CreatePopupMenu();
AppendMenu(hmenu,MF_POPUP,(UINT_PTR)hmenu_popup_algorithms,"&Algorithms");

AppendMenu(hmenu_popup_algorithms,MF_STRING,ID_MENU_PRIMES,"&Primes");
//AppendMenu(hmenu_popup_regions,MF_STRING,ID_MENU_REGION_ROUNRECT,"&Round Rect");


SetMenu(hwnd,hmenu);
}
//---------------------------------------------
HFONT create_font(HWND hwnd)
{
HFONT hfont;
hfont=CreateFont(12,5,0,0,FW_BOLD,0,0,0,ANSI_CHARSET, 
      OUT_TT_PRECIS, CLIP_DEFAULT_PRECIS, DEFAULT_QUALITY, 
      DEFAULT_PITCH | FF_DONTCARE,"Tahoma");
SendMessage(hwnd,WM_SETFONT,(UINT)hfont,0);
return hfont;
}
//---------------------------------------------
HFONT select_font(HWND hwnd)
{
HDC hdc;                  // display device context of owner window
CHOOSEFONT cf;            // common dialog box structure
static LOGFONT lf;        // logical font structure
static DWORD rgbCurrent;  // current text color
HFONT hfont, hfontPrev;
DWORD rgbPrev;

// Initialize CHOOSEFONT
ZeroMemory(&cf, sizeof(cf));
cf.lStructSize = sizeof (cf);
cf.hwndOwner = hwnd;
cf.lpLogFont = &lf;
cf.rgbColors = rgbCurrent;
cf.Flags = CF_SCREENFONTS | CF_EFFECTS;

hdc=GetDC(hwnd);

	if (ChooseFont(&cf)==TRUE)
	{
	hfont = CreateFontIndirect(cf.lpLogFont);
	//cf.lpLogFont->lfHeight*=-1;
	
	hfontPrev = (HFONT)SelectObject(hdc,hfont);
	SendMessage(hwnd,WM_SETFONT,(UINT)hfont,1);

	rgbPrev = SetTextColor(hdc,cf.rgbColors);

	SetBkMode(hdc,1);
	//SetTextColor(hdc,cf.rgbColors);


	//InvalidateRect(hwnd,0,1);
	//SendMessage(hwnd,WM_PAINT,0,0);
	//InvalidateRect(hwnd,0,0);

	TextOut(hdc,0,220,"1523126351237",12);

	//InvalidateRect(hwnd,0,1);
	//SendMessage(hwnd,WM_PAINT,0,1);
	//InvalidateRect(hwnd,0,0);
	}
return hfont;
}
//---------------------------------------------
ULONG choose_color(HWND hwnd)
{
CHOOSECOLOR cc;
static COLORREF acrCustClr[16]; // array of custom colors                    // owner window
HBRUSH hbrush;                  // brush handle
static DWORD rgbCurrent;        // initial color selection

// Initialize CHOOSECOLOR 
ZeroMemory(&cc, sizeof(CHOOSECOLOR));
cc.lStructSize = sizeof(CHOOSECOLOR);
cc.hwndOwner = hwnd;
cc.lpCustColors = (LPDWORD) acrCustClr;
cc.rgbResult = rgbCurrent;

cc.Flags = CC_FULLOPEN | CC_RGBINIT;
 
	if (ChooseColor(&cc)==TRUE)
	{
    //hbrush = CreateSolidBrush(cc.rgbResult);
    return cc.rgbResult;
	}
return 0;
}
//---------------------------------------------
int find_text(HWND hwnd)
{
FINDREPLACE fr;       // common dialog box structure           // owner window
CHAR szFindWhat[380]="Hail";  // buffer receiving string
HWND hdlg = NULL;     // handle of Find dialog box
 
// Initialize FINDREPLACE
ZeroMemory(&fr, sizeof(FINDREPLACE));
fr.lStructSize = sizeof(FINDREPLACE);
fr.hwndOwner = hwnd;
fr.lpstrFindWhat = szFindWhat;
fr.wFindWhatLen = 380;
fr.Flags = FR_FINDNEXT;

hdlg = FindText(&fr);
return 1;
}
//---------------------------------------------
HWND CreateAToolBar(HWND hwndParent,int ID_TOOLBAR,HINSTANCE g_hinst) 
{ 
#define NUM_BUTTONS 5
#define IDM_PASTE	100
#define	BMP_PASTE	200

#define IDM_COPY	300
#define BMP_COPY	400
#define IDM_CUT		500

#define BMP_CUT		600
#define IDS_PASTE	700
#define IDS_COPY	800
#define IDB_BUTTONS	900
#define	IDS_CUT		910
#define NUM_BUTTON_BITMAPS 920
#define MAX_LEN		1000

    HWND hwndTB; 
    TBADDBITMAP tbab; 
    TBBUTTON tbb[3]; 
    char szBuf[16]; 
    int iCut, iCopy, iPaste; 
 
    // Ensure that the common control DLL is loaded. 
    InitCommonControls(); 
 
    // Create a toolbar that the user can customize and that has a 

    // tooltip associated with it. 
    hwndTB = CreateWindowEx(0, TOOLBARCLASSNAME, (LPSTR) NULL, 
        WS_CHILD | TBSTYLE_TOOLTIPS | CCS_ADJUSTABLE, 
        0, 0, 0, 0, hwndParent, (HMENU) ID_TOOLBAR, g_hinst, NULL); 
 
    // Send the TB_BUTTONSTRUCTSIZE message, which is required for 
    // backward compatibility. 
    SendMessage(hwndTB, TB_BUTTONSTRUCTSIZE, 
        (WPARAM) sizeof(TBBUTTON), 0); 
 
    // Add the bitmap containing button images to the toolbar. 

    tbab.hInst = g_hinst; 
    tbab.nID   = IDB_BUTTONS; 
    SendMessage(hwndTB, TB_ADDBITMAP, (WPARAM) NUM_BUTTON_BITMAPS, 
        (WPARAM) &tbab); 
 
    // Add the button strings to the toolbar. 
    LoadString(g_hinst, IDS_CUT, (LPSTR) &szBuf, MAX_LEN); 
    iCut = SendMessage(hwndTB, TB_ADDSTRING, 0, (LPARAM) (LPSTR) szBuf); 
 
    LoadString(g_hinst, IDS_COPY, (LPSTR) &szBuf, MAX_LEN); 
    iCopy = SendMessage(hwndTB, TB_ADDSTRING, (WPARAM) 0, 
        (LPARAM) (LPSTR) szBuf); 

 
    LoadString(g_hinst, IDS_PASTE, (LPSTR) &szBuf, MAX_LEN); 
    iPaste = SendMessage(hwndTB, TB_ADDSTRING, (WPARAM) 0, 
        (LPARAM) (LPSTR) szBuf); 
 
    // Fill the TBBUTTON array with button information, and add the 
    // buttons to the toolbar. 
    tbb[0].iBitmap = BMP_CUT; 
    tbb[0].idCommand = IDM_CUT; 
    tbb[0].fsState = TBSTATE_ENABLED; 
    tbb[0].fsStyle = TBSTYLE_BUTTON; 
    tbb[0].dwData = 0; 
    tbb[0].iString = iCut; 
 
    tbb[1].iBitmap = BMP_COPY; 

    tbb[1].idCommand = IDM_COPY; 
    tbb[1].fsState = TBSTATE_ENABLED; 
    tbb[1].fsStyle = TBSTYLE_BUTTON; 
    tbb[1].dwData = 0; 
    tbb[1].iString = iCopy; 
 
    tbb[2].iBitmap = BMP_PASTE; 
    tbb[2].idCommand = IDM_PASTE; 
    tbb[2].fsState = TBSTATE_ENABLED; 
    tbb[2].fsStyle = TBSTYLE_BUTTON; 
    tbb[2].dwData = 0; 
    tbb[2].iString = iPaste; 
 
    SendMessage(hwndTB, TB_ADDBUTTONS, (WPARAM) NUM_BUTTONS, 
        (LPARAM) (LPTBBUTTON) &tbb); 

 
    ShowWindow(hwndTB, SW_SHOW); 
    return hwndTB; 
} 


int isprime(int p)
{

return p;
}