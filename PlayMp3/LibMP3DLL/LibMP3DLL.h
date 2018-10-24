
#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers
#include <windows.h>

#ifndef LIBMP3
	#define LIBMP3_API __declspec(dllimport)
#else
	#define LIBMP3_API __declspec(dllexport)
#endif

extern "C" bool LIBMP3_API __stdcall Load(LPCWSTR filename);
extern "C" void LIBMP3_API __stdcall Cleanup();

extern "C" bool LIBMP3_API __stdcall Play();
extern "C" bool LIBMP3_API __stdcall Pause();
extern "C" bool LIBMP3_API __stdcall Stop();

extern "C" bool LIBMP3_API __stdcall WaitForCompletion(long msTimeout, long* EvCode);

extern "C" bool LIBMP3_API __stdcall SetVolume(long vol);

extern "C" long LIBMP3_API __stdcall GetVolume();

extern "C" __int64 LIBMP3_API __stdcall GetDuration();

extern "C" __int64 LIBMP3_API __stdcall GetCurrentPosition();

extern "C" bool LIBMP3_API __stdcall SetPositions(__int64* pCurrent, __int64* pStop, bool bAbsolutePositioning);
