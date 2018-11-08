// MenuFunctions.h: Definitions of the MenuFunctins namespace functions
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_MENUFUNCTIONS_H__D5783CD5_BEBB_4EE8_8889_BE6EA42EB2D7__INCLUDED_)
#define AFX_MENUFUNCTIONS_H__D5783CD5_BEBB_4EE8_8889_BE6EA42EB2D7__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

namespace MenuFunctions  
{
	bool AddMenuItem(HMENU hTargetMenu, const CString& itemText, UINT itemID);
	bool AddMenuItem(HMENU hTargetMenu, const CString& itemText, UINT itemID, int* pInsertPositions);
	int CalculateMenuHeight(HMENU hMenu);
    int CalculateMenuWidth(HMENU hMenu);
};

#endif // !defined(AFX_MENUFUNCTIONS_H__D5783CD5_BEBB_4EE8_8889_BE6EA42EB2D7__INCLUDED_)
