// TestApp.h : main header file for the TESTAPP application
//

#if !defined(AFX_TESTAPP_H__24E22D47_9A26_496B_9D1A_15CDC6E86935__INCLUDED_)
#define AFX_TESTAPP_H__24E22D47_9A26_496B_9D1A_15CDC6E86935__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CTestAppApp:
// See TestApp.cpp for the implementation of this class
//

class CTestAppApp : public CWinApp
{
public:
	CTestAppApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CTestAppApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CTestAppApp)
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_TESTAPP_H__24E22D47_9A26_496B_9D1A_15CDC6E86935__INCLUDED_)
