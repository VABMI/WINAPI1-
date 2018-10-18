// 
// FileInputBox - by Napalm and EvilFourZero
// 
// You may use all or any part of the following code as long as you agree 
// to the Creative Commons Attribution 2.0 UK: England & Wales license.
// http://creativecommons.org/licenses/by/2.0/uk/
// 
// 
#pragma comment(lib, "comctl32.lib")
#pragma comment(lib, "shlwapi.lib")
#pragma comment(lib, "ole32.lib")
 
#include "FileInputBox.h"
 // 
// DropApp - by Napalm and EvilFourZero
// 
// You may use all or any part of the following code as long as you agree 
// to the Creative Commons Attribution 2.0 UK: England & Wales license.
// http://creativecommons.org/licenses/by/2.0/uk/
// 
// 
#pragma comment(lib, "comctl32.lib")
 
#include "DropApp.h"
#include "FileInputBox.h"
 
 
// Child Window/Control IDs
#define IDC_LBLINFO             100
#define IDC_TXTFILE1            101
#define IDC_TXTFILE2            102
#define IDC_TXTFILE3            103
 
// Globals
HINSTANCE g_hInst;
HFONT g_hfText;
 
// Prototypes
LRESULT CALLBACK MainWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
BOOL CenterWindow(HWND hWnd, HWND hWndCenter);
 
 
// Program Entry Point
int APIENTRY WinMain(HINSTANCE hInst, HINSTANCE hPrev, LPSTR lpCmdLine, INT nShowCmd)
{
    WNDCLASSEX wcex;
    HDC hdcScreen;
    HWND hWnd;
    MSG msg;
     
    g_hInst = hInst;
     
    // Link in comctl32.dll
    InitCommonControls();
     
    ZeroMemory(&msg,  sizeof(MSG));
    ZeroMemory(&wcex, sizeof(WNDCLASSEX));
     
    // Register our Main Window class.
    wcex.cbSize        = sizeof(WNDCLASSEX);
    wcex.hInstance     = hInst;
    wcex.lpszClassName = TEXT("MainWindow");
    wcex.lpfnWndProc   = MainWindowProc;
    wcex.hCursor       = LoadCursor(NULL, IDC_ARROW);
    wcex.hIcon         = LoadIcon(NULL, IDI_APPLICATION);
    wcex.hIconSm       = wcex.hIcon;
    wcex.hbrBackground = (HBRUSH)(COLOR_BTNFACE + 1);
    if(!RegisterClassEx(&wcex))
        return 1;
     
    // Create a font we can later use on our controls. We use MulDiv and GetDeviceCaps to convert
    // our point size to match the users DPI setting.
    hdcScreen = GetDC(HWND_DESKTOP);
    g_hfText = CreateFont(-MulDiv(11, GetDeviceCaps(hdcScreen, LOGPIXELSY), 72), // 11pt
        0, 0, 0, FW_NORMAL, FALSE, FALSE, FALSE, ANSI_CHARSET, OUT_TT_PRECIS,
        CLIP_DEFAULT_PRECIS, DEFAULT_QUALITY, FF_DONTCARE, TEXT("Tahoma"));
    ReleaseDC(HWND_DESKTOP, hdcScreen);
     
    // Create an instance of the Main Window.
    hWnd = CreateWindowEx(WS_EX_APPWINDOW, wcex.lpszClassName, TEXT("Drop App - by Napalm"),
        WS_OVERLAPPEDWINDOW | WS_CLIPCHILDREN | WS_CLIPSIBLINGS, CW_USEDEFAULT, CW_USEDEFAULT, 450, 250,
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
 
 
LRESULT CALLBACK MainWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    static HWND s_hWndLastFocus;
    switch(uMsg)
    {
        // Initialize our window and create our child controls.
        case WM_CREATE:
            {
                OPENFILENAME ofnBrowse;
                HWND hWndChild;
                INT nFile;
 
                CenterWindow(hWnd, NULL);
                 
                // Create the info text at the the top.
                hWndChild = CreateWindowEx(0, WC_STATIC,
                    TEXT("Try and drag and drop a files into the boxes below or press the ")
                    TEXT("browse buttons. The boxes also autocomplete as you type."), 
                    SS_LEFT | SS_EDITCONTROL | WS_CHILD | WS_VISIBLE,
                    0, 0, 0, 0, hWnd, (HMENU)(IDC_LBLINFO), g_hInst, NULL);
                if(!hWndChild) return -1;
                // Set the static text properties.
                SendMessage(hWndChild, WM_SETFONT, (WPARAM)g_hfText, FALSE);
                 
                // Setup the defaults for the openfilename structure.
                ZeroMemory(&ofnBrowse, sizeof(ofnBrowse));
                ofnBrowse.lStructSize = sizeof(ofnBrowse);
                ofnBrowse.lpstrFilter = TEXT("All Files\0*.*\0\0");
                ofnBrowse.lpstrTitle  = TEXT("Browse for file...");
                ofnBrowse.Flags       = OFN_DONTADDTORECENT | OFN_ENABLESIZING | 
                    OFN_EXPLORER | OFN_FILEMUSTEXIST | OFN_HIDEREADONLY;
                         
                for(nFile = 0; nFile < 3; nFile++){
                    hWndChild = CreateWindowEx(WS_EX_CLIENTEDGE, WC_EDIT, NULL, 
                        ES_AUTOHSCROLL | WS_CHILD | WS_TABSTOP | WS_VISIBLE,
                        0, 0, 0, 0, hWnd, (HMENU)(IDC_TXTFILE1 + nFile), g_hInst, NULL);
                    if(!hWndChild) return -1;
                    // Set the edit controls properties and convert it to a input box.
                    SendMessage(hWndChild, WM_SETFONT, (WPARAM)g_hfText, FALSE);
                    ConvertEditToFileInputBox(hWndChild, &ofnBrowse);
                }
                 
                // Create the close button.
                hWndChild = CreateWindowEx(0, WC_BUTTON, TEXT("&Close"),
                    BS_DEFPUSHBUTTON | BS_TEXT | WS_CHILD | WS_TABSTOP | WS_VISIBLE,
                    0, 0, 0, 0, hWnd, (HMENU)IDCANCEL, g_hInst, NULL);
                if(!hWndChild) return -1;
                SendMessage(hWndChild, WM_SETFONT, (WPARAM)g_hfText, FALSE);
                 
                // Focus on the static text.
                SetFocus(GetDlgItem(hWnd, IDC_LBLINFO));
                s_hWndLastFocus = NULL;
            }
            return 0;
         
        // We get this message with WA_INACTIVE set when our window is no-longer
        // the foreground window so we want to save which of our controls has the focus
        // so that when the user returns the right control gets the keyboard input.
        case WM_ACTIVATE:
            if(LOWORD(wParam) == WA_INACTIVE)
                s_hWndLastFocus = GetFocus();
            return 0;
             
        // We get this message when our window receives the user focus. We then
        // move that focus to the previously used child window.
        case WM_SETFOCUS:
            if(s_hWndLastFocus)
                SetFocus(s_hWndLastFocus);
            return 0;
         
        // We accept this message so we can set a minimum window size. This only sets the users
        // tracking size. The window itself can always be resized smaller programmatically unless
        // you restrict it in WM_WINDOWPOSCHANGING/WM_WINDOWPOSCHANGED. 
        case WM_GETMINMAXINFO:
            {
                LPMINMAXINFO lpInfo = (LPMINMAXINFO)lParam;
                if(lpInfo)
                    lpInfo->ptMinTrackSize.x = 300, lpInfo->ptMinTrackSize.y = 250;
            }
            return 0;
         
        // These next two messages are better to use rather than WM_MOVE/WM_SIZE.
        // Remember WM_MOVE/WM_SIZE are from 16bit windows. In 32bit windows the window
        // manager only sends these two messages and the DefWindowProc() handler actually
        // accepts them and converts them to WM_MOVE/WM_SIZE.
        // 
        // We accept this so we can scale our controls to the client size.
        case WM_WINDOWPOSCHANGING:
        case WM_WINDOWPOSCHANGED:
            {
                HDWP hDWP;
                RECT rc;
                INT nFile;
                 
                // Create a deferred window handle.
                if(hDWP = BeginDeferWindowPos(5)){ // Deferring 5 child controls
                    GetClientRect(hWnd, &rc);
                     
                    // Defer each window move/size until end and do them all at once.
                    hDWP = DeferWindowPos(hDWP, GetDlgItem(hWnd, IDC_LBLINFO), NULL,
                        10, 10, rc.right - 20, 35,
                        SWP_NOZORDER | SWP_NOREDRAW);
                     
                    for(nFile = 0; nFile < 3; nFile++){
                        hDWP = DeferWindowPos(hDWP, GetDlgItem(hWnd, IDC_TXTFILE1 + nFile), NULL,
                            10, 70 + (nFile * 35), rc.right - 20, 25,
                            SWP_NOZORDER | SWP_NOREDRAW);
                    }
                     
                    hDWP = DeferWindowPos(hDWP, GetDlgItem(hWnd, IDCANCEL), NULL,
                        (rc.right  / 2) - 60, rc.bottom - 40, 120, 25,
                        SWP_NOZORDER | SWP_NOREDRAW);
                     
                    // Resize all windows under the deferred window handled at the same time.
                    EndDeferWindowPos(hDWP);
                     
                    // We told DeferWindowPos not to redraw the controls so we can redraw
                    // them here all at once.
                    RedrawWindow(hWnd, NULL, NULL, RDW_INVALIDATE | RDW_ALLCHILDREN | 
                        RDW_ERASE | RDW_NOFRAME | RDW_UPDATENOW);
                }
            }
            return 0;
         
        // Handle the notifications of button presses.
        case WM_COMMAND:
            // If it was a button press and it came from our button.
            if(wParam == MAKELONG(IDCANCEL, BN_CLICKED)){
                DestroyWindow(hWnd);
                return 0;
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
 
 
// Center window in primary monitor or owner/parent.
BOOL CenterWindow(HWND hWnd, HWND hWndCenter = NULL)
{
    RECT rcDlg, rcArea, rcCenter;
    HWND hWndParent;
    DWORD dwStyle, dwStyleCenter;
     
    // Determine owner window to center against.
    dwStyle = GetWindowLong(hWnd, GWL_STYLE);
    if(!hWndCenter)
        hWndCenter = (dwStyle & WS_CHILD) ? GetParent(hWnd) : GetWindow(hWnd, GW_OWNER);
     
    // Get coordinates of the window relative to its parent.
    GetWindowRect(hWnd, &rcDlg);
    if(!(dwStyle & WS_CHILD)){
        // Don't center against invisible or minimized windows.
        if(hWndCenter){
            dwStyleCenter = GetWindowLong(hWndCenter, GWL_STYLE);
            if(!(dwStyleCenter & WS_VISIBLE) || (dwStyleCenter & WS_MINIMIZE))
                hWndCenter = NULL;
        }
        // Center within screen coordinates.
        SystemParametersInfo(SPI_GETWORKAREA, NULL, &rcArea, NULL);
        if(hWndCenter) GetWindowRect(hWndCenter, &rcCenter);
        else rcCenter = rcArea;
    }else{
        // Center within parent client coordinates.
        hWndParent = GetParent(hWnd);
        GetClientRect(hWndParent, &rcArea);
        GetClientRect(hWndCenter, &rcCenter);
        MapWindowPoints(hWndCenter, hWndParent, (LPPOINT)&rcCenter, 2);
    }
 
    int DlgWidth  = rcDlg.right  - rcDlg.left;
    int DlgHeight = rcDlg.bottom - rcDlg.top;
 
    // Find dialog's upper left based on rcCenter.
    int xLeft = (rcCenter.left + rcCenter.right)  / 2 - DlgWidth  / 2;
    int yTop  = (rcCenter.top  + rcCenter.bottom) / 2 - DlgHeight / 2;
 
    // If the dialog is outside the screen, move it inside.
    if(xLeft < rcArea.left) xLeft = rcArea.left;
    else if(xLeft + DlgWidth > rcArea.right) xLeft = rcArea.right - DlgWidth;
    if(yTop < rcArea.top) yTop = rcArea.top;
    else if(yTop + DlgHeight > rcArea.bottom) yTop = rcArea.bottom - DlgHeight;
 
    // Map screen coordinates to child coordinates.
    return SetWindowPos(hWnd, NULL, xLeft, yTop, 0, 0,
        SWP_NOSIZE | SWP_NOZORDER | SWP_NOACTIVATE);
}
 
// Use this function to convert any edit control to a file input control.
BOOL ConvertEditToFileInputBox(HWND hWndEdit, LPOPENFILENAME lpofnSettings)
{
    LPFILEINPUTBOX lpFileInput;
    HINSTANCE hInst;
    HANDLE hHeap;
    // Is this edit control already a file-input control
    if(GetDlgItem(hWndEdit, IDC_BTNBROWSE))
        return FALSE;
     
    // Create state structure and copy variables.
    hHeap = GetProcessHeap();
    lpFileInput = (LPFILEINPUTBOX)HeapAlloc(hHeap, HEAP_ZERO_MEMORY,
        sizeof(FILEINPUTBOX) + lpofnSettings->lStructSize);
    if(!lpFileInput)
        return FALSE;
    CopyMemory(&lpFileInput->ofnSettings, lpofnSettings, lpofnSettings->lStructSize);
    lpFileInput->ofnSettings.hwndOwner = hWndEdit;
    lpFileInput->ofnSettings.nMaxFile  = ((MAX_PATH + 1) * 2);
    lpFileInput->ofnSettings.lpstrFile = (LPTSTR)HeapAlloc(hHeap, HEAP_ZERO_MEMORY,
        (lpFileInput->ofnSettings.nMaxFile * sizeof(TCHAR)));
     
    // Try and create a browse button to select a file.
    hInst = (lpofnSettings->hInstance) ? lpofnSettings->hInstance : GetModuleHandle(NULL);
    lpFileInput->hWndBrowse = CreateWindowEx(0, WC_BUTTON, TEXT("Browse"),
        BS_PUSHBUTTON | WS_CHILD | WS_VISIBLE | WS_TABSTOP,
        0, 0, 80, 25, hWndEdit, (HMENU)IDC_BTNBROWSE, hInst, NULL);
    if(!lpFileInput->hWndBrowse){
        HeapFree(GetProcessHeap(), 0, lpFileInput);
        return FALSE;
    }
 
    // Assign state structure and subclass the edit control so we can add our own features.
    SetWindowLongPtr(hWndEdit, GWLP_USERDATA, (LONG_PTR)lpFileInput);
    lpFileInput->wpOld = SafeSubclassWindow(hWndEdit, FileInputBoxProc);
     
    // Initialize/Reference the COM library (ole32.dll) and give the edit control
    // the ability to auto-complete from the file-system. If this fails we continue
    // just without the auto-complete ability.
    if(SUCCEEDED(CoInitialize(NULL))){
        SHAutoComplete(hWndEdit, SHACF_AUTOSUGGEST_FORCE_ON | SHACF_FILESYSTEM | SHACF_USETAB);
        lpFileInput->bUsingCOM = TRUE;
    }
 
    // Set the browse buttons font to match that of the edit control.
    SendMessage(hWndEdit, WM_SETFONT, (WPARAM)SendMessage(hWndEdit, WM_GETFONT, 0, 0), FALSE);
     
    // Add a input cue just to improve the UI slightly.
    SendMessage(hWndEdit, EM_SETCUEBANNER, 0, (LPARAM)L"Drag & Drop File Here Or Press Browse");
     
    // Set the extra styles needed for clipping the rendering around the browse button and
    // enabling the accept-files feature so we are a drop target.
    SetWindowLongPtr(hWndEdit, GWL_STYLE,
        GetWindowLongPtr(hWndEdit, GWL_STYLE) | WS_CLIPCHILDREN);
    SetWindowLongPtr(hWndEdit, GWL_EXSTYLE,
        GetWindowLongPtr(hWndEdit, GWL_EXSTYLE) | WS_EX_ACCEPTFILES);
     
    return TRUE;
}
 
// The sub-classed window procedure to add our extra functionality.
LRESULT CALLBACK FileInputBoxProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    LPFILEINPUTBOX lpFileInput = (LPFILEINPUTBOX)GetWindowLong(hWnd, GWLP_USERDATA);
    LRESULT lr;
     
    if(lpFileInput)
    {
        switch(uMsg)
        {
            // Set the margins on the edit control so that the input text does not interfere with
            // the browse button.
            case WM_SETFONT:
                {
                    // Forward the WM_SETFONT to the edit control and browse button.
                    lr = SafeCallWndProc(lpFileInput->wpOld, hWnd, uMsg, wParam, lParam);
                    SendMessage(lpFileInput->hWndBrowse, WM_SETFONT, wParam, lParam);
                     
                    // Get the text metrics of the new font.
                    TEXTMETRIC tmEdit;
                    HDC hdcEdit = GetDC(hWnd);
                    HFONT hfOld = (HFONT)SelectObject(hdcEdit, 
                        (HFONT)SendMessage(hWnd, WM_GETFONT, 0, 0));
                    GetTextMetrics(hdcEdit, &tmEdit);
                    SelectObject(hdcEdit, hfOld);
                    ReleaseDC(hWnd, hdcEdit);
 
                    // Get the ideal size for the browse button based on that font. 
                    lpFileInput->szBrowse.cx = 80;
                    SendMessage(lpFileInput->hWndBrowse, BCM_GETIDEALSIZE, 0, 
                        (LPARAM)&lpFileInput->szBrowse);
                    lpFileInput->szBrowse.cx += tmEdit.tmMaxCharWidth;
 
                    SendMessage(hWnd, EM_SETMARGINS, EC_LEFTMARGIN | 
                        EC_RIGHTMARGIN, MAKELONG(0, lpFileInput->szBrowse.cx));
                }
                return lr;
             
            // When the edit control is resized move our browse button so its always
            // on the right size of the control.
            case WM_WINDOWPOSCHANGING:
            case WM_WINDOWPOSCHANGED:
                {
                    RECT rc;
                    if(GetClientRect(hWnd, &rc)){
                        SetWindowPos(GetDlgItem(hWnd, IDC_BTNBROWSE), HWND_TOP,
                            rc.right - lpFileInput->szBrowse.cx, rc.top,
                            lpFileInput->szBrowse.cx, rc.bottom, SWP_NOACTIVATE);
                    }
                }
                // Pass this message onto the original control.
                break;
 
            // SHAutoComplete() does not give us the ability to dismiss the auto-complete
            // drop box. So we move the focus away and return it to dismiss the drop list.
            case WM_KEYUP:
                if(wParam == VK_RETURN){
                    SetFocus(GetParent(hWnd));
                    SetFocus(hWnd);
                }
                // Pass this message onto the original control.
                break;
                 
            // Received a command message
            case WM_COMMAND:
                // Did we receive a message from the browse button.
                if(LOWORD(wParam) == IDC_BTNBROWSE){
                    // Was the message a button click notification.
                    if(HIWORD(wParam) == BN_CLICKED){
                        GetWindowText(hWnd, lpFileInput->ofnSettings.lpstrFile,
                            lpFileInput->ofnSettings.nMaxFile);
                        if(PathIsDirectory(lpFileInput->ofnSettings.lpstrFile)){
                            PathAddBackslash(lpFileInput->ofnSettings.lpstrFile);
                            PathRenameExtension(lpFileInput->ofnSettings.lpstrFile, TEXT("*.*"));
                        }
                        if(GetOpenFileName(&lpFileInput->ofnSettings)){
                            SetWindowText(hWnd, lpFileInput->ofnSettings.lpstrFile);
                            SendMessage(hWnd, EM_SETSEL, 0, -1);
                        }
                    }
                    // DO NOT pass this message onto edit control, because our child button
                    // has nothing to do with the edit control itself. Passing it along might
                    // have erroneous effects if the control was not a default windows control.
                    return 0;
                }
                // If the WM_COMMAND was not for the browse button pass it along to the
                // original control.
                break;
             
            // Someone has dropped a file on our modified edit control.
            case WM_DROPFILES:
                {
                    HDROP hDrop = (HDROP)wParam;
                    HANDLE hHeap;
                    UINT nLength;
                    LPTSTR lpBuff;
                     
                    // See if they dropped more than 1 file. Perhaps a better solution would be
                    // to just use the first file in the drop list. But again.. the user might not
                    // expect this behavior.
                    if(DragQueryFile(hDrop, -1, NULL, 0) == 1){
                        nLength = DragQueryFile(hDrop, 0, NULL, 0) + 1;
                        hHeap   = GetProcessHeap();
                        lpBuff  = (LPTSTR)HeapAlloc(hHeap, HEAP_ZERO_MEMORY,
                            nLength * sizeof(TCHAR));
                        if(lpBuff){
                            // Get the first filepath into our buffer.
                            if(DragQueryFile(hDrop, 0, lpBuff, nLength)){
                                // Update our control with the new filepath
                                SetWindowText(hWnd, lpBuff);
                                SendMessage(hWnd, EM_SETSEL, 0, -1);
                            }
                            HeapFree(hHeap, 0, lpBuff);
                        }else{
                            MessageBeep(MB_ICONERROR);
                        }
                    }else{
                        // So lets beep if there is more than 1 file so the user is notified that
                        // there was a problem with the drop. You should not do anything in
                        // WM_DROPFILES that might remove the focus away from the drop source,
                        // this means no MessageBox().
                        MessageBeep(MB_ICONWARNING);
                    }
                }
                return 0;
             
            // Last message to the window
            case WM_NCDESTROY:
                {
                    // Free state structure.
                    HANDLE hHeap = GetProcessHeap();
                    WNDPROC wpOld = lpFileInput->wpOld;
                    SetWindowLong(hWnd, GWLP_USERDATA, 0);
                    HeapFree(hHeap, 0, lpFileInput->ofnSettings.lpstrFile);
                    HeapFree(hHeap, 0, lpFileInput);
                     
                    // Uninitialize/Dereference the COM library.
                    if(lpFileInput->bUsingCOM) CoUninitialize();
                     
                    // Remove our subclass and call the original windows procedure and return.
                    return SafeCallWndProc(SafeSubclassWindow(hWnd, wpOld),
                        hWnd, uMsg, wParam, lParam);
                }
        }
         
        // Safe way to call the original window procedure.
        return SafeCallWndProc(lpFileInput->wpOld, hWnd, uMsg, wParam, lParam);
    }
     
    // Call the default handler just in case there are problems.
    return SafeDefWindowProc(hWnd, uMsg, wParam, lParam);
}
 
WNDPROC SafeSubclassWindow(HWND hWnd, WNDPROC wpNew)
{
    if(IsWindowUnicode(hWnd))
        return (WNDPROC)SetWindowLongPtrW(hWnd, GWLP_WNDPROC, (LONG_PTR)wpNew);
    else
        return (WNDPROC)SetWindowLongPtrA(hWnd, GWLP_WNDPROC, (LONG_PTR)wpNew);
}
 
LRESULT SafeCallWndProc(WNDPROC wpOld, HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    if(IsWindowUnicode(hWnd))
        return CallWindowProcW(wpOld, hWnd, uMsg, wParam, lParam);
    else
        return CallWindowProcA(wpOld, hWnd, uMsg, wParam, lParam);
}
 
LRESULT SafeDefWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    if(IsWindowUnicode(hWnd))
        return DefWindowProcW(hWnd, uMsg, wParam, lParam);
    else
        return DefWindowProcA(hWnd, uMsg, wParam, lParam);
}