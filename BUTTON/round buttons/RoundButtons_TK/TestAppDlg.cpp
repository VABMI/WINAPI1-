// TestAppDlg.cpp : implementation file
//

#include "stdafx.h"
#include "TestApp.h"
#include "TestAppDlg.h"
#include ".\testappdlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CTestAppDlg dialog

CTestAppDlg::CTestAppDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CTestAppDlg::IDD, pParent)
	, m_bUseSmoothing(TRUE)
{
	//{{AFX_DATA_INIT(CTestAppDlg)
	//}}AFX_DATA_INIT
}

BEGIN_MESSAGE_MAP(CTestAppDlg, CDialog)
	//{{AFX_MSG_MAP(CTestAppDlg)
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_USESMOOTHING, OnBnClickedUsesmoothing)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CTestAppDlg message handlers

BOOL CTestAppDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	m_Button1.SubclassDlgItem(IDC_BUTTON1, this);
	m_Button2.SubclassDlgItem(IDC_BUTTON2, this);
	m_Button3.SubclassDlgItem(IDC_BUTTON3, this);   
   
	return TRUE; 
}


void CTestAppDlg::OnBnClickedUsesmoothing()
{
	UpdateData(TRUE);
	m_Button1.SetSmoothing(m_bUseSmoothing);
	m_Button2.SetSmoothing(m_bUseSmoothing);
	m_Button3.SetSmoothing(m_bUseSmoothing);
}

void CTestAppDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
    DDX_Check(pDX, IDC_USESMOOTHING, m_bUseSmoothing);
}
