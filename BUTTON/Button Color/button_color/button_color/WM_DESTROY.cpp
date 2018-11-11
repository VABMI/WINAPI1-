       case WM_DESTROY:
            {
                DeleteObject(defaultbrush);
                DeleteObject(selectbrush);
                DeleteObject(hotbrush);
                DeleteObject(push_checkedbrush);
                DeleteObject(push_hotbrush1);
                DeleteObject(push_hotbrush2);
                DeleteObject(push_uncheckedbrush);
                PostQuitMessage(0);
                return 0;
            }
        break;