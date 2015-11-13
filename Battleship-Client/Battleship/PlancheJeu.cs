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
using System.Net.Sockets;
using System.IO;

namespace Battleship
{
    public partial class PlancheJeu : Form
    {
        char[] LetterArray = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
        int[] NumberArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        Flotte maFlotte;

        TcpClient unClient;
        NetworkStream netStream;
        ThreadJeu unThreadJeu;
        string s_maFlotte;
        bool commencer; 

        public PlancheJeu(string flotte, TcpClient client)
        {
            InitializeComponent();
            unClient = client;
            netStream = unClient.GetStream();
            s_maFlotte = flotte;
            InitializeFlotte();
        }

        private void PlancheJeu_Load(object sender, EventArgs e)
        {
            LoadPlan(PN_Ennemi, "_E");
            LoadPlan(PN_Joueur, "_A");
            LoadMesBateaux();
        }

        private void BoucleJeu()
        {
            string fini = "";

            while (fini == "Perdu")
            {
                if (commencer)
                {
                    // mon tour
                }
                else
                {
                    // SonTour
                }
            }
        }

        private void JouerMonTour()
        {
            // ENABLED TRUE 
        }

        private void JouerSonTour()
        {
            // ENABLED FALSE 

            // J'attend son attaque
        }

        private void DeterminerLeTour()
        {
            string reponse = "";
            if (netStream.CanRead)
            {
                byte[] bytes = new byte[unClient.ReceiveBufferSize];

                netStream.Read(bytes, 0, (int)unClient.ReceiveBufferSize);

                reponse = Encoding.UTF8.GetString(bytes);
            }

            if (reponse.Substring(0, 9) == "StartTour")
            {
                LB_Tour.Text = "C'est à vous !";
                PN_Joueur.Enabled = true;
                commencer = true; 
            }
            else
            {
                LB_Tour.Text = "";
                PN_Joueur.Enabled = false;
                commencer = false; 

                unThreadJeu = new ThreadJeu(unClient, netStream, this, maFlotte);
                Thread unThread = new Thread(new ThreadStart(unThreadJeu.Demarrer));
                unThread.Start();
            }
            this.Refresh();
        }

        private void BTN_uneAction_Click(object sender, EventArgs e)
        {
            Button aClickedButton = (Button)sender;

            aClickedButton.Enabled = false;

            string name = aClickedButton.Name;

            string position = name.Substring(4, 2);


            if (VerifierTouche(position))
            {
                CreatePanelOverButton(PN_Ennemi, name, Battleship.Properties.Resources.Explosion_Fire, sender);
            }
            else
            {
                CreatePanelOverButton(PN_Ennemi, name, Battleship.Properties.Resources.WaterExplosion, sender);
            }

            PN_Joueur.Enabled = false;

            unThreadJeu = new ThreadJeu(unClient, netStream, this, maFlotte);
            Thread unThread = new Thread(new ThreadStart(unThreadJeu.Demarrer));
            unThread.Start();
            this.Refresh();
        }

        private bool VerifierTouche(string name)
        {
            bool touche = false;
            string reponse = "";

            if (netStream.CanWrite)
            {
                Byte[] sendBytes = Encoding.UTF8.GetBytes(name);
                netStream.Write(sendBytes, 0, sendBytes.Length);
                PN_Joueur.Enabled = false;
            }

            if (netStream.CanRead)
            {
                byte[] bytes = new byte[unClient.ReceiveBufferSize];

                netStream.Read(bytes, 0, (int)unClient.ReceiveBufferSize);

                reponse = Encoding.UTF8.GetString(bytes);
            }

            int index = reponse.IndexOf('/');
            int indexb = reponse.IndexOf('\0');
            string avant = reponse.Substring(0, index);
            string apres = reponse.Substring(index + 1, indexb - index - 1);
            if (index > 0)
            {
                if (avant == "true")
                {
                    touche = true;
                }

                if (apres != "aucun")
                {
                    AnalyseBateau(apres);
                }
            }

            return touche;
        }

