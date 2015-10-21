#include <windows.h>
#include <iostream>
using namespace std;



int main(int argc, CHAR* argv[])
{
   STARTUPINFO si; PROCESS_INFORMATION pi;
   ZeroMemory(&si, sizeof(si)); si.cb = sizeof(si); ZeroMemory(&pi, sizeof(pi));
   // IMPORTANT - Doit être dans une variable
   wchar_t processusStr[] = L"calc.exe";
   // Lancement du processus enfant
   if (!CreateProcess(NULL, processusStr,
      NULL, // Process handle not inheritable
      NULL, // Thread handle not inheritable
      FALSE, // Set handle inheritance to FALSE
      0, // No creation flags
      NULL, // Use parent's environment block
      NULL, // Use parent's starting directory
      &si, // Pointer to STARTUPINFO structure
      &pi) // Pointer to PROCESS_INFORMATION structure
      )
   {
      cout << "Création du processus echouee..." << endl;
   }
   cout << "fuckaaaaaaaaaaaaaaaaaa" << endl;
   // Attente de la fin du processus enfant
   WaitForSingleObject(pi.hProcess, INFINITE);
   cout << "fuck" << endl;
   // Ferme les handles de processus et de thread
   CloseHandle(pi.hProcess); CloseHandle(pi.hThread);

   

   return 0;
}
