
#include "globals.h"

	HWND editboxia1,editboxia2,hwndglob;
void login(HWND hwnd)
{ HWND static1;

	RECT r;
	GetClientRect(hwnd,&r);
	

	//	HWND hWndGroupBox = CreateWindow("Button", "ffffff????", WS_CHILD | WS_VISIBLE | BS_GROUPBOX,60, 100, 400, 300,hwnd,(HMENU) 10, 0, 0);

	CreateWindow("edit","",WS_VISIBLE|WS_CHILD| WS_CLIPSIBLINGS ,r.right/2-125,r.bottom/2-50,250,30,hwnd,(HMENU)1,0,0);
	CreateWindow("edit","",WS_VISIBLE|WS_CHILD,r.right/2-125,r.bottom/2,250,30,hwnd,(HMENU)2,0,0);

HWND bMainOK=CreateWindow("button","click",WS_VISIBLE|WS_CHILD,r.right/2-125,r.bottom/2+50,100,30,hwnd,(HMENU)3,(HINSTANCE)GetWindowLong(hwnd, GWL_HINSTANCE),0);






	CreateWindow("button","REGISTRACION",WS_VISIBLE|WS_CHILD,r.right/2,r.bottom/2+50,125,30,hwnd,(HMENU)5,0,0);


	static1=	CreateWindow("static","",WS_VISIBLE|WS_CHILD,r.right/2-50,r.bottom/2-155,100,100,hwnd,(HMENU)4,0,0);

	  	HDC hdc=GetDC(static1);
		RECT s;
		GetClientRect(static1,&s);
		HRGN hrgn;

		hrgn=CreateEllipticRgn(s.left,0,s.right,s.bottom);
	//	SetWindowRgn(static1,hrgn,1);
	 //	InvalidateRect(hwnd,0,1);
	//	SendMessage(static1,WM_SIZE,0,0);
			//	MoveWindow(static1,r.right/2-50,r.bottom/2-155,100,100,1);
}

