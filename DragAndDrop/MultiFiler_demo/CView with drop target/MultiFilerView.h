// MultiFilerView.h : interface of the CMultiFilerView class
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_MULTIFILERVIEW_H__D626641B_521D_4EE0_AAFB_96734DF04F15__INCLUDED_)
#define AFX_MULTIFILERVIEW_H__D626641B_521D_4EE0_AAFB_96734DF04F15__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

struct IDropTargetHelper;    // in case the newest PSDK isn't available.

class CMultiFilerView : public CListView
{
protected: // create from serialization only
	CMultiFilerView();
	DECLARE_DYNCREATE(CMultiFilerView)

// Attributes
public:
	CMultiFilerDoc* GetDocument();

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMultiFilerView)
	public:
	virtual void OnDraw(CDC* pDC);  // overridden to draw this view
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
	virtual DROPEFFECT OnDragEnter(COleDataObject* pDataObject, DWORD dwKeyState, CPoint point);
	virtual DROPEFFECT OnDragOver(COleDataObject* pDataObject, DWORD dwKeyState, CPoint point);
	virtual BOOL OnDrop(COleDataObject* pDataObject, DROPEFFECT dropEffect, CPoint point);
	virtual void OnDragLeave();
	protected:
	virtual void OnInitialUpdate(); // called first time after construct
	//}}AFX_VIRTUAL

// Implementation
public:
	virtual ~CMultiFilerView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:
    CImageList         m_imglist;
    COleDropTarget     m_droptarget;

    IDropTargetHelper* m_piDropHelper;
    bool               m_bUseDnDHelper;

    BOOL ReadHdropData ( COleDataObject* pDataObject );

// Generated message map functions
protected:
	//{{AFX_MSG(CMultiFilerView)
	afx_msg void OnRclick(NMHDR* pNMHDR, LRESULT* pResult);
	afx_msg void OnBegindrag(NMHDR* pNMHDR, LRESULT* pResult);
	afx_msg void OnSelectAll();
	afx_msg void OnInvertSelection();
	afx_msg void OnRemoveFromList();
	afx_msg void OnClearList();
	afx_msg void OnDestroy();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

#ifndef _DEBUG  // debug version in MultiFilerView.cpp
inline CMultiFilerDoc* CMultiFilerView::GetDocument()
   { return (CMultiFilerDoc*)m_pDocument; }
#endif

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MULTIFILERVIEW_H__D626641B_521D_4EE0_AAFB_96734DF04F15__INCLUDED_)
