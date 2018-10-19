


long __stdcall on_create(HWND hwnd,unsigned int message, unsigned int wparam,long lparam)
{
	
	CreateWindow("Button","asda",WS_VISIBLE|WS_CHILD|WS_BORDER,0,0,50,100,hwnd,(HMENU)1,0,0);
		CreateWindow("Button","asda",WS_VISIBLE|WS_CHILD|WS_BORDER,0,150,50,100,hwnd,(HMENU)2,0,0);
CreateWindow("Edit","asda",WS_VISIBLE|WS_CHILD|WS_BORDER,0,250,350,100,hwnd,(HMENU)3,0,0);
return 0;
}