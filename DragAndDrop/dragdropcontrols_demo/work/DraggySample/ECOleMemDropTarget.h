#ifndef ECOLEMEMDROPTARGET_H
#define ECOLEMEMDROPTARGET_H

//*************************************************************
//	ECOleMemDropTarget
//		Class that inherits from IDropTarget
//
//		Implements the "repeat 6000000 times QueryInterface, 
//		AddRef, Release from IUnknown" offcourse, and the
//		members from IDropTarget
//
//		Class will be used as a base class, lets try some reusable
//		OO for a change ;-)
//
//		Most important are the virtual Got* members, they will 
//		call our parents. Note that these are NOT implemented pure
//		virtual. Little use, as sometimes we just dont want them
//*************************************************************
interface ECOleMemDropTarget : public IDropTarget
{
public:
	ECOleMemDropTarget();
	~ECOleMemDropTarget();

	//	basic IUnknown stuff
	HRESULT	STDMETHODCALLTYPE	QueryInterface(REFIID iid, void ** ppvObject); 
	ULONG	STDMETHODCALLTYPE	AddRef(void); 
	ULONG	STDMETHODCALLTYPE	Release(void); 

	HRESULT	STDMETHODCALLTYPE	DragEnter(struct IDataObject *,unsigned long,struct _POINTL,unsigned long *); 
	HRESULT	STDMETHODCALLTYPE	DragOver(unsigned long,struct _POINTL,unsigned long *); 
	HRESULT	STDMETHODCALLTYPE	DragLeave(void);
	HRESULT	STDMETHODCALLTYPE	Drop(struct IDataObject *,unsigned long,struct _POINTL,unsigned long *);

	//	called by parents
	BOOL						Register(CWnd* pWnd, UINT pDataType);
	void						Revoke();

	//	call parent we have goodies
	virtual	void				GotDrop(void);
	virtual	DWORD				GotDrag(void);
	virtual	void				GotLeave(void);
	virtual	DWORD				GotEnter(void);
public:
	BYTE			*m_Data;

	CPoint			m_DropPoint;

	DWORD			m_KeyState;

protected:
	CWnd*			m_DropTargetWnd;

	UINT			m_RegisterType;

	DWORD			m_dwRefCount;
};

#endif