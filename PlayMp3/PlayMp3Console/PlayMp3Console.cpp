// PlayMp3Console.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <string>
#include <sstream>
#include "../LibMP3/Mp3.h"

#ifdef _DEBUG
	#pragma comment(lib, "../Debug/LibMP3.lib")
#else
	#pragma comment(lib, "../Release/LibMP3.lib")
#endif 

using namespace std;

#define SV_LOADED 1
#define SV_PLAYING 2
#define SV_STOPPED 3
#define SV_PAUSED 4

class com_init
{
public:
	com_init()
	{
		::CoInitialize(NULL);
	}

	~com_init()
	{
		::CoUninitialize();
	}
};

int _tmain(int argc, _TCHAR* argv[])
{
	com_init init;

	wcout<<L"Enter the MP3 path: ";
	std::wstring path;
	getline(wcin, path);

	wcout<<path<<endl;

	Mp3 mp3;

	int status = 0;
	if(mp3.Load(path.c_str()))
	{
		status = SV_LOADED;
	}
	else
	{
		wcout<<L"File cannot be loaded! Program exited!";
		return 0;
	}

	if(mp3.Play())
	{
		status = SV_PLAYING;
	}
	else
	{
		wcout<<L"File cannot be played! Program exited!";
		return 0;
	}

	wcout<<L"Enter s to stop or p to pause or q to quit: ";

	while(true)
	{
		LONG evCode=0;
		if(mp3.WaitForCompletion(0, &evCode))
		{
			wcout<<L"Playing has stopped! Program exited!";
			return 0;
		}

		std::wstring action;
		wcin >> action;

		if(action==L"q"||action==L"Q")
		{
			if(status==SV_PLAYING)
				mp3.Stop();

			wcout<<L"Program quitting!";
			return 0;
		}
		else if(status==SV_PLAYING&&(action==L"s"||action==L"S"))
		{
			// Stop method do not stop the mp3, it merely pause it!
			if(mp3.Stop())
			{
				status = SV_STOPPED;
				wcout<<L"Playing is stopped!"<<endl;
				wcout<<L"Enter l to play: ";
			}
			else
			{
				wcout<<L"File cannot be stopped!";
			}
		}
		else if(status==SV_PLAYING&&(action==L"p"||action==L"P"))
		{
			if(mp3.Pause())
			{
				status = SV_PAUSED;
				wcout<<L"Playing is paused!"<<endl;
				wcout<<L"Enter r to resume: ";
			}
			else
			{
				wcout<<L"File cannot be paused!";
			}
		}
		else if(status==SV_PAUSED&&(action==L"r"||action==L"R"))
		{
			if(mp3.Play())
			{
				status = SV_PLAYING;
				wcout<<L"Playing is resumed!"<<endl;
				wcout<<L"Enter s to stop or p to pause: ";
			}
			else
			{
				wcout<<L"File cannot be resumed!";
			}
		}
		else if(status==SV_STOPPED&&(action==L"l"||action==L"L"))
		{
			if(mp3.Play())
			{
				status = SV_PLAYING;
				wcout<<L"Playing is restarted!"<<endl;
				wcout<<L"Enter s to stop or p to pause: ";
			}
			else
			{
				wcout<<L"File cannot be restarted!";
			}
		}
		else
		{
			wcout<<L"You have entered a invalid key!";

		}

	}
	return 0;
}

