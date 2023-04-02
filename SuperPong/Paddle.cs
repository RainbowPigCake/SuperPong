/*  January 8, 2020
 *  This is the parent class for the paddles
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace SuperPong
{
    abstract class Paddle
    {
        //Protected variables are for child classes to see
        //Create a rectangle and store the speed. 
        protected Rectangle paddleBox = new Rectangle();
        protected int velocity = 10;

        //Store whether it should be moving right or left
        protected bool left = false;
        protected bool right = false;

        //Store the pen thickness
        public const int THICKNESS = 4;

        //Direction constants
        public const int LEFT = 1, RIGHT = 2;

        //Move the paddle
        public virtual bool Move()
        {
            //Move left or right depending on the boolean respectively
            if (left)
            {
                paddleBox.X -= velocity;
            }
            else if (right)
            {
                paddleBox.X += velocity;
            }

            //Return true if the paddle is off the map
            if (paddleBox.X <= -paddleBox.Width)
            {
                paddleBox.X = Game.WIDTH + paddleBox.Width;
                return true;
            }

            else if (paddleBox.X >= Game.WIDTH + paddleBox.Width)
            {
                paddleBox.X = paddleBox.Width;
                return true;
            }
            return false;
        }

        //Abstract constructor
        public abstract void DrawPaddle(PaintEventArgs e);

        //Shooting by default will do nothing except return false unless overridden by a paddle that actually does shoot
        public virtual bool Shoot(Point start)
        {
            return false;
        }

        //Left bool property
        public bool Left
        {
            get
            {
                return left;
            }
            set
            {
                left = value;
            }
        }

        //Right bool property
        public bool Right
        {
            get
            {
                return right;
            }
            set
            {
                right = value;
            }
        }

        //Paddle rectangle property
        public Rectangle PaddleBox
        {
            get
            {
                return paddleBox;
            }
        }     
    }
}
