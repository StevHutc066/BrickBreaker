using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuperSnakeGame.Screens;

namespace SuperSnakeGame
{
    public partial class Form1 : Form
    {
        public static List<Block> blocks = new List<Block>();
        public static string WinOrLose = "Begin Game";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Program goes directly to the GameScreen method on start
            PlayScreen ps = new PlayScreen();
            this.Controls.Add(ps);
            ps.Location = new Point((this.Width - ps.Width) / 2, (this.Height - ps.Height) / 2);
        }
    }
}
