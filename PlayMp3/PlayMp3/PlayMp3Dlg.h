
// PlayMp3Dlg.h : header file
//

#pragma once

//#include "..\\LibMP3\\mp3.h"
//#pragma comment(lib, "..\\Debug\\LibMP3.lib")

#include "LibMP3DLL.h"
#include "afxwin.h"
#include "afxcmn.h"

// CPlayMp3Dlg dialog
class CPlayMp3Dlg : public CDialogEx
{
// Construction
public:
	CPlayMp3Dlg(CWnd* pParent = NULL);	// standard constructor
	//Mp3 m_player;
private:
	CLibMP3DLL m_playerDll;
	bool playing;
	bool pause;
	static bool FileExists(const TCHAR *fileName);
	int GetVolume();

public:

// Dialog Data
	enum { IDD = IDD_PLAYMP3_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnDestroy();
	afx_msg void OnTimer(UINT_PTR nIDEvent);
	afx_msg void OnBnClickedBtnPlay();
	afx_msg void OnBnClickedBtnPause();
	afx_msg void OnBnClickedBtnStop();
	CEdit m_edtMp3File;
	afx_msg void OnBnClickedBtnBrowse();
	CButton m_btnPlay;
	CButton m_btnPause;
	CButton m_btnStop;
	CSliderCtrl m_sliderVolume;
	afx_msg void OnHScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar);
	CStatic m_staticDuration;
	CSliderCtrl m_sliderTimeElapsed;
};
