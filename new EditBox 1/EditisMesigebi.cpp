






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

				//   if(lParam==VK_CONTROL)
				     if(wParam==VK_CONTROL)
				   {
					   if(lParam==0x43)
					   int y=0;

				   }
				   break;
			   case WM_SYSKEYDOWN:
				 

				   if(wParam==0x43)
				   {
					//	if(lParam==VK_CONTROL)
					   int y=0;

				   }


				   break;				
			   }
			    


    return CallWindowProc( g_wndProcButtonOrigianl, hwnd, message, wParam, lParam );
}
