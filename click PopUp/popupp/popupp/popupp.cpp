// popupp.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "popupp.h"

// MenuFunctions.cpp: implementation of the MenuFunctions class.
//
//////////////////////////////////////////////////////////////////////


#include "MenuFunctions.h"

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

// this is a recursive function which will attempt
// to add the item "itemText" to the menu with the
// given ID number. The "itemText" will be parsed for
// delimiting "\" characters for levels between
// popup menus. If a popup menu does not exist, it will
// be created and inserted at the end of the menu
bool MenuFunctions::AddMenuItem(HMENU hTargetMenu, const CString& itemText, UINT itemID)
{
	bool bSuccess = false;

	ASSERT(itemText.GetLength() > 0);
	ASSERT(itemID != 0);
	ASSERT(hTargetMenu != NULL);

	// first, does the menu item have any required submenus to be found/created?
	if (itemText.Find('\\') >= 0)
	{
		// yes, we need to do a recursive call on a submenu handle and with that sub
		// menu name removed from itemText

		// 1:get the popup menu name
		CString popupMenuName = itemText.Left(itemText.Find('\\'));
		// 2:get the rest of the menu item name minus the delimiting '\' character
		CString remainingText = itemText.Right(itemText.GetLength() - popupMenuName.GetLength() - 1);
		// 3:See whether the popup menu already exists
		int itemCount = ::GetMenuItemCount(hTargetMenu);
		bool bFoundSubMenu = false;
		MENUITEMINFO menuItemInfo;

		memset(&menuItemInfo, 0, sizeof(MENUITEMINFO));
		menuItemInfo.cbSize = sizeof(MENUITEMINFO);
		menuItemInfo.fMask = MIIM_TYPE | MIIM_STATE | MIIM_ID | MIIM_SUBMENU;
		for (int itemIndex = 0 ; itemIndex < itemCount && !bFoundSubMenu ; itemIndex++)
		{
			::GetMenuItemInfo(hTargetMenu, itemIndex, TRUE, &menuItemInfo);
			if (menuItemInfo.hSubMenu != 0)
			{
				// this menu item is a popup menu (non popups give 0)
				TCHAR	buffer[MAX_PATH];
				::GetMenuString(hTargetMenu, itemIndex, buffer, MAX_PATH, MF_BYPOSITION);
				TRACE("%s\n", buffer);
				if (popupMenuName == buffer)
				{
					// this is the popup menu we have to add to
					bFoundSubMenu = true;
				}
			}
		}
		// 4: If exists, do recursive call, else create do recursive call and then insert it
		if (bFoundSubMenu)
		{
			bSuccess = AddMenuItem(menuItemInfo.hSubMenu, remainingText, itemID);
		}
		else
		{
			// we need to create a new sub menu and insert it
			HMENU hPopupMenu = ::CreatePopupMenu();
			if (hPopupMenu != NULL)
			{
				bSuccess = AddMenuItem(hPopupMenu, remainingText, itemID);
				if (bSuccess)
				{
					if (::AppendMenu(hTargetMenu, MF_POPUP, (UINT)hPopupMenu, popupMenuName) > 0)
					{
						bSuccess = true;
						// hPopupMenu now owned by hTargetMenu, we do not need to destroy it
					}
					else
					{
						// failed to insert the popup menu
						bSuccess = false;
						::DestroyMenu(hPopupMenu);	// stop a resource leak
					}
				}
			}
		}		
	}
	else
	{
		// no sub menu's required, add this item to this HMENU
		if (::AppendMenu(hTargetMenu, MF_BYCOMMAND, itemID, itemText) > 0)
		{
			// we successfully added the item to the menu
			bSuccess = true;
		}
	}

	return bSuccess;
}

