	pp=&datesaati;
			

			if(wparam==combo)
	  {  	
		MessageBox(hwnd,"ssa","ssa",1);
	  }


			
				SendMessage(comb,WM_GETTEXT,80,(LPARAM)ssa);	 
				if(strlen(ssa)>=3)
				{
					
		 			if(strlen(ssa)==strlen("BACKU")&&strlen(bufferCity)!=strlen(ssa))
						{	
							MessageBox(hwnd,ssa,ssa,1);
							*pp=3;
							sprintf(city,"%s"," ");
							strcat(city,"BACKU");

							sprintf(bufferCity,"%s","");
							strcat(bufferCity,"BACKU");

						}
					else 	if(strlen(ssa)==strlen("TBILISI")&&strlen(bufferCity)!=strlen(ssa))
						{	
							MessageBox(hwnd,ssa,ssa,1);
							*pp=4;
								sprintf(city,"%s"," ");
							strcat(city,"TBILISI");


							sprintf(bufferCity,"%s","");
							strcat(bufferCity,"TBILISI");

						}
					else 	if(strlen(ssa)==strlen("LONDON")&&strlen(bufferCity)!=strlen(ssa))
						{	
							MessageBox(hwnd,ssa,ssa,1);
							*pp=1;
							sprintf(city,"%s"," ");
							strcat(city,"LONDON");


							sprintf(bufferCity,"%s","");
							strcat(bufferCity,"LONDON");
						}
						

					//	MessageBox(hwnd,"garetaa","garetaa",1);
				  // SendMessage(ff,WM_GETTEXT,80,(LPARAM)ssa);
				
				}

				
