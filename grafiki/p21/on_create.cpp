//---------------------------------------------
long __stdcall on_create(HWND hwnd,unsigned int message
					, unsigned int wparam,long lparam)
{
HWND hw=0,statiki,statiki1,statiki2,statiki3,statiki4,statiki5,statiki6,statiki7,statiki8,statiki9,statiki10,statiki11;
int X,Y,W,H,PLUS=90;
int H1,H2,H3,H4,H5,H6,H7,H8,H9,H10,H11,H12,H13;
int Y1,Y2,Y3,Y4,Y5,Y6,Y7,Y8,Y9,Y10,Y11,Y12;
 H1=H2=H3=H4=H5=H6=H7=H8=H9=H10=H11=H12=H13=270;
 Y1=Y2=Y3=Y4=Y5=Y6=Y7=Y8=Y9=Y10=Y11=Y12=330;
 H1=240;
 Y1=360;


DWORD style=WS_VISIBLE|WS_CHILD;



X=200;Y=330;W=30;H=270;
/////////////////////////////////////////////////////////////////////
//Desk=CreateWindow("static","desk",style|SS_BITMAP,3,30,1200,1200,hwnd,(HMENU)40,0,0);

HBITMAP 	mybitmap= (HBITMAP)LoadImage(NULL,"C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\mountain.BMP", IMAGE_BITMAP,1300,600, LR_LOADFROMFILE);
	//SendMessage(Desk,STM_SETIMAGE,   (WPARAM)IMAGE_BITMAP, (LPARAM)mybitmap);

///////////////////////////////////////////////////////////


HWND style1=CreateWindow("static","static",style|SS_BITMAP,200,300,100,3,hwnd,(HMENU)40,0,0);
 HWND style2=CreateWindow("static","static",style|SS_BITMAP,200,600,100,3,hwnd,(HMENU)40,0,0);
  HWND style3=CreateWindow("static","static",style|SS_BITMAP,200,450,100,3,hwnd,(HMENU)40,0,0);
 //312.5

  ///////////////////////////     ////////////////////////////////////////////
  //H-- da y++ 
statiki1=CreateWindow("static","1",style,X,Y1,W,H1,hwnd,(HMENU)61,0,0);


statiki2=CreateWindow("static","2",style,X=X+PLUS,Y2,W,H2,hwnd,(HMENU)62,0,0);

statiki3=CreateWindow("static","3",style,X=X+PLUS,Y3,W,H3,hwnd,(HMENU)63,0,0);

statiki4=CreateWindow("static","4",style,X=X+PLUS,Y4,W,H4,hwnd,(HMENU)64,0,0);

statiki5=CreateWindow("static","5",style,X=X+PLUS,Y5,W,H5,hwnd,(HMENU)65,0,0);

statiki6=CreateWindow("static","6",style,X=X+PLUS,Y6,W,H6,hwnd,(HMENU)66,0,0);

statiki7=CreateWindow("static","7",style,X=X+PLUS,Y7,W,H7,hwnd,(HMENU)67,0,0);

statiki8=CreateWindow("static","8",style,X=X+PLUS,Y8,W,H8,hwnd,(HMENU)68,0,0);

statiki9=CreateWindow("static","9",style,X=X+PLUS,Y9,W,H9,hwnd,(HMENU)69,0,0);

statiki10=CreateWindow("static","10",style,X=X+PLUS,Y10,W,H10,hwnd,(HMENU)70,0,0);

statiki11=CreateWindow("static","11",style,X=X+PLUS,Y11,W,H11,hwnd,(HMENU)71,0,0);
	
	
	//CreateWindow("static","static",style,X=X+PLUS,Y12,W,H12,hw,(HMENU)4,0,0);



	///////////////////////////////////////////////////////////////////////
  HBITMAP	witeli= (HBITMAP)LoadImage(NULL,"C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\witeli.BMP", IMAGE_BITMAP,950,5, LR_LOADFROMFILE);
    HBITMAP	mwvane= (HBITMAP)LoadImage(NULL,"C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\mwvane.BMP", IMAGE_BITMAP,950,5, LR_LOADFROMFILE);
	  HBITMAP	yviteli= (HBITMAP)LoadImage(NULL,"C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\yviteli.BMP", IMAGE_BITMAP,950,5, LR_LOADFROMFILE);
  if(!witeli&&!yviteli&&!mwvane){
	  	MessageBox(hwnd,"asdasd","Asdasd",1);
  }
  SendMessage(style1,STM_SETIMAGE,   (WPARAM)IMAGE_BITMAP, (LPARAM)witeli); //witeli
    SendMessage(style2,STM_SETIMAGE,   (WPARAM)IMAGE_BITMAP, (LPARAM)mwvane); ///mwvane
	    SendMessage(style3,STM_SETIMAGE,   (WPARAM)IMAGE_BITMAP, (LPARAM)yviteli); // yviteli
	return 0;
}



long __stdcall on_create2(HWND hwnd,unsigned int message, unsigned int wparam,long lparam)
{
HWND editi1,editi2,editi3,editi4,editi5,editi6,editi7,editi8,editi9,editi10,editi11;
int X,Y,W,H,PLUS=50;
DWORD style=WS_VISIBLE|WS_CHILD|WS_BORDER;



X=10;Y=0;W=90;H=20;

editi1=CreateWindow("Edit","1",style,X,Y=Y+PLUS,W,H,hwnd,(HMENU)401,0,0);
editi2=CreateWindow("Edit","2",style,X,Y=Y+PLUS,W,H,hwnd,(HMENU)402,0,0);
editi3=CreateWindow("Edit","3",style,X,Y=Y+PLUS,W,H,hwnd,(HMENU)403,0,0);
editi4=CreateWindow("Edit","4",style,X,Y=Y+PLUS,W,H,hwnd,(HMENU)404,0,0);
editi5=CreateWindow("Edit","5",style,X,Y=Y+PLUS,W,H,hwnd,(HMENU)405,0,0);
editi6=CreateWindow("Edit","6",style,X,Y=Y+PLUS,W,H,hwnd,(HMENU)406,0,0);
editi7=CreateWindow("Edit","7",style,X,Y=Y+PLUS,W,H,hwnd,(HMENU)407,0,0);
editi8=CreateWindow("Edit","8",style,X,Y=Y+PLUS,W,H,hwnd,(HMENU)408,0,0);
editi9=CreateWindow("Edit","9",style,X,Y=Y+PLUS,W,H,hwnd,(HMENU)409,0,0);
editi10=CreateWindow("Edit","10",style,X,Y=Y+PLUS,W,H,hwnd,(HMENU)410,0,0);
editi11=CreateWindow("Edit","11",style,X,Y=Y+PLUS,W,H,hwnd,(HMENU)411,0,0);


HDC hdc=GetDC(editi1);

		int x=1,y=1;
		SetPixel(hdc,x,y-1,RGB(250,0,0));
		SetPixel(hdc,x-1,y-1,RGB(250,0,0));
		SetPixel(hdc,x-1,y,RGB(250,0,0));
		SetPixel(hdc,x,y,RGB(250,0,0));
		SetPixel(hdc,x+1,y,RGB(250,0,0));
		SetPixel(hdc,x+1,y+1,RGB(250,0,0));


CreateWindow("button","diagram1",style,X=X,Y+PLUS,W,H,hwnd,(HMENU)3,0,0);


return 0;
}