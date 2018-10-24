#include "StdAfx.h"
#include "LibMP3DLL.h"

typedef bool (__stdcall *StrParamFuncType)(LPCWSTR dll); 
typedef void (__stdcall *VoidFuncType)(); 
typedef bool (__stdcall *BoolFuncType)(); 
typedef bool (__stdcall *WaitForCompletionFuncType)(long msTimeout, long* EvCode);
typedef bool (__stdcall *SetVolumeFuncType)(long vol);
typedef long (__stdcall *GetVolumeFuncType)();
typedef __int64 (__stdcall *GetDurationFuncType)();
typedef __int64 (__stdcall *GetCurrentPositionFuncType)();
typedef bool (__stdcall *SetPositionsFuncType)(__int64* pCurrent, __int64* pStop, bool bAbsolutePositioning);


StrParamFuncType LoadFunc = NULL;
VoidFuncType CleanupFunc = NULL;
BoolFuncType PlayFunc = NULL;
BoolFuncType PauseFunc = NULL;
BoolFuncType StopFunc = NULL;
WaitForCompletionFuncType WaitForCompletionFunc = NULL;

SetVolumeFuncType SetVolumeFunc = NULL;
GetVolumeFuncType GetVolumeFunc = NULL;

GetDurationFuncType GetDurationFunc = NULL;
GetCurrentPositionFuncType GetCurrentPositionFunc = NULL;
SetPositionsFuncType SetPositionsFunc=NULL;

CLibMP3DLL::CLibMP3DLL(void)
	:
	m_Mod(NULL)
{
}


CLibMP3DLL::~CLibMP3DLL(void)
{
	UnloadDLL();
}

bool CLibMP3DLL::LoadDLL(LPCWSTR dll)
{
	m_Mod = LoadLibrary(dll);

	if(m_Mod)
	{
		LoadFunc = (StrParamFuncType)(GetProcAddress(m_Mod, "Load"));
		CleanupFunc = (VoidFuncType)(GetProcAddress(m_Mod, "Cleanup"));
		PlayFunc = (BoolFuncType)(GetProcAddress(m_Mod, "Play"));
		PauseFunc = (BoolFuncType)(GetProcAddress(m_Mod, "Pause"));
		StopFunc = (BoolFuncType)(GetProcAddress(m_Mod, "Stop"));
		WaitForCompletionFunc = (WaitForCompletionFuncType)(GetProcAddress(m_Mod, "WaitForCompletion"));

		SetVolumeFunc = (SetVolumeFuncType)(GetProcAddress(m_Mod, "SetVolume"));
		GetVolumeFunc = (GetVolumeFuncType)(GetProcAddress(m_Mod, "GetVolume"));

		GetDurationFunc = (GetDurationFuncType)(GetProcAddress(m_Mod, "GetDuration"));
		GetCurrentPositionFunc = (GetCurrentPositionFuncType)(GetProcAddress(m_Mod, "GetCurrentPosition"));
		SetPositionsFunc = (SetPositionsFuncType)(GetProcAddress(m_Mod, "SetPositions"));
		return true;
	}

		return false;
}

void CLibMP3DLL::UnloadDLL()
{
	if(m_Mod)
	{
		FreeLibrary(m_Mod);
	}

	LoadFunc = NULL;
	CleanupFunc = NULL;
	PlayFunc = NULL;
	PauseFunc = NULL;
	StopFunc = NULL;
	WaitForCompletionFunc = NULL;

	SetVolumeFunc = NULL;
	GetVolumeFunc = NULL;

	GetDurationFunc = NULL;
	GetCurrentPositionFunc = NULL;

	m_Mod = NULL;
}

bool CLibMP3DLL::Load(LPCWSTR filename)
{
	if(LoadFunc)
	{
		return LoadFunc(filename);
	}

	return false;
}

bool CLibMP3DLL::Cleanup()
{
	if(CleanupFunc)
	{
		CleanupFunc();
		return true;
	}

	return false;
}

bool CLibMP3DLL::Play()
{
	if(PlayFunc)
	{
		return PlayFunc();
	}

	return false;
}

bool CLibMP3DLL::Pause()
{
	if(PauseFunc)
	{
		return PauseFunc();
	}

	return false;
}

bool CLibMP3DLL::Stop()
{
	if(StopFunc)
	{
		return StopFunc();
	}

	return false;
}

bool CLibMP3DLL::WaitForCompletion(long msTimeout, long* EvCode)
{
	if(WaitForCompletionFunc)
	{
		return WaitForCompletionFunc(msTimeout, EvCode);
	}
	return true;
}

bool CLibMP3DLL::SetVolume(long vol)
{
	if(SetVolumeFunc)
	{
		return SetVolumeFunc(vol);
	}

	return false;
}

long CLibMP3DLL::GetVolume()
{
	if(GetVolumeFunc)
	{
		return GetVolumeFunc();
	}
	return -1;
}

__int64 CLibMP3DLL::GetDuration()
{
	if(GetDurationFunc)
	{
		return GetDurationFunc();
	}
	return -1;
}

__int64 CLibMP3DLL::GetCurrentPosition()
{
	if(GetCurrentPositionFunc)
	{
		return GetCurrentPositionFunc();
	}
	return -1;
}

bool CLibMP3DLL::SetPositions(__int64* pCurrent, __int64* pStop, bool bAbsolutePositioning)
{
	if(SetPositionsFunc)
	{
		return SetPositionsFunc(pCurrent, pStop, bAbsolutePositioning);
	}
	return false;
}
