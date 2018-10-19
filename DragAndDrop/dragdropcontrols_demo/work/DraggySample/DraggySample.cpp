#include "stdafx.h"
#include "DraggySample.h"
#include "DraggySampleDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

BEGIN_MESSAGE_MAP(CDraggySampleApp, CWinApp)
	//{{AFX_MSG_MAP(CDraggySampleApp)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG
	ON_COMMAND(ID_HELP, CWinApp::OnHelp)
END_MESSAGE_MAP()

// *****************************************************************
//	CDraggySampleApp
// *****************************************************************
CDraggySampleApp::CDraggySampleApp()
{
	// TODO: add construction code here,
	// Place all significant initialization in InitInstance
}

CDraggySampleApp theApp;

// *****************************************************************
//	InitInstance
// *****************************************************************
BOOL CDraggySampleApp::InitInstance()
{

	AfxEnableControlContainer();

	Enable3dControls();			// Call this when using MFC in a shared DLL

	//	important!
	AfxOleInit();

	CDraggySampleDlg dlg;
	m_pMainWnd = &dlg;
	int nResponse = dlg.DoModal();
	if (nResponse == IDOK)
	{
		// TODO: Place code here to handle when the dialog is
		//  dismissed with OK
	}
	else if (nResponse == IDCANCEL)
	{
		// TODO: Place code here to handle when the dialog is
		//  dismissed with Cancel
	}

	// Since the dialog has been closed, return FALSE so that we exit the
	//  application, rather than start the application's message pump.
	return FALSE;
}
