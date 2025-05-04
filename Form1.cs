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
        Piece[] Pieces = new Piece[32];
        Game GameState = new Game();


        public Form1()
        {

            //Set Up pieces
                //Player 1
                Pieces[11] = new Knight(true, 7, 1);
                Pieces[12] = new Knight(true, 7, 6);
                //Player 2
                Pieces[27] = new Knight(false, 0, 1);
                Pieces[28] = new Knight(false, 0, 1);

            //End

            InitializeComponent();
        }


        private void HandleButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            TextBox.Text = String.Format($"{clickedButton.Tag}");

            int row = int.Parse(clickedButton.Tag.ToString().Substring(0,1));
            int col = int.Parse(clickedButton.Tag.ToString().Substring(1,1));

            int NumberOnboard = Chessboard.board[row, col];
            HandleTurn(NumberOnboard,row,col);



            DrawBoard();
        }

        public void HandleTurn(int NumberOnBoard,int row, int col)
        {
            //Overall turn structure

            //See if a Piece has been just selected // That is your peice
            if (GameState.whoTurn) //If Player 1
            {
                if (NumberOnBoard >= 1 && NumberOnBoard <= 16) //if it is your piece// player 1
                {
                    GameState.SelectedPiece = new Tuple<int, int>(row, col); //Then set the new selected piece location
                    GameState.PieceSelected = true;   
                }
                else //Not your piece
                {
                    if (GameState.PieceSelected) //If there is a piece selected then move that piece
                    {
                        List<Tuple<int, int>> PossibleMoves = new List<Tuple<int, int>>();
                        int MovingPiece = Chessboard.board[GameState.SelectedPiece.Item1, GameState.SelectedPiece.Item2];
                        TextBox.Text = String.Format($"{MovingPiece}");

                        //PossibleMoves = Pieces[Chessboard.board[row, col]].GetPossibleMoves();
                        PossibleMoves = Pieces[MovingPiece].GetPossibleMoves();

                        //bool valid = Pieces[0].IsValidPostion(GameState.whoTurn, new Tuple<int, int>(row, col), PossibleMoves, Chessboard);
                        bool valid = Pieces[MovingPiece].IsValidPostion(GameState.whoTurn, new Tuple<int, int>(row, col), PossibleMoves, Chessboard);

                        //Pieces = Pieces[0].Move(valid,Chessboard,Pieces,new Tuple<int,int>(row,col),MovingPiece);
                        Pieces = Pieces[MovingPiece].Move(valid, Chessboard, Pieces, new Tuple<int, int>(row, col), 12);


                      
                        GameState.PieceSelected = false;
                        GameState.SelectedPiece = null;
                        
                        //And then change the turn and stuff
                        Chessboard = Chessboard.UpdateBoard(Pieces);
                        //TextBox.Text = String.Format("Piece Selected: ###");

                    }
                    //else
                        //TextBox.Text = String.Format("No Piece Selected");
                }
            }
            else if (!GameState.whoTurn)
            {

            }
            //Yes: GameState.SelectedPiece = new Tuple<row,col>;
            //No: 
            //If a Piece has already been selected
            //Yes: Call various functions to Move
            //GetPossibleMoves of piece selected Piece[ChessBoard.Board[GameState.Selected.Item1,GameState.Selected.Item2]].GetPossibleMoves
            //Valid = IsValid(Stuff in here IDK);
            //Move(Valid, Plue stuff here to update pieces)
            //UpdateBoard based on Pieces[]
            //Other stuff??
            //No: Nothing happens
            //Save the current board state
            //Change the turn
            //Repeat 
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
