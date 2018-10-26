#include <windows.h>

#include <stdio.h>
HWND out,plus,minus,gam,gay,pro;   /// editis +-*/ hendelebi



void keys(HWND hwnd){ //// am funqciashi agvwer yvela  fanjrebs button ebs da edit-s da am funqcias vidzaxeb WM_CREATE shi amashi weria yvela gilakebi da EDIT -i

int u=10; //<--- amans ar miaqcio yuradgeba gilakebis adgilmdebareobis ganmsazgvrelia ra shecvale sxva ricxvit da mixvdebi rac aris

     	CreateWindowW(L"static",L"",WS_VISIBLE|WS_CHILD|WS_BORDER,220,5,10,50,hwnd,(HMENU)1,0,0);
		out=CreateWindowW(L"EDIT",L"",WS_VISIBLE|WS_CHILD|WS_BORDER|ES_READONLY,10,5,210,40,hwnd,(HMENU)1,0,0);
		CreateWindowW(L"static",L"",WS_VISIBLE|WS_CHILD|WS_BORDER,1,5,10,265,hwnd,(HMENU)1,0,0);
	
	CreateWindowW(L"static",L"",WS_VISIBLE|WS_CHILD|WS_BORDER,10,u+40,200,8,hwnd,(HMENU)1,0,0);
CreateWindowW(L"button",L"1",WS_VISIBLE|WS_CHILD|WS_BORDER,20,u+50,40,40,hwnd,(HMENU)1,0,0);
CreateWindowW(L"button",L"2",WS_VISIBLE|WS_CHILD|WS_BORDER,70,u+50,40,40,hwnd,(HMENU)2,0,0);
CreateWindowW(L"button",L"3",WS_VISIBLE|WS_CHILD|WS_BORDER,120,u+50,40,40,hwnd,(HMENU)3,0,0);
CreateWindowW(L"button",L"4",WS_VISIBLE|WS_CHILD|WS_BORDER,20,u+100,40,40,hwnd,(HMENU)4,0,0);
CreateWindowW(L"button",L"5",WS_VISIBLE|WS_CHILD|WS_BORDER,70,u+100,40,40,hwnd,(HMENU)5,0,0);
CreateWindowW(L"button",L"6",WS_VISIBLE|WS_CHILD|WS_BORDER,120,u+100,40,40,hwnd,(HMENU)6,0,0);
CreateWindowW(L"button",L"7",WS_VISIBLE|WS_CHILD|WS_BORDER,20,u+150,40,40,hwnd,(HMENU)7,0,0);
CreateWindowW(L"button",L"8",WS_VISIBLE|WS_CHILD|WS_BORDER,70,u+150,40,40,hwnd,(HMENU)8,0,0);
CreateWindowW(L"button",L"9",WS_VISIBLE|WS_CHILD|WS_BORDER,120,u+150,40,40,hwnd,(HMENU)9,0,0);
CreateWindowW(L"button",L"C",WS_VISIBLE|WS_CHILD|WS_BORDER,20,u+200,40,40,hwnd,(HMENU)11,0,0);
CreateWindowW(L"button",L"0",WS_VISIBLE|WS_CHILD|WS_BORDER,70,u+200,40,40,hwnd,(HMENU)0,0,0);
CreateWindowW(L"button",L"=",WS_VISIBLE|WS_CHILD|WS_BORDER,120,u+200,40,40,hwnd,(HMENU)12,0,0);
	CreateWindowW(L"static",L"",WS_VISIBLE|WS_CHILD|WS_BORDER,10,u+252,200,8,hwnd,(HMENU)1,0,0);

CreateWindowW(L"static",L"",WS_VISIBLE|WS_CHILD|WS_BORDER,170,u+40,60,220,hwnd,(HMENU)1,0,0);
minus=CreateWindowW(L"button",L"-",WS_VISIBLE|WS_CHILD|WS_BORDER,180,u+50,40,40,hwnd,(HMENU)1111,0,0);
plus=CreateWindowW(L"button",L"+",WS_VISIBLE|WS_CHILD|WS_BORDER,180,u+100,40,40,hwnd,(HMENU)1112,0,0);
gam=CreateWindowW(L"button",L"*",WS_VISIBLE|WS_CHILD|WS_BORDER,180,u+150,40,40,hwnd,(HMENU)1113,0,0);
gay=CreateWindowW(L"button",L"/",WS_VISIBLE|WS_CHILD|WS_BORDER,180,u+200,40,40,hwnd,(HMENU)1114,0,0);



CreateWindowW(L"static",L"",WS_VISIBLE|WS_CHILD,1,u+265,230,50,hwnd,(HMENU)1,0,0);
pro=CreateWindowW(L"button",L"%",WS_VISIBLE|WS_CHILD|WS_BORDER,5,u+270,40,40,hwnd,(HMENU)1115,0,0);

CreateWindowW(L"button",L"X",WS_VISIBLE|WS_CHILD|WS_BORDER,195,u+270,30,40,hwnd,(HMENU)999,0,0);




}




