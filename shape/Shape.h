// Shape.h : main header file for the SHAPE application
//

#if !defined(AFX_SHAPE_H__60868D6C_1B32_43FF_82E7_B551DDDF29E7__INCLUDED_)
#define AFX_SHAPE_H__60868D6C_1B32_43FF_82E7_B551DDDF29E7__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"       // main symbols

/////////////////////////////////////////////////////////////////////////////
// CShapeApp:
// See Shape.cpp for the implementation of this class
//

class CShapeApp : public CWinApp
{
public:
	CShapeApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CShapeApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation
	//{{AFX_MSG(CShapeApp)
	afx_msg void OnAppAbout();
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SHAPE_H__60868D6C_1B32_43FF_82E7_B551DDDF29E7__INCLUDED_)
