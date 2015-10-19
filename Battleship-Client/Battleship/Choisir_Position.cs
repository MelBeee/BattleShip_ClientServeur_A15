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
            planche.Show();
            this.Close();
        }
    }
}
