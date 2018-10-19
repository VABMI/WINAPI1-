


long __stdcall on_create(HWND hwnd,unsigned int message	, unsigned int wparam,long lparam,int z)
{		
	static int but;
	
	
	


	int widthButton1=90;
	
	

	bckferi=RGB(40,40,40);
	textlineferi=RGB(40,40,40);
	edittextferi=RGB(210,74,196);


RECT r;
GetClientRect(hwnd,&r);

HWND hw;
HWND Hbutton;
//olde=(WNDPROC)SetWindowLong(childButton,GWL_WNDPROC	,(long)buttonNewPro);


 //olde=(WNDPROC)SetWindowLong(childButton,GWL_WNDPROC,(long)buttonNewPro);

//  olde=(WNDPROC)SetWindowLong(hwnd,GWL_WNDPROC,(long)buttonNewPro);
HANDLE ss;
 ss=LoadLibrary("RICHED32.DLL");


int X,Y,W,H;
X=10,Y=40,W=700,H=500;

	hw=CreateWindow("edit","123456789111213",editstyle,10,30,r.right-10,r.bottom-30.5,hwnd,(HMENU)editc,0,0);

	CreateWindow("SCROLLBAR",0,   WS_VISIBLE | WS_CHILD | SBS_VERT,0,0,20,100,hw,(HMENU)20,0,0);


		SendMessage(hw,EM_LIMITTEXT,999000,0);


	//	hw=CreateWindowEx(MSFTEDIT_CLASS,TEXT("123456789111213"),editstyle,10,30,r.right-10,r.bottom-30.5,hwnd,(HMENU)editc,0,0);


////////////////// region //////







////////////////////////////////////////

/////////// create font ////////////////
/// richedit 
	/*
HFONT hfont=create_font(hwnd);
SendMessage(hw,WM_SETFONT,(UINT)hfont,1);

///SendMessage(hw,EM_SETSEL,4,12);

CHARFORMAT cf={0};
//memset
int sss2=SendMessage(hw,EM_GETCHARFORMAT,0,(LPARAM)&cf);

//cf.cbSize=sizeof (cf);
cf.dwMask=CFM_COLOR;
cf.crTextColor=RGB(255,0,25);
int sss=SendMessage(hw,EM_SETCHARFORMAT,SCF_SELECTION,(LPARAM) &cf);
UINT err=GetLastError();

///richedit class or rich edit32

*/

return 0;
}