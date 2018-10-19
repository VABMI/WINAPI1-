#if !defined(AFX_DRAGDROPBUTTON_H__4219C9CE_A600_4038_A95A_6744ECFD5220__INCLUDED_)
#define AFX_DRAGDROPBUTTON_H__4219C9CE_A600_4038_A95A_6744ECFD5220__INCLUDED_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#include "ECMaterialDropText.h"

// *****************************************************************
// CDragDropButton window
// *****************************************************************
class CDragDropButton : public CButton
{
	DECLARE_DYNAMIC(CDragDropButton)

// Construction
public:
	CDragDropButton();
	virtual ~CDragDropButton();

		//	call to init us
	void		InitDrag();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CDragDropButton)
	//}}AFX_VIRTUAL

	// Generated message map functions
protected:
	//{{AFX_MSG(CDragDropButton)
	afx_msg void OnLButtonUp(UINT nFlags, CPoint point);
	afx_msg void OnLButtonDown(UINT nFlags, CPoint point);
	afx_msg void OnTimer(UINT nIDEvent);
	afx_msg void OnMouseMove(UINT nFlags, CPoint point);
	afx_msg void OnDestroy();
	//}}AFX_MSG

	DECLARE_MESSAGE_MAP()
private:
	ECMaterialDropText		*m_TargetDrop;

	CPoint					m_StartPoint;
	UINT					m_TimerID;

	//	why not?
	friend class ECMaterialDropText;
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Developer Studio will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_DRAGDROPBUTTON_H__4219C9CE_A600_4038_A95A_6744ECFD5220__INCLUDED_)
