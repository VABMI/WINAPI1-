#pragma once
class CLibMP3DLL
{
public:
	CLibMP3DLL(void);
	~CLibMP3DLL(void);

	bool LoadDLL(LPCWSTR dll);
	void UnloadDLL();

	bool Load(LPCWSTR filename);
	bool Cleanup();

	bool Play();
	bool Pause();
	bool Stop();
	bool WaitForCompletion(long msTimeout, long* EvCode);

	bool SetVolume(long vol);
	long GetVolume();

	__int64 GetDuration();
	__int64 GetCurrentPosition();

	// Seek to position
	bool SetPositions(__int64* pCurrent, __int64* pStop, bool bAbsolutePositioning);

private:
	HMODULE m_Mod;
};

