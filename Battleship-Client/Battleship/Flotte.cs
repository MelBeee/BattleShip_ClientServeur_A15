using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public enum TypeFlotte
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
        TypeFlotte unType;

        public Flotte(Position[] a_BattleShip, Position[] a_Destroyeur, Position[] a_Submarine, Position[] a_Aircraft, Position[] a_Patrol, TypeFlotte leType)
        {
            BattleShip = new Bateau(TypeBateau.BattleShip, 5, false, 0, a_BattleShip);
            Destroyeur = new Bateau(TypeBateau.Destroyer, 4, false, 0, a_Destroyeur);
            Submarine = new Bateau(TypeBateau.Submarine, 3, false, 0, a_Submarine);
            Aircraft = new Bateau(TypeBateau.AircraftCarrier, 3, false, 0, a_Aircraft);
            Patrol = new Bateau(TypeBateau.PatrolBoat, 2, false, 0, a_Patrol);
            unType = leType;
        }

        public Flotte(Flotte uneFlotte)
        {
            BattleShip = uneFlotte.BattleShip;
            Destroyeur = uneFlotte.Destroyeur;
            Submarine = uneFlotte.Submarine;
            Aircraft = uneFlotte.Aircraft;
            Patrol = uneFlotte.Patrol;
            unType = uneFlotte.unType;
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

        public bool VerifierTouche(char Lettre, int Nombre)
        {
            bool touche = false;

            if (VerifierBateau(BattleShip, Lettre, Nombre))
                touche = true;
            if (VerifierBateau(Patrol, Lettre, Nombre))
                touche = true;
            if (VerifierBateau(Destroyeur, Lettre, Nombre))
                touche = true;
            if (VerifierBateau(Aircraft, Lettre, Nombre))
                touche = true;
            if (VerifierBateau(Submarine, Lettre, Nombre))
                touche = true; 

            return touche;
        }

        private bool VerifierBateau(Bateau bateau, char lettre, int nombre)
        {
            bool touche = false;
            bool detruit = true;

            for (int i = 0; i < bateau.Tab.Length; i++)
            {
                if (bateau.Tab[i].letter == lettre && bateau.Tab[i].number == nombre)
                {
                    touche = true;
                    bateau.Tab[i].touche = true;
                }

                if (detruit)
                {
                    if (bateau.Tab[i].touche)
                    {
                        detruit = true;
                    }
                    else
                    {
                        detruit = false;
                    }
                }
            }

            if (!detruit)
            {
                bateau.Detruit = true;
            }

            return touche;
        }
    }
}
