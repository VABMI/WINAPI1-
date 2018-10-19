/*if(message==findmsg)
{
	
		
	LPFINDREPLACE lpfr =(LPFINDREPLACE) lparam;

		static	char bufs2[100];
		    char bufs[100];
			
////////////////////////// finde Text funqciis struqturidan wchar_t to lpcstr /////////////////////////////////////
			////////////////////////////////// tu sheyvanilia igive striqonii magis logikaa /////////////////////////
			if(vb)
			{
				for(int i=0;i<6;i++)
				{
			
				bufs[i]=szFindWhat[i];
			
				}
				vb=0;
					sechwat=bufs;
					sechwatPev=sechwat;
			}
			else 
		{	
			for(int i=0;i<6;i++)
				{
			
				bufs[i]=szFindWhat[i];
			
				}sechwat=bufs;

			}		
	////////////////////////////////////////////////////////////
	
	/////////////////////////// dziebaaaaaaaaa masivshi ////////////////////////////////////////////

	static char  bufg[10000]; //////////////// enacvlebian ertmanets mashi rodesac sxvadasxva sadziebo sityva shedis fanjris gautishvelad 
	static char bufg1[10000]; // anu tu chavwer raime sadziebo sityvas da mere sxva sadziebo sityvas 
    UINT ff;
	  

		if(lpfr->Flags&FR_FINDNEXT)
		{
	
			


			if(delet12)
				 {
					  SendMessage(EDITGLOBAL,WM_GETTEXT,999,(LPARAM)bufg);
					  sprintf(bufg1,"%s",bufg);
					  delet12=0;
				 }
			static bool nm=0;
			static bool nm2=0;
			////////////////////////////////////////////////// if IIIIIIIIIIIII
			//if(strlen1==strlen){
			for(int i=0;i<strlen(bufs);i++)
			{
				if(bufs[i]!=bufs2[i])
				{
					nm2=0;

				}
				else nm2=1;


			}
			

			if(sechwat==sechwatPev&&nm==0)
			{
		 
	
				const char *position1=strstr(bufg,sechwat);
				
				ff=(position1-bufg);

		/////////////////////// monishvna //////////////////

				SendMessage(EDITGLOBAL,EM_SETSEL,ff,ff+strlen(sechwat));
		
				if(position1)
				{
					for(int i=ff;i<ff+strlen(sechwat);i++)
					{

						bufg[i]=(char)(-52);

					}

				}

				else 
				{

					
					sprintf(bufg,"%s",bufg1);

				}


			}
			else if(nm==0)
			{
				nm=1;

				sechwatPev=sechwat;
				sprintf(bufg,"%s",bufg1);
			}

			//////////////////////////////// if II_II_II_II_II_
			else if(sechwatPev==sechwat&&nm)
			{

				
				const char *position2=strstr(bufg1,sechwat);
		
		
				ff=(position2-bufg1);

	



		/////////////////////// monishvna //////////////////

				SendMessage(EDITGLOBAL,EM_SETSEL,ff,ff+strlen(sechwat));
		
					if(position2)
				{
						 for(int i=ff;i<ff+strlen(sechwat);i++)
						{

							bufg1[i]=(char)(-52);

						}
				}

			} 
			else if(nm) {
				nm=0;

				sechwatPev=sechwat;
			
				sprintf(bufg1,"%s",bufg);
			}


			

		}
		else delet12=1;






}


*/