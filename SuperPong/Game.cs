/*  January 8, 2020
 *  This is for the game class which is responsible for the information and events of the game
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace SuperPong
{
    class Game
    {
        //Create objects for the game
        private Ball ball;
        private Paddle[] paddles = new Paddle[2];

        private Rectangle backgroundBox = new Rectangle(new Point(0, 0), new Size(WIDTH, HEIGHT));

        private Random numberGenerator = new Random();

        private Paddle turn;

        //Constants for the width of the form and height 
        public const int WIDTH = 1280, HEIGHT = 720;

        //Store whether a paddle is shooting
        private bool[] shoot = { false, false };

        //Winning score
        private const int MAX_SCORE = 5;

        /// <summary>
        /// Constructor for the game class
        /// </summary>
        public Game()
        {
            //Make a new game
            ball = new Ball(new Point(WIDTH / 2, (int)(HEIGHT * 0.9)));

            //Points for default paddles
            Point[] points = { new Point(WIDTH / 2, 10), new Point(WIDTH / 2, HEIGHT - 60) };

            //Generate 2 random paddles
            for (int i = 0; i < 2; i++)
            {
                int type = numberGenerator.Next(0, 4);
                switch (type)
                {
                    case 0:
                        paddles[i] = new RedPaddle(points[i]);
                        break;
                    case 1:
                        paddles[i] = new GreenPaddle(points[i]);
                        break;
                    case 2:
                        paddles[i] = new BluePaddle(points[i]);
                        break;
                    case 3:
                        paddles[i] = new PinkPaddle(points[i]);
                        break;

                }
            }
            //Set the turn to the first paddle
            turn = paddles[0];


            //Load information from save if the file exists
            if (File.Exists("savefile.txt")
                && false) //override to false for external usability
            {
                try
                {
                    using (StreamReader sr = new StreamReader("savefile.txt"))
                    {
                        //If the file is empty
                        if (sr.Peek() == -1)
                        {
                            return;
                        }

                        //Paddle loading
                        for (int i = 0; i < paddles.Length; i++)
                        {
                            //Store the first line
                            string line = sr.ReadLine();

                            //Check which paddle the first and second are and create them with a point
                            switch (line)
                            {
                                case "red":
                                    paddles[i] = new RedPaddle(new Point(Int32.Parse(line = sr.ReadLine()), Int32.Parse(line = sr.ReadLine())));
                                    break;
                                case "green":
                                    paddles[i] = new GreenPaddle(new Point(Int32.Parse(line = sr.ReadLine()), Int32.Parse(line = sr.ReadLine())));
                                    break;
                                case "blue":
                                    paddles[i] = new BluePaddle(new Point(Int32.Parse(line = sr.ReadLine()), Int32.Parse(line = sr.ReadLine())));
                                    break;
                                case "pink":
                                    paddles[i] = new PinkPaddle(new Point(Int32.Parse(line = sr.ReadLine()), Int32.Parse(line = sr.ReadLine())));
                                    break;
                            }
                        }

                        //Ball load
                        //Save location and speeds
                        ball = new Ball(new Point(Int32.Parse(sr.ReadLine()), Int32.Parse(sr.ReadLine())));
                        ball.SpeedX = Int32.Parse(sr.ReadLine());
                        ball.SpeedY = Int32.Parse(sr.ReadLine());

                        //Score and paddle turn load
                        int[] score = { Int32.Parse(sr.ReadLine()), Int32.Parse(sr.ReadLine()) };
                        ball.Score = score;
                        turn = paddles[Int32.Parse(sr.ReadLine())];

                        //Check if there are no projectiles or anything left to load
                        if (sr.Peek() == -1)
                        {
                            return;
                        }

                        //Load projectile 
                        string line2 = sr.ReadLine();
                        int index = Int32.Parse(line2);
                        PinkPaddle tempPaddle = (PinkPaddle)paddles[index];
                        tempPaddle.Shoot(new Point(Int32.Parse(sr.ReadLine()), Int32.Parse(sr.ReadLine())));
                        shoot[index] = true;

                        //Check if there are no projectiles or anything left to load
                        line2 = sr.ReadLine();
                        if (line2 == null)
                        {
                            return;
                        }

                        //Load projectile 
                        index = Int32.Parse(line2);
                        tempPaddle = (PinkPaddle)paddles[index];
                        tempPaddle.Shoot(new Point(Int32.Parse(sr.ReadLine()), Int32.Parse(sr.ReadLine())));
                        shoot[index] = true;
                        return;
                    }
                }

                catch (Exception x)
                {
                    //Error message
                    MessageBox.Show("Error while reading saves:\r\n" + x);
                }
            }
        }

        public void GameUpdate(ref Timer tmrUpdate, ref Label lblScore1, ref Label lblScore2, ref Label lblMessage)
        {
            //Update the score and if a player wins
            lblScore1.Text = ball.Score[0].ToString();
            lblScore2.Text = ball.Score[1].ToString();
            if (ball.Score[0] == MAX_SCORE)
            {
                tmrUpdate.Enabled = false;
                lblMessage.Text = ("Player 1 Wins");
                return;
            }
            else if (ball.Score[1] == MAX_SCORE)
            {
                tmrUpdate.Enabled = false;
                lblMessage.Text = ("Player 2 Wins");
                return;
            }

            //Move ball
            ball.Motion();

            //Check for ball collision
            if (ball.Collide(paddles[0], turn))
            {
                turn = paddles[1];
            }
            else if (ball.Collide(paddles[1], turn))
            {
                turn = paddles[0];
            }

            //Move the paddles
            paddles[0].Move();
            paddles[1].Move();

            //Shoot the projectiles
            if (shoot[0])
            {
                //When the paddles are null stop shooting
                if (paddles[0].Shoot(new Point(paddles[0].PaddleBox.X + paddles[0].PaddleBox.Width / 2, paddles[0].PaddleBox.Y)))
                {
                    shoot[0] = false;
                }
            }
            if (shoot[1])
            {
                if (paddles[1].Shoot(new Point(paddles[1].PaddleBox.X + paddles[1].PaddleBox.Width / 2, paddles[1].PaddleBox.Y)))
                {
                    shoot[1] = false;
                }
            }
        }
        
        public void PaddleLeft(int paddle)
        {
            //Set the variable depending on which paddle
            if (paddle == 1)
            {
                paddles[0].Left = true;
            }
            else if (paddle == 2)
            {
                paddles[1].Left = true;
            }
        }
        public void PaddleRight(int paddle)
        {
            //Set the variable depending on which paddle
            if (paddle == 1)
            {
                paddles[0].Right = true;
            }
            else if (paddle == 2)
            {
                paddles[1].Right = true;
            }
        }

        public void Draw(PaintEventArgs e)
        {
            //Draw the rectangles and other things
            e.Graphics.FillRectangle(new SolidBrush(Color.Black), backgroundBox);
            ball.DrawBall(e);
            paddles[0].DrawPaddle(e);
            paddles[1].DrawPaddle(e);
        }

        public void KeyPress(KeyEventArgs e, bool state)
        { 
            //Check for movement key presses and set their variables to the state
            if (e.KeyCode == Keys.A)
            {
                paddles[0].Left = state;
            }
            else if (e.KeyCode == Keys.D)
            {
                paddles[0].Right = state;
            }
            else if (e.KeyCode == Keys.Left)
            {
                paddles[1].Left = state;
            }
            else if (e.KeyCode == Keys.Right)
            {
                paddles[1].Right = state;
            }

            //Projectile keybind
            else if (e.KeyCode == Keys.W)
            {
                if (paddles[0] is PinkPaddle)
                {
                    shoot[0] = true;
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (paddles[1] is PinkPaddle)
                {
                    shoot[1] = true;
                }
            }
        }

        public Paddle[] Paddles
        {
            //Returns the array of paddles
            get
            {
                return paddles;
            }
        }

        public Paddle Turn
        {
            //Returns which paddle's turn it is
            get
            {
                return turn;
            }
        }

        public Ball GetBall
        {
            //Returns the ball
            get
            {
                return ball;
            }
        }

        public bool[] Shoot
        {
            //Returns who is shooting
            get
            {
                return shoot;
            }
            set
            {
                shoot = value;
            }
        }
    }
}
