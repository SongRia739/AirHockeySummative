using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirHockeySummative
{
    public partial class Form1 : Form
    {
        int playerTurn = 1;

        int player1X = 175;
        int player1Y = 550;
        int player1Score = 0;

        int player2X = 175;
        int player2Y = 1;
        int player2Score = 0;

        int paddleWidth = 50;
        int paddleHeight = 50;
        int paddleSpeed = 4;

        //black puck
        int puckX = 295;
        int puckY = 195;
        int puckXSpeed = 0;
        int puckYSpeed = 0;
        int puckWidth = 50;
        int puckHeight = 50; 

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
            if (wDown == true && player1Y > 0)
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
            if (downArrowDown == true && player2Y < this.Height - paddleHeight)
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

            //check if ball hits either paddle. If it does change the direction 
            if (player1Rec.IntersectsWith(puckRec))
            {
                puckXSpeed *= -1;
                puckX = player1X + paddleWidth + 1;
             
            }
            else if (player2Rec.IntersectsWith(puckRec))
            {
                puckXSpeed *= -1;
                puckX = player2X - puckWidth - 1;
                
            }

            //ball hits wall
            if (puckY < 0 || puckY > this.Height - puckHeight)
            {
                puckYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed;
                puckY = player2Y - puckHeight - 1;
                puckX += puckXSpeed;
                puckY += puckYSpeed;
            }

            //nets

            //score tracker

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
