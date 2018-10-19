
static int fgh;
	static	char buff2[100];
if(IDM_UNDO==id)
{
	static	char buff[100];
	static bool gn=1;
		
		
		int i;

			
			if(gn)
			{
			SendMessage(EDITGLOBAL,WM_GETTEXT,sizeof(buff),(LPARAM)buff);
				//	buff2=buff;
				//	strcat(buff2,buff);
			sprintf(buff2,"%s",buff);

			gn=0;
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
			gn=1;

		
}
if(id==IDM_NEXT)
{

	
	char po[200];
	///sprintf(po,"%s",buff2);
/*

		//	MessageBox(hwnd,buff2,buff2,0);

		strcat(po,buff2);
	int G=strlen(buff2);
	for(int i=fgh;i<=strlen(buff2);i++)
	{

		if(po[i]==' ')
		{
			for(int u=i;u<=strlen(po);u++)
			{
				if(po[u]==' ')
				{
					po[u]=(char)0;
					



				}

			}


			fgh=i;
			break;

		}

		
	}
	


	*/
    SendMessage(EDITGLOBAL,WM_SETTEXT,strlen(po),(LPARAM)po);


}