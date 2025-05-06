using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    internal class Game
    {
        public bool GameStart = false;
        public bool whoTurn = true; //Player 1 = true. Player 2 = false

        //Stuff for going back int turns
        public int turnCount = 0;
        

        //Stuff for current selected Piece
        public Tuple<int, int> SelectedPiece;
        public bool PieceSelected = false;


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
