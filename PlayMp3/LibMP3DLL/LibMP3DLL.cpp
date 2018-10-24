// LibMP3DLL.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "LibMP3DLL.h"
#include "Mp3.h"

Mp3* g_pMp3;

extern "C" bool LIBMP3_API __stdcall Load(LPCWSTR filename)
{
	if(g_pMp3)
	{
		delete g_pMp3;
		g_pMp3 = NULL;
	}
	g_pMp3 = new Mp3();
	if(g_pMp3)
		return g_pMp3->Load(filename);

	return false;
}

extern "C" void LIBMP3_API __stdcall Cleanup()
{
	if(g_pMp3)
	{
		g_pMp3->Cleanup();
		delete g_pMp3;
		g_pMp3 = NULL;
	}
}

extern "C" bool LIBMP3_API __stdcall Play()
{
	if(g_pMp3)
		return g_pMp3->Play();

	return false;
}

extern "C" bool LIBMP3_API __stdcall Pause()
{
	if(g_pMp3)
		return g_pMp3->Pause();

	return false;
}

extern "C" bool LIBMP3_API __stdcall Stop()
{
	if(g_pMp3)
		return g_pMp3->Stop();

	return false;
}

extern "C" bool LIBMP3_API __stdcall WaitForCompletion(long msTimeout, long* EvCode)
{
	if(g_pMp3)
		return g_pMp3->WaitForCompletion(msTimeout, EvCode);

	return true;
}

extern "C" bool LIBMP3_API __stdcall SetVolume(long vol)
{
	if(g_pMp3)
		return g_pMp3->SetVolume(vol);

	return false;
}

extern "C" long LIBMP3_API __stdcall GetVolume()
{
	if(g_pMp3)
		return g_pMp3->GetVolume();

	return -1;
}

extern "C" __int64 LIBMP3_API __stdcall GetDuration()
{
	if(g_pMp3)
		return g_pMp3->GetDuration();

	return -1;
}

extern "C" __int64 LIBMP3_API __stdcall GetCurrentPosition()
{
	if(g_pMp3)
		return g_pMp3->GetCurrentPosition();

	return -1;
}

extern "C" bool LIBMP3_API __stdcall SetPositions(__int64* pCurrent, __int64* pStop, bool bAbsolutePositioning)
{
	if(g_pMp3)
		return g_pMp3->SetPositions(pCurrent, pStop, bAbsolutePositioning);

	return false;
}
