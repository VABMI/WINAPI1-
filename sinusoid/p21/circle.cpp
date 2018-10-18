#include <windows.h>
#include "circle.h"

/* set window handle */
static HWND sHwnd;
void SetWindowHandle(HWND hwnd)
{
  sHwnd=hwnd; 
}

static COLORREF sDefaultColor=RGB(255,0,0);

/* SetPixel */
void Circle_SetPixel(int x,int y,COLORREF& color=sDefaultColor)
{
  if(sHwnd==NULL){
    MessageBox(NULL,"sHwnd was not initialized !","Error",MB_OK|MB_ICONERROR);
    exit(0);
  }

  HDC hdc=GetDC(sHwnd);
    SetPixel(hdc,x,y,color);
  ReleaseDC(sHwnd,hdc);
  return;
  // NEVERREACH //
}

#define PLOT_ALL_EIGHT(x,y,x1,y1)  { Circle_SetPixel(x+x1,y+y1);Circle_SetPixel(y+x1,x+y1);\
                                    Circle_SetPixel(-x+x1,y+y1);Circle_SetPixel(-y+x1,x+y1);\
                                    Circle_SetPixel(-x+x1,-y+y1);Circle_SetPixel(-y+x1,-x+y1);\
                                    Circle_SetPixel(x+x1,-y+y1);Circle_SetPixel(y+x1,-x+y1);}


/* DrawCircle */
void DrawCircle(int x,int y,int radius)
{
  int yk =radius;
  int xk=0;
  double pk=5.0/4-radius;   // you may wonder why I used the double floating point arithmetic here.
                            // the answer is in modern computing architecture encourages the use of floating 
                            // point math.

  while(yk>=xk)
  {
    PLOT_ALL_EIGHT(xk,yk,x,y)
    if(pk>=0)
    {
      pk+=2*xk -2*yk +5;
      xk=xk+1;
      yk=yk-1;
    }else
    {
      pk+=2*xk+3;
      xk=xk+1;
      yk=yk;
    }    
  }  
}