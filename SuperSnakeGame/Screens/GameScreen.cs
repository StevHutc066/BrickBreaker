/*  Created by: Steven HL
 *  Project: Brick Breaker
 *  Date: Tuesday, April 4th
 */ 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace SuperSnakeGame.Screens
{
    public partial class GameScreen : UserControl
    {
        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }

        #region Values
        //player1 button control keys - DO NOT CHANGE
        Boolean leftArrowDown, downArrowDown, rightArrowDown, upArrowDown, spaceDown;

        // Game values
        public int lives;
        public static int ticks;
        
        // Paddle values
        public static int paddleSpeed, paddleX, paddleY;
        public static int paddleLength = 80;
        public static int paddleHeight = 20;

        // Ball values
        public int ballX;
        public int ballY;

        // Brushes
        SolidBrush paddleBrush = new SolidBrush(Color.White);
        SolidBrush ballBrush = new SolidBrush(Color.White);
        SolidBrush blockBrush = new SolidBrush(Color.Red);

        // Random number generator
        Random random = new Random();

        // Creats a ball
        public Ball ball;

        // Sound players
        public static SoundPlayer wallPlatCollision = new SoundPlayer(SuperSnakeGame.Properties.Resources.WallPlatCollision);
        SoundPlayer brickBreak = new SoundPlayer(SuperSnakeGame.Properties.Resources.BrickBreak1);
        SoundPlayer ballLost = new SoundPlayer(SuperSnakeGame.Properties.Resources.BallLost);
        SoundPlayer winSound = new SoundPlayer(SuperSnakeGame.Properties.Resources.WinSound);
        SoundPlayer loseSound = new SoundPlayer(SuperSnakeGame.Properties.Resources.GameOver);
        #endregion

        public void OnStart()
        {
            #region Setting Values
            lives = 3;
            ticks = 0;
            leftArrowDown = downArrowDown = rightArrowDown = upArrowDown = spaceDown = false;

            paddleX = ((this.Width / 2) - (paddleLength / 2));
            paddleY = (this.Height - paddleHeight) - 60;
            paddleSpeed = 4;

            ballX = ((this.Width / 2) - 10);
            ballY = (this.Height - paddleHeight) - 80;
            int x = 10;
            #endregion

            // Creates a new ball
            ball = new Ball(ballX, ballY);

            // Creates blocks
            while (Form1.blocks.Count < 12)
            {
                x += 57;
                Block b1 = new Block(x, 10, 1, Color.White);
                Form1.blocks.Add(b1);
            }

            gameTimer.Enabled = true;
        }

        private void GameScreen_Load(object sender, EventArgs e)
        {

        }

        public void LifeLost()
        {
            lives--;

            // Plays lost ball sound
            ballLost.Play();

            leftArrowDown = downArrowDown = rightArrowDown = upArrowDown = spaceDown = false;

            // Moves the ball back to origin
            ball.x = ((paddleX - (ball.size / 2)) + (paddleLength / 2));
            ball.y = (this.Height - paddleHeight) - 85;

            gameTimer.Enabled = true;
        }

        public void OnEnd()
        {
            Form1.blocks.Clear();
            

            // Goes to the game over screen
            Form form = this.FindForm();
            PlayScreen ps = new PlayScreen();
            ps.Location = new Point((form.Width - ps.Width) / 2, (form.Height - ps.Height) / 2);
            form.Controls.Add(ps);
            form.Controls.Remove(this);
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Space:
                    spaceDown = true;
                    lives = 0;
                    break;
                default:
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Space:
                    spaceDown = false;
                    break;
                default:
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            ticks++;

            if (lives >= 1)
            {
                if (Form1.blocks.Count != 0)
                {
                    // Paddle physics
                    if (leftArrowDown && paddleX > 10)
                        paddleX -= 5;
                    if (rightArrowDown && paddleX < (this.Width - paddleLength - 10))
                        paddleX += 5;

                    // Paddle collision
                    if (ball.PaddleCollision())
                    {
                        // Play sound
                        wallPlatCollision.Play();

                        
                        if (ball.y + ball.size >= paddleY)
                        {
                            // If the ball 
                            if ((ball.x) < paddleX && (ball.y + ball.size) > paddleY)
                            {
                                ball.xSpeed = -Math.Abs(ball.xSpeed);
                                ball.ySpeed = Math.Abs(ball.ySpeed);
                            }
                            else if (ball.x + ball.size > (paddleX + paddleLength) && (ball.y + ball.size) > paddleY)
                            {
                                ball.xSpeed = Math.Abs(ball.xSpeed);
                                ball.ySpeed = -Math.Abs(ball.ySpeed);
                            }
                            else
                            {
                                ball.ySpeed *= -1;
                            }
                        }

                        // Changes horizontal direction to platform direction
                        if (leftArrowDown)
                            ball.xSpeed = -Math.Abs(ball.xSpeed);
                        else if (rightArrowDown)
                            ball.xSpeed = Math.Abs(ball.xSpeed);
                    }

                    // Checks for collision
                    Boolean keepPlaying = true;

                    // Wall collision; lose life if bottom
                    if (keepPlaying != ball.WallCollision(this))
                        LifeLost();

                    // Ball physics
                    foreach (Block b in Form1.blocks)
                    {
                        if (ball.BlockCollision(b))
                        {
                            brickBreak.Play();
                            Form1.blocks.Remove(b);
                            break;
                        }
                    }
                    // Moves ball
                    ball.Move(); 
                }
                else
                {
                    gameTimer.Enabled = false;
                    Form1.WinOrLose = "You Won!";
                    winSound.Play();
                    OnEnd();
                }
                Refresh();
            }
            else
            {
                gameTimer.Enabled = false;
                Form1.WinOrLose = "You Lost!";
                loseSound.Play();
                OnEnd();
            }
        }

        public void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            // Draws paddle
            e.Graphics.FillRectangle(paddleBrush, paddleX, paddleY, paddleLength, paddleHeight);

            // Draws blocks
            foreach (Block b in Form1.blocks)
                e.Graphics.FillRectangle(blockBrush, b.x, b.y, b.blockWidth, b.blockHeight);

            // Darws balls
            e.Graphics.FillRectangle(ballBrush, ball.x, ball.y, ball.size, ball.size);
        }
    }
}
