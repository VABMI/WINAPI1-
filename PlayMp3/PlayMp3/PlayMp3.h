
// PlayMp3.h : main header file for the PROJECT_NAME application
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


// CPlayMp3App:
// See PlayMp3.cpp for the implementation of this class
//

class CPlayMp3App : public CWinApp
{
public:
	CPlayMp3App();

// Overrides
public:
	virtual BOOL InitInstance();

// Implementation

	DECLARE_MESSAGE_MAP()
};

extern CPlayMp3App theApp;