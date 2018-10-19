
#include "head.h"

void date(HDC hdc)
{
	wchar_t m [100];

SYSTEMTIME st;
GetSystemTime(&st);

swprintf_s(m, L"%ld-%ld-%ld==%ld-%ld-%ld",st.wYear,st.wMonth,st.wDay,st.wHour,st.wMinute,st.wSecond,1);
 TextOut(hdc,60,10,m,wcslen(m));
	



}