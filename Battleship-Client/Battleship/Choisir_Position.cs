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
            BTN_Start.Enabled = false;
        }

        private void BTN_Start_Click(object sender, EventArgs e)
        {
            StringACharlie = BattleShip + PatrolBoat + Destroyer + Submarine + AircraftCarrier;
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
            if (PassPosition != null)
            {
                if (PassPosition.BackColor != Color.Blue)
                {
                    PassPosition.BackColor = Color.Black;
                }
            }
            PassPosition = (Button)sender;
            string Position_name = Position_Choisi.Name;

            string Position = "";
            Position = Position_name.Substring(4, 2);
            Lettre_ = Position.Substring(0,1);
            Nombre_ = Position.Substring(1, Position.Length - 1);


            if (PositionStart == null)
            {
                PositionStart = Lettre_ + Nombre_;
                BateauPlacer = false;
                
            }
            else if(PositionStart != null)
            {
                
                PositionFin = Lettre_ + Nombre_;
                if (RB_PortAvion.Checked)
                {
                    MettreEnPlace(PositionStart, PositionFin, 5, "BattleShip");
                    RB_PortAvion.Enabled = false;
                    RB_PortAvion.Checked = false;
                }
                else if (RB_Croiseur.Checked)
                {
                    MettreEnPlace(PositionStart, PositionFin, 4, "Destroyer");
                    RB_Croiseur.Enabled = false;
                    RB_Croiseur.Checked = false;
                }
                else if (RB_ContreTorpilleur.Checked)
                {
                    MettreEnPlace(PositionStart, PositionFin, 3, "AircraftCarrier");
                    RB_ContreTorpilleur.Enabled = false;
                    RB_ContreTorpilleur.Checked = false;
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
                }
                EnablePlage();
                PositionStart = null;
                if(CompteurBateau == 5)
                {
                    BTN_Start.Enabled = true;
                }
            }

            if (BateauPlacer == false)
            {
                if (RB_PortAvion.Checked)
                {
                    ChoisirPositionBateau(Lettre_, Nombre_, 5);
                    DisablePlage();
                }
                else if (RB_Croiseur.Checked)
                {
                    ChoisirPositionBateau(Lettre_, Nombre_, 4);
                    DisablePlage();
                }
                else if (RB_ContreTorpilleur.Checked)
                {
                    ChoisirPositionBateau(Lettre_, Nombre_, 3);
                    DisablePlage();
                }
                else if (RB_Torpilleur.Checked)
                {
                    ChoisirPositionBateau(Lettre_, Nombre_, 2);
                    DisablePlage();
                }
                else if (RB_SousMarin.Checked)
                {
                    ChoisirPositionBateau(Lettre_, Nombre_, 3);
                    DisablePlage();
                }
                
            }

        }
        public void MettreEnPlace(string PosStart, string PosFin ,int GrosseurBateau, string NomBateau)
        {
            string StartLettre_;
            string StartNombre_;

            string FinLettre_;
            string FinNombre_;

            StartLettre_ = PosStart.Substring(0, 1);
            StartNombre_ = PosStart.Substring(1, PosStart.Length - 1);

            FinLettre_ = PosFin.Substring(0, 1);
            FinNombre_ = PosFin.Substring(1, PosFin.Length - 1);

            if((int)Convert.ToChar(StartLettre_) == (int)Convert.ToChar(FinLettre_))
            {
                
                if(Convert.ToInt16(StartNombre_) < Convert.ToInt16(FinNombre_))
                {
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
                if (Convert.ToInt16(StartNombre_) > Convert.ToInt16(FinNombre_))
                {
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
            if((int)Convert.ToChar(StartLettre_) != (int)Convert.ToChar(FinLettre_))
            {
                if ((int)Convert.ToChar(StartLettre_) < (int)Convert.ToChar(FinLettre_))
                {
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
                if ((int)Convert.ToChar(StartLettre_) > (int)Convert.ToChar(FinLettre_))
                {
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
            CompteurBateau++;
            BateauPlacer = true;

        }
        public void DisablePlage()
        {
            for (int i = (int)Convert.ToChar('A'); i <= 74; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    Button BTN_Pos = this.Controls.Find("BTN_" + Convert.ToChar(i) + j, true).FirstOrDefault() as Button;
                    BTN_Pos.Enabled = false;
                    if (BTN_Pos.BackColor == Color.Red)
                    {
                        BTN_Pos.Enabled = true;
                    }

                }

            }
        }
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
        public void ChoisirPositionBateau(string Lettre, string Nombre, int NombreCase)
        {
            bool abord = false;
            if (Convert.ToInt16(Nombre) - NombreCase >= -1)
            {
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
            if (Convert.ToInt16(Nombre) + NombreCase <= 10)
            {
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
            if ((int)Convert.ToChar(Lettre) + NombreCase <= 75)
            {
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
            if ((int)Convert.ToChar(Lettre) - NombreCase >= 64)
            {
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
