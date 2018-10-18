//
// Log Window Test App - by Napalm
//
// You may use all or any part of the following code as long as you agree
// to the Creative Commons Attribution 2.0 UK: England & Wales license.
// [url="http://creativecommons.org/licenses/by/2.0/uk/"]http://creativecommo...nses/by/2.0/uk/[/url]
//
// You must have the up to date headers to compile this.. if you can't
// compile it install the PSDK/Windows SDK and try again.
//
//
#pragma comment(linker, "\"/manifestdependency:type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")
#pragma comment(lib, "comctl32.lib")
#pragma comment(lib, "shlwapi.lib")
#pragma comment(lib, "msimg32.lib")
 
#define _WIN32_WINNT            0x0501
#include <windows.h>
#include <commctrl.h>
#include <shlwapi.h>
 
#define LOG_LINE_LIMIT          25
 
// Child Window/Control IDs
#define IDC_TXTENTRY            100
#define IDC_TXTLOG              101
#define IDC_BTNADDENTRY         102
#define IDC_CHKPINTOBOTTOM      103
#define IDC_CHKLIMITBUFFER      104
 
// Globals
HINSTANCE g_hInst;
HFONT g_hfText;
 
LRESULT CALLBACK SubclassEditProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    WNDPROC wpOld = (WNDPROC)GetWindowLongPtr(hWnd, GWLP_USERDATA);
    LRESULT lrResult = 0;
 
    if(wpOld){
        switch(uMsg)
        {

            case WM_NCDESTROY:
               // SetWindowLongPtr(hWnd, GWLP_WNDPROC, (LONG_PTR)wpOld);
               // SetWindowLongPtr(hWnd, GWLP_USERDATA, 0);
                break;
 
    
        }
 

    }
	return 0;
//    return DefWindowProc(hWnd, uMsg, wParam, lParam);
}
 
