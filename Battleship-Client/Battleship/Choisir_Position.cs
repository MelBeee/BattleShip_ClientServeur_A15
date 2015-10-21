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
    public partial class Choisir_Position : Form
    {
        public Choisir_Position()
        {
            InitializeComponent();
        }

        Flotte maFlotte;

        private void Choisir_Position_Load(object sender, EventArgs e)
        {
            if(maFlotte!= null)
            {
                Position[] tabAircraft = new Position[maFlotte.Aircraft.NbreCases]; // 3
                SetLesTrucs(0, 'A', 0, false, tabAircraft);
                SetLesTrucs(1, 'A', 1, false, tabAircraft);
                SetLesTrucs(2, 'A', 2, false, tabAircraft);
                maFlotte.Aircraft.Tab = tabAircraft;

                Position[] tabBattleShip = new Position[maFlotte.BattleShip.NbreCases]; // 5
                SetLesTrucs(0, 'B', 0, false, tabBattleShip);
                SetLesTrucs(1, 'B', 1, false, tabBattleShip);
                SetLesTrucs(2, 'B', 2, false, tabBattleShip);
                SetLesTrucs(3, 'B', 3, false, tabBattleShip);
                SetLesTrucs(4, 'B', 4, false, tabBattleShip);
                maFlotte.BattleShip.Tab = tabBattleShip;

                Position[] tabDestroyeur = new Position[maFlotte.Destroyeur.NbreCases]; // 4
                SetLesTrucs(0, 'C', 0, false, tabDestroyeur);
                SetLesTrucs(1, 'C', 1, false, tabDestroyeur);
                SetLesTrucs(2, 'C', 2, false, tabDestroyeur);
                SetLesTrucs(3, 'C', 3, false, tabDestroyeur);
                maFlotte.Destroyeur.Tab = tabDestroyeur;

                Position[] tabSubmarine = new Position[maFlotte.Submarine.NbreCases]; // 3
                SetLesTrucs(0, 'D', 0, false, tabDestroyeur);
                SetLesTrucs(1, 'D', 1, false, tabDestroyeur);
                SetLesTrucs(2, 'D', 2, false, tabDestroyeur);
                maFlotte.Submarine.Tab = tabSubmarine;

                Position[] tabPatrol = new Position[maFlotte.Patrol.NbreCases]; // 2
                SetLesTrucs(0, 'E', 0, false, tabDestroyeur);
                SetLesTrucs(1, 'E', 1, false, tabDestroyeur);
                maFlotte.Patrol.Tab = tabPatrol;
            }
            
        }

        private void SetLesTrucs(int index, char lettre, int nombre, bool etat, Position[] tab)
        {
            tab[index].letter = lettre;
            tab[index].number = nombre;
            tab[index].touche = etat;
        }

        private void BTN_Start_Click(object sender, EventArgs e)
        {
            PlancheJeu planche = new PlancheJeu(maFlotte);
            this.Visible = false;
            planche.ShowDialog();
            this.Close();
        }

        private void BTN_A1_Click(object sender, EventArgs e)
        {

        }
    }
}
