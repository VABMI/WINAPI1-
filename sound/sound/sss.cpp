




#include <iostream>
#include <windows.h>
#include <string>
#include <fstream>
#include <mmsystem.h>
#pragma comment (lib, "winmm.lib")
using namespace std;

int main(){while ( 1 ){
	long int A;
	cout << "\t0 = EXIT\n"
			"\t1 = STOP\n"
			"\t2 = PAUSE\n"
			"\t3 = PLAY\n"; 
	cin >> A;
	system ("CLS");
	if (A == 0){ exit(EXIT_SUCCESS); }
	if (A == 1){ PlaySound(0, 0, 0); cout << "To restart music: ";system ("PAUSE"); }
	if (A == 2){mciSendString("pause MY_SND", NULL, 0, NULL); system("pause");}
	if (A == 3){	PlaySound(TEXT("C:\\Users\\vaxoa\\OneDrive\\Desktop\\as.wav"), NULL, SND_ASYNC|SND_FILENAME|SND_LOOP|SND_NOWAIT);
 }
	}
}




/*
#include <iostream>
#include <windows.h>
#include <mmsystem.h>


using namespace std;


int main()
{
	
	
	PlaySound(TEXT("C:\\Users\\vaxoa\\OneDrive\\Desktop\\as.wav"),NULL,SND_SYNC);
	
	return 0;
}
*/







/*

#include <conio.h>

#include "inc/fmod.h"

FSOUND_SAMPLE* handle;

int main ()
{
   // init FMOD sound system
   FSOUND_Init (44100, 32, 0);

   // load and play mp3
   handle=FSOUND_Sample_Load (0,"C:\\Users\\vaxoa\\OneDrive\\Desktop\\STING  CHEB MAMI - DESERT ROSE.mp3",0, 0, 0);
   FSOUND_PlaySound (0,handle);

   // wait until the users hits a key to end the app
   while (!_kbhit())
   {
   }

   // clean up
   FSOUND_Sample_Free (handle);
   FSOUND_Close();
}

*/