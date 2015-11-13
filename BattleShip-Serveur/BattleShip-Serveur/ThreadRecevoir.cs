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
               //Envoit a chaque joueur qui commence
               envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("StartTour");
               CommunicationJoueur = Joueur1.GetStream();
               CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);

               envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("WaitTour");
               CommunicationJoueur = Joueur2.GetStream();
               CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);

               //Tant que le serveur est ouvert et que le Joueur 1 et Joueur 2 sont entrain de jouer
               while (ServeurOuvert && Joueur1EntrainDeJouer && Joueur2EntrainDeJouer)
               {
                   //Si c'est le tour du joueur 1
                   if (JoueurTour)
                   {
                       //Get le stream du Joueur1
                       CommunicationJoueur = Joueur1.GetStream();
                       //Read le coup qu'il joue
                       infoJoueur = CommunicationJoueur.Read(recevoirJoueur, 0, recevoirJoueur.Length);
                       //Si ce n'est pas égal a 0 alors il a joué sinon il n'est plus connecté
                       if (infoJoueur != 0)
                       {
                           //Get le coup du joueur en string
                           AttaqueBateaux1 = Encoding.UTF8.GetString(recevoirJoueur);
                           AttaqueBateaux1 = AttaqueBateaux1.Substring(0, 2);
                           Array.Clear(recevoirJoueur, 0, recevoirJoueur.Length);

                           //Check si l'attaque envoyé par le joueur touche un bateau adverse
                           if (CibleToucher(AttaqueBateaux1))
                           {
                               //Check si le bateau est couler
                               String touchercouler = CheckSiCouler(ref Joueur2BateauxPosition);
                               if (touchercouler != "")
                               {
                                   String SJoueur1 = "ovule/";
                                   String SJoueur2 = "ovule/";
                                   CheckFinDePartie(ref SJoueur1, ref SJoueur2);

                                   //Si coulé envoie
                                   CommunicationJoueur = Joueur1.GetStream();
                                   envoyezJoueur = System.Text.Encoding.ASCII.GetBytes(SJoueur1 +"true/" + touchercouler);
                                   CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);

                                   //Renvoie au joueur 1 (le joueur qui c'est fait attaqué) la possition du coup du joueur 2
                                   CommunicationJoueur = Joueur2.GetStream();
                                   envoyezJoueur = System.Text.Encoding.ASCII.GetBytes(SJoueur2 + AttaqueBateaux1 + "/" + touchercouler);
                                   CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);
                               }
                               else
                               {
                                   //Si pas coulé
                                   CommunicationJoueur = Joueur1.GetStream();
                                   envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("ovule/true/aucun");
                                   CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);

                                   CommunicationJoueur = Joueur2.GetStream();
                                   envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("ovule/" + AttaqueBateaux1 + "/aucun");
                                   CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);
                               }
                           }
                           else
                           {
                               //S'il ne touche personne
                               CommunicationJoueur = Joueur1.GetStream();
                               envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("ovule/false/aucun");
                               CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);

                               CommunicationJoueur = Joueur2.GetStream();
                               envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("ovule/" + AttaqueBateaux1 + "/aucun");
                               CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);
                           }

                           
                           //Finit le tour du joueur 1
                           JoueurTour = false;
                       
                       }
                       else
                       {
                           Joueur1EntrainDeJouer = false;
                           //Joueur 2 win
                           envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("Gagne/false/aucun");
                           CommunicationJoueur = Joueur2.GetStream();
                           CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);
                           JoueurTour = true;

                       }
                   }

                   //Si c'est le tour du joueur 2
                   if (!JoueurTour)
                   {
                       //Get le stream du Joueur2
                       CommunicationJoueur = Joueur2.GetStream();
                       //Read le coup qu'il joue
                       infoJoueur = CommunicationJoueur.Read(recevoirJoueur, 0, recevoirJoueur.Length);
                       if (infoJoueur != 0)
                       {
                           //Get le coup du joueur en string
                           AttaqueBateaux2 = Encoding.UTF8.GetString(recevoirJoueur);
                           AttaqueBateaux2 = AttaqueBateaux2.Substring(0, 2);
                           Array.Clear(recevoirJoueur, 0, recevoirJoueur.Length);

                           //Check si l'attaque envoyé par le joueur touche un bateau adverse
                           if (CibleToucher(AttaqueBateaux2))
                           {
                               //Check si le bateau est couler
                               String touchercouler = CheckSiCouler(ref Joueur1BateauxPosition);
                               if (touchercouler != "")
                               {
                                   String SJoueur1 = "ovule/";
                                   String SJoueur2 = "ovule/";
                                   CheckFinDePartie(ref SJoueur1, ref SJoueur2);

                                   //Si coulé envoie
                                   CommunicationJoueur = Joueur2.GetStream();
                                   envoyezJoueur = System.Text.Encoding.ASCII.GetBytes(SJoueur2+"true/" + touchercouler);
                                   CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);

                          
                                   CommunicationJoueur = Joueur1.GetStream();
                                   envoyezJoueur = System.Text.Encoding.ASCII.GetBytes(SJoueur1 + AttaqueBateaux2 + "/" + touchercouler);
                                   CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);
                               }
                               else
                               {
                                   //Si pas coulé
                                   CommunicationJoueur = Joueur2.GetStream();
                                   envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("ovule/true/aucun");
                                   CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);

                              
                                   CommunicationJoueur = Joueur1.GetStream();
                                   envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("ovule/" + AttaqueBateaux2 + "/aucun");
                                   CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);
                               }
                           }
                           else
                           { 
                               //S'il ne touche personne
                               CommunicationJoueur = Joueur2.GetStream();
                               envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("ovule/false/aucun");
                               CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);

                            
                               CommunicationJoueur = Joueur1.GetStream();
                               envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("ovule/" + AttaqueBateaux2 + "/aucun");
                               CommunicationJoueur.Write(envoyezJoueur, 0, envoyezJoueur.Length);
                           }
                         
                           JoueurTour = true;
                
                       }
                       else
                       {
                           //Joueur 1 win
                           envoyezJoueur = System.Text.Encoding.ASCII.GetBytes("Gagne/false/aucun");
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

	   private void CheckFinDePartie(ref String Joueur1, ref String Joueur2)
	   {
    
           if (Joueur1BateauxPosition == "BattleShip:----/PatrolBoat:-/Destroyer:---/Submarine:--/AircraftCarrier:--/")
			{
                Joueur1 = "Gagne/";
                Joueur2 = "Perdu/";
			}
           if (Joueur2BateauxPosition == "BattleShip:----/PatrolBoat:-/Destroyer:---/Submarine:--/AircraftCarrier:--/")
            {
                Joueur2 = "Gagne/";
                Joueur1 = "Perdu/";
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


       //Fonction qui regarde si la cible attaquer est toucher
       private Boolean CibleToucher(String attaque)
       {
		   Boolean toucher = false;
		   
		   if(JoueurTour && Joueur2BateauxPosition.Contains(attaque) )
		   { 
			  Joueur2BateauxPosition =  Joueur2BateauxPosition.Replace(attaque, "");
			  toucher = true;
		   }
		   else if(!JoueurTour && Joueur1BateauxPosition.Contains(attaque))
		   {
			 Joueur1BateauxPosition  =  Joueur1BateauxPosition.Replace(attaque, "");
			 toucher = true;
		   }		
           return toucher;
       }


	   private String CheckSiCouler(ref String JoueurBateaux)
	   {
		   String ToucherCouler = "";

		   if (JoueurBateaux.Contains("BattleShip:----/"))
           {
               ToucherCouler = "BattleShip";
               JoueurBateaux = JoueurBateaux.Replace("BattleShip:----/","");
           }
           else if (JoueurBateaux.Contains("PatrolBoat:-/"))
           {
                ToucherCouler = "PatrolBoat";
                JoueurBateaux = JoueurBateaux.Replace("PatrolBoat:-/", "");
           }			   
           else if (JoueurBateaux.Contains("Destroyer:---/"))
           {
                ToucherCouler = "Destroyer";
                JoueurBateaux = JoueurBateaux.Replace("Destroyer:---/", "");
           }			   
           else if (JoueurBateaux.Contains("Submarine:--/"))
           {
                ToucherCouler = "Submarine";
                JoueurBateaux = JoueurBateaux.Replace("Submarine:--/", "");
           }			  
           else if (JoueurBateaux.Contains("AircraftCarrier:--/"))
           {
               ToucherCouler = "AircraftCarrier";
               JoueurBateaux = JoueurBateaux.Replace("AircraftCarrier:--/", "");
           }			  
		   
		   return ToucherCouler;
	   }
    }
	  

}