void reg(HWND hwnd)
{	 int y=30;
	HDC hdc1 =GetDC(hwnd);
	CreateWindow("button","<-",WS_VISIBLE|WS_CHILD|WS_BORDER,10,5,30,20,hwnd,(HMENU)55,0,0);
//	CreateWindow("static","registracia",WS_VISIBLE|WS_CHILD|WS_BORDER,50,10,210,30,hwnd,(HMENU)111,0,0);

	

	CreateWindow("edit","saxeli",WS_VISIBLE|WS_CHILD|WS_BORDER,10,y,240,25,hwnd,(HMENU)111,0,0);
	y=y+40;
	CreateWindow("edit","gvari",WS_VISIBLE|WS_CHILD|WS_BORDER,10,y,240,25,hwnd,(HMENU)112,0,0);
	y=y+40;
	CreateWindow("edit","User Name",WS_VISIBLE|WS_CHILD|WS_BORDER,10,y,240,25,hwnd,(HMENU)113,0,0);
	y=y+40;
	CreateWindow("ComboBox","wwwww",WS_VISIBLE|WS_CHILD|WS_BORDER|CBS_DROPDOWN | CBS_HASSTRINGS | CBS_SIMPLE |WS_VSCROLL|CBS_HASSTRINGS ,10,y,130,100,hwnd,(HMENU)114,0,0);
	y=y+40;
	CreateWindow("edit","+995",WS_VISIBLE|WS_CHILD|WS_BORDER|ES_READONLY,10,y,75,25,hwnd,(HMENU)115,0,0);
	CreateWindow("edit","Telefoni",WS_VISIBLE|WS_CHILD|WS_BORDER,90,y,140,25,hwnd,(HMENU)116,0,0);
	y=y+40;
	CreateWindow("edit","Email",WS_VISIBLE|WS_CHILD|WS_BORDER,10,y,240,25,hwnd,(HMENU)117,0,0);
	y=y+40;
	CreateWindow("edit","password",WS_VISIBLE|WS_CHILD|WS_BORDER,10,y,240,25,hwnd,(HMENU)118,0,0);
	y=y+40;
	CreateWindow("edit","password",WS_VISIBLE|WS_CHILD|WS_BORDER,10,y,240,25,hwnd,(HMENU)119,0,0);

	char dfd[100];
	y=y+40;
	HWND hh=CreateWindow("ComboBox","wwwww",WS_VISIBLE|WS_CHILD|WS_BORDER|CBS_DROPDOWN | CBS_HASSTRINGS | CBS_SIMPLE |WS_VSCROLL|CBS_HASSTRINGS ,10,y,60,300,hwnd,(HMENU)120,0,0);
	
	for(int i=1940;i<2018;i++)
	{
	sprintf(dfd,"%i",i);
	SendMessage(hh,CB_ADDSTRING,0,(LPARAM)dfd);
	}
	
	
	
	HWND hhh=CreateWindow("ComboBox","wwwww",WS_VISIBLE|WS_CHILD|WS_BORDER|CBS_DROPDOWN | CBS_HASSTRINGS | CBS_SIMPLE |WS_VSCROLL,10+40+30,y,60,300,hwnd,(HMENU)121,0,0);


	for(int i=1;i<=12;i++)
	{
	sprintf(dfd,"%i",i);
	SendMessage(hhh,CB_ADDSTRING,0,(LPARAM)dfd);
	}


	HWND h=CreateWindow("ComboBox","wwwww",WS_VISIBLE|WS_CHILD|WS_BORDER|CBS_DROPDOWN | CBS_HASSTRINGS | CBS_SIMPLE |WS_VSCROLL,10+40+40+60+10,y,60,300,hwnd,(HMENU)122,0,0);

	for(int i=1;i<=31;i++)
	{
	sprintf(dfd,"%i",i);
	SendMessage(h,CB_ADDSTRING,0,(LPARAM)dfd);
	}

	y=y+40;
	HWND mamr=CreateWindow("Button","mamr",WS_VISIBLE|WS_CHILD|WS_BORDER|BS_AUTORADIOBUTTON|BS_BITMAP ,10,y,70,30,hwnd,(HMENU)123,0,0);
		HBITMAP hsea=(HBITMAP)LoadImage(0,"C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\Shoes-2-2-icon.bmp",IMAGE_BITMAP,40,60,LR_LOADFROMFILE);
	if(hsea){
	SendMessage(mamr,BM_SETIMAGE,IMAGE_BITMAP,(LPARAM)hsea);
	}


	HWND mdedr=CreateWindow("Button","mded",WS_VISIBLE|WS_CHILD|WS_BORDER|BS_AUTORADIOBUTTON|BS_BITMAP	,10+40+40+30,y,60,30,hwnd,(HMENU)124,0,0);
	
	HBITMAP hsa=(HBITMAP)LoadImage(0,"C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\shoe-woman-icon-61449[1].bmp",IMAGE_BITMAP,30,30,LR_LOADFROMFILE);
	if(hsa){
	SendMessage(mdedr,BM_SETIMAGE,IMAGE_BITMAP,(LPARAM)hsa);
	}
	y=y+40;
	
	CreateWindow("button","IMAGE",WS_VISIBLE|WS_CHILD|WS_BORDER,10,y,100,30,hwnd,(HMENU)125,0,0);
	CreateWindow("button","SAVE",WS_VISIBLE|WS_CHILD|WS_BORDER,120,y,100,30,hwnd,(HMENU)126,0,0);



	int x=260,y1=30,width=3,height=425;
	CreateWindow("static","LINE",WS_VISIBLE|WS_CHILD|0x00000106FL,x,y1,width,height,hwnd,(HMENU)127,0,0);




	//HWND photo=	CreateWindow("static","LINE22",WS_VISIBLE|WS_CHILD|SS_BITMAP,272,y1,300,300,hwnd,(HMENU)128,0,0);
		HBITMAP bitm=(HBITMAP)LoadImage(0,"C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\gialurji.bmp",IMAGE_BITMAP,300,300,LR_LOADFROMFILE);
	if(bitm)
	{
//	SendMessage(photo,STM_SETIMAGE,IMAGE_BITMAP,(LPARAM)bitm);
	}




















	//////////////////////////////////////////////////////////













	HWND reg1=GetDlgItem(hwnd,55);

		   ShowWindow(reg1,SW_HIDE);
	for(int i=111;i<=128;i++)
		{
			  HWND reg=GetDlgItem(hwnd,i);

			   ShowWindow(reg,SW_HIDE);
		}



}

