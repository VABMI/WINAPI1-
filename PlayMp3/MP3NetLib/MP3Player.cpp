#include "StdAfx.h"
#include "MP3Player.h"

namespace MP3NetLib
{

MP3Player::MP3Player(void)
{
	player = new Mp3();
}

MP3Player::~MP3Player(void)
{
	if(player!=NULL)
	{
		delete player;
		player = NULL;
	}
}

System::Boolean MP3Player::Load(System::String^ filename)
{
	if(player==NULL)
		return false;

	System::IntPtr ptr = System::Runtime::InteropServices::Marshal::StringToHGlobalUni(filename);

	System::Boolean b = player->Load((LPCWSTR)ptr.ToPointer());

	System::Runtime::InteropServices::Marshal::FreeHGlobal(ptr);

	return b;
}

void MP3Player::Cleanup()
{
	if(player==NULL)
		return;

	player->Cleanup();
}

System::Boolean MP3Player::Play()
{
	if(player==NULL)
		return false;

	return player->Play();
}

System::Boolean MP3Player::Pause()
{
	if(player==NULL)
		return false;

	return player->Pause();
}

System::Boolean MP3Player::Stop()
{
	if(player==NULL)
		return false;

	return player->Stop();
}

System::Boolean MP3Player::WaitForCompletion(System::Int32 msTimeout, System::Int32% EvCode)
{
	if(player==NULL)
		return true;

	long evCode = 0;
	System::Boolean b = player->WaitForCompletion(msTimeout, &evCode);

	EvCode = evCode;

	return b;
}

System::Boolean MP3Player::SetVolume(System::Int32 vol)
{
	if(player==NULL)
		return false;

	return player->SetVolume(vol);
}

System::Int32 MP3Player::GetVolume()
{
	if(player==NULL)
		return -1;

	return player->GetVolume();
}

System::Int64 MP3Player::GetDuration()
{
	if(player==NULL)
		return -1;

	return player->GetDuration();
}

System::Int64 MP3Player::GetCurrentPosition()
{
	if(player==NULL)
		return -1;

	return player->GetCurrentPosition();
}

System::Boolean MP3Player::SetPositions(System::Int64% pCurrent, System::Int64% pStop, System::Boolean bAbsolutePositioning)
{
	if(player==NULL)
		return false;

	__int64 pCurrentTemp = pCurrent; 
	__int64 pStopTemp = pStop;
	bool b = player->SetPositions(&pCurrentTemp, &pStopTemp, bAbsolutePositioning);

	pCurrent = pCurrentTemp; 
	pStop = pStopTemp;

	return b;
}

}