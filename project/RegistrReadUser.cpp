#include "globals.h"

void ssdsf(HWND hwnd,bool MamDedr,void *path)
{

//	char **new_chars=reinterpret_cast<char**>(path);


	MessageBox(hwnd,(LPCSTR)(path),(char*)path,0);




	char fv[100];
	char sg[100];
	HWND saxeli=GetDlgItem(hwnd,111);
	HWND gvari=GetDlgItem(hwnd,112);
	HWND telefoni=GetDlgItem(hwnd,113);
	HWND email=GetDlgItem(hwnd,114);
	HWND misamarti=GetDlgItem(hwnd,115);
	HWND password1=GetDlgItem(hwnd,116);
	HWND password2=GetDlgItem(hwnd,117);

	HWND com1=GetDlgItem(hwnd,118);
	HWND com2=GetDlgItem(hwnd,119);
	HWND com3=GetDlgItem(hwnd,120);
	
	 FILE * file; 

////////////////////////////////////////// profilis informaciis chawera  .////////////////////////
	for(int i=111;i<=115;i++)
	{
	  GetWindowText(GetDlgItem(hwnd,i),fv,99);
	  
	//  strcpy(sg,fv);	


	
	file=fopen("C:\\Users\\vaxoa\\OneDrive\\Desktop\\smses.txt","a");

	fwrite((char*)fv,strlen(((char*)fv)),1,file);
	fwrite((char*)"\n",2,1,file);





	//  MessageBox(hwnd,(LPCSTR)fv,fv,0);
	}
////////////////// dabadebis tarigi ///////////////////////////////////////////////
	
		 GetWindowText(com1,fv,99);							//////////////////////
		// 	fwrite((char*)fv,strlen(((char*)fv)),1,file);
			fwrite((char*)"\n",2,1,file);
		  GetWindowText(com2,fv,99);
		//  	fwrite((char*)fv,strlen(((char*)fv)),1,file);
			fwrite((char*)"\n",2,1,file);
		   GetWindowText(com3,fv,99);
		   	fwrite((char*)fv,strlen(((char*)fv)),1,file);
			fwrite((char*)"\n",2,1,file);

/////////////////////////////////// end dabadebis tarigi //////////////////////////////////////////////////

		   			fclose(file);

///////////////////////////////// DASASRULI   profilis informaciis chawera  .////////////////////////

//////////////////////////////////// parolebis shedareba ////////////////////////////////
GetWindowText(password1,sg,99);
GetWindowText(password2,fv,99);
bool h=1;
	if(strlen(fv)==strlen(sg))
	{
		for(int i=0;i<=strlen(fv);i++)
		{
				if((char)fv[i]==(char)sg[i])
				{

					

				} else{h=0;}
		}

	} else{h=0;} 

	

//////////////////////////////////// END	parolebis shedareba ////////////////////////////////
	
//////////////////////////////////// 	parolebis chawera////////////////////////////////
	if(h)
	{
		


		MessageBox(hwnd,"asdasd","asdasd",0);
	 FILE * file1; 
	file1=fopen("C:\\Users\\vaxoa\\OneDrive\\Desktop\\paroli.txt","a");

	
	fwrite((char*)fv,strlen((fv)),1,file1);
	fwrite((char*)"\n",2,1,file1);
		
			
			fwrite((char*)fv,strlen(((char*)fv)),1,file1);
			fwrite((char*)"\n",2,1,file1);


					fclose(file1);



	}

	//////////////////////////////////// END	parolebis chawera////////////////////////////////



	/////////////////////// suratis path ////////////////////////////////////

	if(path)
	{
		 FILE * file2; 
	file2=fopen("C:\\Users\\vaxoa\\OneDrive\\Desktop\\surati.txt","a");

	
	fwrite(path,strlen((char*)path),1,file2);

	

					fclose(file2);


	}

		/////////////////////// END suratis path ////////////////////////////////////

//	=(unsigned char*)calloc(100);
		//strcat(fvq1,"dfsd");
	

	












	//	 free(sg);
}