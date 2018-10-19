


void ru (char *cha,int *zy,int *nh)
{
int	M=atoi(cha);
*zy=*zy-(10+M);
*nh=*nh+(10+M);

}



long __stdcall on_cmd(HWND hwnd,unsigned int message
					,unsigned int wparam,long lparam)

{


int ctrl_id = (unsigned short)wparam;

char s1[100],s2[100];




if(ctrl_id==3)
{ int X=200,Y=390,W=30,H=200,PLUS=90;  
  int Y2=595,H2=5,M;
/////////////////////// 1 /////////////////////////

	HWND st1=GetDlgItem(hwnd,61);
	HWND ed1=GetDlgItem(hwnd,401);


	SendMessage(ed1,WM_GETTEXT,99,(LPARAM)s1);
	M=atoi(s1);
	Y2=Y2-(25+M);H2=H2+(25+M);
	//ru(s1,&Y2,&H2);
	SetWindowPos(st1 ,0,X,Y2,W,H2,0);
///////////////////////// 2 /////////////////////////
	X=X+PLUS;
	HWND ed2=GetDlgItem(hwnd,402);	
	HWND st2=GetDlgItem(hwnd,62);
	
	SendMessage(ed2,WM_GETTEXT,99,(LPARAM)s2);
	ru(s2,&Y2,&H2);
	SetWindowPos(st2 ,0,X,Y2,W,H2,0);
	////////////////////  3 //////////////////////////
	X=X+PLUS;


	HWND ed3=GetDlgItem(hwnd,403);	
	HWND st3=GetDlgItem(hwnd,63);
	
	SendMessage(ed3,WM_GETTEXT,99,(LPARAM)s1);
	ru(s1,&Y2,&H2);
	SetWindowPos(st3 ,0,X,Y2,W,H2,0);
////////////////////// 4 //////////////////////////

	X=X+PLUS;
	HWND ed4=GetDlgItem(hwnd,404);	
	HWND st4=GetDlgItem(hwnd,64);
	
	SendMessage(ed4,WM_GETTEXT,99,(LPARAM)s1);
	ru(s1,&Y2,&H2);
	SetWindowPos(st4 ,0,X,Y2,W,H2,0);
////////////////////// 5 //////////////////////////
	X=X+PLUS;

	HWND ed5=GetDlgItem(hwnd,405);	
	HWND st5=GetDlgItem(hwnd,65);
	
	SendMessage(ed5,WM_GETTEXT,99,(LPARAM)s1);
	ru(s1,&Y2,&H2);
	SetWindowPos(st5 ,0,X,Y2,W,H2,0);
///////////////////// 6 /////////////////////////////
	X=X+PLUS;
	HWND ed6=GetDlgItem(hwnd,406);	
	HWND st6=GetDlgItem(hwnd,66);
	
	SendMessage(ed6,WM_GETTEXT,99,(long)s1);
	ru(s1,&Y2,&H2);
	SetWindowPos(st6,0,X,Y2,W,H2,0);
//////////////// 7 //////////////////////////////
		X=X+PLUS;
	HWND ed7=GetDlgItem(hwnd,407);	
	HWND st7=GetDlgItem(hwnd,67);
	
	SendMessage(ed7,WM_GETTEXT,99,(LPARAM)s1);
	ru(s1,&Y2,&H2);
	SetWindowPos(st7 ,0,X,Y2,W,H2,0);


///////////////////// 8 //////////////////////
		X=X+PLUS;
	HWND ed8=GetDlgItem(hwnd,408);	
	HWND st8=GetDlgItem(hwnd,68);
	
	SendMessage(ed8,WM_GETTEXT,99,(LPARAM)s1);
	ru(s1,&Y2,&H2);
	SetWindowPos(st8 ,0,X,Y2,W,H2,0);






	int xx,yy;
	

}





	if(ctrl_id==200)
	{

	//SendMessage(hwnd,WM_SETFONT,(UINT)hfont,0);
	}

	else if(ctrl_id==10)
	{
	WinExec("calc.exe",1);
	HWND h=0;
	char str[500];
	h=GetDlgItem(hwnd,50);
	SendMessage(h,WM_GETTEXT,500,(long)str);
	strcat(str,"1");
	SendMessage(h,WM_SETTEXT,0,(long)str);
	
	h=GetDlgItem(hwnd,20);
	SendMessage(h,LB_DELETESTRING,0,0);

	h=GetDlgItem(hwnd,30);
	SendMessage(h,CB_DELETESTRING,0,0);

	}
return 0;
}