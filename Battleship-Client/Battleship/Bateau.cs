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
        public string nom;
        public int NbreCases;
        public int NbreCasesTouche;
        public bool Detruit;
        public Position[] Tab;

        public Bateau(TypeBateau unType, int unNbre, bool unEtat, int unNbreCaseTouche, Position[] Tableau, string unnom)
        {
            leType = unType;
            NbreCases = unNbre;
            Detruit = unEtat;
            NbreCasesTouche = unNbreCaseTouche;
            Tab = Tableau;
            nom = unnom;
        }
    }
}
