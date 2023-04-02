/*  January 8, 2020
 *  This is for the form code for the superpong visuals and other things
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SuperPong
{
    public partial class Form1 : Form
    {
        //Create a game when the program starts
        private Game game = new Game();

        public Form1()
        {
            //Start
            InitializeComponent();

            //Update the game info and the game starts paused
            tmrUpdate.Enabled = false;

            //Show controls
            //MessageBox.Show("F1 = Toggle Pause\r\nF2 = Reset Game\r\nF3 = Save Game State (Removed)");
            
            game.GameUpdate(ref tmrUpdate, ref lblScore1, ref lblScore2, ref lblMessage);
        }
        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            //Timer will update the game and refresh drawings
            game.GameUpdate(ref tmrUpdate, ref lblScore1, ref lblScore2, ref lblMessage);
            Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Sends keydown events to the game object that has other objects 
            game.KeyPress(e, true);

            //For special keys for pause, save, restart
            switch (e.KeyCode)
            {
                case Keys.F1: //Pause Button
                    tmrUpdate.Enabled = !tmrUpdate.Enabled;
                    break;
                case Keys.F2: //Restart Button
                    //Clear file and reset
                    //File.WriteAllText("savefile.txt", "");
                    Application.Restart();
                    break;
                /*
                case Keys.F3: //Save Button
                    //File saving the paddle and general game information
                    try
                    {
                        using (StreamWriter sw = new StreamWriter("savefile.txt", false))
                        {
                            //Format: type of paddle, x, y locations
                            Paddle[] paddles = game.Paddles;
                            for (int i = 0; i < paddles.Length; i++)
                            {
                                //Loop through the 2 paddles of the program and write the type of paddle
                                if (paddles[i] is RedPaddle)
                                {
                                    sw.WriteLine("red");
                                }
                                else if (paddles[i] is GreenPaddle)
                                {
                                    sw.WriteLine("green");
                                }
                                else if (paddles[i] is BluePaddle)
                                {
                                    sw.WriteLine("blue");
                                }
                                else if (paddles[i] is PinkPaddle)
                                {
                                    sw.WriteLine("pink");
                                }
                                sw.WriteLine(paddles[i].PaddleBox.X);
                                sw.WriteLine(paddles[i].PaddleBox.Y);
                            }

                            //Format: ball x, ball y, x speed, y speed
                            //Write the ball information such as speed and location
                            Ball temp = game.GetBall;
                            sw.WriteLine(temp.BallBox.X);
                            sw.WriteLine(temp.BallBox.Y);
                            sw.WriteLine(temp.SpeedX);
                            sw.WriteLine(temp.SpeedY);

                            //Format: score1, score2, paddleturn
                            sw.WriteLine(game.GetBall.Score[0]);
                            sw.WriteLine(game.GetBall.Score[1]);
                            if (game.Paddles[0] == game.Turn)
                            {
                                sw.WriteLine("0");
                            }
                            else
                            {
                                sw.WriteLine("1");
                            }

                            //Format: index paddle, projectile x, y
                            //If any paddles are pink then see if they have projectiles and save them
                            for (int i = 0; i < paddles.Length; i++)
                            {
                                if (paddles[i] is PinkPaddle)
                                {
                                    PinkPaddle tempPaddle = (PinkPaddle)paddles[i];
                                    if (tempPaddle.Projectile != null)
                                    {
                                        sw.WriteLine(i);
                                        sw.WriteLine(tempPaddle.Projectile.PaddleBox.X);
                                        sw.WriteLine(tempPaddle.Projectile.PaddleBox.Y);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception x)
                    {
                        //Show error message while writing the save
                        MessageBox.Show("Error while saving:\r\n" + x);
                    }
                    break;
                */
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //Draw the game's rectangles
            game.Draw(e);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //Send keyup events to the game to process it
            game.KeyPress(e, false);
        }
    }
}
