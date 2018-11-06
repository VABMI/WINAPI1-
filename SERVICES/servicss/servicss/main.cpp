#define _WIN32_WINNT 0x0501

#include <windows.h>

#include <stdio.h>
#define srvName "MyTestService_01312009"
void installService(char*path)
{
    SC_HANDLE handle = ::OpenSCManager( NULL, NULL, SC_MANAGER_ALL_ACCESS );
    SC_HANDLE service = ::CreateService(
        handle,
        srvName,
        "MyTestService_01312009b",
        GENERIC_READ | GENERIC_EXECUTE,
        SERVICE_WIN32_OWN_PROCESS,
        SERVICE_AUTO_START,
		SERVICE_ERROR_SEVERE,
        path,
        NULL,
        NULL,
        NULL,
        NULL,
        NULL
    );

	if(service==NULL)
	{
		printf("OpenSCManager() failed, error: %d.\n", GetLastError());

	}





}








 

int main(void)

{

SC_HANDLE schSCManager, schService;

 

// The service executable location, just dummy here else make sure the executable is there :o)...

// Well as a real example, we are going to create our own telnet service.

// The executable for telnet is: C:\WINDOWS\system32\tlntsvr.exe

LPCTSTR lpszBinaryPathName = "F:\\WINAPI1-\\ShutDown on desktop\\Debug\\p21.exe";
	//	installService((char*)lpszBinaryPathName);


// Service display name...

LPCTSTR lpszDisplayName = "MyOwnTelnetService";

// Registry Subkey

LPCTSTR lpszServiceName = "MyTelnetSrv";

 

// Open a handle to the SC Manager database...

schSCManager = OpenSCManager(

NULL, // local machine

NULL, // SERVICES_ACTIVE_DATABASE database is opened by default

SC_MANAGER_ALL_ACCESS); // full access rights

 

if (NULL == schSCManager)

printf("OpenSCManager() failed, error: %d.\n", GetLastError());

else

printf("OpenSCManager() looks OK.\n");

 

// Create/install service...

// If the function succeeds, the return value is a handle to the service. If the function fails, the return value is NULL.

schService = CreateService(

schSCManager, // SCManager database

lpszServiceName, // name of service

lpszDisplayName, // service name to display

SERVICE_ALL_ACCESS, // desired access

SERVICE_WIN32_OWN_PROCESS, // service type

SERVICE_DEMAND_START, // start type

SERVICE_ERROR_NORMAL, // error control type

lpszBinaryPathName, // service's binary

NULL, // no load ordering group

NULL, // no tag identifier

NULL, // no dependencies, for real telnet there are dependencies lor

NULL, // LocalSystem account

NULL); // no password



if (schService == NULL)

{

printf("CreateService() failed, error: %ld\n", GetLastError());

return FALSE;

}

else

{

printf("CreateService() for %S looks OK.\n", lpszServiceName);

if (CloseServiceHandle(schService) == 0)

printf("CloseServiceHandle() failed, Error: %d.\n", GetLastError());

else

printf("CloseServiceHandle() is OK.\n");



}
 
return 0;

}