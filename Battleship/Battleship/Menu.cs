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
    }
}
