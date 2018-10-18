// NProgressBar.cpp: Implementierungsdatei
//


/*
NProgressBar Version History:

24.6.2006
---------
- Repositioning bug fixed

27.05.2005
----------
- Fixed a lot of bugs!!!

25.05.2005
----------
- Progressbar is now centered and not streched in horizontal mode

16.05.2005
----------
- Changed code in PutWindowIntoTaskbar from ShowWindow(xxx) to RedrawWindow(...)
- SetPosEx changed

14.05.2005
----------
- First Release

*/


#include "stdafx.h"
#include "NProgressBar.h"
#include "NOperatingSystem.h"
#include "wnd.hpp"

/*
#ifdef BATTERYX
#include "..\\CRegistry.h"
#endif
*/

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

#define TIMER_EV 100

// Shell_TrayWnd
// |
// |------ ReBarWindow32
// |
// |------ TrayNotifyWnd

const char* CNProgressBar::m_pstcoReBarWindow = "ReBarWindow32";
const char* CNProgressBar::m_pstcoTrayNotifyWnd = "TrayNotifyWnd";
const char* CNProgressBar::m_pstcoParent = "Shell_TrayWnd";
int CNProgressBar::m_stnWindowWidth = 60;
int CNProgressBar::m_stnWindowHeight = 30;

/////////////////////////////////////////////////////////////////////////////
// CNProgressBar

CNProgressBar::CNProgressBar(CWnd* pcoParent)
{
	m_bOk = true;
	m_nMode = NOTDEFINED;
	m_nLeft=0;
	m_bSpezialColored = false;
	m_bHided = false;
	m_pcoParentWindow = pcoParent;


	m_ReBarWindowCurrentRect.left = m_ReBarWindowCurrentRect.right = m_ReBarWindowCurrentRect.top = m_ReBarWindowCurrentRect.bottom;

	m_pcoParent = new Wnd((char*)m_pstcoParent, NULL);
	m_pcoReBarWindow = new Wnd((char*)m_pstcoReBarWindow, m_pcoParent->GetHWnd());
	m_pcoTrayNotifyWnd = new Wnd((char*)m_pstcoTrayNotifyWnd, m_pcoParent->GetHWnd());

	
	CNOperatingSystem *pcoOS = new CNOperatingSystem();
	if (pcoOS->GetOS() < SBOS_NT4)
	{
		m_bOk = false;
	}
	delete pcoOS;
	 

	if (!(m_pcoReBarWindow->GetHWnd()))
	{
		::MessageBox(NULL,
					 m_pstcoReBarWindow,
					 "Can't find...",
					 MB_OK);
		m_bOk = false;
	}
	
	if (!(m_pcoReBarWindow->GetHWnd()))
	{
		::MessageBox(NULL,
					 m_pstcoTrayNotifyWnd,
					 "Can't find...",
					 MB_OK);
		m_bOk = false;
	}

	Wnd* m_pcoReBarWindow = new Wnd("msctls_progress32", m_pcoParent->GetHWnd());
	if (m_pcoReBarWindow->GetHWnd())
	{
		::MessageBox(NULL,
					 "I can place just one Progressbar in the Taskbar! Progressbar not created!",
					 "Control already exists",
					 MB_OK|MB_ICONWARNING);
		m_bOk = false;		
	}
	delete m_pcoReBarWindow;


	if (IsControlSuccessfullyCreated())
	{	
	
		this->StartUp();
		this->SetBkColour(RGB(0, 0, 0));
		this->SetForeColour(RGB(0,255,0));
		this->SetTextBkColour(RGB(0,0,0));
		this->SetTextForeColour(RGB(255,255,255));
		this->SetRange32(0, 100);
	}
}

CNProgressBar::~CNProgressBar()
{
	if (IsControlSuccessfullyCreated())
	{
		this->ReModifyTaskbar();	
	}
	delete m_pcoReBarWindow;
	delete m_pcoTrayNotifyWnd;
	delete m_pcoParent;
}


