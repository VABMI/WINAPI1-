#if !defined(AFX_DRAGDROPLISTBOX_H__AABD5F4E_EBAD_4DFA_BE83_541F717699BA__INCLUDED_)
#define AFX_DRAGDROPLISTBOX_H__AABD5F4E_EBAD_4DFA_BE83_541F717699BA__INCLUDED_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#include "ECMaterialDropText.h"

// *****************************************************************
// CDragDropListBox window
// *****************************************************************
class CDragDropListBox : public CListBox
{
	DECLARE_DYNAMIC(CDragDropListBox)

public:
	CDragDropListBox();
	virtual ~CDragDropListBox();

	//	call to init us
	void		InitDrag();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CDragDropListBox)
	//}}AFX_VIRTUAL

	// Generated message map functions
protected:
	//{{AFX_MSG(CDragDropListBox)
	afx_msg void OnLButtonDown(UINT nFlags, CPoint point);
	afx_msg void OnLButtonUp(UINT nFlags, CPoint point);
	afx_msg void OnMouseMove(UINT nFlags, CPoint point);
	afx_msg void OnTimer(UINT nIDEvent);
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

#endif // !defined(AFX_DRAGDROPLISTBOX_H__AABD5F4E_EBAD_4DFA_BE83_541F717699BA__INCLUDED_)
