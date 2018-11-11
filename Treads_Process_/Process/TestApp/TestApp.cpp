#include <stdlib.h>
#include <Windows.h>

int main(int argc, char** argv)
{
	if (argc < 2) return 1;
	int timeToSleep = atoi(argv[1]);
	if (timeToSleep == 0) return 2;

	Sleep(timeToSleep);
	return 0;
}