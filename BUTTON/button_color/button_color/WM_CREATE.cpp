﻿        case WM_CREATE:
            {
                HWND Exit_Button = CreateWindowEx(NULL, L"BUTTON", L"EXIT", WS_VISIBLE | WS_CHILD | BS_PUSHBUTTON|BS_NOTIFY,10, 50, 68, 68, hwnd, (HMENU)IDC_EXIT_BUTTON, NULL, NULL);
				
				HBITMAP mybut12= (HBITMAP)LoadImage(NULL,L"C:\\Users\\vakho1\\Desktop\\New Bitmap Image.bmp", IMAGE_BITMAP,100,100, LR_LOADFROMFILE);
				



				
				
				
				
				
		HCURSOR	hCursor2 = LoadCursorFromFile(L"F:\\WINAPI1-\\ShutDown on desktop\\Glove Normal.cur");

		SendMessage(Exit_Button, WM_SETCURSOR, 0, (LPARAM) hCursor2);
		SetWindowLong(Exit_Button, GCL_HCURSOR, (LONG)hCursor2);
		SetWindowLongPtr(Exit_Button, GCL_HCURSOR, (LONG_PTR)hCursor2);

		// SetClassLong (buton, GCL_HCURSOR, (LONG) LoadCursor (NULL, IDC_CROSS)); //// standartuli kursoris chasma
	//	 SetClassLong (Exit_Button, GCL_HCURSOR, (LONG) hCursor2);
				





				if(mybut12)	{
					
					//SendMessage(Exit_Button, (UINT)BM_SETIMAGE,   (WPARAM)IMAGE_BITMAP, (LPARAM)mybut12);


				}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

				
				  HWND Exit_Button1 = CreateWindowEx(NULL, L"BUTTON", L"qss", WS_VISIBLE | WS_CHILD | BS_PUSHBUTTON,300, 50, 68, 68, hwnd, (HMENU)IDC_EXIT_BUTTON, NULL, NULL);

				HBITMAP mybut13=  (HBITMAP)LoadImage(NULL,L"C:\\Users\\vakho1\\Desktop\\aa.bmp", IMAGE_BITMAP,100,70, LR_LOADFROMFILE);
				
				
		HCURSOR	hCursor1 = LoadCursorFromFile(L"F:\\WINAPI1-\\icon\\cur197.ani");

		SendMessage(Exit_Button1, WM_SETCURSOR, 0, (LPARAM) hCursor1);
		SetWindowLong(Exit_Button1, GCL_HCURSOR, (LONG)hCursor1);
		SetWindowLongPtr(Exit_Button1, GCL_HCURSOR, (LONG_PTR)hCursor1);

		// SetClassLong (buton, GCL_HCURSOR, (LONG) LoadCursor (NULL, IDC_CROSS)); //// standartuli kursoris chasma
	//	 SetClassLong (Exit_Button1, GCL_HCURSOR, (LONG) hCursor1);
				
				
				
				
				
				if(mybut13){



		







							SendMessage(Exit_Button1, (UINT)BM_SETIMAGE,   (WPARAM)IMAGE_BITMAP, (LPARAM)mybut13);
						}


				if(Exit_Button == NULL)
                    {
                        MessageBox(NULL, L"Button Creation Failed!", L"Error!", MB_ICONEXCLAMATION);
                        exit(EXIT_FAILURE);
                    }

                HWND Pushlike_Button = CreateWindowEx(NULL, L"BUTTON", L"PUSH ME!", 
                                                        WS_VISIBLE | WS_CHILD | BS_AUTOCHECKBOX | BS_PUSHLIKE, 
                                                        200, 50, 30, 30, hwnd, (HMENU)IDC_PUSHLIKE_BUTTON, NULL, NULL);








                if(Pushlike_Button == NULL)
                    {
                        MessageBox(NULL, L"Button Creation Failed!", L"Error!", MB_ICONEXCLAMATION);
                        exit(EXIT_FAILURE);
                    }




				   HWND Pushlike_Button1 = CreateWindowEx(NULL, L"BUTTON", L"PUSH ME!", 
                                                        WS_VISIBLE | WS_CHILD | BS_AUTOCHECKBOX | BS_PUSHLIKE|BS_OWNERDRAW, 
                                                        300, 50, 30, 30, hwnd, (HMENU)IDC_PUSHLIKE_BUTTON1, NULL, NULL);








                if(Pushlike_Button1 == NULL)
                    {
                        MessageBox(NULL, L"Button Creation Failed!", L"Error!", MB_ICONEXCLAMATION);
                        exit(EXIT_FAILURE);
                    }



				   HWND Pushlike_Button2 = CreateWindowEx(NULL, L"BUTTON", L"PUSH ME!", 
                                                        WS_VISIBLE | WS_CHILD | BS_AUTOCHECKBOX | BS_PUSHLIKE| BS_PUSHBUTTON, 
                                                        0, 150, 100, 100, hwnd, (HMENU)IDC_PUSHLIKE_BUTTON2, NULL, NULL);



				   HBITMAP  mybut= (HBITMAP)LoadImage(NULL,L"C:\\Users\\vaxoa\\OneDrive\\Desktop\\icon\\MARBLES.BMP", IMAGE_BITMAP,300,300, LR_LOADFROMFILE);

				 if(mybut)
					{
						SendMessage(Pushlike_Button2, (UINT)BM_SETIMAGE,   (WPARAM)IMAGE_BITMAP, (LPARAM)mybut);
						//SendMessage(hlogom,STM_SETIMAGE,   (WPARAM)IMAGE_BITMAP, (LPARAM)mybut);
					}

                if(Pushlike_Button1 == NULL)
                    {
                        MessageBox(NULL, L"Button Creation Failed!", L"Error!", MB_ICONEXCLAMATION);
                        exit(EXIT_FAILURE);
                    }


            }
        break;