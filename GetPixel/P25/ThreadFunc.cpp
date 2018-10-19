#include "header.h"

DWORD __stdcall IIdiagram(void* hd)
{	HWND  zeda;




	zeda=GetDlgItem((HWND)hd,15);
	HWND zeda2=GetDlgItem((HWND)hd,16);
	HWND zeda3=GetDlgItem((HWND)hd,17);
	
		for(int i=0;i<=100;i++)
		{
			//SetWindowPos(zeda,NULL,0,5,i,20,SWP_DRAWFRAME);
		}

		for(int i=0;i<=100;i++)
		{
//			SetWindowPos(zeda2,0,0,30,i,20,SWP_DRAWFRAME);
		}

		for(int i=0;i<=100;i++)
		{
	//		SetWindowPos(zeda3,0,0,55,i,20,SWP_DRAWFRAME);
		}
		
		ZeroMemory(&zeda,sizeof(zeda));
		ZeroMemory(&zeda,sizeof(zeda2));
		ZeroMemory(&zeda,sizeof(zeda3));

		return 0;
}















DWORD __stdcall I_IIdiagram(void* hd)
{
	HDC hdc=(HDC)hd;
	for(int y=0;y<=100;y++)
		{
			for(int i=1;i<=18;i++)
			{
			SetPixel(hdc,1+y,i,RGB(250,0,0));
			SetPixel(hdc,0+y,i,RGB(250,0,0));
			}
	
		}



						
		for(int y=0;y<=100;y++)
		{
			for(int i=20;i<=38;i++)
			{
			SetPixel(hdc,1+y,i,RGB(25,70,83));
			SetPixel(hdc,0+y,i,RGB(25,70,83));
			}

		}


		

						
		for(int y=0;y<=100;y++)
		{
			for(int i=40;i<=58;i++)
			{
			SetPixel(hdc,1+y,i,RGB(115,7,133));
			SetPixel(hdc,0+y,i,RGB(115,7,133));
			}

		}

								
		for(int y=0;y<=100;y++)
		{
			for(int i=60;i<=78;i++)
			{
			SetPixel(hdc,1+y,i,RGB(53,19,13));
			SetPixel(hdc,0+y,i,RGB(535,19,13));
			}
	
		}
	return 0;
}