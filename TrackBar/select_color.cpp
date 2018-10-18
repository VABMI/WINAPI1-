COLORREF select_color(HWND hwnd)
{


	CHOOSECOLOR cc;                 // common dialog box structure  
	static COLORREF acrCustClr[16]; // array of custom colors 
									//HWND hwnd;                      // owner window
	HBRUSH hbrush;                  // brush handle
	static DWORD rgbCurrent;        // initial color selection

									// Initialize CHOOSECOLOR 
	ZeroMemory(&cc, sizeof(CHOOSECOLOR));
	cc.lStructSize = sizeof(CHOOSECOLOR);
	cc.hwndOwner = hwnd;
	cc.lpCustColors = (LPDWORD)acrCustClr;
	cc.rgbResult = rgbCurrent;
	cc.Flags = CC_FULLOPEN | CC_RGBINIT;


	if (ChooseColor(&cc) == TRUE) {
		hbrush = CreateSolidBrush(cc.rgbResult);
		rgbCurrent = cc.rgbResult;
	}

	return rgbCurrent;
}