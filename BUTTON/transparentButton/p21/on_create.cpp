
//---------------------------------------------
UINT create_menu(HWND hwnd)
{
HMENU hmenu=CreateMenu();
	if(!hmenu)
	return GetLastError();

HMENU hmenu_popup_file=CreatePopupMenu();
AppendMenu(hmenu,MF_POPUP,(UINT_PTR)hmenu_popup_file,"&File");
AppendMenu(hmenu_popup_file,MF_STRING,100,"&ABCD");
AppendMenu(hmenu_popup_file,MF_STRING,200,"&RTY");

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
hfont=CreateFont(23,14,1,1,FW_BOLD,1,1,1,ANSI_CHARSET, 
      OUT_TT_PRECIS, CLIP_DEFAULT_PRECIS, DEFAULT_QUALITY, 
      DEFAULT_PITCH | FF_DONTCARE,"Tahoma");
SendMessage(hwnd,WM_SETFONT,(UINT)hfont,0);
return hfont;
}
//---------------------------------------------
long __stdcall on_create(HWND hwnd,unsigned int message
					, unsigned int wparam,long lparam)
{
	
//create_menu(hwnd);
//HFONT hfont=create_font(hwnd);


HWND hw=0;
int X,Y,W,H;
DWORD style=WS_VISIBLE|WS_CHILD|WS_BORDER;

X=10;Y=30;W=30;H=30;
int u=10;
int y=0;
RECT r;

hw=CreateWindow("button","eeee",style|BS_BITMAP,u,Y,W,H,hwnd,(HMENU)10,0,0);
GetWindowRect(hwnd,&r);
	HRGN hrgn;	
	hrgn=CreateEllipticRgn(X+100,Y,W-100,H);

	SetWindowRgn(hw,hrgn,1);





HBITMAP mybut= (HBITMAP)LoadImage(NULL,"C:\\Users\\vakho1\\Desktop\\New Bitmap Image.bmp", IMAGE_BITMAP,30,30, LR_LOADFROMFILE);
int gf=0;
for(int i=0;i<=300;i++)
{
	//hw=CreateWindow("button","eeee",style|i|BS_BITMAP,u,Y,W,H,hwnd,(HMENU)10,0,0);
	u=u+50;
	gf++;


	if(gf==8)
	{
		gf=0;
		u=10;
		Y=Y+100;

	}

 // if(mybut)	{SendMessage(hw, (UINT)BM_SETIMAGE,   (WPARAM)IMAGE_BITMAP, (LPARAM)mybut);}


}
	//269,200,348

return 0;
}