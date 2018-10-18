// MultiFilerDlg.cpp : implementation file
//

#include "stdafx.h"
#include "MultiFiler.h"
#include "MultiFilerDlg.h"

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
// CMultiFilerDlg dialog

CMultiFilerDlg::CMultiFilerDlg(CWnd* pParent /*=NULL*/)
    : CDialog(CMultiFilerDlg::IDD, pParent)
{
    //{{AFX_DATA_INIT(CMultiFilerDlg)
    //}}AFX_DATA_INIT
    // Note that LoadIcon does not require a subsequent DestroyIcon in Win32
}

void CMultiFilerDlg::DoDataExchange(CDataExchange* pDX)
{
    CDialog::DoDataExchange(pDX);
    //{{AFX_DATA_MAP(CMultiFilerDlg)
    DDX_Control(pDX, IDC_FILELIST, c_FileList);
    //}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CMultiFilerDlg, CDialog)
    //{{AFX_MSG_MAP(CMultiFilerDlg)
    ON_WM_SYSCOMMAND()
    ON_WM_DESTROY()
    ON_NOTIFY(LVN_BEGINDRAG, IDC_FILELIST, OnBegindragFilelist)
    ON_NOTIFY(NM_RCLICK, IDC_FILELIST, OnRclickFilelist)
    ON_COMMAND(IDC_SELECT_ALL, OnSelectAll)
    ON_COMMAND(IDC_INVERT_SELECTION, OnInvertSelection)
    ON_COMMAND(IDC_REMOVE_FROM_LIST, OnRemoveFromList)
    ON_COMMAND(IDC_CLEAR_LIST, OnClearList)
    ON_WM_DROPFILES()
    //}}AFX_MSG_MAP
END_MESSAGE_MAP()


/////////////////////////////////////////////////////////////////////////////
// CMultiFilerDlg message handlers

// We need to call TranslateAccelerator() ourselves - dialogs don't do this
// automagically.
BOOL CMultiFilerDlg::PreTranslateMessage(MSG* pMsg) 
{
    if ( NULL != m_hAccel && 
         pMsg->message >= WM_KEYFIRST  &&  pMsg->message <= WM_KEYLAST )
        {
        return TranslateAccelerator ( m_hWnd, m_hAccel, pMsg );
        }
    else
        return CDialog::PreTranslateMessage(pMsg);
}

BOOL CMultiFilerDlg::OnInitDialog()
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
    //  when the application's main window is not a dialog.
HICON hLgIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
HICON hSmIcon = (HICON) LoadImage ( AfxGetResourceHandle(), 
                                    MAKEINTRESOURCE(IDR_MAINFRAME),
                                    IMAGE_ICON, 16, 16, LR_DEFAULTCOLOR );

    SetIcon(hLgIcon, TRUE);         // Set big icon
    SetIcon(hSmIcon, FALSE);        // Set small icon
    
    // Remove Size & Maximize from the system menu.
    
    pSysMenu->DeleteMenu ( SC_MAXIMIZE, MF_BYCOMMAND );
    pSysMenu->DeleteMenu ( SC_SIZE, MF_BYCOMMAND );

    // Get a handle to the system small icon image list, and set that as the
    // image list for our list control

SHFILEINFO rInfo;
HIMAGELIST hImglist;

    hImglist = (HIMAGELIST) SHGetFileInfo ( _T(""), 0, &rInfo, sizeof(SHFILEINFO), 
                                            SHGFI_ICON | SHGFI_SMALLICON | 
                                              SHGFI_SYSICONINDEX );

    m_imglist.Attach ( hImglist );

    // Make sure the list control has the share image list style, so it won't
    // destroy the system image list when the dialog closes.

    c_FileList.ModifyStyle ( 0, LVS_SHAREIMAGELISTS );

    c_FileList.SetImageList ( &m_imglist, LVSIL_SMALL );

    // Set up the list columns.

    c_FileList.InsertColumn ( 0, _T("Filename"), LVCFMT_LEFT, 0, 0 );
    c_FileList.InsertColumn ( 1, _T("Type"), LVCFMT_LEFT, 0, 1 );
    c_FileList.InsertColumn ( 2, _T("Size"), LVCFMT_LEFT, 0, 2 );

    c_FileList.SetColumnWidth ( 0, LVSCW_AUTOSIZE_USEHEADER );
    c_FileList.SetColumnWidth ( 1, LVSCW_AUTOSIZE_USEHEADER );
    c_FileList.SetColumnWidth ( 2, LVSCW_AUTOSIZE_USEHEADER );

    // Enable tooltips for items that aren't completely visible.

    c_FileList.SetExtendedStyle ( LVS_EX_INFOTIP );

    // Position the dlg at the bottom-right of the desktop.
CRect  rcDesktop;
CRect  rcDlg;
CPoint pt;

    SystemParametersInfo ( SPI_GETWORKAREA, 0, &rcDesktop, 0 );
    GetWindowRect ( &rcDlg );

    pt.x = rcDesktop.right - rcDlg.Width();
    pt.y = rcDesktop.bottom - rcDlg.Height();

    SetWindowPos ( NULL, pt.x, pt.y, 0, 0, SWP_NOSIZE | SWP_NOZORDER );

    // Load the accelerator table.
    m_hAccel = LoadAccelerators ( AfxGetResourceHandle(),
                                  MAKEINTRESOURCE(IDR_ACCEL) );

    return TRUE;  // return TRUE  unless you set the focus to a control
}

void CMultiFilerDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CMultiFilerDlg::OnDestroy() 
{
    // Detach our CImageList from the system image list.

    if ( NULL != m_imglist.GetSafeHandle() )
        m_imglist.Detach();

    CDialog::OnDestroy();
}

void CMultiFilerDlg::OnBegindragFilelist(NMHDR* pNMHDR, LRESULT* pResult) 
{
NMLISTVIEW*    pNMLV = (NMLISTVIEW*) pNMHDR;
COleDataSource datasrc;
HGLOBAL        hgDrop;
DROPFILES*     pDrop;
CStringList    lsDraggedFiles;
POSITION       pos;
int            nSelItem;
CString        sFile;
UINT           uBuffSize = 0;
TCHAR*         pszBuff;
FORMATETC      etc = { CF_HDROP, NULL, DVASPECT_CONTENT, -1, TYMED_HGLOBAL };

    *pResult = 0;   // return value ignored

    // For every selected item in the list, put the filename into lsDraggedFiles.

    pos = c_FileList.GetFirstSelectedItemPosition();

    while ( NULL != pos )
        {
        nSelItem = c_FileList.GetNextSelectedItem ( pos );
        sFile = c_FileList.GetItemText ( nSelItem, 0 );

        lsDraggedFiles.AddTail ( sFile );

        // Calculate the # of chars required to hold this string.

        uBuffSize += lstrlen ( sFile ) + 1;
        }

    // Add 1 extra for the final null char, and the size of the DROPFILES struct.

    uBuffSize = sizeof(DROPFILES) + sizeof(TCHAR) * (uBuffSize + 1);

    // Allocate memory from the heap for the DROPFILES struct.

    hgDrop = GlobalAlloc ( GHND | GMEM_SHARE, uBuffSize );

    if ( NULL == hgDrop )
        return;

    pDrop = (DROPFILES*) GlobalLock ( hgDrop );

    if ( NULL == pDrop )
        {
        GlobalFree ( hgDrop );
        return;
        }

    // Fill in the DROPFILES struct.

    pDrop->pFiles = sizeof(DROPFILES);

#ifdef _UNICODE
    // If we're compiling for Unicode, set the Unicode flag in the struct to
    // indicate it contains Unicode strings.

    pDrop->fWide = TRUE;
#endif;

    // Copy all the filenames into memory after the end of the DROPFILES struct.

    pos = lsDraggedFiles.GetHeadPosition();
    pszBuff = (TCHAR*) (LPBYTE(pDrop) + sizeof(DROPFILES));

    while ( NULL != pos )
        {
        lstrcpy ( pszBuff, (LPCTSTR) lsDraggedFiles.GetNext ( pos ) );
        pszBuff = 1 + _tcschr ( pszBuff, '\0' );
        }

    GlobalUnlock ( hgDrop );

    // Put the data in the data source.

    datasrc.CacheGlobalData ( CF_HDROP, hgDrop, &etc );

    // Add in our own custom data, so we know that the drag originated from our 
    // window.  CMyDropTarget::DragEnter() checks for this custom format, and
    // doesn't allow the drop if it's present.  This is how we prevent the user
    // from dragging and then dropping in our own window.
    // The data will just be a dummy bool.
HGLOBAL hgBool;

    hgBool = GlobalAlloc ( GHND | GMEM_SHARE, sizeof(bool) );

    if ( NULL == hgBool )
        {
        GlobalFree ( hgDrop );
        return;
        }

    // Put the data in the data source.

    etc.cfFormat = g_uCustomClipbrdFormat;
    
    datasrc.CacheGlobalData ( g_uCustomClipbrdFormat, hgBool, &etc );


    // Start the drag 'n' drop!

DROPEFFECT dwEffect = datasrc.DoDragDrop ( DROPEFFECT_COPY | DROPEFFECT_MOVE );

    // If the DnD completed OK, we remove all of the dragged items from our
    // list.

    switch ( dwEffect )
        {
        case DROPEFFECT_COPY:
        case DROPEFFECT_MOVE:
            {
            // Note: Don't call GlobalFree() because the data will be freed by the drop target.

            for ( nSelItem = c_FileList.GetNextItem ( -1, LVNI_SELECTED );
                  nSelItem != -1;
                  nSelItem = c_FileList.GetNextItem ( nSelItem, LVNI_SELECTED ) )
                {
                c_FileList.DeleteItem ( nSelItem );
                nSelItem--;
                }

            // Resize the list columns.

            c_FileList.SetColumnWidth ( 0, LVSCW_AUTOSIZE_USEHEADER );
            c_FileList.SetColumnWidth ( 1, LVSCW_AUTOSIZE_USEHEADER );
            c_FileList.SetColumnWidth ( 2, LVSCW_AUTOSIZE_USEHEADER );
            }
        break;

        case DROPEFFECT_NONE:
            {
            // This needs special handling, because on NT, DROPEFFECT_NONE
            // is returned for move operations, instead of DROPEFFECT_MOVE.
            // See Q182219 for the details.
            // So if we're on NT, we check each selected item, and if the
            // file no longer exists, it was moved successfully and we can
            // remove it from the list.

            if ( g_bNT )
                {
                bool bDeletedAnything = false;

                for ( nSelItem = c_FileList.GetNextItem ( -1, LVNI_SELECTED );
                      nSelItem != -1;
                      nSelItem = c_FileList.GetNextItem ( nSelItem, LVNI_SELECTED ) )
                    {
                    CString sFilename = c_FileList.GetItemText ( nSelItem, 0 );

                    if ( 0xFFFFFFFF == GetFileAttributes ( sFile ) &&
                         GetLastError() == ERROR_FILE_NOT_FOUND )
                        {
                        // We couldn't read the file's attributes, and GetLastError()
                        // says the file doesn't exist, so remove the corresponding 
                        // item from the list.

                        c_FileList.DeleteItem ( nSelItem );
                    
                        nSelItem--;
                        bDeletedAnything = true;
                        }
                    }

                // Resize the list columns if we deleted any items.

                if ( bDeletedAnything )
                    {
                    c_FileList.SetColumnWidth ( 0, LVSCW_AUTOSIZE_USEHEADER );
                    c_FileList.SetColumnWidth ( 1, LVSCW_AUTOSIZE_USEHEADER );
                    c_FileList.SetColumnWidth ( 2, LVSCW_AUTOSIZE_USEHEADER );

                    // Note: Don't call GlobalFree() because the data belongs to 
                    // the caller.
                    }
                else
                    {
                    // The DnD operation wasn't accepted, or was canceled, so we 
                    // should call GlobalFree() to clean up.

                    GlobalFree ( hgDrop );
                    GlobalFree ( hgBool );
                    }
                }   // end if (NT)
            else
                {
                // We're on 9x, and a return of DROPEFFECT_NONE always means
                // that the DnD operation was aborted.  We need to free the
                // allocated memory.

                GlobalFree ( hgDrop );
                GlobalFree ( hgBool );
                }
            }
        break;  // end case DROPEFFECT_NONE
        }   // end switch
}

