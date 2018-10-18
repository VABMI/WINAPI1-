// MultiFilerView.cpp : implementation of the CMultiFilerView class
//

#include "stdafx.h"
#include "MultiFiler.h"

#include "MultiFilerDoc.h"
#include "MultiFilerView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

//********************************************************************
// If you don't have a recent Platform SDK installed, you'll get linker
// errors on CLSID_DragDropHelper and IID_IDropTargetHelper, and you
// won't have the definition of IDropTargetHelper.  Uncomment the
// following stuff to fix that.
/*
struct IDropTargetHelper : public IUnknown
{
    // IUnknown methods
    STDMETHOD (QueryInterface)(THIS_ REFIID riid, void **ppv) PURE;
    STDMETHOD_(ULONG, AddRef) ( THIS ) PURE;
    STDMETHOD_(ULONG, Release) ( THIS ) PURE;

    // IDropTargetHelper
    STDMETHOD (DragEnter)(THIS_ HWND hwndTarget, IDataObject* pDataObject,
                          POINT* ppt, DWORD dwEffect) PURE;
    STDMETHOD (DragLeave)(THIS) PURE;
    STDMETHOD (DragOver)(THIS_ POINT* ppt, DWORD dwEffect) PURE;
    STDMETHOD (Drop)(THIS_ IDataObject* pDataObject, POINT* ppt,
                     DWORD dwEffect) PURE;
    STDMETHOD (Show)(THIS_ BOOL fShow) PURE;
};

// {4657278A-411B-11d2-839A-00C04FD918D0}
extern "C" const GUID __declspec(selectany) CLSID_DragDropHelper = 
    { 0x4657278a, 0x411b, 0x11d2, { 0x83, 0x9a, 0x0, 0xc0, 0x4f, 0xd9, 0x18, 0xd0 }};

// {4657278B-411B-11d2-839A-00C04FD918D0}
extern "C" const GUID __declspec(selectany) IID_IDropTargetHelper = 
    { 0x4657278b, 0x411b, 0x11d2, { 0x83, 0x9a, 0x0, 0xc0, 0x4f, 0xd9, 0x18, 0xd0 }};
*/
//********************************************************************

/////////////////////////////////////////////////////////////////////////////
// CMultiFilerView

IMPLEMENT_DYNCREATE(CMultiFilerView, CListView)

BEGIN_MESSAGE_MAP(CMultiFilerView, CListView)
	//{{AFX_MSG_MAP(CMultiFilerView)
	ON_NOTIFY_REFLECT(NM_RCLICK, OnRclick)
	ON_NOTIFY_REFLECT(LVN_BEGINDRAG, OnBegindrag)
	ON_COMMAND(IDC_SELECT_ALL, OnSelectAll)
	ON_COMMAND(IDC_INVERT_SELECTION, OnInvertSelection)
	ON_COMMAND(IDC_REMOVE_FROM_LIST, OnRemoveFromList)
	ON_COMMAND(IDC_CLEAR_LIST, OnClearList)
	ON_WM_DESTROY()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CMultiFilerView construction/destruction

CMultiFilerView::CMultiFilerView() : m_bUseDnDHelper(false),
                                     m_piDropHelper(NULL)
{
    // Create an instance of the shell DnD helper object.

    if ( SUCCEEDED( CoCreateInstance ( CLSID_DragDropHelper, NULL, 
                                       CLSCTX_INPROC_SERVER,
                                       IID_IDropTargetHelper, 
                                       (void**) &m_piDropHelper ) ))
        {
        m_bUseDnDHelper = true;
        }
}

CMultiFilerView::~CMultiFilerView()
{
    if ( NULL != m_piDropHelper )
        m_piDropHelper->Release();
}

BOOL CMultiFilerView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CListView::PreCreateWindow(cs);
}

/////////////////////////////////////////////////////////////////////////////
// CMultiFilerView drawing

void CMultiFilerView::OnDraw(CDC* pDC)
{
	CMultiFilerDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	// TODO: add draw code for native data here
}