LRESULT CALLBACK WndProc(HWND hwnd, UINT Message, WPARAM wp, LPARAM lp) {
char arr[1];  /*es cvalebadi masiviaaa yovel dacheraze tito ricxi modis jer vwer arr -shi da shemdeg strcat is meshveobit vwer arr2 
rom ar damekargos mnishvnelobebi yovel dacheraze 
*/
 static char arr2[1000]; //// arr2 aris buferi am masivshi vagroveb cifrebs da +-/* nishnebs yovel dacheraze tito dacheraze tito simbolo modis ra
	  int sl=strlen(arr2); 
	switch(Message) {
		case WM_CREATE:
			
				keys(hwnd); /// gilakebis funqcis gamodzaxeba es shegidzlia qvemotac  gamoidzaxo mtavar fanjris sheqmnis shemdeg da aq washalo mainc imushavebs 
			break;
			
			
			
			case WM_COMMAND:
				/* wp= wparam    wp shi WM_COMMAND brdzanebis shemtxvevashi modis fanjris identifikatori  
				am identifikatorebi vigeb ricxvebs yovel dacheraze da vagroveb arr2 shi  MAGRAM /*+- nishnebs wigeb GetWindowText is meshveobit */
					if(wp>=1111&&wp<=1115){
						if(wp==1111){
							
							
							/* aq magalitad 1111 aris - gilakis identifikatori 	GetWindowText(minus,arr,99) aq "minus" aris - gilakis hendeli ,arr aris droebiti
							nu masivi shemdeg arr masividan vamateb arr2 masivshi,, arr2 shi vagroveb yvela shetanil mnishvnelobebs strcat(arr2,arr)
							*/
	            			if(arr2[sl-1]=='-')	{return ;} /* if daachire ertxel - gilaks meored dacheris shemtxvevashi retuenis daxmarebit 
							gadava funqciis boloshi da ar miscems arr2 shi chaweris sashualebas
							*/
							
							
							else if(arr2[sl-1]=='+'||arr2[sl-1]=='*'||arr2[sl-1]=='%'||arr2[sl-1]=='/'){arr2[sl-1]=NULL;} /* xolo aq tu davacher jer +/* da shmded -
							
							xo - is chaanacvlebs anu im +/* ra washale YVELGAN da mixvdebi rasac aketebs */ 
							
							
							
							
						GetWindowText(minus,arr,99); // rogorc zemot vtqvi amis meshveobit vigeb chaweril simbolos gilakshi minus aris hendeli - gilakis tavshi miweria hendelebi 
					
						
					}
						///// analogiurad aris yvelgan 
						
						else if(wp==1112){
							
								if(arr2[sl-1]=='+')	{return;} else if(arr2[sl-1]=='-'||arr2[sl-1]=='%'||arr2[sl-1]=='*'||arr2[sl-1]=='/'){arr2[sl-1]=NULL;}
						GetWindowText(plus,arr,99);
					
						
					}
						
							else if(wp==1113)
							{
						if(arr2[sl-1]=='*')	{return;} else if(arr2[sl-1]=='%'||arr2[sl-1]=='-'||arr2[sl-1]=='+'||arr2[sl-1]=='/'||arr2[sl-1]=='0'){arr2[sl-1]=NULL;}
					   	GetWindowText(gam,arr,99);	
						
						}
								else if(wp==1114)
								{
							if(arr2[sl-1]=='/')	{return ;} else if(arr2[sl-1]=='%'||arr2[sl-1]=='-'||arr2[sl-1]=='+'||arr2[sl-1]=='*'){arr2[sl-1]=NULL;		 }
					
				
				GetWindowText(gay,arr,99);
					} 
					else if(wp==1115)
								{
							if(arr2[sl]=='%')	{return ;} else if(arr2[sl-1]=='/'||arr2[sl-1]=='-'||arr2[sl-1]=='+'||arr2[sl-1]=='*'){arr2[sl-1]=NULL;}
						GetWindowText(pro,arr,99);
				
					} 

						
						///////////////////
					    	strcat(arr2,arr);
					    
						SetWindowText(out,arr2);	 ////gamotana editshi 
					}
					
					
					
					
					
					
					///// ricxvebis amogeba 0 dan 9 mde
				if(wp>=0&&wp<=9)
				{ 
			     	itoa(wp,arr,50);
			     	strcat(arr2,arr);
					 
					 SetWindowText(out,arr2);	
					 }	
					 

					 
					 	
				 
					 
  
 		 
					 
					 
				
					 
					 
					 
					 
			
			
				if(wp==999){ /// gatishvis gilaki
					exit(1);
				}
				
				
				
				if(wp==11){
					//// gasuftavebis gilakis
					
					int i=0;
					for(i;i<=strlen(arr2);i++){
						arr2[i]=NULL;
					}
				
					SetWindowText(out,"");
				}
				
				
				
				
//// 12 aris = is identifikatori  ricxvebis +-/* sheyvanis da = ze dacheris shemdeg sruldeba aritmetikuli operaciebi da paxodu gamotana	
if(wp==12){
strcat(arr2," ");


 char iout[100];	/// axali masivi 					 
   strcat(iout,arr2); /* arr2 vwer iout shi shenaxvisstvis radgan rodesac davyof arr2 nawileba mnishvnelobebi
    akldeba da qvemot kide cikli mchirdeba magalitad  ai if(iout[u]=='+'){sprintf(arr2,"%f",(s+m));} es....
	 me  ro aq iout is nacvlad arr2 mwereboda ar imushavebda radgan dayofis dros arr2 shi mnishvelobebi ikargeba 
	 
	 
	 
   dayofa xdeba ase::
    char *ffs=strtok(arr2,sep);
char *ff=strtok(NULL,sep);
   */
   
   
   
  static int ut=0;
     char *ws='+';
int c=strlen(arr2);
char sep[32]="+-/*%";
char *ffs=strtok(arr2,"+-/*%");/// pirvel ricxvis minicheba
char *ff=strtok(NULL,"+-/*%");/// meore ricxvi 
 double s,m;
 
 
s=atoi(ffs); //gadayvana charidan dablshi 
m=atoi(ff); 

int u=0;
for(;u<=c;u++){
//// if iout shi shexvda + shesrulebs + tu shexvda - gamoklebas da ase % zec analogiurad aris boloa
		if(iout[u]=='+'){sprintf(arr2,"%f",(s+m));}
		if(iout[u]=='-'){sprintf(arr2,"%f",(s-m));}
		if(iout[u]=='/'){sprintf(arr2,"%f",(s/m));}
		if(iout[u]=='*'){sprintf(arr2,"%f",(s*m));}
		if(iout[u]=='%'){sprintf(arr2,"%f",(s*m)/100);}
		
}

		     
SetWindowText(out,arr2);
						
}
					
	
	
	
				
	break;
			
			
		case WM_DESTROY: {
			PostQuitMessage(0);
			break;
		}
		
		
		default:
			return DefWindowProc(hwnd, Message, wp, lp);
	}
	return 0;
}



