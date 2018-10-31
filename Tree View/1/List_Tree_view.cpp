/*
#include "head.h"
void create(HWND hwnd,UINT msg,WPARAM wp,LPARAM lp)
{
	
	HWND tree;
	HINSTANCE hInst; // main function handler
#define WIN32_LEAN_AND_MEAN // this will assume smaller exe
TV_ITEM tvi;
HTREEITEM Selected;
TV_INSERTSTRUCT tvinsert;   // struct to config out tree control
HTREEITEM Parent;           // Tree item handle
HTREEITEM Before;           // .......
HTREEITEM Root;             // .......
HIMAGELIST hImageList;      // Image list array hadle
HBITMAP hBitMap;            // bitmap handler
bool flagSelected=false;

// for drag and drop
HWND hTree;
TVHITTESTINFO tvht; 
HTREEITEM hitTarget;
POINTS Pos;
bool Dragging;

// for lable editing
HWND hEdit;
	//=====================================================//

	tree=CreateWindowEx(0, WC_TREEVIEW,TEXT("Tree View"), WS_VISIBLE | WS_CHILD|WS_BORDER,0, 0, 200, 500, hwnd, (HMENU)123, GetModuleHandle(NULL), NULL);
	//CreateWindowEx(0, WC_TREEVIEW,TEXT("Tree View"), WS_VISIBLE | WS_CHILD,0, 0, 200, 500, hwnd, (HMENU)123, GetModuleHandle(NULL), NULL);
		tree=GetDlgItem(hwnd,123);
	
		
		
		
		
		hImageList=ImageList_Create(16,16,ILC_COLOR16,2,10);
		
		
		SendDlgItemMessage(tree,123,TVM_SETIMAGELIST,0,(LPARAM)hImageList);

}


*/