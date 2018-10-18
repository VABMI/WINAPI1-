// stdafx.h : include file for standard system include files,
//  or project specific include files that are used frequently, but
//      are changed infrequently
//

#if !defined(AFX_STDAFX_H__96D5C147_EDDE_4F62_9B9B_BA4C7EC96B1D__INCLUDED_)
#define AFX_STDAFX_H__96D5C147_EDDE_4F62_9B9B_BA4C7EC96B1D__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#define VC_EXTRALEAN		// Exclude rarely-used stuff from Windows headers

#define WINVER 0x0500       // Enable Win2K features
#define _WIN32_IE 0x0500    // Enable IE5 common control features

#include <afxwin.h>         // MFC core and standard components
#include <afxext.h>         // MFC extensions
#include <afxdtctl.h>		// MFC support for Internet Explorer 4 Common Controls
#ifndef _AFX_NO_AFXCMN_SUPPORT
#include <afxcmn.h>			// MFC support for Windows Common Controls
#endif // _AFX_NO_AFXCMN_SUPPORT
#include <afxole.h>

#include <shlwapi.h>

extern UINT g_uCustomClipbrdFormat;
extern bool g_bNT;

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_STDAFX_H__96D5C147_EDDE_4F62_9B9B_BA4C7EC96B1D__INCLUDED_)
