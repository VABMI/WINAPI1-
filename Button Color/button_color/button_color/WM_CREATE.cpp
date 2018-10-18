        case WM_CREATE:
            {
                HWND Exit_Button = CreateWindowEx(NULL, L"BUTTON", L"EXIT", WS_VISIBLE | WS_CHILD | BS_PUSHBUTTON,50, 50, 100, 100, hwnd, (HMENU)IDC_EXIT_BUTTON, NULL, NULL);
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
                                                        WS_VISIBLE | WS_CHILD | BS_AUTOCHECKBOX | BS_PUSHLIKE, 
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