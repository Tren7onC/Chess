using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Pieces;

namespace Chess.Players
{
    internal class Player1 : Player
    {

        public Player1()
        {
            this.points = 0;
        }
        public override void UpdatePiecesTaken(Piece[] Pieces)
        {
            this.points = 0;
            //this.PiecesTaken.Clear();
            for (int k = 17; k <= 32;k++)
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
            Tuple<int, int> kPos = new Tuple<int, int>(Pieces[16].row, Pieces[16].col); //Player 1 king position

            for (int k = 17; k <= 32;k++)
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
            return new Player1
            {
                points = this.points,
                //PiecesTaken = this.PiecesTaken,
            };
        }
    }
}