int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow) {
	WNDCLASSEX wc;
	HWND hwnd;
	MSG msg; 

	memset(&wc,0,sizeof(wc));
	wc.cbSize		 = sizeof(WNDCLASSEX);
	wc.lpfnWndProc	 = WndProc;
	wc.hInstance	 = hInstance;
	wc.hCursor		 = LoadCursor(NULL, IDC_ARROW);
	wc.hbrBackground = (HBRUSH)(COLOR_WINDOW+4);
	wc.lpszClassName = "WindowClass";
	wc.hIcon		 = LoadIcon(NULL, IDI_APPLICATION); /* Load a standard icon */
	wc.hIconSm		 = LoadIcon(NULL, IDI_APPLICATION); /* use the name "A" to use the project icon */

	if(!RegisterClassEx(&wc)) {
		MessageBox(NULL, "Window Registration Failed!","Error!",MB_ICONEXCLAMATION|MB_OK);
		return 0;
	}

	hwnd = CreateWindowEx(WS_EX_CLIENTEDGE,"WindowClass","Caption",WS_VISIBLE|WS_CLIPCHILDREN|WS_OVERLAPPED,CW_USEDEFAULT,CW_USEDEFAULT,238,358,NULL,NULL,hInstance,NULL);


	if(hwnd == NULL) {
		MessageBox(NULL, "Window Creation Failed!","Error!",MB_ICONEXCLAMATION|MB_OK);
		return 0;
	}


	while(GetMessage(&msg, NULL, 0, 0) > 0) {
		TranslateMessage(&msg); 
		DispatchMessage(&msg);
	}
	return msg.wParam;
}



