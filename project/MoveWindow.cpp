
				MoveWindow(GetDlgItem(hwnd,1233),r.right-80,0,70,70,1);
				MoveWindow(GetDlgItem(hwnd,1234),r.right-160,0,70,70,1);
				MoveWindow(GetDlgItem(hwnd,1235),r.right-(160+80),0,70,70,1);
				MoveWindow(GetDlgItem(hwnd,1236),r.right-(160+80+80),0,70,70,1);
				for(int y=1233;y<=1236;y++)
				{


					InvalidateRect(GetDlgItem(hwnd,y),0,1);
				}

						MoveWindow(GetDlgItem(hwnd,1231),0,70,r.right,3,1);