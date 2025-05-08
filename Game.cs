using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chess.Pieces;

namespace Chess
{
    using System.Media;
    internal class Game
    {
        public bool GameStart = false;
        public bool whoTurn = true; //Player 1 = true. Player 2 = false

        //Stuff for going back int turns
        public int turnCount = 0;
        

        //Stuff for current selected Piece
        public Tuple<int, int> SelectedPiece;
        public bool PieceSelected = false;

        //Pawn Promotion
        public int pawnPromotionrow;
        public int pawnPromotioncol;
        public int pawnNumber;

        //Read only values in the Game class
        public List<string> SideBoard { get; } = new List<string> { "Back", "Forward", "Resume", "textBox1",
                                                    "textBox5", "PL1_points", "PL2_points" };

        public List<string> PawnPromoteBoard { get; } = new List<string> { "P1", "P2", "P3", "P4", "P5" };

        public bool SomeoneWon(Piece[] Pieces)
        {
            if (Pieces[16].hasBeenTaken || Pieces[32].hasBeenTaken)        //If a King as been taken
            {
                return true;
            }
            return false;
        }

        public bool CheckPawnPromotion(Piece[] Pieces )
        {
            for(int k = 1; k <= 32; k++)
            {
                if ((Pieces[k].name == "WhitePawn" || Pieces[k].name == "BlackPawn") && (Pieces[k].row == 7 || Pieces[k].row == 0))     //If a Pawn is ready to be promoted
                {
                    pawnPromotionrow = Pieces[k].row;
                    pawnPromotioncol = Pieces[k].col;
                    pawnNumber = k;
                    return true;
                }
            }
            return false;
        }

        public Game Copy()
        {
            return new Game
            {
                GameStart = this.GameStart,
                whoTurn = this.whoTurn,
                turnCount = this.turnCount,
                SelectedPiece = this.SelectedPiece == null ? null : new Tuple<int, int>(this.SelectedPiece.Item1, this.SelectedPiece.Item2),
                PieceSelected = this.PieceSelected
            };

        }
    }
}
