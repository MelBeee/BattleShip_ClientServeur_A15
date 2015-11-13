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
   
    public partial class Menu : Form
    {
        TcpClient client;
        public Menu()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void SeConnecter()
        {
                client = new TcpClient();
                //client.Connect("172.17.104.107", 1234);
                client.Connect("172.17.104.112", 1234);
                //client.Connect("127.0.0.1", 1234);        
                
        }

        private void BTN_Quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTN_Start_Click(object sender, EventArgs e)
        {
            try
            {
                SeConnecter();

                Choisir_Position form = new Choisir_Position(client);

                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Le serveur n'est pas encore ouvert");
            }   
        }

        bool button;

        private void BTN_Start_MouseDown(object sender, MouseEventArgs e)
        {
            if (button)
                BTN_Start.BackColor = Color.LightSlateGray;
            else
                BTN_Quit.BackColor = Color.LightSlateGray;
        }

        private void BTN_Start_MouseEnter(object sender, EventArgs e)
        {
            BTN_Start.BackColor = Color.LightSteelBlue;
            button = true; 
        }

        private void BTN_Start_MouseHover(object sender, EventArgs e)
        {
            if (button)
                BTN_Start.BackColor = Color.LightSteelBlue;
            else
                BTN_Quit.BackColor = Color.LightSteelBlue;       
        }

        private void BTN_Start_MouseLeave(object sender, EventArgs e)
        {
            if (button)
                BTN_Start.BackColor = Color.Transparent;
            else
                BTN_Quit.BackColor = Color.Transparent;
        }

        private void BTN_Start_MouseMove(object sender, MouseEventArgs e)
        {
            if (button)
                BTN_Start.BackColor = Color.LightSteelBlue;
            else
                BTN_Quit.BackColor = Color.LightSteelBlue;
        }

        private void BTN_Start_MouseUp(object sender, MouseEventArgs e)
        {
            if (button)
                BTN_Start.BackColor = Color.LightSteelBlue;
            else
                BTN_Quit.BackColor = Color.LightSteelBlue;
        }

        private void BTN_Quit_MouseEnter(object sender, EventArgs e)
        {
            BTN_Quit.BackColor = Color.LightSteelBlue;
            button = false; 
        }
    }
}
