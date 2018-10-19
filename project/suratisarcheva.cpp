
	static char path[100];

		if(wparam== 123)
		{
		
			char *pp=open1(hwnd);

		if(pp){
			 	 for(int i=0;i<=strlen(pp);i++)
				 {

				//	if(((int)(szFile[i]))==47)
					if(((int)pp[i])==92)
					{
					path[co]=pp[i];
					
					co++;
					path[co]=pp[i];

					}
					else
					{
						path[co]=pp[i];

					}



			  
				co++;


				 }
				
				  co=0;
				  /*
				  HBITMAP mybbit= (HBITMAP)LoadImage(NULL,path, IMAGE_BITMAP,305,300, LR_LOADFROMFILE);



				SendMessage(hwImage, (UINT)STM_SETIMAGE,  (WPARAM)IMAGE_BITMAP, (LPARAM)mybbit);


					if(coArr==0)
					{	
						//	BufImagePath[0]=((char*))path;
							strcat(BufImagePath,path);
							mybbit= (HBITMAP)LoadImage(NULL,(char*)path, IMAGE_BITMAP,100,100, LR_LOADFROMFILE);

							SendMessage(hwImage1, (UINT)STM_SETIMAGE,  (WPARAM)IMAGE_BITMAP, (LPARAM)mybbit);
							
					}
					
					if(coArr==1)
					{	//BufImagePath[1]=((char*))path;
						strcat(BufImagePath,"+");
						strcat(BufImagePath,path);
							mybbit= (HBITMAP)LoadImage(NULL,(char*)path, IMAGE_BITMAP,100,100, LR_LOADFROMFILE);

							SendMessage(hwImage2, (UINT)STM_SETIMAGE,  (WPARAM)IMAGE_BITMAP, (LPARAM)mybbit);
								
					}
					if(coArr==2)
					{	//BufImagePath[2]=((char*))path;
						strcat(BufImagePath,"+");
						strcat(BufImagePath,path);
							mybbit= (HBITMAP)LoadImage(NULL,(char*)path, IMAGE_BITMAP,100,100, LR_LOADFROMFILE);

							SendMessage(hwImage3, (UINT)STM_SETIMAGE,  (WPARAM)IMAGE_BITMAP, (LPARAM)mybbit);
								
					}
					

			coArr++;
			if (coArr==3){coArr=0;}

			//	MessageBox(hwnd,(LPCSTR)BufImagePath[0],(LPCSTR)path,0);

						//	SendMessage(hwImage,WM_PAINT,0,1);


				//	 MessageBox(hwnd,(LPCSTR)path,(LPCSTR)path,0);



*/

			}
			

		}