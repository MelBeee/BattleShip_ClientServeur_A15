using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public enum Type
    {
        allier,
        ennemi
    }

    public class Flotte
    {
        public Bateau BattleShip;
        public Bateau Destroyeur;
        public Bateau Submarine;
        public Bateau Aircraft;
        public Bateau Patrol;
        Type unType;

        public Flotte(Position[] a_BattleShip, Position[] a_Destroyeur, Position[] a_Submarine, Position[] a_Aircraft, Position[] a_Patrol, Type leType)
        {
            BattleShip = new Bateau(TypeBateau.BattleShip, 5, false, 0, a_BattleShip);
            Destroyeur = new Bateau(TypeBateau.Destroyer, 4, false, 0, a_Destroyeur);
            Submarine = new Bateau(TypeBateau.Submarine, 3, false, 0, a_Submarine);
            Aircraft = new Bateau(TypeBateau.AircraftCarrier, 3, false, 0, a_Aircraft);
            Patrol = new Bateau(TypeBateau.PatrolBoat, 2, false, 0, a_Patrol);
            unType = leType;
        }

        public bool FlotteDetruite()
        {
            if (BattleShip.NbreCasesTouche != BattleShip.NbreCases)
                return false;
            if (Destroyeur.NbreCasesTouche != Destroyeur.NbreCases)
                return false;
            if (Submarine.NbreCasesTouche != Submarine.NbreCases)
                return false;
            if (Aircraft.NbreCasesTouche != Aircraft.NbreCases)
                return false;
            if (Patrol.NbreCasesTouche != Patrol.NbreCases)
                return false; 

            return true;
        }
    }
}
