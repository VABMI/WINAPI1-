#include <windows.h>
#include<stdio.h>
#include <math.h>
/* This is where all the input to the window goes to */
LRESULT CALLBACK WndProc(HWND hwnd, UINT Message, WPARAM wParam, LPARAM lParam) {
		HWND cm=GetDlgItem(hwnd,1);	HWND lm=GetDlgItem(hwnd,3); HWND edit=GetDlgItem(hwnd,4);
		char in[100];
		char out[100];
	switch(Message) {
		
		case WM_COMMAND:
			
		switch(wParam)
			{
			case 2:
			MessageBox(hwnd,"Asdasd","Asdas",1);
        	SendMessage(edit,WM_GETTEXT,99,(LPARAM)in);
	    	long int ini=atoi(in);
	    	
	        for (long int i=3; i<ini; i=i+2) 
			{
        	for (long int j=2; j<=i; j=j++)
        		{
            	if (i % j == 0) 
                break;
            	else if (i == j+1)
					{
            		sprintf(out,"%i",i);
					SendMessage(cm,CB_ADDSTRING,0,(LPARAM)out);
					SendMessage(lm,LB_ADDSTRING,0,(LPARAM)out);
			    
					}
           

        		}   
  			}
				
				
			}
			
			break;
		case WM_DESTROY: {
			PostQuitMessage(0);
			break;
		}
		default:
			return DefWindowProc(hwnd, Message, wParam, lParam);
	}
	return 0;
}
int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow) {
	WNDCLASSEX wc; /* A properties struct of our window */
	HWND hwnd,combo,listb,edit; /* A 'HANDLE', hence the H, or a pointer to our window */
	MSG msg;
	memset(&wc,0,sizeof(wc));
	wc.cbSize		 = sizeof(WNDCLASSEX);
	wc.lpfnWndProc	 = WndProc; /* This is where we will send messages to */
	wc.hInstance	 = hInstance;
	wc.hCursor		 = LoadCursor(NULL, IDC_ARROW);
	wc.hbrBackground = (HBRUSH)(COLOR_WINDOW+1);
	wc.lpszClassName = "WindowClass";
	wc.hIcon		 = LoadIcon(NULL, IDI_APPLICATION); /* Load a standard icon */
	wc.hIconSm		 = LoadIcon(NULL, IDI_APPLICATION); /* use the name "A" to use the project icon */

	if(!RegisterClassEx(&wc)) {
		MessageBox(NULL, "Window Registration Failed!","Error!",MB_ICONEXCLAMATION|MB_OK);
		return 0;
	}
	hwnd = CreateWindowEx(WS_EX_CLIENTEDGE,"WindowClass","Caption",WS_VISIBLE|WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT,
		CW_USEDEFAULT,
		640, 
		480, 
		NULL,NULL,hInstance,NULL);
	edit=CreateWindow("Edit","",WS_VISIBLE|WS_BORDER|WS_CHILD|WS_OVERLAPPED,10,50,100,20,hwnd,(HMENU)4,0,0);	
	combo=CreateWindow("ComboBox","",WS_VISIBLE|WS_BORDER|WS_CHILD|  CBS_DROPDOWN | CBS_HASSTRINGS | WS_OVERLAPPED|WS_VSCROLL ,150,40,100,200,hwnd,(HMENU)1,0,0);
	listb=CreateWindow("listBox","asdasd",WS_VISIBLE|WS_BORDER|WS_CHILD|WS_VSCROLL ,350,40,240,200,hwnd,(HMENU)3,0,0);
	CreateWindow("button","asdasd",WS_VISIBLE|WS_BORDER|WS_CHILD,10,150,40,40,hwnd,(HMENU)2,0,0);

	if(hwnd == NULL) {
		MessageBox(NULL, "Window Creation Failed!","Error!",MB_ICONEXCLAMATION|MB_OK);
		return 0;
	}
	while(GetMessage(&msg, NULL, 0, 0) > 0) { /* If no error is received... */
		TranslateMessage(&msg); /* Translate key codes to chars if present */
		DispatchMessage(&msg); /* Send it to WndProc */
	}
	return msg.wParam;
}
