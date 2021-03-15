/*Ria Das
 * Air Hockey
 * 
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
using System.Media;

namespace AirHockeySummative
{
    public partial class Form1 : Form
    {
        int playerTurn = 1;

        int player1X = 175;
        int player1Y = 500;
        int player1Score = 0;

        int player2X = 175;
        int player2Y = 50;
        int player2Score = 0;

        int paddleWidth = 50;
        int paddleHeight = 50;
        int paddleSpeed = 4;

        //black puck
        int puckX = 190;
        int puckY = 285;
        int puckXSpeed = 3;
        int puckYSpeed = 3;
        int puckWidth = 40;
        int puckHeight = 40; 

        //keys
        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;

        bool upArrowDown = false;
        bool downArrowDown = false;
        bool rightArrowDown = false;
        bool leftArrowDown = false;

        SolidBrush purpleBrush = new SolidBrush(Color.MediumPurple);
        SolidBrush pinkBrush = new SolidBrush(Color.LightPink);
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush blueBrush = new SolidBrush(Color.LightSkyBlue);
        SolidBrush greyBrush = new SolidBrush(Color.Gray);
        SolidBrush lightgreyBrush = new SolidBrush(Color.LightGray);
        Pen purplePen = new Pen(Color.MediumPurple);

        Font screenFont = new Font("Consolas", 12);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //move by key directions
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //move by key directions
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //arena
            e.Graphics.FillRectangle(pinkBrush, 0, 300, 1000, 10);
       //     e.Graphics.DrawEllipse(purplePen, 160, 250, 70, 70);
            //border
            e.Graphics.FillRectangle(greyBrush, 0, 1, 1000, 20);
            e.Graphics.FillRectangle(greyBrush, 0, 580, 1000, 20);
            e.Graphics.FillRectangle(lightgreyBrush, 0, 0, 30, 600);
            e.Graphics.FillRectangle(lightgreyBrush, 350, 0, 30, 600);
            //net
            e.Graphics.FillRectangle(blueBrush, 150, 1, 100, 20);
            e.Graphics.FillRectangle(blueBrush, 150, 580, 100, 20);
            //two player icons
            e.Graphics.FillEllipse(purpleBrush, player1X, player1Y, paddleWidth, paddleHeight);
            e.Graphics.FillEllipse(pinkBrush, player2X, player2Y, paddleWidth, paddleHeight);
            //puck
            e.Graphics.FillEllipse(blackBrush, puckX, puckY, puckWidth, puckHeight);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move ball 
            puckX += puckXSpeed;
            puckY += puckYSpeed;

            //move player 1
            if (wDown == true && player1Y > 300) 
            {
                player1Y -= paddleSpeed;
            }

            if (sDown == true && player1Y < this.Height - paddleHeight)
            {
                player1Y += paddleSpeed;
            }
            if (aDown == true && player1X > 0)
            {
                player1X -= paddleSpeed;
            }
            if (dDown == true && player1X < this.Width - paddleWidth)
            {
                player1X += paddleSpeed;
            }

            //mover player 2
            if (upArrowDown == true && player2Y > 0)
            {
                player2Y -= paddleSpeed;
            }
            if (downArrowDown == true && player2Y < 260) 
            {
                player2Y += paddleSpeed;
            }
            if (leftArrowDown == true && player2X > 0)
            {
                player2X -= paddleSpeed;
            }
            if (rightArrowDown == true && player2X < this.Width - paddleWidth)
            {
                player2X += paddleSpeed;
            }

            //create Rectangles of objects on screen to be used for collision detection 
            Rectangle player1Rec = new Rectangle(player1X, player1Y, paddleWidth, paddleHeight);
            Rectangle player2Rec = new Rectangle(player2X, player2Y, paddleWidth, paddleHeight);
            Rectangle puckRec = new Rectangle(puckX, puckY, puckWidth, puckHeight);
            Rectangle p1goal = new Rectangle(150, 1, 100, 20);
            Rectangle p2goal = new Rectangle(150, 580, 100, 20);

            //check if ball hits either paddle. If it does change the direction 
            SoundPlayer hitSound = new SoundPlayer(Properties.Resources.HitSound);
            if (player1Rec.IntersectsWith(puckRec))
            {
                hitSound.Play();
                puckXSpeed *= -1;
                puckX = player1X + paddleWidth + 1;
             
            }
            else if (player2Rec.IntersectsWith(puckRec))
            {
                hitSound.Play();
                puckXSpeed *= -1;
                puckX = player2X - puckWidth + 1;
                
            }

            //ball hits wall
            puckX += puckXSpeed;
            puckY += puckYSpeed;

            if (puckY < 20 || puckY > this.Height - 20 - puckHeight)
            {
                puckYSpeed *= -1;  
            }
            if (puckX < 30 || puckX > this.Width - 30 - puckWidth)
            {
                puckXSpeed *= -1;
            }

            //nets
            if (puckRec.IntersectsWith(p1goal))
            {
                player2Score++;

                p2ScoreLabel.Text = $"{player2Score}";
                int puckX = 190;
                int puckY = 285;
            }
           if (puckRec.IntersectsWith(p2goal))
            {
                player1Score++;

                p1ScoreLabel.Text = $"{player1Score}";
                int puckX = 190;
                int puckY = 285;
            }

            //3 point tracker
            if (player1Score == 3 || player2Score == 3)
            {
                gameTimer.Enabled = false;
            }

            //refresh
            Refresh();
        }

    }
}
