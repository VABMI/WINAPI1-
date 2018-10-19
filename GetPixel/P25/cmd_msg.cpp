


long __stdcall on_cmd(HWND hwnd,unsigned int message,unsigned int wparam,long lparam)
{	
	if(wparam==1){

		MessageBox(hwnd,"Asdas","Asdas",1);
	
	HDC hdc=GetDC(hwnd);
	
		 for(int i=0;i<=6;i++)
			{
						for(int j=0;j<=6;j++)
						{
							SetPixel(hdc,100+i,100+j,RGB(220,123,74));
							SetPixel(hdc,100+i,100+j,RGB(220,123,74));
							SetPixel(hdc,100+i,100+j,RGB(220,123,74));
							SetPixel(hdc,100+i,100+j,RGB(220,123,74));

						}
			}



	}

	char ch[10];


	HWND edi=GetDlgItem(hwnd,3);

	if(wparam==2){
		COLORREF colll;
		MessageBox(hwnd,"Asdas","Asdas",1);
	
	HDC hdc=GetDC(hwnd);
	
		
					
						colll=GetPixel(hdc,100+2,100+2);
					

				for(int i=0;i<=6;i++)
					{
						for(int j=0;j<=6;j++)
						{
							SetPixel(hdc,200+i,200+j,colll);
							SetPixel(hdc,200+i,200+j,colll);
							SetPixel(hdc,200+i,200+j,colll);
							SetPixel(hdc,200+i,200+j,colll);

						}
					}


			sprintf(ch,"%i",colll);

				SetWindowText(edi,ch);

	}

			
return 0;
}