// MultiFiler.h : main header file for the MULTIFILER application
//

#if !defined(AFX_MULTIFILER_H__AF3292CD_054F_4AFB_8BF4_994D9F2FFCC5__INCLUDED_)
#define AFX_MULTIFILER_H__AF3292CD_054F_4AFB_8BF4_994D9F2FFCC5__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"       // main symbols

/////////////////////////////////////////////////////////////////////////////
// CMultiFilerApp:
// See MultiFiler.cpp for the implementation of this class
//

class CMultiFilerApp : public CWinApp
{
public:
	CMultiFilerApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMultiFilerApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation
	//{{AFX_MSG(CMultiFilerApp)
	afx_msg void OnAppAbout();
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MULTIFILER_H__AF3292CD_054F_4AFB_8BF4_994D9F2FFCC5__INCLUDED_)
