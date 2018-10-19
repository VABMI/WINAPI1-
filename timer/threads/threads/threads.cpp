#include "stdafx.h"

#define BUF_SIZE 255

long __stdcall vaxo(HWND hw,UINT sms,WPARAM wp,LPARAM lp){
	static int i=0;
	//_______________________//
	HANDLE Handle_Of_Thread_5;
	int Data_Of_Thread_5=0;
	//_ _ _ _ _ _ _ _ _ _ _ //
	HDC hdcTxt=GetDC(hw);
	wchar_t m[50];
	switch(sms)
	{

	case WM_TIMER:
		switch(wp)
		{
		case 101:
			i++;
			swprintf_s(m, L"%d",i,1);
			TextOut(hdcTxt,100,100,(LPCSTR)m,6);
			break;

		}
		break;
	case WM_CREATE:
		menubar(hw);
		
		break;

	case WM_COMMAND:
		switch(wp)
		{
		case 33:	MessageBox(hw,"asd","asdasd",1);
			break;
		case 1:
		 Handle_Of_Thread_5 = CreateThread(NULL,0,timer,&Data_Of_Thread_5,0,0);
		//SetTimer(hw,101,300,0);
			break;
		case 2:
			KillTimer(hw,101);
			break;
		}
		
		break;



	}

	return DefWindowProc(hw,sms,wp,lp);
}


DWORD WINAPI  wndClass(LPVOID lpParam );


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

	while(1)
	{
		DisplayMessage (hStdout, "Thread_no_1", Data, count);
	}
    
	return 0; 
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

	while(1)
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

 
	{
		DisplayMessage (hStdout, "Thread_no_3", Data, count);
	}
    
	return 0; 
} 

 
void main()
{





//	wndClass();



	int Data_Of_Thread_1 = 1;            // Data of Thread 1
    int Data_Of_Thread_2 = 2;            // Data of Thread 2
	int Data_Of_Thread_3 = 3; 
	int Data_Of_Thread_4 = 4;  // Data of Thread 3
	HANDLE Handle_Of_Thread_1 = 0;       // variable to hold handle of Thread 1
	HANDLE Handle_Of_Thread_2 = 0;       // variable to hold handle of Thread 1 
	HANDLE Handle_Of_Thread_3 = 0;     
	HANDLE Handle_Of_Thread_4 = 0;  
	HANDLE Array_Of_Thread_Handles[3];   // Aray to store thread handles 

 	// Create thread 1.
    Handle_Of_Thread_1 = CreateThread( NULL, 0, Thread_no_1, &Data_Of_Thread_1, 0, NULL);  
    if ( Handle_Of_Thread_1 == NULL)  ExitProcess(Data_Of_Thread_1);
    
	// Create thread 2.
	Handle_Of_Thread_2 = CreateThread( NULL, 0, Thread_no_2, &Data_Of_Thread_2, 0, NULL);  
    if ( Handle_Of_Thread_2 == NULL)  ExitProcess(Data_Of_Thread_2);
    
	// Create thread 3.
	Handle_Of_Thread_3 = CreateThread( NULL, 0, Thread_no_3, &Data_Of_Thread_3, 0, NULL);  
    if ( Handle_Of_Thread_3 == NULL)  ExitProcess(Data_Of_Thread_3);


	 Handle_Of_Thread_4 = CreateThread(NULL,0,wndClass,&Data_Of_Thread_4,0,0);
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

DWORD WINAPI  wndClass(LPVOID lpParam )
{





	WNDCLASS wc={0};

	wc.lpszClassName="vaxo";
	wc.lpfnWndProc=&vaxo;
	wc.hbrBackground= (HBRUSH)GetStockObject(COLOR_WINDOW+1);
	wc.hCursor=LoadCursor(NULL, IDC_ICON);
	if(!RegisterClass(&wc)){
	}

hwnd1=CreateWindow("vaxo","main",WS_VISIBLE|WS_CLIPCHILDREN|WS_OVERLAPPEDWINDOW,100,100,1000,1000,0,0,0,0);
	CreateWindow("button","click",WS_VISIBLE|WS_CHILD|WS_BORDER,10,10,50,50,hwnd1,(HMENU)33,0,0);
	MSG msg;
	while(GetMessage(&msg,0,0,0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}

	return 0;

}