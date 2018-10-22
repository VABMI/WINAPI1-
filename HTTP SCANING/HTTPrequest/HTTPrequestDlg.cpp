// HTTPrequestDlg.cpp : implementation file
//

#include "stdafx.h"
#include "HTTPrequest.h"
#include "HTTPrequestDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif


#include "Request.h"

#include "mshtml.h"

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
// CHTTPrequestDlg dialog

CHTTPrequestDlg::CHTTPrequestDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CHTTPrequestDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CHTTPrequestDlg)
	m_sURL = _T("");
	m_HTTPsend = _T("");
	m_HTTPreceive = _T("");
	m_sData = _T("");
	//}}AFX_DATA_INIT
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CHTTPrequestDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CHTTPrequestDlg)
	DDX_Text(pDX, IDC_EDIT_URL, m_sURL);
	DDX_Text(pDX, IDC_EDIT_HTTP_SEND, m_HTTPsend);
	DDX_Text(pDX, IDC_EDIT_HTTP_RECEIVE, m_HTTPreceive);
	DDX_Control(pDX, IDC_EXPLORER1, m_Browser);
	DDX_Text(pDX, IDC_EDIT_POST_DATA, m_sData);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CHTTPrequestDlg, CDialog)
	//{{AFX_MSG_MAP(CHTTPrequestDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON_GO, OnButtonGo)
	ON_BN_CLICKED(IDC_BUTTON_VIEW_HTTP, OnButtonViewHttp)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CHTTPrequestDlg message handlers

BOOL CHTTPrequestDlg::OnInitDialog()
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

	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	

	// TODO: Add extra initialization here

	//read post data
	/*
	if( (fp = fopen(post,"rb")) == NULL )
    {
           printf("\nCan't open %s for read.\n\n",post);
           return 1;
	}

	fseek(fp,0,SEEK_END);
    i				=ftell(fp);
    fseek(fp,0,SEEK_SET);
    buffer			= (char*) malloc(i+1);
    fread(buffer,i,1,fp);
    fclose(fp);	
	*/

	CButton *myButton	= (CButton*)GetDlgItem( IDC_RADIO_GET );
	myButton->SetCheck(1);

	bFirstTime					= true;

	CString		csAddress("about:blank");			//about:blank
	COleVariant vtEmpty;
	m_Browser.Navigate(csAddress, &vtEmpty, &vtEmpty, &vtEmpty, &vtEmpty);

	UpdateData(false);
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CHTTPrequestDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CHTTPrequestDlg::OnPaint() 
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

HCURSOR CHTTPrequestDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

void CHTTPrequestDlg::PrintResults()
{
	Request			myRequest;
	//char			szMessage[8024];
	//char			szHeaderSend[1024];
	//char			szHeaderReceive[1024];

	CString			sHeaderSend, sHeaderReceive, sMessage;
	

	UpdateData(true);

	//look at get/post radio buttons
	CButton		*myButton;
	int			IsPost		= 0;
	myButton	= (CButton*)GetDlgItem( IDC_RADIO_POST );
	IsPost		= myButton->GetCheck();
	
	sHeaderSend	= m_sData;

	myRequest.SendRequest(IsPost, (LPCTSTR)m_sURL, sHeaderSend, sHeaderReceive, sMessage);

	m_HTTPsend		= sHeaderSend;
	m_HTTPreceive	= sHeaderReceive;
	m_HTTPbody		= sMessage;

	UpdateData(false);

}

void CHTTPrequestDlg::OnButtonGo() 
{
	UpdateData(true);

	//look at get/post radio buttons
	CButton		*myButton;
	int			IsPost		= 0;
	myButton	= (CButton*)GetDlgItem( IDC_RADIO_POST );
	IsPost		= myButton->GetCheck();
	
	if ( IsPost )
	{
		PrintResults();
		InsertHTML();
	}
	else
	{
		m_sData		= "";

		//PrintResults();
		//InsertHTML();
		m_Browser.Navigate(m_sURL, NULL, NULL, NULL, NULL);
		InsertHTML();
	}

}

