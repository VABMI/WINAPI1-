#if !defined(AFX_DRAGGYSAMPLEDLG_H__CF9FFDF7_3C37_4FD1_B54E_BE61BD3D61CE__INCLUDED_)
#define AFX_DRAGGYSAMPLEDLG_H__CF9FFDF7_3C37_4FD1_B54E_BE61BD3D61CE__INCLUDED_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#include "DragDropButton.h"
#include "DragDropComboBox.h"
#include "DragDropListBox.h"

// *****************************************************************
// CDraggySampleDlg dialog
// *****************************************************************
class CDraggySampleDlg : public CDialog
{
// Construction
public:
	CDraggySampleDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CDraggySampleDlg)
	enum { IDD = IDD_DRAGGYSAMPLE_DIALOG };
	CDragDropListBox	m_DragList_Control;
	CDragDropComboBox	m_DragCombo_Control;
	CDragDropButton	m_DragCheck_Control;
	CDragDropButton	m_DragButton_Control;
	int		m_DragCombo;
	CString	m_FeedBack_String;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CDraggySampleDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CDraggySampleDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Developer Studio will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_DRAGGYSAMPLEDLG_H__CF9FFDF7_3C37_4FD1_B54E_BE61BD3D61CE__INCLUDED_)
