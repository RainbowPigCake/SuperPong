/*  January 8, 2020
 *  This is the child class of the parent paddle class for the red paddle
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
    class RedPaddle : Paddle
    {
        /// <summary>
        /// Constructor for the red paddle
        /// </summary>
        /// <param name="start">Takes in a starting location</param>
        public RedPaddle(Point start)
        {
            //Assign a slower velocity and the rectangle information
            paddleBox.Location = start;
            paddleBox.Size = new Size(140, 10);
            velocity = 8;
        }

        /// <summary>
        /// Draws the paddle in red
        /// </summary>
        /// <param name="e">Paint event variable</param>
        public override void DrawPaddle(PaintEventArgs e)
        {
            //Draw the rectangle outline with red
            e.Graphics.DrawRectangle(new Pen(Color.Red, THICKNESS), paddleBox);
        }
    
    }
}
