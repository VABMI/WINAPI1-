// TestAppDlg.h : header file
//

#if !defined(AFX_TESTAPPDLG_H__A97D368F_C588_45E8_A1FA_A48B96C58339__INCLUDED_)
#define AFX_TESTAPPDLG_H__A97D368F_C588_45E8_A1FA_A48B96C58339__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "roundbutton.h"

/////////////////////////////////////////////////////////////////////////////
// CTestAppDlg dialog

class CTestAppDlg : public CDialog
{
// Construction
public:
	CTestAppDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CTestAppDlg)
	enum { IDD = IDD_TESTAPP_DIALOG };
		// NOTE: the ClassWizard will add data members here
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CTestAppDlg)
	protected:
	//}}AFX_VIRTUAL

// Implementation
protected:
   CRoundButton m_Button1, m_Button2, m_Button3;

	// Generated message map functions
	//{{AFX_MSG(CTestAppDlg)
	virtual BOOL OnInitDialog();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedUsesmoothing();
	// Determines whether or not smoothing is enabled for the buttons
	BOOL m_bUseSmoothing;
protected:
	virtual void DoDataExchange(CDataExchange* pDX);
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_TESTAPPDLG_H__A97D368F_C588_45E8_A1FA_A48B96C58339__INCLUDED_)
