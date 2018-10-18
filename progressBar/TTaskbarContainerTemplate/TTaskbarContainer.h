#if !defined(AFX_TTASKBARCONTAINER_H__432FF06E_0462_4C88_9DB9_9C36B32DBAD2__INCLUDED_)
#define AFX_TTASKBARCONTAINER_H__432FF06E_0462_4C88_9DB9_9C36B32DBAD2__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

///////////////////////////////////////////////////////////////////////////////
//
// Copyright 2007 Nautilus Master Software Solutions
//
// Nautilus Master Software Solutions grants you ("Licensee") a non-exclusive, 
// royalty free, licence to use, modify and redistribute this software in 
// source and binary code form, provided that 
//   -) this copyright notice and licence appear on all copies of the software;
//   -) Licensee does not utilize the software in a manner which is disparaging 
//      to Nautilus Master Software Solutions.
// This software is provided "AS IS," without a warranty of any kind. ALL
// EXPRESS OR IMPLIED CONDITIONS, REPRESENTATIONS AND WARRANTIES, INCLUDING 
// ANY IMPLIED WARRANTY OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE 
// OR NON-INFRINGEMENT, ARE HEREBY EXCLUDED. NAUTILUS MASTER SOFTWARE SOLUTIONS 
// AND ITS LICENSORS SHALL NOT BE LIABLE FOR ANY DAMAGES SUFFERED BY LICENSEE 
// AS A RESULT OF USING, MODIFYING OR DISTRIBUTING THE SOFTWARE OR ITS 
// DERIVATIVES. IN NO EVENT WILL NAUTILUS MASTER SOFTWARE SOLUTIONS BE LIABLE 
// FOR ANY LOST REVENUE, PROFIT OR DATA, OR FOR DIRECT, INDIRECT, SPECIAL, 
// CONSEQUENTIAL, INCIDENTAL OR PUNITIVE DAMAGES, HOWEVER CAUSED AND REGARDLESS 
// OF THE THEORY OF LIABILITY, ARISING OUT OF THE USE OF OR INABILITY TO USE 
// SOFTWARE, EVEN IF NAUTIULUS MASTER SOFTWARE SOLUTIONS HAS BEEN ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGES.
//
///////////////////////////////////////////////////////////////////////////////


///////////////////////////////////////////////////////////////////////////////
// TTaskbarContainer Version History
//
// 2007-03-31
// ----------
// - Class name changed from CNProgressbar to TaskbarContainer
// - Changed the class to a template class
//
// 2007-03-06
// ----------
// - Previous progressbar detached
//
// 2007-02-27
// ----------
// - Bugfixs
// - Started redesign
// - Bug with 100% cpu load on mousehover or click fixed. This happened before if the control 
//   was enabled. With SS_NOTIFY it should be fixed
//
// 24.6.2006
// ---------
// - Repositioning bug fixed
//
// 27.05.2005
// ----------
// - Fixed a lot of bugs!!!
//
// 25.05.2005
// ----------
// - Progressbar is now centered and not streched in horizontal mode
//
// 16.05.2005
// ----------
// - Changed code in PutWindowIntoTaskbar from ShowWindow(xxx) to RedrawWindow(...)
// - SetPosEx changed
//
// 14.05.2005
// ----------
// - First Release
//
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Header

#include "stdafx.h"
#include "globals.hpp"
#include "NOperatingSystem.h"
#include "wnd.hpp"
#include "TemplateAFX.hpp"


///////////////////////////////////////////////////////////////////////////////
// Defines

#define REBARWND "ReBarWindow32"
#define TRAYNOTIFYWND "TrayNotifyWnd"
#define PARENTWND "Shell_TrayWnd"
#define TIMER_EV 100
#define TEMPLATE(t1) t1
#define TCLASS(theClass, t1) theClass<t1>

///////////////////////////////////////////////////////////////////////////////
// Declarations

class Wnd;

///////////////////////////////////////////////////////////////////////////////
// Class definition

template <class T>
class TTaskbarContainer : public T
{
public:
	TTaskbarContainer(CWnd* pcoParent);
	void Show(bool bShow);
	void Refresh();	
	bool IsHided();
	bool IsControlSuccessfullyCreated();
	void SetWidth(int nWidth);
	void SetHeight(int nHeight);

public:
// Überschreibungen
	// Vom Klassen-Assistenten generierte virtuelle Funktionsüberschreibungen
	//{{AFX_VIRTUAL(TTaskbarContainer)
	//}}AFX_VIRTUAL

public:
	virtual ~TTaskbarContainer();
	
protected:
	//{{AFX_MSG(TTaskbarContainer)
	afx_msg void OnTimer(UINT nIDEvent);	
	//}}AFX_MSG

