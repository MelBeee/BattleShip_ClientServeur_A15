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
        TcpClient[] TabClient;


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
                            

                for (int nbClient = 0; nbClient < 1; nbClient++)
                {
                    this.Refresh();
                    TabClient[nbClient] = socketServeur.AcceptTcpClient();
                    if (nbClient == 0)
                        Lb_JoueurConnecter.Text = "Joueur trouvé en attente d'un autre joueur...";
                    else if(nbClient == 1)
                        Lb_JoueurConnecter.Text = "Deuxième Joueur Trouvé démarage de la partie...";

                }                

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
