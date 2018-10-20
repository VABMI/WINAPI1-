


//---------------------------------------------
UINT create_menu(HWND hwnd)
{
HMENU hmenu=CreateMenu();
	if(!hmenu)
	return GetLastError();
	/////////////// text align /////////


	/*
	menu


	CheckMenuItem(
    __in HMENU hMenu,
    __in UINT uIDCheckItem,
    __in UINT uCheck);
	*/
	  HMENU submenu=CreateMenu();
  AppendMenu(submenu,MF_STRING,5010,"&Center");
  AppendMenu(submenu,MF_STRING,5011,"&Right");
  AppendMenu(submenu,MF_STRING,5012,"&Left");



//////////////////// File /////
HMENU hmenu_popup_newfile=CreatePopupMenu();
AppendMenu(hmenu,MF_POPUP,(UINT_PTR)hmenu_popup_newfile,"&file");
AppendMenu(hmenu_popup_newfile,MF_STRING,5001,"&New File");
AppendMenu(hmenu_popup_newfile,MF_STRING,5002,"&Open File");
  AppendMenu( hmenu_popup_newfile,MF_SEPARATOR,0,0);
AppendMenu(hmenu_popup_newfile,MF_STRING,5003,"&Save");
 //AppendMenu(hmenu_popup_newfile,MF_STRING,5004,"&Save");
  AppendMenu( hmenu_popup_newfile,MF_SEPARATOR,0,0);
  AppendMenu(hmenu_popup_newfile,MF_STRING,5005,"&exit");



///////////////////////////// edittttt ///////////////
HMENU hmenu_popup_newstyle=CreatePopupMenu();
AppendMenu(hmenu,MF_POPUP,(UINT_PTR)hmenu_popup_newstyle,"&Edit");
AppendMenu(hmenu_popup_newstyle,MF_STRING,5006,"&font style");
AppendMenu(hmenu_popup_newstyle,MF_STRING,5007,"&Text color");
AppendMenu(hmenu_popup_newstyle,MF_STRING,5008,"&background color");
AppendMenu(hmenu_popup_newstyle,MF_STRING,5009,"&line color");
  AppendMenu(hmenu_popup_newstyle,MF_SEPARATOR,0,0);



AppendMenu(hmenu_popup_newstyle,MF_POPUP,(UINT_PTR)submenu,"&Text align");



//////////////////////////////////////////////////////

HMENU hmenu_popup_file=CreatePopupMenu();
AppendMenu(hmenu,MF_POPUP,(UINT_PTR)hmenu_popup_file,"&Common Dialogs");
AppendMenu(hmenu_popup_file,MF_STRING,ID_MENU_COLOR,"&Color");
	//	AppendMenu(hmenu_popup_file,MF_STRING,ID_MENU_FONT,"&Font");
AppendMenu(hmenu_popup_file,MF_STRING,ID_MENU_FINDTEXT,"&Find Text");
AppendMenu(hmenu_popup_file,MF_STRING,ID_MENU_REPLACETEXT,"&Finde Replace Text");

HMENU hmenu_popup_ToolBar=CreatePopupMenu();

AppendMenu(hmenu,MF_POPUP,(UINT_PTR)hmenu_popup_ToolBar,"&Tool Bar");

AppendMenu(hmenu_popup_ToolBar,MF_STRING,MENU_SAVE,"&Save");
AppendMenu(hmenu_popup_ToolBar,MF_STRING,MENU_OPEN,"&Open");
AppendMenu(hmenu_popup_ToolBar,MF_STRING,MENU_SERCH,"&Search");
AppendMenu(hmenu_popup_ToolBar,MF_STRING,MENU_PRINT,"&Print");
AppendMenu(hmenu_popup_ToolBar,MF_STRING, MENU_NEXT_PREV,"&Prev Next");
AppendMenu(hmenu_popup_ToolBar,MF_STRING,MENU_HELP,"&Help");



HMENU hmenu_popup_options=CreatePopupMenu();
AppendMenu(hmenu, MF_POPUP, (UINT_PTR)hmenu_popup_options, "&Options");

AppendMenu(hmenu_popup_options,MF_STRING,ID_MENU_ABOUT,"&About");

HMENU hmenu_popup_help=CreatePopupMenu();
AppendMenu(hmenu_popup_options, MF_POPUP, (UINT_PTR)hmenu_popup_help, "&Help");
AppendMenu(hmenu_popup_help,MF_STRING,ID_MENU_C_CPP,"&c,c++");
AppendMenu(hmenu_popup_help,MF_STRING,ID_MENU_WINAPI,"&Winapi");
AppendMenu(hmenu_popup_help,MF_STRING,ID_MENU_ALL,"&All");





HMENU hmenu_popup_algorithms=CreatePopupMenu();
AppendMenu(hmenu,MF_POPUP,(UINT_PTR)hmenu_popup_algorithms,"&Algorithms");

AppendMenu(hmenu_popup_algorithms,MF_STRING,ID_MENU_PRIMES,"&Calculator");
//AppendMenu(hmenu_popup_regions,MF_STRING,ID_MENU_REGION_ROUNRECT,"&Round Rect");


SetMenu(hwnd,hmenu);
}
//---------------------------------------------
HFONT create_font(HWND hwnd)
{
HFONT hfont;
hfont=CreateFont(20,5,0,0,FW_BOLD,0,1,0,ANSI_CHARSET, 
      OUT_TT_PRECIS, CLIP_DEFAULT_PRECIS, DEFAULT_QUALITY, 
      DEFAULT_PITCH | FF_DONTCARE,"Tahoma");

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
	//cf.lpLogFont->lfHeight*=-1; hfontPrev = (HFONT)SelectObject(hdc,hfont);
	
	
//	HWND hwn=GetDlgItem(hwnd,50000);
	//SendMessage(hwn,WM_SETFONT,(UINT)hfont,1);

//	rgbPrev = SetTextColor(hdc,cf.rgbColors);

//	SetBkMode(hdc,1);
	//SetTextColor(hdc,cf.rgbColors);


	//InvalidateRect(hwnd,0,1);
	//SendMessage(hwnd,WM_PAINT,0,0);
	//InvalidateRect(hwnd,0,0);

	//TextOut(hdc,0,220,"1523126351237",12);

	//InvalidateRect(hwnd,0,1);
	//SendMessage(hwnd,WM_PAINT,0,1);
	//InvalidateRect(hwnd,0,0);
	return hfont;
	}else hfontPrev;


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
FINDREPLACEW fr; ///// with finde
FINDREPLACEA fns; //// with replace
//---------------------------------------------
int find_text(HWND hwnd)
{



	HWND edit=GetDlgItem(hwnd,editc);
    
// common dialog box structure           // owner window

   // handle of Find dialog box
 
// Initialize FINDREPLACE
		ZeroMemory(&fr, sizeof(fr));

	fr.lStructSize = sizeof(fr);
	fr.hwndOwner = hwnd;
	fr.lpstrFindWhat =szFindWhat;
	fr.wFindWhatLen =6;
	//fr.hInstance=0;
	//fr.lCustData=0;
	//fr.lpfnHook=0;
	
	fr.Flags=FR_FINDNEXT;//= FR_FINDNEXT|FR_REPLACE;
	// fr.wReplaceWithLen;
  
  if(!FindTextW(&fr))
	{
	  MessageBox(hwnd,"FindeText ERROR","FindeText ERROR",0);
	  //return GetLastError();
  }
	//MessageBox(hwnd,(LPCSTR)fr.lpstrFindWhat,"asda",0);
 
 return 0;
}