	DECLARE_MESSAGE_MAP()

private:
	CWnd* m_pcoParentWindow;
	bool  m_bOk;
	bool  m_bWindowSet;
	bool  m_bSet;
	bool  m_bCreated;
	int   m_nMode;
	int   m_nLeft;
	int   m_nHeightTskControl;
	int   m_stnWindowWidth;
	int   m_stnWindowHeight;
	CRect m_ReBarWindowCurrentRect;
	Wnd*  m_pcoReBarWindow;
	Wnd*  m_pcoTrayNotifyWnd;
	Wnd*  m_pcoParent;
	bool  m_bSpezialColored;
	bool  m_bHided;
	bool  m_bTaskbarSizeChanged;

private:
	void  StartUp();
	void  PutWindowIntoTaskbar(int nDirection);
	void  ModifyTaskbar();
	void  ReModifyTaskbar();
	void  RefreshControl();

};


//BEGIN_TEMPLATE_MESSAGE_MAP(TEMPLATE_1(class T), TCLASS_1(TTaskbarContainer, T), CStatic)
BEGIN_TEMPLATE_MESSAGE_MAP(TEMPLATE(class T), TCLASS(TTaskbarContainer, T), CWnd)
	//{{AFX_MSG_MAP(TTaskbarContainer)
	ON_WM_TIMER()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()


///////////////////////////////////////////////////////////////////////////////
// Methods

