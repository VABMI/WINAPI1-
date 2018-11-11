
#include <windows.h>
#include <strsafe.h>
#include <stdio.h>

#define BUF_SIZE 255
DWORD WINAPI wnd(void *data);

//-------------------------------------------------------------------
// A function to Display the message indicating in which tread we are
//-------------------------------------------------------------------
void DisplayMessage (HANDLE hScreen, char *ThreadName, int Data, int Count)
{

    TCHAR msgBuf[BUF_SIZE];
    size_t cchStringSize;
    DWORD dwChars;

	// Print message using thread-safe functions.
	StringCchPrintf(msgBuf, BUF_SIZE, TEXT("Executing iteration %02d of %s having data = %02d \n"), Count, ThreadName, Data); 
	StringCchLength(msgBuf, BUF_SIZE, &cchStringSize);
	WriteConsole(hScreen, msgBuf, cchStringSize, &dwChars, NULL);
	Sleep(1000);
}

//-------------------------------------------
// A function that represents Thread number 1
//-------------------------------------------
DWORD WINAPI Thread_no_1( LPVOID lpParam ) 
{

    int     Data = 0;
	int     count = 0;
    HANDLE  hStdout = NULL;

    // Get Handle To screen. Else how will we print?
    if( (hStdout = GetStdHandle(STD_OUTPUT_HANDLE)) == INVALID_HANDLE_VALUE )  
		return 1;

    // Cast the parameter to the correct data type passed by callee i.e main() in our case.
    Data = *((int*)lpParam); 

	for (count = 0; count <= 4; count++ )
	{
		DisplayMessage (hStdout, "Thread_no_1", Data, count);
	}
    
	return 1; 
} 

//-------------------------------------------
// A function that represents Thread number 2
//-------------------------------------------
DWORD WINAPI Thread_no_2( LPVOID lpParam ) 
{

    int     Data = 0;
	int     count = 0;
    HANDLE  hStdout = NULL;

    
    // Get Handle To screen. Else how will we print?
    if( (hStdout = GetStdHandle(STD_OUTPUT_HANDLE)) == INVALID_HANDLE_VALUE )  
		return 1;
	
    // Cast the parameter to the correct data type passed by callee i.e main() in our case.
    Data = *((int*)lpParam); 

	for (count = 0; count <= 7; count++ )
	{
		DisplayMessage (hStdout, "Thread_no_2", Data, count);
	}
    
	return 0; 
} 

