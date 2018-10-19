#if !defined(AFX_DRAGGYSAMPLE_H__8402E1B9_4300_4E42_A3E9_95AD5D5747B7__INCLUDED_)
#define AFX_DRAGGYSAMPLE_H__8402E1B9_4300_4E42_A3E9_95AD5D5747B7__INCLUDED_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

// *****************************************************************
// CDraggySampleApp:
// *****************************************************************
class CDraggySampleApp : public CWinApp
{
public:
	CDraggySampleApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CDraggySampleApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CDraggySampleApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Developer Studio will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_DRAGGYSAMPLE_H__8402E1B9_4300_4E42_A3E9_95AD5D5747B7__INCLUDED_)
