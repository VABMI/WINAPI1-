
static int fgh;	static bool gm=1;

static	char buff2[100];

	
	if(IDM_UNDO==id)
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
	

		
}
if(id==IDM_NEXT)
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
	
}