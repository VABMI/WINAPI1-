






LRESULT CALLBACK WndProcButton (HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{	static int z;
	static int y;


	
static int fgh;	static bool gm=1;

static	char buff2[100];

	
	   WNDPROC wpOld = (WNDPROC)GetWindowLongPtr(EDITGLOBAL, GWLP_USERDATA);

	   if(wpOld)
	   {  
		   
		   int x=0;

			   switch(message)
			   {
			   case WM_KEYDOWN:


				 
				   break;				
			   }
			    

	   }

	   
			   switch(message)

			   {
				    case WM_KEYUP:
							{
					 if(DWORD(wParam)==VK_CONTROL){
						 y=0;

					 }
							
							}

						break;
				    case WM_KEYDOWN:
						{
							
						
			 if(DWORD(wParam)==VK_CONTROL)
						   {
							 y=DWORD(wParam);
							}		


						
							   if(y==17&&DWORD(wParam)==0x59)
							   {

								   static	char buff[100];

		
		int i;

			
			if(gn)
			{
			SendMessage(EDITGLOBAL,WM_GETTEXT,sizeof(buff),(LPARAM)buff);
				//	buff2=buff;
				//	strcat(buff2,buff);
			sprintf(buff2,"%s",buff);

			gn=0;
			}

		if(gm)
			{

	
			sprintf(buff,"%s",buff2);

			gm=0;
			}



			i=strlen(buff);
			int k=strlen(buff);
			while(i--)
			{

				if(buff[i]==' ')
				{

					for(int v=i;v<=k;v++)
					{

						buff[v]=(char)0;

					}
					fgh=i;
					break;
				}


			}
			SendMessage(EDITGLOBAL,WM_SETTEXT,strlen(buff),(LPARAM)buff);
			return CallWindowProc( wpOld, hwnd, message, wParam, lParam );

							  
							   }

							   
							   if(z==17&&DWORD(wParam)==0x5A)
							   {
								   
			gm=1;
	static bool ko=0;
	char *po=(char*)malloc(1*strlen(buff2));
	///sprintf(po,"%s",buff2);
	//int fg=fgh;

		//	MessageBox(hwnd,buff2,buff2,0);
	if(!ko){
	sprintf(po,"%s",buff2);
	//ko=1;
	}
//	int G=strlen(buff2);

	


	for(int ui=fgh;ui<=strlen(buff2);ui++)
	{
	if(buff2[ui]==' ') 
	{	
		for(int oi=ui;oi<=strlen(buff2);oi++)
		{
		po[oi]=NULL;	
		
		}
	fgh=ui+1;
		break;
	}

   }
	 SendMessage(EDITGLOBAL,WM_SETTEXT,strlen(po),(LPARAM)po);
	return CallWindowProc( wpOld, hwnd, message, wParam, lParam );						   
							  
							   }


						}
						break;
 
					
					
					
					//case WM_KEYDOWN:
			   
				   if(wParam== 0x43){
				

						//   if(lParam==VK_CONTROL)
							 if(wParam==VK_CONTROL)
						   {
					 
					   
					   
							   if(lParam==0x43)
							   int y=0;

						   }
				   }
				   break;

			   case WM_CHAR:



				   if(wParam==0x43)
				   {
					//	if(lParam==VK_CONTROL)
					   int y=0;

				   }


				   break;
			   case WM_SYSKEYDOWN:
				 

				   if(wParam==0x43)
				   {
					//	if(lParam==VK_CONTROL)
					   int y=0;

				   }


				   break;	
			   case WM_SYSCHAR:
				   

				   if(wParam==0x43)
				   {
					//	if(lParam==VK_CONTROL)
					   int y=0;

				   }


				   break;


			   }
			    


    return CallWindowProc( g_wndProcButtonOrigianl, hwnd, message, wParam, lParam );
}
