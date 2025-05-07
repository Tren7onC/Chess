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
    using Chess.Players;

    public partial class Form1 : Form
    {
        List<Board> Chessboard = new List<Board>();
        Piece[] Pieces = new Piece[33];
        Game GameState = new Game();
        Player Player1 = new Player1();
        Player Player2 = new Player2();

        //Cant put in GameState because history stuffs
        bool Pause = false;
        int maxturn = 0;


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
            Pieces[17] = new Pawn(false, 6, 0);
            Pieces[18] = new Pawn(false, 1, 1);
            Pieces[19] = new Pawn(false, 6, 2);
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
            //Gets the button clicked
            Button clickedButton = sender as Button;
            //This is for the Start the Game button
            
            if(clickedButton != null && !Pause && GameState.GameStart)
            { //This is for the normal game play
                int row = int.Parse(clickedButton.Tag.ToString().Substring(0, 1));
                int col = int.Parse(clickedButton.Tag.ToString().Substring(1, 1));

                int NumberOnboard = Chessboard[GameState.turnCount].board[row, col];
                HandleTurn(NumberOnboard, row, col);


                DrawBoard();
            }
        }

        private void Start(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (GameState.GameStart == false)
            {
                if (clickedButton != null)
                {
                    if (clickedButton.Name.ToString() == "GameStart")
                    {
                        foreach (Control c in this.Controls) //Turns all buttons visible and one
                        {
                            if (c is Button || c is Label || c is TextBox)
                            {
                                if(!GameState.PawnPromoteBoard.Contains(c.Name.ToString()) && c.Name.ToString() != "Player1Won" && c.Name.ToString() != "Player2Won")
                                {
                                    c.Visible = true;
                                    c.Enabled = true;
                                }
                            }
                        }
                        GameState.GameStart = true;
                        TextBox.Text = String.Format($"Start!");
                        clickedButton.Enabled = false;
                        clickedButton.Visible = false;
                        Chessboard.Add(new Board());
                        Chessboard[GameState.turnCount] = Chessboard[GameState.turnCount].UpdateBoard(Pieces, GameState, Player1, Player2);
                        TextBox.Text = String.Format("Player 1 Turn");
                        DrawBoard();
                    }
                }

            }
        }
        
        private void History(object sender, EventArgs e) //Overall some slightly different repaeting of code.
        {
            Button clickedButton = sender as Button;
            if (clickedButton.Name == "Back" && GameState.turnCount > 0)
            {
                Pause = true;
                TextBox.Text = String.Format($"{GameState.turnCount}");

                Pieces = Chessboard[GameState.turnCount - 1].SavedPiece;
                Player1 = Chessboard[GameState.turnCount - 1].SavedPlayer1;
                Player2 = Chessboard[GameState.turnCount - 1].SavedPlayer2;
                GameState = Chessboard[GameState.turnCount - 1].SavedGame;

                Chessboard[GameState.turnCount] = Chessboard[GameState.turnCount].UpdateBoard(Pieces, GameState, Player1, Player2);
                PL1_points.Text = String.Format($"{Player1.points}");
                PL2_points.Text = String.Format($"{Player2.points}");
                if (GameState.whoTurn)
                    TextBox.Text = String.Format("Player 1 Turn");
                else
                    TextBox.Text = String.Format($"Player 2 Turn");
                
                DrawBoard();
            }
            else if(clickedButton.Name.ToString() == "Forward" && GameState.turnCount < maxturn)
            {
                TextBox.Text = String.Format($"{GameState.turnCount}");

                Pieces = Chessboard[GameState.turnCount + 1].SavedPiece;
                Player1 = Chessboard[GameState.turnCount + 1].SavedPlayer1;
                Player2 = Chessboard[GameState.turnCount + 1].SavedPlayer2;
                GameState = Chessboard[GameState.turnCount + 1].SavedGame;

                Chessboard[GameState.turnCount] = Chessboard[GameState.turnCount].UpdateBoard(Pieces, GameState, Player1, Player2);

                if (GameState.whoTurn)
                    TextBox.Text = String.Format("Player 1 Turn");
                else
                    TextBox.Text = String.Format($"Player 2 Turn");
                DrawBoard();
            }
            else if(clickedButton.Name.ToString() == "Resume")
            {
                Chessboard.RemoveRange(GameState.turnCount + 1, Chessboard.Count - (GameState.turnCount + 1)); // Keeps 0 - currentturn. 

                maxturn = GameState.turnCount;
                Chessboard[GameState.turnCount] = Chessboard[GameState.turnCount].UpdateBoard(Pieces, GameState, Player1, Player2);

                if (GameState.whoTurn)
                    TextBox.Text = String.Format("Player 1 Turn");
                else
                    TextBox.Text = String.Format($"Player 2 Turn");
            
                PL2_points.Refresh();
                Pause = false;
                DrawBoard();
            }
            //if (clickedButton.Name.ToString() == "Forward")
        }

        private void Promotion(object sender, EventArgs e) //Buttons input for promotion
        {
            Button clickedButton = sender as Button;
            if (clickedButton.Name.ToString() == "P2")
                Pieces[GameState.pawnNumber] = new Rook(!GameState.whoTurn, GameState.pawnPromotionrow, GameState.pawnPromotioncol);
            else if(clickedButton.Name.ToString() == "P3")
                Pieces[GameState.pawnNumber] = new Knight(!GameState.whoTurn, GameState.pawnPromotionrow, GameState.pawnPromotioncol);
            else if (clickedButton.Name.ToString() == "P4")
                Pieces[GameState.pawnNumber] = new Bishop(!GameState.whoTurn, GameState.pawnPromotionrow, GameState.pawnPromotioncol);
            else if (clickedButton.Name.ToString() == "P5")
                Pieces[GameState.pawnNumber] = new Queen(!GameState.whoTurn, GameState.pawnPromotionrow, GameState.pawnPromotioncol);

            foreach (Control c in this.Controls) //Turns all buttons visible and one
            {
                if (c is Button || c is Label || c is TextBox)
                {
                    if (!GameState.PawnPromoteBoard.Contains(c.Name.ToString()) && c.Name.ToString() != "GameStart" && c.Name.ToString() != "Player1Won" && c.Name.ToString() != "Player2Won")
                    {
                        c.Visible = true;
                        c.Enabled = true;
                    }
                    else
                    {
                        c.Visible = false;
                        c.Enabled = false;
                    }
                }
            }
            Chessboard[GameState.turnCount] = Chessboard[GameState.turnCount].UpdateBoard(Pieces, GameState, Player1, Player2);
            DrawBoard();
        }

        private bool IsFriend(int val)
        {
            return GameState.whoTurn ? val >= 1 && val <= 16 : val >= 17; //This checks if, based on team, if friend or not 
        }

        public void HandleTurn(int NumberOnBoard,int row, int col)
        {
            //Overall turn structure
            if (IsFriend(NumberOnBoard)) //Check to see if your peice based on turn
            {
                GameState.SelectedPiece = new Tuple<int, int>(row, col); //Then set the new selected piece location
                GameState.PieceSelected = true;
                TextBox.Text = String.Format($"Selected {Pieces[NumberOnBoard].name}");
            }
            else //Not your piece
            {
                if (GameState.PieceSelected) //If there is a piece selected then move that piece
                {
                    List<Tuple<int, int>> PossibleMoves = new List<Tuple<int, int>>();
                    int MovingPiece = Chessboard[GameState.turnCount].board[GameState.SelectedPiece.Item1, GameState.SelectedPiece.Item2];

                    PossibleMoves = Pieces[MovingPiece].GetPossibleMoves(Chessboard[GameState.turnCount]);

                    bool valid = Pieces[MovingPiece].IsValidPostion(new Tuple<int, int>(row, col), PossibleMoves);

                    Pieces = Pieces[MovingPiece].Move(valid, Chessboard[GameState.turnCount], Pieces, new Tuple<int, int>(row, col), MovingPiece);


                    //And then change the turn and stuff
                    if (valid)
                    {
                        GameState.whoTurn = !GameState.whoTurn;
                        if (GameState.whoTurn)
                            TextBox.Text = String.Format("Player 1 Turn");
                        else
                            TextBox.Text = String.Format($"Player 2 Turn");
                        //History implementation
                        GameState.turnCount++;
                        maxturn++;
                        Chessboard.Add(new Board());
                        Chessboard[GameState.turnCount] = Chessboard[GameState.turnCount].UpdateBoard(Pieces, GameState, Player1, Player2);

                        //If someone won
                        if (GameState.SomeoneWon(Pieces))
                        {
                            Winner();
                        }
                        else if (GameState.CheckPawnPromotion(Pieces))   //Pawn Promotion
                        {
                            PawnPromotion();
                        }
                    }
                    else
                    {
                        if (GameState.whoTurn)
                            TextBox.Text = String.Format("Player 1 Turn");
                        else
                            TextBox.Text = String.Format($"Player 2 Turn");
                    }

                }
                GameState.PieceSelected = false;
                GameState.SelectedPiece = null;
            }
        }

        void PawnPromotion()
        {
            foreach (Control c in this.Controls) //Turns all buttons visible and one
            {
                if (c is Button || c is Label || c is TextBox)
                {
                    if (GameState.PawnPromoteBoard.Contains(c.Name.ToString()))
                    {
                        c.Visible = true;
                        c.Enabled = true;
                        if (!GameState.whoTurn) //set player 1 pieces
                        {
                            if (c.Name.ToString() == "P2")
                            {
                                c.BackgroundImage = Properties.Resources.WhiteRook;
                                //c.ImageAlign = ContentAlignment.MiddleCenter;
                                c.BackgroundImageLayout = ImageLayout.Zoom;
                            }
                            else if (c.Name.ToString() == "P3")
                            {
                                c.BackgroundImage = Properties.Resources.WhiteKnight;
                                //c.ImageAlign = ContentAlignment.MiddleCenter;
                                c.BackgroundImageLayout = ImageLayout.Zoom;
                            }
                            else if (c.Name.ToString() == "P4")
                            {
                                c.BackgroundImage = Properties.Resources.WhiteBishop;
                                //c.ImageAlign = ContentAlignment.MiddleCenter;
                                c.BackgroundImageLayout = ImageLayout.Zoom;
                            }
                            else if (c.Name.ToString() == "P5")
                            {
                                c.BackgroundImage = Properties.Resources.WhiteQueen;
                                //c.ImageAlign = ContentAlignment.MiddleCenter;
                                c.BackgroundImageLayout = ImageLayout.Zoom;
                            }
                        }
                        else                   //Player 2 pieces
                        {
                            if (c.Name.ToString() == "P2")
                            {
                                c.BackgroundImage = Properties.Resources.BlackRook;
                                //c.ImageAlign = ContentAlignment.MiddleCenter;
                                c.BackgroundImageLayout = ImageLayout.Zoom;
                            }
                            else if (c.Name.ToString() == "P3")
                            {
                                c.BackgroundImage = Properties.Resources.BlackKnight;
                                //c.ImageAlign = ContentAlignment.MiddleCenter;
                                c.BackgroundImageLayout = ImageLayout.Zoom;
                            }
                            else if (c.Name.ToString() == "P4")
                            {
                                c.BackgroundImage = Properties.Resources.BlackBishop;
                                //c.ImageAlign = ContentAlignment.MiddleCenter;
                                c.BackgroundImageLayout = ImageLayout.Zoom;
                            }
                            else if (c.Name.ToString() == "P5")
                            {
                                c.BackgroundImage = Properties.Resources.BlackQueen;
                                //c.ImageAlign = ContentAlignment.MiddleCenter;
                                c.BackgroundImageLayout = ImageLayout.Zoom;
                            }
                        }
                    }
                    else if(GameState.SideBoard.Contains(c.Name.ToString()))
                    {
                        c.Visible = false;
                        c.Enabled = false;
                    }
                    else
                        c.Enabled= false;
                }
            }
        }

        void Winner()
        {
            foreach (Control c in this.Controls) //Turns all buttons visible and one
            {
                if (c is Button || c is Label || c is TextBox)
                {
                    if (GameState.PawnPromoteBoard.Contains(c.Name.ToString()) || GameState.SideBoard.Contains(c.Name.ToString()))
                    {
                        c.Visible = false;
                        c.Enabled = false;
                    }
                    else if (c.Name.ToString() == "Player1Won" && !GameState.whoTurn)
                    {
                        c.Visible = true;
                        c.Enabled = true;
                    }
                    else if (c.Name.ToString() == "Player2Won" && GameState.whoTurn)
                    {
                        c.Visible = true;
                        c.Enabled = true;
                    }
                    else
                    {
                        c.Enabled = false;
                    }

                }
            }
        }

        public void DrawBoard()
        {
            string tag;
            int piece;
            Button targetbutton = null;
            Player1.UpdatePiecesTaken(Pieces);
            Player2.UpdatePiecesTaken(Pieces);
            PL1_points.Text = String.Format($"{Player1.points}");
            PL2_points.Text = String.Format($"{Player2.points}");
            if(Player1.IsInCheck(Pieces, Chessboard[GameState.turnCount]))
                IsInCheck.Text = String.Format("Player 1 is in Check!!");
            else if (Player2.IsInCheck(Pieces, Chessboard[GameState.turnCount]))
                IsInCheck.Text = String.Format("Player 2 is in Check!!");
            else
                IsInCheck.Text = String.Format("");



            //Will go through row and col
            for (int row = 0; row < 8; row++)
                {
                    for (int col = 0; col < 8; col++)
                    {
                        tag = $"{row}{col}";
                        piece = Chessboard[GameState.turnCount].board[row, col];


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
                            if (piece == 0)
                            {
                                targetbutton.BackgroundImage = null;
                            }
                            else if (Pieces[piece].name == "WhitePawn")
                            {
                                //White Pawns
                                targetbutton.BackgroundImage = Properties.Resources.WhitePawn;
                                targetbutton.ImageAlign = ContentAlignment.MiddleCenter;
                                targetbutton.BackgroundImageLayout = ImageLayout.Zoom;
                            }
                            else if (Pieces[piece].name == "WhiteRook")
                            {
                                //White Rook
                                targetbutton.BackgroundImage = Properties.Resources.WhiteRook;
                                targetbutton.ImageAlign = ContentAlignment.MiddleCenter;
                                targetbutton.BackgroundImageLayout = ImageLayout.Zoom;
                            }
                            else if (Pieces[piece].name == "WhiteKnight")
                            {
                                //White Knight
                                targetbutton.BackgroundImage = Properties.Resources.WhiteKnight;
                                targetbutton.ImageAlign = ContentAlignment.MiddleCenter;
                                targetbutton.BackgroundImageLayout = ImageLayout.Zoom;
                            }
                            else if (Pieces[piece].name == "WhiteBishop")
                            {
                                //White Bishop
                                targetbutton.BackgroundImage = Properties.Resources.WhiteBishop;
                                targetbutton.ImageAlign = ContentAlignment.MiddleCenter;
                                targetbutton.BackgroundImageLayout = ImageLayout.Zoom;
                            }
                            else if (Pieces[piece].name == "WhiteQueen")
                            {
                                //White Queen
                                targetbutton.BackgroundImage = Properties.Resources.WhiteQueen;
                                targetbutton.ImageAlign = ContentAlignment.MiddleCenter;
                                targetbutton.BackgroundImageLayout = ImageLayout.Zoom;
                            }
                            else if (Pieces[piece].name == "WhiteKing")
                            {
                                //White King
                                targetbutton.BackgroundImage = Properties.Resources.WhiteKing;
                                targetbutton.ImageAlign = ContentAlignment.MiddleCenter;
                                targetbutton.BackgroundImageLayout = ImageLayout.Zoom;
                            }
                            else if (Pieces[piece].name == "BlackPawn")
                            {
                                //Black Pawn
                                targetbutton.BackgroundImage = Properties.Resources.BlackPawn;
                                targetbutton.ImageAlign = ContentAlignment.MiddleCenter;
                                targetbutton.BackgroundImageLayout = ImageLayout.Zoom;
                            }
                            else if (Pieces[piece].name == "BlackRook")
                            {
                                //Black Rook
                                targetbutton.BackgroundImage = Properties.Resources.BlackRook;
                                targetbutton.ImageAlign = ContentAlignment.MiddleCenter;
                                targetbutton.BackgroundImageLayout = ImageLayout.Zoom;
                            }
                            else if (Pieces[piece].name == "BlackKnight")
                            {
                                //Black Rook
                                targetbutton.BackgroundImage = Properties.Resources.BlackKnight;
                                targetbutton.ImageAlign = ContentAlignment.MiddleCenter;
                                targetbutton.BackgroundImageLayout = ImageLayout.Zoom;
                            }
                            else if (Pieces[piece].name == "BlackBishop")
                            {
                                //Black Rook
                                targetbutton.BackgroundImage = Properties.Resources.BlackBishop;
                                targetbutton.ImageAlign = ContentAlignment.MiddleCenter;
                                targetbutton.BackgroundImageLayout = ImageLayout.Zoom;
                            }
                            else if (Pieces[piece].name == "BlackQueen")
                            {
                                //Black Rook
                                targetbutton.BackgroundImage = Properties.Resources.BlackQueen;
                                targetbutton.ImageAlign = ContentAlignment.MiddleCenter;
                                targetbutton.BackgroundImageLayout = ImageLayout.Zoom;
                            }
                            else if (Pieces[piece].name == "BlackKing")
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
