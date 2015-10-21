using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace BattleShip_Serveur
{
    public partial class Form1 : Form
    {
        TcpListener socketServeur;
        Boolean ServeurOuvert = false;
        IPAddress adresseIp;
        TcpClient Joueur1;
        TcpClient Joueur2;


        public Form1()
        {
            InitializeComponent();
        }

        private void Btn_DémarrerServeur_Click(object sender, EventArgs e)
        {
            if(!ServeurOuvert)
            {  
                Btn_DémarrerServeur.Text = "Fermer";
                LB_StatusServer.Text = "SERVEUR OUVERT";
                LB_StatusServer.ForeColor = Color.Green;
               
                OuvrirServeur();
                ServeurOuvert = true;              
            }
            else
            { 
                Btn_DémarrerServeur.Text = "Ouvrir";
                LB_StatusServer.Text = "SERVEUR FERMER";
                LB_StatusServer.ForeColor = Color.Red;
                Lb_JoueurConnecter.Text = "";
                FermerServeur();
                ServeurOuvert = false;              
            }            
        }

        
        private void OuvrirServeur()
        {
                       
            try
            {
                adresseIp = IPAddress.Parse("0");
                socketServeur = new TcpListener(adresseIp, 1234);
                socketServeur.Start();
                Lb_JoueurConnecter.Text = "En attente de joueur...";             
              
                Btn_DémarrerServeur.Enabled = false;
                

                for (int nbClient = 0; nbClient < 2; nbClient++)
                 {
                     this.Refresh();
                     if (nbClient == 0)
                     {
                         Joueur1 = socketServeur.AcceptTcpClient();
                         Lb_JoueurConnecter.Text = "Joueur trouvé en attente d'un autre joueur...";
                         this.Refresh();
                     }
                     else if (nbClient == 1)
                     {
                         Joueur2 = socketServeur.AcceptTcpClient();
                         Lb_JoueurConnecter.Text = "Deuxième Joueur Trouvé démarage de la partie...";
                         this.Refresh();
                     }                         
             
                 }            
                 Btn_DémarrerServeur.Enabled = true;                              

            }
            catch(SocketException ex)
            {

            }                 
        }
        private void FermerServeur()
        {
            Joueur1.Close();
            Joueur2.Close();
            socketServeur.Stop();
            socketServeur = null;
        }
    }
}
