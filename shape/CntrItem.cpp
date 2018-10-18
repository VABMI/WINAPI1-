// CntrItem.cpp : implementation of the CShapeCntrItem class
//

#include "stdafx.h"
#include "Shape.h"

#include "ShapeDoc.h"
#include "ShapeView.h"
#include "CntrItem.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CShapeCntrItem implementation

IMPLEMENT_SERIAL(CShapeCntrItem, CRichEditCntrItem, 0)

CShapeCntrItem::CShapeCntrItem(REOBJECT* preo, CShapeDoc* pContainer)
	: CRichEditCntrItem(preo, pContainer)
{
	// TODO: add one-time construction code here
	
}

CShapeCntrItem::~CShapeCntrItem()
{
	// TODO: add cleanup code here
	
}

/////////////////////////////////////////////////////////////////////////////
// CShapeCntrItem diagnostics

#ifdef _DEBUG
void CShapeCntrItem::AssertValid() const
{
	CRichEditCntrItem::AssertValid();
}

void CShapeCntrItem::Dump(CDumpContext& dc) const
{
	CRichEditCntrItem::Dump(dc);
}
#endif

/////////////////////////////////////////////////////////////////////////////
