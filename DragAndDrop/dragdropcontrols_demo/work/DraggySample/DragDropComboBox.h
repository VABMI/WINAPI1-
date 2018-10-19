#if !defined(AFX_DRAGDROPCOMBOBOX_H__DE87AA38_5B50_4F05_8A46_CFCB3AAE1B3D__INCLUDED_)
#define AFX_DRAGDROPCOMBOBOX_H__DE87AA38_5B50_4F05_8A46_CFCB3AAE1B3D__INCLUDED_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#include "ECMaterialDropText.h"

// *****************************************************************
// CDragDropComboBox window
// *****************************************************************
class CDragDropComboBox : public CComboBox
{
	DECLARE_DYNAMIC(CDragDropComboBox)

public:
	CDragDropComboBox();
	virtual ~CDragDropComboBox();

	//	call to init us
	void		InitDrag();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CDragDropComboBox)
	//}}AFX_VIRTUAL

	// Generated message map functions
protected:
	//{{AFX_MSG(CDragDropComboBox)
	afx_msg void OnLButtonUp(UINT nFlags, CPoint point);
	afx_msg void OnLButtonDown(UINT nFlags, CPoint point);
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

#endif // !defined(AFX_DRAGDROPCOMBOBOX_H__DE87AA38_5B50_4F05_8A46_CFCB3AAE1B3D__INCLUDED_)
