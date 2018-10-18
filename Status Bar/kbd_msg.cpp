
long __stdcall on_kbd(HWND hwnd,unsigned int message
					, unsigned int wparam,long lparam)
{

	switch(wparam)
	{
	case VK_RETURN:	break;
	case VK_TAB:	break;
	case VK_ESCAPE:exit(1);break;	
	
	}
return 0;
}