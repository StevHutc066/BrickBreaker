using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SuperSnakeGame.Screens;
using System.Windows.Forms;

namespace SuperSnakeGame
{
    public class Ball
    {
        public int size = 20;
        public int ballSpeed = 5;

        public int x, y;
        public int xSpeed = 4;
        public int ySpeed = 4;
        public Color colour;

        public static Random rand = new Random();

        // <summary>
        /// Constructer method for a Ball object
        /// </summary>
        /// <param name="_x">Sets the initial x coordinate</param>
        /// <param name="_y">Set the initial y coorinate</param>
        /// <param name="_size">Sets the initial size</param>
        /// <param name="_xSpeed">Sets the initial horizontal speed</param>
        /// <param name="_ySpeed">Sets the inital verital speed</param>
        public Ball(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public void Move()
        {
            x = x + xSpeed;
            y = y + ySpeed;
        }

        public bool BlockCollision(Block b)
        {
            Rectangle blockRec = new Rectangle(b.x, b.y, b.blockWidth, b.blockHeight);
            Rectangle ballRec = new Rectangle(x, y, size, size);

            if (blockRec.IntersectsWith(ballRec))
            {
                if (x <= (b.x + b.blockWidth))
                    xSpeed = Math.Abs(xSpeed);

                if ((x + size) >= b.x)
                    xSpeed = -Math.Abs(xSpeed);

                if (y <= (b.y + b.blockHeight))
                    ySpeed = -ySpeed;
            }

            return blockRec.IntersectsWith(ballRec);         
        }

        public bool PaddleCollision()
        {
            Rectangle ballRec = new Rectangle(x, y, size, size);
            Rectangle paddleRec = new Rectangle(GameScreen.paddleX, GameScreen.paddleY, GameScreen.paddleLength, GameScreen.paddleHeight);

            return ballRec.IntersectsWith(paddleRec);
        }

        public Boolean WallCollision(UserControl UC)
        {
            Boolean gameOn = true;

            // Collision with left wall
            if (x <= 0)
            {
                xSpeed *= -1;
                GameScreen.wallPlatCollision.Play();
            }
            // Collision with right wall
            if (x >= (UC.Width - size))
            {
                xSpeed *= -1;
                GameScreen.wallPlatCollision.Play();
            }
            // Collision with top wall
            if (y <= 2)
            {
                ySpeed *= -1;
                GameScreen.wallPlatCollision.Play();
            }
            // Ball goes out of bottom
            if (y >= UC.Height)
            {
                gameOn = false;
            }
                
            return gameOn;
        }

    }
}