// this is a recursive function which will attempt
// to add the item "itemText" to the menu with the
// given ID number. The "itemText" will be parsed for
// delimiting "\" characters for levels between
// popup menus. If a popup menu does not exist, it will
// be created and inserted at the specified position (if given)
// or the end of the menu if not
// this version of the procedure can have insert positions
// passed in using an int* array
bool MenuFunctions::AddMenuItem(
	HMENU hTargetMenu,				// menu to add to
	const CString& itemText,		// item name
	UINT itemID,					// 0 when separator insertion
	int *pInsertPositions)			// can be NULL
{
	bool bSuccess = false;

	ASSERT(itemText.GetLength() > 0);
	ASSERT(hTargetMenu != NULL);

	// work out where this item will be inserted
	int itemCount = ::GetMenuItemCount(hTargetMenu);
	int position = (pInsertPositions != NULL) ? *pInsertPositions : itemCount;
	int *pNextLevelInsert = NULL;

	// insert the option at a required position
	// -ve positions are from the end of the menu level
	if (position < 0)
	{
		// -ve positions are inserted from the end upwards
		position = max(0, itemCount + position);
	}
	else
	{
		// make sure we are trying to insert into a valid position
		position = min (position, itemCount);
	}
	if (pInsertPositions != NULL)
	{
		// have correct pointer for next recursive call, if required
		pNextLevelInsert = pInsertPositions + 1;
	}

	// first, does the menu item have any required submenus to be found/created?
	if (itemText.Find('\\') >= 0)
	{
		// yes, we need to do a recursive call on a submenu handle and with that sub
		// menu name removed from itemText

		// 1:get the popup menu name
		CString popupMenuName = itemText.Left(itemText.Find('\\'));
		// 2:get the rest of the menu item name minus the delimiting '\' character
		CString remainingText = itemText.Right(itemText.GetLength() - popupMenuName.GetLength() - 1);
		// 3:See whether the popup menu already exists
		int itemCount = ::GetMenuItemCount(hTargetMenu);
		bool bFoundSubMenu = false;
		MENUITEMINFO menuItemInfo;

		memset(&menuItemInfo, 0, sizeof(MENUITEMINFO));
		menuItemInfo.cbSize = sizeof(MENUITEMINFO);
		menuItemInfo.fMask = MIIM_TYPE | MIIM_STATE | MIIM_ID | MIIM_SUBMENU;
		for (int itemIndex = 0 ; itemIndex < itemCount && !bFoundSubMenu ; itemIndex++)
		{
			::GetMenuItemInfo(
					hTargetMenu, 
					itemIndex, 
					TRUE, 
					&menuItemInfo);
			if (menuItemInfo.hSubMenu != 0)
			{
				// this menu item is a popup menu (non popups give 0)
				TCHAR	buffer[MAX_PATH];
				::GetMenuString(
						hTargetMenu, 
						itemIndex, 
						buffer, 
						MAX_PATH, 
						MF_BYPOSITION);
				if (popupMenuName == buffer)
				{
					// this is the popup menu we have to add to
					bFoundSubMenu = true;
				}
			}
		}
		// 4: If exists, do recursive call, else create do recursive call and then insert it
		if (bFoundSubMenu)
		{
			bSuccess = AddMenuItem(
					menuItemInfo.hSubMenu, 
					remainingText, 
					itemID, 
					pNextLevelInsert);
		}
		else
		{
			// we need to create a new sub menu and insert it
			HMENU hPopupMenu = ::CreatePopupMenu();
			if (hPopupMenu != NULL)
			{
				bSuccess = AddMenuItem(
						hPopupMenu, 
						remainingText, 
						itemID, 
						pNextLevelInsert);
				if (bSuccess)
				{
					// we want to insert the new menu item by position
					bSuccess = (::InsertMenu(hTargetMenu, position, MF_BYPOSITION | MF_POPUP, (UINT)hPopupMenu, popupMenuName) > 0);
					if (!bSuccess)
					{
						// failed to insert the popup menu
						bSuccess = false;
						::DestroyMenu(hPopupMenu);	// stop a resource leak
					}
				}
			}
		}		
	}
	else
	{
		// no sub menu's required, add this item to this HMENU
		if (itemID > 0)
		{
			
			bSuccess = (::InsertMenu(hTargetMenu, position, MF_BYPOSITION | MF_STRING, itemID, itemText) > 0);
		}
		else
		{
			// inserting a separator
			bSuccess = (::InsertMenu(hTargetMenu, position, MF_BYPOSITION | MF_SEPARATOR, itemID, "") > 0);
		}
	}	

	return bSuccess;
}

int MenuFunctions::CalculateMenuHeight(HMENU hMenu)
{
    ASSERT(hMenu != NULL);
    int height = ::GetMenuItemCount(hMenu) * ::GetSystemMetrics(SM_CYMENUSIZE);

    // take the menu borders into account
    height += ::GetSystemMetrics(SM_CYEDGE);
    return height;
}

namespace MenuFunctions
{
    const int f_iconWidth = 16;
    const int f_menuBorder = 8;
    const int f_popupArrowSize = 8;
}

int MenuFunctions::CalculateMenuWidth(HMENU hMenu)
{
    // create a copy of the font that shoule be used to render a menu
	NONCLIENTMETRICS nm;
	LOGFONT lf;
	CFont menuFont;
    TCHAR menuItemText[_MAX_PATH];

    nm.cbSize = sizeof(NONCLIENTMETRICS);
	VERIFY(SystemParametersInfo(SPI_GETNONCLIENTMETRICS, nm.cbSize,&nm, 0));
    lf = nm.lfMenuFont;
    menuFont.CreateFontIndirect(&lf);

    CDC dc;

    dc.Attach(::GetDC(NULL));       // get screen DC
    dc.SaveDC();
    dc.SelectObject(&menuFont);

    // look at each item and work out its width
    int maxWidth = 0;
    int itemCount = ::GetMenuItemCount(hMenu);

    for (int item = 0 ; item < itemCount ; item++)
    {
        // get each items data
        int itemWidth = f_iconWidth + f_menuBorder;
	    MENUITEMINFO	itemInfo;
        itemInfo.dwTypeData = menuItemText;
        itemInfo.cch = _MAX_PATH - 1;

	    memset(&itemInfo, 0, sizeof(MENUITEMINFO));
	    itemInfo.cbSize = sizeof(MENUITEMINFO);

	    itemInfo.fMask = MIIM_SUBMENU | MIIM_STRING;
	    ::GetMenuItemInfo(hMenu, item, TRUE, &itemInfo);

        if (itemInfo.hSubMenu != 0)
        {
            // its a popup menu, include the width of the > arrow
            itemWidth += f_popupArrowSize;
        }
        if (itemInfo.wID == 0)
        {
            // its a separator, dont measure the text
        }
        else
        {
            // measure the text using the font
            CSize textSize;
            CString itemText = menuItemText;
            // expand any tabs in the menu text
            itemText.Replace("\t", "    ");

            textSize = dc.GetTextExtent(itemText);
            itemWidth += textSize.cx;
        }
        if (itemWidth > maxWidth)
        {
            maxWidth = itemWidth;
        }
    }

    dc.RestoreDC(-1);
    ::ReleaseDC(NULL, dc.Detach());
    return maxWidth;
}
