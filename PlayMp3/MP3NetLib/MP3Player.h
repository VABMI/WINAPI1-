#pragma once

#include "..\\LibMP3\\mp3.h"

#ifdef _DEBUG
	#pragma comment(lib, "..\\Debug\\LibMP3.lib")
#else
	#pragma comment(lib, "..\\Release\\LibMP3.lib")
#endif

namespace MP3NetLib
{


public ref class MP3Player
{
public:
	MP3Player(void);
	~MP3Player(void);

	System::Boolean Load(System::String^ filename);
	void Cleanup();

	System::Boolean Play();
	System::Boolean Pause();
	System::Boolean Stop();

	// Poll this function with msTimeout = 0, so that it return immediately.
	// If the mp3 finished playing, WaitForCompletion will return true;
	System::Boolean WaitForCompletion(System::Int32 msTimeout, System::Int32% EvCode);

	// -10000 is lowest volume and 0 is highest volume, positive value > 0 will fail
	System::Boolean SetVolume(System::Int32 vol);

	// -10000 is lowest volume and 0 is highest volume
	System::Int32 GetVolume();

	// Returns the duration in 1/10 millionth of a second,
	// meaning 10,000,000 == 1 second
	// You have to divide the result by 10,000,000 
	// to get the duration in seconds.
	System::Int64 GetDuration();

	// Returns the current playing position
	// in 1/10 millionth of a second,
	// meaning 10,000,000 == 1 second
	// You have to divide the result by 10,000,000 
	// to get the duration in seconds.
	System::Int64 GetCurrentPosition();

	// Seek to position
	System::Boolean SetPositions(System::Int64% pCurrent,System::Int64% pStop, System::Boolean bAbsolutePositioning);

private:
	Mp3* player;

};

} // ns MP3NetLib