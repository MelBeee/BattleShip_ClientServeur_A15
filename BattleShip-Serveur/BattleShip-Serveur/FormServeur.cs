using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace BattleShip_Serveur
{
    public partial class FormServeur : Form
    {
		//Boolean du serveur
        Boolean ServeurOuvert =false;
		//Thread
        Thread t;
		//Thread qui recoit l'information des clients
        ThreadRecevoir leThreadRecevoir;

        public FormServeur()
        {
            InitializeComponent();
        }

        private void Btn_DémarrerServeur_Click(object sender, EventArgs e)
        {
			//Si le serveur est ouvert
            if(ServeurOuvert)
            {  
				Btn_DémarrerServeur.Text = "Fermer";
                LB_StatusServer.Text = "SERVEUR OUVERT";
                LB_StatusServer.ForeColor = Color.Green;               
                OuvrirServeur();  
            }
            else
            { 
                 //Update les controles du form du serveur pour qu'il marque s
               	Btn_DémarrerServeur.Text = "Ouvrir";
                LB_StatusServer.Text = "SERVEUR FERMER";
                LB_StatusServer.ForeColor = Color.Red;
                Lb_JoueurConnecter.Text = "";
				//Ferme le serveur
                FermerServeur();                        
            }            
        }

        private void OuvrirServeur()
        {
			//Crée le thread avec comme parametre le form du serveur
            leThreadRecevoir = new ThreadRecevoir(this);
			//Set le boolean de la boucle de gestion a true
            leThreadRecevoir.setBooleanServeur(true);
			//Met le boolean comme nouvelle valeur de ServeurOuvert
            ServeurOuvert = leThreadRecevoir.getBooleanServeur();
			//Crée le thread
            t = new Thread(new ThreadStart(leThreadRecevoir.ThreadRecevoirLoop));
            t.IsBackground = true;
			//Start le thread
            t.Start();    
        }
        private void FermerServeur()
        {
			//Set le boolean de la boucle de gestion a false
            leThreadRecevoir.setBooleanServeur(false);
			//Met le boolean comme nouvelle valeur de ServeurOuvert
            ServeurOuvert = leThreadRecevoir.getBooleanServeur();
			//Abort le thread
            t.Abort();
        }
    }
}
