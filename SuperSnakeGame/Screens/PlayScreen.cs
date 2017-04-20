using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperSnakeGame.Screens
{
    public partial class PlayScreen : UserControl
    {
        public PlayScreen()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            // Goes to the game screen
            GameScreen gs = new GameScreen();
            Form form = this.FindForm(); 
            form.Controls.Add(gs);
            form.Controls.Remove(this);
            gs.Location = new Point((form.Width - gs.Width) / 2, (form.Height - gs.Height) / 2);
        }

        private void playButton_Enter(object sender, EventArgs e)
        {
            playButton.BackColor = Color.Green;
            exitButton.BackColor = Color.Lime;
        }

        private void exitButton_Enter(object sender, EventArgs e)
        {
            playButton.BackColor = Color.Lime;
            exitButton.BackColor = Color.Green;
        }

        private void PlayScreen_Load(object sender, EventArgs e)
        {
            winLoseLabel.Text = Form1.WinOrLose;
            if (GameScreen.ticks > 0)
                 scoreLabel.Text = "Score: " + GameScreen.ticks;
        }
    }
}