void CMultiFilerDlg::OnRclickFilelist(NMHDR* pNMHDR, LRESULT* pResult) 
{
CPoint ptCursorPos;
CMenu  menu;

    // We'll show the menu at the current cursor position.

    GetCursorPos ( &ptCursorPos );

    VERIFY ( menu.LoadMenu ( IDR_LIST_CONTEXTMENU ));
    ASSERT ( NULL != menu.GetSubMenu(0) );  // check that there's a popup menu at index 0.

    // Show the menu!

    menu.GetSubMenu(0)->TrackPopupMenu ( TPM_RIGHTBUTTON, ptCursorPos.x,
                                         ptCursorPos.y, this );

    *pResult = 0;   // return value ignored
}

void CMultiFilerDlg::OnDropFiles(HDROP hdrop) 
{
UINT        uNumFiles;
TCHAR       szNextFile [MAX_PATH];
SHFILEINFO  rFileInfo;
LVFINDINFO  rlvFind = { LVFI_STRING, szNextFile };
LVITEM      rItem;
int         nIndex = c_FileList.GetItemCount();
HANDLE      hFind;
WIN32_FIND_DATA rFind;
TCHAR       szFileLen [64];

    // Get the # of files being dropped.

    uNumFiles = DragQueryFile ( hdrop, -1, NULL, 0 );

    for ( UINT uFile = 0; uFile < uNumFiles; uFile++ )
        {
        // Get the next filename from the HDROP info.

        if ( DragQueryFile ( hdrop, uFile, szNextFile, MAX_PATH ) > 0 )
            {
            // If the filename is already in the list, skip it.

            if ( -1 != c_FileList.FindItem ( &rlvFind, -1 ))
                continue;

            // Get the index of the file's icon in the system image list and
            // its type description.

            SHGetFileInfo ( szNextFile, 0, &rFileInfo, sizeof(rFileInfo),
                            SHGFI_SYSICONINDEX | SHGFI_ATTRIBUTES |
                              SHGFI_TYPENAME );

            // Set up an LVITEM for the file we're about to insert.

            ZeroMemory ( &rItem, sizeof(LVITEM) );

            rItem.mask    = LVIF_IMAGE | LVIF_TEXT;
            rItem.iItem   = nIndex;
            rItem.pszText = szNextFile;
            rItem.iImage  = rFileInfo.iIcon;

            // If the file has the hidden attribute set, its attributes in
            // the SHFILEINFO struct will include SFGAO_GHOSTED.  We'll mark
            // the item as "cut" in our list to give it the same ghosted look.

            if ( rFileInfo.dwAttributes & SFGAO_GHOSTED )
                {
                rItem.mask |= LVIF_STATE;
                rItem.state = rItem.stateMask = LVIS_CUT;
                }

            // Insert it into the list!

            c_FileList.InsertItem ( &rItem );

            // Set column #1 to the file's type description.

            c_FileList.SetItemText ( nIndex, 1, rFileInfo.szTypeName );

            // Get the file size, and put that in column #2.

            hFind = FindFirstFile ( szNextFile, &rFind );

            if ( INVALID_HANDLE_VALUE != hFind )
                {
                StrFormatByteSize ( rFind.nFileSizeLow, szFileLen, 64 );
                FindClose ( hFind );
                }

            c_FileList.SetItemText ( nIndex, 2, szFileLen );

            nIndex++;
            }
        }   // end for

    // Resize columns so all text is visible.

    c_FileList.SetColumnWidth ( 0, LVSCW_AUTOSIZE_USEHEADER );
    c_FileList.SetColumnWidth ( 1, LVSCW_AUTOSIZE_USEHEADER );
    c_FileList.SetColumnWidth ( 2, LVSCW_AUTOSIZE_USEHEADER );

    c_FileList.EnsureVisible ( nIndex-1, FALSE );

    DragFinish ( hdrop );
}


