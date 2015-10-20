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
        public Menu()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var pos = this.PointToScreen(label1.Location);
            pos = this.PointToClient(pos);
            label1.Parent = this;
            label1.Location = pos;
            label1.BackColor = Color.Transparent;
        }

        private void flashButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void flashButton1_Click(object sender, EventArgs e)
        {
            //SeConnecter();

            Choisir_Position form = new Choisir_Position();

            form.ShowDialog();

            

        }
        private void SeConnecter()
        {
            try
            {
                TcpClient client = new TcpClient();
                client.Connect("172.17.104.101", 1234);
            }
            catch(SocketException ext)
            {
                MessageBox.Show(ext.ToString());
                this.Close();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
               
            }

            
        }
    }
}