void CMultiFilerView::OnInitialUpdate()
{
	CListView::OnInitialUpdate();

CListCtrl& list = GetListCtrl();

    // Register our view as a drop target.

    m_droptarget.Register ( this );

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

    list.ModifyStyle ( LVS_TYPEMASK, 
                       LVS_REPORT | LVS_NOSORTHEADER | LVS_SHAREIMAGELISTS );

    list.SetImageList ( &m_imglist, LVSIL_SMALL );

    // Enable tooltips for items that aren't completely visible.

    list.SetExtendedStyle ( LVS_EX_INFOTIP );

    // Set up the list control columns.

    list.InsertColumn ( 0, _T("Filename"), LVCFMT_LEFT, 0, 0 );
    list.InsertColumn ( 1, _T("Type"), LVCFMT_LEFT, 0, 1 );
    list.InsertColumn ( 2, _T("Size"), LVCFMT_LEFT, 0, 2 );

    list.SetColumnWidth ( 0, LVSCW_AUTOSIZE_USEHEADER );
    list.SetColumnWidth ( 1, LVSCW_AUTOSIZE_USEHEADER );
    list.SetColumnWidth ( 2, LVSCW_AUTOSIZE_USEHEADER );
}

/////////////////////////////////////////////////////////////////////////////
// CMultiFilerView diagnostics

#ifdef _DEBUG
void CMultiFilerView::AssertValid() const
{
	CListView::AssertValid();
}

void CMultiFilerView::Dump(CDumpContext& dc) const
{
	CListView::Dump(dc);
}

CMultiFilerDoc* CMultiFilerView::GetDocument() // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CMultiFilerDoc)));
	return (CMultiFilerDoc*)m_pDocument;
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CMultiFilerView message handlers

void CMultiFilerView::OnDestroy() 
{
    // Detach our CImageList from the system image list.

    if ( NULL != m_imglist.GetSafeHandle() )
        m_imglist.Detach();

    CListView::OnDestroy();
}

void CMultiFilerView::OnRclick(NMHDR* pNMHDR, LRESULT* pResult) 
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