        private void AnalyseBateau(string nom)
        {
            switch (nom)
            {
                case "BattleShip":
                    LB_E_1.BackColor = Color.FromArgb(255, 128, 128);
                    break;
                case "Destroyer":
                    LB_E_2.BackColor = Color.FromArgb(255, 128, 128);
                    break;
                case "AircraftCarrier":
                    LB_E_3.BackColor = Color.FromArgb(255, 128, 128);
                    break;
                case "Submarine":
                    LB_E_4.BackColor = Color.FromArgb(255, 128, 128);
                    break;
                case "PatrolBoat":
                    LB_E_5.BackColor = Color.FromArgb(255, 128, 128);
                    break;
            }
            this.Refresh();
        }

        private bool VerifierBateau(char lettre, int nombre, Bateau unBateau)
        {
            for (int i = 0; i < unBateau.Tab.Length; i++)
            {
                if (unBateau.Tab[i].letter == lettre && unBateau.Tab[i].number == nombre)
                {
                    unBateau.Tab[i].touche = true;
                    return true;
                }
            }
            return false;
        }

        private string RemoveBoatName(string bateau)
        {
            int index = bateau.IndexOf(':');
            return bateau.Substring(index + 1, bateau.Length - index - 1);
        }

        private void RentrerDansTableau(string bateau, Position[] tableau)
        {
            int count = 0;
            foreach (char c in bateau)
            {
                if (c == '-')
                {
                    count++;
                }
            }
            int index = 0;
            char lettre = ' ';
            int nombre = 0;
            string tempo = "";

            for (int i = 0; i <= count; i++)
            {
                tempo = bateau.Substring(index, 2);
                lettre = char.Parse(tempo.Substring(0, 1));
                nombre = int.Parse(tempo.Substring(1, 1));
                SetLesTrucs(i, lettre, nombre, false, tableau);

                index += 3;
            }
        }

        private void SetLesTrucs(int index, char lettre, int nombre, bool etat, Position[] tab)
        {
            tab[index].letter = lettre;
            tab[index].number = nombre;
            tab[index].touche = etat;
        }

        private void InitializeFlotte()
        {
            string battleship = RemoveBoatName(s_maFlotte.Substring(0, 25));
            string patrolboat = RemoveBoatName(s_maFlotte.Substring(26, 16));
            string destroyer = RemoveBoatName(s_maFlotte.Substring(43, 21));
            string submarine = RemoveBoatName(s_maFlotte.Substring(65, 18));
            string aircraft = RemoveBoatName(s_maFlotte.Substring(84, 24));

            Position[] tabBattleShip = new Position[5];
            Position[] tabPatrol = new Position[2];
            Position[] tabDestroyeur = new Position[4];
            Position[] tabSubmarine = new Position[3];
            Position[] tabAircraft = new Position[3];

            RentrerDansTableau(battleship, tabBattleShip);
            RentrerDansTableau(patrolboat, tabPatrol);
            RentrerDansTableau(destroyer, tabDestroyeur);
            RentrerDansTableau(submarine, tabSubmarine);
            RentrerDansTableau(aircraft, tabAircraft);

            maFlotte = new Flotte(tabAircraft, tabBattleShip, tabDestroyeur, tabSubmarine, tabPatrol, TypeFlotte.allier);
        }

        private void LoadMesBateaux()
        {
            AfficheUnBateau(maFlotte.BattleShip);
            AfficheUnBateau(maFlotte.Destroyeur);
            AfficheUnBateau(maFlotte.Aircraft);
            AfficheUnBateau(maFlotte.Patrol);
            AfficheUnBateau(maFlotte.Submarine);
        }

