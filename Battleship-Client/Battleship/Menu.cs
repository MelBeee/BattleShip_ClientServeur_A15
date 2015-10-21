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

        private void flashButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void flashButton1_Click(object sender, EventArgs e)
        {   
            try
            {
                SeConnecter();

                Choisir_Position form = new Choisir_Position();

                form.ShowDialog();           
            }catch(Exception ex)
            {
                MessageBox.Show("Le serveur n'est pas encore ouvert");

            }   
        }
        private void SeConnecter()
        {
                client = new TcpClient();
                client.Connect("127.0.0.1", 1234);                      
        }
    }
}
