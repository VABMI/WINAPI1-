#if !defined(AFX_BUT_H__6B04ABE1_44A1_11D6_B990_86147FA52342__INCLUDED_)
#define AFX_BUT_H__6B04ABE1_44A1_11D6_B990_86147FA52342__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// ButtonStyle .h : header file
//

/////////////////////////////////////////////////////////////////////////////
// CButtonStyle window

class CButtonStyle : public CButton
{
private:
	LRESULT OnMouseLeave(WPARAM wParam, LPARAM lParam);
// Construction
public:
	CButtonStyle();
	CDC mem;
	CBitmap bmp;
// Attributes
public:

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CButtonStyle)
	public:
	virtual void DrawItem(LPDRAWITEMSTRUCT lpDrawItemStruct);
	protected:
	//}}AFX_VIRTUAL

// Implementation
public:
	void FontStyle(CString = "MS Sans Serif");
	CFont m_Font;
	BOOL m_Selected;
	HICON m_hIcon1, m_hIcon2;
	void SetIcons(UINT, UINT);
	HDC m_hHdc;
	void SetTransparent();
	void PaintBG(CDC*);
	int nState;
	COLORREF m_TextColorDark, m_TextColorLight;
	COLORREF SetTextColor(COLORREF, COLORREF);
	BOOL bMouse;
	virtual ~CButtonStyle();

	// Generated message map functions
protected:
	//{{AFX_MSG(CButtonStyle)
	afx_msg void OnMouseMove(UINT nFlags, CPoint point);
	afx_msg void OnLButtonDown(UINT nFlags, CPoint point);
	afx_msg void OnLButtonUp(UINT nFlags, CPoint point);
	afx_msg void OnLButtonDblClk(UINT nFlags, CPoint point);
//	afx_msg void OnPaint();
	//}}AFX_MSG
	CBitmap m_Bit1, m_Bit2, m_Bit3;
	
	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_BUT_H__6B04ABE1_44A1_11D6_B990_86147FA52342__INCLUDED_)
