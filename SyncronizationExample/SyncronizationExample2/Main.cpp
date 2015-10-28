#include <iostream>
#include <Windows.h>

using namespace std;

class DivisionParZero {};
DWORD WINAPI Surprise(void *p)
// LE THREAD
{
   int *val = reinterpret_cast<int *>(p);
   while (*val > 0)
   {
      (*val)--;
         Sleep(100); // Sleep(1) pour s'assurer que les autres threads
     
   } // mais, en pratique, cela ne change rien au problème proposé ici
   return 0L;
}
int f(int &denominateur) throw(DivisionParZero)
{
   const int NUM = 10;
   
   if (denominateur == 0)
   {
       // (a)
      throw DivisionParZero();
   }
      Sleep(2000);
      return NUM / denominateur; // (b)
}
int main()
{
   int val = 10;
   HANDLE hSurprise = CreateThread(0, 0, Surprise, &val, 0, 0);

 
      try
      {
         f(val);
      }
      catch (DivisionParZero) { cout << "Division sur 0" << endl; }
  
      WaitForSingleObject(hSurprise, INFINITE);
      CloseHandle(hSurprise);
      getchar();


   return 0;
}