void page(HWND hwnd)
{
	RECT r;

	GetClientRect(hwnd,&r);

	CreateWindow("static","LINE",WS_VISIBLE|WS_CHILD|0x20000109FL,1,70,r.right,3,hwnd,(HMENU)1231,0,0);

	CreateWindow("static","LINE",WS_VISIBLE|WS_CHILD|0x20000109FL,250,70,3,r.bottom,hwnd,(HMENU)1232,0,0);


			CreateWindow("Button","LogOut",WS_VISIBLE|WS_CHILD,r.right-80,0,70,70,hwnd,(HMENU)LOGOUT,0,0);
			 CreateWindow("Button","VIDEO",WS_VISIBLE|WS_CHILD,r.right-160,0,70,70,hwnd,(HMENU)1234,0,0);
			 CreateWindow("Button","SMS",WS_VISIBLE|WS_CHILD,r.right-(160+80),0,70,70,hwnd,(HMENU)1235,0,0);
			 CreateWindow("Button","PHOTO",WS_VISIBLE|WS_CHILD,r.right-(160+80+80),0,70,70,hwnd,(HMENU)1236,0,0);
		
			 	 CreateWindow("Static","PHOTO",WS_VISIBLE|WS_CHILD,10,0,70,70,hwnd,(HMENU)1237,0,0);

				  CreateWindow("Edit","joe cocker",WS_VISIBLE|WS_CHILD,90,5,150,30,hwnd,(HMENU)1238,0,0);

				 //////////////////////////////// smsmmmsmsmsmsm//////////////////////////////////////////
				 int g=35;
				 CreateWindow("Edit","PHOTO",WS_VISIBLE|WS_CHILD,260,75,r.right-(230+305),r.bottom-250,hwnd,(HMENU)1239,0,0);
				 CreateWindow("Edit","PHOTO",WS_VISIBLE|WS_CHILD,260,r.bottom-130,r.right-(230+395+g),50,hwnd,(HMENU)12310,0,0);

				 CreateWindow("button","SEND",WS_VISIBLE|WS_CHILD,r.right-(230+385-250+g),r.bottom-130,50,50,hwnd,(HMENU)12311,0,0);

				 CreateWindow("button","SMILES",WS_VISIBLE|WS_CHILD,r.right-(230+385-250-55+g),r.bottom-130,70,50,hwnd,(HMENU)12312,0,0);






				  
									for(int i=1231;i<=12312;i++)
									{

										ShowWindow(GetDlgItem(hwnd,i),SW_HIDE);

									}











}

void MessangerForm(HWND hwnd1)
{
	HWND hw1,hw2,hw3;
//create_menu(hwnd);
	///	HFONT hfont=create_font(hwnd);


int X,Y,W,H;
DWORD style=WS_VISIBLE|WS_CHILD|WS_BORDER;

	//CreateWindow("Edit","",WS_CHILD|WS_VISIBLE|WS_BORDER,0,30,280,500,hwnd1,(HMENU)904,0,0);


X=300;Y=30;W=600;H=500;

//hw1=CreateWindow("edit","edit",style|ES_MULTILINE|WS_VSCROLL,X,Y,W,H,hwnd1,(HMENU)901,0,0);
X=300;Y=535;W=250;H=50;
//hw2=CreateWindow("edit","edit",style|WS_HSCROLL|ES_MULTILINE,X,Y,W,H,hwnd1,(HMENU)902,0,0);
X=550;Y=535;W=50;H=50;
//hw3=CreateWindow("Button","butt",style,X,Y,W,H,hwnd1,(HMENU)903,0,0);




	


//	SendMessage(hw,WM_SETFONT,(UINT)hfont,0);
/*
  ShowWindow(hw1,SW_HIDE);
    ShowWindow(hw2,SW_HIDE);
	  ShowWindow(hw3,SW_HIDE);
	  */

}
HWND DoCreateStatusBar(HWND hwndParent, int idStatus, HINSTANCE hinst, int cParts)
{
	static HWND hwndStatus={0};
    RECT rcClient;
    HLOCAL hloc;
    PINT paParts;
    int i, nWidth;

    // Ensure that the common control DLL is loaded.
    InitCommonControls();
	DestroyWindow(hwndStatus);
    // Create the status bar.
    hwndStatus = CreateWindowEx(
        0,                       // no extended styles
        STATUSCLASSNAME,         // name of status bar class
        (PCTSTR) NULL,           // no text when first created
        SBARS_SIZEGRIP |         // includes a sizing grip
        WS_CHILD | WS_VISIBLE,   // creates a visible child window
        0, 25, 0, 0,              // ignores size and position
        hwndParent,              // handle to parent window
        (HMENU) idStatus,       // child window identifier
        hinst,                   // handle to application instance
        NULL);                   // no window creation data
	
    // Get the coordinates of the parent window's client area.
    GetClientRect(hwndParent, &rcClient);

    // Allocate an array for holding the right edge coordinates.
    hloc = LocalAlloc(LHND, sizeof(int) * cParts);
    paParts = (PINT) LocalLock(hloc);

    // Calculate the right edge coordinate for each part, and
    // copy the coordinates to the array.
    nWidth = rcClient.right / cParts;
    int rightEdge = nWidth;
    for (i = 0; i < cParts; i++) { 
       paParts[i] = rightEdge;
       rightEdge += nWidth;
    }

    // Tell the status bar to create the window parts.
    SendMessage(hwndStatus, SB_SETPARTS, (WPARAM) cParts, (LPARAM)
               paParts);

    // Free the array, and return.
    LocalUnlock(hloc);
    LocalFree(hloc);
    return hwndStatus;
}  

