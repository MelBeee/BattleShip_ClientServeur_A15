#include <Windows.h>
#include <iostream>

using namespace std;

DWORD WINAPI Numero2(void* pParam)
{
   bool* pb;
   while (true)
   {
      pb = reinterpret_cast<bool*>(pParam);
      Sleep(100);
      cout << *pb<<endl;
     
   }
   return 0;
}


void Numero1()
{
   bool b = true; // par ex.
   HANDLE hNo2;
   hNo2 = CreateThread(0, 0, Numero2, &b, 0, 0);
   Sleep(3000);
   WaitForSingleObject(hNo2, INFINITE);
}


DWORD WINAPI Thread(void *Param)
{
   HANDLE hThread;
   Numero1(); 
   
   return 0;
}

int main()
{
   HANDLE hThread;
   int ThreadParam = 0 ;
   cout << "Deux thread ecrivent a l'écrant critique";

   hThread = CreateThread(NULL, 0, Thread, &ThreadParam, 0, 0);
  
   CloseHandle(hThread);

   cin.get();
  
   return 0;
}