BEGIN_EVENTSINK_MAP(CHTTPrequestDlg, CDialog)
    //{{AFX_EVENTSINK_MAP(CHTTPrequestDlg)
	ON_EVENT(CHTTPrequestDlg, IDC_EXPLORER1, 250 /* BeforeNavigate2 */, OnBeforeNavigate2Explorer1, VTS_DISPATCH VTS_PVARIANT VTS_PVARIANT VTS_PVARIANT VTS_PVARIANT VTS_PVARIANT VTS_PBOOL)
	//}}AFX_EVENTSINK_MAP
END_EVENTSINK_MAP()



void CHTTPrequestDlg::OnBeforeNavigate2Explorer1(LPDISPATCH pDisp, VARIANT FAR* URL, VARIANT FAR* Flags, VARIANT FAR* TargetFrameName, VARIANT FAR* PostData, VARIANT FAR* Headers, BOOL FAR* Cancel) 
{	
	m_sURL			= URL->bstrVal;	
		
	//________________________________________________________
		USES_CONVERSION;
		
		VARIANT*			vtPostedData	= V_VARIANTREF(PostData);
		CByteArray			array;
		if (V_VT(vtPostedData) & VT_ARRAY)
		{
			// must be a vector of bytes
			ASSERT(vtPostedData->parray->cDims == 1 &&
				vtPostedData->parray->cbElements == 1);
			
			vtPostedData->vt				|= VT_UI1;
			COleSafeArray	safe(vtPostedData);
			
			DWORD			dwSize			= safe.GetOneDimSize();
			LPVOID			pVoid;
			safe.AccessData(&pVoid);
			
			array.SetSize(dwSize);
			LPBYTE			lpByte			= array.GetData();
			
			memcpy(lpByte, pVoid, dwSize);
			safe.UnaccessData();

			//is post, so signal this
			CButton *myButton;
			myButton	= (CButton*)GetDlgItem( IDC_RADIO_GET );
			myButton->SetCheck(0);
			myButton	= (CButton*)GetDlgItem( IDC_RADIO_POST );
			myButton->SetCheck(1);

			CString			sPostData(lpByte);
			m_sData			= sPostData;		
		}		// make real parameters out of the notification
		else
		{
			//is get, so signal this
			CButton		*myButton;
			myButton	= (CButton*)GetDlgItem( IDC_RADIO_GET );
			myButton->SetCheck(1);
			myButton	= (CButton*)GetDlgItem( IDC_RADIO_POST );
			myButton->SetCheck(0);

			m_sData		= "";
		}


	//________________________________________________________


	
	UpdateData(false);
		
	if (! bFirstTime )
	{
		PrintResults();
	}
	else
		bFirstTime	= false;
}



void CHTTPrequestDlg::OnButtonViewHttp() 
{
	
	InsertHTML();
	AfxMessageBox(m_HTTPbody);
}

void CHTTPrequestDlg::InsertHTML() 
{
	//_____________________________________________________________
	IHTMLDocument2*		pHTMLDocument2;
	LPDISPATCH			lpDispatch;
	lpDispatch			= m_Browser.GetDocument();

    if (lpDispatch)
	{
		HRESULT hr;
		hr				= lpDispatch->QueryInterface(IID_IHTMLDocument2,
									                (LPVOID*)
													 &pHTMLDocument2);
		lpDispatch->Release();

		IHTMLElement*  pBody;
		hr				= pHTMLDocument2 -> get_body(&pBody);

		if (FAILED(hr)) return;

		BSTR			bstr;                
		bstr			= 	m_HTTPbody.AllocSysString();
		
	   	/// We insert a string of HTML to the end of the document
		//pBody->insertAdjacentHTML(L"BeforeEnd",  bstr);
		pBody->put_innerHTML(bstr);
	   
		SysFreeString(bstr);
		pBody->Release();
	}
	//_____________________________________________________________
}