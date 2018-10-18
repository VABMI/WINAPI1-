// MultiFilerDlg.h : header file
//

#if !defined(AFX_MULTIFILERDLG_H__34130357_45C0_4842_885A_283A5728849B__INCLUDED_)
#define AFX_MULTIFILERDLG_H__34130357_45C0_4842_885A_283A5728849B__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "MyDropTarget.h"

/////////////////////////////////////////////////////////////////////////////
// CMultiFilerDlg dialog

class CMultiFilerDlg : public CDialog
{
// Construction
public:
	CMultiFilerDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CMultiFilerDlg)
	enum { IDD = IDD_MULTIFILER_DIALOG };
	CListCtrl	c_FileList;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMultiFilerDlg)
	public:
	virtual BOOL PreTranslateMessage(MSG* pMsg);
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
    CImageList    m_imglist;
    CMyDropTarget m_droptarget;
    HACCEL        m_hAccel;

	// Generated message map functions
	//{{AFX_MSG(CMultiFilerDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnDestroy();
	afx_msg void OnBegindragFilelist(NMHDR* pNMHDR, LRESULT* pResult);
	afx_msg void OnRclickFilelist(NMHDR* pNMHDR, LRESULT* pResult);
	afx_msg void OnSelectAll();
	afx_msg void OnInvertSelection();
	afx_msg void OnRemoveFromList();
	afx_msg void OnClearList();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MULTIFILERDLG_H__34130357_45C0_4842_885A_283A5728849B__INCLUDED_)
