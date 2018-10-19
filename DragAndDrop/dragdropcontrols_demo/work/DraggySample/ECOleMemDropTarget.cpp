#include "stdafx.h"

#include "ECOleMemDropTarget.h"

//*************************************************************
//	ECOleMemDropTarget
//*************************************************************
ECOleMemDropTarget::ECOleMemDropTarget()
	:	m_DropTargetWnd(NULL),
		m_dwRefCount(1),
		m_RegisterType(0),
		m_KeyState(0L),
		m_Data(NULL)
{
	OleInitialize(NULL);

	//	required: these MUST be strong locked
	CoLockObjectExternal(this, TRUE, 0);
}

ECOleMemDropTarget::~ECOleMemDropTarget()
{
	//	unlock
	CoLockObjectExternal(this, FALSE, 0);

	// bye bye COM
	OleUninitialize();
}

HRESULT ECOleMemDropTarget::QueryInterface(REFIID iid, void **ppvObject)
{
	if(ppvObject == NULL)
		return E_FAIL;
	
    if (iid == IID_IUnknown)
    {
		AddRef();
		(*ppvObject) = this;
		return S_OK;
    }
	//	compare guids fast and dirty
    if (iid == IID_IDropTarget)
	{
		AddRef();
		(*ppvObject) = this;
		return S_OK;
	}

	return E_FAIL;
}

ULONG ECOleMemDropTarget::AddRef(void)
{
	m_dwRefCount++;

	return m_dwRefCount;
}

ULONG ECOleMemDropTarget::Release(void)
{
	m_dwRefCount--;
//	if(0 == m_dwRefCount)
//		delete this;

	return m_dwRefCount;
}

//*************************************************************
//	Register
//		Called by whom implements us so we can serve
//*************************************************************
BOOL ECOleMemDropTarget::Register(CWnd* pWnd, UINT pDataType)
{
	if(NULL == pWnd)
		return E_FAIL;
	
	if(0L == pDataType)
		return E_FAIL;

	//	keep
	m_DropTargetWnd = pWnd;
	m_RegisterType = pDataType;

	//	this is ok, we have it
	DWORD hRes = ::RegisterDragDrop(m_DropTargetWnd->m_hWnd, this);
	if(SUCCEEDED(hRes))
		return TRUE;

	//	wont accept data now
	return FALSE;
}

//*************************************************************
//	Revoke
//		Unregister us as a target
//*************************************************************
void ECOleMemDropTarget::Revoke()
{
	if(NULL == m_DropTargetWnd)
		return;

	RevokeDragDrop(m_DropTargetWnd->m_hWnd);
}
 
//*************************************************************
//	DragEnter
//*************************************************************
HRESULT	ECOleMemDropTarget::DragEnter(struct IDataObject *pDataObject, unsigned long grfKeyState, struct _POINTL pMouse, unsigned long * pDropEffect)
{
	if(pDataObject == NULL)
		return E_FAIL;	//	must have data

	//	keep point
	m_DropPoint.x = pMouse.x;
	m_DropPoint.y = pMouse.y;

	//	keep key
	m_KeyState = grfKeyState;

	//	call top
	*pDropEffect = GotEnter();

	return S_OK;
}

//*************************************************************
//	DragOver
//		Coming over!
//*************************************************************
HRESULT	ECOleMemDropTarget::DragOver(unsigned long grfKeyState, struct _POINTL pMouse, unsigned long *pEffect)
{
	//	keep point
	m_DropPoint.x = pMouse.x;
	m_DropPoint.y = pMouse.y;

	//	keep key
	m_KeyState = grfKeyState;

	//	call top
	*pEffect = GotDrag();

	return S_OK;
}

//*************************************************************
//	DragLeave
//		Free! At last!
//*************************************************************
HRESULT	ECOleMemDropTarget::DragLeave(void)
{
	GotLeave();

	return S_OK;
}

//*************************************************************
//	Drop
//		Released stuff here. We check for text, but this could
//		also be mem, a clipboard, ... Not real clean for a baseclass
//*************************************************************
HRESULT	ECOleMemDropTarget::Drop(struct IDataObject *pDataObject, unsigned long grfKeyState, struct _POINTL pMouse, unsigned long *pdwEffect)
{
	if(NULL == pDataObject)
		return E_FAIL;

	//	do final effect
	*pdwEffect = DROPEFFECT_COPY;
	
	//	Check the data
	FORMATETC iFormat;
	ZeroMemory(&iFormat, sizeof(FORMATETC));

	STGMEDIUM iMedium;
	ZeroMemory(&iMedium, sizeof(STGMEDIUM));

	//	data
	iFormat.cfFormat = m_RegisterType;	//	its my type
	iFormat.dwAspect = DVASPECT_CONTENT;
	iFormat.lindex = -1;			//	give me all baby
	iFormat.tymed = TYMED_HGLOBAL;	//	want mem

	HRESULT hRes = pDataObject->GetData(&iFormat, &iMedium);
	if(FAILED(hRes))
	{
		TRACE("BAD Data format clipboard");
		return hRes;
	}

	//	we have the data, get it		
	BYTE *iMem = (BYTE *)::GlobalLock(iMedium.hGlobal);
	DWORD iLen = ::GlobalSize(iMedium.hGlobal);

	//	Send small notify
	TRACE("OnDrop received data = '%s', '%d'\r\n", (char *)m_Data, iLen);

	//	pass over
	m_Data = iMem;
	
	//	keep point
	m_DropPoint.x = pMouse.x;
	m_DropPoint.y = pMouse.y;

	//	keep key
	m_KeyState = grfKeyState;

	//	notify parent of drop
	GotDrop();

	::GlobalUnlock(iMedium.hGlobal);

	//	free data
	if(iMedium.pUnkForRelease != NULL)
		iMedium.pUnkForRelease->Release();

	return S_OK;
}

//*************************************************************
//	Stub implementation
//		Real stuff would be done in parent
//*************************************************************
void ECOleMemDropTarget::GotDrop()
{
}

DWORD ECOleMemDropTarget::GotDrag(void)
{
	return DROPEFFECT_LINK;
}

void ECOleMemDropTarget::GotLeave(void)
{
}

DWORD ECOleMemDropTarget::GotEnter(void)
{
	return DROPEFFECT_LINK;
}

