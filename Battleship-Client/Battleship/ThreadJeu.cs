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
            byte[] bytes = new byte[unClient.ReceiveBufferSize];

            netStream.Read(bytes, 0, (int)unClient.ReceiveBufferSize);

            PositionRecu = Encoding.UTF8.GetString(bytes);

            RecevoirTouche(PositionRecu);

            if (unForm.InvokeRequired)
                unForm.Invoke(new MethodInvoker(delegate { unForm.PN_Joueur.Enabled = true; }));
        }

        public string GetPosition()
        {
            return PositionRecu;
        }

        private void RecevoirTouche(string Position)
        {
            if (Position == "Perdu" || Position == "Gagné")
            {
                AfficherMessageFin(Position);
                unForm.Close();
            }
            else
            {
                if (Position != "")
                {
                    AnalyseTouche(Position);
                }
            }
        }

        private void AfficherMessageFin(string resultat)
        {
            if (resultat == "Perdu")
            {
                //MessageBox.Show("DÉSOLÉ, VOUS AVEZ PERDU !!");
            }
            else if (resultat == "Gagné")
            {
                //MessageBox.Show("FÉLICITATION, VOUS AVEZ GAGNÉ !!");
            }
        }

        public void AnalyseTouche(string touche)
        {
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
            if (unForm.InvokeRequired)
                unForm.Invoke(new MethodInvoker(delegate { unForm.Refresh(); }));
        }

        private bool VerifierFlotte(string touche)
        {
            bool toucher = false;

            char lettre = char.Parse(touche.Substring(0, 1));
            int chiffre = int.Parse(touche.Substring(1, 1));

            toucher = uneFlotte.VerifierTouche(lettre, chiffre);
            AnalyseBateau();

            return toucher;
        }

        private void AnalyseBateau()
        {
            if (unForm.InvokeRequired)
                unForm.Invoke(new MethodInvoker(delegate
                {
                    if (uneFlotte.BattleShip.Detruit)
                    {
                        unForm.LB_A_1.BackColor = Color.FromArgb(255, 128, 128);
                    }
                    if (uneFlotte.Destroyeur.Detruit)
                    {
                        unForm.LB_A_2.BackColor = Color.FromArgb(255, 128, 128);
                    }
                    if (uneFlotte.Aircraft.Detruit)
                    {
                        unForm.LB_A_3.BackColor = Color.FromArgb(255, 128, 128);
                    }
                    if (uneFlotte.Submarine.Detruit)
                    {
                        unForm.LB_A_4.BackColor = Color.FromArgb(255, 128, 128);
                    }
                    if (uneFlotte.Patrol.Detruit)
                    {
                        unForm.LB_A_5.BackColor = Color.FromArgb(255, 128, 128);
                    }
                    unForm.Refresh();
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