//-------------------------------------------
// A function that represents Thread number 3
//-------------------------------------------
DWORD WINAPI Thread_no_3( LPVOID lpParam ) 
{

    int     Data = 0;
	int     count = 0;
    HANDLE  hStdout = NULL;

    // Get Handle To screen. Else how will we print?
    if( (hStdout = GetStdHandle(STD_OUTPUT_HANDLE)) == INVALID_HANDLE_VALUE )  
		return 1;

    // Cast the parameter to the correct data type passed by callee i.e main() in our case.
    Data = *((int*)lpParam); 

	for (count = 0; count <= 10; count++ )
	{
		DisplayMessage (hStdout, "Thread_no_3", Data, count);
	}
    
	return 0; 
} 

 
void main()
{

	int Data_Of_Thread_1 = 1;            // Data of Thread 1
    int Data_Of_Thread_2 = 2;            // Data of Thread 2
	int Data_Of_Thread_3 = 3;    
	int Data_Of_Thread_4 = 4;   // Data of Thread 3
	HANDLE Handle_Of_Thread_1 = 0;       // variable to hold handle of Thread 1
	HANDLE Handle_Of_Thread_2 = 0;       // variable to hold handle of Thread 1 
	HANDLE Handle_Of_Thread_3 = 0;       // variable to hold handle of Thread 1
	HANDLE Handle_Of_Thread_4 = 0;  
 	HANDLE Array_Of_Thread_Handles[3];   // Aray to store thread handles 


 	// Create thread 1.
    Handle_Of_Thread_1 = CreateThread( NULL, 0, Thread_no_1, &Data_Of_Thread_1, 0, NULL);  
	if ( Handle_Of_Thread_1 == NULL)  {
		
		
		ExitProcess(Data_Of_Thread_1);
	
	
	
	}
    
	// Create thread 2.
	Handle_Of_Thread_2 = CreateThread( NULL, 0, Thread_no_2, &Data_Of_Thread_2, 0, NULL);  
    if ( Handle_Of_Thread_2 == NULL)  ExitProcess(Data_Of_Thread_2);
    
	// Create thread 3.
	Handle_Of_Thread_3 = CreateThread( NULL, 0, Thread_no_3, &Data_Of_Thread_3, 0, NULL);  
    if ( Handle_Of_Thread_3 == NULL) {
		
		
		ExitProcess(Data_Of_Thread_3);
	}




		Handle_Of_Thread_4 = CreateThread( NULL, 0, wnd, &Data_Of_Thread_3, 0, NULL);  



	// Store Thread handles in Array of Thread Handles as per the requirement of WaitForMultipleObjects() 
	Array_Of_Thread_Handles[0] = Handle_Of_Thread_1;
	Array_Of_Thread_Handles[1] = Handle_Of_Thread_2;
	Array_Of_Thread_Handles[2] = Handle_Of_Thread_3;
    
	// Wait until all threads have terminated.
    WaitForMultipleObjects( 3, Array_Of_Thread_Handles, TRUE, INFINITE);

	printf("Since All threads executed lets close their handles \n");

	// Close all thread handles upon completion.
    CloseHandle(Handle_Of_Thread_1);
    CloseHandle(Handle_Of_Thread_2);
	CloseHandle(Handle_Of_Thread_3);
}

long __stdcall window_main_function_chvenia(HWND hwnd,unsigned int message
					, unsigned int wparam,long lparam)
{
	switch(message)
	{
	
	}
return DefWindowProc(hwnd,message,wparam,lparam);
}


DWORD WINAPI wnd(void *data)
{

	PROCESS_INFORMATION ProcessInfo; //This is what we get as an [out] parameter

STARTUPINFO StartupInfo; //This is an [in] parameter

ZeroMemory(&StartupInfo, sizeof(StartupInfo));
StartupInfo.cb = sizeof StartupInfo ; //Only compulsory field



HWND hwnd=0;
int X,Y,W,H;
ULONG style=0;

WNDCLASS wc;
ZeroMemory(&wc,sizeof(WNDCLASS));

wc.style=CS_DBLCLKS;
wc.lpfnWndProc=(WNDPROC)&window_main_function_chvenia;
wc.lpszClassName="12";
wc.hbrBackground=(HBRUSH)CreateSolidBrush(RGB(200,200,20));
wc.hIcon=(HICON)LoadImage(0,"c:\\1.ico",IMAGE_ICON,16,16,LR_LOADFROMFILE);

	if(RegisterClass(&wc)==0)
	{
	MessageBox(hwnd,"RegisterClass error",0,0);
	return 0;
	}


//mtavari fanjara:
style=WS_VISIBLE|WS_OVERLAPPEDWINDOW|WS_CLIPCHILDREN;
X=10;Y=30;W=750;H=500;
hwnd=CreateWindow(wc.lpszClassName,"Main",style,X,Y,W,H,0,0,0,0);
WinExec("calc.exe",1);


CreateProcess("calc.exe", NULL, 
NULL,NULL,FALSE,0,NULL,
NULL,&StartupInfo,&ProcessInfo);


	//HWND hwnd = FindWindowFromExe("calc.exe");
	HWND hwnd12 = FindWindow("calc.exe",0);
	DestroyWindow(hwnd12);








MSG msg;
	while(GetMessage(&msg,0,0,0))
	{
	TranslateMessage(&msg);
	DispatchMessage(&msg);
	}



}
