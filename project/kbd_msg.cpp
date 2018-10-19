



DWORD WINAPI chawera(void* p)
{
	int ffa=0;
	
	char *jj=(char*)p;

		ffa=strlen(jj);
//	MessageBox(*globhwnd,(char*)p,(char*)p,1);

	FILE * file; 
	file=fopen("C:\\Users\\vaxoa\\OneDrive\\Desktop\\smses.txt","a");

	
	fwrite((char*)p,strlen(((char*)p)),1,file);
	
	fclose(file);



		return 0;
}



DWORD WINAPI reading(void * pp)
{


	char s2[1000];
	char s1[1000];

	int mem=0;
	int mem2=1;


		
	while(1)
	{
 

		
		   FILE * file; 
	
		file=fopen("C:\\Users\\vaxoa\\OneDrive\\Desktop\\smses.txt","r");






	while(!feof(file))
	{    
	fgets(s1,1000,file);




	//	mem2=(void*) (((int)mem2)+strlen(s1));
	
		//strcat(s2,s1);
		sprintf(s2,"%s",s1);
	}

		mem2=strlen(s1);
	if(((int)mem2)!=((int)mem))
	{

	
	SendMessage(hw1,WM_SETTEXT,10,(LPARAM)"");
	SendMessage(hw1,EM_REPLACESEL,900,(LPARAM)s2);
		
		mem=strlen(s2);


		for(int i=0;i<=mem;i++)
		{

		//	s2[i]=0;

		}

		
		
	}	
ZeroMemory(&s2,sizeof(s2));


fclose(file);
		

	Sleep(1000);
	}



	return 0;
}

/*

void on_create(HWND hwnd1,unsigned int mess, unsigned int wpar,long lpar)
{
	HWND hw1,hw2,hw3;
//create_menu(hwnd);
	///	HFONT hfont=create_font(hwnd);


int X,Y,W,H;
DWORD style=WS_VISIBLE|WS_CHILD|WS_BORDER;


X=0;Y=30;W=300;H=500;
hw1=CreateWindow("edit","edit",style|ES_MULTILINE|WS_VSCROLL,X,Y,W,H,hwnd1,(HMENU)901,0,0);
X=00;Y=535;W=250;H=50;
hw2=CreateWindow("edit","edit",style|WS_HSCROLL|ES_MULTILINE,X,Y,W,H,hwnd1,(HMENU)902,0,0);
X=250;Y=535;W=50;H=50;
hw3=CreateWindow("Button","butt",style,X,Y,W,H,hwnd1,(HMENU)903,0,0);
//	SendMessage(hw,WM_SETFONT,(UINT)hfont,0);



}

*/