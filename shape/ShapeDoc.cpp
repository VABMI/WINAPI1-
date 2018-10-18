// ShapeDoc.cpp : implementation of the CShapeDoc class
//

#include "stdafx.h"
#include "Shape.h"

#include "ShapeDoc.h"
#include "CntrItem.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CShapeDoc

IMPLEMENT_DYNCREATE(CShapeDoc, CRichEditDoc)

BEGIN_MESSAGE_MAP(CShapeDoc, CRichEditDoc)
	//{{AFX_MSG_MAP(CShapeDoc)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG_MAP
	// Enable default OLE container implementation
	ON_UPDATE_COMMAND_UI(ID_OLE_EDIT_LINKS, CRichEditDoc::OnUpdateEditLinksMenu)
	ON_COMMAND(ID_OLE_EDIT_LINKS, CRichEditDoc::OnEditLinks)
	ON_UPDATE_COMMAND_UI_RANGE(ID_OLE_VERB_FIRST, ID_OLE_VERB_LAST, CRichEditDoc::OnUpdateObjectVerbMenu)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CShapeDoc construction/destruction

CShapeDoc::CShapeDoc()
{
	// TODO: add one-time construction code here

}

CShapeDoc::~CShapeDoc()
{
}

BOOL CShapeDoc::OnNewDocument()
{
	if (!CRichEditDoc::OnNewDocument())
		return FALSE;

	// TODO: add reinitialization code here
	// (SDI documents will reuse this document)

	return TRUE;
}

CRichEditCntrItem* CShapeDoc::CreateClientItem(REOBJECT* preo) const
{
	// cast away constness of this
	return new CShapeCntrItem(preo, (CShapeDoc*) this);
}



/////////////////////////////////////////////////////////////////////////////
// CShapeDoc serialization

void CShapeDoc::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		// TODO: add storing code here
	}
	else
	{
		// TODO: add loading code here
	}

	// Calling the base class CRichEditDoc enables serialization
	//  of the container document's COleClientItem objects.
	// TODO: set CRichEditDoc::m_bRTF = FALSE if you are serializing as text
	CRichEditDoc::Serialize(ar);
}

/////////////////////////////////////////////////////////////////////////////
// CShapeDoc diagnostics

#ifdef _DEBUG
void CShapeDoc::AssertValid() const
{
	CRichEditDoc::AssertValid();
}

void CShapeDoc::Dump(CDumpContext& dc) const
{
	CRichEditDoc::Dump(dc);
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CShapeDoc commands
