// MultiFilerDoc.h : interface of the CMultiFilerDoc class
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_MULTIFILERDOC_H__42073DE2_9010_4069_938B_97351D0C24CA__INCLUDED_)
#define AFX_MULTIFILERDOC_H__42073DE2_9010_4069_938B_97351D0C24CA__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000


class CMultiFilerDoc : public CDocument
{
protected: // create from serialization only
	CMultiFilerDoc();
	DECLARE_DYNCREATE(CMultiFilerDoc)

// Attributes
public:

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMultiFilerDoc)
	public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);
	//}}AFX_VIRTUAL

// Implementation
public:
	virtual ~CMultiFilerDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	//{{AFX_MSG(CMultiFilerDoc)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MULTIFILERDOC_H__42073DE2_9010_4069_938B_97351D0C24CA__INCLUDED_)
