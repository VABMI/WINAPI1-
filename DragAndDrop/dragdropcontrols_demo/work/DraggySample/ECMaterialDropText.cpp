#include "stdafx.h"
#include "resource.h"
#include "ECMaterialDropText.h"
#include "DraggySampleDlg.h"

#include "DragDropButton.h"
#include "DragDropComboBox.h"
#include "DragDropListBox.h"

// ************************************************************
//	ECMaterialDropText
//		Constructor
// ************************************************************
ECMaterialDropText::ECMaterialDropText()
{
}

// ************************************************************
//	GotDrag
//		Dragging in the control.
//
//		We just check the runtime class to see if its
//		one of us. We could then call members to check if the 
//		drop is acceptable, ...
//
//		In our case, just check the type and give some graphics
// ************************************************************
DWORD ECMaterialDropText::GotDrag(void)
{
	if(m_DropTargetWnd)
	{
		CPoint iPoint = m_DropPoint;
		//	lets check if this one wants use
		CWnd *iPossibleWnd = CWnd::WindowFromPoint(iPoint);
		if(NULL == iPossibleWnd)
			return DROPEFFECT_NONE;	//	nope

		//	check if this is one of us...
		if(iPossibleWnd->IsKindOf(RUNTIME_CLASS(CDragDropButton)))
		{
			//	to do give some more feedback, lets set some text
			if(AfxGetMainWnd())
			{
				CString iRSString;
				if(iRSString.LoadString(IDS_DROP_BUTTON))
				{
					//	we know this is a dialog. Dont do this at home!
					((CDraggySampleDlg *)AfxGetMainWnd())->m_FeedBack_String = iRSString;
					((CDraggySampleDlg *)AfxGetMainWnd())->UpdateData(FALSE);
				}
			}
			return DROPEFFECT_COPY;   
		}
		else if(iPossibleWnd->IsKindOf(RUNTIME_CLASS(CDragDropComboBox)))
		{
			//	to do give some more feedback, lets set some text
			if(AfxGetMainWnd())
			{
				CString iRSString;
				if(iRSString.LoadString(IDS_DROP_COMBO))
				{
					//	we know this is a dialog. Dont do this at home!
					((CDraggySampleDlg *)AfxGetMainWnd())->m_FeedBack_String = iRSString;
					((CDraggySampleDlg *)AfxGetMainWnd())->UpdateData(FALSE);
				}
			}
			return DROPEFFECT_LINK;		//	something else
		}
		else if(iPossibleWnd->IsKindOf(RUNTIME_CLASS(CDragDropListBox)))
		{
			//	to do give some more feedback, lets set some text
			if(AfxGetMainWnd())
			{
				CString iRSString;
				if(iRSString.LoadString(IDS_DROP_LISTBOX))
				{
					//	we know this is a dialog. Dont do this at home!
					((CDraggySampleDlg *)AfxGetMainWnd())->m_FeedBack_String = iRSString;
					((CDraggySampleDlg *)AfxGetMainWnd())->UpdateData(FALSE);
				}
			}
			return DROPEFFECT_LINK;		//	something else again
		}
	}

	return DROPEFFECT_NONE;   
}

// ************************************************************
//	GotLeave
// ************************************************************
void ECMaterialDropText::GotLeave(void)
{
}

// ************************************************************
//	GotEnter
// ************************************************************
DWORD ECMaterialDropText::GotEnter(void)
{
	return DROPEFFECT_LINK;   
}

// ************************************************************
//	GotDrop
//		Called if we have a drop text drop here.
//	
//		We could now begin sending messages to the controls
//		to check if they want the drag and drop; ....
//
//		This sample just presumes that since they registered,
//		they probably do
// ************************************************************
void ECMaterialDropText::GotDrop(void)
{
	//	value contains the material itself
	if(m_Data)
	{
		//	get the text
		char *iText = (char *)m_Data;
		if((iText) && (m_DropTargetWnd))
		{
			TRACE("Found text %s.\n", iText);

			CPoint iPoint = m_DropPoint;

			//	lets check if this one wants use
			CWnd *iPossibleWnd = CWnd::WindowFromPoint(iPoint);
			if(NULL == iPossibleWnd)
				return;	//	nope

			//	check if this is one of us...
			if(iPossibleWnd->IsKindOf(RUNTIME_CLASS(CDragDropButton)))
			{
				((CDragDropButton *)iPossibleWnd)->SetWindowText(iText);	//	Ha the cast. The terrible cast!
			}
			else if(iPossibleWnd->IsKindOf(RUNTIME_CLASS(CDragDropComboBox)))
			{
				//	could check if its already present...
				((CDragDropComboBox *)iPossibleWnd)->AddString(iText);	//	Ha the cast. The terrible cast!
			}
			else if(iPossibleWnd->IsKindOf(RUNTIME_CLASS(CDragDropListBox)))
			{
				//	could check if its already present...
				((CDragDropListBox *)iPossibleWnd)->AddString(iText);	//	Ha the cast. The terrible cast!
			}
		}
	}
}
