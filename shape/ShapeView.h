// ShapeView.h : interface of the CShapeView class
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_SHAPEVIEW_H__F43BC918_1C7A_4591_9801_386A69EB3896__INCLUDED_)
#define AFX_SHAPEVIEW_H__F43BC918_1C7A_4591_9801_386A69EB3896__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CShapeCntrItem;

class CShapeView : public CRichEditView
{
protected: // create from serialization only
	CShapeView();
	DECLARE_DYNCREATE(CShapeView)

// Attributes
public:
	CShapeDoc* GetDocument();

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CShapeView)
	public:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
	protected:
	virtual void OnInitialUpdate(); // called first time after construct
	virtual BOOL OnPreparePrinting(CPrintInfo* pInfo);
	//}}AFX_VIRTUAL

// Implementation
public:
	virtual ~CShapeView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	//{{AFX_MSG(CShapeView)
	afx_msg void OnDestroy();
	afx_msg void OnMakeElliptical();
	afx_msg void OnOnMakeTriangle();
	afx_msg void OnMakePolygan();
	afx_msg void OnMakeRoundedCorner();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

#ifndef _DEBUG  // debug version in ShapeView.cpp
inline CShapeDoc* CShapeView::GetDocument()
   { return (CShapeDoc*)m_pDocument; }
#endif

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SHAPEVIEW_H__F43BC918_1C7A_4591_9801_386A69EB3896__INCLUDED_)
