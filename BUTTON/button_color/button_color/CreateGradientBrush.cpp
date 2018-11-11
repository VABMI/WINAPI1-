
HBRUSH CreateGradientBrush(COLORREF top, COLORREF bottom, LPNMCUSTOMDRAW item)
    {
        HBRUSH Brush = NULL;
        HDC hdcmem = CreateCompatibleDC(item->hdc);
        HBITMAP hbitmap = CreateCompatibleBitmap(item->hdc, item->rc.right-item->rc.left, item->rc.bottom-item->rc.top);
        SelectObject(hdcmem, hbitmap);

        int r1 = GetRValue(top), r2 = GetRValue(bottom), g1 = GetGValue(top), g2 = GetGValue(bottom), b1 = GetBValue(top), b2 = GetBValue(bottom);
        for(int i = 0; i < item->rc.bottom-item->rc.top; i++)
        { 
            RECT temp;
            int r,g,b;
            r = int(r1 + double(i * (r2-r1) / item->rc.bottom-item->rc.top));
            g = int(g1 + double(i * (g2-g1) / item->rc.bottom-item->rc.top));
            b = int(b1 + double(i * (b2-b1) / item->rc.bottom-item->rc.top));
            Brush = CreateSolidBrush(RGB(r, g, b));
            temp.left = 0;
            temp.top = i;
            temp.right = item->rc.right-item->rc.left;
            temp.bottom = i + 1; 
            FillRect(hdcmem, &temp, Brush);
            DeleteObject(Brush);
        }
        HBRUSH pattern = CreatePatternBrush(hbitmap);

        DeleteDC(hdcmem);
        DeleteObject(Brush);
        DeleteObject(hbitmap);

        return pattern;
    }
