
long __stdcall on_kbd(HWND hwnd,unsigned int message, unsigned int wparam,long lparam)
{
	char fff[100];
	switch(wparam)
	{

		sprintf(fff,"%i",wparam);
		MessageBox(hwnd,fff,fff,0);



	case VK_RETURN:	break;




	case VK_TAB:	break;
	case VK_ESCAPE:exit(1);break;	
	
	}
return 0;
}