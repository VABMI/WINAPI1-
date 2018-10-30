

char *read(HWND hwnd)
{	HWND editaa=GetDlgItem(hwnd,70000);
	char buf[100];
	char msg[] = "this is a test";
	char *ptr, c = ((char)-52);
	FILE *file;
	file=fopen(path,"rb");

	
	fseek(file,0,SEEK_END);
	size=ftell(file);
	fseek(file,0,SEEK_SET);
//	int d=14;
	/*
	if(size>14){d=14;}
	else {size=1; d=0;}
	*/ 
	 char *cv;
	if(size<16)
	{
		cv=( char*)malloc(size);
	}
	else 
	{
		cv=( char*)malloc(size);

	}
	 //free(&z);
	
	//ZeroMemory(&str,sizeof(str));
//	ZeroMemory(&editaa,sizeof(editaa));
		if(cv==NULL)
	{

		MessageBox(hwnd,"could not alloc","asda",0);

	}
	else
	{
		//	while(fgetc(c,size, 1, file));
		fread(cv,size,1,file);
		cv[size]=0;
		fclose(file);
	}

/*
	int poss=20;
	
     ptr = strchr(buf, 'Ì');
   if (ptr){
    
	   poss=(int)(ptr-buf);

   }

   
	
//	char *f=(char*)malloc(poss+6 * sizeof(buf));
   
   char f[51];
	

	
	
	for(int i=0;i<=poss;i++)
	{
		if(buf[i]=='Ì')
		{
		break;
		}
		else f[i]=buf[i];
	}
	
	if(buf[49]=='Ì'){
	MessageBox(hwnd,p,p,0);
	}
	
	
	for(int i=0;i<=strlen(buf);i++)
	{
		if(buf[i]=='Ì')
		{
		break;
		}
		else f[i]=buf[i];
	}
	*/
//	MessageBox(hwnd,f,f,0);
	//	strcat((char*)(str),f);

	//	free(z);
	//	char *g=cv;
	//	free(cv); FFF


		
	return (char*)cv;

	
}

void write(HWND hwnd,char msg[1000])
{
	

	FILE *file;
	file=fopen(path,"wb");


	fwrite(msg, strlen(msg)+1, 1, file);

	fclose(file);


}