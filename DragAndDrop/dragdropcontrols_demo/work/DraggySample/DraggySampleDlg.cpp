#include "stdafx.h"
#include "DraggySample.h"

#include "DraggySampleDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

// *****************************************************************
// CAboutDlg dialog used for App About
// *****************************************************************
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

// *****************************************************************
// CDraggySampleDlg dialog
// *****************************************************************
CDraggySampleDlg::CDraggySampleDlg(CWnd* pParent /*=NULL*/)
	:	CDialog(CDraggySampleDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CDraggySampleDlg)
	m_DragCombo = 0;
	m_FeedBack_String = _T("");
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CDraggySampleDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CDraggySampleDlg)
	DDX_Control(pDX, IDC_LIST_DRAG, m_DragList_Control);
	DDX_Control(pDX, IDC_COMBO_DRAG, m_DragCombo_Control);
	DDX_Control(pDX, IDC_CHECK_DRAG, m_DragCheck_Control);
	DDX_Control(pDX, IDC_BUTTON_DRAG, m_DragButton_Control);
	DDX_CBIndex(pDX, IDC_COMBO_DRAG, m_DragCombo);
	DDX_Text(pDX, IDC_STATIC_FEEDBACK, m_FeedBack_String);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CDraggySampleDlg, CDialog)
	//{{AFX_MSG_MAP(CDraggySampleDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

// *****************************************************************
// CDraggySampleDlg message handlers
// *****************************************************************
BOOL CDraggySampleDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

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

	//	Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	// *****************************************
	//	Init all drag and drop control
	//	Note that they must be windows by now
	// *****************************************
	m_DragList_Control.InitDrag();
	m_DragCombo_Control.InitDrag();
	m_DragCheck_Control.InitDrag();
	m_DragButton_Control.InitDrag();
	
	return TRUE;
}

// *****************************************************************
// *****************************************************************
void CDraggySampleDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

// *****************************************************************
// *****************************************************************
void CDraggySampleDlg::OnPaint() 
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
		CDialog::OnPaint();
	}
}

// *****************************************************************
// *****************************************************************
HCURSOR CDraggySampleDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}
