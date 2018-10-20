

HWND    g_hwndButton            = NULL;  
HWND       g_hwndButton1	=NULL;
WNDPROC g_wndProcButtonOrigianl = NULL;   
BOOL    g_bSeeingMouse          = FALSE;






LRESULT CALLBACK WndProcButton (HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{

	   WNDPROC wpOld = (WNDPROC)GetWindowLongPtr(EDITGLOBAL, GWLP_USERDATA);

	   if(wpOld)
	   {  
		   
		   int x=0;
			   switch(message)
			   {
			   case WM_KEYDOWN:


				 
				   break;				
			   }
			    

	   }

	   
			   switch(message)
			   {
			   case WM_KEYDOWN:

				   if(gn==0)
				   {
					   gn=1;

				   }
				 	  // int x=0;
				   break;				
			   }
			    


    return CallWindowProc( g_wndProcButtonOrigianl, hwnd, message, wParam, lParam );
}
