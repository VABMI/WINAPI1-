#include "head.h"


/*
void print(HWND hwnd)
{ int zz=0;
	HANDLE print;
	DOC_INFO_1
		doc;
	unsigned char ss[]="asdadasdads";
	
//	zz= OpenPrinter("asdasdasd",&print,NULL);

	  StartDocPrinter(hwnd,1,ss);
	   //   MessageBox(hwnd,(LPCSTR)GetLastError(),"Asdasd",1);
	//zz= StartPagePrinter((HANDLE )hwnd);
			SetPrinter(print,0,NULL,PRINTER_CONTROL_PAUSE);

	
}





  BOOL RawDataToPrinter(LPSTR szPrinterName, LPBYTE lpData, DWORD dwCount)
   {
     HANDLE     hPrinter;
     DOC_INFO_1 DocInfo;
     DWORD      dwJob;
     DWORD      dwBytesWritten;

     // Need a handle to the printer.
     if( ! OpenPrinter( szPrinterName, &hPrinter, NULL ) )
       return FALSE;

     // Fill in the structure with info about this "document."
     DocInfo.pDocName = "My Document";
     DocInfo.pOutputFile = NULL;
     DocInfo.pDatatype = "RAW";
     // Inform the spooler the document is beginning.
     if( (dwJob = StartDocPrinter( hPrinter, 1, (LPBYTE)&DocInfo )) == 0 )
     {
       ClosePrinter( hPrinter );
       return FALSE;
     }
     // Start a page.
     if( ! StartPagePrinter( hPrinter ) )
     {
       EndDocPrinter( hPrinter );
       ClosePrinter( hPrinter );
       return FALSE;
     }
     // Send the data to the printer.
     if( ! WritePrinter( hPrinter, lpData, dwCount, &dwBytesWritten ) )
     {
       EndPagePrinter( hPrinter );
       EndDocPrinter( hPrinter );
       ClosePrinter( hPrinter );
       return FALSE;
     }
     // End the page.
     if( ! EndPagePrinter( hPrinter ) )
     {
       EndDocPrinter( hPrinter );
       ClosePrinter( hPrinter );
       return FALSE;
     }
     // Inform the spooler that the document is ending.
     if( ! EndDocPrinter( hPrinter ) )
     {
       ClosePrinter( hPrinter );
       return FALSE;
     }
     // Tidy up the printer handle.
     ClosePrinter( hPrinter );
     // Check to see if correct number of bytes were written.
     if( dwBytesWritten != dwCount )
       return FALSE;
     return TRUE;
   }


   */
     void RawDataToPrinter(HWND hwnd)
	 {

		 static PRINTDLG  pDlg;
		 static DOCINFO dInfo;
		 dInfo.cbSize=sizeof(dInfo);
		 dInfo.lpszDocName="print";
		 pDlg.Flags=PD_RETURNDC;
		 pDlg.hInstance=(HINSTANCE)GetWindowLong(hwnd,GWL_HINSTANCE);
		 pDlg.lStructSize=sizeof(pDlg);
		 pDlg.hwndOwner=hwnd;

		 PrintDlg(&pDlg);
		 StartDoc(pDlg.hDC,&dInfo);
		 StartPage(pDlg.hDC);
		 TextOut(pDlg.hDC,0,0,"HELLO",6);
		 EndPage(pDlg.hDC);
		 EndDoc(pDlg.hDC);
		 ReleaseDC(hwnd,pDlg.hDC);
	 }