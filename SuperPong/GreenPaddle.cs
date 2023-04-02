/*  January 8, 2020
 *  This is the child paddle class for the green paddle
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
    class GreenPaddle : Paddle
    {
        /// <summary>
        /// Constructor for the green paddle
        /// </summary>
        /// <param name="start">Starting location</param>
        public GreenPaddle(Point start)
        {
            //Assign velocity and rectangle information
            paddleBox.Location = start;
            paddleBox.Size = new Size(70, 10);
            velocity = 10;
        }

        /// <summary>
        /// Draws green for the paddle
        /// </summary>
        /// <param name="e">Paint event variable</param>
        public override void DrawPaddle(PaintEventArgs e)
        {
            //Draw a green rectangle
            e.Graphics.DrawRectangle(new Pen(Color.LightGreen, THICKNESS), paddleBox);
        }

    }
}
