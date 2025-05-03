using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    using Chess.Pieces;

    public partial class Form1 : Form
    {
        Board Chessboard = new Board();
        Piece[] Pieces = new Piece[32];

        public Form1()
        {
            InitializeComponent();
        }


        private void HandleButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            TextBox.Text = String.Format($"{clickedButton.Tag}");
            DrawBoard();
        }

        public void DrawBoard()
        {
            string tag;
            int piece;
            Button targetbutton = null;
            
            //Will go through row and col
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    tag = $"{row}{col}";
                    piece = Chessboard.board[row, col];

                    //This will look through each button made and look at tags. Can do this becuase buttons are auto put into Controls
                    foreach (Control control in this.Controls)
                    {
                        if (control is Button btn && btn.Tag != null && btn.Tag.ToString() == tag)
                        {
                            targetbutton = btn;
                            break;
                        }
                    }
                    if (targetbutton != null)
                    {
                        if (piece >= 1 && piece <= 8)
                        {
                            //White Pawns
                            targetbutton.BackgroundImage = Properties.Resources.WhitePawn;
                            targetbutton.ImageAlign = ContentAlignment.MiddleCenter;
                            targetbutton.BackgroundImageLayout = ImageLayout.Zoom;
                        }
                        else if(piece == 9 || piece == 10)
                        {
                            //White Rook
                            targetbutton.BackgroundImage = Properties.Resources.WhiteRook;
                            targetbutton.ImageAlign = ContentAlignment.MiddleCenter;
                            targetbutton.BackgroundImageLayout = ImageLayout.Zoom;
                        }
                        else if (piece == 11 || piece == 12)
                        {
                            //White Knight
                            targetbutton.BackgroundImage = Properties.Resources.WhiteKnight;
                            targetbutton.ImageAlign = ContentAlignment.MiddleCenter;
                            targetbutton.BackgroundImageLayout = ImageLayout.Zoom;
                        }
                        else if (piece == 13 || piece == 14)
                        {
                            //White Bishop
                            targetbutton.BackgroundImage = Properties.Resources.WhiteBishop;
                            targetbutton.ImageAlign = ContentAlignment.MiddleCenter;
                            targetbutton.BackgroundImageLayout = ImageLayout.Zoom;
                        }
                        else if (piece == 15)
                        {
                            //White Queen
                            targetbutton.BackgroundImage = Properties.Resources.WhiteQueen;
                            targetbutton.ImageAlign = ContentAlignment.MiddleCenter;
                            targetbutton.BackgroundImageLayout = ImageLayout.Zoom;
                        }
                        else if (piece == 16)
                        {
                            //White King
                            targetbutton.BackgroundImage = Properties.Resources.WhiteKing;
                            targetbutton.ImageAlign = ContentAlignment.MiddleCenter;
                            targetbutton.BackgroundImageLayout = ImageLayout.Zoom;
                        }
                        else if (piece >= 17 && piece <= 24)
                        {
                            //Black Pawn
                            targetbutton.BackgroundImage = Properties.Resources.BlackPawn;
                            targetbutton.ImageAlign = ContentAlignment.MiddleCenter;
                            targetbutton.BackgroundImageLayout = ImageLayout.Zoom;
                        }
                        else if (piece == 25 || piece == 26)
                        { 
                            //Black Rook
                            targetbutton.BackgroundImage = Properties.Resources.BlackRook;
                            targetbutton.ImageAlign = ContentAlignment.MiddleCenter;
                            targetbutton.BackgroundImageLayout = ImageLayout.Zoom;
                        }
                        else if (piece == 27 || piece == 28)
                        {
                            //Black Rook
                            targetbutton.BackgroundImage = Properties.Resources.BlackKnight;
                            targetbutton.ImageAlign = ContentAlignment.MiddleCenter;
                            targetbutton.BackgroundImageLayout = ImageLayout.Zoom;
                        }
                        else if (piece == 29 || piece == 30)
                        {
                            //Black Rook
                            targetbutton.BackgroundImage = Properties.Resources.BlackBishop;
                            targetbutton.ImageAlign = ContentAlignment.MiddleCenter;
                            targetbutton.BackgroundImageLayout = ImageLayout.Zoom;
                        }
                        else if (piece == 31)
                        {
                            //Black Rook
                            targetbutton.BackgroundImage = Properties.Resources.BlackQueen;
                            targetbutton.ImageAlign = ContentAlignment.MiddleCenter;
                            targetbutton.BackgroundImageLayout = ImageLayout.Zoom;
                        }
                        else if (piece == 32)
                        {
                            //Black Rook
                            targetbutton.BackgroundImage = Properties.Resources.BlackKing;
                            targetbutton.ImageAlign = ContentAlignment.MiddleCenter;
                            targetbutton.BackgroundImageLayout = ImageLayout.Zoom;
                        }
                    }
                }
            }
        }

    }
}
