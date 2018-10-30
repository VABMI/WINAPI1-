
LRESULT ProcessCustomDraw (LPARAM lParam)
{
    LPNMLVCUSTOMDRAW lplvcd = (LPNMLVCUSTOMDRAW)lParam;

    switch(lplvcd->nmcd.dwDrawStage) 
    {
        case CDDS_PREPAINT : //Before the paint cycle begins
            //request notifications for individual listview items
            return CDRF_NOTIFYITEMDRAW;
            
        case CDDS_ITEMPREPAINT: //Before an item is drawn
            {
                return CDRF_NOTIFYSUBITEMDRAW;
            }
            break;
    
        case CDDS_SUBITEM | CDDS_ITEMPREPAINT: //Before a subitem is drawn
            {
                switch(lplvcd->iSubItem)
                {
                    case 0:
                    {
                      lplvcd->clrText   = RGB(255,255,255);
                      lplvcd->clrTextBk = RGB(240,55,23);
                      return CDRF_NEWFONT;
                    }
                    break;
                    
                    case 1:
                    {
                      lplvcd->clrText   = RGB(255,255,0);
                      lplvcd->clrTextBk = RGB(0,0,0);
                      return CDRF_NEWFONT;
                    }
                    break;  

                    case 2:
                    {
                      lplvcd->clrText   = RGB(20,26,158);
                      lplvcd->clrTextBk = RGB(200,200,10);
                      return CDRF_NEWFONT;
                    }
                    break;

                    case 3:
                    {
                      lplvcd->clrText   = RGB(12,15,46);
                      lplvcd->clrTextBk = RGB(200,200,200);
                      return CDRF_NEWFONT;
                    }
                    break;

                    case 4:
                    {
                      lplvcd->clrText   = RGB(120,0,128);
                      lplvcd->clrTextBk = RGB(20,200,200);
                      return CDRF_NEWFONT;
                    }
                    break;

                    case 5:
                    {
                      lplvcd->clrText   = RGB(255,255,255);
                      lplvcd->clrTextBk = RGB(0,0,150);
                      return CDRF_NEWFONT;
                    }
                    break;

                }
 
            }
    }
    return CDRF_DODEFAULT;
}
