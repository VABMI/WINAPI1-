// ButtonsDlg.h : header file
//

#if !defined(AFX_BUTTONSDLG_H__41D360CA_F3EE_44A2_9204_45A39882B450__INCLUDED_)
#define AFX_BUTTONSDLG_H__41D360CA_F3EE_44A2_9204_45A39882B450__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CButtonsDlg dialog
#include "ButtonStyle.h"
#include "Slider.h"

class CButtonsDlg : public CDialog
{
// Construction
public:
	CSlider m_Slider;
	CButtonsDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CButtonsDlg)
	enum { IDD = IDD_BUTTONS_DIALOG };
	CButtonStyle	m_btnClick;
	CButtonStyle	m_btnExit;
	CButtonStyle	m_btnOK;
	CButtonStyle	m_btnButton1;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CButtonsDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CButtonsDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	virtual void OnOK();
	afx_msg void OnLButtonDblClk(UINT nFlags, CPoint point);
	afx_msg void OnBtnMe();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_BUTTONSDLG_H__41D360CA_F3EE_44A2_9204_45A39882B450__INCLUDED_)