template <class T>
TTaskbarContainer<T>::TTaskbarContainer(CWnd* pcoParent) : m_stnWindowWidth(60), m_stnWindowHeight(30)
{
	m_bOk = true;
	m_nMode = NOTDEFINED;
	m_nLeft=0;
	m_bSpezialColored = false;
	m_bHided = false;
	m_pcoParentWindow = pcoParent;

	this->m_bCreated = false;
	m_ReBarWindowCurrentRect.left = m_ReBarWindowCurrentRect.right = m_ReBarWindowCurrentRect.top = m_ReBarWindowCurrentRect.bottom;
	m_pcoParent = new Wnd((char*)PARENTWND, NULL);
	m_pcoReBarWindow = new Wnd((char*)REBARWND, m_pcoParent->GetHWnd());
	m_pcoTrayNotifyWnd = new Wnd((char*)TRAYNOTIFYWND, m_pcoParent->GetHWnd());

	CNOperatingSystem *pcoOS = new CNOperatingSystem();
	if (pcoOS->GetOS() < SBOS_NT4)
	{
		m_bOk = false;
	}
	delete pcoOS;
		 
	if (!(m_pcoReBarWindow->GetHWnd()))
	{
		::MessageBox(NULL,
					 REBARWND,
					 "Can't find...",
					 MB_OK);
		m_bOk = false;
	}
		
	if (!(m_pcoReBarWindow->GetHWnd()))
	{
		::MessageBox(NULL,
					 TRAYNOTIFYWND,
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
	}
}

template <class T>
void TTaskbarContainer<T>::Refresh()
{
	this->ModifyTaskbar();
	this->RedrawWindow();
}

template <class T>
bool TTaskbarContainer<T>::IsControlSuccessfullyCreated()
{
	return m_bOk;
}


template <class T>
void TTaskbarContainer<T>::SetWidth(int nWidth) 
{
	if (IsControlSuccessfullyCreated())
	{
		this->m_stnWindowWidth = nWidth;
		this->ReModifyTaskbar();
		this->RefreshControl();
	}
}

template <class T>
TTaskbarContainer<T>::~TTaskbarContainer()
	{
		if (IsControlSuccessfullyCreated())
		{
			this->ReModifyTaskbar();	
		}
		delete m_pcoReBarWindow;
		delete m_pcoTrayNotifyWnd;
		delete m_pcoParent;
	}

template <class T>
afx_msg void TTaskbarContainer<T>::OnTimer(UINT nIDEvent) 
{
	switch (nIDEvent)
	{
	case TIMER_EV:
		this->RefreshControl();
		break;
	};
	
	T::OnTimer(nIDEvent);
}

template <class T>
void TTaskbarContainer<T>::StartUp()
	{
		this->ModifyTaskbar();
		this->PutWindowIntoTaskbar(m_pcoReBarWindow->GetProportion());	
		this->SetTimer(TIMER_EV, 500, 0);
	}


template <class T>
void TTaskbarContainer<T>::PutWindowIntoTaskbar(int nDirection)
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
			this->Create("", PBS_SMOOTH | WS_CHILD | WS_VISIBLE | SS_NOTIFY, coRect, CWnd::FromHandle(m_pcoParent->GetHWnd()),NULL); // SS_NOTIFY: control sends messages to the parent window !!!!!!!
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
			this->Create("", PBS_SMOOTH | WS_CHILD | WS_VISIBLE | SS_NOTIFY, coRect, CWnd::FromHandle(m_pcoParent->GetHWnd()),NULL);	
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

template <class T>
void TTaskbarContainer<T>::ModifyTaskbar()
{	
	RECT coReBarWindowRect = m_pcoReBarWindow->GetRect();
	RECT coTrayNotifyWndRect = m_pcoTrayNotifyWnd->GetRect();	

	if (m_pcoReBarWindow->GetProportion() == HORIZONTAL)
	{ 
		m_ReBarWindowCurrentRect.left = coReBarWindowRect.left + m_pcoParent->GetRect().left;
		m_ReBarWindowCurrentRect.right = coTrayNotifyWndRect.left-coReBarWindowRect.left-m_stnWindowWidth + m_pcoParent->GetRect().left;
		m_ReBarWindowCurrentRect.bottom = coReBarWindowRect.bottom-coReBarWindowRect.top;		
		m_ReBarWindowCurrentRect.top = 0;
		m_nMode = HORIZONTAL;		
	}
	else if (m_pcoReBarWindow->GetProportion() == VERTICAL)
	{		
		m_ReBarWindowCurrentRect.left =  0;
		m_ReBarWindowCurrentRect.right = coTrayNotifyWndRect.right-coTrayNotifyWndRect.left;
		m_ReBarWindowCurrentRect.top = coReBarWindowRect.top + m_pcoParent->GetRect().top;		
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


template <class T>
void TTaskbarContainer<T>::RefreshControl()
{
	if (!m_bHided)
	{
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
	}
}

template <class T>
void TTaskbarContainer<T>::ReModifyTaskbar()
{
	RECT coReBarWindowRect = m_pcoReBarWindow->GetRect();
	RECT coTrayNotifyWndRect = m_pcoTrayNotifyWnd->GetRect();

	if (m_nMode == HORIZONTAL)
	{
		m_ReBarWindowCurrentRect.left =  coReBarWindowRect.left + m_pcoParent->GetRect().left;
		m_ReBarWindowCurrentRect.right = m_pcoTrayNotifyWnd->GetRect().left-coReBarWindowRect.left;
		m_ReBarWindowCurrentRect.top = 0;
		m_ReBarWindowCurrentRect.bottom = coReBarWindowRect.bottom-coReBarWindowRect.top;		
	}
	else if (m_nMode == VERTICAL)
	{
		m_ReBarWindowCurrentRect.left =  0;
		m_ReBarWindowCurrentRect.right = coTrayNotifyWndRect.right-coTrayNotifyWndRect.left;
		m_ReBarWindowCurrentRect.top = coReBarWindowRect.top + m_pcoParent->GetRect().top;
		m_ReBarWindowCurrentRect.bottom = coTrayNotifyWndRect.top-coReBarWindowRect.top;		
	}

	::MoveWindow(m_pcoReBarWindow->GetHWnd(), 
				   m_ReBarWindowCurrentRect.left, 
				   m_ReBarWindowCurrentRect.top, 
				   m_ReBarWindowCurrentRect.right, 
				   m_ReBarWindowCurrentRect.bottom, 
				   TRUE);	
}

template <class T>
void TTaskbarContainer<T>::Show(bool bShow)
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

template <class T>
bool TTaskbarContainer<T>::IsHided() 
{
	return m_bHided;
}




template <class T>
void TTaskbarContainer<T>::SetHeight(int nHeight) 
{
	if (IsControlSuccessfullyCreated())
	{
		this->m_stnWindowHeight = nHeight;
		this->ReModifyTaskbar();
		this->RefreshControl();
	}
}
/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ fügt unmittelbar vor der vorhergehenden Zeile zusätzliche Deklarationen ein.

#endif // AFX_TTASKBARCONTAINER_H__432FF06E_0462_4C88_9DB9_9C36B32DBAD2__INCLUDED_
