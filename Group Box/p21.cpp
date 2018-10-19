#include <Windows.h>
#include <tchar.h>

TCHAR szWndClass[] = _T("WndClass");
TCHAR szWndTitle[] = _T("GroupBox (BS_GROUPBOX)");

HINSTANCE hInstance;

HWND hWndMain;

#define IDB_BUTTON1 1

LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);
LRESULT CALLBACK GroupBoxProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
WNDPROC OldGroupBoxProc;

//	int WINAPI WinMain(HINSTANCE hInst, HINSTANCE hPrevInst,LPSTR lpCmdLine, int nCmdShow)
void main()
{  
	
	int nCmdShow;
	HINSTANCE hInst;


	WNDCLASS wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)COLOR_WINDOW;
	wc.hCursor = LoadCursor(0, IDC_ARROW);
	wc.hIcon = 0;
	wc.hInstance = 0;
	wc.lpfnWndProc = WndProc;
	wc.lpszClassName = szWndClass;
	wc.lpszMenuName = 0;
	wc.style = 0;

	RegisterClass(&wc);

	hWndMain = CreateWindow(wc.lpszClassName,L"Main",WS_VISIBLE|WS_OVERLAPPEDWINDOW|WS_CLIPCHILDREN,10,10,700,500,0,0,0,0);

	

	MSG msg;
	while(GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}


}

LRESULT CALLBACK WndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	int wId;

	static HWND hWndGroupBox;

	switch (uMsg)
	{
	case WM_CREATE:
		hWndGroupBox = CreateWindow(_T("BUTTON"), _T("ქართული"), WS_CHILD | WS_VISIBLE | BS_GROUPBOX,
					 10, 10, 400, 300,
					 hWnd, 0, hInstance, 0);

		CreateWindow(_T("BUTTON"), _T("საქართველო"), WS_CHILD | WS_VISIBLE,
					 10, 20, 200, 25,
					 hWndGroupBox, (HMENU)IDB_BUTTON1, hInstance, 0);

		OldGroupBoxProc = (WNDPROC)GetWindowLongPtr(hWndGroupBox, GWLP_WNDPROC);
		SetWindowLongPtr(hWndGroupBox, GWLP_WNDPROC, (LONG)GroupBoxProc);

		break;
	case WM_COMMAND:
		wId = LOWORD(wParam);
		switch(wId)
		{
		case IDB_BUTTON1:
			MessageBox(0, _T("საქართველოო!!!!"), _T(""), MB_OK);
			break;
		}
		break;
	case WM_DESTROY:
		PostQuitMessage(0);
		break;
	default:
		return DefWindowProc(hWnd, uMsg, wParam, lParam);
	}
}

LRESULT CALLBACK GroupBoxProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    switch(uMsg)
    {
        case WM_COMMAND:
			CallWindowProc(WndProc, GetParent(hWnd), uMsg, wParam, lParam);
            break;
    }

    return CallWindowProc(OldGroupBoxProc, hWnd, uMsg, wParam, lParam);
}