LRESULT CALLBACK MainWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    // We should not have used a static.. we should really attach this value to the window
    // using either GWL_USERDATA or a allocated structure with a a pointer stored in GWL_USERDATA.
    static HWND s_hWndLastFocus;
 
    switch(uMsg)
    {
        // Initialize our window and create our child controls.
        case WM_CREATE:
            {
                HWND hWndChild;
                TCHAR szBuffer[MAX_PATH];
 
                // TEXT("This text will be appended to the box below.")
                // Create the 'entry box' single-line edit control.
                hWndChild = CreateWindowEx(WS_EX_CLIENTEDGE, WC_EDIT, NULL,ES_AUTOHSCROLL | WS_CHILD | WS_TABSTOP | WS_VISIBLE,0, 0, 0, 0, hWnd, (HMENU)IDC_TXTENTRY, g_hInst, NULL);
                if(!hWndChild) return -1;
                // Subclass the edit control.
                SetWindowLongPtr(hWndChild, GWLP_USERDATA, GetWindowLongPtr(hWndChild, GWLP_WNDPROC));
                SetWindowLongPtr(hWndChild, GWLP_WNDPROC, (LONG_PTR)SubclassEditProc); //////// shvilis mesijebi 
                // Set the edit controls properties.
      
         
                SendMessage(hWndChild, EM_SETCUEBANNER, 0, (LPARAM)TEXT("Log Entry Text"));
 
                // Create the 'add entry' button.
                hWndChild = CreateWindowEx(0, WC_BUTTON, TEXT("&Add Entry"),BS_PUSHBUTTON | BS_TEXT |WS_CHILD | WS_TABSTOP | WS_VISIBLE,0, 0, 0, 0, hWnd, (HMENU)IDC_BTNADDENTRY, g_hInst, NULL);
                if(!hWndChild) return -1;
                // Set the button controls properties.
   


 
                // Create first options check-box.
                hWndChild = CreateWindowEx(0, WC_BUTTON, TEXT("Pin scroll to bottom."),BS_CHECKBOX | BS_AUTOCHECKBOX | BS_TEXT | BS_VCENTER |WS_CHILD | WS_TABSTOP | WS_VISIBLE,0, 0, 0, 0, hWnd, (HMENU)IDC_CHKPINTOBOTTOM, g_hInst, NULL);
                if(!hWndChild) return -1;
            



 
                // Create second options check-box.
                wsprintf(szBuffer, TEXT("Limit log to %u lines."), LOG_LINE_LIMIT);
                hWndChild = CreateWindowEx(0, WC_BUTTON, szBuffer,BS_CHECKBOX | BS_AUTOCHECKBOX | BS_TEXT | BS_VCENTER |WS_CHILD | WS_TABSTOP | WS_VISIBLE,0, 0, 0, 0, hWnd, (HMENU)IDC_CHKLIMITBUFFER, g_hInst, NULL);
                if(!hWndChild) return -1;
         


 
                // Create 'log window' multi-line edit control.
                hWndChild = CreateWindowEx(WS_EX_CLIENTEDGE, WC_EDIT, NULL,ES_MULTILINE | ES_WANTRETURN | ES_AUTOVSCROLL | ES_NOHIDESEL |WS_VSCROLL | WS_CHILD | WS_TABSTOP | WS_VISIBLE,
                    0, 0, 0, 0, hWnd, (HMENU)IDC_TXTLOG, g_hInst, NULL);
                if(!hWndChild) return -1;


                // Subclass the edit control.
			//	SetWindowLongPtr(hWndChild, GWLP_USERDATA, GetWindowLongPtr(hWndChild, GWLP_WNDPROC));
              SetWindowLongPtr(hWndChild, GWLP_WNDPROC, (LONG_PTR)SubclassEditProc); //////// shvilis mesijebi 
           
                 
                SetFocus(hWndChild);
            //    s_hWndLastFocus = NULL;
            }
            return 0;

        case WM_ACTIVATE:
            if(LOWORD(wParam) == WA_INACTIVE)
                s_hWndLastFocus = GetFocus();
            return 0;

        case WM_SETFOCUS:

            if(s_hWndLastFocus)
          //      SetFocus(s_hWndLastFocus);
            return 0;
 

        case WM_GETMINMAXINFO:
            {
			
			}
            return 0;
 

        case WM_WINDOWPOSCHANGING:
        case WM_WINDOWPOSCHANGED:
            {
                HDWP hDWP;
                RECT rc;
 
                // Create a deferred window handle.
                if(hDWP = BeginDeferWindowPos(5)){ // Deferring 5 child controls
                    GetClientRect(hWnd, &rc);
 
                    // Defer each window move/size until end and do them all at once.
                    hDWP = DeferWindowPos(hDWP, GetDlgItem(hWnd, IDC_TXTENTRY), NULL,
                        10, 10, rc.right - 130, 25,
                        SWP_NOZORDER | SWP_NOREDRAW);
 
                    hDWP = DeferWindowPos(hDWP, GetDlgItem(hWnd, IDC_BTNADDENTRY), NULL,
                        rc.right - 110, 10, 100, 25,
                        SWP_NOZORDER | SWP_NOREDRAW);
 
                    hDWP = DeferWindowPos(hDWP, GetDlgItem(hWnd, IDC_CHKPINTOBOTTOM), NULL,
                        10, 35, (rc.right / 2) - 15, 35,
                        SWP_NOZORDER | SWP_NOREDRAW);
 
                    hDWP = DeferWindowPos(hDWP, GetDlgItem(hWnd, IDC_CHKLIMITBUFFER), NULL,
                        (rc.right / 2) + 5, 35, (rc.right / 2) - 15, 35,
                        SWP_NOZORDER | SWP_NOREDRAW);
 
                 
 
                    // Resize all windows under the deferred window handled at the same time.
                    EndDeferWindowPos(hDWP);
 
                    // We told DeferWindowPos not to redraw the controls so we can redraw
                    // them here all at once.
                  
                }
            }
            return 0;
 
        // Handle the notifications of button presses.
        case WM_COMMAND:
            // If it was a button press and came from our button.
            if(wParam == MAKELONG(IDC_BTNADDENTRY, BN_CLICKED)){
 
                UINT uChkPinToBottom, uChkLimitLogLength;
                INT nSelStart, nSelEnd;
                SCROLLINFO siLogVert;
                HWND hWndChild;
                LPTSTR lpBuffer;
                INT cchTextLen;
 
                // Allocate a buffer for our entry text.
                // The +2 is for an extra space we append and null terminator.

                hWndChild  = GetDlgItem(hWnd, IDC_TXTENTRY);
                cchTextLen = GetWindowTextLength(hWndChild);
                lpBuffer   = (LPTSTR)HeapAlloc(GetProcessHeap(),
                    HEAP_ZERO_MEMORY, (cchTextLen + 2) * sizeof(TCHAR));


                if(lpBuffer == NULL)
				{
                    // Fuck.. what happened???
                    MessageBeep(MB_ICONERROR);
                    return 0;
                }
 
                // Read our entry text.
                if(GetWindowText(hWndChild, lpBuffer, cchTextLen + 1)){
                    StrCat(lpBuffer, TEXT(" "));
 
                    // Get the check-box states so we can change our logic depending on them.
                    uChkPinToBottom = (DWORD)SendDlgItemMessage(hWnd,
                        IDC_CHKPINTOBOTTOM, BM_GETCHECK, 0, 0);
                    uChkLimitLogLength = (DWORD)SendDlgItemMessage(hWnd,
                        IDC_CHKLIMITBUFFER, BM_GETCHECK, 0, 0);
 
                    // Get our edit log window handle.
                    hWndChild = GetDlgItem(hWnd, IDC_TXTLOG);
 
                    // Tell edit control not to update the screen.
                    SendMessage(hWndChild, WM_SETREDRAW, FALSE, 0);
 
                    // Save our current selection.
                    nSelStart = nSelEnd = 0;
                    SendMessage(hWndChild, EM_GETSEL, (WPARAM)&nSelStart, (LPARAM)&nSelEnd);
 
                    // Save our current scroll info.
                    ZeroMemory(&siLogVert, sizeof(SCROLLINFO));
                    siLogVert.cbSize = sizeof(SCROLLINFO);
                    siLogVert.fMask  = SIF_PAGE | SIF_POS | SIF_RANGE;
                    GetScrollInfo(hWndChild, SB_VERT, &siLogVert);
 
                    // Limit log to LOG_LINE_LIMIT lines.
                    if(uChkLimitLogLength == BST_CHECKED){
                        // Test if more than LOG_LINE_LIMIT.
                        INT nLines = (INT)SendMessage(hWndChild, EM_GETLINECOUNT, 0, 0);
                        if(nLines > LOG_LINE_LIMIT){
                            // Replace content to remove with nothing.
                            INT nRemove = (DWORD)SendMessage(hWndChild, EM_LINEINDEX,
                                (WPARAM)(nLines - LOG_LINE_LIMIT), 0);
                            SendMessage(hWndChild, EM_SETSEL, 0, nRemove);
                            SendMessage(hWndChild, EM_REPLACESEL, FALSE, (LPARAM)"");
                            // Update old selection indexes.
                            nSelStart = max(nSelStart - nRemove, 0);
                            nSelEnd   = max(nSelEnd - nRemove, 0);
                        }
                    }
 
                    // Update the log window by appending text to it.
                    cchTextLen = GetWindowTextLength(hWndChild);
                    SendMessage(hWndChild, EM_SETSEL, cchTextLen, cchTextLen);
                    SendMessage(hWndChild, EM_REPLACESEL, FALSE, (LPARAM)lpBuffer);
 
                    // Update Pin-To-Bottom behavior.
                    if(uChkPinToBottom == BST_CHECKED){
                        // Only Pin-To-Bottom when the user is in the bottom page.
                        UINT uScrollLines = 1;
                        SystemParametersInfo(SPI_GETWHEELSCROLLLINES, 0, &uScrollLines, 0);
                        if(siLogVert.nPos > (INT)(siLogVert.nMax -
                            siLogVert.nPage - uScrollLines))
                            SendMessage(hWndChild, WM_VSCROLL, SB_BOTTOM, 0);
                    }else{
                        // Restore scroll position if not pinned.
                        SendMessage(hWndChild, WM_VSCROLL, MAKELONG(siLogVert.nPos,
                            SB_THUMBPOSITION), 0);
                    }
 
                    // Restore old text selection.
                    SendMessage(hWndChild, EM_SETSEL, nSelStart, nSelEnd);
 
                    // Update the state of the edit control on the screen.
                    SendMessage(hWndChild, WM_SETREDRAW, TRUE, 0);
                    UpdateWindow(hWndChild);
 
                }else{
                    // No text in the entry box?
                    MessageBeep(MB_ICONWARNING);
                }
 
                // Free temporary entry box allocation.
                HeapFree(GetProcessHeap(), 0, lpBuffer);
                return 0;
            }
            break;
 
        // Sent by all edit controls that are not disabled.
        case WM_CTLCOLOREDIT:
            // Test to see if the request is from our log edit control.
            if((HWND)lParam == GetDlgItem(hWnd, IDC_TXTLOG)){
                // Set the edit control painting of the background to transparent.
                SetBkMode((HDC)wParam, TRANSPARENT);
                SetTextColor((HDC)wParam, RGB(0x2B, 0x4C, 0x67));
                return (LRESULT)GetStockObject(HOLLOW_BRUSH);
            }
            break;
 
        case WM_DESTROY:
            // We post a WM_QUIT when our window is destroyed so we break the main message loop.
            PostQuitMessage(0);
            break;
    }
 
    // Not a message we wanted? No problem hand it over to the Default Window Procedure.
    return DefWindowProc(hWnd, uMsg, wParam, lParam);
}
 
 
// Program Entry Point
int APIENTRY WinMain(HINSTANCE hInst, HINSTANCE hPrev, LPSTR lpCmdLine, INT nShowCmd)
{
    OSVERSIONINFO lpVer;
    WNDCLASSEX wcex;
    DWORD dwExStyle;
    HDC hdcScreen;
    HWND hWnd;
    MSG msg;
 
    g_hInst = hInst;
 
    // Link in comctl32.dll
    InitCommonControls();
 
    ZeroMemory(&msg,  sizeof(MSG));
    ZeroMemory(&wcex, sizeof(WNDCLASSEX));
 
    // Register our Main Window class.
    wcex.cbSize     = sizeof(WNDCLASSEX);
    wcex.hInstance   = hInst;
    wcex.lpszClassName = TEXT("MainWindow");
    wcex.lpfnWndProc   = MainWindowProc;
    wcex.hCursor       = LoadCursor(NULL, IDC_ARROW);
    wcex.hIcon       = LoadIcon(NULL, IDI_APPLICATION);
    wcex.hIconSm       = wcex.hIcon;
    wcex.hbrBackground = (HBRUSH)(COLOR_BTNFACE + 1);
    if(!RegisterClassEx(&wcex))
        return 1;
 
    // Create a font we can later use on our controls.
    hdcScreen = GetDC(HWND_DESKTOP);
    g_hfText = CreateFont(-MulDiv(11, GetDeviceCaps(hdcScreen, LOGPIXELSY), 72), // 11pt
        0, 0, 0, FW_NORMAL, FALSE, FALSE, FALSE, ANSI_CHARSET, OUT_TT_PRECIS,
        CLIP_DEFAULT_PRECIS, DEFAULT_QUALITY, FF_DONTCARE, TEXT("Tahoma"));
    ReleaseDC(HWND_DESKTOP, hdcScreen);
 
    // Default main window ex-style.
    dwExStyle = WS_EX_APPWINDOW;
 
    // If we are using XP or above lets 'double-buffer' the window to reduce the
    // flicker to the edit controls when drawing (notepad needs this).
    lpVer.dwOSVersionInfoSize = sizeof(OSVERSIONINFO);
    if(GetVersionEx(&lpVer) && (lpVer.dwMajorVersion > 5 ||
        (lpVer.dwMajorVersion == 5 && lpVer.dwMinorVersion == 1)))
        dwExStyle |= WS_EX_COMPOSITED;
 
    // Create an instance of the Main Window.
    hWnd = CreateWindowEx(dwExStyle, wcex.lpszClassName, TEXT("Log Window Test App v2 - by Napalm"),
        WS_OVERLAPPEDWINDOW | WS_CLIPCHILDREN, CW_USEDEFAULT, CW_USEDEFAULT, 450, 330,
        HWND_DESKTOP, NULL, hInst, NULL);
 
    if(hWnd){
        // Show the main window and enter the message loop.
        ShowWindow(hWnd, nShowCmd);
        UpdateWindow(hWnd);
        while(GetMessage(&msg, NULL, 0, 0))
        {
            // If the message was not wanted by the Dialog Manager dispatch it like normal.
            if(!IsDialogMessage(hWnd, &msg)){
                TranslateMessage(&msg);
                DispatchMessage(&msg);
            }
        }
    }
 
    // Free up our resources and return.
    DeleteObject(g_hfText);
    return (int)msg.wParam;
}