// ShapeDoc.h : interface of the CShapeDoc class
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_SHAPEDOC_H__43AEEB48_95A1_4E62_A50D_1DFE03D9BEE0__INCLUDED_)
#define AFX_SHAPEDOC_H__43AEEB48_95A1_4E62_A50D_1DFE03D9BEE0__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000


class CShapeDoc : public CRichEditDoc
{
protected: // create from serialization only
	CShapeDoc();
	DECLARE_DYNCREATE(CShapeDoc)

// Attributes
public:

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CShapeDoc)
	public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);
	//}}AFX_VIRTUAL
	virtual CRichEditCntrItem* CreateClientItem(REOBJECT* preo) const;

// Implementation
public:
	virtual ~CShapeDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	//{{AFX_MSG(CShapeDoc)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SHAPEDOC_H__43AEEB48_95A1_4E62_A50D_1DFE03D9BEE0__INCLUDED_)
