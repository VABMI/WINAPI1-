case WM_NOTIFY:
        {
            LPNMHDR some_item = (LPNMHDR)lparam;




////////////////////////////////////////////////////////////////////  EXIT BUTTONNNNN  //////////////////////////////////////////////////



            if (some_item->idFrom == SHUTDOWN && some_item->code == NM_CUSTOMDRAW)
            {
                LPNMCUSTOMDRAW item = (LPNMCUSTOMDRAW)some_item;
				     MessageBox(hwnd,"asdad","asdasd",0);
                if (item->uItemState & CDIS_SELECTED)
                {
                    MessageBox(hwnd,"asdad","asdasd",0);
				
				}
                else
                {
                    if (item->uItemState & CDIS_HOT) //Our mouse is over the button
                    {
                      
                    }

                   
                }
            }

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////






        }
        break;