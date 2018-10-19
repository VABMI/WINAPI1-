WORD __stdcall Idiagram(void* hd)
{	HWND  zeda;
RECT r;
	HWND stat=GetDlgItem((HWND)hd,1);
	HDC hdc;

	zeda=GetDlgItem((HWND)hd,15);

	HWND zeda2=GetDlgItem((HWND)hd,16);
	HWND zeda3=GetDlgItem((HWND)hd,17);
	
	hdc=GetDC(zeda);
	char e[4];
		for(int i=0;i<=p1;i++)
		{
			SetWindowPos(zeda,NULL,0,5,i*2,20,SWP_DRAWFRAME);
			sprintf(e,"%i",i);
			SetWindowText(zeda,e);
			//Sleep(2);
		}

		InvalidateRect(zeda,0,1);
		//hdc=GetDC(zeda2);
		for(int i=0;i<=p2;i++)
		{
			SetWindowPos(zeda2,0,0,30,i*2,20,SWP_DRAWFRAME);
			sprintf(e,"%i",i);
			SetWindowText(zeda2,e);
		}
	//	hdc=GetDC(zeda3);
		for(int i=0;i<=p3;i++)
		{
			SetWindowPos(zeda3,0,0,55,i*2,20,SWP_DRAWFRAME);
			sprintf(e,"%i",i);
			SetWindowText(zeda3,e);
		}
		
		ZeroMemory(&zeda,sizeof(zeda));
		ZeroMemory(&zeda,sizeof(zeda2));
		ZeroMemory(&zeda,sizeof(zeda3));

		return 0;
}


WORD __stdcall IIdiagram(void* hd)
{	
/////////////////////////////////////////////////////////////////////////////////////

/////////	SIMAGLIS GANSAZGVRA ///////////////
/*//////*/ int YP=0;
/*//////*/ int YH=0;
///////////////////////////////////////////////


	HWND statchilde;

	int X,Y,W,H; int DACILEBA=10;
	W=10;X=5;
							//	MessageBox(hwnd,"Asdsad","asdad",1);
////////////////////////////////////////////////// H2 /////////////////////////////////////////////////////////////////////////
						statchilde=GetDlgItem((HWND)hd,2);
						SetWindowPos(statchilde,NULL,X,Y2+YP,W,H2+YH,SWP_DRAWFRAME);
						dagrmcolor(statchilde,H2);
////////////////////////////////////////////////// H3 /////////////////////////////////////////////////////////////////////////
						X=X+W+DACILEBA;
						statchilde=GetDlgItem((HWND)hd,3);
						SetWindowPos(statchilde,NULL,X,Y3+YP,W,H3+YH,SWP_DRAWFRAME);
						dagrmcolor(statchilde,H3);
////////////////////////////////////////////////// H4 /////////////////////////////////////////////////////////////////////////
						X=X+W+DACILEBA;
						statchilde=GetDlgItem((HWND)hd,4);
						SetWindowPos(statchilde,NULL,X,Y4+YP,W,H4+YH,SWP_DRAWFRAME);
						dagrmcolor(statchilde,H4);
////////////////////////////////////////////////// H5 /////////////////////////////////////////////////////////////////////////
							X=X+W+DACILEBA;
						statchilde=GetDlgItem((HWND)hd,5);
						SetWindowPos(statchilde,NULL,X,Y5+YP,W,H5+YH,SWP_DRAWFRAME);
						dagrmcolor(statchilde,H5);
////////////////////////////////////////////////// H6 /////////////////////////////////////////////////////////////////////////
							X=X+W+DACILEBA;
						statchilde=GetDlgItem((HWND)hd,6);
						SetWindowPos(statchilde,NULL,X,Y6+YP,W,H6+YH,SWP_DRAWFRAME);
						dagrmcolor(statchilde,H6);
////////////////////////////////////////////////// H7 /////////////////////////////////////////////////////////////////////////
							X=X+W+DACILEBA;
						statchilde=GetDlgItem((HWND)hd,7);
						SetWindowPos(statchilde,NULL,X,Y7+YP,W,H7+YH,SWP_DRAWFRAME);
						dagrmcolor(statchilde,H7);
////////////////////////////////////////////////// H8 /////////////////////////////////////////////////////////////////////////
							X=X+W+DACILEBA;
						statchilde=GetDlgItem((HWND)hd,8);
						SetWindowPos(statchilde,NULL,X,Y8+YP,W,H8+YH,SWP_DRAWFRAME);
						dagrmcolor(statchilde,H8);
////////////////////////////////////////////////// H9 /////////////////////////////////////////////////////////////////////////
							X=X+W+DACILEBA;
						statchilde=GetDlgItem((HWND)hd,9);
						SetWindowPos(statchilde,NULL,X,Y9+YP,W,H9+YH,SWP_DRAWFRAME);
						dagrmcolor(statchilde,H9);
////////////////////////////////////////////////// H10 /////////////////////////////////////////////////////////////////////////
							X=X+W+DACILEBA;
						statchilde=GetDlgItem((HWND)hd,10);
						SetWindowPos(statchilde,NULL,X,Y10+YP,W,H10+YH,SWP_DRAWFRAME);
						dagrmcolor(statchilde,H10);
////////////////////////////////////////////////// H11 /////////////////////////////////////////////////////////////////////////
							X=X+W+DACILEBA;
						statchilde=GetDlgItem((HWND)hd,11);
						SetWindowPos(statchilde,NULL,X,Y11+YP,W,H11+YH,SWP_DRAWFRAME);
						dagrmcolor(statchilde,H11);
////////////////////////////////////////////////// END ////////////////////////////////////////////////////////////////////////
					
return 0;
}

WORD __stdcall IIIdiagram(void* hd){
	int v=0;
static	int w=0;
	bool k=0;
	bool q=0;
	bool plus=1;
	for(int y=0;y<=100;y++)
		{
			for(int i=60-v;i<=65-v;i++)
			{
			SetPixel((HDC)hd,1+y,i,RGB(53,19,13));
			SetPixel((HDC)hd,0+y,i,RGB(535,19,13));
			


			}

	if(v==30)
	{
	k=1;
	w++;
	q=0;
		if(w==10)
		{
		q=1;
		w=0;
		}
	
	}

	
		
	


	if(v==0)
	{
		k=0;
		q=0;
		w++;
		if(w==50)
		{
		q=1;
		w=0;
		}
	}



	if(k==1&&q){
	v=v-2;
	}
	if(k==0&&q){
	v=v+2;
	}


		}
	return 0;
}

