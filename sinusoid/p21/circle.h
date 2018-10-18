//
// circle.h
//  defines the functions to draw circle.
//
#include <stdio.h>
#include<math.h>
#include <windows.h>
#include <iostream>

using namespace std;

#ifndef CIRCLE_H
#define CIRCLE_H



typedef struct tagCircle
{
  int x;
  int y;
  int radius;
} Circle;


/* set the window handle */
void SetWindowHandle(HWND);

/* set pixel 
  @parms int x xposition
  @parms int y yposition
  @parms Color color of the pixel.
  @returns void
*/
void Circle_SetPixel(int,int,COLORREF&);


/* circle dawing function
   
   @parms int x x position of the circle to be drawn
   @parms int y center y position.
   @parms int radius radius of the circle.

 */


void DrawCircle(int,int,int);

#endif // END CIRCLE_H