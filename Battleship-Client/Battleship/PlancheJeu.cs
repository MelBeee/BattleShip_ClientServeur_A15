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

        public PlancheJeu(Flotte uneFlotte)
        {
            InitializeComponent();

            maFlotte = uneFlotte;
        }

        private void PlancheJeu_Load(object sender, EventArgs e)
        {
            LoadPlan(PN_Ennemi, "_F");
            LoadPlan(PN_Joueur, "_S");

            LoadMesBateaux();
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
                unPanel.Location = new Point(GetPosition(lettre) * 35, chiffre * 35);
                unPanel.Height = 35;
                unPanel.Width = 35;
                unPanel.BorderStyle = BorderStyle.FixedSingle;

            }
        }

        private int GetPosition(char uneLettre)
        {
            int Position = 0;

            for (int i = 0; i < LetterArray.Length; i++)
            {
                if(LetterArray[i] == uneLettre)
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
                    unBouton.Name = "BTN_" + (i + 1).ToString() + LetterArray[y].ToString() + unString;

                    if (unPanel == PN_Joueur)
                    {
                        unBouton.Enabled = false;
                    }
                }
            }
        }

        private void BTN_Action_Over(object sender, EventArgs e)
        {

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
    }
}
