                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                #include <windows.h>
#include "head.h"
#include "globals.h"
	//	#include <ole.h>
#pragma comment(lib,"comctl32.lib")
#include "defines.h"
#include "ReadAndWrite.cpp"
#include "functions.cpp"

#include "cmd_msg.cpp"

#include "paint_msg.cpp"

#include "EditisMesigebi.cpp"
#include "on_create_msg.cpp"


#pragma comment(lib,"comctl32.lib")

//----------------------------------------------------------------
HCURSOR hCursor;


	 bool delet12=1;
	 char *sechwat="fff";
	  char *sechwat2="fff";
	 static char*sechwatPev=sechwat;

	 static char* charReplAl=sechwat2;
//----------------------------------------------------------------





//----------------------------------------------------------------
const UINT findmsg=RegisterWindowMessage(FINDMSGSTRING);  //// finde funqciis messagi
const UINT findmsgReplace=RegisterWindowMessage(FINDMSGSTRING); //// finde replace funqciis messagi
long __stdcall vaxo(HWND hwnd,unsigned int message, unsigned int wparam,long lparam)
{
	HWND editaa=GetDlgItem(hwnd,editc);
	 StatusBar= GetDlgItem(hwnd,idStatusBar);
	RECT r;
//	HWND tolbar=GetDlgItem(hwnd,70000);


	
///////////////////////////////////////////// Marto finde funqciaaa ////////////////////////////////////////////////////////////
#include "FindeText.cpp"
/////////////////////////////////////////////END Marto finde funqciaaa //////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////// REPLACE_finde funqciaaa //////////////////////////////////////////////////////////////////////////////

#include "FindeReplace.cpp"

/////////////////////////////////////////////END REPLACE_finde funqciaaa //////////////////////////////////////////////////////////////////////////////

	switch(message)
	{ 
	
		case WM_LBUTTONDOWN:

		
		break;

		case WM_CREATE:
		
		create_menu(hwnd);
		tolbar=Toolar(hwnd);
		DoCreateStatusBar(hwnd,6,0,5);
	  	on_create(hwnd,message,wparam,lparam,10);
		// RegisterDragDrop(editaa,);

		DragAcceptFiles(hwnd,1); ////// calll drag and drop function

		break;
		
		case WM_LBUTTONUP:

			break;

//////////////////////////Drag and drop Receve /////////////////////////
		case WM_DROPFILES:
		{
			static char ksk[100];
					
			HDROP	hDrop =(HDROP) wparam;
				DragQueryFile(hDrop,0,ksk,99);

    	//	MessageBox(hwnd,ksk,ksk,0);
				int co=0;
		 for(int i=0;i<=strlen(ksk);i++)
				 {

				//	if(((int)(szFile[i]))==47)
					if(((int)ksk[i])==92)
					{
					path[co]=ksk[i];
					
					co++;
					path[co]=ksk[i];

					}
					else
					{
						path[co]=ksk[i];

					}



			  
				co++;


				 }
	
			void *buferEdit1=read(hwnd);
			SendMessage(editaa,WM_SETTEXT,99,(LPARAM)buferEdit1);
		}
		break;
//////////////////////////	END Drag and drop Receve /////////////////////////

		case WM_COMMAND:
			on_cmd(hwnd,message,wparam,lparam);

		break;
		case WM_PAINT:
		//on_paint(hwnd,message,wparam,lparam);
		break;

		case WM_KEYDOWN:
		switch(wparam)
		{

		case VK_RETURN:

			MessageBox(hwnd,"adasdas","asdas",0);

			break;


		}
	//	on_kbd(hwnd,message,wparam,lparam);
		
		break;

		case WM_CTLCOLOREDIT:
		{
		 HDC hdcStatic = (HDC) wparam;
		 SetTextColor(hdcStatic, edittextferi);
		 SetBkColor(hdcStatic,textlineferi);
	     return (INT_PTR)CreateSolidBrush(bckferi);
		
		}
		break;
		case WM_SIZE:
				GetClientRect(hwnd,&r);
	     		MoveWindow(editaa,2,31,r.right-4,r.bottom-55,1);
				//// toll bari Status bari ///////////////
				SendMessage(tolbar,TB_AUTOSIZE, 0, 0);/////
		//	 SendMessage(StatusBar, TB_AUTOSIZE, 0, 0); //////
				SendMessage(tolbar,TB_AUTOSIZE, 0, 0);/////
			//  SendMessage(StatusBar, WM_SIZE, 0, 0); 
			StatusBar=DoCreateStatusBar(hwnd,6,0,5);

				static  char statusc[5];
			  sprintf(statusc,"BOTTOM /=/%i",r.bottom);
			  SendMessage(StatusBar, SB_SETTEXT, 3,(LPARAM)statusc);
			  ZeroMemory(&statusc,strlen(statusc));
			  
			  sprintf(statusc,"RIGHT /=/%i",r.right);
			  SendMessage(StatusBar, SB_SETTEXT, 4,(LPARAM)statusc);

			   SendMessage(StatusBar, SB_SETTEXT, 1,(LPARAM)path);

			//  SendMessage(StatusBar, SB_SETBKCOLOR , (WPARAM) 1, (LPARAM)RGB(0,128,0));
			  
			//  SendMessage(StatusBar, SB_SETPARTS , (WPARAM) 5, (LPARAM)r.right-30);
 //////////////////////////////////////////////////////////////////////////////////////////////

			// DoCreateStatusBar(hwnd,6,0,5);
		break;

		case WM_DESTROY:
		exit(1);
		break;
	}
	 SendMessage(StatusBar,SB_SETTEXT,0,(LPARAM)"Ready");
	
return DefWindowProc(hwnd,message,wparam,lparam);
}
//----------------------------------------------------------------

int __stdcall WinMain(HINSTANCE,HINSTANCE,char *,int)
{
HWND hwnd=0;
int X,Y,W,H;
ULONG style=0;

WNDCLASS wc;
ZeroMemory(&wc,sizeof(WNDCLASS));

wc.style=CS_DBLCLKS;
wc.lpfnWndProc=(WNDPROC)&vaxo;
wc.lpszClassName="12";
wc.hbrBackground=(HBRUSH)CreateSolidBrush(RGB(25,255,55));
wc.hIcon=(HICON)LoadImage(0,"c:\\1.ico",IMAGE_ICON,16,16,LR_LOADFROMFILE);
wc.hCursor=LoadCursor(NULL,IDC_WAIT);
	if(RegisterClass(&wc)==0)
	{
	MessageBox(hwnd,"RegisterClass error",0,0);
	return 0;
	}


//mtavari fanjara:
style=WS_VISIBLE|WS_OVERLAPPEDWINDOW|WS_CLIPCHILDREN|WS_EX_TRANSPARENT;
X=10;Y=30;W=750;H=500;
hwnd=CreateWindow(wc.lpszClassName,"Main",style,X,Y,W,H,0,0,0,0);


/////--------------------------///////
	
MSG msg;
	while(GetMessage(&msg,0,0,0))
	{/*
		if(msg.message==WM_COMMAND)
		{

			if(msg.wParam==30001)
			{

				MessageBox(msg.hwnd,"asdasd","Asdasd",0);
			}

		}
		*/
	TranslateMessage(&msg);
	DispatchMessage(&msg);
	}
}




/*
long __stdcall buttonNewPro(HWND hwnd,unsigned int message, unsigned int wparam,long lparam)
{
		SendMessage(hwnd,message,wparam,lparam);
return DefWindowProc(hwnd,message,wparam,lparam);
}
*/
//-------------                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         