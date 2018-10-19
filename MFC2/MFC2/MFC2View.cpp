
// MFC2View.cpp : implementation of the CMFC2View class
//

#include "stdafx.h"
// SHARED_HANDLERS can be defined in an ATL project implementing preview, thumbnail
// and search filter handlers and allows sharing of document code with that project.
#ifndef SHARED_HANDLERS
#include "MFC2.h"
#endif

#include "MFC2Doc.h"
#include "MFC2View.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CMFC2View

IMPLEMENT_DYNCREATE(CMFC2View, CView)

BEGIN_MESSAGE_MAP(CMFC2View, CView)
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, &CMFC2View::OnFilePrintPreview)
	ON_WM_CONTEXTMENU()
	ON_WM_RBUTTONUP()
END_MESSAGE_MAP()

// CMFC2View construction/destruction

CMFC2View::CMFC2View()
{
	// TODO: add construction code here

}

CMFC2View::~CMFC2View()
{
}

BOOL CMFC2View::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CView::PreCreateWindow(cs);
}

// CMFC2View drawing

void CMFC2View::OnDraw(CDC* /*pDC*/)
{
	CMFC2Doc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (!pDoc)
		return;

	// TODO: add draw code for native data here
}


// CMFC2View printing


void CMFC2View::OnFilePrintPreview()
{
#ifndef SHARED_HANDLERS
	AFXPrintPreview(this);
#endif
}

BOOL CMFC2View::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default preparation
	return DoPreparePrinting(pInfo);
}

void CMFC2View::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add extra initialization before printing
}

void CMFC2View::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add cleanup after printing
}

void CMFC2View::OnRButtonUp(UINT /* nFlags */, CPoint point)
{
	ClientToScreen(&point);
	OnContextMenu(this, point);
}

void CMFC2View::OnContextMenu(CWnd* /* pWnd */, CPoint point)
{
#ifndef SHARED_HANDLERS
	theApp.GetContextMenuManager()->ShowPopupMenu(IDR_POPUP_EDIT, point.x, point.y, this, TRUE);
#endif
}


// CMFC2View diagnostics

#ifdef _DEBUG
void CMFC2View::AssertValid() const
{
	CView::AssertValid();
}

void CMFC2View::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CMFC2Doc* CMFC2View::GetDocument() const // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CMFC2Doc)));
	return (CMFC2Doc*)m_pDocument;
}
#endif //_DEBUG


// CMFC2View message handlers
