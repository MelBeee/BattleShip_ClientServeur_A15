using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{

    public partial class Choisir_Position : Form
    {
        public Button PassPosition;
        public string PositionStart = null;
        public string PositionFin = null;
        public string Bateau;
        public bool BateauPlacer = false;
        public string BattleShip;
        public string Destroyer;
        public string AircraftCarrier;
        public string PatrolBoat;
        public string Submarine;
        public string StringACharlie = "";
        public int CompteurBateau = 0;

        TcpClient client;
        NetworkStream netStream;
        public Choisir_Position(TcpClient unclient)
        {
            InitializeComponent();
            client = unclient;
            netStream = unclient.GetStream();
        }
        //Attend que le joueur 2 est pret a jouer
        private void BoucleAttente()
        {
            bool debut = false;
            string reponse = "";
            if (netStream.CanRead)
            {
                byte[] bytes = new byte[client.ReceiveBufferSize];
                netStream.Read(bytes, 0, (int)client.ReceiveBufferSize);
                reponse = Encoding.UTF8.GetString(bytes);
            }
            if (reponse == "Debut")
            {
                PlancheJeu planche = new PlancheJeu(client);
                this.Visible = false;
                planche.ShowDialog();
                this.Close();
                debut = true;
            }
        }

        private void Choisir_Position_Load(object sender, EventArgs e)
        {
            //Bloque le start button temp et aussi longtemp que tout les bateaux ne sont pas place
            BTN_Start.Enabled = true;
            RB_Croiseur.Enabled = false;
            RB_ContreTorpilleur.Enabled = false;
            RB_SousMarin.Enabled = false;
            RB_Torpilleur.Enabled = false;
        }
        //Fonction qui fait partire la partie 
        private void BTN_Start_Click(object sender, EventArgs e)
        {
            //String des differents bateau avec leur position sur la plage
           // StringACharlie = BattleShip + PatrolBoat + Destroyer + Submarine + AircraftCarrier;
            StringACharlie = "BattleShip:B0-B1-B2-B3-B4/PatrolBoat:E0-E1/Destroyer:C0-C1-C2-C3/Submarine:D0-D1-D2/AircraftCarrier:A0-A1-A2/";
            if (netStream.CanWrite)
            {
                Byte[] sendBytes = Encoding.UTF8.GetBytes(StringACharlie);
                netStream.Write(sendBytes, 0, sendBytes.Length);
            }
            string reponse = "";
            if (netStream.CanRead)
            {
                byte[] bytes = new byte[client.ReceiveBufferSize];
                netStream.Read(bytes, 0, (int)client.ReceiveBufferSize);
                reponse = Encoding.UTF8.GetString(bytes);
            }
            BTN_Start.Enabled = false;
            if (reponse == "Attendre")
            {
                BoucleAttente();
            }
            else
            {
                PlancheJeu planche = new PlancheJeu(client);
                this.Visible = false;
                planche.ShowDialog();
                this.Close();
            }
        }

        private void BTN_A1_Click(object sender, EventArgs e)
        {
            string Lettre_ ;
            string Nombre_;
            Button Position_Choisi = (Button)sender;
            //Cette fonction remet tout les case avec un background noire sauf pour ceux avec un background blue
            if (PassPosition != null)
            {
                if (PassPosition.BackColor != Color.Blue)
                {
                    PassPosition.BackColor = Color.Black;
                }
            }
            //Cette section prend le nom du button choisie et recupere les informations importantes
            //comme la lettre et le nombre
            PassPosition = (Button)sender;
            string Position_name = Position_Choisi.Name;
            string Position = "";
            Position = Position_name.Substring(4, 2);
            //Ici j'obtien la lettre selectionne
            Lettre_ = Position.Substring(0,1);
            //Ici j'obtien le nombre selectionne
            Nombre_ = Position.Substring(1, Position.Length - 1);
            //Cette reverifie si une position a deja ete choisie ou non
            if (PositionStart == null)
            {
                PositionStart = Lettre_ + Nombre_;
                BateauPlacer = false;
                
            }
            else if(PositionStart != null)
            {
                
                //Une position a deja ete choisie il est donc pret a positione son bateau sur la plage
                //Selon où il est rendu dans les bateaux a place
                PositionFin = Lettre_ + Nombre_;
                if (RB_PortAvion.Checked)
                {
                    MettreEnPlace(PositionStart, PositionFin, 5, "BattleShip");
                    RB_PortAvion.Enabled = false;
                    RB_PortAvion.Checked = false;
                    RB_Croiseur.Checked = true;
                    RB_Croiseur.Enabled = true;
                }
                else if (RB_Croiseur.Checked)
                {
                    MettreEnPlace(PositionStart, PositionFin, 4, "Destroyer");
                    RB_Croiseur.Enabled = false;
                    RB_Croiseur.Checked = false;
                    RB_ContreTorpilleur.Checked = true;
                    RB_ContreTorpilleur.Enabled = true;
                }
                else if (RB_ContreTorpilleur.Checked)
                {
                    MettreEnPlace(PositionStart, PositionFin, 3, "AircraftCarrier");
                    RB_ContreTorpilleur.Enabled = false;
                    RB_ContreTorpilleur.Checked = false;
                    RB_SousMarin.Checked = true;
                    RB_SousMarin.Enabled = true;
                }
                else if (RB_Torpilleur.Checked)
                {
                    MettreEnPlace(PositionStart, PositionFin, 2, "PatrolBoat");
                    RB_Torpilleur.Enabled = false;
                    RB_Torpilleur.Checked = false;

                }
                else if (RB_SousMarin.Checked)
                {
                    MettreEnPlace(PositionStart, PositionFin, 3, "Submarine");
                    RB_SousMarin.Enabled = false;
                    RB_SousMarin.Checked = false;
                    RB_Torpilleur.Checked = true;
                    RB_Torpilleur.Enabled = true;
                }
                EnablePlage();
                PositionStart = null;
                if(CompteurBateau == 5)
                {
                    BTN_Start.Enabled = true;
                }
            }

            //Aucune position n'a ete choisie a date la selection est donc une nouvelle
            //Il faut maintenant donner les choix de positionnements pour le bateau sur la plage
            if (BateauPlacer == false)
            {
                if (RB_PortAvion.Checked)
                {
                    //Cette fonction donne les choix qui s'offre au joueur selon la position qu'il a choisie
                    ChoisirPositionBateau(Lettre_, Nombre_, 5);
                    //Cette fonction bloque tout les autres bouttons sauf ceux que le joueur a comme possibilite de jouer
                    DisablePlage();
                }
                else if (RB_Croiseur.Checked)
                {
                    //Cette fonction donne les choix qui s'offre au joueur selon la position qu'il a choisie
                    ChoisirPositionBateau(Lettre_, Nombre_, 4);
                    //Cette fonction bloque tout les autres bouttons sauf ceux que le joueur a comme possibilite de jouer
                    DisablePlage();
                }
                else if (RB_ContreTorpilleur.Checked)
                {
                    //Cette fonction donne les choix qui s'offre au joueur selon la position qu'il a choisie
                    ChoisirPositionBateau(Lettre_, Nombre_, 3);
                    //Cette fonction bloque tout les autres bouttons sauf ceux que le joueur a comme possibilite de jouer
                    DisablePlage();
                }
                else if (RB_Torpilleur.Checked)
                {
                    //Cette fonction donne les choix qui s'offre au joueur selon la position qu'il a choisie
                    ChoisirPositionBateau(Lettre_, Nombre_, 2);
                    //Cette fonction bloque tout les autres bouttons sauf ceux que le joueur a comme possibilite de jouer
                    DisablePlage();
                }
                else if (RB_SousMarin.Checked)
                {
                    //Cette fonction donne les choix qui s'offre au joueur selon la position qu'il a choisie
                    ChoisirPositionBateau(Lettre_, Nombre_, 3);
                    //Cette fonction bloque tout les autres bouttons sauf ceux que le joueur a comme possibilite de jouer
                    DisablePlage();
                }
                
            }

        }
        //Cette fonction est la fase finale du placement d'un bateau et prend en paramettre la position de depart et de fin du bateau
        //ainsi que le nom et la grosseur
        //Le positionnement des bateaux est fait de gauche a droite ou de haut en bas
        public void MettreEnPlace(string PosStart, string PosFin ,int GrosseurBateau, string NomBateau)
        {
            string StartLettre_;
            string StartNombre_;
            string FinLettre_;
            string FinNombre_;
            //Un peu comme au debut je substring la position pour obtenir la lettre et le nombre de la position
            StartLettre_ = PosStart.Substring(0, 1);
            StartNombre_ = PosStart.Substring(1, PosStart.Length - 1);
            FinLettre_ = PosFin.Substring(0, 1);
            FinNombre_ = PosFin.Substring(1, PosFin.Length - 1);

            //Si la lettre est la meme du debut jusqu'a la fin il suffit de changer le nombre 
            if((int)Convert.ToChar(StartLettre_) == (int)Convert.ToChar(FinLettre_))
            {
                //Si le nombre est plus grand a la fin il faut donc augmenter le i a partire du debut
                if(Convert.ToInt16(StartNombre_) < Convert.ToInt16(FinNombre_))
                {
                    //Bateau store les positions ainsi que le nom du bateau a placer dans un meme string
                    Bateau = NomBateau + ":" ;
                    for (int i = Convert.ToInt16(StartNombre_); i <= Convert.ToInt16(StartNombre_) + (GrosseurBateau - 1); i++)
                    {
                        Bateau = Bateau + "-" + StartLettre_ + Convert.ToString(i) ;
                        Button BTN_Pos = this.Controls.Find("BTN_" + StartLettre_ + Convert.ToString(i), true).FirstOrDefault() as Button;
                        BTN_Pos.BackColor = Color.Blue;
                        BTN_Pos.Enabled = false;
                    }
                    Bateau = Bateau + "/";
                }
                //si le nombre est plus petit au debut il faut donc augmenter le i a partire de la fin
                if (Convert.ToInt16(StartNombre_) > Convert.ToInt16(FinNombre_))
                {
                    //Bateau store les positions ainsi que le nom du bateau a placer dans un meme string
                    Bateau = NomBateau + ":" ;
                    for (int i = Convert.ToInt16(FinNombre_); i <= Convert.ToInt16(StartNombre_); i++)
                    {
                        Bateau = Bateau + "-"+ FinLettre_ + Convert.ToString(i) ;
                        Button BTN_Pos = this.Controls.Find("BTN_" + FinLettre_ + Convert.ToString(i), true).FirstOrDefault() as Button;
                        BTN_Pos.BackColor = Color.Blue;
                        BTN_Pos.Enabled = false;
                    }
                    Bateau = Bateau + "/";
                }
            }
            //Si la lettre de debut et differente de celle de fin il faut donc garder le meme nombre mais
            //augmenter ou diminuer la lettre
            if((int)Convert.ToChar(StartLettre_) != (int)Convert.ToChar(FinLettre_))
            {
                //si la lettre de debut est plus petite que celle de la fin il faut donc augmenter le i a partire du debut
                if ((int)Convert.ToChar(StartLettre_) < (int)Convert.ToChar(FinLettre_))
                {
                    //Bateau store les positions ainsi que le nom du bateau a placer dans un meme string
                    Bateau = NomBateau + ":";
                    for (int i = (int)Convert.ToChar(StartLettre_); i <= (int)Convert.ToChar(StartLettre_) + (GrosseurBateau - 1); i++)
                    {
                        Bateau = Bateau + "-" + Convert.ToString(Convert.ToChar(i)) + StartNombre_ ;
                        Button BTN_Pos = this.Controls.Find("BTN_" + Convert.ToString(Convert.ToChar(i)) + StartNombre_, true).FirstOrDefault() as Button;
                        BTN_Pos.BackColor = Color.Blue;
                        BTN_Pos.Enabled = false;
                    }
                    Bateau = Bateau +  "/";
                }
                //Si la lettre de debut est plus grande que la lettre de fin il faut augmenter le i a partire de la fin
                if ((int)Convert.ToChar(StartLettre_) > (int)Convert.ToChar(FinLettre_))
                {
                    //Bateau store les positions ainsi que le nom du bateau a placer dans un meme string
                    Bateau = NomBateau + ":";
                    for (int i = (int)Convert.ToChar(FinLettre_); i <= (int)Convert.ToChar(StartLettre_); i++)
                    {
                        Bateau = Bateau + "-" + Convert.ToString(Convert.ToChar(i)) + StartNombre_;
                        Button BTN_Pos = this.Controls.Find("BTN_" + Convert.ToString(Convert.ToChar(i)) + StartNombre_, true).FirstOrDefault() as Button;
                        BTN_Pos.BackColor = Color.Blue;
                        BTN_Pos.Enabled = false;
                    }
                    Bateau = Bateau +  "/";
                }
            }
            //Cette section store les informations complete de chaque bateau dans leurs string personnel pour ordonner le tout comme
            //on veut a la fin
            if (NomBateau == "BattleShip")
            {
                BattleShip = Bateau;
            }
            else if (NomBateau == "Destroyer")
            {
                Destroyer = Bateau;
            }
            else if (NomBateau == "AircraftCarrier")
            {
                AircraftCarrier = Bateau;
            }
            else if (NomBateau == "PatrolBoat")
            {
                PatrolBoat = Bateau;
            }
            else if (NomBateau == "Submarine")
            {
                Submarine = Bateau;
            }
            //Compteur de bateau qui permet de verifier si les 5 bateaux son place sur la plage
            CompteurBateau++;
            BateauPlacer = true;

        }
        //Bloque pour de bon tout les cases qui ne sont pas disponible au joueur de cliquer une fois une position de depart
        //choisie pour leur bateau
        public void DisablePlage()
        {
            for (int i = (int)Convert.ToChar('A'); i <= 74; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    Button BTN_Pos = this.Controls.Find("BTN_" + Convert.ToChar(i) + j, true).FirstOrDefault() as Button;
                    BTN_Pos.Enabled = false;
                    //Si le fond du button est rouge il doit etre maintenu disponible pour permettre au joueur de choisir la position de fin de leur bateau
                    if (BTN_Pos.BackColor == Color.Red)
                    {
                        BTN_Pos.Enabled = true;
                    }

                }

            }
        }
        //Fait le contraire un peu du DisablePlage() il remet tout les buttons utilisables sauf pour les buttons avec un background bleu qui signifie
        //un bateau place
        public void EnablePlage()
        {
            for (int i = (int)Convert.ToChar('A'); i <= 74; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    
                    Button BTN_Pos = this.Controls.Find("BTN_" + Convert.ToChar(i) + j, true).FirstOrDefault() as Button;
                    BTN_Pos.Enabled = true;
                    if (BTN_Pos.BackColor == Color.Blue)
                    {
                        BTN_Pos.Enabled = false;
                    }
                    if(BTN_Pos.BackColor == Color.Red || BTN_Pos.BackColor == Color.Green)
                    {
                        BTN_Pos.BackColor = Color.Black;
                    }

                }

            }
        }
        //Cette fonction s'occupe de donner les differents obtions disponible au joueur pour placer son bateau
        public void ChoisirPositionBateau(string Lettre, string Nombre, int NombreCase)
        {
            bool abord = false;
            //Si le bateau peu etre placer vers la gauche signifie que le la position choisi moin le grandeur du bateau - 1 est >= 0 
            if (Convert.ToInt16(Nombre) - NombreCase >= -1)
            {
                //Cette boucle tourne temp et aussi longtemp que la boucle atteind pas une case bleu 
                //Ou que la boucle a atteint sa limite de longeur du bateau moin 1 
                for (int i = Convert.ToInt16(Nombre); i >= Convert.ToInt16(Nombre) - (NombreCase-1) && abord == false; i--)
                {
                    Button BTN_Pos = this.Controls.Find("BTN_" + Lettre + Convert.ToString(i), true).FirstOrDefault() as Button;
                    if(BTN_Pos.BackColor == Color.Blue)
                    {
                        abord = true;
                    }
                    else { 
                        BTN_Pos.BackColor = Color.Green;
                        BTN_Pos.Enabled = false;
                        if (i == Convert.ToInt16(Nombre) - (NombreCase - 1))
                        {
                            BTN_Pos.BackColor = Color.Red;
                            BTN_Pos.Enabled = true;
                        }
                    }
                }
            }
            //Si le bateau peu etre placer vers la droite signifie que la position choisi plus la grandeur du bateau - 1 est <= 9  
            if (Convert.ToInt16(Nombre) + NombreCase <= 10)
            {
                //Cette boucle tourne temp et aussi longtemp que la boucle atteind pas une case bleu 
                //Ou que la boucle a atteint sa limite de longeur du bateau moin 1 
                for (int i = Convert.ToInt16(Nombre); i <= Convert.ToInt16(Nombre) + (NombreCase - 1) && abord == false; i++)
                {
                    Button BTN_Pos = this.Controls.Find("BTN_" + Lettre + Convert.ToString(i), true).FirstOrDefault() as Button;
                    if (BTN_Pos.BackColor == Color.Blue)
                    {
                        abord = true;
                    }
                    else
                    {
                        BTN_Pos.BackColor = Color.Green;
                        BTN_Pos.Enabled = false;
                        if (i == Convert.ToInt16(Nombre) + (NombreCase - 1))
                        {
                            BTN_Pos.BackColor = Color.Red;
                            BTN_Pos.Enabled = true;
                        }
                    }
                }
            }
            //Si le bateau peu etre placer vers le bas signifie que la position choisi plus la grandeur du bateau - 1 est <= 74  
            if ((int)Convert.ToChar(Lettre) + NombreCase <= 75)
            {
                //Cette boucle tourne temp et aussi longtemp que la boucle atteind pas une case bleu 
                //Ou que la boucle a atteint sa limite de longeur du bateau moin 1 
                for (int i = (int)Convert.ToChar(Lettre); i <= (int)Convert.ToChar(Lettre) + (NombreCase - 1) && abord == false; i++)
                {

                    Button BTN_Pos = this.Controls.Find("BTN_" + Convert.ToChar(i) + Nombre, true).FirstOrDefault() as Button;
                    if (BTN_Pos.BackColor == Color.Blue)
                    {
                        abord = true;
                    }
                    else
                    {
                        BTN_Pos.BackColor = Color.Green;
                        BTN_Pos.Enabled = false;
                        if (i == (int)Convert.ToChar(Lettre) + (NombreCase - 1))
                        {
                            BTN_Pos.BackColor = Color.Red;
                            BTN_Pos.Enabled = true;
                        }
                    }
                }
            }
            //Si le bateau peu etre placer vers le haut signifie que la position choisi plus la grandeur du bateau - 1 est >= 65  
            if ((int)Convert.ToChar(Lettre) - NombreCase >= 64)
            {
                //Cette boucle tourne temp et aussi longtemp que la boucle atteind pas une case bleu 
                //Ou que la boucle a atteint sa limite de longeur du bateau moin 1 
                for (int i = (int)Convert.ToChar(Lettre); i >= (int)Convert.ToChar(Lettre) - (NombreCase - 1) && abord == false; i--)
                {
                    Button BTN_Pos = this.Controls.Find("BTN_" + Convert.ToChar(i) + Nombre, true).FirstOrDefault() as Button;
                    if (BTN_Pos.BackColor == Color.Blue)
                    {
                        abord = true;
                    }
                    else
                    {
                        BTN_Pos.BackColor = Color.Green;
                        BTN_Pos.Enabled = false;
                        if (i == (int)Convert.ToChar(Lettre) - (NombreCase - 1))
                        {
                            BTN_Pos.BackColor = Color.Red;
                            BTN_Pos.Enabled = true;
                        }
                    }
                }
            }



        }


    }
}
