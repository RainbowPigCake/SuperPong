/*  January 8, 2020
 *  This is the class for the ball
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
    class Ball
    {
        //Store the x and y speeds
        private int xSpeed = 5;
        private int ySpeed = 10;
        
        //Ball rectangle
        private Rectangle ballBox = new Rectangle();

        //Score array for each player
        private int[] score = { 0, 0 };

        /// <summary>
        /// Constructor for the ball class
        /// </summary>
        /// <param name="start">Starting location of the ball</param>
        public Ball(Point start)
        {
            //Set the start location and size for the rectangle
            ballBox.Location = start;
            ballBox.Size = new Size(30, 30);
        }
        
        //Draws the ball with white
        public void DrawBall(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.FloralWhite, 5), ballBox);
        }

        public void Motion()
        {
            //Move the ball by the speeds
            ballBox.X += xSpeed;
            ballBox.Y -= ySpeed;

            //If the ball hits a wall then invert the x speed
            if (ballBox.X <= 0 || ballBox.X >= Game.WIDTH - ballBox.Width)
            {
                xSpeed = -xSpeed;
            }

            //Otherwise, if the ball goes into another's player area then a player gets a point
            else if (ballBox.Y <= -ballBox.Height)
            {
                score[1]++;
                ballBox.Location = new Point((int)(Game.WIDTH / 2), (int)(Game.HEIGHT * 0.8));
            }
            else if (ballBox.Y >= Game.HEIGHT - BallBox.Height)
            {
                score[0]++;
                ballBox.Location = new Point((int)(Game.WIDTH / 2), (int)(Game.HEIGHT * 0.2));
            }
        }

        public bool Collide(Paddle p, Paddle turn)
        {
            //If the ball hits a paddle and it is that paddle's turn to move
            if (ballBox.IntersectsWith(p.PaddleBox) && turn == p)
            {
                //Invert the y speed and return true that it has hit
                ySpeed = -ySpeed;
                return true;
            }
            //If the paddle hitting is a pink one then check for a projectile intersection
            else if (p is PinkPaddle)
            {
                PinkPaddle pink = (PinkPaddle)p;
                if (pink.Projectile == null)
                {
                    return false;
                }
                else if (ballBox.IntersectsWith(pink.Projectile.PaddleBox) && turn == p)
                {
                    ySpeed = -ySpeed;
                    return true;
                }
            }
            return false;
        }


        //Property for x speed
        public int SpeedX
        {
            get
            {
                return xSpeed;
            }
            set
            {
                xSpeed = value;
            }
        }

        //Property for y speed
        public int SpeedY
        {
            get
            {
                return ySpeed;
            }
            set
            {
                ySpeed = value;
            }
        }

        //Property for score
        public int[] Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
            }
        }

        //Property for rectangle
        public Rectangle BallBox
        {
            get
            {
                return ballBox;
            }
        }
    }

}
