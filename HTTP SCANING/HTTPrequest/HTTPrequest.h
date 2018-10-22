// HTTPrequest.h : main header file for the HTTPREQUEST application
//

#if !defined(AFX_HTTPREQUEST_H__C083D791_3127_47FD_BEF7_0C4439837135__INCLUDED_)
#define AFX_HTTPREQUEST_H__C083D791_3127_47FD_BEF7_0C4439837135__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CHTTPrequestApp:
// See HTTPrequest.cpp for the implementation of this class
//

class CHTTPrequestApp : public CWinApp
{
public:
	CHTTPrequestApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CHTTPrequestApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CHTTPrequestApp)
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_HTTPREQUEST_H__C083D791_3127_47FD_BEF7_0C4439837135__INCLUDED_)
