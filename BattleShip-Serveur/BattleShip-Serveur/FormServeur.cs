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
        Boolean ServeurOuvert =false;
        Thread t;
        ThreadRecevoir leThreadRecevoir;

        public FormServeur()
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
            }
            else
            { 
                Btn_DémarrerServeur.Text = "Ouvrir";
                LB_StatusServer.Text = "SERVEUR FERMER";
                LB_StatusServer.ForeColor = Color.Red;
                Lb_JoueurConnecter.Text = "";
                FermerServeur();                            
            }            
        }

        private void OuvrirServeur()
        {
            leThreadRecevoir = new ThreadRecevoir(this);
            leThreadRecevoir.setBooleanServeur(true);
            ServeurOuvert = leThreadRecevoir.getBooleanServeur();
            t = new Thread(new ThreadStart(leThreadRecevoir.ThreadRecevoirLoop));
            t.Start();    
        }
        private void FermerServeur()
        {
            leThreadRecevoir.setBooleanServeur(false);
            ServeurOuvert = leThreadRecevoir.getBooleanServeur();
            t.Abort();
        }
    }
}
