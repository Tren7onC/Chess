using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Pieces;

namespace Chess.Players
{
    internal class Player2 : Player
    {

        public Player2()
        {
            this.points = 0;
        }
        public override void UpdatePiecesTaken(Piece[] Pieces)
        {
            this.points = 0;
            //this.PiecesTaken.Clear();
            for (int k = 1; k <= 16; k++)
            {
                if (Pieces[k].hasBeenTaken == true)
                {
                    //this.PiecesTaken.Add(Pieces[k].GetType().Name);
                    this.points += Pieces[k].Pointvalue;
                }
            }
        }
        public override bool IsInCheck(Piece[] Pieces, Board Chessboard)
        {
            List<Tuple<int, int>> tmp = new List<Tuple<int, int>>();
            Tuple<int, int> kPos = new Tuple<int, int>(Pieces[32].row, Pieces[32].col); //Player 1 king position

            for (int k = 1; k <= 16; k++)
            {
                tmp.AddRange(Pieces[k].GetPossibleMoves(Chessboard));
            }

            if (tmp.Contains(kPos)) //See if Enemy can move on a King
                return true;
            else
                return false;
        }

        public override Player Copy()
        {
            return new Player2
            {
                points = this.points,
                //PiecesTaken = this.PiecesTaken,
            };
        }
    }
}
