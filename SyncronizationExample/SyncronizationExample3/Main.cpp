#include <Windows.h>
#include <iostream>
#include <string>


using namespace std;

DWORD WINAPI Inverse(void *p)
{
   string *s = reinterpret_cast<string*>(p);
   int longueur = static_cast<int>(s->length());
   int milieu = longueur / 2;
   char temp;
   for (int i = 0; i< milieu; i++)
   {
      Sleep(i + 20);
      temp = (*s)[i];
      (*s)[i] = (*s)[longueur - i - 1];
      (*s)[longueur - i - 1] = temp;
   }
   return 0;
}
DWORD WINAPI Plus2(void *p)
{
   string *s = reinterpret_cast<string*>(p);
   int longueur = static_cast<int>(s->length());
   for (int i = 0; i< longueur; i++)
   {
      Sleep(i*10);
      (*s)[i] += 2;
   }
   return 0;
}

int main()
{

   string chaine =  "abcdef";

   HANDLE th1 = CreateThread(0, 0, Plus2, &chaine, 0, 0);
   
   HANDLE th2 = CreateThread(0, 0, Inverse, &chaine, 0, 0);

  
   

   WaitForSingleObject(th1, INFINITE);
   CloseHandle(th1);
   WaitForSingleObject(th2, INFINITE);
   CloseHandle(th2);
   cout << "la chaine est maintenant egal a " + chaine << endl;

   getchar();


   return 0;
}