int replace_text(HWND hwnd)
{
	ZeroMemory(&fns,sizeof(fns));
	fns.lStructSize=sizeof(fns);
	fns.hwndOwner=hwnd;
	fns.lpstrReplaceWith=ReplaceWith;
	fns.wReplaceWithLen=strlen("aaaaaaaasd");
	fns.lpstrFindWhat=szFindWhat2;
	fns.wFindWhatLen=strlen("FindeWhaaat");
	fns.Flags=FR_REPLACE|FR_REPLACE;

	if(!ReplaceText(&fns))
	{

		MessageBox(hwnd,"Error Replace Text","Error Replace Text",0);

	}








	return 0;
}







 int open(HWND hwnd)
{


	OPENFILENAME ofn={0};       // common dialog box structure
	char szFile[260];       // buffer for file name
         // owner window
	HANDLE hf;              // file handle
	
	// Initialize OPENFILENAME
	//ZeroMemory(&amp.ofn, sizeof(ofn));
	ofn.lStructSize = sizeof(ofn);
	ofn.hwndOwner = hwnd;
	ofn.lpstrFile = szFile;
	
	ofn.lpstrFile[0] = '\0';
	ofn.nMaxFile = sizeof(szFile);
	ofn.lpstrFilter = "All\0*.*\0Text\0*.TXT\0";
	ofn.nFilterIndex = 1;
	ofn.lpstrFileTitle = NULL;
	ofn.nMaxFileTitle = 259;
	ofn.lpstrInitialDir = NULL;
//	ofn.Flags = OFN_PATHMUSTEXIST | OFN_FILEMUSTEXIST;
	
	// Display the Open dialog box. 
	
	if (GetOpenFileName(&ofn)==TRUE) 
	{
				char g[200];	
				int co=0;
				sprintf(szFile,"%s",ofn.lpstrFile);

			 	 for(int i=0;i<=strlen(szFile);i++)
				 {

				//	if(((int)(szFile[i]))==47)
					if(((int)szFile[i])==92)
					{
					path[co]=szFile[i];
					
					co++;
					path[co]=szFile[i];

					}
					else
					{
						path[co]=szFile[i];

					}



			  
				co++;


				 }

	return 1;
		}	

	else return 0;
 }
	 
	 
int save(HWND hwnd)
{
	  	
		OPENFILENAME ofn={0};       // common dialog box structure
		char szFile[260];       // buffer for file name
         // owner window
		HANDLE hf;              // file handle
		
		// Initialize OPENFILENAME
		//ZeroMemory(&amp.ofn, sizeof(ofn));
		ofn.lStructSize = sizeof(ofn);
		ofn.hwndOwner = hwnd;
		ofn.lpstrFile = szFile;
		
		ofn.lpstrFile[0] = '\0';
		ofn.nMaxFile = sizeof(szFile);
		ofn.lpstrFilter = "All\0*.*\0Text\0*.TXT\0";
		ofn.nFilterIndex = 1;
		ofn.lpstrFileTitle = NULL;
		ofn.nMaxFileTitle = 100;
		ofn.lpstrInitialDir = NULL;
		//ofn.Flags = OFN_PATHMUSTEXIST | OFN_FILEMUSTEXIST;
		
		// Display the Open dialog box. 
		
		if (GetSaveFileName(&ofn)==TRUE) 
		{ 
		char g[200];	
		int co=0;
				sprintf(szFile,"%s",ofn.lpstrFile);
			 	 for(int i=0;i<=strlen(szFile);i++)
				 {

				//	if(((int)(szFile[i]))==47)
					if(((int)szFile[i])==92)
					{
					path[co]=szFile[i];
					
					co++;
					path[co]=szFile[i];

					}
					else
					{
						path[co]=szFile[i];

					}



			  
				co++;


				 }

		return 1;
		}	else return 0;
	//	return path;
		
			
}
	 
	 
