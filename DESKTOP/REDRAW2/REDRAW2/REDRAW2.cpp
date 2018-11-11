#include "FloatingWindow.h"
#include "resource.h"
#include <Windows.h>
#include <tchar.h>
#define MYWAY
#ifdef MYWAY


bool		FloatingWindow::isRegistered	= false;
COLORREF	FloatingWindow::crefBackGround	= NULL;
HBRUSH		FloatingWindow::hbrBackGround = NULL;
WNDCLASSEX	FloatingWindow::wc = {NULL};

LRESULT CALLBACK FloatingWindowProc( HWND hwnd , UINT msg , WPARAM wparam , LPARAM lparam);


bool	FloatingWindow::RegisterFloatingWindowClass()
{
	FloatingWindow::crefBackGround		= RGB (255,255,0);
	FloatingWindow::hbrBackGround		= CreateSolidBrush(FloatingWindow::crefBackGround);

	FloatingWindow::wc	.cbClsExtra = 0;
	wc.cbSize = sizeof(wc);
	wc.cbWndExtra = 0;
	wc.hbrBackground = hbrBackGround;
	wc.hCursor = LoadCursor(NULL, IDC_ARROW);
	wc.hIcon = LoadIcon(GetModuleHandle(0) , MAKEINTRESOURCE(IDI_ICON1));
	wc.hIconSm = LoadIcon(GetModuleHandle(0) , MAKEINTRESOURCE(IDI_ICON1SM));
	wc.hInstance = GetModuleHandle(NULL);
	wc.lpfnWndProc = FloatingWindowProc;
	wc.lpszClassName = _T("FloatingWindowClass");
	wc.lpszMenuName = NULL;
	wc.style = NULL;

	if( RegisterClassEx( &wc ) == NULL)
		return false;
	else
		return true;
}


FloatingWindow::FloatingWindow(void)
{
	if( this->isRegistered == false ) // register the class only when the first object gets created
	{
		if ( this->RegisterFloatingWindowClass() == false )
		{
			return;
		}
		this->isRegistered = true;
	}

	this->hbmImage = LoadBitmap(GetModuleHandle(NULL) , MAKEINTRESOURCE(IDB_BITMAP1));
	GetObject( this->hbmImage , sizeof(BITMAP) , &bm);

	RECT rcDesktop;
	SystemParametersInfo(SPI_GETWORKAREA , NULL , &rcDesktop , NULL);

	this->hwnd = CreateWindowEx(	WS_EX_LAYERED ,
					_T("FloatingWindowClass"),
					NULL,
					WS_POPUP,
					rcDesktop.left , rcDesktop.top ,
					rcDesktop.right - rcDesktop.left , rcDesktop.bottom - rcDesktop.top,
					NULL , NULL , GetModuleHandle(NULL) , NULL);
	
	

	if( this->hwnd == NULL )
		PostQuitMessage(-1);

	ShowWindow(this->hwnd , SW_SHOW );
	SetWindowPos(	this->hwnd , HWND_BOTTOM , 0 , 0 , 0 , 0 , SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE );
	SetLayeredWindowAttributes(hwnd , this->crefBackGround , 0 , LWA_COLORKEY);
	UpdateWindow(this->hwnd);


	this->Pos.X = 0;
	this->Pos.Y = 0;

	this->delta.X = 5;
	this->delta.Y = 5;

	this->hdcMem = NULL;
	this->ID_StoredDC = NULL;
}


FloatingWindow::~FloatingWindow(void)
{
	DeleteDC(this->hdcMem);
	RestoreDC(this->hdcMem , this->ID_StoredDC);

	DeleteObject(this->hbmImage);
}


void FloatingWindow::UpdateStatus()
{
	// Redraw the old position of the image to erase it.
	RECT rc = { this->Pos.X , this->Pos.Y , this->Pos.X + this->bm.bmWidth , this->Pos.Y + this->bm.bmHeight };
	RedrawWindow(this->hwnd , &rc , NULL , RDW_ERASE |  RDW_INVALIDATE | RDW_UPDATENOW );


	this->UpdatePosition();

	this->DrawImage();

}



void FloatingWindow::UpdatePosition()
{
	RECT rcWindow;
	SystemParametersInfo(SPI_GETWORKAREA , NULL , &rcWindow , NULL);


	// if the bitmap reaches the border of the window, reverse it move direction.
	if( (this->Pos.X < 0) || ((this->Pos.X + this->bm.bmWidth) > rcWindow.right) )
		this->delta.X = - this->delta.X;

	if( (this->Pos.Y < 0) || ((this->Pos.Y + this->bm.bmHeight) > rcWindow.bottom) )
		this->delta.Y = - this->delta.Y;


	
	this->Pos.X += this->delta.X;
	this->Pos.Y += this->delta.Y;
}


void FloatingWindow::DrawImage()
{
	HDC hdcWnd = GetDC(this->hwnd);
	this->hdcMem = CreateCompatibleDC(hdcWnd);
	this->ID_StoredDC = SaveDC(this->hdcMem);

	SelectObject(this->hdcMem , this->hbmImage);

	BitBlt(hdcWnd , this->Pos.X , this->Pos.Y , this->bm.bmWidth , this->bm.bmHeight , this->hdcMem , 0 , 0 , SRCCOPY);

	DeleteDC(this->hdcMem);
	RestoreDC(this->hdcMem , this->ID_StoredDC);
	ReleaseDC(this->hwnd , hdcWnd);

}
#endif 