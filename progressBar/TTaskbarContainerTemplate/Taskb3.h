// Taskb3.h : Haupt-Header-Datei für die Anwendung TASKB3
//

#if !defined(AFX_TASKB3_H__039F0FBE_F360_4C26_8DA2_2409D3D41E34__INCLUDED_)
#define AFX_TASKB3_H__039F0FBE_F360_4C26_8DA2_2409D3D41E34__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// Hauptsymbole

/////////////////////////////////////////////////////////////////////////////
// CTaskb3App:
// Siehe Taskb3.cpp für die Implementierung dieser Klasse
//

class CTaskb3App : public CWinApp
{
public:
	CTaskb3App();

// Überladungen
	// Vom Klassenassistenten generierte Überladungen virtueller Funktionen
	//{{AFX_VIRTUAL(CTaskb3App)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementierung

	//{{AFX_MSG(CTaskb3App)
		// HINWEIS - An dieser Stelle werden Member-Funktionen vom Klassen-Assistenten eingefügt und entfernt.
		//    Innerhalb dieser generierten Quelltextabschnitte NICHTS VERÄNDERN!
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ fügt unmittelbar vor der vorhergehenden Zeile zusätzliche Deklarationen ein.

#endif // !defined(AFX_TASKB3_H__039F0FBE_F360_4C26_8DA2_2409D3D41E34__INCLUDED_)