BEGIN_MESSAGE_MAP(CNProgressBar, CTextProgressCtrl)
	//{{AFX_MSG_MAP(CNProgressBar)
	ON_WM_TIMER()
	ON_WM_RBUTTONDOWN()
	ON_WM_LBUTTONDBLCLK()
	ON_WM_LBUTTONDOWN()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// Behandlungsroutinen für Nachrichten CNProgressBar 

void CNProgressBar::StartUp()
{
	this->ModifyTaskbar();
	this->PutWindowIntoTaskbar(m_pcoReBarWindow->GetProportion());	
	this->SetTimer(TIMER_EV, 500, 0);
}


bool CNProgressBar::IsControlSuccessfullyCreated()
{
	return m_bOk;
}

void CNProgressBar::PutWindowIntoTaskbar(int nDirection)
{
	RECT coRectReBar;
	CRect coRect;

	coRectReBar = m_pcoReBarWindow->GetRect();	

	int nDiff = 3;
	int nRightSmaller = 5;
	int nCenter = 0;

	if (m_pcoReBarWindow->GetProportion() == HORIZONTAL)
	{		
		m_nHeightTskControl =  m_pcoReBarWindow->GetRect().bottom - m_pcoReBarWindow->GetRect().top;
		if (m_nHeightTskControl > m_stnWindowHeight)
		{
			nCenter = (m_nHeightTskControl - m_stnWindowHeight) / 2;
			m_nHeightTskControl = m_stnWindowHeight ;			
		}

		nDiff = m_nHeightTskControl / 10;

		coRect.SetRect(coRectReBar.right,
					0+nDiff+nCenter, 
					coRectReBar.right+m_stnWindowWidth-nRightSmaller,
					m_nHeightTskControl-nDiff+nCenter);
							
		if (m_bCreated != true)
		{
			this->Create(PBS_SMOOTH | WS_CHILD | WS_VISIBLE  , coRect, CWnd::FromHandle(m_pcoParent->GetHWnd()),NULL);	
			m_bCreated = true;
		}
		else
		{
			this->MoveWindow(coRect.left, 
				             coRect.top, 
							 m_stnWindowWidth-nRightSmaller, 
							 coRect.bottom-coRect.top, 
							 TRUE);			

			this->RedrawWindow(NULL,NULL,RDW_UPDATENOW|RDW_FRAME|RDW_INVALIDATE );
		}
	}
	else if (m_pcoReBarWindow->GetProportion() == VERTICAL)
	{		

		coRect.SetRect(0+nDiff,
					coRectReBar.bottom, 
					coRectReBar.right-coRectReBar.left-nDiff,
					coRectReBar.bottom+m_stnWindowHeight-nRightSmaller);
							
		if (m_bCreated != true)
		{
			this->Create(PBS_SMOOTH | WS_CHILD | WS_VISIBLE  , coRect, CWnd::FromHandle(m_pcoParent->GetHWnd()),NULL);	
			m_bCreated = true;
		}
		else
		{
			this->MoveWindow(coRect.left, 
				             coRect.top, 
							 coRect.right-coRect.left, 
							 m_stnWindowHeight-nRightSmaller, 
							 TRUE);

			this->RedrawWindow(NULL,NULL,RDW_UPDATENOW|RDW_FRAME|RDW_INVALIDATE );
		}		
	}
} 

