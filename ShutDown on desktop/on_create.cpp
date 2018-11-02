
//---------------------------------------------
UINT create_menu(HWND hwnd)
{
HMENU hmenu=CreateMenu();
	if(!hmenu)
	return GetLastError();

HMENU hmenu_popup_file=CreatePopupMenu();
AppendMenu(hmenu,MF_POPUP,(UINT_PTR)hmenu_popup_file,"&File");
AppendMenu(hmenu_popup_file,MF_STRING,100,"&ABCD");
AppendMenu(hmenu_popup_file,MF_STRING,200,"&2");

HMENU hmenu_popup_options=CreatePopupMenu();
AppendMenu(hmenu, MF_POPUP, (UINT_PTR)hmenu_popup_options, "&Options");
AppendMenu(hmenu_popup_options,MF_STRING,300,"&3");
AppendMenu(hmenu_popup_options,MF_STRING,400,"&4");
SetMenu(hwnd,hmenu);
}
//---------------------------------------------
HFONT create_font(HWND hwnd)
{
HFONT hfont;
hfont=CreateFont(14,6,0,0,FW_HEAVY,0,1,0,ANSI_CHARSET, 
      OUT_TT_PRECIS, CLIP_DEFAULT_PRECIS, DEFAULT_QUALITY, 
      DEFAULT_PITCH | FF_DONTCARE,"Tahoma");

SendMessage(hwnd,WM_SETFONT,(UINT)hfont,1);
return hfont;
}
//---------------------------------------------
long __stdcall on_create(HWND hwnd,unsigned int message
					, unsigned int wparam,long lparam)
{
create_menu(hwnd);
HFONT hfont=create_font(hwnd);


HWND hw=0;
int X,Y,W,H;
DWORD style=WS_VISIBLE|WS_CHILD|WS_BORDER;

X=10;Y=30;W=100;H=100;
hw=CreateWindow("button","button",style,X,Y,W,H,hwnd,(HMENU)10,0,0);
SendMessage(hw,WM_SETFONT,(UINT)hfont,0);

X=150;Y=30;W=100;H=100;
hw=CreateWindow("listbox","listbox",style,X,Y,W,H,hwnd,(HMENU)20,0,0);
SendMessage(hw,WM_SETFONT,(UINT)hfont,0);

X=300;Y=30;W=100;H=100;
hw=CreateWindow("combobox","combobox",style,X,Y,W,H,hwnd,(HMENU)30,0,0);
SendMessage(hw,WM_SETFONT,(UINT)hfont,0);


X=450;Y=30;W=100;H=100;
hw=CreateWindow("static","static",style,X,Y,W,H,hwnd,(HMENU)40,0,0);
SendMessage(hw,WM_SETFONT,(UINT)hfont,0);

X=600;Y=30;W=100;H=100;
hw=CreateWindow("edit","edit",style,X,Y,W,H,hwnd,(HMENU)50,0,0);
SendMessage(hw,WM_SETFONT,(UINT)hfont,0);

style=WS_VISIBLE|WS_OVERLAPPEDWINDOW|WS_CLIPCHILDREN;
X=1310;Y=230;W=200;H=100;
//CreateWindow("1223","Main",style,X,Y,W,H,0,0,0,0);

return 0;
}