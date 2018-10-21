
// Copyright (c) 2002 Andreas Jönsson, www.AngelCode.com

// Don't forget to link with winhook.lib


/************************************************
Modified by Chris Grams to eliminate the need for
windows message hooks

instead of installing message hooks, what I have done here is
used the RedrawWindow function to update the parts of the desktop
the need updating.

this erases everything inside the rect rc and the RDW_UPDATENOW flag
tells RedrawWindow that the window should be redrawn immediately
RedrawWindow( hDesktopWnd, &rc, NULL, RDW_INVALIDATE | RDW_ERASE | RDW_UPDATENOW );

this just draws everything thats over top of the desktop background.
RDW_NOERASE tells RedrawWindow not to have the desktop erase the background
which essentially does everything that the windows hooks did, only without
the windows hooks.
RedrawWindow( hDesktopWnd, &rc, NULL, RDW_NOERASE | RDW_INVALIDATE | RDW_UPDATENOW );
*************************************************/

#include "stdafx.h"
#define WIN32_LEAN_AND_MEAN
#include <windows.h>
#include <tchar.h>
#include <math.h>
//#include "resource.h"

// Global Variables:
HWND hWnd        = NULL;
HWND hDlg        = NULL;
TCHAR szWindowClass[] = _T("Test Class");
COLORREF crefKey = RGB(0, 255, 0);
HBRUSH hbrBkgnd  = NULL;
HINSTANCE hInst  = NULL;

int     posX        = 0;
int     posY        = 0;
float   angle       = 0;

// Foward declarations of functions included in this code module:
ATOM MyRegisterClass(HINSTANCE hInstance);
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow);
LRESULT CALLBACK WndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
INT_PTR CALLBACK DialogProc(HWND hDlg, UINT uMsg, WPARAM wParam, LPARAM lParam);

void DrawWindow(HWND hWnd);

// Main function
int APIENTRY WinMain(HINSTANCE hInstance,
                     HINSTANCE hPrevInstance,
                     LPSTR     lpCmdLine,
                     int       nCmdShow)
{
    hbrBkgnd = CreateSolidBrush(crefKey);

    MyRegisterClass(hInstance);

    // Create our window:
    if (!InitInstance(hInstance, nCmdShow))
        return FALSE;

    // Main message loop:
    MSG msg;
    while( GetMessage(&msg, NULL, 0, 0) ) 
    {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    DeleteObject(hbrBkgnd);

    return msg.wParam;
}

ATOM MyRegisterClass(HINSTANCE hInstance)
{
    WNDCLASSEX wcex = {0};
    wcex.cbSize         = sizeof(WNDCLASSEX);
    wcex.style          = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc    = WndProc;
    wcex.hInstance      = hInstance;
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_APPLICATION));
    wcex.hCursor        = LoadCursor(NULL, IDC_ARROW);
    wcex.hbrBackground  = hbrBkgnd;
    wcex.lpszClassName  = szWindowClass;
    wcex.hIconSm        = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_APPLICATION));

    return RegisterClassEx(&wcex);
}

BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
    hInst = hInstance;

    RECT rect = {0};
    SystemParametersInfo(SPI_GETWORKAREA, 0, &rect, 0);

    hWnd = CreateWindowEx( WS_EX_LAYERED,
                           szWindowClass,
                           NULL,
                           WS_POPUP,
                           rect.left,
                           rect.top,
                           rect.right  - rect.left,
                           rect.bottom - rect.top,
                           NULL,
                           NULL,
                           hInstance,
                           NULL );

    if(!hWnd)
    {
        return FALSE;
    }

    ShowWindow(hWnd, nCmdShow);
    SetWindowPos(hWnd, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE);
    SetLayeredWindowAttributes(hWnd, crefKey, 255, LWA_COLORKEY);
    UpdateWindow(hWnd);

    return TRUE;
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    switch (uMsg) 
    {
    case WM_USER:
        // Close the main window when the dialog tells us to
        DestroyWindow(hWnd);
        return TRUE;

    case WM_CREATE:
        // Create a dialog so we have a way of closing the app
        hDlg = CreateDialog(hInst,0, 0, (DLGPROC)DialogProc);
        ShowWindow(hDlg, SW_SHOW);

        // Create a timer so that we can animate our drawing
        SetTimer(hWnd, 1, 50, 0);
        break;

    case WM_DESTROY:
        // Kill our timer
        KillTimer(hWnd, 1);

        // Post the quit message so that our main loop will know that we have quit
        PostQuitMessage(0);
        break;

    case WM_TIMER:
        {
            angle += 0.1f;
            if( angle >= 2*3.141592f )
                angle -= 2*3.141592f;

            // Get the current mouse position
            POINT pt = {0, 0};
            GetCursorPos(&pt);
            ScreenToClient(hWnd, &pt);

            // Update position
            RECT rc = {posX-50, posY-50, posX+50, posY+50};

            // redraw the desktop window right away
            RedrawWindow( hWnd, &rc, NULL, RDW_INVALIDATE | RDW_ERASE | RDW_UPDATENOW );

            posX = pt.x;
            posY = pt.y;

            // are we re-drawing the window at the new cursor position just to make sure that
            // the part of the window under the animation is validated?!
            RECT rc2 = {posX-50, posY-50, posX+50, posY+50};
            RedrawWindow( hWnd, &rc, NULL, RDW_INVALIDATE | RDW_ERASE | RDW_UPDATENOW );

            DrawWindow(hWnd);
        }
        break;

        default:
            return DefWindowProc(hWnd, uMsg, wParam, lParam);
    }

    return 0;
}

INT_PTR CALLBACK DialogProc(HWND hDlg, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    switch (uMsg)
    {
    case WM_COMMAND:
        // Close the dialog no matter what command it is
        // AND tell parent window to close, too
        PostMessage(hWnd, WM_USER, 0, 0);
        DestroyWindow(hDlg);
        return TRUE;
    }
   
    return FALSE;
}

// This is where we draw our image on the desktop
void DrawWindow(HWND hWnd)
{
    // Get the DC to the desktop window
    HDC dc = GetDC(hWnd);

    // Store DC settings
    int storedDC = SaveDC(dc);

    // Draw the rotating star
    POINT pts[5];
    pts[0].x = posX + int(50*cos(angle + 0.000)); pts[0].y = posY + int(50*sin(angle + 0.000));
    pts[1].x = posX + int(50*cos(angle + 2.513)); pts[1].y = posY + int(50*sin(angle + 2.513));
    pts[2].x = posX + int(50*cos(angle + 5.027)); pts[2].y = posY + int(50*sin(angle + 5.027));
    pts[3].x = posX + int(50*cos(angle + 1.257)); pts[3].y = posY + int(50*sin(angle + 1.257));
    pts[4].x = posX + int(50*cos(angle + 3.770)); pts[4].y = posY + int(50*sin(angle + 3.770));
    Polygon(dc, pts, 5);

    // redraw the portion of the window that was just painted over with the rotating star,
    // but specify RDW_NOERASE to keep the desktop from drawing the background as well
    RECT rc = {posX-50, posY-50, posX+50, posY+50};
    RedrawWindow( hWnd, &rc, NULL, RDW_NOERASE | RDW_INVALIDATE | RDW_UPDATENOW );

    // Restore DC settings to their original values
    RestoreDC(dc, storedDC);

    // Release the DC
    ReleaseDC(hWnd, dc);
}