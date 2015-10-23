using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace BattleShip_Serveur
{
    

    class ThreadRecevoir
    {
       TcpListener socketServeur;
       Boolean ServeurOuvert = false;
       IPAddress adresseIp;
       TcpClient Joueur1;
       TcpClient Joueur2;
       Boolean ServerOuvert;
       FormServeur leform;
       public ThreadRecevoir(FormServeur FormParent)
       {
              leform = FormParent;
       }

       public void setBooleanServeur(Boolean serveur)
       {
           ServerOuvert = serveur;
       }
       public Boolean getBooleanServeur()
       {
           return ServerOuvert;
       }
       public void ThreadRecevoirLoop()
       {
           
            try
            {
                adresseIp = IPAddress.Parse("0");
                socketServeur = new TcpListener(adresseIp, 1234);
                socketServeur.Start();
                leform.Lb_JoueurConnecter.Text = "En attente de joueur...";              
                leform.Btn_DémarrerServeur.Enabled = false;
                

                for (int nbClient = 0; nbClient < 2; nbClient++)
                 {
                     leform.Refresh();
                     if (nbClient == 0)
                     {
                         Joueur1 = socketServeur.AcceptTcpClient();
                         leform.Lb_JoueurConnecter.Text = "Joueur trouvé en attente d'un autre joueur...";
                         leform.Refresh();
                     }
                     else if (nbClient == 1)
                     {
                         Joueur2 = socketServeur.AcceptTcpClient();
                         leform.Lb_JoueurConnecter.Text = "Deuxième Joueur trouvé démarage de la partie...";
                         leform.Refresh();
                     }                  
                 }
                 leform.Btn_DémarrerServeur.Enabled = true;
                   
                   while(ServerOuvert)
                   {
                       /*Tu update la grille des deux joueurs pis tu leur renvoie leurs informations*/


                   }
                   
            }
            catch (SocketException ex)
            {

            } 
           
          
       }
    }
}
