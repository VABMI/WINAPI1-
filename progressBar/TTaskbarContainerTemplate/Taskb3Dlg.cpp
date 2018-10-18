// Taskb3Dlg.cpp : Implementierungsdatei
//

#include "stdafx.h"
#include "Taskb3.h"
#include "Taskb3Dlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CAboutDlg-Dialogfeld f�r Anwendungsbefehl "Info"

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialogfelddaten
	//{{AFX_DATA(CAboutDlg)
	enum { IDD = IDD_ABOUTBOX };
	//}}AFX_DATA

	// Vom Klassenassistenten generierte �berladungen virtueller Funktionen
	//{{AFX_VIRTUAL(CAboutDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV-Unterst�tzung
	//}}AFX_VIRTUAL

// Implementierung
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
		// Keine Nachrichten-Handler
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CTaskb3Dlg Dialogfeld

CTaskb3Dlg::CTaskb3Dlg(CWnd* pParent /*=NULL*/)
	: CDialog(CTaskb3Dlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CTaskb3Dlg)
		// HINWEIS: Der Klassenassistent f�gt hier Member-Initialisierung ein
	//}}AFX_DATA_INIT
	// Beachten Sie, dass LoadIcon unter Win32 keinen nachfolgenden DestroyIcon-Aufruf ben�tigt
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

CTaskb3Dlg::~CTaskb3Dlg()
{
	delete pcoControl;
}

void CTaskb3Dlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CTaskb3Dlg)
	DDX_Control(pDX, IDC_CHECK3, m_CheckCol);
	DDX_Control(pDX, IDC_CHECK2, m_Check2);
	DDX_Control(pDX, IDC_CHECK1, m_Click1);
	DDX_Control(pDX, IDC_SLIDER1, m_Slider1);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CTaskb3Dlg, CDialog)
	//{{AFX_MSG_MAP(CTaskb3Dlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_NOTIFY(NM_OUTOFMEMORY, IDC_SLIDER1, OnOutofmemorySlider1)
	ON_NOTIFY(NM_RELEASEDCAPTURE, IDC_SLIDER1, OnReleasedcaptureSlider1)
	ON_BN_CLICKED(IDC_CHECK1, OnCheck1)
	ON_BN_CLICKED(IDC_CHECK2, OnCheck2)
	ON_BN_CLICKED(IDC_CHECK3, OnCheck3)
	ON_BN_CLICKED(IDC_BUTTON1, OnButtonTest)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CTaskb3Dlg Nachrichten-Handler

BOOL CTaskb3Dlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Hinzuf�gen des Men�befehls "Info..." zum Systemmen�.

	// IDM_ABOUTBOX muss sich im Bereich der Systembefehle befinden.
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

	// Symbol f�r dieses Dialogfeld festlegen. Wird automatisch erledigt
	//  wenn das Hauptfenster der Anwendung kein Dialogfeld ist
	SetIcon(m_hIcon, TRUE);			// Gro�es Symbol verwenden
	SetIcon(m_hIcon, FALSE);		// Kleines Symbol verwenden
	
	// ZU ERLEDIGEN: Hier zus�tzliche Initialisierung einf�gen
	 
	this->m_Check2.SetCheck(TRUE);
	pcoControl = new TTaskbarContainer<CButton>(this);
	this->m_Slider1.SetRange(0, 100, TRUE);
	this->m_Slider1.SetPos(0);
	//pcoControl->SetPosEx(this->m_Slider1.GetPos());


	return TRUE;  // Geben Sie TRUE zur�ck, au�er ein Steuerelement soll den Fokus erhalten
}

void CTaskb3Dlg::OnSysCommand(UINT nID, LPARAM lParam)
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

// Wollen Sie Ihrem Dialogfeld eine Schaltfl�che "Minimieren" hinzuf�gen, ben�tigen Sie 
//  den nachstehenden Code, um das Symbol zu zeichnen. F�r MFC-Anwendungen, die das 
//  Dokument/Ansicht-Modell verwenden, wird dies automatisch f�r Sie erledigt.

void CTaskb3Dlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // Ger�tekontext f�r Zeichnen

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Symbol in Client-Rechteck zentrieren
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Symbol zeichnen
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// Die Systemaufrufe fragen den Cursorform ab, die angezeigt werden soll, w�hrend der Benutzer
//  das zum Symbol verkleinerte Fenster mit der Maus zieht.
HCURSOR CTaskb3Dlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

void CTaskb3Dlg::OnOK() 
{
	// TODO: Zus�tzliche Pr�fung hier einf�gen
	
	CDialog::OnOK();
}

void CTaskb3Dlg::OnOutofmemorySlider1(NMHDR* pNMHDR, LRESULT* pResult) 
{


	*pResult = 0;
}

void CTaskb3Dlg::OnReleasedcaptureSlider1(NMHDR* pNMHDR, LRESULT* pResult) 
{
	// TODO: Code f�r die Behandlungsroutine der Steuerelement-Benachrichtigung hier einf�gen
	
	*pResult = 0;
}

void CTaskb3Dlg::OnCheck1() 
{
	if (pcoControl->IsControlSuccessfullyCreated())
	{
		/*
		if (m_Click1.GetCheck()==0)
		{
			pcoControl->ShowMyText("Power", false);
		}
		else
		{
			pcoControl->ShowMyText("Power", true);
		}
		pcoControl->RedrawWindow();
		*/
	}
}

void CTaskb3Dlg::OnCheck2() 
{
	if (pcoControl->IsControlSuccessfullyCreated())
	{
		
		if (this->m_Check2.GetCheck()==0)
		{
			pcoControl->Show(false);
		}
		else
		{
			pcoControl->Show(true);
		}
		
	}
}

void CTaskb3Dlg::OnCheck3() 
{
	if (pcoControl->IsControlSuccessfullyCreated())
	{
		/*
		if (m_CheckCol.GetCheck() == 0)
		{
			pcoControl->SetColorMode(false);
		}
		else
		{
			pcoControl->SetColorMode(true);
		}
		*/
	}
	//pcoControl->SetPosEx(this->m_Slider1.GetPos());
}

void CTaskb3Dlg::OnButtonTest() 
{
	this->pcoControl->SetWidth(100);	
}
