using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public enum TypeBateau
    {
        Destroyer,
        BattleShip,
        AircraftCarrier,
        Submarine,
        PatrolBoat
    }

    public struct Position
    {
        public int number;
        public char letter;
        public bool touche;
    }

    public class Bateau
    {
        public TypeBateau leType;
        public int NbreCases;
        public int NbreCasesTouche;
        public bool Detruit;
        public Position[] Tab;

        public Bateau(TypeBateau unType, int unNbre, bool unEtat, int unNbreCaseTouche, Position[] Tableau)
        {
            leType = unType;
            NbreCases = unNbre;
            Detruit = unEtat;
            NbreCasesTouche = unNbreCaseTouche;
            Tab = Tableau;
        }

        public void UpdateDetruit()
        {
            for(int i = 0; i < NbreCases || !Detruit ; i++)
            {
                if (Tab[i].touche)
                    Detruit = true;
                else
                    Detruit = false;
            }
        }
    }
}
