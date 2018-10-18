// Taskb3Dlg.h : Header-Datei
//

#if !defined(AFX_TASKB3DLG_H__E1600B80_1C90_4D14_8F26_1ECDF2AA54E4__INCLUDED_)
#define AFX_TASKB3DLG_H__E1600B80_1C90_4D14_8F26_1ECDF2AA54E4__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CTaskb3Dlg Dialogfeld

#include "NProgressBar.h"

class CTaskb3Dlg : public CDialog
{
private:
	CNProgressBar *pcoControl;

// Konstruktion
public:
	CTaskb3Dlg(CWnd* pParent = NULL);	// Standard-Konstruktor
	~CTaskb3Dlg();

// Dialogfelddaten
	//{{AFX_DATA(CTaskb3Dlg)
	enum { IDD = IDD_TASKB3_DIALOG };
	CButton	m_CheckCol;
	CButton	m_Check2;
	CButton	m_Click1;
	CSliderCtrl	m_Slider1;
	//}}AFX_DATA

	// Vom Klassenassistenten generierte Überladungen virtueller Funktionen
	//{{AFX_VIRTUAL(CTaskb3Dlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV-Unterstützung
	//}}AFX_VIRTUAL

// Implementierung
protected:
	HICON m_hIcon;

	// Generierte Message-Map-Funktionen
	//{{AFX_MSG(CTaskb3Dlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	virtual void OnOK();
	afx_msg void OnOutofmemorySlider1(NMHDR* pNMHDR, LRESULT* pResult);
	afx_msg void OnReleasedcaptureSlider1(NMHDR* pNMHDR, LRESULT* pResult);
	afx_msg void OnCheck1();
	afx_msg void OnCheck2();
	afx_msg void OnCheck3();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ fügt unmittelbar vor der vorhergehenden Zeile zusätzliche Deklarationen ein.

#endif // !defined(AFX_TASKB3DLG_H__E1600B80_1C90_4D14_8F26_1ECDF2AA54E4__INCLUDED_)
