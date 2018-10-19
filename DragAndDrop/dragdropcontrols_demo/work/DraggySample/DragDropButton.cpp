#include "stdafx.h"

#include <afxole.h>         // MFC OLE classes
#include <afxodlgs.h>       // MFC OLE dialog classes
#include <afxdisp.h >       // MFC OLE automation classes
#include <afxpriv.h>

#include "DraggySample.h"
#include "DragDropButton.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

IMPLEMENT_DYNAMIC(CDragDropButton, CButton)	//	Want info for drag

#define	BUTTON_EVENT		1
#define	BUTTON_EVENT_TIME	100

#define	MINIMUM_MOVE_SQUARE	100	//	10 pixels

// *****************************************************************
// CDragDropButton
// *****************************************************************
CDragDropButton::CDragDropButton()
	:	m_TargetDrop(NULL),
		m_TimerID(0)
{
	m_StartPoint = CPoint(-1, -1);

	//	make us
	m_TargetDrop = new ECMaterialDropText();
}

CDragDropButton::~CDragDropButton()
{
}

BEGIN_MESSAGE_MAP(CDragDropButton, CButton)
	//{{AFX_MSG_MAP(CDragDropButton)
	ON_WM_LBUTTONUP()
	ON_WM_LBUTTONDOWN()
	ON_WM_TIMER()
	ON_WM_MOUSEMOVE()
	ON_WM_DESTROY()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

void CDragDropButton::OnDestroy() 
{
	//	could use the release. 
	if(m_TargetDrop)
	{
		//	stop drag and drop
		m_TargetDrop->Revoke();

		//	KILL!
		delete m_TargetDrop;
	}
	m_TargetDrop = NULL;

	CButton::OnDestroy();
}

// *****************************************************************
//	InitDrag
//		Init us as a drag and drop control. The h_Wnd MUST be valid now
// *****************************************************************
void CDragDropButton::InitDrag()
{
	//	register us as a drag/drop client
	if(m_TargetDrop)
		m_TargetDrop->Register(this, CF_TEXT);
}

// *****************************************************************
// CDragDropButton message handlers
// *****************************************************************
void CDragDropButton::OnLButtonDown(UINT nFlags, CPoint point) 
{
	//	keep start point
	m_StartPoint = 	point;

	//	start a timer
	m_TimerID = SetTimer(BUTTON_EVENT, BUTTON_EVENT_TIME, NULL);
	
	CButton::OnLButtonDown(nFlags, point);
}

void CDragDropButton::OnLButtonUp(UINT nFlags, CPoint point) 
{
	m_StartPoint.x = -100;
	m_StartPoint.y = -100;
	
	if(m_TimerID)
	{
		KillTimer(m_TimerID);
		m_TimerID = 0;
	}

	CButton::OnLButtonUp(nFlags, point);
}

// *****************************************************************
//	OnTimer
//		Need this to check if we are still in button...
// *****************************************************************
void CDragDropButton::OnTimer(UINT nIDEvent) 
{
	//	check if mouse is still in rect
	if(nIDEvent == BUTTON_EVENT)
	{
		POINT pt;
		::GetCursorPos(&pt);
		CRect iRect;
		GetWindowRect(iRect);
		if(!(iRect.PtInRect(pt)))
		{
			KillTimer(nIDEvent);
			m_TimerID = 0;
		}
	}
	
	CButton::OnTimer(nIDEvent);
}

// *****************************************************************
//	OnMouseMove
// *****************************************************************
void CDragDropButton::OnMouseMove(UINT nFlags, CPoint point) 
{
	if(m_TimerID > 0)
	{
		//	check if we really moved enough
		int iX = m_StartPoint.x - point.x;
		int iY = m_StartPoint.y - point.y;
		if((iX*iX + iY*iY) > MINIMUM_MOVE_SQUARE)
		{
			COleDataSource*	pSource = new COleDataSource();
			if(pSource)
			{
				CSharedFile	sf(GMEM_MOVEABLE|GMEM_DDESHARE|GMEM_ZEROINIT);
				CString iText;

				//	lets move our text
				GetWindowText(iText);

				//	write name to clipboard
				sf.Write(iText, iText.GetLength());

				HGLOBAL hMem = sf.Detach();
				if (!hMem) 
					return;
				pSource->CacheGlobalData(CF_TEXT, hMem);

				//	Do drag and drop!
				pSource->DoDragDrop();

				//	free source
				delete pSource;
			}
		}
	}
	
	CButton::OnMouseMove(nFlags, point);
}

