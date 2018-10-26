HWND g_hwndButton;



DWORD WINAPI EditProc(HWND hwnd,UINT sms,WPARAM wp,LPARAM lp)
{
		   WNDPROC wpOld = (WNDPROC)GetWindowLongPtr(g_hwndButton, GWLP_USERDATA);

	   if(wpOld)
	   {
			   switch(sms)
			   {
				    
			   }
			    

	   }










	 switch( sms )
    {
   
      


	      case WM_KEYDOWN:

			  	
	
			  break;

    }





    return CallWindowProc( g_wndProcButtonOrigianl, hwnd, sms, wp, lp);
}