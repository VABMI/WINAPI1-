// MultiFilerDoc.cpp : implementation of the CMultiFilerDoc class
//

#include "stdafx.h"
#include "MultiFiler.h"

#include "MultiFilerDoc.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CMultiFilerDoc

IMPLEMENT_DYNCREATE(CMultiFilerDoc, CDocument)

BEGIN_MESSAGE_MAP(CMultiFilerDoc, CDocument)
	//{{AFX_MSG_MAP(CMultiFilerDoc)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CMultiFilerDoc construction/destruction

CMultiFilerDoc::CMultiFilerDoc()
{
	// TODO: add one-time construction code here

}

CMultiFilerDoc::~CMultiFilerDoc()
{
}

BOOL CMultiFilerDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	// TODO: add reinitialization code here
	// (SDI documents will reuse this document)

	return TRUE;
}



/////////////////////////////////////////////////////////////////////////////
// CMultiFilerDoc serialization

void CMultiFilerDoc::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		// TODO: add storing code here
	}
	else
	{
		// TODO: add loading code here
	}
}

/////////////////////////////////////////////////////////////////////////////
// CMultiFilerDoc diagnostics

#ifdef _DEBUG
void CMultiFilerDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CMultiFilerDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CMultiFilerDoc commands
