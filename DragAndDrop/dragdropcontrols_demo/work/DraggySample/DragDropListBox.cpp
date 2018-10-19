#include "stdafx.h"
#include <afxole.h>         // MFC OLE classes
#include <afxodlgs.h>       // MFC OLE dialog classes
#include <afxdisp.h >       // MFC OLE automation classes
#include <afxpriv.h>

#include "DraggySample.h"
#include "DragDropListBox.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

IMPLEMENT_DYNAMIC(CDragDropListBox, CListBox)	//	Want info for drag

#define	LISTBOX_EVENT		2
#define	LISTBOX_EVENT_TIME	100

#define	MINIMUM_MOVE_SQUARE	100	//	10 pixels

// *****************************************************************
// CDragDropListBox
// *****************************************************************
CDragDropListBox::CDragDropListBox()
	:	m_TargetDrop(NULL),
		m_TimerID(0)
{
	m_StartPoint = CPoint(-1, -1);

	m_TargetDrop = new ECMaterialDropText();
}

CDragDropListBox::~CDragDropListBox()
{
}

BEGIN_MESSAGE_MAP(CDragDropListBox, CListBox)
	//{{AFX_MSG_MAP(CDragDropListBox)
	ON_WM_LBUTTONDOWN()
	ON_WM_LBUTTONUP()
	ON_WM_MOUSEMOVE()
	ON_WM_TIMER()
	ON_WM_DESTROY()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

void CDragDropListBox::OnDestroy() 
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

	CListBox::OnDestroy();	
}

// *****************************************************************
//	InitDrag
//		Init us as a drag and drop control. The h_Wnd MUST be valid now
// *****************************************************************
void CDragDropListBox::InitDrag()
{
	//	register us as a drag/drop client
	if(m_TargetDrop)
		m_TargetDrop->Register(this, CF_TEXT);
}

// *****************************************************************
// CDragDropListBox message handlers
// *****************************************************************
void CDragDropListBox::OnLButtonDown(UINT nFlags, CPoint point) 
{
	//	keep start point
	m_StartPoint = 	point;

	//	start a timer
	m_TimerID = SetTimer(LISTBOX_EVENT, LISTBOX_EVENT_TIME, NULL);
	
	CListBox::OnLButtonDown(nFlags, point);
}

void CDragDropListBox::OnLButtonUp(UINT nFlags, CPoint point) 
{
	m_StartPoint.x = -100;
	m_StartPoint.y = -100;
	
	if(m_TimerID)
	{
		KillTimer(m_TimerID);
		m_TimerID = 0;
	}
	
	CListBox::OnLButtonUp(nFlags, point);
}

// *****************************************************************
//	OnMouseMove
// *****************************************************************
void CDragDropListBox::OnMouseMove(UINT nFlags, CPoint point) 
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
				GetText(GetCurSel(), iText);

				//	wanne move something (and have mem)
				if(iText.GetLength())
				{
					//	write name to clipboard
					sf.Write(iText, iText.GetLength());

					HGLOBAL hMem = sf.Detach();
					if (!hMem) 
						return;
					pSource->CacheGlobalData(CF_TEXT, hMem);

					//	Do drag and drop!
					pSource->DoDragDrop();
				}

				//	free source
				delete pSource;
			}
		}
	}
	
	CListBox::OnMouseMove(nFlags, point);
}

// *****************************************************************
//	OnTimer
// *****************************************************************
void CDragDropListBox::OnTimer(UINT nIDEvent) 
{
	//	check if mouse is still in rect
	if(nIDEvent == LISTBOX_EVENT)
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
	
	CListBox::OnTimer(nIDEvent);
}