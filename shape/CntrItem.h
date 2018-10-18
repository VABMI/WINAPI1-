// CntrItem.h : interface of the CShapeCntrItem class
//

#if !defined(AFX_CNTRITEM_H__9ABBE447_74F4_41B9_BCC3_3E5D81727583__INCLUDED_)
#define AFX_CNTRITEM_H__9ABBE447_74F4_41B9_BCC3_3E5D81727583__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CShapeDoc;
class CShapeView;

class CShapeCntrItem : public CRichEditCntrItem
{
	DECLARE_SERIAL(CShapeCntrItem)

// Constructors
public:
	CShapeCntrItem(REOBJECT* preo = NULL, CShapeDoc* pContainer = NULL);
		// Note: pContainer is allowed to be NULL to enable IMPLEMENT_SERIALIZE.
		//  IMPLEMENT_SERIALIZE requires the class have a constructor with
		//  zero arguments.  Normally, OLE items are constructed with a
		//  non-NULL document pointer.

// Attributes
public:
	CShapeDoc* GetDocument()
		{ return (CShapeDoc*)CRichEditCntrItem::GetDocument(); }
	CShapeView* GetActiveView()
		{ return (CShapeView*)CRichEditCntrItem::GetActiveView(); }

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CShapeCntrItem)
	public:
	protected:
	//}}AFX_VIRTUAL

// Implementation
public:
	~CShapeCntrItem();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_CNTRITEM_H__9ABBE447_74F4_41B9_BCC3_3E5D81727583__INCLUDED_)