        private void AfficheUnBateau(Bateau unBateau)
        {
            char lettre;
            int chiffre;
            Panel unPanel;
            for (int i = 0; i < unBateau.Tab.Length; i++)
            {
                lettre = unBateau.Tab[i].letter;
                chiffre = unBateau.Tab[i].number;

                unPanel = new Panel();
                unPanel.BackgroundImage = new Bitmap(Battleship.Properties.Resources.Bateau);
                unPanel.Parent = PN_Joueur;
                unPanel.Location = new Point(chiffre * 35, GetPosition(lettre) * 35);
                unPanel.Height = 35;
                unPanel.Width = 35;
                unPanel.BorderStyle = BorderStyle.FixedSingle;
                unPanel.BackgroundImageLayout = ImageLayout.Stretch;
                unPanel.BringToFront();
            }
        }

        private int GetPosition(char uneLettre)
        {
            int Position = 0;

            for (int i = 0; i < LetterArray.Length; i++)
            {
                if (LetterArray[i] == uneLettre)
                {
                    Position = NumberArray[i];
                }
            }

            return Position;
        }

        private void LoadPlan(Panel unPanel, string unString)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int y = 0; y < 10; y++)
                {
                    Button unBouton = new Button();
                    unBouton.FlatStyle = FlatStyle.Flat;
                    unBouton.FlatAppearance.BorderSize = 1;
                    unBouton.FlatAppearance.BorderColor = Color.Azure;
                    unBouton.Height = 35;
                    unBouton.Width = 35;
                    unBouton.BackgroundImage = new Bitmap(Battleship.Properties.Resources.WaterTile);
                    unBouton.BackgroundImageLayout = ImageLayout.Stretch;
                    unBouton.Parent = unPanel;
                    unBouton.Location = new Point(i * 35, y * 35);
                    unBouton.Name = "BTN_" + LetterArray[y].ToString() + (i).ToString() + unString;
                    unBouton.Click += new EventHandler(this.BTN_uneAction_Click);

                    if (unPanel == PN_Joueur)
                    {
                        unBouton.Enabled = false;
                    }
                }
            }
        }

        private void CreatePanelOverButton(Panel unPanel, string name, Bitmap Image, object sender)
        {
            try
            {
                Button unBouton = (Button)sender;

                PictureBox unPB = new PictureBox();
                unPB.BorderStyle = BorderStyle.FixedSingle;
                unPB.Height = 35;
                unPB.Width = 35;
                unPB.BackgroundImage = Image;
                unPB.BackgroundImageLayout = ImageLayout.Stretch;
                unPB.Parent = unPanel;
                unPB.Location = unBouton.Location;
                unPB.BringToFront();
                unPB.BackColor = Color.Transparent;
            }
            catch (Exception ext)
            {
                MessageBox.Show(ext.ToString());
            }
        }

        private void BTN_Quit_Click(object sender, EventArgs e)
        {
            FermerForm();
        }

        private void BTN_NewGame_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Etes vous sur de vouloir quitter la partie en cours pour en recommencer une nouvelle ? ", "Attention !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void BTN_NewGame_MouseEnter(object sender, EventArgs e)
        {
            BTN_NewGame.BackColor = Color.Gray;
        }

        private void BTN_NewGame_MouseLeave(object sender, EventArgs e)
        {
            BTN_NewGame.BackColor = Color.Transparent;
        }

        private void BTN_Quit_MouseEnter(object sender, EventArgs e)
        {
            BTN_Quit.BackColor = Color.Gray;
        }

        private void BTN_Quit_MouseLeave(object sender, EventArgs e)
        {
            BTN_Quit.BackColor = Color.Transparent;
        }

        private void PlancheJeu_FormClosing(object sender, FormClosingEventArgs e)
        {
            FermerForm();
        }

        private void FermerForm()
        {
            if (MessageBox.Show("Etes vous sur de vouloir quitter la partie en cours ? ", "Attention !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void PlancheJeu_Shown(object sender, EventArgs e)
        {
            DeterminerLeTour();
        }
    }
}
