#include <Windows.h>
#include <iostream>
#include <sstream>
#include <vector>
#include <memory>
#include <stdlib.h>
#include <time.h>

using namespace std;

class Process
{
public:
	typedef void (*ProcessExitCallback)(Process const*);

	Process() : 
		hProcess(NULL), 
		hThread(NULL),
		hWait(NULL),
		exitCallback(NULL)
	{
	}

	~Process()
	{
		if (hThread) CloseHandle(hThread);

		if (hWait) 
		{
			::UnregisterWaitEx(hWait, INVALID_HANDLE_VALUE); // INVALID_HANDLE_VALUE means "Wait for pending callbacks"
			hWait = NULL;
		}

		if (hProcess)
		{
			DWORD code;
			if (GetExitCodeProcess(hProcess, &code))
			{
				if (code == STILL_ACTIVE) TerminateProcess(hProcess, 1);
			}

			CloseHandle(hProcess);
		}
	}

	DWORD getId() const { return id; }
	HANDLE getHandle() const { return hProcess; }

	bool CreateSuspended(int sleepTime)
	{
		ostringstream cmdLine;
		cmdLine << "TestApp.exe " << sleepTime;

		STARTUPINFOA si = { sizeof(STARTUPINFOA) };
		PROCESS_INFORMATION pi;
		BOOL bProcessCreated = CreateProcessA(NULL, (char*)cmdLine.str().c_str(), NULL, NULL, FALSE, DETACHED_PROCESS | CREATE_SUSPENDED, NULL, NULL, &si, &pi);

		if (bProcessCreated)
		{
			hProcess = pi.hProcess;
			hThread = pi.hThread;
			id = pi.dwProcessId;
		}
		else
		{
			DWORD error = GetLastError();
			cerr << "CreateProcess() failed with error " << error << " for command line '" << cmdLine << "'" << endl;
		}

		return bProcessCreated;
	}

	void Resume()
	{
		ResumeThread(hThread);
	}

	bool RegisterExitCallback(ProcessExitCallback callback)
	{
		if (!callback) return false;
		if (exitCallback) return false; // already registered

		exitCallback = callback;

		BOOL result = RegisterWaitForSingleObject(&hWait, hProcess, OnExited, this, INFINITE, WT_EXECUTEONLYONCE);
		if (!result)
		{
			DWORD error = GetLastError();
			cerr << "RegisterWaitForSingleObject() failed with error code " << error << endl;
		}

		return result;
	}

private:

	static void CALLBACK OnExited(void* context, BOOLEAN isTimeOut)
	{
		((Process*)context)->OnExited();
	}

	void OnExited()
	{
		exitCallback(this);
	}

	DWORD id;
	HANDLE hProcess;
	HANDLE hThread;
	HANDLE hWait;
	ProcessExitCallback exitCallback;
};

CRITICAL_SECTION coutAccess;
int nRunningProcesses;

void OnProcessExited(Process const* process)
{
	EnterCriticalSection(&coutAccess);
	try
	{
		cout << "Process with id " << process->getId() << " has exited" << endl;
		if (--nRunningProcesses == 0) cout << "All child processes have finished. Press ENTER to exit the program" << endl;
	}
	catch (...) 
	{
	}
	LeaveCriticalSection(&coutAccess);
}

int main(int argc, char** argv)
{
	InitializeCriticalSection(&coutAccess);

	srand(time(NULL));

	int nProcesses = 0;
	int maxSleep = 0;

	if (argc >= 2) nProcesses = atoi(argv[1]);
	if (nProcesses == 0) nProcesses = 100;

	if (argc >= 3) maxSleep = atoi(argv[2]);
	if (maxSleep == 0) maxSleep = 5;

	cout << "Creating " << nProcesses << " processes" << endl;

	vector<int> sleepTimes(nProcesses);
	for (int i=0; i<nProcesses; ++i) sleepTimes[i] = rand() % maxSleep*1000;

	vector<unique_ptr<Process>> processes(nProcesses);

	for (int i=0; i<nProcesses; ++i)
	{
		processes[i].reset(new Process());
		if (!processes[i]->CreateSuspended(sleepTimes[i])) return 1;
	}

	cout << "Registering wait handles" << endl;

	for (int i=0; i<nProcesses; ++i) 
	{
		if (!processes[i]->RegisterExitCallback(OnProcessExited)) return 2;
	}

	nRunningProcesses = nProcesses;

	cout << "Resuming created processes" << endl;

	for (int i=0; i<nProcesses; ++i) processes[i]->Resume();

	cin.get(); // wait for Enter

	cout << "Done" << endl;

	return 0;
}