void CNProgressBar::ModifyTaskbar()
{	
	RECT coReBarWindowRect = m_pcoReBarWindow->GetRect();
	RECT coTrayNotifyWndRect = m_pcoTrayNotifyWnd->GetRect();	

	if (m_pcoReBarWindow->GetProportion() == HORIZONTAL)
	{ 
		//if ((coReBarWindowRect.left-m_ReBarWindowCurrentRect.left) > 2) // Avoid the running taskbar bug in non-skinned windows mode
		//{
			m_ReBarWindowCurrentRect.left = coReBarWindowRect.left + m_pcoParent->GetRect().left;
		//}

		m_ReBarWindowCurrentRect.right = coTrayNotifyWndRect.left-coReBarWindowRect.left-m_stnWindowWidth + m_pcoParent->GetRect().left;
		m_ReBarWindowCurrentRect.bottom = coReBarWindowRect.bottom-coReBarWindowRect.top;		
		m_ReBarWindowCurrentRect.top = 0;

		m_nMode = HORIZONTAL;		
	}
	else if (m_pcoReBarWindow->GetProportion() == VERTICAL)
	{		
		m_ReBarWindowCurrentRect.left =  0;
		m_ReBarWindowCurrentRect.right = coTrayNotifyWndRect.right-coTrayNotifyWndRect.left;

		//if ((coReBarWindowRect.top-m_ReBarWindowCurrentRect.top) > 2) // Avoid the running taskbar bug in non-skinned windows mode
		//{
			m_ReBarWindowCurrentRect.top = coReBarWindowRect.top + m_pcoParent->GetRect().top;
		//}

		m_ReBarWindowCurrentRect.bottom = coTrayNotifyWndRect.top-m_stnWindowHeight-coReBarWindowRect.top + m_pcoParent->GetRect().top;		
		
		m_nMode = VERTICAL;	
	}

	::MoveWindow(m_pcoReBarWindow->GetHWnd(), 
						m_ReBarWindowCurrentRect.left, 
						m_ReBarWindowCurrentRect.top, 
						m_ReBarWindowCurrentRect.right, 
						m_ReBarWindowCurrentRect.bottom, 
						TRUE);	

}

void CNProgressBar::ReModifyTaskbar()
{
	RECT coReBarWindowRect = m_pcoReBarWindow->GetRect();
	RECT coTrayNotifyWndRect = m_pcoTrayNotifyWnd->GetRect();

	if (m_nMode == HORIZONTAL)
	{
		//if ((coReBarWindowRect.left-m_ReBarWindowCurrentRect.left) > 2) // Avoid the running taskbar bug in non-skinned windows mode
		//{
			m_ReBarWindowCurrentRect.left =  coReBarWindowRect.left + m_pcoParent->GetRect().left;
		//}
		m_ReBarWindowCurrentRect.right = m_pcoTrayNotifyWnd->GetRect().left-coReBarWindowRect.left;
		m_ReBarWindowCurrentRect.top = 0;
		m_ReBarWindowCurrentRect.bottom = coReBarWindowRect.bottom-coReBarWindowRect.top;		
	}
	else if (m_nMode == VERTICAL)
	{
		m_ReBarWindowCurrentRect.left =  0;
		m_ReBarWindowCurrentRect.right = coTrayNotifyWndRect.right-coTrayNotifyWndRect.left;
		//if ((coReBarWindowRect.top-m_ReBarWindowCurrentRect.top) > 2) // Avoid the running taskbar bug in non-skinned windows mode
		//{
			m_ReBarWindowCurrentRect.top = coReBarWindowRect.top + m_pcoParent->GetRect().top;
		//}
		m_ReBarWindowCurrentRect.bottom = coTrayNotifyWndRect.top-coReBarWindowRect.top;		
	}

	::MoveWindow(m_pcoReBarWindow->GetHWnd(), 
				   m_ReBarWindowCurrentRect.left, 
				   m_ReBarWindowCurrentRect.top, 
				   m_ReBarWindowCurrentRect.right, 
				   m_ReBarWindowCurrentRect.bottom, 
				   TRUE);	
}