void CMultiFilerView::OnBegindrag(NMHDR* pNMHDR, LRESULT* pResult) 
{
NMLISTVIEW*    pNMLV = (NMLISTVIEW*) pNMHDR;
CListCtrl&     list = GetListCtrl();
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

    pos = list.GetFirstSelectedItemPosition();

    while ( NULL != pos )
        {
        nSelItem = list.GetNextSelectedItem ( pos );
        sFile = list.GetItemText ( nSelItem, 0 );

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
#endif

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
    // window.  OnDragEnter() checks for this custom format, and
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
            // The files were copied or moved.
            // Note: Don't call GlobalFree() because the data will be freed by the drop target.

            for ( nSelItem = list.GetNextItem ( -1, LVNI_SELECTED );
                  nSelItem != -1;
                  nSelItem = list.GetNextItem ( nSelItem, LVNI_SELECTED ) )
                {
                list.DeleteItem ( nSelItem );
                nSelItem--;
                }

            // Resize the list columns.

            list.SetColumnWidth ( 0, LVSCW_AUTOSIZE_USEHEADER );
            list.SetColumnWidth ( 1, LVSCW_AUTOSIZE_USEHEADER );
            list.SetColumnWidth ( 2, LVSCW_AUTOSIZE_USEHEADER );
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

                for ( nSelItem = list.GetNextItem ( -1, LVNI_SELECTED );
                      nSelItem != -1;
                      nSelItem = list.GetNextItem ( nSelItem, LVNI_SELECTED ) )
                    {
                    CString sFilename = list.GetItemText ( nSelItem, 0 );

                    if ( 0xFFFFFFFF == GetFileAttributes ( sFile ) &&
                         GetLastError() == ERROR_FILE_NOT_FOUND )
                        {
                        // We couldn't read the file's attributes, and GetLastError()
                        // says the file doesn't exist, so remove the corresponding 
                        // item from the list.

                        list.DeleteItem ( nSelItem );
                    
                        nSelItem--;
                        bDeletedAnything = true;
                        }
                    }

                // Resize the list columns if we deleted any items.

                if ( bDeletedAnything )
                    {
                    list.SetColumnWidth ( 0, LVSCW_AUTOSIZE_USEHEADER );
                    list.SetColumnWidth ( 1, LVSCW_AUTOSIZE_USEHEADER );
                    list.SetColumnWidth ( 2, LVSCW_AUTOSIZE_USEHEADER );

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


/////////////////////////////////////////////////////////////////////////////
// CMultiFilerView drag 'n' drop handlers

DROPEFFECT CMultiFilerView::OnDragEnter(COleDataObject* pDataObject, DWORD dwKeyState, CPoint point) 
{
DROPEFFECT dwEffect = DROPEFFECT_NONE;

    // Check for our own custom clipboard format in the data object.  If it's
    // present, then the DnD was initiated from our own window, and we won't
    // accept the drop.
    // If it's not present, then we check for CF_HDROP data in the data object.

    if ( NULL == pDataObject->GetGlobalData ( g_uCustomClipbrdFormat ))
        {
        // Look for CF_HDROP data in the data object, and accept the drop if
        // it's there.

        if ( NULL != pDataObject->GetGlobalData ( CF_HDROP ) )
            dwEffect = DROPEFFECT_COPY;
        }

    // Call the DnD helper.

    if ( m_bUseDnDHelper )
        {
        // The DnD helper needs an IDataObject interface, so get one from
        // the COleDataObject.  Note that the FALSE param means that
        // GetIDataObject will not AddRef() the returned interface, so 
        // we do not Release() it.

        IDataObject* piDataObj = pDataObject->GetIDataObject ( FALSE ); 

        m_piDropHelper->DragEnter ( GetSafeHwnd(), piDataObj, 
                                    &point, dwEffect );
        }

    return dwEffect;
}

DROPEFFECT CMultiFilerView::OnDragOver(COleDataObject* pDataObject, DWORD dwKeyState, CPoint point) 
{
DROPEFFECT dwEffect = DROPEFFECT_NONE;

    // Check for our own custom clipboard format in the data object.  If it's
    // present, then the DnD was initiated from our own window, and we won't
    // accept the drop.
    // If it's not present, then we check for CF_HDROP data in the data object.

    if ( NULL == pDataObject->GetGlobalData ( g_uCustomClipbrdFormat ))
        {
        // Look for CF_HDROP data in the data object, and accept the drop if
        // it's there.

        if ( NULL != pDataObject->GetGlobalData ( CF_HDROP ) )
            dwEffect = DROPEFFECT_COPY;
        }

    // Call the DnD helper.

    if ( m_bUseDnDHelper )
        {
        m_piDropHelper->DragOver ( &point, dwEffect );
        }

    return dwEffect;
}

BOOL CMultiFilerView::OnDrop(COleDataObject* pDataObject, DROPEFFECT dropEffect, CPoint point) 
{
BOOL bRet;

    // Read the CF_HDROP data and put the files in the main window's list.

    bRet = ReadHdropData ( pDataObject );

    // Call the DnD helper.

    if ( m_bUseDnDHelper )
        {
        // The DnD helper needs an IDataObject interface, so get one from
        // the COleDataObject.  Note that the FALSE param means that
        // GetIDataObject will not AddRef() the returned interface, so 
        // we do not Release() it.

        IDataObject* piDataObj = pDataObject->GetIDataObject ( FALSE ); 

        m_piDropHelper->Drop ( piDataObj, &point, dropEffect );
        }
    
    return bRet;
}

void CMultiFilerView::OnDragLeave() 
{
    if ( m_bUseDnDHelper )
        {
        m_piDropHelper->DragLeave();
        }
}

// ReadHdropData() reads CF_HDROP data from the passed-in data object, and 
// puts all dropped files/folders into the main window's list control.
BOOL CMultiFilerView::ReadHdropData ( COleDataObject* pDataObject )
{
HGLOBAL     hg;
HDROP       hdrop;
UINT        uNumFiles;
TCHAR       szNextFile [MAX_PATH];
SHFILEINFO  rFileInfo;
LVFINDINFO  rlvFind = { LVFI_STRING, szNextFile };
LVITEM      rItem;
CListCtrl&  list = GetListCtrl();
int         nIndex = list.GetItemCount();
HANDLE      hFind;
WIN32_FIND_DATA rFind;
TCHAR       szFileLen [64];

    // Get the HDROP data from the data object.

    hg = pDataObject->GetGlobalData ( CF_HDROP );
    
    if ( NULL == hg )
        {
        return FALSE;
        }

    hdrop = (HDROP) GlobalLock ( hg );

    if ( NULL == hdrop )
        {
        GlobalUnlock ( hg );
        return FALSE;
        }

    // Get the # of files being dropped.

    uNumFiles = DragQueryFile ( hdrop, -1, NULL, 0 );

    for ( UINT uFile = 0; uFile < uNumFiles; uFile++ )
        {
        // Get the next filename from the HDROP info.

        if ( DragQueryFile ( hdrop, uFile, szNextFile, MAX_PATH ) > 0 )
            {
            // If the filename is already in the list, skip it.

            if ( -1 != list.FindItem ( &rlvFind, -1 ))
                continue;

            // Get the index of the file's icon in the system image list and
            // it's type description.

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

            list.InsertItem ( &rItem );

            // Set column #1 to the file's type description.

            list.SetItemText ( nIndex, 1, rFileInfo.szTypeName );

            // Get the file size, and put that in column #2.

            hFind = FindFirstFile ( szNextFile, &rFind );

            if ( INVALID_HANDLE_VALUE != hFind )
                {
                StrFormatByteSize ( rFind.nFileSizeLow, szFileLen, 64 );
                FindClose ( hFind );
                }

            list.SetItemText ( nIndex, 2, szFileLen );

            nIndex++;
            }
        }

    // Resize columns so all text is visible.

    list.SetColumnWidth ( 0, LVSCW_AUTOSIZE_USEHEADER );
    list.SetColumnWidth ( 1, LVSCW_AUTOSIZE_USEHEADER );
    list.SetColumnWidth ( 2, LVSCW_AUTOSIZE_USEHEADER );

    list.EnsureVisible ( nIndex-1, FALSE );

    return TRUE;
}


/////////////////////////////////////////////////////////////////////////////
// CMultiFilerView command handlers

void CMultiFilerView::OnSelectAll() 
{
CListCtrl& list = GetListCtrl();
int        nIndex, nMaxIndex = list.GetItemCount() - 1;

    for ( nIndex = 0; nIndex <= nMaxIndex; nIndex++ )
        {
        list.SetItemState ( nIndex, LVIS_SELECTED, LVIS_SELECTED );
        }
}

void CMultiFilerView::OnInvertSelection() 
{
CListCtrl& list = GetListCtrl();
int        nIndex;
int        nMaxIndex = list.GetItemCount() - 1;
UINT       uItemState;

    for ( nIndex = 0; nIndex <= nMaxIndex; nIndex++ )
        {
        uItemState = list.GetItemState ( nIndex, LVIS_SELECTED );

        list.SetItemState ( nIndex, uItemState ^ LVIS_SELECTED, 
                            LVIS_SELECTED );
        }
}

void CMultiFilerView::OnRemoveFromList() 
{
CListCtrl& list = GetListCtrl();
int        nSelItem;

    for ( nSelItem = list.GetNextItem ( -1, LVNI_SELECTED );
          nSelItem != -1;
          nSelItem = list.GetNextItem ( nSelItem, LVNI_SELECTED ) )
        {
        list.DeleteItem ( nSelItem );
        nSelItem--;
        }
}

void CMultiFilerView::OnClearList() 
{
    GetListCtrl().DeleteAllItems();
}
