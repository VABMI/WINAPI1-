// TestApp.cpp : Defines the class behaviors for the application.
//

#include "stdafx.h"
#include "TestApp.h"
#include "TestAppDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CTestAppApp

BEGIN_MESSAGE_MAP(CTestAppApp, CWinApp)
	//{{AFX_MSG_MAP(CTestAppApp)
	//}}AFX_MSG
	ON_COMMAND(ID_HELP, CWinApp::OnHelp)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CTestAppApp construction

CTestAppApp::CTestAppApp()
{
}

/////////////////////////////////////////////////////////////////////////////
// The one and only CTestAppApp object

CTestAppApp theApp;

/////////////////////////////////////////////////////////////////////////////
// CTestAppApp initialization

BOOL CTestAppApp::InitInstance()
{
	// Standard initialization

	CTestAppDlg dlg;
	m_pMainWnd = &dlg;
	int nResponse = dlg.DoModal();
	if (nResponse == IDOK)
	{
	}
	else if (nResponse == IDCANCEL)
	{
	}

	// Since the dialog has been closed, return FALSE so that we exit the
	//  application, rather than start the application's message pump.
	return FALSE;
}
