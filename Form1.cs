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
    using System.Media;
    using Chess.Pieces;

    public partial class Form1 : Form
    {
        Board Chessboard = new Board();
        Piece[] Pieces = new Piece[33];
        Game GameState = new Game();


        public Form1()
        {
            //Set Up pieces
            //Player 1
            Pieces[1] = new Pawn(true, 6, 0);
            Pieces[2] = new Pawn(true, 6, 1);
            Pieces[3] = new Pawn(true, 6, 2);
            Pieces[4] = new Pawn(true, 6, 3);
            Pieces[5] = new Pawn(true, 6, 4);
            Pieces[6] = new Pawn(true, 6, 5);
            Pieces[7] = new Pawn(true, 6, 6);
            Pieces[8] = new Pawn(true, 6, 7);
            Pieces[9] = new Rook(true, 7, 0);
            Pieces[10] = new Rook(true, 7, 7);
            Pieces[11] = new Knight(true, 7, 1);
            Pieces[12] = new Knight(true, 7, 6);
            Pieces[13] = new Bishop(true, 7, 2);
            Pieces[14] = new Bishop(true, 7, 5);
            Pieces[15] = new Queen(true, 7, 3);
            Pieces[16] = new King(true, 7, 4);

            ////Player 2
            Pieces[17] = new Pawn(false, 1, 0);
            Pieces[18] = new Pawn(false, 1, 1);
            Pieces[19] = new Pawn(false, 1, 2);
            Pieces[20] = new Pawn(false, 1, 3);
            Pieces[21] = new Pawn(false, 1, 4);
            Pieces[22] = new Pawn(false, 1, 5);
            Pieces[23] = new Pawn(false, 1, 6);
            Pieces[24] = new Pawn(false, 1, 7);
            Pieces[25] = new Rook(false, 0, 0);
            Pieces[26] = new Rook(false, 0, 7);
            Pieces[27] = new Knight(false, 0, 1);
            Pieces[28] = new Knight(false, 0, 6);
            Pieces[29] = new Bishop(false, 0, 2);
            Pieces[30] = new Bishop(false, 0, 5);
            Pieces[31] = new Queen(false, 0, 3);
            Pieces[32] = new King(false, 0, 4);

            //End

            InitializeComponent();
        }


        private void HandleButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            //TextBox.Text = String.Format($"{clickedButton.Tag}");

            int row = int.Parse(clickedButton.Tag.ToString().Substring(0,1));
            int col = int.Parse(clickedButton.Tag.ToString().Substring(1,1));

            int NumberOnboard = Chessboard.board[row, col];
            HandleTurn(NumberOnboard,row,col);


            Chessboard = Chessboard.UpdateBoard(Pieces);
            DrawBoard();
        }

        private bool IsFriend(int val)
        {
            return GameState.whoTurn ? val >= 1 && val <= 16 : val >= 17; //This checks if, based on team, if friend or not 

        }

        public void HandleTurn(int NumberOnBoard,int row, int col)
        {
            //Overall turn structure
                //NumberOnBoard >= 1 && NumberOnBoard <= 16 ////if it is your piece// player 1
            if (IsFriend(NumberOnBoard)) //Check to see if your peice based on turn
            {
                GameState.SelectedPiece = new Tuple<int, int>(row, col); //Then set the new selected piece location
                GameState.PieceSelected = true;
                TextBox.Text = String.Format($"Slected {Pieces[NumberOnBoard].name}");
            }
            else //Not your piece
            {
                if (GameState.PieceSelected) //If there is a piece selected then move that piece
                {
                    List<Tuple<int, int>> PossibleMoves = new List<Tuple<int, int>>();
                    int MovingPiece = Chessboard.board[GameState.SelectedPiece.Item1, GameState.SelectedPiece.Item2];

                    PossibleMoves = Pieces[MovingPiece].GetPossibleMoves(Chessboard);

                    bool valid = Pieces[MovingPiece].IsValidPostion(new Tuple<int, int>(row, col), PossibleMoves);

                    Pieces = Pieces[MovingPiece].Move(valid, Chessboard, Pieces, new Tuple<int, int>(row, col), MovingPiece);


                      
                    //GameState.PieceSelected = false;
                    //GameState.SelectedPiece = null;
                        
                    //And then change the turn and stuff
                    if(valid)
                    {
                        GameState.whoTurn = !GameState.whoTurn;
                        TextBox.Text = String.Format("");

                    }

                }
                GameState.PieceSelected = false;
                GameState.SelectedPiece = null;
            }
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
                        else if(piece == 0)
                        {
                            targetbutton.BackgroundImage = null;
                        }
                    }
                }
            }
        }

    }
}
