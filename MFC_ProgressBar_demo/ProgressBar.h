// ProgressBar.h : header file
//
// Drop-in status bar progress control
//
// Written by Chris Maunder (chris@codeproject.com)
// Copyright (c) 1998.
//
// This code may be used in compiled form in any way you desire. This
// file may be redistributed unmodified by any means PROVIDING it is 
// not sold for profit without the authors written consent, and 
// providing that this notice and the authors name is included. If 
// the source code in this file is used in any commercial application 
// then an email to me would be nice.
//
// This file is provided "as is" with no expressed or implied warranty.
// The author accepts no liability if it causes any damage to your
// computer, causes your pet cat to fall ill, increases baldness or
// makes you car start emitting strange noises when you start it up.
//
// Expect bugs.
// 
// Please use and enjoy. Please let me know of any bugs/mods/improvements 
// that you have found/implemented and I will fix/incorporate them into this
// file. 
//
// Version 1.1  Chris Maunder (chris@codeproject.com)
//
// Version 1.2  Michael Martin 07 Aug 1998	mmartin@netspace.net.au
//              Added code to enable progress bar to appear in any pane
//              of the status bar.
//
//              Chris Maunder 17 Aug 1998 
//              Fixed Pane text restoration, and general cleanup.
//
// Version 1.3  Chris Maunder (chris@codeproject.com)
//              Updates from YoSilver to fix a multi-thread issue
//
// Version 1.4  Alex Paterson 23 Aug 2010
//              Updates to allow status bar to be specified
//
/////////////////////////////////////////////////////////////////////////////

#ifndef _INCLUDE_PROGRESSBAR_H_
#define _INCLUDE_PROGRESSBAR_H_


/////////////////////////////////////////////////////////////////////////////
// CProgressBar -  status bar progress control
//
// Copyright (c) Chris Maunder, 1997
// Please feel free to use and distribute.

class CProgressBar: public CProgressCtrl
// Creates a ProgressBar in the status bar
{
public:
	CProgressBar();
	CProgressBar(LPCTSTR strMessage, int nSize=100, int MaxValue=100, 
                 BOOL bSmooth=FALSE, int nPane=0, CStatusBar* pBar = NULL);
	~CProgressBar();
	BOOL Create(LPCTSTR strMessage, int nSize=100, int MaxValue=100, 
                BOOL bSmooth=FALSE, int nPane=0);

	DECLARE_DYNCREATE(CProgressBar)

// operations
public:
	BOOL SetRange(int nLower, int nUpper, int nStep = 1);
	BOOL SetText(LPCTSTR strMessage);
	BOOL SetSize(int nSize);
	COLORREF SetBarColour(COLORREF clrBar);
	COLORREF SetBkColour(COLORREF clrBk);
	int  SetPos(int nPos);
	int  OffsetPos(int nPos);
	int  SetStep(int nStep);
	int  StepIt();
	void Clear();

// Overrides
	//{{AFX_VIRTUAL(CProgressBar)
	//}}AFX_VIRTUAL

// implementation
protected:
	int		m_nSize;		// Percentage size of control
	int		m_nPane;		// ID of status bar pane progress bar is to appear in
	CString	m_strMessage;	// Message to display to left of control
    CString m_strPrevText;  // Previous text in status bar
	CRect	m_Rect;			// Dimensions of the whole thing
	CStatusBar* const m_pStatusBar; // Set in the ctor to explicitly state which status bar to use.

	CStatusBar *GetStatusBar();
	BOOL Resize();

// Generated message map functions
protected:
	//{{AFX_MSG(CProgressBar)
	afx_msg BOOL OnEraseBkgnd(CDC* pDC);
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()

};

#endif
/////////////////////////////////////////////////////////////////////////////
