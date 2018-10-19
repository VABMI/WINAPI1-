#include "head.h"



HWND DoCreateStatusBar(HWND hwndParent, int idStatus, HINSTANCE
                      hinst, int cParts)
{
	static HWND hwndStatus={0};
    RECT rcClient;
    HLOCAL hloc;
    PINT paParts;
    int i, nWidth;
	DestroyWindow(hwndStatus);
    // Ensure that the common control DLL is loaded.
    InitCommonControls();

	   // Create the status bar.
    hwndStatus = CreateWindowEx(
        0,                       // no extended styles
        STATUSCLASSNAME,         // name of status bar class
        (PCTSTR) NULL,           // no text when first created
         0x0820 |         // includes a sizing grip
        WS_CHILD | WS_VISIBLE,   // creates a visible child window
        0, 25, 0, 0,              // ignores size and position
        hwndParent,              // handle to parent window
        (HMENU) idStatusBar,       // child window identifier
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
    SendMessage(hwndStatus, SB_SETPARTS, (WPARAM) cParts, (LPARAM)paParts);

	 HICON hIcon = (HICON)LoadImage(NULL, "C:\\Users\\vaxoa\\OneDrive\\Documents\\GitHub\\WINAPI\\icon\\Debug\\Itzikgur-My-Seven-Downloads-2.ico", IMAGE_ICON, 20, 20, LR_LOADFROMFILE);
	 
	 
	 if(!hIcon)
	  {

		  MessageBox(hwndParent,"ERROR Status Bar icon ","ERROR Status Bar icon ",0);

	  }
	 SendMessage(hwndStatus, SB_SETICON, 4, (LPARAM)hIcon);
	
    // Free the array, and return.
    LocalUnlock(hloc);
    LocalFree(hloc);

    return hwndStatus;
}  
