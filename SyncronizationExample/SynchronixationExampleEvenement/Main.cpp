#include <windows.h>
#include <iostream>
#include <string>
using namespace std;
const int MINUTES_MAX = 1;
const int SECONDES_MAX = 30;
class ParamAppl
{
public:
   ParamAppl()
   {
      Boucler1 = TRUE; Boucler2 = TRUE;
   }
public:
   BOOL Boucler1; BOOL Boucler2; HANDLE hEvent[2];
};
DWORD WINAPI Thread1(void* pParam)
{
   ParamAppl* pParamAppl = reinterpret_cast<ParamAppl*>(pParam);
   while (pParamAppl->Boucler1 || pParamAppl->Boucler2)
   {
      Sleep(1000);
      // *** 3. Déclencher les événements
      SetEvent(pParamAppl->hEvent[0]);
      SetEvent(pParamAppl->hEvent[1]);
   }
   return 0;
}
DWORD WINAPI Thread2(void* pParam)
{
   int Minutes, Secondes;
   ParamAppl* pParamAppl = reinterpret_cast<ParamAppl*>(pParam);
   Secondes = SECONDES_MAX;
   for (Minutes = MINUTES_MAX; Minutes >= 0; Minutes--)
   {
      for (; Secondes >= 0; Secondes--)
      {
         // *** 2. Attendre l'événement
         WaitForSingleObject(pParamAppl->hEvent[0], INFINITE);
         printf("Duree restante :%0.2d:%0.2d\n\r", Minutes, Secondes);
      }
      Secondes = 59;
   }
   pParamAppl->Boucler1 = FALSE; return 0;
}


DWORD WINAPI Thread3(void* pParam)
{
   int Minutes, Secondes;
   ParamAppl* pParamAppl = reinterpret_cast<ParamAppl*>(pParam);
   for (Minutes = 0; Minutes<MINUTES_MAX; Minutes++)
   {
      for (Secondes = 0; Secondes<60; Secondes++)
      {
         // *** 2. Attendre l'événement
         WaitForSingleObject(pParamAppl->hEvent[1], INFINITE);
         printf("\t\t\tDuree ecoulee :%0.2d:%0.2d\n\r", Minutes, Secondes);
      }
   }
   for (Secondes = 0; Secondes <= SECONDES_MAX; Secondes++)
   {
      // *** 2. Attendre l'événement
      WaitForSingleObject(pParamAppl->hEvent[1], INFINITE);
      printf("\t\t\tDuree ecoulee :%0.2d:%0.2d\n\r", Minutes, Secondes);
   }
   pParamAppl->Boucler2 = FALSE; return 0;
}

int main()
{
   HANDLE ListeThreads[3]; ParamAppl param;
   cout << "Synchronisation de deux threads avec un troisieme...\n\n\r";
   // *** 1. Créer les événements
   param.hEvent[0] = CreateEvent(NULL, FALSE, TRUE,
      L"Evenement pour Thread2"); // AutoReset
   param.hEvent[1] = CreateEvent(NULL, FALSE, TRUE,
      L"Evenement pour Thread3"); // AutoReset
   ListeThreads[0] = CreateThread(NULL, 0, Thread1, &param, 0, 0);
   ListeThreads[1] = CreateThread(NULL, 0, Thread2, &param, 0, 0);
   ListeThreads[2] = CreateThread(NULL, 0, Thread3, &param, 0, 0);
   WaitForMultipleObjects(3, ListeThreads, TRUE, INFINITE);
   // *** 4. Relacher les événements
   CloseHandle(param.hEvent[0]); CloseHandle(param.hEvent[1]);
   CloseHandle(ListeThreads[0]); CloseHandle(ListeThreads[1]);
   CloseHandle(ListeThreads[2]);

   cin.get(); return 0;
}