/////////////////////////////////////////////////////////////////////////////
// CMultiFilerDlg command handlers

void CMultiFilerDlg::OnSelectAll() 
{
int  nIndex, nMaxIndex = c_FileList.GetItemCount() - 1;

    for ( nIndex = 0; nIndex <= nMaxIndex; nIndex++ )
        {
        c_FileList.SetItemState ( nIndex, LVIS_SELECTED, LVIS_SELECTED );
        }
}

void CMultiFilerDlg::OnInvertSelection() 
{
int  nIndex, nMaxIndex = c_FileList.GetItemCount() - 1;
UINT uItemState;

    for ( nIndex = 0; nIndex <= nMaxIndex; nIndex++ )
        {
        uItemState = c_FileList.GetItemState ( nIndex, LVIS_SELECTED );

        c_FileList.SetItemState ( nIndex, uItemState ^ LVIS_SELECTED, 
                                  LVIS_SELECTED );
        }
}

void CMultiFilerDlg::OnRemoveFromList() 
{
int nSelItem;

    for ( nSelItem = c_FileList.GetNextItem ( -1, LVNI_SELECTED );
          nSelItem != -1;
          nSelItem = c_FileList.GetNextItem ( nSelItem, LVNI_SELECTED ) )
        {
        c_FileList.DeleteItem ( nSelItem );
        nSelItem--;
        }
}

void CMultiFilerDlg::OnClearList() 
{
    c_FileList.DeleteAllItems();    
}
