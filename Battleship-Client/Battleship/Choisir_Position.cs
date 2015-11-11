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
        public Button PassPosition;
        public Choisir_Position()
        {
            InitializeComponent();
        }

        private void Choisir_Position_Load(object sender, EventArgs e)
        {
           
            
        }

        private void BTN_Start_Click(object sender, EventArgs e)
        {
            PlancheJeu planche = new PlancheJeu();
            this.Visible = false;
            planche.ShowDialog();
            this.Close();
        }

        private void BTN_A1_Click(object sender, EventArgs e)
        {
            string Lettre_ ;
            string Nombre_;
            Button Position_Choisi = (Button)sender;
            if (PassPosition != null)
            {
                PassPosition.BackColor = Color.Black;
            }
            PassPosition = (Button)sender;
            string Position_name = Position_Choisi.Name;
            string Position = "";
            Position = Position_name.Substring(4, 2);
            Lettre_ = Position.Substring(0,1);
            Nombre_ = Position.Substring(1, Position.Length - 1);
            Position_Choisi.BackColor = Color.Blue;


            //for (int i = (int)Convert.ToChar("A"); i <= 75; i++)
            //{
            //    for (int j = 1; j <= 10; j++)
            //    {
            //        Button BTN_Pos = this.Controls.Find("BTN_" + Convert.ToChar(i) + j, true).FirstOrDefault() as Button;
            //        if (BTN_Pos.BackColor == Color.Red)
            //        {
                        
            //        }
            //        else if (BTN_Pos.BackColor == Color.Green)
            //        {
            //            BTN_Pos.BackColor = Color.Black;
            //            BTN_Pos.Enabled = true;
            //        }

            //    }

            //}


                if (RB_PortAvion.Checked)
                {
                    PlacerBateau(Lettre_, Nombre_, 5);
                }
                else if (RB_Croiseur.Checked)
                {
                    PlacerBateau(Lettre_, Nombre_, 4);
                }
                else if (RB_ContreTorpilleur.Checked)
                {
                    PlacerBateau(Lettre_, Nombre_, 3);
                }
                else if (RB_Torpilleur.Checked)
                {
                    PlacerBateau(Lettre_, Nombre_, 2);
                }
                else if (RB_SousMarin.Checked)
                {
                    PlacerBateau(Lettre_, Nombre_, 3);
                }

        }
        public void PlacerBateau(string Lettre, string Nombre, int NombreCase)
        {
            if (Convert.ToInt16(Nombre) - NombreCase >= 0)
            {
                for (int i = Convert.ToInt16(Nombre); i >= Convert.ToInt16(Nombre) - (NombreCase-1); i--)
                {
                    Button BTN_Pos = this.Controls.Find("BTN_" + Lettre + Convert.ToString(i), true).FirstOrDefault() as Button;
                    BTN_Pos.BackColor = Color.Green;
                }
            }
            if (Convert.ToInt16(Nombre) + NombreCase <= 11)
            {
                for (int i = Convert.ToInt16(Nombre); i <= Convert.ToInt16(Nombre) + (NombreCase - 1); i++)
                {
                    Button BTN_Pos = this.Controls.Find("BTN_" + Lettre + Convert.ToString(i), true).FirstOrDefault() as Button;
                    BTN_Pos.BackColor = Color.Green;
                }
            }
            if ((int)Convert.ToChar(Lettre) + NombreCase <= 75)
            {
                for (int i = (int)Convert.ToChar(Lettre); i <= (int)Convert.ToChar(Lettre) + (NombreCase - 1); i++)
                {
                    Button BTN_Pos = this.Controls.Find("BTN_" + Convert.ToChar(i) + Nombre, true).FirstOrDefault() as Button;
                    BTN_Pos.BackColor = Color.Green;
                }
            }
            if ((int)Convert.ToChar(Lettre) - NombreCase >= 65)
            {
                for (int i = (int)Convert.ToChar(Lettre); i >= (int)Convert.ToChar(Lettre) - (NombreCase-1); i--)
                {
                    Button BTN_Pos = this.Controls.Find("BTN_" + Convert.ToChar(i) + Nombre, true).FirstOrDefault() as Button;
                    BTN_Pos.BackColor = Color.Green;
                }
            }



        }


    }
}
