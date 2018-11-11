case WM_NOTIFY:
        {
            LPNMHDR some_item = (LPNMHDR)lParam;




////////////////////////////////////////////////////////////////////  EXIT BUTTONNNNN  //////////////////////////////////////////////////


			/*
            if (some_item->idFrom == IDC_EXIT_BUTTON && some_item->code == NM_CUSTOMDRAW)
            {
                LPNMCUSTOMDRAW item = (LPNMCUSTOMDRAW)some_item;

                if (item->uItemState & CDIS_SELECTED)
                {
                    //Select our color when the button is selected
						if (selectbrush == NULL)
                        selectbrush = CreateGradientBrush(RGB(180, 0, 0), RGB(255, 180, 0), item);

                    //Create pen for button border
                    HPEN pen = CreatePen(PS_INSIDEFRAME, 0, RGB(0, 0, 0));

                    //Select our brush into hDC
                    HGDIOBJ old_pen = SelectObject(item->hdc, pen);
                    HGDIOBJ old_brush = SelectObject(item->hdc, selectbrush);

                    //If you want rounded button, then use this, otherwise use FillRect().
                    RoundRect(item->hdc, item->rc.left, item->rc.top, item->rc.right, item->rc.bottom, 100, 100);

                    //Clean up
                    SelectObject(item->hdc, old_pen);
                    SelectObject(item->hdc, old_brush);
                    DeleteObject(pen);

                    //Now, I don't want to do anything else myself (draw text) so I use this value for return:
                    return CDRF_DODEFAULT;
                    //Let's say I wanted to draw text and stuff, then I would have to do it before return with
                    //DrawText() or other function and return CDRF_SKIPDEFAULT
                }
                else
                {
                    if (item->uItemState & CDIS_HOT) //Our mouse is over the button
                    {
                        //Select our color when the mouse hovers our button
                        if (hotbrush == NULL)
                            hotbrush = CreateGradientBrush(RGB(255, 230, 51), RGB(245, 128, 0), item);

                        HPEN pen = CreatePen(PS_INSIDEFRAME, 0, RGB(139, 0, 0));

                        HGDIOBJ old_pen = SelectObject(item->hdc, pen);
                        HGDIOBJ old_brush = SelectObject(item->hdc, hotbrush);

                        RoundRect(item->hdc, item->rc.left, item->rc.top, item->rc.right, item->rc.bottom, 100, 100); //// hoveri exit buttonis ??

                        SelectObject(item->hdc, old_pen);
                        SelectObject(item->hdc, old_brush);
                        DeleteObject(pen);

                        return CDRF_DODEFAULT;
                    }

                    //Select our color when our button is doing nothing
                    if (defaultbrush == NULL)
                        defaultbrush = CreateGradientBrush(RGB(255, 180, 89), RGB(180, 0, 0), item);

                    HPEN pen = CreatePen(PS_INSIDEFRAME, 0, RGB(0, 0, 0));

                    HGDIOBJ old_pen = SelectObject(item->hdc, pen);
                    HGDIOBJ old_brush = SelectObject(item->hdc, defaultbrush);

                   RoundRect(item->hdc, item->rc.left, item->rc.top, item->rc.right, item->rc.bottom, 100, 100); /// exit button

                    SelectObject(item->hdc, old_pen);
                    SelectObject(item->hdc, old_brush);
                    DeleteObject(pen);

                    return CDRF_DODEFAULT;
                }
            }
			*/

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////






			//////////////////////// gilakis dacheris dros feris shecvlaaaaaaa 1 ////////////////////////////////
             if (some_item->idFrom == IDC_PUSHLIKE_BUTTON && some_item->code == NM_CUSTOMDRAW)
            {
                LPNMCUSTOMDRAW item = (LPNMCUSTOMDRAW)some_item;

                if (IsDlgButtonChecked(hwnd, some_item->idFrom))
                {
                    if (item->uItemState & CDIS_HOT)
                    {

                        if (push_hotbrush1 == NULL)
                            push_hotbrush1 = CreateGradientBrush(RGB(246, 0, 245), RGB(129, 230, 255), item);

                        HPEN pen = CreatePen(PS_INSIDEFRAME, 0, RGB(0, 0, 0));  

                        HGDIOBJ old_pen = SelectObject(item->hdc, pen);
                        HGDIOBJ old_brush = SelectObject(item->hdc, push_hotbrush1);

                        RoundRect(item->hdc, item->rc.left, item->rc.top, item->rc.right, item->rc.bottom, 10, 10);

                        SelectObject(item->hdc, old_pen);
                        SelectObject(item->hdc, old_brush);
                        DeleteObject(pen);

                        return CDRF_DODEFAULT;
                    }


                    if (push_checkedbrush == NULL)
                        push_checkedbrush = CreateGradientBrush(RGB(255, 255, 180), RGB(255, 222, 200), item);


                    HPEN pen = CreatePen(PS_INSIDEFRAME, 0, RGB(255, 0, 0)); ///// clicked border 


                    HGDIOBJ old_pen = SelectObject(item->hdc, pen);
                    HGDIOBJ old_brush = SelectObject(item->hdc, push_checkedbrush);


                    RoundRect(item->hdc, item->rc.left, item->rc.top, item->rc.right, item->rc.bottom, 10, 10);


                    SelectObject(item->hdc, old_pen);
                    SelectObject(item->hdc, old_brush);
                    DeleteObject(pen);


                    return CDRF_DODEFAULT;
                }
                else
                {
                    if (item->uItemState & CDIS_HOT)
                    {
                        if (push_hotbrush2 == NULL)
                            push_hotbrush2 = CreateGradientBrush(RGB(255, 230, 0), RGB(245, 0, 0), item);

                        HPEN pen = CreatePen(PS_INSIDEFRAME, 0, RGB(0, 0, 0)); //// HOVER border colorr 

                        HGDIOBJ old_pen = SelectObject(item->hdc, pen);
                        HGDIOBJ old_brush = SelectObject(item->hdc, push_hotbrush2);

                        RoundRect(item->hdc, item->rc.left, item->rc.top, item->rc.right, item->rc.bottom, 10, 10);

                        SelectObject(item->hdc, old_pen);
                        SelectObject(item->hdc, old_brush);
                        DeleteObject(pen);

                        return CDRF_DODEFAULT;
                    }

                    if (push_uncheckedbrush == NULL)
                        push_uncheckedbrush = CreateGradientBrush(RGB(255, 180, 0), RGB(180, 0, 0), item);

                    HPEN pen = CreatePen(PS_INSIDEFRAME, 0, RGB(255, 0, 0));  //// bordr color static position 

                    HGDIOBJ old_pen = SelectObject(item->hdc, pen);
                    HGDIOBJ old_brush = SelectObject(item->hdc, defaultbrush);

                    RoundRect(item->hdc, item->rc.left, item->rc.top, item->rc.right, item->rc.bottom, 10, 10);

                    SelectObject(item->hdc, old_pen);
                    SelectObject(item->hdc, old_brush);
                    DeleteObject(pen);

                    return CDRF_DODEFAULT;
                }
            }





					//////////////////////// gilakis dacheris dros feris shecvlaaaaaaa 2 ////////////////////////////////

		  if (some_item->idFrom == IDC_PUSHLIKE_BUTTON1 && some_item->code == NM_CUSTOMDRAW) /////// chemi damatebuli
            {
                LPNMCUSTOMDRAW item = (LPNMCUSTOMDRAW)some_item;

                if (IsDlgButtonChecked(hwnd, some_item->idFrom))
                {
                    if (item->uItemState & CDIS_HOT)
                    {

                        if (push_hotbrush1 == NULL)
                            push_hotbrush1 = CreateGradientBrush(RGB(0, 0, 245), RGB(0, 230, 255), item);

                        HPEN pen = CreatePen(PS_INSIDEFRAME, 0, RGB(255, 0, 0));

                        HGDIOBJ old_pen = SelectObject(item->hdc, pen);
                        HGDIOBJ old_brush = SelectObject(item->hdc, push_hotbrush1);

                        RoundRect(item->hdc, item->rc.left, item->rc.top, item->rc.right, item->rc.bottom, 10, 10);

                        SelectObject(item->hdc, old_pen);
                        SelectObject(item->hdc, old_brush);
                        DeleteObject(pen);

                        return CDRF_DODEFAULT;
                    }


                    if (push_checkedbrush == NULL)
                        push_checkedbrush = CreateGradientBrush(RGB(0, 0, 180), RGB(0, 222, 200), item);


                    HPEN pen = CreatePen(PS_INSIDEFRAME, 0, RGB(255, 0, 0)); ///// clicked border 22 


                    HGDIOBJ old_pen = SelectObject(item->hdc, pen);
                    HGDIOBJ old_brush = SelectObject(item->hdc, push_checkedbrush);


                    RoundRect(item->hdc, item->rc.left, item->rc.top, item->rc.right, item->rc.bottom, 10, 10);


                    SelectObject(item->hdc, old_pen);
                    SelectObject(item->hdc, old_brush);
                    DeleteObject(pen);


                    return CDRF_DODEFAULT;
                }
                else
                {
                    if (item->uItemState & CDIS_HOT)
                    {
                        if (push_hotbrush2 == NULL)
                            push_hotbrush2 = CreateGradientBrush(RGB(255, 230, 0), RGB(245, 0, 0), item);

                        HPEN pen = CreatePen(PS_INSIDEFRAME, 0, RGB(255, 0, 0)); //// HOVER border   color 

                        HGDIOBJ old_pen = SelectObject(item->hdc, pen);
                        HGDIOBJ old_brush = SelectObject(item->hdc, push_hotbrush2);

                        RoundRect(item->hdc, item->rc.left, item->rc.top, item->rc.right, item->rc.bottom, 10, 10);

                        SelectObject(item->hdc, old_pen);
                        SelectObject(item->hdc, old_brush);
                        DeleteObject(pen);

                        return CDRF_DODEFAULT;
                    }

                    if (push_uncheckedbrush == NULL)
                        push_uncheckedbrush = CreateGradientBrush(RGB(255, 180, 0), RGB(180, 0, 0), item);

                    HPEN pen = CreatePen(PS_INSIDEFRAME, 0, RGB(255, 0, 0));   //// border color static position 

                    HGDIOBJ old_pen = SelectObject(item->hdc, pen);
                    HGDIOBJ old_brush = SelectObject(item->hdc, defaultbrush);

                    RoundRect(item->hdc, item->rc.left, item->rc.top, item->rc.right, item->rc.bottom, 10, 10);

                    SelectObject(item->hdc, old_pen);
                    SelectObject(item->hdc, old_brush);
                    DeleteObject(pen);

                    return CDRF_DODEFAULT;
                }
            }





		  					//////////////////////// gilakis dacheris dros feris shecvlaaaaaaa 3 ////////////////////////////////

		  else if (some_item->idFrom == IDC_PUSHLIKE_BUTTON2 && some_item->code == NM_CUSTOMDRAW) /////// chemi damatebuli
            {
                LPNMCUSTOMDRAW item = (LPNMCUSTOMDRAW)some_item;

                if (IsDlgButtonChecked(hwnd, some_item->idFrom))
                {
                    if (item->uItemState & CDIS_HOT)
                    {

                        if (push_hotbrush1 == NULL)
                            push_hotbrush1 = CreateGradientBrush(RGB(0, 0, 0), RGB(0, 0, 0), item);

                        HPEN pen = CreatePen(PS_INSIDEFRAME, 0, RGB(0, 0, 0));

                        HGDIOBJ old_pen = SelectObject(item->hdc, pen);
                        HGDIOBJ old_brush = SelectObject(item->hdc, push_hotbrush1);

                        RoundRect(item->hdc, item->rc.left, item->rc.top, item->rc.right, item->rc.bottom, 100, 100);
						 SetBkMode(item->hdc, TRANSPARENT);
                        SelectObject(item->hdc, old_pen);
                        SelectObject(item->hdc, old_brush);
                        DeleteObject(pen);

                        return CDRF_DODEFAULT;
                    }


                    if (push_checkedbrush == NULL)
                        push_checkedbrush = CreateGradientBrush(RGB(0, 0, 0), RGB(0, 0, 0), item);


                    HPEN pen = CreatePen(PS_INSIDEFRAME, 0, RGB(255, 0, 0)); ///// clicked border 22 


                    HGDIOBJ old_pen = SelectObject(item->hdc, pen);
                    HGDIOBJ old_brush = SelectObject(item->hdc, push_checkedbrush);


                    RoundRect(item->hdc, item->rc.left, item->rc.top, item->rc.right, item->rc.bottom, 100, 100);

					 SetBkMode(item->hdc, TRANSPARENT);
                    SelectObject(item->hdc, old_pen);
                    SelectObject(item->hdc, old_brush);
                    DeleteObject(pen);


                    return CDRF_DODEFAULT;
                }
                else
                {
                    if (item->uItemState & CDIS_HOT)
                    {
                        if (push_hotbrush2 == NULL)
                            push_hotbrush2 = CreateGradientBrush(RGB(0, 0, 0), RGB(0, 0, 0), item);

                        HPEN pen = CreatePen(PS_INSIDEFRAME, 0, RGB(0, 0, 0)); //// HOVER border   color 

                        HGDIOBJ old_pen = SelectObject(item->hdc, pen);
                        HGDIOBJ old_brush = SelectObject(item->hdc, push_hotbrush2);

                        RoundRect(item->hdc, item->rc.left, item->rc.top, item->rc.right, item->rc.bottom, 100, 100);
						 SetBkMode(item->hdc, TRANSPARENT);
                        SelectObject(item->hdc, old_pen);
                        SelectObject(item->hdc, old_brush);
                        DeleteObject(pen);

                        return CDRF_DODEFAULT;
                    }

                    if (push_uncheckedbrush == NULL)
                        push_uncheckedbrush = CreateGradientBrush(RGB(0, 0, 0), RGB(0, 0, 0), item);

                    HPEN pen = CreatePen(PS_INSIDEFRAME, 0, RGB(225, 0, 0));   //// border color static position 

                    HGDIOBJ old_pen = SelectObject(item->hdc, pen);
                    HGDIOBJ old_brush = SelectObject(item->hdc, defaultbrush);

                    RoundRect(item->hdc, item->rc.left, item->rc.top, item->rc.right, item->rc.bottom, 100, 100);
					 SetBkMode(item->hdc, TRANSPARENT);
                    SelectObject(item->hdc, old_pen);
                    SelectObject(item->hdc, old_brush);
                    DeleteObject(pen);

                    return CDRF_DODEFAULT;
                }
            }





            return CDRF_DODEFAULT;
        }
        break;