 
#include"globals.h"



char *open1(HWND hwnd)
{
	char path[100];

	OPENFILENAME ofn={0};       // common dialog box structure
	char szFile[260];       // buffer for file name
         // owner window
	HANDLE hf;              // file handle
	
	// Initialize OPENFILENAME
	//ZeroMemory(&amp.ofn, sizeof(ofn));
	ofn.lStructSize = sizeof(ofn);
	ofn.hwndOwner = hwnd;
	ofn.lpstrFile = szFile;
	
	ofn.lpstrFile[0] = '\0';
	ofn.nMaxFile = sizeof(szFile);
	ofn.lpstrFilter = "All\0*.*\0Text\0*.TXT\0";
	ofn.nFilterIndex = 1;
	ofn.lpstrFileTitle = NULL;
	ofn.nMaxFileTitle = 259;
	ofn.lpstrInitialDir = NULL;
//	ofn.Flags = OFN_PATHMUSTEXIST | OFN_FILEMUSTEXIST;
	
	// Display the Open dialog box. 
	
	if (GetOpenFileName(&ofn)==TRUE) 
	{
				char g[200];	
				int co=0;
				sprintf(szFile,"%s",ofn.lpstrFile);

			 	 for(int i=0;i<=strlen(szFile);i++)
				 {

				//	if(((int)(szFile[i]))==47)
					if(((int)szFile[i])==92)
					{
					path[co]=szFile[i];
					
					co++;
					path[co]=szFile[i];

					}
					else
					{
						path[co]=szFile[i];

					}



			  
				co++;


				 }
			
				// sprintf(hh,"%s",path);
	return (char*)szFile;
		}	


 }
