/*  January 8, 2020
 *  This is the child class of the paddle for the blue one
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
    class BluePaddle : Paddle
    {
        /// <summary>
        /// Constructor for the blue paddle
        /// </summary>
        /// <param name="start">Takes in a starting location of the paddle</param>
        public BluePaddle(Point start)
        {
            //Store the rectangle information and the velocity of the blue paddle
            paddleBox.Location = start;
            paddleBox.Size = new Size(35, 10);
            velocity = 20;
        }

        /// <summary>
        /// Draws the box
        /// </summary>
        /// <param name="e">Paint event variable</param>
        public override void DrawPaddle(PaintEventArgs e)
        {
            //Draw a rectangle onto the box in blue
            e.Graphics.DrawRectangle(new Pen(Color.CornflowerBlue, THICKNESS), paddleBox);
        }



    }
}
