/*  January 8, 2020
 *  This is a child class of the paddle for the pink one
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
    class Projectile : Paddle
    {
        //Constants for up or down for the direction
        public const int UP = -1, DOWN = 1;
        private int direction = 0;

        /// <summary>
        /// Constructor for the projectile
        /// </summary>
        /// <param name="start">Point for start</param>
        /// <param name="direction">Direction of flight</param>
        public Projectile(Point start, int direction)
        {
            //Set the location and the size. Also velocity and dir
            paddleBox.Location = start;
            paddleBox.Size = new Size(10, 70);
            velocity = 15;

            this.direction = direction;
        }

        public override void DrawPaddle(PaintEventArgs e)
        {
            //Draw the rectangle
            e.Graphics.DrawRectangle(new Pen(Color.Orange, THICKNESS + 2), paddleBox);
        }


        public override bool Move()
        {
            //Move the rectangle based on direction and check bounds
            paddleBox.Y += velocity * direction;
            if (paddleBox.Y <= 0)
            {
                return true;
            }
            else if (paddleBox.Y >= Game.HEIGHT - paddleBox.Height)
            {
                return true;
            }
            return false;
        }
    }
}
