DWORD editstyle=WS_VISIBLE|WS_CHILD|WS_BORDER|ES_MULTILINE|ES_NOHIDESEL|WS_OVERLAPPED|WS_VSCROLL;
static int butc=50001;
static bool bn=false;
static int cbutc=30001;
static int editc=70001;
HWND HwndGlob,tolbar,StatusBar; 
HWND EDITGLOBAL,childButton;

ULONG edittextferi;
ULONG textlineferi;
ULONG bckferi;
static bool gn=1;
HFONT hfont_glob;
CHOOSEFONT cf_glob;
LOGFONT lf_glob;
ULONG rgb_glob=0;
char path[100];
static int size;
wchar_t szFindWhat[100]=L"drtert";  
char szFindWhat2[100]="drtert"; 

char ReplaceWith[100]="ReplaceWith"; 

bool vb=1;


