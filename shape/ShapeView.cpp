// ShapeView.cpp : implementation of the CShapeView class
//

#include "stdafx.h"
#include "Shape.h"

#include "ShapeDoc.h"
#include "CntrItem.h"
#include "ShapeView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CShapeView

IMPLEMENT_DYNCREATE(CShapeView, CRichEditView)

BEGIN_MESSAGE_MAP(CShapeView, CRichEditView)
	//{{AFX_MSG_MAP(CShapeView)
	ON_WM_DESTROY()
	ON_COMMAND(ID_TEST_MAKEELLPTICAL, OnMakeElliptical)
	ON_COMMAND(ID_CHANGEWINDOWSHAPES_MAKETRIANGLE, OnOnMakeTriangle)
	ON_COMMAND(ID_TEST_MAKEPOLYGAN, OnMakePolygan)
	ON_COMMAND(ID_TEST_MAKEROUNDEDCORNER, OnMakeRoundedCorner)
	//}}AFX_MSG_MAP
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, CRichEditView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, CRichEditView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, CRichEditView::OnFilePrintPreview)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CShapeView construction/destruction

CShapeView::CShapeView()
{
	// TODO: add construction code here

}

CShapeView::~CShapeView()
{
}

BOOL CShapeView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CRichEditView::PreCreateWindow(cs);
}

void CShapeView::OnInitialUpdate()
{
	CRichEditView::OnInitialUpdate();


	// Set the printing margins (720 twips = 1/2 inch).
	SetMargins(CRect(720, 720, 720, 720));
}

/////////////////////////////////////////////////////////////////////////////
// CShapeView printing

BOOL CShapeView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default preparation
	return DoPreparePrinting(pInfo);
}


void CShapeView::OnDestroy()
{
	// Deactivate the item on destruction; this is important
	// when a splitter view is being used.
   CRichEditView::OnDestroy();
   COleClientItem* pActiveItem = GetDocument()->GetInPlaceActiveItem(this);
   if (pActiveItem != NULL && pActiveItem->GetActiveView() == this)
   {
      pActiveItem->Deactivate();
      ASSERT(GetDocument()->GetInPlaceActiveItem(this) == NULL);
   }
}


/////////////////////////////////////////////////////////////////////////////
// CShapeView diagnostics

#ifdef _DEBUG
void CShapeView::AssertValid() const
{
	CRichEditView::AssertValid();
}

void CShapeView::Dump(CDumpContext& dc) const
{
	CRichEditView::Dump(dc);
}

CShapeDoc* CShapeView::GetDocument() // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CShapeDoc)));
	return (CShapeDoc*)m_pDocument;
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CShapeView message handlers

void CShapeView::OnMakeElliptical() 
{
	CRgn	nit;
//	CWnd	cwindow;
	HWND	hwnd;
	
	hwnd = ::GetActiveWindow();

	nit.CreateEllipticRgn(10,0, 630, 400);
	::SetWindowRgn(hwnd, nit, TRUE);
}

void CShapeView::OnOnMakeTriangle() 
{
	//For Triangle.
	CRgn	nit;
//	CWnd	cwindow;
	HWND	hwnd;
	hwnd = ::GetActiveWindow();

	CRgn   rgnA, rgnB;
	CPoint VertexPoint[3];	

//  To Create The Triangle Window.
	VertexPoint[0].x = 0;
	VertexPoint[0].y = 0;
	VertexPoint[1].x = 850;
	VertexPoint[1].y = 600;
	VertexPoint[2].x = 850;
	VertexPoint[2].y = 0;

	nit.CreatePolygonRgn(VertexPoint, 3, ALTERNATE);	
	::SetWindowRgn(hwnd,nit,TRUE);
}

void CShapeView::OnMakePolygan() 
{
	// For Polygan 
	CRgn	nit;
//	CWnd	cwindow;
	HWND	hwnd;
	hwnd = ::GetActiveWindow();

	CRgn   rgnA, rgnB;
	CPoint VertexPoint[5];

//	Create a Pentagon.
//					Column				Row
	VertexPoint[0].x = 290; VertexPoint[0].y = 0;
	VertexPoint[1].x = 0; 	VertexPoint[1].y = 200;
	VertexPoint[2].x = 200; VertexPoint[2].y = 500;
	VertexPoint[3].x = 400; VertexPoint[3].y = 500;
	VertexPoint[4].x = 650; VertexPoint[4].y = 200;



//  For Pentagon;
	nit.CreatePolygonRgn(VertexPoint, 5, WINDING);
	::SetWindowRgn(hwnd,nit,TRUE);
}

void CShapeView::OnMakeRoundedCorner() 
{

	//Make Rounded Corner....
	CRgn	nit;
//	CWnd	cwindow;
	HWND	hwnd;
	hwnd = ::GetActiveWindow();
	
	int x1, y1, x2, y2;
	CRect crect;
	CPoint cpointtop, cpointbottom;
	
	::GetWindowRect(hwnd, &crect);
	
	//Get The Coordianets of the Window.
	cpointtop	 = crect.TopLeft();
	cpointbottom = crect.BottomRight();

	//Break the Coordinates into four different variables.
	x1 = cpointtop.x;
	y1 = cpointtop.y;
	x2 = cpointbottom.x;
	y2 = cpointbottom.y;

	nit.CreateRoundRectRgn(x1, y1, x2, y2, 30, 30 );
	::SetWindowRgn(hwnd, nit,TRUE);
}
