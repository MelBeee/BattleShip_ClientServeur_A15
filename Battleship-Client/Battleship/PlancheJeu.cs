using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{
    public partial class PlancheJeu : Form
    {
        char[] LetterArray = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
        int[] NumberArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        Flotte maFlotte;

        public PlancheJeu(/*Flotte uneFlotte*/)
        {
            InitializeComponent();

            InitializeFlotte();
            //maFlotte = new Flotte(uneFlotte);
        }

        private void PlancheJeu_Load(object sender, EventArgs e)
        {
            LoadPlan(PN_Ennemi, "_E");
            LoadPlan(PN_Joueur, "_J");


        }
        private void SetLesTrucs(int index, char lettre, int nombre, bool etat, Position[] tab)
        {
            tab[index].letter = lettre;
            tab[index].number = nombre;
            tab[index].touche = etat;
        }

        private void InitializeFlotte()
        {
            Position[] tabAircraft = new Position[3]; // 3
            SetLesTrucs(0, 'A', 0, false, tabAircraft);
            SetLesTrucs(1, 'A', 1, false, tabAircraft);
            SetLesTrucs(2, 'A', 2, false, tabAircraft);

            Position[] tabBattleShip = new Position[5]; // 5
            SetLesTrucs(0, 'B', 0, false, tabBattleShip);
            SetLesTrucs(1, 'B', 1, false, tabBattleShip);
            SetLesTrucs(2, 'B', 2, false, tabBattleShip);
            SetLesTrucs(3, 'B', 3, false, tabBattleShip);
            SetLesTrucs(4, 'B', 4, false, tabBattleShip);

            Position[] tabDestroyeur = new Position[4]; // 4
            SetLesTrucs(0, 'C', 0, false, tabDestroyeur);
            SetLesTrucs(1, 'C', 1, false, tabDestroyeur);
            SetLesTrucs(2, 'C', 2, false, tabDestroyeur);
            SetLesTrucs(3, 'C', 3, false, tabDestroyeur);

            Position[] tabSubmarine = new Position[3]; // 3
            SetLesTrucs(0, 'D', 0, false, tabSubmarine);
            SetLesTrucs(1, 'D', 1, false, tabSubmarine);
            SetLesTrucs(2, 'D', 2, false, tabSubmarine);

            Position[] tabPatrol = new Position[2]; // 2
            SetLesTrucs(0, 'E', 0, false, tabPatrol);
            SetLesTrucs(1, 'E', 1, false, tabPatrol);

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
                    unBouton.Name = "BTN_" + LetterArray[y].ToString() + (i + 1).ToString() + unString;
                    unBouton.Click += new EventHandler(this.BTN_uneAction_Click);

                    if (unPanel == PN_Joueur)
                    {
                        unBouton.Enabled = false;
                    }
                }
            }
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
            catch(Exception ext)
            {
                MessageBox.Show(ext.ToString());
            }
        }

        private void ThreadJeu()
        {
            // Attendre le mouvement de l'autre joueur 
        }

        private bool VerifierTouche(string name)
        {
            char lettre = char.Parse(name.Substring(0, 1));
            int nombre = int.Parse(name.Substring(1, 1));
            bool touche = false;

            // ici j'envois la position 

            return touche;
        }

        private bool RecevoirTouche()
        {
            bool touche = false;

            // ici je revois la position

            return touche;
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

        private void BTN_Quit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Etes vous sur de vouloir quitter la partie en cours ? ", "Attention !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void BTN_NewGame_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Etes vous sur de vouloir quitter la partie en cours pour en recommencer une nouvelle ? ", "Attention !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Visible = false;
                Choisir_Position unForm = new Choisir_Position();
                unForm.ShowDialog();
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

        private void button1_Click(object sender, EventArgs e)
        {
            LoadMesBateaux();
        }
    }
}
