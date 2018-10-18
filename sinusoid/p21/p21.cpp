
#include "circle.h"
	float x=0,y=100,n=6.3,r=0.1,k=150,t=0.3;
	HWND hf;

	
	DWORD WINAPI ssd(void *d);

/* forward declarations */
LRESULT CALLBACK WndProc(HWND,UINT,WPARAM,LPARAM);

void main()
{
HWND hwnd=0;
int X,Y,W,H;
ULONG style=0;

WNDCLASS wc,wc1,A2121,wererterytr;
ZeroMemory(&wc,sizeof(WNDCLASS));

wc.style=CS_DBLCLKS;
wc.lpfnWndProc=(WNDPROC)&WndProc;
wc.lpszClassName="12";
wc.hbrBackground=(HBRUSH)CreateSolidBrush(RGB(200,200,200));
wc.hIcon=(HICON)LoadImage(0,"c:\\1.ico",IMAGE_ICON,16,16,LR_LOADFROMFILE);

	if(RegisterClass(&wc)==0)
	{
	MessageBox(hwnd,"RegisterClass error",0,0);
	return;
	}


//mtavari fanjara:
style=WS_VISIBLE|WS_OVERLAPPEDWINDOW|WS_CLIPCHILDREN;
X=100;Y=30;W=1000;H=500;
hwnd=CreateWindow(wc.lpszClassName,"Main",style,X,Y,W,H,0,0,0,0);
//shvili fanjrebi:
style=WS_VISIBLE|WS_CHILD|WS_BORDER;

X=210;Y=30;W=60;H=60;
CreateWindow("button","button",style,X,Y,W,H,hwnd,(HMENU)25,0,0);
X=280;Y=30;W=60;H=20;
CreateWindow("edit","0",style,X,Y,W,H,hwnd,(HMENU)27,0,0);
X=350;Y=30;W=60;H=20;
CreateWindow("edit","300",style,X,Y,W,H,hwnd,(HMENU)28,0,0);
X=420;Y=30;W=60;H=20;
CreateWindow("edit","0",style,X,Y,W,H,hwnd,(HMENU)29,0,0);


MSG msg;
int s=1;
	while(s!=0)
	{
	s=GetMessage(&msg,0,0,0);
	TranslateMessage(&msg);
	DispatchMessage(&msg);
	}
}


/* Window Procedure WndProc */
LRESULT CALLBACK WndProc(HWND hwnd,UINT message,WPARAM wParam,LPARAM lParam)
{






	
	if(message==WM_COMMAND)
	{

			int ctrl_id = LOWORD(wParam);
			if(ctrl_id==25)
			{
			
						
			//	x=0;y=0;
				char jk[60];
				GetWindowText(GetDlgItem(hwnd,27),jk,59);
				x=atof(jk);

				GetWindowText(GetDlgItem(hwnd,28),jk,59);
				y=atof(jk);

				GetWindowText(GetDlgItem(hwnd,29),jk,59);
				t=atof(jk);


				hf=hwnd;
				 CreateThread( NULL, 0, ssd, &hwnd, 0, NULL);

			}

	}














  switch(message)
  {
    
    case WM_PAINT:
       SetWindowHandle(hwnd);
       DrawCircle(100,100,100);



       return 0;    
    case WM_CLOSE: // FAIL THROUGH to call DefWindowProc
       break;
     
    case WM_DESTROY:
       PostQuitMessage(0);
       return 0;
    default:
       break; // FAIL to call DefWindowProc //
  }
  return DefWindowProc(hwnd,message,wParam,lParam);
}




DWORD WINAPI ssd(void *d)
{
///	while(true){
HDC hdc=GetDC(hf);
	SetPixel(hdc,100,99,RGB(92, 0, 230));
	SetPixel(hdc,100,100,RGB(92, 0, 230));
	SetPixel(hdc,100,101,RGB(92, 0, 230));


	SetPixel(hdc,99,99,RGB(92, 0, 230));
	SetPixel(hdc,99,100,RGB(92, 0, 230));
	SetPixel(hdc,99,101,RGB(92, 0, 230));

	SetPixel(hdc,101,99,RGB(92, 0, 230));
	SetPixel(hdc,101,100,RGB(92, 0, 230));
	SetPixel(hdc,101,101,RGB(92, 0, 230));
	float u=100;
	static float tu=0.2;
	static int jh=0;
	static int vv=1;
	for(int g=100;g>=jh;g--)
	{
		 ////////////////// yyyyyy /////////////////////
			SetPixel(hdc,100,g,RGB(92, 0, 230));


			SetPixel(hdc,100,100+(100-g),RGB(92, 0, 230));
		//////////////////////////////////////////////////////
			////////////////// XXXXXXXXXXXXXXX ///////////////

			SetPixel(hdc,g,100,RGB(92, 0, 230));

			SetPixel(hdc,100+(100-g),100,RGB(92, 0, 230));
			///////////////////////////////////////////////
			u+=tu;

			SetPixel(hdc,u,g--,RGB(92, 0, 230));





	}

	//tu=tu+0.2;

	//	Sleep(50);
	tu=tu+0.2;
	//if(tu==2){system("pause");}else if(tu>1){tu=tu+0.2;}else { tu=tu+0.2;  }

		for(float i=0;i<=120;i+=.01)
				{

					y=sin(i);
					y=400+y*100;
					SetPixel(hdc,x,y,RGB(92, 0, 230));

					x=x+r+t;
				

				}
		x=0;y=300;
	//	jh=jh+vv;
	//	vv++;

	//}
		return 0;

}

