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
        public override void UpdatePiecesTaken(Piece[] Pieces)
        {
            this.PiecesTaken.Clear();
            for(int k = 17; k <= 32;k++)
            {
                if (Pieces[k].hasBeenTaken == true)
                    this.PiecesTaken.Add(Pieces[k].GetType().Name);
            }
        }

        public override Player Copy()
        {
            return new Player1
            {
                points = this.points,
                Time = this.Time,
                PiecesTaken = this.PiecesTaken,
            };
        }
    }
}
