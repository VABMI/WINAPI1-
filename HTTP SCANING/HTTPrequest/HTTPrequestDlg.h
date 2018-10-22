// HTTPrequestDlg.h : header file
//
//{{AFX_INCLUDES()
#include "webbrowser2.h"
//}}AFX_INCLUDES

#if !defined(AFX_HTTPREQUESTDLG_H__403451A7_8164_440C_BE74_AAC91AAE3B8B__INCLUDED_)
#define AFX_HTTPREQUESTDLG_H__403451A7_8164_440C_BE74_AAC91AAE3B8B__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CHTTPrequestDlg dialog

class CHTTPrequestDlg : public CDialog
{
// Construction
public:
	CHTTPrequestDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CHTTPrequestDlg)
	enum { IDD = IDD_HTTPREQUEST_DIALOG };
	CString	m_sURL;
	CString	m_HTTPsend;
	CString	m_HTTPreceive;
	CWebBrowser2	m_Browser;
	CString	m_sData;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CHTTPrequestDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CHTTPrequestDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnButtonGo();
	afx_msg void OnBeforeNavigate2Explorer1(LPDISPATCH pDisp, VARIANT FAR* URL, VARIANT FAR* Flags, VARIANT FAR* TargetFrameName, VARIANT FAR* PostData, VARIANT FAR* Headers, BOOL FAR* Cancel);
	afx_msg void OnButtonViewHttp();
	DECLARE_EVENTSINK_MAP()
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()

private:
	void	PrintResults();
	void	InsertHTML();
	bool	bFirstTime;
	CString	m_HTTPbody;
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_HTTPREQUESTDLG_H__403451A7_8164_440C_BE74_AAC91AAE3B8B__INCLUDED_)
