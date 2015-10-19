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
                OuvrirServeur();
                ServeurOuvert = true;
            }
            else
            {
                FermerServeur();
            }            
        }

        
        private void OuvrirServeur()
        {
                       
            try
            {
                adresseIp = IPAddress.Parse("127.0.0.1");
                socketServeur = new TcpListener(adresseIp, 1234);
                socketServeur.Start();
                Lb_JoueurConnecter.Text = "En attente de joueur...";
                LB_StatusServer.Text = "SERVEUR OUVERT";
                LB_StatusServer.ForeColor = Color.Green;
                Btn_DémarrerServeur.Text = "Déconnecter";
                Btn_DémarrerServeur.Enabled = false;
                this.Refresh();

                for (int nbClient = 0; nbClient < 2; nbClient++)
                 {
                    
                     if (nbClient == 0)
                     {
                         Joueur1 = socketServeur.AcceptTcpClient();
                         Lb_JoueurConnecter.Text = "Joueur trouvé en attente d'un autre joueur...";
                     }
                     else if (nbClient == 1)
                     {
                         Lb_JoueurConnecter.Text = "Deuxième Joueur Trouvé démarage de la partie...";
                     }
                         
                     this.Refresh();
                 }            
                 Btn_DémarrerServeur.Enabled = true;
                
                 

              //  }                

            }
            catch(SocketException ex)
            {

            }                 
        }
        private void FermerServeur()
        {
            socketServeur.Stop();
            socketServeur = null;
        }
    }
}
