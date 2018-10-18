#ifndef _WND_HPP_
#define _WND_HPP_


class Wnd
{

public:
	Wnd(char* pcClassName, HWND parentWnd);
	~Wnd();
	int   GetProportion();
	HWND  GetHWnd();
	RECT  GetRect();	
	CWnd* GetCWnd();
	bool  IsRectChanged();
private:
	RECT  r;
	HWND  hWnd;
	CWnd* m_pCWnd;
private:
	void GetHandle(char* pcClassName, HWND parentWnd);
	void SaveHWnd(HWND hWnd);
};







#endif