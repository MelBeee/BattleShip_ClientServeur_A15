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
       IPAddress adresseIp;
                
       Boolean ServeurOuvert = false; 
      
       TcpClient Joueur1;
       TcpClient Joueur2;
       Boolean Joueur1EntrainDeJouer = false;
       Boolean Joueur2EntrainDeJouer = false;
       
       //Variable de transfert d'information
       NetworkStream CommunicationJoueur;

       Byte[] bytes = new Byte[1024];

       FormServeur leform;

       String Joueur1BateauxPosition;
       String Joueur2BateauxPosition;
          
       public ThreadRecevoir(FormServeur FormParent)
       {
              leform = FormParent;
       }

       public void setBooleanServeur(Boolean serveur)
       {
           ServeurOuvert = serveur;
       }
       public Boolean getBooleanServeur()
       {
           return ServeurOuvert;
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

                 AttendreLesInfosBateaux();



                 while (ServeurOuvert && Joueur1EntrainDeJouer && Joueur2EntrainDeJouer)
                 {
                       // Tu update la grille des deux joueurs pis tu leur renvoie leurs informations
                       

                 }
                   
            }
            catch (SocketException ex)
            {

            } 
       }

       private void AttendreLesInfosBateaux()
       {
           int infoJoueur;         
           Byte[] envoyezJoueur;

           while(!Joueur1EntrainDeJouer && !Joueur2EntrainDeJouer)
           {
               CommunicationJoueur = Joueur1.GetStream();
               if (!Joueur1EntrainDeJouer)
               {
                  
                   infoJoueur = CommunicationJoueur.Read(bytes, 0, bytes.Length);
                   if (infoJoueur != 0)
                   {
                        Joueur1BateauxPosition = System.Text.Encoding.ASCII.GetString(bytes, 0, infoJoueur);
                        Joueur1EntrainDeJouer = true;  
                   }                                  
               }
               else
               {
                   envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("Attendre pour l'autre Joueur");
                   CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);                  
                  
               }
               
               
               CommunicationJoueur = Joueur2.GetStream();
               if(!Joueur2EntrainDeJouer)
               {
                   
                   infoJoueur = CommunicationJoueur.Read(bytes, 0, bytes.Length);
                   if (infoJoueur != 0)
                   {
                        Joueur2BateauxPosition = System.Text.Encoding.ASCII.GetString(bytes, 0, infoJoueur);
                        Joueur2EntrainDeJouer = true; 
                   }                            
               }
               else
               {
                   envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("Attendre pour l'autre Joueur");
                   CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);  
               }
           }
           
           //Envoie Du Serveur qui dit de commencer la partie
           envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("Debut");

           CommunicationJoueur = Joueur1.GetStream();
           CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);

           CommunicationJoueur = Joueur2.GetStream();
           CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);
                    
       }
    }
}
