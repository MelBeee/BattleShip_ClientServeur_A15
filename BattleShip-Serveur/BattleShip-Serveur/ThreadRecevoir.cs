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
	   //Socket listener du serveur
       TcpListener socketServeur;
	   //Variable contenant l'adresse ip du serveur
       IPAddress adresseIp;
        
       //boolean en charge de la boucle de gestion 
       Boolean ServeurOuvert = false; 
      
	   //Socket des clients
       TcpClient Joueur1;
       TcpClient Joueur2;

	   //Boolean en charge de s'assurer que les joueurs sont toujours connecté au serveur
       Boolean Joueur1EntrainDeJouer = false;
       Boolean Joueur2EntrainDeJouer = false;

       //Variable qui détermine a qui le tour est rendu(Si true Joueur1 sinon Joueur2)
       Boolean JoueurTour = true;
       
       
       //Variable de transfert d'information
       NetworkStream CommunicationJoueur;

	   // Byte utiliser lors de la reception des données
       Byte[] recevoirJoueur = new Byte[1024];
	   // Byte utiliser lors de l'envoie des données
       Byte[] envoyezJoueur;
	   //int qui s'assure si lors de réception il y de l'information d'envoyé
       int infoJoueur; 
       
	   //form serveur passer en paramêtre afin de modifier certain label lors de la connection
       FormServeur leform;

	   //Strings contenant les positions de tous les bateaux des joueurs
       String Joueur1BateauxPosition = "";
       String Joueur2BateauxPosition = "";

	   //String contentant les attaques recus des joueurs
       String AttaqueBateaux1 = "";
       String AttaqueBateaux2 ="";


       public ThreadRecevoir(FormServeur FormParent)
       {
		      //form parent mis dans la variable le form afin d'avoir d'avoir accès a ses objets
              leform = FormParent;
       }

	   //Set qui permet de changer la variable de la boucle de gestion
       public void setBooleanServeur(Boolean serveur)
       {
           ServeurOuvert = serveur;
       }
	   //Get qui permet d'avoir accès à la variable de gestion
       public Boolean getBooleanServeur()
       {
           return ServeurOuvert;
       }
       public void ThreadRecevoirLoop()
       { 
            try
            {
				//Démarage du tcplistener(pour qu'il soit prêt a recevoir les connections des tcpclients)
                adresseIp = IPAddress.Parse("0");
                socketServeur = new TcpListener(adresseIp, 1234);
                socketServeur.Start();

                //leform.Lb_JoueurConnecter.Text = "En attente de joueur...";              
                //leform.Btn_DémarrerServeur.Enabled = false;
                
				//Boucle d'attente de connection des joueurs
                for (int nbClient = 0; nbClient < 2; nbClient++)
                 {
                   // leform.Refresh();
                     if (nbClient == 0)
                     {
                         Joueur1 = socketServeur.AcceptTcpClient();
                       // leform.Lb_JoueurConnecter.Text = "Joueur trouvé en attente d'un autre joueur...";
                        //leform.Refresh();
                     }
                     else if (nbClient == 1)
                     {
                         Joueur2 = socketServeur.AcceptTcpClient();
                        //leform.Lb_JoueurConnecter.Text = "Deuxième Joueur trouvé démarage de la partie...";
                        // leform.Refresh();
                     }                  
                 }
               //  leform.Btn_DémarrerServeur.Enabled = true;                 

				//fonction qui attends la position des bateaux des deux joueurs
                 AttendreLesInfosBateaux();

              //   leform.Lb_JoueurConnecter.Text = "Le jeu est commencé";
               //  leform.Refresh();
				  
				 //boucle de gestion principale du jeu
                 BoucleJeu();          
                
                   
            }
            catch (SocketException ex)
            {

            }    
           //leform.Lb_JoueurConnecter.Text = "Le jeu est finit";
           //leform.Refresh();
       }

       private void BoucleJeu()
       {
           try
           {
               envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("StartTour");
               CommunicationJoueur = Joueur1.GetStream();
               CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);

               envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("WaitTour");
               CommunicationJoueur = Joueur2.GetStream();
               CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);

               while (ServeurOuvert && Joueur1EntrainDeJouer && Joueur2EntrainDeJouer)
               {
                   while (JoueurTour)
                   {
                       CommunicationJoueur = Joueur1.GetStream();
                       infoJoueur = CommunicationJoueur.Read(recevoirJoueur, 0, recevoirJoueur.Length);

                       if (infoJoueur != 0)
                       {
                           AttaqueBateaux1 = Encoding.UTF8.GetString(recevoirJoueur);
                           AttaqueBateaux1 = AttaqueBateaux1.Substring(0, 2);
                           Array.Clear(recevoirJoueur, 0, recevoirJoueur.Length);

                           //Traitement des attaques                         
                           if (CibleToucher(AttaqueBateaux1))
                           {
                               String touchercouler = CheckSiCouler();
                               if (touchercouler != "")
                               {
                                   envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("true/" + touchercouler);
                                   CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);
                               }
                               else
                               {
                                   envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("true/aucun");
                                   CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);
                               }
                           }
                           else
                           {
                               envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("false/aucun");
                               CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);
                           }

                           CommunicationJoueur = Joueur2.GetStream();
                           envoyezJoueur = System.Text.Encoding.ASCII.GetBytes(AttaqueBateaux1);
                           CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);

                           JoueurTour = false;
                           CheckFinDePartie();
                       }
                       else
                       {
                           Joueur1EntrainDeJouer = false;
                           //Joueur 2 win
                           envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("Gagné");
                           CommunicationJoueur = Joueur2.GetStream();
                           CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);
                           JoueurTour = true;

                       }
                   }


                   while (!JoueurTour)
                   {
                       CommunicationJoueur = Joueur2.GetStream();
                       infoJoueur = CommunicationJoueur.Read(recevoirJoueur, 0, recevoirJoueur.Length);
                       if (infoJoueur != 0)
                       {
                           AttaqueBateaux2 = Encoding.UTF8.GetString(recevoirJoueur);
                           AttaqueBateaux2 = AttaqueBateaux2.Substring(0, 2);
                           Array.Clear(recevoirJoueur, 0, recevoirJoueur.Length);
                           //Traitement des attaques
                           if (CibleToucher(AttaqueBateaux2))
                           {
                               String touchercouler = CheckSiCouler();
                               if (touchercouler != "")
                               {
                                   envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("true/" + touchercouler);
                                   CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);
                               }
                               else
                               {
                                   envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("true/aucun");
                                   CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);
                               }
                           }
                           else
                           {
                               envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("false/aucun");
                               CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);
                           }

                           CommunicationJoueur = Joueur1.GetStream();
                           envoyezJoueur = System.Text.Encoding.ASCII.GetBytes(AttaqueBateaux2);
                           CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);
                           JoueurTour = true;
                           CheckFinDePartie();
                       }
                       else
                       {
                           //Joueur 1 win
                           envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("Gagné");
                           CommunicationJoueur = Joueur1.GetStream();
                           CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);

                           Joueur2EntrainDeJouer = false;
                           JoueurTour = false;
                       }
                   }
               }
           }
           catch(Exception ex)
           {

           }          
       }

	   private void CheckFinDePartie()
	   {
           if (Joueur1BateauxPosition == "BattleShip:----/PatrolBoat:-/Destroyer:---/Submarine:--/AircraftCarrier:--/")
			{			
                //Joueur 2 win
                envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("Gagné");
                CommunicationJoueur = Joueur2.GetStream();
                CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);


                //Joueur 1 lost
                envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("Perdu");
                CommunicationJoueur = Joueur1.GetStream();
                CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);

			}
           if (Joueur2BateauxPosition == "BattleShip:----/PatrolBoat:-/Destroyer:---/Submarine:--/AircraftCarrier:--/")
            {
                //Joueur 1 win
                envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("Gagné");
                CommunicationJoueur = Joueur1.GetStream();
                CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);


                //Joueur 2 lost
                envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("Perdu");
                CommunicationJoueur = Joueur2.GetStream();
                CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);

            }
	   }

       private void AttendreLesInfosBateaux()
       {
           try
           {
			   //Tant que les deux joueur n'ont pas confirmer les positions de leurs bateaux
               while (!Joueur1EntrainDeJouer && !Joueur2EntrainDeJouer)
               {
				   //Lie la variable networkstream au stream du joueur 1
                   CommunicationJoueur = Joueur1.GetStream();
				   //S'il n'est toujours entrain de Jouer
                   if (!Joueur1EntrainDeJouer)
                   {
					   //Recoit les informations qui l'envoie
                       infoJoueur = CommunicationJoueur.Read(recevoirJoueur, 0, recevoirJoueur.Length);
					   //Si la fonction Read ne retourne pas 0 alors le joueur a envoyé une valeur
                       if (infoJoueur != 0)
                       {
						   //Transforme la variable de retour recevoirJoueur en string et la met comme étant la position des Bateaux du joueur 1
                           Joueur1BateauxPosition = System.Text.Encoding.ASCII.GetString(recevoirJoueur, 0, infoJoueur);
                           Array.Clear(recevoirJoueur, 0, recevoirJoueur.Length);                            
   						   //Set le joueur 1 comme étant entrain de jouer
                           Joueur1EntrainDeJouer = true;
						   //Envoit a son client d'attendre que le deuxième joueur soit prêt a commencer
                           envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("Attendre");
                           CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);
                       }
                   }

				   //Lie la variable networkstream au stream du joueur 2
                   CommunicationJoueur = Joueur2.GetStream();
				   //S'il n'est toujours entrain de Jouer
                   if (!Joueur2EntrainDeJouer)
                   {
					   //Recoit les informations qui l'envoie
                       infoJoueur = CommunicationJoueur.Read(recevoirJoueur, 0, recevoirJoueur.Length);
					   //Si la fonction Read ne retourne pas 0 alors le joueur a envoyé une valeur
                       if (infoJoueur != 0)
                       {
						   //Transforme la variable de retour recevoirJoueur en string et la met comme étant la position des Bateaux du joueur 2
                           Joueur2BateauxPosition = System.Text.Encoding.ASCII.GetString(recevoirJoueur, 0, infoJoueur);
						   Array.Clear(recevoirJoueur, 0, recevoirJoueur.Length);
						   //Set le joueur 2 comme étant entrain de jouer
                           Joueur2EntrainDeJouer = true;
						   //Envoit a son client d'attendre que le premier joueur soit prêt a commencer
                           envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("Attendre");
                           CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);
                       }
                   }
               }

               //Envoie Du Serveur qui dit de commencer la partie
               envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("Debut");

               CommunicationJoueur = Joueur2.GetStream();
               CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);
                    
           }
           catch(Exception ex)
           {
               
           } 
         
       }


       private Boolean CibleToucher(String attaque)
       {
		   Boolean toucher = false;
		   
		   if( JoueurTour && Joueur1BateauxPosition.Contains(attaque) )
		   { 
			   Joueur1BateauxPosition.Replace(attaque, "");
			   toucher = true;
		   }
		   else if(!JoueurTour && Joueur2BateauxPosition.Contains(attaque))
		   {
			   Joueur2BateauxPosition.Replace(attaque, "");
			   toucher = true;
		   }		
           return toucher;
       }


	   private String CheckSiCouler()
	   {
		   String ToucherCouler = "";

		   if (Joueur1BateauxPosition.Contains("Battleship:----/") || Joueur2BateauxPosition.Contains("Battleship:----/"))
			   ToucherCouler = "Battleship";
		   else if (Joueur1BateauxPosition.Contains("PatrolBoat:-/") || Joueur2BateauxPosition.Contains("PatrolBoat:-/"))
			   ToucherCouler = "PatrolBoat";
		   else if (Joueur1BateauxPosition.Contains("Destroyer:---/") || Joueur2BateauxPosition.Contains("Destroyer:---/"))
			   ToucherCouler = "Destroyer";
		   else if (Joueur1BateauxPosition.Contains("Submarine:--/") || Joueur2BateauxPosition.Contains("Submarine:--/"))
			   ToucherCouler = "Submarine";
		   else if (Joueur1BateauxPosition.Contains("AircraftCarrier:--/") || Joueur2BateauxPosition.Contains("AircraftCarrier:--/"))
			   ToucherCouler = "AircraftCarrier"; 
		   
		   return ToucherCouler;
	   }
    }
	  

}
