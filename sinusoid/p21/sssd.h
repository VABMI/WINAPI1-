
// GraphicsFramework.h

#ifndef GRAPHICSFRAMEWORK_H
#define GRAPHICSFRAMEWORK_H

#include <windows.h>        // must include this for windows programs

// A graphics framework class used to implement a drawing area within a windows control.
// This class implements a back buffer as a memory bitmap which will store pixels and then
// copy them with a call to the Draw method.
class GraphicsFramework {
private:
	HBITMAP     memBMP;					// back buffer
	HDC         memHDC;
	HANDLE oldHDC;
	int wd, ht;
    int wdOver2;
    int htOver2;
    RECT rect;                          // rect for this window
    HWND hwnd;							// handle to the output control

public:
    GraphicsFramework() {
        this->hwnd = NULL;
    }

	// construct from a windows handle
    GraphicsFramework(HWND hwnd) {
		BITMAPINFO bmi;					// bitmap info structure for creating the back buffer
		HDC hdc;

        this->hwnd = hwnd;

		// get the window's client rectangle and its center
		hdc = GetDC(hwnd);
		GetClientRect(hwnd, &rect);
		wd = rect.right - rect.left;
		ht = rect.bottom - rect.top;
		wdOver2 = wd / 2;
		htOver2 = ht / 2;

		// set up a 32bit rgb dib for an offscreen (memory) render target (the back buffer)
		memHDC = CreateCompatibleDC(hdc);
		bmi.bmiHeader.biWidth = wd;
		bmi.bmiHeader.biHeight = ht;
		bmi.bmiHeader.biBitCount = 32;
		bmi.bmiHeader.biCompression = BI_RGB;
		bmi.bmiHeader.biClrUsed = 0;
		bmi.bmiHeader.biClrImportant = 0;
		bmi.bmiHeader.biSize = sizeof(BITMAPINFOHEADER);
		bmi.bmiHeader.biPlanes = 1;
		bmi.bmiHeader.biSizeImage = 0;

		memBMP = CreateDIBSection(memHDC, &bmi, DIB_RGB_COLORS, NULL, NULL, 0);

		// select this memory bmp for drawing
		// note: it will remain selected until destructor is called
		oldHDC = SelectObject(memHDC, memBMP);

		// release the device context
        ReleaseDC(hwnd, hdc);
    }

    ~GraphicsFramework() {
		// deselect and delete the memory dc that was selected in the constructor
		SelectObject(memHDC, oldHDC);		
		DeleteObject(memBMP);
		DeleteDC(memHDC);
    }

	// this method copies the back buffer to the output window
	void Draw()    {
        HDC graphics;                       // graphics device context

		graphics = GetDC(hwnd);

		// copy from offscreen memory bmp to screen
		BitBlt(graphics, 0, 0, wd, ht, memHDC, 0, 0, SRCCOPY);

        // release the device context
        ReleaseDC(hwnd, graphics);
    }

	void DrawPixel(int x, int y, COLORREF color) {
		if (x  < -wdOver2 || x >= wdOver2 ||
            y <= -htOver2 || y >  htOver2) 
        {
            // x and/or y is out of bounds so don't draw

        } else {
            // move origin from upper left to center of buffer
            // draw the pixel 
            SetPixel(memHDC, wdOver2 + x, htOver2 - y, color);
        }
	}

    void ClearScene(COLORREF backColor)
    {
        // clear the offscreen memory bmp to backColor
        HBRUSH hBrush;

		// create the brush with specified color
        hBrush = CreateSolidBrush(backColor);

        // fill the rectangle with black
		rect.left = 0;
		rect.right = wd;
		rect.top = 0;
		rect.bottom = ht;
        FillRect(memHDC, &rect, hBrush);

        // release the brush
        DeleteObject(hBrush);
    }
    
    // add a point to be drawn
    void AddPoint(int x, int y, COLORREF color) {
		SetPixel(memHDC, wdOver2 + x, htOver2 - y, color);
    }

