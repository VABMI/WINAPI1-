
//---------------------------------------------
UINT create_menu(HWND hwnd)
{
HMENU hmenu=CreateMenu();
	if(!hmenu)
	return GetLastError();

HMENU hmenu_popup_file=CreatePopupMenu();
AppendMenu(hmenu,MF_POPUP,(UINT_PTR)hmenu_popup_file,"&File");
AppendMenu(hmenu_popup_file,MF_STRING,100,"&ABCD");
AppendMenu(hmenu_popup_file,MF_STRING,200,"&2");

HMENU hmenu_popup_options=CreatePopupMenu();
AppendMenu(hmenu, MF_POPUP, (UINT_PTR)hmenu_popup_options, "&Options");
AppendMenu(hmenu_popup_options,MF_STRING,300,"&3");
AppendMenu(hmenu_popup_options,MF_STRING,400,"&4");
SetMenu(hwnd,hmenu);
}
/*

void loadimg(){
	hlogoimg = (HBITMAP)LoadImageW(NULL,L"flag.bmp",IMAGE_BITMAP,0,0,LR_LOADFROMFILE);
}

*/
void open_file(HWND hwnd){
//OPENFILENAME ofn;
 
char filename[MAX_PATH];

	OPENFILENAME ofn;
	ZeroMemory(&filename, sizeof(filename));
	ZeroMemory(&ofn, sizeof(ofn));
	ofn.lStructSize = sizeof(ofn);
	ofn.hwndOwner = NULL;  // If you have a window to center over, put its HANDLE here
	ofn.lpstrFilter = "txt Text Files\0*.txt\0All File\0*.*\0";
	ofn.lpstrFile = filename;
	ofn.nMaxFile = MAX_PATH;
	ofn.lpstrTitle = "Select a File";
	ofn.Flags =  OFN_FILEMUSTEXIST;

	if (GetOpenFileNameA(&ofn))
	{
		std::cout << "You chose the file \"" << filename << "\"\n" << "\"\n";
	}

MessageBox(NULL,filename,"",MB_OK);

FILE *file;
file=fopen(filename,"rb");
fseek(file,0,SEEK_END);
int _size=ftell(file);
rewind(file);
char *data=(char*)malloc(_size+1);//new char(_size+1);
fread(data,_size,1,file);
data[_size]='\0';



MessageBox(0,data,0,0);
static const TCHAR Hex[] = TEXT("0123456789ABCDEF");
LPTSTR pszFileText = (LPTSTR) LocalAlloc(LMEM_FIXED, ((_size * 3) + 1) * sizeof(TCHAR));
DWORD dwOffset = 0;

for (DWORD idx = 0; idx < _size; ++idx)
                    {
                        pszFileText[dwOffset++] = Hex[(data[idx] & 0xF0) >> 4];
                        pszFileText[dwOffset++] = Hex[data[idx] & 0x0F];
                        pszFileText[dwOffset++] = TEXT(' ');
                    }
SetWindowText(hte,pszFileText);
free(data);//delete[] data;
fclose(file);

}


long __stdcall on_create(HWND hwnd,unsigned int message
					, unsigned int wparam,long lparam)
{


create_menu(hwnd);



HWND hw=0;
int X,Y,W,H;
DWORD style=WS_VISIBLE|WS_CHILD|WS_BORDER;


//hlbl = CreateWindowW(L"Static", L"0", style, 
 //       270, 250, 30, 30, hwnd, (HMENU)3, NULL, NULL);

hTrack  = CreateWindowW(TRACKBAR_CLASSW, L"Trackbar Control",WS_CHILD | WS_VISIBLE | TBS_AUTOTICKS,
        20, 250, 170, 30, hwnd, (HMENU) 3, NULL, NULL);

X=10;Y=30;W=100;H=100;
hw=CreateWindow("button","button",style,X,Y,W,H,hwnd,(HMENU)10,0,0);


X=150;Y=30;W=400;H=40;
CreateWindowW(TRACKBAR_CLASSW, L"Trackbar Control",WS_CHILD | WS_VISIBLE | TBS_AUTOTICKS,
        X, Y, W, H, hwnd, (HMENU) 31, NULL, NULL);

//hw=CreateWindow("listbox","listbox",style,X,Y,W,H,hwnd,(HMENU)20,0,0);


X=300;Y=30;W=100;H=100;
//hw=CreateWindow("combobox","combobox",style,X,Y,W,H,hwnd,(HMENU)30,0,0);



X=450;Y=30;W=100;H=100;
//style=WS_VISIBLE|WS_CHILD|SS_BITMAP;
//hlogo=CreateWindow("static","static",WS_VISIBLE|WS_CHILD|SS_BITMAP,X,Y,W,H,hwnd,(HMENU)38,0,0);
//SendMessageW(hlogo,STM_SETIMAGE,IMAGE_BITMAP,(LPARAM)hLOGO_IMG);

style=WS_VISIBLE|WS_CHILD|WS_BORDER|WS_VSCROLL|WS_HSCROLL|ES_MULTILINE|ES_AUTOHSCROLL|ES_AUTOVSCROLL;
X=200;Y=100;W=400;H=100;
//hte=CreateWindow("edit","edit",style,X,Y,W,H,hwnd,(HMENU)50,0,0);
hte=CreateWindowW(L"edit",L"",style,X,Y,W,H,hwnd,NULL,NULL,NULL);

style=WS_VISIBLE|WS_OVERLAPPEDWINDOW|WS_CLIPCHILDREN;
X=1310;Y=230;W=200;H=100;
//CreateWindow("1223","Main",style,X,Y,W,H,0,0,0,0);

return 0;
}
//void UpdateLabel(HWND hwnd,unsigned int wParam) 
void UpdateLabel(HWND hwnd){
	HDC hdc=GetDC(hwnd);
	
    float pos = SendMessageW(hTrack, TBM_GETPOS, 0, 0);

	
	//for(int i=10;i<100;i++)
	SetPixel(hdc, pos,10, RGB(0,0,0));

	SetPixel(hdc, pos + 300,sin (pos*0.1)*9+430, RGB(0,0,0));

    wchar_t buf[4];
    wsprintfW(buf, L"%ld", pos);

    SetWindowTextW(hlbl, buf);
	





}