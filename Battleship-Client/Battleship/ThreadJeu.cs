using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Drawing;

namespace Battleship
{
    class ThreadJeu
    {
        TcpClient unClient;
        NetworkStream netStream;
        string PositionRecu = "";
        PlancheJeu unForm;
        Flotte uneFlotte;

        public ThreadJeu(TcpClient client, NetworkStream Stream, PlancheJeu form, Flotte flotte)
        {
            unClient = client;
            netStream = Stream;
            unForm = form;
            uneFlotte = flotte;
        }

        public void Demarrer()
        {
            if (unForm.InvokeRequired)
                unForm.Invoke(new MethodInvoker(delegate { unForm.PN_Cache_2.Visible = false; }));

            byte[] bytes = new byte[unClient.ReceiveBufferSize];

            netStream.Read(bytes, 0, (int)unClient.ReceiveBufferSize);

            PositionRecu = Encoding.UTF8.GetString(bytes);

            RecevoirTouche(PositionRecu);
        }

        public string GetPosition()
        {
            return PositionRecu;
        }

        private void RecevoirTouche(string Position)
        {
            string verifierfin = Position.Substring(0, 5);
            Position = Position.Replace(verifierfin + "/", "");
            if (Position != "")
            {
                AnalyseTouche(Position);
            }

            if (unForm.InvokeRequired)
                unForm.Invoke(new MethodInvoker(delegate
                {
                    unForm.LB_Tour.Text = "C'est à vous !";

                    if (VerifierPartiFini())
                    {
                        unForm.Close();
                    }

                    unForm.Refresh();
                }));

        }

        private bool VerifierPartiFini()
        {
            int cpt = 0;
            if (unForm.LB_A_1.BackColor == Color.FromArgb(255, 128, 128))
            {
                cpt++;
            }
            if (unForm.LB_A_2.BackColor == Color.FromArgb(255, 128, 128))
            {
                cpt++;
            }
            if (unForm.LB_A_3.BackColor == Color.FromArgb(255, 128, 128))
            {
                cpt++;
            }
            if (unForm.LB_A_4.BackColor == Color.FromArgb(255, 128, 128))
            {
                cpt++;
            }
            if (unForm.LB_A_5.BackColor == Color.FromArgb(255, 128, 128))
            {
                cpt++;
            }

            if(cpt == 5)
            {
                return true;
            }

            return false;
        }

        public void AnalyseTouche(string touche)
        {
            int index = touche.IndexOf('/');
            int indexb = touche.IndexOf('\0');
            string avant = touche.Substring(0, index);
            string apres = touche.Substring(index + 1, indexb - index - 1);
            if (index > 0)
            {
                if (apres != "aucun")
                {
                    AnalyseBateau(apres);
                }
            }

            touche = touche.Substring(0, 2);
            Button btn = new Button();
            if (unForm.InvokeRequired)
                unForm.Invoke(new MethodInvoker(delegate { btn = unForm.Controls.Find("BTN_" + touche + "_A", true).FirstOrDefault() as Button; }));


            if (VerifierFlotte(touche))
            {
                CreatePanelOverButton(unForm.PN_Joueur, touche, Battleship.Properties.Resources.Explosion_Fire, btn);
            }
            else
            {
                CreatePanelOverButton(unForm.PN_Joueur, touche, Battleship.Properties.Resources.WaterExplosion, btn);
            }
        }

        private bool VerifierFlotte(string touche)
        {
            bool toucher = false;

            char lettre = char.Parse(touche.Substring(0, 1));
            int chiffre = int.Parse(touche.Substring(1, 1));

            toucher = uneFlotte.VerifierTouche(lettre, chiffre);

            return toucher;
        }

        private void AnalyseBateau(string unbateau)
        {
            if (unForm.InvokeRequired)
                unForm.Invoke(new MethodInvoker(delegate
                {
                    if (unbateau == "BattleShip")
                    {
                        unForm.LB_A_1.BackColor = Color.FromArgb(255, 128, 128);
                    }
                    if (unbateau == "Destroyer")
                    {
                        unForm.LB_A_2.BackColor = Color.FromArgb(255, 128, 128);
                    }
                    if (unbateau == "AircraftCarrier")
                    {
                        unForm.LB_A_3.BackColor = Color.FromArgb(255, 128, 128);
                    }
                    if (unbateau == "Submarine")
                    {
                        unForm.LB_A_4.BackColor = Color.FromArgb(255, 128, 128);
                    }
                    if (unbateau == "PatrolBoat")
                    {
                        unForm.LB_A_5.BackColor = Color.FromArgb(255, 128, 128);
                    }
                }));
        }

        private void CreatePanelOverButton(Panel unPanel, string name, Bitmap Image, object sender)
        {
            try
            {
                if (unForm.InvokeRequired)
                    unForm.Invoke(new MethodInvoker(delegate
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
                    }));
            }
            catch (Exception ext)
            {
                MessageBox.Show(ext.ToString());
            }
        }
    }
}