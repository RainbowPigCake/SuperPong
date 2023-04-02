/*  January 8, 2020
 *  This is for the child paddle class for the pink paddle
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
    class PinkPaddle : Paddle
    {
        //Projectile object
        Projectile projectile = null;

        /// <summary>
        /// Constructor for the pink paddle
        /// </summary>
        /// <param name="start">Starting location</param>
        public PinkPaddle(Point start)
        {
            //Assign location, size, and velocity
            paddleBox.Location = start;
            paddleBox.Size = new Size(15,10);
            velocity = 30;
        }

        public override void DrawPaddle(PaintEventArgs e)
        {
            //Draw the rectangles and the projectile if it isn't null
            e.Graphics.DrawRectangle(new Pen(Color.HotPink, THICKNESS), paddleBox);
            if (projectile != null)
            {
                projectile.DrawPaddle(e);
            }
        }

        public override bool Shoot(Point start)
        {
            //If the projectile is null and the user shoots
            if (projectile == null)
            {
                //Shoot it up or down depending on the paddle
                if (paddleBox.Y == 10)
                {
                    projectile = new Projectile(start, Projectile.DOWN);
                }
                else
                {
                    projectile = new Projectile(start, Projectile.UP);
                }
            }
            else
            {
                //If the projectile is not off the map, keep moving it
                if (projectile.Move())
                {
                    projectile = null;
                    return true;
                }
            }
            return false;
        }

        public Projectile Projectile
        {
            //Returns the projectile
            get
            {
                return projectile;
            }
        }
    }
}