    // draw x and y axis in specifice color, with single dot every tic step
    void AddAxis(COLORREF color, int ticStep) {
        int x, y;

        // draw horizontal line
        for (x = -wdOver2; x <= wdOver2; x++) {
            if (x % ticStep == 0)     {
                //pList.Add(x, 0, color);
		        DrawPixel(x, 0, color);

            }
        }

        // draw vertical line
        for (y = -htOver2; y < htOver2; y++) {
            if (y % ticStep == 0){
                //pList.Add(0, y, color);
				DrawPixel(0, y, color);

            }
        }
    }
};

#endif 




/*
#include <windows.h>
#include <stdio.h>
#include<math.h>
	float x=0,y=100,n=6.3,r=1,k=150,t=1;
	HWND hf;
DWORD WINAPI ssd(void *d)
{
HDC hdc=GetDC(hf);
	
	
		for(float i=0;i<=120;i+=.01)
				{

					y=sin(i);
					y=200+y*100;
					SetPixel(hdc,x,y,RGB(92, 0, 230));

					x=x+r+t;
					Sleep(0.1);

				}
		x=0;y=100;


	
	int c=600;
	int count=0;
	for(int i=0;i<=100;i++)
	{


		
			count++;
	}
			SetPixel(hdc,c,0,RGB(92, 0, 230));
			SetPixel(hdc,c,100,RGB(92, 0, 230));	
		
			SetPixel(hdc,500,50,RGB(92, 0, 230));
			SetPixel(hdc,700,50,RGB(92, 0, 230));	
		return 0;

}
//----------------------------------------------------------------
long __stdcall window_main_function(HWND hwnd,unsigned int message
					, unsigned int wparam,long lparam)
{

//strtok

	float y1=200; float x1=200;
	
	if(message==WM_COMMAND)
	{

			int ctrl_id = LOWORD(wparam);
			if(ctrl_id==25)
			{
			
				 start comment   
				
				bool v=0;
				
				
				
				while(1){


					if(!v)
					{

						y1=y1-5;
					}
					if(v)
					{

						y1=y1+5;
					}


					if(y1<20&&!v)
					{

						v=1;
					}
						if(y1>200&&v)
					{

						v=0;
					}

			x1=x1+2;//sqrt(x1*x1-y1*y1);
				    
					
				 ////////////////////////////////////////
						for(int i=0;i<=2;i++)
						{
							for(int y=0;y<=2;y++)
							{
								SetPixel(hdc,x1-i,y1-y,RGB(25,33,56));
								SetPixel(hdc,x1-i,y1+y,RGB(25,33,56));

								SetPixel(hdc,x1+i,y1-y,RGB(25,33,56));
								SetPixel(hdc,x1+i,y1+y,RGB(25,33,56));
						
							}
			
					

							
						}

					
       ////////////////////////////////
					
	        
				}








			

///////////////////////////////////// end comment ////////////////////

			
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
	else if(message==WM_RBUTTONDOWN)
	{
	
	}
	
	


return DefWindowProc(hwnd,message,wparam,lparam);
}
//----------------------------------------------------------------

void main()
{
HWND hwnd=0;
int X,Y,W,H;
ULONG style=0;

WNDCLASS wc,wc1,A2121,wererterytr;
ZeroMemory(&wc,sizeof(WNDCLASS));

wc.style=CS_DBLCLKS;
wc.lpfnWndProc=(WNDPROC)&window_main_function;
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

X=10;Y=30;W=60;H=60;
CreateWindow("button","button",style,X,Y,W,H,hwnd,(HMENU)25,0,0);
X=80;Y=30;W=60;H=20;
CreateWindow("edit","x",style,X,Y,W,H,hwnd,(HMENU)27,0,0);
X=150;Y=30;W=60;H=20;
CreateWindow("edit","y",style,X,Y,W,H,hwnd,(HMENU)28,0,0);
X=220;Y=30;W=60;H=20;
CreateWindow("edit","t",style,X,Y,W,H,hwnd,(HMENU)29,0,0);


MSG msg;
int s=1;
	while(s!=0)
	{
	s=GetMessage(&msg,0,0,0);
	TranslateMessage(&msg);
	DispatchMessage(&msg);
	}
}*/