﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SuperSnakeGame.Screens;

namespace SuperSnakeGame
{
    public class Block
    {
        public int blockWidth = 50;
        public int blockHeight = 25;

        public int x;
        public int y; 
        public int hp;
        public Color colour;

        public static Random rand = new Random();

        public Block(int _x, int _y, int _hp, Color _colour)
        {
            x = _x;
            y = _y;
            hp = _hp;
            colour = _colour;
        }
    }
}