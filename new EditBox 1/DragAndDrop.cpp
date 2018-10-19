//#include "head.h"


/*


STDMETHODIMP CDropTarget::Drop (LPDATAOBJECT pDataObj, 
    DWORD grfKeyState, POINTL pointl, LPDWORD pdwEffect)  
{ 
    FORMATETC fmtetc; 
    SCODE sc = S_OK; 
 
    UndrawDragFeedback(); // removes any visual feedback 
 
    // QueryDrop returns TRUE if the app. can accept a drop based on 
    // the current key state, requested action, and cursor position. 
    if (pDataObj && QueryDrop(grfKeyState,pointl,FALSE,pdwEffect)) { 
        m_pDoc->m_lpSite = CSimpleSite::Create(m_pDoc); 

        m_pDoc->m_lpSite->m_dwDrawAspect = DVASPECT_CONTENT; 
 
        // Initialize the FORMATETC structure. 
        fmtetc.cfFormat = NULL; 
        fmtetc.ptd = NULL; 
        fmtetc.lindex = -1; 
        fmtetc.dwAspect = DVASPECT_CONTENT; // draws object's content 
        fmtetc.tymed = TYMED_NULL; 
        HRESULT hrErr = OleCreateFromData 
            (pDataObj,IID_IOleObject,OLERENDER_DRAW, 
            	&fmtetc, &m_pDoc->m_lpSite->m_OleClientSite, 

            m_pDoc->m_lpSite->m_lpObjStorage, 
            (LPVOID FAR *)&m_pDoc->m_lpSite->m_lpOleObject); 
        if (hrErr == NOERROR) 
            // The object was created successfully. 
        else 
            // The object creation failed. 
            sc = GetScode(hrErr); 
    } 
    return ResultFromScode(sc); 
} 
*/