void CNProgressBar::OnTimer(UINT nIDEvent) 
{
	switch (nIDEvent)
	{
	case TIMER_EV:
		if (!m_bHided)
		{
			
			// BUGFIX 24.6.2006	

			if (m_pcoParent->GetProportion() == HORIZONTAL)
			{
				if ( ((m_pcoTrayNotifyWnd->GetRect().left - m_pcoReBarWindow->GetRect().right) < m_stnWindowWidth) ||
					 (m_pcoTrayNotifyWnd->IsRectChanged() ||
					  m_pcoParent->IsRectChanged())
					  )
				{
					this->Refresh();
					this->PutWindowIntoTaskbar(m_pcoReBarWindow->GetProportion());							
				}
				else
				{
					this->RedrawWindow();
				}
			}
			else
			{
				if ( ((m_pcoTrayNotifyWnd->GetRect().top - m_pcoReBarWindow->GetRect().bottom) < m_stnWindowHeight) ||
					 (m_pcoTrayNotifyWnd->IsRectChanged() ||
					  m_pcoParent->IsRectChanged())
					 )
				{
					this->Refresh();
					this->PutWindowIntoTaskbar(m_pcoReBarWindow->GetProportion());							
				}
				else
				{
					this->RedrawWindow();
				}
			}


			/*
			if (m_pcoTrayNotifyWnd->IsRectChanged() ||
				m_pcoParent->IsRectChanged()) // If an icon is added, ...
			{
				//this->ModifyTaskbar();
				this->Refresh();
				this->PutWindowIntoTaskbar(m_pcoReBarWindow->GetProportion());		
			}
			else
			{
				//this->Refresh();		
				this->RedrawWindow();
			}
			*/
		}
		break;
	};
	
	CTextProgressCtrl::OnTimer(nIDEvent);
}


void CNProgressBar::Refresh()
{
	this->ModifyTaskbar();
	this->RedrawWindow();
}

void CNProgressBar::SetPosEx(int nPos)
{
	int r, g;
	
	if (IsControlSuccessfullyCreated())
	{
		if (m_bSpezialColored)
		{
			if (nPos<0) nPos = 0;
			if (nPos>255) nPos = 255;
			r=(int)((100-nPos)*5.1);
			g=(int)(nPos*5.1);
			if (g>255) g=255;
			if (r>255) r=255;
			if (g<0) g=0;
			if (r<0) r=0;
			this->SetForeColour(RGB(r,g,0));
		}
		else
		{ 
			this->SetBkColour(RGB(0, 0, 0));
			this->SetForeColour(RGB(0,255,0));
			this->SetTextBkColour(RGB(0,0,0));
			this->SetTextForeColour(RGB(255,255,255));
		}

		this->SetPos(nPos);
		this->RedrawWindow();
	}
}


void CNProgressBar::Show(bool bShow)
{
	if (!bShow)
	{
		this->ShowWindow(false);
		KillTimer(TIMER_EV);
		ReModifyTaskbar();
		m_bHided = true;

	}
	else
	{
		this->ShowWindow(true);
		this->ModifyTaskbar();
		this->PutWindowIntoTaskbar(m_pcoReBarWindow->GetProportion());	
		this->SetTimer(TIMER_EV, 1000, 0);		
		m_bHided = false;
	}
}

bool CNProgressBar::IsHided()
{
	return m_bHided;
}

void CNProgressBar::SetColorMode(bool bColorMode)
{
	m_bSpezialColored = bColorMode;
	this->RedrawWindow();
}

void CNProgressBar::OnRButtonDown(UINT nFlags, CPoint point) 
{
	//this->MessageBox("Here should be the context menu", "TODO");

	CTextProgressCtrl::OnRButtonDown(nFlags, point);
}

void CNProgressBar::OnLButtonDblClk(UINT nFlags, CPoint point) 
{
	//this->m_pcoParentWindow->ShowWindow(SW_RESTORE);
	//this->Show(false);	

	CTextProgressCtrl::OnLButtonDblClk(nFlags, point);
}

void CNProgressBar::OnLButtonDown(UINT nFlags, CPoint point) 
{
/*
#ifdef BATTERYX

	this->m_pcoParentWindow->ShowWindow(SW_RESTORE);
	this->Show(false);		
	CRegistry::SetInTaskbar(false);

#endif	
*/
	CTextProgressCtrl::OnLButtonDown(nFlags, point);
}
