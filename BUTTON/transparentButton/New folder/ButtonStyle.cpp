// ButtonStyle.cpp : implementation file
//

#include "stdafx.h"
#include "ButtonStyle.h"
#include "Buttons.h"	

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CButtonStyle

CButtonStyle::CButtonStyle()
{
	nState=bMouse=TRUE;
	m_TextColorDark=m_TextColorLight=RGB(255,255,255);
	m_hIcon1=m_hIcon2=NULL;
	m_Selected = FALSE;
}

CButtonStyle::~CButtonStyle()
{
}

BEGIN_MESSAGE_MAP(CButtonStyle, CButton)
	//{{AFX_MSG_MAP(CButtonStyle)
	ON_WM_MOUSEMOVE()
	ON_WM_LBUTTONDOWN()
	ON_WM_LBUTTONUP()
	ON_WM_LBUTTONDBLCLK()
//	ON_WM_PAINT()
	//}}AFX_MSG_MAP
	ON_MESSAGE(WM_MOUSELEAVE, OnMouseLeave)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CButtonStyle message handlers

void CButtonStyle::DrawItem(LPDRAWITEMSTRUCT lpDIS) 
{
	CDC *pDC = CDC::FromHandle(lpDIS->hDC);

	CString sCaption;
	CRect rect=lpDIS->rcItem;

	GetWindowText(sCaption);
	pDC->SetBkMode(TRANSPARENT);

	CFont *oldFont = pDC->SelectObject(&m_Font);

	m_Selected = (lpDIS->itemState & ODS_SELECTED);

	if(m_hHdc!=NULL)
		PaintBG(pDC);

	switch(nState)
	{
	case 1:
		pDC->DrawIcon(0,3,m_hIcon1);
		pDC->SetTextColor(m_TextColorDark);
		pDC->DrawText(sCaption, rect, DT_SINGLELINE|DT_RIGHT|DT_VCENTER);
		break;
	case 2:
		pDC->DrawIcon(0,3,m_hIcon2);
		pDC->SetTextColor(m_TextColorLight);
		pDC->DrawText(sCaption, rect, DT_SINGLELINE|DT_RIGHT|DT_VCENTER);
		pDC->Draw3dRect(rect, ::GetSysColor(COLOR_BTNHILIGHT), ::GetSysColor(COLOR_BTNSHADOW));
		break;
	case 3:
		if(m_Selected)
		{
			pDC->DrawIcon(1,4,m_hIcon2);
			pDC->SetTextColor(m_TextColorLight);
			pDC->DrawText(sCaption, rect+CPoint(1,1), DT_SINGLELINE|DT_RIGHT|DT_VCENTER);
			pDC->Draw3dRect(rect, ::GetSysColor(COLOR_BTNSHADOW), ::GetSysColor(COLOR_BTNHILIGHT));
		}
		else
		{
			pDC->DrawIcon(0,3,m_hIcon2);
			pDC->SetTextColor(m_TextColorLight);
			pDC->DrawText(sCaption, rect, DT_SINGLELINE|DT_RIGHT|DT_VCENTER);
		}
		break;
	}
	pDC->SelectObject(oldFont);

}

void CButtonStyle::OnMouseMove(UINT nFlags, CPoint point)
{
	if(bMouse)
	{
		nState=2;
		Invalidate();
		bMouse=FALSE;
	}

	TRACKMOUSEEVENT tme;
	tme.cbSize = sizeof(TRACKMOUSEEVENT);
	tme.dwFlags = TME_LEAVE;
	tme.hwndTrack = m_hWnd;
	::_TrackMouseEvent(&tme);

	CButton::OnMouseMove(nFlags, point);
}

void CButtonStyle::OnLButtonDown(UINT nFlags, CPoint point) 
{
	nState=3;
	Invalidate();

	CButton::OnLButtonDown(nFlags, point);
}

void CButtonStyle::OnLButtonUp(UINT nFlags, CPoint point) 
{
	nState=2;
	Invalidate();

	CButton::OnLButtonUp(nFlags, point);
}

void CButtonStyle::OnLButtonDblClk(UINT nFlags, CPoint point) 
{
	SendMessage(WM_LBUTTONDOWN);	
	//CButton::OnLButtonDblClk(nFlags, point);
}

LRESULT CButtonStyle::OnMouseLeave(WPARAM wParam, LPARAM lParam)
{
	nState=1;
	bMouse=TRUE;
	Invalidate();
	
	return(0);
}

COLORREF CButtonStyle::SetTextColor(COLORREF DarkColor, COLORREF LightColor)
{
	m_TextColorDark=DarkColor;
	m_TextColorLight=LightColor;
		
	return (DarkColor);
}

void CButtonStyle::PaintBG(CDC *pDC)
{
	CClientDC dc(GetParent());
	CRect cltRect, wndRect;

	GetClientRect(&cltRect);
	GetWindowRect(&wndRect);
	GetParent()->ScreenToClient(&wndRect);

	::BitBlt(pDC->m_hDC, 0, 0, cltRect.Width(), cltRect.Height(), m_hHdc, wndRect.left, wndRect.top, SRCCOPY);
}

void CButtonStyle::SetTransparent()
{
		HBITMAP hBmp = ::LoadBitmap(::AfxGetInstanceHandle(), MAKEINTRESOURCE(IDB_BITMAP1));
		m_hHdc = ::CreateCompatibleDC(NULL);
		SelectObject(m_hHdc, hBmp);
}

void CButtonStyle::SetIcons(UINT nIcon1, UINT nIcon2)
{
	m_hIcon1 = ::LoadIcon(::AfxGetInstanceHandle(), MAKEINTRESOURCE(nIcon1));
	m_hIcon2 = ::LoadIcon(::AfxGetInstanceHandle(), MAKEINTRESOURCE(nIcon2));
}

void CButtonStyle::FontStyle(CString sFont)
{
	m_Font.CreateFont(10, 6, 0, 0, 600, 0, 0, 0, 0, 0, 0, 0,0, sFont);	
}
