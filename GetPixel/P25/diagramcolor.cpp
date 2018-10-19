#include"header.h"
HBITMAP dagrmcolor(HWND hwnDagrm,int P)
{

	HBITMAP ret;

	HBITMAP witeli=(HBITMAP)LoadImage(NULL,"C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\witeli.BMP",IMAGE_BITMAP,10,200,LR_LOADFROMFILE);//>50<75
	HBITMAP yviteli=(HBITMAP)LoadImage(NULL,"C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\yviteli.BMP",IMAGE_BITMAP,10,200,LR_LOADFROMFILE);//>25<=50
	HBITMAP mwvane=(HBITMAP)LoadImage(NULL,"C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\mwvane.bmp",IMAGE_BITMAP,10,200,LR_LOADFROMFILE);//<25
	HBITMAP muqiwiteli=(HBITMAP)LoadImage(NULL,"C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\New Bitmap Image.bmp",IMAGE_BITMAP,10,200,LR_LOADFROMFILE); ///>75 <=100
		//////////////////////// H2 ////////////////////////////////////////////////////////////////////////////
						
						if(P<=25)
						{
						//	ret=mwvane;
							SendMessage(hwnDagrm,STM_SETIMAGE,(WPARAM)IMAGE_BITMAP,(LPARAM) mwvane);
						}
						if(P>25&&P<=50)
						{
							ret=yviteli;
							SendMessage(hwnDagrm,STM_SETIMAGE,(WPARAM)IMAGE_BITMAP,(LPARAM) yviteli);
						}
						if(P<=75&&P>50)
						{
							ret=witeli;
							SendMessage(hwnDagrm,STM_SETIMAGE,(WPARAM)IMAGE_BITMAP,(LPARAM) witeli);
						}
						if(P>75&&P<=100)
						{
							ret=muqiwiteli;
							SendMessage(hwnDagrm,STM_SETIMAGE,(WPARAM)IMAGE_BITMAP,(LPARAM) muqiwiteli);

						}

			/////////////////////////////////////////// END	H2 ////////////////////////////////////


return 0;
}
