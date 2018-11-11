       case WM_COMMAND:
            {
                switch(LOWORD(wParam))
                    {
                        case IDC_EXIT_BUTTON:
                            {
                                SendMessage(hwnd, WM_CLOSE, 0, 0);
                            }
                        break;
                    }
            }
        break;
