// ButtonsDlg.cpp : implementation file
//

#include "stdafx.h"
#include "Buttons.h"
#include "ButtonsDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	//{{AFX_DATA(CAboutDlg)
	enum { IDD = IDD_ABOUTBOX };
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CAboutDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	//{{AFX_MSG(CAboutDlg)
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
	//{{AFX_DATA_INIT(CAboutDlg)
	//}}AFX_DATA_INIT
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CAboutDlg)
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
	//{{AFX_MSG_MAP(CAboutDlg)
		// No message handlers
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CButtonsDlg dialog

CButtonsDlg::CButtonsDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CButtonsDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CButtonsDlg)
		// NOTE: the ClassWizard will add member initialization here
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CButtonsDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CButtonsDlg)
	DDX_Control(pDX, IDC_BTN_ME, m_btnClick);
	DDX_Control(pDX, IDC_BUTTON3, m_btnExit);
	DDX_Control(pDX, IDC_BUTTON2, m_btnOK);
	DDX_Control(pDX, IDC_BUTTON1, m_btnButton1);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CButtonsDlg, CDialog)
	//{{AFX_MSG_MAP(CButtonsDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_WM_LBUTTONDBLCLK()
	ON_BN_CLICKED(IDC_BTN_ME, OnBtnMe)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CButtonsDlg message handlers

BOOL CButtonsDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	// TODO: Add extra initialization here
	m_btnButton1.SetTransparent();
	m_btnButton1.SetTextColor(RGB(0,220,0), RGB(0,255,0));
	m_btnButton1.SetIcons(IDI_ICON4, IDI_ICON4);
	m_btnButton1.FontStyle("MS Sans Serif");

	m_btnOK.SetTransparent();
	m_btnOK.SetTextColor(RGB(0,220, 0), RGB(0,255,0));
	m_btnOK.SetIcons(IDI_ICON2, IDI_ICON2);
	m_btnOK.FontStyle("MS Sans Serif");

	m_btnExit.SetTransparent();
	m_btnExit.SetTextColor(RGB(0,220,0), RGB(0,255,0));
	m_btnExit.SetIcons(IDI_ICON1, IDI_ICON1);
	m_btnExit.FontStyle("MS Sans Serif");

	m_btnClick.SetTransparent();
	m_btnClick.SetTextColor(RGB(0,220,0), RGB(0,255,0));
	m_btnClick.SetIcons(IDI_ICON3, IDI_ICON5);
	m_btnClick.FontStyle("MS Sans Serif");

	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CButtonsDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CButtonsDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CPaintDC dc(this);
		CRect rect;
		GetClientRect(&rect);
		//ScreenToClient(rect);
		BITMAP bmp;
		HBITMAP hBmp = ::LoadBitmap(::AfxGetInstanceHandle(), MAKEINTRESOURCE(IDB_BITMAP1));
		::GetObject(hBmp, sizeof(bmp), &bmp);
		HDC hDC = ::CreateCompatibleDC(NULL);
		SelectObject(hDC, hBmp);
		//::StretchBlt(dc.m_hDC, 0, 20, rect.Width(), rect.Height(), hDC, 0, 0, bmp.bmWidth, bmp.bmHeight, SRCCOPY);
		::BitBlt(dc.m_hDC, 0, 0, rect.Width(), rect.Height(), hDC, 0, 0, SRCCOPY);
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CButtonsDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

void CButtonsDlg::OnOK() 
{
	CDialog::OnOK();
}

void CButtonsDlg::OnLButtonDblClk(UINT nFlags, CPoint point) 
{
	// TODO: Add your message handler code here and/or call default
	
	CDialog::OnLButtonDblClk(nFlags, point);
}

void CButtonsDlg::OnBtnMe() 
{
	CAboutDlg dlgAbout;
	dlgAbout.DoModal();	
}
